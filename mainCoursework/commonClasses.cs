using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using mainCoursework;

namespace commonClasses
{
	//Generic logging of events throughout the program
	public class customLogging
	{
		/// <summary>
		/// Logs the creation of a new session
		/// </summary>
		public static void newSession()
		{
			string result;
			string dividerASCII = "------------------------------------------";
			result = dividerASCII + generateTimestamp() + dividerASCII;
			writeEntry("");
			writeEntry(result);
		}

		/// <summary>
		/// Writes a timestamped entry to the logfile
		/// </summary>
		/// <param name="entryText">The text that will be written to the logfile</param>
		public static void newEntry(string entryText)
		{
			string result = generateTimestamp() + " " + entryText;
			writeEntry(result);
		}

		/// <summary>
		/// Writes a timestamped entry to the logfile with the exception message
		/// </summary>
		/// <param name="except">The exception thrown</param>
		public static void newException(Exception except)
		{
			string result = generateTimestamp() + " An error occurred in " + except.Source + " with message " + except.Message;
			writeEntry(result);
		}

		//Checks both possible locations for the logfile (this needs a better solution) then writes the given text to them
		private static void writeEntry(string entryText)
		{
			try
			{
				using (StreamWriter logFile = new StreamWriter(@"\\albert \2011\R04637\Computer Science\coursework\mainCoursework\App_Data\log.txt", true))
				{
					logFile.WriteLine(entryText);
				}
			}
			catch (IOException)
			{
				using (StreamWriter logFile = new StreamWriter(@"C:\Users\Edward\Source\Repos\coursework\mainCoursework\App_Data\log.txt", true))
				{
					logFile.WriteLine(entryText);
				}
			}
		}

		//Generates a timestamp string in square bracketss
		private static string generateTimestamp()
		{
			string output = "[" + Convert.ToString(DateTime.Now) + "]";
			return output;
		}
	}

	/// <summary>
	/// A small class containing some tools for sanitizing string inputs so SQL injection can't happen
	/// </summary>
	public class customSecurity
	{
		/// <summary>
		/// A constant error message for easy consistency
		/// </summary>
		public const string sanitizeErrorMessage = "The(, ), +, -, = and ' characters are not valid in ANY fields";

		/// <summary>
		/// Check if the code contains (, ), +, -, = or '
		/// </summary>
		/// <param name="input">An array containing all strings to be checked</param>
		/// <returns>True if clean, false if dirty (contains SQL characters)</returns>
		public static bool sanitizeCheck(string[] input)
		{
			bool isClean = true;
			//Goes through all strings in the array
			foreach (string i in input)
			{
				//If any strings contain illegal characters, sets the isclean variable to false and skips the rest of the loops
				if (i.Contains("(") || i.Contains(")") || i.Contains("'") || i.Contains("=") || i.Contains("-") || i.Contains("+"))
				{
					isClean = false;
					break;
				}

			}
			return isClean;
		}

		/// <summary>
		/// MD5 Hasher
		/// </summary>
		/// <param name="input">The string to be hashed</param>
		/// <returns>A hexadecimal value (with lowercase letters)</returns>
		public static string generateMD5(string input)
		{
			//Make a new md5 object
			var md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);
			//Make a new string and add all the new bytes to it
			string output = "";
			for (int i = 0; i < hash.Length; i++)
			{
				output = output + hash[i].ToString("x2");
			}
			return output;
		}
	}

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

		/// <summary>
		/// Gets all the associated data in a string array;
		/// 0 = productName, 1 = displayName, 2 = stock, 3 = Unsaved stock value, 4 = price, 5 = band, 6 = description, 7 = imagePath
		/// </summary>
		public string[] Data
		{
			get
			{
				string[] result = new string[8];
				result[0] = ProductName;
				result[1] = displayName;
				result[2] = Convert.ToString(stock);
				result[3] = Convert.ToString(NewStock);
				result[4] = Convert.ToString(price);
				result[5] = band;
				result[6] = description;
				result[7] = imagePath;
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
		}
	}

	public class productList
	{

	}
}