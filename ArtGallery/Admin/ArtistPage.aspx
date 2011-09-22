<%@ Page Title="Artist Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArtistPage.aspx.cs" Inherits="ArtGallery.Admin.ArtistPage" %>
<%@ Register  Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<cc:TPMSObjectDataSource ID="odsArtist" runat="server"  ErrorLabelID="lblErrorArtist"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetArtist" 
            TypeName="ArtGallery.SiteDL" UpdateMethod="UpdateArtist" 
            onupdated="odsArtist_Updated">
            <UpdateParameters>
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="ArtistImagePath" Type="String" />
                  <asp:Parameter Name="ArtistPageText" Type="String" />
                <asp:Parameter Name="contact" Type="String" />
            </UpdateParameters>
</cc:TPMSObjectDataSource>
Get Artist Image File:   <asp:FileUpload ID="FileUploadArtistImage" runat="server" />
    <asp:Button runat="server" id="UploadArtistImageButton" text="Upload" 
        onclick="UploadArtistImageButton_Click" ValidationGroup="none" />
    <br />
    <br />

<asp:Label runat="server" ID="lblErrorArtist" ForeColor="Red" EnableViewState="false" />
         <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
            ValidationGroup="artist" />
   <asp:UpdatePanel runat="server" ID="upPayPal" UpdateMode="Conditional">
    <ContentTemplate>
        <cc:TPMSFormView ID="FormView2" runat="server" DataSourceID="odsArtist" Width="100%" 
            DefaultMode="Edit" UpdatePanelID="upPayPal" InsertOrUpdateCheckField="None">
            <EditItemTemplate>
            <table width="100%">
            <tr>
            <td width="150px" align="right">
            
               Artist Email:</td>
                <td colspan="2">
                    <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("email") %>' 
                    Width="200px" />
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ValidationGroup="artist"
                    ControlToValidate="emailTextBox" ErrorMessage="Email is required">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="emailTextBox" 
                    ErrorMessage="Email must be 250 characters or less"  ValidationGroup="artist"
                    ValidationExpression=".{0,250}">*</asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"  ValidationGroup="artist"
                    ErrorMessage="Email must be an email address." ControlToValidate="emailTextBox"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" >*</asp:RegularExpressionValidator>
               </td>
                </tr>
                <tr>
                <td align="right" width="150px">
                Artist Image File Name:
                </td>
                <td>
                <asp:TextBox ID="ArtistImagePathTextBox" runat="server" 
                    Text='<%# Bind("ArtistImagePath") %>' Width="200px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="ArtistImagePathTextBox"  ValidationGroup="artist"
                    ErrorMessage="Artist Image path is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ControlToValidate="ArtistImagePathTextBox" 
                    ErrorMessage="Artist Image Path must be 100 characters or less" 
                    ValidationExpression=".{0,100}" ValidationGroup="artist">*</asp:RegularExpressionValidator>
                </td>
                </table>
               
                Artist Page Text:<br />
                <HTMLEditor:Editor runat="server" Height="300px" Width="90%"  
                 Content='<%# Bind("ArtistPageText") %>' 
               AutoFocus="true"  ID="txtArtistPageText" />
                <br />
                 Contact Information:<br />
                <HTMLEditor:Editor runat="server" Height="300px" Width="90%"  
                 Content='<%# Bind("contact") %>' 
               AutoFocus="true"  ID="ContactEditor" />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True"  ValidationGroup="artist"
                    CommandName="Update" Text="Update" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />

</EditItemTemplate>
</cc:TPMSFormView>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>