<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="mainCoursework.cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Panel ID="productsListPanel" runat="server"></asp:Panel>
	<asp:Button ID="purchaseButton" runat="server" Text="Checkout" OnClick="purchaseButton_Click" />
	<asp:Label ID="returnLabel" runat="server" Text="Label"></asp:Label>
</asp:Content>
