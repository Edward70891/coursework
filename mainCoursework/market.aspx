<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="market.aspx.cs" Inherits="mainCoursework.market" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Label ID="titleLabel" runat="server" Text=""></asp:Label>
	<asp:Panel ID="productsListPanel" runat="server">

	</asp:Panel>
	<asp:Panel ID="takingPanel" runat="server">

	</asp:Panel>
	<asp:Button ID="applyButton" runat="server" Text="Apply Changes" OnClick="applyButton_Click" />
	<asp:Button ID="endStallButton" runat="server" Text="End Stall (Record Purchases)" OnClick="endStallButton_Click" />
</asp:Content>
