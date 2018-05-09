<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="mainCoursework.products1" %>

<%@ Register Assembly="mainCoursework" Namespace="mainCoursework" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" CssClass="content">
	<div style="display:inline-block">
	<asp:Panel ID="sortingControls" runat="server" style="border:thin;border-color:grey;margin-left:2px">
		Sort By:
		<br />
		<asp:DropDownList ID="sortField" runat="server">
			<asp:ListItem Value="name">Name</asp:ListItem>
			<asp:ListItem Value="band">Band</asp:ListItem>
			<asp:ListItem Value="stock">Stock</asp:ListItem>
			<asp:ListItem Value="price">Price</asp:ListItem>
		</asp:DropDownList>
		<br />
		<asp:DropDownList ID="sortType" runat="server">
			<asp:ListItem Value="true">Ascending</asp:ListItem>
			<asp:ListItem Value="false">Descending</asp:ListItem>
		</asp:DropDownList>
		<br />
		<asp:Button ID="startSortButton" runat="server" OnClick="startSortButton_Click" Text="Sort" />
	</asp:Panel>

	<asp:Panel ID="filteringControls" runat="server" style="border:thin;border-color:grey;margin-left:2px">
		Filter by:
		<br />
		<asp:DropDownList ID="searchFieldDropdown" runat="server">

			<asp:ListItem Value="displayName">Name</asp:ListItem>
			<asp:ListItem Value="band">Band</asp:ListItem>

		</asp:DropDownList>
		<asp:TextBox ID="searchBox" runat="server" placeholder="Filter Text"></asp:TextBox>
		<asp:Button ID="searchButton" runat="server" Text="Filter" OnClick="searchButton_Click" />
		<br />
		<asp:RadioButtonList ID="whitelistSelect" runat="server">


			<asp:ListItem Value="true">White List</asp:ListItem>
			<asp:ListItem Value="false">Black List</asp:ListItem>


		</asp:RadioButtonList>

	<asp:Panel ID="coastersOrClocksPanel" runat="server">
		Show Only:
		<br />
		<asp:DropDownList ID="coastersOrClocks" runat="server">

			<asp:ListItem Value="coaster">Coaster</asp:ListItem>
			<asp:ListItem Value="clock">Clock</asp:ListItem>

		</asp:DropDownList>
		<asp:Button ID="coasterClockButton" runat="server" Text="Apply" OnClick="coasterClockButton_Click" />
	</asp:Panel>
	</asp:Panel>
	</div>
	<asp:Button ID="resetFilter" runat="server" Text="Reset Filter" OnClick="resetFilter_Click" />
	<asp:Panel ID="productsListPanel" runat="server"></asp:Panel>
	</asp:Content>
