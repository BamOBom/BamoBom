using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace Entities
{
    public class CvJobCertificate : BaseEntity
    {

        public string Title { get; set; }

        public string GetPlace { get; set; }
        public string Description { get; set; }

        public string Date { get; set; }

        public int CvJobId { get; set; }

        public virtual CvJob CvJob { get; set; }

    }

    public class CvJobCertificateConfiguration : IEntityTypeConfiguration<CvJobCertificate>
    {
        public void Configure(EntityTypeBuilder<CvJobCertificate> builder)
        {

            builder.HasOne(p => p.CvJob).WithMany(p => p.JobCertificate).HasForeignKey(p => p.CvJobId);

            builder.Property(p => p.Description).HasMaxLength(1000).IsUnicode(true).HasColumnType("ntext");
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.GetPlace).IsRequired().HasMaxLength(100);
        }
    }
}
