using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.UI;
using commonClasses;
using System.Web.UI.WebControls;

namespace mainCoursework
{
    public partial class products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

		protected void productsTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			string displayName = productsTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
			switch (e.CommandName)
			{
				//Runs if the delete button is pressed
				case "deleteProduct":
					//Checks if this is the second button press
					if ((deletingProductsPersistent.deleting) && (displayName == deletingProductsPersistent.product))
					{
						//Returns that the product has been deleted
						returnLabel.Text = "Product " + displayName + " was deleted";
						using (var deleteProduct = new defaultDataSetTableAdapters.productsTableAdapter())
						{
							//Deletes the product, logs the action, posts the result to the box then clears it
							deleteProduct.deleteProduct(displayName);
							returnLabel.Text = "Product deleted";
							customLogging.newEntry("A user deleted the product " + displayName, "user");
							System.Threading.Thread.Sleep(2000);
							returnLabel.Text = "";
						}

					}
					else
					{
						//Runs on first click, warns and sets up second click using external class
						returnLabel.Text = "Click again to delete - note that this cannot be undone!";
						deletingProductsPersistent.deleting = true;
						deletingProductsPersistent.product = displayName;
					}
					break;
			}
		}

		//Adding products
        protected void productAddButton_Click(object sender, EventArgs e)
        {
			//A while loop that is broken at the end or prematurely if conditions are not met
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
				using (var checkProductName = new defaultDataSetTableAdapters.productsTableAdapter())
				{
					if (checkProductName.checkProductName(productName) != null)
					{
						returnMessage.Text = "There is already a product with that name!";
						System.Threading.Thread.Sleep(750);
					}
				}

				//Returns the the product has been created then creates it and logs it and refreshes the table
				returnMessage.Text = "Product created named " + productName + ", priced at £" + Convert.ToString(price) + " and displayed as " + displayName;
				using (var addProductAdapter = new defaultDataSetTableAdapters.productsTableAdapter())
				{
					addProductAdapter.addProduct(productName, price, displayName, typeDropdown.SelectedValue);
				}
				customLogging.newEntry("A user created the product " + displayName, "user");
				productsTable.DataBind();
				break;
            }
        }
    }

	static class deletingProductsPersistent
	{
		public static string product;
		public static bool deleting;
	}
}