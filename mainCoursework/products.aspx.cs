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
            if (Convert.ToBoolean(Session["loggedState"]) != true)
            {
                Server.Transfer("login.aspx", true);
            }
        }

        protected void productAddButton_Click(object sender, EventArgs e)
        {
            while (true)
            {
                //Check and format the name for both display and storage/reference purposes
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
				productName = char.ToLower(productName[0]) + productName.Substring(1);

				//Check and format the price to ensure 2dp accuracy and only digits content
				decimal price;
                if (decimal.TryParse(productPrice.Text, out price) == false)
                {
                    productPrice.Text = "";
                    returnMessage.Text = "Please input prices in the format X.XX";
                    break;
                }
                decimal priceCheck = price * 100;
                if (priceCheck != Math.Floor(priceCheck))
                {
					productPrice.Text = "";
					returnMessage.Text = "Please input prices in the format X.XX";
					break;
				}

				returnMessage.Text = "Product created named " + productName + ", priced at £" + Convert.ToString(price) + " and displayed as " + displayName;

				//Input formatted values into DB
				//OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Edward\Source\Repos\coursework\mainCoursework\App_Data\main.accdb; Persist Security Info = True");
				//connection.Open();
				//using (OleDbCommand addProduct = new OleDbCommand(@"insert into products(productName, price, displayName)values(@submittedProductName, @submittedPrice, @submittedDisplayName)", connection))
				//{
				//	addProduct.Parameters.AddWithValue("@submittedProductName", productName);
				//	addProduct.Parameters.AddWithValue("@submittedPrice", price);
				//	addProduct.Parameters.AddWithValue("@submittedDisplayName", displayName);
				//}

				using (var addProductAdapter = new defaultDataSetTableAdapters.productsTableAdapter())
				{
					addProductAdapter.addProduct(productName, price, displayName, typeDropdown.SelectedValue);
				}

					break;
            }
        }
    }
}