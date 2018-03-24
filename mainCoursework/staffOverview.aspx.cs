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
		struct dataValueStruct : IComparable
		{
			public dataValueStruct(string Xaxis, decimal Yaxis)
			{
				Xvalue = Xaxis;
				Yvalue = Yaxis;
			}
			public readonly string Xvalue;
			public readonly decimal Yvalue;

			int IComparable.CompareTo(object obj)
			{
				dataValueStruct comparison = (dataValueStruct)obj;
				if (comparison.Yvalue > this.Yvalue)
				{
					return 1;
				}
				else if (comparison.Yvalue < this.Yvalue)
				{
					return -1;
				}
				else
				{
					return 0;
				}
			}
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
		private dataValueStruct[] getData(DateTime startTime)
		{
			//Get the data
			DataTable data = adaptor.GetData();
			DateTime[] bounds = new DateTime[2];
			dataValueStruct[] output;
			bounds[0] = startTime;

			//Filter out the unwanted data if the user isn't having time divisions on the X axis
			if (dataFilterType.SelectedIndex < 1 && timeLength.SelectedValue != "forever")
			{
				DataView view = new DataView(data);
				switch (timeLength.SelectedValue)
				{
					case "day":
						bounds[1] = startTime.AddDays(1);
						break;
					case "week":
						bounds[1] = startTime.AddDays(7);
						break;
					case "month":
						bounds[1] = startTime.AddMonths(1);
						break;
					case "6month":
						bounds[1] = startTime.AddMonths(6);
						break;
					case "year":
						bounds[1] = startTime.AddYears(1);
						break;
				}
				view.RowFilter = "#" + bounds[0].ToString() + "# < datePlaced < #" + bounds[1].ToString() + "#";
				data = view.ToTable();
			}

			//Group the values in the table into their needed format
			if (dataFilterType.SelectedValue == "customer")
			{
				//Group them according to the count of each for now
				var result = from row in data.AsEnumerable()
							 group row by row.Field<string>("user") into customerGrouping
							 select new
							 {
								 productName = customerGrouping.Key,
								 count = customerGrouping.Count()
							 };
				List<dataValueStruct> list = new List<dataValueStruct>();
				foreach (var t in result)
				{
					dataValueStruct element = new dataValueStruct(t.productName, t.count);
					list.Add(element);
				}
				output = list.ToArray();
				Array.Sort(output);
			}
			else if (dataFilterType.SelectedValue == "product")
			{
				//Group them according to the count of each for now
				var result = from row in data.AsEnumerable()
							 group row by row.Field<string>("productName") into productGrouping
							 select new
							 {
								 productName = productGrouping.Key,
								 count = productGrouping.Count()
							 };
				List<dataValueStruct> list = new List<dataValueStruct>();
				foreach (var t in result)
				{
					dataValueStruct element = new dataValueStruct(t.productName, t.count);
					list.Add(element);
				}
				output = list.ToArray();
				Array.Sort(output);
			}
			else
			{
				output = new dataValueStruct[0];
			}

			return output;
		}
	}
}