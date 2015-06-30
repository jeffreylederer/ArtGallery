<%@ Page Title="Edit Book Page" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="EditBookPage.aspx.cs" Inherits="ArtGallery.DataLayer.EditBookPage" %>

<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        <asp:Label runat="server" ID="lblBook" /></h3>
    Picture File:
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button runat="server" ID="UploadButton" Text="Upload"
        OnClick="UploadButton_Click" ValidationGroup="none" />
    <br />
    <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <div style="text-align: center;">

                            <asp:ValidationSummary runat="server" ID="vs" />

                            <asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" />
                            <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1"
                                DefaultMode="Edit" Width="100%"
                                DataKeyNames="id,lastupdated,bookid">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td>

                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td align="right">Title<cc3:Popup runat="server" ID="puTitle" Key="PictureTitle" />:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'
                                                                Width="300px" Rows="2" TextMode="MultiLine" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="lblTitle" ErrorMessage="Title is required">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                ControlToValidate="lblTitle" ErrorMessage="Title must be 300 character or less"
                                                                ValidationExpression=".{1,300}">*</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>



                                                    <tr>
                                                        <td align="right">Picture<br />
                                                            File Name<cc3:Popup runat="server" ID="puPictureFile" Key="PictureFile" />:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblPicturePath" runat="server" Text='<%# Bind("PicturePath") %>'
                                                                Width="300px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                ControlToValidate="lblPicturePath" ErrorMessage="Picture File is required">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                                ControlToValidate="lblPicturePath" ErrorMessage="Picture File must be 256 character or less"
                                                                ValidationExpression=".{1,256}">*</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="right">Active:</td>
                                                        <td>
                                                            <asp:CheckBox runat="server" ID="chkActive" Checked='<%# Bind("Active") %>' />
                                                        </td>

                                                        <tr>
                                                            <td colspan="2" align="center">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True"
                                                                    CommandName="Update" Text="Update" /><br />
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "EditBook.aspx?id=" + Eval("bookid").ToString() %>'>Return to Edit Book page</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                </table>
                                </EditItemTemplate>
                            </asp:FormView>
                    </td>

                    <td>
                        <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource1"
                            DataKeyNames="id" Width="100%">
                            <ItemTemplate>

                                <table>
                                    <tr>
                                        <td class="leftarrow">
                                            <asp:ImageButton runat="server" AlternateText="Previous" ID="btnPrevious" ImageUrl="~/Images/left.jpg" Height="20px" Width="20px" OnCommand="btnPrevious_Command" />
                                        </td>
                                        <td class="picture">
                                            <cc1:GeneratedImage ID="WatermarkedImageGenerator" runat="server" ImageHandlerUrl="~/ImageHandlers/PageImageHandler.ashx">
                                                <Parameters>
                                                    <cc1:ImageParameter Name="ImageUrl" Value='<%#  "~/App_Data/" + Eval("PicturePath") %>' />
                                                    <cc1:ImageParameter Name="Height" Value="450" />
                                                    <cc1:ImageParameter Name="Width" Value="450" />
                                                    <cc1:ImageParameter Name="ActualHeight" Value='10' />
                                                    <cc1:ImageParameter Name="ActualWidth" Value='8' />
                                                </Parameters>
                                            </cc1:GeneratedImage>
                                        </td>
                                        </td>
                                    <td class="rightarrow">
                                        <asp:ImageButton runat="server" ID="btnNext" ImageUrl="~/Images/right.jpg"
                                            BorderStyle="None" OnClick="btnNext_Click" Height="20px" Width="20px" /></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>

            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" UpdateMethod="Update" OldValuesParameterFormatString="original_{0}" SelectMethod="GetById" TypeName="ArtGallery.BookPageDL" OnSelected="ObjectDataSource1_Selected" OnUpdated="ObjectDataSource1_Updated" OnUpdating="ObjectDataSource1_Updating">
        <UpdateParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
            <asp:Parameter Name="PicturePath" Type="String" />
            <asp:Parameter Name="title" Type="String" />
            <asp:Parameter Name="active" Type="Boolean" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="id" Type="Int32" QueryStringField="id" DefaultValue="0" />
        </SelectParameters>

    </asp:ObjectDataSource>

</asp:Content>
