using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnumValue;

namespace Entities
{
    public class StaticPage : BaseEntity
    {
        public string PageTitle { get; set; }
        public string Caption { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string Keywords { get; set; }
        public string DescriptionSeo { get; set; }
        public StaticPageType StaticPageType { get; set; } = StaticPageType.Other;
    }
    public class StaticPageConfiguration : IEntityTypeConfiguration<StaticPage>
    {
        public void Configure(EntityTypeBuilder<StaticPage> builder)
        {
            builder.Property(p => p.Caption).HasMaxLength(3000).IsUnicode(true);
            builder.Property(p => p.Url).HasMaxLength(300).IsRequired().IsUnicode(true);
            builder.Property(p => p.PageTitle).IsRequired().HasMaxLength(300).IsUnicode(true);
            builder.Property(p => p.Content).IsRequired().IsUnicode(true).HasColumnType("ntext");
        }
    }
}
