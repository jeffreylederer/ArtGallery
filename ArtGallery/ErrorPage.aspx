<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="ArtGallery.ErrorPage"  EnableViewState="false"  Title="Processing Error"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Page</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
    <h1>Proccessing Error</h1><br />
    An error has occurred when proccessing your request. Click <a href="~/Default.aspx">here</a> to return to the site.
    <br />Error ID: <asp:Label runat="server" ID="lblError" />
    </div>
    </form>
</body>
</html>
