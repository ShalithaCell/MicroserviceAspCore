using Identity.Infrastructure.IServices.Repository;
using Identity.Model.DataModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
                Email = "abc@gmail.com"
            };


            unitOfWork.Tenants.Insert(tenant);
            unitOfWork.Commit();
        }
    }
}
