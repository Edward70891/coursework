<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="configuration.aspx.cs" Inherits="mainCoursework.configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	

	<asp:GridView ID="usersDisplayTable" runat="server" AutoGenerateColumns="False" DataSourceID="usersDataSource" EmptyDataText="There are no data records to display." CellPadding="4" ForeColor="#333333" GridLines="None">
		<AlternatingRowStyle BackColor="White" />
		<Columns>
			<asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username" />
			<asp:BoundField DataField="clearanceLevel" HeaderText="Access Level" SortExpression="clearanceLevel" />
			<asp:ButtonField ButtonType="Button" Text="Delete" CommandName="deleteUser" />
			<asp:ButtonField ButtonType="Button" Text="Change Password" CommandName="changeUserPassword" />
		</Columns>
		<EditRowStyle BackColor="#2461BF" />
		<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
		<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
		<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
		<RowStyle BackColor="#EFF3FB" />
		<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		<SortedAscendingCellStyle BackColor="#F5F7FB" />
		<SortedAscendingHeaderStyle BackColor="#6D95E1" />
		<SortedDescendingCellStyle BackColor="#E9EBEF" />
		<SortedDescendingHeaderStyle BackColor="#4870BE" />
	</asp:GridView>

	<asp:Label ID="returnLabel" runat="server" Text=""></asp:Label>
	<asp:Button ID="deleteConfirm" runat="server" Text="Comfirm Deletion" Visible="False" />
	<asp:AccessDataSource ID="usersDataSource" runat="server" DataFile="App_Data\main.accdb" SelectCommand="SELECT [username], [clearanceLevel] FROM [users]"></asp:AccessDataSource>
</asp:Content>
