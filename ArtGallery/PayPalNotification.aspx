<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayPalNotification.aspx.cs" Inherits="ArtGallery.PayPalNotification" %>
<%@ Register Assembly="SpiceLogicPayPalStd" Namespace="SpiceLogic.PayPalCtrlForWPS.Controls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <cc2:IPNHandler runat="server" ID="btnHandler" 
    onipn_exception="btnHandler_IPN_Exception" 
    onipn_notified="btnHandler_IPN_Notified" />
</asp:Content>
