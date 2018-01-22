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
    public partial class CustomerSiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			//Check if anyone is logged in
			if (Convert.ToString(Session["currentUser"]) == "")
			{
				// Show no one is logged in
				usernameLabel.Text = "No user logged in";
				signOut.Visible = false;
			}
			else
			{
				//Show who is logged in and make sure the sign out button is visible
				usernameLabel.Text = "Welcome, " + Session["currentUser"] + "!";
				signOut.Visible = true;
			}

			if (Convert.ToString(Session["userType"]) != "employee")
			{
				employeeRedirect.Visible = false;
			}
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

		protected void employeeRedirect_Click(object sender, EventArgs e)
		{
			Server.Transfer("~/staffOverview.aspx", false);
		}
	}
}