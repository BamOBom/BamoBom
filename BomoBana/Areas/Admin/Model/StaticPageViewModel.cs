using Entities;
using EnumValue;
using System;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace BomoBana.Areas.Admin
{
    #region MyRegion
    public class StaticPageDto : BaseDto<StaticPageDto, StaticPage>
    {
        public string PageTitle { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
        public StaticPageType StaticPageType { get; set; }
        public bool IsActive { get; set; }
    }
    #endregion

    #region Add satic page

    public class AddStaticPageDto : BaseDto<AddStaticPageDto, StaticPage, int>
    {
        [Display(Name = "تیتر")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(300, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        public string PageTitle { get; set; }

        [Display(Name = "کپشن")]
        [MaxLength(3000, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Caption { get; set; }

        [Display(Name = "محتوا")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(150, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Url { get; set; } 

        [Display(Name = "فعال باشد؟")]
        public bool IsActive { get; set; }

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
    public class DetailStaticPageDto : BaseDto<DetailStaticPageDto, StaticPage, int>
    {
        public string PageTitle { get; set; }
        public string Caption { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
    }

    #endregion

    #region Edit static page

    public class EditStaticPageDto : BaseDto<EditStaticPageDto, StaticPage, int>
    {
        [Display(Name = "تیتر")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(300, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        public string PageTitle { get; set; }

        [Display(Name = "کپشن")]
        [MaxLength(3000, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Caption { get; set; }

        [Display(Name = "محتوا")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(150, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Url { get; set; }

        [Display(Name = "فعال باشد؟")]
        public bool IsActive { get; set; }

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
}
