using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BomoBana.Areas.Admin
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [DisplayName("نام کاربری")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [DataType(DataType.Password)]
        [DisplayName("گذرواژه")]
        public string Password { get; set; }
        public bool RemmeberMe { get; set; }
        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [DisplayName("شماره همراه")]
        public string PhoneNumber { get; set; }
    }
    public class ForgotPasswordModel
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
    public class VerifyPhoneNumberViewModel
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
    }
    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "گذرواژه ها با یکدیگر مطابقت ندارند.")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
