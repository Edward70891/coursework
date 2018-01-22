<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="productsConfig.aspx.cs" Inherits="mainCoursework.products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		Products<br />
		<asp:GridView ID="productsTable" runat="server" AutoGenerateColumns="False" DataKeyNames="productName" DataSourceID="productsDataSource" EmptyDataText="There are no data records to display." onrowcommand="productsTable_RowCommand">
			<Columns>
				<asp:BoundField DataField="productName" HeaderText="Name" />
				<asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" />
				<asp:BoundField DataField="stock" HeaderText="Stock" SortExpression="stock" />
				<asp:BoundField DataField="productType" HeaderText="Type" SortExpression="productType" />
				<asp:BoundField DataField="creator" HeaderText="Created By" SortExpression="creator" />
				<asp:TemplateField>
					<ItemTemplate>
						<asp:Button ID="deleteButton" ButtonType="Button" CommandName="deleteProduct" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Delete" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<asp:AccessDataSource ID="productsDataSource" runat="server" DataFile="App_Data\main.accdb" DeleteCommand="DELETE FROM `products` WHERE `ID` = ?" InsertCommand="INSERT INTO `products` (`ID`, `productName`, `stock`, `price`, `displayName`, `productType`, `creator`) VALUES (?, ?, ?, ?, ?, ?, ?)" SelectCommand="SELECT productName, stock, price, displayName, productType, creator FROM products" UpdateCommand="UPDATE `products` SET `productName` = ?, `stock` = ?, `price` = ?, `displayName` = ?, `productType` = ?, `creator` = ? WHERE `ID` = ?">
			<DeleteParameters>
				<asp:Parameter Name="ID" Type="Int32" />
			</DeleteParameters>
			<InsertParameters>
				<asp:Parameter Name="ID" Type="Int32" />
				<asp:Parameter Name="productName" Type="String" />
				<asp:Parameter Name="stock" Type="Int32" />
				<asp:Parameter Name="price" Type="Decimal" />
				<asp:Parameter Name="displayName" Type="String" />
				<asp:Parameter Name="productType" Type="String" />
				<asp:Parameter Name="creator" Type="String" />
			</InsertParameters>
			<UpdateParameters>
				<asp:Parameter Name="productName" Type="String" />
				<asp:Parameter Name="stock" Type="Int32" />
				<asp:Parameter Name="price" Type="Decimal" />
				<asp:Parameter Name="displayName" Type="String" />
				<asp:Parameter Name="productType" Type="String" />
				<asp:Parameter Name="creator" Type="String" />
				<asp:Parameter Name="ID" Type="Int32" />
			</UpdateParameters>
		</asp:AccessDataSource>
		<asp:Label ID="returnLabel" runat="server"></asp:Label>
		<br />
		<br />
		Add Product<br />
		<br />
		<asp:TextBox ID="productNameBox" runat="server" placeholder="Product Name"></asp:TextBox>
		<br />
		<asp:TextBox ID="productPrice" runat="server" TextMode="Number" placeholder ="Product Price"></asp:TextBox>
		<br />
		Product Type:<br />
		<asp:DropDownList ID="typeDropdown" runat="server">
			<asp:ListItem Selected="True" Value="coaster">Coaster</asp:ListItem>
			<asp:ListItem Value="clock">Clock</asp:ListItem>
		</asp:DropDownList>
        <br />
		<asp:TextBox ID="bandBox" runat="server" placeholder="Band Name"></asp:TextBox>
		<br />
		<asp:TextBox ID="descriptionBox" runat="server" Rows="4" placeholder="Product Description"></asp:TextBox>
        <asp:FileUpload ID="imageUpload" runat="server" />
        <br />
        <p>
			<asp:Button ID="productAddButton" runat="server" Text="Add Product" OnClick="productAddButton_Click" />
			<asp:Label ID="returnMessage" runat="server" Text=""></asp:Label>
        </p>

    </div>
</asp:Content>
