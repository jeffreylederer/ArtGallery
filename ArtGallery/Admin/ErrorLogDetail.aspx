<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorLogDetail.aspx.cs" Inherits="ArtGallery.Admin.ErrorLogDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetById" 
        TypeName="ArtGallery.ErrorLogDL">
        <SelectParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
        DataKeyNames="id" DataSourceID="ObjectDataSource1" Height="50px" Width="125px">
        <Fields>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="errormessage" HeaderText="Error Message" 
                SortExpression="errormessage" />
            <asp:BoundField DataField="lastupdated" HeaderText="Date" 
                SortExpression="lastupdated" />
        </Fields>
    </asp:DetailsView>
    <br /><a href="ErrorLog.aspx">Back to Error Log</a>
</asp:Content>
