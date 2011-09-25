<%@ Page Title="Error Log" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorLog.aspx.cs" Inherits="ArtGallery.Admin.ErrorLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
        TypeName="ArtGallery.ErrorLogDL"></asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="id" 
        DataSourceID="ObjectDataSource1" EmptyDataText="No Errors">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                ReadOnly="True" SortExpression="id" />
            <asp:TemplateField HeaderText="error message" SortExpression="errormessage">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Split(Eval("errormessage")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="lastupdated" HeaderText="Date/Time" 
                SortExpression="lastupdated" />
        </Columns>
    </asp:GridView>
    Look up Error: <asp:TextBox runat="server" ID="txtid" /><asp:RequiredFieldValidator
    runat="server" ID="req1" ControlToValidate="txtid" Text="*" />
<asp:CompareValidator
    runat="server" ID="cv" Type="Integer"
     Operator="DataTypeCheck" Text="*" ControlToValidate="txtid" />
    <asp:Button runat="server" ID="btnSubmit" Text="Look Up"  
        onclick="btnSubmit_Click" />
</asp:Content>
