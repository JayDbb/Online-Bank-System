using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Online_Bank_System
{
    /// <summary>
    /// Web Service for checking accounts and handling payments
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Uncomment the next line to allow the WebService to be called from JavaScript (AJAX)
    // [System.Web.Script.Services.ScriptService]
    public class MyWebService : System.Web.Services.WebService
    {
        private MyDbContext dbContext;

        public MyWebService()
        {
            this.dbContext = new MyDbContext();
        }

        // Check if an account exists by its ID
        [WebMethod]
        public bool DoesAccountExist(int AccountID)
        {
            var isFound = dbContext.Accounts.Any(acc => acc.ID == AccountID);
            return isFound;
        }

        //Top Up Account

        [WebMethod]
        public bool TopUp(int recipientAccountId, int OwnerAccountID, decimal topUpAmount)
        {
            User OwnerAccount = dbContext.Users.FirstOrDefault(a => a.ID == OwnerAccountID);
            Account recipientAccount = dbContext.Accounts.FirstOrDefault(a => a.UserId == recipientAccountId);


            if (recipientAccount == null)
            {
                return false;
            }

            if (OwnerAccount.Balance < topUpAmount)
            {
                //topupValidator.ErrorMessage = "Insufficient balance in the sender account.";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('');", true);
                return false;
            }

            // Deduct and credit balances
            OwnerAccount.Balance -= topUpAmount;
            recipientAccount.Balance += topUpAmount;

            // Record the transaction
            var transaction = new Transaction
            {
                Amount = topUpAmount,
                Date = DateTime.Now,
                SenderAccountID = OwnerAccount.ID,
                ReceiverAccountID = recipientAccount.ID
            };
            dbContext.Transactions.Add(transaction);

            // Save changes
            dbContext.SaveChanges();

            return true;
        }


        // Add an account (assuming the account exists and the password is correct)
        [WebMethod]
        public bool AddAccount(int AccountID, string Password)
        {
            bool isAccount = DoesAccountExist(AccountID);

            if (isAccount)
            {
                var foundAccount = dbContext.Accounts.FirstOrDefault(acc => acc.ID == AccountID);

                // Validate password before making changes
                if (foundAccount.Password != Password)
                {
                    return false;
                }

                foundAccount.IsHidden = false;
                dbContext.Accounts.AddOrUpdate(foundAccount);
                dbContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        // Payment handling section
        [WebMethod]
        public bool SubmitPayment(int senderAccountID, int receiverAccountID, decimal amount, string senderPassword)
        {
            // Check if both accounts exist
            var senderAccount = dbContext.Accounts.FirstOrDefault(acc => acc.ID == senderAccountID);
            var receiverAccount = dbContext.Accounts.FirstOrDefault(acc => acc.ID == receiverAccountID);

            if (senderAccount == null || receiverAccount == null)
            {
                return false;
            }

            if (senderAccount.Password != senderPassword)
            {
                return false;
            }

            if (senderAccount.Balance < amount)
            {
                return false;
            }

            //Payment:
            senderAccount.Balance -= amount;

            //Add to receiver account
            receiverAccount.Balance += amount;

            //Save changes to both accounts
            dbContext.Accounts.AddOrUpdate(senderAccount);
            dbContext.Accounts.AddOrUpdate(receiverAccount);

            //Transaction record
            Transaction transaction = new Transaction
            {
                Amount = amount,
                SenderAccountID = senderAccountID,
                ReceiverAccountID = receiverAccountID,
                Date = DateTime.Now
            };

            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();

            return true;
        }
    }
}

