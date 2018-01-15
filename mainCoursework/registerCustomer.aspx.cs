using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
					try
					{
						customersQueryTable.newCustomer(usernameBox.Text, passwordBox.Text, address1Box.Text, address2Box.Text, cityBox.Text, countryDropdown.SelectedValue, postcodeBox.Text, phoneNumberBox.Text, forenameBox.Text, surnameBox.Text);
					}
					catch
					{
						//Catch database exceptions here
					}
				}
				else
				{
					phoneNumReturn.Text = "The phone number must be digits only!";
				}
			}
			else
			{
				confirmPasswordBox.Text = "";
				passwordBoxReturn.Text = "The passwords don't match!";
			}
		}

		protected void confirmPasswordBox_TextChanged(object sender, EventArgs e)
		{
			passwordsMatchCheck();
		}

		protected void passwordBox_TextChanged(object sender, EventArgs e)
		{
			passwordsMatchCheck();
		}

		private void passwordsMatchCheck()
		{
			if (passwordBox.Text != confirmPasswordBox.Text)
			{
				confirmPasswordBox.Text = "";
				passwordBoxReturn.Text = "The passwords don't match!";
			}
		}
	}
}