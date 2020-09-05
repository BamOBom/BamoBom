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
using EnumValue;
using ExcelDataReader;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using Common.Utilities;

namespace BomoBana.Areas.Admin
{
    [Area("Admin")]
    [Authorize]
    public class LocationsController : BaseController
    {
        private readonly IRepository<Country> _CountryService;
        private readonly IRepository<Province> _ProvinceService;
        private readonly IRepository<City> _CityService;
        private readonly IRepository<Distric> _DistricService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public LocationsController(IRepository<Country> CountryService, IRepository<Province> ProvinceService,
            IRepository<City> CityService, IRepository<Distric> DistricService, IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _CountryService = CountryService;
            _ProvinceService = ProvinceService;
            _CityService = CityService;
            _DistricService = DistricService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }
        #region Country

        #region Index
        public async Task<ActionResult<CountryViewModel>> Index(CancellationToken cancellationToken)
        {

            List<CountryListDto> list = await _CountryService.TableNoTracking.Include(e => e.Provinces)
                .ProjectTo<CountryListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return View(new CountryViewModel { CountryListDto = list });
        }

        #endregion

        #endregion

        #region Province

        #region Index
        public async Task<ActionResult<ProvinceListDto>> Province(int CountryId, CancellationToken cancellationToken)
        {

            List<ProvinceListDto> list = await _ProvinceService.TableNoTracking.Where(e => e.CountryId == CountryId)
                .ProjectTo<ProvinceListDto>(_mapper.ConfigurationProvider)
                .OrderBy(e=>e.Name).ToListAsync(cancellationToken);
            Country country = await _CountryService.TableNoTracking.FirstOrDefaultAsync(e => e.Id == CountryId);
            List<City> city = _CityService.TableNoTracking.Include(e=>e.User)/*.Include(e=>e.)*/
                .Where(e => e.Province.CountryId == CountryId).ToList();
            return View(new ProvinceViewModel
            {
                ProvinceListDto = list,
                CountryName = country.Name,
                Flag = country.Flag,
                ISO = country.ISO,
                CountryId = country.Id,
                City = city
            });
        }
        [HttpPost]   
        public async Task<IActionResult> AddProvince(ProvinceViewModel ModelDto, int CountryId, CancellationToken cancellationToken)
        {
            try
            {
                if (String.IsNullOrEmpty(ModelDto.Name.Trim()))
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.ModelStateIsNotValid.ToDisplay() });
                }
                var ListProvince = _ProvinceService.TableNoTracking.Where(e => e.CountryId == CountryId).ToList();
                if (ListProvince.Any(e => e.Name.Equals(ModelDto.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.DoestExist.ToDisplay() });
                }
                var province = new CreateProvinceDto
                {
                    Name = ModelDto.Name
                };
                var model = province.ToEntity(_mapper);
                model.CountryId = CountryId;
                await _ProvinceService.AddAsync(model, cancellationToken);
                SuccessNotification(OperationMessage.OperationSucceed.ToDisplay(), true);
                string name = "'" + model.Name + "'";
                string Url = "'DeleteProvince'";
                return new JsonResult(new { confirm = true, message = OperationMessage.OperationSucceed.ToDisplay(),
                             Id = model.Id, Name = model.Name, Url = Url, NameData = name });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { confirm = false, message = OperationMessage.OperationFailed.ToDisplay() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProvince(int Id, CancellationToken cancellationToken)
        {
                var model = await _ProvinceService.GetByIdAsync(cancellationToken, Id);
                await _ProvinceService.DeleteAsync(model, cancellationToken);
                return new JsonResult(new { id = Id, name = model.Name });
        }
        #endregion

        #region AddProvinceToCountryWithExcel

        [HttpPost/*, CustomActionName("افزودن استان توسط اکسل")*/]
        public async Task<IActionResult> AddProvinceToCountryWithExcel(Exel ProvinceExel, int CountryId, CancellationToken cancellationToken)
        {

                if (ProvinceExel.File.Length < 1)
            {
                AddNotification(NotifyType.Warning, OperationMessage.SelectFile.ToDisplay(), true);
                return RedirectToAction("Province", new { CountryId = CountryId });
            }
            try
            {
                string fName = Guid.NewGuid().ToString("D") + ProvinceExel.File.FileName;
                using (var fileStream = new FileStream(Path.Combine(webHostEnvironment.WebRootPath,
                            ApplicationPathes.LocationExel.VirtualFolder, fName), FileMode.Create))
                {
                    await ProvinceExel.File.CopyToAsync(fileStream);
                    List<Province> ProvinceExcel = new List<Province>();
                    using (var reader = ExcelReaderFactory.CreateOpenXmlReader(fileStream: fileStream))
                    {
                        var items = reader.AsDataSet().Tables[index: 0].AsEnumerable().Skip(count: 0).ToList();

                        ProvinceExcel = items.Select(x => new Province()
                        {
                            Name = x[columnIndex: 0]?.ToString(),
                            CountryId = CountryId

                       }).ToList();
                    }
                    var ListProvince = _ProvinceService.TableNoTracking.Where(e => e.CountryId == CountryId)
                        .ToList();
                    List<Province> resultlist = new List<Province>();
                    foreach (var item in ProvinceExcel)
                    {
                        string name = item.Name;
                        if (!ListProvince.Any(e=>e.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)))
                        {
                            item.Name = name.Trim();
                            resultlist.Add(item);
                        }

                    }
                    await _ProvinceService.AddRangeAsync(resultlist, cancellationToken);
                    SuccessNotification(OperationMessage.FileSaved.ToDisplay(), true);
                    return RedirectToAction("Province", new { CountryId = CountryId });
                }
            }
            catch (Exception ex)
            {
                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return RedirectToAction("Province", new { CountryId = CountryId });
            }

        }
  
        //usage in above situation
        #endregion

        #endregion

        #region City
        public async Task<ActionResult<CityListDto>> City(int ProvinceId, CancellationToken cancellationToken)
        {

            List<CityListDto> list = await _CityService.TableNoTracking.Where(e => e.ProvinceId == ProvinceId)
                .ProjectTo<CityListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            Province province = await _ProvinceService.TableNoTracking.Include(e => e.Country)
                .FirstOrDefaultAsync(e => e.Id == ProvinceId);
            return View(new CityViewModel
            {
                CountryId = province.CountryId,
                CityListDto = list,
                CountryName = province.Country.Name,
                Flag = province.Country.Flag,
                ProvinceName = province.Name,
                ISO = province.Country.ISO,
                ProvinceId = province.Id
            });
        }

        public async Task<IActionResult> AddCity(CityViewModel ModelDto, int ProvinceId, CancellationToken cancellationToken)
        {
            try
            {
                if (String.IsNullOrEmpty(ModelDto.Name.Trim()))
                {
                    return new JsonResult(new { confirm = false , message = OperationMessage.ModelStateIsNotValid.ToDisplay()});
                }
                var ListProvince = _CityService.TableNoTracking.Where(e => e.ProvinceId == ProvinceId).ToList();
                if (ListProvince.Any(e => e.Name.Equals(ModelDto.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new JsonResult(new { confirm = false, message = OperationMessage.DoestExist.ToDisplay() });
                }
                var city = new CreateCityDto
                {
                    Name = ModelDto.Name
                };
                var model = city.ToEntity(_mapper);
                model.ProvinceId = ProvinceId;
                await _CityService.AddAsync(model, cancellationToken);
                string name = "'" + model.Name + "'";
                string Url = "'DeleteCity'";
                return new JsonResult(new { confirm = true, message = OperationMessage.OperationSucceed.ToDisplay(),
                    Id = model.Id, Name = model.Name, Url = Url, NameData = name });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { confirm = false, message = OperationMessage.OperationFailed.ToDisplay() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(int Id, CancellationToken cancellationToken)
        {
            var model = await _CityService.GetByIdAsync(cancellationToken, Id);
            await _CityService.DeleteAsync(model, cancellationToken);
            return new JsonResult(new { id = Id, name = model.Name });
        }

        #region AddCityToProvinceWithExcel

        [HttpPost/*, CustomActionName("افزودن استان توسط اکسل")*/]
        public async Task<IActionResult> AddCityToProvinceWithExcel(Exel CityExel, int ProvinceId, CancellationToken cancellationToken)
        {

            if (CityExel.File.Length < 1)
            {
                AddNotification(NotifyType.Warning, OperationMessage.SelectFile.ToDisplay(), true);
                return RedirectToAction("City", new { ProvinceId = ProvinceId });
            }
            try
            {
                string fName = Guid.NewGuid().ToString("D") + CityExel.File.FileName;
                using (var fileStream = new FileStream(Path.Combine(webHostEnvironment.WebRootPath,
                            ApplicationPathes.LocationExel.VirtualFolder, fName), FileMode.Create))
                {
                    await CityExel.File.CopyToAsync(fileStream);
                    List<City> CityExcel = new List<City>();
                    using (var reader = ExcelReaderFactory.CreateOpenXmlReader(fileStream: fileStream))
                    {
                        var items = reader.AsDataSet().Tables[index: 0].AsEnumerable().Skip(count: 0).ToList();

                        CityExcel = items.Select(x => new City()
                        {
                            Name = x[columnIndex: 0]?.ToString(),
                            ProvinceId = ProvinceId

                        }).ToList();
                    }
                    var ListCity = _CityService.TableNoTracking.Where(e => e.ProvinceId == ProvinceId)
                        .ToList();
                    List<City> resultlist = new List<City>();
                    foreach (var item in CityExcel)
                    {
                        string name = item.Name;
                        if (!ListCity.Any(e => e.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)))
                        {
                            item.Name = name.Trim();
                            resultlist.Add(item);
                        }

                    }
                    await _CityService.AddRangeAsync(resultlist, cancellationToken);
                    SuccessNotification(OperationMessage.FileSaved.ToDisplay(), true);
                    return RedirectToAction("City", new { ProvinceId = ProvinceId });
                }
            }
            catch (Exception ex)
            {
                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return RedirectToAction("City", new { ProvinceId = ProvinceId });
            }

        }

        //usage in above situation
        #endregion

        #endregion
    }
}