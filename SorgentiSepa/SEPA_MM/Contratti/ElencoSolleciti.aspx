<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoSolleciti.aspx.vb" Inherits="Contratti_ElencoSolleciti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Solleciti</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="12pt" Text="Elenco Solleciti"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="arial" 
            Font-Size="10pt"></asp:Label>
        <br />
        <div id="contenitore" 
            
            style="position: absolute; width: 332px; height: 234px; top: 78px; overflow: auto; left: 10px;">
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="arial" 
            Font-Size="10pt"></asp:Label>
        </div>
    </div>
    </form>
    <p>
        <img alt="" src="../NuoveImm/Img_Esci.png" onclick="self.close();" 
            style="position: absolute; top: 319px; left: 260px; cursor: pointer;" /></p>
</body>
</html>
