using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Online_Bank_System.myWebServiceRef;

namespace Online_Bank_System
{
    public partial class OurWebServices : System.Web.UI.Page
    {
        private MyWebService myWebService;
        private IQueryable<Account> RecipientAccounts; // Represents the recipient accounts owned by the same user


        protected void Page_Load(object sender, EventArgs e)
        {
            var dbContext = new MyDbContext();
            var OwnerAccount = dbContext.Users.FirstOrDefault(a => a.Email == User.Identity.Name); // Assuming "Primary" indicates the owner's main account
            RecipientAccounts = dbContext.Accounts.Where(a => a.UserId == OwnerAccount.ID);
            myWebService = new MyWebService();
            if (!IsPostBack)
            {

                if (!RecipientAccounts.Any())
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No accounts available for the top-up.');", true);
                    //var acc = { ID = "0" TypeWithID = "No Accounts" };
                    ddlRecipientAccount.DataSource = "No Account";
                }

                // Bind data to dropdowns
                BindSenderAccount();
            }
        }

        //protected void btnCheckAccount_Click(object sender, EventArgs e)
        //{
        //    int accountID;
        //    if (int.TryParse(txtAccountID.Text, out accountID))
        //    {
        //        bool accountExists = myWebService.DoesAccountExist(accountID);
        //        lblCheckAccountResult.Text = accountExists ? "Account exists." : "Account does not exist.";
        //    }
        //    else
        //    {
        //        lblCheckAccountResult.Text = "Invalid Account ID.";
        //    }
        //}

        // Handle Add Account
        //protected void btnAddAccount_Click(object sender, EventArgs e)
        //{
        //    int accountID;
        //    string password = txtPassword.Text;

        //    if (int.TryParse(txtAddAccountID.Text, out accountID))
        //    {
        //        bool success = myWebService.AddAccount(accountID, password);
        //        lblAddAccountResult.Text = success ? "Account added successfully." : "Failed to add account.";
        //    }
        //    else
        //    {
        //        lblAddAccountResult.Text = "Invalid Account ID.";
        //    }
        //}

        private void BindSenderAccount()
        {

            var accounts = RecipientAccounts.Select(a => new
            {
                a.ID, // Keeps the actual ID for DataValueField
                TypeWithID = a.Type + " - " + a.ID // Combines Type and ID for display
            }).ToList();

            ddlRecipientAccount.DataSource = accounts.ToList();
            ddlRecipientAccount.DataTextField = "TypeWithID";
            ddlRecipientAccount.DataValueField = "ID";
            ddlRecipientAccount.DataBind();
            ddlRecipientAccount.Items.Insert(0, new ListItem("Select Recipient Account", ""));
        }

        protected void ddlRecipientAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRecipientBalance();
        }

        private void UpdateRecipientBalance()
        {
            if (int.TryParse(ddlRecipientAccount.SelectedValue, out int accountId))
            {
                var recipientAccount = RecipientAccounts.FirstOrDefault(a => a.ID == accountId);
                lblRecipientBalance.Text = recipientAccount != null ? recipientAccount.Balance.ToString("C") : "0.00";
            }
            else
            {
                lblRecipientBalance.Text = "0.00";
            }
        }

        // Handle Submit Payment
        protected void btnSubmitPayment_Click(object sender, EventArgs e)
        {
            int senderAccountID, receiverAccountID;
            decimal amount;
            string senderPassword = txtSenderPassword.Text;

            if (int.TryParse(ddlRecipientAccount.SelectedValue, out senderAccountID) &&
                int.TryParse(txtReceiverAccountID.Text, out receiverAccountID) &&
                decimal.TryParse(txtAmount.Text, out amount))
            {
                bool paymentSuccess = myWebService.SubmitPayment(senderAccountID, receiverAccountID, amount, senderPassword);
                lblSubmitPaymentResult.Text = paymentSuccess ? "Payment successful." : "Payment failed.";
            }
            else
            {
                lblSubmitPaymentResult.Text = "Invalid data entered.";
            }
        }
    }
}
