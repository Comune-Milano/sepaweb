<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES13.aspx.vb" Inherits="AMMSEPA_Controllo_CancFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cancella</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        &nbsp;<asp:Button ID="Button6" runat="server" style="height: 26px" Text="conferma" />
    
        <br />
        <br />
        <asp:TextBox ID="txtFile" runat="server" Visible="False" Width="706px"></asp:TextBox>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Size="9pt" 
            Text="Esempio ALLEGATI/CONTRATTI/XXX.PDF" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button7" runat="server" Text="Procedi" Visible="False" />
    
    </div>
    </form>
</body>
</html>
