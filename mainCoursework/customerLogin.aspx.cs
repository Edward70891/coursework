using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.OleDb;
using System.Threading;
using commonClasses;
using System.Web.UI.WebControls;

namespace mainCoursework
{
    public partial class customerLogin : System.Web.UI.Page
    {
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void submitCustomerCredentialsButton_Click(object sender, EventArgs e)
		{
			//Pull the given username into a variable
			string attemptedName = customerUsernameBox.Text;
			using (var checkCredentials = new defaultDataSetTableAdapters.customersTableAdapter())
			{
				//Runs if a user with the given credentials exists
				if (checkCredentials.loginCheck(attemptedName, customerPasswordBox.Text) != null)
				{
					//Signs the user in and logs the signin to the logfile
					Session["isLoggedIn"] = true;
					Session["currentUser"] = attemptedName;
					Session["userType"] = "user";
					Session["userIsAdmin"] = false;
					customLogging.newEntry("Customer " + attemptedName + " logged in");
					Server.Transfer("~/default.aspx", false);
				}
				else
				{
					//Posts an error and logs the attempted login to the logfile
					customerLoginReturnLabel.Text = "The Username or Password is incorrect.";
					customLogging.newEntry("Someone attempted to login as a customer with username '" + attemptedName + "' but the credentials were incorrect");
				}
			}
		}

		protected void registerButton_Click(object sender, EventArgs e)
		{
			Server.Transfer("registerCustomer.aspx", false);
		}
	}
}