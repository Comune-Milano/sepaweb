<%@ Page Language="VB" AutoEventWireup="false" CodeFile="domandaNuova.aspx.vb" Inherits="VSA_NuovaDomandaVSA_domandaNuova" %>

<%@ Register Src="Tab_DocAllegati.ascx" TagName="Dom_DocAllegati" TagPrefix="uc9" %>
<%@ Register Src="Tab_Dichiara.ascx" TagName="Dom_Dichiara_Cambi" TagPrefix="uc8" %>
<%@ Register Src="Tab_Alloggio.ascx" TagName="Dom_Alloggio_ERP" TagPrefix="uc7" %>
<%@ Register Src="Tab_Requisiti.ascx" TagName="Dom_Requisiti" TagPrefix="uc6" %>
<%@ Register Src="Tab_Note.ascx" TagName="Note" TagPrefix="uc5" %>
<%@ Register Src="Tab_Decisioni.ascx" TagName="Dom_Decisioni" TagPrefix="uc10" %>
<%@ Register Src="Tab_Ospiti.ascx" TagName="Dom_Ospiti" TagPrefix="uc11" %>
<%@ Register Src="Tab_InfoRateizz.ascx" TagName="Dom_InfoRateizz" TagPrefix="uc12" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
<script type="text/javascript" src="js2/jquery-1.8.2.js"></script>
<script type="text/javascript" src="js2/jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="../../CONTRATTI/jquery.corner.js"></script>
<script type="text/javascript" src="../../CONTRATTI/common.js"></script>
<script type="text/javascript" language="javascript">
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            initialize();
        }
    }
</script>
<head id="Head1" runat="server">
    <title>Domanda VSA</title>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js2/jquery-ui-1.9.0.custom.js"></script>
    <script type="text/javascript" src="js2/jquery-ui-1.9.0.custom.min.js"></script>
    <link rel="stylesheet" type="text/css" href="Styles/impromptu.css" />
    <link href="Styles/StileAU.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        /*function window_onbeforeunload() {
        //aa.close();
        if (document.getElementById('H1').value == 1) {
        event.returnValue = "Attenzione...Uscire dalla Domanda utilizzando il pulsante ESCI!! In caso contrario la domanda VERRA' BLOCCATA E NON SARA' PIU' POSSIBILE MODIFICARE!";
        }
        }*/

        function visibleMotivazioni() {
            if (document.getElementById('divCondNonAccolta')) {
                if (document.getElementById('Dom_Decisioni1_rdbListDecisione_1').checked == true) {
                    document.getElementById('divCondNonAccolta').style.visibility = 'visible'
                }
                else {
                    document.getElementById('divCondNonAccolta').style.visibility = 'hidden'
                }


            }
            if (document.getElementById('divCondNonAccolta2')) {
                if (document.getElementById('Dom_Decisioni1_rdbListRiesame_1').checked == true) {
                    document.getElementById('divCondNonAccolta2').style.visibility = 'visible'
                }
                else {
                    document.getElementById('divCondNonAccolta2').style.visibility = 'hidden'
                }


            }
            if (document.getElementById('divNoteDecis0')) {
                if (document.getElementById('Dom_Decisioni1_rdbListDecis0_1').checked == true) {
                    document.getElementById('divNoteDecis0').style.visibility = 'visible'
                }
                else {
                    document.getElementById('divNoteDecis0').style.visibility = 'hidden'
                }


            }
            if (document.getElementById('divNoteRies0')) {
                if (document.getElementById('Dom_Decisioni1_rdbListRies0_1').checked == true) {
                    document.getElementById('divNoteRies0').style.visibility = 'visible'
                }
                else {
                    document.getElementById('divNoteRies0').style.visibility = 'hidden'
                }


            }
        }

        // DATA OSSERVAZIONI
        function visibleDataOsserv() {
            if (document.getElementById('divOsservazioni')) {
                if (document.getElementById('Dom_Decisioni1_chkOsserv').checked == true) {
                    document.getElementById('divOsservazioni').style.visibility = 'visible';
                }
                else {
                    document.getElementById('divOsservazioni').style.visibility = 'hidden';
                }
            }
        }

        function divSospesione() {
            //if (document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value != "3")
            //{
            if (document.getElementById('divSospesa')) {
                if (document.getElementById('Dom_Note1_documMancante').value == "1" && document.getElementById('Dom_Decisioni1_esNegativo1').value == "0") {
                    document.getElementById('divSospesa').style.visibility = 'visible';
                    document.getElementById('imgAvvisoSospesa').style.visibility = 'visible';
                    document.getElementById('lblSospesa').style.visibility = 'visible';
                    document.getElementById('resto').style.visibility = 'hidden';
                    //document.getElementById('lblStatoDOM').style.visibility = 'hidden'
                    if (document.getElementById('lblScadenza')) {
                        document.getElementById('imgAlertScadenza').style.visibility = 'hidden';
                        document.getElementById('lblScadenza').style.visibility = 'hidden';
                    }
                    if (document.getElementById('Dom_Note1_chkSosp')) {
                        if (document.getElementById('Dom_Note1_chkSosp').checked == true) {
                            //                            document.getElementById('divSospesa').style.visibility = 'hidden'
                            //                            document.getElementById('imgAvvisoSospesa').style.visibility = 'hidden'
                            //                            document.getElementById('lblSospesa').style.visibility = 'hidden'
                            document.getElementById('divSospesa').style.display = 'none';
                            document.getElementById('imgAvvisoSospesa').style.display = 'none';
                            document.getElementById('lblSospesa').style.display = 'none';
                            document.getElementById('resto').style.visibility = 'visible';
                            if (document.getElementById('lblStatoDOM')) {
                                document.getElementById('lblStatoDOM').style.visibility = 'visible';
                            }
                        }
                    }
                }
                else {
                    //                    document.getElementById('divSospesa').style.visibility = 'hidden';
                    //                    document.getElementById('imgAvvisoSospesa').style.visibility = 'hidden';
                    //                    document.getElementById('lblSospesa').style.visibility = 'hidden';
                    document.getElementById('divSospesa').style.display = 'none';
                    document.getElementById('imgAvvisoSospesa').style.display = 'none';
                    document.getElementById('lblSospesa').style.display = 'none';
                    document.getElementById('resto').style.visibility = 'visible'
                    if (document.getElementById('lblStatoDOM')) {
                        document.getElementById('lblStatoDOM').style.visibility = 'visible';
                    }

                }


            }
            //}
            //else
            //{
            // document.getElementById ('divSospesa').style.visibility = 'hidden'
            //}
        }

        function visibleOspiti() {
            if (document.getElementById('osp')) {

                if (document.getElementById('tipoRichiesta').value == '7') {
                    //document.getElementById('Img1').style.visibility = 'visible'
                    document.getElementById('osp').style.visibility = 'visible'
                }

                else {
                    //document.getElementById('Img1').style.visibility = 'hidden'
                    //document.getElementById('osp').style.visibility = 'hidden'
                    var elem = document.getElementById('osp');
                    elem.parentNode.removeChild(elem);
                }

            }
        }

        function visibleTabRateizz() {
            if (document.getElementById('rateizz')) {

                if (document.getElementById('tipoRichiesta').value == '12') {
                    document.getElementById('rateizz').style.visibility = 'visible'
                }

                else {

                    var elem = document.getElementById('rateizz');
                    elem.parentNode.removeChild(elem);
                }

            }
        }


        function cerca() {
            document.getElementById('txtModificato').value = '0';
            if (document.all) {
                finestra = showModelessDialog('Find.htm', window, 'dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
                finestra.focus
                finestra.document.close()
            }
            else if (document.getElementById) {
                self.find()
            }
            else window.alert('Il tuo browser non supporta questo metodo')
        }


    </script>
</head>
<body style="background-image: url(../../NuoveImm/XBackGround.gif); background-repeat: repeat-x;">
    <div id="caric" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; background-color: #eeeeee; z-index: 500;">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('../Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2222" runat="server" ImageUrl="../../NuoveImm/load.gif" />
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 30%" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 40%" align="center">
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Domanda
                                <br />
                                <br />
                                <asp:Label ID="lblDomNUOVA" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="10pt" Height="18px" ForeColor="Black" BackColor="#CCFFFF" Visible="False">PG Dom. correlata</asp:Label>
                                <asp:Label ID="lblDomNUOVA2" runat="server" CssClass="CssLabel" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="10pt" Height="18px" ForeColor="Black" BackColor="#CCFFFF"
                                    Visible="False"></asp:Label>
                            </span></strong>
                        </td>
                        <td style="width: 30%" align="right">
                            <asp:Image ID="imgAlertScadenza" runat="server" ImageUrl="../../IMG/Alert.gif" Style="height: 17px;"
                                Visible="False" />
                            <asp:Label ID="lblScadenza" runat="server" Text="" Font-Names="arial" Font-Size="8pt"
                                ForeColor="#0000C0" Style="width: 611px;" Font-Bold="True" Visible="False"></asp:Label>
                            <br />
                            <asp:Image ID="imgAvvisoSospesa" runat="server" ImageUrl="../../IMG/Alert.gif" Style="z-index: 195;
                                height: 17px; visibility: hidden;" />
                            <asp:Label ID="lblStatoDOM" runat="server" Font-Names="arial" Font-Size="8pt" Font-Bold="True"
                                ForeColor="Red" Width="95%"></asp:Label>
                            <asp:Label ID="lblSospesa" runat="server" Text="Procedimento SOSPESO!" Font-Names="arial"
                                Font-Size="8pt" ForeColor="#0000C0" Style="visibility: hidden;" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="width: 200px">
                            <asp:Button ID="btnSalva" runat="server" ToolTip="Salva" CssClass="bottone" Text="Salva"
                                OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;document.getElementById('txtModificato').value='0';" />
                            <asp:Button ID="imgStampa" runat="server" Enabled="False" CssClass="bottone" Text="Stampa"
                                OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;ConfermaStampa();"
                                ToolTip="Elabora e Stampa" Width="60px" />
                        </td>
                        <td>
                            <asp:Button ID="imgEventi" runat="server" CssClass="bottone" Width="60px" Text="Eventi"
                                OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;VisualizzaEventi();return false;"
                                ToolTip="Elenco Eventi" Visible="True" />
                        </td>
                        <td>
                            <asp:Button ID="IMGCanone" runat="server" Text="Canone a Regime" CssClass="bottone"
                                OnClientClick="CalcoloCanone();return false;" ToolTip="Calcolo del canone a regime secondo L.R. 27/07 e L.R. 36/2008"
                                Width="120px" />
                        </td>
                        <td width="330px" align="center">
                            &nbsp
                        </td>
                        <td style="text-align: right">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 500px; text-align: right;">
                                        <asp:Button runat="server" CssClass="bottone" Text="Preferenze" ID="IMGPREFERENZE"
                                            ToolTip="Preferenze Utente" Width="90px" Visible="False" />
                                        <asp:Button ID="imgAnagrafe" runat="server" CssClass="bottone" Text="Anagrafe" ToolTip="Anagrafe della popolazione"
                                            Width="75" />
                                        <td style="width: 150px;">
                                            <asp:Menu ID="MenuStampe" runat="server" ForeColor="Black" Orientation="Horizontal"
                                                CssClass="bottoneDoc" RenderingMode="List">
                                                <DynamicHoverStyle BackColor="#FFFFCC" />
                                                <DynamicMenuItemStyle BackColor="White" Height="20px" ItemSpacing="2px" />
                                                <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                                                    VerticalPadding="2px" />
                                                <Items>
                                                    <asp:MenuItem Selectable="False" Value=" " Text="Documentazione"></asp:MenuItem>
                                                </Items>
                                            </asp:Menu>
                                        </td>
                                        <td style="width: 42px;">
                                            <asp:Button ID="imgUscita" runat="server" ToolTip="Esci" CssClass="bottone" Text="Esci"
                                                Width="42px" OnClientClick="document.getElementById('H1').value=0;" />
                                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width: 37%">
                            &nbsp;
                        </td>
                        <td style="width: 35%">
                            &nbsp
                        </td>
                        <td style="vertical-align: top; text-align: right;">
                            &nbsp
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: center; width: 100%; font-family: Arial; font-size: 12pt;"
                            colspan="9">
                            <strong>DATI GENERALI</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <hr style="border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #C0C0C0;" />
                        </td>
                    </tr>
                    <tr style="font-family: Arial; font-size: 9pt">
                        <td style="width: 6%;">
                            PG N.
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="lblPG" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="CssLblValori" Width="80px" ReadOnly="True" Font-Bold="True"
                                Font-Names="Arial" Font-Size="9pt">0000000000</asp:TextBox>
                            <asp:TextBox ID="lblSlash" runat="server" ReadOnly="True" Font-Bold="True" Font-Names="Arial"
                                Font-Size="11pt" BorderStyle="None" Width="5px" BorderWidth="0px" Visible="False">/</asp:TextBox>
                            <asp:TextBox ID="lblPGcoll" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="CssLblValori" Width="80px" ReadOnly="True" Font-Bold="True"
                                Font-Names="Arial" Font-Size="9pt" Visible="False">0000000000</asp:TextBox>
                        </td>
                        <td style="width: 6%; text-align: right;">
                            DATA&nbsp;
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="txtDataPG" runat="server" CssClass="CssLblValori" BorderColor="#0078C2"
                                BorderStyle="Solid" BorderWidth="2px" Width="80px" TabIndex="1" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td style="width: 8%; text-align: right;">
                            <asp:Label ID="lblDomPG" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                Text="N. DICH."></asp:Label>&nbsp
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="lblPGDic" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                ReadOnly="True" BorderWidth="2px" Width="122px" CssClass="CssLblValori" TabIndex="1"
                                Font-Bold="True">F205-000000000-00</asp:TextBox>
                        </td>
                        <td style="width: 8%; text-align: right;">
                            <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                Text="ISEE"></asp:Label>&nbsp
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="lblISBAR" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                ReadOnly="True" BorderWidth="2px" Width="77px" CssClass="CssLblValori" TabIndex="1"
                                Font-Bold="True">0</asp:TextBox>
                        </td>
                        <td style="width: 13%; text-align: center;">
                            <asp:Label ID="lblProcessi" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                BorderWidth="2px" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" ForeColor="#990000"
                                BackColor="White" Style="width: 107px;" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <hr style="border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #C0C0C0;" />
                        </td>
                    </tr>
                </table>
                <table width="100%" style="font-family: Arial; font-size: 9pt; text-align: center;">
                    <tr>
                        <td style="text-align: center" width="99%">
                            <asp:Label ID="lblNumDom" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                                Font-Size="9pt" Height="18px" ForeColor="Blue" Visible="False"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
                                ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#DOM" Target="_blank"
                                Width="16px">Aiuto</asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <table width="100%" style="font-family: Arial; font-size: 9pt;">
                    <tr>
                        <td style="width: 100%; color: #0078C2; font-family: Arial; font-size: 11pt;" colspan="9">
                            <img src="img/id_card.png" style="width: 30px; height: 30px;" alt="Dati Richiedente" />
                            <strong>Dati Richiedente</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: 3px solid #CCCCCC;" colspan="9">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        Cognome
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCognome" runat="server" TabIndex="4" Width="140px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Nome
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNome" runat="server" TabIndex="5" Width="140px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Cod.Fiscale
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCF" runat="server" TabIndex="6" AutoPostBack="True" Width="140px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Sesso
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbSesso" TabIndex="3" runat="server" Height="20px" Font-Bold="False"
                                            CssClass="CssProv" Font-Names="Arial" Font-Size="9pt" Width="48px">
                                            <asp:ListItem Value="M">M</asp:ListItem>
                                            <asp:ListItem Value="F">F</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9" style="display: none">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%;">
                                                    Periodo di residenza in Lombardia
                                                </td>
                                                <td style="width: 80%;">
                                                    <asp:DropDownList ID="cmbResidenza" runat="server" CssClass="CssComuniNazioni" Font-Bold="False"
                                                        Font-Names="Arial" Font-Size="9pt" Height="20px" TabIndex="5" Width="436px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblNatoS" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                            Text="Nato in"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:DropDownList ID="cmbNazioneNas" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                            Font-Names="Arial" Font-Size="9pt" TabIndex="7" Enabled="False" Width="195px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblPrNasSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                            Font-Size="9pt" Text="Provincia"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:DropDownList ID="cmbPrNas" runat="server" AutoPostBack="True" CssClass="CssProv"
                                            Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="8" Width="50px"
                                            Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblComuneNas2" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                            Font-Size="9pt" Text="Comune"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:DropDownList ID="cmbComuneNas" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                            Font-Names="Arial" Font-Size="9pt" TabIndex="9" Enabled="False" Width="145px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-family: Arial; font-size: 9pt;" align="center">
                                        <asp:Label ID="lblDataNascS" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                            Font-Size="9pt" Text="Il"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtDataNascita" runat="server" Columns="7" MaxLength="10" TabIndex="10"
                                            CssClass="CssMaiuscolo" Width="90px" Font-Names="Arial"></asp:TextBox>
                                        <asp:Label ID="lblErrData" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                            Font-Size="X-Small" ForeColor="Red" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblResidS" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                            Text="Residenza"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:DropDownList ID="cmbNazioneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                            Font-Names="Arial" Font-Size="9pt" TabIndex="25" Enabled="True" Width="195px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblPrResSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                            Font-Size="9pt" Text="Provincia"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:DropDownList ID="cmbPrRes" runat="server" AutoPostBack="True" CssClass="CssProv"
                                            Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="26" Width="50px"
                                            Enabled="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblComuneResSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                            Font-Size="9pt" Text="Comune"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:DropDownList ID="cmbComuneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                            Font-Names="Arial" Font-Size="9pt" TabIndex="27" Enabled="True" Width="145px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblIndirSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                            Font-Size="9pt" Text="Indirizzo"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:DropDownList ID="cmbTipoIRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                            Font-Names="Arial" Font-Size="9pt" TabIndex="28" Enabled="True" Width="80px">
                                        </asp:DropDownList>
                                        &nbsp
                                        <asp:TextBox ID="txtIndRes" runat="server" TabIndex="29" Width="195px" Font-Names="Arial"></asp:TextBox>
                                    </td>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblCivicoSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                            Font-Size="9pt" Text="Civico"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtCivicoRes" runat="server" TabIndex="30" Width="60px" Font-Names="Arial"></asp:TextBox>
                                    </td>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblCapSot" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                            Text="CAP"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtCAPRes" runat="server" TabIndex="31" Width="40px" Font-Names="Arial"></asp:TextBox>
                                    </td>
                                    <td style="font-family: Arial; font-size: 9pt;">
                                        <asp:Label ID="lblTelSot" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                            Text="Telefono"></asp:Label>
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTelRes" runat="server" TabIndex="33" Width="100px" Font-Names="Arial"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" style="font-family: Arial; font-size: 9pt;">
                    <tr>
                        <td style="width: 100%; color: #0078C2; font-family: Arial; font-size: 11pt; vertical-align: top;"
                            colspan="9">
                            <img src="img/Img_Alloggio.png" style="width: 26px; height: 26px;" alt="Dati Recapito" />
                            <strong>Dati Recapito</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9" style="border: 3px solid #CCCCCC;">
                            <table width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td>
                                        Presso&nbsp
                                    </td>
                                    <td width="15px">
                                        &nbsp
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPresso" Width="280px" runat="server" Height="20px" Font-Bold="False"
                                            CssClass="CssMaiuscolo" Font-Names="Arial" Font-Size="9pt" TabIndex="18"></asp:TextBox>
                                    </td>
                                    <td>
                                        Provincia
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbProvRec" runat="server" AutoPostBack="True" CssClass="CssProv"
                                            Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="26" Width="70px"
                                            Enabled="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Comune
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbComuneRec" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                            Font-Names="Arial" Font-Size="9pt" TabIndex="27" Enabled="True" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Indirizzo
                                    </td>
                                    <td width="15px">
                                        &nbsp
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbTipoIRec" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                            Font-Names="Arial" Font-Size="9pt" TabIndex="28" Enabled="True" Width="80px">
                                        </asp:DropDownList>
                                        &nbsp
                                        <asp:TextBox ID="txtIndirizzoRec" runat="server" TabIndex="29" Width="195px" Font-Names="Arial"></asp:TextBox>
                                    </td>
                                    <td>
                                        Civico
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCivicoRec" runat="server" TabIndex="30" Width="60px" Font-Names="Arial"></asp:TextBox>
                                    </td>
                                    <td>
                                        CAP
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCAPRec" runat="server" TabIndex="31" Width="40px" Font-Names="Arial"></asp:TextBox>
                                    </td>
                                    <td>
                                        Telefono
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelRec" runat="server" TabIndex="33" Width="100px" Font-Names="Arial"> </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            <div id="tabs">
                                <ul>
                                    <li><a href="#tabs-1">Alloggio</a></li>
                                    <li><a href="#tabs-2">Dichiara</a></li>
                                    <li id="osp"><a href="#tabs-3">Ospiti</a></li>
                                    <li><a href="#tabs-4">Requisiti</a></li>
                                    <li><a href="#tabs-5">Note/D.Man.</a></li>
                                    <li><a href="#tabs-6">Documenti</a></li>
                                    <li><a href="#tabs-7">Decisioni</a></li>
                                    <li id="rateizz"><a href="#tabs-8">Parametri Rateizz.</a></li>
                                </ul>
                                <div id="tabs-1">
                                    <uc7:Dom_Alloggio_ERP ID="Dom_Alloggio_ERP1" runat="server" Visible="true" />
                                </div>
                                <div id="tabs-2">
                                    <uc8:Dom_Dichiara_Cambi ID="Dom_Dichiara_Cambi1" runat="server" />
                                </div>
                                <div id="tabs-3">
                                    <uc11:Dom_Ospiti ID="Dom_Ospiti1" runat="server" />
                                </div>
                                <div id="tabs-4">
                                    <uc6:Dom_Requisiti ID="Dom_Requisiti_Cambi1" runat="server" Visible="true" />
                                </div>
                                <div id="tabs-5">
                                    <uc5:Note ID="Dom_Note1" runat="server" Visible="true" />
                                </div>
                                <div id="tabs-6">
                                    <uc9:Dom_DocAllegati ID="Dom_DocAllegati" runat="server" />
                                </div>
                                <div id="tabs-7">
                                    <uc10:Dom_Decisioni ID="Dom_Decisioni1" runat="server" />
                                </div>
                                <div id="tabs-8">
                                    <uc12:Dom_InfoRateizz ID="Dom_InfoRateizz1" runat="server" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:ImageButton ID="btnVisualizzaDich" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                    Style="z-index: 100; left: 41px; position: absolute; top: 829px; visibility: hidden;"
                    OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" />
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
    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        Font-Names="Arial" Font-Size="8pt" Height="31px" Width="368px" Visible="False"
        Style="z-index: 119; left: 257px; position: absolute; top: 204px">
        <HeaderStyle BackColor="white" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
        <Columns>
            <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA"></asp:BoundColumn>
            <asp:BoundColumn DataField="PERC_INVAL" HeaderText="INVALIDITA"></asp:BoundColumn>
            <asp:BoundColumn DataField="INDENNITA_ACC" HeaderText="ACC"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <asp:Label ID="lblIdDomanda" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblIdDichiarazione" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="ProgrComponente" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblBando" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblIdBando" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Button ID="btnfunzelimina" runat="server" Text="" Style="display: none;" CauseValidation="false" />
    <asp:Button ID="btnfunzesci2" runat="server" Text="" Style="display: none;" CauseValidation="false" />
    <asp:HiddenField ID="H1" runat="server" Value="0" />
    <asp:HiddenField ID="H2" runat="server" Value="0" />
    <asp:HiddenField ID="txtTab" runat="server" />
    <asp:HiddenField ID="HiddenID" runat="server" Value="0" />
    <asp:HiddenField ID="conferma" runat="server" Value="0" />
    <asp:HiddenField ID="Ccodicetrovato" runat="server" Value="" />
    <asp:HiddenField ID="Ucodicetrovato" runat="server" Value="" />
    <asp:HiddenField ID="contrattodialer" runat="server" Value="" />
    <asp:HiddenField ID="inlettura" runat="server" Value="0" />
    <asp:HiddenField ID="chiudidirettamente" runat="server" Value="0" />
    <asp:HiddenField ID="ProcediProtoc" runat="server" Value="0" />
    <asp:HiddenField ID="ConfRidCan" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
    <asp:HiddenField ID="tipoRichiesta" runat="server" />
    <%--<asp:HiddenField ID="esPositivo" runat="server" Value="0" />
    <asp:HiddenField ID="esNegativo1" runat="server" Value="0" />
    <asp:HiddenField ID="esNegaRiesame" runat="server" Value="0" />--%>
    <%--<asp:HiddenField ID="documMancante" runat="server" Value="0" />--%>
    <asp:HiddenField ID="RinnovoDataChiusura" runat="server" />
    <asp:HiddenField ID="data_riconsegna" runat="server" />
    <asp:HiddenField ID="Hcanone" runat="server" Value="0" />
    <asp:HiddenField ID="ISEEnullo" runat="server" Value="0" />
    <asp:HiddenField ID="nuovocanone" runat="server" Value="0" />
    <asp:HiddenField ID="nuovoCodContr" runat="server" Value="0" />
    <asp:HiddenField ID="hdnFlag" runat="server" Value="0" />
    <asp:HiddenField ID="pressoCOR" runat="server" />
    <asp:HiddenField ID="importoCanone" runat="server" Value="0" />
    <asp:HiddenField ID="numProvvedim" runat="server" Value="0" />
    <asp:HiddenField ID="dataProvvedim" runat="server" Value="0" />
    <asp:HiddenField ID="restituisciCauz" runat="server" />
    <asp:HiddenField ID="fl_import_registr" runat="server" />
    <asp:HiddenField ID="tabSelect" runat="server" Value="-1" />
    <!-- hidden aggiunta in caso di dichiarazione incompleta -->
    <asp:HiddenField ID="dichCompleta" runat="server" Value="0" />
    <!-- hidden aggiunta per datagrid ospiti -->
    <asp:HiddenField ID="idOsp" runat="server" Value="0" />
    <asp:HiddenField ID="idRateizz" runat="server" Value="0" />
    </form>
</body>
<script type="text/javascript">
        //if per controllare il valore della hidden dichCompleta e nel caso di dichiarazione incompleta lancia un alert
        if (document.getElementById('<%= dichCompleta.ClientID%>').value == "1") {
            alert('LA DICHIARAZIONE NON è COMPLETA!');
        }

        if (document.getElementById('caric')) {
            document.getElementById('caric').style.visibility = 'hidden';
        };

        //document.getElementById('txtIndici').style.visibility = 'hidden';

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\,]/g
        }

        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
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
                        risultato = dascrivere + risultato + ',' + decimali
                        //document.getElementById(obj.id).value = a.replace('.', ',')
                        document.getElementById(obj.id).value = risultato
                    }
                    else {
                        document.getElementById(obj.id).value = a.replace('.', ',')
                    }

                }
                else
                    document.getElementById(obj.id).value = ''
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

        };

        /*if (document.getElementById('tipoRichiesta').value == '10') {
    
            if (document.getElementById('Dom_Decisioni1_txtDataScelta').value != '') {
                document.getElementById('btnDeposito').style.display = 'block';
                document.getElementById('Dom_Decisioni1_btnRiduzCanone').style.display = 'none';
            }
            else {
                document.getElementById('Dom_Decisioni1_btnRiduzCanone').style.display = 'block';
                document.getElementById('btnDeposito').style.display = 'none';
            };
        }
        if (document.getElementById('Dom_Decisioni1_txtdataAUTORIZ').value != '') {
            document.getElementById('btnDeposito').disabled = 'disabled';
        };*/

        //visibleMotivazioni();
        //visibleOspiti();
        //divSospesione();
        //visibleDataOsserv();
        //controlloAllegati();

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

            visibleOspiti();
            visibleMotivazioni();
            divSospesione();
            visibleDataOsserv();
            visibleTabRateizz();

            var tipo;
            tipo = document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value;

            if (tipo != '5') {
                document.getElementById('imgDatiContratto').style.visibility = 'hidden';
            }

            if (tipo == '9' && document.getElementById('Dom_Decisioni1_btnRiduzCanone').disabled == false) {
                document.getElementById('Dom_Decisioni1_btnRiduzCanone').style.display = 'none';
                document.getElementById('buttonAutorAbus').style.display = 'block';
            }
            if (tipo == '9' && document.getElementById('Dom_Decisioni1_autorizzFinale').value == '1') {
                document.getElementById('buttonAutorAbus').disabled = true;
            }

            if (document.getElementById('tipoRichiesta').value == '10') {

                if (document.getElementById('Dom_Decisioni1_txtDataScelta').value != '') {
                    //document.getElementById('btnDeposito').style.display = 'block';
                    //document.getElementById('Dom_Decisioni1_btnRiduzCanone').style.display = 'none';
                }
                else {
                    document.getElementById('Dom_Decisioni1_btnRiduzCanone').style.display = 'block';
                    document.getElementById('btnDeposito').style.display = 'none';
                }
            }
            if (document.getElementById('Dom_Decisioni1_txtdataAUTORIZ').value != '') {
                document.getElementById('btnDeposito').disabled = 'disabled';
            }

        }

        // FUNZIONI TAB OSPITI
        function MyDialogArguments() {
            this.Sender = null;
            this.StringValue = "";
        }

        function AggiungiNucleo() {
            a = document.getElementById('Dom_Ospiti1_txtprogr').Value;
            dialogArgs = new MyDialogArguments();
            dialogArgs.StringValue = a;
            dialogArgs.Sender = window;
            var dialogResults = window.showModalDialog("com_ospiti.aspx?OP=0&IDDOM=" + document.getElementById('Dom_Ospiti1_iddom').value + '&IDCONN=' + <%=lIdConnessDOMANDA %> + "&PR=" + a, window, 'status:no;dialogWidth:500px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
        //alert(dialogResults);
        if (dialogResults != undefined) {
            if (dialogResults == '1') {
                //document.getElementById('salvaEsterno').value = '1';
                document.getElementById('btnSalva').click();

            }
            if (dialogResults == '2') {
                document.getElementById('txtModificato').value = '1';

            }
        }
    }

    function ModificaNucleo() {
        a = document.getElementById('Dom_Ospiti1_txtprogr').Value;
        if (document.getElementById('idOsp').value == 0) {
            alert('Selezionare una riga dalla lista!');
        }
        else {
            //document.getElementById('caric').style.visibility = 'visible';
            cognome = document.getElementById('Dom_Ospiti1_cognome').value;
            nome = document.getElementById('Dom_Ospiti1_nome').value;
            data = document.getElementById('Dom_Ospiti1_data_nasc').value;
            cf = document.getElementById('Dom_Ospiti1_cod_fiscale').value;
            dataingr = document.getElementById('Dom_Ospiti1_data_inizio').value;
            datafine = document.getElementById('Dom_Ospiti1_data_fine').value;
            RI = document.getElementById('Dom_Ospiti1_IDselectedRow').value;

            str = cognome + " " + nome + " " + data + " " + cf + " " + dataingr + " " + datafine + " ";

            var dialogResults = window.showModalDialog("com_ospiti.aspx?OP=1&IDDOM=" + document.getElementById('Dom_Ospiti1_iddom').value + '&IDCONN=' + <%=lIdConnessDOMANDA %> + "&ID=" + document.getElementById('idOsp').value + "&RI=" + RI + "&COGNOME=" + cognome + "&NOME=" + nome + "&DATA=" + data + "&CF=" + cf + "&TESTO=" + str + "&PR=" + a + "&DATAINGR=" + dataingr + "&DATAFINE=" + datafine, window, 'status:no;dialogWidth:500px;dialogHeight:480px;dialogHide:true;help:no;scroll:no'); //width era 433 ed height 450

            //alert(dialogResults);
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    //document.getElementById('salvaEsterno').value='1';
                    document.getElementById('btnSalva').click();

                    //document.getElementById('salvaEsterno').value='0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';

                }
            }
        }
    }

    function Verifica() {

        SceltaFunzioneOP1('Sicuri di voler eliminare?');

    }

    function SceltaFunzioneOP1(TestoMessaggio) {
        $(document).ready(function () {
            $('#ScriptScelta').text(TestoMessaggio);
            $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('<%=btnfunzelimina.ClientID %>', ''); { $(this).dialog('close');/*document.getElementById('caric').style.visibility='visible';*/ } }, 'No': function () { $(this).dialog('close'); document.getElementById('idOsp').value = '-1'; " & Funzione2 & " } } });
        });
    }

    function EliminaSoggetto() {
        a = document.getElementById('Dom_Ospiti1_txtprogr').Value;


        if (document.getElementById('idOsp').value == 0 || document.getElementById('idOsp').value == -1) {
            alert('Selezionare una riga dalla lista!');
        }
        else {
            Verifica();
        }

    }


    /* FUNZIONI PULSANTI */
    function SceltaFunzioneOP2(TestoMessaggio) {
        $(document).ready(function () {
            $('#ScriptScelta').text(TestoMessaggio);
            $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Ok': function () { __doPostBack('<%=btnfunzesci2.ClientID %>', ''); { $(this).dialog('close'); } }, 'Annulla': function () { $(this).dialog('close'); document.getElementById('txtModificato').value = '111'; " & Funzione2 & " } } });
        });
    }


    function ConfermaEsci() {
        if (document.getElementById('txtModificato')) {
            if (document.getElementById('txtModificato').value == '1') {

                AggContratto();
                if (document.getElementById('Attesa')) {
                    document.getElementById('Attesa').style.visibility = 'visible';
                }

                //}

            }
            else {

                if (opener) {
                    if (typeof opener != 'undefined') {
                        //if (opener.opener) {

                        if (typeof opener.opener != 'undefined') {

                            if (typeof opener.opener != 'unknown') {
                                if (opener.opener) {
                                    if (opener.opener.name.substring(0, 9) == 'Contratto') {
                                        if (opener.opener.document.getElementById('imgSalva')) {

                                            opener.opener.document.getElementById('nuovocanone').value = document.getElementById('nuovocanone').value;
                                            opener.opener.document.getElementById('pressoCOR').value = document.getElementById('pressoCOR').value;
                                            opener.opener.document.getElementById('imgSalva').click();
                                        }
                                    }
                                    else {
                                        self.close();
                                    }
                                }
                            }
                        }
                        if (typeof opener.opener != 'undefined') {
                            if (typeof opener.opener != 'unknown') {
                                if (opener.name.substring(0, 9) == 'Contratto') {
                                    if (opener.document.getElementById('imgSalva')) {
                                        opener.document.getElementById('nuovocanone').value = document.getElementById('nuovocanone').value;
                                        opener.document.getElementById('pressoCOR').value = document.getElementById('pressoCOR').value;
                                        opener.document.getElementById('imgSalva').click();
                                    }
                                }
                            }
                        }
                        // }
                    }
                }
                if (document.getElementById('Attesa')) {
                    document.getElementById('Attesa').style.visibility = 'visible';
                }

            }
        }
        self.close();
    }


    function AggContratto() {
        if (opener) {
            if (typeof opener != 'undefined') {
                if (typeof opener.opener != 'undefined') {
                    if (typeof opener.opener != 'unknown') {
                        if (opener.opener.name.substring(0, 9) == 'Contratto') {
                            if (opener.opener.document.getElementById('imgSalva')) {
                                opener.opener.document.getElementById('nuovocanone').value = document.getElementById('nuovocanone').value;
                                opener.opener.document.getElementById('pressoCOR').value = document.getElementById('pressoCOR').value;
                                opener.opener.document.getElementById('imgSalva').click();
                            }
                        }
                    }
                }
                if (typeof opener.opener != 'undefined') {
                    if (typeof opener.opener != 'unknown') {
                        if (opener.name.substring(0, 9) == 'Contratto') {
                            if (opener.document.getElementById('imgSalva')) {
                                opener.document.getElementById('nuovocanone').value = document.getElementById('nuovocanone').value;
                                opener.document.getElementById('pressoCOR').value = document.getElementById('pressoCOR').value;
                                opener.document.getElementById('imgSalva').click();
                            }
                        }
                    }
                }
            }
        }
    }

    /* FINE FUNZIONI PULSANTI */

    //########################################## Suddivisione per tipologia di domanda ##########################################

    //###################################inizio sezione TIPOLOGIA: Riduzione Canone ##########################################    
    //##################### STAMPA RICEZIONE RICHIESTA #####################
    function RicezRichiesta() {
        document.getElementById('H1').value = '0';
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
               document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
               '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RichRC', '');
       }
       else {
           alert('Salvare le modifiche prima di procedere!');
       }

    }
    //#####################FINE STAMPA RICEZIONE RICHIESTA #####################

    //*************************************************************************************************************************************************
    //##################### STAMPA DOCUMENTO MANCANTE #####################
    function StampaDoc() {
        if (document.getElementById('txtModificato').value != 1) {
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallStampaDoc,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
            });
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }

    }

    function mycallStampaDoc(e, v, m, f) {
        if (v != undefined)

            if (v != '2') {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=DocMancRC', '', '');
                }

            return true;
        }
        //#####################FINE STAMPA DOCUMENTO MANCANTE #####################
        //*************************************************************************************************************************************************
        //#####################STAMPA AVVIO PROCEDIMENTO #####################
        function AvvioProc() {
            if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) {
                if (document.getElementById('txtModificato').value != 1) {
                    var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                    jQuery.prompt(txt, {
                        submit: mycallAvvioProc,
                        buttons: { Procedi: '1', Annulla: '2' },
                        show: 'slideDown',
                        focus: 0
                    });
                }
                else {
                    alert('Salvare le modifiche prima di procedere!');
                }
            }
            else {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvioProc(e, v, m, f) {
            if (v != undefined)

                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=AvvProcRC', '', '');
                }

            return true;
        }
        //#####################FINE STAMPA PROCEDIMENTO #####################
        //***********************************************************************************************************************************************
        //#####################STAMPA AUTOCERTIFICAZIONE #####################

        function AutoCert() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=AutoCertRC', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }
        //#####################FINE STAMPA AUTOCERTIFICAZIONE #####################


        //#####################STAMPA EsitoPositivo #####################
        function EsitoPos() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoPos,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPos(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPositRC', '', '');
                }

            return true;
        }
        //#####################FINE STAMPA EsitoPositivo #####################
        //*************************************************************************************************************************************************


        //#####################STAMPA EsitoPositivo DEFINITIVO #####################
        function EsitoPosDEF() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoPosDEF,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPosDEF(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoPosDEF', '', '');
                }

            return true;
        }
        //#####################FINE STAMPA EsitoPositivo DEFINITIVO #####################
        //*************************************************************************************************************************************************



        //#####################STAMPA EsitoPositivo Provvisorio #####################
        function EsitoPosProvv() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoPosProvv,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPosProvv(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPosRCProvv', '', '');
                }

            return true;
        }
        //#####################FINE STAMPA EsitoPositivo Provvisorio #####################
        //*************************************************************************************************************************************************



        //*************************************************************************************************************************************************
        //#####################STAMPA EsitoNegativo #####################
        function EsitoNeg() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNeg,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNeg(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsNegatRC', '', '');
                }

            return true;
        }
        //#####################FINE STAMPA EsitoNegativo #####################
        //*************************************************************************************************************************************************


        //#####################STAMPA EsitoNegativoRiesame SENZA Oss.#####################
        function EsNegRiesame() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsNegRiesame,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegRiesame(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsNegRiesNOoss', '', '');
                }
            return true;
        }


        // **** Esito Negativo CON Osservazioni ****
        function EsNegRiesConOss() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsNegRiesConOss,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegRiesConOss(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsNegRiesameRC', '', '');
                }
            return true;
        }



        // **** Rapporto Sintetico RECA ****
        function RapportoRECA() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RapportoRECA', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }
        //#####################FINE STAMPA Rapporto Sintetico RECA  #####################

        //################################### fine sezione TIPOLOGIA: Riduzione Canone ##########################################



        //################################### 16/11/'11 Inizio sezione TIPOLOGIA: Ampliamento ##########################################    

        //##################### STAMPA RICEZIONE RICHIESTA #####################
        function RicRichiesta() {
            document.getElementById('H1').value = '0';
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RicRichiesta', '');
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }

       }
       //#####################FINE STAMPA RICEZIONE RICHIESTA #####################


       //**** Documentazione Mancante ****
       function DocMancante() {
           if (document.getElementById('txtModificato').value != 1) {
               var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
               jQuery.prompt(txt, {
                   submit: mycallDocMancante,
                   buttons: { Procedi: '1', Annulla: '2' },
                   show: 'slideDown',
                   focus: 0
               });
           }
           else {
               alert('Salvare le modifiche prima di procedere!');
           }

       }

       function mycallDocMancante(e, v, m, f) {
           if (v != undefined)

               if (v != '2') {
                   window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=DocMancanteAMPL', '', '');
               }

           return true;
       }


       //**** Autocertificazione nucleo di famiglia ****
       function AUcertStFamiglia() {
           if (document.getElementById('txtModificato').value != 1) {
               window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=StFamigliaAMPL', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //**** Convivenza More Uxorio ****
        function MoreUxorio() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=MoreUxorioAMPL', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //**** Convivenza Assistenza ****
        function Assistenza() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=AssistenzaAMPL', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }


        //**** Avvio Procedimento ****
        function AvvProcedim() {
            if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) {
                if (document.getElementById('txtModificato').value != 1) {
                    var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                    jQuery.prompt(txt, {
                        submit: mycallAvvProcedim,
                        buttons: { Procedi: '1', Annulla: '2' },
                        show: 'slideDown',
                        focus: 0
                    });
                }
                else {
                    alert('Salvare le modifiche prima di procedere!');
                }
            }
            else {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvProcedim(e, v, m, f) {
            if (v != undefined)

                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=AvvioProcAMPL', '', '');
                }

            return true;
        }


        //**** Esito Negativo ****
        function EsNegativo() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsNegativo,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegativo(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsNegativoAMPL', '', '');
                }

            return true;
        }


        // **** Esito Postivo ****
        function EsitoPosit() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoPosit,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPosit(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPositivoAMPL', '', '');
                }

            return true;
        }

        // --------------------------------- DOCUMENTI AGGIUNTIVI 27/02/2012 ---------------------------------

        // **** Permanenza requisiti ERP (titolare) ****
        function PermReqERP() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=PermanenzaAMPL1', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }

        // **** Permanenza requisiti ERP (nuovo componente) ****
        function PermReqERP2() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=PermanenzaAMPL2', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }


        //**** Sopralluogo ****
        function SopralluogoAMPL() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=SoprallAMPL', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

        function SopralluogoRID() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=SoprallRID', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //**** Comunicazione per sopralluogo ****
        function ComSoprallAMPL() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallComSoprallAMPL,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallComSoprallAMPL(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=ComSopralAMPL', '', '');
                }

            return true;
        }

        // **** Presa D'atto Per Rientro ****
        function PresaAttoRientro() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallPresaAttoRientro,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallPresaAttoRientro(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=PresaAttoRientro', '', '');
                }

            return true;
        }


        // **** Esito Positivo Riesame ****
        function EsPosRiesAMPL() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPosRiesAMPL,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesAMPL(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPosRiesAMPL', '', '');
                }

            return true;
        }


        // **** Esito Positivo Rientro ****
        function EsPosRiesRientro() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPosRiesRientro,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesRientro(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPosRiesRientro', '', '');
                }

            return true;
        }

        // **** Esito Negativo Con Osservazioni ****
        function EsNegRiesameAMPL() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsNegRiesameAMPL,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegRiesameAMPL(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsNegRiesameAMPL', '', '');
                }
            return true;
        }

        // **** Esito Negativo Senza Osservazioni ****
        function EsNegRiesameNOoss() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsNegRiesameNOoss,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegRiesameNOoss(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsNegRiesameNOoss', '', '');
                }
            return true;
        }


        // **** Provvedimento Definitivo Comune ****
        function ProvvDefComune() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=ProvvDefComune', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }


        // **** Rapporto Sintetico ANF ****
        function RapportoANF() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RapportoANF', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }




        //################################### fine sezione TIPOLOGIA: Ampliamento ##########################################



        //################################### 01/12/'11 Inizio sezione TIPOLOGIA: Subentro ##########################################    
        //#####################STAMPA AVVIO PROCEDIMENTO #####################
        function AvvioProcSUB() {
            if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) {
                if (document.getElementById('txtModificato').value != 1) {
                    var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                    jQuery.prompt(txt, {
                        submit: mycallAvvioProcSUB,
                        buttons: { Procedi: '1', Annulla: '2' },
                        show: 'slideDown',
                        focus: 0
                    });
                }
                else {
                    alert('Salvare le modifiche prima di procedere!');
                }
            }
            else {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvioProcSUB(e, v, m, f) {
            if (v != undefined)

                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=AvvioProcSUB', '', '');
             }

         return true;
     }
     //#####################FINE STAMPA PROCEDIMENTO #####################


     //**** Domanda di subentro ****
     function DomSubentro() {
         if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) {
             if (document.getElementById('txtModificato').value != 1) {
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                        document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                        '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=DomandaSUB', '');
                }
                else {
                    alert('Salvare le modifiche prima di procedere!');
                }
            }
            else {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        //**** Domanda di subentro FFOO ****
        function DomSubentroFFOO() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                 '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=DomandaSUBFFOO', '');
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }
     }

     //**** Dichiarazione certificazione rinuciante ****
     function CertRinunciante() {
         if (document.getElementById('txtModificato').value != 1) {
             window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                 '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=PermReqRSUB', '');
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }
     }


     //**** Sopralluogo ****
     function Sopralluogo() {
         if (document.getElementById('txtModificato').value != 1) {
             window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                 '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=SoprallSUB', '');
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }
     }


     //**** Comunicazione per sopralluogo ****
     function ComSoprall() {
         if (document.getElementById('txtModificato').value != 1) {
             var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
             jQuery.prompt(txt, {
                 submit: mycallComSoprall,
                 buttons: { Procedi: '1', Annulla: '2' },
                 show: 'slideDown',
                 focus: 0
             });
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }

     }

     function mycallComSoprall(e, v, m, f) {
         if (v != undefined)
             if (v != '2') {
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=ComSopralSUB', '', '');
             }

         return true;
     }



     //******** Doc. Mancante ***********
     function DocMancanteSUB() {
         if (document.getElementById('txtModificato').value != 1) {
             var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
             jQuery.prompt(txt, {
                 submit: mycallDocMancanteSUB,
                 buttons: { Procedi: '1', Annulla: '2' },
                 show: 'slideDown',
                 focus: 0
             });
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }

     }

     function mycallDocMancanteSUB(e, v, m, f) {
         if (v != undefined)
             if (v != '2') {
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=DocMancanteSUB', '', '');
                }

            return true;
        }


        //**** Esito Positivo ****
        function EsPositSub() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPositSub,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositSub(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoPosSUB', '', '');
                }

            return true;
        }


        //**** Esito Positivo FFOO ****
        function EsPositFFOO() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPositFFOO,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositFFOO(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CAUS=' + document.getElementById('Dom_Dichiara_Cambi1_cmbPresentaD').value + '&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoPosFFOO', '', '');
                }

            return true;
        }

        //**** Esito Positivo FFOO (decesso 2) ****
        function EsPositFFOO2() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPositFFOO2,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositFFOO2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CAUS=' + document.getElementById('Dom_Dichiara_Cambi1_cmbPresentaD').value + '&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoPosFFOO2', '', '');
                }

            return true;
        }


        //**** Esito Positivo Comun. Commissiariato ****
        function EsPosCommiss() {

            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoPosComSUB', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        //**** Esito Positivo Condomini ****
        function EsPosCondom() {

            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPosCondom', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }



        //**** Esito Positivo Direzione Crediti ****
        function EsPosDirezCrediti() {

            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                     document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                     '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoPosDRCSUB', '');
             }
             else {
                 alert('Salvare le modifiche prima di procedere!');
             }

         }


         //**** Comunicazione Comiss Governo ****
         function ComCommissGoverno() {

             if (document.getElementById('txtModificato').value != 1) {
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=ComGovSUB', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }


        //**** Esito Negativo ****
        function EsiNegat() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsiNegat,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegat(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitNegSUB', '', '');
                }

            return true;
        }


        //**** Esito Negativo FFOO ****
        function EsiNegatFFOO1() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsiNegatFFOO1,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }
        function mycallEsiNegatFFOO1(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitNegFFOO1', '', '');
                }

            return true;
        }
        //**** Esito Negativo FFOO ****
        function EsiNegatFFOO2() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsiNegatFFOO2,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }
        function mycallEsiNegatFFOO2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitNegFFOO2', '', '');
                }

            return true;
        }
        //**** Esito Negativo FFOO ****
        function EsiNegatFFOO3() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsiNegatFFOO3,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }
        function mycallEsiNegatFFOO3(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitNegFFOO3', '', '');
                }

            return true;
        }


        //**** Lett. di decadenza FFOO ****
        function LetteraDecadenza() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=LettDecadComune', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }


        //**** Esito Positivo Riesame ****
        function EsiPosRies() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsiPosRies,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiPosRies(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitPosRiSUB', '', '');
                }

            return true;
        }


        //**** Esito Negativo Riesame Con OSS****
        function EsiNegaRies() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsiNegaRies,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegaRies(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitNegRiesSUB', '', '');
                }

            return true;
        }



        //**** Esito Negativo Riesame Senza Oss ****
        function EsiNegaRiesNoOSS() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsiNegaRiesNoOSS,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegaRiesNoOSS(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitNegRiesSUBNoOS', '', '');
                }

            return true;
        }




        function RapportoVAIN() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RapportoVAIN', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }

        //**** Lett. di trasformazione FFOO ****
        function LetteraTrasf() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=LettTrasfor', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }

        function RapportoVAINFFOO() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RapportoVAINFFOO', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }
        //################################### fine sezione TIPOLOGIA: Subentro ##########################################

        //################################### 05/12/'11 Inizio sezione TIPOLOGIA: Voltura ##########################################    

        //***** Modulo richiesta Voltura ******
        function RichVoltura() {

            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RicezRicVOL', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }


        //***** Modulo richiesta Voltura *****
        function AvvProcVoltura() {
            if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) {
                if (document.getElementById('txtModificato').value != 1) {
                    var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                    jQuery.prompt(txt, {
                        submit: mycallAvvProcVoltura,
                        buttons: { Procedi: '1', Annulla: '2' },
                        show: 'slideDown',
                        focus: 0
                    });
                }
                else {
                    alert('Salvare le modifiche prima di procedere!');
                }
            }
            else {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvProcVoltura(e, v, m, f) {
            if (v != undefined)

                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=AvvProcedVOL', '', '');
            }

        return true;
    }


    //***** Sopralluogo *****
    function ModSoprall() {
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=ModuloSoprallVOL', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //***** Com. Sopralluogo *****
        function SoprallUtente() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallSoprallUtente,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallSoprallUtente(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=SoprUtenteVOL', '', '');
             }

         return true;
     }


     //***** Com. Esito Positivo *****
     function EsPositVolt() {
         if (document.getElementById('txtModificato').value != 1) {
             var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
             jQuery.prompt(txt, {
                 submit: mycallEsPositVolt,
                 buttons: { Procedi: '1', Annulla: '2' },
                 show: 'slideDown',
                 focus: 0
             });
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }

     }
     function mycallEsPositVolt(e, v, m, f) {
         if (v != undefined)
             if (v != '2') {
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoPosiVOL', '', '');
                }

            return true;
        }


        //***** Com. Esito Negativo *****
        function EsitoNega() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNega,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNega(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoNegaVOL', '', '');
                }

            return true;
        }


        //***** Com. Esito Pos. Riesame *****
        function EsitoPosRies() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoPosRies,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPosRies(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoPosRiesVOL', '', '');
                }

            return true;
        }



        //***** Com. Esito Neg. Riesame *****
        function EsitoNegatRies() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegatRies,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNegatRies(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoNegatRiesVOL', '', '');
                }

            return true;
        }



        //***** Com. Esito Neg. Riesame No Osservazioni *****
        function EsitoNegatRiesNO() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegatRiesNO,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNegatRiesNO(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoNegatRiesNOVOL', '', '');
                }

            return true;
        }


        // STAMPA Rapporto Sintetico CAIN
        function RapportoCAIN() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RapportoCAIN', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }
        // FINE STAMPA Rapporto Sintetico CAIN  
        //################################### fine sezione TIPOLOGIA: Voltura ##########################################    

        //################################### 13/12/'11 Inizio sezione TIPOLOGIA: Ospitalità ##########################################    


        //***** Modulo richiesta Ospitalità ******
        function RichOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RichOSP', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

        //***** Modulo richiesta Ospitalità Badanti ******
        function RichOSPbada() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                 '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RichOSPbada', '');
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }
     }

     //***** Modulo richiesta Ospitalità Autorizz.scolastiche ******
     function RichOSPscol() {
         if (document.getElementById('txtModificato').value != 1) {
             window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                 '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RichOSPscol', '');
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }
     }


     //***** Autocertificazione stato di famiglia Ospitalità ******
     function StFamigliaOSP() {
         if (document.getElementById('txtModificato').value != 1) {
             window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                 '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=StFamigliaOSP', '');
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }
     }

     //**** Documentazione Mancante ****
     function DocMancanteOSP() {
         if (document.getElementById('txtModificato').value != 1) {
             var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
             jQuery.prompt(txt, {
                 submit: mycallDocMancanteOSP,
                 buttons: { Procedi: '1', Annulla: '2' },
                 show: 'slideDown',
                 focus: 0
             });
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }

     }

     function mycallDocMancanteOSP(e, v, m, f) {
         if (v != undefined)

             if (v != '2') {
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=DocMancanteOSP', '', '');
            }

        return true;
    }


    //**** Avvio Procedimento ****
    function AvvioProcOSP() {
        if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallAvvioProcOSP,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }
        else {
            alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
        }
    }

    function mycallAvvioProcOSP(e, v, m, f) {
        if (v != undefined)

            if (v != '2') {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=AvvioProcOSP', '', '');
                }

            return true;
        }


        //***** Sopralluogo *****
        function SopralluogoOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=SopralluogoOSP', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //***** Com. Sopralluogo *****
        function ComSoprallOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallComSoprallOSP,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallComSoprallOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=ComSoprallOSP', '', '');
             }

         return true;
     }



     //***** Com. Esito Negativo *****
     function EsiNegatOSP() {
         if (document.getElementById('txtModificato').value != 1) {
             var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
             jQuery.prompt(txt, {
                 submit: mycallEsiNegatOSP,
                 buttons: { Procedi: '1', Annulla: '2' },
                 show: 'slideDown',
                 focus: 0
             });
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }

     }

     function mycallEsiNegatOSP(e, v, m, f) {
         if (v != undefined)
             if (v != '2') {
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsiNegatOSP', '', '');
                }

            return true;
        }



        //***** Com. Esito Positivo *****
        function EsPositOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPositOSP,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPositOSP', '', '');
                }

            return true;
        }

        //***** Com. Esito Positivo Badanti *****
        function EsPositOSPbada() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPositOSPbada,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositOSPbada(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPositOSPbada', '', '');
                }

            return true;
        }


        //***** Com. Esito Positivo Autorizz.scolastiche*****
        function EsPositOSPscol() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPositOSPscol,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositOSPscol(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPositOSPscol', '', '');
                }

            return true;
        }

        //***** Com. Esito Pos. Riesame *****
        function EsPosRiesOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPosRiesOSP,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPosRiesOSP', '', '');
                }

            return true;
        }

        //***** Com. Esito Pos. Riesame Badanti *****
        function EsPosRiesOSPbada() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPosRiesOSPbada,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesOSPbada(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPosRiesOSPbada', '', '');
                }

            return true;
        }


        //***** Com. Esito Pos. Riesame Autorizz. Scolastica *****
        function EsPosRiesOSPscol() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPosRiesOSPscol,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesOSPscol(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPosRiesOSPscol', '', '');
                }

            return true;
        }

        //***** Com. Esito Neg. Riesame *****
        function EsitoNegRiesOsservOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegRiesOsservOSP,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNegRiesOsservOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoNegRiesOsservOSP', '', '');
                }

            return true;
        }



        //***** Com. Esito Neg. Riesame No Osservazioni *****
        function EsitoNegRiesNoOsservOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegRiesNoOsservOSP,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNegRiesNoOsservOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsitoNegRiesNoOsservOSP', '', '');
                }

            return true;
        }

        // STAMPA Rapporto Sintetico OSP
        function RapportoOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=RapportoOSP', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }
        // FINE STAMPA Rapporto Sintetico OSP  


        //################################### 30/01/'12 Inizio sezione TIPOLOGIA: Cambio Consensuale ##########################################    

        //**** Ricezione Richiesta ****
        function RichCAMB() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=RichCAMB', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //**** Ricezione Richiesta2 ****
        function RichCAMB2() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                   document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                   '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=RichCAMB2', '');
           }
           else {
               alert('Salvare le modifiche prima di procedere!');
           }
       }


       //**** Dichiarazione Perm. Requisiti ****
       function DichPermanenzaReq() {
           if (document.getElementById('txtModificato').value != 1) {
               window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                   document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                   '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=DichPermanenza', '');
           }
           else {
               alert('Salvare le modifiche prima di procedere!');
           }
       }

       //**** Dichiarazione Perm. Requisiti 2 ****
       function DichPermanenzaReq2() {
           if (document.getElementById('txtModificato').value != 1) {
               window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                   document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                   '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=DichPermanenza2', '');
           }
           else {
               alert('Salvare le modifiche prima di procedere!');
           }
       }



       //**** Documentazione Mancante ****
       function DocMancanteCAMB() {
           if (document.getElementById('txtModificato').value != 1) {
               var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
               jQuery.prompt(txt, {
                   submit: mycallDocMancanteCAMB,
                   buttons: { Procedi: '1', Annulla: '2' },
                   show: 'slideDown',
                   focus: 0
               });
           }
           else {
               alert('Salvare le modifiche prima di procedere!');
           }

       }

       function mycallDocMancanteCAMB(e, v, m, f) {
           if (v != undefined)

               if (v != '2') {
                   window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=DocMancCAMB', '', '');
                }

            return true;
        }

        //**** Documentazione Mancante 2****
        function DocMancanteCAMB2() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallDocMancanteCAMB2,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallDocMancanteCAMB2(e, v, m, f) {
            if (v != undefined)

                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=DocMancCAMB2', '', '');
                }

            return true;
        }



        //**** Avvio Procedimento ****
        function AvvioProcCAMB() {
            if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) {
                if (document.getElementById('txtModificato').value != 1) {
                    var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                    jQuery.prompt(txt, {
                        submit: mycallAvvioProcCAMB,
                        buttons: { Procedi: '1', Annulla: '2' },
                        show: 'slideDown',
                        focus: 0
                    });
                }
                else {
                    alert('Salvare le modifiche prima di procedere!');
                }
            }
            else {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvioProcCAMB(e, v, m, f) {
            if (v != undefined)

                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=AvvProcedCAMB', '', '');
                }

            return true;
        }



        //**** Avvio Procedimento 2****
        function AvvioProcCAMB2() {
            if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) {
                if (document.getElementById('txtModificato').value != 1) {
                    var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                    jQuery.prompt(txt, {
                        submit: mycallAvvioProcCAMB2,
                        buttons: { Procedi: '1', Annulla: '2' },
                        show: 'slideDown',
                        focus: 0
                    });
                }
                else {
                    alert('Salvare le modifiche prima di procedere!');
                }
            }
            else {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvioProcCAMB2(e, v, m, f) {
            if (v != undefined)

                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=AvvProcedCAMB2', '', '');
                }

            return true;
        }



        //***** Sopralluogo *****
        function SopralluogoCAMB() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=SoprallCAMB', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //***** Com. Sopralluogo *****
        function ComSoprallCAMB() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallComSoprallCAMB,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallComSoprallCAMB(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=ComSoprallCAMB', '', '');
                }

            return true;
        }


        //***** Com. Sopralluogo 2 *****
        function ComSoprallCAMB2() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallComSoprallCAMB2,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallComSoprallCAMB2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=ComSoprallCAMB2', '', '');
                }

            return true;
        }



        //***** Com. Esito Negativo *****
        function EsiNegatCAMB() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsiNegatCAMB,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegatCAMB(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=EsNegaCAMB', '', '');
                }

            return true;
        }


        //***** Com. Esito Negativo 2 *****
        function EsiNegatCAMB2() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsiNegatCAMB2,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegatCAMB2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=EsNegaCAMB2', '', '');
                }

            return true;
        }


        //***** Com. Esito Positivo *****
        function EsPositCAMB() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPositCAMB,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositCAMB(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=EsPositCAMB', '', '');
                }

            return true;
        }

        //***** Com. Esito Positivo 2*****
        function EsPositCAMB2() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsPositCAMB2,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositCAMB2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=EsPositCAMB2', '', '');
                }

            return true;
        }

        // STAMPA Rapporto Sintetico OSP
        function RapportoCACO() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&NUMCONT2=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value + '&TIPO=RapportoCACO', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }


        }
        // FINE STAMPA Rapporto Sintetico OSP  


        function ModOffertaAlloggio() {
            window.open('ReportAbbinamento.aspx?ABB=' + document.getElementById('numOfferta').value + '&IDALL=' + document.getElementById('idAlloggio').value, '');
        }
        //Documenti per Cambi In Emergenza


        //window.open('ModelliCambio22/ElencoStampe.aspx?IDDOM=' + <%=lIdDomanda %>, '');

        function ComposizioneNucleo() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallComposizioneNucleo,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

        function mycallComposizioneNucleo(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=CompNucleo', '');
                }

            return true;
        }

        function DocumentazioneMancante() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallDocumentazioneMancante,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallDocumentazioneMancante(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=DocMancante', '');
                }

            return true;
        }

        function EsitoNegativoArt22() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegativoArt22,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNegativoArt22(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsNegativo', '');
                }
            return true;
        }

        function EsitoNegatAllAdeguato() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegatAllAdeguato,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

        function mycallEsitoNegatAllAdeguato(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsNegatAllAdeg', '');
                }

            return true;
        }

        function EsitoNegativoISEE() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegativoISEE,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

        function mycallEsitoNegativoISEE(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsNegatISEE', '');
                }

            return true;
        }

        function EsitoNegatMorosita() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegatMorosita,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoNegatMorosita(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsNegatMoros', '');
                }
            return true;
        }

        function EsitoNegatMorosita() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegatMorosita,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

        function mycallEsitoNegatMorosita(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsNegatMoros', '');

                }
            return true;
        }

        function EsitoNegatRequisiti() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegatRequisiti,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoNegatRequisiti(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsNegatRequis', '');
                }

            return true;
        }

        function EsitoPositivoArt22() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoPositivoArt22,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoPositivoArt22(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsPositivo', '');
                }
            return true;
        }

        function EsitoPositMorosita() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoPositMorosita,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoPositMorosita(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsPositMoros', '');
                }
            return true;
        }

        function EsitoNegatRicorso() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoNegatRicorso,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoNegatRicorso(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsNegatRicorso', '');
                }
            return true;
        }


        function EsitoPositRicorso() {
            if (document.getElementById('txtModificato').value != 1) {
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                    submit: mycallEsitoPositRicorso,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoPositRicorso(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('../ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT=' + f.txtprot + '&TIPODOC=EsPositRicorso', '');
                }

            return true;
        }

        function ElStampeART22() {

            window.open('../ModelliCambio22/ElencoStampe.aspx?IDDOM=' + <%=lIdDomanda %>, '');
        }




        //Stampe Rateizzazione 05/07/2018 
        function EsitoNegRAT() {
            if (document.getElementById('txtModificato').value != 1) {
               window.open('../StampeRateizzStraord.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsNegativo','','');

           }
           else {
               alert('Salvare le modifiche prima di procedere!');
           }

        }

        function EsitoPosRAT() {
            if (document.getElementById('txtModificato').value != 1) {
               window.open('../StampeRateizzStraord.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value + '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=EsPositivo','','');

           }
           else {
               alert('Salvare le modifiche prima di procedere!');
           }

        }
        //#####################FINE STAMPA EsitoNegativo #####################
        //*************************************************************************************************************************************************

function Frontespizio() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                    '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&TIPO=Frontespizio', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

        function ConfermaStampa() {
            var tipo;

            tipo = document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value;

            if (tipo == 3) {
                if (document.getElementById('Dom_Alloggio_ERP1_cmbTipoU').value == 0) {
                    var chiediConferma

                    chiediConferma = window.confirm("Attenzione, procedere con la stampa?");
                    if (chiediConferma == true) {
                        document.getElementById('conferma').value = '1';
                    }
                    else {
                        document.getElementById('conferma').value = '0';
                    }
                }
                else {

                    document.getElementById('conferma').value = '0';
                    alert('Attenzione...è possibile utilizzare solo alloggi di tipo E.R.P.');
                }

            }
        }

        function ConfContabilizza() {
            chiediConferma = window.confirm('Sei sicuro di voler contabilizzare l\'importo calcolato?');
            if (chiediConferma == true) {
                document.getElementById('ConfRidCan').value = '1';
            }
            else {
                document.getElementById('ConfRidCan').value = '0';
            }
        }

        function InserisciCanone() {

            var txt = 'Importo di locazione annuo:<br/><input id="txtcanone" type="text" name = "txtcanone" value = "" onkeypress="SostPuntVirg(event, this);" onchange="valid(this,\'notnumbers\');AutoDecimal2(this);" /><br />N.ro provvedimento:<br /><input type="text" id="txtNumProvv" name="txtNumProvv" /><br />Data provvedimento:<br /><input type="text" id="txtDataProvv" name="txtDataProvv" onkeypress="CompletaData(event,this);"/>';
            jQuery.prompt(txt, {
                submit: mycallInserCanone,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
            });

        }

        function mycallInserCanone(e, v, m, f) {

            if (v != undefined)
                if (v != '2') {
                    document.getElementById('importoCanone').value = f.txtcanone;
                    document.getElementById('numProvvedim').value = f.txtNumProvv;
                    document.getElementById('dataProvvedim').value = f.txtDataProvv;
                    if (document.getElementById('importoCanone').value != 0 && document.getElementById('numProvvedim').value != 0 && document.getElementById('dataProvvedim').value != 0) {
                        ConfRiduzCanone();
                        document.getElementById('Dom_Decisioni1_btnRiduzCanone').click();
                    }
                    else {
                        alert('Compilare i campi!');
                    }

                }

            return true;
        }


        function ConfRiduzCanone() {
            var chiediConferma
            var tipoDomanda
            var msg1 = "Attenzione, procedere con l\'adeguamento del canone?"
            //var msg1 = "Attenzione, procedendo con l\'operazione, se la domanda risultasse idonea, le variazioni al canone SARANNO APPLICATE DALLA PROSSIMA BOLLETTAZIONE UTILE.\nGli eventuali importi a credito dell\'inquilino, maturati dalla data di presentazione della domanda ad oggi, saranno gestiti previa verifica del Comune di Milano o ALER.\n\nSei sicuro di voler proseguire?"
            //var msg2 = "Attenzione, procedendo con l\'operazione, se la domanda risultasse idonea, le variazioni al canone SARANNO APPLICATE DALLA PROSSIMA BOLLETTAZIONE UTILE. Gli eventuali importi a credito dell\'inquilino, maturati dalla data di presentazione della domanda ad oggi, saranno gestiti previa verifica del Comune di Milano o ALER.\n\nSei sicuro di voler proseguire?"
            var msg2 = "Attenzione, procedere con l\'ampliamento del nucleo familiare?"
            var msg3 = "Attenzione, procedere con la variazione di intestazione?"
            var msg4 = "Attenzione, procedere con il cambio di intestazione? Il processo provvederà alla formalizzazione di un nuovo contratto di locazione in stato di BOZZA.\n\nSei sicuro di voler proseguire?"
            var msg5 = "Attenzione, procedendo con l\'operazione verrà concessa l\'autorizzazione all\'ospitalità temporanea per la persona inserita.\n\nSei sicuro di voler proseguire?"
            var msg6 = "Attenzione, procedendo con l\'operazione la domanda verrà messa in graduatoria per la stipula dei nuovi contratti.\n\nSei sicuro di voler proseguire?"
            var msg7 = "Attenzione, procedendo con l\'operazione la domanda verrà messa in assegnazione per una nuova offerta di alloggio.\n\nSei sicuro di voler proseguire?"
            var msg8 = "Attenzione, procedere con la creazione posizione abusiva? Il processo provvederà alla formalizzazione di un nuovo contratto di locazione in stato di BOZZA.\n\nSei sicuro di voler proseguire?"
            var msg9 = "Attenzione, procedendo con l\'operazione il processo provvederà alla formalizzazione di un nuovo contratto di locazione in stato di BOZZA.\n\nSei sicuro di voler proseguire?"
            var msg10 = "Attenzione, procedendo con l\'operazione il processo provvederà alla formalizzazione di un nuovo piano di rateizzazione.\n\nSei sicuro di voler proseguire?"

            tipoDomanda = document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value;

            if (tipoDomanda == '3') {
                chiediConferma = window.confirm(msg1);
                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';

                }
                else {
                    document.getElementById('ConfRidCan').value = '0';

                }
            }

            if (tipoDomanda == '2') {
                chiediConferma = window.confirm(msg2);
                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';

                }
                else {
                    document.getElementById('ConfRidCan').value = '0';

                }
            }


            if (tipoDomanda == '1' || tipoDomanda == '6') {
                chiediConferma = window.confirm(msg3);
                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';

                }
                else {
                    document.getElementById('ConfRidCan').value = '0';

                }
            }


            if (tipoDomanda == '0') {
                chiediConferma = window.confirm(msg4);
                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';

                }
                else {
                    document.getElementById('ConfRidCan').value = '0';

                }
            }

            if (tipoDomanda == '7') {
                chiediConferma = window.confirm(msg5);
                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';

                }
                else {
                    document.getElementById('ConfRidCan').value = '0';

                }
            }

            if (tipoDomanda == '5') {
                chiediConferma = window.confirm(msg6);
                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';

                }
                else {
                    document.getElementById('ConfRidCan').value = '0';

                }
            }



            if (tipoDomanda == '8') {
                chiediConferma = window.confirm(msg8);
                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';

                }
                else {
                    document.getElementById('ConfRidCan').value = '0';

                }
            }


            if (tipoDomanda == '9' || tipoDomanda == '10') {
                chiediConferma = window.confirm(msg9);

                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';


                }
                else {
                    document.getElementById('ConfRidCan').value = '0';

                }
            }




            if (tipoDomanda == '4') {
                chiediConferma = window.confirm(msg7);
                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';

                }
                else {
                    document.getElementById('ConfRidCan').value = '0';

                }
            }


            if (tipoDomanda == '12') {
                //ApriRateizzazione();
                chiediConferma = window.confirm(msg10);
                if (chiediConferma == true) {

                    document.getElementById('ConfRidCan').value = '1';

            }
                else {
                    document.getElementById('ConfRidCan').value = '0';


        }
            }


        }

    function ApriRateizzazione() {
        var win = null;
        LeftPosition = (screen.width) ? (screen.width - 795) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 600) / 2 : 0;
        LeftPosition = LeftPosition - 15;
        TopPosition = TopPosition - 15;
        window.open('../../RATEIZZAZIONE/RateizzMorositaPregressa.aspx?DPR='+ document.getElementById('Dom_Dichiara_Cambi1_txtDataPrRichiesta').value + '&CODRU=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value, 'Rateizz', 'width=795,height=600,scrollbars=no,toolbar=no,resizable=no,top=' + TopPosition + ',left=' + LeftPosition);

    };

        function RimborsoDeposito() {

            var txt = 'Selezionare le operazioni che si intendono eseguire sul NUOVO contratto: <br /><input type="radio" id="docDeposito" name="rdbScegli" value="docDeposito"/>Restituzione vecchio deposito e calcolo nuovo importo<br /><input type="radio" id = "docDeposito2" name="rdbScegli" value="docDeposito2"/>Restituzione deposito nel nuovo RU <br /><br /><input type="checkbox" id="chkRegistr" name="chk1" value="chkRegistr"/>Import dati registrazione fiscale';
            jQuery.prompt(txt, {
                submit: mycallRimborsoDeposito,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
            });

        };

        function mycallRimborsoDeposito(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    if (f.rdbScegli != undefined) {
                        errore = '0';

                        if (f.rdbScegli == "docDeposito") {
                            document.getElementById('restituisciCauz').value = '0';
                        }
                        if (f.rdbScegli == "docDeposito2") {
                            document.getElementById('restituisciCauz').value = '1';
                        }
                        if (f.chk1 == "chkRegistr") {
                            document.getElementById('fl_import_registr').value = '1';
                        } else {
                            document.getElementById('fl_import_registr').value = '0';
                        }

                        chiediConferma = window.confirm('Attenzione, il processo provvederà alla formalizzazione di un nuovo contratto di locazione ERP in stato di BOZZA.\n\nSei sicuro di voler proseguire?');
                        if (chiediConferma == true) {
                            document.getElementById('ConfRidCan').value = '1';
                            document.getElementById('Dom_Decisioni1_btnRiduzCanone').click();
                        }
                        else {
                            document.getElementById('ConfRidCan').value = '0';
                        }

                    }
                    else {
                        alert('Selezionare la funzione che si intende eseguire!');
                        errore = '1';
                        return false;
                    }
                }
        };
        function Indici() {

            window.open("../indici.aspx?" + document.getElementById('txtIndici').value, "", "top=0,left=0,width=490,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no");

        }

        function TempiProcessi() {

            window.open("../Locatari/TempisticaProcessi.aspx", "", "top=0,left=0,width=600,height=400,resizable=no,menubar=no,toolbar=no,scrollbars=no");

        }

        function CalcoloCanone() {
            var tipo;
            tipo = document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value;

            document.getElementById('Hcanone').value = '1';
            if (tipo != '5') {
                if (document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value != '') {
                    window.open("../Canone.aspx?ID=" + document.getElementById('HiddenID').value + '&T=' + tipo + '&COD=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&IDUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value, "", "top=0,left=0,width=550,height=600,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes");
                }
                else {
                    alert('Inserire il codice del contratto e assicurarsi di aver elaborato la domanda!');
                }
            }
            else {
                if (document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value != '') {
                    window.open("../Canone.aspx?ID=" + document.getElementById('HiddenID').value + '&T=' + tipo + '&COD=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&IDUNITA=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value, "", "top=0,left=0,width=550,height=600,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes");
                }
                else {
                    alert('Inserire il codice unità oggetto dello scambio e assicurarsi di aver elaborato la domanda!');
                }
            }



        }

        function AggContratto() {

            if (typeof opener != 'undefined') {
                if (typeof opener.opener != 'undefined') {
                    if (typeof opener.opener != 'unknown') {
                        if (opener.opener.name.substring(0, 9) == 'Contratto') {
                            if (opener.opener.document.getElementById('imgSalva')) {
                                opener.opener.document.getElementById('nuovocanone').value = document.getElementById('nuovocanone').value;
                                opener.opener.document.getElementById('pressoCOR').value = document.getElementById('pressoCOR').value;
                                opener.opener.document.getElementById('MostrMsgSalva').value = '0';
                                opener.opener.document.getElementById('imgSalva').click();
                            }
                        }
                    }
                }
                if (typeof opener.opener != 'undefined') {
                    if (typeof opener.opener != 'unknown') {
                        if (opener.name.substring(0, 9) == 'Contratto') {
                            if (opener.document.getElementById('imgSalva')) {
                                opener.document.getElementById('nuovocanone').value = document.getElementById('nuovocanone').value;
                                opener.document.getElementById('pressoCOR').value = document.getElementById('pressoCOR').value;
                                opener.opener.document.getElementById('MostrMsgSalva').value = '0';
                                opener.document.getElementById('imgSalva').click();
                            }
                        }
                    }
                }
            }
        }

        //function rinnovaContratto() {
        //                    if (typeof opener != 'undefined') 
        //                    {
        //                        if (opener.opener.name.substring(0,9) == 'Contratto')
        //                        {
        //                           var dataR;
        //                           dataR = document.getElementById('data_riconsegna').value;
        //                           opener.opener.window.document.getElementById('datariconsegna').value = dataR;
        //                            opener.opener.document.getElementById('imgSalva').click();
        //                            
        //                        }
        //                        if (opener.opener.opener.name.substring(0,9) == 'Contratto')
        //                        {
        //                           
        //                                    opener.opener.window.ApriRinnovoUSD();
        //                            
        //                        }

        //                    }
        //                    else 
        //                    {
        //                        if (opener.name.substring(0,9) == 'Contratto')
        //                        {
        //                            
        //                            opener.opener.window.ApriCambioBOX();
        //                            

        //                        }
        //                    
        //                    }
        //                                        
        //}



        function ApriContratto() {
            if (document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value != '') {
                window.open('../DatiContratto.aspx?COD=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value, 'DatiContratto', '');
            }
        }
        function ElStampe() {

            window.open('../ElencoStampe.aspx?IDDIC=' + <%=lIdDichiarazione %>,'elstmp', '');

    }

    function VisualizzaEventi() {

        window.open('../EventiVSA.aspx?IDDOM=' + <%=lIdDomanda %>,'eleventi', '');

    }


</script>
</html>
