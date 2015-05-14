<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" Inherits="ArtGallery.SelectGallery" Codebehind="SelectGallery.aspx.cs" %>
<%@ OutputCache Duration="60" VaryByParam="id" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
            <div style="text-align:center;">
 <asp:FormView runat="server" ID="frmTop" DataKeyNames="id" DefaultMode="ReadOnly"
        DataSourceID="ObjectDataSource2" onprerender="frmTop_PreRender" Width="100%">
        <ItemTemplate>
              <asp:Label runat="server" ID="lblTitle" CssClass="heading" Text='<%# Eval("gallerytitle") %>' ></asp:Label>
        </ItemTemplate>
    </asp:FormView>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetById" 
        TypeName="ArtGallery.GalleryDL" >
         <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="id" QueryStringField="id" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
   <asp:ListView runat="server" ID="ListView1" GroupItemCount="6" DataKeyNames="id" 
        DataSourceID="ObjectDataSource1">
     
      <LayoutTemplate>
        <table cellpadding="2" runat="server"
               id="tblProducts">
            <tr runat="server" id="groupPlaceholder">
          </tr>
        </table>
    
      </LayoutTemplate>
      <GroupTemplate>
        <tr runat="server" id="productRow"
            style="height:80px">
          <td runat="server" id="itemPlaceholder">
          </td>
        </tr>
      </GroupTemplate>

      <ItemTemplate>
         <td runat="server" valign="top">
         <asp:HyperLink runat="server" ID="hyp1" NavigateUrl='<%# "EditPicture.aspx?id=" + Eval("id").ToString() %>' ForeColor="Transparent" >
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

