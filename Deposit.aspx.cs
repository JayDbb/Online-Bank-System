using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Bank_System
{
    public partial class Deposit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyDbContext dbContext = new MyDbContext();
            var user = dbContext.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            Balance.Text = user.Balance.ToString("C");
        }
        protected void btnDeposit_Click(object sender, EventArgs e)
        {
            MyDbContext dbContext = new MyDbContext();


            if(int.TryParse(txtAmount.Text, out int amount))
            {
                var user = dbContext.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                user.Balance += amount;

                dbContext.SaveChanges();
                Response.Redirect("/Deposit");
            }
            else
            {

            }
        }
        
    }
}