using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            ChildCategories = new HashSet<Category>();
            CategoryProperty = new HashSet<CategoryProperty>();
            //Product = new HashSet<Product>();
            IsActive = true;
        }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int? ParentCategoryId { get; set; }
        [ForeignKey(nameof(ParentCategoryId))]
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
        public virtual ICollection<CategoryProperty> CategoryProperty { get; set; }
        //public virtual ICollection<Product> Product { get; set; }
    }
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(500); ;
        }
    }
}
