<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="ArtGallery.EditPicture"  Codebehind="EditPicture.aspx.cs" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Register  Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    &nbsp;Picture File: <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button runat="server" id="UploadButton" text="Upload" 
        onclick="UploadButton_Click" ValidationGroup="none" />
    <br />
    <cc:TPMSObjectDataSource ID="ObjectDataSource1" runat="server" ErrorLabelID="ErrorLabel"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetById"   IsFormView="True"
        TypeName="ArtGallery.PictureDL" DeleteMethod="Delete" 
        UpdateMethod="Update" AddDummyRow="True" 
        onupdated="ObjectDataSource1_Updated" >
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
            <asp:Parameter Name="original_metatags" Type="String" />
        </DeleteParameters>
         <SelectParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" 
                Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
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
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_metatags" Type="String" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
        </UpdateParameters>
    </cc:TPMSObjectDataSource>
    <asp:ObjectDataSource ID="odsGallery" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
        TypeName="ArtGallery.GalleryDL"></asp:ObjectDataSource>
 
<table>
<tr>
<td>
<div style="text-align:center;">
   <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
    <ContentTemplate>
     <asp:ValidationSummary runat="server" ID="vs" /><asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" />
</div>
 
    <cc:TPMSFormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1"  UpdatePanelID="up2"
        DataKeyNames="id,lastupdated,metatags" DefaultMode="Edit" Width="100%" InsertOrUpdateCheckField="lastupdated"
        onprerender="FormView1_PreRender">
        <EditItemTemplate>
         <table>
            <tr>
            <td >

            <table cellpadding="2" cellspacing="2">
            <tr>
            <td align="right">
            Title<cc3:Popup runat="server" ID="puTitle" Key="PictureTitle" />:
            </td>
            <td>
            <asp:TextBox ID="lblTitle" runat="server" Text='<%# Bind("Title") %>' 
                    Width="300px" Rows="2" TextMode="MultiLine" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="lblTitle" ErrorMessage="Title is required">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="lblTitle" ErrorMessage="Title must be 300 character or less" 
                    ValidationExpression=".{1,300}">*</asp:RegularExpressionValidator>
            </td>
            </tr>

             <tr>
            <td align="right">
            Picture<br />File Name<cc3:Popup runat="server" ID="puPictureFile" Key="PictureFile" />:
            </td>
            <td >
            <asp:TextBox ID="lblPicturePath" runat="server" Text='<%# Bind("PicturePath") %>' 
                    Width="300px" />
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
                    Width="300px" /><asp:RequiredFieldValidator ID="RequiredFieldValidatorMedia" runat="server" 
                    ControlToValidate="lblMedia" ErrorMessage="Media is required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMedia" runat="server" 
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
                    Rows="2" TextMode="MultiLine" Width="300px" />
            </td>
            </tr>

            <tr>
            <td align="right">
            Notes<cc3:Popup runat="server" ID="puNotes" Key="Notes" />:
            </td>
            <td>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Notes") %>' Rows="2" 
                    TextMode="MultiLine" Width="300px" />
            </td>
            </tr>

             <tr>
            <td align="right">
            Description<cc3:Popup runat="server" ID="puDescription" Key="Description" />:
            </td>
            <td>
            <asp:TextBox ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' 
                    Rows="8" TextMode="MultiLine" Width="300px" />
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
            <asp:TextBox ID="lblHandling" runat="server" Text='<%# Bind("handling", "{0:0.00}") %>' 
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
          <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" /><cc3:Popup runat="server" ID="puUpdate" Key="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" /><cc3:Popup runat="server" ID="puCancel" Key="CancelUpdate" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" 
                CausesValidation="False" CommandName="Delete" Text="Delete" /><cc3:Popup runat="server" ID="puDeletePicture" Key="DeletePicture" />
            <br /><asp:HyperLink  runat="server" 
            NavigateUrl='<%#"SelectGallery.aspx?id=" +  Eval("GalleryId").ToString() %>'
                Text='<%# "Back to " + Eval("Gallery").ToString() + " Gallery" %>' ID="HyperLink1" />&nbsp; 
            <asp:HyperLink  runat="server" 
            NavigateUrl='<%#"AddPicture.aspx?id=" +  Eval("GalleryId").ToString() %>'
                Text="Add new picture to this Gallery" ID="hyp1" />

          </td>
          </tr>
          
        </table>
        </td>
        </tr>
        </table>
          </EditItemTemplate>
    </cc:TPMSFormView>
     </ContentTemplate>
    </asp:UpdatePanel>
</td>
              
<td>
    <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource1" 
        DataKeyNames="id,metatags" Width="100%"
        onprerender="FormView1_PreRender">
        <ItemTemplate>

    <table>
    <tr>
           <td class="leftarrow">
                 <asp:ImageButton runat="server" AlternateText="Previous" ID="btnPrevious" ImageUrl="~/Images/left.jpg" Height="20px" Width="20px" oncommand="btnPrevious_Command" />
           </td>
            <td class="picture">
               <cc1:GeneratedImage ID="WatermarkedImageGenerator" runat="server" ImageHandlerUrl="~/ImageHandlers/PageImageHandler.ashx">
                <Parameters>
                    <cc1:ImageParameter Name="ImageUrl" Value='<%#  "~/App_Data/" + Eval("PicturePath") %>' />
                    <cc1:ImageParameter Name="Height" Value="500" />
                    <cc1:ImageParameter Name="Width" Value="450" />
                    <cc1:ImageParameter Name="ActualHeight" Value='<%# Eval("Height") %>' />
                    <cc1:ImageParameter Name="ActualWidth" Value='<%# Eval("Width") %>' />
                 </Parameters>
              </cc1:GeneratedImage>
              </td>
              </td>
                <td class="rightarrow"> <asp:ImageButton runat="server" ID="btnNext" ImageUrl="~/Images/right.jpg" 
                    BorderStyle="None"  OnClick="btnNext_Click" Height="20px" Width="20px" /></td>
               </tr>
               </table>
       </ItemTemplate>
    </asp:FormView>
 </td>
 </tr>
 <tr>
 <td colspan="2">
 <asp:UpdatePanel runat="server" UpdateMode="Conditional"  ID="up1">
 <ContentTemplate>
     
<asp:Label ID="GridErrorLabel" runat="server" EnableViewState="False" 
         ForeColor="Red"></asp:Label><asp:ValidationSummary runat="server" ID="vsGrid"
 ValidationGroup="grid" />
 <cc:TPMSGridView ID="GridView1" runat="server" AddButtonIDs="btnAdd"  UpdatePanelID="up1"
         AutoGenerateColumns="False" bordercolor="" DataKeyNames="id,lastupdated" 
         DataSourceID="odsReproductions">
     <Columns>
         <asp:TemplateField ShowHeader="False">
             <ItemTemplate>
                 <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                     CommandName="Edit" Text="Edit"></asp:LinkButton>
                 <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                     CommandName="Delete" Text="Delete"></asp:LinkButton>
             </ItemTemplate>
             <EditItemTemplate>
                 <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" ValidationGroup="grid"
                     CommandName="Update" Text="Update"></asp:LinkButton>
                 &nbsp;<asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                     CommandName="Cancel" Text="Cancel"></asp:LinkButton>
             </EditItemTemplate>
              <FooterTemplate>
                 <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="True" ValidationGroup="grid"
                     CommandName="Insert" Text="Insert"></asp:LinkButton>
                 &nbsp;<asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="False" 
                     CommandName="Cancel" Text="Cancel"></asp:LinkButton>
             </FooterTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Description" SortExpression="description">
             <ItemTemplate>
                 <asp:Label ID="Label2" runat="server" Text='<%# Bind("description") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
                 <asp:TextBox width="250px" ID="txtDescription" runat="server" Text='<%# Bind("description") %>'></asp:TextBox>
                 <asp:RequiredFieldValidator runat="server" ID="rqDescription" ControlToValidate="txtDescription"
                 Text="*" ErrorMessage="Description is required" ValidationGroup="grid" />
                 <asp:RegularExpressionValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtDescription"
                 Text="*" ErrorMessage="Description must be 300 characters or less" ValidationGroup="grid" ValidationExpression=".{1,300}" />
             </EditItemTemplate>
             <FooterTemplate>
                 <asp:TextBox width="250px" ID="txtDescription" runat="server" ></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rqDescription" ControlToValidate="txtDescription"
                 Text="*" ErrorMessage="Description is required" ValidationGroup="grid" />
                 <asp:RegularExpressionValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtDescription"
                 Text="*" ErrorMessage="Description must be 300 characters or less" ValidationGroup="grid" ValidationExpression=".{1,300}" />
             </FooterTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Width" SortExpression="width">
             <ItemTemplate>
                 <asp:Label ID="Label3" runat="server" Text='<%# Bind("width") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
                 <asp:TextBox width="75px" ID="txtWidth" runat="server" Text='<%# Bind("width") %>'></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rqWidth" ControlToValidate="txtWidth"
                 Text="*" ErrorMessage="Width is required" ValidationGroup="grid" />
                 <asp:RangeValidator runat="server" ID="rangeWidth" ControlToValidate="txtWidth"
                 Text="*" ErrorMessage="Width must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Double" />
             </EditItemTemplate>
              <FooterTemplate>
                 <asp:TextBox width="75px" ID="txtWidth" runat="server" ></asp:TextBox>
                 <asp:RequiredFieldValidator runat="server" ID="rqWidth" ControlToValidate="txtWidth"
                 Text="*" ErrorMessage="Width is required" ValidationGroup="grid" />
                  <asp:RangeValidator runat="server" ID="rangeWidth" ControlToValidate="txtWidth"
                 Text="*" ErrorMessage="Width must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Double" />
             </FooterTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="Height" SortExpression="height">
             <ItemTemplate>
                 <asp:Label ID="Label4" runat="server" Text='<%# Bind("height") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
                 <asp:TextBox width="75px" ID="txtHeight" runat="server" Text='<%# Bind("height") %>'></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rqHeight" ControlToValidate="txtWeight"
                 Text="*" ErrorMessage="Height is required" ValidationGroup="grid" />
                 <asp:RangeValidator runat="server" ID="rangeHeight" ControlToValidate="txtWeight"
                 Text="*" ErrorMessage="Height must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Double" />
             </EditItemTemplate>
             <FooterTemplate>
                 <asp:TextBox width="75px" ID="txtHeight" runat="server" ></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rqHeight" ControlToValidate="txtHeight"
                 Text="*" ErrorMessage="Height is required" ValidationGroup="grid" />
                 <asp:RangeValidator runat="server" ID="rangeHeight" ControlToValidate="txtWeight"
                 Text="*" ErrorMessage="Height must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Double" />
             </FooterTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Weight" SortExpression="weight">
             <ItemTemplate>
                 <asp:Label ID="Label5" runat="server" Text='<%# Bind("weight") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
                 <asp:TextBox width="75px" ID="txtWeight" runat="server" Text='<%# Bind("weight") %>'></asp:TextBox>
                 <asp:RequiredFieldValidator runat="server" ID="rqWeight" ControlToValidate="txtWeight"
                 Text="*" ErrorMessage="Weight is required" ValidationGroup="grid" />
             </EditItemTemplate>
             <FooterTemplate>
                 <asp:TextBox width="75px" ID="txtWeight" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rqWeight" ControlToValidate="txtWeight"
                 Text="*" ErrorMessage="Weight is required" ValidationGroup="grid" />
             </FooterTemplate>
         </asp:TemplateField>

        <asp:TemplateField HeaderText="Packing Weight" SortExpression="packingweight">
             <ItemTemplate>
                 <asp:Label ID="Label8" runat="server" Text='<%# Bind("packingweight") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
                 <asp:TextBox width="75px" ID="txtPacking" runat="server" Text='<%# Bind("packingweight") %>'></asp:TextBox>
                  <asp:RangeValidator runat="server" ID="rangetxtPacking" ControlToValidate="txtPacking"
                 Text="*" ErrorMessage="Packing weight must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Double" />
             </EditItemTemplate>
             <FooterTemplate>
                 <asp:TextBox width="75px" ID="txtPacking" runat="server" Text='<%# Bind("packingweight") %>'></asp:TextBox>
                 <asp:RangeValidator runat="server" ID="rangetxtPacking" ControlToValidate="txtPacking"
                 Text="*" ErrorMessage="Packing weight must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Double" />
             </FooterTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Handling" SortExpression="handling">
             <ItemTemplate>
                 <asp:Label ID="Label6" runat="server" Text='<%# Bind("handling") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
                 <asp:TextBox width="75px" ID="txtHandling" runat="server" Text='<%# Bind("handling") %>'></asp:TextBox>
                  <asp:RangeValidator runat="server" ID="rangetxtHandling" ControlToValidate="txtHandling"
                 Text="*" ErrorMessage="Handling must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Double" />
              </EditItemTemplate>
             <FooterTemplate>
                 <asp:TextBox width="75px" ID="txtHandling" runat="server" Text='<%# Bind("handling") %>'></asp:TextBox>
                 <asp:RangeValidator runat="server" ID="rangetxtHandling" ControlToValidate="txtHandling"
                 Text="*" ErrorMessage="Handling must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Double" />
             </FooterTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Price" SortExpression="price">
             <ItemTemplate>
                 <asp:Label ID="Label7" runat="server" Text='<%# Bind("price", "{0:0.00}") %>'></asp:Label>
             </ItemTemplate>
             <EditItemTemplate>
                 <asp:TextBox width="75px" ID="txtPrice" runat="server" Text='<%# Bind("price") %>'></asp:TextBox>
                 <asp:RequiredFieldValidator runat="server" ID="rqPrice" ControlToValidate="txtPrice"
                 Text="*" ErrorMessage="Price is required" ValidationGroup="grid" />
                  <asp:RangeValidator runat="server" ID="rangetxtPrice" ControlToValidate="txtPrice"
                 Text="*" ErrorMessage="Price must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Currency" />
             </EditItemTemplate>
             <FooterTemplate>
                 <asp:TextBox width="75px" ID="txtPrice" runat="server" Text='<%# Bind("price") %>'></asp:TextBox>
                 <asp:RequiredFieldValidator runat="server" ID="rqPrice" ControlToValidate="txtPrice"
                 Text="*" ErrorMessage="Price is required" ValidationGroup="grid" />
                  <asp:RangeValidator runat="server" ID="rangetxtPrice" ControlToValidate="txtPrice"
                 Text="*" ErrorMessage="Price must be a positive number" ValidationGroup="grid" 
                 MinimumValue="0"  MaximumValue="100000" Type="Currency" />
             </FooterTemplate>                       
         </asp:TemplateField>

       

     </Columns>
 </cc:TPMSGridView>
 <asp:Button ID="btnAdd" runat="server" Text="Add Reproduction" />
 </ContentTemplate>
 </asp:UpdatePanel>
 <cc:TPMSObjectDataSource ID="odsReproductions" runat="server" AddDummyRow="True" 
         DeleteMethod="Delete" ErrorLabelID="GridErroLabel" InsertMethod="Insert" 
         OldValuesParameterFormatString="original_{0}" SelectMethod="GetByPictureId" 
         TypeName="ArtGallery.ReproductionDL" UniqueConstaintMessage="" 
         UpdateMethod="Update" oninserting="odsReproduction_Inserting" >
     <DeleteParameters>
         <asp:Parameter Name="original_id" Type="Int32" />
         <asp:Parameter Name="original_lastupdated" Type="DateTime" />
     </DeleteParameters>
     <InsertParameters>
         <asp:Parameter Name="pictureid" Type="Int32" />
         <asp:Parameter Name="description" Type="String" />
         <asp:Parameter Name="price" Type="Decimal" />
         <asp:Parameter Name="weight" Type="Decimal" />
         <asp:Parameter Name="handling" Type="Decimal" />
         <asp:Parameter Name="width" Type="Decimal" />
         <asp:Parameter Name="height" Type="Decimal" />
         <asp:Parameter Name="packingweight" Type="Double" />
     </InsertParameters>
     <SelectParameters>
         <asp:QueryStringParameter Name="pictureid" QueryStringField="id" Type="Int32" />
     </SelectParameters>
     <UpdateParameters>
         <asp:Parameter Name="description" Type="String" />
         <asp:Parameter Name="price" Type="Decimal" />
         <asp:Parameter Name="weight" Type="Decimal" />
         <asp:Parameter Name="handling" Type="Decimal" />
         <asp:Parameter Name="width" Type="Decimal" />
         <asp:Parameter Name="height" Type="Decimal" />
         <asp:Parameter Name="packingweight" Type="Double" />
         <asp:Parameter Name="original_id" Type="Int32" />
         <asp:Parameter Name="original_lastupdated" Type="DateTime" />
     </UpdateParameters>
 </cc:TPMSObjectDataSource>
 
 </td>
 </tr>
 </table>
 </asp:Content>