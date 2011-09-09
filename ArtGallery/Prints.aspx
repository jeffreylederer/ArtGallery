<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prints.aspx.cs" Inherits="ArtGallery.Prints" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="SpiceLogicPayPalStd" Namespace="SpiceLogic.PayPalCtrlForWPS.Controls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <script src="scripts/jquery-1.4.1.js" type="text/javascript"></script>
   <script src="http://jtruncate.googlecode.com/svn/trunk/jquery.jtruncate.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
<!--

        $().ready(function () {
            $('#description').jTruncate();
        });
    
// -->
</script>
<div class="standardtext"> 
   
    <asp:FormView ID="FormView1" runat="server"  
         DataKeyNames="id" 
        DataSourceID="odsPicture" onprerender="FormView1_PreRender">
        <ItemTemplate>
        <asp:HiddenField runat="server" Value='<%# Eval("Metatags") %>' ID="metatags" />
         
            <table cellpadding="2" cellspacing="2">
            <tr>
            <td align="left">
            Gallery:
            </td>
            <td>
            <asp:Hyperlink ID="Hyperlink1" runat="server" NavigateUrl='<%# "~/GalleryPage.aspx?id=" + Eval("galleryid").ToString() %>'  Text='<%# Eval("Gallery") %>' />
            </td>
            </tr>

                     
           
            <tr>
            <td  align="left">
            Year:
            </td>
            <td>
            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>' />
            </td>
            </tr>

            <tr>
            <td align="left">
            Original Media:
            </td>
            <td>
            <asp:Label ID="lblMedia" runat="server" Text='<%# Eval("Media") %>' />
            </td>
            </tr>

            <tr runat="server" id="descrow" visible='<%# Eval("Description").ToString() != ""  %>' >
            <td align="left"  valign="top">
            Description:
            </td>
            <td>
            <div id="description" style="width:300px;" >
            <%# Eval("Description") %>
            </div>
            </td>
            </tr>
            
                    
            

            </table>
            </ItemTemplate>
    </asp:FormView>
    

     
 <br />    
    <cc2:BuyNowButton ID="btnBuy" 
                        BusinessEmailOrMerchantID="placeholder"
                        runat="server" 
                        ImageUrl="https://www.paypalobjects.com/en_US/i/btn/btn_buynowCC_LG.gif" 
                        Quantity="1" 
                        CurrencyCode="US_Dollar"
                        WeightUnit="Pounds"
                        onpaypal_returned="BuyNowButton_PayPal_Returned" 
       Visible="False">
       </cc2:BuyNowButton>
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="90%"
        DataKeyNames="id" DataSourceID="odsReproduction" 
        onrowcommand="GridView1_RowCommand">
        <Columns>
            <asp:CommandField ButtonType="Button" CausesValidation="False" 
                SelectText="Buy Now" ShowCancelButton="False" ShowSelectButton="True" />
            <asp:TemplateField HeaderText="Description" >
                <ItemTemplate>
                    <div style="text-align:left;" >
                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("Description") %>'  Width="100px"></asp:Label>
                </div>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Size" >
                <ItemTemplate>
                    <div style="text-align:right;" >
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Height").ToString() + " x " + Eval("Width").ToString() %>' Width="85px"></asp:Label>&nbsp;&nbsp;
                </div>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" DataFormatString="{0:0.00}" />
            
        </Columns>
    </asp:GridView>

     <div style="text-align:center;">
             <br /><a href='<%= "PicturePage.aspx?id=" + FormView1.DataKey.Value.ToString() %>'>Back to original art work</a>
     </div>
    
</div>
<div class="picture">
         <asp:FormView ID="FormView2" runat="server" DataSourceID="odsPicture">
        <ItemTemplate>   

            <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' Font-Size="X-Large" /> <br />
            <cc1:GeneratedImage ID="WatermarkedImageGenerator" runat="server" ImageHandlerUrl="~/ImageHandlers/PageImageHandler.ashx">
                <Parameters>
                    <cc1:ImageParameter Name="ImageUrl" Value='<%#  "~/App_Data/" + Eval("PicturePath") %>' />
                    <cc1:ImageParameter Name="Height" Value="500" />
                    <cc1:ImageParameter Name="Width" Value="450" />
                    <cc1:ImageParameter Name="ActualHeight" Value='<%# Eval("Height") %>' />
                    <cc1:ImageParameter Name="ActualWidth" Value='<%# Eval("Width") %>' />
                 </Parameters>
              </cc1:GeneratedImage><br />
               <asp:Label runat="server" ID="lblCopyright" Text='<%#"&#64; " + Eval("copyrightholder").ToString() + " " +  Eval("Date").ToString() %>' ></asp:Label>
        </ItemTemplate>
    </asp:FormView>
</div>

<asp:ObjectDataSource ID="odsPicture" runat="server"  
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetById" TypeName="ArtGallery.PictureDL" >
      <SelectParameters>
         <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
     </SelectParameters>
    </asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsReproduction" runat="server" DeleteMethod="Delete" 
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetByPictureId" TypeName="ArtGallery.ReproductionDL" 
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="pictureid" Type="Int32" />
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="price" Type="Decimal" />
            <asp:Parameter Name="weight" Type="Decimal" />
            <asp:Parameter Name="handling" Type="Decimal" />
            <asp:Parameter Name="width" Type="Decimal" />
            <asp:Parameter Name="height" Type="Decimal" />
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
            <asp:Parameter Name="original_id" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
