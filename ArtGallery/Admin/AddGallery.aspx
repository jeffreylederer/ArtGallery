<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddGallery.aspx.cs" Inherits="ArtGallery.Admin.AddGallery" EnableViewState="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetById" TypeName="ArtGallery.GalleryDL" 
        oninserted="ObjectDataSource1_Inserted">
        <InsertParameters>
            <asp:Parameter Name="menutext" Type="String" />
            <asp:Parameter Name="gallerytitle" Type="String" />
            <asp:Parameter Name="active" Type="Boolean" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="id" QueryStringField="id" 
                Type="Int32" />
        </SelectParameters>
     </asp:ObjectDataSource>
    <div style="text-align:center;">
    <asp:ValidationSummary runat="server" ID="vs" /><asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" />
    </div>
    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" 
        DefaultMode="Insert" >
          <InsertItemTemplate>
            <table cellpadding="2" cellspacing="2">
             <tr>
            <td  align="right">
                Gallery Visible<cc3:Popup runat="server" Key="GalleryAvailable" ID="puGalleryAvailable" />:
            </td>
            <td>
            <asp:CheckBox runat="server" ID="chkActive" Checked='<%# Bind("active") %>' />
            </td>
            </tr>
            <tr>
            <tr>
            <td align="right">
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
            <asp:TextBox ID="gallerytitleTextBox" runat="server"  width="300px"
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
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" /><cc3:Popup runat="server" Key="Insert" ID="puInsert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" /><cc3:Popup runat="server" Key="CancelInsert" ID="puCanel" />
            </td>
            </tr>
            </table>
        </InsertItemTemplate>
     </asp:FormView>
</asp:Content>
