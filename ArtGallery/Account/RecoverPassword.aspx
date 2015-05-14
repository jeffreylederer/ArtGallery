<%@ Page Title="Recover Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" Inherits="ArtGallery.Account.RecoverPassword" %>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
<ContentTemplate>
<asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" />
    <br />
<br />
Enter User Id: <asp:TextBox runat="server" ID="txtUserName" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtUserName" ErrorMessage="Please enter User Id"></asp:RequiredFieldValidator>
    <br />
<cc1:captchacontrol id="CAPTCHA" runat="server" CaptchaLineNoise="Low" CaptchaLength="6"></cc1:captchacontrol>
    <br />
    <br />
    <br />
<asp:Button ID="btnSubmit" Text="Submit" runat="server" onclick="btnSubmit_Click" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
