<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="employeeLogin.aspx.cs" Inherits="mainCoursework.employeeLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
		<h3>Please login to access the site</h3>
    </div>

	<div class="login">
		<asp:TextBox ID="employeeUsernameBox" runat="server" placeholder ="Username"></asp:TextBox>
		<br />
		<asp:TextBox ID="employeePasswordBox" runat="server" TextMode="Password" placeholder ="Password"></asp:TextBox>
		<p><asp:Label ID="employeeLoginReturnLabel" runat="server"></asp:Label></p>
		<asp:Button ID="employeeSubmitCredentialsButton" runat="server" Text="Log In" OnClick="submitEmployeeCredentialsButton_Click"/>
		<br />
		<asp:Button ID="customerRedirect" runat="server" Text="Customer Login" OnClick="customerRedirect_Click" />
	</div>
</asp:Content>
