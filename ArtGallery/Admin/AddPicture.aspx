﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPicture.aspx.cs" Inherits="ArtGallery.Admin.AddPicture" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Register  Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery"  %>
<%@ OutputCache Duration="1" VaryByParam="id" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Picture File: <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button runat="server" id="UploadButton" text="Upload" 
        onclick="UploadButton_Click" ValidationGroup="none" />
    <br />
    <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
    <ContentTemplate>
   
    <div style="text-align:center;">
    <asp:ValidationSummary runat="server" ID="vs" /><asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" />
    </div>
    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1"  
        DataKeyNames="id" DefaultMode="Insert" Width="100%" 
        onprerender="FormView1_PreRender" >
        <InsertItemTemplate>
            <table>
            <tr>
            <td>
            <table cellpadding="2" cellspacing="2">
            <tr>
            <td align="right">
            Title<cc3:Popup runat="server" ID="puTitle" Key="PictureTitle" />:
            </td>
            <td>
            <asp:TextBox ID="lblTitle" runat="server" Text='<%# Bind("Title") %>' 
                    Width="200px" Rows="2" TextMode="MultiLine" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="lblTitle" ErrorMessage="Title is required">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="lblTitle" ErrorMessage="Title must be 300 character or less" 
                    ValidationExpression=".{1,300}">*</asp:RegularExpressionValidator>
            </td>
            </tr>

             <tr>
            <td align="right">
            Picture<br />File Name<cc3:Popup runat="server" ID="puPictureFile" Key="PictureFile" />
            </td>
            <td >
            <asp:TextBox ID="lblPicturePath" runat="server" Text='<%# Bind("PicturePath") %>' 
                    Width="187px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="lblPicturePath" ErrorMessage="Picture File is required">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                    ControlToValidate="lblPicturePath" ErrorMessage="Picture File must be 256 character or less" 
                    ValidationExpression=".{1,256}">*</asp:RegularExpressionValidator>
            </td>
            </tr>

            <tr>
            <td align="right">
            Gallery<cc3:Popup runat="server" ID="puGallery" Key="GalleryName" />:
            </td>
            <td>
             <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="odsGallery" AppendDataBoundItems="true"
                      DataValueField="id" DataTextField="menutext" 
                    SelectedValue='<%# Bind("galleryid") %>'>
                    <asp:ListItem />
                </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="DropDownList1" ErrorMessage="Gallery is required">*</asp:RequiredFieldValidator>
            </td>
            </tr>

            <tr>
            <td align="right">
            Frame<cc3:Popup runat="server" ID="puFrame" Key="Frame" />:
            </td>
            <td>
            <asp:TextBox ID="lblFrame" runat="server" Text='<%# Bind("Frame") %>' />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="lblFrame" ErrorMessage="Frame is required">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="lblFrame" ErrorMessage="Title must be 100 character or less" 
                    ValidationExpression=".{1,100}">*</asp:RegularExpressionValidator>
            </td>
            </tr>

            <tr>
            <td align="right">
            Surface:
            </td>
            <td>
            <asp:TextBox ID="lblSurface" runat="server" Text='<%# Bind("surface") %>' />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ControlToValidate="lblSurface" ErrorMessage="Surface must be 50 character or less" 
                    ValidationExpression=".{1,100}">*</asp:RegularExpressionValidator>
            </td>
            </tr>

            <tr>
            <td  align="right">
            Width<cc3:Popup runat="server" ID="puWidth" Key="Width" />:
            </td>
            <td>
            <asp:TextBox ID="lblWidth" runat="server" Text='<%# Bind("Width") %>' 
                    Width="50px" />"
            </td>
            </tr>

            <tr>
            <td align="right">
            Height<cc3:Popup runat="server" ID="puHeight" Key="Height" />:
            </td>
            <td>
            <asp:TextBox ID="lblHeight" runat="server" Text='<%# Bind("Height") %>' 
                    Width="50px" />"
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="lblHeight" ErrorMessage="Height is required">*</asp:RequiredFieldValidator>
             <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="lblHeight" MinimumValue="0.0" 
               MaximumValue="32000" Type="Double" Text="*" ErrorMessage="Height must be a positive number" />
            </td>
            </tr>

            <tr>
            <td  align="right">
            Weight<cc3:Popup runat="server" ID="puWeight" Key="Weight" />:
            </td>
            <td>
            <asp:TextBox ID="lblWeight" runat="server" Text='<%# Bind("Weight") %>' 
                    Width="50px" /> lbs
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="lblWeight" ErrorMessage="Weight is required">*</asp:RequiredFieldValidator>
             <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="lblWeight" MinimumValue="0.0" 
               MaximumValue="32000" Type="Double" Text="*" ErrorMessage="Weight must be a positive number" />

            </td>
            </tr>

            <tr>
            <td  align="right">
            Year<cc3:Popup runat="server" ID="puYear" Key="Year" />:
            </td>
            <td>
            <asp:TextBox ID="lblDate" runat="server" Text='<%# Bind("Date") %>' Width="50px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="lblDate" ErrorMessage="Year is required">*</asp:RequiredFieldValidator>
             <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="lblDate" MinimumValue="1900" 
               MaximumValue='<%# DateTime.Now.Year %>' Type="Integer" Text="*" ErrorMessage="Year must be between 1900 and this year" />
            </td>
            </tr>

            <tr>
            <td align="right">
            Media<cc3:Popup runat="server" ID="puMedia" Key="Media" />:
            </td>
            <td>
            <asp:TextBox ID="lblMedia" runat="server" Text='<%# Bind("Media") %>' 
                    Width="200px" /><asp:RequiredFieldValidator runat="server" ID="reqMedia" ControlToValidate="lblMedia"
                    Text="*" ErrorMessage="Media is requied" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                    ControlToValidate="lblMedia" ErrorMessage="Media must be 50 character or less" 
                    ValidationExpression=".{1,50}">*</asp:RegularExpressionValidator>
            </td>
            </tr>

             <tr>
            <td align="right">
            MetaTags<cc3:Popup runat="server" ID="puMetaTags" Key="MetaTagsPicture" />:
            </td>
            <td>
            <asp:TextBox ID="lblMegatags" runat="server" Text='<%# Bind("MetaTags") %>' 
                    Rows="2" TextMode="MultiLine" Width="200px" />
            </td>
            </tr>

            <tr>
            <td align="right">
            Notes<cc3:Popup runat="server" ID="puNotes" Key="Notes" />:
            </td>
            <td>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Notes") %>' Rows="2" 
                    TextMode="MultiLine" Width="200px" />
            </td>
            </tr>

             <tr>
            <td align="right">
            Description<cc3:Popup runat="server" ID="puDescription" Key="Description" />:
            </td>
            <td>
            <asp:TextBox ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' 
                    Rows="8" TextMode="MultiLine" Width="200px" />
            </td>
            </tr>

           
            <tr>
            <td align="right">
            Price<cc3:Popup runat="server" ID="puPrice" Key="Price" />:
            </td>
            <td>
            $<asp:TextBox ID="lblPrice" runat="server" Text='<%# Bind("price") %>' 
                    Width="75px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="lblPrice" ErrorMessage="Price is required">*</asp:RequiredFieldValidator>
             <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="lblPrice" MinimumValue="0" 
               MaximumValue='20000' Type="Integer" Text="*" ErrorMessage="Price must be a positive number less than 20000" />

            </td>
            </tr>

            <td align="right">
            Handling<cc3:Popup runat="server" ID="puHandling" Key="Handling" />:
            </td>
            <td>
            $<asp:TextBox ID="lblHandling" runat="server" Text='<%# Bind("handling") %>' 
                    Width="75px" />
             <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="lblHandling" MinimumValue="0" 
               MaximumValue='2000' Type="Currency" Text="*" ErrorMessage="Handling must be a positive number less than 2000" />

            </td>
            </tr>

            <tr>
            <td align="right">
            Packing weight<cc3:Popup runat="server" ID="puPackingWeight" Key="PackingWeight" />:
            </td>
            <td>
            <asp:TextBox ID="lblPackingweight" runat="server" Text='<%# Bind("Packingweight") %>' 
                    Width="75px" />
            <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="lblPackingweight" MinimumValue="0" 
               MaximumValue='200' Type="Double" Text="*" ErrorMessage="Packing weight must be a positive number less than 200" />

            </td>
            </tr>

            <tr>
            <td align="right">
            Original Available for Sale<cc3:Popup runat="server" ID="puAvailable" Key="Available" />:
            </td>
            <td>
            <asp:CheckBox runat="server" ID="chkAvailble" Checked='<%# Bind("Available") %>' />
            
            </td>
            </tr>

            
            
          <tr>
          <td colspan="2" align="center">
          <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
          </td>
          </tr>
        </table>
           
 </InsertItemTemplate>
    </asp:FormView>
     </ContentTemplate>
    </asp:UpdatePanel>
   
       <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetById" 
        TypeName="ArtGallery.PictureDL" oninserted="ObjectDataSource1_Inserted">
          <InsertParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Frame" Type="String" />
            <asp:Parameter Name="Width" Type="Single" />
            <asp:Parameter Name="Height" Type="Single" />
            <asp:Parameter Name="Date" Type="Int16" />
            <asp:Parameter Name="MetaTags" Type="String" />
            <asp:Parameter Name="Notes" Type="String" />
            <asp:Parameter Name="Media" Type="String" />
            <asp:Parameter Name="GalleryId" Type="Int32" />
            <asp:Parameter Name="PicturePath" Type="String" />
            <asp:Parameter Name="surface" Type="String" />
            <asp:Parameter Name="price" Type="Int16" />
            <asp:Parameter Name="weight" Type="Single" />
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="handling" Type="Double" />
            <asp:Parameter Name="Packingweight" Type="Decimal" />
            <asp:Parameter Name="Available" Type="Boolean" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="id"  DefaultValue="0" Type="Int32" />
        </SelectParameters>
     </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsGallery" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
        TypeName="ArtGallery.GalleryDL"></asp:ObjectDataSource>
       </asp:Content>
