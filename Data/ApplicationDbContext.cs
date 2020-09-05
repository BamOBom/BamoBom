using Common.Utilities;
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using EnumValue;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    //DbContext
    {

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MyApiDb;Integrated Security=true");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();

        }

        #region Functions
        public override int SaveChanges()
        {
            try
            {
                _cleanString();

                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var errors = e.GetValidationErrors();

                throw new DbEntityValidationException(string.Join(Environment.NewLine, errors), e.EntityValidationErrors);
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            try
            {
                _cleanString();

                return base.SaveChanges(acceptAllChangesOnSuccess);

            }
            catch (DbEntityValidationException e)
            {
                var errors = e.GetValidationErrors();

                throw new DbEntityValidationException(string.Join(Environment.NewLine, errors), e.EntityValidationErrors);
            }
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            try
            {
                _cleanString();

                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (DbEntityValidationException e)
            {
                var errors = e.GetValidationErrors();
                throw new DbEntityValidationException(string.Join("; ", errors), e.EntityValidationErrors);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _cleanString();

                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException e)
            {
                var errors = e.GetValidationErrors();
                throw new DbEntityValidationException(string.Join("; ", errors), e.EntityValidationErrors);
            }
        }

        #endregion
    
        #region Helpers

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added || x.State == Microsoft.EntityFrameworkCore.EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

        #endregion

    }   
}
