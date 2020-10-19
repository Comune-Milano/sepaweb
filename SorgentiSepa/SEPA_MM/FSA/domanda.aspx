<%@ Page Language="VB" AutoEventWireup="false" CodeFile="domanda.aspx.vb" Inherits="FSA_domanda" %>
<%@ Reference Control="~/Dom_Richiedente.ascx" %>
<%@ Reference Control="~/Dom_Dichiara_FSA.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Dom_Dichiara" Src="../Dom_Dichiara_FSA.ascx" %>
<%@ Register Src="../Dom_Requisiti_FSA.ascx" TagName="Dom_Requisiti" TagPrefix="uc6" %>

<%@ Register Src="../Dom_Alloggio_FSA.ascx" TagName="Dom_Abitative_1" TagPrefix="uc3" %>
<%@ Register Src="../Dom_Abitative_2.ascx" TagName="Dom_Abitative_2" TagPrefix="uc4" %>
<%@ Register Src="../Dom_Note_Cert.ascx" TagName="Note" TagPrefix="uc5" %>

<%@ Register Src="../Dom_Contratto_FSA.ascx" TagName="Dom_Contratto" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Dom_Richiedente" Src="../Dom_Richiedente.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<script type="text/javascript">
var Uscita;
Uscita=0;



function $onkeydown() 
{  

if (event.keyCode==8) 
      {  
      //alert('Questo tasto non può essere usato!');
      event.keyCode=0;
      }  
}

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Domanda</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<style type="text/css">.CssMaiuscolo { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 16px; FONT-VARIANT: normal }
	.CssComuniNazioni { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 166px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssPresenta { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 450px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssFamiAbit { FONT-SIZE: 8pt;  WIDTH: 600px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssProv { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 48px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssIndirizzo { FONT-SIZE: 8pt; TEXT-TRANSFORM: uppercase; WIDTH: 66px; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 20px; FONT-VARIANT: normal }
	.CssLabel { FONT-SIZE: 8pt; COLOR: black; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: times; FONT-VARIANT: normal }
	.CssLblValori { FONT-SIZE: 8pt; COLOR: blue; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-FAMILY: arial; HEIGHT: 16px; FONT-VARIANT: normal }
	.CssEtichetta { ALIGNMENT: center }
		</style>

<script language="javascript" type="text/javascript">
<!--

function window_onbeforeunload() {
aa.close();
if (document.getElementById('H1').value==1) {
event.returnValue = "Attenzione...Uscire dalla Domanda utilizzando il pulsante ESCI!! In caso contrario la domanda VERRA' BLOCCATA E NON SARA' PIU' POSSIBILE MODIFICARE!";
}
}

// -->
</script>
	


</head>
<script language="javascript" type="text/javascript" for="window" event="onbeforeunload">
<!--

if (document.getElementById('H1').value==1) {
    return window_onbeforeunload()
}
// -->
</script>
<script type="text/javascript" src="Funzioni.js"></script>



<script type="text/javascript">
    var win=null;
    LeftPosition=(screen.width) ? (screen.width-250)/2 :0 ;
    TopPosition=(screen.height) ? (screen.height-150)/2 :0;
    LeftPosition=LeftPosition;
    TopPosition=TopPosition;
    
    aa=window.open('loading.htm','','height=150,top='+TopPosition+',left='+LeftPosition+',width=250');
</script>

<script type="text/javascript">
function Indici(){

window.open("indici.aspx?" + document.getElementById('txtIndici').value ,"","top=0,left=0,width=490,height=490,resizable=no,menubar=no,toolbar=no,scrollbars=no");

}

function Riassunto(){

window.open("Riassunto.aspx?ID=" + document.getElementById('txtIndici').value ,"","");

}
</script>
	<body onload="return AggTabDom(document.getElementById('txtTab').value,document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" bgColor="#f2f5f1">
	<script type="text/javascript">
document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;

            <br />
            <br />
            <br />
            <br />
            <asp:imagebutton id="btnNote" style="Z-INDEX: 100; LEFT: 460px; POSITION: absolute; TOP: 391px" runat="server" ImageUrl="..\p_menu\NOTE_0.gif" Height="21px" Width="42px" CausesValidation="False" TabIndex="7" Visible="False"></asp:imagebutton>
            &nbsp;
                <asp:imagebutton id="btnAbitative2" style="Z-INDEX: 101; LEFT: 312px; POSITION: absolute; TOP: 390px" runat="server" ImageUrl="..\p_menu\ABIT2_0.gif" Height="21px" Width="76px" CausesValidation="False" TabIndex="5" Visible="False"></asp:imagebutton><asp:imagebutton id="btnAbitative1" style="Z-INDEX: 102; LEFT: 234px; POSITION: absolute; TOP: 391px" runat="server" ImageUrl="..\p_menu\ABIT1_0.gif" Height="21px" Width="76px" CausesValidation="False" TabIndex="4" Visible="False"></asp:imagebutton><asp:imagebutton id="btnFamiliari" style="Z-INDEX: 103; LEFT: 168px; POSITION: absolute; TOP: 399px" runat="server" ImageUrl="..\p_menu\FAM_0.gif" Height="21px" Width="64px" CausesValidation="False" TabIndex="3" Visible="False"></asp:imagebutton><asp:imagebutton id="btnDichiara" style="Z-INDEX: 104; LEFT: 103px; POSITION: absolute; TOP: 392px" runat="server" ImageUrl="..\p_menu\DICH_0.gif" Height="21px" Width="63px" CausesValidation="False" TabIndex="2" Visible="False"></asp:imagebutton><asp:label id="llISEE" style="Z-INDEX: 105; LEFT: 490px; POSITION: absolute; TOP: 64px" runat="server" Width="70px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FFC080" CssClass="CssLblValori" Font-Bold="True">0</asp:label><asp:label id="lblPGDic" style="Z-INDEX: 106; LEFT: 323px; POSITION: absolute; TOP: 64px" runat="server" Width="126px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FFC080" CssClass="CssLblValori">pg</asp:label><asp:label id="Label4" style="Z-INDEX: 107; LEFT: 454px; POSITION: absolute; TOP: 64px" runat="server" Height="18px" Width="31px" CssClass="CssLabel" Font-Bold="True">ISEE</asp:label>
            <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
                Style="z-index: 107; left: 365px; position: absolute; top: 484px" 
                Width="64px">CARICO A:</asp:Label>
                <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
                
                
                Style="z-index: 107; left: 303px; position: absolute; top: 455px; width: 129px;">DETRAZIONI CANONE:</asp:Label>
                <asp:textbox id="txtDetrazioniCanone" 
                style="Z-INDEX: 112; LEFT: 434px; POSITION: absolute; TOP: 452px; text-align: right" 
                runat="server" MaxLength="10" Width="68px" TabIndex="100" Font-Names="arial" 
                Font-Size="8pt">0</asp:textbox>
                <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
                
                
                
                Style="z-index: 107; left: 505px; position: absolute; top: 455px; width: 129px;">Euro (Valori interi)</asp:Label>
            &nbsp;
            &nbsp;&nbsp;
            <asp:label id="Label3" style="Z-INDEX: 108; LEFT: 275px; POSITION: absolute; TOP: 64px" runat="server" Height="18px" Width="43px" CssClass="CssLabel" Font-Bold="True">N. Dich.</asp:label><asp:label id="Label2" style="Z-INDEX: 109; LEFT: 172px; POSITION: absolute; TOP: 63px" runat="server" Height="18px" Width="33px" CssClass="CssLabel" Font-Bold="True">Data</asp:label><asp:label id="Label1" style="Z-INDEX: 110; LEFT: 4px; POSITION: absolute; TOP: 64px" runat="server" Height="18px" Width="31px" CssClass="CssLabel" Font-Bold="True">PG N.</asp:label><asp:label id="lblPG" style="Z-INDEX: 111; LEFT: 66px; POSITION: absolute; TOP: 64px" runat="server" Width="66px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FFC080" CssClass="CssLblValori">0000000000</asp:label><asp:textbox id="txtDataPG" style="Z-INDEX: 112; LEFT: 202px; POSITION: absolute; TOP: 62px" runat="server" CssClass="CssMaiuscolo" Columns="10" MaxLength="10" Width="68px" TabIndex="1"></asp:textbox><asp:imagebutton id="btnRichiedente" style="Z-INDEX: 113; LEFT: 16px; POSITION: absolute; TOP: 392px" runat="server" ImageUrl="..\p_menu\RICH_0.gif" Height="21px" Width="85px" CausesValidation="False" TabIndex="1" Visible="False"></asp:imagebutton><uc1:dom_richiedente id="Dom_Richiedente1" runat="server" Visible="true"></uc1:dom_richiedente><uc1:dom_dichiara id="Dom_Dichiara1" runat="server" Visible="true"></uc1:dom_dichiara>
            <asp:Label ID="lblSPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
                BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 114;
                left: 38px; position: absolute; top: 64px" Width="26px">06-1</asp:Label>
            <asp:Label ID="Label7" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
                BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 115;
                left: 134px; position: absolute; top: 64px" Width="31px">F205</asp:Label>
            &nbsp;
            <uc2:Dom_Contratto ID="Dom_Contratto1" runat="server" Visible="true" />
            <uc3:Dom_Abitative_1 ID="Dom_Abitative_1_1" runat="server" Visible="true" />
            <uc4:Dom_Abitative_2 ID="Dom_Abitative_2_1" runat="server" Visible="true" />
            <uc5:Note ID="Note1" runat="server" Visible="true" />
            &nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnRequisiti" runat="server" CausesValidation="False" Height="21px"
                ImageUrl="..\p_menu\REC_0.gif" Style="z-index: 116; left: 391px; position: absolute;
                top: 391px" Width="66px" TabIndex="6" Visible="False" />
            <uc6:Dom_Requisiti ID="Dom_Requisiti1" runat="server" Visible="true" /><asp:ImageButton ID="imgAttendi" runat="server" ImageUrl="../IMG/A1.gif" style="z-index: 117; left: 295px; position: absolute; top: 247px" Visible="False" />
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
                width: 674px; position: absolute; top: 0px">
                <tr>
                    <td style="width: 670px; text-align: right">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Domanda&nbsp;</strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <asp:Image ID="imgAnagrafe" runat="server" ImageUrl="~/NuoveImm/Img_Anagrafe.png"
                Style="z-index: 127; left: 267px; cursor: pointer; position: absolute; top: 29px" ToolTip="Anagrafe della popolazione" />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="lblIdDomanda" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblIdDichiarazione" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="ProgrComponente" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblBando" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblIdBando" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                Font-Names="Arial" Font-Size="8pt" Height="31px" Width="368px" Visible="False" style="z-index: 119; left: 257px; position: absolute; top: 204px">
                <PagerStyle Mode="NumericPages" />
                <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                <Columns>
                    <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="PERC_INVAL" HeaderText="INVALIDITA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INDENNITA_ACC" HeaderText="ACC"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
            <img id="i1" src="../p_menu/RICH_0.gif" style="z-index: 135; left: 10px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('1',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
            <img id="i2" src="../p_menu/DICH_0.gif" style="z-index: 129; left: 97px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('2',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
            <img id="i3" src="../p_menu/CON_0.gif" style="z-index: 130; left: 162px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('3',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
            <img id="i4" src="../p_menu/ALL_0.gif" style="z-index: 131; left: 228px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('4',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
            <img id="i5" src="../p_menu/ABIT2_0.gif" style="z-index: 132; left: 559px; position: absolute;
                top: 155px" language="javascript" 
                onclick="return AggTabDom('5',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
            <img id="i6" src="../p_menu/REC_0.gif" style="z-index: 133; left: 315px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('6',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
            <img id="i7" src="../p_menu/NOTE_FSA_0.gif" style="z-index: 134; left: 383px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('7',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
            &nbsp;&nbsp;
            <asp:TextBox ID="txtIndici" runat="server" Style="z-index: 120; left: 16px; position: absolute;
                top: 399px"></asp:TextBox>
            &nbsp;
                        <asp:Label ID="LBLENTE" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
                            Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 123;
                            left: 12px; position: absolute; top: 446px" Text="VISUALIZZA INDICI" ToolTip="Ente che ha inserito la domanda"
                            Visible="False" Width="130px"></asp:Label>
            <asp:Label ID="lblEventi" runat="server" BackColor="#C0FFC0" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0"
                Style="z-index: 123; left: 12px; position: absolute; top: 428px; cursor: pointer; text-align: center;" Text="VIS. EVENTI"
                ToolTip="Click qui per visualizzare la lista degli eventi!" Width="94px"></asp:Label>
            <asp:Label ID="LBLDIFFICOLTA" runat="server" BackColor="Red" 
                BorderStyle="Solid" BorderWidth="1px"
                Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="White" Style="z-index: 123;
                left: 10px; position: absolute; top: 474px; text-align: center" Text="STATO DI GRAVE DIFFICOLTA'"
                ToolTip="Ente che ha inserito la domanda" Visible="False" Width="211px"></asp:Label>
            <asp:Label ID="lblRegionale" runat="server" BackColor="#C0FFC0" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0"
                Style="z-index: 123; left: 109px; position: absolute; top: 428px" Text="C. Regionale:"
                Width="150px"></asp:Label>
            <asp:Label ID="lblComunale" runat="server" BackColor="#C0FFC0" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0"
                Style="z-index: 123; left: 265px; position: absolute; top: 428px" Text="C. Comunale:"
                Width="150px"></asp:Label>
            <asp:Label ID="lblTotale" runat="server" BackColor="#C0FFC0" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0"
                Style="z-index: 123; left: 421px; position: absolute; top: 428px" Text="Totale:"
                Width="120px"></asp:Label>
            <asp:Label ID="lblAbbattimento" runat="server" BackColor="#C0FFC0" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0"
                Style="z-index: 123; left: 546px; position: absolute; top: 428px" Width="105px"></asp:Label>
                        <asp:Label ID="Label10" runat="server" Style="z-index: 124; left: 14px; position: absolute;
                            top: 504px" Width="634px" Font-Names="arial" Font-Size="8pt"></asp:Label>
                        <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" OnClientClick="aa.close();document.getElementById('H1').value=0;" style="z-index: 125; left: 435px; position: absolute; top: 29px" ToolTip="Esci" />
                        <asp:ImageButton ID="btnSalva" runat="server" 
                ImageUrl="~/NuoveImm/Img_Salva.png" 
                OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" 
                style="z-index: 126; left: 9px; position: absolute; top: 29px; height: 12px; right: 1450px;" 
                ToolTip="Salva" /><asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png" Enabled="False" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" style="z-index: 127; left: 77px; position: absolute; top: 29px" ToolTip="Elabora e Stampa" /><asp:ImageButton ID="imgRiassunto" runat="server" ImageUrl="~/NuoveImm/Img_Correla.png" style="z-index: 127; left: 199px; position: absolute; top: 29px" ToolTip="Correlazioni" />
                        <img src="../NuoveImm/Img_Indici.png" id="IMG1" language="javascript" onclick="Indici()" style="cursor: pointer; z-index: 136; left: 146px; position: absolute; top: 29px;" alt="Indici" />
            &nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="chDaLiquidare" runat="server" Font-Bold="True" Font-Names="arial"
                Font-Size="8pt" Style="left: 570px; position: absolute; top: 61px" Text="Da Liquidare"
                Width="92px" />
            <asp:CheckBox ID="ChMandato" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                Style="left: 570px; position: absolute; top: 81px" Text="Mandato Eff." Width="92px" />
            <asp:DropDownList ID="cmbOperatore" runat="server" CssClass="CssPresenta" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" Height="83px"
                Style="z-index: 103; left: 434px; position: absolute; top: 481px" TabIndex="8"
                Width="217px">
            </asp:DropDownList>
            <asp:HiddenField ID="txtTab" runat="server" />
            <asp:HiddenField ID="H1" runat="server" Value="0" />
            <asp:HiddenField ID="H2" runat="server" Value="0" />
        </form>
	</body>
	<script type="text/javascript">
	document.getElementById('txtIndici').style.visibility='hidden';
	aa.close();
	</script>
</html>

