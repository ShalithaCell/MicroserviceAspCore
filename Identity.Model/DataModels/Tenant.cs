using Identity.Model.DataModels.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.Model.DataModels
{
    public class Tenant : DefaultBaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }


        public ICollection<ApplicationRole> ApplicationRoles { get; }
        public ICollection<ApplicationUser> ApplicationUsers { get; }

    }
}
