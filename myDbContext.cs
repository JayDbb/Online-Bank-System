using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Principal;

namespace Online_Bank_System
{

    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Account -> User relationship
            modelBuilder.Entity<Account>()
                .HasRequired(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId);

            // Configure Transaction -> Account relationships
            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.SenderAccount)
                .WithMany(a => a.SentTransactions)
                .HasForeignKey(t => t.SenderAccountID)
                .WillCascadeOnDelete(false); // Disable cascading delete for SenderAccount

            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.ReceiverAccount)
                .WithMany(a => a.ReceivedTransactions)
                .HasForeignKey(t => t.ReceiverAccountID)
                .WillCascadeOnDelete(false); // Disable cascading delete for ReceiverAccount

            base.OnModelCreating(modelBuilder);
        }

    }


    public class User
    {
        public int ID { get; set; } // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; } // balance of bank account

        // Navigation Property
        public ICollection<Account> Accounts { get; set; }
    }


    public class Account
    {
        public int ID { get; set; } // Primary Key
        public string Type { get; set; } // Type of account (e.g., Flow, Sagicor)
        public string Password { get; set; } 
        public bool IsHidden { get; set; } // Allows the account to be hidden when removed
        public decimal Balance { get; set; }

        // Foreign Key for User
        public int UserId { get; set; }
        public User User { get; set; } // Navigation Property

        // Navigation Property for Transactions
        public ICollection<Transaction> SentTransactions { get; set; }
        public ICollection<Transaction> ReceivedTransactions { get; set; }
    }

    public class Transaction
    {
        public int ID { get; set; } // Primary Key
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        // Foreign Key for Sender Account
        public int SenderAccountID { get; set; }
        public Account SenderAccount { get; set; }

        // Foreign Key for Receiver Account
        public int ReceiverAccountID { get; set; }
        public Account ReceiverAccount { get; set; }
    }

}