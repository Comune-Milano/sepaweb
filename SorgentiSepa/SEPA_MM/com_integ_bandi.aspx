<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_integ_bandi.aspx.vb" Inherits="com_integ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self"> </base>
    <title>Integrazione ERP</title>
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
        <input id="Button2" language="javascript" onclick="Chiudi()" style="z-index: 110;
            left: 384px; position: absolute; top: 163px" type="button" 
            value="Chiudi" />
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 100; left: 233px; position: absolute;
            top: 163px" TabIndex="4" Text="SALVA e Chiudi" />
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="1px" MaxLength="6" Style="left: 203px; position: absolute; top: 134px"
            TabIndex="3" Width="1px"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Style="z-index: 104; left: 10px; position: absolute;
            top: 28px; height: 20px; width: 71px;">Componente</asp:Label>
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Style="z-index: 104; left: 10px; position: absolute;
            top: 58px; height: 20px; width: 100px;">Contributi/Sussidi</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" Style="z-index: 105;
            left: 123px; position: absolute; top: 26px" TabIndex="1" Width="316px" 
            Font-Names="arial" Font-Size="8pt">
        </asp:DropDownList>
        <asp:DropDownList onchange="ControllaImporto();" ID="cmbContributi" runat="server" Style="z-index: 105;
            left: 123px; position: absolute; top: 57px" TabIndex="2" Width="316px" 
            Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="-1">--</asp:ListItem>
            <asp:ListItem Value="0">AUTOCERTIFICATO</asp:ListItem>
            <asp:ListItem Value="1">EROGAZIONE FSA</asp:ListItem>
            <asp:ListItem Value="2">EROGAZIONE SUSSIDI</asp:ListItem>
            <asp:ListItem Value="3">PROVVIDENZE INV. CIVILI</asp:ListItem>
            <asp:ListItem Value="4">PENS./ASS. SOCIALE</asp:ListItem>
            <asp:ListItem Value="5">ALTRO</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 195px; position: absolute;
            top: 91px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtImporto" runat="server" Font-Bold="False"
            Font-Names="ARIAL" Font-Size="8pt" MaxLength="8" Style="z-index: 107;
            left: 122px; position: absolute; top: 89px; width: 66px;" TabIndex="3"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="ARIAL"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 11px; position: absolute;
            top: 91px" Width="71px">Importo</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 223px; position: absolute; top: 91px"
            Text="(valorizzare)" Visible="False" Width="213px"></asp:Label>
    
    </div>
    <asp:HiddenField ID="txtRiga" runat="server" />
    </form>
</body>
<script type="text/javascript">
document.getElementById('txtRiga').style.visibility='hidden';
document.getElementById('txtOperazione').style.visibility = 'hidden';

ControllaImporto();

function ControllaImporto() {

    if (document.getElementById('cmbContributi').value == '-1') {
        document.getElementById('Label8').style.visibility = 'hidden';
        document.getElementById('txtImporto').style.visibility = 'hidden';
        document.getElementById('Label5').style.visibility = 'hidden';
        document.getElementById('btnSalva').style.visibility = 'hidden';
        if (document.getElementById('L3')) {
            document.getElementById('L3').style.visibility = 'hidden';
        }
    }
    else {
        document.getElementById('Label8').style.visibility = 'visible';
        document.getElementById('txtImporto').style.visibility = 'visible';
        document.getElementById('Label5').style.visibility = 'visible';
        document.getElementById('btnSalva').style.visibility = 'visible';
    }
}

function Chiudi()
{
window.close();
}

</script>
</html>
