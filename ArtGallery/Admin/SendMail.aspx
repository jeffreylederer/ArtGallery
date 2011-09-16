<%@ Page Title="Setup Email Server" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="ArtGallery.Admin.SendMail" %>
<%@ Register  Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="white-space:pre-wrap">
    This page has the information you need to fill out in order to send email from 
    this application to your customers and back to you<br />
</div>  
   <cc:TPMSObjectDataSource ID="ObjectDataSource1" runat="server"  ErrorLabelID="ErrorLabel"
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
        TypeName="ArtGallery.SendMailDL" UpdateMethod="Update" 
        onupdated="ObjectDataSource1_Updated" >
        <UpdateParameters>
            <asp:Parameter Name="EnableSSL" Type="Boolean" />
            <asp:Parameter Name="Port" Type="Int32" />
            <asp:Parameter Name="Host" Type="String" />
            <asp:Parameter Name="emailaddress" Type="String" />
            <asp:Parameter Name="password" Type="String" />
            <asp:Parameter Name="EmailName" Type="String" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
        </UpdateParameters>
    </cc:TPMSObjectDataSource >
    <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
    <ContentTemplate>
   
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" /><asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" />
    <cc:TPMSFormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" 
         UpdatePanelID="up1"
        DefaultMode="Edit" DataKeyNames="lastupdated" 
        InsertOrUpdateCheckField="lastupdated">
        <EditItemTemplate>
            EnableSSL:
            <asp:CheckBox ID="EnableSSLCheckBox" runat="server" 
                Checked='<%# Bind("EnableSSL") %>' />
            <br />
            Port:
            <asp:TextBox ID="PortTextBox" runat="server" Text='<%# Bind("Port") %>' />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="PortTextBox" ErrorMessage="Port is required">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ControlToValidate="PortTextBox" ErrorMessage="Port must be a number" 
                Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
            <br />
            Host:
            <asp:TextBox ID="HostTextBox" runat="server" Text='<%# Bind("Host") %>' />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="HostTextBox" ErrorMessage="Host address is required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="HostTextBox" 
                ErrorMessage="Host address must be less than 100 characters" 
                ValidationExpression=".{0,100}">*</asp:RegularExpressionValidator>
            <br />
            Email Address:
            <asp:TextBox ID="emailaddressTextBox" runat="server" 
                Text='<%# Bind("emailaddress") %>' />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="emailaddressTextBox" ErrorMessage="Emal Address is required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="emailaddressTextBox" 
                ErrorMessage="Email address is not valid" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ControlToValidate="emailaddressTextBox"  ValidationExpression=".{1,100}"
                ErrorMessage="Email address must be 100 characters or less">*</asp:RegularExpressionValidator>
            <br />
            password:
            <asp:TextBox ID="passwordTextBox" runat="server" Text='<%# Bind("password") %>' 
                TextMode="Password" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="passwordTextBox" ErrorMessage="Password is required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                ControlToValidate="passwordTextBox" 
                ErrorMessage="Password must be 50 characters or less" 
                ValidationExpression=".{0,50}">*</asp:RegularExpressionValidator>
            <br />
            Email Name:
            <asp:TextBox ID="EmailNameTextBox" runat="server" 
                Text='<%# Bind("EmailName") %>' />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="EmailNameTextBox" ErrorMessage="Email Name is required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                ControlToValidate="EmailNameTextBox" ValidationExpression=".{1,100}"
                ErrorMessage="Email Name must be 100 character or less">*</asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" />
&nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" 
                CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
    </cc:TPMSFormView>
        <br />
        <asp:Button ID="Button1" runat="server" CausesValidation="False" 
            onclick="Button1_Click" Text="Test Email" />
        <br />
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
