<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES16.aspx.vb" Inherits="AMMSEPA_Controllo_Datafine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        &nbsp;<asp:Button ID="Button6" runat="server" style="height: 26px" Text="conferma" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Codice Contratto"></asp:Label>
        <br />
        <asp:TextBox ID="txtCodice" runat="server" Width="199px" Visible="False"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Procedi" Visible="False" />
    
    </div>
    </form>
</body>
</html>
