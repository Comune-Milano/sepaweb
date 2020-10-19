<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_detrazioni_bandi.aspx.vb" Inherits="com_detrazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self"> </base>
    <title>Detrazioni</title>
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
        <input id="Button2" language="javascript" onclick="Chiudi()" style="z-index: 112;
            left: 369px; position: absolute; top: 158px" type="button" value="Chiudi" />
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 101; left: 221px; position: absolute;
            top: 158px" TabIndex="7" Text="SALVA e Chiudi" />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 12px; position: absolute;
            top: 22px; width: 82px;">Componente</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 13px; position: absolute;
            top: 52px; width: 95px;">Tipo</asp:Label>
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 13px; position: absolute;
            top: 83px; width: 95px;">Tipo Detrazione</asp:Label>
        <asp:DropDownList onchange="ControlloTipo();" ID="cmbDetrazione" runat="server" Style="z-index: 104;
            left: 116px; position: absolute; top: 79px" TabIndex="2" Width="311px" 
            Font-Names="arial" Font-Size="8pt">
        </asp:DropDownList>
        <asp:DropDownList onchange="ControllaImporto();" ID="cmbTipoReddito" runat="server" Style="z-index: 104;
            left: 116px; position: absolute; top: 51px" TabIndex="2" Width="311px" 
            Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="-1">--</asp:ListItem>
            <asp:ListItem Value="0">CUD</asp:ListItem>
            <asp:ListItem Value="1">730</asp:ListItem>
            <asp:ListItem Value="2">UNICO</asp:ListItem>
            <asp:ListItem Value="3">AUTOCERTIFICATO</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="cmbComponente" runat="server" Style="z-index: 105;
            left: 115px; position: absolute; top: 19px" TabIndex="1" Width="311px" 
            Font-Names="arial" Font-Size="8pt">
        </asp:DropDownList>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 183px; position: absolute;
            top: 115px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtImporto" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="8" Style="z-index: 107;
            left: 115px; position: absolute; top: 113px; width: 62px;" TabIndex="3"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 14px; position: absolute;
            top: 116px" Width="71px">Importo</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 109; left: 212px; position: absolute; top: 115px"
            Text="(valorizzare)" Visible="False" Width="214px"></asp:Label>
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="12px" MaxLength="6" Style="left: 313px; position: absolute; top: 163px"
            TabIndex="3" Width="5px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="11px" MaxLength="6"
            Style="left: 232px; position: absolute; top: 163px" TabIndex="3" Width="5px"></asp:TextBox>
    
    </div>
    </form>
</body>
<script type="text/javascript">
document.getElementById('txtRiga').style.visibility='hidden';
document.getElementById('txtOperazione').style.visibility='hidden';

ControllaImporto();

function ControllaImporto() {

    if (document.getElementById('cmbTipoReddito').value == '-1') {
        document.getElementById('Label8').style.visibility = 'hidden';
        document.getElementById('Label3').style.visibility = 'hidden';
        document.getElementById('Label5').style.visibility = 'hidden';
        document.getElementById('txtImporto').style.visibility = 'hidden';
        document.getElementById('cmbDetrazione').style.visibility = 'hidden';

        document.getElementById('btnSalva').style.visibility = 'hidden';
        if (document.getElementById('L3')) {
            document.getElementById('L3').style.visibility = 'hidden';
        }

        
    }
    else {
        document.getElementById('Label3').style.visibility = 'visible';
        document.getElementById('cmbDetrazione').style.visibility = 'visible';
    }
}


function ControlloTipo() {
    if (document.getElementById('cmbDetrazione').value == '3') {
        document.getElementById('btnSalva').style.visibility = 'hidden';
        document.getElementById('Label8').style.visibility = 'hidden';
        document.getElementById('Label5').style.visibility = 'hidden';
        document.getElementById('txtImporto').style.visibility = 'hidden';
        if (document.getElementById('L3')) {
            document.getElementById('L3').style.visibility = 'hidden';
        }
    }
    else {
        document.getElementById('btnSalva').style.visibility = 'visible';
        document.getElementById('Label8').style.visibility = 'visible';
        document.getElementById('Label5').style.visibility = 'visible';
        document.getElementById('txtImporto').style.visibility = 'visible';
    }
}

function Chiudi()
{
window.close();
}

</script>
</html>
