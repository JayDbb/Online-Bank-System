using System;
using System.Linq;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Bank_System
{
    public partial class Top_Up : Page
    {
        private MyDbContext dbContext;
        private User OwnerAccount; // Represents the owner's main account
        private IQueryable<Account> RecipientAccounts; // Represents the recipient accounts owned by the same user

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize the database context
            dbContext = new MyDbContext();

            // Fetch the owner (logged-in user) and accounts (Replace 1 with the logged-in User ID)
            int userId = 1; // Replace this with dynamic user identification
            OwnerAccount = dbContext.Users.FirstOrDefault(a => a.ID == userId); // Assuming "Primary" indicates the owner's main account
            RecipientAccounts = dbContext.Accounts.Where(a => a.UserId == userId);
            if (!IsPostBack)
            {

                if (OwnerAccount == null || !RecipientAccounts.Any())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No accounts available for the top-up.');", true);
                    return;
                }

                // Bind data to dropdowns
                BindSenderAccount();
                BindRecipientAccounts();
            }
        }

        private void BindSenderAccount()
        {
            //ddlSenderAccount.Items.Clear();
            //ddlSenderAccount.Items.Add(new ListItem(OwnerAccount.Type + " - Balance: " + OwnerAccount.Balance.ToString("C"), OwnerAccount.ID.ToString()));
            AccID.Text = OwnerAccount.ID.ToString();
            AccountName.Text = OwnerAccount.Name;
            lblSenderBalance.Text = OwnerAccount.Balance.ToString("C");
        }

        private void BindRecipientAccounts()
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

        protected void btnTopUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlRecipientAccount.SelectedValue) || string.IsNullOrEmpty(txtAmount.Text) ||
                !decimal.TryParse(txtAmount.Text, out decimal topUpAmount))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please enter a valid amount and select a recipient account.');", true);
                return;
            }

            int recipientAccountId = int.Parse(ddlRecipientAccount.SelectedValue);
            var recipientAccount = dbContext.Accounts.FirstOrDefault(a => a.ID == recipientAccountId);


            if (recipientAccount == null)
            {
                topupValidator.ErrorMessage = "Recipient account not found.";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('R.');", true);
                return;
            }

            if (OwnerAccount.Balance < topUpAmount)
            {
                topupValidator.ErrorMessage = "Insufficient balance in the sender account.";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('');", true);
                return;
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

            // Update balances on UI
            lblSenderBalance.Text = OwnerAccount.Balance.ToString("C");
            lblRecipientBalance.Text = recipientAccount.Balance.ToString("C");

            txtAmount.Text = "0";

            Response.Redirect("/");

            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Top-up successful!');", true);
        }
    }
}
