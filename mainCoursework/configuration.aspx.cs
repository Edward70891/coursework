using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class configuration : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var checkLogin = Convert.ToBoolean(Session["loggedState"]);
			if (checkLogin == false)
			{
				Server.Transfer("login.aspx", true);
			}
		}

		protected void usersDisplayTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "deleteUser":
					//Checks if they're trying to delete the admin account
					if (Convert.ToInt32(usersDisplayTable.Rows[Convert.ToInt32(e.CommandArgument)]) == 0)
					{
						returnLabel.Text = "You can't delete the central admin account";
					}
					//Prompts them to delete the account
					else
					{
						returnLabel.Text = "Do you really want to delete the account '" + Convert.ToString(usersDisplayTable.RowHeaderColumn) +"?";
					}
					break;
				case "changeUserPassword":
					break;
				default:
					break;
			}
		}
	}
}