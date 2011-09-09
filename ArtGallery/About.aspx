<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="About" EnableViewState="false" Codebehind="About.aspx.cs" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1">
             <ItemTemplate>
             <div class="aboutpicture">
             <cc1:GeneratedImage ID="WatermarkedImageGenerator" runat="server" ImageHandlerUrl="~/ImageHandlers/PageImageHandler.ashx">
                <Parameters>
                    <cc1:ImageParameter Name="ImageUrl" Value='<%#  "~/Images/" + Eval("ArtistImagePath") %>' />
                    <cc1:ImageParameter Name="Height" Value="500" />
                    <cc1:ImageParameter Name="Wdith" Value="500" />
                 </Parameters>
              </cc1:GeneratedImage>
              <div class="spacer"></div>
             </div>
            <div class="aboutstandardtext">
             <asp:Label ID="HomePageTextLabel" runat="server" 
                    Text='<%# Bind("ArtistPageText") %>' />
            </div>
            
             
             </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
            TypeName="ArtGallery.SiteDL" >
         </asp:ObjectDataSource>
</asp:Content>
