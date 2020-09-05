using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using BomoBana.Areas.Admin;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using BomoBana.Controllers;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using EnumValue;
using BomoBana.Areas.Admin;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using System;
using System.IO;
using Common;
using Common.Utilities;
using AutoMapper.QueryableExtensions;

namespace BomoBana.Area.Admin
{
    [Area("Admin")]
    [AllowAnonymous]
    public class UploadController : BaseController
    {
        private readonly IRepository<UploadCenter> _UploadCenterService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UploadController(IRepository<UploadCenter> UploadCenterService, IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _UploadCenterService = UploadCenterService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(UploadCenterDto model, CancellationToken cancellationToken)
        {
            try
            {
                var fileupload = model.ToEntity(_mapper);
                if (model.File.Length > 0)
                {
                    string LName = Guid.NewGuid().ToString("D") + model.File.FileName;
                    using (var fileStream = new FileStream(Path.Combine(webHostEnvironment.WebRootPath,
                        ApplicationPathes.UploadCenter.VirtualUploadFolder, LName), FileMode.Create))
                    {
                        model.File.CopyTo(fileStream);
                        fileupload.File = LName;
                        fileupload.Url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}" +
                            "/Images/UploadCenter/" + LName;
                    }
                }
                await _UploadCenterService.AddAsync(fileupload, cancellationToken);

                return new JsonResult(new
                {
                    confirm = true,
                    message = OperationMessage.OperationSucceed.ToDisplay(),
                    file = fileupload.File,
                    url = fileupload.Url

                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { confirm = false, message = OperationMessage.OperationFailed.ToDisplay() });
            }
        }

        public async Task<ActionResult<UploadListDto>> List(CancellationToken cancellationToken)
        {
            try
            {
                List<UploadListDto> list = await _UploadCenterService.TableNoTracking.Where(e => !e.IsDeleted)
               .ProjectTo<UploadListDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
                return View(list);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }

    }
}