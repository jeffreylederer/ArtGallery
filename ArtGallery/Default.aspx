<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="ArtGallery.DefaultPage" EnableViewState="false" CodeBehind="Default.aspx.cs" %>

<%@ OutputCache Duration="1" VaryByParam="id" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="scripts/site.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-xs-12 col-sm-6  col-md-8 ">
            <p class="text-left" style="text-wrap: normal;">
                <%= HomePageText %>
            </p>
        </div>



        <div class="col-xs-12 col-sm-6 col-md-4 ">
            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <asp:FormView runat="server" ID="frmTop">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="hyp1" CssClass="default" NavigateUrl='<%# "~/PicturePage.aspx?id=" + Eval("id").ToString() %>' ForeColor="White">
                                <cc1:GeneratedImage ID="WatermarkedImageGenerator" runat="server" ImageHandlerUrl="~/ImageHandlers/PageImageHandler.ashx" BorderColor="White">
                                    <Parameters>
                                        <cc1:ImageParameter Name="ImageUrl" Value='<%#  "~/App_Data/" + Eval("PicturePath") %>' />
                                        <cc1:ImageParameter Name="Height" Value="500" />
                                        <cc1:ImageParameter Name="Width" Value="500" />
                                        <cc1:ImageParameter Name="WatermarkText" Value='<%# Eval("WatermarkText") %>' />
                                        <cc1:ImageParameter Name="WatermarkFontFamily" Value='<%# Eval("WatermarkFontFamily") %>' />
                                        <cc1:ImageParameter Name="WatermarkFontColor" Value='<%# Eval("WatermarkFontColor") %>' />
                                        <cc1:ImageParameter Name="WatermarkFontSize" Value='<%# Eval("WatermarkFontSize") %>' />
                                    </Parameters>
                                </cc1:GeneratedImage>
                            </asp:HyperLink>

                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Timer ID="Timer1" Interval="4000" runat="server" OnTick="Timer1_Tick" />
</asp:Content>
