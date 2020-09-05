using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Entities
{
    public class CvJobResearch : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string Date { get; set; }

        public int CvJobId { get; set; }

        public virtual CvJob CvJob { get; set; }

    }
    public class CvJobResearchConfiguration : IEntityTypeConfiguration<CvJobResearch>
    {
        public void Configure(EntityTypeBuilder<CvJobResearch> builder)
        {
            builder.HasOne(p => p.CvJob).WithMany(p => p.JobResearch).HasForeignKey(p => p.CvJobId);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.Description).HasMaxLength(100).IsUnicode(true).HasColumnType("ntext");

        }
    }
}
