using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Entities
{
    public class ContactUs : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public bool IsVisited { get; set; } = false;

        public bool IsReplied { get; set; } = false;

        public DateTime? ReplyDate { get; set; }
    }
    public class ContactUsConfiguration : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(200).IsUnicode(true);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200).IsUnicode(true);
            builder.Property(x => x.Subject).HasMaxLength(150).IsUnicode(true);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(1000).IsUnicode(true);
        }
    }
}
