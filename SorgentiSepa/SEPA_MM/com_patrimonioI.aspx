<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_patrimonioI.aspx.vb" Inherits="com_patrimonioI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self"> </base>
    <title>Patrimonio Immobiliare Componenti</title>
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
        <input id="Button2" language="javascript" onclick="Chiudi()" style="z-index: 122;
            left: 355px; position: absolute; top: 226px" type="button" value="Chiudi" />
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="#0000C0" Style="z-index: 100; left: 9px; position: absolute; top: 14px"
            Text="Componente Nucleo" Width="209px"></asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 101; left: 204px; position: absolute;
            top: 226px" TabIndex="7" Text="SALVA e Chiudi" />
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="3px" MaxLength="6" Style="left: 251px; position: absolute;
            top: 226px" TabIndex="3" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="11px" MaxLength="6"
            Style="left: 297px; position: absolute; top: 227px" TabIndex="3"
            Width="5px"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 11px; position: absolute;
            top: 52px" Width="50px">Componente</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 105;
            left: 94px; position: absolute; top: 50px" TabIndex="1" Width="316px">
        </asp:DropDownList>
        <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 12px; position: absolute;
            top: 81px" Width="79px">Tipologia F.</asp:Label>
        <asp:DropDownList ID="cmbTipo" runat="server" CssClass="CssMaiuscolo" Style="z-index: 107;
            left: 95px; position: absolute; top: 79px" TabIndex="2" Width="316px">
            <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
            <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
            <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 13px; position: absolute;
            top: 108px" Width="69px">% Proprietà</asp:Label>
        <asp:TextBox ID="txtPerc" runat="server" Columns="4" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 109;
            left: 95px; position: absolute; top: 107px" TabIndex="3" Width="34px">100</asp:TextBox>
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 110; left: 141px; position: absolute; top: 109px"
            Text="(valorizzare)" Visible="False" Width="245px"></asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 189px; position: absolute; top: 140px"
            Text="(valorizzare)" Visible="False" Width="210px"></asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 112; left: 161px; position: absolute;
            top: 140px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtValore" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 113;
            left: 94px; position: absolute; top: 138px" TabIndex="4"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 14px; position: absolute;
            top: 138px" Width="71px">Valore</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 115; left: 162px; position: absolute;
            top: 170px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="TxtMutuo" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 116;
            left: 94px; position: absolute; top: 167px" TabIndex="5"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 117; left: 15px; position: absolute;
            top: 168px" Width="71px">Mutuo</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 118; left: 190px; position: absolute; top: 169px"
            Text="(valorizzare)" Visible="False" Width="197px"></asp:Label>
        <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 17px; position: absolute;
            top: 197px" Width="50px">Residenza</asp:Label>
        <asp:DropDownList ID="cmbResidenza" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
            left: 94px; position: absolute; top: 196px" TabIndex="6" Width="45px">
            <asp:ListItem Selected="True" Value="0">NO</asp:ListItem>
            <asp:ListItem Value="1">SI</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 150px; position: absolute; top: 198px"
            Text="(valorizzare)" Visible="False" Width="245px"></asp:Label>
    
    </div>
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
