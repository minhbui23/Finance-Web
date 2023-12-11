using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Finance.Models.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 1)]
        public string ID_Card {get; set;}

        [Required]
        public decimal Balance { get; set; }

        public string UserId { get; set; }

        // Navigation property for the relationship with IdentityUser
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        // Navigation property for the relationship with Spending
        public virtual List<Spending>? Spendings { get; set; }
    }
}