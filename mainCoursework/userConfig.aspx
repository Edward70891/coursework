<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="userConfig.aspx.cs" Inherits="mainCoursework.configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	

	Configure Users<br />
	

	<asp:GridView ID="usersDisplayTable" runat="server" AutoGenerateColumns="False" DataSourceID="usersDataSource" EmptyDataText="There are no data records to display." onrowcommand="usersDisplayTable_RowCommand">
		<Columns>
			<asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username" />
			<asp:BoundField DataField="forename" HeaderText="Forename" />
			<asp:BoundField DataField="surname" HeaderText="Surname" />
			<asp:CheckBoxField DataField="admin" HeaderText="Admin" />
			<asp:TemplateField Visible="True">
				<ItemTemplate>
					<asp:Button ID="deleteButton" ButtonType="Button" CommandName="deleteUser" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Delete" runat="server" />
					<asp:Button ID="passwordChange" ButtonType="Button" CommandName="changeUserPassword" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Change Password" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>

	<asp:TextBox ID="passwordBox" runat="server" TextType ="" placeholder ="New Password" TextMode="Password"></asp:TextBox>
	<br />
	<asp:TextBox ID="confirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
	<br />
	<asp:Label ID="returnLabel" runat="server" Text=""></asp:Label>
	<asp:AccessDataSource ID="usersDataSource" runat="server" DataFile="App_Data\main.accdb" SelectCommand="SELECT * FROM [employees]"></asp:AccessDataSource>
	<br />
	<br />
	<asp:Button ID="registerRedirect" runat="server" Text="Register a New Employee" OnClick="registerRedirect_Click" />
	
</asp:Content>
