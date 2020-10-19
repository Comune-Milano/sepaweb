<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_nucleo.aspx.vb" Inherits="FSA_com_nucleo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"> </base>
    
    <title>Componenti Nucleo</title>
<script language="javascript" type="text/javascript">
<!--


function Button2_onclick() {
window.close();
}



// -->
</script>
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
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <div id="Div1" style="border-right: lightsteelblue 2px solid; border-top: lightsteelblue 2px solid;
            z-index: 191; background-attachment: fixed; left: 2px; border-left: lightsteelblue 2px solid;
            width: 433px; border-bottom: lightsteelblue 2px solid; position: absolute; top: 2px;
            height: 291px; background-color: lightsteelblue">
            <asp:TextBox ID="txtCognome" runat="server" Columns="35" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="25"
                Style="z-index: 100; left: 103px; position: absolute; top: 39px" TabIndex="1"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 101; left: 21px; position: absolute;
                top: 40px" Width="50px">Cognome</asp:Label>
            <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 102; left: 21px; position: absolute;
                top: 67px" Width="31px">Nome</asp:Label>
            <p>
                <asp:TextBox ID="txtNome" runat="server" Columns="35" CssClass="CssMaiuscolo" Font-Bold="False"
                    Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="25"
                    Style="z-index: 103; left: 103px; position: absolute; top: 65px" TabIndex="2"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                    Font-Size="8pt" Height="18px" Style="z-index: 104; left: 21px; position: absolute;
                    top: 93px" Width="69px">Data Nascita</asp:Label>
                <asp:TextBox ID="txtData" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False"
                    Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
                    Style="z-index: 105; left: 103px; position: absolute; top: 92px" TabIndex="3"></asp:TextBox>
            </p>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <p>
                &nbsp;&nbsp;
                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                    ForeColor="#0000C0" Style="z-index: 106; left: 3px; position: absolute; top: 2px"
                    Text="Componente Nucleo" Width="209px"></asp:Label>
                <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 107; left: 281px; position: absolute; top: 40px"
                    Text="(valorizzare)" Visible="False" Width="138px"></asp:Label>
            </p>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <p>
                <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 108; left: 281px; position: absolute; top: 67px"
                    Text="(valorizzare)" Visible="False" Width="136px"></asp:Label>
                <input id="Button2" style="z-index: 129; left: 358px; position: absolute; top: 263px"
                    type="button" value="Chiudi" language="javascript" onclick="return Button2_onclick()" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="Button1" runat="server" Style="z-index: 110; left: 211px; position: absolute;
                    top: 263px" TabIndex="9" Text="SALVA e Chiudi" />
                <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                    Font-Size="8pt" Height="18px" Style="z-index: 111; left: 21px; position: absolute;
                    top: 121px" Width="71px">Cod. Fiscale</asp:Label>
                <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                    Font-Size="8pt" Height="18px" Style="z-index: 112; left: 21px; position: absolute;
                    top: 149px" Width="71px">Gr. Parentela</asp:Label>
                <asp:TextBox ID="txtCF" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False"
                    Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="16"
                    Style="z-index: 113; left: 103px; position: absolute; top: 120px" TabIndex="4"></asp:TextBox>
                <asp:DropDownList ID="cmbParenti" runat="server" CssClass="CssMaiuscolo" Style="z-index: 114;
                    left: 103px; position: absolute; top: 146px" TabIndex="5" Width="316px">
                    <asp:ListItem Value="1">CAPOFAMIGLIA</asp:ListItem>
                    <asp:ListItem Value="2">CONIUGE</asp:ListItem>
                    <asp:ListItem Value="3">FIGLIO/A</asp:ListItem>
                    <asp:ListItem Value="4">GENITORE</asp:ListItem>
                    <asp:ListItem Value="5">FRATELLO/SORELLA</asp:ListItem>
                    <asp:ListItem Value="6">NIPOTE</asp:ListItem>
                    <asp:ListItem Value="7">NIPOTE COLLATERALE</asp:ListItem>
                    <asp:ListItem Value="8">NIPOTE AFFINE</asp:ListItem>
                    <asp:ListItem Value="9">ZIO/A</asp:ListItem>
                    <asp:ListItem Value="10">CUGINO/A</asp:ListItem>
                    <asp:ListItem Value="11">NUORA/GENERO</asp:ListItem>
                    <asp:ListItem Value="12">SUOCERO/A</asp:ListItem>
                    <asp:ListItem Value="13">COGNATO/A</asp:ListItem>
                    <asp:ListItem Value="14">BISCUGINO/A</asp:ListItem>
                    <asp:ListItem Value="15">ALTRO AFFINE</asp:ListItem>
                    <asp:ListItem Value="16">CONVIVENTE</asp:ListItem>
                    <asp:ListItem Value="17">NUBENDO/A</asp:ListItem>
                    <asp:ListItem Value="18">ALTRO PARENTE</asp:ListItem>
                    <asp:ListItem Value="20">NONNO/A</asp:ListItem>
                    <asp:ListItem Value="22">BISNONNO/A</asp:ListItem>
                    <asp:ListItem Value="24">FIGLIASTRO/A</asp:ListItem>
                    <asp:ListItem Value="26">PATRIGNO/MATRIGNA</asp:ListItem>
                    <asp:ListItem Value="28">FRATELLASTRO/SORELLASTRA</asp:ListItem>
                    <asp:ListItem Value="30">ZIO/A AFFINE</asp:ListItem>
                    <asp:ListItem Value="32">PRONIPOTE</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                    Font-Size="8pt" Height="18px" Style="z-index: 115; left: 21px; position: absolute;
                    top: 176px" Width="69px">% Invalidità</asp:Label>
                <asp:TextBox ID="txtInv" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                    Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="z-index: 116;
                    left: 103px; position: absolute; top: 175px" TabIndex="6"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                    Font-Size="8pt" Height="18px" Style="z-index: 117; left: 21px; position: absolute;
                    top: 202px" Width="69px">ASL</asp:Label>
                <asp:TextBox ID="txtASL" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                    Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 118;
                    left: 103px; position: absolute; top: 201px" TabIndex="7"></asp:TextBox>
                <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                    Font-Size="8pt" Height="18px" Style="z-index: 119; left: 21px; position: absolute;
                    top: 229px" Width="71px">Ind. Accomp.</asp:Label>
                <asp:DropDownList ID="cmbAcc" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
                    left: 103px; position: absolute; top: 227px" TabIndex="8" Width="49px">
                </asp:DropDownList>
                <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 121; left: 180px; position: absolute; top: 93px"
                    Text="(valorizzare)" Visible="False" Width="229px"></asp:Label>
                <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 122; left: 226px; position: absolute; top: 122px"
                    Text="(valorizzare)" Visible="False" Width="190px"></asp:Label>
                <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 123; left: 160px; position: absolute; top: 176px"
                    Text="(valorizzare)" Visible="False" Width="243px"></asp:Label>
                <asp:Label ID="L6" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 124; left: 160px; position: absolute; top: 203px"
                    Text="(valorizzare)" Visible="False" Width="250px"></asp:Label>
                <asp:Label ID="L7" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 125; left: 160px; position: absolute; top: 230px"
                    Text="(valorizzare)" Visible="False" Width="248px"></asp:Label>
                <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
                    Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                    MaxLength="6" Style="left: 252px; position: absolute; top: 266px"
                    TabIndex="3" Width="17px" Height="12px"></asp:TextBox>
                <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                    Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 288px; position: absolute; top: 265px" TabIndex="3" Width="11px" Height="12px"></asp:TextBox>
                <asp:TextBox ID="txtProgr" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                    Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 313px; position: absolute; top: 266px" TabIndex="3" Width="11px" Height="11px"></asp:TextBox>
            </p>
        </div>
    </form>
    
</body>
<script type="text/javascript">
document.getElementById('txtRiga').style.visibility='hidden';
document.getElementById('txtProgr').style.visibility='hidden';
document.getElementById('txtOperazione').style.visibility='hidden';
</script>
</html>

