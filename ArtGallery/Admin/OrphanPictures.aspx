<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrphanPictures.aspx.cs" Inherits="ArtGallery.Admin.OrphanPictures" %>
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
        DeleteMethod="Delete" InsertMethod="Insert" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetOrphanPicture" 
        TypeName="ArtGallery.PictureDL" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_metatags" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Frame" Type="String" />
            <asp:Parameter Name="Width" Type="Single" />
            <asp:Parameter Name="Height" Type="Single" />
            <asp:Parameter Name="Date" Type="Int16" />
            <asp:Parameter Name="MetaTags" Type="String" />
            <asp:Parameter Name="Notes" Type="String" />
            <asp:Parameter Name="Media" Type="String" />
            <asp:Parameter Name="GalleryId" Type="Int32" />
            <asp:Parameter Name="PicturePath" Type="String" />
            <asp:Parameter Name="surface" Type="String" />
            <asp:Parameter Name="price" Type="Int16" />
            <asp:Parameter Name="weight" Type="Single" />
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="picturestateid" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Frame" Type="String" />
            <asp:Parameter Name="Width" Type="Single" />
            <asp:Parameter Name="Height" Type="Single" />
            <asp:Parameter Name="Date" Type="Int16" />
            <asp:Parameter Name="MetaTags" Type="String" />
            <asp:Parameter Name="Notes" Type="String" />
            <asp:Parameter Name="Media" Type="String" />
            <asp:Parameter Name="GalleryId" Type="Int32" />
            <asp:Parameter Name="PicturePath" Type="String" />
            <asp:Parameter Name="surface" Type="String" />
            <asp:Parameter Name="price" Type="Int16" />
            <asp:Parameter Name="weight" Type="Single" />
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="picturestateid" Type="Int32" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_metatags" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
