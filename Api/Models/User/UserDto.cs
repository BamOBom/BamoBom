using Entities;
using EnumValue;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class UserDto : IValidatableObject
    {
        //[Required]
        //[StringLength(100)]
        //public string UserName { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string Email { get; set; }
        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }
        //[Required]
        //[StringLength(500)]
        //public string Password { get; set; }
        //[Required]
        //[StringLength(100)]
        //public string FirstName { get; set; }
        //[Required]
        //[StringLength(100)]
        //public string Surname { get; set; }
        //public int? CityId { get; set; }
        //public GenderType Gender { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
            //    yield return new ValidationResult("نام کاربری نمیتواند Test باشد", new[] { nameof(UserName) });
            //if (Password.Equals("123456"))
            //    yield return new ValidationResult("رمز عبور نمیتواند 123456 باشد", new[] { nameof(Password) });
            //    if (Gender == GenderType.Male && Age > 30)
            //        yield return new ValidationResult("آقایان بیشتر از 30 سال معتبر نیستند", new[] { nameof(Gender), nameof(Age) });
            //
            if (PhoneNumber.Length != 11 )
                yield return new ValidationResult("تعداد ارقام شماره همراه وارد شده صحیح نیست", new[] { nameof(PhoneNumber) });
        }
    }
    public class ConfirmPhoneNumberDto
    {
        public bool HasRegister { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
    }
   
    public class GetTokenRegisterDto
    {
        public string token { get; set; }
        public int UserId { get; set; }
    }
    public class ConfirmTokenRegisterDto
    {
        public string Message{ get; set; }
        public bool Success{ get; set; }
    }
    public class ContinueRegisterDto
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public int CityId { get; set; }
        public GenderType Gender { get; set; }
    }
}
