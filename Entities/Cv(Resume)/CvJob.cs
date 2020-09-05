using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace Entities
{   
    public class CvJob : BaseEntity
    {
        public CvJob()
        {
            JobCertificate = new HashSet<CvJobCertificate>();
            JobEducation = new HashSet<CvJobEducation>();
            JobResearch = new HashSet<CvJobResearch>();
            JobWork = new HashSet<CvJobWork>();
            JobSkill = new HashSet<CvJobSkill>();
        }
        ///// <summary>
        ///// شناسه درخواست شغل
        ///// </summary>
        public int JobRequestId { get; set; }

        /// <summary>
        /// خلاصه رزومه
        /// </summary>
        public string CvSummary { get; set; }

        /// <summary>
        /// اختصاصی هر رزومه
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// تاریخ ساخت رزومه
        /// </summary>
        public DateTime CreateDate { get; set; }

        public virtual JobRequest JobRequest { get; set; }

        public virtual ICollection<CvJobCertificate> JobCertificate { get; set; }
        public virtual ICollection<CvJobEducation> JobEducation { get; set; }
        public virtual ICollection<CvJobResearch> JobResearch { get; set; }
        public virtual ICollection<CvJobWork> JobWork { get; set; }
        public virtual ICollection<CvJobSkill> JobSkill { get; set; }

    }
    public class CvJobConfiguration : IEntityTypeConfiguration<CvJob>
    {
        public void Configure(EntityTypeBuilder<CvJob> builder)
        {
            builder.HasOne(p => p.JobRequest).WithMany(p => p.CvJobs).HasForeignKey(p => p.JobRequestId);
            builder.Property(p => p.Url).HasMaxLength(500).IsUnicode(true).IsRequired();
        }
    }
}
