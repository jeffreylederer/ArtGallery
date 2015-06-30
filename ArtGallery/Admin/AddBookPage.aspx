<%@ Page Title="Add Book Page" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddBookPage.aspx.cs" Inherits="ArtGallery.Admin.AddBookPage" %>

<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Picture File:
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button runat="server" ID="UploadButton" Text="Upload"
        OnClick="UploadButton_Click" ValidationGroup="none" />
    <br />
    <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
        <ContentTemplate>


            <asp:ValidationSummary runat="server" ID="vs" />

            <asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" />
            <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1"
                DefaultMode="Insert" Width="100%" 
                OnItemInserted="FormView1_ItemInserted">
                <InsertItemTemplate>
                    <table>
                        <tr>
                            <td>

                                <table>
                                    <tr>
                                        <td style="text-align:right;">Title<cc3:Popup runat="server" ID="puTitle" Key="PictureTitle" />:
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
                                        <td style="text-align:right;">Picture<br />File Name<cc3:Popup runat="server" ID="puPictureFile" Key="PictureFile" />:
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
                                        <td style="text-align:right;">Active:</td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkActive" Checked='<%# Bind("Active") %>' />
                                        </td>

                                        <tr>
                                            <td colspan="2" style="text-align:center;">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True"
                                                    CommandName="Insert" Text="Insert" />
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server"
                                                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                            </td>
                                        </tr>
                                </table>
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetById" TypeName="ArtGallery.BookPageDL">
        <InsertParameters>
            <asp:QueryStringParameter Name="bookid" Type="Int32" QueryStringField="id" DefaultValue="0" />
            <asp:Parameter Name="PicturePath" Type="String" />
            <asp:Parameter Name="title" Type="String" />
            <asp:Parameter Name="active" Type="Boolean" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
