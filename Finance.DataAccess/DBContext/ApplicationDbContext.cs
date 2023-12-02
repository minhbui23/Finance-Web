using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Finance.Models.Models;
using Microsoft.EntityFrameworkCore;


namespace Finance.DataAccess.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Spending> Spendings {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<Wallet> Wallets {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            
        }
    }
}