<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="marketEnd.aspx.cs" Inherits="mainCoursework.marketEnd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	Please enter the amounts of each product you have sold in the relevant textboxes below:
	<asp:Panel ID="productsBox" runat="server">
		<%-- Products go in here --%>
	</asp:Panel>
	<asp:Button ID="applyButton" runat="server" Text="Finalize Sales" OnClick="applyButton_Click" />
	<asp:Label ID="returnLabel" runat="server" Text="" CssClass="returnLabel"></asp:Label>
</asp:Content>
