<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DichAUnuova.aspx.vb" Inherits="VSA_NuovaDichiarazioneVSA_DichAUnuova" %>

<%@ Register Src="Tab_Reddito.ascx" TagName="Dic_Reddito" TagPrefix="uc2" %>
<%@ Register Src="Tab_Patrimonio.ascx" TagName="Dic_Patrimonio" TagPrefix="uc3" %>
<%@ Register Src="Tab_Documentazione.ascx" TagName="dic_Documenti" TagPrefix="uc4" %>
<%@ Register Src="Tab_Nucleo.ascx" TagName="Tab_Nucleo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 0;
    var Selezionato;
    var OldColor;
    var SelColo;

    function $onkeydown() {

        if (event.keyCode == 8) {
            //alert('Questo tasto non può essere usato!');
            event.keyCode = 0;
        }
    }

    function visibileSottoscr() {
        if (document.getElementById('divSottoscr')) {

            var radioButtons = document.forms['form1'].elements['rdbListSott'];

            for (var x = 0; x < radioButtons.length; x++) {
                if (radioButtons[x].checked) {
                    document.getElementById('divSottoscr').style.display = 'none';
                }
                else {
                    document.getElementById('divSottoscr').style.display = 'block';
                }
            }

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
</script>
<head runat="server">
    <title>Dichiarazione VSA</title>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js2/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="js2/jquery-impromptu.4.0.min.js"></script>
    <script type="text/javascript" src="js2/jquery-ui-1.9.0.custom.js"></script>
    <script type="text/javascript" src="js2/jquery-ui-1.9.0.custom.min.js"></script>
    <link rel="stylesheet" type="text/css" href="Styles/impromptu.css" />
    <link href="Styles/StileAU.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-image: url(../../NuoveImm/XBackGround.gif); background-repeat: repeat-x;">
    <div id="caric" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px; margin-top: -48px; background-image: url('../Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2222" runat="server" ImageUrl="..\..\NuoveImm\load.gif" />
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
                            <td align="center">
                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Dichiarazione
                                <asp:Label ID="lblTipoDom" runat="server" Text=""></asp:Label>
                                </span></strong>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">
                                <br />
                                <asp:Image ID="imgAlert" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="true"
                                    Style="display: none" />
                                <asp:Label ID="lblAvviso" runat="server" Font-Names="arial" Font-Size="9pt" Text="Attenzione, in caso di domanda di RIDUZIONE CANONE inserire i redditi dell'anno corrente, anche se presunti!"
                                    Font-Italic="True" Style="display: none"></asp:Label>
                                <asp:Label ID="lblDichNUOVA" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="10pt" Height="18px" ForeColor="Black" BackColor="#CCFFFF" Visible="False">PG Dich. correlata </asp:Label>
                                <asp:Label ID="lblDichNUOVA2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="10pt" Height="18px" ForeColor="Black" BackColor="#CCFFFF" Visible="False"></asp:Label>
                                <br />
                                <br />
                                <asp:CheckBox ID="ChFO" runat="server" Font-Names="arial" Font-Size="9pt" Text="TRATTASI DI DICHIARAZIONE PER FORZE DELL'ORDINE" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td style="width: 180px">
                                <asp:Button ID="btnSalva" runat="server" OnClientClick="Uscita=1;"
                                    ToolTip="Salva" CssClass="bottone" Text="Salva" />
                                <asp:Button runat="server" ID="btnApplica" CssClass="bottone" Text="Elabora" Enabled="False"
                                    OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;"
                                    ToolTip="Elabora dichiarazione" Visible="False" Width="60px" />
                                <asp:Image ID="imgElabora" runat="server" ImageUrl="~/CALL_CENTER/Immagini/alertSegn.gif"
                                    ToolTip="Elaborare" Width="18px" Height="23px" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="imgStampa" runat="server" CssClass="bottone" Text="Stampa" OnClientClick="Uscita=1;"
                                    ToolTip="Elabora e Stampa" Width="60px" />
                            </td>
                            <td>
                                <asp:Button ID="IMGCanone" runat="server" Text="Canone a Regime" CssClass="bottone"
                                    OnClientClick="CalcoloCanone();return false;" ToolTip="Calcolo del canone a regime secondo L.R. 27/07 e L.R. 36/2008"
                                    Width="120px" Visible="False" />
                            </td>
                            <td width="330px" align="center">&nbsp
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="text-align: right; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" CssClass="bottone" Text="Passa alla domanda" ID="imgVaiDomanda"
                                                ToolTip="Passa alla domanda" Width="130px" BackColor="#FFFF84" Visible="False" />
                                            <asp:Button ID="imgAnagrafe" runat="server" CssClass="bottone" Text="Anagrafe" ToolTip="Anagrafe della popolazione"
                                                Width="75" />
                                        </td>
                                        <td>
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
                                        <td>
                                            <asp:Button ID="imgUscita" runat="server" ToolTip="Esci" CssClass="bottone" Text="Esci"
                                                Width="42px" OnClientClick="AggPaginaAbbinamento();document.getElementById('H1').value=0;" />
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="width: 37%">
                                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"
                                    ForeColor="Black" Height="18px" Width="220px">ANNO DI RIFERIMENTO REDDITUALE</asp:Label>
                                <asp:DropDownList ID="cmbAnnoReddituale" runat="server" Font-Names="arial" Font-Size="8pt"
                                    ForeColor="Black" Width="80px" AutoPostBack="True" ToolTip="Anno di riferimento reddituale">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 35%">&nbsp
                            </td>
                            <td style="vertical-align: top; text-align: right;">&nbsp
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
                            <td style="width: 12%;">DICHIARAZIONE NUM.
                            </td>
                            <td style="width: 13%">
                                <asp:TextBox ID="lblPG" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                    BorderWidth="2px" CssClass="CssLblValori" Width="80px" ReadOnly="True" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="9pt">0000000000</asp:TextBox>
                                <asp:TextBox ID="lblSlash" runat="server" ReadOnly="True" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="11pt" BorderStyle="None" Width="5px" BorderWidth="0px" Visible="False">/</asp:TextBox>
                                <asp:TextBox ID="lblPGcoll" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                    BorderWidth="2px" CssClass="CssLblValori" Width="80px" ReadOnly="True" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="9pt" Visible="False">0000000000</asp:TextBox>
                                <asp:Image ID="lblNumDich" runat="server" ImageUrl="~/NuoveImm/Info_ElencoDomOLD.png"
                                    Style="cursor: pointer;" Width="23px" Height="25px" Visible="false" ToolTip="Visualizza dichiarazione collegata" />
                            </td>
                            <td style="width: 12%; text-align: right;">DATA&nbsp;
                            </td>
                            <td style="width: 13%">
                                <asp:TextBox ID="txtDataPG" runat="server" CssClass="CssLblValori" BorderColor="#0078C2"
                                    BorderStyle="Solid" BorderWidth="2px" Width="80px" TabIndex="1" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td style="width: 12%; text-align: right;">
                                <asp:Label ID="lblDomPG" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                    Text="DOMANDA NUM."></asp:Label>&nbsp
                            </td>
                            <td style="width: 13%">
                                <asp:TextBox ID="lblDomAssociata" runat="server" BorderColor="#0078C2" BorderStyle="Solid"
                                    BorderWidth="2px" Width="80px" CssClass="CssLblValori" TabIndex="1" Font-Bold="True"></asp:TextBox>
                            </td>
                            <td style="width: 13%; text-align: right;">STATO&nbsp;
                            </td>
                            <td style="width: 13%">
                                <div id="divBordoDrop" class="bordoComboBox">
                                    <asp:DropDownList ID="cmbStato" runat="server" TabIndex="3" Width="190px" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="9pt" CssClass="CssLblValori">
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
                            <td align="center">
                                <table id="comuneAssegn" style="display: none;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Style="vertical-align: top;" Font-Names="Arial"
                                                Font-Size="9pt" Font-Bold="True" Text="Comune di assegnazione"></asp:Label>&nbsp&nbsp&nbsp
                                        <asp:DropDownList ID="cmbComunAssegn" runat="server" Style="vertical-align: top;"
                                            Font-Names="Arial" Font-Size="9pt" Width="280px">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
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
                                        <td>Cognome
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCognome" runat="server" TabIndex="4" Width="140px"></asp:TextBox>
                                        </td>
                                        <td>Nome
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtNome" runat="server" TabIndex="5" Width="140px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cod.Fiscale
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCF" runat="server" TabIndex="6" AutoPostBack="True" Width="140px"></asp:TextBox>
                                        </td>
                                        <td colspan="7">
                                            <asp:LinkButton ID="CFLABEL" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nato in
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbNazioneNas" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="7" Enabled="False" Width="145px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPr" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                                Text="Provincia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbPrNas" runat="server" AutoPostBack="True" CssClass="CssProv"
                                                Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="8" Width="50px"
                                                Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblComNas" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                                Text="Comune"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbComuneNas" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="9" Enabled="False" Width="145px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>Data
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDataNascita" runat="server" Columns="7" MaxLength="10" TabIndex="10"
                                                CssClass="CssMaiuscolo" Width="90px"></asp:TextBox>
                                            <asp:Label ID="lblErrData" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
                                                Font-Size="X-Small" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Sottoscrittore
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdbListSott" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                RepeatDirection="Horizontal" AutoPostBack="True">
                                                <asp:ListItem Value="1">SI</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblCognomeS" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                Font-Size="9pt" Text="Cognome"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:TextBox ID="txtCognomeS" runat="server" TabIndex="4" Width="140px"></asp:TextBox>
                                        </td>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblNomeS" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                                Text="Nome"></asp:Label>
                                        </td>
                                        <td colspan="3" style="">
                                            <asp:TextBox ID="txtNomeS" runat="server" TabIndex="5" Width="140px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblNatoS" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                                Text="Nato in"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:DropDownList ID="cmbNazioneNasSott" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="7" Enabled="False" Width="145px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblPrNasSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                Font-Size="9pt" Text="Provincia"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:DropDownList ID="cmbPrNasSott" runat="server" AutoPostBack="True" CssClass="CssProv"
                                                Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="8" Width="50px"
                                                Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblComuneNas2" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                Font-Size="9pt" Text="Comune"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:DropDownList ID="cmbComuneNas2" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="9" Enabled="False" Width="145px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblDataNascS" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                Font-Size="9pt" Text="Data"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:TextBox ID="txtDataNascitaSott" runat="server" Columns="7" MaxLength="10" TabIndex="10"
                                                CssClass="CssMaiuscolo" Width="90px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblResidS" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                                Text="Residenza"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:DropDownList ID="cmbNazioneResSott" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="25" Enabled="True" Width="195px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblPrResSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                Font-Size="9pt" Text="Provincia"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:DropDownList ID="cmbPrResSott" runat="server" AutoPostBack="True" CssClass="CssProv"
                                                Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="26" Width="50px"
                                                Enabled="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblComuneResSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                Font-Size="9pt" Text="Comune"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:DropDownList ID="cmbComuneResSott" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="27" Enabled="True" Width="145px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblIndirSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                Font-Size="9pt" Text="Indirizzo"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:DropDownList ID="cmbTipoIResSott" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="28" Enabled="True" Width="80px">
                                            </asp:DropDownList>
                                            &nbsp
                                        <asp:TextBox ID="txtIndResSott" runat="server" TabIndex="29" Width="100px"></asp:TextBox>
                                        </td>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblCivicoSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                Font-Size="9pt" Text="Civico"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:TextBox ID="txtCivicoResSott" runat="server" TabIndex="30" Width="60px"></asp:TextBox>
                                        </td>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblCapSot" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                                Text="CAP"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:TextBox ID="txtCAPResSott" runat="server" TabIndex="31" Width="40px"></asp:TextBox>
                                        </td>
                                        <td style="font-family: Arial; font-style: italic; font-size: 9pt;">
                                            <asp:Label ID="lblTelSot" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                                                Text="Telefono"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:TextBox ID="txtTelResSott" runat="server" TabIndex="33" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <%--<div id="divSottoscr" style="display: block;width:100%;">
                                <table width="100%;" style="font-family: Arial; font-size: 9pt; font-style:italic;">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        Cognome
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCognomeS" runat="server" TabIndex="4" Width="140px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Nome
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtNomeS" runat="server" TabIndex="5" Width="140px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Nato in
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbNazioneNasSott" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                            Font-Names="Arial" Font-Size="9pt" TabIndex="7" Enabled="False" Width="145px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPrNasSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                            Font-Size="9pt" Text="Provincia"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbPrNasSott" runat="server" AutoPostBack="True" CssClass="CssProv"
                                                            Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="8" Width="50px"
                                                            Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblComuneNas2" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                            Font-Size="9pt" Text="Comune"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbComuneNas2" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                            Font-Names="Arial" Font-Size="9pt" TabIndex="9" Enabled="False" Width="145px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        Data
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDataNascitaSott" runat="server" Columns="7" MaxLength="10" TabIndex="10"
                                                            CssClass="CssMaiuscolo" Width="90px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Residenza&nbsp
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbNazioneResSott" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                            Font-Names="Arial" Font-Size="9pt" TabIndex="25" Enabled="True" Width="290px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPrResSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                            Font-Size="9pt" Text="Provincia"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbPrResSott" runat="server" AutoPostBack="True" CssClass="CssProv"
                                                            Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="26" Width="50px"
                                                            Enabled="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblComuneResSott" runat="server" CssClass="CssLabel" Font-Names="Arial"
                                                            Font-Size="9pt" Text="Comune"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbComuneResSott" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                            Font-Names="Arial" Font-Size="9pt" TabIndex="27" Enabled="True" Width="145px">
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
                                                        <asp:DropDownList ID="cmbTipoIResSott" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                            Font-Names="Arial" Font-Size="9pt" TabIndex="28" Enabled="True" Width="80px">
                                                        </asp:DropDownList>
                                                        &nbsp
                                                        <asp:TextBox ID="txtIndResSott" runat="server" TabIndex="29" Width="195px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Civico
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCivicoResSott" runat="server" TabIndex="30" Width="60px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        CAP
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCAPResSott" runat="server" TabIndex="31" Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Telefono
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTelResSott" runat="server" TabIndex="33" Width="100px"></asp:TextBox>
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
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="font-family: Arial; font-size: 9pt;">
                        <tr>
                            <td style="width: 100%; color: #0078C2; font-family: Arial; font-size: 11pt; vertical-align: top;"
                                colspan="8">
                                <img src="img/Img_Alloggio.png" style="width: 26px; height: 26px;" alt="Dati Dichiarante" />
                                <strong>Dati Residenza</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 3px solid #CCCCCC;">
                                <table width="100%" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td>Residenza&nbsp
                                        </td>
                                        <td width="15px">&nbsp
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbNazioneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="25" Enabled="True" Width="290px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>Provincia
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbPrRes" runat="server" AutoPostBack="True" CssClass="CssProv"
                                                Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="26" Width="70px"
                                                Enabled="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td>Comune
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbComuneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="27" Enabled="True" Width="130px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Indirizzo
                                        </td>
                                        <td width="15px">&nbsp
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbTipoIRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
                                                Font-Names="Arial" Font-Size="9pt" TabIndex="28" Enabled="True" Width="80px">
                                            </asp:DropDownList>
                                            &nbsp
                                        <asp:TextBox ID="txtIndRes" runat="server" TabIndex="29" Width="195px"></asp:TextBox>
                                        </td>
                                        <td>Civico
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCivicoRes" runat="server" TabIndex="30" Width="60px"></asp:TextBox>
                                        </td>
                                        <td>CAP
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCAPRes" runat="server" TabIndex="31" Width="40px"></asp:TextBox>
                                        </td>
                                        <td>Telefono 1
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTelRes" runat="server" TabIndex="33" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>Telefono 2
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTelRes1" runat="server" TabIndex="33" Width="100px"></asp:TextBox>
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
                                        <li><a href="#tabs-1">Nucleo</a></li>
                                        <li><a href="#tabs-2">Reddito</a></li>
                                        <li><a href="#tabs-3">Patrimonio</a></li>
                                        <li><a href="#tabs-4">Documenti e note</a></li>
                                    </ul>
                                    <div id="tabs-1">
                                        <uc1:Tab_Nucleo ID="Tab_Nucleo1" runat="server" />
                                    </div>
                                    <div id="tabs-2">
                                        <uc2:Dic_Reddito ID="Dic_Reddito1" runat="server" />
                                    </div>
                                    <div id="tabs-3">
                                        <uc3:Dic_Patrimonio ID="Dic_Patrimonio1" runat="server" />
                                    </div>
                                    <div id="tabs-4">
                                        <uc4:dic_Documenti ID="dic_Documenti1" runat="server" />
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
        <div id="dialog" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial; width: 1000px">
        </div>
        <div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt; font-family: Arial">
        </div>
        <asp:Button ID="btnfunzelimina" runat="server" Text="" Style="display: none;" CauseValidation="false" />
        <asp:Button ID="btnfunzesci2" runat="server" Text="" Style="display: none;" CauseValidation="false" />
        <asp:HiddenField ID="tipoCausale" runat="server" />
        <asp:HiddenField ID="propdec" runat="server" />
        <asp:HiddenField ID="txtTab" runat="server" />
        <asp:HiddenField ID="H1" runat="server" Value="0" />
        <asp:HiddenField ID="H2" runat="server" Value="0" />
        <asp:HiddenField ID="assTemp" runat="server" />
        <asp:HiddenField ID="txt36" runat="server" Value="1" />
        <asp:HiddenField ID="solalettura" runat="server" Value="0" />
        <asp:HiddenField ID="txtbinserito" runat="server" Value="0" />
        <asp:HiddenField ID="vcomunitario" runat="server" Value="0" />
        <asp:HiddenField ID="nonstampare" runat="server" Value="0" />
        <asp:HiddenField ID="txtbinseritoTab" runat="server" Value="0" />
        <asp:HiddenField ID="tabSelect" runat="server" Value="-1" />
        <asp:HiddenField ID="numComp" runat="server" />
        <asp:HiddenField ID="idComp" runat="server" Value="0" />
        <asp:HiddenField ID="salvaEsterno" runat="server" Value="0" />
        <asp:HiddenField ID="tipoRichiesta" runat="server" />
        <asp:HiddenField ID="id_contr" runat="server" />
        <asp:HiddenField ID="codUI" runat="server" />
        <asp:HiddenField ID="codcontr2" runat="server" />
        <asp:HiddenField ID="stampaClick" runat="server" Value="0" />
        <asp:HiddenField ID="gradoParentela" runat="server" Value="1" />
        <asp:HiddenField ID="fuoriMilano" runat="server" Value="0" />
        <asp:HiddenField ID="RU392" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="fl_proprieta" runat="server" Value="0" />
        <asp:HiddenField ID="fl_elabora" runat="server" Value="0" />
    </form>
</body>
<script type="text/javascript">

    if (document.getElementById('caric')) {
        document.getElementById('caric').style.visibility = 'hidden';
    };
    if (document.getElementById('fuoriMilano').value == '1') {
        document.getElementById('comuneAssegn').style.display = 'block';
        document.getElementById('lblDomPG').style.display = 'none';
        document.getElementById('lblDomAssociata').style.display = 'none';
    };

    initialize();

    visibileSottoscr();

    function ElStampe() {

        window.open('../ElencoStampe.aspx?IDDIC=' + <%=lIdDichiarazione %>,'elstmp', '');

    }

    function cerca() {
        if (document.all) {
            finestra = showModelessDialog('../Find.htm', window, 'dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
            finestra.focus
            finestra.document.close()
        }
        else if (document.getElementById) {
            self.find()
        }
        else window.alert('Il tuo browser non supporta questo metodo')
    }

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
        aaa = window.open('../loadingCF.htm', '', 'height=150,top=' + TopPosition + ',left=' + LeftPosition + ',width=250');
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

    //    function ConfermaEsci() {

    //        if (document.getElementById('txtModificato').value == '1') {

    //            var chiediConferma
    //            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?\nUSCIRE SENZA SALVARE CAUSERA\' LA PERDITA DELLE MODIFICHE!\nPER NON USCIRE PREMERE IL PULSANTE ANNULLA.");
    //            if (chiediConferma == false) {
    //                document.getElementById('txtModificato').value = '111';
    //            }
    //            else {
    //                if (document.getElementById('caric')) {
    //                    document.getElementById('caric').style.visibility = 'visible';
    //                }
    //            }
    //        }
    //        else {
    //            if (document.getElementById('caric')) {
    //                document.getElementById('caric').style.visibility = 'visible';
    //            }
    //        }
    //    }

    function AggPaginaAbbinamento() {
        
            
                if (typeof opener != 'undefined') {
                    if (typeof opener != 'unknown') {
                        if (opener.document.getElementById('btnAggiornaPagina')) {
                            opener.document.getElementById('btnAggiornaPagina').click();
                        }
                        if (opener.document.getElementById('btnRicarica')) {
                            opener.document.getElementById('btnRicarica').click();
                        }
                    }
                }
         
    }



    /* SCRIPT TAB NUCLEO */

    function MyDialogArguments() {
        this.Sender = null;
        this.StringValue = "";
    }


    function AggiungiNucleo() {
        if (document.getElementById('txtModificato').value == '1' || document.getElementById('txtModificato').value == '111') {
            alert('Salvare le modifiche prima di procedere!');
        }
        else {
            document.getElementById('caric').style.visibility = 'visible';
            a = document.getElementById('Tab_Nucleo1_txtProgr').value;
            dialogArgs = new MyDialogArguments();
            dialogArgs.StringValue = a;
            dialogArgs.Sender = window;
            var dialogResults = window.showModalDialog('com_nucleo.aspx?OP=0&PR=' + a + '&IDCONN=' + <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:460px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('btnSalva').click();
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';

                }
            }
        }
    }

    function ModificaNucleo() {
        a = document.getElementById('Tab_Nucleo1_txtProgr').value;
        if (document.getElementById('idComp').value == 0) {
            alert('Selezionare una riga dalla lista!');
        }
        else {
            document.getElementById('caric').style.visibility = 'visible';
            cognome = document.getElementById('Tab_Nucleo1_cognome').value;
            nome = document.getElementById('Tab_Nucleo1_nome').value;
            data = document.getElementById('Tab_Nucleo1_data_nasc').value;
            cf = document.getElementById('Tab_Nucleo1_cod_fiscale').value;
            parenti = document.getElementById('Tab_Nucleo1_parentela').value;
            inv = document.getElementById('Tab_Nucleo1_perc_inval').value;
            asl = document.getElementById('Tab_Nucleo1_asl').value;
            acc = document.getElementById('Tab_Nucleo1_ind_accomp').value;
            tipo_inval = document.getElementById('Tab_Nucleo1_tipo_inval').value;
            natura_inval = document.getElementById('Tab_Nucleo1_natura_inval').value;
            nuovoComp = document.getElementById('Tab_Nucleo1_nuovoComp').value;
            dataIngr = document.getElementById('Tab_Nucleo1_dataIngr').value;

            var dialogResults = window.showModalDialog('com_nucleo.aspx?OP=1&RI=' + document.getElementById('idComp').value + '&IDCONN=' + <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %> +'&COGNOME=' + cognome + '&NOME=' + nome + '&DATA=' + data + '&CF=' + cf + '&PARENTI=' + parenti + '&INV=' + inv + '&ASL=' + asl + '&ACC=' + acc + '&PR=' + a + '&TIPO_INVAL=' + tipo_inval + '&NATURA_INVAL=' + natura_inval + '&NCOMP=' + nuovoComp + '&DATAINGR=' + dataIngr, window, 'status:no;dialogWidth:460px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('btnSalva').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';

                }
            }
        }
    }

    function ModificaSpese() {
        if (document.getElementById('txtModificato').value != 1) {
            if (document.getElementById('Tab_Nucleo1_idCompSpese').value == 0) {
                alert('Selezionare una riga dalla lista!');
            }
            else {
                document.getElementById('caric').style.visibility = 'visible';
                componente = document.getElementById('Tab_Nucleo1_componente').value;
                importo = document.getElementById('Tab_Nucleo1_importo').value;
                descrizione = document.getElementById('Tab_Nucleo1_descrizione').value;
                var dialogResults = window.showModalDialog('com_spese.aspx?RI=' + document.getElementById('Tab_Nucleo1_idCompSpese').value + '&CM=' + componente + '&IM=' + importo + '&DS=' + descrizione + '&IDCONN=' + <%=vIdConnessione %>, window, 'status:no;dialogWidth:433px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
                if (dialogResults != undefined) {
                    if (dialogResults == '1') {
                        document.getElementById('salvaEsterno').value = '1';
                        document.getElementById('btnSalva').click();
                        document.getElementById('salvaEsterno').value = '0';
                    }

                }

            }
        } else {
            alert('Salvare le modifiche prima di procedere!');
        }

    }

    function EliminaSoggetto() {
        a = document.getElementById('Tab_Nucleo1_txtProgr').value;


        if (document.getElementById('idComp').value == 0 || document.getElementById('idComp').value == -1) {
            alert('Selezionare una riga dalla lista!');
        }
        else {
            if (a != 0) {
                document.getElementById('caric').style.visibility = 'visible';
                cognome = document.getElementById('Tab_Nucleo1_cognome').value;
                nome = document.getElementById('Tab_Nucleo1_nome').value;
                data = document.getElementById('Tab_Nucleo1_data_nasc').value;
                cf = document.getElementById('Tab_Nucleo1_cod_fiscale').value;

                var dialogResults = window.showModalDialog('com_uscita.aspx?OP=1&RI=' + document.getElementById('idComp').value + '&IDCONN=' + <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %> + '&COGNOME=' + cognome + '&NOME=' + nome + '&DATA=' + data + '&CF=' + cf, window, 'status:no;dialogWidth:470px;dialogHeight:340px;dialogHide:true;help:no;scroll:no');
                if (dialogResults != undefined) {
                    if (dialogResults == '1') {
                        document.getElementById('salvaEsterno').value = '1';
                        document.getElementById('btnSalva').click();
                        document.getElementById('salvaEsterno').value = '0';
                    }

                }
            }
            else {
                alert('Impossibile eliminare il componente scelto!');
            }
        }

    }


    /* FINE SCRIPT TAB NUCLEO */


    /* SCRIPT DETRAZIONI */

    function AggiungiDetrazioni() {
        if (document.getElementById('txtModificato').value != 1) {
            document.getElementById('caric').style.visibility = 'visible';
            a = document.getElementById('Dic_Reddito1_idCompDetraz').value;
            dialogArgs = new MyDialogArguments();
            dialogArgs.StringValue = a;
            dialogArgs.Sender = window;
            var dialogResults = window.showModalDialog('com_detrazioni.aspx?OP=0&PR=' + a + '&IDCONN=' + <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %> , window, 'status:no;dialogWidth:433px;dialogHeight:340px;dialogHide:true;help:no;scroll:no');

            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('btnSalva').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';

                }
            }
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }

    }


    function ModificaDetrazioni() {
        if (document.getElementById('Dic_Reddito1_idDetraz').value == 0 || document.getElementById('Dic_Reddito1_idDetraz').value == -1) {
            alert('Selezionare una riga dalla lista!');
        }
        else {
            document.getElementById('caric').style.visibility = 'visible';
            componente = document.getElementById('Dic_Reddito1_idCompDetraz').value;
            importo = document.getElementById('Dic_Reddito1_importo').value;
            tipo = document.getElementById('Dic_Reddito1_tipoDetraz').value;

            var dialogResults = window.showModalDialog('com_detrazioni.aspx?OP=1&RI=' + document.getElementById('Dic_Reddito1_idDetraz').value + '&IDCONN=' + <%=vIdConnessione %> +'&COMPONENTE=' + componente + '&COMP=' + componente + '&IM=' + importo + '&TI=' + tipo + '&IDDICH=' + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:433px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');

            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('btnSalva').click();
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
        if (document.getElementById('txtModificato').value != 1) {
            document.getElementById('caric').style.visibility = 'visible';
            a = document.getElementById('Dic_Patrimonio1_idCompMob').value;
            dialogArgs = new MyDialogArguments();
            dialogArgs.StringValue = a;
            dialogArgs.Sender = window;
            var dialogResults = window.showModalDialog('com_patrimonio.aspx?OP=0&PR=' + a + '&IDCONN=' + <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %> , window, 'status:no;dialogWidth:433px;dialogHeight:340px;dialogHide:true;help:no;scroll:no');

            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('btnSalva').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';
                }
            }
        } else {
            alert('Salvare le modifiche prima di procedere!');
        }

    }

    function ModificaPatrimMob() {
        if (document.getElementById('txtModificato').value != 1) {
            if (document.getElementById('Dic_Patrimonio1_idCompMob').value == 0 || document.getElementById('Dic_Patrimonio1_idCompMob').value == -1) {
                alert('Selezionare una riga dalla lista!');
            }
            else {
                document.getElementById('caric').style.visibility = 'visible';
                componente = document.getElementById('Dic_Patrimonio1_idCompMob').value;
                abi = document.getElementById('Dic_Patrimonio1_abi').value;

                inter = document.getElementById('Dic_Patrimonio1_inter').value;
                imp = document.getElementById('Dic_Patrimonio1_importo').value;
                tipo = document.getElementById('Dic_Patrimonio1_tipo').value;
                prp = document.getElementById('Dic_Patrimonio1_prp').value;

                var dialogResults = window.showModalDialog('com_patrimonio.aspx?PRP=' + prp + '&OP=1&RI=' + document.getElementById('Dic_Patrimonio1_idPatrMob').value + '&IDCONN=' + <%=vIdConnessione %> +'&COMPONENTE=' + componente + '&COMP=' + componente + '&ABI=' + abi + '&INT=' + inter + '&IMP=' + imp + '&TIPO=' + tipo + '&IDDICH=' + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:433px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');

                if (dialogResults != undefined) {
                    if (dialogResults == '1') {
                        document.getElementById('salvaEsterno').value = '1';
                        document.getElementById('btnSalva').click();
                        document.getElementById('salvaEsterno').value = '0';
                    }
                    if (dialogResults == '2') {
                        document.getElementById('txtModificato').value = '1';
                    }
                }
            }

        } else {
            alert('Salvare le modifiche prima di procedere!');
        }

    }

    /* FINE SCRIPT PATRIMONIO MOBILIARE */


    /* SCRIPT PATRIMONIO IMMOBILIARE */

    function AggiungiPatrimonioI() {
        if (document.getElementById('txtModificato').value != 1) {
            document.getElementById('caric').style.visibility = 'visible';
            a = document.getElementById('Dic_Reddito1_idCompDetraz').value;
            dialogArgs = new MyDialogArguments();
            dialogArgs.StringValue = a;
            dialogArgs.Sender = window;

            var dialogResults = window.showModalDialog('com_patrimonioI.aspx?OP=0&COMPONENTE=' + a + '&IDDICH=' + <%=lIdDichiarazione %> + '&IDCONN=' + <%=vIdConnessione %>,window, 'status:no;dialogWidth:433px;dialogHeight:440px;dialogHide:true;help:no;scroll:no');
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('btnSalva').click();
                    document.getElementById('salvaEsterno').value = '0';
                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';
                }
            }
        } else {
            alert('Salvare le modifiche prima di procedere!');
        }

    }

    function ModificaPatrimonioI() {
        if (document.getElementById('txtModificato').value != 1) {
            if (document.getElementById('Dic_Patrimonio1_idCompImmob').value == 0 || document.getElementById('Dic_Patrimonio1_idCompImmob').value == -1) {
                alert('Selezionare una riga dalla lista!');
            }
            else {
                document.getElementById('caric').style.visibility = 'visible';
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
                //pie =document.getElementById('Dic_Patrimonio1_pie').value;

                //indirizzo = document.getElementById('Dic_Patrimonio1_indirizzo').value;
                //civico = document.getElementById('Dic_Patrimonio1_civico').value;
                rendita = document.getElementById('Dic_Patrimonio1_rendita').value;

                valoreM = document.getElementById('Dic_Patrimonio1_valoreM').value;

                var dialogResults = window.showModalDialog('com_patrimonioI.aspx?OP=1&RI=' + document.getElementById('Dic_Patrimonio1_idPatrImmob').value + '&IDCONN=' + <%=vIdConnessione %> +'&COMPONENTE=' + componente + '&COMP=' + componente + '&TIPO=' + tipo + '&TIPOPR=' + tipoPropr + '&PER=' + perc + '&VAL=' + valore + '&VALM=' + valoreM + '&MU=' + mutuo + '&SUP=' + sup + '&VANI=' + vani + '&comune=' + comune + '&CATASTALE=' + catastale + '&RENDITA=' + rendita + '&IDDICH=' + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:433px;dialogHeight:440px;dialogHide:true;help:no;scroll:no');
                if (dialogResults != undefined) {
                    if (dialogResults == '1') {
                        document.getElementById('salvaEsterno').value = '1';
                        document.getElementById('btnSalva').click();
                        document.getElementById('salvaEsterno').value = '0';
                    }
                    if (dialogResults == '2') {
                        document.getElementById('txtModificato').value = '1';
                    }
                }
            }
        } else {
            alert('Salvare le modifiche prima di procedere!');
        }
    }

    /* FINE SCRIPT PATRIMONIO IMMOBILIARE */




    /* SCRIPT TAB DOCUMENTI */

    function AggiungiDocumento() {

        dialogArgs = new MyDialogArguments();
        dialogArgs.StringValue = '';
        dialogArgs.Sender = window;

        var dialogResults = window.showModalDialog('com_documenti.aspx?OP=0&IDCONN=' + <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %>, 'window', 'status:no;dialogWidth:580px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');
        if (dialogResults != undefined) {
            if (dialogResults == '1') {
                document.getElementById('salvaEsterno').value = '1';
                document.getElementById('btnSalva').click();
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
        if (document.getElementById('txtModificato').value != 1) {
            document.getElementById('caric').style.visibility = 'visible';
            var dialogResults = window.showModalDialog('Dett_Reddito.aspx?MOD=0&IDCONN=' + <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:800px;dialogHeight:690px;dialogHide:true;help:no;scroll:no;');


            //          var dialogResults =window.open('Dett_Reddito.aspx?MOD=0&IDCONN='+ <%=vIdConnessione %> +'&IDDICH=' + <%=lIdDichiarazione %>, 'Dett_Reddito', 'status:no;Width:800px;Height:690px;help:no;scroll:no');

            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('btnSalva').click();
                    document.getElementById('salvaEsterno').value = '0';

                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';
                }
            }
        } else {
            alert('Salvare le modifiche prima di procedere!');
        }

    }

    function ModificaDettReddito() {
        if (document.getElementById('Dic_Reddito1_idReddito').value != '') {
            document.getElementById('caric').style.visibility = 'visible';
            var dialogResults = window.showModalDialog('Dett_Reddito.aspx?MOD=1&IDCONN=' + <%=vIdConnessione %> +'&IDREDD=' + document.getElementById('Dic_Reddito1_idReddito').value + '&IDDICH=' + <%=lIdDichiarazione %>, window, 'status:no;dialogWidth:800px;dialogHeight:690px;dialogHide:true;help:no;scroll:no');
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('salvaEsterno').value = '1';
                    document.getElementById('btnSalva').click();
                    document.getElementById('salvaEsterno').value = '0';

                }
                if (dialogResults == '2') {
                    document.getElementById('txtModificato').value = '1';
                }
            }
        }
        else {
            alert('Selezionare un elemento dalla lista!')
            return false;
        }
    }
    /* FINE SCRIPT TAB REDDITO */



    /* FUNZIONI PULSANTI */


    function CalcoloCanone() {

        if (document.getElementById('cmbStato').value == '1') {
            if (document.getElementById('fl_elabora').value == '1') {
                window.open('../../ASS/CalcolaCanone.aspx?Tipo=3&P=' + document.getElementById('fl_proprieta').value + '&FFOO=' + <%=tipoFFOO %> +'&ID=' + document.getElementById('codUI').value + '&IdDich=' + <%=lIdDichiarazione %> + '&IdDomanda=' + <%=lIdDomanda %>, '', 'top=0,left=0,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes');
            }
            else {

                alert('Attenzione...La dichiarazione deve essere ELABORATA!');
            }
        }
        else {

            alert('Attenzione...Lo stato della dichiarazione deve essere COMPLETA!');
        }

    }



    function Verifica() {

        SceltaFunzioneOP1('Sicuri di voler eliminare?');

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




    /* FINE FUNZIONI PULSANTI */



    // ********** DOCUMENTI PER AMPLIAMENTO **********

    //##################### STAMPA RICEZIONE RICHIESTA #####################
    function RicRichiesta() {
        document.getElementById('H1').value = '0';
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=RicRichiesta', '');
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
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=DocMancanteAMPL', '', '');
            }

        return true;
    }


    //**** Autocertificazione nucleo di famiglia ****
    function AUcertStFamiglia() {
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=StFamigliaAMPL', '');
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }
    }


    //**** Convivenza More Uxorio ****
    function MoreUxorio() {
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=MoreUxorioAMPL', '');
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }
    }


    //**** Convivenza Assistenza ****
    function Assistenza() {
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=AssistenzaAMPL', '');
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }


    }


    //**** Avvio Procedimento ****
    function AvvProcedim() {
        if (document.getElementById('dic_Documenti1_documAlleg').value == 1) {
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
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=AvvioProcAMPL', '', '');
            }

        return true;
    }

    // **** Permanenza requisiti ERP (titolare) ****
    function PermReqERP() {
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=PermanenzaAMPL1', '');
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }


    }

    // **** Permanenza requisiti ERP (nuovo componente) ****
    function PermReqERP2() {
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=PermanenzaAMPL2', '');
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }


    }


    //**** Sopralluogo ****
    function SopralluogoAMPL() {
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=SoprallAMPL', '');
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }
    }

    //**** Sopralluogo ****
    function SopralluogoRID() {
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=SoprallRID', '');
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
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=ComSopralAMPL', '', '');
            }

        return true;
    }
    // *************** FINE DOCUMENTI PER AMPLIAMENTO


    // *************** DOCUMENTI PER REVISIONE CANONE

    //##################### STAMPA RICEZIONE RICHIESTA #####################
    function RicezRichiesta() {
        document.getElementById('H1').value = '0';
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=RichRC', '');
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
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=DocMancRC', '', '');
            }

        return true;
    }
    //#####################FINE STAMPA DOCUMENTO MANCANTE #####################
    //*************************************************************************************************************************************************
    //#####################STAMPA AVVIO PROCEDIMENTO #####################
    function AvvioProc() {
        if (document.getElementById('dic_Documenti1_documAlleg').value == 1) {
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
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=AvvProcRC', '', '');
            }

        return true;
    }
    //#####################FINE STAMPA PROCEDIMENTO #####################
    //***********************************************************************************************************************************************
    //#####################STAMPA AUTOCERTIFICAZIONE #####################

    function AutoCert() {
        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                document.getElementById('codUI').value +
                '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=AutoCertRC', '');
        }
        else {
            alert('Salvare le modifiche prima di procedere!');
        }
    }
    //#####################FINE STAMPA AUTOCERTIFICAZIONE #####################


    // *************** DOCUMENTI PER VOLTURA *******************
    //***** Modulo richiesta Voltura ******
    function RichVoltura() {

        if (document.getElementById('txtModificato').value != 1) {
            window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=RicezRicVOL', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }

        }


        //***** Modulo richiesta Voltura *****
        function AvvProcVoltura() {
            if (document.getElementById('dic_Documenti1_documAlleg').value == 1) {

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
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=AvvProcedVOL', '', '');
                }

            return true;
        }


        //***** Sopralluogo *****
        function ModSoprall() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=ModuloSoprallVOL', '');
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
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=SoprUtenteVOL', '', '');
                }

            return true;
        }




        // ********* DOCUMENTI PER SUBENTRO ***********
        //#####################STAMPA AVVIO PROCEDIMENTO #####################
        function AvvioProcSUB() {
            if (document.getElementById('dic_Documenti1_documAlleg').value == 1) {
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
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=AvvioProcSUB', '', '');
                }

            return true;
        }
        //#####################FINE STAMPA PROCEDIMENTO #####################



        //**** Domanda di subentro ****
        function DomSubentro() {

            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=DomandaSUB', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
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
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=PermReqRSUB', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //**** Sopralluogo ****
        function Sopralluogo() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=SoprallSUB', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //**** Comunicazione Doc. Mancante ****
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
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=DocMancanteSUB', '', '');
                }

            return true;
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
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=ComSopralSUB', '', '');
                }

            return true;
        }


        // ************ DOMANDA DI OSPITALITA' **************

        //***** Modulo richiesta Ospitalità ******
        function RichOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=RichOSP', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

        //***** Modulo richiesta Ospitalità Badanti ******
        function RichOSPbada() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=RichOSPbada', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

        //***** Modulo richiesta Ospitalità Autorizz.scolastiche ******
        function RichOSPscol() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=RichOSPscol', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }


        //***** Autocertificazione stato di famiglia Ospitalità ******
        function StFamigliaOSP() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=StFamigliaOSP', '');
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
                    window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=DocMancanteOSP', '', '');
             }

         return true;
     }


     //**** Avvio Procedimento ****
     function AvvioProcOSP() {
         if (document.getElementById('dic_Documenti1_documAlleg').value == 1) {

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
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=AvvioProcOSP', '', '');
             }

         return true;
     }


     //***** Sopralluogo *****
     function SopralluogoOSP() {
         if (document.getElementById('txtModificato').value != 1) {
             window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('codUI').value +
                 '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=SopralluogoOSP', '');
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
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=ComSoprallOSP', '', '');
             }

         return true;
     }


     // ************* DOMANDA DI CAMBIO CONSENSUALE ***********

     //**** Ricezione Richiesta ****
     function RichCAMB() {
         if (document.getElementById('txtModificato').value != 1) {
             window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('codUI').value +
                 '&NUMCONT=' + document.getElementById('id_contr').value + '&NUMCONT2=' + document.getElementById('codcontr2').value + '&TIPO=RichCAMB', '');
         }
         else {
             alert('Salvare le modifiche prima di procedere!');
         }
     }

     //**** Dichiarazione Perm. Requisiti ****
     function DichPermanenzaReq() {
         if (document.getElementById('txtModificato').value != 1) {
             window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('codUI').value +
                 '&NUMCONT=' + document.getElementById('id_contr').value + '&NUMCONT2=' + document.getElementById('codcontr2').value + '&TIPO=DichPermanenza', '');
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
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&NUMCONT2=' + document.getElementById('codcontr2').value + '&TIPO=DocMancCAMB', '', '');
             }

         return true;
     }


     //**** Avvio Procedimento ****
     function AvvioProcCAMB() {
         if (document.getElementById('dic_Documenti1_documAlleg').value == 1) {

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
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&NUMCONT2=' + document.getElementById('codcontr2').value + '&TIPO=AvvProcedCAMB', '', '');
             }

         return true;
     }

     //***** Sopralluogo *****
     function SopralluogoCAMB() {
         if (document.getElementById('txtModificato').value != 1) {
             window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                 document.getElementById('codUI').value +
                 '&NUMCONT=' + document.getElementById('id_contr').value + '&NUMCONT2=' + document.getElementById('codcontr2').value + '&TIPO=SoprallCAMB', '');
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
                 window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value + '&PROT=' + f.txtprot + '&NUMCONT=' + document.getElementById('id_contr').value + '&NUMCONT2=' + document.getElementById('codcontr2').value + '&TIPO=ComSoprallCAMB', '', '');
                }

            return true;
        }
         function Frontespizio() {
            if (document.getElementById('txtModificato').value != 1) {
                window.open('../StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                    document.getElementById('codUI').value +
                    '&NUMCONT=' + document.getElementById('id_contr').value + '&TIPO=Frontespizio', '');
            }
            else {
                alert('Salvare le modifiche prima di procedere!');
            }
        }

</script>
</html>
