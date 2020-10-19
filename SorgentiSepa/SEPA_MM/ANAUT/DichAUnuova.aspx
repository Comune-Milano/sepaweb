<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DichAUnuova.aspx.vb" Inherits="ANAUT_DichAUnuova" %>

<%@ Register Src="Tab_Reddito.ascx" TagName="Dic_Reddito" TagPrefix="uc2" %>
<%@ Register Src="Tab_Patrimonio.ascx" TagName="Dic_Patrimonio" TagPrefix="uc3" %>
<%@ Register Src="Tab_Documentazione.ascx" TagName="dic_Documenti" TagPrefix="uc5" %>
<%@ Register Src="Tab_Nucleo.ascx" TagName="Tab_Nucleo" TagPrefix="uc1" %>
<%@ Register Src="Tab_InfoContratto.ascx" TagName="Tab_InfoContratto" TagPrefix="uc4" %>
<%@ Register Src="Tab_Diffide.ascx" TagName="Tab_Diffide" TagPrefix="uc6" %>
<%@ Register Src="Tab_ISEE.ascx" TagName="Tab_ISEE" TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 0;
    var Selezionato;
    var OldColor;
    var SelColo;

    function $onkeydown() {

        if (event.keyCode == 8) {
            event.keyCode = 0;
        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" language="javascript">
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            initialize();
        }
    }

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\,]/g
    };

    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
        o.value = o.value.replace('.', ',');
    }

    function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a != 'NaN') {
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali;
                    //document.getElementById(obj.id).value = a.replace('.', ',')
                    document.getElementById(obj.id).value = risultato;
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',');
                }

            }
            else
                document.getElementById(obj.id).value = '';
        }
    }
    function SostPuntVirg(e, obj) {
        var keyPressed;
        keypressed = (window.event) ? event.keyCode : e.which;
        if (keypressed == 46) {
            if (navigator.appName == 'Microsoft Internet Explorer') {
                event.keyCode = 0;
            }
            else {
                e.preventDefault();
            }
            obj.value += ',';
            obj.value = obj.value.replace('.', '');
        }
    }
</script>
<head runat="server">
    <title>Anagrafe Utenza</title>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <script type="text/javascript" src="../Standard/Scripts/jsFunzioni.js"></script>
    <link href="../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsFunzioniLock.js" type="text/javascript"></script>
    <link href="Styles/StileAU.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        window.onbeforeunload = warn;

        function warn() {

            var warning = 'Attenzione...Uscendo dalla dichiarazione le modifiche non salvate andranno perse. Si consiglia di salvare prima di uscire e di utilizzare il pulsante ESCI!';
            var hack = /irefox\/([4-9]|1\d+)/.test(navigator.userAgent);
            if (hack) alert(warning + '\n\n(Pardon the double dialogs '
                + 'caused by Firefox bug 588292.)');

            if (Uscita == 0) {
                return warning;
            }

        };

        window.onunload = Exit;

        function Exit() {

            if (document.getElementById('imgUscita') != null) {
                document.getElementById('imgUscita').click();
            }
        };
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 34px;
        }
    </style>
</head>
<body style="background-image: url(../NuoveImm/XBackGround.gif); background-repeat: repeat-x;">
    <div id="caric" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75;
        background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2222" runat="server" ImageUrl="..\NuoveImm\load.gif" />
                        <br />
                        <br />
                        <asp:Label ID="lblcarica222" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%Response.Flush()%>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdateProgress ID="UpdateProgressEventi" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;
                z-index: 500">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
                    <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="..\NuoveImm\load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Anagrafe Utenza</span></strong>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="150px">
                            <asp:Button ID="btnSalva" runat="server" OnClientClick="Uscita=1;document.getElementById('txtModificato').value='0';"
                                ToolTip="Salva" CssClass="bottone" Text="Salva" Height="22px" />
                            <asp:Button ID="btnCaricaDoc" runat="server" ToolTip="CaricaDoc" Style="position: absolute;
                                top: -100px; left: -100px; visibility: hidden" Text="Salva" />
                        </td>
                        <td width="150px">
                            <asp:Button ID="ImgIndici" runat="server" CausesValidation="False" CssClass="bottone"
                                OnClientClick="Indici();return false;" Text="Indici" ToolTip="Indici" />
                        </td>
                        <td width="150px">
                            <asp:Button ID="ImgEventi" runat="server" CssClass="bottone" OnClientClick="Eventi();return false;"
                                Text="Eventi" ToolTip="Eventi" />
                        </td>
                        <td width="150px">
                            <asp:Button ID="imgStampa" runat="server" CssClass="bottone" OnClientClick="Uscita=1;document.getElementById('txtModificato').value='0';"
                                Text="Stampa" ToolTip="Elabora e Stampa" Width="60px" />
                        </td>
                        <td width="350px">
                            &nbsp;
                        </td>
                        <td width="150px">
                            <asp:Button ID="ImgPropDec" runat="server" CssClass="bottone" OnClientClick="PropDec();return false;"
                                Text="Prop. di Decadenza" ToolTip="Proposta di Decadenza" Visible="False" Width="130px" />
                        </td>
                        <td width="150px">
                            <asp:Button ID="IMGCanone" runat="server" CssClass="bottone" OnClientClick="CalcoloCanone();return false;"
                                Text="Canone a Regime" ToolTip="Calcolo del canone a regime secondo L.R. 27/07 e L.R. 36/2008"
                                Visible="False" Width="120" />
                        </td>
                        <td width="150px">
                            <asp:Button ID="imgAnagrafe" runat="server" CssClass="bottone" Text="Anagrafe" ToolTip="Anagrafe della popolazione"
                                Width="75" />
                        </td>
                        <td width="200px">
                            <asp:Menu ID="MenuStampe" runat="server" CssClass="bottoneDoc" ForeColor="Black"
                                Orientation="Horizontal" RenderingMode="List">
                                <DynamicHoverStyle BackColor="#FFFFCC" />
                                <DynamicMenuItemStyle BackColor="White" Height="20px" ItemSpacing="2px" />
                                <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                                    VerticalPadding="2px" />
                                <Items>
                                    <asp:MenuItem Selectable="False" Text="Documentazione" Value=" ">
                                        <asp:MenuItem NavigateUrl="javascript:StampaFronte();" Text="Frontespizio" Value="Frontespizio">
                                        </asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="javascript:ElencoModelliStampa();" Text="Elenco Modelli Disponibili"
                                            Value="ElencoModelli"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="javascript:ElencoStampe();" Text="Elenco Stampe" Value="10">
                                        </asp:MenuItem>
                                    </asp:MenuItem>
                                </Items>
                            </asp:Menu>
                        </td>
                        <td style="text-align: right" align="right">
                            <asp:Button ID="imgUscita" runat="server" CssClass="bottone" OnClientClick="document.getElementById('H1').value=0;document.getElementById('inUscita').value=1;"
                                Text="Esci" ToolTip="Esci" Width="42px" />
                        </td>
                    </tr>
                </table>
                <br />
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width: 63%">
                            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                                ForeColor="Black" Height="18px" Width="220px">ANNO DI RIFERIMENTO REDDITUALE</asp:Label>
                            <asp:DropDownList ID="cmbAnnoReddituale" runat="server" Font-Names="arial" Font-Size="8pt"
                                ForeColor="Black" Width="80px" AutoPostBack="True" ToolTip="Anno di riferimento reddituale">
                            </asp:DropDownList>
                            &nbsp&nbsp&nbsp
                            <asp:Label ID="lblApplica36" runat="server" Font-Bold="False" Font-Names="arial"
                                Font-Size="9pt" ForeColor="Black" Width="100px" Height="18px">Applica 36/2008</asp:Label>
                            <asp:RadioButton ID="rdApplica" runat="server" Font-Names="arial" Font-Size="8pt"
                                ForeColor="Black" Text="SI" ToolTip="Indicando SI la funzione Terrà conto di quanto previsto dalla LR 36/2008: Limite Isee=35.000 ed Isee per la pronuncia della decadenza"
                                Visible="False" GroupName="a" />
                            <asp:RadioButton ID="rdNoApplica" runat="server" Font-Names="arial" Font-Size="8pt"
                                ForeColor="Black" Text="NO" ToolTip="Indicando NO la funzione Terrà conto di quanto previsto dalla LR 36/2008"
                                Visible="False" GroupName="a" Checked="True" />
                        </td>
                        <td style="width: 15%">
                            &nbsp
                        </td>
                        <td style="vertical-align: top;">
                            <asp:Label ID="lbl45_Lotto" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                                ForeColor="Black" Height="18px" Visible="False">TRATTASI DI A.U. 4-5 LOTTO - Cod.Convocazione:</asp:Label>
                            &nbsp
                            <asp:TextBox ID="txtCodConvocazione" runat="server" Columns="7" Font-Bold="True"
                                Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="width: 60px;"
                                TabIndex="3" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: center; width: 100%; font-family: Arial; font-size: 12pt;"
                            colspan="8">
                            <strong>DATI GENERALI</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <hr style="border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #C0C0C0;" />
                        </td>
                    </tr>
                    <tr style="font-family: Arial; font-size: 9pt">
                        <td style="width: 12%;">
                            NUM. PROTOCOLLO
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="lblPG" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="CssLblValori" Width="130px" ReadOnly="True" Font-Bold="True"
                                Font-Names="Arial" Font-Size="9pt">0000000000</asp:TextBox>
                            <asp:TextBox ID="lblArt15" runat="server" BackColor="Red" BorderColor="#FFC080" BorderStyle="Solid"
                                BorderWidth="1px" CssClass="CssLblValori" ForeColor="White" Visible="False" ReadOnly="True"
                                Width="50px">ART.15</asp:TextBox>
                        </td>
                        <td style="width: 12%; text-align: right;">
                            DATA&nbsp;
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="txtDataPG" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                BorderWidth="2px" Width="130px" TabIndex="1" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td style="width: 13%; text-align: right;">
                            ISEE €&nbsp;
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="lblISEE" runat="server" CssClass="CssLblValori" Width="130px" TabIndex="2"
                                Font-Bold="True" Font-Names="arial" Font-Size="9pt" ReadOnly="True" BorderColor="#0078C2"
                                BorderStyle="Solid" BorderWidth="2px">0,00</asp:TextBox>
                        </td>
                        <td style="width: 13%; text-align: right;">
                            STATO&nbsp;
                        </td>
                        <td style="width: 13%">
                            <div id="divBordoDrop" class="bordoComboBox">
                                <asp:DropDownList ID="cmbStato" runat="server" TabIndex="3" Width="190px" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="9pt">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <hr style="border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #C0C0C0;" />
                        </td>
                    </tr>
                </table>
                <table width="100%" style="font-family: Arial; font-size: 9pt;">
                    <tr>
                        <td style="width: 100%; color: #0078C2; font-family: Arial; font-size: 11pt;" colspan="8">
                            <img src="img/id_card.png" style="width: 30px; height: 30px;" alt="Dati Dichiarante" />
                            <strong>Dati Dichiarante</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: 3px solid #CCCCCC;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        Cognome
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCognome" runat="server" TabIndex="4" Width="140px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        Nome
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtNome" runat="server" TabIndex="5" Width="140px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Cod.Fiscale
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCF" runat="server" TabIndex="6" AutoPostBack="True" Width="140px"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                    <td colspan="6">
                                        <asp:LinkButton ID="CFLABEL" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <asp:UpdatePanel ID="UpdatePanelNascita" runat="server">
                                        <ContentTemplate>
                                            <td>
                                                Nato in
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbNazioneNas" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                    Font-Names="Arial" Font-Size="9pt" TabIndex="7" Width="145px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblProvincia" runat="server" Text="Provincia"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbPrNas" runat="server" AutoPostBack="True" CssClass="CssProv"
                                                    Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="8" Width="50px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;<asp:Label ID="lblComune" runat="server" Text="Comune"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbComuneNas" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                    Font-Names="Arial" Font-Size="9pt" TabIndex="9" Width="145px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Data
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDataNascita" runat="server" Columns="7" MaxLength="10" TabIndex="10"
                                                    CssClass="CssMaiuscolo" Width="90px"></asp:TextBox>
                                                <asp:Label ID="lblErrData" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
                                                    Font-Size="X-Small" ForeColor="Red" Visible="False"></asp:Label>
                                            </td>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </tr>
                                <tr>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        Tipo Documento
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:DropDownList ID="cmbTipoDocumento" runat="server" CssClass="CssProv" TabIndex="11"
                                            Width="145px">
                                            <asp:ListItem Selected="True" Value="0">CARTA IDENTITA</asp:ListItem>
                                            <asp:ListItem Value="1">PASSAPORTO</asp:ListItem>
                                            <asp:ListItem Value="2">PATENTE DI GUIDA</asp:ListItem>
                                            <asp:ListItem Value="-1">--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        Num. Documento
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:TextBox ID="txtCINum" runat="server" TabIndex="12"></asp:TextBox>
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        Data
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:TextBox ID="txtCIData" runat="server" TabIndex="13" Width="70px"></asp:TextBox>
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        Rilasciata da
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:TextBox ID="txtCIRilascio" runat="server" TabIndex="14" CssClass="CssMaiuscolo"
                                            Width="140px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Permesso Soggiorno N.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPSNum" runat="server" TabIndex="15" Width="140px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Data
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPSData" runat="server" TabIndex="16" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Scadenza
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPSScade" runat="server" MaxLength="10" TabIndex="18" CssClass="CssMaiuscolo"
                                            Width="140px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Rinnovo
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPSRinnovo" runat="server" MaxLength="10" TabIndex="19" CssClass="CssMaiuscolo"
                                            Width="140px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Carta Soggiorno N.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCSNum" runat="server" TabIndex="20" Width="140px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Data
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCSData" runat="server" TabIndex="21" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        Attività Lavorativa
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:DropDownList ID="cmbLavorativa" runat="server" CssClass="CssProv" Font-Bold="False"
                                            TabIndex="22" Width="45px">
                                            <asp:ListItem Selected="True" Value="1">SI</asp:ListItem>
                                            <asp:ListItem Value="0">NO</asp:ListItem>
                                            <asp:ListItem Value="9">--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="2" style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="ChNatoEstero" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            Text="Trattasi di Italiano nato all'estero" TabIndex="23" />
                                    </td>
                                    <td colspan="3" style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="ChCittadinanza" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            Text="Nato all'estero ma in possesso di cittadinanza" TabIndex="24" />
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        Il Dichiarante è:
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="chIntestatario" runat="server" Text="Intestatario di Contratto"
                                            Width="160px" TabIndex="25" />
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="chCompNucleo" runat="server" Text="Comp. nucleo" Width="160px"
                                            TabIndex="26" />
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="chRappLegale" runat="server" Text="Rappresentante legale" Width="150px"
                                            TabIndex="27" />
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        La dichiarazione è stata ricevuta:
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="chPosta" runat="server" Width="190px" Text="Tramite Posta" TabIndex="28" />
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="chSindacato" runat="server" Width="230px" Text="Tramite sindacato"
                                            TabIndex="29" />
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="chComitati" runat="server" TabIndex="31" Text="Tramite Comitati"
                                            Width="130px" />
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="chInServizio" runat="server" TabIndex="32" Text="In servizio" Width="130px" />
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:CheckBox ID="chDomicilio" runat="server" TabIndex="33" Text="Visita a Domicilio"
                                            Width="130px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                        <asp:Label ID="lblData392" runat="server" Text="Data Disdetta Comunicata (MM o prec. gestore)"
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;"
                                        colspan="5">
                                        <asp:TextBox ID="txtData392" runat="server" TabIndex="30" Visible="False" Width="70px"></asp:TextBox>
                                        &nbsp;<asp:Label ID="lblAvviso393" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                            ForeColor="Maroon" Visible="False"></asp:Label>
                                        &nbsp;<asp:Label ID="lblAvviso394" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                            ForeColor="Maroon" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanelResidenzaHead" runat="server">
                    <ContentTemplate>
                        <table width="100%" style="font-family: Arial; font-size: 9pt;">
                            <tr>
                                <td style="color: #0078C2; font-family: Arial; font-size: 11pt; vertical-align: top;"
                                    class="style1">
                                    <img src="img/Img_Alloggio.png" style="width: 26px; height: 26px;" alt="Dati Dichiarante" />
                                    <strong>Dati Residenza</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="btnImportaRes" runat="server" ImageUrl="~/ANAUT/img/import-icon.png"
                                        Style="cursor: pointer" ToolTip="Recupera i dati dell'unità presente nel contratto" />
                                    &nbsp;<strong><asp:Label ID="lblImportaRes" runat="server" Text="Importa dati da Alloggio"
                                        Visible="False"></asp:Label>
                                    </strong>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 3px solid #CCCCCC;">
                                    <%--<asp:UpdatePanel ID="UpdatePanelResidenza" runat="server">
                                        <ContentTemplate>--%>
                                    <table width="100%" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td>
                                                Residenza&nbsp
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbNazioneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                    Font-Names="Arial" Font-Size="9pt" TabIndex="34" Enabled="True" Width="290px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Provincia
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbPrRes" runat="server" AutoPostBack="True" CssClass="CssProv"
                                                    Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="35" Width="70px"
                                                    Enabled="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Comune
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbComuneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                    Font-Names="Arial" Font-Size="9pt" TabIndex="36" Enabled="True" Width="130px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Indirizzo
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbTipoIRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                    Font-Names="Arial" Font-Size="9pt" TabIndex="37" Enabled="True" Width="80px">
                                                </asp:DropDownList>
                                                &nbsp
                                                <asp:TextBox ID="txtIndRes" runat="server" TabIndex="35" Width="195px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Civico
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCivicoRes" runat="server" TabIndex="38" Width="60px"></asp:TextBox>
                                            </td>
                                            <td>
                                                CAP
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCAPRes" runat="server" TabIndex="39" Width="40px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Telefono
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTelRes" runat="server" TabIndex="40" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Tel. Aggiuntivo
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTelefono" runat="server" Width="100px" TabIndex="41"></asp:TextBox>
                                            </td>
                                            <td>
                                                Email
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmail" runat="server" Width="100px" TabIndex="42"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                Cod.Alloggio
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                <asp:TextBox ID="txtPosizione" runat="server" TabIndex="43" Width="282px"></asp:TextBox>&nbsp
                                                <asp:Image ID="imgPatrimonio" runat="server" Style="cursor: pointer; width: 18px;
                                                    height: 18px; vertical-align: top;" ImageUrl="../NuoveImm/Img_Info.png" ToolTip="Visualizza i dati dell'unità immobiliare" />
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                Scala
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                <asp:TextBox ID="txtScala" runat="server" TabIndex="44" Width="60px"></asp:TextBox>
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                Piano
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                <asp:TextBox ID="txtPiano" runat="server" TabIndex="45" Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                Num. Alloggio
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                <asp:TextBox ID="txtAlloggio" runat="server" TabIndex="46" Width="40px"></asp:TextBox>
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                Foglio
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                <asp:TextBox ID="txtFoglio" runat="server" TabIndex="47" Width="40px"></asp:TextBox>
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                Map
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                <asp:TextBox ID="txtMappale" runat="server" TabIndex="47" Width="40px"></asp:TextBox>
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                Sub
                                            </td>
                                            <td style="border-top-style: dotted; border-top-width: thin; border-top-color: #C0C0C0;">
                                                <asp:TextBox ID="txtSub" runat="server" TabIndex="48" Width="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanelInfo" runat="server">
                    <ContentTemplate>
                        <table width="100%" style="font-family: Arial; font-size: 9pt;">
                            <tr>
                                <td style="width: 100%; color: #0078C2; font-family: Arial; font-size: 11pt; vertical-align: top;">
                                    <img src="img/Mail-icon.png" style="width: 26px; height: 26px;" alt="Dati Dichiarante" />
                                    <strong>Dati Recapito</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton
                                        ID="btnImportaRec" runat="server" ImageUrl="~/ANAUT/img/import-icon.png" Style="cursor: pointer"
                                        ToolTip="Recupera i dati dal contratto" />
                                    &nbsp;<asp:Label ID="lblImportaRec" runat="server" Text="Importa dati da Contratto"
                                        Visible="False" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 3px solid #CCCCCC;">
                                    <table width="100%" style="font-family: Arial; font-size: 8pt;" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td>
                                                Riceve comunicazioni presso
                                            </td>
                                            <td colspan="1">
                                                Indirizzo
                                            </td>
                                            <td>
                                                Civico
                                            </td>
                                            <td>
                                                CAP
                                            </td>
                                            <td>
                                                Provincia
                                            </td>
                                            <td>
                                                Comune
                                            </td>
                                            <td>
                                                Presso (Indicare intestatario o altro Nominativo)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkRicevePresso" runat="server" TabIndex="49" Text="" Width="53px"
                                                    AutoPostBack="True" />
                                            </td>
                                            <td colspan="1">
                                                <asp:DropDownList ID="cmbTipoISped" runat="server" Enabled="True" Font-Names="Arial"
                                                    Font-Size="9pt" TabIndex="50" Width="80px">
                                                </asp:DropDownList>
                                                &nbsp;
                                                <asp:TextBox ID="txtIndirizzo" runat="server" Font-Size="9pt" Style="text-transform: uppercase;"
                                                    TabIndex="51" Width="320px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCivicoSpediz" runat="server" Font-Size="9pt" TabIndex="52"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCapSpediz" runat="server" Font-Size="9pt" TabIndex="53" Width="60px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbPrSpediz" runat="server" AutoPostBack="True" Font-Bold="False"
                                                    Font-Names="Arial" Font-Size="9pt" TabIndex="54" Width="150px" Enabled="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbComuneSpediz" runat="server" AutoPostBack="True" Font-Bold="False"
                                                    Font-Names="Arial" Font-Size="9pt" TabIndex="55" Width="150px" Enabled="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPressoIndirizzo" runat="server" Font-Size="9pt" Style="text-transform: uppercase;"
                                                    TabIndex="56" Width="218px" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            <div id="tabs">
                                <ul>
                                    <li><a href="#tabs-1">Nucleo</a></li>
                                    <li><a href="#tabs-2">Reddito</a></li>
                                    <li><a href="#tabs-3">Patrimonio</a></li>
                                    <li><a href="#tabs-4">ISEE</a></li>
                                    <li><a href="#tabs-5">Altre Inform.</a></li>
                                    <li><a href="#tabs-6">Documenti</a></li>
                                    <li><a href="#tabs-7">Diffide
                                        <asp:Image ID="imgDiffide" runat="server" ImageUrl="../CALL_CENTER/Immagini/alertSegn.gif"
                                            Height="15px" Visible="False" />
                                    </a></li>
                                </ul>
                                <div id="tabs-1">
                                    <uc1:tab_nucleo id="Tab_Nucleo1" runat="server" />
                                </div>
                                <div id="tabs-2">
                                    <uc2:dic_reddito id="Dic_Reddito1" runat="server" />
                                </div>
                                <div id="tabs-3">
                                    <uc3:dic_patrimonio id="Dic_Patrimonio1" runat="server" />
                                </div>
                                <div id="tabs-4">
                                    <uc7:Tab_ISEE id="Tab_ISEE1" runat="server" />
                                </div>
                                <div id="tabs-5">
                                    <uc4:tab_infocontratto id="Tab_InfoContratto1" runat="server" />
                                </div>
                                <div id="tabs-6">
                                    <uc5:dic_documenti id="dic_Documenti1" runat="server" />
                                </div>
                                <div id="tabs-7">
                                    <uc6:tab_diffide id="Tab_Diffide1" runat="server" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="confCanc" runat="server" Value="0" />
            <asp:HiddenField ID="provenienza" runat="server" Value="0" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnfunzelimina" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnfunzesci2" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <p>
        &nbsp;
    </p>
    <div id="dialog" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
    <div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
    <asp:Button ID="btnfunzelimina" runat="server" Text="" Style="display: none;" CauseValidation="false" />
    <asp:Button ID="btnfunzesci2" runat="server" Text="" Style="display: none;" CauseValidation="false" />
    <asp:HiddenField ID="propdec" runat="server" />
    <asp:HiddenField ID="txtTab" runat="server" />
    <asp:HiddenField ID="H1" runat="server" Value="0" />
    <%--   <asp:HiddenField ID="txtModificato" runat="server" Value="0" />--%>
    <asp:HiddenField ID="assTemp" runat="server" />
    <asp:HiddenField ID="txt36" runat="server" Value="1" />
    <asp:HiddenField ID="solalettura" runat="server" Value="0" />
    <asp:HiddenField ID="txtbinserito" runat="server" Value="0" />
    <asp:HiddenField ID="vcomunitario" runat="server" Value="0" />
    <asp:HiddenField ID="lotto45" runat="server" Value="0" />
    <asp:HiddenField ID="nonstampare" runat="server" Value="0" />
    <asp:HiddenField ID="txtbinseritoTab" runat="server" Value="0" />
    <asp:HiddenField ID="tabSelect" runat="server" Value="-1" />
    <asp:HiddenField ID="numComp" runat="server" Value="0" />
    <asp:HiddenField ID="idComp" runat="server" Value="0" />
    <asp:HiddenField ID="salvaEsterno" runat="server" Value="0" />
    <asp:HiddenField ID="stampaClick" runat="server" Value="0" />
    <asp:HiddenField ID="hiddenLockCorrenti" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="contrLocked" runat="server" Value="0" />
    <asp:HiddenField ID="PageID" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="inUscita" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="InLettura" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="RU392" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="RU431" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="lIdUnita" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="sIndirizzoUI" runat="server" Value="" ClientIDMode="Static" />
    </form>
</body>
<script type="text/javascript">

        if (document.getElementById('caric')) {
            document.getElementById('caric').style.visibility = 'hidden';
        }

        initialize();

        function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }

        }

        function AttendiCF() {

            LeftPosition = (screen.width) ? (screen.width - 250) / 2 : 0;
            TopPosition = (screen.height) ? (screen.height - 150) / 2 : 0;
            LeftPosition = LeftPosition;
            TopPosition = TopPosition;
            aaa = window.open('loadingCF.htm', '', 'height=150,top=' + TopPosition + ',left=' + LeftPosition + ',width=250');
            setTimeout("aaa.close();", 5000);
        }

        function AzzeraCF(C1, C2) {
            //C1.value='';
            C2.value = '0';
            if (document.getElementById('txtCF').value != '') {
                document.getElementById('txtbinserito').value = '0';
                document.getElementById('txtCF').value = '';
            }
        }

        function initialize() {

            var availableTags = [
                "ActionScript",
                "AppleScript",
                "Asp",
                "BASIC",
                "C",
                "C++",
                "Clojure",
                "COBOL",
                "ColdFusion",
                "Erlang",
                "Fortran",
                "Groovy",
                "Haskell",
                "Java",
                "JavaScript",
                "Lisp",
                "Perl",
                "PHP",
                "Python",
                "Ruby",
                "Scala",
                "Scheme"
            ];

            $('#tabs').tabs();

            $(document).ready(function () {
                $("#tabs").bind('tabsselect', function (event, ui) {
                    window.location.href = ui.tab;
                    document.getElementById("tabSelect").value = $('#tabs').tabs('option', 'selected')
                }
                );
            }
            );

            if (document.getElementById("tabSelect").value != '-1') {
                document.getElementById("tabSelect").value = $('#tabs').tabs('option', 'selected') + 1
                $('#tabs').tabs('select', document.getElementById("tabSelect").value);
            }
        }

        if (document.getElementById('numComp').value >= 10) {
            document.getElementById('elencoComp').style.height = '150px';
        }


        /* SCRIPT TAB NUCLEO */

        function MyDialogArguments() {
            this.Sender = null;
            this.StringValue = "";
        }


        function AggiungiNucleo() {
            a = document.getElementById('Tab_Nucleo1_txtProgr').value;
            dialogArgs = new MyDialogArguments();
            dialogArgs.StringValue = a;
            dialogArgs.Sender = window;
            var dialogResults = window.showModalDialog("com_nucleo.aspx?OP=0&PR=" + a + "&IDCONN=" + <%=vIdConnessione %> +"&IDDICH=" + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:505px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
        if (dialogResults != undefined) {
            if (dialogResults == '1') {
                document.getElementById('salvaEsterno').value = '1';
                document.getElementById('Tab_Nucleo1_btnAggiungiNucleo').click();
            }
            if (dialogResults == '2') {
                document.getElementById('txtModificato').value = '1';
            }
        }
    }

    function ModificaNucleo() {

        a = document.getElementById('Tab_Nucleo1_txtProgr').value;
        if (document.getElementById('idComp').value == 0) {
            message('Attenzione', 'Selezionare una riga dalla lista!');
            return false;
        }
        else {
            cognome = document.getElementById('Tab_Nucleo1_cognome').value
            nome = document.getElementById('Tab_Nucleo1_nome').value
            data = document.getElementById('Tab_Nucleo1_data_nasc').value
            cf = document.getElementById('Tab_Nucleo1_cod_fiscale').value
            parenti = document.getElementById('Tab_Nucleo1_parentela').value
            inv = document.getElementById('Tab_Nucleo1_perc_inval').value
            asl = document.getElementById('Tab_Nucleo1_asl').value
            acc = document.getElementById('Tab_Nucleo1_ind_accomp').value
            tipo_inval = document.getElementById('Tab_Nucleo1_tipo_inval').value
            natura_inval = document.getElementById('Tab_Nucleo1_natura_inval').value
            telefono1 = document.getElementById('Tab_Nucleo1_telefono1').value
            telefono2 = document.getElementById('Tab_Nucleo1_telefono2').value
            mail1 = document.getElementById('Tab_Nucleo1_mail1').value
            mail2 = document.getElementById('Tab_Nucleo1_mail2').value
            l104 = document.getElementById('Tab_Nucleo1_l104').value
            var dialogResults = window.showModalDialog("com_nucleo.aspx?OP=1&RI=" + document.getElementById('idComp').value + "&IDCONN=<%=vIdConnessione %>&COGNOME=" + cognome + "&NOME=" + nome + "&DATA=" + data + "&CF=" + cf + "&PARENTI=" + parenti + "&INV=" + inv + "&ASL=" + asl + "&ACC=" + acc + "&PR=" + a + "&TIPO_INVAL=" + tipo_inval + "&NATURA_INVAL=" + natura_inval + "&TELEFONO1=" + telefono1 + "&TELEFONO2=" + telefono2 + "&MAIL1=" + mail1 + "&MAIL2=" + mail2 + "&L104=" + l104, window, 'status:no;dialogWidth:505px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
            //var dialogResults = window.showModalDialog("com_nucleo.aspx?OP=1&RI=" + document.getElementById('idComp').value + "&IDCONN=<%=vIdConnessione %>&COGNOME=" + cognome + "&NOME=" + nome + "&DATA=" + data + "&CF=" + cf + "&PARENTI=" + parenti + "&INV=" + inv + "&ASL=" + asl + "&ACC=" + acc + "&PR=" + a + "&T_INVAL=" + tipo_inval + "&N_INVAL=" + natura_inval + "&TEL1=" + telefono1 + "&TEL2=" + "1", window, 'status:no;dialogWidth:505px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('Tab_Nucleo1_btnAggiungiNucleo').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';

                }
            }
        }
    }

    function ModificaSpese() {

        if (document.getElementById('Tab_Nucleo1_idCompSpese').value == 0) {
            message('Attenzione', 'Selezionare una riga dalla lista!');
            return false;
        }
        else {
            componente = document.getElementById('Tab_Nucleo1_componente').value
            importo = document.getElementById('Tab_Nucleo1_importo').value
            descrizione = document.getElementById('Tab_Nucleo1_descrizione').value
            var dialogResults = window.showModalDialog("com_spese.aspx?RI=" + document.getElementById('Tab_Nucleo1_idCompSpese').value + "&CM=" + componente + "&IM=" + importo + "&DS=" + descrizione + "&IDCONN=" + <%=vIdConnessione %>, window, 'status:no;dialogWidth:505px;dialogHeight:405px;dialogHide:true;help:no;scroll:no');
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('Tab_Nucleo1_btnAggiungiSpesa').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
            }
        }
    }

    /* FINE SCRIPT TAB NUCLEO */


    /* SCRIPT DETRAZIONI */

    function AggiungiDetrazioni() {
        a = document.getElementById('Dic_Reddito1_idCompDetraz').value;
        dialogArgs = new MyDialogArguments();
        dialogArgs.StringValue = a;
        dialogArgs.Sender = window;
        var dialogResults = window.showModalDialog("com_detrazioni.aspx?OP=0&PR=" + a + "&IDCONN=" + <%=vIdConnessione %> +"&IDDICH=" + <%=lIdDichiarazione %> , window, 'status:no;dialogWidth:505px;dialogHeight:405px;dialogHide:true;help:no;scroll:no');
        if (dialogResults != undefined) {
            if (dialogResults == '1') {
                document.getElementById('salvaEsterno').value = '1';
                document.getElementById('Dic_Reddito1_btnAggiungiDetrazione').click();
                document.getElementById('salvaEsterno').value = '0';
            }
            if (dialogResults == '2') {
                document.getElementById('txtModificato').value = '1';
            }
        }
    }


    function ModificaDetrazioni() {
        if (document.getElementById('Dic_Reddito1_idDetraz').value == 0 || document.getElementById('Dic_Reddito1_idDetraz').value == -1) {
            message('Attenzione', 'Selezionare una riga dalla lista!');
            return false;
        }
        else {
            componente = document.getElementById('Dic_Reddito1_idCompDetraz').value;
            importo = document.getElementById('Dic_Reddito1_importo').value;
            tipo = document.getElementById('Dic_Reddito1_tipoDetraz').value;

            var dialogResults = window.showModalDialog("com_detrazioni.aspx?OP=1&RI=" + document.getElementById('Dic_Reddito1_idDetraz').value + "&IDCONN=" + <%=vIdConnessione %> +"&COMPONENTE=" + componente + "&COMP=" + componente + "&IM=" + importo + "&TI=" + tipo + "&IDDICH=" + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:505px;dialogHeight:405px;dialogHide:true;help:no;scroll:no');

            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('Dic_Reddito1_btnAggiungiDetrazione').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';
                }
            }
        }
    }
    /* FINE SCRIPT DETRAZIONI */


    /* SCRIPT PATRIMONIO MOBILIARE */

    function AggiungiPatrimMob() {
        a = document.getElementById('Dic_Patrimonio1_idCompMob').value;
        dialogArgs = new MyDialogArguments();
        dialogArgs.StringValue = a;
        dialogArgs.Sender = window;
        var dialogResults = window.showModalDialog("com_patrimonio.aspx?OP=0&PR=" + a + "&IDCONN=" + <%=vIdConnessione %> +"&IDDICH=" + <%=lIdDichiarazione %> , window, 'status:no;dialogWidth:505px;dialogHeight:405px;dialogHide:true;help:no;scroll:no');

        if (dialogResults != undefined) {
            if (dialogResults == '1') {
                document.getElementById('salvaEsterno').value = '1';
                document.getElementById('Dic_Patrimonio1_btnAggiungiReddM').click();
                document.getElementById('salvaEsterno').value = '0';
            }
            if (dialogResults == '2') {
                document.getElementById('txtModificato').value = '1';
            }
        }

    }

    function ModificaPatrimMob() {
        if (document.getElementById('Dic_Patrimonio1_idCompMob').value == 0 || document.getElementById('Dic_Patrimonio1_idCompMob').value == -1) {
            message('Attenzione', 'Selezionare una riga dalla lista!');
            return false;
        }
        else {
            componente = document.getElementById('Dic_Patrimonio1_idCompMob').value;
            abi = document.getElementById('Dic_Patrimonio1_abi').value;

            inter = document.getElementById('Dic_Patrimonio1_inter').value;
            imp = document.getElementById('Dic_Patrimonio1_importo').value;
            tipo = document.getElementById('Dic_Patrimonio1_tipo').value;
            prp = document.getElementById('Dic_Patrimonio1_prp').value;
            var dialogResults = window.showModalDialog("com_patrimonio.aspx?PRP=" + prp + "&OP=1&RI=" + document.getElementById('Dic_Patrimonio1_idPatrMob').value + "&IDCONN=" + <%=vIdConnessione %> +"&COMPONENTE=" + componente + "&COMP=" + componente + "&ABI=" + abi + "&INT=" + inter + "&IMP=" + imp + "&TIPO=" + tipo + "&IDDICH=" + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:505px;dialogHeight:405px;dialogHide:true;help:no;scroll:no');

            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('Dic_Patrimonio1_btnAggiungiReddM').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';
                }
            }
        }
    }

    /* FINE SCRIPT PATRIMONIO MOBILIARE */


    /* SCRIPT PATRIMONIO IMMOBILIARE */

    function AggiungiPatrimonioI() {
        a = document.getElementById('Dic_Reddito1_idCompDetraz').value;
        dialogArgs = new MyDialogArguments();
        dialogArgs.StringValue = a;
        dialogArgs.Sender = window;

        var dialogResults = window.showModalDialog("com_patrimonioI.aspx?OP=0&COMPONENTE=" + a + "&IDDICH=" + <%=lIdDichiarazione %> + "&IDCONN=" + <%=vIdConnessione %>,window, 'status:no;dialogWidth:505px;dialogHeight:455px;dialogHide:true;help:no;scroll:no');
        if (dialogResults != undefined) {
            if (dialogResults == '1') {
                document.getElementById('salvaEsterno').value = '1';
                document.getElementById('Dic_Patrimonio1_btnAggiungiReddI').click();
                document.getElementById('salvaEsterno').value = '0';
            }
            if (dialogResults == '2') {
                document.getElementById('txtModificato').value = '1';
            }
        }
    }

    function ModificaPatrimonioI() {

        if (document.getElementById('Dic_Patrimonio1_idCompImmob').value == 0 || document.getElementById('Dic_Patrimonio1_idCompImmob').value == -1) {
            message('Attenzione', 'Selezionare una riga dalla lista!');
            return false;
        }
        else {
            componente = document.getElementById('Dic_Patrimonio1_idCompImmob').value;
            tipo = document.getElementById('Dic_Patrimonio1_tipoImmob').value;
            tipoPropr = document.getElementById('Dic_Patrimonio1_tipoPropr').value;
            perc = document.getElementById('Dic_Patrimonio1_perc').value;
            valore = document.getElementById('Dic_Patrimonio1_valore').value;
            mutuo = document.getElementById('Dic_Patrimonio1_mutuo').value;

            catastale = document.getElementById('Dic_Patrimonio1_catastale').value;
            comune = document.getElementById('Dic_Patrimonio1_comune').value;
            vani = document.getElementById('Dic_Patrimonio1_vani').value;
            sup = document.getElementById('Dic_Patrimonio1_sup').value;
            pie = document.getElementById('Dic_Patrimonio1_pie').value;

            indirizzo = document.getElementById('Dic_Patrimonio1_indirizzo').value;
            civico = document.getElementById('Dic_Patrimonio1_civico').value;
            rendita = document.getElementById('Dic_Patrimonio1_rendita').value;
            ven = document.getElementById('Dic_Patrimonio1_ven').value;
            var dialogResults = window.showModalDialog("com_patrimonioI.aspx?VEN=" + ven + "&OP=1&RI=" + document.getElementById('Dic_Patrimonio1_idPatrImmob').value + "&IDCONN=" + <%=vIdConnessione %> +"&COMPONENTE=" + componente + "&COMP=" + componente + "&TIPO=" + tipo + "&TIPOPR=" + tipoPropr + "&PER=" + perc + "&VAL=" + valore + "&MU=" + mutuo + "&SUP=" + sup + "&VANI=" + vani + "&comune=" + comune + "&CATASTALE=" + catastale + "&PIE=" + pie + "&IND=" + indirizzo + "&CIV=" + civico + "&RENDITA=" + rendita + "&IDDICH=" + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:505px;dialogHeight:455px;dialogHide:true;help:no;scroll:no');
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('Dic_Patrimonio1_btnAggiungiReddI').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';
                }
            }
        }
    }

    /* FINE SCRIPT PATRIMONIO IMMOBILIARE */




    /* SCRIPT TAB DOCUMENTI */

    function AggiungiDocumento() {

        dialogArgs = new MyDialogArguments();
        dialogArgs.StringValue = '';
        dialogArgs.Sender = window;

        var dialogResults = window.showModalDialog('com_documenti.aspx?OP=0&IDCONN=' + <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %>, 'window', 'status:no;dialogWidth:505px;dialogHeight:405px;dialogHide:true;help:no;scroll:no');
        if (dialogResults != undefined) {
            if (dialogResults == '1') {
                document.getElementById('salvaEsterno').value = '1';
                document.getElementById('dic_Documenti1_btnAggiungiDoc').click();
                document.getElementById('salvaEsterno').value = '0';
            }
            if (dialogResults == '2') {
                document.getElementById('txtModificato').value = '1';
            }
        }
    }

    /* FINE SCRIPT TAB DOCUMENTI */


    /* SCRIPT TAB REDDITO */
    function ApriDettReddito() {
        var dialogResults = window.showModalDialog('Dett_Reddito.aspx?S=' + document.getElementById('InLettura').value + '&MOD=0&IDCONN=' + <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:820px;dialogHeight:720px;dialogHide:true;help:no;scroll:no;resizable:no;');
        if (dialogResults != undefined) {
            if (dialogResults == '1') {
                document.getElementById('salvaEsterno').value = '1';
                document.getElementById('Dic_Reddito1_btnAggiungiReddito').click();
                document.getElementById('salvaEsterno').value = '0';
            }
            if (dialogResults == '2') {
                document.getElementById('txtModificato').value = '1';
            }
        }
    }

    function ModificaDettReddito() {
        if (document.getElementById('Dic_Reddito1_idReddito').value != '&nbsp;' && document.getElementById('Dic_Reddito1_idReddito').value != '' && document.getElementById('Dic_Reddito1_idReddito').value != '-1' & document.getElementById('Dic_Reddito1_idReddito').value != '0') {
            var dialogResults = window.showModalDialog('Dett_Reddito.aspx?S=' + document.getElementById('InLettura').value + '&MOD=1&IDCONN=' + <%=vIdConnessione %> +'&IDREDD=' + document.getElementById('Dic_Reddito1_idReddito').value + '&IDDICH=' + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:820px;dialogHeight:720px;dialogHide:true;help:no;scroll:no;resizable:no;');
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('Dic_Reddito1_btnAggiungiReddito').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';
                }
            }
        }
        else {
            message('Attenzione', 'Selezionare una riga dalla lista!');
            return false;
        }
    }
    /* FINE SCRIPT TAB REDDITO */

    function VisualizzaDettReddito() {
        if (document.getElementById('Dic_Reddito1_idReddito').value != '&nbsp;' && document.getElementById('Dic_Reddito1_idReddito').value != '' && document.getElementById('Dic_Reddito1_idReddito').value != '-1' & document.getElementById('Dic_Reddito1_idReddito').value != '0') {
            var dialogResults = window.showModalDialog('Dett_Reddito.aspx?S=' + document.getElementById('InLettura').value + '&MOD=1&IDCONN=' + <%=vIdConnessione %> +'&IDREDD=' + document.getElementById('Dic_Reddito1_idReddito').value + '&IDDICH=' + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:820px;dialogHeight:720px;dialogHide:true;help:no;scroll:no;resizable:no;');
        }
        else {
            message('Attenzione', 'Selezionare una riga dalla lista!');
            return false;
        }
    }

    /* FUNZIONI PULSANTI */
    function Indici() {
        window.open('Indici.aspx?ID=' + <%=lIdDichiarazione %>, 'Indici', 'top=0,left=0,width=490,height=650');
    }

    function Eventi() {
        window.open('Eventi.aspx?ID=' + <%=lIdDichiarazione %> + '&IDCONN=' + <%=vIdConnessione %>, 'Eventi', '');
    }

    function PropDec() {
        if (document.getElementById('propdec').value == '1') {
            window.open('ProposteDec.aspx?ID=' + <%= lIdDichiarazione %> +'&PG=' + document.getElementById('lblPG').value + '&I=0', 'Proposte', 'top=0,left=0,width=498,height=650');
        }
        else {
            message('Attenzione', 'Non disponibile!');
        }
    }

    function CalcoloCanone() {
        if (document.getElementById('Tab_InfoContratto1_TxtRapporto').value != '') {
            if (document.getElementById('assTemp').value == '') {
                window.open("../Contratti/AU_abusivi/Canone.aspx?ID=" + <%=lIdDichiarazione %> + '&COD=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value + '&IDUNITA=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value, '', 'top=0,left=0,width=550,height=600,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes');
          }
          else {
              window.open("../Contratti/AU_temporanee/Canone.aspx?ID=" + <%=lIdDichiarazione %> + '&COD=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value + '&IDUNITA=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value, '', 'top=0,left=0,width=550,height=600,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes');
            }
        }
        else {
            message('Attenzione', 'Inserire il codice del contratto e assicurarsi di aver elaborato la domanda!');
        }
    }

    function StampaFronte() {
        Uscita = 0;
        if (document.getElementById('txtModificato').value != '1') {
            var win = null;
            LeftPosition = (screen.width) ? (screen.width - 400) / 2 : 0;
            TopPosition = (screen.height) ? (screen.height - 400) / 2 : 0;
            LeftPosition = LeftPosition;
            TopPosition = TopPosition;
            //aa = window.open('Stampe/Fascicolo.aspx?COD=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'fascicolo', 'resizable=yes,height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=800');
            aa = window.showModalDialog('Stampe/Fascicolo.aspx?COD=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', window, 'status:no;dialogWidth:800px;dialogHeight:480px;dialogHide:true;help:no;scroll:no;resizable:yes;');
            //btnCaricaDoc
            if (document.getElementById('btnCaricaDoc')) {
                document.getElementById('btnCaricaDoc').click();
            }
        }
        else {
            message('Attenzione', 'Sono state effettuate modifiche, premere il pulsante SALVA prima di procedere!');
        }
    }

    function ElencoModelliStampa() {
        Uscita = 0;
        //if (document.getElementById('nonstampare').value == '1') {
        if (document.getElementById('txtModificato').value != '1') {
            var win = null;
            LeftPosition = (screen.width) ? (screen.width - 400) / 2 : 0;
            TopPosition = (screen.height) ? (screen.height - 400) / 2 : 0;
            LeftPosition = LeftPosition;
            TopPosition = TopPosition;
            if (document.getElementById('lotto45').value == '0') {
                if (document.getElementById('Tab_InfoContratto1_TxtRapporto').value != '') {
                    aa = window.open('ElencoModelliStampa.aspx?45=0&COD=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Modelli', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=800');
                        }
                        else {
                            message('Attenzione', 'Inserire il codice del rapporto, premere il pulsante SALVA prima di procedere!');
                        }
                    }
                    else {
                        aa = window.open('ElencoModelliStampa.aspx?45=1&COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Modelli', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=800');
                    }
            }
            else {
                message('Attenzione', 'Sono state effettuate modifiche, premere il pulsante SALVA prima di procedere!');
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
                    if (document.getElementById('Tab_InfoContratto1_TxtRapporto').value != '') {
                        aa = window.open('NotificaISEIncompleta.aspx?45=0&COD=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Notifica', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                        }
                        else {
                            message('Attenzione', 'Inserire il codice del rapporto, premere il pulsante SALVA prima di procedere!');
                        }
                    }
                    else {
                        aa = window.open('NotificaISEIncompleta.aspx?45=1&COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Notifica', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                    }
                }
                else {
                    message('Attenzione', 'Sono state effettuate modifiche, premere il pulsante SALVA prima di procedere!');
                }
            }
            else {
                message('Attenzione', 'Non è possibile procedere. Domanda non modificabile!');
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
                    if (document.getElementById('Tab_InfoContratto1_TxtRapporto').value != '') {
                        aa = window.open('NotificaISE.aspx?45=0&COD=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Notifica', 'height=480,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                        }
                        else {
                            message('Attenzione', 'Inserire il codice del rapporto, premere il pulsante SALVA prima di procedere!');
                        }
                    }
                    else {
                        aa = window.open('NotificaISE.aspx?45=1&COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Notifica', 'height=450,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                    }

                }
                else {
                    message('Attenzione', 'Sono state effettuate modifiche, premere il pulsante SALVA prima di procedere!');
                }
            }
            else {
                message('Attenzione', 'Non è possibile procedere. Domanda non modificabile!');
            }
        }

        function AutocertStServ() {
            Uscita = 0;
            if (document.getElementById('nonstampare').value == '0') {
                if (document.getElementById('txtModificato').value != '1') {
                    var win = null;
                    if (document.getElementById('lotto45').value == '0') {
                        if (document.getElementById('Tab_InfoContratto1_TxtRapporto').value != '') {
                            aa = window.open('Stampe/AutocertStServ.aspx?COD=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Autocert', '');
                    }
                    else {
                        message('Attenzione', 'Inserire il codice del rapporto, premere il pulsante SALVA prima di procedere!');
                    }
                }
                else {
                    aa = window.open('Stampe/AutocertStServ.aspx?COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Autocert', '');
                }
                }
                else {
                    message('Attenzione', 'Sono state effettuate modifiche, premere il pulsante SALVA prima di procedere!');
                }
            }
            else {
                message('Attenzione', 'Non è possibile procedere. Domanda non modificabile!');
            }
        }

        function ElencoStampe() {
            if (document.getElementById('lotto45').value == '0') {
                window.open('Stampe/ElencoStampe.aspx?COD=' + document.getElementById('Tab_InfoContratto1_TxtRapporto').value + '&ID=<%=lIdDichiarazione %>', 'Elenco', '');
            }
            else {
                window.open('Stampe/ElencoStampe.aspx?COD=<%=lIdDichiarazione %>&ID=<%=lIdDichiarazione %>', 'Elenco', '');
             }
             Uscita = 0;
         }




         function Verifica() {

             SceltaFunzioneOP1('Sicuri di voler eliminare l\' elemento selezionato?');

         }

         function SceltaFunzioneOP1(TestoMessaggio) {
             $(document).ready(function () {
                 $('#ScriptScelta').text(TestoMessaggio);
                 $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('<%=btnfunzelimina.ClientID %>', ''); { $(this).dialog('close'); document.getElementById('caric').style.visibility = 'visible'; } }, 'No': function () { $(this).dialog('close'); document.getElementById('dic_Documenti1_idDoc').value = '-1'; " & Funzione2 & " } } });
        });
    }

    function ConfermaEsci() {
        SceltaFunzioneOP2('Attenzione...Sono state apportate delle modifiche.\nUscire senza salvare causerà la perdita delle modifiche!\nUscire ugualmente? Per non uscire premere ANNULLA.');
    }

    function SceltaFunzioneOP2(TestoMessaggio) {
        $(document).ready(function () {
            $('#ScriptScelta').text(TestoMessaggio);
            $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Ok': function () { __doPostBack('<%=btnfunzesci2.ClientID %>', ''); { $(this).dialog('close'); } }, 'Annulla': function () { $(this).dialog('close'); document.getElementById('txtModificato').value = '111'; " & Funzione2 & " } } });
         });
     }

     function Avviso() {
         message('Attenzione', 'Selezionare un elemento dalla lista!');
     }


     /* FINE FUNZIONI PULSANTI */

     $(function () {
         if (lockTimer == 0)
             lockTimer = setInterval(KeepSessionAliveMaster, 60000);
     });
</script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        var altezzaPaginaIntera = $(window).width() - 5;
        var altezzaContenuto = 0;
        altezzaContenuto = altezzaPaginaIntera - 150;
        $("#elencoCompRedditi").width(altezzaContenuto);
        $("#DivElencoDetr").width(altezzaContenuto);
    });

    $(window).resize(function () {
        var altezzaPaginaIntera = $(window).width() - 5;
        var altezzaContenuto = 0;
        altezzaContenuto = altezzaPaginaIntera - 150;
        $("#elencoCompRedditi").width(altezzaContenuto);
        $("#DivElencoDetr").width(altezzaContenuto);
    });

    $(document).submit(function () {
        var altezzaPaginaIntera = $(window).width() - 5;
        var altezzaContenuto = 0;
        altezzaContenuto = altezzaPaginaIntera - 150;
        $("#elencoCompRedditi").width(altezzaContenuto);
        $("#DivElencoDetr").width(altezzaContenuto);
    });
</script>
</html>
