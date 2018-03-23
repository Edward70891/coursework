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
			//Redirect the user to the default page if they are already logged in
			if (Convert.ToString(Session["isLoggedIn"]) == "True")
			{
				Server.Transfer("~/default.aspx", false);
			}
		}

		protected void submitCustomerCredentialsButton_Click(object sender, EventArgs e)
		{
			//Checks if there are any SQL sensitive characters in the inputs
			if (customSecurity.sanitizeCheck(new string[] { customerUsernameBox.Text, customerPasswordBox.Text }))
			{
				//Pull the given username into a variable
				string attemptedName = customerUsernameBox.Text;
				using (var checkCredentials = new defaultDataSetTableAdapters.customersTableAdapter())
				{
					//Runs if a user with the given credentials exists
					if (checkCredentials.loginCheck(attemptedName, customSecurity.generateMD5(customerPasswordBox.Text)) != null)
					{
						//Signs the user in and logs the signin to the logfile
						Session["isLoggedIn"] = "True";
						Session["currentUser"] = attemptedName;
						Session["userType"] = "customer";
						Session["userIsAdmin"] = "False";
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
			else
			{
				customerLoginReturnLabel.Text = customSecurity.sanitizeErrorMessage;
			}
		}

		protected void registerButton_Click(object sender, EventArgs e)
		{
			Server.Transfer("registerCustomer.aspx", false);
		}

		protected void employeeRedirect_Click(object sender, EventArgs e)
		{
			Server.Transfer("employeeLogin.aspx", false);
		}
	}
}