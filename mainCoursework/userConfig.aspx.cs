using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using commonClasses;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class configuration : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//Checks if the user is an employee, and if not redirects them to the default page
			if (Convert.ToString(Session["userType"]) != "employee")
			{
				Server.Transfer("~/default.aspx", false);
			}
			//Checks if they're an admin and if they're not, hides the admin related checkboxes and ui elements (otherwise shows them)
			if (Convert.ToString(Session["userIsAdmin"]) != "false")
			{
				passwordBox.Visible = false;
				confirmPassword.Visible = false;
			}
			else
			{
				passwordBox.Visible = true;
				confirmPassword.Visible = true;
			}
		}

		private string username;
		private defaultDataSetTableAdapters.employeesTableAdapter employeeQueryTable = new defaultDataSetTableAdapters.employeesTableAdapter();

		//Deleting and change account passwords
		protected void usersDisplayTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			username = usersDisplayTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
			switch (e.CommandName)
			{
				//If they're trying to delete something
				case "deleteUser":
					//Checks if they're trying to delete the admin account
					if (username == "default")
					{
						returnLabel.Text = "You can't delete the default account!";
						deletingUsersPersistent.deleting = false;
					}

					//Prompts them to delete the account
					else
					{
						if ((deletingUsersPersistent.deleting) && (username == deletingUsersPersistent.username))
						{
							//If this is the second click on the same entry, post that the user was deleted
							returnLabel.Text = "Account " + deletingUsersPersistent.username + " was deleted";
							deletingUsersPersistent.deleting = false;
							//Delete the user
							employeeQueryTable.deleteEmployee(username);
							//Log the deletion, refresh the table and wait, then clear the post box
							customLogging.newEntry("User " + username + " was deleted");
							usersDisplayTable.DataBind();
							System.Threading.Thread.Sleep(750);
							returnLabel.Text = "";
						}
						else
						{
							//Runs on first click, warns and sets up second click using external class
							returnLabel.Text = "Click again to delete - note that this cannot be undone!";
							deletingUsersPersistent.deleting = true;
							deletingUsersPersistent.username = username;
						}
					}
					//Ends the function
					break;


				//Runs if the user is trying to change a password
				case "changeUserPassword":
					returnLabel.Text = "";
					//Disables second click coding
					deletingUsersPersistent.deleting = false;
					//Checks there is a value entered in the box
					if (SQLSanitization.sanitizeCheck(new string[] { passwordBox.Text }))
					{
						if (passwordBox.Text != "" || confirmPassword.Text != "")
						{
							if (passwordBox.Text == confirmPassword.Text)
							{
								//Changes the password in the DB
								employeeQueryTable.changePassword(passwordBox.Text, username);
								//Posts that the password has been changed and logs it
								customLogging.newEntry("User " + username + "'s password changed");
								returnLabel.Text = "User " + username + "'s password was changed to '" + passwordBox.Text + "'.";
							}
							else
							{
								//Runs if the passwords don't match, notifies the user
								returnLabel.Text = "The passwords do not match!";
								System.Threading.Thread.Sleep(750);
								returnLabel.Text = "";
							}
						}
						else
						{
							//Prompts the user to fill the box
							returnLabel.Text = "You must fill both boxes!";
							System.Threading.Thread.Sleep(750);
							returnLabel.Text = "";
						}
					}
					else
					{
						returnLabel.Text = SQLSanitization.sanitizeErrorMessage;
					}
					break;
			}
		}


		//Custom class for storing deletion data outside the button_click event to facilitate clicking twice to delete
		static class deletingUsersPersistent
		{
			public static string username;
			public static bool deleting;
		}
	}
}