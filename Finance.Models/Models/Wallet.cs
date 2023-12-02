using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public int Phone { get; set; }
        public string? Mail { get; set; }
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User? User { get; set; }

        // Navigation property for the relationship with Spending
        public List<Spending>? Spendings { get; set; }
    }
}