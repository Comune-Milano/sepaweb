<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoAllegati.aspx.vb" Inherits="Contratti_ElencoAllegati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
	Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Elenco Allegati</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
            Width="100%"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" 
            Text="Elenco Allegati"></asp:Label><br />
        <br />
        <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Width="100%"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" 
            Width="100%" Visible="False"></asp:Label></div>
    <p>
        &nbsp;</p>
    <p style="text-align: right">
        <asp:ImageButton ID="btnStampa" runat="server" 
            ImageUrl="~/NuoveImm/Img_StampaContratto.png" />
    &nbsp;&nbsp;
        <img style="cursor:pointer" onclick="self.close();" id="imgEsci" alt="esci" src="../NuoveImm/Img_EsciCorto.png" /></p>
    </form>
</body>
</html>