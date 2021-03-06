﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.UI;
using System.IO;
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
			if(e.CommandName != "deleteProduct")
			{
				return;
			}
			string productName = productsTable.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
			//Runs if the delete button is pressed
			//Checks if this is the second button press
			if ((deletingProductsPersistent.deleting) && (productName == deletingProductsPersistent.product))
			{
				//Returns that the product has been deleted
				returnLabel.Text = "Product " + productName + " was deleted";
				//Deletes the product and it's image, logs the action, posts the result to the box then clears it
				try
				{
					System.IO.File.Delete(Server.MapPath("~/images/") + productName);
				}
				catch { }
				//Delete all the cart items of this product
				using (var cartsAdaptor = new defaultDataSetTableAdapters.cartsTableAdapter())
				{
					cartsAdaptor.deleteProducts(productName);
				}
				//Delete all the market items of this product
				using (var marketAdaptor = new defaultDataSetTableAdapters.marketTableAdapter())
				{
					marketAdaptor.deleteProducts(productName);
				}
				//Delete all the order items of this product
				using (var ordersAdaptor = new defaultDataSetTableAdapters.ordersTableAdapter())
				{
					ordersAdaptor.removeProducts(productName);
				}
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
			string errorAppend = "";

			//Check that the inputs are all full and have no SQL sensitive characters in them
			if (customSecurity.sanitizeCheck(new string[] { productNameBox.Text, productPrice.Text, bandBox.Text, descriptionBox.Text }) != true)
			{
				returnMessage.Text = customSecurity.sanitizeErrorMessage;
				return;
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
				return;
			}

			//Check and format the price to ensure 2dp accuracy and only digits content
			if (!priceValid(productPrice.Text))
			{
				returnMessage.Text = "Invalid price format";
				return;
			}
			decimal price = Convert.ToDecimal(productPrice.Text);

			//Checks the user has selected a file
			if (imageUpload.HasFile)
			{
				var extension = Path.GetExtension(imageUpload.FileName).ToLower();
				if (extension != ".png")
				{
					returnMessage.Text = "The image must be a PNG!";
					return;
				}
				try
				{
					string fileName = imageUpload.FileName;
					imagePath = "~/images/" + fileName;
					imageUpload.SaveAs(Server.MapPath(imagePath)); 
				}
				//Catches any exceptions that might occur and posts them; this is necessary because this procedure is likely to be error ridden
				catch (Exception except)
				{
					returnMessage.Text = "File Upload failed with error " + except.Message + ", please contact a developer";
					return;
				}
			}
			else
			{
				errorAppend = " with no image";
				imagePath = "NONE";
			}

			//Returns the the product has been created then creates it and logs it and refreshes the table
			try
			{
				productQueryTable.newProduct(productName, 0, Convert.ToDecimal(price), displayName, typeDropdown.SelectedValue, Convert.ToString(Session["currentUser"]), imagePath, bandBox.Text, descriptionBox.Text);
			}
			catch (OleDbException)
			{
				returnMessage.Text = "There's already an item like this one!";
				return;
			}
			returnMessage.Text = "Product created named " + productName + ", priced at £" + common.formatPrice(price) + " and displayed as " + displayName + errorAppend;
			customLogging.newEntry("The product " + productName + " was created");
			productsTable.DataBind();
		}

		private static bool priceValid(string input)
		{
			//Make a new string without the decimal point
			string noPoint = input.Replace(".", "");
			//Check if that string contains any non-digit characters
			if(!noPoint.All(Char.IsDigit))
			{
				return false;
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