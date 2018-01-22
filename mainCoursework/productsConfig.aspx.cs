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
	public partial class products : System.Web.UI.Page
	{
		private defaultDataSetTableAdapters.productsTableAdapter productQueryTable = new defaultDataSetTableAdapters.productsTableAdapter();

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void productsTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			string displayName = productsTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
				//Runs if the delete button is pressed
				//Checks if this is the second button press
				if ((deletingProductsPersistent.deleting) && (displayName == deletingProductsPersistent.product))
				{
					//Returns that the product has been deleted
					returnLabel.Text = "Product " + displayName + " was deleted";
					//Deletes the product, logs the action, posts the result to the box then clears it
					productQueryTable.deleteProduct(displayName);
					returnLabel.Text = "Product deleted";
					customLogging.newEntry("The product " + displayName + " was deleted");
					productsTable.DataBind();
					System.Threading.Thread.Sleep(2000);
					returnLabel.Text = "";
				}
				else
				{
					//Runs on first click, warns and sets up second click using external class
					returnLabel.Text = "Click again to delete - note that this cannot be undone!";
					deletingProductsPersistent.deleting = true;
					deletingProductsPersistent.product = displayName;
				}
		}

		//Adding products
		protected void productAddButton_Click(object sender, EventArgs e)
		{
			//A dowhile loop that is broken at the end if conditions are met or prematurely if conditions are not met
			do
			{
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
				double price = Convert.ToDouble(productPrice.Text);
				if (((price * 100) % 1.0) != 0)
				{
					returnMessage.Text = "Please input prices to two decimal places!";
					break;
				}

				if (customSecurity.sanitizeCheck(new string[] { productNameBox.Text, productPrice.Text, bandBox.Text, descriptionBox.Text }) != true)
				{
					returnMessage.Text = customSecurity.sanitizeErrorMessage;
					break;
				}

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
					returnMessage.Text = "You must upload an image";
					break;
				}

				//Returns the the product has been created then creates it and logs it and refreshes the table
				returnMessage.Text = "Product created named " + productName + ", priced at £" + Convert.ToString(price) + " and displayed as " + displayName;
				productQueryTable.newProduct(productName, 0, Convert.ToDecimal(price), displayName, typeDropdown.SelectedValue, Convert.ToString(Session["currentUser"]), imagePath, bandBox.Text, descriptionBox.Text);
				customLogging.newEntry("The product " + displayName + " was created");
				productsTable.DataBind();
			} while (false);
		}

		static class deletingProductsPersistent
		{
			public static string product;
			public static bool deleting;
		}
	}
}