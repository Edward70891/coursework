using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class cart : System.Web.UI.Page
	{
		product[] list;
		defaultDataSetTableAdapters.cartsTableAdapter adapter = new defaultDataSetTableAdapters.cartsTableAdapter();

		protected void Page_Load(object sender, EventArgs e)
		{
			System.Data.DataTable productsTable = adapter.getCart(Convert.ToString(Session["currentUser"]));
			list = new product[productsTable.Rows.Count];
			int[] amounts = new int[productsTable.Rows.Count];
			int i = 0;
			foreach (System.Data.DataRow row in productsTable.Rows)
			{
				list[i] = new product(Convert.ToString(row[2]));
				amounts[i] = Convert.ToInt32(row[3]);
			}
		}

		private void populatePage()
		{
			productPanel current;
			foreach (product i in list)
			{
				current = new productPanel(i.productInfo);
				TextBox changeAmountBox = new TextBox()
				{

				};

			}
		}
	}
}