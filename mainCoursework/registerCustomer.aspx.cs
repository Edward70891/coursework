using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using commonClasses;

namespace mainCoursework
{
	public partial class registerCustomer : System.Web.UI.Page
	{
		private defaultDataSetTableAdapters.customersTableAdapter customersQueryTable = new defaultDataSetTableAdapters.customersTableAdapter();
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void registerButton_Click(object sender, EventArgs e)
		{
			if (passwordBox.Text == confirmPasswordBox.Text)
			{
				int dump;
				if (int.TryParse(phoneNumberBox.Text, out dump))
				{
					if (customSecurity.sanitizeCheck(new string[] { usernameBox.Text, passwordBox.Text, address1Box.Text, address2Box.Text, cityBox.Text, countryDropdown.SelectedValue, postcodeBox.Text, phoneNumberBox.Text, forenameBox.Text, surnameBox.Text }))
					{
						try
						{
							customersQueryTable.newCustomer(usernameBox.Text, customSecurity.generateMD5(passwordBox.Text), address1Box.Text, address2Box.Text, cityBox.Text, countryDropdown.SelectedValue, postcodeBox.Text, phoneNumberBox.Text, forenameBox.Text, surnameBox.Text);
							customLogging.newEntry("Someone registered the user " + usernameBox.Text);
							returnLabel.Text = "User " + usernameBox.Text + " created";
						}
						catch (Exception except)
						{
							returnLabel.Text = "Registration failed, reason: " + except.Message;
						}
					}
					else
					{
						returnLabel.Text = customSecurity.sanitizeErrorMessage;
					}
				}
				else
				{
					phoneNumReturn.Text = "The phone number must be digits only!";
				}
			}
			//else
			//{
			//	confirmPasswordBox.Text = "";
			//	passwordBoxReturn.Text = "The passwords don't match!";
			//}
		}

		//The events that catch the text of either password box changing
		protected void confirmPasswordBox_TextChanged(object sender, EventArgs e)
		{
			passwordsMatchCheck();
		}

		protected void passwordBox_TextChanged(object sender, EventArgs e)
		{
			passwordsMatchCheck();
		}

		//Notifies the user that the boxes aren't the same
		private void passwordsMatchCheck()
		{
			if (passwordBox.Text != confirmPasswordBox.Text)
			{
				passwordBoxReturn.Text = "The passwords don't match!";
			}
		}
	}
}