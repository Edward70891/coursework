<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="configuration.aspx.cs" Inherits="mainCoursework.configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	

	Configure Users<br />
	

	<asp:GridView ID="usersDisplayTable" runat="server" AutoGenerateColumns="False" DataSourceID="usersDataSource" EmptyDataText="There are no data records to display." onrowcommand="usersDisplayTable_RowCommand">
		<Columns>
			<asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username" />
			<asp:BoundField DataField="clearanceLevel" HeaderText="Access Level" SortExpression="clearanceLevel" />
			<asp:TemplateField>
				<ItemTemplate>
					<asp:Button ID="deleteButton" ButtonType="Button" CommandName="deleteUser" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Delete" runat="server" />
					<asp:Button ID="passwordChange" ButtonType="Button" CommandName="changeUserPassword" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Change Password" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>

	<asp:TextBox ID="passwordBox" runat="server" Text ="Password Box"></asp:TextBox>
	<asp:Label ID="returnLabel" runat="server" Text=""></asp:Label>
	<asp:AccessDataSource ID="usersDataSource" runat="server" DataFile="App_Data\main.accdb" SelectCommand="SELECT [username], [clearanceLevel] FROM [users]"></asp:AccessDataSource>
	<br />
	Register a New User:<br />
	<br />
	Username<br />
	<asp:TextBox ID="submittedUsernameBox" runat="server"></asp:TextBox>
	<br />
	Password<br />
	<asp:TextBox ID="submittedPasswordBox" runat="server" TextMode="Password"></asp:TextBox>
	<br />
	Confirm Password<br />
	<asp:TextBox ID="confirmPasswordBox" runat="server" TextMode="Password"></asp:TextBox>
	<br />
	Clearance Level<br />
	<asp:TextBox ID="submittedAccessLevelBox" runat="server"></asp:TextBox>
	<br />
	<br />
	<asp:Button ID="newUser" runat="server" Text="Register New User" OnClick="newUser_Click" />
	<asp:Label ID="registerReturn" runat="server" Text=""></asp:Label>
</asp:Content>
