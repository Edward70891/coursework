<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="mainCoursework.products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
		<p>

		</p>

		<p></p>

        <p>
			<asp:TextBox ID="productNameBox" runat="server"></asp:TextBox>
			<asp:TextBox ID="productPrice" runat="server"></asp:TextBox>
			<asp:DropDownList ID="typeDropdown" runat="server">
				<asp:ListItem Selected="True" Value="coaster">Coaster</asp:ListItem>
				<asp:ListItem Value="clock">Clock</asp:ListItem>
			</asp:DropDownList>
        </p>

        <p>
			<asp:Button ID="productAddButton" runat="server" Text="Add Product" OnClick="productAddButton_Click" />
			<asp:Label ID="returnMessage" runat="server" Text=""></asp:Label>
        </p>

    </div>
</asp:Content>
