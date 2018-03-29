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
		//It would be best to delete this structure and just deal with the arrays on their own
		struct dataEntryStruct : IComparable
		{
			//Declare variables and set them, this constructor should be mandatory
			public dataEntryStruct(string Xaxis, decimal Yaxis)
			{
				Xvalue = Xaxis;
				Yvalue = Yaxis;
			}
			public readonly string Xvalue;
			public readonly decimal Yvalue;

			//Define sorting logic
			int IComparable.CompareTo(object obj)
			{
				dataEntryStruct comparison = (dataEntryStruct)obj;
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

		//The dataset adaptor, this should really be global
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
			returnLabel.Text = "";
			//Pull the date the user wants and deny them if it's not in a valid format (so long as it's needed)
			DateTime startTime;
			if (!DateTime.TryParse(dateBox.Text, out startTime) && timeLength.SelectedValue != "forever" && dataFilterType.SelectedIndex < 2)
			{
				returnLabel.Text = "That's not a valid date format!";
				return;
			}


			////Initialize the needed variables
			//var data = getDataByType(startTime);
			//string[] xValues = new string[data.Length];
			//decimal[] yValues = new decimal[data.Length];
			//int i = 0;
			////Transfer the data values into the array axes we've made
			//foreach (dataEntryStruct current in data)
			//{
			//	xValues[i] = current.Xvalue;
			//	yValues[i] = current.Yvalue;
			//	i++;
			//}
			////Insert the axes into the chart area
			//mainChart.ChartAreas["chartArea"].AxisX = new Axis(mainChart.ChartAreas["chartArea"], AxisName.X);
			//mainChart.ChartAreas["chartArea"].AxisY = new Axis(mainChart.ChartAreas["chartArea"], AxisName.Y);
			//mainChart.Series["Default"].Points.DataBindXY(xValues, yValues);
			////Try to render the chart on the page, this doesn't work
			////mainChart.SaveImage(System.IO.Directory.GetCurrentDirectory(), ChartImageFormat.Jpeg);
		}

		////Get the data and format it ready to add to the chart
		//private dataEntryStruct[] getDataByType(DateTime startTime)
		//{
		//	//Get the data
		//	DataTable data = adaptor.GetData();
		//	DateTime[] bounds = new DateTime[2];
		//	dataEntryStruct[] output;
		//	bounds[0] = startTime;

		//	//Filter out the unwanted data if the user isn't having time divisions on the X axis
		//	if (dataFilterType.SelectedIndex < 1 && timeLength.SelectedValue != "forever")
		//	{
		//		DataView view = new DataView(data);
		//		switch (timeLength.SelectedValue)
		//		{
		//			case "day":
		//				bounds[1] = startTime.AddDays(1);
		//				break;
		//			case "week":
		//				bounds[1] = startTime.AddDays(7);
		//				break;
		//			case "month":
		//				bounds[1] = startTime.AddMonths(1);
		//				break;
		//			case "6month":
		//				bounds[1] = startTime.AddMonths(6);
		//				break;
		//			case "year":
		//				bounds[1] = startTime.AddYears(1);
		//				break;
		//		}
		//		view.RowFilter = "#" + bounds[0].ToString() + "# < datePlaced < #" + bounds[1].ToString() + "#";
		//		data = view.ToTable();
		//	}

		//	groupData(dataFilterType.SelectedValue);

		//	//Sort the output and return it
		//	Array.Sort(output);
		//	return output;

		//	//Group the data according to the type requested by the user
		//	void groupData(string type)
		//	{
		//		//Make a list of the custom structure and define a query to group the data and sum the amounts
		//		List<dataEntryStruct> list = new List<dataEntryStruct>();
		//		var query = data.Rows.Cast<DataRow>()
		//			.GroupBy(product => product[type])
		//			.Select(grouped => new {
		//				name = grouped.Key,
		//				total = grouped.Sum(product => (int)product["productAmount"])
		//		});

		//		//Populate the list by running the query and convert it into an array
		//		foreach (var t in query)
		//		{
		//			list.Add(new dataEntryStruct(Convert.ToString(t.name), Convert.ToDecimal(t.total)));
		//		}
		//		output = list.ToArray();
		//	}
		//}
	}
}