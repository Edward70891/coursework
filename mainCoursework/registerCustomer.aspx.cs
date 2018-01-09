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
				try
				{
					customersQueryTable.newCustomer(usernameBox.Text, passwordBox.Text, address1Box.Text, address2Box.Text, cityBox.Text, countryDropdown.SelectedValue, postcodeBox.Text, phoneNumberBox.Text, forenameBox.Text, surnameBox.Text);
				}
				catch
				{
					//Catch database exceptions and problems with phone number here
				}
			}
		}

		protected void confirmPasswordBox_TextChanged(object sender, EventArgs e)
		{
			passwordsMatch();
		}

		protected void passwordBox_TextChanged(object sender, EventArgs e)
		{
			passwordsMatch();
		}

		//Fix me later!
		private void passwordsMatch()
		{
			//if (passwordBox.Text != confirmPasswordBox.Text)
			//{
			//	passwordBoxReturn.Text = "The Passwords do not Match!";
			//}
			//else
			//{
			//	passwordBoxReturn.Text = "";
			//}
		}
	}
}