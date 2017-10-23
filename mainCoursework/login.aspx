<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="mainCoursework.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
		<h3>Please login to access the site</h3>
    </div>

	<div class="login">
		<p>User Name:</p>
		<asp:TextBox ID="usernameBox" runat="server"></asp:TextBox>
		<p>Password:</p>
		<asp:TextBox ID="passwordBox" runat="server" TextMode="Password"></asp:TextBox>
		<p><asp:Label ID="returnLabel" runat="server" Text=""></asp:Label></p>
		<asp:Button ID="submitCredentialsButton" runat="server" Text="Log In" OnClick="submitCredentialsButton_Click"/>
	</div>
</asp:Content>
