<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" Inherits="ArtGallery.EditBook" CodeBehind="EditBook.aspx.cs" %>

<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="Controls" TagPrefix="cc3" Namespace="ArtGallery" %>
<%@ Register Assembly="Controls" TagPrefix="cc" Namespace="ArtGallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    Picture File:
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button runat="server" ID="UploadButton" Text="Upload"
        OnClick="UploadButton_Click" ValidationGroup="none" />
    <br />
    <cc:TPMSObjectDataSource ID="ObjectDataSource1" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetById" IsFormView="True"
        TypeName="ArtGallery.BookDL"
        UpdateMethod="Update" AddDummyRow="True"
        UniqueConstaintMessage=""
        OnSelected="ObjectDataSource1_Selected"
        OnDeleted="ObjectDataSource1_Deleted"
        OnUpdated="ObjectDataSource1_Updated" ErrorLabelID="" DeleteMethod="Delete">

        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id"
                Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="menutext" Type="String" />
            <asp:Parameter Name="title" Type="String" />
            <asp:Parameter Name="style" Type="String" />
            <asp:Parameter Name="price" Type="Double" />
            <asp:Parameter Name="weight" Type="Decimal" />
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="PicturePath" Type="String" />
            <asp:Parameter Name="Date" Type="Int16" />
            <asp:Parameter Name="active" Type="Boolean" />
            <asp:Parameter Name="height" Type="Single" />
            <asp:Parameter Name="width" Type="Single" />
            <asp:Parameter Name="metatags" Type="String" />
            <asp:Parameter Name="notes" Type="String" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
        </UpdateParameters>
    </cc:TPMSObjectDataSource>

    <table>
        <tr>
            <td>
                <div style="text-align: center;">
                    <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ValidationSummary runat="server" ID="vs" />
                            <asp:Label runat="server" ID="ErrorLabel" ForeColor="Red" EnableViewState="false" />
                            </div>
 
    <cc:TPMSFormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" UpdatePanelID="up2"
        DataKeyNames="id,lastupdated" DefaultMode="Edit" Width="100%"
        InsertOrUpdateCheckField="lastupdated">
        <EditItemTemplate>
            <table>
                <tr>
                    <td>

                        <table cellpadding="2" cellspacing="2">
                            <tr>
                                <td align="right">Title<cc3:Popup runat="server" ID="puTitle" Key="PictureTitle" />:
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
                                <td align="right">Menu Text:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMenuText" runat="server" Text='<%# Bind("menutext") %>'
                                        Width="300px" Rows="2" TextMode="MultiLine" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                        ControlToValidate="txtMenuText" ErrorMessage="Menu Text is required">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="txtMenuText" ErrorMessage="Menu Text must be 50 character or less"
                                        ValidationExpression=".{1,50}">*</asp:RegularExpressionValidator>
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Picture<br />
                                    File Name<cc3:Popup runat="server" ID="puPictureFile" Key="PictureFile" />:
                                </td>
                                <td>
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
                                <td align="right">Style:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="lblSurface" runat="server" Text='<%# Bind("style") %>' />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        ControlToValidate="lblSurface" ErrorMessage="Surface must be 50 character or less"
                                        ValidationExpression=".{1,100}">*</asp:RegularExpressionValidator>
                                </td>
                            </tr>


                            <tr>
                                <td align="right">Height<cc3:Popup runat="server" ID="puHeight" Key="Height" />:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="lblHeight" runat="server" Text='<%# Bind("Height","{0:0.000}") %>'
                                        Width="50px" />"
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                ControlToValidate="lblHeight" ErrorMessage="Height is required">*</asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="lblHeight" MinimumValue="0.0"
                                        MaximumValue="32000" Type="Double" Text="*" ErrorMessage="Height must be a positive number" />
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Width<cc3:Popup runat="server" ID="puWidth" Key="Width" />:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="lblWidth" runat="server" Text='<%# Bind("Width","{0:0.000}") %>'
                                        Width="50px" />"
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Weight<cc3:Popup runat="server" ID="puWeight" Key="Weight" />:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="lblWeight" runat="server" Text='<%# Bind("Weight","{0:0.000}") %>'
                                        Width="50px" />
                                    lbs
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                ControlToValidate="lblWeight" ErrorMessage="Weight is required">*</asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="lblWeight" MinimumValue="0.0"
                                        MaximumValue="32000" Type="Double" Text="*" ErrorMessage="Weight must be a positive number" />

                                </td>
                            </tr>

                            <tr>
                                <td align="right">Year<cc3:Popup runat="server" ID="puYear" Key="Year" />:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="lblDate" runat="server" Text='<%# Bind("Date") %>' Width="50px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="lblDate" ErrorMessage="Year is required">*</asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="lblDate" MinimumValue="1900"
                                        MaximumValue='<%# DateTime.Now.Year %>' Type="Integer" Text="*" ErrorMessage="Year must be between 1900 and this year" />
                                </td>
                            </tr>

                            <tr>
                                <td align="right">MetaTags<cc3:Popup runat="server" ID="puMetaTags" Key="MetaTagsPicture" />:
                                </td>
                                <td>
                                    <asp:TextBox ID="lblMegatags" runat="server" Text='<%# Bind("MetaTags") %>'
                                        Rows="2" TextMode="MultiLine" Width="300px" />
                                    <asp:RequiredFieldValidator runat="server" ID="requiredmetatags" ControlToValidate="lblMegatags"
                                        Text="*" ErrorMessage="Metatags are required." />
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Notes<cc3:Popup runat="server" ID="puNotes" Key="Notes" />:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Notes") %>' Rows="2"
                                        TextMode="MultiLine" Width="300px" />
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Description<cc3:Popup runat="server" ID="puDescription" Key="Description" />:
                                </td>
                                <td>
                                    <asp:TextBox ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'
                                        Rows="8" TextMode="MultiLine" Width="300px" />
                                </td>
                            </tr>


                            <tr>
                                <td align="right">Price<cc3:Popup runat="server" ID="puPrice" Key="Price" />:
                                </td>
                                <td align="left">$<asp:TextBox ID="lblPrice" runat="server" Text='<%# Bind("price","{0:0.00}") %>'
                                    Width="75px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="lblPrice" ErrorMessage="Price is required">*</asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="lblPrice" MinimumValue="0"
                                        MaximumValue='1000000' Type="Currency" Text="*" ErrorMessage="Price must be a positive number." />

                                </td>
                            </tr>

                            <tr>
                                <td align="right">Active:
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" Checked='<%# Bind("active") %>' ID="chkActive" />
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2" align="center">
                                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True"
                                        CommandName="Update" Text="Update" /><cc3:Popup runat="server" ID="puUpdate" Key="Update" />
                                    <asp:LinkButton ID="UpdateCancelButton" runat="server"
                                        CausesValidation="False" CommandName="Cancel" Text="Cancel" /><cc3:Popup runat="server" ID="puCancel" Key="CancelUpdate" />
                                    <asp:LinkButton ID="DeleteButton" runat="server"
                                        CausesValidation="False" CommandName="Delete" Text="Delete" /><cc3:Popup runat="server" ID="puDeletePicture" Key="DeletePicture" />
                                    <br />

                                    <asp:HyperLink runat="server"
                                        NavigateUrl="AddBook.aspx"
                                        Text="Add new book" ID="hyp1" />

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
                </div>
            </td>

            <td>
                <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource1"
                    DataKeyNames="id,metatags" Width="100%">
                    <ItemTemplate>

                        <table>
                            <tr>
                                <td class="leftarrow">
                                    <asp:ImageButton runat="server" AlternateText="Previous" ID="btnPrevious" ImageUrl="~/Images/left.jpg" Height="20px" Width="20px" OnCommand="btnPrevious_Command" />
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
                <td class="rightarrow">
                    <asp:ImageButton runat="server" ID="btnNext" ImageUrl="~/Images/right.jpg"
                        BorderStyle="None" OnClick="btnNext_Click" Height="20px" Width="20px" /></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
            </td>
        </tr>



    </table>
    <asp:Label runat="server" ID="lblErrorGrid" ForeColor="Red" EnableViewState="false" />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id,lastupdated" DataSourceID="odsPages">
        <Columns>
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <cc1:GeneratedImage ID="GeneratedImage1" runat="server" ImageHandlerUrl="~/ImageHandlers/ThumbnailImageHandler.ashx">
                        <Parameters>
                            <cc1:ImageParameter Name="ImageUrl" Value='<%# "~/App_Data/" + Eval("PicturePath").ToString() %>' />
                            <cc1:ImageParameter Name="Height" Value="50" />
                        </Parameters>
                    </cc1:GeneratedImage>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="title" HeaderText="title" ReadOnly="true" />
            <asp:CheckBoxField DataField="active" HeaderText="active" ReadOnly="true" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="hpy1" NavigateUrl='<%# "EditBookPage.aspx?id=" + Eval("id").ToString() %>' Text="Edit" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:Button runat="server" ID="btnAddPageView" Text="Add Inside Page" OnClick="btnAddPageView_Click" />
    <asp:ObjectDataSource ID="odsPages" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetByBookId" TypeName="ArtGallery.BookPageDL" DeleteMethod="Delete" OnDeleted="odsPages_Deleted">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_lastupdated" Type="DateTime" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="bookid" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
