using Data.Repositories;
using Entities;
using EnumValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.DataInitializer
{
    public class ApplicationDataInitializer : IDataInitializer
    {
        private readonly IRepository<Country> _CountryService;
        private readonly IRepository<Province> _ProvinceService;
        private readonly IRepository<City> _CityService;
        private readonly IRepository<StaticPage> _StaticPageService;
        private readonly IRepository<Setting> _SettingService;
        public ApplicationDataInitializer(IRepository<Country> CountryService,
            IRepository<Province> ProvinceService, IRepository<City> CityService,
            IRepository<StaticPage> StaticPageService, IRepository<Setting> SettingService)
        {
            this._CountryService = CountryService;
            this._ProvinceService = ProvinceService;
            this._StaticPageService = StaticPageService;
            this._CityService = CityService;
            this._SettingService = SettingService;
        }

        public void InitializeData()
        {

            #region Country
            if (!_CountryService.TableNoTracking.Any(p => p.Name == "ایران"))
            {
                _CountryService.Add(new Country
                {
                    Name = "ایران",
                    ISO = "IR",
                    Flag = "IR.png"
                });
            }
            #endregion

            #region Province
            if (!_ProvinceService.TableNoTracking.Any(p => p.Name == "تهران"))
            {
                _ProvinceService.Add(new Province
                {
                    Name = "تهران",
                    CountryId = 1
                });
            }
            if (!_ProvinceService.TableNoTracking.Any(p => p.Name == "البرز"))
            {
                _ProvinceService.Add(new Province
                {
                    Name = "البرز",
                    CountryId = 1
                });
            }
            #endregion

            #region City
            if (!_CityService.TableNoTracking.Any(p => p.Name == "تهران"))
            {
                _CityService.Add(new City
                {
                    Name = "تهران",
                    ProvinceId = 1
                });
            }
            if (!_CityService.TableNoTracking.Any(p => p.Name == "دماوند"))
            {
                _CityService.Add(new City
                {
                    Name = "دماوند",
                    ProvinceId = 1
                });
            }
            if (!_CityService.TableNoTracking.Any(p => p.Name == "کرج"))
            {
                _CityService.Add(new City
                {
                    Name = "کرج",
                    ProvinceId = 2
                });
            }
            if (!_CityService.TableNoTracking.Any(p => p.Name == "فردیس"))
            {
                _CityService.Add(new City
                {
                    Name = "فردیس",
                    ProvinceId = 2
                });
            }
            if (!_CityService.TableNoTracking.Any(p => p.Name == "هشتگرد"))
            {
                _CityService.Add(new City
                {
                    Name = "هشتگرد",
                    ProvinceId = 2
                });
            }
            #endregion

            #region Setting

            if (!_SettingService.TableNoTracking.Any(p => p.ApplicationTitle == "بوم و بنا"))
            {
                _SettingService.Add(new Setting
                {
                    ApplicationTitle = "بوم و بنا",
                    Description = "توضیحات سایت",
                    Favicon = "favicon.ico",
                    KeyWords = "کلمات کلیدی",
                    Logo = "logo.png",
                    Address = "آدرس",
                    Email = "Info@site.com",
                    FaxNumber = "026-32333333",
                    PhoneNumber = "021-323333333"
                });
            }

            #endregion

        }
    }
}
