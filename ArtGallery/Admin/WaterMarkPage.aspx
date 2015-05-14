<%@ Page Title="Setup WaterMark" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="WaterMarkPage.aspx.cs" Inherits="ArtGallery.WaterMarkPage" %>
<%@ Register assembly="Controls" namespace="ArtGallery" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel runat="server" ID="up1"  UpdateMode="Conditional">
<ContentTemplate>
<asp:ValidationSummary runat="server" ID="vs" /><asp:Label runat="server" ID="ErrorLabel"
 EnableViewState="false" ForeColor="Red" />
    <cc1:TPMSFormView ID="TPMSFormView1" runat="server" DataKeyNames="lastupdated" 
        DataSourceID="TPMSObjectDataSource1" DefaultMode="Edit" 
        InsertOrUpdateCheckField="lastupdated" UpdatePanelID="up1">
        <EditItemTemplate>
            <table>
            <tr>
            <td align="right">Text:<td>
            <asp:TextBox ID="WatermarkTextTextBox" runat="server"  CssClass="form-control"
                Text='<%# Bind("WatermarkText") %>' /><asp:RegularExpressionValidator
                runat="server" ID="reg1" ControlToValidate="WatermarkTextTextBox" Text="*"
                ErrorMessage="Text must be 50 charaters or less" 
               ValidationExpression=".{0,50}" />
             </td>
             </tr>

             <tr>
            <td align="right">
            Font Family:<td>
            <asp:TextBox ID="WatermarkFontFamilyTextBox" runat="server"  CssClass="form-control"
                Text='<%# Bind("WatermarkFontFamily") %>' /><asp:RegularExpressionValidator
                runat="server" ID="RegularExpressionValidator1" ControlToValidate="WatermarkFontFamilyTextBox" Text="*"
                ErrorMessage="Font Family must be 50 charaters or less" 
               ValidationExpression=".{0,50}" />
             </td>
             </tr>

            <tr>
            <td align="right">
            Font Color:<td>
            <asp:TextBox ID="WatermarkFontColorTextBox" runat="server"  CssClass="form-control"
                Text='<%# Bind("WatermarkFontColor") %>' /><asp:RegularExpressionValidator
                runat="server" ID="RegularExpressionValidator2" ControlToValidate="WatermarkFontColorTextBox" Text="*"
                ErrorMessage="Font Color must be 50 charaters or less" 
               ValidationExpression=".{0,50}" />
             </td>
             </tr>

             <tr>
            <td align="right">
            Font Size:<td>
            <asp:TextBox ID="WatermarkFontSizeTextBox" runat="server"  CssClass="form-control"
                Text='<%# Bind("WatermarkFontSize", "{0:N}") %>' /><asp:CompareValidator runat="server" ID="cp1"
                ControlToValidate="WatermarkFontSizeTextBox" Type="Double" Operator="DataTypeCheck" Text="*"
                 ErrorMessage="Font Size must be a number" />
            </td>
             </tr>

            <tr>
            <td colspan="2" align="center">
             <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CssClass="btn btn-primary"
                CommandName="Update" Text="Update" />
            &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CssClass="btn btn-default"
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
          </td>
            </tr>
            </table>
        </EditItemTemplate>
  
    </cc1:TPMSFormView>
    <cc1:TPMSObjectDataSource ID="TPMSObjectDataSource1" runat="server" 
        AddDummyRow="True" ErrorLabelID="ErrorLabel"
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
        TypeName="ArtGallery.WaterMarkDL" UniqueConstaintMessage="" 
        UpdateMethod="Update" IsFormView="True">
        <UpdateParameters>
            <asp:Parameter Name="WatermarkText" Type="String" />
            <asp:Parameter Name="WatermarkFontFamily" Type="String" />
            <asp:Parameter Name="WatermarkFontColor" Type="String" />
            <asp:Parameter Name="WatermarkFontSize" Type="String" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
        </UpdateParameters>
    </cc1:TPMSObjectDataSource>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
