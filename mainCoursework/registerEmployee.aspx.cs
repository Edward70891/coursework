using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using commonClasses;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class registerEmployee : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//Checks if they're an admin and if they're not, hides the admin related checkboxes and ui elements (otherwise shows them)
			if (Convert.ToString(Session["userIsAdmin"]) != "false")
			{
				adminCheckBox.Visible = false;
			}
			else
			{
				adminCheckBox.Visible = true;
			}
		}

		protected void newUser_Click(object sender, EventArgs e)
		{
			//Checks the password boxes are the same
			if (submittedPasswordBox.Text == submittedConfirmPasswordBox.Text)
			{
				//Checks there's no SQL related things in the boxes
				if (customSecurity.sanitizeCheck(new string[] { submittedUsernameBox.Text, submittedPasswordBox.Text, forenameBox.Text, surnameBox.Text }))
				{
					using (defaultDataSetTableAdapters.employeesTableAdapter employeeQueryTable = new defaultDataSetTableAdapters.employeesTableAdapter())
					{
						try
						{
							//Creates the new user, logs the creation of the user and notifies the current user
							employeeQueryTable.newEmployee(submittedUsernameBox.Text, customSecurity.generateMD5(submittedPasswordBox.Text), adminCheckBox.Checked, forenameBox.Text, surnameBox.Text);
							registerReturn.Text = "New user created";
							customLogging.newEntry("User " + submittedUsernameBox.Text + " was created");
						}
						//Catches errors (hopefully only database related) and posts them to the user
						catch (Exception except)
						{
							registerReturn.Text = "Database operation failed with error " + except.Message;
						}
					}
				}
				else
				{
					registerReturn.Text = customSecurity.sanitizeErrorMessage;
				}
			}
			else
			{
				//Notifies the user if the password fields don't match
				registerReturn.Text = "The passwords don't match!";
				submittedConfirmPasswordBox.Text = "";
			}
		}
	}
}