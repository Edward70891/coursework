using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.OleDb;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			OleDbConnection database = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=W:\Repos\mainCoursework\main.accdb");
		}

		protected void submitCredentialsButton_Click(object sender, EventArgs e)
		{
			attemptLogin(usernameBox.Text, passwordBox.Text);
		}

		public void attemptLogin(string username, string password)
		{

		}
	}
}