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

            // One-to-Many relationship between User and Wallet
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.IdUser);

            // One-to-Many relationship between Wallet and Spending
            modelBuilder.Entity<Spending>()
                .HasOne(s => s.Wallet)
                .WithMany(w => w.Spendings)
                .HasForeignKey(s => s.IdWallet);

                // Seed initial data

            // User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "SampleUser",
                    Password = "SamplePassword",
                    Name = "John Doe",
                    Address = "123 Main Street",
                    Phone = 1234567890,
                    Mail = "john.doe@example.com"
                }
            );
            // Wallets
            modelBuilder.Entity<Wallet>().HasData(
                new Wallet { Id = 1, IdUser = 1 , ID_Card = 0123456789 },
                new Wallet { Id = 2, IdUser = 1 , ID_Card = 2345134553 }
            );

            // Spendings
            modelBuilder.Entity<Spending>().HasData(
                new Spending { Id = 1, Time = new DateTime(2023,12,3,15,30,0),  Description = SpendingCategory.Food, Amount = 50.00m, Balance = 100.00m, IdWallet = 1 },
                new Spending { Id = 2, Time = new DateTime(2023,12,4,17,30,0),  Description = SpendingCategory.Entertainment, Amount = 20.00m, Balance = 80.00m, IdWallet = 1 },
                new Spending { Id = 3, Time = new DateTime(2023,12,4,20,30,0),  Description = SpendingCategory.Moving, Amount = 30.00m, Balance = 50.00m, IdWallet = 1 },
                new Spending { Id = 4, Time = new DateTime(2023,12,5,15,30,0),  Description = SpendingCategory.Tuition, Amount = 100.00m, Balance = 50.00m, IdWallet = 2 },
                new Spending { Id = 5, Time = new DateTime(2023,12,5,20,30,0),  Description = SpendingCategory.RentHouse, Amount = 200.00m, Balance = 300.00m, IdWallet = 2 },
                new Spending { Id = 6, Time = new DateTime(2023,12,6,08,20,0),  Description = SpendingCategory.Other, Amount = 25.00m, Balance = 275.00m, IdWallet = 2 },
                new Spending { Id = 7, Time = new DateTime(2023,12,6,21,50,0),  Description = SpendingCategory.Food, Amount = 40.00m, Balance = 235.00m, IdWallet = 2 }
            );
        }
    }
}