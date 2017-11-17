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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			
        }

		protected void submitCredentialsButton_Click(object sender, EventArgs e)
		{
			//Pull the given username into a variable
			string attemptedName = usernameBox.Text;
			using (var checkCredentials = new defaultDataSetTableAdapters.usersTableAdapter())
			{
				//Runs if a user with the given credentials exists
				if (checkCredentials.searchCredentials(attemptedName, passwordBox.Text) != null)
				{
					//Signs the user in and logs the signin to the logfile
					FormsAuthentication.RedirectFromLoginPage(attemptedName,false);
					customLogging.newEntry(attemptedName + " logged in");
				}
				else
				{
					//Posts an error and logs the attempted login to the logfile
					returnLabel.Text = "The Username or Password is incorrect.";
					customLogging.newEntry("Someone attempted to login with username '" + attemptedName + "' but the credentials were incorrect");
				}
			}
		}
    }
}