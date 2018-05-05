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
		defaultDataSetTableAdapters.cartsTableAdapter cartsAdaptor = new defaultDataSetTableAdapters.cartsTableAdapter();
		protected void Page_Load(object sender, EventArgs e)
		{
			//If there's no product instruction to load, redirect the user back to the product list
			if (Convert.ToString(Session["productRedirectName"]) == null || Convert.ToString(Session["productRedirectName"]) == "")
			{
				Server.Transfer("~/products.aspx", false);
			}
			//Instantiate a new product and a new panel, make them, change the css class then add them
			currentProduct = new product(Convert.ToString(Session["productRedirectName"]));
			productPanel currentPanel = new productPanel(currentProduct);
			currentPanel.CssClass = "fullProductPanel";
			product.Controls.Add(currentPanel);
		}

		protected void cartButton_Click(object sender, EventArgs e)
		{
			//Makes sure it's a logged in customer trying to add the item and denies them if it isn't
			if (Convert.ToString(Session["userType"]) != "customer")
			{
				returnLabel.Text = "You must log in to a customer account to have a cart!";
				return;
			}
			//Make sure they're not trying to add 0 of something to their cart
			if (amountToAdd.Text == "0")
			{
				returnLabel.Text = "You can't add 0 of something to your cart!";
				return;
			}

			//If isn't already an entry for that product in that cart, add it
			if (cartsAdaptor.checkExisting(Convert.ToString(Session["currentUser"]), currentProduct.productName) == null)
			{
				cartsAdaptor.insertProduct(Convert.ToString(Session["currentUser"]), currentProduct.productName, Convert.ToInt32(amountToAdd.Text));
			}
			//If there is, add the amount they want onto it
			else
			{
				cartsAdaptor.updateAmount(Convert.ToInt32(amountToAdd.Text), Convert.ToString(Session["currentUser"]), currentProduct.productName);
			}
			returnLabel.Text = amountToAdd.Text + " " + currentProduct.productName + "s added to your cart!";
		}
	}
}