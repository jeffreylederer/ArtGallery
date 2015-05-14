<%@ Page Title="" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="ReportPage.aspx.cs" Inherits="ArtGallery.Admin.ReportPage" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"  
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" Height="700"
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1050px">
        <LocalReport ReportPath="Admin\Picture.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
</rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
        TypeName="ArtGallery.PictureDL">
    </asp:ObjectDataSource>
</asp:Content>
