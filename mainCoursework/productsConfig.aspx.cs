using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.UI;
using commonClasses;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class productsConfig : System.Web.UI.Page
	{
		private defaultDataSetTableAdapters.productsTableAdapter productQueryTable = new defaultDataSetTableAdapters.productsTableAdapter();

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void productsTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			string productName = productsTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
				//Runs if the delete button is pressed
				//Checks if this is the second button press
				if ((deletingProductsPersistent.deleting) && (productName == deletingProductsPersistent.product))
				{
					//Returns that the product has been deleted
					returnLabel.Text = "Product " + productName + " was deleted";
					//Deletes the product and it's image, logs the action, posts the result to the box then clears it
					System.IO.File.Delete(Server.MapPath("~/images/") + productName);
					productQueryTable.deleteProduct(productName);
					returnLabel.Text = "Product deleted";
					customLogging.newEntry("The product " + productName + " was deleted");
					productsTable.DataBind();
					System.Threading.Thread.Sleep(2000);
					returnLabel.Text = "";
				}
				else
				{
					//Runs on first click, warns and sets up second click using external class
					returnLabel.Text = "Click again to delete - note that this cannot be undone!";
					deletingProductsPersistent.deleting = true;
					deletingProductsPersistent.product = productName;
				}
		}

		//Adding products
		protected void productAddButton_Click(object sender, EventArgs e)
		{
			//A dowhile loop that is broken at the end if conditions are met or prematurely if conditions are not met
			do
			{
				string errorAppend = "";

				//Check that the inputs are all full and have no SQL sensitive characters in them
				if (customSecurity.sanitizeCheck(new string[] { productNameBox.Text, productPrice.Text, bandBox.Text, descriptionBox.Text }) != true)
				{
					returnMessage.Text = customSecurity.sanitizeErrorMessage;
					break;
				}

				//Check and format the name for both display and storage/reference purposes
				string displayName = productNameBox.Text;
				string productName = displayName;
				string imagePath;
				TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
				productName = cultInfo.ToTitleCase(productName);
				productName = productName.Replace(" ", "");
				productName = char.ToLower(productName[0]) + productName.Substring(1);
				if (productName.All(Char.IsLetterOrDigit) == false)
				{
					returnMessage.Text = "Please only use numbers and letters in the product name!";
					break;
				}

				//Check and format the price to ensure 2dp accuracy and only digits content
				if (!priceValid(productPrice.Text))
				{
					returnMessage.Text = "Invalid price format";
					break;
				}
				decimal price = Convert.ToDecimal(productPrice.Text);

				//Checks the user has selected a file
				if (imageUpload.HasFile)
				{
					try
					{
						string fileName = productName + imageUpload.FileName.Substring(imageUpload.FileName.IndexOf('.'));
						imagePath = Server.MapPath("~/images/") + fileName;
						imageUpload.SaveAs(imagePath);
					}
					//Catches any exceptions that might occur and posts them; this is necessary because this procedure is likely to be error ridden
					catch (Exception except)
					{
						returnMessage.Text = "File Upload failed with error " + except.Message + ", please contact a developer";
						break;
					}
				}
				else
				{
					errorAppend = " with no image";
					imagePath = "NONE";
				}

				//Returns the the product has been created then creates it and logs it and refreshes the table
				returnMessage.Text = "Product created named " + productName + ", priced at £" + common.formatPrice(price) + " and displayed as " + displayName + errorAppend;
				productQueryTable.newProduct(productName, 0, Convert.ToDecimal(price), displayName, typeDropdown.SelectedValue, Convert.ToString(Session["currentUser"]), imagePath, bandBox.Text, descriptionBox.Text);
				customLogging.newEntry("The product " + productName + " was created");
				productsTable.DataBind();
			} while (false);
		}

		private static bool priceValid(string input)
		{
			//Make a new string without the decimal point
			string noPoint = input.Replace(".", "");
			//Check if that string contains any non-digit characters
			foreach (char c in noPoint)
			{
				if (!char.IsDigit(c))
				{
					return false;
				}
			}
			//Check how many decimal places there are, and return false if there are more than 2
			try
			{
				string decimalPlaces = input.Substring(input.IndexOf('.') + 1);
				if (decimalPlaces.Length > 2)
				{
					return false;
				}
				return true;
			}
			catch
			{
				return true;
			}
		}

		static class deletingProductsPersistent
		{
			public static string product;
			public static bool deleting;
		}
	}
}