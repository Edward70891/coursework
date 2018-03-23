<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="staffOverview.aspx.cs" Inherits="mainCoursework.overview" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<%-- X axis content panel --%>
	<asp:Panel ID="dataFilterTypePanel" runat="server">
		Filter:
		<asp:RadioButtonList ID="dataFilterType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dataFilterType_SelectedIndexChanged">
			<asp:ListItem Value="customer">Per Customer</asp:ListItem>
			<asp:ListItem Value="product">Per Product</asp:ListItem>
			<asp:ListItem Value="week">Per Week</asp:ListItem>
			<asp:ListItem Value="month">Per Month</asp:ListItem>
			<asp:ListItem Value="quarter">Per Quarter</asp:ListItem>
		</asp:RadioButtonList>
	</asp:Panel>

	<%-- Y axis content panel --%>
	<asp:Panel ID="yAxisSelectionPanel" runat="server">
		Show:
		<asp:RadioButtonList ID="yAxisSelection" runat="server">
			<asp:ListItem Value="orderVolume">Order Volume</asp:ListItem>
			<asp:ListItem Value="transactionValue">Transaction Value</asp:ListItem>
		</asp:RadioButtonList>
	</asp:Panel>

	<%-- Length of time to show panel --%>
	<asp:Panel ID="timeLengthPanel" runat="server">
		Time Period:
		<asp:RadioButtonList ID="timeLength" runat="server" OnSelectedIndexChanged="timeLength_SelectedIndexChanged" AutoPostBack="True">
			<asp:ListItem Value="day">Day</asp:ListItem>
			<asp:ListItem Value="week">Week</asp:ListItem>
			<asp:ListItem Value="month">Month</asp:ListItem>
			<asp:ListItem Value="6month">6 Months</asp:ListItem>
			<asp:ListItem Value="year">Year</asp:ListItem>
			<asp:ListItem Value="forever">All Time</asp:ListItem>
		</asp:RadioButtonList>
		Beginning:
		<asp:TextBox ID="dateBox" runat="server" TextMode="DateTime" readonly="false"></asp:TextBox>
	</asp:Panel>
	
	<%-- Apply button --%>
	<br />
	<asp:Button ID="applyButton" runat="server" Text="Apply Settings" OnClick="applyButton_Click" />

	<%-- Chart is held here --%>
	<asp:Panel ID="chartHolder" runat="server">

	</asp:Panel>
</asp:Content>
