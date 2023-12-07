using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string Name {  get; set; }
        public string? Address { get; set; }

        public virtual List<Wallet>? Wallets {  get; set; }

    }
}
