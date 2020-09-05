using EnumValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Entities
{
    public class CvJobSkill : BaseEntity
    {
        public string Title { get; set; }

        public EnglishSkill Skill { get; set; } = EnglishSkill.BelowAverage;

        public int CvJobId { get; set; }
        public virtual CvJob CvJob { get; set; }

    }
    public class CvJobSkillConfiguration : IEntityTypeConfiguration<CvJobSkill>
    {
        public void Configure(EntityTypeBuilder<CvJobSkill> builder)
        {
            builder.HasOne(p => p.CvJob).WithMany(p => p.JobSkill).HasForeignKey(p => p.CvJobId);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100).IsUnicode(true);

        }
    }
}
