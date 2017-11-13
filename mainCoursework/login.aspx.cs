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
			customLogging.newSession();
        }

		protected void submitCredentialsButton_Click(object sender, EventArgs e)
		{
			string attemptedName = usernameBox.Text;
			using (var checkCredentials = new defaultDataSetTableAdapters.usersTableAdapter())
			{
				if (checkCredentials.searchCredentials(attemptedName, passwordBox.Text) != null)
				{
					FormsAuthentication.RedirectFromLoginPage(attemptedName,false);
					//Session["loggedState"] = true;
					//customLogging.newEntry(attemptedName + " logged in", "user");
					//Server.Transfer("overview.aspx", true);
				}
				else
				{
					returnLabel.Text = "The Username or Password is incorrect.";
					customLogging.newEntry("Someone attempted to login with username '" + attemptedName + "' but the credentials were incorrect", "user");
				}
			}
		}
    }
}