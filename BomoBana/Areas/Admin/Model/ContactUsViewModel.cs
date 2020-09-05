using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebFramework.Api;

namespace BomoBana.Areas.Admin
{
    public class ContactUsDto
    {
        public ContactUsDto()
        {
            ContactUsListDto = new HashSet<ContactUsListDto>();

        }
        public ICollection<ContactUsListDto> ContactUsListDto { get; set; }
    }

    public class ContactUsViewModel : ContactUsDto
    {

    }
    public class ContactUsListDto : BaseDto<ContactUsListDto, ContactUs, int>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public bool IsVisited { get; set; } = false;

        public bool IsReplied { get; set; } = false;
        public DateTime CreateDate { get; set; }
        public DateTime? ReplyDate { get; set; }
    }
    public class DetailContactUsDto : BaseDto<DetailContactUsDto, ContactUs, int>
    {
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
