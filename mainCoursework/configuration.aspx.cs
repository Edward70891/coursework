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
			
		}

		protected void usersDisplayTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			string username = usersDisplayTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].ToString();
			switch (e.CommandName)
			{
				case "deleteUser":
					//Checks if they're trying to delete the admin account
					if (username == "admin")
					{
						returnLabel.Text = "You can't delete the root admin account!";
					}

					//Prompts them to delete the account
					else
					{
						returnLabel.Text = "Do you really want to delete the account '" + username +"?";
						deleteConfirm.Visible = true;
						//using (var deleteUser = new defaultDataSetTableAdapters.usersTableAdapter())
						//{
						//	deleteUser.deleteUser(username);
						//}
					}
					break;
				case "changeUserPassword":
					break;
			}
		}
	}
}