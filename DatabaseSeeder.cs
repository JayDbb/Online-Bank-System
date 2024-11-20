using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Bank_System
{
    public class DatabaseSeeder
    {
        public static void SeedTestUser()
        {
            using (var dbContext = new MyDbContext())
            {
                // Check if test user already exists
                if (!dbContext.Users.Any(u => u.Email == "testuser@example.com"))
                {
                    // Create a new user
                    var testUser = new User
                    {
                        Name = "Test User",
                        Email = "testuser@example.com",
                        Balance = 1000.00m,
                        Accounts = new List<Account>
                        {
                            new Account
                            {
                                Type = "Flow",
                                Password = "Test@1234",
                                IsHidden = false,
                                Balance = 500.00m
                            },
                            new Account
                            {
                                Type = "Sagicor",
                                Password = "Test@5678",
                                IsHidden = false,
                                Balance = 500.00m
                            }
                        }
                    };

                    // Add user to the database
                    dbContext.Users.Add(testUser);
                    dbContext.SaveChanges();

                    Console.WriteLine("Test user added to the database.");
                }
                else
                {
                    Console.WriteLine("Test user already exists in the database.");
                }
            }
        }
    }
}
