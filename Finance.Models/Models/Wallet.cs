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
        public long ID_Card {get; set;}
        
        [Required]
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual User? User { get; set; }

        // Navigation property for the relationship with Spending
        public virtual List<Spending>? Spendings { get; set; }
    }
}