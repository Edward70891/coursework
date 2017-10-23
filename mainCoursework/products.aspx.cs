using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
    public partial class products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var checkLogin = Convert.ToInt32(Session["loggedState"]);
            if (checkLogin == 0)
            {
                Server.Transfer("login.aspx", true);
            }
        }

        protected void productAddButton_Click(object sender, EventArgs e)
        {
            while (true)
            {
                string displayName = productNameBox.Text;
                if (displayName.All(Char.IsLetterOrDigit) == false)
                {
                    productNameBox.Text = "";
                    returnMessage.Text = "Please only use numbers and letters in the product name";
                    break;
                }
                string productName = displayName;
                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                productName = cultInfo.ToTitleCase(productName);
                productName = productName.Replace(" ", "");

                //Database adding code goes here
            }
        }
    }
}