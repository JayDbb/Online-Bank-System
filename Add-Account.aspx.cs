using System;
using System.Text.RegularExpressions;

namespace Online_Bank_System
{
    public partial class Add_Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string accountNumber = txtAccountNumber.Text.Trim();

            // Server-side validation
            if (Regex.IsMatch(accountNumber, @"^\d{12}$"))
            {
                // Logic for adding the account (e.g., save to database)
                Response.Write("<script>alert('Account added successfully.');</script>");
            }
            else
            {
                cvAccountNumber.IsValid = false;
                cvAccountNumber.ErrorMessage = "Account number must be exactly 12 digits.";
            }
        }
    }
}
