using System;
using System.Collections.Generic;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            myWebService = new MyWebService();
        }

        protected void btnCheckAccount_Click(object sender, EventArgs e)
        {
            int accountID;
            if (int.TryParse(txtAccountID.Text, out accountID))
            {
                bool accountExists = myWebService.DoesAccountExist(accountID);
                lblCheckAccountResult.Text = accountExists ? "Account exists." : "Account does not exist.";
            }
            else
            {
                lblCheckAccountResult.Text = "Invalid Account ID.";
            }
        }

        // Handle Add Account
        protected void btnAddAccount_Click(object sender, EventArgs e)
        {
            int accountID;
            string password = txtPassword.Text;

            if (int.TryParse(txtAddAccountID.Text, out accountID))
            {
                bool success = myWebService.AddAccount(accountID, password);
                lblAddAccountResult.Text = success ? "Account added successfully." : "Failed to add account.";
            }
            else
            {
                lblAddAccountResult.Text = "Invalid Account ID.";
            }
        }

        // Handle Submit Payment
        protected void btnSubmitPayment_Click(object sender, EventArgs e)
        {
            int senderAccountID, receiverAccountID;
            decimal amount;
            string senderPassword = txtSenderPassword.Text;

            if (int.TryParse(txtSenderAccountID.Text, out senderAccountID) &&
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
