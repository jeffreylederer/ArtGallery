<%@ Page Title="PayPal Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayPal.aspx.cs" Inherits="ArtGallery.Admin.PayPal" %>
<%@ Register  Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <cc:TPMSObjectDataSource ID="odsPayPal" runat="server"  ErrorLabelID="ErrorLabelPayPal"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetByMode" 
            TypeName="ArtGallery.PayPayDL" UpdateMethod="Update" 
            onupdated="odsPayPal_Updated" AddDummyRow="True" 
        UniqueConstaintMessage="">
            <SelectParameters>
                <asp:ControlParameter ControlID="modeDropDownList" Name="mode" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="buynowurl" Type="String" />
                <asp:Parameter Name="BusinessEmailOrMerchantID" Type="String" />
                <asp:Parameter Name="active" Type="Boolean" />
                <asp:Parameter Name="PDTAuthenticationToken" Type="String" />
                <asp:Parameter Name="CertificateId" Type="String" />
                <asp:Parameter Name="PKCS12CertFile" Type="String" />
                <asp:Parameter Name="PKCS12Password" Type="String" />
                <asp:Parameter Name="PayPalCertPath" Type="String" />
                <asp:Parameter Name="original_lastupdated" Type="DateTime" />
                <asp:Parameter Name="original_mode" Type="String" />
                </UpdateParameters>
    </cc:TPMSObjectDataSource>
       PayPal Mode: <asp:DropDownList ID="modeDropDownList" runat="server" AutoPostBack="True" 
            onselectedindexchanged="modeDropDownList_SelectedIndexChanged" >
        <asp:ListItem Text="Sandbox" Value="SandBox" />
        <asp:ListItem Text="Real" Value="Real" selected="True" />
       </asp:DropDownList><br />

    
     Get PKCS12 Cert File: <asp:FileUpload ID="FileUplPKCS12Cert" runat="server" />
                    <asp:Button runat="server" id="PKCS12CertButton" text="Upload" 
                        onclick="UploadPKCS12CertButton_Click" ValidationGroup="none" /><br />

       &nbsp; Get PayPal Cert File: <asp:FileUpload ID="FileUpPayPal" runat="server" />
                    <asp:Button runat="server" id="UploadPayPalButton" text="Upload" 
                        onclick="UploadPayPalButton_Click" ValidationGroup="none" />
 
       <br />
       <br />
 <asp:UpdatePanel runat="server" ID="upPayPal" UpdateMode="Conditional">
   <ContentTemplate>
    <asp:Label runat="server" ID="ErrorLabelPayPal" ForeColor="Red" EnableViewState="False" />
    <asp:ValidationSummary runat="server" ID="vsPayPal" ValidationGroup="paypal" />
      
    <cc:TPMSFormView ID="fmPayPal" runat="server" DataKeyNames="mode,lastupdated" 
            UpdatePanelID="upPayPal" InsertOrUpdateCheckField="lastupdated"
            DataSourceID="odsPayPal" DefaultMode="Edit">
            <EditItemTemplate>
            <table>

            <tr><td align="right">
                PayPal URL:</td><td>
                <asp:TextBox ID="buynowurlTextBox" runat="server"  width="200px"
                    Text='<%# Bind("buynowurl") %>' /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                        runat="server" ControlToValidate="buynowurlTextBox" 
                        ErrorMessage="PayPal url is required" ValidationGroup="paypal">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator5" runat="server" 
                        ControlToValidate="buynowurlTextBox" 
                        ErrorMessage="PayPal url must be 100 characters or less" 
                        ValidationExpression=".{0,100}" ValidationGroup="paypal">*</asp:RegularExpressionValidator>
                </td></tr>

                <tr><td align="right">
                Business Email Or MerchantID:</td><td>
                <asp:TextBox ID="BusinessEmailOrMerchantIDTextBox" runat="server"  width="200px"
                    Text='<%# Bind("BusinessEmailOrMerchantID") %>' /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" 
                        runat="server" ControlToValidate="BusinessEmailOrMerchantIDTextBox" 
                        ErrorMessage="Business Email or MerchantID is required" 
                        ValidationGroup="paypal">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator6" runat="server" ControlToValidate="BusinessEmailOrMerchantIDTextBox" 
                        ErrorMessage="Business Email or Merchant ID must be 100 characters or less" 
                        ValidationExpression=".{0,100}" ValidationGroup="paypal">*</asp:RegularExpressionValidator>
                 </td></tr>

                <tr><td align="right">
                active:</td><td>
                <asp:CheckBox ID="activeCheckBox" runat="server" 
                    Checked='<%# Bind("active") %>' />
                 </td></tr>

                <tr><td align="right">
                PDT Authentication Token:</td><td>
                <asp:TextBox ID="PDTAuthenticationTokenTextBox" runat="server" width="500px"
                    Text='<%# Bind("PDTAuthenticationToken") %>' /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                        runat="server" ControlToValidate="PDTAuthenticationTokenTextBox" 
                        ErrorMessage="PDT Authentication Token is required" 
                        ValidationGroup="paypal">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator8" runat="server" ControlToValidate="PDTAuthenticationTokenTextBox" 
                        ErrorMessage="PDT Authentication Token must be 200 characters or less" 
                        ValidationExpression=".{0,200}" ValidationGroup="paypal">*</asp:RegularExpressionValidator>
                 </td></tr>

                 <tr><td align="right">
                Certificate Id:</td><td>
                <asp:TextBox ID="CertificateIdTextBox" runat="server"  width="150px"
                    Text='<%# Bind("CertificateId") %>' /><asp:RequiredFieldValidator ID="RequiredFieldValidator8" 
                        runat="server" ControlToValidate="CertificateIdTextBox" 
                        ErrorMessage="Certificate Id is required" 
                        ValidationGroup="paypal">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator9" runat="server" ControlToValidate="CertificateIdTextBox" 
                        ErrorMessage="Certificate Id must be 50 characters or less" 
                        ValidationExpression=".{0,50}" ValidationGroup="paypal">*</asp:RegularExpressionValidator>
                 </td></tr>

               <tr><td align="right">
                PayPal Cert File:</td><td>
                <asp:TextBox ID="PayPalCertPathTextBox" runat="server"  width="200px"
                    Text='<%# Bind("PayPalCertPath") %>' /><asp:RequiredFieldValidator ID="RequiredFieldValidator9" 
                        runat="server" ControlToValidate="PayPalCertPathTextBox" 
                        ErrorMessage="PayPal Cert is required" 
                        ValidationGroup="paypal">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator10" runat="server" ControlToValidate="PayPalCertPathTextBox" 
                        ErrorMessage="PayPal Cert must be 100 characters or less" 
                        ValidationExpression=".{0,100}" ValidationGroup="paypal">*</asp:RegularExpressionValidator>
                   
                 </td></tr>

                 <tr><td align="right">
                PKCS12 Cert File:</td><td>
                <asp:TextBox ID="PKCS12CertFileTextBox" runat="server"  width="200px"
                    Text='<%# Bind("PKCS12CertFile") %>' /><asp:RequiredFieldValidator ID="RequiredFieldValidator11" 
                        runat="server" ControlToValidate="PKCS12CertFileTextBox" 
                        ErrorMessage="PKCS12 Cert File is required" 
                        ValidationGroup="paypal">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator12" runat="server" ControlToValidate="PKCS12CertFileTextBox" 
                        ErrorMessage="PKCS12 Cert File must be 100 characters or less" 
                        ValidationExpression=".{0,100}" ValidationGroup="paypal">*</asp:RegularExpressionValidator>
                   
                 </td></tr>

                <tr><td align="right">
                PKCS12 Password:</td><td>
                <asp:TextBox ID="PKCS12PasswordTextBox" runat="server"  width="150px"
                    Text='<%# Bind("PKCS12Password") %>' /><asp:RequiredFieldValidator ID="RequiredFieldValidator10" 
                        runat="server" ControlToValidate="PKCS12PasswordTextBox" 
                        ErrorMessage="PKCS12 Password is required" 
                        ValidationGroup="paypal">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator11" runat="server" ControlToValidate="PKCS12PasswordTextBox" 
                        ErrorMessage="PKCS12 Password must be 50 characters or less" 
                        ValidationExpression=".{0,50}" ValidationGroup="paypal">*</asp:RegularExpressionValidator>
                 </td></tr>

                <tr><td align="center" colspan="2">
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True"  ValidationGroup="paypal"
                    CommandName="Update" Text="Update" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                 </td></tr>
                 </table>
            </EditItemTemplate>
         </cc:TPMSFormView>
 </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
