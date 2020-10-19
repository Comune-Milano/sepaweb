<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CicloPassivoHome.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivoHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ciclo Passivo</title>
    <script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <script src="../funzioni.js" type="text/javascript"></script>
    <style type="text/css">
        .RadWindow_Web20 .rwCorner .rwTopLeft, .RadWindow_Web20 .rwTitlebar, .RadWindow_Web20 .rwCorner .rwTopRight, .RadWindow_Web20 .rwIcon, .RadWindow_Web20 table .rwTopLeft, .RadWindow_Web20 table .rwTopRight, .RadWindow_Web20 table .rwFooterLeft, .RadWindow_Web20 table .rwFooterRight, .RadWindow_Web20 table .rwFooterCenter, .RadWindow_Web20 table .rwBodyLeft, .RadWindow_Web20 table .rwBodyRight, .RadWindow_Web20 table .rwTitlebar, .RadWindow_Web20 table .rwTopResize, .RadWindow_Web20 table .rwStatusbar, .RadWindow_Web20 table .rwStatusbar .rwLoading {
            display: none !important;
        }

        .loading {
            background-color: #e7edf7;
            height: 100%;
            width: 100%;
        }
    </style>
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" type="text/javascript">
            var currentLoadingPanel = null;
            var currentUpdatedControl = null;
            function loading(tipo) {
                currentLoadingPanel = $find("<%= RadAjaxLoadingPanel1.ClientID %>");
                currentUpdatedControl = "<%= divContenuto.ClientID %>";
                if (tipo == 1) {
                    /*Mostra caricamento in corso RadWindow*/
                    if (currentLoadingPanel && currentUpdatedControl) {
                        currentLoadingPanel.show(currentUpdatedControl);
                    };
                } else {
                    /*Nasconde caricamento in corso RadWindow*/
                    if (currentLoadingPanel && currentUpdatedControl) {
                        currentLoadingPanel.hide(currentUpdatedControl);
                    };
                };
            };
            function Apri(Page) {
                loading(1);
                var oWnd = $find('RadWindow1');
                oWnd.setUrl(Page);
            };
            function Chiudi(sender, args) {
                loading(0);
            };
        </script>
    </telerik:RadCodeBlock>
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0"
            MinDisplayTime="100" Width="100%" Height="100%">
            <div class="loading">
                <asp:Image ID="Image1" runat="server" ImageUrl="../Images/Telerik/loading.gif" AlternateText="loading" Style="position: absolute; top: 50%; left: 50%; margin-left: -25px; margin-top: -25px;"></asp:Image>
            </div>
        </telerik:RadAjaxLoadingPanel>
        <asp:Panel runat="server" ID="divTop">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 33%; vertical-align: top;" rowspan="2">
                        <img alt="Logo Gestore" src="../immagini/LogoComuneC.gif" style="position: relative; top: -3px; left: 0px; height: 58px; width: 53px;" />
                    </td>
                    <td style="width: 34%;">
                        <center>
                            <table>
                                <tr>
                                    <td class="TitoloModulo">
                                        <center>
                                            CICLO PASSIVO</center>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </td>
                    <td style="width: 33%; text-align: right;" rowspan="2">
                        <table align="right">
                            <tr>
                                <td style="width: 60px;" class="TitoloOperatore">Utente:
                                </td>
                                <td style="width: 10px;">&nbsp;
                                </td>
                                <td class="TitoloNomeOperatore" nowrap="nowrap">
                                    <asp:Label ID="lblOperatore" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TitoloOperatore" nowrap="nowrap" style="vertical-align: top;">Filiale:
                                </td>
                                <td>&nbsp;
                                </td>
                                <td class="TitoloNomeOperatore" style="vertical-align: top;" nowrap="nowrap">
                                    <asp:Label ID="lblFiliale" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="divMenu" runat="server" class="menu">
            <telerik:RadMenu ID="NavigationMenu" runat="server" ResolvedRenderMode="Classic" CssClass="nmenu">
                <Items>
                    <telerik:RadMenuItem runat="server" Text="Dashboard" Value="Dashboard"
                                NavigateUrl="javascript:CicloPassivo('Dashboard');" Owner="">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True" PostBack="False" Text="i15" Value ="SepDashboardFornitori">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Fornitori" Value="Fornitori" PostBack="False">
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="Ricerca" Value="RicFornitori"
                                NavigateUrl="javascript:CicloPassivo('RicFornitori');" Owner="">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True" Value="SepFornitoriContratti">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Contratti" PostBack="False" Value="APPALTI">
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="Inserimento" Value='InserimentoAppalti'
                                NavigateUrl="javascript:CicloPassivo('InserimentoAppalti');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ricerca" Value="RicAppalti"
                                NavigateUrl="javascript:CicloPassivo('RicAppalti');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ritenute di Legge" Value="RitLegge"
                                NavigateUrl="javascript:CicloPassivo('RitLegge');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Situazione contabile" Value="ReportContratti"
                                NavigateUrl="javascript:CicloPassivo('ReportContratti');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Gestione prezzi unitari" Value="GestionePU"
                                NavigateUrl="javascript:CicloPassivo('GestionePU');">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True" Value="SepContrattiOrdini">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Ordini" Value="OrdC" PostBack="False">
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="Manutenzioni e Servizi" Value="Manutenzioni"
                                PostBack="False" Owner="">
                                <Items>
                                    <telerik:RadMenuItem runat="server" Text="Inserimento"
                                        Value="InserimentoManutenzioneEdfifici"
                                        NavigateUrl="javascript:CicloPassivo('InserimentoManutenzioneEdfifici');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Ins. fuori lotto"
                                        Value="InserimentoManutenzioneEdfificiFuoriLotto"
                                        NavigateUrl="javascript:CicloPassivo('InserimentoManutenzioneEdfificiFuoriLotto');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Ricerca" Owner="">
                                        <Items>
                                            <telerik:RadMenuItem runat="server" Text="Selettiva" Value="RicSelettiva"
                                                NavigateUrl="javascript:CicloPassivo('RicSelettiva');" Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Diretta" Value="RicDiretta"
                                                NavigateUrl="javascript:CicloPassivo('RicDiretta');" Owner="">
                                            </telerik:RadMenuItem>
                                        </Items>
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Consuntivazione"
                                        Value="Consuntivazione" Owner="">
                                        <Items>
                                            <telerik:RadMenuItem runat="server" Text="Selettiva" Value="ConsSelettiva"
                                                NavigateUrl="javascript:CicloPassivo('ConsSelettiva');" Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Diretta" Value="ConsDiretta"
                                                NavigateUrl="javascript:CicloPassivo('ConsDiretta');" Owner="">
                                            </telerik:RadMenuItem>
                                        </Items>
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="SAL" Owner="">
                                        <Items>
                                            <telerik:RadMenuItem runat="server" Text="Nuovo" Value="NuovoSal"
                                                NavigateUrl="javascript:CicloPassivo('NuovoSal');" Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Ricerca" Value="RicSal"
                                                NavigateUrl="javascript:CicloPassivo('RicSal');" Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Stampa Pagamenti" Value="StampaPagamenti"
                                                NavigateUrl="javascript:CicloPassivo('StampaPagamenti');" Owner="">
                                            </telerik:RadMenuItem>
                                        </Items>
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Segnalazione" Value="Segnalazione"
                                        Owner="">
                                        <Items>
                                            <telerik:RadMenuItem runat="server" Text="Con Odl" Value="RicSegnalazione"
                                                NavigateUrl="javascript:if((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)){alert('Utente non abilitato oppure non ha la struttura assegnata!');}else{Apri('CicloPassivo/MANUTENZIONI/RicercaSegnalazioni.aspx');}"
                                                Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" NavigateUrl="javascript:if((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)){alert('Utente non abilitato oppure non ha la struttura assegnata!');}else{Apri('CicloPassivo/MANUTENZIONI/RicercaSegnalazioniNOEmesso.aspx');}" Text="Senza Odl">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Chiusura massiva" Value="ChiusuraSegnalazioni"
                                                NavigateUrl="javascript:CicloPassivo('ChiusuraSegnalazioni');" Owner="">
                                            </telerik:RadMenuItem>
                                        </Items>
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="STR" Value="STR">
                                        <Items>
                                            <telerik:RadMenuItem runat="server" Text="Export STR" Value="ExportSTR" NavigateUrl="javascript:CicloPassivo('ExportSTR');">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Patrimonio" Value="Patrimonio" NavigateUrl="javascript:CicloPassivo('Patrimonio');">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Import" Value="Import" NavigateUrl="javascript:CicloPassivo('Import');">
                                            </telerik:RadMenuItem>
                                        </Items>
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Pagamenti a Canone" Value="Pagam_canone"
                                PostBack="False" Owner="">
                                <Items>
                                    <telerik:RadMenuItem runat="server" Text="Da Approvare" Value="Pagam_Da_Approvare"
                                        NavigateUrl="javascript:CicloPassivo('Pagam_Da_Approvare');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Approvati"
                                        NavigateUrl="javascript:CicloPassivo('PagamApprovati');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Emesso SAL" Value="StampaPagCanone"
                                        NavigateUrl="javascript:CicloPassivo('PagamEmessoSal');" Owner="">
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ordini e Pagamenti" Value="Ordini_Pagamenti"
                                PostBack="False" Owner="">
                                <Items>
                                    <telerik:RadMenuItem runat="server" Text="Inserimento" Value="Inserimento_Pagamenti"
                                        NavigateUrl="javascript:CicloPassivo('Inserimento_Pagamenti');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Ricerca" Owner="">
                                        <Items>
                                            <telerik:RadMenuItem runat="server" Text="Selettiva" Value="RicPagSelettiva"
                                                NavigateUrl="javascript:CicloPassivo('RicPagSelettiva');" Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Diretta" Value="RicPagDiretta"
                                                NavigateUrl="javascript:CicloPassivo('RicPagDiretta')" Owner="">
                                            </telerik:RadMenuItem>
                                        </Items>
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>

                            <telerik:RadMenuItem runat="server" Text="Gestione Non Patrimoniale" Value="RRS"
                                PostBack="False" Owner="">
                                <Items>
                                    <telerik:RadMenuItem runat="server" Text="Inserimento" Value="InserimentoRRS"
                                        NavigateUrl="javascript:CicloPassivo('InserimentoRRS');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Ricerca" Owner="">
                                        <Items>
                                            <telerik:RadMenuItem runat="server" Text="Selettiva" Value="RicRRS_Sel"
                                                NavigateUrl="javascript:CicloPassivo('RicRRS_Sel');" Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Diretta" Value="RicRRS_Dir"
                                                NavigateUrl="javascript:CicloPassivo('RicRRS_Dir');" Owner="">
                                            </telerik:RadMenuItem>
                                        </Items>
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Consuntivazione"
                                        Value="ConsuntivazioneRRS" Owner="">
                                        <Items>
                                            <telerik:RadMenuItem runat="server" Text="Selettiva" Value="ConsSelettivaRRS"
                                                NavigateUrl="javascript:CicloPassivo('ConsSelettivaRRS');" Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Diretta" Value="ConsDirettaRRS"
                                                NavigateUrl="javascript:CicloPassivo('ConsDirettaRRS');" Owner="">
                                            </telerik:RadMenuItem>
                                        </Items>
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="SAL" Owner="">
                                        <Items>
                                            <telerik:RadMenuItem runat="server" Text="Nuovo" Value="NuovoSAL_RRS"
                                                NavigateUrl="javascript:CicloPassivo('NuovoSAL_RRS');" Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Ricerca" Value="RicSAL_RRS"
                                                NavigateUrl="javascript:CicloPassivo('RicSAL_RRS');" Owner="">
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem runat="server" Text="Stampa Pagamenti" Value="StampaPag_RRS"
                                                NavigateUrl="javascript:CicloPassivo('StampaPag_RRS');" Owner="">
                                            </telerik:RadMenuItem>
                                        </Items>
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True" Value="SepOrdiniUtenze">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Utenze" Value="MenuUtenze"
                        PostBack="False">
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="Caricamento File" Value="UtenzeCaricamento"
                                NavigateUrl="javascript:CicloPassivo('UtenzeCaricamento');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ricerca Fatture" Value="UtenzeRicerca"
                                NavigateUrl="javascript:CicloPassivo('UtenzeRicerca');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ricerca CDP Emessi" Value="RicCDP_Emessi"
                                NavigateUrl="javascript:CicloPassivo('RicCDP_Emessi');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Fatture con CDP" Value="Fatt_CDP"
                                NavigateUrl="javascript:CicloPassivo('Fatt_CDP');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Elenco POD" Value="ElencoPOD"
                                NavigateUrl="javascript:CicloPassivo('ElencoPOD');" Owner="">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True" Value="SepUtenzeMulte">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Custodi" Value="MenuCustodi"
                        PostBack="False">
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="Caricamento File" Value="CustodiCaricamento"
                                NavigateUrl="javascript:CicloPassivo('CustodiCaricamento');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ricerca" Value="CustodiRicerca"
                                NavigateUrl="javascript:CicloPassivo('CustodiRicerca');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ricerca CDP Emessi" Value="CustRicCDP_Emessa"
                                NavigateUrl="javascript:CicloPassivo('CustRicCDP_Emessa');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Custodi con CDP" Value="Cust_CDP"
                                NavigateUrl="javascript:CicloPassivo('Cust_CDP');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Anagrafica" Value="AnaCust"
                                NavigateUrl="javascript:CicloPassivo('AnaCust');" Owner="">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True" Value="SepCustodiMulte">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Multe" Value="Multe" PostBack="False">
                        <Items>
                            <telerik:RadMenuItem runat="server" Value="MulteCaricamento" Text="Caricamento File"
                                NavigateUrl="javascript:CicloPassivo('MulteCaricamento');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ricerca" Value="MulteRicerca"
                                NavigateUrl="javascript:CicloPassivo('MulteRicerca');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ricerca CDP Emessi" Value="RicMulteCDP"
                                NavigateUrl="javascript:CicloPassivo('RicMulteCDP');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Multe con CDP" Value="MulteCDP"
                                NavigateUrl="javascript:CicloPassivo('MulteCDP');" Owner="">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True" Value="SepMulteCosap">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Cosap" Value="Cosap" PostBack="False">
                        <Items>
                            <telerik:RadMenuItem runat="server" Value="CosapCaricamento" Text="Caricamento File"
                                NavigateUrl="javascript:CicloPassivo('CosapCaricamento');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ricerca" Value="CosapRicerca"
                                NavigateUrl="javascript:CicloPassivo('CosapRicerca');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Ricerca cosap Emessi" Value="RicCosapCDP"
                                NavigateUrl="javascript:CicloPassivo('RicCosapCDP');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Cosap con CDP" Value="CosapCDP"
                                NavigateUrl="javascript:CicloPassivo('CosapCDP');" Owner=""></telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Report" Value="ReportC"
                        PostBack="False">
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="Situazione Contabile" Value="SContabile"
                                PostBack="False" Owner="">
                                <Items>
                                    <telerik:RadMenuItem runat="server" Text="Generale" Value="ReportGenerale"
                                        NavigateUrl="javascript:CicloPassivo('ReportGenerale');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Per Struttura" Value="ReportStruttura"
                                        NavigateUrl="javascript:CicloPassivo('ReportStruttura');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Pagamenti" Value="ReportPagamenti"
                                        NavigateUrl="javascript:CicloPassivo('ReportPagamenti');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="ODL" Value="PagamentiODL"
                                        NavigateUrl="javascript:CicloPassivo('PagamentiODL');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Servizi" Value="RicercaPerServizi"
                                        NavigateUrl="javascript:CicloPassivo('RicercaPerServizi');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Contabilità" Value="ReportContab"
                                        NavigateUrl="javascript:CicloPassivo('ReportContab');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Contabilità Dettagliata" Value="ReportContabDett"
                                        NavigateUrl="javascript:CicloPassivo('ReportContabDett');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Per Esercizio" Value="ReportEsercizio"
                                        NavigateUrl="javascript:CicloPassivo('ReportEsercizio');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Sintesi" Value="ReportSintesi"
                                        NavigateUrl="javascript:CicloPassivo('ReportSintesi');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Completo" Value="ReportCompleto"
                                        NavigateUrl="javascript:CicloPassivo('ReportCompleto');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Estrazione Pagamenti" Value="ReportEstrPag"
                                        NavigateUrl="javascript:CicloPassivo('ReportEstrPag');" Owner="">
                                    </telerik:RadMenuItem>

                                    <telerik:RadMenuItem runat="server" Text="ODL/SAL" Value="ODLSAL"
                                        NavigateUrl="javascript:CicloPassivo('OdlSal');" Owner="">
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Residui" Value="Residui"
                                PostBack="False" Owner="">
                                <Items>
                                    <telerik:RadMenuItem runat="server" Text="Uscite Conto Capitale" Value="ReportResidui"
                                        NavigateUrl="javascript:CicloPassivo('ReportResidui');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Uscite Correnti" Value="ReportUsciteCor"
                                        NavigateUrl="javascript:CicloPassivo('ReportUsciteCor');" Owner="">
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Report Pagamenti" Value="Report_Pagamenti"
                                PostBack="False"
                                NavigateUrl="javascript:CicloPassivo('Report_Pagamenti');" Owner="">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Gestione" Value="GestioneC"
                        PostBack="False">
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="Gestione Lotti" Value="GESTIONE_LOTTI"
                                PostBack="False" Owner="">
                                <Items>
                                    <telerik:RadMenuItem runat="server" Text="Nuovo Lotto Edifici" Value="Nuovo_lotto_E"
                                        NavigateUrl="javascript:CicloPassivo('Nuovo_Lotto_E');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Nuovo Lotto Impianti" Value="Nuovo_lotto_I"
                                        NavigateUrl="javascript:CicloPassivo('Nuovo_Lotto_I');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Ricerca" Value="Ricerca_Lotti"
                                        NavigateUrl="javascript:CicloPassivo('Ricerca_Lotti');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="Guida" Value="GuidaLotti"
                                        NavigateUrl="javascript:CicloPassivo('GuidaLotti');" Owner="">
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Parametri" Value="Parametri"
                                PostBack="False" Owner="">
                                <Items>
                                    <telerik:RadMenuItem runat="server" Text="Modalità di Pagamento" Value="ParametriPag"
                                        NavigateUrl="javascript:CicloPassivo('ParametriPag');" Owner="">
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem runat="server" Text="CDP Tracciati" Value="CDP_Tracciati"
                                        NavigateUrl="javascript:CicloPassivo('CDP_Tracciati');" Owner="">
                                    </telerik:RadMenuItem>
                                    <%--<telerik:RadMenuItem runat="server" Text="Gestione Crediti" Value="Gest_Crediti"
                                        NavigateUrl="javascript:CicloPassivo('Gest_Crediti');" Owner="">
                                    </telerik:RadMenuItem>--%>
                                </Items>
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Building Manager" Value="B_Manager" PostBack="False"
                                NavigateUrl="javascript:CicloPassivo('B_Manager');" Owner="">
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem runat="server" Text="Upload Firme" Value="UploadFirma" PostBack="False"
                                NavigateUrl="javascript:CicloPassivo('UploadFirma');" Owner="">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True" Value="SepGestioneCambioIva">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Cambio IVA" Value="CambioIVA" PostBack="False"
                        NavigateUrl="javascript:CicloPassivo('CambioIVA');" Owner="">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" IsSeparator="True">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem runat="server" Text="Home" NavigateUrl="javascript:CicloPassivo('Home');">
                    </telerik:RadMenuItem>
                </Items>
            </telerik:RadMenu>
        </asp:Panel>
        <asp:Panel ID="divContenuto" class="contenuto" runat="server">
            <telerik:RadWindow ID="RadWindow1" runat="server" VisibleOnPageLoad="true" VisibleStatusbar="false"
                VisibleTitlebar="false" CenterIfModal="true" InitialBehaviors="Maximize" RestrictionZoneID="divContenuto"
                OnClientPageLoad="Chiudi" ShowOnTopWhenMaximized="false">
            </telerik:RadWindow>
        </asp:Panel>
        <asp:HiddenField ID="Bp_Formalizzazione" runat="server" Value="" />
        <asp:HiddenField ID="HFBpCompilazione" runat="server" Value="" />
        <asp:HiddenField ID="HFBpConvAler" runat="server" Value="" />
        <asp:HiddenField ID="HFBpConvComune" runat="server" Value="" />
        <asp:HiddenField ID="HFBpGestCapitoli" runat="server" Value="" />
        <asp:HiddenField ID="HFVociServizi" runat="server" Value="" />
        <asp:HiddenField ID="HFBpGenerale" runat="server" Value="" />
        <asp:HiddenField ID="HFIdStruttura" runat="server" Value="" />
        <asp:HiddenField ID="HFParamCP" runat="server" Value="" />
        <asp:HiddenField ID="HFModBuildingManager" runat="server" Value="" />
        <asp:HiddenField ID="HFBpMS" runat="server" Value="" />
        <asp:HiddenField ID="HFSTR" runat="server" Value="" />
        <asp:HiddenField ID="optMenu" runat="server" Value="0" />
    </form>
    <script type="text/javascript">
            window.onresize = setDimensioni;
            Sys.Application.add_load(setDimensioni);
            function setDimensioni() {
                $("#divContenuto").height($(window).innerHeight() - 40);
                $("#divContenuto").width($(window).innerWidth() - 3);
                $("#divMenu").height(30);
                $("#divMenu").width($(window).innerWidth() - 3);
                GetRadWindow();
            };
    </script>
</body>
</html>
