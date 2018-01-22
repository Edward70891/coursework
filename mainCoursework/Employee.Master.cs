using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using commonClasses;

namespace mainCoursework
{
    public partial class EmployeeSiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if (Convert.ToString(Session["userType"]) != "employee")
			{
				Server.Transfer("~/default.aspx", true);
			}
			//Show who is logged in, whether they are an admin and make sure the sign out button is visible
			string admin = "";
			if (Convert.ToString(Session["userIsAdmin"]) == "admin")
			{
				admin = " You are an admin!";
			}
			usernameLabel.Text = "Welcome, employee " + Session["currentUser"] + "!" + admin;
			signOut.Visible = true;
		}

		protected void signOut_Click(object sender, EventArgs e)
		{
			Session["isLoggedIn"] = false;
			Session["currentUser"] = "";
			Session["userIsAdmin"] = false;
			Session["userType"] = "";
			customLogging.newEntry("User logged out");
			Server.Transfer("~/default.aspx", false);
		}

		protected void employeeIn_Click(object sender, EventArgs e)
		{
			Server.Transfer("~/employeeLogin.aspx", false);
		}
	}
}