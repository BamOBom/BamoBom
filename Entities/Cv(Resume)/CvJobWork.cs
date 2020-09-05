using EnumValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Entities
{
    public class CvJobWork : BaseEntity
    {
        public string Post { get; set; }

        public string Workplace { get; set; }
        
        public string Description { get; set; }

        public bool IsWorking { get; set; }

        public int CvJobId { get; set; }

        public virtual CvJob CvJob { get; set; }

    }
    public class CvJobWorkConfiguration : IEntityTypeConfiguration<CvJobWork>
    {
        public void Configure(EntityTypeBuilder<CvJobWork> builder)
        {
            builder.HasOne(p => p.CvJob).WithMany(p => p.JobWork).HasForeignKey(p => p.CvJobId);
            builder.Property(p => p.Post).IsRequired().HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.Workplace).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(100).IsUnicode(true).HasColumnType("ntext");

        }
    }
}
