using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class ApplicationUser : IdentityUser
    {
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Display(Name = "Default Store")]
            public int? DefaultStoreId { get; set; }
            
            [ForeignKey("DefaultStoreId")]
            public virtual Store? Store { get; set; }

    }
}
