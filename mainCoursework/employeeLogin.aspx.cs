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
    public partial class employeeLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			
        }

		protected void submitEmployeeCredentialsButton_Click(object sender, EventArgs e)
		{
			//Pull the given username into a variable
			string attemptedName = employeeUsernameBox.Text;
			using (var checkCredentials = new defaultDataSetTableAdapters.employeesTableAdapter())
			{
				//Runs if a user with the given credentials exists
				if (checkCredentials.loginCheck(attemptedName, employeePasswordBox.Text) != null)
				{
					//Signs the user in and logs the signin to the logfile
					Session["isLoggedIn"] = true;
					Session["currentUser"] = attemptedName;
					Session["userType"] = "employee";
					Session["userIsAdmin"] = true;
					Server.Transfer("~/mamangerial/staffOverview.aspx", false);
					customLogging.newEntry("Employee " + attemptedName + " logged in");
				}
				else
				{
					//Posts an error and logs the attempted login to the logfile
					empoyeeLoginReturnLabel.Text = "The Username or Password is incorrect.";
					customLogging.newEntry("Someone attempted to login as an employee with username '" + attemptedName + "' but the credentials were incorrect");
				}
			}
		}
    }
}