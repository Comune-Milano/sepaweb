<%@ Page Language="VB" AutoEventWireup="false" CodeFile="max.aspx.vb" Inherits="ANAUT_max"
    EnableSessionState="True" EnableEventValidation="false" %>

<%@ Register Src="../Dic_Utenza.ascx" TagName="Dic_Utenza" TagPrefix="uc1" %>
<%@ Register Src="../Dic_Patrimonio_Utenza.ascx" TagName="Dic_Patrimonio" TagPrefix="uc3" %>
<%@ Register Src="../Dic_Reddito.ascx" TagName="Dic_Reddito" TagPrefix="uc4" %>
<%@ Register Src="../Dic_Reddito_Conv.ascx" TagName="Dic_Reddito_Conv" TagPrefix="uc5" %>
<%@ Register Src="../Dic_Integrazione.ascx" TagName="Dic_Integrazione" TagPrefix="uc6" %>
<%@ Register Src="../Dic_Note_UT.ascx" TagName="Dic_Note" TagPrefix="uc7" %>
<%@ Register Src="../Dic_Nucleo.ascx" TagName="Dic_Nucleo" TagPrefix="uc2" %>
<%@ Register Src="../dic_Documenti.ascx" TagName="dic_Documenti" TagPrefix="uc8" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-Transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 0;

    function $onkeydown() {

        if (event.keyCode == 8) {
            //alert('Questo tasto non può essere usato!');
            event.keyCode = 0;
        }
    } 

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script language="javascript" type="text/javascript">
<!--

    function window_onbeforeunload() {
        if (Uscita == 0) {
            event.returnValue = "Attenzione, stai per abbandonare SEPA@Web! Assicurarsi di aver salvato i dati e di uscire tramite il pulsante ESCI";
        }
    }

// -->
</script>
<head id="Head1" runat="server">
    <title>SEPA@Web - Dichiarazione ISEE</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <style type="text/css">
        .CssMaiuscolo
        {
            font-size: 8pt;
            text-transform: uppercase;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
        .CssComuniNazioni
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 166px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssPresenta
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 450px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssFamiAbit
        {
            font-size: 8pt;
            width: 600px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssProv
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 48px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssIndirizzo
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 66px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssLabel
        {
            font-size: 8pt;
            color: black;
            line-height: normal;
            font-style: normal;
            font-family: times;
            height: 20px;
            font-variant: normal;
        }
        .CssLblValori
        {
            font-size: 8pt;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
        .CssEtichetta
        {
            alignment: center;
        }
        .CssNuovoMaiuscolo
        {
            font-size: 8pt;
            text-transform: uppercase;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
    </style>
</head>
<script language="javascript" type="text/javascript" for="window" event="onbeforeunload">
<!--

if (document.getElementById('H1').value==0) {
    return window_onbeforeunload()
}
// -->
</script>
<script type="text/javascript" src="Funzioni.js"></script>
<script type="text/javascript">
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 250) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 150) / 2 : 0;
    LeftPosition = LeftPosition;
    TopPosition = TopPosition;
    aa = window.open('../loading.htm', '', 'height=150,top=' + TopPosition + ',left=' + LeftPosition + ',width=250');
  

</script>
<body onload="return AggTabDic(document.getElementById('txtTab').value,document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('redC').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('doc').style);"
    bgcolor="#f2f5f1">
    <script type="text/javascript">
        document.onkeydown = $onkeydown;
    </script>
    &nbsp;<atlas:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </atlas:ScriptManager>
    <form id="form1" runat="server">
    <asp:HiddenField ID="txtTab" runat="server" />
    <asp:HiddenField ID="H1" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="assTemp" runat="server" />
    <br />
    <br />
    <br />
    <img id="i1" language="javascript" onclick="return AggTabDic('1',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('redC').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('doc').style);"
        src="../p_menu/D1_0.gif" style="z-index: 130; left: 10px; position: absolute;
        top: 86px; cursor: pointer;" />
    <img id="i2" language="javascript" onclick="return AggTabDic('2',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('redC').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('doc').style);"
        src="../p_menu/D2_0.gif" style="z-index: 129; left: 109px; position: absolute;
        top: 86px; cursor: pointer;" />
    <img id="i3" language="javascript" onclick="return AggTabDic('3',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('redC').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('doc').style);"
        src="../p_menu/D3_0.gif" style="z-index: 128; left: 168px; position: absolute;
        top: 86px; cursor: pointer;" />
    <img id="i4" language="javascript" onclick="return AggTabDic('4',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('redC').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('doc').style);"
        src="../p_menu/D4_0.gif" style="z-index: 127; left: 251px; position: absolute;
        top: 86px; cursor: pointer;" />
    <img id="i5" language="javascript" onclick="return AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('redC').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('doc').style);"
        src="../p_menu/ReddConv_0.gif" style="z-index: 126; left: 315px; position: absolute;
        top: 86px; cursor: pointer;" />
    <img id="i6" language="javascript" onclick="return AggTabDic('6',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('redC').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('doc').style);"
        src="../p_menu/D8_0.gif" style="z-index: 125; left: 416px; position: absolute;
        top: 86px; cursor: pointer;" />
    <img id="i7" language="javascript" onclick="return AggTabDic('7',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('redC').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('doc').style);"
        src="../p_menu/D7_0.gif" style="z-index: 124; left: 510px; position: absolute;
        top: 86px; cursor: pointer;" />
    <img id="i8" language="javascript" onclick="return AggTabDic('8',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('redC').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('doc').style);"
        src="../p_menu/D9_0.gif" style="z-index: 124; left: 555px; position: absolute;
        top: 86px; cursor: pointer;" />
    <br />
    <br />
    <br />
    <br />
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
        width: 674px; position: absolute; top: 0px">
        <tr>
            <td style="width: 670px; text-align: right">
                <br />
                <asp:HiddenField ID="txt36" runat="server" Value="1" />
                <asp:HiddenField ID="solalettura" runat="server" Value="0" />
                <asp:HiddenField ID="txtbinserito" runat="server" Value="0" />
                <asp:HiddenField ID="vcomunitario" runat="server" Value="0" />
                <asp:HiddenField ID="lotto45" runat="server" Value="0" />
                <asp:HiddenField ID="nonstampare" runat="server" Value="0" />
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
                <br />
            </td>
        </tr>
    </table>
    <br />
    <asp:Image ID="imgAnagrafe" runat="server" ImageUrl="~/NuoveImm/Img_Anagrafe.png"
        Style="z-index: 127; left: 238px; cursor: pointer; position: absolute; top: 29px"
        ToolTip="Anagrafe della popolazione" />
    <asp:ImageButton ID="IMGCanone" runat="server" ImageUrl="../NuoveImm/Img_CanoneRegime.png"
        OnClientClick="CalcoloCanone();return false;" Style="z-index: 136; left: 505px;
        position: absolute; top: 29px;" ToolTip="Calcolo del canone a regime secondo L.R. 27/07 e L.R. 36/2008"
        Visible="False" />
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
    <asp:Label ID="LBLENTE" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
        Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 100;
        left: 10px; position: absolute; top: 427px" Text="VISUALIZZA INDICI" ToolTip="Ente che ha inserito la domanda"
        Visible="False" Width="150px" Height="20px"></asp:Label>
    <asp:Label ID="Label6" runat="server" BackColor="Red" BorderStyle="Solid" BorderWidth="1px"
        Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt" ForeColor="White" Height="20px"
        Style="z-index: 101; left: 162px; position: absolute; top: 509px" ToolTip="Ente che ha inserito la domanda"
        Width="489px"></asp:Label>
    <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Red"
        Style="z-index: 102; left: 174px; position: absolute; top: 532px" Width="476px"></asp:Label>
    &nbsp;<br />
    &nbsp;
    <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 103; left: 269px; position: absolute; top: 62px" Width="32px">ISEE</asp:Label>
    <br />
    <atlas:UpdatePanel ID="up" runat="server" Mode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div>
                <uc1:Dic_Utenza ID="Dic_Utenza1" runat="server" Visible="true" />
            </div>
        </ContentTemplate>
    </atlas:UpdatePanel>
    <asp:RadioButton ID="rdApplica" runat="server" Font-Names="arial" Font-Size="8pt"
        ForeColor="White" Style="z-index: 104; left: 578px; position: absolute; top: 508px"
        Text="SI" ToolTip="Indicando SI la funzione Terrà conto di quanto previsto dalla LR 36/2008: Limite Isee=35.000 ed Isee per la pronuncia della decadenza"
        Visible="False" GroupName="a" />
    <asp:RadioButton ID="rdNoApplica" runat="server" Font-Names="arial" Font-Size="8pt"
        ForeColor="White" Style="z-index: 104; left: 610px; position: absolute; top: 508px"
        Text="NO" ToolTip="Indicando NO la funzione Terrà conto di quanto previsto dalla LR 36/2008"
        Visible="False" GroupName="a" Checked="True" />
    &nbsp; &nbsp;&nbsp;
    <asp:Label ID="lblISEE" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 106;
        left: 304px; position: absolute; top: 62px" Width="122px">0,00</asp:Label>
    <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
        Style="z-index: 107; left: 604px; position: absolute; top: 29px" ToolTip="Esci" />
    <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
        Style="z-index: 108; left: 16px; position: absolute; top: 29px;" OnClientClick="Uscita=1;"
        ToolTip="Salva" />
    <asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
        Style="z-index: 109; left: 75px; position: absolute; top: 29px;" Enabled="False"
        OnClientClick="Uscita=1;" ToolTip="Elabora e Stampa" />
    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 110;
        left: 11px; position: absolute; top: 509px" Width="156px"></asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <atlas:UpdatePanel ID="Updatepanel1" runat="server" Mode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div>
                <uc2:Dic_Nucleo ID="Dic_Nucleo1" runat="server" Visible="true" />
            </div>
        </ContentTemplate>
    </atlas:UpdatePanel>
    <atlas:UpdatePanel ID="Updatepanel2" runat="server" Mode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div>
                <uc3:Dic_Patrimonio ID="Dic_Patrimonio1" runat="server" Visible="true" />
            </div>
        </ContentTemplate>
    </atlas:UpdatePanel>
    <atlas:UpdatePanel ID="Updatepanel3" runat="server" Mode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div>
                <uc4:Dic_Reddito ID="Dic_Reddito1" runat="server" Visible="true" />
            </div>
        </ContentTemplate>
    </atlas:UpdatePanel>
    <atlas:UpdatePanel ID="Updatepanel4" runat="server" Mode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div>
                <uc5:Dic_Reddito_Conv ID="Dic_Reddito_Conv1" runat="server" Visible="true" />
            </div>
        </ContentTemplate>
    </atlas:UpdatePanel>
    <atlas:UpdatePanel ID="Updatepanel5" runat="server" Mode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div>
                <uc6:Dic_Integrazione ID="Dic_Integrazione1" runat="server" Visible="true" />
            </div>
        </ContentTemplate>
    </atlas:UpdatePanel>
    <atlas:UpdatePanel ID="Updatepanel6" runat="server" Mode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div>
                <uc7:Dic_Note ID="Dic_Note1" runat="server" Visible="true" />
            </div>
        </ContentTemplate>
    </atlas:UpdatePanel>
    <atlas:UpdatePanel ID="Updatepanel8" runat="server" Mode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div>
                <uc8:dic_Documenti ID="Dic_Documenti1" runat="server" Visible="true" />
            </div>
        </ContentTemplate>
    </atlas:UpdatePanel>
    <asp:DropDownList ID="cmbComp" runat="server" Style="z-index: 112; left: 147px; position: absolute;
        top: 397px" Width="236px" Visible="False">
    </asp:DropDownList>
    <img src="../NuoveImm/Img_Eventi.png" language="javascript" onclick="Eventi()" style="left: 184px;
        position: absolute; top: 29px; cursor: pointer; right: 1313px;" alt="Eventi"
        id="ImgEventi" />
    <img src="../NuoveImm/Img_propDecadenza.png" language="javascript" onclick="PropDec()"
        style="left: 291px; position: absolute; top: 29px; cursor: pointer;" id="ImgPropDec"
        alt="Proposta di Decadenza" />
    <img src="../NuoveImm/Img_Indici.png" language="javascript" onclick="Indici()" style="left: 132px;
        position: absolute; top: 29px; cursor: pointer;" id="ImgIndici" alt="Indici" />
    <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 115; left: 156px; position: absolute; top: 62px" Width="32px">Data</asp:Label>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 116; left: 6px; position: absolute; top: 62px" Width="34px">PG N.</asp:Label>
    <asp:Label ID="lblApplica36" runat="server" Font-Bold="False" Font-Names="arial"
        Font-Size="8pt" ForeColor="White" Height="18px" Style="z-index: 117; left: 494px;
        position: absolute; top: 511px" Visible="False" Width="82px">Applica 36/2008</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        ForeColor="White" Height="18px" Style="z-index: 117; left: 164px; position: absolute;
        top: 512px" Width="301px">SPECIFICARE L'ANNO DI RIFERIMENTO REDDITUALE</asp:Label>
    <asp:Label ID="lblPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080" BorderStyle="Solid"
        BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 118; left: 44px; position: absolute;
        top: 62px" Width="96px">0000000000</asp:Label>
    &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="lblArt15" runat="server" BackColor="Red" BorderColor="#FFC080" BorderStyle="Solid"
        BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 118; left: 109px; position: absolute;
        top: 62px; width: 39px;" ForeColor="White" Visible="False">ART.15</asp:Label>
    <asp:DropDownList ID="cmbStato" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 119; left: 481px; position: absolute;
        top: 59px" Width="166px">
    </asp:DropDownList>
    <asp:DropDownList ID="cmbAnnoReddituale" runat="server" Font-Names="arial" Font-Size="8pt"
        ForeColor="Black" Style="z-index: 120; left: 417px; position: absolute; top: 511px"
        Width="55px" Height="20px" AutoPostBack="True" ToolTip="Anno di riferimento reddituale">
        <asp:ListItem Selected="True">2006</asp:ListItem>
        <asp:ListItem>2007</asp:ListItem>
        <asp:ListItem>2008</asp:ListItem>
        <asp:ListItem>2009</asp:ListItem>
        <asp:ListItem>2010</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 121; left: 439px; position: absolute; top: 61px" Width="44px">STATO</asp:Label>
    &nbsp;
    <asp:TextBox ID="txtDataPG" runat="server" Columns="10" CssClass="CssNuovoMaiuscolo"
        MaxLength="10" Style="z-index: 122; left: 189px; position: absolute; top: 61px"
        Width="63px" Height="18px"></asp:TextBox>
    <script type="text/javascript">

    function CalcoloCanone() {

                if (document.getElementById('Dic_Utenza1_TxtRapporto').value != '') {
                    if (document.getElementById('assTemp').value == '')
                    {
                      window.open("../Contratti/AU_abusivi/Canone.aspx?ID=" + <%=lIdDichiarazione %> + '&COD=' + document.getElementById('Dic_Utenza1_TxtRapporto').value + '&IDUNITA=' + document.getElementById('Dic_Utenza1_txtPosizione').value, '', 'top=0,left=0,width=550,height=600,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes');
                    } 
                    else
                    {
                      window.open("../Contratti/AU_temporanee/Canone.aspx?ID=" + <%=lIdDichiarazione %> + '&COD=' + document.getElementById('Dic_Utenza1_TxtRapporto').value + '&IDUNITA=' + document.getElementById('Dic_Utenza1_txtPosizione').value, '', 'top=0,left=0,width=550,height=600,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes');
                    }
                }
                else {
                    alert('Inserire il codice del contratto e assicurarsi di aver elaborato la domanda!');
                }
        }


        function ElencoModelliStampa() {
            Uscita = 0;
            if (document.getElementById('nonstampare').value == '0') {
                if (document.getElementById('txtModificato').value != '1') {
                    var win = null;
                    LeftPosition = (screen.width) ? (screen.width - 400) / 2 : 0;
                    TopPosition = (screen.height) ? (screen.height - 400) / 2 : 0;
                    LeftPosition = LeftPosition;
                    TopPosition = TopPosition;
                    if (document.getElementById('lotto45').value == '0') {
                        if (document.getElementById('Dic_Utenza1_TxtRapporto').value != '') {
                            aa = window.open('ElencoModelliStampa.aspx?45=0&COD=' + document.getElementById('Dic_Utenza1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Modelli', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=800');
                        }
                        else {
                            alert('Attenzione...inserire il codice del rapporto, premere il pulsante SALVA prima di procedere!');
                        }
                    }
                    else {
                        aa = window.open('ElencoModelliStampa.aspx?45=1&COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Modelli', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=800');
                    }
                }
                else {
                    alert('Attenzione...sono state effettuate modifiche, premere il pulsante SALVA prima di procedere!');
                }
            }
            else {
                alert('Attenzione...Non è possibile procedere. Domanda non modificabile!');
            }
        }


        function NotificaISEIncompleta() {
            Uscita = 0;
            if (document.getElementById('nonstampare').value == '0') {
                if (document.getElementById('txtModificato').value != '1') {
                    var win = null;
                    LeftPosition = (screen.width) ? (screen.width - 400) / 2 : 0;
                    TopPosition = (screen.height) ? (screen.height - 400) / 2 : 0;
                    LeftPosition = LeftPosition;
                    TopPosition = TopPosition;



                    if (document.getElementById('lotto45').value == '0') {
                        if (document.getElementById('Dic_Utenza1_TxtRapporto').value != '') {
                            aa = window.open('NotificaISEIncompleta.aspx?45=0&COD=' + document.getElementById('Dic_Utenza1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Notifica', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                        }
                        else {
                            alert('Attenzione...inserire il codice del rapporto, premere il pulsante SALVA prima di procedere!');
                        }
                    }
                    else {
                        aa = window.open('NotificaISEIncompleta.aspx?45=1&COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Notifica', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                    }
                }
                else {
                    alert('Attenzione...sono state effettuate modifiche, premere il pulsante SALVA prima di procedere!');

                }
            }
            else {
                alert('Attenzione...Non è possibile procedere. Domanda non modificabile!');
            }
        }

        function NotificaISE() {
            Uscita = 0;
            if (document.getElementById('nonstampare').value == '0') {
                if (document.getElementById('txtModificato').value != '1') {
                    var win = null;
                    LeftPosition = (screen.width) ? (screen.width - 400) / 2 : 0;
                    TopPosition = (screen.height) ? (screen.height - 400) / 2 : 0;
                    LeftPosition = LeftPosition;
                    TopPosition = TopPosition;
                    if (document.getElementById('lotto45').value == '0') {
                        if (document.getElementById('Dic_Utenza1_TxtRapporto').value != '') {
                            aa = window.open('NotificaISE.aspx?45=0&COD=' + document.getElementById('Dic_Utenza1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Notifica', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                        }
                        else {
                            alert('Attenzione...inserire il codice del rapporto, premere il pulsante SALVA prima di procedere!');
                        }
                    }
                    else {
                        aa = window.open('NotificaISE.aspx?45=1&COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Notifica', 'height=450,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                    }

                }

                else {

                    alert('Attenzione...sono state effettuate modifiche, premere il pulsante SALVA prima di procedere!');

                }
            }
            else {
                alert('Attenzione...Non è possibile procedere. Domanda non modificabile!');
            }
        }

         function AutocertStServ() {
            Uscita = 0;
             if (document.getElementById('nonstampare').value == '0') {
            if (document.getElementById('txtModificato').value != '1') {
                var win = null;
                if (document.getElementById('lotto45').value == '0') {
                    if (document.getElementById('Dic_Utenza1_TxtRapporto').value != '') {
                        aa = window.open('Stampe/AutocertStServ.aspx?COD=' + document.getElementById('Dic_Utenza1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Autocert', '');
                    }
                    else {
                        alert('Attenzione...inserire il codice del rapporto, premere il pulsante SALVA prima di procedere!');
                    }
                }
                else {
                    aa = window.open('Stampe/AutocertStServ.aspx?COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Autocert', '');
                }
               
            }

            else {

                alert('Attenzione...sono state effettuate modifiche, premere il pulsante SALVA prima di procedere!');
                
            }
        }
        else {
            alert('Attenzione...Non è possibile procedere. Domanda non modificabile!');
        }
        }

        function ElencoStampe() {
            if (document.getElementById('lotto45').value == '0') {
                window.open('Stampe/ElencoStampe.aspx?COD=' + document.getElementById('Dic_Utenza1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Elenco', '');
            }
            else {
                window.open('Stampe/ElencoStampe.aspx?COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Elenco', '');
            }
            Uscita = 0;
        }




        //--></script>
    <asp:HiddenField ID="propdec" runat="server" />
    <table onmousedown="Uscita=1;" style="width: 68px; position: absolute; top: 24px;
        left: 409px; z-index: 200;">
        <tr>
            <td>
                <asp:Menu ID="MenuStampe" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                    Orientation="Horizontal">
                    <DynamicHoverStyle BackColor="#FFFFCC" />
                    <DynamicMenuItemStyle BackColor="White" Height="20px" ItemSpacing="2px" />
                    <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                        VerticalPadding="1px" />
                    <Items>
                        <asp:MenuItem ImageUrl="~/NuoveImm/Img_Documentazione.png" Selectable="False" Value=" ">
                            <asp:MenuItem NavigateUrl="javascript:ElencoModelliStampa();" 
                                Text="Elenco Modelli Disponibili" Value="ElencoModelli"></asp:MenuItem>
                            <asp:MenuItem Text="Elenco Stampe" Value="10" NavigateUrl="javascript:ElencoStampe();">
                            </asp:MenuItem>
                        </asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>
        </tr>
    </table>
    </form>
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

    if (document.getElementById('propdec').value == '0') {
        document.getElementById('ImgPropDec').style.visibilty = 'hidden';
    }

    if (document.getElementById('solalettura').value == '1') {
        document.getElementById('ImgIndici').style.visibility = 'hidden';
        document.getElementById('ImgIndici').style.position = 'absolute';
        document.getElementById('ImgIndici').style.left = '-100px';
        document.getElementById('ImgIndici').style.display = 'none';

        document.getElementById('ImgEventi').style.visibility = 'hidden';
        document.getElementById('ImgEventi').style.position = 'absolute';
        document.getElementById('ImgEventi').style.left = '-100px';
        document.getElementById('ImgEventi').style.display = 'none';


        document.getElementById('ImgPropDec').style.visibility = 'hidden';
        document.getElementById('ImgPropDec').style.position = 'absolute';
        document.getElementById('ImgPropDec').style.left = '-100px';
        document.getElementById('ImgPropDec').style.display = 'none';


    }   
</script>
</html>
