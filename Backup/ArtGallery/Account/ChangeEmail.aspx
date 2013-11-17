<%@ Page Title="Change Email Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangeEmail.aspx.cs" Inherits="ArtGallery.Account.ChangeEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
    <ContentTemplate>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <br />
    <table cellpadding="2px" cellspacing="2px">
    <tr>
    <td align="right">
    Current Email Address:
    </td>
    <td>
    <asp:Label runat="server" ID="lblCurrent">    </asp:Label>
    </td>
    </tr>
    <tr>
    <td align="right">
    New Email Address:
    </td>
    <td>
    <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ControlToValidate="txtEmailAddress" ErrorMessage="Not an Email Address" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtEmailAddress" 
        ErrorMessage="Please enter an Email address.">*</asp:RequiredFieldValidator>
 </td>
    </tr>
    </table>
     </ContentTemplate>
     <Triggers>
        <asp:PostBackTrigger ControlID="btnSubmit" />
     </Triggers>
    </asp:UpdatePanel>
    <br />
    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
        Text="Submit" />
</asp:Content>
