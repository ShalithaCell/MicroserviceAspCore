using Identity.Model.DataModels;
using Identity.Model.DataModels.BaseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Identity.Infrastructure.DAL
{
    /// <summary>
    /// Dao
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // model set
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Fluent API contrains
            //<no Fluent API contraints yet>

            // Set model default values
            foreach (var entityType in builder.Model.GetEntityTypes()
                 .Where(t => t.ClrType.IsSubclassOf(typeof(DefaultBaseEntity))))
            {
                builder.Entity(
                        entityType.Name,
                        x =>
                        {
                            x.Property("RegistedDate")
                                .HasDefaultValueSql("GETDATE()");

                            x.Property("ModifiedDate")
                                .HasDefaultValueSql("GETDATE()");

                            x.Property("IsActive")
                                .HasDefaultValue(true);
                        });
            }
        }

    }

    /// <summary>
    /// Injecting appsettings.json connection string to ApplicationDbContext
    /// Date       => 10-06-2020
    /// Updated By => <>
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            AppConfiguration app = new AppConfiguration();
            var connectionString = app.ConnectionString;

            builder.UseSqlServer(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}
