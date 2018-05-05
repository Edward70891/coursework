using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public class product
	{
		private defaultDataSetTableAdapters.productsTableAdapter adapter = new defaultDataSetTableAdapters.productsTableAdapter();

		public readonly string productName;
		public readonly string displayName;
		private int Stock;
		public int stock { get { return Stock; } }
		public readonly decimal price;
		public readonly string band;
		public readonly string description;
		public readonly string imagePath;
		public readonly string type;
		public int newStock { get; set; }
		
		public product(string iniName)
		{
			var rows = adapter.getDataTable(iniName);
			DataRow row = rows[0];
			productName = iniName;
			displayName = Convert.ToString(row[3]);
			Stock = Convert.ToInt32(row[1]);
			price = Convert.ToDecimal(row[2]);
			band = Convert.ToString(row[7]);
			description = Convert.ToString(row[8]);
			imagePath = Convert.ToString(row[6]);
			type = Convert.ToString(row[4]);
		}
		public product(DataRow row)
		{
			productName = Convert.ToString(row[0]);
			displayName = Convert.ToString(row[3]);
			Stock = Convert.ToInt32(row[1]);
			price = Convert.ToDecimal(row[2]);
			band = Convert.ToString(row[7]);
			description = Convert.ToString(row[8]);
			imagePath = Convert.ToString(row[6]);
			type = Convert.ToString(row[4]);
		}
		
		public void saveStock()
		{
			adapter.updateStock(newStock, productName);
			Stock = newStock;
			newStock = 0;
		}
	}
	
	public class productPanel : System.Web.UI.WebControls.Panel
	{
		public product info { get; }
		public productPanel(product productInfo)
		{
			info = productInfo;

			this.CssClass = "productDisplayPanel";
			this.ID = info.productName + "Panel";

			Image displayedImage = new Image()
			{
				CssClass = "productImage",
				ID = info.productName + "ImageTag",
				ImageUrl = "~/images/" + info.imagePath,
				Height = 100,
				Width = 100
			};
			displayedImage.Attributes.Add("runat", "server");
			this.Controls.Add(displayedImage);

			Panel textWrapper = new Panel()
			{
				CssClass = "textWrapper",
				ID = info.productName + "TextWrapper"
			};
			textWrapper.Attributes.Add("runat", "server");

			Label nameTag = new Label()
			{
				CssClass = "nameTag",
				Text = info.displayName,
				ID = info.productName + "NameTag"
			};
			nameTag.Attributes.Add("runat", "server");
			textWrapper.Controls.Add(nameTag);

			Label priceTag = new Label()
			{
				CssClass = "priceTag",
				Text = "£" + commonClasses.common.formatPrice(info.price),
				ID = info.productName + "PriceTag"
			};
			priceTag.Attributes.Add("runat", "server");
			textWrapper.Controls.Add(priceTag);

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
			textWrapper.Controls.Add(stockTag);

			Label descriptionTag = new Label()
			{
				CssClass = "descriptionTag",
				Text = Convert.ToString(info.description),
				ID = info.productName + "DescriptionTag"
			};
			descriptionTag.Attributes.Add("runat", "server");
			textWrapper.Controls.Add(descriptionTag);

			this.Controls.Add(textWrapper);
		}
	}
	
	public class productList
	{
		private defaultDataSetTableAdapters.productsTableAdapter adapter = new defaultDataSetTableAdapters.productsTableAdapter();
		private product[] masterList;
		private product[] WorkingList;
		public enum sortType { name,band,stock,price }
		public enum filterType { name,band,type }
		public product[] list
		{
			get
			{
				return WorkingList;
			}
		}
		
		public void resetWorkingList()
		{
			WorkingList = masterList;
		}
		
		public Panel[] generateControls()
		{
			Panel[] result = new Panel[WorkingList.Length];
			for (int i = 0; i < WorkingList.Length; i++)
			{
				result[i] = new productPanel(WorkingList[i]);
			}
			return result;
		}
		
		public void setWorkingList(product[] array)
		{
			WorkingList = array;
		}

		////////////////
		//Constructors//
		////////////////
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
		public productList(string[] productNames)
		{
			int i = 0;
			masterList = new product[productNames.Length];
			foreach (string str in productNames)
			{
				masterList[i] = new product(str);
				i++;
			}
			WorkingList = masterList;
		}
		public productList(DataTable data)
		{
			int i = 0;
			masterList = new product[data.Rows.Count];
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
		
		public void sort(bool ascending, sortType type)
		{
			switch (type)
			{
				case sortType.price:
					WorkingList = sortPrice(WorkingList, ascending);
					break;
				case sortType.stock:
					WorkingList = sortStock(WorkingList, ascending);
					break;
				case sortType.name:
					WorkingList = sortName(WorkingList, ascending);
					break;
				case sortType.band:
					WorkingList = sortBand(WorkingList, ascending);
					break;
			}
		}

		private static product[] sortPrice(product[] input, bool ascending)
		{
			if (input.Length <= 1)
			{
				return input;
			}
			else if (input.Length == 2)
			{
				if ((input[0].price > input[1].price && ascending) || (input[0].price < input[1].price && !ascending))
				{
					product temp = input[0];
					input[0] = input[1];
					input[1] = temp;
				}
			}
			
			product[] subArray;
			List<product> subList = new List<product>();
			product[] superArray;
			List<product> superList = new List<product>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;
			
			for (int i = 0; i < input.Length; i++)
			{
				if (i == pivotIndex)
				{
					continue;
				}
				else if (input[i].price <= input[pivotIndex].price)
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
				else if (input[i].price > input[pivotIndex].price)
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

			subArray = subList.ToArray();
			superArray = superList.ToArray();
			subArray = sortPrice(subArray, ascending);
			superArray = sortPrice(superArray, ascending);

			product[] result;
			result = commonClasses.common.appendArray(subArray, input[pivotIndex]);
			result = commonClasses.common.appendArray(result, superArray);

			return result;
		}

		private static product[] sortStock(product[] input, bool ascending)
		{
			if (input.Length <= 1)
			{
				return input;
			}
			else if (input.Length == 2)
			{
				if ((input[0].stock > input[1].stock && ascending) || (input[0].stock < input[1].stock && !ascending))
				{
					product temp = input[0];
					input[0] = input[1];
					input[1] = temp;
				}
				return input;
			}

			product[] subArray;
			List<product> subList = new List<product>();
			product[] superArray;
			List<product> superList = new List<product>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;

			for (int i = 0; i < input.Length; i++)
			{
				if (i == pivotIndex)
				{
					continue;
				}
				else if (input[i].stock <= input[pivotIndex].stock)
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
				else if (input[i].stock > input[pivotIndex].stock)
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

			subArray = subList.ToArray();
			superArray = superList.ToArray();
			subArray = sortStock(subArray, ascending);
			superArray = sortStock(superArray, ascending);

			product[] result;
			result = commonClasses.common.appendArray(subArray, input[pivotIndex]);
			result = commonClasses.common.appendArray(result, superArray);

			return result;
		}

		private static product[] sortBand(product[] input, bool ascending)
		{
			if (input.Length <= 1)
			{
				return input;
			}
			else if (input.Length == 2)
			{
				if ((input[0].band.CompareTo(input[1].band) > 0 && ascending) || (input[0].band.CompareTo(input[1].band) < 0 && !ascending))
				{
					product temp = input[0];
					input[0] = input[1];
					input[1] = temp;
				}
				return input;
			}

			product[] subArray;
			List<product> subList = new List<product>();
			product[] superArray;
			List<product> superList = new List<product>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;

			for (int i = 0; i < input.Length; i++)
			{
				if (i == pivotIndex)
				{
					continue;
				}
				else if (input[i].band.CompareTo(input[pivotIndex].band) <= 0)
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
				else if (input[i].band.CompareTo(input[pivotIndex].band) > 0)
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

			subArray = subList.ToArray();
			superArray = superList.ToArray();
			subArray = sortName(subArray, ascending);
			superArray = sortName(superArray, ascending);

			product[] result;
			result = commonClasses.common.appendArray(subArray, input[pivotIndex]);
			result = commonClasses.common.appendArray(result, superArray);
			
			return result;
		}

		private static product[] sortName(product[] input, bool ascending)
		{
			if (input.Length <= 1)
			{
				return input;
			}
			else if (input.Length == 2)
			{
				if ((input[0].displayName.CompareTo(input[1].displayName) > 0 && ascending) || (input[0].displayName.CompareTo(input[1].displayName) < 0 && !ascending))
				{
					product temp = input[0];
					input[0] = input[1];
					input[1] = temp;
				}
				return input;
			}
			
			product[] subArray;
			List<product> subList = new List<product>();
			product[] superArray;
			List<product> superList = new List<product>();
			int pivotIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(input.Length) / 2)) - 1;
			
			for (int i = 0; i < input.Length; i++)
			{
				if (i == pivotIndex)
				{
					continue;
				}
				else if (input[i].displayName.CompareTo(input[pivotIndex].displayName) <= 0)
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
				else if (input[i].displayName.CompareTo(input[pivotIndex].displayName) > 0)
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
			
			subArray = subList.ToArray();
			superArray = superList.ToArray();
			subArray = sortName(subArray, ascending);
			superArray = sortName(superArray, ascending);
			
			product[] result;
			result = commonClasses.common.appendArray(subArray, input[pivotIndex]);
			result = commonClasses.common.appendArray(result, superArray);
			
			return result;
		}
		
		public void filter(filterType field, string value, bool whitelist)
		{
			List<product> filteredList = new List<product>();
			int filteredIndex = 0;
			switch (field)
			{
				case filterType.name:
					for (int i = 0; i < masterList.Length; i++)
					{
						if (whitelist)
						{
							if (masterList[i].displayName.ToUpper() == value.ToUpper())
							{
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
						else
						{
							if (masterList[i].displayName.ToUpper() != value.ToUpper())
							{
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
					}
					break;
				case filterType.band:
					for (int i = 0; i < masterList.Length; i++)
					{
						if (whitelist)
						{
							if (masterList[i].band.ToUpper() == value.ToUpper())
							{
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
						else
						{
							if (masterList[i].band.ToUpper() != value.ToUpper())
							{
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
					}
					break;

				case filterType.type:
					for (int i = 0; i < masterList.Length; i++)
					{
						if (whitelist)
						{
							if (masterList[i].type.ToUpper() == value.ToUpper())
							{
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
						else
						{
							if (masterList[i].type.ToUpper() != value.ToUpper())
							{
								filteredList.Add(masterList[i]);
								filteredIndex++;
							}
						}
					}
					break;
			}
			WorkingList = filteredList.ToArray();
		}

		public void removeOutOfStock()
		{
			List<product> filteredList = new List<product>();
			int filteredIndex = 0;
			for (int i = 0; i < masterList.Length; i++)
			{
				if (masterList[i].stock > 0)
				{
					filteredList.Add(masterList[i]);
					filteredIndex++;
				}
			}
			WorkingList = filteredList.ToArray();
		}
	}
}