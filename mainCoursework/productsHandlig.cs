using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	/// <summary>
	/// A class to instantiate when wanting to have an easy to manage product object with all the relevant information attached.
	/// Please use me instead of directly accessing the database to modify stock!
	/// </summary>
	public class product
	{
		//Gets the data link
		private defaultDataSetTableAdapters.productsTableAdapter adapter = new defaultDataSetTableAdapters.productsTableAdapter();

		//Declares the productstruct
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
			var rows = adapter.getDataTable(searchName);
			//Converts the rows variable to a single object
			DataRow row = rows[0];
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

	/// <summary>
	/// A control that builds itself ready to add
	/// </summary>
	public class productPanel : System.Web.UI.WebControls.Panel
	{
		public productStruct info;
		public productPanel(productStruct productInfo)
		{
			info = productInfo;

			this.CssClass = "productDisplayPanel";
			this.ID = info.productName + "Panel";

			Image displayedImage = new Image()
			{
				CssClass = "productImage",
				ID = info.productName + "ImageTag"
			};
			//Configure image here
			displayedImage.Attributes.Add("runat", "server");
			this.Controls.Add(displayedImage);
			this.Controls.Add(new LiteralControl("<br />"));

			Label nameTag = new Label()
			{
				CssClass = "nameTag",
				Text = info.displayName,
				ID = info.productName + "NameTag"
			};
			nameTag.Attributes.Add("runat", "server");
			this.Controls.Add(nameTag);
			this.Controls.Add(new LiteralControl("<br />"));

			Label priceTag = new Label()
			{
				CssClass = "priceTag",
				Text = "£" + commonClasses.common.formatPrice(info.price),
				ID = info.productName + "PriceTag"
			};
			priceTag.Attributes.Add("runat", "server");
			this.Controls.Add(priceTag);

			Label stockTag = new Label()
			{
				CssClass = "stockTag",
				ID = info.productName + "StockTag"
			};
			if (info.stock == 0)
			{
				stockTag.Text = "Out of stock";
			}
			else
			{
				stockTag.Text = Convert.ToString(info.stock) + " in stock";
			}
			stockTag.Attributes.Add("runat", "server");
			this.Controls.Add(stockTag);
			this.Controls.Add(new LiteralControl("<br />"));

			Label descriptionTag = new Label()
			{
				CssClass = "descriptionTag",
				Text = Convert.ToString(info.description),
				ID = info.productName + "DescriptionTag"
			};
			descriptionTag.Attributes.Add("runat", "server");
			this.Controls.Add(descriptionTag);
			this.Controls.Add(new LiteralControl("<br />"));
		}
	}
	
	/// <summary>
	/// A class for holding a list of products; filtering and sorting them
	/// </summary>
	public class productList
	{
		private defaultDataSetTableAdapters.productsTableAdapter adapter = new defaultDataSetTableAdapters.productsTableAdapter();
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
		/// Generate an array of panels containing product info
		/// </summary>
		/// <returns>An array of panels containing the appropriate information identical to the working list</returns>
		public Panel[] generateControls()
		{
			Panel[] result = new Panel[WorkingList.Length];
			for (int i = 0; i < WorkingList.Length; i++)
			{
				result[i] = new productPanel(WorkingList[i].productInfo);
			}
			return result;
		}

		/// <summary>
		/// Initialize the list with a prebuilt array
		/// </summary>
		/// <param name="array">The array to load into the class</param>
		public void setWorkingList(product[] array)
		{
			WorkingList = array;
		}

		////////////////
		//Constructors//
		////////////////
		/// <summary>
		/// Sets the list to be the entire contents of the database table, initializes working list
		/// </summary>
		public productList()
		{
			var data = adapter.GetData();
			int i = 0;
			masterList = new product[data.Rows.Count];
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
		public productList(string[] productNames)
		{
			int i = 0;
			foreach (string str in productNames)
			{
				masterList[i] = new product(str);
				i++;
			}
		}
		/// <summary>
		/// Initialize the list with a datatable
		/// </summary>
		/// <param name="data">The datatable to use to initialize</param>
		public productList(DataTable data)
		{
			int i = 0;
			foreach (DataRow row in data.Rows)
			{
				masterList[i] = new product(row);
				i++;
			}
			WorkingList = masterList;
		}
		public productList(product[] products)
		{
			masterList = products;
			WorkingList = masterList;
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
					WorkingList = sortPrice(WorkingList, ascending);
					break;
				case "stock":
					WorkingList = sortStock(WorkingList, ascending);
					break;
				case "name":
					WorkingList = sortName(WorkingList, ascending);
					break;
				case "band":
					WorkingList = sortBand(WorkingList, ascending);
					break;
				default:
					throw new System.ArgumentException("The sort type must be one of the specified values.");
			}
		}

		private static product[] sortPrice(product[] input, bool ascending)
		{
			//All the base cases
			//If it is passed an array with a single (or no) element, return just that
			if (input.Length <= 1)
			{
				return input;
			}
			//If it is passed an array with two elements, check if they need swapping and do so if necessary, then return them
			else if (input.Length == 2)
			{
				if ((input[0].productInfo.price > input[1].productInfo.price && ascending) || (input[0].productInfo.price < input[1].productInfo.price && !ascending))
				{
					product temp = input[0];
					input[0] = input[1];
					input[1] = temp;
				}
			}

			//Initialize the needed variables; the two lists to add the numbers to, the arrays to add those to, and the index of the pivot
			product[] subArray;
			List<product> subList = new List<product>();
			product[] superArray;
			List<product> superList = new List<product>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;

			//The actual sort, cycle through all the elements in the array
			for (int i = 0; i < input.Length; i++)
			{
				//If it's looking at the pivot, don't sort it
				if (i == pivotIndex)
				{
					continue;
				}
				//If the current element is smaller than or equal to the pivot, add it to the appropriate list for the sort type
				else if (input[i].productInfo.price <= input[pivotIndex].productInfo.price)
				{
					if (ascending)
					{
						subList.Add(input[i]);
					}
					else
					{
						superList.Add(input[i]);
					}
				}
				//If the current element is larger than the picot, add it to the appropriate list for the sort type
				else if (input[i].productInfo.price > input[pivotIndex].productInfo.price)
				{
					if (ascending)
					{
						superList.Add(input[i]);
					}
					else
					{
						subList.Add(input[i]);
					}
				}
			}

			//Calls itself on the two arrays we produce from the lists
			subArray = subList.ToArray();
			superArray = superList.ToArray();
			subArray = sortPrice(subArray, ascending);
			superArray = sortPrice(superArray, ascending);

			//Assembles the final array
			product[] result;
			result = commonClasses.common.appendArray(subArray, input[pivotIndex]);
			result = commonClasses.common.appendArray(result, superArray);

			//Returns the final array
			return result;
		}

		private static product[] sortStock(product[] input, bool ascending)
		{
			//All the base cases
			//If it is passed an array with a single (or no) element, return just that
			if (input.Length <= 1)
			{
				return input;
			}
			//If it is passed an array with two elements, check if they need swapping and do so if necessary, then return them
			else if (input.Length == 2)
			{
				if ((input[0].productInfo.stock > input[1].productInfo.stock && ascending) || (input[0].productInfo.stock < input[1].productInfo.stock && !ascending))
				{
					product temp = input[0];
					input[0] = input[1];
					input[1] = temp;
				}
				return input;
			}

			//Initialize the needed variables; the two lists to add the numbers to, the arrays to add those to, and the index of the pivot
			product[] subArray;
			List<product> subList = new List<product>();
			product[] superArray;
			List<product> superList = new List<product>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;

			//The actual sort, cycle through all the elements in the array
			for (int i = 0; i < input.Length; i++)
			{
				//If it's looking at the pivot, don't sort it
				if (i == pivotIndex)
				{
					continue;
				}
				//If the current element is smaller than or equal to the pivot, add it to the appropriate list for the sort type
				else if (input[i].productInfo.stock <= input[pivotIndex].productInfo.stock)
				{
					if (ascending)
					{
						subList.Add(input[i]);
					}
					else
					{
						superList.Add(input[i]);
					}
				}
				//If the current element is larger than the picot, add it to the appropriate list for the sort type
				else if (input[i].productInfo.stock > input[pivotIndex].productInfo.stock)
				{
					if (ascending)
					{
						superList.Add(input[i]);
					}
					else
					{
						subList.Add(input[i]);
					}
				}
			}

			//Calls itself on the two arrays we produce from the lists
			subArray = subList.ToArray();
			superArray = superList.ToArray();
			subArray = sortStock(subArray, ascending);
			superArray = sortStock(superArray, ascending);

			//Assembles the final array
			product[] result;
			result = commonClasses.common.appendArray(subArray, input[pivotIndex]);
			result = commonClasses.common.appendArray(result, superArray);

			//Returns the final array
			return result;
		}

		private static product[] sortBand(product[] input, bool ascending)
		{
			//All the base cases
			//If it is passed an array with a single (or no) element, return just that
			if (input.Length <= 1)
			{
				return input;
			}
			//If it is passed an array with two elements, check if they need swapping and do so if necessary, then return them
			else if (input.Length == 2)
			{
				if ((input[0].productInfo.band.CompareTo(input[1].productInfo.band) > 0 && ascending) || (input[0].productInfo.band.CompareTo(input[1].productInfo.band) < 0 && !ascending))
				{
					product temp = input[0];
					input[0] = input[1];
					input[1] = temp;
				}
				return input;
			}

			//Initialize the needed variables; the two lists to add the numbers to, the arrays to add those to, and the index of the pivot
			product[] subArray;
			List<product> subList = new List<product>();
			product[] superArray;
			List<product> superList = new List<product>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;

			//The actual sort, cycle through all the elements in the array
			for (int i = 0; i < input.Length; i++)
			{
				//If it's looking at the pivot, don't sort it
				if (i == pivotIndex)
				{
					continue;
				}
				//If the current element is smaller than or equal to the pivot, add it to the sublist
				else if (input[i].productInfo.band.CompareTo(input[pivotIndex].productInfo.band) <= 0)
				{
					if (ascending)
					{
						subList.Add(input[i]);
					}
					else
					{
						superList.Add(input[i]);
					}
				}
				//If the current element is larger than the picot, add it to the superlist
				else if (input[i].productInfo.band.CompareTo(input[pivotIndex].productInfo.band) > 0)
				{
					if (ascending)
					{
						superList.Add(input[i]);
					}
					else
					{
						subList.Add(input[i]);
					}
				}
			}

			//Calls itself on the two arrays we produce from the lists
			subArray = subList.ToArray();
			superArray = superList.ToArray();
			subArray = sortName(subArray, ascending);
			superArray = sortName(superArray, ascending);

			//Assembles the final array
			product[] result;
			result = commonClasses.common.appendArray(subArray, input[pivotIndex]);
			result = commonClasses.common.appendArray(result, superArray);

			//Returns the final array
			return result;
		}

		private static product[] sortName(product[] input, bool ascending)
		{
			//All the base cases
			//If it is passed an array with a single (or no) element, return just that
			if (input.Length <= 1)
			{
				return input;
			}
			//If it is passed an array with two elements, check if they need swapping and do so if necessary, then return them
			else if (input.Length == 2)
			{
				if ((input[0].productInfo.displayName.CompareTo(input[1].productInfo.displayName) > 0 && ascending) || (input[0].productInfo.displayName.CompareTo(input[1].productInfo.displayName) < 0 && !ascending))
				{
					product temp = input[0];
					input[0] = input[1];
					input[1] = temp;
				}
				return input;
			}

			//Initialize the needed variables; the two lists to add the numbers to, the arrays to add those to, and the index of the pivot
			product[] subArray;
			List<product> subList = new List<product>();
			product[] superArray;
			List<product> superList = new List<product>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;

			//The actual sort, cycle through all the elements in the array
			for (int i = 0; i < input.Length; i++)
			{
				//If it's looking at the pivot, don't sort it
				if (i == pivotIndex)
				{
					continue;
				}
				//If the current element is smaller than or equal to the pivot, add it to the sublist
				else if (input[i].productInfo.displayName.CompareTo(input[pivotIndex].productInfo.displayName) <= 0)
				{
					if (ascending)
					{
						subList.Add(input[i]);
					}
					else
					{
						superList.Add(input[i]);
					}
				}
				//If the current element is larger than the picot, add it to the superlist
				else if (input[i].productInfo.displayName.CompareTo(input[pivotIndex].productInfo.displayName) > 0)
				{
					if (ascending)
					{
						superList.Add(input[i]);
					}
					else
					{
						subList.Add(input[i]);
					}
				}
			}

			//Calls itself on the two arrays we produce from the lists
			subArray = subList.ToArray();
			superArray = superList.ToArray();
			subArray = sortName(subArray, ascending);
			superArray = sortName(superArray, ascending);

			//Assembles the final array
			product[] result;
			result = commonClasses.common.appendArray(subArray, input[pivotIndex]);
			result = commonClasses.common.appendArray(result, superArray);

			//Returns the final array
			return result;
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