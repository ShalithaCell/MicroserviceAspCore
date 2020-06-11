using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Identity.Model.DataModels
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        public int TenantID { get; set; }

        [ForeignKey("TenantID")]
        public Tenant tenant { get; }
    }
}
