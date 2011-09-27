﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prints.aspx.cs" Inherits="ArtGallery.Prints" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="SpiceLogicPayPalStd" Namespace="SpiceLogic.PayPalCtrlForWPS.Controls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <script src="scripts/jquery-1.4.1.js" type="text/javascript"></script>
   <script src="http://jtruncate.googlecode.com/svn/trunk/jquery.jtruncate.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">
<!--
      function pageLoad() {
          $().ready(function () {
              $('#description').jTruncate({
                  minTrail: 20
              });
          });
      }
-->
</script>

<asp:Panel runat="server" ID="pnlProcessing" Visible="false">
    <h1>Processing your order</h1>
    </asp:Panel>
<div class="standardtext"> 
    <asp:FormView ID="FormView1" runat="server"  
         DataKeyNames="id" 
        DataSourceID="odsPicture" onprerender="FormView1_PreRender">
        <ItemTemplate>
        <asp:HiddenField runat="server" Value='<%# Eval("Metatags") %>' ID="metatags" />
         
            <table cellpadding="2" cellspacing="2">
            <tr>
            <td align="left">
            Gallery:
            </td>
            <td>
            <asp:Hyperlink ID="Hyperlink1" runat="server" NavigateUrl='<%# "~/GalleryPage.aspx?id=" + Eval("galleryid").ToString() %>'  Text='<%# Eval("Gallery") %>' />
            </td>
            </tr>

                     
           
            <tr>
            <td  align="left">
            Year:
            </td>
            <td>
            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>' />
            </td>
            </tr>

            <tr>
            <td align="left">
            Original Media:
            </td>
            <td>
            <asp:Label ID="lblMedia" runat="server" Text='<%# Eval("Media") %>' />
            </td>
            </tr>

            <tr runat="server" id="descrow" visible='<%# Eval("Description").ToString() != ""  %>' >
            <td align="left"  valign="top">
            Description:
            </td>
            <td>
            <div id="description" style="width:300px;" >
            <%# Eval("Description") %>
            </div>
            </td>
            </tr>
            
                    
            

            </table>
            </ItemTemplate>
    </asp:FormView>
    

     
 <br />    
   <cc2:BuyNowButton ID="btnBuy" 
                        BusinessEmailOrMerchantID="ilene@magiceyegallery.com"
                        runat="server" 
                        ImageUrl="~/Images/dot.jpg"
                        Quantity="1" 
                        CurrencyCode="US_Dollar"
                        WeightUnit="Pounds"
                        GenerateEncryptedButton = "true"
                        AlternateText="Buy now via PayPal">
        <PayPalDisplayPage 
            ShippingAddress="ShippingAddressMust"/>

        <PayPalIPN 
            Custom_IPN_Url="~/PayPalNotification.aspx" 
            EnablePageLoadEventInIPNSession="True" />

         <paypalformsubmission 
                        postactionurl="https://www.paypal.com/cgi-bin/webscr"
                        postdestination="PayPal_Website" />

         <EncryptedButtonGeneration
                         CertificateId="XVERPTF7DGQXN"
                         PayPalCertPath="~/App_Data/paypal_cert_pem.txt"
                         PKCS12CertPath="~/App_Data/artgallery.p12"
                         PKCS12Password="Jeffrey17" />

        <PayPalReturn
                          Custom_CancelledReturnURL="~/DedicatedPayPalReturnHandler.aspx?sLPPCStatus=cancel"
                          Custom_CompletedReturnURL="~/DedicatedPayPalReturnHandler.aspx"
                          PDTAuthenticationToken="s2xIb5KB3iOEY9GLnpnmIRe3-uvwPEySuIHDdCeDjmxOyZWz1i-2wJKnpLu" />
    </cc2:BuyNowButton>
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="90%"
        DataKeyNames="id" DataSourceID="odsReproduction" 
        onrowcommand="GridView1_RowCommand">
        <Columns>
            <asp:CommandField ButtonType="Button" CausesValidation="False" 
                SelectText="Buy Now" ShowCancelButton="False" ShowSelectButton="True" />
            <asp:TemplateField HeaderText="Description" >
                <ItemTemplate>
                    <div style="text-align:left;" >
                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("Description") %>'  Width="100px"></asp:Label>
                </div>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Size" >
                <ItemTemplate>
                    <div style="text-align:right;" >
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Height").ToString() + " x " + Eval("Width").ToString() %>' Width="85px"></asp:Label>&nbsp;&nbsp;
                </div>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" DataFormatString="{0:0.00}" />
            
        </Columns>
    </asp:GridView>

     <div style="text-align:center;" runat="server" id="divBack">
             <br /><a href='<%= "PicturePage.aspx?id=" + FormView1.DataKey.Value.ToString() %>'>Back to original art work</a>
     </div>
    
</div>
<div class="picture">
         <asp:FormView ID="FormView2" runat="server" DataSourceID="odsWaterMark">
        <ItemTemplate>   

            <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' Font-Size="X-Large" /> <br />
            <cc1:GeneratedImage ID="WatermarkedImageGenerator" runat="server" ImageHandlerUrl="~/ImageHandlers/PageImageHandler.ashx">
                <Parameters>
                    <cc1:ImageParameter Name="ImageUrl" Value='<%#  "~/App_Data/" + Eval("PicturePath") %>' />
                    <cc1:ImageParameter Name="Height" Value="500" />
                    <cc1:ImageParameter Name="Width" Value="450" />
                    <cc1:ImageParameter Name="WatermarkText" Value='<%# Eval("WatermarkText") %>' />
                    <cc1:ImageParameter Name="WatermarkFontFamily" Value='<%# Eval("WatermarkFontFamily") %>' />
                    <cc1:ImageParameter Name="WatermarkFontColor" Value='<%# Eval("WatermarkFontColor") %>' />
                    <cc1:ImageParameter Name="WatermarkFontSize" Value='<%# Eval("WatermarkFontSize") %>' />
                 </Parameters>
              </cc1:GeneratedImage><br />
               <asp:Label runat="server" ID="lblCopyright" Text='<%#"&#64; " + Eval("copyrightholder").ToString() + " " +  Eval("Date").ToString() %>' ></asp:Label>
        </ItemTemplate>
    </asp:FormView>
         <asp:ObjectDataSource ID="odsWaterMark" runat="server" 
             OldValuesParameterFormatString="original_{0}" SelectMethod="GetWithWaterMark" 
             TypeName="ArtGallery.PictureDL">
             <SelectParameters>
                 <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
             </SelectParameters>
         </asp:ObjectDataSource>
</div>

<asp:ObjectDataSource ID="odsPicture" runat="server"  
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetById" TypeName="ArtGallery.PictureDL" >
      <SelectParameters>
         <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
     </SelectParameters>
    </asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsReproduction" runat="server"  
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetByPictureId" TypeName="ArtGallery.ReproductionDL">
         <SelectParameters>
            <asp:QueryStringParameter Name="pictureid" QueryStringField="id" Type="Int32" />
        </SelectParameters>  
    </asp:ObjectDataSource>
</asp:Content>
