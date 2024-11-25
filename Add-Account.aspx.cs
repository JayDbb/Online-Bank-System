using System;
using System.Data.Entity;
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
            

            //@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,12}$"  
            // Server-side validation
            //if (Regex.IsMatch(pwd, @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,12}$"))
            //{
            //    // Logic for adding the account (e.g., save to database)
            //    Response.Write("<script>alert('Account added successfully.');</script>");
            //}
            //else
            //{
            //    CustomValidatorPassword.IsValid = false;
            //    CustomValidatorPassword.ErrorMessage = "Password Invalid Format.";
            //    return;
            //}

            MyDbContext dbContext = new MyDbContext();
            int accountNumberInt;

            if (int.TryParse(accountNumber, out accountNumberInt))
            {
                // Now you can safely use accountNumberInt
            var foundAccount = dbContext.Accounts.FirstOrDefault(acc => acc.ID == accountNumberInt);
                if (foundAccount.Password != pwd)
                {
                    CustomValidatorPassword.IsValid = false;
                    CustomValidatorPassword.ErrorMessage = "Password Invalid.";
                    return;
                }

                myWebServiceRef.MyWebService ws = new myWebServiceRef.MyWebService();


                ws.AddAccount(accountNumberInt, pwd);
            }
            else
            {
                accNumValidator.IsValid = false;
                accNumValidator.ErrorMessage = "Invalid Account number";
            }

            Response.Redirect("/Accounts");
        }


        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {

            string password = newTxtPassword.Text.Trim();
            string accountType = ddlAccountType.SelectedValue;

            if (!Regex.IsMatch(password, @"^\\d{4}$"))
            {
                cvPassword.IsValid = false;
                cvPassword.ErrorMessage = "Password must be 4 digits";
                return;
            }

            if(accountType == null)
            {
                cvAccountType.IsValid = false;
                return;

            }


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
