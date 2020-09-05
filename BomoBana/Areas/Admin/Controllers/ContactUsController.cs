using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BomoBana.Models;
using Data.Repositories;
using Entities;
using System.Threading;
using System.Threading.Tasks;
using BomoBana.Areas.Admin;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System;
using EnumValue;
using System.IO;
using Common;
using Common.Utilities;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace BomoBana.Area.Admin
{
    [Area("Admin")]
    [Authorize]
    public class ContactUsController : BaseController
    {
        private readonly IRepository<ContactUs> _ContactUsService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ContactUsController(IRepository<ContactUs> ContactUsService, IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _ContactUsService = ContactUsService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }
        #region Index
        public async Task<ActionResult<ContactUsViewModel>> Index(CancellationToken cancellationToken)
        {
            try
            {
                List<ContactUsListDto> list = await _ContactUsService.TableNoTracking.Where(e => !e.IsDeleted)
              .ProjectTo<ContactUsListDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);
                return View(new ContactUsViewModel { ContactUsListDto = list });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        #endregion
        #region LoadDetail
        public async Task<IActionResult> DetailsPage(int Id, CancellationToken cancellationToken)
        {
            try
            {
                ContactUs details = await _ContactUsService.TableNoTracking
                    .FirstOrDefaultAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
                details.IsVisited = true;
                await _ContactUsService.UpdateAsync(details, cancellationToken);

                DetailContactUsDto model = new DetailContactUsDto
                {
                    FullName = details.FullName,
                    Content = details.Content,
                    Subject = details.Subject,
                };
                return PartialView("_ContentPage", model);
            }

            catch (Exception ex)
            {
                return new JsonResult(new { items = false, message = OperationMessage.OperationFailed.ToDisplay() });
            }

        }

        #endregion
        #region Delete
        [HttpPost]
        public async Task<IActionResult> DeleteContactUs(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _ContactUsService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    await _ContactUsService.DeleteAsync(model, cancellationToken);                   
                    return new JsonResult(new { confirm = true, id = Id, name = model.FullName });
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

    }
}