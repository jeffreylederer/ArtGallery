<%@ Page Title="Search Results" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="ArtGallery.SearchResult" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <script src="scripts/site.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="text-align:center;" >
 <asp:Label runat="server" ID="lblSearch" />
 </div>
  <asp:ListView runat="server" ID="ListView1" GroupItemCount="6" DataKeyNames="id" >
     
      <LayoutTemplate>
        <table cellpadding="2" runat="server"
               id="tblProducts">
           <tr runat="server" id="groupPlaceholder">
          </tr>
        </table>

        <asp:DataPager runat="server" ID="DataPager"
                       PageSize="30">
           <Fields>
             <asp:NumericPagerField ButtonCount="3"
                  PreviousPageText="<--"
                  NextPageText="-->" />
           </Fields>
        </asp:DataPager>
      </LayoutTemplate>
      <GroupTemplate>

        <tr runat="server" id="productRow"
            style="height:80px">
          <td runat="server" id="itemPlaceholder">
          </td>
        </tr>
      </GroupTemplate>

     <ItemTemplate>
         <td id="Td1" runat="server" valign="top">
        
         <asp:HyperLink runat="server" ID="hyp1" NavigateUrl='<%# "~/SearchPicturePage.aspx?id=" + Eval("id").ToString() %>' ForeColor="Transparent" >
         <cc1:GeneratedImage ID="GeneratedImage1" runat="server" ImageHandlerUrl="~/ImageHandlers/ThumbnailImageHandler.ashx">
                <Parameters>
                    <cc1:ImageParameter Name="ImageUrl" Value='<%# "~/App_Data/" + Eval("PicturePath").ToString() %>' />
                    <cc1:ImageParameter Name="Height" Value="50" />
                </Parameters>
          </cc1:GeneratedImage>
        <br />
         <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' CssClass="title" Width="150px" ForeColor="Blue"/>
         </asp:HyperLink></td></ItemTemplate></asp:ListView><asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetByGalleryId" 
        TypeName="ArtGallery.PictureDL">
        <SelectParameters>
            <asp:QueryStringParameter Name="Galleryid" QueryStringField="id" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
