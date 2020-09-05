using EnumValue;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {
            IsActive = true;
        }
        public User(string userName)
             : this()
        {
            this.UserName = userName;
        }
        [StringLength(100)]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public GenderType Gender { get; set; } = GenderType.None;
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public UserType UserType { get; set; } = UserType.None;
        public Status Status { get; set; } = Status.Draft;
        public string ProfileImage { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(Surname))
                {
                    return FirstName + "" + Surname;
                }
                else if (!String.IsNullOrWhiteSpace(FirstName) && String.IsNullOrWhiteSpace(Surname))
                    return FirstName;
                else if (String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(Surname))
                    return Surname;
                else
                {
                    return UserName;
                }
            }
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).IsRequired(false);
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.HasOne(p => p.City).WithMany(c => c.User).HasForeignKey(p => p.CityId);

        }
    }

   
}
