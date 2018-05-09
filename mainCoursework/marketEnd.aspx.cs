using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using commonClasses;

namespace mainCoursework
{
	public partial class marketEnd : System.Web.UI.Page
	{
		protected struct marketItem
		{
			public product product;
			public int amount;
		}

		marketItem[] takenItems = new marketItem[0];
		defaultDataSetTableAdapters.marketTableAdapter marketAdaptor = new defaultDataSetTableAdapters.marketTableAdapter();

		protected void Page_Load(object sender, EventArgs e)
		{
			getTakingItems();
			drawControls();
		}

		protected void getTakingItems()
		{
			var data = marketAdaptor.getStallItems(Convert.ToString(Session["currentUser"]));
			foreach (DataRow current in data)
			{
				var tempItem = new marketItem();
				tempItem.product = new product(Convert.ToString(current[0]));
				tempItem.amount = Convert.ToInt32(current[1]);
				takenItems = common.appendArray(takenItems, tempItem);
			}
		}

		protected void drawControls()
		{
			foreach (marketItem current in takenItems)
			{
				productPanel panel = new productPanel(current.product);
				Label takenLabel = new Label()
				{
					ID = current.product.productName + "AmountTakenLabel",
					CssClass = "amountTakenLabel",
					Text = current.amount + " Taken"
				};
				takenLabel.Attributes.Add("runat", "server");
				panel.Controls[1].Controls.Add(takenLabel);
				TextBox amountSold = new TextBox()
				{
					ID = current.product.productName + "AmountSoldBox",
					CssClass = "amountSoldBox",
					TextMode = TextBoxMode.Number,
					Text = "0"
				};
				amountSold.Attributes.Add("runat", "server");
				panel.Controls[1].Controls.Add(amountSold);
				productsBox.Controls.Add(panel);
			}
		}

		protected void applyButton_Click(object sender, EventArgs e)
		{
			//Create a soldItems array and populate it with the relevant data
			marketItem[] soldItems = new marketItem[takenItems.Length];
			for (int i = 0; i < takenItems.Length; i++)
			{
				soldItems[i].product = takenItems[i].product;
				TextBox amountBox = (TextBox)productsBox.Controls[i].FindControl(takenItems[i].product.productName + "AmountSoldBox");
				soldItems[i].amount = Convert.ToInt32(amountBox.Text);
				if (soldItems[i].amount > takenItems[i].amount)
				{
					returnLabel.Text = "You can't have sold more items than you took!";
					return;
				}
			}

			decimal profit = 0;
			int totalSold = 0;
			defaultDataSetTableAdapters.productsTableAdapter productsAdaptor = new defaultDataSetTableAdapters.productsTableAdapter();
			defaultDataSetTableAdapters.ordersTableAdapter ordersAdaptor = new defaultDataSetTableAdapters.ordersTableAdapter();
			//Go through and run all the needed queries
			for (int i = 0; i < takenItems.Length; i++)
			{
				string currentName = takenItems[i].product.productName;
				//Add back the unsold stock
				int stockChange = takenItems[i].amount - soldItems[i].amount;
				int currentStock = soldItems[i].product.stock;
				productsAdaptor.updateStock(currentStock + stockChange, currentName);
				//Add the relevant order
				decimal spent = soldItems[i].product.price * soldItems[i].amount;
				ordersAdaptor.newOrder(DateTime.Now, spent, soldItems[i].amount, "Market", soldItems[i].product.productName);
				//Remove the item from market
				marketAdaptor.removeListing(soldItems[i].product.productName, Convert.ToString(Session["currentUser"]));
				//Update the values
				profit += spent;
				totalSold += soldItems[i].amount;
			}

			returnLabel.Text = "Congratulations on making " + profit + "!";
			customLogging.newEntry("Employee " + Convert.ToString(Session["currentUser"] + " sold " + totalSold + " products."));
			getTakingItems();
			drawControls();
		}
	}
}