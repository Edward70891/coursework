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
			if (HttpContext.Current.User.Identity.Name == "")
			{
				// Show no one is logged in
				usernameLabel.Text = "No user logged in";
				signOut.Visible = false;
			}
			else
			{
				//Show who is logged in and make sure the sign out button is visible
				usernameLabel.Text = "Welcome, " + HttpContext.Current.User.Identity.Name + "!";
				signOut.Visible = true;
			}
		}

		protected void signOut_Click(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();
			FormsAuthentication.RedirectToLoginPage();
		}
	}
}