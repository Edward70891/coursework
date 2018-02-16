using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class productView : System.Web.UI.Page
	{
		//Create a new product
		product currentProduct;
		defaultDataSetTableAdapters.cartsTableAdapter adapter = new defaultDataSetTableAdapters.cartsTableAdapter();

		protected void Page_Load(object sender, EventArgs e)
		{
			//If there's no product instruction to load, redirect the user back to the product list
			if (Convert.ToString(Session["productRedirectName"]) == null || Convert.ToString(Session["productRedirectName"]) == "")
			{
				Server.Transfer("~/products.aspx", false);
			}
			//Instantiate a new product and a new panel, make them, change the css class then add them
			currentProduct = new product(Convert.ToString(Session["productRedirectName"]));
			productPanel currentPanel = new productPanel(currentProduct.productInfo);
			Panel toAdd = currentPanel;
			toAdd.CssClass = "fullProductPanel";
			product.Controls.Add(toAdd);
		}

		protected void cartButton_Click(object sender, EventArgs e)
		{
			//Makes sure it's a logged in customer trying to add the item and denies them if it isn't
			if (Convert.ToString(Session["userType"]) != "user")
			{
				returnLabel.Text = "You must log in to a customer account to have a cart!";
			}
			else
			{
				//Make sure they're not trying to add 0 of something to their cart
				if (amountToAdd.Text != "0")
				{
					adapter.insertProduct(Convert.ToString(Session["currentUser"]), currentProduct.productInfo.productName, Convert.ToInt32(amountToAdd.Text));
					returnLabel.Text = amountToAdd.Text + " " + currentProduct.productInfo.productName + "s added to your cart!";
				}
				else
				{
					returnLabel.Text = "You can't add 0 of something to your cart!";
				}
			}
		}
	}
}