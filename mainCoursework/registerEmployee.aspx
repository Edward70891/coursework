<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="registerEmployee.aspx.cs" Inherits="mainCoursework.registerEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:TextBox ID="submittedUsernameBox" runat="server" placeholder = "Username"></asp:TextBox>
	<br />
	<asp:TextBox ID="submittedPasswordBox" runat="server" TextMode="Password" placeholder ="Password"></asp:TextBox>
	<br />
	<asp:TextBox ID="submittedConfirmPasswordBox" runat="server" TextMode="Password" placeholder ="Confirm Password"></asp:TextBox>
	<br />
	<asp:CheckBox ID="adminCheckBox" runat="server" Text="Admin" />
	<br />
	<asp:TextBox ID="forenameBox" runat="server" placeholder ="Forename(s)"></asp:TextBox>
	<br />
	<asp:TextBox ID="surnameBox" runat="server" placeholder ="Surname"></asp:TextBox>
	<br />
	<asp:Button ID="newUser" runat="server" Text="Register New Employee" OnClick="newUser_Click" />
	<asp:Label ID="registerReturn" runat="server" Text=""></asp:Label>
</asp:Content>
