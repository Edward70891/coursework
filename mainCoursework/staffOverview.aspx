<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="staffOverview.aspx.cs" Inherits="mainCoursework.overview" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	Show:
	<asp:RadioButtonList ID="dataFilterType" runat="server">
		<asp:ListItem Value="customer">Customer</asp:ListItem>
	</asp:RadioButtonList>

	Time Period:
	<asp:RadioButtonList ID="timeLength" runat="server" OnSelectedIndexChanged="timeLength_SelectedIndexChanged">
		<asp:ListItem Value="day">Day</asp:ListItem>
		<asp:ListItem Value="week">Week</asp:ListItem>
		<asp:ListItem Value="month">Month</asp:ListItem>
		<asp:ListItem Value="6month">6 Months</asp:ListItem>
		<asp:ListItem Value="year">Year</asp:ListItem>
		<asp:ListItem Value="forever">All Time</asp:ListItem>
	</asp:RadioButtonList>
	Beginning:
	<asp:TextBox ID="dateBox" runat="server" TextMode="DateTime" readonly="false"></asp:TextBox>
	<asp:Button ID="applyButton" runat="server" Text="Apply Settings" OnClick="applyButton_Click" />
</asp:Content>
