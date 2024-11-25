using System;
using System.Linq;
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
            string pwd = txtPassword.Text.Trim();

            // Server-side validation
            if (Regex.IsMatch(accountNumber, @"^\d$"))
            {
                // Logic for adding the account (e.g., save to database)
                //Response.Write("<script>alert('Account added successfully.');</script>");
            }
            else
            {
                cvAccountNumber.IsValid = false;
                cvAccountNumber.ErrorMessage = "Account number must be exactly 12 digits.";
            }

            //@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,12}$"  
            // Server-side validation
            if (Regex.IsMatch(pwd, @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,12}$"))
            {
                // Logic for adding the account (e.g., save to database)
                Response.Write("<script>alert('Account added successfully.');</script>");
            }
            else
            {
                CustomValidatorPassword.IsValid = false;
                CustomValidatorPassword.ErrorMessage = "Password Invalid Format.";
                return;
            }

            myWebServiceRef.MyWebService ws = new myWebServiceRef.MyWebService();

            ws.AddAccount(int.Parse(accountNumber), "");
        }


        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {

            string password = newTxtPassword.Text.Trim();
            string accountType = ddlAccountType.SelectedValue;

            // Validate data and create account
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(accountType))
            {
                using (var db = new MyDbContext())
                {
                    var Id = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                    // Create new account linked to the user
                    Account account = new Account
                    {
                        Type = accountType,
                        Password = password,
                        Balance = 0,
                        UserId = Id.ID, // user ID
                        IsHidden = false
                    };
                    db.Accounts.Add(account);
                    db.SaveChanges();
                }

                Response.Redirect("/Top-Up");
                //Response.Write("<script>alert('Account created successfully!');</script>");
            }
            else
            {
                Response.Write("<script>alert('Please fill out all fields.');</script>");
            }
        }


    }


}
