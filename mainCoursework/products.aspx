<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="mainCoursework.products1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Panel ID="SortingControlsdddddd" runat="server">
		<p>
			Sort By:
		</p>
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
	<asp:Panel ID="productsListPanel" runat="server"></asp:Panel>
	</asp:Content>
