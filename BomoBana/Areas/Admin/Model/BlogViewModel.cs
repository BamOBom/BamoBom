using Entities;
using EnumValue;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;
using WebFramework.Extensions;

namespace BomoBana.Areas.Admin
{
    #region MyRegion
    public class BlogDto : BaseDto<BlogDto, Blog>
    {
        public string Title { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public bool SetSliderHome { get; set; }
    }
    #endregion

    #region Add satic page

    public class AddBlogDto : BaseDto<AddBlogDto, Blog, int>
    {
        [Display(Name = "تیتر")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(300, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        public string Title { get; set; }

        [Display(Name = "کپشن")]
        [MaxLength(3000, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Caption { get; set; }

        [Display(Name = "محتوا")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxFileSize(5 * 512 * 512)]
        public IFormFile Image { get; set; }

        [Display(Name = "فعال باشد؟")]
        public bool IsActive { get; set; }

        [Display(Name = "نمایش در اسلایدر")]
        public bool SetSliderHome { get; set; }

        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [Display(Name = "کلمات کلیدی (موتور جستجو)")]
        [MaxLength(5000, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        public string Keywords { get; set; }
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [Display(Name = "توضیحات (موتور جستجو)")]
        [DataType(DataType.MultilineText)]
        [MaxLength(10000, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        public string DescriptionSeo { get; set; }
    }

    #endregion

    #region Detail
    public class DetailBlogDto : BaseDto<DetailBlogDto, Blog, int>
    {
        public string Title { get; set; }
        public string Caption { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }

    #endregion

    #region Edit static page

    public class EditBlogDto : BaseDto<EditBlogDto, Blog, int>
    {
        [Display(Name = "تیتر")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(300, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        public string Title { get; set; }

        [Display(Name = "کپشن")]
        [MaxLength(3000, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Caption { get; set; }

        [Display(Name = "محتوا")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [MaxFileSize(5 * 512 * 512)]
        public IFormFile Image { get; set; }

        [Display(Name = "فعال باشد؟")]
        public bool IsActive { get; set; }
        [Display(Name = "نمایش در اسلایدر")]
        public bool SetSliderHome { get; set; }
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [Display(Name = "کلمات کلیدی (موتور جستجو)")]
        [MaxLength(5000, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        public string Keywords { get; set; }
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [Display(Name = "توضیحات (موتور جستجو)")]
        [DataType(DataType.MultilineText)]
        [MaxLength(10000, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        public string DescriptionSeo { get; set; }
        public string ImageName { get; set; }

    }

    #endregion
}
