<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_spese_bandi.aspx.vb" Inherits="com_spese" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self"> </base>
    <title>Spese componenti Nucleo</title>
        <style type="text/css">
    .CssMaiuscolo { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; COLOR:blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 16px; FONT-VARIANT: normal }
	.CssComuniNazioni { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 166px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssPresenta { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 450px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssFamiAbit { FONT-SIZE: 8pt;  WIDTH: 600px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssProv { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 48px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssIndirizzo { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 66px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssLabel { FONT-SIZE: 8pt; COLOR: black; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: times; HEIGHT: 20px;FONT-VARIANT: normal }
	.CssLblValori { FONT-SIZE: 8pt; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 16px; FONT-VARIANT: normal }
	.CssEtichetta { ALIGNMENT: center }
	.CssNuovoMaiuscolo { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; COLOR:blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 16px; FONT-VARIANT: normal }
		</style>
</head>
<script type="text/javascript" src="Funzioni.js"></script>
<body bgcolor="lightsteelblue">
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtDescrizione" runat="server"
            Font-Bold="False" Font-Names="arial" Font-Size="8pt" MaxLength="100"
            
            Style="z-index: 100; left: 86px; position: absolute; top: 84px; width: 238px; height: 65px;" 
            TabIndex="2" TextMode="MultiLine"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 101; left: 12px; position: absolute;
            top: 87px; width: 72px;">Descrizione</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 13px; position: absolute;
            top: 54px; width: 52px;">Importo</asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 151px; position: absolute;
            top: 55px" Width="31px">,00</asp:Label>
        <asp:TextBox ID="txtImporto" runat="server"
            Font-Bold="False" Font-Names="arial" Font-Size="8pt"
            MaxLength="6" Style="z-index: 104; left: 86px; position: absolute; top: 51px; width: 61px;"
            TabIndex="1"></asp:TextBox>
        <asp:Label ID="txtComponente" runat="server" Font-Bold="True"
            Font-Names="arial" Font-Size="10pt" Height="18px" Style="z-index: 105;
            left: 11px; position: absolute; top: 16px" Width="401px"></asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 107; left: 183px; position: absolute; top: 55px; width: 217px;"
            Text="(valorizzare con cifre senza virgola)" Visible="False"></asp:Label>
            <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 107; left: 335px; position: absolute; top: 80px; width: 82px;"
            Text="(valorizzare)" Visible="False"></asp:Label>
        &nbsp;
        <asp:Button ID="Button1" runat="server" Style="z-index: 108; left: 199px; position: absolute;
            top: 171px" TabIndex="3" Text="SALVA e Chiudi" />
        <input id="Button2" language="javascript" onclick="Chiudi()" style="z-index: 111;
            left: 348px; position: absolute; top: 171px" type="button" 
            value="Chiudi" />
    
    </div>
    <asp:HiddenField ID="txtOperazione" runat="server" />
    <asp:HiddenField ID="txtRiga" runat="server" />
    </form>
</body>
<script type="text/javascript">
document.getElementById('txtRiga').style.visibility='hidden';
document.getElementById('txtOperazione').style.visibility='hidden';

function Chiudi()
{
window.close();
}

</script>
</html>
