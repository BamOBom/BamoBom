using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnumValue
{
    public enum TypeOfInputValue
    {
        [Display(Name = "رشته")]
        text = 0,

        [Display(Name = "True/False")]
        checkbox = 1,

        [Display(Name = "تاریخ")]
        datetime = 2,

        [Display(Name = "عدد")]
        number = 3,
    }
    public enum GenderType
    {
        [Display(Name = "نامشخص")]
        None = 0,

        [Display(Name = "مرد")]
        Male = 1,

        [Display(Name = "زن")]
        Female = 2
    }
    public enum HasRegister
    {
        [Display(Name = "ثبت نام شده")]
        True = 0,

        [Display(Name = "ثبت نام نشده")]
        False = 1,
    }
    public enum BoolType
    {
        [Display(Name = "فعال")]
        True = 0,

        [Display(Name = "غیر فعال")]
        False = 1,
    }
    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
    public enum Status : byte
    {   
        [Display(Name = "پیشنویس")]
        Draft = 1,

        [Display(Name = "در انظار تایید")]
        PendingApproval = 2,

        [Display(Name = "تایید شده")]
        Approved = 3,

        [Display(Name = "رد شد")]
        Rejected = 4
    }
    public enum UserType : byte
    {
        [Display(Name = "نا مشخص")]
        None = 1,
        [Display(Name = "کاربر عادی")]
        SimpleUser = 2,
        [Display(Name = "آزانس")]
        Agency = 3,
        [Display(Name = "مدیریت")]
        Admin = 4,
    }
    public enum StaticPageType : byte
    {
        [Display(Name = "دیگر")]
        Other = 0,

        [Display(Name = "درباره ما")]
        About = 1,

        [Display(Name = "ارتباط با ما")]
        Contact = 2,

        [Display(Name = "همکاری با ما")]
        Coopretion = 3,
    }
    public enum NotifyType
    {
        Success,
        Error,
        Warning,
        Info
    }
    public enum OperationMessage
    {
        [Display(Name = "عملیات با خطا روبرو شد")]
        OperationFailed = 1,
        [Display(Name = "عملیات با موفقیت انجام شد")]
        OperationSucceed = 2,
        [Display(Name = "فایل ذخیره شد")]
        FileSaved = 3,
        [Display(Name = "تغییرات ذخیره شد")]
        ChagesSaved = 4,
        [Display(Name = "خطایی در داده های ارسالی شما وجود دارد.")]
        ModelStateIsNotValid = 5,
        [Display(Name = "پست الکترونیکی شما تایید نشده لطفاً به ایمیل خود مراجعه کنید.")]
        ConfirmationEmailSent = 6,
        [Display(Name = "خطای سیستمی")]
        SystemFailure = 7,
        [Display(Name = "تغییری حاصل نشد")]
        NoChangeApplied = 8,
        [Display(Name = "این مورد وجود ندارد")]
        DoesNotExist = 9,
        [Display(Name = "با سپاس از تماس شما، پیام شما دریافت شد.")]
        MessageSent = 10,
        [Display(Name = "این مورد وجود دارد")]
        DoestExist = 11,
        [Display(Name = "لطفا یک فایل انتخاب کنید")]
        SelectFile = 12,
        [Display(Name = "لطفا یک عکس انتخاب کنید")]
        SelectImage = 13,
    }
    public enum DisplayUserInLocations
    {
        Country,
        Province,
        City,
        All
    }
    public enum MilitaryServiceStatus
    {
        [Display(Name = "نا مشخص")]
        None = 0,
        /// <summary>
        /// انجام شده
        /// </summary>
        [Display(Name = "انجام شده")]
        Done = 1,

        /// <summary>
        /// در حال انجام
        /// </summary>
        [Display(Name = "در حال انجام")]
        InProgress = 2,

        /// <summary>
        /// انجام نشده
        /// </summary>
        [Display(Name = "انجام نشده")]
        HaveNotDone = 3,

        /// <summary>
        /// معافیت
        /// </summary>
        [Display(Name = "معافیت")]
        Exemption = 4
    }
    public enum CooprationStatus : byte
    {
        /// <summary>
        /// نوع همکاری نا مشخض
        /// </summary>
        [Display(Name = "نوع همکاری نا مشخص")]
        //[LocalizedDescription("UnknownTypeofcooperation", typeof(ResourceEnum))]
        Non = 0,
        /// <summary>
        /// پارهوقت
        /// </summary>
        [Display(Name = "پاره هوقت")]
        //[LocalizedDescription("PartTime", typeof(ResourceEnum))]
        PartTime = 1,

        /// <summary>
        /// تمام وقت
        /// </summary>
        [Display(Name = "تمام وقت")]
        //[LocalizedDescription("FullTime", typeof(ResourceEnum))]
        FullTime = 2,

        /// <summary>
        /// پروژه ای
        /// </summary>
        [Display(Name = "پروژه ای")]
        //[LocalizedDescription("aProject", typeof(ResourceEnum))]
        ProjectContract = 3
    }
    public enum EnglishSkill : byte
    {
        /// <summary>
        /// ضعیف
        /// </summary>
        [Display(Name = "ضعیف")]
        BelowAverage = 1,

        /// <summary>
        /// متوسط
        /// </summary>
        [Display(Name = "متوسط")]
        Avarage = 2,

        /// <summary>
        /// خوب
        /// </summary>
        [Display(Name = "خوب")]
        Good = 3,

        /// <summary>
        /// عالی
        /// </summary>
        [Display(Name = "عالی")]
        Excelent = 4,

        /// <summary>
        /// حرفهای
        /// </summary>
        [Display(Name = "حرفهای")]
        Professional = 5
    }
    public enum JobStatus : byte
    {
        /// <summary>
        /// تائید شده
        /// </summary>
        [Display(Name = "تائید شده")]
        //[LocalizedDescription("Confirmed", typeof(ResourceEnum))]
        Confirmed = 1,
        /// <summary>
        /// در انتظار بررسی
        /// </summary>
        [Display(Name = "در انتظار بررسی")]
        //[LocalizedDescription("Await", typeof(ResourceEnum))]
        Await = 2,
        /// <summary>
        /// تائید برای مصاحبه
        /// </summary>
        [Display(Name = "تائید برای مصاحبه")]
        //[LocalizedDescription("Interview", typeof(ResourceEnum))]
        Interview = 3,
        /// <summary>
        /// رد شده
        /// </summary>
        [Display(Name = "رد شده")]
        //[LocalizedDescription("Rejected", typeof(ResourceEnum))]
        Rejected = 4,
        /// <summary>
        /// حذف شده
        /// </summary>
        [Display(Name = "حذف شده")]
        //[LocalizedDescription("Delete", typeof(ResourceEnum))]
        Delete = 5,
    }
    public enum Education : byte
    {
        /// <summary>
        /// دیپلم
        /// </summary>
        [Display(Name = "دیپلم")]
        //[LocalizedDescription("Diploma", typeof(ResourceEnum))]
        Diploma = 0,
        /// <summary>
        /// کاردانی
        /// </summary>
        [Display(Name = "کاردانی")]
        //[LocalizedDescription("AboveDiploma", typeof(ResourceEnum))]
        AboveDiploma = 1,

        /// <summary>
        /// کارشناسی
        /// </summary>
        [Display(Name = "کارشناسی")]
        //[LocalizedDescription("Bachelor", typeof(ResourceEnum))]
        Bachelor = 2,
        /// <summary>
        /// کارشناسی ارشد
        /// </summary>
        [Display(Name = "کارشناسی ارشد")]
        //[LocalizedDescription("AboveBachelor", typeof(ResourceEnum))]
        AboveBachelor = 3,
        /// <summary>
        /// دکتری
        /// </summary>
        [Display(Name = "دکتری")]
        //[LocalizedDescription("PHD", typeof(ResourceEnum))]
        PHD = 4,
        /// <summary>
        /// فوق دکتری
        /// </summary>
        [Display(Name = "فوق دکتری")]
        //[LocalizedDescription("AbovePHD", typeof(ResourceEnum))]
        AbovePHD = 5,

    }
}
