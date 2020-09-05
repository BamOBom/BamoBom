using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Entities
{
    public class UploadCenter : BaseEntity
    {     
        public string File { get; set; }
        public string Url { get; set; }

    }
    public class UploadCenterConfiguration : IEntityTypeConfiguration<UploadCenter>
    {
        public void Configure(EntityTypeBuilder<UploadCenter> builder)
        {
            builder.Property(p => p.File).HasMaxLength(300).IsUnicode(true);
            builder.Property(p => p.Url).HasMaxLength(500).IsUnicode(true);
        }
    }
}
