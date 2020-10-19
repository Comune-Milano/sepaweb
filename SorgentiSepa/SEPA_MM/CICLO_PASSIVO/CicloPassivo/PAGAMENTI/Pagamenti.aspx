<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pagamenti.aspx.vb" Inherits="PAGAMENTI_Pagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>MODULO GESTIONE ORDINI e PAGAMENTI</title>
    <script language="javascript" type="text/javascript">

        function ApriEventi() {
            window.open('Report/Eventi.aspx?ID_ODL=' + document.getElementById('HiddenID').value, "WindowPopup", "scrollbars=1, width=800px, height=600px, resizable");
        };

        function AllegaFile() {
            if ((document.getElementById('HiddenID').value == '') || (document.getElementById('HiddenID').value == '0')) {
                apriAlert('E\' necessario salvare il pagamento prima di allegare documenti!', 300, 150, 'Attenzione', null, null);
            } else {
                CenterPage('../../../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('HiddenID').value, 'Allegati', 1000, 800);
            };

            //window.open('ElencoAllegati.aspx?T=3&COD=' + document.getElementById('txtIdAppalto').value, 'AllegatiContratto', 'scrollbars=1, width=800px, height=600px, resizable');
            return false;
        };
        function CenterPage(pageURL, title, w, h) {
            var left = (screen.width / 2) - (w / 2) - 15;
            var top = (screen.height / 2) - (h / 2) - 15;
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        };
        var Uscita;
        Uscita = 0;
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {
                e.preventDefault();
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '111';
            };
        };
        function $onkeydown() {
            if (event.keyCode == 13) {
                event.keyCode = 0;
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '111';
            };
        };
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        };
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //        o.value = o.value.replace('.', ',');
            document.getElementById('txtModificato').value = '1';
        };
        function DelPointer(obj) {
            obj.value = obj.value.replace('.', '');
            document.getElementById(obj.id).value = obj.value;
        };
        function $onkeydown() {
            if (event.keyCode == 46) {
                event.keyCode = 0;
            };
        };
        function AutoDecimal2(obj) {
            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    }
                    risultato = dascrivere + risultato + ',' + decimali;
                    //document.getElementById(obj.id).value = a.replace('.', ',')
                    document.getElementById(obj.id).value = risultato;
                } else {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                };
            };
        };
        function CalcolaPrezzoTotale(obj, quantita, prezzo) {
            var risultato;
            quantita = quantita.replace('.', '');
            quantita = quantita.replace(',', '.');
            prezzo = prezzo.replace('.', '');
            prezzo = prezzo.replace(',', '.');
            risultato = quantita * prezzo;
            risultato = risultato.toFixed(2);
        };
    </script>
    <script type="text/javascript">

        function EliminaPagamento() {
            var sicuro = confirm('Sei sicuro di voler eliminare questo Pagamento? Tutti i dati andranno persi.');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            } else {
                document.getElementById('txtElimina').value = '0';
            };
        };
        function AnnullaPagamento() {
            var sicuro = confirm('Sei sicuro di voler annullare questa pagamento? L\'ordine visualizzato non sarà più modificabile!');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            } else {
                document.getElementById('txtElimina').value = '0';
            };
        };
        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda manutenzione premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                } else {
                    return "Attenzione...Uscire dalla scheda manutenzione premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                };
            };
        };


        //        function StampaOrdine() {
        //            if (document.getElementById('txtModificato').value == '1') {
        //                alert('Sono state apportate modifiche, salvare prima di stampare!')
        //                return;
        //            };
        //            if (document.getElementById('txtStatoPagamento').value <= 1) {
        //                Conferma = window.confirm("Attenzione...Confermi di voler emettere l\'ordine?");
        //                if (Conferma == true) {
        //                    document.getElementById('txtElimina').value = '1';
        //                } else {
        //                    document.getElementById('txtElimina').value = '0';
        //                };
        //            } else {
        //                document.getElementById('txtElimina').value = '1';
        //                document.getElementById('txtModificato').value = '0';
        //            };
        //        };
        function PaymentConfirm() {
            var Conferma
            if (document.getElementById('txtModificato').value == '1') {
                if (document.getElementById('txtVisualizza').value < 2) {
                    alert('Sono state apportate modifiche, salvare prima di stampare!')
                    return;
                };
            };
            if (document.getElementById('txtStatoPagamento').value <= 3) {
                Conferma = window.confirm("Attenzione...Confermi di voler emettere un pagamento?");
                if (Conferma == true) {
                    document.getElementById('txtElimina').value = '1';
                } else {
                    document.getElementById('txtElimina').value = '0';
                };
            } else {
                document.getElementById('txtElimina').value = '1';
                document.getElementById('txtModificato').value = '0';
            };
        };
        function CompletaData(e, obj) {
            var sKeyPressed;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    } else {
                        e.preventDefault();
                    };
                };
            } else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                } else if (obj.value.length == 5) {
                    obj.value += "/";
                } else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        } else {
                            e.preventDefault();
                        };
                    };
                };
            };
        };
        function CalcolaLordo(campo) {
            var iva, cassa, ritenuta, imponibile, penale;
            var risIVA, risCassa, risRitenuta, risRivalsa;
            var prezzoP, prezzoC;
            var risultato1;
            var Rivalsa;
            var risultato123;
            var risultato12;
            var risultato9;
            var risultato10;
            prezzoResiduo = document.getElementById('txtResiduoControllo').value.replace(/\./g, '');
            prezzoResiduo = prezzoResiduo.replace(/\,/g, '.');
            if (campo == 1) {
                if (document.getElementById('txtIVA_PRE').value == '') { document.getElementById('txtIVA_PRE').value = '0'; };
                if (document.getElementById('txtCass_PRE').value == '') { document.getElementById('txtCass_PRE').value = '0'; };
                if (document.getElementById('txtRitenuta_PRE').value == '') { document.getElementById('txtRitenuta_PRE').value = '0'; };
                if (document.getElementById('txtRivalsa_PRE').value == '') { document.getElementById('txtRivalsa_PRE').value = '0'; };
                document.getElementById('txtPercIVA').value = document.getElementById('txtIVA_PRE').value
                document.getElementById('txtPercCassa').value = document.getElementById('txtCass_PRE').value
                document.getElementById('txtPercRitenuta').value = document.getElementById('txtRitenuta_PRE').value
                document.getElementById('txtPercRivalsa').value = document.getElementById('txtRivalsa_PRE').value
                iva = document.getElementById('txtPercIVA').value.replace(/\./g, '');
                iva = iva.replace(/\,/g, '.'); //(',', '.');
                cassa = document.getElementById('txtPercCassa').value.replace(/\./g, '');
                cassa = cassa.replace(/\,/g, '.');
                ritenuta = document.getElementById('txtPercRitenuta').value.replace(/\./g, '');
                ritenuta = ritenuta.replace(/\,/g, '.'); //(',', '.');
                Rivalsa = document.getElementById('txtPercRivalsa').value.replace(/\./g, '');
                Rivalsa = Rivalsa.replace(/\,/g, '.'); //(',', '.');
                imponibile = document.getElementById('txtImponibileP').value.replace(/\./g, '');
                imponibile = imponibile.replace(/\,/g, '.'); //(',', '.');
                percipiente = document.getElementById('txtIMPScaricoPercipiente').value.replace(/\./g, '');
                percipiente = percipiente.replace(/\,/g, '.'); //(',', '.');
                azienda = document.getElementById('txtIMPScaricoAzienda').value.replace(/\./g, '');
                azienda = azienda.replace(/\,/g, '.'); //(',', '.');
                if (azienda == '') { azienda = '0'; };
                if (percipiente == '') { percipiente = '0'; };
                if (imponibile == '') { imponibile = '0'; };
                if (iva == '') { iva = '0'; };
                if (cassa == '') { cassa = '0'; };
                if (ritenuta == '') { ritenuta = '0'; };
                if (Rivalsa == '') { Rivalsa = '0'; };
                //******************* 1) prenotato netto *************************
                prezzoP = document.getElementById('txtNettoP').value.replace(/\./g, '');
                prezzoP = prezzoP.replace(/\,/g, '.');
                //****************************************************************
                //******************* 2) rivalsa imps *************************
                if (Rivalsa == 0) {
                    risRivalsa = 0;
                } else {
                    risRivalsa = (parseFloat(prezzoP) * parseFloat(Rivalsa)) / 100;
                    risRivalsa = risRivalsa.toFixed(2);
                };
                //*************************************************************
                //******************* 3) cassa *************************
                if (cassa == 0) {
                    risCassa = 0;
                } else {
                    risCassa = (parseFloat(prezzoP) * parseFloat(cassa)) / 100;
                    risCassa = risCassa.toFixed(2);
                };
                //******************************************************
                //******************************** 4) iva ****************************************
                risultato123 = parseFloat(prezzoP) + parseFloat(risRivalsa) + parseFloat(risCassa);
                risIVA = (parseFloat(risultato123) * parseFloat(iva)) / 100;
                risIVA = risIVA.toFixed(2);
                //********************************************************************************
                //******************************** 5) ritenuta *******************
                risultato12 = parseFloat(prezzoP) + parseFloat(risRivalsa);
                risRitenuta = (parseFloat(risultato12) * parseFloat(ritenuta)) / 100;
                risRitenuta = risRitenuta.toFixed(2);
                //****************************************************************
                if (document.getElementById('codRitenuta').value == '301') {
                    risultato9 = parseFloat(risultato123) + parseFloat(risIVA) + parseFloat(imponibile) + parseFloat(azienda);
                    risultato10 = parseFloat(risultato123) + parseFloat(risIVA) + parseFloat(imponibile) - parseFloat(percipiente) - parseFloat(risRitenuta);
                } else {
                    risultato9 = parseFloat(risultato123) + parseFloat(risIVA) + parseFloat(imponibile);
                    risultato10 = parseFloat(risultato123) + parseFloat(risIVA) + parseFloat(imponibile) - parseFloat(risRitenuta);
                };
                if (parseFloat(risultato9) > parseFloat(prezzoResiduo)) {
                    alert('Attenzione...Importo LORDO inserito superiore al residuo!');
                    document.getElementById('txtCassaP').value = '';
                    document.getElementById('txtIVAP').value = '';
                    document.getElementById('txtRitenutaP').value = '';
                    document.getElementById('txtRivalsaP').value = '';
                    document.getElementById('txtNettoP').value = '';
                    document.getElementById('txtLordoP').value = '';
                    document.getElementById('txtIMPScaricoPercipiente').value = '';
                    document.getElementById('txtIMPScaricoAzienda').value = '';
                    document.getElementById('txtPrenotatoLordo').value = 0;
                    document.getElementById('txtPrenotatoLordoRIT').value = 0;
                    document.getElementById('txtAzienda').value = 0;
                    document.getElementById('txtPercipiente').value = 0;
                    return;
                };
                risultato9 = risultato9 + '';
                var prenotatoLordo;
                prenotatoLordo = risultato10 + '';
                if (document.getElementById('HiddenFieldDataEmissione')) {
                    if (((parseInt(document.getElementById('HiddenFieldDataEmissione').value) >= 20170101) && (parseInt(document.getElementById('HiddenFieldDataEmissione').value) < 20180715)) || (parseInt(document.getElementById('HiddenFieldDataEmissione').value) == 0)) {
                        if (document.getElementById('HiddenFieldTipoProfessionista').value == 'P') {
                        risultato10 = risultato10 - risIVA + '';
                    } else {
                        risultato10 = risultato10 + '';
                        }
                    } else {
                        risultato10 = risultato10 + '';
                    };
                };
                risIVA = risIVA + '';
                risCassa = risCassa + '';
                risRitenuta = risRitenuta + '';
                risRivalsa = risRivalsa + '';
                document.getElementById('txtCassaP').value = risCassa.replace('.', ',');
                document.getElementById('txtIVAP').value = risIVA.replace('.', ',');
                document.getElementById('txtRitenutaP').value = risRitenuta.replace('.', ',');
                document.getElementById('txtRivalsaP').value = risRivalsa.replace('.', ',');
                document.getElementById('txtLordoP').value = risultato9.replace('.', ',');
                document.getElementById('txtPrenotatoLordo').value = risultato9.replace('.', ',');
                document.getElementById('txtPrenotatoLordoRIT').value = prenotatoLordo.replace('.', ',');
                
                document.getElementById('txtDaPagare').value = risultato10.replace('.', ',');
                document.getElementById('txtAzienda').value = azienda.replace('.', ',');
                document.getElementById('txtPercipiente').value = percipiente.replace('.', ',');
                AutoDecimal2(document.getElementById('txtCassaP'));
                AutoDecimal2(document.getElementById('txtIVAP'));
                AutoDecimal2(document.getElementById('txtRitenutaP'));
                AutoDecimal2(document.getElementById('txtRivalsaP'));
                AutoDecimal2(document.getElementById('txtIMPScaricoPercipiente'));
                AutoDecimal2(document.getElementById('txtIMPScaricoAzienda'));
                AutoDecimal2(document.getElementById('txtLordoP'));
                AutoDecimal2(document.getElementById('txtPrenotatoLordo'));
                AutoDecimal2(document.getElementById('txtPrenotatoLordoRIT'));
                AutoDecimal2(document.getElementById('txtDaPagare'));
            } else {
                if (document.getElementById('txtIVA_CONS').value == '') { document.getElementById('txtIVA_CONS').value = '0'; };
                if (document.getElementById('txtCass_CONS').value == '') { document.getElementById('txtCass_CONS').value = '0'; };
                if (document.getElementById('txtRitenuta_CONS').value == '') { document.getElementById('txtRitenuta_CONS').value = '0'; };
                if (document.getElementById('txtRivalsa_CONS').value == '') { document.getElementById('txtRivalsa_CONS').value = '0'; };
                document.getElementById('txtPercIVA_C').value = document.getElementById('txtIVA_CONS').value
                document.getElementById('txtPercCassa_C').value = document.getElementById('txtCass_CONS').value
                document.getElementById('txtPercRivalsa_C').value = document.getElementById('txtRivalsa_CONS').value
                document.getElementById('txtPercRitenuta_C').value = document.getElementById('txtRitenuta_CONS').value
                iva = document.getElementById('txtPercIVA_C').value.replace(/\./g, '');
                iva = iva.replace(/\,/g, '.'); //(',', '.');
                cassa = document.getElementById('txtPercCassa_C').value.replace(/\./g, '');
                cassa = cassa.replace(/\,/g, '.');
                Rivalsa = document.getElementById('txtPercRivalsa_C').value.replace(/\./g, '');
                Rivalsa = Rivalsa.replace(/\,/g, '.');
                ritenuta = document.getElementById('txtPercRitenuta_C').value.replace(/\./g, '');
                ritenuta = ritenuta.replace(/\,/g, '.'); //(',', '.');
                imponibile = document.getElementById('txtImponibileC').value.replace(/\./g, '');
                imponibile = imponibile.replace(/\,/g, '.'); //(',', '.');
                percipiente = document.getElementById('txtIMPScaricoPercipienteC').value.replace(/\./g, '');
                percipiente = percipiente.replace(/\,/g, '.'); //(',', '.');
                azienda = document.getElementById('txtIMPScaricoAziendaC').value.replace(/\./g, '');
                azienda = azienda.replace(/\,/g, '.'); //(',', '.');
                penale = document.getElementById('txtPenaleC').value.replace(/\./g, '');
                penale = penale.replace(/\,/g, '.'); //(',', '.');
                prezzoC = document.getElementById('txtNettoC').value.replace(/\./g, '');
                prezzoC = prezzoC.replace(/\,/g, '.');
                if (azienda == '') { azienda = '0'; };
                if (percipiente == '') { percipiente = '0'; };
                if (imponibile == '') { imponibile = '0'; };
                if (iva == '') { iva = '0'; };
                if (cassa == '') { cassa = '0'; };
                if (ritenuta == '') { ritenuta = '0'; };
                if (Rivalsa == '') { Rivalsa = '0'; };
                if (penale == '') { penale = '0'; };
                if (parseFloat(penale) > parseFloat(prezzoC)) {
                    alert('Attenzione...L\'importo della penale non deve superiore l\'importo netto!');
                    return;
                };
                //******************* 1) prezzo senza penale *************************
                prezzoC = parseFloat(prezzoC) - parseFloat(penale);
                //******************* 2) rivalsa imps *************************
                if (Rivalsa == 0) {
                    risRivalsa = 0;
                } else {
                    risRivalsa = (parseFloat(prezzoC) * parseFloat(Rivalsa)) / 100;
                    risRivalsa = risRivalsa.toFixed(2);
                };
                //*************************************************************
                //******************* 3) cassa *************************
                if (cassa == 0) {
                    risCassa = 0;
                } else {
                    risCassa = (parseFloat(prezzoC) * parseFloat(cassa)) / 100;
                    risCassa = risCassa.toFixed(2);
                };
                //******************************************************
                //******************************** 4) iva ****************************************
                risultato123 = parseFloat(prezzoC) + parseFloat(risRivalsa) + parseFloat(risCassa);
                risIVA = (parseFloat(risultato123) * parseFloat(iva)) / 100;
                risIVA = risIVA.toFixed(2);
                //********************************************************************************
                //******************************** 5) ritenuta *******************
                risultato12 = parseFloat(prezzoC) + parseFloat(risRivalsa);
                risRitenuta = (parseFloat(risultato12) * parseFloat(ritenuta)) / 100;
                risRitenuta = risRitenuta.toFixed(2);
                //****************************************************************
                if (document.getElementById('codRitenuta').value == '301') {
                    risultato9 = parseFloat(risultato123) + parseFloat(risIVA) + parseFloat(imponibile) + parseFloat(azienda);
                    risultato10 = parseFloat(risultato123) + parseFloat(risIVA) + parseFloat(imponibile) - parseFloat(percipiente) - parseFloat(risRitenuta);
                } else {
                    risultato9 = parseFloat(risultato123) + parseFloat(risIVA) + parseFloat(imponibile);
                    risultato10 = parseFloat(risultato123) + parseFloat(risIVA) + parseFloat(imponibile) - parseFloat(risRitenuta);
                };
                if (parseFloat(risultato9) > (parseFloat(prezzoResiduo) + parseFloat(prezzoC))) {
                    alert('Attenzione...Importo LORDO inserito superiore al residuo!');
                    document.getElementById('txtCassaC').value = '';
                    document.getElementById('txtIVAC').value = '';
                    document.getElementById('txtRitenutaC').value = '';
                    document.getElementById('txtRivalsaC').value = '';
                    document.getElementById('txtNettoC').value = '';
                    document.getElementById('txtLordoC').value = '';
                    document.getElementById('txtPenaleC').value = '';
                    document.getElementById('txtIMPScaricoPercipienteC').value = '';
                    document.getElementById('txtIMPScaricoAziendaC').value = '';
                    document.getElementById('txtConsuntivatoLordo').value = 0;
                    document.getElementById('txtConsuntivatoLordoRIT').value = 0;
                    document.getElementById('txtAzienda').value = 0;
                    document.getElementById('txtPercipiente').value = 0;
                    return;
                };
                risultato9 = risultato9 + '';
                var prenotatoLordoC;
                prenotatoLordoC = risultato10 + '';
                if (document.getElementById('HiddenFieldDataEmissione')) {
                    if (((parseInt(document.getElementById('HiddenFieldDataEmissione').value) >= 20170101) && (parseInt(document.getElementById('HiddenFieldDataEmissione').value) < 20180715)) || (parseInt(document.getElementById('HiddenFieldDataEmissione').value) == 0)) {
                        if (document.getElementById('HiddenFieldTipoProfessionista').value == 'P') {
                        risultato10 = risultato10 - risIVA + '';
                    } else {
                        risultato10 = risultato10 + '';
                        }
                    } else {
                        risultato10 = risultato10 + '';
                    };
                };
                risIVA = risIVA + '';
                risCassa = risCassa + '';
                risRitenuta = risRitenuta + '';
                risRivalsa = risRivalsa + '';
                document.getElementById('txtPenaleC').value = penale.replace('.', ',');
                document.getElementById('txtCassaC').value = risCassa.replace('.', ',');
                document.getElementById('txtIVAC').value = risIVA.replace('.', ',');
                document.getElementById('txtRitenutaC').value = risRitenuta.replace('.', ',');
                document.getElementById('txtRivalsaC').value = risRivalsa.replace('.', ',');
                document.getElementById('txtLordoC').value = risultato9.replace('.', ',');
                document.getElementById('txtConsuntivatoLordo').value = risultato9.replace('.', ',');
                document.getElementById('txtConsuntivatoLordoRIT').value = prenotatoLordoC.replace('.', ',');
                document.getElementById('txtDaPagareC').value = risultato10.replace('.', ',');
                document.getElementById('txtAzienda_C').value = azienda.replace('.', ',');
                document.getElementById('txtPercipiente_C').value = percipiente.replace('.', ',');
                AutoDecimal2(document.getElementById('txtCassaC'));
                AutoDecimal2(document.getElementById('txtIVAC'));
                AutoDecimal2(document.getElementById('txtRitenutaC'));
                AutoDecimal2(document.getElementById('txtRivalsaC'));
                AutoDecimal2(document.getElementById('txtLordoC'));
                AutoDecimal2(document.getElementById('txtIMPScaricoPercipienteC'));
                AutoDecimal2(document.getElementById('txtIMPScaricoAziendaC'));
                AutoDecimal2(document.getElementById('txtConsuntivatoLordo'));
                AutoDecimal2(document.getElementById('txtConsuntivatoLordoRIT'));
                AutoDecimal2(document.getElementById('txtDaPagareC'));
                AutoDecimal2(document.getElementById('txtPenaleC'));
            };
        };
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        } else {
            window.document.addEventListener("keydown", TastoInvio, true);
        };
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <div>
            <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
                EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="100" Position="BottomRight"
                OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
            </telerik:RadNotification>
        </div>
        <div class="FontTelerik">
            <table style="width: 100%">
                <tr>
                    <td class="TitoloModulo">Ordini e pagamenti
                    </td>
                </tr>
                <tr>
                    <td style="height: 7px;"></td>
                </tr>
                <tr>
                    <td style="width: 800px; height: 1px;" id="TD_Principale">
                        <table style="width: 760px">
                            <tr>
                                <td style="width: 76px">
                                    <telerik:RadButton ID="btnINDIETRO" runat="server" Text="Indietro" ToolTip="Indietro"
                                        OnClientClicking="function(sender, args){Blocca_SbloccaMenu('0');document.getElementById('USCITA').value='1';ConfermaEsci();}" />
                                </td>
                                <td style="width: 76px">
                                    <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}" />
                                </td>
                                <td style="width: 76px;">
                                    <telerik:RadButton ID="btnElimina" runat="server" Text="Elimina" ToolTip="Elimina il Pagamento visualizzato"
                                        OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';  EliminaPagamento();}" />
                                </td>
                                <td style="width: 76px">
                                    <telerik:RadButton ID="btnAnnulla" runat="server" Text="Annulla Ordine" ToolTip="Annulla il pagamento visualizzato"
                                        OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';  AnnullaPagamento();}" />
                                </td>
                                <td style="width: 76px">
                                    <telerik:RadButton ID="ImgStampa" runat="server" Text="Stampa Impegno" ToolTip="Stampa Impegno di Spesa e Ordine di Lavoro" />
                                </td>
                                <td style="width: 76px">
                                    <telerik:RadButton ID="ImgStampaPag" runat="server" Text="Stampa Pagamento" ToolTip="Stampa Mandato di Pagamento"
                                        OnClientClicking="function(sender, args){PaymentConfirm();}" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnRielaboraPagamento" runat="server" Text="Rielabora Pagamento"
                                        Style="z-index: 100; left: 584px; position: static; top: 32px"
                                        ToolTip="Rielabora il Pagamento" />
                                </td>
                                <td style="width: 76px">
                                    <telerik:RadButton ID="ImgEventi" runat="server" Text="Eventi" ToolTip="Eventi Scheda Pagamenti"
                                        OnClientClicking="function(sender, args){ApriEventi();}" />
                                </td>
                                <td style="width: 76px">
                                    <telerik:RadButton ID="ImgAllegaFile" runat="server" Text="Allegati" ToolTip="Allegati" AutoPostBack="False"
                                        OnClientClicking="function(sender, args){AllegaFile();}" />
                                </td>
                                <td style="width: 76px">
                                    <telerik:RadButton ID="imgUscita" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="function(sender, args){Blocca_SbloccaMenu('0');document.getElementById('USCITA').value='1';ConfermaEsci();}" />
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="Label4" runat="server" Width="760px" CssClass="TitoloH1" Style="text-align: left">DETTAGLI VOCE</asp:Label>
                        <table style="width: 760px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCodCodice" runat="server" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                        Width="100px">CODICE BP</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCODICE" runat="server" MaxLength="20" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px" Enabled="False" Font-Bold="True"></asp:TextBox>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:Label ID="lblDESC" runat="server" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                        Width="70px">VOCE BP</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDESCRIZIONE" runat="server" MaxLength="20" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="320px" Enabled="False" Font-Bold="True"></asp:TextBox>
                                </td>
                                <td></td>
                                <td class="TitoloH1">
                                    <asp:Label ID="lblEsercizioFinanziario" runat="server" Style="font-size: 8pt" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblVal1" runat="server" Font-Bold="False" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="230px">Budget o consistenza inizale</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                        Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro1" runat="server" Font-Bold="False" ForeColor="Black" Style="text-align: right"
                                        Text="&#8364;" Width="16px"></asp:Label>
                                </td>
                                <td style="width: 58px"></td>
                                <td class="TitoloH1">
                                    <asp:HyperLink ID="HLink_Prenotato" runat="server" Font-Underline="True"
                                        Style="cursor: pointer; font-size: 8pt; text-align: left" ToolTip="Visualizza tutte le prenotazioni della voce BP"
                                        Width="170px">Totale in Prenotazione</asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto2" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                        Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        Width="120px" Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro3" runat="server" ForeColor="Black" Style="text-align: right"
                                        Text="&#8364;" Width="16px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblVal2" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="230px">Budget assestato o consistenza assestante</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto1" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                        Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        Width="120px" Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro2" runat="server" ForeColor="Black" Style="text-align: right"
                                        Text="&#8364;" Width="16px"></asp:Label>
                                </td>
                                <td style="width: 58px"></td>
                                <td class="TitoloH1">
                                    <asp:HyperLink ID="HLink_Consuntivo" runat="server" Font-Bold="True" Font-Underline="True"
                                        Style="cursor: pointer; font-size: 8pt; text-align: left" ToolTip="Visualizza tutti i pagamenti della voce BP"
                                        Width="170px">Totale Consuntivato</asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto3" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                        Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        Width="120px" Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro4" runat="server" ForeColor="Black" Style="text-align: right"
                                        Text="&#8364;" Width="16px"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 58px">
                        </td>
                        <td>
                            <asp:HyperLink ID="HLink_Pagato" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" Font-Underline="True" ForeColor="Blue" Style="cursor: hand" ToolTip="Visualizza tutti i pagamenti della voce BP"
                                Width="170px" Visible="False">Totale Pagato</asp:HyperLink>
                        </td>
                        <td>
                            <asp:TextBox ID="txtImporto4" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                                Width="120px" Enabled="False" Visible="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblEuro5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="&#8364;" Width="16px"
                                Visible="False"></asp:Label>
                        </td>
                    </tr>--%>
                            <tr>
                                <td>
                                    <asp:Label ID="lblval6" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="230px">Disponibilità residua</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto5" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                        Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        Width="120px" Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro6" runat="server" Style="text-align: right" Text="&#8364;"
                                        Width="16px"></asp:Label>
                                </td>
                                <td style="width: 58px"></td>
                                <td class="TitoloH1">
                                    <asp:HyperLink ID="HLink_ElencoPag" runat="server" Font-Bold="True" Font-Underline="True"
                                        Style="cursor: pointer; font-size: 8pt; text-align: left" Width="170px" ToolTip="Visualizza tutti le prenotazioni e pagamenti della voce BP"
                                        Visible="False">Dettaglio Pagamenti</asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIVA" runat="server" Enabled="False" Font-Bold="True" MaxLength="30"
                                        ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        Width="60px" Visible="False"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                        <asp:Label ID="Label1" CssClass="TitoloH1" Style="text-align: left"
                            runat="server" Width="760px">DETTAGLI ORDINE DI LAVORO</asp:Label><br />
                        <table width="100%">
                            <tr>
                                <td colspan="4">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblStato" runat="server" Font-Bold="False" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                                    Width="70px">STATO ODL</asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbStato" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                    LoadingMessage="Caricamento...">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 9px"></td>
                                <td style="width: 15px"></td>
                                <td colspan="8" class="TitoloH1" style="text-align: left">
                                    <asp:Label ID="lblODL" runat="server" Font-Bold="false" Style="cursor: pointer; font-size: 8pt; text-align: left" Width="130px">ORDINE DI SERVIZIO N°</asp:Label>
                                    <asp:Label ID="lblODL1" runat="server" Style="cursor: pointer; font-size: 8pt; text-align: left" Font-Bold="true" Width="100px"></asp:Label>
                                    <asp:Label ID="lblDataDel" runat="server" Style="cursor: pointer; font-size: 8pt; text-align: left">del</asp:Label>
                                    <asp:Label ID="lblData" runat="server" Font-Bold="True" Style="cursor: pointer; font-size: 8pt; text-align: left" Width="70px"></asp:Label>
                                </td>


                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFornitore" runat="server" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                                    Width="70px">Beneficiario *</asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbfornitore" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                    LoadingMessage="Caricamento...">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                                <td></td>
                                <td colspan="7">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDescrizione" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                                    Width="80px">Descrizione *</asp:Label>
                                            </td>
                                            <td>
                                                <%--<asp:Label ID="lblContoCorrente" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                 Visible="False">CC*</asp:Label>
                            <asp:DropDownList ID="cmbContoCorrente" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 10; left: 142px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 224px" Width="100px" TabIndex="1" Visible="False">
                                <asp:ListItem Selected="True" Value="-1">---</asp:ListItem>
                            </asp:DropDownList>--%>
                                                <asp:TextBox ID="txtDescrizioneP" runat="server" MaxLength="2000" Style="width: 400px;"
                                                    TextMode="MultiLine" Rows="2"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="Label2" runat="server" CssClass="TitoloH1" Style="text-align: left">IMPORTO PRENOTATO</asp:Label>
                                </td>

                                <td style="width: 9px"></td>
                                <td style="width: 15px"></td>
                                <td colspan="2">
                                    <asp:Label ID="Label8" runat="server" CssClass="TitoloH1" Style="text-align: left">IMPORTO CONSUNTIVATO</asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                                <td></td>
                                <td style="width: 15px"></td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" CssClass="TitoloH1" Style="text-align: left" Width="100px">PAGAMENTO</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblNettoP" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100%">Prenotato netto* (IVA esclusa)</asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtNettoP" runat="server" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td style="width: 9px"></td>
                                <td style="width: 15px"></td>
                                <td colspan="2">
                                    <asp:Label ID="lblNettoC" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100%">Consuntivato netto* (IVA esclusa)</asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:TextBox ID="txtNettoC" runat="server" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label22" runat="server" ForeColor="Black" Style="text-align: right"
                                        Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td class="TitoloH1" style="text-align: left">
                                    <asp:HyperLink ID="HLink_ElencoMandati" runat="server" Font-Bold="True" Font-Underline="True"
                                        Style="cursor: pointer; font-size: 8pt; text-align: left" ToolTip="Visualizza tutti i mandati di pagamento"
                                        Width="140px">Mandati di Pagamento</asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;
                                </td>
                                <td></td>
                                <td>&nbsp;
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label13" runat="server" Font-Bold="False" ForeColor="Black" Style="text-align: right"
                                        Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td colspan="2">
                                    <asp:Label ID="Label3" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100px">Penale</asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:TextBox ID="txtPenaleC" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px" ToolTip="Penale"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td>
                                    <asp:Label ID="lblNettoP0" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="80px">Data Scadenza*</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRivalsa" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="80px">Rivalsa INPS</asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtRivalsa_PRE" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label29" runat="server" Style="z-index: 100; left: 8px; top: 88px; text-align: center"
                                        Width="15px">%</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRivalsaP" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px" ToolTip="Rivalsa IMPS"></asp:TextBox>
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label30" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td>
                                    <asp:Label ID="lblRivalsa0" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="80px">Rivalsa INPS</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRivalsa_CONS" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td style="width: 20px">
                                    <asp:Label ID="Label7" runat="server" Style="z-index: 100; left: 8px; top: 88px; text-align: center"
                                        Width="15px">%</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRivalsaC" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px" ToolTip="Rivalsa IMPS"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td>
                                    <asp:TextBox ID="txtDScad" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="80px" ToolTip="Contributo Cassa di Previdenza"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCassaP" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="80px">Cassa</asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtCass_PRE" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Style="z-index: 100; left: 8px; top: 88px; text-align: center"
                                        Width="15px">%</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCassaP" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px" ToolTip="Contributo Cassa di Previdenza"></asp:TextBox>
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label5" runat="server" ForeColor="Black" Style="text-align: right"
                                        Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td>
                                    <asp:Label ID="lblCassaC" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100px">Cassa</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCass_CONS" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td style="width: 20px">
                                    <asp:Label ID="Label15" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px; text-align: center"
                                        Width="15px">%</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCassaC" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        ToolTip="Contributo Cassa di Previdenza"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label28" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="140px">STATO LIQUIDAZIONE</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIVAP" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="80px">IVA</asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtIVA_PRE" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Style="z-index: 100; left: 8px; top: 88px; text-align: center"
                                        Width="15px">%</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIVAP" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label26" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td>
                                    <asp:Label ID="lblIVAC" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100px">IVA</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIVA_CONS" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td style="width: 20px">
                                    <asp:Label ID="Label17" runat="server" Style="z-index: 100; left: 8px; top: 88px; text-align: center"
                                        Width="15px">%</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIVAC" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label20" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td>
                                    <asp:DropDownList ID="cmb_Liquidazione" runat="server" BackColor="White" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid; top: 56px"
                                        Width="140px" Enabled="False"
                                        Font-Bold="True">
                                        <asp:ListItem Selected="True" Value="0">DA LIQUIDARE</asp:ListItem>
                                        <asp:ListItem Value="1">PARZIALE</asp:ListItem>
                                        <asp:ListItem Value="3">COMPLETO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRitenutaP" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="80px">Rit. d'acconto</asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtRitenuta_PRE" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Style="z-index: 100; left: 8px; top: 88px; text-align: center"
                                        Width="15px">%</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRitenutaP" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label23" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td>
                                    <asp:Label ID="lblRitenutaC" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100px">Rit. d'acconto</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRitenuta_CONS" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td style="width: 20px">
                                    <asp:Label ID="Label34" runat="server" Style="z-index: 100; left: 8px; top: 88px; text-align: center"
                                        Width="15px">%</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRitenutaC" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblImponibileP" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100%">Imponibile non sogg. a IVA</asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtImponibileP" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label24" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td colspan="2">
                                    <asp:Label ID="lblImponibileC" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100%">Imponibile non sogg. a IVA</asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:TextBox ID="txtImponibileC" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label35" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblIMPScaricoPercipiente" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100%">INPS carico Percipiente</asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtIMPScaricoPercipiente" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label31" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td colspan="2">
                                    <asp:Label ID="lblIMPScaricoPercipiente0" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100%">INPS carico Percipiente</asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:TextBox ID="txtIMPScaricoPercipienteC" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label36" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblIMPScaricoAzienda" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100%">INPS carico Azienda</asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtIMPScaricoAzienda" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label32" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td colspan="2">
                                    <asp:Label ID="lblIMPScaricoAzienda0" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100%">INPS carico Azienda</asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:TextBox ID="txtIMPScaricoAziendaC" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblLordoP" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="80px">Prenotato lordo*</asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtLordoP" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px" Enabled="False"></asp:TextBox>
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label12" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td colspan="2">
                                    <asp:Label ID="lblLordoC" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="100%">Consuntivato lordo*</asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:TextBox ID="txtLordoC" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label37" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblImponibileP2" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="80px">Da Pagare</asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtDaPagare" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px" Enabled="False"></asp:TextBox>
                                </td>
                                <td style="width: 9px">
                                    <asp:Label ID="Label33" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td colspan="2">
                                    <asp:Label ID="lblImponibileP3" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                        Width="80px">Da Pagare</asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:TextBox ID="txtDaPagareC" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                        Width="100px" Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label38" runat="server" Style="text-align: right" Text="&#8364;"></asp:Label>
                                </td>
                                <td style="width: 15px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:TextBox ID="USCITA" runat="server" Style="visibility: hidden">0</asp:TextBox>
            <asp:TextBox ID="txtModificato" runat="server" Style="visibility: hidden">0</asp:TextBox>
            <asp:TextBox ID="txtindietro" runat="server" Style="visibility: hidden">0</asp:TextBox>
            <asp:TextBox ID="txtConnessione" runat="server" Style="visibility: hidden"></asp:TextBox>
            <asp:HiddenField ID="txtPrenotatoLordo" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="txtConsuntivatoLordo" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="txtPrenotatoLordoRIT" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="txtConsuntivatoLordoRIT" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="txtPercIVA" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPercRivalsa" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPercCassa" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPercRitenuta" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPercIVA_C" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPercCassa_C" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPercRitenuta_C" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPercRivalsa_C" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPercipiente" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPercipiente_C" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtAzienda" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtAzienda_C" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="codRitenuta" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtVisualizza" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="x" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="trovato_cmbfornitore" runat="server" Value="-1"></asp:HiddenField>
            <asp:HiddenField ID="txtStatoPagamento" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtSTATO" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtNettoP_ODL" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtNettoC_ODL" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtImponibileP_ODL" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtImponibileC_ODL" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtID_STRUTTURA" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtTipoFiltroSelect" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="txtResiduoControllo" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtStatoPF" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtFlagVOCI" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtElimina" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="ANNULLO" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="HiddenID" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="TipoAllegato" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="HiddenFieldRielabPagam" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="HiddenFieldMostraRielPag" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="HiddenFieldSolaLettura" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="HiddenFieldDataEmissione" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="HiddenFieldTipoProfessionista" runat="server" Value="0"></asp:HiddenField>
        </div>
    </form>
    <script type="text/javascript">

        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma;
                if (document.getElementById('txtVisualizza').value < 2) {
                    if (document.getElementById('txtStatoPagamento').value <= 3) {
                        chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                        if (chiediConferma == false) {
                            document.getElementById('txtModificato').value = '111';
                            //document.getElementById('USCITA').value='0';
                        };
                    };
                };
            };
        };

        function ApriAllegati() {
            window.open('ElencoAllegati.aspx?LT=0&COD=<%=vIdODL %>', 'Allegati', '');
        };


    </script>
</body>
</html>
