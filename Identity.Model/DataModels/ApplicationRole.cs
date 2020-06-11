using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Identity.Model.DataModels
{
    /// <summary>
    /// Role class inherited from IdentityRole and added custom attrubutes
    /// </summary>
    public class ApplicationRole : IdentityRole<int>
    {
        [Required]
        public int TenantID { get; set; }

        [ForeignKey("TenantID")]
        public Tenant tenant { get; }
    }
}
