using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.OleDb;
using System.Threading;
using commonClasses;
using System.Web.UI.WebControls;

namespace mainCoursework
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitCredentialsButton_Click(object sender, EventArgs e)
        {
			String attemptedName = usernameBox.Text;
			switch (attemptLogin(attemptedName, passwordBox.Text, new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = |Datadirectory|main.accdb; Persist Security Info = True")))
            {
                case 0:
                    returnLabel.Text = "Correct!";
                    Session["loggedState"] = 1;
                    Thread.Sleep(1000);
					customLogging.newEntry(attemptedName + "logged in", "user");
                    Server.Transfer("overview.aspx", true);
                    break;
                case 1:
                    returnLabel.Text = "The Username or Password is incorrect.";
					customLogging.newEntry("Someone attempted to login with username '" + attemptedName + "' but the credentials were incorrect", "user");
					break;
            }
        }


        //Attempting login
        public int attemptLogin(string submittedUsername, string submittedPassword, OleDbConnection connection)
        {
            connection.Open();
            using (OleDbCommand getUserCredentials = new OleDbCommand(@"SELECT * FROM users WHERE username=@username AND password=@password", connection))
            {
                getUserCredentials.Parameters.AddWithValue("@username", submittedUsername);
                getUserCredentials.Parameters.AddWithValue("@password", submittedPassword);
                int check = Convert.ToInt32(getUserCredentials.ExecuteScalar());
                if (check > 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}