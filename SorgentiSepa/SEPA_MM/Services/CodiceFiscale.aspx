<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CodiceFiscale.aspx.vb" Inherits="SERVICES_CodiceFiscale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>Sepa@Web - Codice Fiscale</title>
    <link rel="icon" href="../favicon.ico" type="image/x-icon" />
    <link href="../StandardTelerik/Style/Site.css?v=<%=version %>" rel="stylesheet" type="text/css" />
    <script src="../StandardTelerik/Scripts/jsFunzioni.js?v=<%=version %>" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/modalTelerik.js?v=<%=version %>" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsMessage.js?v=<%=version %>" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsAutoComplete.js?v=<%=version %>" type="text/javascript"></script>
    <script src="../SERVICES/js/jsFunzioni.js?v=<%=version %>" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsRedirectLoading.js?v=<%=version %>" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad(sender, eventArgs) {
            initialize();
        };
        function loadingMenu() {
            var currentLoadingPanel = $find("<%= RadAjaxLoadingPanel1.ClientID%>");
            var currentUpdatedControl = "<%= RestrictionZoneID.ClientID %>";
            currentLoadingPanel.show(currentUpdatedControl);
        };
        function closeloadingMenu() {
            var currentLoadingPanel = $find("<%= RadAjaxLoadingPanel1.ClientID%>");
            var currentUpdatedControl = "<%= RestrictionZoneID.ClientID %>";
            currentLoadingPanel.hide(currentUpdatedControl);
        };
        function CaricaDatiMaschera() {
            
            document.getElementById('HFControlImposta').value = GetRadWindow().BrowserWindow.ReturnValori(1);
            document.getElementById('txtCognome').value = GetRadWindow().BrowserWindow.ReturnValori(2);
            document.getElementById('HFControlCognome').value = GetRadWindow().BrowserWindow.ReturnValori(3);
            document.getElementById('txtNome').value = GetRadWindow().BrowserWindow.ReturnValori(4);
            document.getElementById('HFControlNome').value = GetRadWindow().BrowserWindow.ReturnValori(5);
            var sesso = GetRadWindow().BrowserWindow.ReturnValori(6);
            var comboSesso = $find("<%= ddlSesso.ClientID %>");
            setComboTelerikJS(comboSesso, sesso);
            document.getElementById('HFControlSesso').value = GetRadWindow().BrowserWindow.ReturnValori(7);
            var Nazione = GetRadWindow().BrowserWindow.ReturnValori(8);
            var InseritaNazione = false;
            if (Nazione != 'ITALIA' && Nazione != '-1') {
                document.getElementById('HFCodComune').value = Nazione;
                var autoCompleteBox = $find("<%= acbComune.ClientID %>");
                var NazioneText = GetRadWindow().BrowserWindow.ReturnValori(15);
                setAutoCompleteTelerikJS(autoCompleteBox, NazioneText)
                InseritaNazione = true;
            };
            document.getElementById('HFControlLuogo1').value = GetRadWindow().BrowserWindow.ReturnValori(9);
            if (InseritaNazione == false) {
                var Comune = GetRadWindow().BrowserWindow.ReturnValori(10);
                if (Comune != '-1') {
                    document.getElementById('HFCodComune').value = Comune;
                    var autoCompleteBox = $find("<%= acbComune.ClientID %>");
                    var ComuneText = GetRadWindow().BrowserWindow.ReturnValori(16);
                    setAutoCompleteTelerikJS(autoCompleteBox, ComuneText)
                    TrovaProvincia(ComuneText);
                };
            };
            document.getElementById('HFControlLuogo2').value = GetRadWindow().BrowserWindow.ReturnValori(11);
            var dataNascitaCtrl = $find("<%= txtDataNascita.ClientID %>");
            var dataNascita = GetRadWindow().BrowserWindow.ReturnValori(12);
            if (dataNascita != '') {
                setDataTelerikJS(dataNascitaCtrl, GetRadWindow().BrowserWindow.ReturnValori(12));
            };
            document.getElementById('HFControlDataNascita').value = GetRadWindow().BrowserWindow.ReturnValori(13);
            document.getElementById('HFControlNN').value = GetRadWindow().BrowserWindow.ReturnValori(14);
            document.getElementById('HFControlCC').value = GetRadWindow().BrowserWindow.ReturnValori(17);
            if (GetRadWindow().BrowserWindow.ReturnValori(18) != undefined) {
                document.getElementById('HFControlPROV').value = GetRadWindow().BrowserWindow.ReturnValori(18);
            };
        };
        function ImpostaCF() {
            var oggetto = document.getElementById('HFControlImposta').value;
            var CF = document.getElementById('txtCodiceFiscale').value;
            if (CF != '') {
                //AGGIORNA COGNOME
                var oggettoCognome = document.getElementById('HFControlCognome').value;
                if (GetRadWindow().BrowserWindow.document.getElementById(oggettoCognome)) {
                    GetRadWindow().BrowserWindow.document.getElementById(oggettoCognome).value = document.getElementById('txtCognome').value;
                };
                //AGGIORNA COGNOME
                //AGGIORNA NOME
                var oggettoNome = document.getElementById('HFControlNome').value;
                if (GetRadWindow().BrowserWindow.document.getElementById(oggettoNome)) {
                    GetRadWindow().BrowserWindow.document.getElementById(oggettoNome).value = document.getElementById('txtNome').value;
                };
                //AGGIORNA NOME
                //AGGIORNA SESSO
                var oggettoSesso = document.getElementById('HFControlSesso').value;
                if (GetRadWindow().BrowserWindow.document.getElementById(oggettoSesso)) {

                    GetRadWindow().BrowserWindow.changeContent(oggettoSesso, document.getElementById('ddlSesso').control._value);
                };
                //AGGIORNA SESSO
                //AGGIORNA DATA NASCITA
                var oggettoDataNascita = document.getElementById('HFControlDataNascita').value;
                if (GetRadWindow().BrowserWindow.document.getElementById(oggettoDataNascita)) {
                    GetRadWindow().BrowserWindow.SetDataNascita(oggettoDataNascita, document.getElementById('txtDataNascita').value);
                };
                //AGGIORNA DATA NASCITA
                //AGGIORNA LUOGO NASCITA
                var oggettoLuogo1 = document.getElementById('HFControlLuogo1').value;
                var oggettoLuogo2 = document.getElementById('HFControlLuogo2').value;
                var oggettoNN = document.getElementById('HFControlNN').value;
                var sigla = document.getElementById('txtProvincia').value;
                var lensigla = sigla.length;
                if (lensigla == 1) {
                    GetRadWindow().BrowserWindow.SetNazioneNascita(oggettoLuogo1, document.getElementById('HFCodComune').value);
                    var oggettoCC = document.getElementById('HFControlCC').value;
                    if (GetRadWindow().BrowserWindow.document.getElementById(oggettoCC)) {
                        GetRadWindow().BrowserWindow.document.getElementById(oggettoCC).value = document.getElementById('HFCodComune').value
                    };
                    GetRadWindow().BrowserWindow.SetComuneNascita(oggettoLuogo2, '');
                    if (GetRadWindow().BrowserWindow.document.getElementById(oggettoNN)) {
                        GetRadWindow().BrowserWindow.document.getElementById(oggettoNN).style.visibility = 'hidden';
                        GetRadWindow().BrowserWindow.document.getElementById(oggettoNN).style.display = 'none';
                    };
                } else {
                    GetRadWindow().BrowserWindow.SetNazioneNascita(oggettoLuogo1, 'ITALIA');
                    var oggettoCC = document.getElementById('HFControlCC').value;
                    if (GetRadWindow().BrowserWindow.document.getElementById(oggettoCC)) {
                        GetRadWindow().BrowserWindow.document.getElementById(oggettoCC).value = document.getElementById('HFCodComune').value
                    };
                    GetRadWindow().BrowserWindow.SetComuneNascita(oggettoLuogo2, document.getElementById('acbComune').value);
                    if (GetRadWindow().BrowserWindow.document.getElementById(oggettoNN)) {
                        GetRadWindow().BrowserWindow.document.getElementById(oggettoNN).style.visibility = 'visible';
                        GetRadWindow().BrowserWindow.document.getElementById(oggettoNN).style.display = 'block';
                    };
                };
                //AGGIORNA LUOGO NASCITA
                //AGGIORNA PROVINCIA
                var oggettoPROV = document.getElementById('HFControlPROV').value;
                if (GetRadWindow().BrowserWindow.document.getElementById(oggettoPROV)) {
                    GetRadWindow().BrowserWindow.document.getElementById(oggettoPROV).value = document.getElementById('txtProvincia').value
                };
                //AGGIORNA PROVINCIA
                if (GetRadWindow().BrowserWindow.document.getElementById(oggetto)) {
                    GetRadWindow().BrowserWindow.document.getElementById(oggetto).value = CF;
                    NotificaTelerik('RadNotificationNote', MessaggioTitolo.Attenzione, Messaggio.Operazione_Eff);
                };
            } else {
                NotificaTelerik('RadNotificationNote', MessaggioTitolo.Attenzione, 'Nessun Codice Fiscale Calcolato!');
            };
        };
    </script>
</head>
<body style="background-image: url('../StandardTelerik/Immagini/CodiceFiscale/Maschera_CF.png');
    background-repeat: no-repeat;">
    <div id="divCaricamentoIniziale" style="margin: 0px; width: 100%; height: 100%; position: fixed;
        top: 0px; left: 0px; filter: alpha(opacity=100); opacity: 1; background-color: #e7edf7;
        z-index: 9999;">
        <div style="margin-left: -117px; margin-top: -48px; width: 234px; height: 97px; position: fixed;
            top: 50%; left: 50%; background-color: transparent;">
            <table style="width: 100%; height: 100%;">
                <tr>
                    <td valign="middle" align="center">
                        <img title="Caricamento in Corso..." alt="Caricamento in Corso..." src="../StandardTelerik/Immagini/loading.gif" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server" onsubmit="validNavigation=true;return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="720000">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="Buttons, CheckBoxes, RadioButtons">
        </telerik:RadFormDecorator>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RestrictionZoneID" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RestrictionZoneID">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RestrictionZoneID" LoadingPanelID="RadAjaxLoadingPanel1">
                        </telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Metro">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager runat="server" ID="RadWindowManagerMaster" VisibleStatusbar="False"
            RestrictionZoneID="RestrictionZoneID" Behavior="Minimize, Pin, Maximize, Move, Resize">
            <Localization Maximize="<%$ Resources:RadWindow, Maximize %>" Minimize="<%$ Resources:RadWindow, Minimize %>"
                Close="<%$ Resources:RadWindow, Close %>" PinOff="<%$ Resources:RadWindow, PinOff %>"
                PinOn="<%$ Resources:RadWindow, PinOn %>" Reload="<%$ Resources:RadWindow,Reload %>"
                Restore="<%$ Resources:RadWindow, Restore%>" Cancel="<%$ Resources:RadWindow, Cancel %>"
                OK="<%$ Resources:RadWindow, OK %>" No="<%$ Resources:RadWindow, No %>" Yes="<%$ Resources:RadWindow, Yes %>" />
        </telerik:RadWindowManager>
        <asp:Panel ID="RestrictionZoneID" runat="server" CssClass="demo-container size-narrow">
            <div style="height: 187px;">
                &nbsp;
            </div>
            <div style="height: 30px; vertical-align: middle;">
                <table>
                    <tr>
                        <td style="width: 100px;">&nbsp;</td>
                        <td>
                            <telerik:RadTextBox ID="txtCodiceFiscale" runat="server" Width="350px" MaxLength="16"
                                ReadOnly="true" Font-Bold="true">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 10px;">
                &nbsp;
            </div>
            <div style="height: 30px; vertical-align: middle;">
                <table>
                    <tr>
                        <td style="width: 100px;">&nbsp;</td>
                        <td>
                            <telerik:RadTextBox ID="txtCognome" runat="server" Width="350px" MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 2px;">
                &nbsp;
            </div>
            <div style="height: 30px; vertical-align: middle;">
                <table>
                    <tr>
                        <td style="width: 100px;">&nbsp;</td>
                        <td>
                            <telerik:RadTextBox ID="txtNome" runat="server" Width="350px" MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                        <td style="width: 95px;">&nbsp;</td>
                        <td>
                            <telerik:RadComboBox ID="ddlSesso" runat="server" Culture="it-IT"
                                EnableLoadOnDemand="true" Filter="None" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="50px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="M" Value="M" Selected="true" />
                                    <telerik:RadComboBoxItem Text="F" Value="F" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 35px; vertical-align: middle;">
                <table>
                    <tr>
                        <td style="width: 100px;">&nbsp;</td>
                        <td>
                            <telerik:RadAutoCompleteBox runat="server" ID="acbComune" OnClientRequesting="requestingComune"
                                InputType="Text" EmptyMessage="Seleziona il Comune..." Width="350px" ClientIDMode="Static"
                                TextSettings-SelectionMode="Single" OnClientTextChanged="OnClientEntryAddingHandlerComune"
                                DropDownHeight="150">
                                <WebServiceSettings Path="../SepacomAutoComplete.asmx" Method="GetListaComuni" />
                                <TextSettings SelectionMode="Single" />
                            </telerik:RadAutoCompleteBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 35px; vertical-align: middle;">
                <table>
                    <tr>
                        <td style="width: 100px;">&nbsp;</td>
                        <td>
                            <telerik:RadTextBox ID="txtProvincia" runat="server" Width="50px" MaxLength="2" ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td style="width: 215px;">&nbsp;</td>
                        <td>
                            <telerik:RadDatePicker ID="txtDataNascita" runat="server" WrapperTableCaption=""
                                MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                ShowPopupOnFocus="true">
                                <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                    <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                    <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                </DateInput>
                                <Calendar ID="Calendar9" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 30px;">
                <table style="width: 94%;">
                    <tr>
                        <td style="width: 15px;">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnReset" runat="server" Text="Reset" ToolTip="Reset Codice Fiscale" />
                        </td>
                        <td style="width: 5px;">&nbsp;</td>
                        <td style="width: 95%;">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnCalcola" runat="server" Text="Calcola" ToolTip="Calcola Codice Fiscale" />
                        </td>
                        <td style="width: 5px;">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnImposta" runat="server" Text="Copia" ToolTip="Copia Codice Fiscale"
                                OnClientClick="ImpostaCF();return false;" />
                        </td>
                        <td style="width: 5px;">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="CancelEdit();return false;" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:HiddenField ID="HFSepaTest" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="HFPathLock" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFPathExit" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFBlockExit" runat="server" Value="1" ClientIDMode="Static" />
        <asp:HiddenField ID="HFVerticalPosition" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="HFResizeColonneGridFit" runat="server" Value="1" ClientIDMode="Static" />
        <asp:HiddenField ID="HFGetProvinciaComune" runat="server" Value="1" ClientIDMode="Static" />
        <asp:HiddenField ID="PageID" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hiddenLockCorrenti" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlImposta" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlImpostaCheck" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlCognome" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlNome" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlSesso" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlLuogo1" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlLuogo2" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlDataNascita" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlNN" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlCC" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="HFControlPROV" runat="server" Value="" ClientIDMode="Static" />
        <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Com"
            Height="85px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
            AutoCloseDelay="3500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
        </telerik:RadNotification>
        <telerik:RadWindow ID="modalRadWindow" runat="server" CenterIfModal="true" Modal="True"
            VisibleStatusbar="False" ClientIDMode="Static" ShowContentDuringLoad="False"
            RestrictionZoneID="RestrictionZoneID">
        </telerik:RadWindow>
        <asp:HiddenField ID="HFCodComune" runat="server" ClientIDMode="Static" />
    </form>
    <script src="../StandardTelerik/Scripts/gestioneDimensioniPaginaTelerik.js?v=<%=version %>"
        type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsFunzioniLock.js?v=<%=version %>" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsfunzioniExit.js?v=<%=version %>" type="text/javascript"></script>
    <script type="text/javascript">
        function initialize() {
            if (document.getElementById('HFSepaTest').value != '0') {
                ParametroAmbiente();
            };
            validNavigation = false;
            if (document.getElementById('divCaricamentoIniziale')) {
                document.getElementById('divCaricamentoIniziale').style.visibility = 'hidden';
            };
            if (document.getElementById('HFControlImpostaCheck').value == '1') {
                CaricaDatiMaschera();
                document.getElementById('HFControlImpostaCheck').value = '0';
            };
        };
    </script>
</body>
</html>
