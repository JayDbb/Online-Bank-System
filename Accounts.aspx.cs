using Online_Bank_System.myWebServiceRef;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Bank_System
{
    public partial class Accounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyDbContext db = new MyDbContext();
                var uid = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                if (uid == null)
                {
                    Response.Redirect("/accounts/Login");
                    return;
                }

                var acc = db.Accounts.Where(x => x.UserId == uid.ID && x.IsHidden == false).ToList();
                AccRepeater.DataSource = acc;
                AccRepeater.DataBind();
            }
        }

        protected void AccRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Check if the item is a data item (not header, footer, or separator)
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Find the button inside the repeater item
                Button iwhatButton = (Button)e.Item.FindControl("iwhat");

                // Get the Account ID from the data item
                var accountId = DataBinder.Eval(e.Item.DataItem, "ID").ToString();

                // Set the data-target attribute dynamically
                iwhatButton.Attributes["data-target"] = "#DelAccModal_" + accountId;
            }
        }

        //public void Remove(object sender, EventArgs e)
        //{
        //    Button editButton = (Button)sender;
        //    string accountId = editButton.CommandArgument;

        //    // Find the Repeater item containing the clicked button
        //    RepeaterItem item = (RepeaterItem)editButton.NamingContainer;

        //    // Find the controls within the current Repeater item
        //    Label delAccModalLabel = (Label)item.FindControl("DelAccModalLabel");
        //    Button delButton = (Button)item.FindControl("del");

        //    if (delAccModalLabel != null && delButton != null)
        //    {
        //        // Set the text of the modal label
        //        delAccModalLabel.Text = "Are you sure you want to remove Account ID: " + accountId;

        //        // Set the CommandArgument for the Delete button
        //        delButton.CommandArgument = accountId;

        //        // Open the modal using jQuery, targeting the modal with the dynamic ID
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
        //            "$('#DelAccModal_" + accountId + "').modal('show');", true);
        //    }
        //}



        protected void btnCheckAccount_Click(object sender, EventArgs e)
        {
            int accountID;
            if (int.TryParse(txtAccountID.Text, out accountID))
            {
                myWebServiceRef.MyWebService ws = new myWebServiceRef.MyWebService();
                bool accountExists = ws.DoesAccountExist(accountID);
                lblCheckAccountResult.Text = accountExists ? "Account exists." : "Account does not exist.";
                lblCheckAccountResult.ForeColor = accountExists ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            }
            else
            {
                lblCheckAccountResult.Text = "Invalid Account ID.";
            }
        }


        public void SaveChangesDel(object sender, EventArgs e)
        {
            MyDbContext db = new MyDbContext();

            Button delButton = (Button)sender;
            int productId = int.Parse(delButton.CommandArgument);

            

            var remove = db.Accounts.FirstOrDefault(x => x.ID == productId);
            
            remove.IsHidden = true;

            db.SaveChanges();
            Response.Redirect("/Accounts");


        }




    }
}