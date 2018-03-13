<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="productView.aspx.cs" Inherits="mainCoursework.productView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Panel ID="product" runat="server"></asp:Panel>
	<asp:Panel CssClass="productControls" runat="server">
		<asp:TextBox ID="amountToAdd" runat="server" TextMode="Number" placeholder ="Amount"></asp:TextBox>
		<asp:Button ID="cartButton" runat="server" Text="Add to Cart" OnClick="cartButton_Click" />
		<asp:Label ID="returnLabel" runat="server" Text=""></asp:Label>
	</asp:Panel>
</asp:Content>
