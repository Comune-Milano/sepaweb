<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES9.aspx.vb" Inherits="AMMSEPA_Controllo_SISCOM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Esegui</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="CONFERMA" />
        <br />
    
    </div>
    <asp:TextBox ID="TextBox2" runat="server" Height="391px" TextMode="MultiLine" 
        Visible="False" Width="989px"></asp:TextBox>
    <asp:Button ID="Button2" runat="server" Text="ESEGUI" Visible="False" />
    </form>
</body>
</html>
