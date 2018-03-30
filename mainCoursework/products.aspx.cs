using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class products1 : System.Web.UI.Page
	{
		//Opens needed variables: a productList, a panel array, and a bool to facilitate stopping checking the database every refresh
		productList productsDisplayList;
		Panel[] panels;
		bool initialized = false;

		protected void Page_Load(object sender, EventArgs e)
		{
			//If the page has already been initialized, just reload the display, otherwise initialize the productList (and set the bool to true so this doesn't run every time)
			if (initialized)
			{
				populatePage();
			}
			else
			{
				productsDisplayList = new productList();
				populatePage();
				initialized = true;
			}
		}

		private void populatePage()
		{
			//Generate the panel array from the product list
			panels = productsDisplayList.generateControls();
			//Clear out the display list
			productsListPanel.Controls.Clear();
			for (int i = 0; i < panels.Length; i++)
			{
				//Creates and sets the properties of a button for each product and slots it into the panel before it's added to the page
				//This is because you must set the EventHandler on the page the button is running on
				Button detailsButton = new Button()
				{
					CssClass = "detailsButton",
					Text = "View",
					ID = productsDisplayList.list[i].productInfo.productName + "DetailsLinkButton",
					CommandArgument = productsDisplayList.list[i].productInfo.productName
				};
				detailsButton.Click += new EventHandler(detailsButton_Click);
				detailsButton.Attributes.Add("runat", "server");
				panels[i].Controls.Add(detailsButton);
				productsListPanel.Controls.Add(panels[i]);
			}
		}

		//Sends the user to the product page, and sets a sessions value to tell the product page what to display
		protected void detailsButton_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			Session["productRedirectName"] = btn.CommandArgument;
			Server.Transfer("~/productView.aspx", false);
		}

		protected void startSortButton_Click(object sender, EventArgs e)
		{
			//Get whether the user wants ascending sort
			bool ascending;
			if (sortType.SelectedIndex == 0)
			{
				ascending = true;
			}
			else
			{
				ascending = false;
			}
			//Run the sort and repopulate the page
			productsDisplayList.sort(ascending, sortField.SelectedValue);
			populatePage();
		}

		protected void searchButton_Click(object sender, EventArgs e)
		{
			//Get all the variables from the controls on the page
			string searchType = searchFieldDropdown.SelectedValue;
			string searchText = searchBox.Text;
			bool whitelist = Convert.ToBoolean(whitelistSelect.SelectedValue);
			//Apply the filter and refresh the page
			productsDisplayList.filter(searchType, searchBox.Text, whitelist);
			populatePage();
		}

		protected void coasterClockButton_Click(object sender, EventArgs e)
		{
			//Get the filter mode from the control
			string filterMode = coastersOrClocks.SelectedValue;
			//Apply the filter and refresh the page
			productsDisplayList.filter("stock", filterMode, true);
			populatePage();
		}

		protected void resetFilter_Click(object sender, EventArgs e)
		{
			productsDisplayList.resetWorkingList();
			populatePage();
		}
	}
}