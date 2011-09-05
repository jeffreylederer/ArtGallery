<%@ Page Title="Recover Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" Inherits="ArtGallery.Account.RecoverPassword" %>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
<ContentTemplate>
<asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" /><br />
 <asp:PasswordRecovery ID="PasswordRecovery1" Runat="server" 
    SubmitButtonText="Get Password" SubmitButtonType="Link" 
        onsendmailerror="PasswordRecovery1_SendMailError" 
        onuserlookuperror="PasswordRecovery1_UserLookupError">
     <MailDefinition From="administrator@Contoso.com" 
    Subject="Your new password"
    BodyFileName="PasswordMail.txt" />
</asp:PasswordRecovery>
<br />
<asp:Panel runat="server" ID="pnlCap"  Visible="false">
Fill in below to continue:<br />
<cc1:captchacontrol id="CAPTCHA" runat="server" CaptchaLineNoise="Low" CaptchaLength="7"  Enabled="false"></cc1:captchacontrol>
<asp:Button ID="btnSubmit" Text="Submit" runat="server" onclick="btnSubmit_Click" />
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
