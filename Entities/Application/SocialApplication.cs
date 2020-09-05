using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Entities
{
    public class SocialApplication : BaseEntity
    {

        public string Title { get; set; }

        public string Link { get; set; }

        public string Icon { get; set; }

    }
    public class SocialApplicationConfiguration : IEntityTypeConfiguration<SocialApplication>
    {
        public void Configure(EntityTypeBuilder<SocialApplication> builder)
        {

            builder.Property(p => p.Link).HasMaxLength(1000).IsRequired().IsUnicode(true);
        }
    }
}
