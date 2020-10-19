<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerificaCondomino.aspx.vb" Inherits="CALL_CENTER_VerificaCondomino" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verifica</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Label ID="lblNome" runat="server" Font-Bold="True" Font-Names="arial" 
        Font-Size="9pt" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblDettagli" runat="server" Font-Bold="False" Font-Names="arial" 
        Font-Size="9pt"></asp:Label>
    <br />
    <asp:HiddenField ID="tipo" runat="server" />
    <asp:HiddenField ID="identificativo" runat="server" />
    </form>
</body>
</html>
