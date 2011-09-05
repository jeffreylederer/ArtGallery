<%@ Page Title="Remove Administrator" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RemoveUser.aspx.cs" Inherits="ArtGallery.Account.RemoveUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
<ContentTemplate>

<asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="False"  DataKeyNames="username"
        onrowcommand="GridView1_RowCommand" EmptyDataText="No Administrator Users (except you)">
    <Columns>
        <asp:CommandField SelectText="Remove User" ShowSelectButton="True" />
        <asp:BoundField DataField="username" HeaderText="User Name" />
    </Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
