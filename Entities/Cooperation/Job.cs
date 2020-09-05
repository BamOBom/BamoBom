using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Job : BaseEntity
    {
        public Job()
        {
            JobRequests = new HashSet<JobRequest>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public virtual ICollection<JobRequest> JobRequests { get; set; }
    }

    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(p => p.Description).HasMaxLength(500).IsUnicode(true);
            builder.Property(p => p.Title).HasMaxLength(200).IsUnicode(true);       
        }
    }
}
