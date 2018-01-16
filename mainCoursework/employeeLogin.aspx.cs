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
			//Check the inputs are clean of SQL injection
			if (SQLSanitization.sanitizeCheck(new string[] { attemptedName, employeePasswordBox.Text }))
			{
				//Initialize the database connection
				using (var checkCredentials = new defaultDataSetTableAdapters.employeesTableAdapter())
				{
					var loginCheck = checkCredentials.loginCheck(attemptedName, employeePasswordBox.Text);
					if (loginCheck != null)
					{
						//Sets relevant Session information so other pages can determine information about the logged session
						Session["isLoggedIn"] = true;
						Session["currentUser"] = attemptedName;
						Session["userType"] = "employee";
						Session["userIsAdmin"] = Convert.ToBoolean(loginCheck);
						//Logs the signin and whether the user was an admin or not
						if (Convert.ToBoolean(Session["userIsAdmin"]))
						{
							customLogging.newEntry("Admin " + attemptedName + " logged in");
						}
						else
						{
							customLogging.newEntry("Employee " + attemptedName + " logged in");
						}
						//Redirects to the staff overview page
						Server.Transfer("~/staffOverview.aspx", false);
					}
					else
					{
						//Posts an error and logs the attempted login to the logfile
						employeeLoginReturnLabel.Text = "The Username or Password is incorrect.";
						customLogging.newEntry("Someone attempted to login as an employee with username '" + attemptedName + "' but the credentials were incorrect");
					}
				}
			}
			else
			{
				employeeLoginReturnLabel.Text = SQLSanitization.sanitizeErrorMessage;
			}
		}
    }
}