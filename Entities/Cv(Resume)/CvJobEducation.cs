using EnumValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Entities
{
    #region DataModel

    public class CvJobEducation : BaseEntity
    {
    
        public Education Educations { get; set; } = Education.Diploma;

        public string Reshte { get; set; }

        public string Unvercity { get; set; }

        public string Description { get; set; }

        public bool Iseducation { get; set; }

        public int CvJobId { get; set; }

        #endregion

        public virtual CvJob CvJob { get; set; }

    }

    public class CvJobEducationConfiguration : IEntityTypeConfiguration<CvJobEducation>
    {
        public void Configure(EntityTypeBuilder<CvJobEducation> builder)
        {
            builder.HasOne(p => p.CvJob).WithMany(p => p.JobEducation).HasForeignKey(p => p.CvJobId);

            builder.Property(p => p.Description).HasMaxLength(100).IsUnicode(true).HasColumnType("ntext");
            builder.Property(p => p.Reshte).IsRequired().HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.Description).HasMaxLength(300);
            builder.Property(p => p.Unvercity).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Iseducation);
        }
    }
}