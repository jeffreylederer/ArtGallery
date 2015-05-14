<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookViewPage.aspx.cs" Inherits="ArtGallery.BookViewPage" %>
<%@ OutputCache Duration="100" VaryByParam="id" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="FormView2" runat="server" OnPreRender="FormView2_PreRender">
        <ItemTemplate>
            <div style="text-align: center;">
                <asp:HyperLink runat="server" ID="hylView" Text='<%# "All Inside Pages for " + Eval("BookTitle").ToString() %>' Font-Size="X-Large" NavigateUrl='<%# "BookPage.aspx?id=" + Eval("bookid").ToString() %>' /><br />
                <asp:HyperLink runat="server" ID="HyperLink1" Text='<%# "Back to " + Eval("BookTitle").ToString() %>' Font-Size="X-Large" NavigateUrl='<%# "Book.aspx?id=" + Eval("bookid").ToString() %>' />
            </div>
            <br />
            <table>
                <tr>
                    <td class="leftarrow">
                        <asp:ImageButton runat="server" ID="btnPrevious" ImageUrl="~/Images/left.jpg"
                            BorderStyle="None" OnClick="btnPrevious_Click" Height="20px" Width="20px" /></td>
                    <td class="picture">

                        <a href="javascript:void(0)" style="text-decoration: none;">
                            <cc1:generatedimage id="WatermarkedImageGenerator" runat="server" imagehandlerurl="~/ImageHandlers/PageImageHandler.ashx" bordercolor="White">
                                                <Parameters>
                                                    <cc1:ImageParameter Name="ImageUrl" Value='<%#  "~/App_Data/" + Eval("PicturePath") %>' />
                                                    <cc1:ImageParameter Name="Height" Value="500" />
                                                    <cc1:ImageParameter Name="Width" Value="450" />
                                                    <cc1:ImageParameter Name="WatermarkText" Value='<%# Eval("WatermarkText") %>' />
                                                    <cc1:ImageParameter Name="WatermarkFontFamily" Value='<%# Eval("WatermarkFontFamily") %>' />
                                                    <cc1:ImageParameter Name="WatermarkFontColor" Value='<%# Eval("WatermarkFontColor") %>' />
                                                    <cc1:ImageParameter Name="WatermarkFontSize" Value='<%# Eval("WatermarkFontSize") %>' />
                                                </Parameters>
                                            </cc1:generatedimage>
                        </a>

                    </td>
                    <td class="rightarrow">
                        <asp:ImageButton runat="server" ID="btnNext" ImageUrl="~/Images/right.jpg"
                            BorderStyle="None" OnClick="btnNext_Click" Height="20px" Width="20px" /></td>


                </tr>
            </table>
            <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' />
            <div class="copyright">
                <asp:Label runat="server" ID="Label1" Text='<%#"&#169; " +Eval("Date").ToString() + " " + Eval("copyrightholder").ToString()%>'></asp:Label>
            </div>


        </ItemTemplate>
    </asp:FormView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
