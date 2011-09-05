<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopUp.ascx.cs" Inherits="ArtGallery.PopUp" %>
<asp:Image runat="server" ImageUrl="~/Images/question.jpg" Width="15px" Height="15px" BorderStyle="None" ID="Image1" />
<ajax:PopupControlExtender runat="server" ID="PopupControlExtender1" PopupControlID="panel" TargetControlID="Image1"/>
<asp:Panel runat="server" ID="panel" CssClass="popup" ><p><%= Result %></p><input type="button" runat="server" value="Close" id="impClose" /></asp:Panel>
 