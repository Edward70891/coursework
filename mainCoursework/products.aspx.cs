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
		productList productsDisplayList = new productList();
		Panel[] panels;
		bool initialized = false;

		protected void Page_Load(object sender, EventArgs e)
		{
			product test = new product("sampleProduct");
			productPanel testPanel = new productPanel(test.productInfo);
			this.Form.Controls.Add(testPanel.generatedControl);
			
			//if (initialized)
			//{
			//	populatePage();
			//}
			//else
			//{
			//	productsDisplayList.importAll();
			//	populatePage();
			//	initialized = true;
			//}
		}

		private void populatePage()
		{
			panels = productsDisplayList.generateControls();
			for (int i = 0; i < panels.Length; i++)
			{
				this.Controls.Add(panels[i]);
			}
		}
	}
}