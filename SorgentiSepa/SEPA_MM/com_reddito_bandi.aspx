<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_reddito_bandi.aspx.vb" Inherits="com_reddito" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self"> </base>
    <title>Reddito Componenti</title>
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
        <input id="Button2" language="javascript" onclick="Chiudi()" style="z-index: 114;
            left: 350px; position: absolute; top: 193px" type="button" 
            value="Chiudi" />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 100; left: 10px; position: absolute;
            top: 29px; width: 84px;">Componente</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" Style="z-index: 102;
            left: 100px; position: absolute; top: 28px" TabIndex="1" Width="307px" 
            Font-Names="arial" Font-Size="8pt">
        </asp:DropDownList>
        <asp:DropDownList onchange="ControllaImporto();MostraSalva();" ID="cmbTipoReddito" runat="server" Style="z-index: 102;
            left: 100px; position: absolute; top: 55px" TabIndex="1" Width="307px" 
            Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="-1">--</asp:ListItem>
            <asp:ListItem Value="0">CUD</asp:ListItem>
            <asp:ListItem Value="1">730</asp:ListItem>
            <asp:ListItem Value="2">UNICO</asp:ListItem>
            <asp:ListItem Value="3">AUTOCERTIFICATO</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList onchange="ControllaAgrari();MostraSalva();" ID="cmbTipoAgrari" runat="server" Style="z-index: 102;
            left: 100px; position: absolute; top: 111px" TabIndex="1" Width="307px" 
            Font-Names="arial" Font-Size="8pt">
            <asp:ListItem Value="-1">--</asp:ListItem>
            <asp:ListItem Value="0">AUTOCERTIFICATO</asp:ListItem>
            <asp:ListItem Value="1">UNICO</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 176px; position: absolute;
            top: 85px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 176px; position: absolute;
            top: 144px" Width="24px">,00</asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 105; left: 201px; position: absolute;
            top: 193px" TabIndex="7" Text="SALVA e Chiudi" />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 9px; position: absolute;
            top: 84px" Width="77px">Reddito IRPEF</asp:Label>
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 9px; position: absolute;
            top: 57px" Width="77px">Tipo Reddito</asp:Label>
        <asp:TextBox ID="txtIRPEF" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="8"
            
            Style="z-index: 107; left: 100px; position: absolute; top: 82px; width: 70px;" 
            TabIndex="5"></asp:TextBox>
        <asp:TextBox ID="txtAGRARI" runat="server" Font-Bold="False"
            Font-Names="arial" Font-Size="8pt" MaxLength="8"
            
            Style="z-index: 108; left: 100px; position: absolute; top: 139px; width: 70px;" 
            TabIndex="6"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 109; left: 9px; position: absolute;
            top: 142px; width: 90px;">Proventi Agrari</asp:Label>
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Height="18px" Style="z-index: 109; left: 9px; position: absolute;
            top: 113px; width: 90px;">Tipo Prov.Agrari</asp:Label>
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 110; left: 206px; position: absolute; top: 85px; width: 200px;"
            Text="(valore Numerico)" Visible="False"></asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 200px; position: absolute; top: 142px"
            Text="(valore Numerico)" Width="175px" Visible="False"></asp:Label>
    
    </div>
    <asp:HiddenField ID="txtRiga" runat="server" />
    <asp:HiddenField ID="txtOperazione" runat="server" />
    </form>
</body>
<script type="text/javascript">
document.getElementById('txtRiga').style.visibility='hidden';
document.getElementById('txtOperazione').style.visibility = 'hidden';

ControllaImporto();
ControllaAgrari();
MostraSalva();

function ControllaImporto() {

    if (document.getElementById('cmbTipoReddito').value == '-1') {
        document.getElementById('Label4').style.visibility = 'hidden';
        document.getElementById('txtIRPEF').style.visibility = 'hidden';
        document.getElementById('Label2').style.visibility = 'hidden';
    }
    else {
        document.getElementById('Label4').style.visibility = 'visible';
        document.getElementById('txtIRPEF').style.visibility = 'visible';
        document.getElementById('Label2').style.visibility = 'visible';
    }
}

function ControllaAgrari() {

    if (document.getElementById('cmbTipoAgrari').value == '-1') {
        document.getElementById('Label5').style.visibility = 'hidden';
        document.getElementById('TxtAGRARI').style.visibility = 'hidden';
        document.getElementById('Label8').style.visibility = 'hidden';
    }
    else {
        document.getElementById('Label5').style.visibility = 'visible';
        document.getElementById('TxtAGRARI').style.visibility = 'visible';
        document.getElementById('Label8').style.visibility = 'visible';
    }
}

function MostraSalva() {
    if (document.getElementById('cmbTipoReddito').value != '-1' || document.getElementById('cmbTipoAgrari').value != '-1') {
        document.getElementById('btnSalva').style.visibility = 'visible';
        if (document.getElementById('L1')) {
            //document.getElementById('L1').style.visibility = 'hidden';
        }
        if (document.getElementById('L2')) {
           // document.getElementById('L2').style.visibility = 'hidden';
        }

    }
    else {
        document.getElementById('btnSalva').style.visibility = 'hidden';
        if (document.getElementById('L1')) {
            document.getElementById('L1').style.visibility = 'hidden';
        }
        if (document.getElementById('L2')) {
            document.getElementById('L2').style.visibility = 'hidden';
        }
    }
}

function Chiudi()
{
window.close();
}

</script>
</html>
