using System;
using System.Data;
using System.Collections.Generic;

namespace mainCoursework
{
	/// <summary>
	/// A class to instantiate when wanting to have an easy to manage product object with all the relevant information attached.
	/// Please use me instead of directly accessing the database to modify stock!
	/// </summary>
	public class product
	{
		/// <summary>
		/// A struct for storing product info, highly recommended to use the product class instead of this as that includes logic
		/// </summary>
		public struct productStruct
		{
			public string productName;
			public string displayName;
			public int stock;
			public decimal price;
			public string band;
			public string description;
			public string imagePath;
			public string type;
		}

		//Gets the data link
		private defaultDataSet.productsDataTable dataTable = new defaultDataSet.productsDataTable();
		private defaultDataSetTableAdapters.productsTableAdapter adapter = new defaultDataSetTableAdapters.productsTableAdapter();

		//Declares the needed variables
		private productStruct ProductInfo = new productStruct();
		/// <summary>
		/// All the info about the current product
		/// </summary>
		public productStruct productInfo
		{
			get
			{
				return ProductInfo;
			}
		}
		public int newStock { get; set; }

		/// <summary>
		/// Initialize the product using the name of a product; this method will access the database (and thus is slower)
		/// </summary>
		/// <param name="iniName">The name of the product to pull from the database</param>
		public product(string iniName)
		{
			getData(iniName);
		}
		/// <summary>
		/// Initialize the product using a datarow passed to the method, does not access database
		/// </summary>
		/// <param name="row">The datarow containing the product information</param>
		public product(DataRow row)
		{
			ProductInfo.productName = Convert.ToString(row[0]);
			ProductInfo.displayName = Convert.ToString(row[3]);
			ProductInfo.stock = Convert.ToInt32(row[1]);
			ProductInfo.price = Convert.ToDecimal(row[2]);
			ProductInfo.band = Convert.ToString(row[7]);
			ProductInfo.description = Convert.ToString(row[8]);
			ProductInfo.imagePath = Convert.ToString(row[6]);
			ProductInfo.type = Convert.ToString(row[4]);
		}
		
		/// <summary>
		/// Writes the new stock value to the database
		/// </summary>
		public void saveStock()
		{
			adapter.updateStock(newStock, ProductInfo.productName);
			ProductInfo.stock = newStock;
			newStock = 0;
		}

		/// <summary>
		/// Refreshes the data using data from the database regardless of how the class was initialized
		/// </summary>
		public void refresh()
		{
			getData(ProductInfo.productName);
		}

		/// <summary>
		/// Load the product straight from the database
		/// </summary>
		/// <param name="searchName">The name of the product to fetch</param>
		private void getData(string searchName)
		{
			//Gets "all" (ie, only the one) rows with the given productname
			var rows = dataTable.Select("productName = '" + searchName + "'");
			//Converts the rows variable to a single object
			System.Data.DataRow row = rows[0];
			ProductInfo.productName = searchName;
			//Sets all the values according the the indexes of the table columns
			ProductInfo.displayName = Convert.ToString(row[3]);
			ProductInfo.stock = Convert.ToInt32(row[1]);
			ProductInfo.price = Convert.ToDecimal(row[2]);
			ProductInfo.band = Convert.ToString(row[7]);
			ProductInfo.description = Convert.ToString(row[8]);
			ProductInfo.imagePath = Convert.ToString(row[6]);
			ProductInfo.type = Convert.ToString(row[4]);
		}
	}


















	/// <summary>
	/// A class for holding a list of products; filtering and sorting them
	/// </summary>
	public class productList
	{
		//The complete list of all the products initialised
		private product[] masterList;
		//The variable that all the code in this class will modify
		private product[] WorkingList;
		//Read only property that returns the working list for the caller to use
		public product[] list
		{
			get
			{
				return WorkingList;
			}
		}

		/// <summary>
		/// Resets the working list to be identical to the master (ie. the raw data)
		/// </summary>
		public void resetWorkingList()
		{
			WorkingList = masterList;
		}

		/// <summary>
		/// Sets the list to be the entire contents of the database table, initializes working list
		/// </summary>
		public void importAll()
		{
			var data = new defaultDataSet.productsDataTable();
			int i = 0;
			foreach (DataRow row in data.Rows)
			{
				masterList[i] = new product(row);
				i++;
			}
			WorkingList = masterList;
		}

		/// <summary>
		/// Call me to initialize the master list with a given set of names
		/// </summary>
		/// <param name="productNames">The productName value of all the products to initialize</param>
		public void importWithString(string[] productNames)
		{
			int i = 0;
			foreach (string str in productNames)
			{
				masterList[i] = new product(str);
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
		/// <param name="field">What piece of data to filter by; "band", "type" or "stock" (stock is treated as all things in stock (stock will be whitelisted regardless of the whitelist input))</param>
		/// <param name="value">What value to look for</param>
		/// <param name="whitelist">Set me to true to keep only entries with that value; false to keep all but that value</param>
		public void filter(string field, string value, bool whitelist)
		{
			//Makes a new list
			List<product> filteredList = new List<product>();
			int filteredIndex = 0;
			//Determines what field to sort by according to user input
			switch (field)
			{
				case "band":
					//Goes through all the objects in the master list
					for (int i = 0; i < masterList.Length; i++)
					{
						if (whitelist)
						{
							//Runs if it's whitelisting
							if (masterList[i].productInfo.band.ToUpper() == value.ToUpper())
							{
								//If their band matches add them to the filtered list
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
						else
						{
							//Runs if it's blacklisting (ie whitelist=false)
							if (masterList[i].productInfo.band.ToUpper() != value.ToUpper())
							{
								//If their band doesn't match add them to the filtered list
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
					}
					//Set the working list to be the filtered list
					WorkingList = filteredList.ToArray();
					break;

				case "type":
					//Goes through all the objects in the master list
					for (int i = 0; i < masterList.Length; i++)
					{
						if (whitelist)
						{
							//Runs if it's whitelisting
							if (masterList[i].productInfo.type.ToUpper() == value.ToUpper())
							{
								//If their band matches add them to the filtered list
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
						else
						{
							//Runs if it's blacklisting (ie whitelist=false)
							if (masterList[i].productInfo.type.ToUpper() != value.ToUpper())
							{
								//If their band doesn't match add them to the filtered list
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
					}
					//Set the working list to be the filtered list
					WorkingList = filteredList.ToArray();
					break;

				case "stock":
					//Goes through all the objects in the master list
					for (int i = 0; i < masterList.Length; i++)
					{
						if (masterList[i].productInfo.stock > 0)
						{
							//If their stock isn't 0 add them to the filtered list
							filteredList.Add(masterList[i]);
							filteredIndex++;
						}
					}
					//Set the working list to be the filtered list
					WorkingList = filteredList.ToArray();
					break;
				//If incorrectly called this is thrown
				default:
					throw new ArgumentException("The filter field must be one of the specified values");
			}
		}
	}
}