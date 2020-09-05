using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities
{
    public class Province : BaseEntity
    {
        public Province()
        {
            Cities = new HashSet<City>();
        }

        public string Name { get; set; }

        public int CountryId { get; set; }

        [CascadeDelete]
        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.HasOne(p => p.Country).WithMany(c => c.Provinces).HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
