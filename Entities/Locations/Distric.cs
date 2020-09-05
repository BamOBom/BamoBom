using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities
{
    public class Distric : BaseEntity
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        [CascadeDelete]
        public virtual City City { get; set; }
    }
    public class DistricConfiguration : IEntityTypeConfiguration<Distric>
    {
        public void Configure(EntityTypeBuilder<Distric> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.HasOne(p => p.City).WithMany(c => c.Districs).HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
