<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditGallery.aspx.cs" Inherits="ArtGallery.Admin.EditGallery"  EnableViewState="true"%>
<%@ OutputCache Duration="1" VaryByParam="id" %>
<%@ Register  Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script src="scripts/jquery-1.4.1.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <cc:TPMSObjectDataSource ID="ObjectDataSource1" runat="server" 
        AddDummyRow="True" OldValuesParameterFormatString="original_{0}"  IsFormView="True"
        SelectMethod="GetById" TypeName="ArtGallery.GalleryDL"  ErrorLabelID="ErrorLabel"
        DeleteMethod="Delete"  
        FkErrorMessage="You must either delete or move to another gallery the pictures in this gallery before delete the gallery."

        UpdateMethod="Update" 
        onselected="ObjectDataSource1_Selected" 
        ondeleted="ObjectDataSource1_Deleted" onupdated="ObjectDataSource1_Updated">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="id" QueryStringField="id" 
                Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="menutext" Type="String" />
            <asp:Parameter Name="gallerytitle" Type="String" />
            <asp:Parameter Name="active" Type="Boolean" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
        </UpdateParameters>
    </cc:TPMSObjectDataSource>

    <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
    <ContentTemplate>
     <div style="text-align:center;">
    <asp:ValidationSummary runat="server" ID="vs" /><asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" />
    </div>
    <cc:TPMSFormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1"  InsertOrUpdateCheckField="lastupdated"
        UpdatePanelID="up1"  
        DataKeyNames="id,lastupdated" DefaultMode="Edit" 
            onprerender="FormView1_PreRender">
        <EditItemTemplate>
            <table cellpadding="2" cellspacing="2">
            <tr>
            <td  align="right"  valign="bottom">
               Gallery Visible<cc3:Popup runat="server" Key="GalleryAvailable" ID="puGalleryAvailable" />:
            </td>
            <td>
            <asp:CheckBox runat="server" ID="chkActive" Checked='<%# Bind("active") %>' />
            </td>
            </tr>
            <tr>
            <td align="right" valign="bottom">
             Menu text<cc3:Popup runat="server" Key="GalleryMenu" ID="puGalleryMenu" />:
             </td>
             <td>
            <asp:TextBox ID="menutextTextBox" runat="server" 
                Text='<%# Bind("menutext") %>' />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="menutextTextBox" ErrorMessage="Menu Text is required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="menutextTextBox" ErrorMessage="Menu Text must be 50 character or less" 
                    ValidationExpression=".{1,50}">*</asp:RegularExpressionValidator>
            </td>
            </tr>

            <tr>
            <td align="right">
            Gallery Name<cc3:Popup runat="server" Key="GalleryDescription" ID="puGalleryDescription" />:
            </td>
             <td>
            <asp:TextBox ID="gallerytitleTextBox" runat="server" width="300px"
                Text='<%# Bind("gallerytitle") %>' />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="gallerytitleTextBox" ErrorMessage="Gallery Name is required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="gallerytitleTextBox" ErrorMessage="Gallery Name must be 100 character or less" 
                    ValidationExpression=".{1,100}">*</asp:RegularExpressionValidator>
             </td>
            </tr>
            <tr>
            <td align="center" colspan="2">
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" /><cc3:Popup runat="server" Key="Update" ID="puUpdate" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" /><cc3:Popup runat="server" Key="CancelUpdate" ID="puCancelUpdate" />
            &nbsp;<asp:LinkButton ID="deleteButton" runat="server" 
                CausesValidation="False" CommandName="Delete" Text="Delete" /><cc3:Popup runat="server" Key="Delete" ID="puDelete" />
                </td>
                </tr>
            </table>
        </EditItemTemplate>
  </cc:TPMSFormView>
   </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
