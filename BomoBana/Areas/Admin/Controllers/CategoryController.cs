using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories;
using System.Threading;
using System;
using System.Linq;
using Common;
using System.Collections.Generic;
using AutoMapper;
using System.IO;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using ExcelDataReader;
using System.Data;
using EnumValue;
using Common.Utilities;
namespace BomoBana.Areas.Admin
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly IRepository<Category> _CategoryService;
        private readonly IRepository<CategoryProperty> _CategoryPropertyService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CategoryController(IRepository<Category> CategoryService,IRepository<CategoryProperty> CategoryPropertyService,
            IMapper mapper,IWebHostEnvironment hostEnvironment)
        {
            _CategoryService = CategoryService;
            _CategoryPropertyService = CategoryPropertyService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        #region Parent

        #region Index
        public async Task<ActionResult<CategoryViewModel>> Index(CancellationToken cancellationToken)
        {
            List<CategoryListDto> list = await _CategoryService.TableNoTracking.Where(e => e.ParentCategoryId == null)
                .Include(e => e.ChildCategories).ProjectTo<CategoryListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return View(new CategoryViewModel { CategoryListDto = list });
        }
        #endregion
        public async Task<IActionResult> AddCategoryWithExcel(Exel CategoryExel, CancellationToken cancellationToken)
        {

            if (CategoryExel.File.Length < 1)
            {
                AddNotification(NotifyType.Warning, OperationMessage.SelectFile.ToDisplay(), true);
                return RedirectToAction("Index");
            }
            try
            {
                string fName = Guid.NewGuid().ToString("D") + CategoryExel.File.FileName;
                using (var fileStream = new FileStream(Path.Combine(webHostEnvironment.WebRootPath,
                            ApplicationPathes.CategoryExel.VirtualFolder, fName), FileMode.Create))
                {
                    await CategoryExel.File.CopyToAsync(fileStream);
                    List<Category> CategoryExcel = new List<Category>();
                    using (var reader = ExcelReaderFactory.CreateOpenXmlReader(fileStream: fileStream))
                    {
                        var items = reader.AsDataSet().Tables[index: 0].AsEnumerable().Skip(count: 0).ToList();
                        CategoryExcel = items.Select(x => new Category()
                        {
                            Title = x[columnIndex: 0]?.ToString(),
                            Description = "",
                            IsActive = true,
                            ParentCategoryId = null,
                            Image = x[columnIndex: 1]?.ToString(),
                        }).ToList();
                    }
                    var Listcategory = _CategoryService.TableNoTracking.Where(e => e.ParentCategoryId == null).ToList();
                    List<Category> resultlist = new List<Category>();
                    foreach (var item in CategoryExcel)
                    {
                        string name = item.Title;
                        if (!Listcategory.Any(e => e.Title.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)))
                        {
                            item.Title = name.Trim();
                            resultlist.Add(item);
                        }

                    }
                    await _CategoryService.AddRangeAsync(resultlist, cancellationToken);
                    SuccessNotification(OperationMessage.FileSaved.ToDisplay(), true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _CategoryService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.IsActive = false;
                    await _CategoryService.UpdateAsync(model, cancellationToken);
                    return new JsonResult(new { confirm = true, id = Id, name = model.Title });
                }
                else
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.ModelStateIsNotValid.ToDisplay() });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { confirm = false, message = OperationMessage.OperationFailed.ToDisplay() });

            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateActiveCategory(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _CategoryService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    if (model.IsActive)
                    {
                        model.IsActive = false;
                        await _CategoryService.UpdateAsync(model, cancellationToken);
                        return new JsonResult(new { confirm = true, IsActive = false , IsActivemsg ="غیر فعال",
                            Id = model.Id,Name = model.Title });
                    }
                    else
                    {
                        model.IsActive = true;
                        await _CategoryService.UpdateAsync(model, cancellationToken);
                        return new JsonResult(new { confirm = true, IsActive = true, IsActivemsg = "فعال",
                            Id = model.Id,Name = model.Title });
                    }
                }
                else
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.ModelStateIsNotValid.ToDisplay() });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { confirm = false, message = OperationMessage.OperationFailed.ToDisplay() });

            }

        }
        #endregion

        #region SubParent
        public async Task<ActionResult<SubCategoryListDto>> SubParent(int CategoryId, CancellationToken cancellationToken)
        {
            List<SubCategoryListDto> list = await _CategoryService.TableNoTracking.Where(e => e.ParentCategoryId == CategoryId)
                .ProjectTo<SubCategoryListDto>(_mapper.ConfigurationProvider)
                .OrderBy(e => e.CreateDate).ToListAsync(cancellationToken);
            Category category = await _CategoryService.TableNoTracking.FirstOrDefaultAsync(e => e.Id == CategoryId);
            List<Category> Child = _CategoryService.TableNoTracking.Include(e => e.ParentCategory)
               .Where(e => e.ParentCategory.ParentCategoryId == CategoryId).ToList();
            return View(new SubCategoryViewModel
            {
                CategoryId = CategoryId,
                CategoryName = category.Title,
                Image = category.Image,
                SubCategoryListDto = list,
                Child = Child,
            });
        }
        #endregion

        #region ChildParent
        public async Task<ActionResult<ChildCategoryListDto>> ChildParent(int SubCategoryId, CancellationToken cancellationToken)
        {
            List<ChildCategoryListDto> list = await _CategoryService.TableNoTracking.Where(e => e.ParentCategoryId == SubCategoryId)
                .ProjectTo<ChildCategoryListDto>(_mapper.ConfigurationProvider)
                .OrderBy(e => e.CreateDate).ToListAsync(cancellationToken);
            Category category = await _CategoryService.TableNoTracking.Include(e => e.ParentCategory).FirstOrDefaultAsync(e => e.Id == SubCategoryId);
            return View(new ChildCategoryViewModel
            {
                SubCategoryId = SubCategoryId,
                SubCategoryName = category.Title,
                Image = category.ParentCategory.Image,
                ChildCategoryListDto = list,
                CategoryId = category.ParentCategory.Id,
                CategoryName = category.ParentCategory.Title
            });
        }
        #endregion

        #region CreateCategory
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto categoryDto, [FromForm]int? LoadCategory, [FromForm]int?
            LoadSubCategory, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddNotification(NotifyType.Warning, OperationMessage.ModelStateIsNotValid.ToDisplay(), true);
                    return View(categoryDto);
                }
                if (LoadCategory == null)
                    LoadCategory = 0;
                if (LoadSubCategory == null)
                    LoadSubCategory = 0;
                 var list =await _CategoryService.TableNoTracking.ToListAsync(cancellationToken);
                if (list.Any(e => e.Title.Equals(categoryDto.Title.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    AddNotification(NotifyType.Info, OperationMessage.DoestExist.ToDisplay(), true);
                    return View(categoryDto);
                }
                string fName = Guid.NewGuid().ToString("D") + categoryDto.Image.FileName;
                var model = categoryDto.ToEntity(_mapper);
                model.Image = fName;               
                if (categoryDto.Image.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(webHostEnvironment.WebRootPath,
                        ApplicationPathes.Category.VirtualUploadFolder, fName), FileMode.Create))
                    {
                        await categoryDto.Image.CopyToAsync(fileStream);
                    }
                    if (LoadSubCategory == 0 & LoadCategory == 0)
                    {
                        model.ParentCategoryId = null;
                        var result = _CategoryService.AddAsync(model, cancellationToken);
                        SuccessNotification(OperationMessage.FileSaved.ToDisplay(), true);
                        return RedirectToAction("Index");
                    }
                    else if (LoadSubCategory == 0 && LoadCategory > 0)
                    {
                        model.ParentCategoryId = LoadCategory;
                        var result = _CategoryService.AddAsync(model, cancellationToken);
                        SuccessNotification(OperationMessage.FileSaved.ToDisplay(), true);
                        return RedirectToAction("SubParent",new { CategoryId = LoadCategory });
                    }
                    else if (LoadSubCategory > 0 && LoadCategory > 0)
                    {
                        model.ParentCategoryId = LoadSubCategory;
                        var result = _CategoryService.AddAsync(model, cancellationToken);
                        SuccessNotification(OperationMessage.FileSaved.ToDisplay(), true);
                        return RedirectToAction("ChildParent", new { SubCategoryId = LoadSubCategory });
                    }
                    else
                    {
                        SuccessNotification(OperationMessage.FileSaved.ToDisplay(), true);
                        return View();
                    }
                }
                else
                {
                    AddNotification(NotifyType.Warning, OperationMessage.ModelStateIsNotValid.ToDisplay(), true);
                    return View(categoryDto);
                }          
            }
            catch (Exception ex)
            {
                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return View(categoryDto);
            }
        }
        #endregion

        #region LoadAjax
        public async Task<IActionResult> LoadCategory(string item, CancellationToken cancellationToken)
        {
            try
            {
                List<LoadAjaxCategoryDto> list = await _CategoryService.TableNoTracking.Where(e => e.ParentCategoryId == null
                && e.IsActive).ProjectTo<LoadAjaxCategoryDto>(_mapper.ConfigurationProvider).OrderBy(e => e.text)
               .ToListAsync(cancellationToken);
                if (!(string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item)))
                {
                    list = list.Where(x => x.text.ToLower().StartsWith(item.ToLower())).ToList();
                }
                SuccessNotification(OperationMessage.FileSaved.ToDisplay(), true);
                return new JsonResult(new { items = list });
            }
            catch (Exception)
            {
                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return new JsonResult(new { items = false });

            }

        }
        public async Task<IActionResult> LoadSubCategory(string item,int categoryid, CancellationToken cancellationToken)
        {
            try
            {
                List<LoadAjaxCategoryDto> list = await _CategoryService.TableNoTracking.Where(e => e.ParentCategoryId == categoryid
                && e.IsActive).ProjectTo<LoadAjaxCategoryDto>(_mapper.ConfigurationProvider).OrderBy(e => e.text)
               .ToListAsync(cancellationToken);
                if (!(string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item)))
                {
                    list = list.Where(x => x.text.ToLower().StartsWith(item.ToLower())).ToList();
                }
                SuccessNotification(OperationMessage.FileSaved.ToDisplay(), true);
                if (list == null)
                {
                    return new JsonResult(new { items = false });

                }
                else
                {
                    return new JsonResult(new { items = list });

                }
            }
            catch (Exception)
            {
                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return new JsonResult(new { items = false });

            }

        }

        #endregion

        #region Property
        public async Task<IActionResult> AddProperty(CategoryPropertyViewModel ModelDto, int CategoryId, CancellationToken cancellationToken)
        {
            try
            {
                if (String.IsNullOrEmpty(ModelDto.TitleValue.Trim()))
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.ModelStateIsNotValid.ToDisplay() });
                }
                var ListProvince = _CategoryPropertyService.TableNoTracking.Where(e => e.TitleValue == ModelDto.TitleValue).ToList();
                if (ListProvince.Any(e => e.TitleValue.Equals(ModelDto.TitleValue.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.DoestExist.ToDisplay() });
                }
                var property = new CreatePropertyCategoryDto
                {
                    CategoryId = CategoryId,
                    TitleValue = ModelDto.TitleValue,
                    Type = EnumExtensions.ParseEnum<TypeOfInputValue>(ModelDto.Type).ToString()
                };
                var model = property.ToEntity(_mapper);
                await _CategoryPropertyService.AddAsync(model, cancellationToken);
                string name = "'" + model.TitleValue + "'";
                string Url = "'Category/DeleteProperty'";
                return new JsonResult(new
                {
                    confirm = true,
                    message = OperationMessage.OperationSucceed.ToDisplay(),
                    Name = name,
                    Url = Url,
                    Id = model.Id
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { confirm = false, message = OperationMessage.OperationFailed.ToDisplay() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProperty(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _CategoryPropertyService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    model.IsDeleted = true;
                    await _CategoryPropertyService.UpdateAsync(model, cancellationToken);
                    return new JsonResult(new { confirm = true, id = Id, name = model.TitleValue });
                }
                else
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.ModelStateIsNotValid.ToDisplay() });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { confirm = false, message = OperationMessage.OperationFailed.ToDisplay() });

            }

        }
        public async Task<IActionResult> ListProperty(int CategoryId, CancellationToken cancellationToken)
        {
            try
            {
                List<ProppertyCategoryListDto> list = await _CategoryPropertyService.TableNoTracking.Where(e => e.CategoryId == CategoryId
                && !e.IsDeleted)
                    /*.Include(e => e.ProductProperty)*/.ProjectTo<ProppertyCategoryListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(e => e.TitleValue).ToListAsync(cancellationToken);              
                var model = new CategoryPropertyViewModel
                {
                    ProppertyCategoryList = list,
                    CategoryId = CategoryId
                };
                return PartialView("_CreateProperty", model);
            }

            catch (Exception ex)
            {
                return new JsonResult(new { items = false, message = OperationMessage.OperationFailed.ToDisplay() });
            }

        }
        #endregion
    }
}