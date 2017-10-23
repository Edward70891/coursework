using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
    public partial class overview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var checkLogin = Convert.ToInt32(Session["loggedState"]);
            if (checkLogin == 0)
            {
                Server.Transfer("login.aspx", true);
            }
        }
    }
}