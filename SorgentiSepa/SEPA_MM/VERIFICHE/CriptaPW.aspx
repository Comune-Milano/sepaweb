<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CriptaPW.aspx.vb" Inherits="CriptaPW" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cripta HASH</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        &nbsp;<asp:Button ID="Button6" runat="server" style="height: 26px" Text="conferma" />
    </div>
    <asp:Button ID="Button1" runat="server" Text="AGGIORNA" Visible="False" />
    </form>
</body>
</html>
