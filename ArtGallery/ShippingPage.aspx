<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShippingPage.aspx.cs" Inherits="ArtGallery.ShippingPage" %>

<%@ Register Assembly="SpiceLogicPayPalStd" Namespace="SpiceLogic.PayPalCtrlForWPS.Controls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="pnlProcessing" Visible="false">
        <h1>Processing your order</h1>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlForm">
        <div class="row">
            <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
                Please fill out the form below with the information on the credit card that you 
                intend to use for your purchase. After selecting
                the buy button, you will be taken to a secure PayPal page to obtain 
                additional credit card information.<br />
            </div>

            <table class="table">
            <tr>
                <td>
                    <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <asp:ValidationSummary runat="server" ID="vs" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">Title:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblTitle" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">Description:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblDescription" />
                                    </td>
                                </tr>


                                <tr>
                                    <td align="right">Shipping Method<cc3:Popup runat="server" ID="puShipping" Key="LocalPickup" />:
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="ddlShipping" runat="server">
                                            <asp:ListItem Value="00">Local Pickup</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="03">Ground</asp:ListItem>
                                            <asp:ListItem Value="01">Next Day Air</asp:ListItem>
                                            <asp:ListItem Value="02">Second Day Air</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                </tr>

                                <tr runat="server" id="cellFramed">
                                    <td align="right">Ship in frame<cc3:Popup runat="server" ID="puunframed" Key="Unframed" />:
                                    </td>
                                    <td>

                                        <asp:CheckBox runat="server" ID="chkFramed" Checked="false" />

                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">First Name:
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtFirstName" ErrorMessage="First Name is required">*</asp:RequiredFieldValidator>

                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">Last Name:
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtLastName" ErrorMessage="Last Name is required">*</asp:RequiredFieldValidator>

                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">Address:
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ErrorMessage="Address is required" ControlToValidate="txtAddress">*</asp:RequiredFieldValidator>

                                    </td>
                                </tr>

                                <tr>
                                    <td align="right"></td>
                                    <td>

                                        <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">City:
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtCity" ErrorMessage="City is required">*</asp:RequiredFieldValidator>

                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">State:
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="odsState" DataTextField="StateName" DataValueField="StateAbbr">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="ddlState" ErrorMessage="State is required">*</asp:RequiredFieldValidator>

                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">
                                        <asp:Label runat="server" ID="lblZip" Text="Zipcode" />:
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rqZip" runat="server"
                                            ControlToValidate="txtZipCode" ErrorMessage="Zipcode is required">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="reZip" runat="server"
                                            ControlToValidate="txtZipCode" ErrorMessage="Not acceptable zipcode"
                                            ValidationExpression="\d{5}(-\d{4})?">*</asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RqZipCalc" runat="server" ValidationGroup="calc"
                                            ControlToValidate="txtZipCode" ErrorMessage="Zipcode is required">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="reZipCalc" runat="server"
                                            ControlToValidate="txtZipCode" ErrorMessage="Not acceptable zipcode" ValidationGroup="calc"
                                            ValidationExpression="\d{5}(-\d{4})?">*</asp:RegularExpressionValidator>

                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">Country:
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="US">United States</asp:ListItem>
                                            <asp:ListItem Value="CA">Canada</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                </tr>




                                <tr>
                                    <td align="right">Email&nbsp; Address</td>
                                    <td>

                                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="txtEmail" ErrorMessage="Email address is requiired">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                            ControlToValidate="txtEmail" ErrorMessage="Not acceptable email address"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>

                                    </td>
                                </tr>
                            </table><br /><br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
  
                </td>

                <td>
                    <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="calc" />
                            <asp:FormView ID="fv" runat="server">
                                <ItemTemplate>
                                    <table cellpadding="1px" cellspacing="2px">
                                        <tr>
                                            <td align="right">Price&nbsp;
                                            </td>
                                            <td align="right">
                                                <asp:Label runat="server" ID="lblPrice" Text='<%# Eval("amount", "{0:c2}") %>' />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right">Shipping Container
                                            </td>
                                            <td align="right">
                                                <asp:Label runat="server" ID="Label1" Text='<%# Eval("handling", "{0:c2}") %>' />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right">Shipping
                                            </td>
                                            <td align="right">
                                                <asp:Label runat="server" ID="Label2" Text='<%# Eval("shipping", "{0:c2}") %>' />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right">Tax
                                            </td>
                                            <td align="right">
                                                <asp:Label runat="server" ID="Label3" Text='<%# Eval("tax", "{0:c2}") %>' />
                                            </td>
                                        </tr>

                                        <tr style="border-top-style: solid; border-top-width: 2px; border-top-color: #000000">
                                            <td align="right">Total
                                            </td>
                                            <td align="right">
                                                <asp:Label runat="server" ID="Label4" Text='<%# Eval("total", "{0:c2}") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:FormView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="text-align: center">
                        <asp:Button ID="btnCalculate" runat="server" CausesValidation="True"
                            EnableViewState="False" OnClick="btnCalculate_Click" Text="Calculate Cost"
                            ValidationGroup="calc" />

                    </div>
                </td>
           
        </tr>
        </table>

    </asp:Panel>
    <div class="row">
        <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
            <div style="text-align: center">
                <cc2:BuyNowButton ID="btnBuy"
                    BusinessEmailOrMerchantID="placeholder@abc.com"
                    runat="server"
                    ImageUrl="~/Images/btn_buynowCC_LG.gif"
                    Quantity="1"
                    CurrencyCode="US_Dollar"
                    WeightUnit="Pounds"
                    OnClick="btnBuy_Click"
                    CausesValidation="true"
                    AlternateText="Buy now via PayPal">
                    <PayPalDisplayPage
                        ShippingAddress="ShippingAddressMust" />

                    <PayPalIPN
                        Custom_IPN_Url="~/PayPalNotification.aspx"
                        EnablePageLoadEventInIPNSession="True" />
                </cc2:BuyNowButton>
            </div>
        </div>
    </div>

    <asp:ObjectDataSource ID="odsState" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetByCountry"
        TypeName="ArtGallery.StateNameDL">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCountry" Name="Country"
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
