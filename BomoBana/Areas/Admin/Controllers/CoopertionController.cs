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
    public class CoopertionController : BaseController
    {
        private readonly IRepository<Job> _JobService;
        private readonly IRepository<JobRequest> _JobRequestService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CoopertionController(IRepository<Job> JobService, IRepository<JobRequest> JobRequestService, IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _JobService = JobService;
            _JobRequestService = JobRequestService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }
        #region Index
        public async Task<ActionResult<CoopertionViewModel>> Index(CancellationToken cancellationToken)
        {
            try
            {
                List<CoopertionListDto> list = await _JobService.TableNoTracking.Where(e => !e.IsDeleted && e.IsActive)
               .ProjectTo<CoopertionListDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
                return View(new CoopertionViewModel { CoopertionListDto = list });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
           
        }

        #endregion

        #region Create
        public async Task<IActionResult> Create(CreateCoopertionDto ModelDto, CancellationToken cancellationToken)
        {
            try
            {
                if (String.IsNullOrEmpty(ModelDto.Title.Trim()))
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.ModelStateIsNotValid.ToDisplay() });
                }
                var ListCoopertion = _JobService.TableNoTracking.Where(e => e.Title == ModelDto.Title).ToList();
                if (ListCoopertion.Any(e => e.Title.Equals(ModelDto.Title.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.DoestExist.ToDisplay() });
                }
                var model = ModelDto.ToEntity(_mapper);
                await _JobService.AddAsync(model, cancellationToken);
                string name = "'" + model.Title + "'";
                string Url = "'DeleteCoopertion'";
                string isactive = "<span class='label label-danger custom-label-acrive font-14 font-weight-100'>غیرفعال</span>";
                string deactive = "<span class='label label-success custom-label-acrive font-14 font-weight-100'>فعال</span>";

                string isrequired = "<span class='badge badge-danger'>فوری</span>";

                return new JsonResult(new
                {
                    confirm = true,
                    message = OperationMessage.OperationSucceed.ToDisplay(),
                    Name = name,
                    Id = model.Id,
                    isactive = (model.IsActive ? isactive : deactive),
                    isrequired = (model.IsRequired ? isrequired : ""),
                    nameCoopertion = model.Title
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { confirm = false, message = OperationMessage.OperationFailed.ToDisplay() });
            }
        }
        #endregion
        #region Delete
        [HttpPost]
        public async Task<IActionResult> DeleteCoopertion(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _JobService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.IsActive = false;
                    await _JobService.UpdateAsync(model, cancellationToken);

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
        public async Task<IActionResult> UpdateActiveCoopertion(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _JobService.GetByIdAsync(cancellationToken, Id);
                if (model != null)
                {
                    if (model.IsActive)
                    {
                        model.IsActive = false;
                        await _JobService.UpdateAsync(model, cancellationToken);
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
                        await _JobService.UpdateAsync(model, cancellationToken);
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

    }
}