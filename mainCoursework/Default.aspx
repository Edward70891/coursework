<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="mainCoursework._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
		<h1>Please Login</h1>
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
