<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="orderHistory.aspx.cs" Inherits="mainCoursework.orderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:GridView ID="ordersDisplayTable" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ordersDataSource">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
			<asp:BoundField DataField="datePlaced" HeaderText="Date" SortExpression="datePlaced" />
			<asp:BoundField DataField="customer" HeaderText="Username" SortExpression="customer" />
			<asp:BoundField DataField="product" HeaderText="Product Name" SortExpression="product" />
			<asp:BoundField DataField="productAmount" HeaderText="Amount" SortExpression="productAmount" />
			<asp:BoundField DataField="spent" HeaderText="Transaction" SortExpression="spent" />
		</Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="ordersDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:mainConnectionString %>" ProviderName="<%$ ConnectionStrings:mainConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [orders]"></asp:SqlDataSource>
</asp:Content>
