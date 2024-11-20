using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Online_Bank_System
{
    /// <summary>
    /// Summary description for MyWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MyWebService : System.Web.Services.WebService
    {
        private MyDbContext dbContext;
        public MyWebService() { 
            this.dbContext = new MyDbContext();

        }

        [WebMethod]
        public bool DoesAccountExist(int AccountID)
        {
            var isFound = dbContext.Accounts.Any(acc => acc.ID == AccountID);
            return isFound;
        }

        [WebMethod]
        public bool AddAccount(int AccountID, string Password)
        {
            bool isAccount = DoesAccountExist(AccountID);

            if (isAccount) {

                var foundAccount = dbContext.Accounts.FirstOrDefault(acc => acc.ID == AccountID);

                if (foundAccount.Password != Password) { 
                    return false;
                }

                foundAccount.IsHidden = false;

                dbContext.Accounts.AddOrUpdate(foundAccount); 



                

                return true;
            }
            else {
                return false;
            }




        }

    }
}
