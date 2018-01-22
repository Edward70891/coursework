using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mainCoursework
{
	public class productsHandlig
	{

		/// <summary>
		/// A class to instantiate when wanting to have an easy to manage product object with all the relevant information attached.
		/// Please use me instead of directly accessing the database to modify stock!
		/// </summary>
		public class product
		{
			//Gets the data link
			private defaultDataSet.productsDataTable dataTable = new defaultDataSet.productsDataTable();
			private mainCoursework.defaultDataSetTableAdapters.productsTableAdapter adapter = new mainCoursework.defaultDataSetTableAdapters.productsTableAdapter();

			//Declares all the read only variables about the product
			private string ProductName;
			private string displayName;
			private int stock;
			private int NewStock;
			public int newStock { set { NewStock = value; } }
			private decimal price;
			private string band;
			private string description;
			private string imagePath;
			private string type;

			/// <summary>
			/// Gets all the associated data in a string array;
			/// 0 = productName, 1 = displayName, 2 = stock, 3 = Unsaved stock value, 4 = price, 5 = band, 6 = description, 7 = imagePath, 8 = type
			/// </summary>
			public string[] data
			{
				get
				{
					string[] result = new string[9];
					result[0] = ProductName;
					result[1] = displayName;
					result[2] = Convert.ToString(stock);
					result[3] = Convert.ToString(NewStock);
					result[4] = Convert.ToString(price);
					result[5] = band;
					result[6] = description;
					result[7] = imagePath;
					result[8] = type;
					return result;
				}
			}

			/// <summary>
			/// Adds all needed values to their properties and gets the required product from the table
			/// </summary>
			/// <param name="iniName">The productName of the item to pull from the DB</param>
			public product(string iniName)
			{
				getData(iniName);
			}

			/// <summary>
			/// Writes the new stock value to the database
			/// </summary>
			public void saveStock()
			{
				adapter.updateStock(NewStock, ProductName);
				stock = NewStock;
				NewStock = 0;
			}

			public void refresh()
			{
				getData(ProductName);
			}

			private void getData(string searchName)
			{
				//Gets "all" (ie, only the one) rows with the given productname
				var rows = dataTable.Select("productName = '" + searchName + "'");
				//Converts the rows variable to a single object
				System.Data.DataRow row = rows[0];
				ProductName = searchName;
				//Sets all the values according the the indexes of the table columns
				displayName = Convert.ToString(row[3]);
				stock = Convert.ToInt32(row[1]);
				price = Convert.ToDecimal(row[2]);
				band = Convert.ToString(row[7]);
				description = Convert.ToString(row[8]);
				imagePath = Convert.ToString(row[6]);
				type = Convert.ToString(row[4]);
			}
		}


















		/// <summary>
		/// A class for holding a list of products; filtering and sorting them
		/// </summary>
		public class productList
		{
			//The complete list of all the products in the product table - please only read from this once constructed
			private product[] masterList;
			//The variable that all the code in this class will modify, 
			private product[] WorkingList;
			//Read only property that returns the working list for the caller to use
			public product[] list
			{
				get
				{
					return WorkingList;
				}
			}
			//Performs the first construction of the master list
			public productList()
			{
				constructMaster();
			}

			/// <summary>
			/// Reloads all products from the table; use me when expecting database changes from another source
			/// NB also resets the working list
			/// </summary>
			public void refreshMasterList()
			{
				constructMaster();
				resetWorkingList();
			}

			/// <summary>
			/// Resets the working list to be identical to the master (ie. the raw data)
			/// </summary>
			public void resetWorkingList()
			{
				WorkingList = masterList;
			}

			//Sets the master list to be the contents of the database table
			private void constructMaster()
			{
				System.Data.DataTable data;
				using (var adaptor = new defaultDataSetTableAdapters.productsTableAdapter())
				{
					data = adaptor.getProductNames();
				}
				int i = 0;
				foreach (DataRow row in data.Rows)
				{
					masterList[i] = new product(Convert.ToString(row[0]));
					i++;
				}
			}

			/// <summary>
			/// Sorts the working list according to it's parameters
			/// </summary>
			/// <param name="ascending">If true, list will be sorted in ascending order; If false, descending order</param>
			/// <param name="sortType">What parameter to sort the list by, "price", "stock", "name", or "band"</param>
			public void sort(bool ascending, string sortType)
			{
				switch (sortType)
				{
					case "price":

					case "stock":

					case "name":

					case "band":

					default:
						throw new System.ArgumentException("The sort type must be one of the specified values.");
				}
			}

			/// <summary>
			/// Filters the working list
			/// </summary>
			/// <param name="field">What piece of data to filter by; "band", "type" or "stock" (stock is treated as all things in stock)</param>
			/// <param name="value">What value to look for</param>
			/// <param name="whitelist">Set me to true to keep only entries with that value; false to keep all but that value</param>
			public void filter(string field, string value, bool whitelist)
			{
				switch (field)
				{
					case "band":

					case "type":

					case "stock":

					default:
						throw new System.ArgumentException("The filter field must be one of the specified values");
				}
			}
		}
	}
}