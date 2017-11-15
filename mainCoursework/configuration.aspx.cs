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

		}

		string username;

		//Deleting and change account passwords
		protected void usersDisplayTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			username = usersDisplayTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
			switch (e.CommandName)
			{
				//If they're trying to delete something
				case "deleteUser":
					//Checks if they're trying to delete the admin account
					if (username == "admin")
					{
						returnLabel.Text = "You can't delete the admin account!";
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
							using (var deleteUser = new defaultDataSetTableAdapters.usersTableAdapter())
							{
								deleteUser.deleteUser(username);
							}
							//Log the deletetion, refresh the table and wait, then clear the post box
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
					if (passwordBox.Text != "" || confirmPassword.Text != "")
					{
						if (passwordBox.Text == confirmPassword.Text)
						{
							using (var changePassword = new defaultDataSetTableAdapters.usersTableAdapter())
							{
								//Changes the password in the DB
								changePassword.changePassword(passwordBox.Text, username);
							}
							//Posts that the password has been changed and logs it
							customLogging.newEntry("User " + username + "'s password changed");
							returnLabel.Text = "User " + username + "'s password was changed to " + passwordBox.Text + ".";
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
					break;
			}
		}

		protected void newUser_Click(object sender, EventArgs e)
		{
			//Checks all boxes have a value
			if (submittedUsernameBox.Text != "" && submittedPasswordBox.Text != "" && submittedAccessLevelBox.Text != "")
			{
				//Pulls values from boxes
				string submittedUsername = submittedUsernameBox.Text;
				string submittedPassword = submittedPasswordBox.Text;
				string passwordConfirmation = submittedConfirmPasswordBox.Text;
				bool allNumeric = true;
				//Checks if the Access Level box only has numbers in it
				foreach (char c in submittedAccessLevelBox.Text)
				{
					if (Char.IsNumber(c) == false)
					{
						allNumeric = false;
						break;
					}
				}
				//Stops and posts if the box contains things other than numbers
				if (allNumeric == false)
				{
					registerReturn.Text = "The clearance level must be a numeric value!";
					System.Threading.Thread.Sleep(750);
				}
				else
				{
					if (submittedPassword == passwordConfirmation)
					{
						int submittedAccessLevel = Convert.ToInt32(submittedAccessLevelBox.Text);
						using (var searchUsername = new defaultDataSetTableAdapters.usersTableAdapter())
						{
							//Checks if there's any existing users with the given username
							if (searchUsername.checkUsername(submittedUsername) == null)
							{
								using (var addUser = new defaultDataSetTableAdapters.usersTableAdapter())
								{
									//Adds the user to the database, posts and refreshes the table then logs the addition
									addUser.newUser(submittedUsername, submittedPassword, submittedAccessLevel, HttpContext.Current.User.Identity.Name);
									registerReturn.Text = "New user created";
									usersDisplayTable.DataBind();
									customLogging.newEntry("Account " + username + " created");
									//Waits then clears the postbox
									System.Threading.Thread.Sleep(2000);
									registerReturn.Text = "";
								}
							}
							else
							{
								//Posts that the given username is not unique
								registerReturn.Text = "That username is already taken!";
								System.Threading.Thread.Sleep(750);
								registerReturn.Text = "";
							}
						}
					}
					else
					{
						//Posts that the given passwords don't match
						registerReturn.Text = "The passwords don't match!";
						System.Threading.Thread.Sleep(750);
						registerReturn.Text = "";
					}
				}
			}
		}
	}

	//Custom class for managing deletions outside of the button click event
	static class deletingUsersPersistent
	{
		public static string username;
		public static bool deleting;
	}
}