<%@ Reference Control="~/Dom_Richiedente.ascx" %>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="domanda.aspx.vb" Inherits="CAMBI_domanda" %>

<%@ Register Src="../Dom_Dichiara_Cambi.ascx" TagName="Dom_Dichiara_Cambi" TagPrefix="uc8" %>
<%@ Register Src="../Dom_Alloggio_ERP.ascx" TagName="Dom_Alloggio_ERP" TagPrefix="uc7" %>

<%@ Register Src="../Dom_Requisiti_Cambi.ascx" TagName="Dom_Requisiti" TagPrefix="uc6" %>

<%@ Register Src="../Dom_Abitative_1.ascx" TagName="Dom_Abitative_1" TagPrefix="uc3" %>
<%@ Register Src="../Dom_Abitative_2.ascx" TagName="Dom_Abitative_2" TagPrefix="uc4" %>
<%@ Register Src="../Note.ascx" TagName="Note" TagPrefix="uc5" %>

<%@ Register Src="../Dom_Familiari.ascx" TagName="Dom_Familiari" TagPrefix="uc2" %>
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

window.open("indici.aspx?" + document.getElementById('txtIndici').value ,"","top=0,left=0,width=490,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no");

}

function Verifica()
{
document.getElementById('Verifica').style.visibility='visible';
}

function IMG3_onclick() {
document.getElementById('Verifica').style.visibility='hidden';
}

</script>
	<body onload="return AggTabDom(document.getElementById('txtTab').value,document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style);" bgColor="#f2f5f1">
	<script type="text/javascript">
document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server">
            &nbsp;
            <br />
            <br />
            <br />
            <asp:Image ID="imgAnagrafe" runat="server" ImageUrl="~/NuoveImm/Img_Anagrafe.png"
                Style="z-index: 127; left: 328px; cursor: pointer; position: absolute; top: 29px" ToolTip="Anagrafe della popolazione" /><asp:Image ID="IMGPREFERENZE" runat="server" ImageUrl="~/NuoveImm/ImgPreferenze.png"
                Style="z-index: 127; left: 381px; cursor: pointer; position: absolute; top: 29px" ToolTip="Preferenze Utente" Visible="False" />
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
            <asp:imagebutton id="btnNote" style="Z-INDEX: 100; LEFT: 460px; POSITION: absolute; TOP: 391px" runat="server" ImageUrl="..\p_menu\NOTE_0.gif" Height="21px" Width="42px" CausesValidation="False" TabIndex="7" Visible="False"></asp:imagebutton>
            &nbsp;
                <asp:imagebutton id="btnAbitative2" style="Z-INDEX: 101; LEFT: 312px; POSITION: absolute; TOP: 390px" runat="server" ImageUrl="..\p_menu\ABIT2_0.gif" Height="21px" Width="76px" CausesValidation="False" TabIndex="5" Visible="False"></asp:imagebutton><asp:imagebutton id="btnAbitative1" style="Z-INDEX: 102; LEFT: 234px; POSITION: absolute; TOP: 391px" runat="server" ImageUrl="../p_menu\ABIT1_0.gif" Height="21px" Width="76px" CausesValidation="False" TabIndex="4" Visible="False"></asp:imagebutton><asp:imagebutton id="btnFamiliari" style="Z-INDEX: 103; LEFT: 168px; POSITION: absolute; TOP: 399px" runat="server" ImageUrl="../p_menu\FAM_0.gif" Height="21px" Width="64px" CausesValidation="False" TabIndex="3" Visible="False"></asp:imagebutton><asp:imagebutton id="btnDichiara" style="Z-INDEX: 104; LEFT: 103px; POSITION: absolute; TOP: 392px" runat="server" ImageUrl="../p_menu\DICH_0.gif" Height="21px" Width="63px" CausesValidation="False" TabIndex="2" Visible="False"></asp:imagebutton><asp:label id="lblISBAR" style="Z-INDEX: 105; LEFT: 522px; POSITION: absolute; TOP: 64px" runat="server" Width="70px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FFC080" CssClass="CssLblValori" Font-Bold="True">0</asp:label><asp:label id="lblPGDic" style="Z-INDEX: 106; LEFT: 325px; POSITION: absolute; TOP: 64px" runat="server" Width="126px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FFC080" CssClass="CssLblValori">pg</asp:label><asp:label id="Label4" style="Z-INDEX: 107; LEFT: 458px; POSITION: absolute; TOP: 64px" runat="server" Height="18px" Width="60px" CssClass="CssLabel" Font-Bold="True">ISBARC/R</asp:label>
            <asp:Label ID="lblPosizione" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                ForeColor="White" Height="18px" Style="z-index: 107; left: 77px; position: absolute;
                top: 447px; background-color: red; text-align: center" Visible="False" Width="493px">POSIZIONE IN GRADUATORIA</asp:Label>
            <asp:label id="Label3" style="Z-INDEX: 108; LEFT: 277px; POSITION: absolute; TOP: 64px" runat="server" Height="18px" Width="43px" CssClass="CssLabel" Font-Bold="True">N. Dich.</asp:label><asp:label id="Label2" style="Z-INDEX: 109; LEFT: 172px; POSITION: absolute; TOP: 63px" runat="server" Height="18px" Width="33px" CssClass="CssLabel" Font-Bold="True">Data</asp:label><asp:label id="Label1" style="Z-INDEX: 110; LEFT: 4px; POSITION: absolute; TOP: 64px" runat="server" Height="18px" Width="31px" CssClass="CssLabel" Font-Bold="True">PG N.</asp:label><asp:label id="lblPG" style="Z-INDEX: 111; LEFT: 66px; POSITION: absolute; TOP: 64px" runat="server" Width="66px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FFC080" CssClass="CssLblValori">0000000000</asp:label><asp:textbox id="txtDataPG" style="Z-INDEX: 112; LEFT: 202px; POSITION: absolute; TOP: 62px" runat="server" CssClass="CssMaiuscolo" Columns="10" MaxLength="10" Width="68px" TabIndex="1"></asp:textbox><asp:imagebutton id="btnRichiedente" style="Z-INDEX: 113; LEFT: 16px; POSITION: absolute; TOP: 392px" runat="server" ImageUrl="../p_menu\RICH_0.gif" Height="21px" Width="85px" CausesValidation="False" TabIndex="1" Visible="False"></asp:imagebutton><uc1:dom_richiedente id="Dom_Richiedente1" runat="server" Visible="true"></uc1:dom_richiedente>
            <asp:Label ID="lblSPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
                BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 114;
                left: 38px; position: absolute; top: 64px" Width="26px">06-1</asp:Label>
            <asp:Label ID="Label7" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
                BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 115;
                left: 134px; position: absolute; top: 64px" Width="31px">F205</asp:Label>
            &nbsp;
            <uc7:Dom_Alloggio_ERP ID="Dom_Alloggio_ERP1" runat="server" Visible="true"/>
            <uc2:Dom_Familiari ID="Dom_Familiari1" runat="server" Visible="true" />
            <uc3:Dom_Abitative_1 ID="Dom_Abitative_1_1" runat="server" Visible="true" />
            <uc4:Dom_Abitative_2 ID="Dom_Abitative_2_1" runat="server" Visible="true" />
            <uc5:Note ID="Note1" runat="server" Visible="true" />
            
            &nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnRequisiti" runat="server" CausesValidation="False" Height="21px"
                ImageUrl="../p_menu\REC_0.gif" Style="z-index: 116; left: 391px; position: absolute;
                top: 391px" Width="66px" TabIndex="6" Visible="False" />
            <uc6:Dom_Requisiti ID="Dom_Requisiti_Cambi1" runat="server" Visible="true" />
            <asp:ImageButton ID="imgAttendi" runat="server" ImageUrl="../IMG/A1.gif" style="z-index: 117; left: 295px; position: absolute; top: 247px" Visible="False" />
            <uc8:Dom_Dichiara_Cambi ID="Dom_Dichiara_Cambi1" runat="server" />
            <br />
            <br />
           <br />
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
                top: 87px" language="javascript" onclick="return AggTabDom('1',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style);" />
            <img id="i1_1" src="../p_menu/ALL_0.gif" style="z-index: 135; left: 97px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('8',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style);" />
            <img id="i2" src="../p_menu/DICH_0.gif" style="z-index: 129; left: 184px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('2',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style);" />
            <img id="i3" src="../p_menu/FAM_0.gif" style="z-index: 130; left: 249px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('3',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style);" />
            <img id="i4" src="../p_menu/ABIT1_0.gif" style="z-index: 131; left: 315px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('4',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style);" />
            <img id="i5" src="../p_menu/ABIT2_0.gif" style="z-index: 132; left: 393px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('5',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style);" />
            <img id="i6" src="../p_menu/REC_0.gif" style="z-index: 133; left: 472px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('6',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style);" />
            <img id="i7" src="../p_menu/NOTE_0.gif" style="z-index: 134; left: 541px; position: absolute;
                top: 87px" language="javascript" onclick="return AggTabDom('7',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style);" />
            &nbsp;&nbsp;
            <asp:TextBox ID="txtIndici" runat="server" Style="z-index: 120; left: 16px; position: absolute;
                top: 399px"></asp:TextBox>
            &nbsp;
                        <asp:Label ID="LBLENTE" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
                            Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 123;
                            left: 11px; position: absolute; top: 428px" Text="VISUALIZZA INDICI" ToolTip="Ente che ha inserito la domanda"
                            Visible="False" Width="157px"></asp:Label>
            <asp:Label ID="Label12" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
                Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 123;
                left: 173px; cursor: pointer; position: absolute; top: 428px" Text="VISUALIZZA EVENTI"
                ToolTip="Visualizza tutti gli eventi di questa domanda" Visible="False" Width="109px"></asp:Label>
                        <asp:Label ID="Label10" runat="server" Style="z-index: 124; left: 11px; position: absolute;
                            top: 468px" Width="639px" Font-Names="arial" Font-Size="8pt" Font-Bold="True"></asp:Label>
                        <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" OnClientClick="aa.close();document.getElementById('H1').value=0;" style="z-index: 125; left: 477px; position: absolute; top: 29px" ToolTip="Esci" />
                        <asp:ImageButton ID="btnSalva" runat="server" 
                ImageUrl="~/NuoveImm/Img_Salva.png" 
                OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" 
                style="z-index: 126; left: 9px; position: absolute; top: 29px; right: 1450px;" 
                ToolTip="Salva" /><asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png" Enabled="False" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" style="z-index: 127; left: 77px; position: absolute; top: 29px" ToolTip="Elabora e Stampa" /><asp:ImageButton ID="imgRiassunto" runat="server" ImageUrl="~/NuoveImm/Img_Riassunto.png" style="z-index: 127; left: 195px; position: absolute; top: 29px" ToolTip="Riassunto Domanda" /><img src="../NuoveImm/Img_Indici.png" id="IMG1" language="javascript" onclick="Indici()" style="cursor: pointer; z-index: 136; left: 145px; position: absolute; top: 29px;" alt="Indici" />
                        <img src="../NuoveImm/Img_Verifica.png" id="Img2" language="javascript" onclick="Verifica()" style="cursor: pointer; z-index: 136; left: 277px; position: absolute; top: 29px;" alt="Verifica Mantenimento requisiti" />
            <asp:Label ID="Label5" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
                Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 123;
                left: 287px; position: absolute; top: 428px; text-align: center;" Text="DOMANDA IN VERIFICA REQUISITI"
                ToolTip="Stato della verifica dei requisiti" Visible="False" Width="363px"></asp:Label>           
            <div id="Verifica" 
                
                style="left: 0px; width: 673px; position: absolute; top: 0px;
                height: 524px; z-index: 500; background-image: url('../NuoveImm/SfondoVerifica.png');">
                <asp:Button ID="btnIniziaVerifica" runat="server" Style="left: 30px; position: absolute; top: 150px; z-index: 100;"
                    Text="INIZIA LA PROCEDURA DI VERIFICA REQUISITI" Visible="False" 
                    Width="315px" /><asp:Button ID="btnFineVerifica" runat="server" Style="left: 30px; position: absolute; top: 244px; z-index: 101;"
                    Text="FINE PROCEDURA DI VERIFICA REQUISITI" Visible="False" 
                    Width="315px" />
                <asp:Button ID="btnInviaAss" runat="server" Style="left: 30px; position: absolute; top: 387px; z-index: 102;"
                    Text="INVIA DOMANDA IN ASSEGNAZIONE" Visible="False" Width="314px" />
                <asp:Label ID="Label6" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Navy"
                    
                    
                    Style="left: 351px; position: absolute; top: 147px; z-index: 103; width: 283px;" Text="Premi questo pulsante per iniziare a procedura di verifica Requisiti. Solo dopo aver avviato tale procedura sarà possibile aprire e modificare la dichiarazione correlata."
                    Visible="False"></asp:Label>
                <asp:Label ID="Label8" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Navy"
                    
                    Style="left: 352px; position: absolute; top: 243px; z-index: 104; width: 290px;" Text="Premi questo pulsante per concludere la procedura di verifica."
                    Visible="False"></asp:Label>
                <asp:Label ID="Label9" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Navy"
                    
                    Style="left: 353px; position: absolute; top: 384px; z-index: 107; height: 15px; width: 283px;" Text="Premi questo pulsante per Inviare la domanda in assegnazione"
                    Visible="False"></asp:Label>
                <asp:Button ID="btnNonInviare" runat="server" Style="left: 30px; position: absolute; top: 322px; z-index: 102;"
                    Text="NON INVIARE DOMANDA IN ASSEGNAZIONE" Visible="False" Width="314px" />
                <asp:Label ID="Label11" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Navy"
                    
                    Style="z-index: 107; left: 353px; position: absolute; top: 320px; width: 286px;" Text="Premi questo pulsante per NON Inviare la domanda in assegnazione a causa di diminuzione punteggio o altro motivo. In seguito sarà possibile iniziare una nuova verifica requisiti"
                    Visible="False"></asp:Label>
                &nbsp;&nbsp;
                <img alt "chiudi" id="IMG3" src="../ImmMaschere/Close.png" onclick="return IMG3_onclick()" 
                    style="left: 611px; position: absolute; top: 99px; z-index: 106;cursor:pointer;" /></div>
            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
            <asp:HiddenField ID="H1" runat="server" Value="0" />
            <asp:HiddenField ID="H2" runat="server" Value="0" />
            <asp:HiddenField ID="txtTab" runat="server" />
        </form>
	</body>
	<script  language="javascript" type="text/javascript">
	document.getElementById('txtIndici').style.visibility='hidden';
	aa.close();
	</script>
	<script  language="javascript" type="text/javascript">
	document.getElementById('Verifica').style.visibility='hidden';
	if (document.getElementById('HiddenField1').value=='0')
	{
	document.getElementById('Img2').style.visibility='hidden';
	}
	else
	{
	document.getElementById('Img2').style.visibility='visible';
	}
    
    //document.getElementById('Img2').style.visibility='hidden';

    </script>
</html>

