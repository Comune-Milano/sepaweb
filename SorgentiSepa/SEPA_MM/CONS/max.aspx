<%@ Page Language="VB" AutoEventWireup="false" CodeFile="max.aspx.vb" Inherits="CONS_max" EnableSessionState="True"  EnableEventValidation="false"%>
<%@ Register Src="../Dic_Dichiarazione.ascx" TagName="Dic_Dichiarazione" TagPrefix="uc1" %>
<%@ Register Src="../Dic_Patrimonio_bandi.ascx" TagName="Dic_Patrimonio" TagPrefix="uc3" %>
<%@ Register Src="../Dic_Reddito_bandi.ascx" TagName="Dic_Reddito" TagPrefix="uc4" %>
<%@ Register Src="../Dic_Sottoscrittore.ascx" TagName="Dic_Sottoscrittore" TagPrefix="uc5" %>
<%@ Register Src="../Dic_Integrazione_bandi.ascx" TagName="Dic_Integrazione" TagPrefix="uc6" %>
<%@ Register Src="../Dic_Note.ascx" TagName="Dic_Note" TagPrefix="uc7" %>
<%@ Register Src="../Dic_Nucleo_bandi.ascx" TagName="Dic_Nucleo" TagPrefix="uc2" %>

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

function VisDomanda() {
    window.open('domanda.aspx?ID=<%=lIdDomanda%>&ID1=-1&PROGR=-1&LE=1&APP=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');
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


    

</head>

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
    &nbsp;<atlas:ScriptManager ID="ScriptManager1" runat="server" enablepartialrendering="true" >
    </atlas:ScriptManager>
    &nbsp;<br />

    <br />

    <br />
    <img id="i1" language="javascript" onclick="return AggTabDic('1',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D1_0.gif" style="z-index: 124; left: 10px; position: absolute; top: 86px" />
    <img id="i2" language="javascript" onclick="return AggTabDic('2',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D2_0.gif" style="z-index: 123; left: 109px; position: absolute; top: 86px" />
    <img id="i3" language="javascript" onclick="return AggTabDic('3',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D3_0.gif" style="z-index: 122; left: 168px; position: absolute; top: 86px" />
    <img id="i4" language="javascript" onclick="return AggTabDic('4',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D4_0.gif" style="z-index: 121; left: 251px; position: absolute; top: 86px" />
    <img id="i5" language="javascript" onclick="return AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D5_0.gif" style="z-index: 120; left: 315px; position: absolute; top: 86px" />
    <img id="i6" language="javascript" onclick="return AggTabDic('6',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D6_0.gif" style="z-index: 119; left: 429px; position: absolute; top: 86px" />
    <img id="i7" language="javascript" onclick="return AggTabDic('7',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);"
        src="../p_menu/D7_0.gif" style="z-index: 118; left: 513px; position: absolute; top: 86px" />
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

    <form id="form1" runat="server">
    <asp:TextBox ID="txtL" runat="server" Style="z-index: 100; left: 580px; position: absolute;
        top: 116px" Width="26px"></asp:TextBox>
        &nbsp;<br />
        &nbsp;
                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 101;
                    left: 11px; position: absolute; top: 428px" Width="632px"></asp:Label>
                <br />
              <atlas:updatepanel id="up" runat="server" mode="Conditional" 
    rendermode="Inline">
    <ContentTemplate>
    <div>
        &nbsp;<uc1:Dic_Dichiarazione ID="Dic_Dichiarazione1" runat="server" Visible="true" />
    </div>
    </ContentTemplate>
              </atlas:UpdatePanel>
        <img id="btnVisDomanda" src="../NuoveImm/Img_VisDomanda_Grande.png" 
        language="javascript" onclick="VisDomanda()" style="z-index: 125; left: 3px;
                    cursor: pointer; position: absolute; top: 16px" />
                <asp:ImageButton ID="imgUscita" runat="server" 
        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
        style="z-index: 102; left: 367px; position: absolute; top: 17px" />
                <asp:Label ID="LBLENTE" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
                    Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 103;
                    left: 114px; position: absolute; top: 33px" Text="VISUALIZZA INDICI" ToolTip="Ente che ha inserito la domanda"
                    Visible="False" Width="157px"></asp:Label>
        &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="txtbinserito" runat="server" Style="left: 572px; position: absolute;
            top: 62px; z-index: 104;" Width="5px" Height="10px">0</asp:TextBox>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;

        <br /><atlas:updatepanel id="Updatepanel1" runat="server" mode="Conditional" 
    rendermode="Inline">
            <ContentTemplate>
                <div>
                    <uc2:Dic_Nucleo ID="Dic_Nucleo1" runat="server" Visible="true" />
                    &nbsp;</div>
            </ContentTemplate>
        </atlas:UpdatePanel>
        <br /><atlas:updatepanel id="Updatepanel2" runat="server" mode="Conditional" 
    rendermode="Inline">
            <ContentTemplate>
                <div>
                    &nbsp;<uc3:Dic_Patrimonio ID="Dic_Patrimonio1" runat="server" Visible="true" />
                </div>
            </ContentTemplate>
        </atlas:UpdatePanel>
        <br /><atlas:updatepanel id="Updatepanel3" runat="server" mode="Conditional" 
    rendermode="Inline">
            <ContentTemplate>
                <div>
                    &nbsp;<uc4:Dic_Reddito ID="Dic_Reddito1" runat="server" Visible="true" />
                </div>
            </ContentTemplate>
        </atlas:UpdatePanel>
        <br />
        <br /><atlas:updatepanel id="Updatepanel4" runat="server" mode="Conditional" 
    rendermode="Inline">
            <ContentTemplate>
                <div>
                    &nbsp;<uc5:Dic_Sottoscrittore ID="Dic_Sottoscrittore1" runat="server" Visible="true" />
                </div>
            </ContentTemplate>
        </atlas:UpdatePanel>
        <br /><atlas:updatepanel id="Updatepanel5" runat="server" mode="Conditional" 
    rendermode="Inline">
            <ContentTemplate>
                <div>
                    &nbsp;<uc6:Dic_Integrazione ID="Dic_Integrazione1" runat="server" Visible="true"  />
                </div>
            </ContentTemplate>
        </atlas:UpdatePanel>
        <br /><atlas:updatepanel id="Updatepanel6" runat="server" mode="Conditional" 
    rendermode="Inline">
            <ContentTemplate>
                <div>
                    &nbsp;<uc7:Dic_Note ID="Dic_Note1" runat="server" Visible="true" />
                </div>
            </ContentTemplate>
        </atlas:UpdatePanel>
        &nbsp;<br />
        <br />
        <br />
        <asp:DropDownList ID="cmbComp" runat="server" Style="z-index: 105; left: 147px; position: absolute;
            top: 397px" Width="236px" Visible="False">
        </asp:DropDownList>
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
        <asp:TextBox ID="txtTab" runat="server" Height="13px" Style="left: 576px;
        position: absolute; top: 60px; z-index: 106;" Width="10px"></asp:TextBox>
        &nbsp; &nbsp; &nbsp;&nbsp;
            <table border="0" cellpadding="0" cellspacing="0" height="55"
        style="z-index: 99; left: 0px; border-top-style: none; border-right-style: none;
        border-left-style: none; position: absolute; top: 0px; border-bottom-style: none"
        width="673" background="../ImmMaschere/greydichiarazioni.jpg">
        <tr>
            <td align="left" valign="top">
                </td>
            <td align="left" valign="top">
                &nbsp;</td>
            <td style="width: 29px; height: 33px; text-align: center">
                &nbsp;</td>
            <td style="height: 33px; text-align: center; width: 155px;">
            </td>
            <td style="height: 33px; text-align: center">
                &nbsp; &nbsp;
                &nbsp;
                &nbsp;&nbsp;
            </td>
            <td style="height: 33px; text-align: center">
            </td>
            <td style="height: 33px; text-align: center; width: 11px;">
            </td>
        </tr>
    </table>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 107; left: 184px; position: absolute; top: 62px" Width="32px">Data</asp:Label>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 108; left: 6px; position: absolute; top: 62px" Width="34px">Dic. N.</asp:Label>
    <asp:Label ID="lblPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080" BorderStyle="Solid"
        BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 109; left: 74px; position: absolute;
        top: 62px" Width="66px">0000000000</asp:Label>
        <asp:Label ID="lblDomAssociata" runat="server" BackColor="PaleTurquoise" BorderColor="#FFFFC0"
            BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 110;
            left: 363px; position: absolute; top: 61px" Width="66px"></asp:Label>
        &nbsp;&nbsp;
    <asp:Label ID="lblSPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 111;
        left: 45px; position: absolute; top: 62px" Width="26px">06-1</asp:Label>
    <asp:Label ID="Label7" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 112;
        left: 142px; position: absolute; top: 62px" Width="31px">F205</asp:Label>
    <asp:DropDownList ID="cmbStato" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 113; left: 481px; position: absolute;
        top: 59px" Width="166px">
    </asp:DropDownList>
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 114; left: 439px; position: absolute; top: 61px" Width="44px">STATO</asp:Label>
        <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
            Style="z-index: 115; left: 289px; position: absolute; top: 62px" Width="105px">Domanda PG</asp:Label>
    <asp:TextBox ID="txtDataPG" runat="server" Columns="10" CssClass="CssNuovoMaiuscolo" MaxLength="10" Style="z-index: 116; left: 217px;
        position: absolute; top: 61px" Width="63px"></asp:TextBox>
    </form>
    <script type="text/javascript">
    
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
    
    document.getElementById('txtL').style.visibility='hidden';
	if (document.getElementById('txtL').value=='1') {
	    document.getElementById('btnVisDomanda').style.visibility='hidden';
	}
	aa.close();
    </script>
</html>

