using AutoMapper;
using Entities;
using System.Collections.Generic;
using System.Linq;
using WebFramework.Api;
using System.ComponentModel.DataAnnotations;
using WebFramework.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using EnumValue;

namespace BomoBana.Areas.Admin
{

    #region Category
    public class CategoryDto
    {
        public CategoryDto()
        {
            CategoryListDto = new HashSet<CategoryListDto>();
            CategoryExel = new Exel();

        }
        public ICollection<CategoryListDto> CategoryListDto { get; set; }
        public Exel CategoryExel { get; set; }
    }

    public class CategoryViewModel : CategoryDto
    {

    }
    public class CategoryListDto : BaseDto<CategoryListDto, Category>
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int SubCategories { get; set; } 
        public int PeropertyCategories { get; set; } 

        public override void CustomMappings(IMappingExpression<Category, CategoryListDto> mappingExpression)
        {
            mappingExpression.ForMember(
                    dest => dest.SubCategories,
                    config => config.MapFrom(src => src.ChildCategories.Count()));
            mappingExpression.ForMember(
                   dest => dest.PeropertyCategories,
                   config => config.MapFrom(src => src.CategoryProperty.Count(e=>!e.IsDeleted)));
        }
    }
    #endregion

    #region SubCategory
    public class SubCategoryDto
    {
        public SubCategoryDto()
        {
            SubCategoryListDto = new HashSet<SubCategoryListDto>();
            Child = new List<Category>();
            SubCategoryExel = new Exel();
        }
        public ICollection<SubCategoryListDto> SubCategoryListDto { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }

        public Exel SubCategoryExel { get; set; }
        public List<Category> Child { get; set; }
    }

    public class SubCategoryViewModel : SubCategoryDto
    {

    }

    public class SubCategoryListDto : BaseDto<SubCategoryListDto, Category>
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int ChildCategories { get; set; }
        public DateTime CreateDate { get; set; }

        public override void CustomMappings(IMappingExpression<Category, SubCategoryListDto> mappingExpression)
        {
            mappingExpression.ForMember(
                dest => dest.ChildCategories, config => config.MapFrom(src => src.ChildCategories.Count()));
        }
    }

    #endregion

    #region ChildCategory
    public class ChildCategoryDto
    {
        public ChildCategoryDto()
        {
            ChildCategoryListDto = new HashSet<ChildCategoryListDto>();
            ChildCategoryExel = new Exel();
        }
        public ICollection<ChildCategoryListDto> ChildCategoryListDto { get; set; }
        public string SubCategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }

        public Exel ChildCategoryExel { get; set; }
    }

    public class ChildCategoryViewModel : ChildCategoryDto
    {

    }

    public class ChildCategoryListDto : BaseDto<ChildCategoryListDto, Category>
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

    }

    #endregion

    public class CreateCategoryDto : BaseDto<CreateCategoryDto, Category, int>
    {
        public CreateCategoryDto()
        {
            IsActive = true;
        }
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(50, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "دسته بندی")]
        public int? ParentCategoryId { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxFileSize(5 * 512 * 512)]
        [Display(Name = "عکس")]
        public IFormFile Image { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; } = true;
    }
    public class LoadAjaxCategoryDto : BaseDto<LoadAjaxCategoryDto, Category, int>
    {
        public string text { get; set; }
        public override void CustomMappings(IMappingExpression<Category, LoadAjaxCategoryDto> mappingExpression)
        {
            mappingExpression.ForMember(
                    dest => dest.text,
                    config => config.MapFrom(src => src.Title));
        }
    }
    //public class LoadAjaxBrandDto : BaseDto<LoadAjaxBrandDto, Brand, int>
    //{
    //    public string text { get; set; }
    //    public override void CustomMappings(IMappingExpression<Brand, LoadAjaxBrandDto> mappingExpression)
    //    {
    //        mappingExpression.ForMember(
    //                dest => dest.text,
    //                config => config.MapFrom(src => src.Title));
    //    }
    //}
    #region Property
    public class CategoryPropertyDto
    {
        public CategoryPropertyDto()
        {
            ProppertyCategoryList = new HashSet<ProppertyCategoryListDto>();
        }
        public ICollection<ProppertyCategoryListDto> ProppertyCategoryList { get; set; }
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(50, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Display(Name = "نوع فیلد")]
        public string Type { get; set; }
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(50, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Display(Name = "عنوان")]
        public string TitleValue { get; set; }
        public int CategoryId { get; set; }

    }

    public class CategoryPropertyViewModel : CategoryPropertyDto
    {

    }
    #endregion
    public class ProppertyCategoryListDto : BaseDto<ProppertyCategoryListDto, CategoryProperty, int>
    {
        public string Type { get; set; }
        public string TitleValue { get; set; }
        public int CategoryId { get; set; }
        //public int ProductCount { get; set; }
        //public override void CustomMappings(IMappingExpression<CategoryProperty, ProppertyCategoryListDto> mappingExpression)
        //{
        //    mappingExpression.ForMember(
        //            dest => dest.ProductCount,
        //            config => config.MapFrom(src => src.ProductProperty.Count()));
        //}
    }

    public class CreatePropertyCategoryDto : BaseDto<CreatePropertyCategoryDto, CategoryProperty, int>
    {
        public string Type { get; set; }
        public string TitleValue { get; set; }
        public int CategoryId { get; set; }
    }
}

