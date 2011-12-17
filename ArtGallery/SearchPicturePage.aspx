﻿<%@ Page Title="Search Result Picture" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  EnableViewState="true" 
 Inherits="ArtGallery.SearchPicturePage"  Codebehind="SearchPicturePage.aspx.cs" %>
<%@ OutputCache Duration="100" VaryByParam="id" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="SpiceLogicPayPalStd" Namespace="SpiceLogic.PayPalCtrlForWPS.Controls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="http://jtruncate.googlecode.com/svn/trunk/jquery.jtruncate.js" type="text/javascript"></script> 
    <script src="http://jtruncate.googlecode.com/svn/trunk/jquery.jtruncate.js" type="text/javascript"></script> 
    <script src="scripts/site.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

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
  <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
    <ContentTemplate>
 <div class="standardtext">
     <asp:FormView ID="FormView1" runat="server" 
        DataKeyNames="id" onprerender="FormView1_PreRender">
        <ItemTemplate>
       <asp:HiddenField runat="server" Value='<%# Eval("Metatags") %>' ID="metatags" />
       <asp:HiddenField runat="server" Value='<%# Eval("Available") %>' ID="Available" /> 
       <asp:HiddenField runat="server" Value='<%# Eval("id") %>' ID="ID" /> 
            <table cellpadding="2" cellspacing="2">
           <table cellpadding="2" cellspacing="2">
            <tr>
            <td align="left">
            Gallery:
            </td>
            <td>
            <asp:Hyperlink ID="Hyperlink2" runat="server" NavigateUrl='<%# "~/GalleryPage.aspx?id=" + Eval("galleryid").ToString() %>'  Text='<%# Eval("Gallery") %>' />
            </td>
            </tr>

            <tr>
            <td align="left">
            Frame:
            </td>
            <td>
            <asp:Label ID="lblFrame" runat="server" Text='<%# Eval("Frame") %>' />
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
            Media:
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
                
            <tr>     
            <td align="left">
            Dimensions:
            </td>
            <td>
            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Height").ToString() + " x " + Eval("Width").ToString() %>' />
            </td>
            </tr>

            <tr>
            <td align="left">
            Price:
            </td>
            <td>
            <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price", "{0:0.00}") %>' />
            <asp:Label ID="lblSold" runat="server" Text="Original is not available" />
            </td>
            </tr>

           
            <tr runat="server" id="tdPrints">
            <td colspan="2" align="left" >
                
                 <br /><asp:Label runat="server" ID="lblPrints" />  are available <asp:HyperLink runat="server" ID="hyPrints" NavigateUrl='<%# "Prints.aspx?id="+ Eval("id").ToString() %>'>here</asp:HyperLink>.
            </td>
            </tr>

            <tr>
            <td colspan="2" align="center">
            <br /><asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/SearchResult.aspx?list=1" Text="Back to Search Results" />
            </td>
            </tr>
            </table>
            <br />
                 <asp:Button runat="server" ID="btnBuy" Text="Buy" OnClick="btnBuy_Click"/>
            </ItemTemplate>
    </asp:FormView>
   
</div>
 <div class="picture">
         <asp:FormView ID="FormView2" runat="server">
        <ItemTemplate>   

            <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' Font-Size="X-Large" /> <br />
            <table>
            <tr>
            <td class="leftarrow"><asp:ImageButton runat="server" ID="btnPrevious" ImageUrl="~/Images/left.jpg" 
                    BorderStyle="None"  OnClick="btnPrevious_Click" Height="20px" Width="20px" /></td>
            <td class="picture">
            <a href="javascript:void(0)" style="text-decoration:none;">
            <cc1:GeneratedImage ID="WatermarkedImageGenerator" runat="server" ImageHandlerUrl="~/ImageHandlers/PageImageHandler.ashx" BorderColor="White">
                <Parameters>
                    <cc1:ImageParameter Name="ImageUrl" Value='<%#  "~/App_Data/" + Eval("PicturePath") %>' />
                    <cc1:ImageParameter Name="Height" Value="500" />
                    <cc1:ImageParameter Name="Width" Value="450" />
                    <cc1:ImageParameter Name="WatermarkText" Value='<%# Eval("WatermarkText") %>' />
                    <cc1:ImageParameter Name="WatermarkFontFamily" Value='<%# Eval("WatermarkFontFamily") %>' />
                    <cc1:ImageParameter Name="WatermarkFontColor" Value='<%# Eval("WatermarkFontColor") %>' />
                    <cc1:ImageParameter Name="WatermarkFontSize" Value='<%# Eval("WatermarkFontSize") %>' />
                 </Parameters>
              </cc1:GeneratedImage>
              </a>
               </td>
                <td class="rightarrow"> <asp:ImageButton runat="server" ID="btnNext" ImageUrl="~/Images/right.jpg" 
                    BorderStyle="None"  OnClick="btnNext_Click" Height="20px" Width="20px" /></td>


               </tr>
               </table>
                <div class="copyright">
               <asp:Label runat="server" ID="Label1" Text='<%#"&#169; " +Eval("Date").ToString() + " " + Eval("copyrightholder").ToString()%>' ></asp:Label>
               </div>
       
        </ItemTemplate>
    </asp:FormView>
</div> 
    </ContentTemplate>
    </asp:UpdatePanel>

      
      
</asp:Content>

