using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
namespace Entities
{
    public partial class Visitor : BaseEntity<Guid>
    {
        public string IP { get; set; }
        public string HttpForward { get; set; }
        public string UrlCurrent { get; set; }
        public string UrlReferrer { get; set; }
        public DateTime? VisitDate { get; set; }
        public string UserAgent { get; set; }
        public string DeviceBrand { get; set; }
        public string DeviceModel { get; set; }
        public bool IsCrawler { get; set; }
        public string OsName { get; set; }
        public string OsVersion { get; set; }
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
    public class VisitorConfiguration : IEntityTypeConfiguration<Visitor>
    {
        public void Configure(EntityTypeBuilder<Visitor> builder)
        {
            builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);

        }
    }
}
