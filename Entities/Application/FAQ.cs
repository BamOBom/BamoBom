using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Entities
{
    public class FAQ : BaseEntity
    {
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Order { get; set; }
    }
    public class FAQConfiguration : IEntityTypeConfiguration<FAQ>
    {
        public void Configure(EntityTypeBuilder<FAQ> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(120).IsUnicode(true);
            builder.Property(p => p.Question).IsRequired().HasMaxLength(500).IsUnicode(true);
            builder.Property(p => p.Answer).IsRequired().HasMaxLength(1500).IsUnicode(true);
        }
    }
}
