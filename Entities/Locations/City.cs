using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities
{
    public class City : BaseEntity
    {
        public City()
        {
            Districs = new HashSet<Distric>();
            User = new HashSet<User>();
        }

        public string Name { get; set; }
        public int ProvinceId { get; set; }
        [CascadeDelete]
        public virtual Province Province { get; set; }
        public virtual ICollection<Distric> Districs { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.HasOne(p => p.Province).WithMany(c => c.Cities).HasForeignKey(p => p.ProvinceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
