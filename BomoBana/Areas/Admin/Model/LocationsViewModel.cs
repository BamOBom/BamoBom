using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebFramework.Api;
using WebFramework.Extensions;

namespace BomoBana.Areas.Admin
{
    #region Country
    public class CountryDto
    {
        public CountryDto()
        {
            CountryListDto = new HashSet<CountryListDto>();
        }
        public ICollection<CountryListDto> CountryListDto { get; set; }

    }

    public class CountryViewModel : CountryDto
    {

    }
    public class CountryListDto : BaseDto<CountryListDto, Country>
    {
        public string Name { get; set; }

        public string Flag { get; set; }
        public string ISO { get; set; }
        public int Provinces { get; set; }

        public override void CustomMappings(IMappingExpression<Country, CountryListDto> mappingExpression)
        {
            mappingExpression.ForMember(
                    dest => dest.Provinces,
                    config => config.MapFrom(src => src.Provinces.Count()));
        }
    }
    #endregion

    #region Province

    public class ProvinceDto
    {
        public ProvinceDto()
        {
            ProvinceListDto = new HashSet<ProvinceListDto>();
            City = new List<City>();
            ProvinceExel = new Exel();
        }
        public ICollection<ProvinceListDto> ProvinceListDto { get; set; }
        public string CountryName { get; set; }
        public int CountryId { get; set; }
        public string ISO { get; set; }
        public string Flag { get; set; }
        public Exel ProvinceExel { get; set; }
        public List<City> City { get; set; }

        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(50, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Display(Name = "نام استان")]
        public string Name { get; set; }
    }

    public class ProvinceViewModel : ProvinceDto
    {

    }
   
    public class ProvinceListDto : BaseDto<ProvinceListDto, Province>
    {
        public string Name { get; set; }    
        public int Cities { get; set; }
        public override void CustomMappings(IMappingExpression<Province, ProvinceListDto> mappingExpression)
        {
            mappingExpression.ForMember(
                dest => dest.Cities, config => config.MapFrom(src => src.Cities.Count()));   
        }
    }
    public class CreateProvinceDto : BaseDto<CreateProvinceDto, Province, int>
    {
        public string Name { get; set; }
    }

    #endregion

    #region City
    public class CityDto
    {
        public CityDto()
        {
            CityListDto = new HashSet<CityListDto>();
            CityExel = new Exel();
        }
        public ICollection<CityListDto> CityListDto { get; set; }
        public string CountryName { get; set; }
        public string ProvinceName { get; set; }
        public string ISO { get; set; }
        public string Flag { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public Exel CityExel { get; set; }

        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(50, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Display(Name = "نام شهر")]
        public string Name { get; set; }
    }

    public class CityViewModel : CityDto
    {

    }
    public class CityListDto : BaseDto<CityListDto, City>
    {
        public string Name { get; set; }
        public int User { get; set; }
        //public int Store { get; set; }

        public override void CustomMappings(IMappingExpression<City, CityListDto> mappingExpression)
        {
            mappingExpression.ForMember(
                dest => dest.User, config => config.MapFrom(src => src.User.Count()));
            //mappingExpression.ForMember(
            //   dest => dest.Store, config => config.MapFrom(src => src.Store.Count()));
        }
    }
    public class CreateCityDto : BaseDto<CreateCityDto, City, int>
    {
        public string Name { get; set; }
    }
    #endregion

    #region LoadAjax
    public class LoadAjaxProvinceDto : BaseDto<LoadAjaxProvinceDto, Province, int>
    {
        public string text { get; set; }
        public override void CustomMappings(IMappingExpression<Province, LoadAjaxProvinceDto> mappingExpression)
        {
            mappingExpression.ForMember(
                    dest => dest.text,
                    config => config.MapFrom(src => src.Name));
        }
    }
    public class LoadAjaxCityDto : BaseDto<LoadAjaxCityDto, City, int>
    {
        public string text { get; set; }
        public override void CustomMappings(IMappingExpression<City, LoadAjaxCityDto> mappingExpression)
        {
            mappingExpression.ForMember(
                    dest => dest.text,
                    config => config.MapFrom(src => src.Name));
        }
    }
    #endregion

}
