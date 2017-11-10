using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			try
			{
				var testConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|Datadirectory|main.accdb;Persist Security Info=True");
				connectionTestLabel.Text = "DB Connection OK";
			}
			catch (System.ArgumentException)
			{
				connectionTestLabel.Text = "DB Connection bad!";
			}
		}
    }
}