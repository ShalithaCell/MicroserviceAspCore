using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Identity.Model.DataModels.BaseModels
{
    /// <summary>
    /// All the default attributes are defined here.
    /// </summary>
    public class DefaultBaseEntity
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public DateTime RegistedDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
