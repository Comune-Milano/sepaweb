<%@ Page Language="VB" AutoEventWireup="false" CodeFile="max.aspx.vb" Inherits="FSA_max"  EnableSessionState="True"  EnableEventValidation="false"%>
<%@ Register Src="../Dic_Dichiarazione.ascx" TagName="Dic_Dichiarazione" TagPrefix="uc1" %>
<%@ Register Src="../Dic_Patrimonio.ascx" TagName="Dic_Patrimonio" TagPrefix="uc3" %>
<%@ Register Src="../Dic_Reddito.ascx" TagName="Dic_Reddito" TagPrefix="uc4" %>
<%@ Register Src="../Dic_Sottoscrittore.ascx" TagName="Dic_Sottoscrittore" TagPrefix="uc5" %>
<%@ Register Src="../Dic_Integrazione.ascx" TagName="Dic_Integrazione" TagPrefix="uc6" %>
<%@ Register Src="../Dic_Note_Cert.ascx" TagName="Dic_Note" TagPrefix="uc7" %>
<%@ Register Src="../Dic_Nucleo_FSA.ascx" TagName="Dic_Nucleo" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
<head id="Head1" runat="server">
    <title>SEPA@Web - Dichiarazione ISEE</title>
    	<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
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


<script language="javascript" type="text/javascript">
<!--

function window_onbeforeunload() {

if (document.getElementById('H1').value==1) {
event.returnValue = "Attenzione...Uscire dalla Dichiarazione utilizzando il pulsante ESCI!! In caso contrario la dichiarazione VERRA' BLOCCATA E NON SARA' PIU' POSSIBILE MODIFICARE!";
}
}

// -->
</script>



</head>

<script language="javascript" type="text/javascript" for="window" event="onbeforeunload">
<!--
aa.close();
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
    aa=window.open('../loading.htm','','height=150,top='+TopPosition+',left='+LeftPosition+',width=250');
</script>
<body onload="return AggTabDic(document.getElementById('txtTab').value,document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);" bgcolor="#f2f5f1">
<script type="text/javascript">
document.onkeydown=$onkeydown;
</script>
    &nbsp;<br />
    <asp:Image ID="imgAnagrafe" runat="server" ImageUrl="~/NuoveImm/Img_Anagrafe.png"
        Style="z-index: 127; left: 280px; cursor: pointer; position: absolute; top: 29px" ToolTip="Anagrafe della popolazione" />
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
        width: 674px; position: absolute; top: 0px">
        <tr>
            <td style="width: 670px; text-align: right">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Dichiarazione&nbsp;</strong></span><br />
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
    <img id="i1" language="javascript" onclick="return AggTabDic('1',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D1_0.gif" style="z-index: 125; left: 10px; position: absolute; top: 86px" />
    <img id="i2" language="javascript" onclick="return AggTabDic('2',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D2_0.gif" style="z-index: 124; left: 109px; position: absolute; top: 86px" />
    <img id="i3" language="javascript" onclick="return AggTabDic('3',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D3_0.gif" style="z-index: 123; left: 168px; position: absolute; top: 86px" />
    <img id="i4" language="javascript" onclick="return AggTabDic('4',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D4_0.gif" style="z-index: 122; left: 251px; position: absolute; top: 86px" />
    <img id="i5" language="javascript" onclick="return AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D5_0.gif" style="z-index: 121; left: 315px; position: absolute; top: 86px" />
    <img id="i6" language="javascript" onclick="return AggTabDic('6',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D6_0.gif" style="z-index: 120; left: 429px; position: absolute; top: 86px" />
    <img id="i7" language="javascript" onclick="return AggTabDic('7',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D70_0.gif" style="z-index: 119; left: 513px; position: absolute; top: 86px" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
               
    <form id="form1" runat="server">

<%--        <atlas:ScriptManager ID="ScriptManager1" runat="server" enablepartialrendering="true" >
    </atlas:ScriptManager>  --%>  
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
        &nbsp;<br />

    <div>
        <uc1:Dic_Dichiarazione ID="Dic_Dichiarazione1" runat="server" Visible="true" />
    </div>

        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
            ForeColor="#0000C0" Style="z-index: 100; left: 496px; position: absolute; top: 428px"
            Text="Aggiornare i redditi al 2006!!" Visible="False" Width="157px"></asp:Label>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/IMG/Alert.gif" Style="z-index: 126;
            left: 477px; position: absolute; top: 427px" Visible="False" />
                <asp:Label ID="LBLENTE" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
                    Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 102;
                    left: 11px; position: absolute; top: 427px" Text="VISUALIZZA INDICI" ToolTip="Ente che ha inserito la domanda"
                    Visible="False" Width="157px"></asp:Label>
        &nbsp; &nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;

                <div>
                    <uc2:Dic_Nucleo ID="Dic_Nucleo1" runat="server" Visible="true" />
                    </div>

                <div>
                    <uc3:Dic_Patrimonio ID="Dic_Patrimonio1" runat="server" Visible="true" />
                </div>

                <div>
                    <uc4:Dic_Reddito ID="Dic_Reddito1" runat="server" Visible="true" />
                </div>

                <div>
                    <uc5:Dic_Sottoscrittore ID="Dic_Sottoscrittore1" runat="server" Visible="true" />
                </div>

                <div>
                    <uc6:Dic_Integrazione ID="Dic_Integrazione1" runat="server" Visible="true"  />
                </div>

                <div>
                    <uc7:Dic_Note ID="Dic_Note1" runat="server" Visible="true" />
                </div>

        <asp:HiddenField ID="H2" runat="server" Value="0" />
        <asp:HiddenField ID="H1" runat="server" Value="0" />
        <asp:HiddenField ID="txtTab" runat="server" />
        <asp:HiddenField ID="txtbinserito" runat="server" Value="0" />
        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
        <asp:DropDownList ID="cmbComp" runat="server" Style="z-index: 106; left: 147px; position: absolute;
            top: 397px" Width="236px" Visible="False">
        </asp:DropDownList>
        <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" OnClientClick="document.getElementById('H1').value=0;" style="z-index: 125; left: 396px; position: absolute; top: 29px" ToolTip="Esci" />
        <asp:ImageButton ID="btnSalva" runat="server" 
            ImageUrl="~/NuoveImm/Img_Salva.png" 
            style="z-index: 100; left: 7px; position: absolute; top: 29px; height: 12px;" 
            OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" 
            ToolTip="Salva" />
            <asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png" style="z-index: 100; left: 77px; position: absolute; top: 29px" Enabled="False" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" ToolTip="Elabora e Stampa" />
            <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 100;
                    left: 184px; position: absolute; top: 426px" Width="284px"></asp:Label>
                    <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Red"
                    Style="z-index: 101; left: 182px; position: absolute; top: 10px" Width="106px"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 100;
                    left: 184px; position: absolute; top: 426px" Width="284px"></asp:Label>
                    <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Red"
                    Style="z-index: 101; left: 146px; position: absolute; top: 10px" Width="129px"></asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 108; left: 184px; position: absolute; top: 62px" Width="32px">Data</asp:Label>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 109; left: 6px; position: absolute; top: 62px" Width="34px">Dic. N.</asp:Label>
    <asp:Label ID="lblPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080" BorderStyle="Solid"
        BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 110; left: 74px; position: absolute;
        top: 62px" Width="66px">0000000000</asp:Label>
        <asp:Label ID="lblDomAssociata" runat="server" BackColor="PaleTurquoise" BorderColor="#FFFFC0"
            BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 111;
            left: 363px; position: absolute; top: 61px" Width="66px"></asp:Label>
    <asp:Label ID="lblSPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 112;
        left: 45px; position: absolute; top: 62px" Width="26px">06-1</asp:Label>
    <asp:Label ID="Label7" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 113;
        left: 142px; position: absolute; top: 62px" Width="31px">F205</asp:Label>
    <asp:DropDownList ID="cmbStato" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 114; left: 481px; position: absolute;
        top: 59px" Width="166px">
    </asp:DropDownList>
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 115; left: 439px; position: absolute; top: 61px" Width="44px">STATO</asp:Label>
        <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
            Style="z-index: 116; left: 289px; position: absolute; top: 62px" Width="105px">Domanda PG</asp:Label>
    <asp:TextBox ID="txtDataPG" runat="server" Columns="10" CssClass="CssNuovoMaiuscolo" MaxLength="10" Style="z-index: 117; left: 217px;
        position: absolute; top: 61px" Width="63px"></asp:TextBox>
    </form>
    <script LANGUAGE=JAVAscript>
    document.getElementById('txtbinserito').style.visibilty='hidden';
    //document.getElementById('attendi').style.visibilty='hidden';
    //aa.close();
    //--></script>
</body>
    <script type="text/xml-script">
        <page xmlns:script="http://schemas.microsoft.com/xml-script/2005">
            <references>
            </references>
            <components>
            </components>
        </page>
    </script>
    
    <script type="text/javascript">
    aa.close();
    </script>
</html>

