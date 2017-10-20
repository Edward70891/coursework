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
			
		}

		protected void submitCredentialsButton_Click(object sender, EventArgs e)
		{
			switch (attemptLogin(usernameBox.Text, passwordBox.Text, new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=W:\Repos\mainCoursework\main.accdb")))
			{
				case 0:
					returnLabel.Text = "Correct!";
					//Sessions code goes here
					break;
				case 1:
					returnLabel.Text = "Incorrect Username";
					break;
				case 2:
					returnLabel.Text = "Incorrect Password";
					break;
				case 3:
					returnLabel.Text = "There a problem with the login checking, this shouldn't happen";
					break;
			}
		}

        //Attempting login
        public int attemptLogin(string submittedUsername, string submittedPassword, OleDbConnection connection)
        {
            connection.Open();
            var acceptable = new List<loginCredentials>();
            using (OleDbCommand getUserCredentials = new OleDbCommand(@"SELECT * FROM users", connection))
            {
                using (var reader = getUserCredentials.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        acceptable.Add(new loginCredentials { username = reader.GetString(0), password = reader.GetString(1), accessLevel = reader.GetInt32(2) });
                    }
                }
            }
            foreach (loginCredentials check in acceptable)
            {
                if (submittedUsername == check.username)
                {
                    if (submittedPassword == check.password)
                    {
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 1;
                }
            }
            return 3;
        }
        //Custom login credentials class for checking en masse
        public class loginCredentials
        {
            public string username;
            public string password;
            public int accessLevel;
        }
    }
}