<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsideBook.ascx.cs" Inherits="ArtGallery.AppCode.InsideBook" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<asp:Button runat="server" ID="HiddenButton" Visible="false" />
<ajax:ModalPopupExtender ID="MPE" runat="server"
    TargetControlID="HiddenButton"
    PopupControlID="pnlPopup"
    DropShadow="true"
    CancelControlID="CancelButton" />

<asp:Panel runat="server" ID="pnlPopup" Width="750" Height="500">

    <asp:UpdatePanel runat="server" ID="upTitle" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr style="background-color: beige;">
                    <td>
                        <span style="text-align: left; font-size: medium; font-weight: 700">
                            <asp:Label runat="server" ID="lblTitle" /></span></td>
                    <td style="text-align: right">
                        <asp:Button runat="server" OnClick="Click_CancelButton" ID="CancelButton" Text="X" Font-Size="Smaller" Width="25px" Height="25px" /></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>


    <table>
        <tr>
            <td style="width: 425px;">

                <asp:FormView runat="server" DataSourceID="odsSingleImage" ID="frmSingleImage" DataKeyNames="id">
                    <ItemTemplate>
                        <cc1:GeneratedImage ID="WatermarkedImageGenerator" runat="server" ImageHandlerUrl="~/ImageHandlers/PageImageHandler.ashx" BorderColor="White">
                            <Parameters>
                                <cc1:ImageParameter Name="ImageUrl" Value='<%#  "~/App_Data/" + Eval("PicturePath") %>' />
                                <cc1:ImageParameter Name="Height" Value="400" />
                                <cc1:ImageParameter Name="Width" Value="400" />
                                <cc1:ImageParameter Name="WatermarkText" Value='<%# Eval("WatermarkText") %>' />
                                <cc1:ImageParameter Name="WatermarkFontFamily" Value='<%# Eval("WatermarkFontFamily") %>' />
                                <cc1:ImageParameter Name="WatermarkFontColor" Value='<%# Eval("WatermarkFontColor") %>' />
                                <cc1:ImageParameter Name="WatermarkFontSize" Value='<%# Eval("WatermarkFontSize") %>' />
                            </Parameters>
                        </cc1:GeneratedImage>
                    </ItemTemplate>
                </asp:FormView>
            </td>

            <td style="width: 325px;">
                <asp:ListView runat="server" ID="ListView1"  GroupItemCount="4"
                    GroupPlaceholderID="groupPlaceholder" ItemPlaceholderID="itemPlaceholder"
                    DataSourceID="odsList" DataKeyNames="id" OnSelectedIndexChanged="ListView1_SelectedIndexChanged">

                    <LayoutTemplate>
                        <table cellpadding="2" width="300px" runat="server" id="tblProducts">
                            <tr runat="server" id="groupPlaceholder" />
                        </table>
                    </LayoutTemplate>


                    <GroupTemplate>
                        <tr runat="server" id="ProductsRow">
                            <td runat="server" id="itemPlaceholder" />
                        </tr>
                    </GroupTemplate>



                    <EmptyDataTemplate>
                        <table style="">
                            <tr>
                                <td>No Inside Pages</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>

                    <SelectedItemTemplate>
                            <asp:ImageButton runat="server" ImageUrl='<% "~/App_Data/" + Eval("PicturePath") %>'
                                CommandArgument='<% Eval("id") %>' CommandName="Select"
                                Width="75px" OnClick="ImageButton1_Click" BorderStyle="None" />
                    </SelectedItemTemplate>
                </asp:ListView>

            </td>
        </tr>

    </table>


</asp:Panel>

<asp:ObjectDataSource ID="odsSingleImage" runat="server" OldValuesParameterFormatString="original_{0}" OnSelecting="odsSingleImage_Selecting"
    SelectMethod="GetWithWaterMark"
    TypeName="ArtGallery.BookPageDL">
    <SelectParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsList" runat="server"
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetByBookIdPublic" TypeName="ArtGallery.BookPageDL">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="0" Name="bookid" QueryStringField="bookid" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

