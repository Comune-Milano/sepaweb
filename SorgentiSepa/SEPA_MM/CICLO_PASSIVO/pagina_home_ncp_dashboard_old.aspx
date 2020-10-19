<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pagina_home_ncp_dashboard_old.aspx.vb" Inherits="CICLO_PASSIVO_pagina_home_ncp_dashboard_old" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../StandardTelerik/Scripts/jsFunzioni.js"></script>


    <script type="text/javascript">
        function RowSelecting(sender, args) {
            document.getElementById('idSelected').value = args.getDataKeyValue("ID");
            document.getElementById('statoSegnalazione').value = args.getDataKeyValue("STATO");
            document.getElementById('idStatoSegnalazione').value = args.getDataKeyValue("ID_STATO");
            //var button = $find('btnVisualizzaManutenzioni');
            //button.click();
        };
        function ModificaDblClick() {
            document.getElementById('btnVisualizzaSegnalazione').click();
        };
        function SalvaOrdine() {
            var sicuro = confirm('Visualizzare la manutenzione per emettere l\'ordine?');
            if (sicuro == true) {
                document.getElementById('txtannullo').value = '1';
            }
            else {
                document.getElementById('txtannullo').value = '0';
            }
        };

        function RowSelectingManu(sender, args) {
            document.getElementById('idSelectedManu').value = args.getDataKeyValue("ID");
            document.getElementById('idEsercizioFinanziario').value = args.getDataKeyValue("ID_ESERCIZIO_FINANZIARIO");
            document.getElementById('progrManutenzione').value = args.getDataKeyValue("PROGR");
            document.getElementById('annoManutenzione').value = args.getDataKeyValue("ANNO");
            document.getElementById('repAppalto').value = args.getDataKeyValue("REP_APPALTO");
            document.getElementById('idAppalto').value = args.getDataKeyValue("ID_APPALTO");
            document.getElementById('idFornitore').value = args.getDataKeyValue("ID_FORNITORE");
            document.getElementById('idServizio').value = args.getDataKeyValue("ID_SERVIZIO");
            document.getElementById('dataEmissione').value = args.getDataKeyValue("DATA_EMISSIONE");


        };
        function ModificaDblClickManu() {
            document.getElementById('btnVisualizzaManu').click();
        };

        function ChiudiSegnalazione(sender, args) {
            apriConfirm('Sei sicuro di voler chiudere la segnalazione corrente? N.B E\' possibile chiudere solo segnalazioni <strong>senza ordini associati</strong> e <strong>in corso</strong>!',
                function callbackFn(arg) { if (arg == true) { document.getElementById('btnChiudiSegn').click(); } }, 300, 150, 'Attenzione', null);

        };

    </script>
    <style type="text/css">
        .nascondi {
            display: none;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1440000">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnPrendiInCarico">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="dgvSegnalazioni">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="dgvODL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbEsercizio">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbServizio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="cmbServizioVoce" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="cmbAppalto" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbServizio">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbServizioVoce" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="cmbAppalto" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbServizioVoce">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbAppalto" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnVisualizzaManutenzioni">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnCaricaAppalti">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbRepertorio">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnChiudiSegn">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnBuildingManager">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="buildingManager" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />

                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnDL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="buildingManager" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />

                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnOrdiniBozza">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="btnODLEmessiNoSegn">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="idSelected" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLBozzaNonEmessi">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLEmessiNoCons">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLConsNoCdP">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnOrdiniEmessi">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnAperte">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnInCorso">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnEvase">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnChiuse">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnAllSegn">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnAperte30gg">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLEmessi">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbCriticita">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnTempoGestione">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Metro">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>

        <table style="width: 100%">
            <tr>
                <td colspan="2" style="text-align: center">
                    <img id="imgLogo" runat="server" alt="Logo Sepa" src="../Images/SepaWeb.png" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <asp:Panel ID="PanelKPI" runat="server">
                        <fieldset style="border-width: 2px" runat="server" id="fieldSet6">
                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label10" runat="server" Text="Area KPI" Font-Size="10pt" Font-Names="Arial"></asp:Label></strong></legend>
                            <table id="tblKPI" runat="server" style="width: 100%">
                                <tr>
                                    <td>
                                        <fieldset style="border-width: 2px" runat="server" id="fieldSetKPI">
                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label2" runat="server" Text="KPI 1" Font-Size="10pt" Font-Names="Arial"></asp:Label></strong></legend>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="KPI1" runat="server" Text="N° segnalazioni aperte:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;    
                                                      
                                           <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnAperte').click();">
                                               <asp:Label ID="lblNumSegnalazioniAperte" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                           </a>
                                                        &nbsp;
                                                        <asp:Image ID="imgSegnAperte" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                        <telerik:RadToolTip ID="tltSegnAperte" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                            Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgSegnAperte" Width="450px"
                                                            Height="60px" Animation="Slide" OffsetY="0">
                                                            <asp:Label ID="lblDettaglioSegnAperte" runat="server" Text="Click sul valore per mostrare le segnalazioni aperte" />
                                                        </telerik:RadToolTip>


                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label15" runat="server" Text="N° segnalazioni in corso:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;    
                                                      
                                           <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnInCorso').click();">
                                               <asp:Label ID="lblNumSegnalazioniInCorso" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                           </a>
                                                        &nbsp;
                                                        <asp:Image ID="imgSegnInCorso" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                        <telerik:RadToolTip ID="tltSegnInCorso" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                            Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgSegnInCorso" Width="450px"
                                                            Height="60px" Animation="Slide" OffsetY="0">
                                                            <asp:Label ID="lblSegnInCorso" runat="server" Text="Click sul valore per mostrare le segnalazioni in corso" />
                                                        </telerik:RadToolTip>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label17" runat="server" Text="N° segnalazioni evase:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;    
                                                      
                                           <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnEvase').click();">
                                               <asp:Label ID="lblNumSegnalazioniEvase" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                           </a>
                                                        &nbsp;
                                                        <asp:Image ID="imgSegnEvase" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                        <telerik:RadToolTip ID="tltSegnEvase" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                            Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgSegnEvase" Width="450px"
                                                            Height="60px" Animation="Slide" OffsetY="0">
                                                            <asp:Label ID="lblSegnEvase" runat="server" Text="Click sul valore per mostrare le segnalazioni evase" />
                                                        </telerik:RadToolTip>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label19" runat="server" Text="N° segnalazioni chiuse:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;    
                                                      
                                           <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnChiuse').click();">
                                               <asp:Label ID="lblNumSegnalazioniChiuse" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                           </a>
                                                        &nbsp;
                                                        <asp:Image ID="imgSegnChiuse" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                        <telerik:RadToolTip ID="tltSegnChiuse" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                            Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgSegnChiuse" Width="450px"
                                                            Height="60px" Animation="Slide" OffsetY="0">
                                                            <asp:Label ID="lblSegnChiuse" runat="server" Text="Click sul valore per mostrare le segnalazioni chiuse" />
                                                        </telerik:RadToolTip>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label14" runat="server" Text="N° segnalazioni chiuse:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;    
                                                      
                                           <a style="cursor: pointer" onclick="javascript:document.getElementById('btnAllSegn').click();">
                                               <asp:Label ID="lblAllSegn" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                           </a>
                                                        &nbsp;
                                                        <asp:Image ID="imgAllSegn" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                        <telerik:RadToolTip ID="RadToolTip1" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                            Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgAllSegn" Width="450px"
                                                            Height="60px" Animation="Slide" OffsetY="0">
                                                            <asp:Label ID="Label18" runat="server" Text="Click sul valore per mostrare tutte le segnalazioni" />
                                                        </telerik:RadToolTip>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td style="vertical-align: top">
                                        <fieldset style="border-width: 2px" runat="server" id="fieldSet5">
                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label11" runat="server" Text="KPI 2" Font-Size="10pt" Font-Names="Arial"></asp:Label></strong></legend>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label16" runat="server" Text="Numero di ODL emessi (con segnalazione):" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLEmessi').click();">
                                                            <asp:Label ID="lblNumODL" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                            &nbsp;
                                                        <asp:Image ID="imgODLEmessi" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                            <telerik:RadToolTip ID="tltOdlEmessi" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                                Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgODLEmessi" Width="450px"
                                                                Height="60px" Animation="Slide" OffsetY="0">
                                                                <asp:Label ID="Label12" runat="server" Text="Click sul valore per mostrare le segnalazioni aperte" />
                                                            </telerik:RadToolTip>
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblOdlEmessiNoSegn" runat="server" Text="Numero di ODL emessi (senza segnalazione):" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLEmessiNoSegn').click();">
                                                            <asp:Label ID="lblNumODLNoSegn" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                            &nbsp;
                                                        <asp:Image ID="imgODLEmessiNosegn" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                            <telerik:RadToolTip ID="tltOdlEmessiNoSegn" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                                Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgODLEmessiNosegn" Width="450px"
                                                                Height="60px" Animation="Slide" OffsetY="0">
                                                                <asp:Label ID="Label13" runat="server" Text="Click sul valore per mostrare le segnalazioni aperte" />
                                                            </telerik:RadToolTip>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>


                                <tr>
                                    <td style="vertical-align: top">
                                        <fieldset style="border-width: 2px" runat="server" id="fieldSet3">
                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label9" runat="server" Text="KPI 3" Font-Size="10pt" Font-Names="Arial"></asp:Label></strong></legend>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="KPI4" runat="server" Text="Tempo di attraversamento:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;                          
                                            <asp:Label ID="lblTempoAttraversamento" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label20" runat="server" Text="Segnalazioni aperte da più di 30 gg:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;                          
                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnAperte30gg').click();">
                                                            <asp:Label ID="lblSegnAperte30gg" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        </a>
                                                        &nbsp;
                                                        <asp:Image ID="imgSegnAperte30gg" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                        <telerik:RadToolTip ID="tltSegnAperte30gg" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                            Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgSegnAperte30gg" Width="450px"
                                                            Height="60px" Animation="Slide" OffsetY="0">
                                                            <asp:Label ID="Label22" runat="server" Text="Click sul valore per mostrare tutte le segnalazioni aperte da più di 30 giorni" />
                                                        </telerik:RadToolTip>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label21" runat="server" Text="ODL in bozza e non emessi:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLBozzaNonEmessi').click();">
                                                            <asp:Label ID="lblOdlBozzaNoEmessi" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                            &nbsp;
                                                        <asp:Image ID="imgODLBozzaNoEmessi" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                            <telerik:RadToolTip ID="tltODLBozzaNoEmessi" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                                Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgODLBozzaNoEmessi" Width="450px"
                                                                Height="60px" Animation="Slide" OffsetY="0">
                                                                <asp:Label ID="Label24" runat="server" Text="Click sul valore per mostrare gli Odl in bozza e non emessi" />
                                                            </telerik:RadToolTip>
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label23" runat="server" Text="ODL emessi e non consuntivati:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLEmessiNoCons').click();">
                                                            <asp:Label ID="lblODLEmessiNoCons" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                            &nbsp;
                                                        <asp:Image ID="imgODLEmessiNoCons" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                            <telerik:RadToolTip ID="tltODLEmessiNoCons" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                                Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgODLEmessiNoCons" Width="450px"
                                                                Height="60px" Animation="Slide" OffsetY="0">
                                                                <asp:Label ID="Label26" runat="server" Text="Click sul valore per mostrare gli Odl emessi e non consuntivati" />
                                                            </telerik:RadToolTip>
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label25" runat="server" Text="ODL consuntivati e senza CDP:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLConsNoCdP').click();">
                                                            <asp:Label ID="lblODLConsNoCDP" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                            &nbsp;
                                                        <asp:Image ID="imgODLConsNoCDP" runat="server" ImageUrl="../StandardTelerik/Immagini/info.png"
                                                            Style="cursor: pointer;" />
                                                            <telerik:RadToolTip ID="tltOdlConsNoCDP" runat="server" ShowEvent="FromCode" HideEvent="FromCode"
                                                                Position="MiddleLeft" RelativeTo="Element" TargetControlID="imgODLConsNoCDP" Width="450px"
                                                                Height="60px" Animation="Slide" OffsetY="0">
                                                                <asp:Label ID="Label28" runat="server" Text="Click sul valore per mostrare gli Odl consuntivati senza CdP" />
                                                            </telerik:RadToolTip>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td style="vertical-align: top">
                                        <fieldset style="border-width: 2px" runat="server" id="fieldSet1">
                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label5" runat="server" Text="KPI 4" Font-Size="10pt" Font-Names="Arial"></asp:Label></strong></legend>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="KPI3" runat="server" Text="Valore medio ODL:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;                          
                                            <asp:Label ID="lblValoreMedio" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="vertical-align: top">
                                        <fieldset style="border-width: 2px" runat="server" id="fieldSet4">
                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label7" runat="server" Text="KPI 5" Font-Size="10pt" Font-Names="Arial"></asp:Label></strong></legend>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label30" runat="server" Text="Criticità:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;                          
                                            <telerik:RadComboBox ID="cmbCriticita" Width="200px"
                                                Filter="Contains" runat="server" AutoPostBack="true" LoadingMessage="Caricamento...">
                                            </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Text="Tempo di presa in carico:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;                          
                                            <asp:Label ID="lblTempoPresaInCarico" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label27" runat="server" Text="Tempo di risoluzione tecnica:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;                          
                                            <asp:Label ID="lblTempoRisoluzioneTecnica" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label29" runat="server" Text="Tempo di contabilizzazione:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;                          
                                            <asp:Label ID="lblTempoContabilizzazione" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td style="vertical-align: top">
                                        <fieldset style="border-width: 2px" runat="server" id="fieldSet2">
                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label6" runat="server" Text="KPI 6" Font-Size="10pt" Font-Names="Arial"></asp:Label></strong></legend>
                                            <table>
                                                <tr>
                                                    <td>

                                                        <asp:Label ID="KPI2_1" runat="server" Text="Importo Preventivato:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp; 
                                                                    <asp:Label ID="lblImportoPreventivo" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="KPI2_2" runat="server" Text="Importo Consuntivato:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;                          
                                            <asp:Label ID="lblImportoConsuntivato" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="KPI2" runat="server" Text="Importo Impegnato:" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                        &nbsp;                          
                                            <asp:Label ID="lblImportoImpegnato" runat="server" Font-Bold="true" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </td>
                <td style="width: 50%; vertical-align: top"></td>
            </tr>
            <tr>
                <td style="width: 50%; vertical-align: top" runat="server" id="tblSegnalazioni">
                    <fieldset style="border-width: 2px" runat="server" id="fieldSetSegnalazioni">
                        <legend>&nbsp;&nbsp;<strong><asp:Label ID="lblSegnalazioni" runat="server" Text="Segnalazioni" Font-Size="10pt" Font-Names="Arial"></asp:Label></strong></legend>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <telerik:RadButton ID="btnVisualizzaSegnalazione" runat="server" Text="Visualizza la segnalazione" ToolTip="Visualizza la segnalazione" Style="cursor: pointer" />
                                                <telerik:RadButton ID="btnPrendiInCarico" runat="server" Text="Prendi in carico" ToolTip="Prendi in carico" Style="cursor: pointer" />
                                                <telerik:RadButton ID="btnManutenzione" runat="server" Text="Emetti Ordine" ToolTip="Visualizza la manutenzione per emettere l'ordine"
                                                    CausesValidation="False">
                                                </telerik:RadButton>
                                                <telerik:RadButton ID="btnVisualizzaManutenzioni" runat="server" Text="Visualizza manutenzioni" ToolTip="Visualizza manutenzioni" Style="cursor: pointer" />
                                                <telerik:RadButton ID="btnBuildingManager" runat="server" Text="Building manager" ToolTip="Building manager" Style="cursor: pointer" />
                                                <telerik:RadButton ID="btnDL" runat="server" Text="Direttore lavori" ToolTip="Direttore lavori" Style="cursor: pointer" />
                                                <telerik:RadButton ID="btnTempoGestione" runat="server" Text="Tempo di gestione" ToolTip="Tempo di gestione" Style="cursor: pointer" />
                                                <telerik:RadButton ID="btnChiudiSegnalazione" runat="server" Text="Chiudi segnalazione" ToolTip="Chiudi segnalazione" Style="cursor: pointer"
                                                    OnClientClicking="ChiudiSegnalazione" AutoPostBack="false" />
                                                <div class="nascondi">
                                                    <telerik:RadButton ID="btnChiudiSegn" runat="server" Text="Chiudi segnalazione" ToolTip="Chiudi segnalazione" Style="cursor: pointer" />
                                                    <telerik:RadButton ID="btnSegnAperte" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnSegnInCorso" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnSegnEvase" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnSegnChiuse" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnODLEmessi" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnODLEmessiNoSegn" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnAllSegn" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnSegnAperte30gg" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnODLBozzaNonEmessi" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnODLEmessiNoCons" runat="server"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnODLConsNoCdP" runat="server"></telerik:RadButton>
                                                </div>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="dgvSegnalazioni" runat="server" AutoGenerateColumns="False" Height="450px"
                                        AllowFilteringByColumn="true" EnableLinqExpressions="False" IsExporting="False"
                                        Width="97%" AllowPaging="false">
                                        <MasterTableView CommandItemDisplay="none" AllowSorting="true" AllowMultiColumnSorting="true" TableLayout="Fixed"
                                            NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true" Width="200%"
                                            ClientDataKeyNames="ID, STATO,ID_STATO" DataKeyNames="ID,STATO,ID_STATO">
                                            <CommandItemSettings ShowAddNewRecordButton="False" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="" FilterControlWidth="85%"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <telerik:RadButton ID="CheckBox1" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                                AutoPostBack="false" OnClientCheckedChanged="function(sender, args){ nascondi = 0;}" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <FilterTemplate>
                                                        <div style="width: 100%; text-align: center;">
                                                            <telerik:RadButton ID="chkSelTutti" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                                AutoPostBack="true" OnClientCheckedChanged="selezionaTuttiSegnalazioni" OnClick="ButtonSelAllSegnalazioni_Click" />
                                                        </div>
                                                    </FilterTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" Visible="true" Exportable="true" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="EqualTo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BUILDING_MANAGER" HeaderText="BUILDING MANAGER" Visible="true" Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="" Visible="false" EmptyDataText=" ">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO_INT" HeaderText="PRIORITA'" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    SortExpression="ID_PERICOLO_SEGNALAZIONE"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CATEGORIA 1" Visible="true" Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CATEGORIA 2" Visible="true" Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="true" Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                    <FilterTemplate>
                                                        <telerik:RadComboBox ID="RadComboBoxStatoOrdine" Width="100%" AppendDataBoundItems="true"
                                                            runat="server" OnClientSelectedIndexChanged="FilterStatoOrdineIndexChanged" DropDownAutoWidth="Enabled"
                                                            ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                            LoadingMessage="Caricamento...">
                                                        </telerik:RadComboBox>
                                                        <telerik:RadScriptBlock ID="RadScriptBlockStatoOrdine" runat="server">
                                                            <script type="text/javascript">
                                                                function FilterStatoOrdineIndexChanged(sender, args) {
                                                                    var tableView = $find("<%# TryCast(Container, GridItem).OwnerTableView.ClientID %>");
                                                                    var filtro = args.get_item().get_value();
                                                                    document.getElementById('HFFiltroEventoStatoOrdine').value = filtro;
                                                                    if (filtro != 'Tutti') {
                                                                        tableView.filter("STATO", filtro, "EqualTo");
                                                                    } else {
                                                                        tableView.filter("STATO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                    };
                                                                };
                                                            </script>
                                                        </telerik:RadScriptBlock>
                                                    </FilterTemplate>
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" Visible="true" Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" Visible="true" Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO" Visible="true" Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="N_SOLLECITI" HeaderText="N° SOLLECITI" Visible="true" Exportable="true" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FIGLI2" HeaderText="" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true" Exportable="true" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUM_MANUTENZIONI" HeaderText="N° MANUTENZIONI" Visible="true" Exportable="true" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="EqualTo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE"
                                                    Visible="false" EmptyDataText=" ">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_SEGNALAZIONE_PADRE" HeaderText="N° SEGN. PADRE"
                                                    Visible="true" Exportable="true" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" Visible="False">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_STATO" HeaderText="ID_STATO" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            <SortExpressions>
                                                <telerik:GridSortExpression FieldName="ID_PERICOLO_SEGNALAZIONE" SortOrder="Descending" />
                                                <telerik:GridSortExpression FieldName="N_SOLLECITI" SortOrder="Descending" />
                                                <telerik:GridSortExpression FieldName="FIGLI" SortOrder="Descending" />
                                                <telerik:GridSortExpression FieldName="NUM_MANUTENZIONI" SortOrder="Ascending" />
                                            </SortExpressions>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                            <ClientEvents OnRowSelecting="RowSelecting" OnRowClick="RowSelecting" OnRowDblClick="ModificaDblClick" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
                                    </telerik:RadGrid>
                                    <asp:HiddenField ID="HFFiltroEventoStatoOrdine" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                <td style="width: 50%; vertical-align: top" runat="server" id="tblOrdini">
                    <fieldset style="border-width: 2px;" runat="server" id="fieldSetOdl">
                        <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label1" runat="server" Text="Ordini" Font-Size="10pt" Font-Names="Arial"></asp:Label></strong></legend>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnVisualizzaManu" runat="server" Text="Visualizza" ToolTip="Visualizza la manutenzione" Style="cursor: pointer" />
                                                <asp:Button ID="btnEmettiSal" runat="server" Text="Emetti Sal" ToolTip="Emetti il Sal per la manutenzione selezionata" Style="cursor: pointer" />
                                                <telerik:RadButton ID="btnCaricaAppalti" runat="server" Text="Carica appalti" Visible="false"></telerik:RadButton>
                                                <telerik:RadButton ID="btnOrdiniBozza" runat="server" Text="Ordini in bozza" Visible="false"></telerik:RadButton>
                                                <telerik:RadButton ID="btnOrdiniEmessi" runat="server" Text="Ordini emessi" Visible="false"></telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Numero repertorio
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbRepertorio" Width="200"
                                                    Filter="Contains" runat="server" AutoPostBack="true"
                                                    LoadingMessage="Caricamento...">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="dgvODL" runat="server" AutoGenerateColumns="False" Height="450px"
                                        AllowFilteringByColumn="true" EnableLinqExpressions="False" IsExporting="False"
                                        Width="97%" AllowPaging="false">
                                        <MasterTableView CommandItemDisplay="None" AllowSorting="true" AllowMultiColumnSorting="true" TableLayout="Fixed"
                                            NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true" Width="200%"
                                            ClientDataKeyNames="ID, ID_ESERCIZIO_FINANZIARIO,PROGR,ANNO,REP_APPALTO, ID_APPALTO,ID_FORNITORE,DATA_EMISSIONE" DataKeyNames="ID,ID_ESERCIZIO_FINANZIARIO,PROGR,ANNO,REP_APPALTO,ID_APPALTO,ID_FORNITORE,DATA_EMISSIONE">
                                            <CommandItemSettings ShowAddNewRecordButton="False" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="ODL/ANNO" UniqueName="ODL" FilterControlWidth="85%"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadButton ID="CheckBox1" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                                            AutoPostBack="false" OnClientCheckedChanged="function(sender, args){ nascondi = 0;}" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ODL") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                    <FilterTemplate>
                                                        <div style="width: 100%; text-align: center;">
                                                            <telerik:RadButton ID="chkSelTutti" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                                AutoPostBack="true" OnClientCheckedChanged="selezionaTutti" OnClick="ButtonSelAll_Click" />
                                                        </div>
                                                    </FilterTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridBoundColumn DataField="SEGNALAZIONE" HeaderText="SEGNALAZIONE" Visible="true"
                                                    Exportable="true" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                    <HeaderStyle Wrap="true" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="APPALTO" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="VOCE_DGR" HeaderText="VOCE DGR" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="VOCE BP" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE"
                                                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_INIZIO_INTERVENTO" HeaderText="DATA INIZIO LAVORO"
                                                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_FINE_INTERVENTO" HeaderText="DATA FINE LAVORO"
                                                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_PGI" HeaderText="DATA PG"
                                                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_TDL" HeaderText="DATA TDL"
                                                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_CHIUSURA_LAVORI" HeaderText="DATA CHIUSURA LAVORI"
                                                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridBoundColumn DataField="IMPORTO_PREVENTIVO" HeaderText="IMPORTO PREVENTIVO" HeaderStyle-HorizontalAlign="Center"
                                                    Exportable="true" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo"
                                                    Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_ESERCIZIO_FINANZIARIO" HeaderText="ID_ESERCIZIO_FINANZIARIO" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PROGR" HeaderText="PROGR" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="REP_APPALTO" HeaderText="REP_APPALTO" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_FORNITORE" HeaderText="ID_FORNITORE" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                            <ClientEvents OnRowSelecting="RowSelectingManu" OnRowClick="RowSelectingManu" OnRowDblClick="ModificaDblClickManu" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
                                    </telerik:RadGrid>

                                </td>
                            </tr>
                        </table>

                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <img alt="Logo SES" src="../IMG/sfVerSisol.jpg" />
                </td>
            </tr>
        </table>

        <div>
            <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade" Text="Operazione effettuata"
                EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="500" Position="BottomRight"
                OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
            </telerik:RadNotification>
        </div>
        <telerik:RadWindow ID="RadWindowEmOrdine" runat="server" CenterIfModal="true" Modal="true"
            Title="Emetti Ordine" Width="800px" Height="500px" VisibleStatusbar="false" Behaviors="Pin, Maximize, Move, Resize">
            <ContentTemplate>
                <asp:Panel ID="PanelEmOrdine" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2" class="TitoloModulo">
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                    ForeColor="Blue">Selezionare tutte le voci</asp:Label>
                            </td>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="btnProcediEF" runat="server" Text="Procedi" ToolTip="Procede con la visualizzazione della manutenzione per emettere l'ordine"
                                                CausesValidation="False" OnClientClicking="function(sender, args){SalvaOrdine();}">
                                            </telerik:RadButton>
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnChiudiEF" runat="server" Text="Annulla" ToolTip="Esci senza inserire o modificare"
                                                CausesValidation="False">
                                            </telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%">
                                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 48px; top: 32px" Width="110px">Esercizio Finanziario</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEsercizio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                    Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoServizio" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
                                    Width="110px">Servizio</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbServizio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                    Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoServizioDett" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 32px"
                                    Width="110px">Voce DGR</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbServizioVoce" Width="100%" AppendDataBoundItems="true"
                                    Filter="Contains" Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblAppalto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 48px; top: 64px" Width="110px">Num. Repertorio</asp:Label>
                            </td>
                            <td style="height: 21px">
                                <telerik:RadComboBox ID="cmbAppalto" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                    Enabled="false" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </telerik:RadWindow>
        <asp:HiddenField ID="hiddenSelTutti" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hiddenSelTuttiSegnalazioni" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="idSelected" runat="server" />
        <asp:HiddenField ID="txtSTATO_PF" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="statoSegnalazione" runat="server" />
        <asp:HiddenField ID="idStatoSegnalazione" runat="server" />
        <asp:HiddenField ID="identificativo" runat="server" />
        <asp:HiddenField ID="tipo" runat="server" />
        <asp:HiddenField ID="txtannullo" runat="server" />

        <asp:HiddenField ID="idSelectedManu" runat="server" />
        <asp:HiddenField ID="idEsercizioFinanziario" runat="server" />
        <asp:HiddenField ID="progrManutenzione" runat="server" />
        <asp:HiddenField ID="annoManutenzione" runat="server" />
        <asp:HiddenField ID="repAppalto" runat="server" />
        <asp:HiddenField ID="idFornitore" runat="server" />
        <asp:HiddenField ID="idAppalto" runat="server" />
        <asp:HiddenField ID="idServizio" runat="server" />
        <asp:HiddenField ID="dataEmissione" runat="server" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <asp:HiddenField ID="HFAltezzaFGriglie" runat="server" />
        <asp:HiddenField ID="buildingManager" runat="server" />
        <asp:HiddenField ID="ordiniBozza" runat="server" />
        <asp:HiddenField ID="ordiniEmesso" runat="server" />
        <asp:HiddenField ID="ordiniEmessoNoSegn" runat="server" />

        <asp:HiddenField ID="segnAperte" runat="server" />
        <asp:HiddenField ID="segnEvase" runat="server" />
        <asp:HiddenField ID="segnInCorso" runat="server" />
        <asp:HiddenField ID="segnChiuse" runat="server" />
        <asp:HiddenField ID="allSegn" runat="server" />
        <asp:HiddenField ID="segn30gg" runat="server" />
        <asp:HiddenField ID="ODLBozzaNonEmessi" runat="server" />
        <asp:HiddenField ID="ODLEmessiNoCons" runat="server" />
        <asp:HiddenField ID="ODLConsNoCDP" runat="server" />


    </form>
    <script type="text/javascript">
                                                                function selezionaTutti(sender, args) {
                                                                    nascondi = 0;
                                                                    if (sender._checked)
                                                                        document.getElementById('hiddenSelTutti').value = "1";
                                                                    else
                                                                        document.getElementById('hiddenSelTutti').value = "0";
                                                                };

                                                                function selezionaTuttiSegnalazioni(sender, args) {
                                                                    nascondi = 0;
                                                                    if (sender._checked)
                                                                        document.getElementById('hiddenSelTuttiSegnalazioni').value = "1";
                                                                    else
                                                                        document.getElementById('hiddenSelTuttiSegnalazioni').value = "0";
                                                                };
        //window.onresize = setDimensioni;
        //Sys.Application.add_load(setDimensioni);
    </script>
</body>

</html>
