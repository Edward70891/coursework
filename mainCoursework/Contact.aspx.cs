using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class Contact : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int loggedCheck = Convert.ToInt32(Session["loggedState"]);
			if (loggedCheck == 1)
			{
				Server.Transfer("Default.aspx", true);
			}
		}
	}
}