using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnumValue;

namespace Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Caption { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Keywords { get; set; }
        public string DescriptionSeo { get; set; }
        public bool SetSliderHome { get; set; }
    }
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(p => p.Caption).HasMaxLength(3000).IsUnicode(true);
            builder.Property(p => p.Image).HasMaxLength(150).IsRequired().IsUnicode(true);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(300).IsUnicode(true);
            builder.Property(p => p.Content).IsRequired().IsUnicode(true).HasColumnType("ntext");
        }
    }
}
