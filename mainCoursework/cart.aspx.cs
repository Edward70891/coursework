using System;
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
		struct cartItem
		{
			public product product;
			public int amount;
		}
		defaultDataSetTableAdapters.cartsTableAdapter cartsTableAdapter = new defaultDataSetTableAdapters.cartsTableAdapter();
		cartItem[] cartList;

		protected void Page_Load(object sender, EventArgs e)
		{
			populateList();
			if (cartList.Length == 0)
			{
				productsListPanel.Controls.Add
					(
						new Label()
						{
							Text = "Your cart is empty!",
							CssClass = "cartEmptyMessage"
						}
					);
			}
			else
			{
				populatePage();
			}
		}

		private void populateList()
		{
			using (DataTable temp = cartsTableAdapter.getCart(Convert.ToString(Session["currentUser"])))
			{
				cartList = new cartItem[temp.Rows.Count];
				int i = 0;
				foreach (DataRow row in temp.Rows)
				{
					cartList[i].product = new product(Convert.ToString(row[2]));
					cartList[i].amount = Convert.ToInt32(row[3]);
					i++;
				}
			}
		}

		private void populatePage()
		{
			foreach(cartItem current in cartList)
			{
				productPanel panel = new productPanel(current.product.productInfo);
				//Remove stockTag and descriptionTag; maybe this works?
				panel.Controls.Remove(panel.Controls[3]);
				panel.Controls.Remove(panel.Controls[3]); //Removing 3 again because it falls into the newly opened space

				TextBox amountBox = new TextBox()
				{
					CssClass = "amountBox",
					ID = current.product.productInfo.productName + "CartAmountBox",
					Text = Convert.ToString(current.amount)
				};
				panel.Controls.Add(amountBox);

				Button removeButton = new Button()
				{
					CssClass = "removeButton",
					ID = current.product.productInfo.productName + "CartRemoveButton",
					Text = "Remove",
					CommandArgument = current.product.productInfo.productName
				};
				removeButton.Click += new EventHandler(removeButton_Click);
				removeButton.Attributes.Add("runat", "server");
				panel.Controls.Add(removeButton);

				Button updateButton = new Button()
				{
					CssClass = "updateButton",
					ID = current.product.productInfo.productName + "CartUpdateButton",
					Text = "Update Amount",
					CommandArgument = current.product.productInfo.productName
				};
				updateButton.Click += new EventHandler(updateButton_Click);
				updateButton.Attributes.Add("runat", "server");
				panel.Controls.Add(updateButton);

				panel.CssClass = "cartProductDisplayPanel";
				productsListPanel.Controls.Add(panel);
			}
		}

		protected void removeButton_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			//Session["productRedirectName"] = btn.CommandArgument;
			////Other code for this goes here
		}

		protected void updateButton_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			//Session["productRedirectName"] = btn.CommandArgument;
			////Other code for this goes here
		}

		protected void purchaseButton_Click(object sender, EventArgs e)
		{
			makeOrder();
		}

		protected void makeOrder()
		{
			if (Convert.ToString(Session["userType"]) != "customer")
			{
				returnLabel.Text = "You're not a registered customer so you can't make an order!";
				return;
			}
			var ordersAdaptor = new defaultDataSetTableAdapters.ordersTableAdapter();
			foreach (cartItem current in cartList)
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