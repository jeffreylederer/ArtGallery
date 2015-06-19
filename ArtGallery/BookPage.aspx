<%@ Page Title="Book" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableViewState="true"
    Inherits="ArtGallery.BookPage" CodeBehind="BookPage.aspx.cs" %>

<%@ OutputCache Duration="100" VaryByParam="id" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="SpiceLogicPayPalStd" Namespace="SpiceLogic.PayPalCtrlForWPS.Controls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12 col-sm-4  col-md-6 col-lg-8">
                    <asp:FormView ID="FormView1" runat="server" DataKeyNames="id,lastupdated"
                        OnPreRender="FormView1_PreRender">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" Value='<%# Eval("Metatags") %>' ID="metatags" />
                             <div class="table-responsive">
                                <table cellpadding="2" cellspacing="2">
                                    
                                    <tr>
                                        <td align="left">Title:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("title") %>' />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left">Style:
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("style") %>' />
                                        </td>
                                    </tr>
                                
                                    <tr runat="server" id="descrow" visible='<%# Eval("Description").ToString() != ""  %>'>
                                        <td align="left" valign="top">Description:
                                        </td>
                                        <td>
                                            <div id="description">
                                                <%# Eval("Description") %>
                                            </div>
                                        </td>
                                    </tr>

                                     <tr>
                                        <td align="left">Size:
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Width") +" x " + Eval("Height") +" inches" %>' />
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td align="left">Price:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price", "{0:0.00}") %>' />
                                        </td>
                                    </tr>

                                </table>
                                <br />
                                <asp:Button runat="server" ID="btnBuy" Text="Buy" OnClick="btnBuy_Click" />
                            </div>


                        </ItemTemplate>
                    </asp:FormView>
                    <%--<asp:LinkButton ID="btnInside" runat="server">See inside the book</asp:LinkButton>
                --%></div>

                <div class="col-xs-12 col-sm-8  col-md-6 col-lg-4">
                    <asp:FormView ID="FormView2" runat="server" OnPreRender="FormView2_PreRender">
                        <ItemTemplate>
                            <div style="text-align: center;">
                                <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' Font-Size="X-Large" />
                            </div>
                            <br />
                            <table> 
                                <tr>
                                    <td class="leftarrow">
                                        <asp:ImageButton runat="server" ID="btnPrevious" ImageUrl="~/Images/left.jpg"
                                            BorderStyle="None" OnClick="btnPrevious_Click" Height="20px" Width="20px" /></td>
                                    <td class="picture">
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
                                            </cc1:GeneratedImage>
                                        </a>

                                    </td>
                                     <td class="rightarrow">
                                        <asp:ImageButton runat="server" ID="btnNext" ImageUrl="~/Images/right.jpg"
                                            BorderStyle="None" OnClick="btnNext_Click" Height="20px" Width="20px" /></td>


                                </tr>
                            </table>
                            <div class="copyright">
                                <asp:Label runat="server" ID="Label1" Text='<%#"&#169; " +Eval("Date").ToString() + " " + Eval("copyrightholder").ToString() %>'></asp:Label>
                            </div>


                        </ItemTemplate>
                    </asp:FormView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



