using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Entities
{
    public class Setting : BaseEntity
    {
        public string ApplicationTitle { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public string Favicon { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }

    }
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(p => p.Description).HasMaxLength(1000).IsUnicode(true);
            builder.Property(p => p.Description).IsUnicode(true);
            builder.Property(p => p.KeyWords).HasMaxLength(250).IsUnicode(true);
            builder.Property(p => p.KeyWords).IsUnicode(true);
            builder.Property(p => p.ApplicationTitle).IsRequired().HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.Description).IsUnicode(true).HasColumnType("ntext");
            builder.Property(p => p.Favicon).HasMaxLength(250);
            builder.Property(p => p.Logo).HasMaxLength(250);
            builder.Property(x => x.PhoneNumber).HasMaxLength(100).IsUnicode(true);
            builder.Property(x => x.FaxNumber).HasMaxLength(100).IsUnicode(true);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.Address).HasMaxLength(500).IsUnicode(true);
        }
    }
}
