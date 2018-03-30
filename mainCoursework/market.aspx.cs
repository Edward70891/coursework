using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using commonClasses;
using System.Data;

namespace mainCoursework
{
	public partial class market : System.Web.UI.Page
	{
		/// <summary>
		/// A custom market item with an amount attached
		/// </summary>
		struct marketItem
		{
			public product product;
			public int amount;
		}

		//The product list that will be displayed on the page
		productList productsList;
		marketItem[] takingItems;
		bool initialized = false;
		defaultDataSetTableAdapters.marketTableAdapter adaptor = new defaultDataSetTableAdapters.marketTableAdapter();

		protected void Page_Load(object sender, EventArgs e)
		{
			//If the page has already been loaded, don't refresh the product list
			if (initialized)
			{
				populatePage();
			}
			else
			{
				productsList = new productList();
				populatePage();
				initialized = true;
			}
		}

		/// <summary>
		/// Populate both panels with the correct functions, only to be used on page load
		/// </summary>
		private void populatePage()
		{
			populateMainPanel();
			populateTakingPanel();
		}

		/// <summary>
		/// Create a product list and fill the main panel with it; could probably do this a better way
		/// </summary>
		private void populateMainPanel()
		{
			//Generate the controls and clear out any already on the page
			var controlSet = productsList.generateControls();
			productsListPanel.Controls.Clear();
			//Modify the controls as necessary and add them all to the page
			int i = 0;
			foreach (productPanel current in controlSet)
			{
				//A button to add them to the stall list
				Button addToStall = new Button()
				{
					ID = productsList.list[i].productInfo.productName + "AddToStall",
					Text = "Add",
					CommandArgument = productsList.list[i].productInfo.productName
				};
				addToStall.Click += new EventHandler(addToStall_Click);
				addToStall.Attributes.Add("runat", "server");
				current.Controls.Add(addToStall);
				//Add the whole product panel to the list panel
				productsListPanel.Controls.Add(current);
				i++;
			}
		}

		/// <summary>
		/// Get all the items the current employee has taken out from the database and runs generateTakingControls
		/// </summary>
		private void populateTakingPanel()
		{
			//Open an adaptor
			var data = adaptor.getStallItems(Convert.ToString(Session["currentUser"]));
			//Make a temporary list to hold all the items we're going to generate from the query we're going to run
			List<marketItem> tempList = new List<marketItem>();
			//For each item returned by the query, convert it into a marketItem with the relevant data and add it to the temporary list
			foreach (DataRow current in data)
			{
				marketItem tempItem = new marketItem();
				tempItem.product = new product(Convert.ToString(current[1]));
				tempItem.amount = Convert.ToInt32(current[2]);
				tempList.Add(tempItem);
			}
			//Convert the temporary list to be an array and set it to be the page's global variable, clear the controls in the list
			takingItems = tempList.ToArray();
			generateTakingControls();
		}

		/// <summary>
		/// Actually generate all the controls for the items the employee is taking; no data is accessed here
		/// </summary>
		private void generateTakingControls()
		{
			takingPanel.Controls.Clear();
			//Generate the productPanel for all the items and add an amount text box to it with a text change event
			foreach (marketItem current in takingItems)
			{
				productPanel panel = new productPanel(current.product.productInfo);
				TextBox amountBox = new TextBox()
				{
					ID = current.product.productInfo.productName + "_AmountBox",
					CssClass = "marketAmountBox",
					Text = Convert.ToString(current.amount)
				};
				amountBox.TextChanged += new EventHandler(amountBox_TextChanged);
				amountBox.Attributes.Add("runat", "server");
				panel.Controls.Add(amountBox);
				//Add the whole panel to the holder panel
				takingPanel.Controls.Add(panel);
			}
		}

		private void addToStall_Click(object sender, EventArgs e)
		{
			//Get the product name from the button,
			Button btn = (Button)sender;
			//Check if that product is already in their cart
			bool existing = false;
			foreach (marketItem current in takingItems)
			{
				if (current.product.productInfo.productName == btn.CommandArgument)
				{
					existing = true;
				}
			}
			//If that product is already in the list, notify the user
			if (existing)
			{
				returnLabel.Text = "You've already got that product in your taking list, please update it's amount or remove it!";
			}
			//If not, generate a product and add it
			else
			{
				marketItem newItem = new marketItem();
				newItem.amount = 1;
				newItem.product = new product(btn.CommandArgument);
				common.appendArray(takingItems, newItem);
				generateTakingControls();
			}
		}

		private void amountBox_TextChanged(object sender, EventArgs e)
		{
			//Get the product name from the textbox that sent it
			TextBox box = (TextBox)sender;
			string productName = box.ID.Split('_')[0];
			//Find the index of that product in the taking list
			int index = 0;
			foreach (marketItem current in takingItems)
			{
				if (current.product.productInfo.productName == productName)
				{
					break;
				}
				index++;
			}
			//Change the amount value
			takingItems[index].amount = Convert.ToInt32(box.Text);
		}

		protected void applyButton_Click(object sender, EventArgs e)
		{
			defaultDataSetTableAdapters.productsTableAdapter productsAdaptor = new defaultDataSetTableAdapters.productsTableAdapter();
			//Check that there's enough stock in the database to take that much to the stall and if there isn't, don't take anything out
			foreach (marketItem current in takingItems)
			{
				int currentStock = Convert.ToInt32(productsAdaptor.getStock(current.product.productInfo.productName));
				if (currentStock < current.amount)
				{
					returnLabel.Text = "Sorry, we only have " + currentStock + " of " + current.product.productInfo.productName;
					return;
				}
			}
			//Use the bootleg method of clearing the table of all that employee's stall items then adding them all back updated
			adaptor.deleteStall(Convert.ToString(Session["currentUser"]));
			foreach (marketItem current in takingItems)
			{
				//Insert a stall item
				adaptor.newStallItem(current.product.productInfo.productName, current.amount, Convert.ToString(Session["currentUser"]));
				//Subtract the stock from the products table
				productsAdaptor.updateStock(Convert.ToInt32(productsAdaptor.getStock(current.product.productInfo.productName)) - current.amount, current.product.productInfo.productName);
			}
			populatePage();
		}

		protected void endStallButton_Click(object sender, EventArgs e)
		{
			//Code to redirect to the market end page
		}
	}
}