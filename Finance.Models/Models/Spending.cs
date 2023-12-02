using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models.Models
{
    public class Spending
    {        
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }

        [DataType(DataType.Time)]
        public TimeOnly Time { get; set; }

        [Required]
        [EnumDataType(typeof(SpendingCategory))]
        public SpendingCategory Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public int IdWallet { get; set; }
        [ForeignKey("IdWallet")]
        public Wallet? Wallet { get; set; }
    }
    public enum SpendingCategory
    {
        Food,
        Entertainment,
        Moving,
        Tuition,
        RentHouse,
        Other
    }
    
}