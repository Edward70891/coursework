using System;
using System.Collections.Generic;
using System.Data.OleDb;
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

                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Edward\Source\Repos\coursework\mainCoursework\App_Data\main.accdb; Persist Security Info = True");
                connection.Open();
                using (OleDbCommand addProduct = new OleDbCommand(@"insert into products(productName, price, displayName)values(@submittedProductName, @submittedPrice, @submittedDisplayName)", connection))
                {
                    addProduct.Parameters.AddWithValue("@submittedProductName", productName);
                    addProduct.Parameters.AddWithValue("@submittedPrice"); //Needs a price box
                    addProduct.Parameters.AddWithValue("@submittedDisplayName", displayName);

                }
            }
        }
    }
}