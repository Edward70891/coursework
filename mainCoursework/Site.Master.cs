using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
    public partial class SiteMaster : MasterPage
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
		}

		protected void signOut_Click(object sender, EventArgs e)
		{
			Session["isLoggedIn"] = false;
			Session["currentUser"] = "";
			Session["userIsAdmin"] = false;
			Session["userType"] = "";
			Server.Transfer("~/default.aspx", false);
		}
	}
}