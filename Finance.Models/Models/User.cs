using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Finance.Models.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public int Phone { get; set; }
        public string? Mail { get; set; }

        public virtual List<Wallet>? Wallets { get; set; }
    }
}