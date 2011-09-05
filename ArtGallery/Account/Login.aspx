<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="ArtGallery.Account.Login" %>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password. This login is for administators only.
        
    </p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false"  
        RenderOuterTable="false" onauthenticate="LoginUser_Authenticate">
 <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                       <p>
                           
                           <p>
                               &nbsp;<cc1:captchacontrol id="CAPTCHA" runat="server" CaptchaLineNoise="Low" CaptchaLength="7"></cc1:captchacontrol>
                               <p>
                               </p>
                               <p>
                                   <asp:CheckBox ID="RememberMe" runat="server" />
                                   <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" 
                                       CssClass="inline">Keep me logged in</asp:Label>
                               </p>
                               <p>
                               </p>
                               <p>
                               </p>
                               <p>
                               </p>
                               <p>
                               </p>
                               <p>
                               </p>
                               <p>
                               </p>
                               <p>
                               </p>
                               <p>
                               </p>
                               <p>
                               </p>
                               <p>
                               </p>
                           </p>
                       </p>     
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"/>
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <asp:HyperLink runat="server" Text="Recover lost password" NavigateUrl="~/Account/RecoverPassword.aspx" ID="hyp1" />
</asp:Content>
