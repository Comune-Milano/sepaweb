<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_integ.aspx.vb" Inherits="com_integ" %>

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
            left: 340px; position: absolute; top: 134px" type="button" value="Chiudi" />
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 100; left: 192px; position: absolute;
            top: 134px" TabIndex="7" Text="SALVA e Chiudi" />
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="1px" MaxLength="6" Style="left: 203px; position: absolute; top: 134px"
            TabIndex="3" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="10px" MaxLength="6"
            Style="left: 248px; position: absolute; top: 139px" TabIndex="3" Width="5px"></asp:TextBox>
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="#0000C0" Style="z-index: 103; left: 9px; position: absolute; top: 10px"
            Text="Componente Nucleo" Width="209px"></asp:Label>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 10px; position: absolute;
            top: 63px" Width="50px">Componente</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 105;
            left: 85px; position: absolute; top: 61px" TabIndex="1" Width="316px">
        </asp:DropDownList>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 153px; position: absolute;
            top: 95px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtImporto" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 107;
            left: 85px; position: absolute; top: 92px" TabIndex="6"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 11px; position: absolute;
            top: 94px" Width="71px">Importo</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 183px; position: absolute; top: 96px"
            Text="(valorizzare)" Visible="False" Width="213px"></asp:Label>
    
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
