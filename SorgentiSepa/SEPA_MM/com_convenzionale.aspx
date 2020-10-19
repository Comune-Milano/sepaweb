<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_convenzionale.aspx.vb" Inherits="com_convenzionale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <base target="_self"> </base>
    <title>Reddito Convenzionale Componenti</title>
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
            left: 350px; position: absolute; top: 240px" type="button" value="Chiudi" />
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 100; left: 10px; position: absolute;
            top: 64px" Width="50px">Componente</asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="#0000C0" Style="z-index: 101; left: 7px; position: absolute; top: 11px"
            Text="Redditi Convenzionali" Width="209px"></asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 102;
            left: 100px; position: absolute; top: 64px" TabIndex="1" Width="307px">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 210px; position: absolute;
            top: 96px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 210px; position: absolute;
            top: 127px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 105; left: 211px; position: absolute;
            top: 157px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 212px; position: absolute;
            top: 190px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txt4" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
            Style="z-index: 107; left: 134px; position: absolute; top: 187px" TabIndex="5"></asp:TextBox>
        <asp:TextBox ID="Txt3" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
            Style="z-index: 108; left: 134px; position: absolute; top: 155px" TabIndex="6"></asp:TextBox>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 123; left: 201px; position: absolute;
            top: 240px" TabIndex="7" Text="SALVA e Chiudi" />
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 110; left: 10px; position: absolute;
            top: 93px" Width="104px">Reddito Autonomo</asp:Label>
        <asp:TextBox ID="txt1" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
            Style="z-index: 111; left: 134px; position: absolute; top: 93px" TabIndex="5"></asp:TextBox>
        <asp:TextBox ID="txt2" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
            Style="z-index: 112; left: 134px; position: absolute; top: 124px" TabIndex="6"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 113; left: 10px; position: absolute;
            top: 123px" Width="98px">Reddito Dip./Pen.</asp:Label>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 10px; position: absolute;
            top: 155px" Width="120px">Reddito Dom./Ag./Fab.</asp:Label>
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 115; left: 234px; position: absolute; top: 96px"
            Text="(valore Numerico)" Visible="False" Width="135px"></asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 116; left: 235px; position: absolute; top: 131px"
            Text="(valore Numerico)" Visible="False" Width="175px"></asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 117; left: 235px; position: absolute; top: 158px"
            Text="(valore Numerico)" Visible="False" Width="175px"></asp:Label>
        <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 118; left: 235px; position: absolute; top: 190px"
            Text="(valore Numerico)" Visible="False" Width="175px"></asp:Label>
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="6" Style="left: 249px; position: absolute; top: 240px; z-index: 119;"
            TabIndex="3" Width="5px" Height="11px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 215px; position: absolute; top: 244px; z-index: 120;" TabIndex="3" Width="5px" Height="10px"></asp:TextBox>
        <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 121; left: 10px; position: absolute;
            top: 187px" Width="98px">Oneri Deducibili</asp:Label>
    
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

