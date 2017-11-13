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

		string username;

		//Deleting and change account passwords
		protected void usersDisplayTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			username = usersDisplayTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
			switch (e.CommandName)
			{
				case "deleteUser":
					//Checks if they're trying to delete the admin account
					if (username == "admin")
					{
						returnLabel.Text = "You can't delete the root admin account!";
						deletingValues.deleting = false;
					}

					//Prompts them to delete the account
					else
					{
						if ((deletingValues.deleting) && (username == deletingValues.username))
						{
							returnLabel.Text = "Account " + deletingValues.username + " was deleted";
							deletingValues.deleting = false;
							using (var deleteUser = new defaultDataSetTableAdapters.usersTableAdapter())
							{
								deleteUser.deleteUser(username);
							}
							usersDisplayTable.DataBind();
							System.Threading.Thread.Sleep(750);
							returnLabel.Text = "";
						}
						else
						{
							returnLabel.Text = "Click again to delete - note that this cannot be undone!";
							deletingValues.deleting = true;
							deletingValues.username = username;
						}
					}
					break;

				case "changeUserPassword":
					returnLabel.Text = "";
					deletingValues.deleting = false;
					if (passwordBox.Text != "")
					{
						using (var changePassword = new defaultDataSetTableAdapters.usersTableAdapter())
						{
							changePassword.changePassword(passwordBox.Text, username);
						}
						returnLabel.Text = "User " + username + "'s password was changed to " + passwordBox.Text + ".";
					}
					else
					{
						returnLabel.Text = "<-- You must enter something in this box!";
						System.Threading.Thread.Sleep(750);
						returnLabel.Text = "";
					}
					break;
			}
		}

		protected void newUser_Click(object sender, EventArgs e)
		{
			if (submittedUsernameBox.Text != "" && submittedPasswordBox.Text != "" && submittedAccessLevelBox.Text != "")
			{
				string submittedUsername = submittedUsernameBox.Text;
				string submittedPassword = submittedPasswordBox.Text;
				bool allNumeric = true;
				foreach (char c in submittedAccessLevelBox.Text)
				{
					if (Char.IsNumber(c) == false)
					{
						allNumeric = false;
						break;
					}
				}
				if (allNumeric == false)
				{
					registerReturn.Text = "The clearance level must be a numeric value";
					System.Threading.Thread.Sleep(750);
				}
				else
				{
					int submittedAccessLevel = Convert.ToInt32(submittedAccessLevelBox.Text);
					using (var searchUsername = new defaultDataSetTableAdapters.usersTableAdapter())
					{
						if (searchUsername.checkUsername(submittedUsername) == null)
						{
							using (var addUser = new defaultDataSetTableAdapters.usersTableAdapter())
							{
								addUser.newUser(submittedUsername, submittedPassword, submittedAccessLevel);
								registerReturn.Text = "New user created";
								usersDisplayTable.DataBind();
								System.Threading.Thread.Sleep(2000);
							}
						}
						else
						{
							registerReturn.Text = "That username is already taken!";
							System.Threading.Thread.Sleep(750);
						}
					}
				}
			}
		}
	}

	static class deletingValues
	{
		public static string username;
		public static bool deleting;
	}
}