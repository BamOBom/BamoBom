using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Entities
{
    public class CategoryProperty : BaseEntity
    {
        public CategoryProperty()
        {
            //ProductProperty = new HashSet<ProductProperty>();
        }
        public string Type { get; set; }
        public string TitleValue { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        //public virtual ICollection<ProductProperty> ProductProperty { get; set; }
    }
    public class CategoryPropertyConfiguration : IEntityTypeConfiguration<CategoryProperty>
    {
        public void Configure(EntityTypeBuilder<CategoryProperty> builder)
        {
            builder.Property(p => p.TitleValue).IsRequired().HasMaxLength(1000);
            builder.HasOne(p => p.Category).WithMany(c => c.CategoryProperty).HasForeignKey(p => p.CategoryId);
        }
    }
}
