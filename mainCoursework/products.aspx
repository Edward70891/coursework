<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="mainCoursework.products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <p><asp:TextBox ID="productNameBox" runat="server"></asp:TextBox><asp:TextBox ID="productPrice" runat="server"></asp:TextBox></p>
        <p><asp:Button ID="productAddButton" runat="server" Text="Add Product" OnClick="productAddButton_Click" /><asp:Label ID="returnMessage" runat="server" Text=""></asp:Label></p>
    </div>
</asp:Content>
