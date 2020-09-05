using AutoMapper;
using Entities;
using System.Collections.Generic;
using System.Linq;
using WebFramework.Api;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;

namespace BomoBana.Areas.Admin
{
    public class Exel
    {
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [DataType(DataType.Upload)]
        [Display(Name = "قایل اکسل")]
        public IFormFile File { get; set; }
    }

    public class UploadCenterDto : BaseDto<UploadCenterDto , UploadCenter>
    {
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public IFormFile File { get; set; }
    }
    public class UploadListDto : BaseDto<UploadListDto, UploadCenter>
    {
        public string File { get; set; }
        public DateTime CreateDate { get; set; }
        public string Url { get; set; }

    }
}
