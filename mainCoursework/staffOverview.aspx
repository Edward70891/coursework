﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="staffOverview.aspx.cs" Inherits="mainCoursework.overview" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<asp:Panel ID="dataFilterTypePanel" runat="server" CssClass="chartConfigPanel">
		Filter:
		<asp:RadioButtonList ID="dataFilterType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dataFilterType_SelectedIndexChanged">
			<asp:ListItem Value="customer">Per Customer</asp:ListItem>
			<asp:ListItem Value="product">Per Product</asp:ListItem>
			<asp:ListItem Value="week">Per Week</asp:ListItem>
			<asp:ListItem Value="month">Per Month</asp:ListItem>
			<asp:ListItem Value="quarter">Per Quarter</asp:ListItem>
			<asp:ListItem Value="year">Per Year</asp:ListItem>
		</asp:RadioButtonList>
	</asp:Panel>

	<asp:Panel ID="yAxisSelectionPanel" runat="server" CssClass="chartConfigPanel">
		Show:
		<asp:RadioButtonList ID="yAxisSelection" runat="server">
			<asp:ListItem Value="orderVolume">Order Volume</asp:ListItem>
			<asp:ListItem Value="transactionValue">Transaction Value</asp:ListItem>
		</asp:RadioButtonList>
	</asp:Panel>

	<asp:Panel ID="timeLengthPanel" runat="server" CssClass="chartConfigPanel">
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
	<br />

	<%-- The Chart itself --%>
	<asp:Label ID="returnLabel" runat="server" CssClass="returnLabel"></asp:Label>
	<asp:Chart ID="mainChart" runat="server">
		<Series>
			<asp:Series Name="Default" ChartType="Bar"></asp:Series>
		</Series>
		<ChartAreas>
			<asp:ChartArea Name="chartArea">

			</asp:ChartArea>
		</ChartAreas>
	</asp:Chart>
</asp:Content>
