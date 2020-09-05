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
    public class StaticPageController : BaseController
    {

        private readonly IRepository<StaticPage> _staticPageService;
        private readonly IMapper _mapper;

        public StaticPageController(IRepository<StaticPage> staticPageService, IMapper mapper)
        {
            _staticPageService = staticPageService;
            _mapper = mapper;
        }

        #region Index
        public async Task<ActionResult<StaticPageDto>> Index(CancellationToken cancellationToken)
        {
            try
            {
                var list = await _staticPageService.TableNoTracking.Where(e => !e.IsDeleted)
               .ProjectTo<StaticPageDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
                ViewData["Url"] = $"{Request.Host}{Request.PathBase}";
                ViewData["BaseUrl"] = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
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
        public async Task<IActionResult> Create(AddStaticPageDto staticpageDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddNotification(NotifyType.Warning, OperationMessage.ModelStateIsNotValid.ToDisplay(), true);
                    return View(staticpageDto);
                }

                var list = await _staticPageService.TableNoTracking.ToListAsync(cancellationToken);
                if (list.Any(e => e.PageTitle.Equals(staticpageDto.PageTitle.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    AddNotification(NotifyType.Info, OperationMessage.DoestExist.ToDisplay(), true);
                    return View(staticpageDto);
                }
                var model = staticpageDto.ToEntity(_mapper);
                model.StaticPageType = StaticPageType.Other;
                await _staticPageService.AddAsync(model, cancellationToken);
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
        public async Task<IActionResult> DeleteStaticPage(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _staticPageService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    await _staticPageService.DeleteAsync(model, cancellationToken);
                    return new JsonResult(new { confirm = true, id = Id, name = model.PageTitle });
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
        public async Task<IActionResult> UpdateActiveStaticPage(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _staticPageService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    if (model.IsActive)
                    {
                        model.IsActive = false;
                        await _staticPageService.UpdateAsync(model, cancellationToken);
                        return new JsonResult(new
                        {
                            confirm = true,
                            IsActive = false,
                            IsActivemsg = "غیر فعال",
                            Id = model.Id,
                            Name = model.PageTitle
                        });
                    }
                    else
                    {
                        model.IsActive = true;
                        await _staticPageService.UpdateAsync(model, cancellationToken);
                        return new JsonResult(new
                        {
                            confirm = true,
                            IsActive = true,
                            IsActivemsg = "فعال",
                            Id = model.Id,
                            Name = model.PageTitle
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
                StaticPage details = await _staticPageService.TableNoTracking
                    .FirstOrDefaultAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
                DetailStaticPageDto model = new DetailStaticPageDto
                {
                    Content = details.Content,
                    Caption = details.Caption,
                    PageTitle = details.PageTitle,
                    Url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}" +"/"+details.Url
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
        public async Task<ActionResult<EditStaticPageDto>> Edit(int Id,CancellationToken cancellationToken)
        {
            try
            {
                var page = await _staticPageService.GetByIdAsync(cancellationToken, Id);

                EditStaticPageDto model = new EditStaticPageDto
                {
                    PageTitle = page.PageTitle,
                    Caption = page.Caption,
                    Url = page.Url,
                    Content = page.Content,
                    DescriptionSeo = page.DescriptionSeo,
                    IsActive = page.IsActive,
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
        public async Task<IActionResult> Edit(int Id, EditStaticPageDto ModelDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddNotification(NotifyType.Warning, OperationMessage.ModelStateIsNotValid.ToDisplay(), true);
                    return View(ModelDto);
                }
                var model = await _staticPageService.GetByIdAsync(cancellationToken, Id);
                
                if (model != null)
                {
                    model.PageTitle = ModelDto.PageTitle;
                    model.Caption = ModelDto.Caption;
                    model.Url = ModelDto.Url;
                    model.Content = ModelDto.Content;
                    model.DescriptionSeo = ModelDto.DescriptionSeo;
                    model.IsActive = ModelDto.IsActive;
                    model.Keywords = ModelDto.Keywords;
                    await _staticPageService.UpdateAsync(model, cancellationToken);
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