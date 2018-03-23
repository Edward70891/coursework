<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="userProfile.aspx.cs" Inherits="mainCoursework.userProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:GridView ID="ordersDisplayTable" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="userOrdersDataSource">
		<Columns>
			<asp:BoundField DataField="datePlaced" HeaderText="Date" SortExpression="datePlaced" />
			<asp:BoundField DataField="product" HeaderText="Product" SortExpression="product" />
			<asp:BoundField DataField="productAmount" HeaderText="Amount" SortExpression="productAmount" />
			<asp:BoundField DataField="spent" HeaderText="Cost" SortExpression="spent" />
		</Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="userOrdersDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:mainConnectionString %>" ProviderName="<%$ ConnectionStrings:mainConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [orders] WHERE ([customer] = ?)">
		<SelectParameters>
			<asp:SessionParameter Name="customer" SessionField="currentUser" Type="String" />
		</SelectParameters>
	</asp:SqlDataSource>
</asp:Content>
