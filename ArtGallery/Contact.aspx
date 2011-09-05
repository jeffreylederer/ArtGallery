<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ArtGallery.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ObjectDataSource ID="odsArtist" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetArtist" 
            TypeName="ArtGallery.SiteDL"/>

            <asp:FormView ID="FormView2" runat="server" DataSourceID="odsArtist" Width="100%" 
            DefaultMode="ReadOnly">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblContact" Text='<%# Eval("contact") %>' width="100%" />
            </ItemTemplate>
            </asp:FormView>
            

</asp:Content>
