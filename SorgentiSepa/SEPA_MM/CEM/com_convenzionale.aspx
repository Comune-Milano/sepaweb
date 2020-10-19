<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_convenzionale.aspx.vb" Inherits="VSA_com_convenzionale" %>

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
        <input id="Button2" language="javascript" onclick="Chiudi()" style="z-index: 138;
            left: 350px; position: absolute; top: 348px" type="button" value="Chiudi" />
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
            top: 204px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 210px; position: absolute;
            top: 152px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 105; left: 211px; position: absolute;
            top: 255px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 213px; position: absolute;
            top: 306px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txt7" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
            Style="z-index: 107; left: 134px; position: absolute; top: 303px" TabIndex="10" Height="18px"></asp:TextBox>
        <asp:TextBox ID="Txt5" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
            Style="z-index: 108; left: 134px; position: absolute; top: 253px" TabIndex="8" Height="18px"></asp:TextBox>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 139; left: 201px; position: absolute;
            top: 348px" TabIndex="11" Text="SALVA e Chiudi" />
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 110; left: 10px; position: absolute;
            top: 203px" Width="104px">Reddito Autonomo</asp:Label>
        <asp:Label ID="Label11" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 111; left: 10px; position: absolute;
            top: 98px" Width="104px">Condizione Prof.</asp:Label>
        <asp:TextBox ID="txt3" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
            Style="z-index: 112; left: 134px; position: absolute; top: 201px" TabIndex="6" Height="18px"></asp:TextBox>
        <asp:TextBox ID="txt1" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
            Style="z-index: 113; left: 134px; position: absolute; top: 149px" TabIndex="4" Height="18px"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 10px; position: absolute;
            top: 150px" Width="116px">Reddito Dipendente</asp:Label>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 115; left: 10px; position: absolute;
            top: 281px" Width="120px">Reddito Dom./Ag./Fab.</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 116; left: 234px; position: absolute; top: 204px"
            Text="(valore Numerico)" Visible="False" Width="135px"></asp:Label>
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 117; left: 235px; position: absolute; top: 150px"
            Text="(valore Numerico)" Visible="False" Width="175px"></asp:Label>
        <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 118; left: 235px; position: absolute; top: 255px"
            Text="(valore Numerico)" Visible="False" Width="175px"></asp:Label>
        <asp:Label ID="Label19" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 211px; position: absolute;
            top: 281px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txt6" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10"
            Style="z-index: 120; left: 134px; position: absolute; top: 278px" TabIndex="9"></asp:TextBox>
        <asp:Label ID="Label20" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 121; left: 10px; position: absolute;
            top: 257px" Width="98px">Em. Occasionali</asp:Label>
        <asp:Label ID="L6" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 122; left: 236px; position: absolute; top: 281px"
            Text="(valore Numerico)" Visible="False" Width="175px"></asp:Label>
        <asp:Label ID="L7" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 236px; position: absolute; top: 306px"
            Text="(valore Numerico)" Visible="False" Width="175px"></asp:Label>
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="6" Style="left: 276px; position: absolute; top: 353px; z-index: 124;"
            TabIndex="3" Width="5px" Height="11px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 229px; position: absolute; top: 352px; z-index: 125;" TabIndex="3" Width="5px" Height="10px"></asp:TextBox>
        <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 126; left: 11px; position: absolute;
            top: 305px" Width="98px">Oneri Deducibili</asp:Label><asp:DropDownList ID="cmbCond" runat="server" CssClass="CssMaiuscolo" Style="z-index: 127;
            left: 134px; position: absolute; top: 96px" TabIndex="2" Width="273px">
            </asp:DropDownList>
        <asp:Label ID="Label12" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 128; left: 10px; position: absolute;
            top: 124px" Width="104px">Professione</asp:Label>
        <asp:DropDownList ID="cmbProf" runat="server" CssClass="CssMaiuscolo" Style="z-index: 129;
            left: 134px; position: absolute; top: 122px" TabIndex="3" Width="273px">
        </asp:DropDownList>
        <asp:Label ID="Label13" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 130; left: 210px; position: absolute;
            top: 178px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txt2" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10"
            Style="z-index: 131; left: 134px; position: absolute; top: 175px" TabIndex="5"></asp:TextBox>
        <asp:Label ID="Label14" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 132; left: 10px; position: absolute;
            top: 177px" Width="98px">Reddito Pensione</asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 133; left: 236px; position: absolute; top: 176px"
            Text="(valore Numerico)" Visible="False" Width="175px"></asp:Label>
        <asp:Label ID="Label16" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 134; left: 210px; position: absolute;
            top: 230px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txt4" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10"
            Style="z-index: 135; left: 134px; position: absolute; top: 228px" TabIndex="7"></asp:TextBox>
        <asp:Label ID="Label17" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 136; left: 10px; position: absolute;
            top: 230px" Width="110px">Em. non Imponibili</asp:Label>
        <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 137; left: 235px; position: absolute; top: 228px"
            Text="(valore Numerico)" Visible="False" Width="175px"></asp:Label>
    
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