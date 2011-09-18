<%@ Page Title="Basic Site Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="ArtGallery.Admin.Setup"  ValidateRequest="false" %>
<%@ Register  Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<cc:TPMSObjectDataSource ID="odsSite" runat="server"  ErrorLabelID="ErrorLabel"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetSite" 
            TypeName="ArtGallery.SiteDL" UpdateMethod="UpdateSite" 
            onupdated="odsSite_Updated">
            <UpdateParameters>
                <asp:Parameter Name="LogoPath" Type="String" />
                <asp:Parameter Name="HomePageText" Type="String" />
                <asp:Parameter Name="Metatags" Type="String" />
                <asp:Parameter Name="copyrightholder" Type="String" />
            </UpdateParameters>
</cc:TPMSObjectDataSource>
   <asp:UpdatePanel runat="server" ID="upPayPal" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="site" />
        <asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="False" />
        <cc:TPMSFormView ID="FormView1" runat="server" DataSourceID="odsSite" Width="100%"  UpdatePanelID="upPayPal"
            DefaultMode="Edit" InsertOrUpdateCheckField="None">
            <EditItemTemplate>
             <table width="100%">

            <tr>
                <td >
                Upload file to Images directory:
                </td>
                <td colspan="2" align="left">
     <asp:FileUpload ID="FileUploadImages" runat="server" />
     <asp:Button runat="server" id="Button1" text="Upload" 
        onclick="UploadImagesButton_Click" ValidationGroup="none" />
      </td>
            </tr>

                 <tr>
                <td>
                Logo File Name:
                </td>
                <td>
                    <asp:TextBox ID="LogoPathTextBox" runat="server" 
                    Text='<%# Bind("LogoPath") %>' Width="200px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="site"
                    ControlToValidate="LogoPathTextBox" ErrorMessage="Logo Path is required">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="LogoPathTextBox" 
                    ErrorMessage="Logo Path must be 100 characters or less" 
                    ValidationExpression=".{0,100}" ValidationGroup="Site">*</asp:RegularExpressionValidator>
                </td>
                <td >
     <asp:FileUpload ID="FileUploadLogo" runat="server" />
     <asp:Button runat="server" id="UploadLogoButton" text="Upload" 
        onclick="UploadLogoButton_Click" ValidationGroup="none" />
            </td>
            </tr>

            <tr>
            <td>Home Page Metatags:</td>
            <td colspan="2"> <asp:TextBox runat="server" ID="txtMetaTags" Text='<%# Bind("metatags") %>' TextMode="MultiLine" Rows="5" Width="80%" />
            </td>
            </tr>
             <tr>
            <td>Copyright holder:</td>
            <td colspan="2"> <asp:TextBox runat="server" ID="txtCopyrightholder" Text='<%# Bind("copyrightholder") %>'  Width="80%" />
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
               AutoFocus="true"  ID="txtHomePageText" />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True"  ValidationGroup="site"
                    CommandName="Update" Text="Update" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            
</EditItemTemplate>
</cc:TPMSFormView>
</ContentTemplate>
</asp:UpdatePanel>
 </asp:Content>
