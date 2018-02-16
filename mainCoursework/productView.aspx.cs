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
			Session["productRedirectName"] = "";
			productPanel currentPanel = new productPanel(currentProduct.productInfo);
			Panel toAdd = currentPanel.generatedControl;
			toAdd.CssClass = "fullProductPanel";
			product.Controls.Add(toAdd);
		}

		protected void cartButton_Click(object sender, EventArgs e)
		{
			if (Convert.ToString(Session["userType"]) != "user")
			{
				returnLabel.Text = "You must log in to a customer account to have a cart!";
			}
			else
			{
				adapter.insertProduct(Convert.ToString(Session["currentUser"]), currentProduct.productInfo.productName, Convert.ToInt32(amountToAdd.Text));
				returnLabel.Text = amountToAdd.Text + " " + currentProduct.productInfo.productName + "s added to your cart!";
			}
		}
	}
}