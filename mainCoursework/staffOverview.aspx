<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="staffOverview.aspx.cs" Inherits="mainCoursework.overview" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:RadioButtonList ID="dataFilterType" runat="server">
		<asp:ListItem Value="customer">Customer</asp:ListItem>

	</asp:RadioButtonList>

	<asp:RadioButtonList ID="timeLength" runat="server">


	</asp:RadioButtonList>
</asp:Content>
