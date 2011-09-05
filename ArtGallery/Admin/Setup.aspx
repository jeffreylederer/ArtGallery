<%@ Page Title="Setup Site" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="ArtGallery.Admin.Setup"  ValidateRequest="false" %>
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

 <asp:ObjectDataSource ID="odsPayPal" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
            TypeName="ArtGallery.PayPayDL" UpdateMethod="Update" 
            onupdated="odsPayPal_Updated">
            <UpdateParameters>
                <asp:Parameter Name="buynowurl" Type="String" />
                <asp:Parameter Name="BusinessEmailOrMerchantID" Type="String" />
                <asp:Parameter Name="active" Type="Boolean" />
                <asp:Parameter Name="original_mode" Type="String" />
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
     <asp:Label runat="server" ID="ErrorLabelPayPal" ForeColor="Red" EnableViewState="False" />
    <asp:ValidationSummary runat="server" ID="vsPayPal" ValidationGroup="paypal" />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="mode" DataSourceID="odsPayPal"><Columns>
<asp:BoundField DataField="mode" HeaderText="mode" ReadOnly="True" 
                    SortExpression="mode" />
<asp:TemplateField HeaderText="Active" SortExpression="active">
    <EditItemTemplate>
                        <asp:CheckBox ID="chkActiveEdit" runat="server" Checked='<%# Bind("Active") %>' />
                    
</EditItemTemplate>
<ItemTemplate>
                       <asp:CheckBox ID="chkActiveItem" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="PayPal URL" SortExpression="buynowurl">
    <EditItemTemplate>
                        <asp:TextBox ID="txtPayPalUrl" runat="server" Width="300px"
            Text='<%# Bind("buynowurl") %>'></asp:TextBox>
                    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
            runat="server" ControlToValidate="txtPayPalUrl" 
            ErrorMessage="PayPal url is required" ValidationGroup="paypal">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator 
            ID="RegularExpressionValidator5" runat="server" 
            ControlToValidate="txtPayPalUrl" 
            ErrorMessage="PayPal url must be 100 characters or less" 
            ValidationExpression="{0.100}" ValidationGroup="paypal">*</asp:RegularExpressionValidator>
                    
</EditItemTemplate>
<ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("buynowurl") %>'></asp:Label>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Business Email Or Merchant ID" 
                    SortExpression="BusinessEmailOrMerchantID">
    <EditItemTemplate>
                        <asp:TextBox ID="txtMerchant" runat="server" 
                            Text='<%# Bind("BusinessEmailOrMerchantID") %>' Width="300px"></asp:TextBox>
                    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" 
            runat="server" ControlToValidate="txtMerchant" 
            ErrorMessage="Business Email or MerchantID is required" 
            ValidationGroup="paypal">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator 
            ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtMerchant" 
            ErrorMessage="Business Email or Merchant ID must be 100 characters or less" 
            ValidationExpression="{0.100}" ValidationGroup="paypal">*</asp:RegularExpressionValidator>
                    
</EditItemTemplate>
<ItemTemplate>
                        <asp:Label ID="Label3" runat="server" 
                            Text='<%# Eval("BusinessEmailOrMerchantID") %>'></asp:Label>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField ShowEditButton="True" />
</Columns>
</asp:GridView>
 </ContentTemplate>
</ajax:TabPanel>

</ajax:TabContainer>
</asp:Content>
