﻿<%@ Page Title="Inside the Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsideBookPage.aspx.cs" Inherits="ArtGallery.InsideBookPage" %>

<%@ OutputCache Duration="1" VaryByParam="id" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="text-align: center;">
        <asp:FormView runat="server" ID="frmTop" DataKeyNames="id"
            DataSourceID="ObjectDataSource2" OnPreRender="frmTop_PreRender" Width="100%">
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="lblTitle" CssClass="heading" Text='<%# Eval("booktitle") %>' NavigateUrl='<%# "BookPage.aspx?id=" + Eval("id").ToString() %>' />
            </ItemTemplate>
        </asp:FormView>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetById"
        TypeName="ArtGallery.BookDL">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="id" QueryStringField="id"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ListView runat="server" ID="ListView1" GroupItemCount="6" DataKeyNames="id"
        DataSourceID="ObjectDataSource1" OnPreRender="ListView1_PreRender">

        <LayoutTemplate>
            <div class="table-responsive">
                <table cellpadding="2" runat="server" id="tblProducts" class="table">

                    <tr runat="server" id="groupPlaceholder">
                    </tr>
                </table>
            </div>
            <asp:DataPager runat="server" ID="dataPager"
                PageSize="30">
                <Fields>
                    <asp:NumericPagerField ButtonCount="6"
                        PreviousPageText="<--"
                        NextPageText="-->" />
                </Fields>
            </asp:DataPager>
        </LayoutTemplate>
        <GroupTemplate>

            <tr runat="server" id="productRow"
                style="height: 80px">
                <td runat="server" id="itemPlaceholder"></td>
            </tr>
        </GroupTemplate>

        <ItemTemplate>
            <td runat="server" valign="top">

                <asp:HyperLink runat="server" ID="hyp1" NavigateUrl='<%# "~/BookViewPage.aspx?id=" + Eval("id").ToString() %>' ForeColor="Transparent">
                    <cc1:GeneratedImage ID="GeneratedImage1" runat="server" ImageHandlerUrl="~/ImageHandlers/ThumbnailImageHandler.ashx">
                        <Parameters>
                            <cc1:ImageParameter Name="ImageUrl" Value='<%# "~/App_Data/" + Eval("PicturePath").ToString() %>' />
                            <cc1:ImageParameter Name="Height" Value="50" />
                        </Parameters>
                    </cc1:GeneratedImage>
                    <br />
                    <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' CssClass="title" Width="150px" ForeColor="Blue" />
                </asp:HyperLink></td>
        </ItemTemplate>
    </asp:ListView>
    <asp:LinkButton runat="server"
        ID="btnViewAll" Text="View All" OnClick="btnViewAll_Click" />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetByBookIdPublic"
        TypeName="ArtGallery.BookPageDL" >
        <SelectParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
