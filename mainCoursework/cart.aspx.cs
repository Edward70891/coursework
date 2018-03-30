﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class cart : System.Web.UI.Page
	{
		/// <summary>
		/// A custom cart item, essentially for making into arrays so we can keep track of the amount of products without making any changes to the logic of the original product class
		/// </summary>
		struct cartItem
		{
			public product product;
			public int amount;
		}

		//Initialize an adaptor to the carts table and an array to hold all the items in the current user's cart
		defaultDataSetTableAdapters.cartsTableAdapter cartsTableAdapter = new defaultDataSetTableAdapters.cartsTableAdapter();
		cartItem[] cartArray;

		protected void Page_Load(object sender, EventArgs e)
		{
			//Generate the list - if it's empty, tell the user their cart's empty and hide the Checkout button
			populateList();
			if (cartArray.Length == 0)
			{
				productsListPanel.Controls.Add
					(
						new Label()
						{
							Text = "Your cart is empty!",
							CssClass = "cartEmptyMessage"
						}
					);
				purchaseButton.Visible = false;
			}
			else
			{
				populatePage();
				purchaseButton.Visible = true;
			}
		}

		// Fill the array with the results of the query by username on the carts table, letting the products generate themselves
		private void populateList()
		{
			//Create a temporary data table to pull information from
			using (DataTable temp = cartsTableAdapter.getCart(Convert.ToString(Session["currentUser"])))
			{
				//Initialize a cart list of the correct length
				cartArray = new cartItem[temp.Rows.Count];
				//Run a pseudo-for loop to initialize all the data, entry by entry
				int i = 0;
				foreach (DataRow row in temp.Rows)
				{
					cartArray[i].product = new product(Convert.ToString(row[2]));
					cartArray[i].amount = Convert.ToInt32(row[3]);
					i++;
				}
			}
		}

		//Generate all the needed productPanels and modify them as needed
		private void populatePage()
		{
			foreach(cartItem current in cartArray)
			{
				productPanel panel = new productPanel(current.product.productInfo);
				//Remove stockTag and descriptionTag; maybe this works?
				panel.Controls.Remove(panel.Controls[3]);
				panel.Controls.Remove(panel.Controls[3]); //Removing 3 again because the descriptionTag has fallen into the newly opened space

				//Create a text box (user modifiable) to hold the amount of the product in the cart - if the amount is changed call an event instead of having a button
				TextBox amountBox = new TextBox()
				{
					CssClass = "amountBox",
					ID = current.product.productInfo.productName + "_CartAmountBox",
					Text = Convert.ToString(current.amount),
					TextMode = TextBoxMode.Number,
					AutoPostBack = true
				};
				amountBox.TextChanged += new EventHandler(amountBox_TextChanged);
				panel.Controls.Add(amountBox);

				//Create a button that will remove all of the relevant product from the cart when clicked
				Button removeButton = new Button()
				{
					CssClass = "removeButton",
					ID = current.product.productInfo.productName + "_CartRemoveButton",
					Text = "Remove",
					CommandArgument = current.product.productInfo.productName
				};
				removeButton.Click += new EventHandler(removeButton_Click);
				removeButton.Attributes.Add("runat", "server");
				panel.Controls.Add(removeButton);
				
				//Button updateButton = new Button()
				//{
				//	CssClass = "updateButton",
				//	ID = current.product.productInfo.productName + "CartUpdateButton",
				//	Text = "Update Amount",
				//	CommandArgument = current.product.productInfo.productName
				//};
				//updateButton.Click += new EventHandler(updateButton_Click);
				//updateButton.Attributes.Add("runat", "server");
				//panel.Controls.Add(updateButton);

				//Add the filled panel to the page
				panel.CssClass = "cartProductDisplayPanel";
				productsListPanel.Controls.Add(panel);
			}
		}

		protected void removeButton_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			string productName = btn.CommandArgument;
			//Find the index of that product in the cart list
			int i = 0;
			int index = 0;
			foreach (cartItem current in cartArray)
			{
				if (current.product.productInfo.productName == productName)
				{
					index = i;
				}
				i++;
			}
			var temp = new List<cartItem>(cartArray);
			temp.RemoveAt(index);
			cartArray = temp.ToArray();
			populatePage();
		}

		protected void amountBox_TextChanged(object sender, EventArgs e)
		{
			//Get the product name from the textbox that sent it
			TextBox box = (TextBox)sender;
			string productName = box.ID.Split('_')[0];
			//Find the index of that product in the cart list
			int i = 0;
			int index = 0;
			foreach(cartItem current in cartArray)
			{
				if (current.product.productInfo.productName == productName)
				{
					index = i;
				}
				i++;
			}
			//Change the amount value
			cartArray[index].amount = Convert.ToInt32(box.Text);
		}

		protected void purchaseButton_Click(object sender, EventArgs e)
		{
			makeOrder();
		}

		//Dummy checkout method
		protected void makeOrder()
		{
			if (Convert.ToString(Session["userType"]) != "customer")
			{
				returnLabel.Text = "You're not a registered customer so you can't make an order!";
				return;
			}
			var ordersAdaptor = new defaultDataSetTableAdapters.ordersTableAdapter();
			foreach (cartItem current in cartArray)
			{
				ordersAdaptor.newOrder(DateTime.Now, current.product.productInfo.price * current.amount, current.amount, Convert.ToString(Session["currentUser"]), current.product.productInfo.productName);
			}
			cartsTableAdapter.deleteCart(Convert.ToString(Session["CurrentUser"]));
			populateList();
			populatePage();
			returnLabel.Text = "Thanks for your order!";
		}
	}
}