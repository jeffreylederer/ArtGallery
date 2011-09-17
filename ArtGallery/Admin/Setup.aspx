<%@ Page Title="Setup Site" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="ArtGallery.Admin.Setup"  ValidateRequest="false" %>
<%@ Register  Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ObjectDataSource ID="odsSite" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetSite" 
            TypeName="ArtGallery.SiteDL" UpdateMethod="UpdateSite" 
            onupdated="odsSite_Updated">
            <UpdateParameters>
                <asp:Parameter Name="LogoPath" Type="String" />
                <asp:Parameter Name="HomePageText" Type="String" />
                <asp:Parameter Name="Metatags" Type="String" />
                <asp:Parameter Name="copyrightholder" Type="String" />
            </UpdateParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="odsArtist" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetArtist" 
            TypeName="ArtGallery.SiteDL" UpdateMethod="UpdateArtist" 
            onupdated="odsArtist_Updated">
            <UpdateParameters>
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="ArtistImagePath" Type="String" />
                  <asp:Parameter Name="ArtistPageText" Type="String" />
                <asp:Parameter Name="contact" Type="String" />
            </UpdateParameters>
</asp:ObjectDataSource>

 


<ajax:TabContainer runat="server" Width="100%"  ID="tab" ActiveTabIndex="0">
<ajax:TabPanel runat="server" HeaderText="Site" ID="pnlSite">

    <ContentTemplate>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="site" />
        <asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="False" />
        <asp:FormView ID="FormView1" runat="server" DataSourceID="odsSite" Width="100%" 
            DefaultMode="Edit">
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
</asp:FormView>
       





    
</ContentTemplate>
</ajax:TabPanel>
<ajax:TabPanel runat="server" HeaderText="Artist" ID="pntArtist">

    <ContentTemplate>
         <asp:Label runat="server" ID="lblErrorArtist" ForeColor="Red" EnableViewState="false" />
         <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
            ValidationGroup="artist" />
             
        <asp:FormView ID="FormView2" runat="server" DataSourceID="odsArtist" Width="100%" 
            DefaultMode="Edit">
            <EditItemTemplate>
            <table width="100%">
            <tr>
            <td width="200px">
            
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
                <td>
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
                 <td>
      <asp:FileUpload ID="FileUploadArtistImage" runat="server" />
    <asp:Button runat="server" id="UploadArtistImageButton" text="Upload" 
        onclick="UploadArtistImageButton_Click" ValidationGroup="none" />

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
</asp:FormView>
     
</ContentTemplate>
</ajax:TabPanel>
<ajax:TabPanel runat="server" HeaderText="PayPal" ID="pnlPayPal">
    <ContentTemplate>
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
       </asp:DropDownList>
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
                <asp:TextBox ID="PDTAuthenticationTokenTextBox" runat="server" width="300px"
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
                    <asp:FileUpload ID="FileUpPayPal" runat="server" />
                    <asp:Button runat="server" id="UploadPayPalButton" text="Upload" 
                        onclick="UploadPayPalButton_Click" ValidationGroup="none" />
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
                    <asp:FileUpload ID="FileUplPKCS12Cert" runat="server" />
                    <asp:Button runat="server" id="PKCS12CertButton" text="Upload" 
                        onclick="UploadPKCS12CertButton_Click" ValidationGroup="none" />
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
 </ContentTemplate>
</ajax:TabPanel>

</ajax:TabContainer>
</asp:Content>
