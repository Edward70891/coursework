using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

namespace mainCoursework
{
    public partial class overview : System.Web.UI.Page
	{
		struct dataStruct
		{
			public string Xvalue;
			public decimal Yvalue;
		}

		defaultDataSetTableAdapters.ordersTableAdapter adaptor = new defaultDataSetTableAdapters.ordersTableAdapter();

		protected void Page_Load(object sender, EventArgs e)
        {
			
		}

		//If "forever" is selected, grey out the time length text box
		protected void timeLength_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (timeLength.SelectedValue == "forever")
			{
				dateBox.ReadOnly = true;
			}
			else
			{
				dateBox.ReadOnly = false;
			}
		}

		//If one of the time modes is selected, grey out the time period radio buttons and the time beginning box
		protected void dataFilterType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (dataFilterType.SelectedIndex > 1)
			{
				timeLength.Enabled = false;
				timeLength.SelectedValue = "day";
				dateBox.ReadOnly = true;
				dateBox.Text = "";
			}
			else
			{
				timeLength.Enabled = true;
				dateBox.ReadOnly = false;
			}
		}

		//Apply the selected settings and render the graph with those settings
		protected void applyButton_Click(object sender, EventArgs e)
		{
			//Pull the date the user wants and deny them if it's not in a valid format (so long as it's needed)
			DateTime startTime;
			if (!DateTime.TryParse(dateBox.Text, out startTime) && timeLength.SelectedValue != "forever" && dataFilterType.SelectedIndex < 2)
			{
				returnLabel.Text = "That's not a valid date format!";
				return;
			}

			var data = getData(startTime);

			chartHolder.Controls.Clear();
			var chart = new Chart();
		}

		//Get the data and format it ready
		private dataStruct getData(DateTime startTime)
		{
			//Get the data
			DataTable data = adaptor.GetData();

			//Filter out the unwanted data if the user isn't having time divisions on the X axis
			if (dataFilterType.SelectedIndex < 1)
			{
				switch (timeLength.SelectedValue)
				{
					case "forever":
						break;
					case "day":

					case "week":

					case "month":

					case "6month":

					case "year":

				}
			}
		}
	}
}