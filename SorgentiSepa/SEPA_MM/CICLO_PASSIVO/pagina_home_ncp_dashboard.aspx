<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pagina_home_ncp_dashboard.aspx.vb"
    Inherits="CICLO_PASSIVO_pagina_home_ncp_dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard Ciclo Passivo</title>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyCpJWvUcp6DPvy1ijkfwtQfOZDFOpAS4M8"></script>
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../StandardTelerik/Scripts/jsFunzioni.js"></script>
    <script type="text/javascript">
        function controllaSelezione() {
            if (document.getElementById('idSelected').value == '') {
                alert('Nessuna riga selezionata!');
                return false;
            };
        };
        function showImageOnSelectedItemChanging(sender, eventArgs) {
            var input = sender.get_inputDomElement();
            input.style.background = "url(" + eventArgs.get_item(sender._selectedIndex).get_imageUrl() + ") no-repeat";
        };
        function showFirstItemImage(sender) {
            var input = sender.get_inputDomElement();
            input.style.background = "url(" + sender.get_items().getItem(sender._selectedIndex).get_imageUrl() + ") no-repeat";
        };
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
    <script type="text/javascript">
        var map;
        var markers = [];
        function initialize() {
            var tipo = document.getElementById('HFTipo').value;
            var descrizione = document.getElementById('HFTipoDescrizione').value;
            var Latitudine = document.getElementById('HFLatitudine').value;
            var Longitudine = document.getElementById('HFLongitudine').value;
            var Colore = document.getElementById('HFColore').value;
            var Edificio = document.getElementById('HFEdificio').value;
            var latlng;
            var myOptions;
            var latit = Latitudine.split("#");
            var longit = Longitudine.split("#");
            var col = Colore.split("#");
            var Ed = Edificio.split("#");
            Latitudine = latit[0];
            Longitudine = longit[0];
            if (Latitudine != '0' && Longitudine != '0') {
                latlng = new google.maps.LatLng(Latitudine, Longitudine);
                myOptions = {
                    zoom: 11,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
            } else {
                Latitudine = 45.4545700;
                Longitudine = 9.1895100;
                latlng = new google.maps.LatLng(Latitudine, Longitudine);
                myOptions = {
                    zoom: 11,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
            };
            map = new google.maps.Map(document.getElementById("map"), myOptions);
            for (var ii = 0; ii < latit.length; ii++) {
                Latitudine = latit[ii];
                Longitudine = longit[ii];
                if (Latitudine != '0' && Longitudine != '0') {
                    var icona = 'http://maps.google.com/mapfiles/ms/icons/red-dot.png';
                    if (col[ii] == 1) {
                        icona = 'http://maps.google.com/mapfiles/ms/icons/red-dot.png';
                    };
                    if (col[ii] == 2) {
                        icona = 'http://maps.google.com/mapfiles/ms/icons/green-dot.png';
                    };
                    if (col[ii] == 3) {
                        icona = 'http://maps.google.com/mapfiles/ms/icons/yellow-dot.png';
                    };
                    if (col[ii] == 4) {
                        icona = 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png';
                    };
                    //var content = tipo + '<br/>' + descrizione + '<br/>Latitudine: ' + Latitudine + '<br/>Longitudine: ' + Longitudine;
                    var content = Ed[ii];
                    var infowindow = new google.maps.InfoWindow();
                    var marker = new google.maps.Marker
                        ({
                            position: new google.maps.LatLng(Latitudine, Longitudine),
                            map: map,
                            title: 'Visualizza Dettagli',
                            icon: icona
                        });
                    google.maps.event.addListener(marker, 'click', (function (marker, content, infowindow) {
                        return function () {
                            infowindow.setContent(content);
                            infowindow.open(map, marker);
                        };
                    })(marker, content, infowindow));
                    markers.push(marker);
                };
            };
        };
        function clearMarkers() {
            setMapOnAll(null);
        };
        function setMapOnAll(map) {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(map);
            };
        };
        window.onload = initialize;
    </script>
    <style type="text/css">
        .nascondi {
            display: none;
        }

        .indicato {
            color: darkorange;
            font-weight: bold;
        }

        .nonindicato {
            color: Black;
            font-weight: bold;
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
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="dgvSegnalazioni">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="dgvODL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbEsercizio">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbServizio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="cmbServizioVoce" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="cmbAppalto" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbServizio">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbServizioVoce" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="cmbAppalto" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbServizioVoce">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbAppalto" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnCaricaAppalti">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbRepertorio">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnChiudiSegn">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnOrdiniBozza">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLEmessiNoSegn">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="idSelected" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLBozzaNonEmessi">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblOdlBozzaNoEmessi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLEmessiNoCons">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblOdlBozzaNoEmessi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLConsNoCdP">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblOdlBozzaNoEmessi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnAperte">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnAnnullate">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnInCorso">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnEvase">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnChiuse">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnAllSegn">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="btnSegnAperteCanone">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnEvaseCanone">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSegnInCorsoCanone">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="btnSegnAperte30gg">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />

                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAperteCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniInCorsoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniEvaseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniChiuseCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumSegnalazioniAnnullateCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLEmessi">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbRepertorio" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="ODLEmessiNoCons" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLBozzaNonEmessi" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="ODLConsNoCDP" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbCriticita">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbCriticita" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblTempoPresaInCaricoExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblTempoPresaInCaricoCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblTempoRisoluzioneTecnicaExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblTempoContabilizzazioneExtraCanone" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnTempoGestione">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadButtonBuildingManager">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadButtonDirettoreLavori">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadButtonFieldQualityManager">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadButtonTecnicoAmministrativo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnOrdini" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnManutenzione">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbEsercizio" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="cmbServizio" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLEmessi">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblNumODL" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumODLNoSegn" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnODLEmessiNoSegn">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblNumODL" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="lblNumODLNoSegn" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnExportODL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvODL" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnEsportaSegnalazioni">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="cmbCategoria">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="mappa" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="btnSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel2" />
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panelHF" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnStampaTempoAttraversamento">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnTempisticheExtraCanone">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnTempisticheCanone">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelKPI" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Transparency="100">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Chiudi la dashboard e torna alla Home Page" />
                            </td>
                            <td>&nbsp&nbsp;&nbsp;</td>
                            <td>
                                <telerik:RadButton ID="RadButtonBuildingManager" runat="server" ToggleType="Radio"
                                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadioChecked" />
                                        <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadio" />
                                    </ToggleStates>
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="RadButtonDirettoreLavori" runat="server" ToggleType="Radio"
                                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadioChecked" />
                                        <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadio" />
                                    </ToggleStates>
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="RadButtonFieldQualityManager" runat="server" ToggleType="Radio"
                                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Field Quality Manager" PrimaryIconCssClass="rbToggleRadioChecked" />
                                        <telerik:RadButtonToggleState Text="Field Quality Manager" PrimaryIconCssClass="rbToggleRadio" />
                                    </ToggleStates>
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="RadButtonTecnicoAmministrativo" runat="server" ToggleType="Radio"
                                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadioChecked" />
                                        <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadio" />
                                    </ToggleStates>
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <img src="http://maps.google.com/mapfiles/ms/icons/red-dot.png" alt="Rosso" />
                            </td>
                            <td>
                                <asp:Label Text="Sede Territoriale A" runat="server" Font-Size="Small" />
                            </td>
                            <td>
                                <img src="http://maps.google.com/mapfiles/ms/icons/green-dot.png" alt="Verde" />
                            </td>
                            <td>
                                <asp:Label ID="Label12" Text="Sede Territoriale B" runat="server" Font-Size="Small" />
                            </td>
                            <td>
                                <img src="http://maps.google.com/mapfiles/ms/icons/yellow-dot.png" alt="Giallo" />
                            </td>
                            <td>
                                <asp:Label ID="Label13" Text="Sede Territoriale C" runat="server" Font-Size="Small" />
                            </td>
                            <td>
                                <img src="http://maps.google.com/mapfiles/ms/icons/blue-dot.png" alt="Blu" />
                            </td>
                            <td>
                                <asp:Label ID="Label18" Text="Sede Territoriale D" runat="server" Font-Size="Small" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 50%; vertical-align: top">
                    <asp:Panel ID="PanelKPI" runat="server">
                        <fieldset style="border-width: 2px" runat="server" id="fieldSet6">
                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label10" runat="server" Text="Sezioni"
                                Font-Names="Arial"></asp:Label></strong></legend>
                            <table id="tblKPI" runat="server" style="width: 100%">
                                <tr>
                                    <td style="vertical-align: top">
                                        <fieldset style="border-width: 2px" runat="server" id="fieldSet7">
                                            <legend>&nbsp;&nbsp;<strong>Segnalazioni&nbsp;&nbsp;</strong></legend>
                                            <table>
                                                <tr>
                                                    <td colspan="3">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td>Categoria
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="cmbCategoria" Width="100%" Filter="Contains" runat="server"
                                                                        AutoPostBack="true" LoadingMessage="Caricamento..." Enabled="false">
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <fieldset style="border-width: 2px" runat="server" id="fieldSetKPI">
                                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label2" runat="server" Text="Extra Canone"
                                                                Font-Names="Arial"></asp:Label></strong></legend>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="KPI1" runat="server" Text="N° segnalazioni aperte:"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnAperte').click();">
                                                                            <asp:Label ID="lblNumSegnalazioniAperteExtraCanone" runat="server" Font-Bold="true"
                                                                                Font-Names="Arial"></asp:Label>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label15" runat="server" Text="N° segnalazioni in corso:"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnInCorso').click();">
                                                                            <asp:Label ID="lblNumSegnalazioniInCorsoExtraCanone" runat="server" Font-Bold="true"
                                                                                Font-Names="Arial"></asp:Label>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label17" runat="server" Text="N° segnalazioni evase:"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnEvase').click();">
                                                                            <asp:Label ID="lblNumSegnalazioniEvaseExtraCanone" runat="server" Font-Bold="true"
                                                                                Font-Names="Arial"></asp:Label>
                                                                        </a>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>
                                                        <fieldset style="border-width: 2px" runat="server" id="fieldSet8">
                                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label14" runat="server" Text="Canone"
                                                                Font-Names="Arial"></asp:Label></strong></legend>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label19" runat="server" Text="N° segnalazioni aperte:"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnAperteCanone').click();">
                                                                            <asp:Label ID="lblNumSegnalazioniAperteCanone" runat="server" Font-Bold="true"
                                                                                Font-Names="Arial"></asp:Label>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label24" runat="server" Text="N° segnalazioni in corso:"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnInCorsoCanone').click();">
                                                                            <asp:Label ID="lblNumSegnalazioniInCorsoCanone" runat="server" Font-Bold="true"
                                                                                Font-Names="Arial"></asp:Label>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label28" runat="server" Text="N° segnalazioni evase:"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnEvaseCanone').click();">
                                                                            <asp:Label ID="lblNumSegnalazioniEvaseCanone" runat="server" Font-Bold="true"
                                                                                Font-Names="Arial"></asp:Label>
                                                                        </a>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>

                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td style="vertical-align: top">
                                        <fieldset style="border-width: 2px" runat="server" id="fieldSet3">
                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label9" runat="server" Text="ODL"
                                                Font-Names="Arial"></asp:Label></strong></legend>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>Numero repertorio
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbRepertorio" Width="200" Filter="Contains" runat="server"
                                                            AutoPostBack="true" LoadingMessage="Caricamento..." Enabled="false">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 90%">
                                                                    <asp:Label ID="Label21" runat="server" Text="ODL in bozza e non emessi:"
                                                                        Font-Names="Arial"></asp:Label>
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLBozzaNonEmessi').click();">
                                                                        <asp:Label ID="lblOdlBozzaNoEmessi" runat="server" Font-Bold="true"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label23" runat="server" Text="ODL emessi e non consuntivati:"
                                                                        Font-Names="Arial"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLEmessiNoCons').click();">
                                                                        <asp:Label ID="lblODLEmessiNoCons" runat="server" Font-Bold="true"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label25" runat="server" Text="ODL consuntivati e senza CDP:"
                                                                        Font-Names="Arial"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLConsNoCdP').click();">
                                                                        <asp:Label ID="lblODLConsNoCDP" runat="server" Font-Bold="true"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label16" runat="server" Text="Numero di ODL emessi (con segnalazione):"
                                                                        Font-Names="Arial"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLEmessi').click();">
                                                                        <asp:Label ID="lblNumODL" runat="server" Font-Bold="true" Font-Names="Arial"></asp:Label>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblOdlEmessiNoSegn" runat="server" Text="Numero di ODL emessi (senza segnalazione):"
                                                                        Font-Names="Arial"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <a style="cursor: pointer" onclick="javascript:document.getElementById('btnODLEmessiNoSegn').click();">
                                                                        <asp:Label ID="lblNumODLNoSegn" runat="server" Font-Bold="true"
                                                                            Font-Names="Arial"></asp:Label>
                                                                    </a>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>

                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="vertical-align: top" colspan="3">
                                        <fieldset style="border-width: 2px" runat="server" id="fieldSet4">
                                            <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label7" runat="server" Text="Tempi"
                                                Font-Names="Arial"></asp:Label></strong></legend>
                                            <table>
                                                <tr>
                                                    <td colspan="5">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="btnStampaTempoAttraversamento" runat="server" Text="Export tempo attraversamento ODL" ToolTip="Export tempo attraversamento ODL"
                                                                                    Style="cursor: pointer" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnTempisticheExtraCanone" runat="server" Text="Export tempistiche Extra canone" ToolTip="Export tempistiche Extra canone"
                                                                                    Style="cursor: pointer" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnTempisticheCanone" runat="server" Text="Export tempistiche Canone" ToolTip="Export tempistiche Canone"
                                                                                    Style="cursor: pointer" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label30" runat="server" Text="Criticità:" Font-Names="Arial"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="cmbCriticita" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Width="100px" OnClientSelectedIndexChanging="showImageOnSelectedItemChanging"
                                                                        OnClientLoad="showFirstItemImage" Filter="Contains" AutoPostBack="true">
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Text="Tempo di presa in carico segnalazioni Extra Canone:"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTempoPresaInCaricoExtraCanone" runat="server" Font-Bold="true"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                    <td style="width: 80px">&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="KPI4" runat="server" Text="Tempo di attraversamento ODL:"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTempoAttraversamento" runat="server" Font-Bold="true"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Tempo di presa in carico segnalazioni a Canone:"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTempoPresaInCaricoCanone" runat="server" Font-Bold="true"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                    <td style="width: 80px">&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label29" runat="server" Text="Tempo di contabilizzazione ODL:"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTempoContabilizzazione" runat="server" Font-Bold="true"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label27" runat="server" Text="Tempo di risoluzione tecnica segnalazioni Extra Canone:"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTempoRisoluzioneTecnica" runat="server" Font-Bold="true"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="Tempo di risoluzione tecnica segnalazioni a Canone:"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTempoRisoluzioneTecnicaCanone" runat="server" Font-Bold="true"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label20" runat="server" Text="Segnalazioni aperte da più di 30 gg:"
                                                            Font-Names="Arial"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <a style="cursor: pointer" onclick="javascript:document.getElementById('btnSegnAperte30gg').click();">
                                                            <asp:Label ID="lblSegnAperte30gg" runat="server" Font-Bold="true"
                                                                Font-Names="Arial"></asp:Label>
                                                        </a>
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
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="border-width: 2px" runat="server">
                        <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label116" runat="server" Text="Georeferenziazione"
                            Font-Names="Arial"></asp:Label></strong> </legend>
                        <asp:Panel runat="server" ID="mappa" Style="width: 100%">
                            <div id="map" style="width: 100%; height: 350px;">
                            </div>
                        </asp:Panel>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; vertical-align: top" runat="server" id="tblSegnalazioni">
                    <fieldset style="border-width: 2px" runat="server" id="fieldSetSegnalazioni">
                        <legend>&nbsp;&nbsp;<strong><asp:Label ID="lblSegnalazioni" runat="server" Text="Segnalazioni"
                            Font-Names="Arial"></asp:Label></strong></legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="btnSegnalazioni">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnVisualizzaSegnalazione" runat="server" Text="Visualizza" ToolTip="Visualizza la segnalazione"
                                                        Style="cursor: pointer" />
                                                    <telerik:RadButton ID="btnPrendiInCarico" runat="server" Text="Prendi in carico"
                                                        ToolTip="Prendi in carico" Style="cursor: pointer" />
                                                    <telerik:RadButton ID="btnManutenzione" runat="server" Text="Crea Ordine" ToolTip="Visualizza la manutenzione per creare l'ordine in bozza"
                                                        CausesValidation="False">
                                                    </telerik:RadButton>
                                                    <%--<telerik:RadButton ID="btnTempoGestione" runat="server" Text="Tempo di gestione"
                                                    ToolTip="Tempo di gestione" Style="cursor: pointer" />--%>
                                                    <telerik:RadButton ID="btnChiudiSegnalazione" runat="server" Text="Chiudi segnalazione"
                                                        ToolTip="Chiudi segnalazione" Style="cursor: pointer" OnClientClicking="ChiudiSegnalazione"
                                                        AutoPostBack="false" />
                                                    <telerik:RadButton ID="btnEsportaSegnalazioni" runat="server" Text="Esporta"
                                                        ToolTip="Esporta le segnalazioni visualizzate" Style="cursor: pointer" Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:Label Text="" runat="server" ID="NumeroSegnalazioni" Font-Bold="true" Font-Size="Large" Style="display: none" />
                                                </td>
                                                <td>
                                                    <div class="nascondi">
                                                        <telerik:RadButton ID="btnChiudiSegn" runat="server" Text="Chiudi segnalazione" ToolTip="Chiudi segnalazione"
                                                            Style="cursor: pointer" />
                                                        <telerik:RadButton ID="btnSegnAperte" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnSegnAnnullate" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnSegnInCorso" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnSegnEvase" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnSegnChiuse" runat="server">
                                                        </telerik:RadButton>

                                                        <telerik:RadButton ID="btnSegnAperteCanone" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnSegnAnnullateCanone" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnSegnInCorsoCanone" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnSegnEvaseCanone" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnSegnChiuseCanone" runat="server">
                                                        </telerik:RadButton>

                                                        <telerik:RadButton ID="btnODLEmessi" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnODLEmessiNoSegn" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnAllSegn" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnSegnAperte30gg" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnODLBozzaNonEmessi" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnODLEmessiNoCons" runat="server">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnODLConsNoCdP" runat="server">
                                                        </telerik:RadButton>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="dgvSegnalazioni" runat="server" AutoGenerateColumns="False"
                                        Height="380px" AllowFilteringByColumn="true" EnableLinqExpressions="False" IsExporting="False"
                                        Width="97%" AllowPaging="false" PageSize="100000">
                                        <MasterTableView CommandItemDisplay="none" AllowSorting="true" AllowMultiColumnSorting="true"
                                            TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                            Width="300%" ClientDataKeyNames="ID,STATO,ID_STATO" DataKeyNames="ID,STATO,ID_STATO">
                                            <CommandItemSettings ShowAddNewRecordButton="False" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_STATO" HeaderText="ID_STATO" Visible="false"
                                                    Exportable="false">
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
                                                <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" Visible="true" Exportable="true"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BUILDING_MANAGER" HeaderText="BUILDING MANAGER"
                                                    Visible="true" Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO_INT" HeaderText="PRIORITA'" Visible="true"
                                                    Exportable="true" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="EqualTo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CATEGORIA 1" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CATEGORIA 2" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="true" Exportable="true"
                                                    DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
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
                                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO" FilterControlWidth="70%"
                                                    Visible="true" Exportable="true" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="EqualTo">
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_PRESA_IN_CARICO" HeaderText="DATA IN CARICO" FilterControlWidth="70%"
                                                    Visible="true" Exportable="true" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="EqualTo">
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_CONTABILIZZAZIONE" HeaderText="DATA CONTABILIZZAZIONE" FilterControlWidth="70%"
                                                    Visible="true" Exportable="true" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="EqualTo">
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridBoundColumn DataField="N_SOLLECITI" HeaderText="N° SOLLECITI" Visible="true" FilterControlWidth="50%"
                                                    Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FIGLI" HeaderText="N° TICKET FIGLI" Visible="true" FilterControlWidth="50%"
                                                    Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUM_MANUTENZIONI" HeaderText="N° ODL EMESSI" FilterControlWidth="50%"
                                                    Visible="true" Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TEMPO_PRESA_IN_CARICO" HeaderText="GIORNI PRESA IN CARICO"
                                                    HeaderStyle-HorizontalAlign="Center" Exportable="true" AutoPostBackOnFilter="true" FilterControlWidth="50%"
                                                    DataFormatString="{0:N2}" CurrentFilterFunction="EqualTo" Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TEMPO_RISOLUZIONE_TECNICA" HeaderText="GIORNI RISOLUZIONE TECNICA"
                                                    HeaderStyle-HorizontalAlign="Center" Exportable="true" AutoPostBackOnFilter="true" FilterControlWidth="50%"
                                                    DataFormatString="{0:N2}" CurrentFilterFunction="EqualTo" Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TEMPO_CONTABILIZZAZIONE" HeaderText="GIORNI CONTABILIZZAZIONE"
                                                    HeaderStyle-HorizontalAlign="Center" Exportable="true" AutoPostBackOnFilter="true" FilterControlWidth="50%"
                                                    DataFormatString="{0:N2}" CurrentFilterFunction="EqualTo" Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NON_CONFORMITA" HeaderText="NON CONFORMITA'" Visible="true" Exportable="true"
                                                    DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    <FilterTemplate>
                                                        <telerik:RadComboBox ID="RadComboBoxNonConformita" Width="100%" AppendDataBoundItems="true"
                                                            runat="server" OnClientSelectedIndexChanged="FilterNonConformitaIndexChanged" DropDownAutoWidth="Enabled"
                                                            ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                            LoadingMessage="Caricamento...">
                                                        </telerik:RadComboBox>
                                                        <telerik:RadScriptBlock ID="RadScriptBlockNonConformita" runat="server">
                                                            <script type="text/javascript">
                                                                function FilterNonConformitaIndexChanged(sender, args) {
                                                                    var tableView = $find("<%# TryCast(Container, GridItem).OwnerTableView.ClientID %>");
                                                                    var filtro = args.get_item().get_value();
                                                                    document.getElementById('HFFiltroEventoNonConformita').value = filtro;
                                                                    if (filtro != 'Tutti') {
                                                                        tableView.filter("NON_CONFORMITA", filtro, "EqualTo");
                                                                    } else {
                                                                        tableView.filter("NON_CONFORMITA", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                    };
                                                                };
                                                            </script>
                                                        </telerik:RadScriptBlock>
                                                    </FilterTemplate>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE"
                                                    Visible="false" EmptyDataText=" ">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PERICOLO_SEGNALAZIONE" HeaderText="PERICOLO_SEGNALAZIONE" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            <SortExpressions>
                                                <telerik:GridSortExpression FieldName="DATA_INSERIMENTO" SortOrder="Descending" />
                                            </SortExpressions>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                            <ClientEvents OnRowSelecting="RowSelecting" OnRowClick="RowSelecting" OnRowDblClick="ModificaDblClick" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                <td style="width: 50%; vertical-align: top" runat="server" id="tblOrdini">
                    <fieldset style="border-width: 2px;" runat="server" id="fieldSetOdl">
                        <legend>&nbsp;&nbsp;<strong><asp:Label ID="Label1" runat="server" Text="Ordini"
                            Font-Names="Arial"></asp:Label></strong></legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="btnOrdini">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnVisualizzaManu" runat="server" Text="Visualizza" ToolTip="Visualizza la manutenzione"
                                                        Style="cursor: pointer" />

                                                    <asp:Button ID="btnEmettiSal" runat="server" Text="Emetti Sal" ToolTip="Emetti il Sal per la manutenzione selezionata"
                                                        Style="cursor: pointer" />

                                                    <telerik:RadButton ID="btnCaricaAppalti" runat="server" Text="Carica appalti" Visible="false">
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="btnOrdiniBozza" runat="server" Text="Ordini in bozza" Visible="false">
                                                    </telerik:RadButton>
                                                </td>

                                                <td>
                                                    <asp:Label Text="" runat="server" ID="NumeroOdl" Font-Bold="true" Font-Size="Large" Style="display: none" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="dgvODL" runat="server" AutoGenerateColumns="False" Height="400px"
                                        AllowFilteringByColumn="true" EnableLinqExpressions="False" IsExporting="False"
                                        Width="97%" AllowPaging="false" PageSize="100000">
                                        <MasterTableView CommandItemDisplay="None" AllowSorting="true" AllowMultiColumnSorting="true"
                                            TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                            Width="200%" ClientDataKeyNames="ID, ID_ESERCIZIO_FINANZIARIO,PROGR,ANNO,REP_APPALTO, ID_APPALTO,ID_FORNITORE,DATA_EMISSIONE"
                                            DataKeyNames="ID,ID_ESERCIZIO_FINANZIARIO,PROGR,ANNO,REP_APPALTO,ID_APPALTO,ID_FORNITORE,DATA_EMISSIONE">
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
                                                    Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    <HeaderStyle Wrap="true" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="true" Exportable="true"
                                                    DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="APPALTO" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="VOCE_DGR" HeaderText="VOCE DGR" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="VOCE BP" Visible="true"
                                                    Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
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
                                                <telerik:GridDateTimeColumn DataField="DATA_PGI" HeaderText="DATA PG" FilterControlWidth="125px"
                                                    PickerType="DatePicker" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}"
                                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    Visible="true" Exportable="true">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="DATA_TDL" HeaderText="DATA TDL" FilterControlWidth="125px"
                                                    PickerType="DatePicker" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}"
                                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    Visible="true" Exportable="true">
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
                                                <telerik:GridBoundColumn DataField="IMPORTO_PREVENTIVO" HeaderText="IMPORTO PREVENTIVO"
                                                    HeaderStyle-HorizontalAlign="Center" Exportable="true" AutoPostBackOnFilter="true"
                                                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo" Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IMPORTO_CONSUNTIVATO" HeaderText="IMPORTO CONSUNTIVATO"
                                                    HeaderStyle-HorizontalAlign="Center" Exportable="true" AutoPostBackOnFilter="true"
                                                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo" Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_ESERCIZIO_FINANZIARIO" HeaderText="ID_ESERCIZIO_FINANZIARIO"
                                                    Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PROGR" HeaderText="PROGR" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO" Visible="false" Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="REP_APPALTO" HeaderText="REP_APPALTO" Visible="false"
                                                    Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="false"
                                                    Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_FORNITORE" HeaderText="ID_FORNITORE" Visible="false"
                                                    Exportable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" Visible="false"
                                                    Exportable="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <SortExpressions>
                                                <telerik:GridSortExpression FieldName="DATA_EMISSIONE" SortOrder="Ascending" />
                                            </SortExpressions>
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
            <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
                Text="Operazione effettuata" EnableRoundedCorners="true" EnableShadow="true"
                AutoCloseDelay="500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
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
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
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
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
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
        <asp:Panel runat="server" ID="panelHF">
            <asp:HiddenField ID="HFFiltroEventoStatoOrdine" runat="server" />
            <asp:HiddenField ID="HFFiltroEventoNonConformita" runat="server" />
            <asp:HiddenField ID="hiddenSelTutti" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="hiddenSelTuttiSegnalazioni" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="idSelected" runat="server" />
            <asp:HiddenField ID="txtSTATO_PF" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="statoSegnalazione" runat="server" />
            <asp:HiddenField ID="idStatoSegnalazione" runat="server" />
            <asp:HiddenField ID="idTipologiaSegnalazione" runat="server" />
            <asp:HiddenField ID="idStatoOrdine" runat="server" />
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
            <asp:HiddenField ID="ordiniEmessoConSegnalazione" runat="server" />
            <asp:HiddenField ID="ordiniEmessoNoSegn" runat="server" />
            <asp:HiddenField ID="allSegn" runat="server" />
            <asp:HiddenField ID="segn30gg" runat="server" />
            <asp:HiddenField ID="ODLBozzaNonEmessi" runat="server" />
            <asp:HiddenField ID="ODLEmessiNoCons" runat="server" />
            <asp:HiddenField ID="ODLConsNoCDP" runat="server" />
            <asp:HiddenField ID="HFTipo" runat="server" ClientIDMode="Static" Value="" />
            <asp:HiddenField ID="HFTipoDescrizione" runat="server" ClientIDMode="Static" Value="" />
            <asp:HiddenField ID="HFLatitudine" runat="server" ClientIDMode="Static" Value="0" />
            <asp:HiddenField ID="HFLongitudine" runat="server" ClientIDMode="Static" Value="0" />
            <asp:HiddenField ID="HFColore" runat="server" ClientIDMode="Static" Value="0" />
            <asp:HiddenField ID="HFEdificio" runat="server" ClientIDMode="Static" Value="" />
            <asp:HiddenField ID="solaLettura" runat="server" Value="0" ClientIDMode="Static" />
        </asp:Panel>
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
