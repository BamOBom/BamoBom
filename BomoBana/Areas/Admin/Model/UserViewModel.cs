using EnumValue;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebFramework.Extensions;

namespace BomoBana.Areas.Admin
{
    public class AdminPanelUserDtoViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public UserType UserType { get; set; }
        public Status Status { get; set; }

    }
    public class AdminPanelCreateUserDto : IValidatableObject
    {
        [Display(Name = "شماره همراه")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "{0} نباید کمتر از '{1}' کاراکتر باشد.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} مورد نیاز است.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "پسورد")]
        [MaxLength(11, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} مورد نیاز است.")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "{0} نباید کمتر از '{1}' کاراکتر باشد.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار پسورد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "برای تأیید پسورد ، باید دوباره آن را وارد کنید")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "نام")]
        [DataType(DataType.Text)]
        [MaxLength(150, ErrorMessage = "{0} نباید بیشتر از '{1}' کاراکتر باشد.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} مورد نیاز است.")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [DataType(DataType.Text)]
        [MaxLength(150, ErrorMessage = "{0} نباید بیشتر از '{1}' کاراکتر باشد.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} مورد نیاز است.")]
        public string SurName { get; set; }

        [Display(Name = "شهر")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "حداقل باید '{1}' انتخاب از {0} داشته باشید")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} مورد نیاز است.")]
        public string CityId { get; set; }

        [Display(Name = "جنسیت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} مورد نیاز است.")]
        public GenderType Gender { get; set; }

        [Display(Name = "نوع کاربر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} مورد نیاز است.")]
        public UserType UserType { get; set; }

        [Display(Name = "وضعیت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} مورد نیاز است.")]
        public Status Status { get; set; }

        [MaxFileSize(5 * 512 * 512)]
        [Display(Name = "عکس پروفایل")]
        [DataType(DataType.Upload)]
        public IFormFile ProfileImage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            if (Password != PasswordConfirm)
                yield return new ValidationResult("پسورد مطابقت ندارند", new string[] { nameof(Password), nameof(PasswordConfirm) });

            if (PhoneNumber.Length != 11)
                yield return new ValidationResult("تعداد ارقام شماره همراه وارد شده صحیح نیست", new[] { nameof(PhoneNumber) });
        }
    }
}
