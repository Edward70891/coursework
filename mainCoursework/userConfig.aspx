<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="userConfig.aspx.cs" Inherits="mainCoursework.configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Panel ID="employeeView" runat="server">
		Configure Employees<br />
		<asp:GridView ID="employeesDisplayTable" runat="server" AutoGenerateColumns="False" DataSourceID="employeesDataSource" EmptyDataText="There are no data records to display." onrowcommand="employeesDisplayTable_RowCommand">
			<Columns>
				<asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username" />
				<asp:BoundField DataField="forename" HeaderText="Forename" />
				<asp:BoundField DataField="surname" HeaderText="Surname" />
				<asp:CheckBoxField DataField="admin" HeaderText="Admin" />
				<asp:TemplateField Visible="True">
					<ItemTemplate>
						<asp:Button ID="employeeDeleteButton" ButtonType="Button" CommandName="deleteUser" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Delete" runat="server" />
						<asp:Button ID="employeePasswordChange" ButtonType="Button" CommandName="changeUserPassword" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Change Password" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<asp:AccessDataSource ID="employeesDataSource" runat="server" DataFile="App_Data\main.accdb" SelectCommand="SELECT * FROM [employees]"></asp:AccessDataSource>
		<br />
		<asp:Button ID="registerRedirect" runat="server" Text="Register a New Employee" OnClick="registerRedirect_Click" />
	</asp:Panel>

	<asp:Panel ID="customerView" runat="server">
		Configure Customers<asp:GridView ID="customersDisplayTable" runat="server" AutoGenerateColumns="False" DataSourceID="customersDataSource" EmptyDataText="There are no data records to display." onrowcommand="customersDisplayTable_RowCommand">
			<Columns>
				<asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username" />
				<asp:BoundField DataField="forename" HeaderText="Forename" />
				<asp:BoundField DataField="surname" HeaderText="Surname" />
				<asp:TemplateField Visible="True">
					<ItemTemplate>
						<asp:Button ID="customerDeleteButton" runat="server" ButtonType="Button" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="deleteUser" Text="Delete" />
						<asp:Button ID="customerPasswordChange" runat="server" ButtonType="Button" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="changeUserPassword" Text="Change Password" />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<asp:SqlDataSource ID="customersDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:mainConnectionString %>" ProviderName="<%$ ConnectionStrings:mainConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [customers]"></asp:SqlDataSource>
		<br />
		<asp:Button ID="newCustomerButton" runat="server" OnClick="newCustomerButton_Click" Text="Register a New Customer" />
		<br />

	</asp:Panel>
	<asp:Panel ID="configControls" runat="server">
		<asp:TextBox ID="passwordBox" runat="server" TextType ="" placeholder ="New Password" TextMode="Password"></asp:TextBox>
		<br />
		<asp:TextBox ID="confirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
		<br />
		<asp:Label ID="returnLabel" runat="server" Text=""></asp:Label>
	</asp:Panel>
</asp:Content>
