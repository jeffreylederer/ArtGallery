<%@ Page Title="" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="OrphanPictures.aspx.cs" Inherits="ArtGallery.Admin.OrphanPictures" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        Caption="Orphan files in Picture Directory" DataKeyNames="PicturePath" 
        DataSourceID="ObjectDataSource1" onrowcommand="GridView1_RowCommand">
        <Columns>
            <asp:ButtonField CommandName="Remove" Text="Remove" />
            <asp:BoundField DataField="PicturePath" HeaderText="Picture File" 
                ReadOnly="True" SortExpression="PicturePath" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetOrphanPicture" 
        TypeName="ArtGallery.PictureDL">
    </asp:ObjectDataSource>
</asp:Content>
