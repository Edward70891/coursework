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
		defaultDataSetTableAdapters.employeesTableAdapter employeeQueryTable = new defaultDataSetTableAdapters.employeesTableAdapter();
		defaultDataSetTableAdapters.customersTableAdapter customerQueryTable = new defaultDataSetTableAdapters.customersTableAdapter();

		protected enum userType { employee,customer }

		//Custom class for storing deletion data outside the button_click event to facilitate clicking twice to delete
		static class deletingUsersPersistent
		{
			public static string username;
			public static bool deleting;
			public static userType type;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//Checks if they're an admin and if they're not, hides the password changing and account deleting elements
			if (Convert.ToString(Session["userIsAdmin"]) == "False")
			{
				configControls.Visible = false;
				employeesDisplayTable.Columns[4].Visible = false;
				customersDisplayTable.Columns[3].Visible = false;
			}
			else
			{
				configControls.Visible = true;
				employeesDisplayTable.Columns[4].Visible = true;
				customersDisplayTable.Columns[3].Visible = true;
			}
		}

		private string username;

		//Deleting and change employee account passwords
		protected void employeesDisplayTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			username = employeesDisplayTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
			switch (e.CommandName)
			{
				//If they're trying to delete something
				case "deleteUser":
					//Checks if they're trying to delete the master account
					if (username == "master" || username == "Market")
					{
						returnLabel.Text = "You can't delete this account!";
						deletingUsersPersistent.deleting = false;
						deletingUsersPersistent.type = userType.customer;
						break;
					}
					deleteUser(username, userType.employee);
					break;
				//Runs if the user is trying to change a password
				case "changeUserPassword":
					changeUserPassword(username, userType.employee);
					break;
			}
		}

		//Deleting and change employee account passwords
		protected void customersDisplayTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			username = customersDisplayTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
			switch (e.CommandName)
			{
				//If they're trying to delete something
				case "deleteUser":
					deleteUser(username, userType.customer);
					break;
				//Runs if the user is trying to change a password
				case "changeUserPassword":
					changeUserPassword(username, userType.customer);
					break;
			}
		}

		/// <summary>
		/// Delete a user. Please make sure the first letter of the type is capitalised.
		/// </summary>
		/// <param name="username">The username of the person to be deleted</param>
		/// <param name="type">"Customer" or "Employee"</param>
		protected void deleteUser(string username, userType type)
		{
			//Make a reference to the correct gridview according to type
			GridView displayTable;
			switch (type)
			{
				case userType.customer:
					displayTable = customersDisplayTable;
					break;
				case userType.employee:
					displayTable = employeesDisplayTable;
					break;
				default:
					throw new Exception();
			}

			if ((deletingUsersPersistent.deleting) && (username == deletingUsersPersistent.username) && deletingUsersPersistent.type == type)
			{
				//If this is the second click on the same entry, post that the user was deleted
				returnLabel.Text = type + " account " + deletingUsersPersistent.username + " was deleted";
				deletingUsersPersistent.deleting = false;
				//Delete the user
				if (type == userType.employee)
				{
					employeeQueryTable.deleteUser(username);
				}
				else
				{
					customerQueryTable.deleteUser(username);
				}
				//Log the deletion, refresh the GridView and wait, then clear the post box
				customLogging.newEntry(type + " " + username + " was deleted");
				displayTable.DataBind();
				System.Threading.Thread.Sleep(750);
				returnLabel.Text = "";
			}
			else
			{
				//Runs on first click, warns and sets up second click using external class
				returnLabel.Text = "Click again to delete - note that this cannot be undone!";
				deletingUsersPersistent.deleting = true;
				deletingUsersPersistent.username = username;
				deletingUsersPersistent.type = type;
			}
		}

		protected void changeUserPassword(string username, userType type)
		{
			//Make a reference to the correct gridview for commands
			GridView displayTable;
			switch (type)
			{
				case userType.customer:
					displayTable = customersDisplayTable;
					break;
				case userType.employee:
					displayTable = employeesDisplayTable;
					break;
				default:
					throw new Exception();
			}

			returnLabel.Text = "";
			//Disables second click coding
			deletingUsersPersistent.deleting = false;

			if (passwordBox.Text == "" || confirmPassword.Text == "")
			{
				returnLabel.Text = "You must fill both boxes!";
				return;
			}

			if (passwordBox.Text != confirmPassword.Text)
			{
				returnLabel.Text = "The passwords do not match!";
				return;
			}

			if (type == userType.employee)
			{
				employeeQueryTable.changePassword(customSecurity.generateMD5(passwordBox.Text), username);
			}
			else
			{
				customerQueryTable.changePassword(customSecurity.generateMD5(passwordBox.Text), username);
			}
			customLogging.newEntry(type + " " + username + "'s password changed");
			returnLabel.Text = type + " " + username + "'s password was changed to '" + passwordBox.Text + "'.";
		}

		protected void registerRedirect_Click(object sender, EventArgs e)
		{
			Server.Transfer("~/registerEmployee.aspx", false);
		}

		protected void newCustomerButton_Click(object sender, EventArgs e)
		{
			Server.Transfer("~/registerCustomer.aspx", false);
		}
	}
}