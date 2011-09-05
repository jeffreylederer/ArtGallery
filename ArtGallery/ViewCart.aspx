<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="ArtGallery.ViewCart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="document.forms.paypal.submit();">
    <form target="paypal" action="https://www.paypal.com/cgi-bin/webscr" method="post" runat="server" id="paypal">
        <input type="hidden" name="cmd" value="_cart" />
        <input type="hidden" name="business" value="SQNHR9ULL6Q8Y" />
        <input type="hidden" name="display" value="1" />
  </form>
  <script language="javascript" type="text/javascript" >
      window.close();
</script>
</body>
</html>
