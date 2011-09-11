<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DedicatedPayPalReturnHandler.aspx.cs" Inherits="ArtGallery.DedicatedPayPalReturnHandler" %>
<%@ Register Assembly="SpiceLogicPayPalStd" Namespace="SpiceLogic.PayPalCtrlForWPS.Controls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <cc2:PayPalReturnHandler ID="btnBuy" runat="server" 
        onpaypal_returned="btnBuy_PayPal_Returned" 
        PDTAuthenticationToken="lOR2XbjyDyxCcVMMdxt4yLD6CaeRPMwCXsL0Y4kX5umFr9kQClSvVyX4yTa"></cc2:PayPalReturnHandler>
<asp:MultiView ID="MultiViewMyShop" runat="server" ActiveViewIndex="0" >
<asp:View ID="ViewPaymentCompleted" runat="server">
    <h1>Thank you for your purchase</h1>
    I hope you enjoy <i><asp:Label runat="server" ID="lblTitleSale"></asp:Label></i>. If you
    have any question, please contact me at <cc3:Email runat="server" ID="emai13" />.
    
    <p>You should receive an email from PayPal confirming your order.</p>          
            
    </asp:View>

<asp:View ID="ViewPaymentCancelled" runat="server">
                
             <h1>Sorry, you cancelled your order</h1>
             <asp:Panel runat="server" ID="pnlCancelledFramed" Visible="false">
             If the shipping and handling charges seem excessive, it is because we ship all
             framed pieces with StrongBox&#169; shipping containers to protect the art work. 
             You can request that the art work be shipped unframed. Directly above the Buy Now button
             there is a checkbox that requests the art work be shipped unframed.
             </asp:Panel>
             <asp:Panel runat="server" ID="pnlCancelUnframed" Visible="false">
             If you have any question about the art work, please feel free to contact the
             artist at <cc3:Email runat="server" ID="emai11" />.
             </asp:Panel>
             <p>
             If you live in the Pittsburgh, PA area, you can email 
             <cc3:Email runat="server" ID="emai12" /> me and arrange 
             to buy the art work locally without shipping or handling.
             </p>
            </asp:View>

</asp:MultiView>
</asp:Content>
