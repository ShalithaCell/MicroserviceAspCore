using Identity.Infrastructure.IServices.Repository;
using Identity.Model.DataModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Identity.Infrastructure.DAL
{
    /// <summary>
    /// Insert default data to database
    /// If any data should be in the database by default, you can define it from here
    /// </summary>
    public static class DataBaseSeeder
    {
        public static void Seed(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedDefaultTenant(unitOfWork);
        }

        /// <summary>
        /// check and insert default tenant data
        /// </summary>
        /// <param name="context"></param>
        private static void SeedDefaultTenant(IUnitOfWork unitOfWork)
        {
            Tenant tenant = new Tenant
            {
                Name = "ABC",
                City = "Toronto",
                Email = "abccompany@gmail.com"
            };


            unitOfWork.Tenants.AddOrUpdateSingle(tenant);
            unitOfWork.Commit();

        }

        public static void SeedDefaultApplicationRoles(RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork)
        {
            ApplicationRole applicationRole = new ApplicationRole
            {
                Name = "Administrator",
                NormalizedName = "Administrator",
                TenantID = unitOfWork.Tenants.Get(o => o.Name == "ABC").FirstOrDefault().ID
            };

            roleManager.CreateAsync(applicationRole).Wait();

            applicationRole = new ApplicationRole
            {
                Name = "AuthenticatedUser",
                NormalizedName = "AuthenticatedUser",
                TenantID = unitOfWork.Tenants.Get(o => o.Name == "ABC").FirstOrDefault().ID
            };

            roleManager.CreateAsync(applicationRole).Wait();
        }

    }
}
