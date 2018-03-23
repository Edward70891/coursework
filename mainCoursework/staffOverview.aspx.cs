using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace mainCoursework
{
    public partial class overview : System.Web.UI.Page
    {
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

		//If one of the time modes is selected, grey out the time period radio buttons
		protected void dataFilterType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (dataFilterType.SelectedValue == "week" || dataFilterType.SelectedValue == "month" || dataFilterType.SelectedValue == "quarter")
			{
				timeLength.Enabled = false;
				timeLength.SelectedValue = "day";
			}
			else
			{
				timeLength.Enabled = true;
			}
		}

		//Apply the selected settings and render the graph with those settings
		protected void applyButton_Click(object sender, EventArgs e)
		{
			chartHolder.Controls.Clear();
			var chart = new Chart();
		}
	}
}