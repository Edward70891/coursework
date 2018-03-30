<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="customerLogin.aspx.cs" Inherits="mainCoursework.customerLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="login">
		Customer Login
		<asp:TextBox ID="customerUsernameBox" runat="server" placeholder ="Username"></asp:TextBox>
		<br />
		<asp:TextBox ID="customerPasswordBox" runat="server" TextMode="Password" placeholder ="Password"></asp:TextBox>
		<p><asp:Label ID="customerLoginReturnLabel" runat="server" Text="" CssClass="returnLabel"></asp:Label></p>
		<asp:Button ID="submitCustomerCredentialsButton" runat="server" Text="Log In" OnClick="submitCustomerCredentialsButton_Click"/>
		<asp:Button ID="registerButton" runat="server" Text="Register" OnClick="registerButton_Click" />
		<br />
		<asp:Button ID="employeeRedirect" runat="server" Text="Employee Login" OnClick="employeeRedirect_Click" />
	</div>
</asp:Content>
