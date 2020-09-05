using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities
{
    public class Country : BaseEntity
    {
        public Country()
        {
            Provinces = new HashSet<Province>();
        }
        public string Name { get; set; }

        public string Flag { get; set; }
        public string ISO { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
    }
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        }
    }
}
