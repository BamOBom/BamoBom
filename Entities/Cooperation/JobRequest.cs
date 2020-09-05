using System;
using System.Collections.Generic;
using EnumValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class JobRequest : BaseEntity
    {
        public JobRequest()
        {
            CvJobs = new HashSet<CvJob>();
        }
        public string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalIdentity { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// وضعیت نظام وضیفه
        /// </summary>
        public string MilitaryServiceStatus { get; set; }

        /// <summary>
        /// دلیل معافیت
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// نوع هکاری
        /// </summary>
        public string CooprationStatus { get; set; }

        /// <summary>
        /// شماره تلفن ثابت
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// شماره تلفن همراه
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// پست الکترونیک
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// معرف
        /// </summary>
        public string Representer { get; set; }
        /// <summary>
        /// زبان 
        /// </summary>
        public string EnglishSkill { get; set; }

        /// <summary>
        /// حقوق درخواستی
        /// </summary>
        public decimal RequestedSalary { get; set; }

        /// <summary>
        /// محل ذخیره رزومه
        /// </summary>
        public string ResumeFileName { get; set; }

        /// <summary>
        /// شناسه شغل مورد نظر 
        /// </summary>
        public int JobTitleId { get; set; }

        /// <summary>
        /// شناسه شهر محل سکونت شخص
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// وضعیت درخواست
        /// </summary>
        public JobStatus JobStatus { get; set; } = JobStatus.Await;


        /// <summary>
        /// بازدید از طرف ادمین
        /// </summary>
        public bool IsVisited { get; set; }

        /// <summary>
        /// تاریخ درخواست
        /// </summary>
        public DateTime InsertDate { get; set; }

        public virtual Job JobTitle { get; set; }
        public virtual ICollection<CvJob> CvJobs { get; set; }
       
        public string FullName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(Surname))
                {
                    return FirstName + " " + Surname;
                }
                else if (!String.IsNullOrWhiteSpace(FirstName) && String.IsNullOrWhiteSpace(Surname))
                    return FirstName;
                else if (String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(Surname))
                    return Surname;
                else
                {
                    return FirstName;
                }
            }
        }

    }
    public class JobRequestConfiguration : IEntityTypeConfiguration<JobRequest>
    {
        public void Configure(EntityTypeBuilder<JobRequest> builder)
        {
            builder.HasOne(x => x.JobTitle).WithMany(x => x.JobRequests).HasForeignKey(x => x.JobTitleId);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200).IsUnicode(true);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NationalIdentity).HasMaxLength(100).IsUnicode(true);
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.MilitaryServiceStatus);
            builder.Property(x => x.MobileNumber).IsRequired().HasMaxLength(100).IsUnicode();
            builder.Property(x => x.PhoneNumber).HasMaxLength(100).IsUnicode(true);
            builder.Property(x => x.Email).HasMaxLength(200).IsUnicode(true);
            builder.Property(x => x.Representer).HasMaxLength(150).IsUnicode(true);
            builder.Property(x => x.RequestedSalary);
            builder.Property(x => x.ResumeFileName).HasMaxLength(100).IsUnicode(true);

        }
    }
}
