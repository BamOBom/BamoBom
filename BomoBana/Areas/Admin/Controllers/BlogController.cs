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
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using EnumValue;
using Common.Utilities;
using System.IO;
namespace BomoBana.Areas.Admin
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : BaseController
    {
        private readonly IRepository<Blog> _BlogService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public BlogController(IRepository<Blog> BlogService, IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _BlogService = BlogService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;

        }

        #region Index
        public async Task<ActionResult<BlogDto>> Index(CancellationToken cancellationToken)
        {
            try
            {
                var list = await _BlogService.TableNoTracking.Where(e => !e.IsDeleted)
               .ProjectTo<BlogDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
                return View(list);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        #endregion

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddBlogDto BlogDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddNotification(NotifyType.Warning, OperationMessage.ModelStateIsNotValid.ToDisplay(), true);
                    return View(BlogDto);
                }

                var list = await _BlogService.TableNoTracking.ToListAsync(cancellationToken);
                if (list.Any(e => e.Title.Equals(BlogDto.Title.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    AddNotification(NotifyType.Info, OperationMessage.DoestExist.ToDisplay(), true);
                    return View(BlogDto);
                }
                var model = BlogDto.ToEntity(_mapper);
                if (BlogDto.Image.Length > 0)
                {
                    string LName = Guid.NewGuid().ToString("D") + BlogDto.Image.FileName;
                    using (var fileStream = new FileStream(Path.Combine(webHostEnvironment.WebRootPath,
                        ApplicationPathes.Blog.VirtualUploadFolder, LName), FileMode.Create))
                    {
                        await BlogDto.Image.CopyToAsync(fileStream);
                        model.Image = LName;
                    }
                }
                await _BlogService.AddAsync(model, cancellationToken);
                AddNotification(NotifyType.Success, OperationMessage.OperationSucceed.ToDisplay(), true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> DeleteBlog(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _BlogService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    await _BlogService.DeleteAsync(model, cancellationToken);
                    string path = webHostEnvironment.WebRootPath + ApplicationPathes.Blog.VirtualDeleteFile + model.Image;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
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

        #endregion

        #region Active/DeActive
        [HttpPost]
        public async Task<IActionResult> UpdateActiveBlog(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _BlogService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    if (model.IsActive)
                    {
                        model.IsActive = false;
                        await _BlogService.UpdateAsync(model, cancellationToken);
                        return new JsonResult(new
                        {
                            confirm = true,
                            IsActive = false,
                            IsActivemsg = "غیر فعال",
                            Id = model.Id,
                            Name = model.Title
                        });
                    }
                    else
                    {
                        model.IsActive = true;
                        await _BlogService.UpdateAsync(model, cancellationToken);
                        return new JsonResult(new
                        {
                            confirm = true,
                            IsActive = true,
                            IsActivemsg = "فعال",
                            Id = model.Id,
                            Name = model.Title
                        });
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

        #region LoadDetail
        public async Task<IActionResult> DetailsPage(int Id, CancellationToken cancellationToken)
        {
            try
            {
                Blog details = await _BlogService.TableNoTracking
                    .FirstOrDefaultAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
                DetailBlogDto model = new DetailBlogDto
                {
                    Content = details.Content,
                    Caption = details.Caption,
                    Title = details.Title,
                    Image = details.Image,
                };
                return PartialView("_ContentPage", model);
            }

            catch (Exception ex)
            {
                return new JsonResult(new { items = false, message = OperationMessage.OperationFailed.ToDisplay() });
            }

        }

        #endregion

        #region Edit
        public async Task<ActionResult<EditBlogDto>> Edit(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var page = await _BlogService.GetByIdAsync(cancellationToken, Id);

                EditBlogDto model = new EditBlogDto
                {
                    Title = page.Title,
                    Caption = page.Caption,
                    ImageName = page.Image,
                    Content = page.Content,
                    DescriptionSeo = page.DescriptionSeo,
                    IsActive = page.IsActive,
                    SetSliderHome = page.SetSliderHome,
                    Id = page.Id,
                    Keywords = page.Keywords,
                };

                return View(model);
            }
            catch (Exception e)
            {

                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, EditBlogDto ModelDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddNotification(NotifyType.Warning, OperationMessage.ModelStateIsNotValid.ToDisplay(), true);
                    return View(ModelDto);
                }
                var model = await _BlogService.GetByIdAsync(cancellationToken, Id);

                if (model != null)
                {
                    model.Title = ModelDto.Title;
                    model.Caption = ModelDto.Caption;
                    model.Content = ModelDto.Content;
                    model.DescriptionSeo = ModelDto.DescriptionSeo;
                    model.IsActive = ModelDto.IsActive;
                    model.SetSliderHome = ModelDto.SetSliderHome;
                    model.Keywords = ModelDto.Keywords;
                    if (ModelDto.Image.Length > 0)
                    {
                        string LName = Guid.NewGuid().ToString("D") + ModelDto.Image.FileName;
                        using (var fileStream = new FileStream(Path.Combine(webHostEnvironment.WebRootPath,
                            ApplicationPathes.Blog.VirtualUploadFolder, LName), FileMode.Create))
                        {
                            string path = webHostEnvironment.WebRootPath + ApplicationPathes.Blog.VirtualDeleteFile + model.Image;
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                            await ModelDto.Image.CopyToAsync(fileStream);
                            model.Image = LName;
                        }
                    }
                    await _BlogService.UpdateAsync(model, cancellationToken);
                    AddNotification(NotifyType.Success, OperationMessage.OperationSucceed.ToDisplay(), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(NotifyType.Success, OperationMessage.ModelStateIsNotValid.ToDisplay(), true);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return RedirectToAction("Error", "Home");
            }
        }

        #endregion
    }
}