using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class userProfile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//If there's noone logged in or they're not a customer, redirect them away
			if (Convert.ToString(Session["currentUser"]) == "" || Convert.ToString(Session["userType"]) != "customer")
			{
				Server.Transfer("~/default.aspx", false);
			}
		}
	}
}