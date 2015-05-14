<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prints.aspx.cs" Inherits="ArtGallery.Prints" %>

<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="SpiceLogicPayPalStd" Namespace="SpiceLogic.PayPalCtrlForWPS.Controls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http://jtruncate.googlecode.com/svn/trunk/jquery.jtruncate.js" type="text/javascript"></script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-xs-12 col-sm-4  col-md-6 col-lg-8">

            <asp:FormView ID="FormView1" runat="server"
                DataKeyNames="id"
                DataSourceID="odsPicture" OnPreRender="FormView1_PreRender">
                <ItemTemplate>
                    <asp:HiddenField runat="server" Value='<%# Eval("Metatags") %>' ID="metatags" />

                    <table cellpadding="2" cellspacing="2">
                        <tr>
                            <td align="left">Gallery:
                            </td>
                            <td>
                                <asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl='<%# "~/GalleryPage.aspx?id=" + Eval("galleryid").ToString() %>' Text='<%# Eval("Gallery") %>' />
                            </td>
                        </tr>



                        <tr>
                            <td align="left">Year:
                            </td>
                            <td>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>' />
                            </td>
                        </tr>

                        <tr>
                            <td align="left">Original Media:
                            </td>
                            <td>
                                <asp:Label ID="lblMedia" runat="server" Text='<%# Eval("Media") %>' />
                            </td>
                        </tr>

                        <tr runat="server" id="descrow" visible='<%# Eval("Description").ToString() != ""  %>'>
                            <td align="left" valign="top">Description:
                            </td>
                            <td>
                                <div id="description" >
                                    <%# Eval("Description") %>
                                </div>
                            </td>
                        </tr>




                    </table>
                </ItemTemplate>
            </asp:FormView>



            <br />

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                BorderStyle="None" 
                DataKeyNames="id" DataSourceID="odsReproduction"
                OnRowCommand="GridView1_RowCommand">
                <Columns>

                    <asp:CommandField ButtonType="Button" SelectText="Buy" ShowCancelButton="False"
                        ShowSelectButton="True" />

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <div style="text-align: left;">
                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("Description") %>' Width="100px"></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size">
                        <ItemTemplate>
                            <div style="text-align: right;">
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Height").ToString() + " x " + Eval("Width").ToString() %>' Width="85px"></asp:Label>&nbsp;&nbsp;
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" DataFormatString="{0:0.00}" />

                </Columns>
            </asp:GridView>

            <div style="text-align: center;" runat="server" id="divBack">
                <br />
                <a href='<%= "PicturePage.aspx?id=" + FormView1.DataKey.Value.ToString() %>'>Back to original art work</a>
            </div>

        </div>
        <div class="col-xs-12 col-sm-8  col-md-6 col-lg-4">
            <asp:FormView ID="FormView2" runat="server" DataSourceID="odsWaterMark">
                <ItemTemplate>
                     <div style="text-align:center;">
                    <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' Font-Size="X-Large" />
                         </div>
                    <br />
                    <a href="javascript:void(0)" style="text-decoration: none;">
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
                        </cc1:GeneratedImage></a><br />
                    <asp:Label runat="server" ID="lblCopyright" Text='<%#"&#64; " + Eval("copyrightholder").ToString() + " " +  Eval("Date").ToString() %>'></asp:Label>
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
    </div>
    <asp:ObjectDataSource ID="odsPicture" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetById" TypeName="ArtGallery.PictureDL">
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
