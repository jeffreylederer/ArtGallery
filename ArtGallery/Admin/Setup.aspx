<%@ Page Title="Basic Site Setup" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="ArtGallery.Admin.Setup" ValidateRequest="false" %>

<%@ Register Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <cc:TPMSObjectDataSource ID="odsSite" runat="server" ErrorLabelID="ErrorLabel"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetSite"
        TypeName="ArtGallery.SiteDL" UpdateMethod="UpdateSite"
        IsFormView="true">
        <UpdateParameters>
            <asp:Parameter Name="LogoPath" Type="String" />
            <asp:Parameter Name="HomePageText" Type="String" />
            <asp:Parameter Name="Metatags" Type="String" />
            <asp:Parameter Name="copyrightholder" Type="String" />
        </UpdateParameters>
    </cc:TPMSObjectDataSource>

    <div class="alert alert-success"  style="width:400px;">
        Upload Logo File:
        <asp:FileUpload ID="FileUploadLogo" runat="server" />
        <asp:Button runat="server" ID="UploadLogoButton" Text="Upload" class="btn btn-primary"
            OnClick="UploadLogoButton_Click" ValidationGroup="none" />
    </div>
    <div class="alert alert-warning"  style="width:400px;">
        Upload file to Images directory:
        <asp:FileUpload ID="FileUploadImages" runat="server" />
        <asp:Button runat="server" ID="Button1" Text="Upload" class="btn btn-primary"
            OnClick="UploadImagesButton_Click" ValidationGroup="none" />
    </div>


    <asp:UpdatePanel runat="server" ID="upPayPal" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="site" />
            <asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="False" />
            <cc:TPMSFormView ID="FormView1" runat="server" DataSourceID="odsSite" Width="100%" UpdatePanelID="upPayPal" CssClass="form-stacked"
                DefaultMode="Edit" InsertOrUpdateCheckField="None">
                <EditItemTemplate>
                    <table>

                        <tr>
                            <td align="right">Logo File Name:
                            </td>
                            <td>
                                <asp:TextBox ID="LogoPathTextBox" runat="server" CssClass="form-control"
                                    Text='<%# Bind("LogoPath") %>' Width="300px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="site"
                                    ControlToValidate="LogoPathTextBox" ErrorMessage="Logo Path is required">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="LogoPathTextBox"
                                    ErrorMessage="Logo Path must be 100 characters or less"
                                    ValidationExpression=".{0,100}" ValidationGroup="Site">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>

                        <tr>
                            <td align="right">Home Page Metatags:</td>
                            <td>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtMetaTags" Text='<%# Bind("metatags") %>' TextMode="MultiLine" Rows="5" Width="450px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right">Copyright holder:</td>
                            <td style="text-align:left;">
                                <asp:TextBox runat="server" ID="txtCopyrightholder" Text='<%# Bind("copyrightholder") %>' Width="300px" CssClass="form-control"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="site"
                                    ControlToValidate="txtCopyrightholder" ErrorMessage="Copyright holder is required">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                    ControlToValidate="txtCopyrightholder"
                                    ErrorMessage="Copyright holder must be 100 characters or less"
                                    ValidationExpression=".{0,100}" ValidationGroup="Site">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                    <br />
                    Home Page Text:<br />
                    <HTMLEditor:Editor runat="server" Height="300px" Width="90%"
                        Content='<%# Bind("HomePageText") %>'
                        AutoFocus="true" ID="txtHomePageText" />
                    <br />
                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" ValidationGroup="site" class="btn btn-primary"
                        CommandName="Update" Text="Update" />
                    &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CssClass="btn btn-default"
                        CausesValidation="False" CommandName="Cancel" Text="Cancel" />

                </EditItemTemplate>
            </cc:TPMSFormView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
