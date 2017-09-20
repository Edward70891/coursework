using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string names;
            OleDbConnection testShop = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=W:\Repos\coursework\Tests\test\Shop.accdb");
            testShop.Open();
            string query1 = "SELECT First_Name, Second_Name FROM tblCustomer";
            OleDbCommand command = new OleDbCommand(query1, testShop);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                names = (string)reader["First_Name"] + " " + (string)reader["Second_Name"];
            }
            displayBox.Text = names;
        }
}