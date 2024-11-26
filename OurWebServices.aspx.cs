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
            var OwnerAccount = dbContext.Users.FirstOrDefault(a => a.Email == User.Identity.Name); 
            RecipientAccounts = dbContext.Accounts.Where(a => a.UserId == OwnerAccount.ID);
            myWebService = new MyWebService();
            if (!IsPostBack)
            {

                if (!RecipientAccounts.Any())
                {
                    ddlRecipientAccount.DataSource = "No Account";
                }

                // Bind data to dropdowns
                BindSenderAccount();
            }
        }


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
                lblSenderBalance.Text = recipientAccount != null ? recipientAccount.Balance.ToString("C") : "0.00";
            }
            else
            {
                lblSenderBalance.Text = "0.00";
            }
        }

        // Handle Submit Payment
        protected void btnSubmitPayment_Click(object sender, EventArgs e)
        {
            int senderAccountID, receiverAccountID;
            decimal amount;
            string senderPassword = txtSenderPassword.Text;

            try
            {
                if (int.TryParse(ddlRecipientAccount.SelectedValue, out senderAccountID) &&
                    int.TryParse(txtReceiverAccountID.Text, out receiverAccountID) &&
                    decimal.TryParse(txtAmount.Text, out amount))
                {
                    bool paymentSuccess = myWebService.SubmitPayment(senderAccountID, receiverAccountID, amount, senderPassword);

                    if (paymentSuccess)
                    {
                        lblSubmitPaymentResult.Text = "Payment successful.";
                    }
                    else
                    {
                        lblSubmitPaymentResult.Text = "Payment failed. Please check the account details or balance.";
                    }

                    // Show transaction details in modal
                    ShowTransactionModal(senderAccountID, receiverAccountID, amount);
                }
                else
                {
                    lblSubmitPaymentResult.Text = "Invalid data entered. Please ensure all fields are filled out correctly.";
                }
            }
            catch
            {
                lblSubmitPaymentResult.Text = "An unexpected error occurred. Please try again later.";
            }
        }

        private void ShowTransactionModal(int senderAccountID, int receiverAccountID, decimal amount)
        {
            var dbContext = new MyDbContext();
            var recipientAccount = dbContext.Accounts.FirstOrDefault(x => x.ID == receiverAccountID);
            var senderAccount = dbContext.Accounts.FirstOrDefault(x => x.ID == senderAccountID);

            string accountNumber = senderAccountID.ToString();
            string recipientAccID = receiverAccountID.ToString();
            string senderAccType = senderAccount.Type;
            string recipientAccType = recipientAccount.Type;
            decimal senderBalance = senderAccount.Balance;
            DateTime date = DateTime.Now;

            txtAmount.Text = "0";

            string script = $@"
        $('#modalAccountName').text('{senderAccType}');
        $('#modalAccountNumber').text('{accountNumber}');
        $('#modalRecipientAccount').text('{recipientAccount.Type}');
        $('#modalSenderAccount').text('{senderAccount.Type}');
        $('#modalRecipAccNum').text('{recipientAccID}');
        $('#modalAmount').text('{amount.ToString("C")}');
        $('#modalSenderBalance').text('{senderBalance.ToString("C")}');
        $('#date').text('{date.Day}/{date.Month}/{date.Year}');
        $('#transactionModal').modal('show');
        $('#transactionModal').on('hidden.bs.modal', function () {{
            window.location.href = '/Accounts';
        }});
    ";

            ClientScript.RegisterStartupScript(this.GetType(), "showModal", script, true);
        }
    }
}
