using Online_Bank_System.myWebServiceRef;
using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Services;
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
            //int userId = dbContext; // Replace this with dynamic user identification
            OwnerAccount = dbContext.Users.FirstOrDefault(a => a.Email == User.Identity.Name); 
            RecipientAccounts = dbContext.Accounts.Where(a => a.UserId == OwnerAccount.ID);
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
                BindRecipientAccounts();
            }
        }

        private void BindSenderAccount()
        {
            //ddlSenderAccount.Items.Clear();
            //ddlSenderAccount.Items.Add(new ListItem(OwnerAccount.Type + " - Balance: " + OwnerAccount.Balance.ToString("C"), OwnerAccount.ID.ToString()));
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No accounts available for the top-up."+ OwnerAccount.Name +" ," + OwnerAccount.ID.ToString() + "');", true);

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

            myWebServiceRef.MyWebService ws = new myWebServiceRef.MyWebService();

            

            var recipientAccount = dbContext.Accounts.FirstOrDefault(a => a.ID == recipientAccountId);

            
            if (recipientAccount == null)
            {
                topupValidator.ErrorMessage = "Recipient account not found.";
                return;
            }



            if (OwnerAccount.Balance < topUpAmount)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('R." + OwnerAccount.Balance + ", " + topUpAmount + "');", true);
                //cvAmount.Visible = true;
                //topupValidator
                topupValidator.ErrorMessage = "Insufficient balance in the sender account.";
                topupValidator.IsValid = false;
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('');", true);
                return;
            }


            ws.TopUp(recipientAccountId, OwnerAccount.ID, (decimal)topUpAmount);

            OwnerAccount.Balance -= topUpAmount; 
            recipientAccount.Balance += topUpAmount;

            // Update balances on UI
            lblSenderBalance.Text = OwnerAccount.Balance.ToString("C");
            lblRecipientBalance.Text = recipientAccount.Balance.ToString("C");
  

            // Get form values for the modal
            string accountName = AccountName.Text;
            string accountNumber = AccID.Text;
            string recipientBalance = lblRecipientBalance.Text;
            string amount = txtAmount.Text;
            DateTime date = DateTime.Now;
            string senderBalance = lblSenderBalance.Text;
            txtAmount.Text = "0";

            // Use JavaScript to show the modal and populate its content
            string script = $@"
                $('#modalAccountName').text('{accountName}');
                $('#modalAccountNumber').text('{accountNumber}');
                $('#modalRecipientAccount').text('{recipientAccount.Type}');
                $('#modalRecipientBalance').text('{recipientAccount.Balance.ToString("C")}');
                $('#modalAmount').text('{amount}');
                $('#modalSenderBalance').text('{OwnerAccount.Balance.ToString("C")}');       
                $('#date').text('{date.Day}/{date.Month}/{date.Year}');

                $('#transactionModal').modal('show');

                $('#transactionModal').on('hidden.bs.modal', function () {{
                    window.location.href = '/Accounts';
                }});
                ";

            ClientScript.RegisterStartupScript(this.GetType(), "showModal", script, true);


            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Top-up successful!');", true);
        }
    }
}
