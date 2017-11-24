<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="employeeLogin.aspx.cs" Inherits="mainCoursework.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
		<h3>Please login to access the site</h3>
    </div>

	<div class="login">
		<asp:TextBox ID="usernameBox" runat="server" placeholder ="Username"></asp:TextBox>
		<br />
		<asp:TextBox ID="passwordBox" runat="server" TextMode="Password" placeholder ="Password"></asp:TextBox>
		<p><asp:Label ID="returnLabel" runat="server" Text=""></asp:Label></p>
		<asp:Button ID="submitCredentialsButton" runat="server" Text="Log In" OnClick="submitCredentialsButton_Click"/>
	</div>
</asp:Content>
