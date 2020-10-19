<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Appalti.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Appalti" %>

<%@ Register Src="Tab_Servizio.ascx" TagName="Tab_Servizio" TagPrefix="uc1" %>
<%@ Register Src="Tab_Appalto_generale.ascx" TagName="Tab_Appalto_generale" TagPrefix="uc2" %>
<%@ Register Src="Tab_Penali.ascx" TagName="Tab_Penali" TagPrefix="uc3" %>
<%@ Register Src="Tab_DatiAmminist.ascx" TagName="Tab_DatiAmminist" TagPrefix="uc6" %>
<%@ Register Src="Tab_Composizione.ascx" TagName="Tab_Composizione" TagPrefix="uc7" %>
<%@ Register Src="Tab_ElencoPrezzi.ascx" TagName="Tab_ElencoPrezzi" TagPrefix="uc8" %>
<%@ Register Src="Tab_SLA.ascx" TagName="Tab_SLA" TagPrefix="uc10" %>
<%@ Register Src="Tab_AppaltiForn.ascx" TagName="Tab_AppaltiForn" TagPrefix="uc11" %>
<%@ Register Src="Tab_VariazioneImporti.ascx" TagName="Tab_VariazioneImporti" TagPrefix="uc12" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="date.js"></script>
<script type="text/javascript">

    function apriEventi() {
        window.open('EventiAppalti.aspx?ID_APPALTO=' + document.getElementById('txtIdAppalto').value, "WindowPopup", "scrollbars=1, width=800px, height=600px, resizable");
    };
    var miosubmit;

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
            if (sKeyPressed1 == 13 || sKeyPressed1 == 8) {

                e.preventDefault();
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '1';
                //alert('Evitare di premere il tasto invio!\nUtilizzare gli appositi pulsanti per completare un operazione.');
                //return false;

            }
        }
    }

    function $onkeydown() {
        if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
            if (event.keyCode == 13 || event.keyCode == 8) {
                event.keyCode = 0;
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '1';
                //alert('Evitare di premere il tasto invio!\nUtilizzare gli appositi pulsanti per completare un operazione.');
                //return false;
            }
        }

    }




    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
        //        o.value = o.value.replace('.', ',');
        document.getElementById('txtModificato').value = '1';

    }


    function AutoDecimal(obj) {
        if (obj.value.replace(',', '.') > 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(4)
            document.getElementById(obj.id).value = a.replace('.', ',')
        }
    }
    function AutoDecimalPercentage(obj) {
        if (obj.value.replace(',', '.') != '') {
            if ((obj.value.replace(',', '.') >= 0) && (obj.value.replace(',', '.') <= 100)) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(3)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
            else {
                document.getElementById(obj.id).value = ''
                apriAlert('La percentuale non può essere superiore a 100!', 300, 150, 'Attenzione', null, null);

            }
        }

    }

    function Fondo() {
        if (document.getElementById("chkRitenute").checked == true) {
            document.getElementById("Tab_Appalto_generale_txtfondoritenute").style.visibility = 'visible';
            document.getElementById("Tab_Appalto_generale_lbleurFond").style.visibility = 'visible';
            document.getElementById("Tab_Appalto_generale_lblFond").style.visibility = 'visible';

        }
        else {
            document.getElementById("Tab_Appalto_generale_txtfondoritenute").style.visibility = 'hidden';
            document.getElementById("Tab_Appalto_generale_lbleurFond").style.visibility = 'hidden';
            document.getElementById("Tab_Appalto_generale_lblFond").style.visibility = 'hidden';
        }
    }

    function selezionaTutti(sender, args) {
        if (sender._checked)
            document.getElementById('hiddenSelTutti').value = "1";
        else
            document.getElementById('hiddenSelTutti').value = "0";
    };

    function AutoDecimalPercentage2(obj) {
        if (obj.value.replace(',', '.') != '') {
            if ((obj.value.replace(',', '.') >= 0) && (obj.value.replace(',', '.') <= 100)) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(0);
                if (document.getElementById('ValoreAttualeIvaMinima') != null) {
                    var minima = document.getElementById('ValoreAttualeIvaMinima').value;
                } else {
                    var minima = '';
                };

                if (document.getElementById('ValoreAttualeIvaRidotta') != null) {
                    var ridotta = document.getElementById('ValoreAttualeIvaRidotta').value;
                } else {
                    var ridotta = '';
                };

                if (document.getElementById('ValoreAttualeIvaOrdinaria') != null) {
                    var ordinaria = document.getElementById('ValoreAttualeIvaOrdinaria').value;
                } else {
                    var ordinaria = '';
                };

                if (a == 0 || a == minima || a == ridotta || a == ordinaria) {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                } else {
                    document.getElementById(obj.id).value = '';
                    var messaggio = document.getElementById('MessaggioIvaDisponibili').value;
                    apriAlert(messaggio, 300, 150, 'Attenzione', null, null);

                };
            }
            else {
                document.getElementById(obj.id).value = ''
                apriAlert('La percentuale non può essere superiore a 100!', 300, 150, 'Attenzione', null, null);

            };
        };

    };

    function AutodecPercVariaz(obj) {
        if (obj.value.replace(',', '.') != '') {

            if ((obj.value.replace(',', '.') <= 20) && (obj.value.replace(',', '.') >= -20)) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2);
                document.getElementById(obj.id).value = a.replace('.', ',');
            }
            else {
                document.getElementById(obj.id).value = '';
                apriAlert('La percentuale della variazione non può essere superiore a +/- 20%!', 300, 150, 'Attenzione', null, null);
            }
        }
    }

    function AutodecPercVariazAuto(obj) {
        if (obj.value.replace(',', '.') != '') {

            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2);
            document.getElementById(obj.id).value = a.replace('.', ',');


        }
    }
    function Activation() {
        btnAttivaContratto_Click

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
    }


    function CalcolaPercentuale(obj, valore, scriviin) {
        var risultato;
        obj.value = obj.value.replace(/\./g, '');
        valore = valore.replace(/\./g, '');
        if (obj.value.replace(/\,/g, '') > 0 && valore.replace(/\,/g, '') > 0) {
            risultato = (100 * (obj.value.replace(/\,/, '.'))) / valore.replace(/\,/, '.');
            risultato = risultato.toFixed(4);
            document.getElementById(scriviin.id).value = risultato.replace('.', ',');
        }
        else {
            document.getElementById(scriviin.id).value = 0;
            risultato = '0,0000';
        }

        if (scriviin.id.indexOf('canone') > 0) {
            document.getElementById('Tab_Servizio_percanone').value = risultato.replace('.', ',');
        }
        if (scriviin.id.indexOf('consumo') > 0) {
            document.getElementById('Tab_Servizio_perconsumo').value = risultato.replace('.', ',');
        }
    }


    function IsDate(txtDate) {
        try {
            if (txtDate.length != 10) {
                return null;
            }
            else if
             (
                isNaN(txtDate.substring(0, 2)) ||
                txtDate.substring(2, 3) != "/" ||
                isNaN(txtDate.substring(3, 5)) ||
                txtDate.substring(5, 6) != "/" ||
                isNaN(txtDate.substring(6, 15))
            ) {
                return false;
            }
            else {
                return true;
            }
        }
        catch (e) {
            return null;
        }
    }

    function CalcolaD(sender, args) {
        var dtInizio = document.getElementById('txtannoinizio').value.substr(8, 2) + '/' + document.getElementById('txtannoinizio').value.substr(5, 2) + '/' + document.getElementById('txtannoinizio').value.substr(0, 4);
        var dtFine = document.getElementById('txtannofine').value.substr(8, 2) + '/' + document.getElementById('txtannofine').value.substr(5, 2) + '/' + document.getElementById('txtannofine').value.substr(0, 4);
        CalcolaDurata(dtInizio, dtFine);
    };


    //durata in mesi
    function CalcolaDurata(inizio, fine) {

        if ((IsDate(inizio) == true && IsDate(inizio) != null) && (IsDate(fine) == true && IsDate(fine) != null)) {
            inizio = inizio.split("/");
            fine = fine.split("/");

            // Millisecondi in un giorno
            var ONE_DAY = 1000 * 60 * 60 * 24

            // Conversione di date in millisecondi
            var date1_ms = new Date(inizio[2], inizio[1] - 1, inizio[0]).getTime();
            var date2_ms = new Date(fine[2], fine[1] - 1, fine[0]).getTime();
            //        alert('' + date1_ms);
            //        alert('' + date2_ms);
            if (date1_ms <= date2_ms) {
                // Differenza in millisecondi
                var difference_ms = Math.abs(date1_ms - date2_ms)
                //        alert('' + difference_ms);

                document.getElementById('Tab_Appalto_generale_durata').value = Math.round(difference_ms / ONE_DAY) + 1;
                document.getElementById('txtdurata').value = Math.round(difference_ms / ONE_DAY) + 1;
                document.getElementById('Tab_Appalto_generale_durataMesi').value = 0

                if (fine[2] == inizio[2]) {
                    //aggiunto +1 perchè la funzione contava un mese in meno sempre
                    document.getElementById('Tab_Appalto_generale_durataMesi').value = fine[1] - inizio[1] + 1;
                }
                else {
                    var anni = fine[2] - inizio[2]
                    document.getElementById('Tab_Appalto_generale_durataMesi').value = (12 - inizio[1])
                    if (anni > 1) {
                        for (i = 1; i < anni; i++) {

                            document.getElementById('Tab_Appalto_generale_durataMesi').value = parseInt(document.getElementById('Tab_Appalto_generale_durataMesi').value) + 12

                        }
                    }
                    //aggiunto +1 perchè la funzione contava un mese in meno sempre
                    document.getElementById('Tab_Appalto_generale_durataMesi').value = parseInt(document.getElementById('Tab_Appalto_generale_durataMesi').value) + parseFloat(fine[1]) + 1

                }


            }
            else {
                document.getElementById('Tab_Appalto_generale_durata').value = "";
                document.getElementById('txtdurata').value = "";
                apriAlert('La data finale non può essere inferiore a quella iniziale!', 300, 150, 'Attenzione', null, null);

            }

        }
        else {
            document.getElementById('Tab_Appalto_generale_durata').value = "";
            document.getElementById('txtdurata').value = "";
        }
    }
    function Nascondimi2() {
        if (document.getElementById("DIV_Fornitori").style.visibility == 'visible') {
            //   document.getElementById("Tab_Fornitori1_bntAggiungiFornitore").style.visibility = 'hidden';
            // document.getElementById("Tab_Fornitori1_btnEliminaFornitore").style.visibility = 'hidden';
            //document.getElementById("Tab_Fornitori1_btnModificaFornitore").style.visibility = 'hidden';
        }

    }
    function ConfermaEliminaFornitore() {
        if (document.getElementById('Tab_Servizio_txtIdFornitore').value == "") {
            apriAlert('Attenzione...Non hai selezionato alcun fornitore!', 300, 150, "Attenzione", null, null);
            //alert('Attenzione...Non hai selezionato nessun fornitore!');
            return false;
        }
        var sicuro = confirm('Sei sicuro di voler eliminare questo fornitore?');
        if (sicuro == true) {
            document.getElementById('Tab_Servizio_txtannulloFornitore').value = '1';
        }
        else {
            document.getElementById('Tab_Servizio_txtannulloFornitore').value = '0';
        }
    };

    function AllegaFile() {
        if ((document.getElementById('txtIdAppalto').value == '') || (document.getElementById('txtIdAppalto').value == '-1')) {
            alert('E\' necessario salvare il contratto prima di allegare documenti!');
        } else {
            CenterPage('../../../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('txtIdAppalto').value, 'Allegati', 1000, 800);
        };
    };
    function CenterPage(pageURL, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    };

    function visbilityImage() {
        if (navigator.userAgent.toLowerCase().indexOf("msie") != -1) {
            var obj = document.getElementById("Tab_Servizio_RadWindowServizi_C_cmbFreqPagamento");
            if (obj.options[obj.selectedIndex].innerText == 'Manuale') {
                //oggetti visibili
                document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'visible';
                //document.getElementById("RBList1").attributes.item attributes.item(4).enabled='true';
                //RBList1.Items(4).Enabled = False

            }
            else {
                //oggetti NON visibili
                document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'hidden';
            }
        }
        else {
            var obj = document.getElementById("Tab_Servizio_RadWindowServizi_C_cmbFreqPagamento");
            if (obj.options[obj.selectedIndex].text == 'Manuale') {
                //oggetti visibili
                document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'visible';
            }
            else {
                //oggetti NON visibili
                document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'hidden';
            }

        }

    };
    function ReturnBozza() {
        var sicuro = confirm('Sei sicuro di voler riportare il contratto in BOZZA?');
        if (sicuro == true) {
            document.getElementById('ConfRitBozza').value = '1';
        }

    };
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<base target="<%=tipo%>" />--%>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>CONTRATTI</title>
    <style type="text/css">
        .panelTabsStrip {
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: White;
            border-bottom-color: #D4D4D4;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-left-style: solid;
            border-left-width: 1px;
            border-left-color: #D4D4D4;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #D4D4D4;
        }
    </style>
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/modalTelerik.js" type="text/javascript"></script>
    <script type="text/javascript">
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');
        //var tabberOptions = {'onClick':function(){alert("clicky!");}};
        var tabberOptions = {


            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;
            },
            'addLinkId': true
        };

    </script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
    <script type="text/javascript" src="../../../CENSIMENTO/function.js"></script>
    <script language="javascript" type="text/javascript">

        //window.onbeforeunload = confirmExit;

        function EliminaAppalto() {
            var sicuro = confirm('Sei sicuro di voler eliminare questo appalto? Tutti i dati andranno persi.');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            }
            else {
                document.getElementById('txtElimina').value = '0';
            }
        }

        //function faisubmit() {
        //    alert(miosubmit);
        //}

        function ReturnBozza() {
            var sicuro = confirm('Sei sicuro di voler riportare il contratto in BOZZA?');
            if (sicuro == true) {
                document.getElementById('ConfRitBozza').value = '1';
            }

        };

        //        function ConfermaEsci() {
        //            if (document.getElementById('txtModificato').value == '1') {
        //                var chiediConferma
        //                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
        //                if (chiediConferma == false) {
        //                    document.getElementById('txtModificato').value = '111';
        //                    //document.getElementById('USCITA').value='0';
        //                }
        //            }
        //        }

        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dall\'appalto premendo il pulsante ESCI. In caso contrario non sara più possibile accedere all\'appalto per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dall\'impianto premendo il pulsante ESCI. In caso contrario non sara più possibile accedere all\'appalto per un determinato periodo di tempo!";
                }
            }
        }

        function ConfermaAnnulloAppalti() {
            if (document.getElementById('Tab_Servizio_txtIdComponente').value == "") {
                apriAlert('Attenzione...Non hai selezionato alcuna riga!', 300, 150, "Attenzione", null, null);
                return false;
            }
            var sicuro = confirm('Sei sicuro di voler eliminare questa voce ?');
            if (sicuro == true) {
                document.getElementById('Tab_Servizio_txtannullo').value = '1';
            }
            else {
                document.getElementById('Tab_Servizio_txtannullo').value = '0';
            }
        }
        function ConfirmActivation() {
            ConfAttiva = window.confirm("ATTENZIONE...Si sta per attivare il contratto...\nProcedere con l\'operazione?");
            //Se sono presenti voci di servizio con iva al 20%,\nla stessa,verrà aggiornata al 21%.\n

            if (ConfAttiva == true) {
                document.getElementById("ConfAttiva").value = '1';
            }
            else {
                document.getElementById("ConfAttiva").value = '0';

            }


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


        function Fondo() {
            if (document.getElementById("chkRitenute").checked == true) {
                document.getElementById("Tab_Appalto_generale_txtfondoritenute").style.visibility = 'visible';
                document.getElementById("Tab_Appalto_generale_lbleurFond").style.visibility = 'visible';
                document.getElementById("Tab_Appalto_generale_lblFond").style.visibility = 'visible';

            }
            else {
                document.getElementById("Tab_Appalto_generale_txtfondoritenute").style.visibility = 'hidden';
                document.getElementById("Tab_Appalto_generale_lbleurFond").style.visibility = 'hidden';
                document.getElementById("Tab_Appalto_generale_lblFond").style.visibility = 'hidden';
            }
        }

        function Nascondimi() {
            //            if (document.getElementById("DIV_Appalti").style.visibility == 'visible') {
            //                document.getElementById("Tab_Servizio_imgAggiungiServ").style.visibility = 'hidden';
            //                document.getElementById("Tab_Servizio_btnEliminaAppalti").style.visibility = 'hidden';
            //                document.getElementById("Tab_Servizio_btnApriAppalti").style.visibility = 'hidden';
            //            }

        }

        function ScegliCC() {
            //if (document.getElementById("chkRitenute").checked == true) {

            //    myOpContoC.toggle();
            //}
            //else {
            document.getElementById('btnPagamento').click();
            // }

        }


        function FinisciCont() {
            var Conferma

            //            Conferma = window.confirm("ATTENZIONE...L\'operazione di chiusura contratto non è reversibile!\nVerra emesso il pagamento delle ritenute di legge (se presenti)!\nProcedere con l\'operazione?");

            Conferma = window.confirm("ATTENZIONE...L\'operazione di chiusura contratto non è reversibile!\nProcedere con l\'operazione?");

            if (Conferma == true) {
                document.getElementById("txtConfChiusura").value = '1';
            }
            else
            { document.getElementById("txtConfChiusura").value = '0'; }

        }

        function ConfSpalmVariazioneCA() {
            //+++++++++controllo l'ammontare della percentuale di variazione inserita!+++++++++++++
            if (document.getElementById("Tab_Variazioni1_PercUsataCanone").value == 20) {
                apriAlert('Hai raggiunto il limite!', 300, 150, 'Attenzione', null, null);
                return;
            }
            if (document.getElementById("Tab_Variazioni1_PercUsataCanone").value > 20) {
                apriAlert('Hai raggiunto e SUPERATO il limite!', 300, 150, 'Attenzione', null, null);
                return;
            }

            var ConfCanone
            //      *********Richiesta conferma CANONE
            if (document.getElementById("Tab_Variazioni1_Spalm_Canone").value == 1) {
                ConfCanone = window.confirm("ATTENZIONE...Consentire al sistema di ripartire automaticamente la variazione sul totale dei servizi per gli importi a Canone?");
                if (ConfCanone == true) {
                    document.getElementById("Tab_Variazioni1_ConfermaSp_Canone").value = '1';
                    //****nascondo la datagrid con i servizi
                    document.getElementById("DivImpServ").style.visibility = 'hidden';

                }
                else {
                    document.getElementById("Tab_Variazioni1_ConfermaSp_Canone").value = '0';
                    document.getElementById("Tab_Variazioni1_lblImporto").style.visibility = 'hidden';
                    document.getElementById("Tab_Variazioni1_txtPercVarCanone").style.visibility = 'hidden';
                    document.getElementById("Tab_Variazioni1_lblperc").style.visibility = 'hidden';

                }

            }
            else {
                document.getElementById("Tab_Variazioni1_lblImporto").style.visibility = 'hidden';
                document.getElementById("Tab_Variazioni1_txtPercVarCanone").style.visibility = 'hidden';
                document.getElementById("Tab_Variazioni1_lblperc").style.visibility = 'hidden';

            }
            document.getElementById('USCITA').value = '1';
            document.getElementById('Tab_Variazioni1_txtAppareV').value = '0';
            document.getElementById('Variazioni').style.visibility = 'visible';
            document.getElementById('Tab_Variazioni1_lblTitle').innerHTML = 'VARIAZIONE SERVIZI (QUINTO D\'OBBLIGO) SULL\'IMPORTO A CANONE';
            document.getElementById('Tab_Variazioni1_Tipo').value = '0';


        }


        function ConfSpalmVariazioneCO() {
            //+++++++++controllo l'ammontare della percentuale di variazione inserita!+++++++++++++
            if (document.getElementById("Tab_Variazioni1_PercUsataConsumo").value == 20) {
                apriAlert('Hai raggiunto il limite!', 300, 150, 'Attenzione', null, null);
                return;
            }
            if (document.getElementById("Tab_Variazioni1_PercUsataConsumo").value > 20) {
                apriAlert('Hai raggiunto e SUPERATO il limite!ATTENZIONE!!!', 300, 150, 'Attenzione', null, null);

                return;
            }

            var ConfConsumo

            //      *********Richiesta conferma CONSUMO

            if (document.getElementById("Tab_Variazioni1_Spalm_Consumo").value == 1) {
                ConfConsumo = window.confirm("ATTENZIONE...Consentire al sistema di ripartire automaticamente la variazione sul totale dei servizi per gli importi a Consumo?");
                if (ConfConsumo == true) {
                    document.getElementById("Tab_Variazioni1_ConfermaSp_Consumo").value = '1';
                    //****nascondo la datagrid con i servizi
                    document.getElementById("DivImpServCons").style.visibility = 'hidden';

                }
                else {
                    document.getElementById("Tab_Variazioni1_ConfermaSp_Consumo").value = '0';
                    document.getElementById("Tab_Variazioni1_lblImportoCons").style.visibility = 'hidden';
                    document.getElementById("Tab_Variazioni1_txtPercVarCons").style.visibility = 'hidden';
                    document.getElementById("Tab_Variazioni1_lblpercCons").style.visibility = 'hidden';


                }
            }

            else {
                document.getElementById("Tab_Variazioni1_lblImportoCons").style.visibility = 'hidden';
                document.getElementById("Tab_Variazioni1_txtPercVarCons").style.visibility = 'hidden';
                document.getElementById("Tab_Variazioni1_lblpercCons").style.visibility = 'hidden';
            }


            document.getElementById('USCITA').value = '1';
            document.getElementById('Tab_Variazioni1_txtAppareVC').value = '0';
            document.getElementById('VariazioniConsumo').style.visibility = 'visible';
            document.getElementById('Tab_Variazioni1_lblTitle0').innerHTML = 'VARIAZIONE SERVIZI (QUINTO D\'OBBLIGO) SULL\'IMPORTO A CONSUMO';
            document.getElementById('Tab_Variazioni1_Tipo').value = '0'

        }

        function CongElimVariaz() {
            var Conf
            if (document.getElementById("Tab_Variazioni1_id_selected").value != 0) {
                Conf = window.confirm("ATTENZIONE...Verranno eliminate tutte le quote della Variazione!\nContinuare l\'operazione?");
                if (Conf == true) {
                    document.getElementById("Tab_Variazioni1_txtElimina").value = '1';

                }

            }
            else { apriAlert('Selezionare la riga che si desidera eliminare!', 300, 150, 'Attenzione', null, null); }

        }

        //***************** SCRIPT GESTIONE TAB VARIAZIOINE LAVORI

        function CaricaVarLavoriCanone() {

            document.getElementById('USCITA').value = '1';
            document.getElementById('Tab_VariazioniLavori1_txtAppare').value = '0';
            document.getElementById('VariazioniLavori').style.visibility = 'visible';
            document.getElementById('Tab_VariazioniLavori1_lblTitle').innerHTML = 'VARIAZIONE LAVORI (QUINTO D\'OBBLIGO) SULL\'IMPORTO A CANONE';
            document.getElementById('Tab_VariazioniLavori1_txtTipo').value = '3';

        }

        function CaricaVarLavoriConsumo() {
            document.getElementById('USCITA').value = '1';
            document.getElementById('Tab_VariazioniLavori1_txtAppare').value = '0';
            document.getElementById('VariazioniLavori').style.visibility = 'visible';
            document.getElementById('Tab_VariazioniLavori1_lblTitle').innerHTML = 'VARIAZIONE LAVORI (QUINTO D\'OBBLIGO) SULL\'IMPORTO A CONSUMO';
            document.getElementById('Tab_VariazioniLavori1_txtTipo').value = '4';

        }


        function CongElimVariazLavori() {

            var Conf
            if (document.getElementById("Tab_VariazioniLavori1_id_selected").value != 0) {
                Conf = window.confirm("ATTENZIONE...Verranno eliminate tutte le quote della Variazione!\nContinuare l\'operazione?");
                if (Conf == true) {
                    document.getElementById("Tab_VariazioniLavori1_txtElimina").value = '1';

                }

            }
            else {
                apriAlert('Selezionare la variazione che si desidera eliminare!', 300, 150, 'Attenzione', null, null);
            }

        }

        function ConfEliminaElPrezzi() {
            var Conf
            if (document.getElementById("Tab_ElencoPrezzi1_idSelezionato").value != -1) {
                Conf = window.confirm("ATTENZIONE...verrà eliminata la riga selezionata!\nContinuare l\'operazione?");
                if (Conf == true) {
                    document.getElementById("Tab_ElencoPrezzi1_confElimina").value = '1';

                }

            }
            else {
                apriAlert('Selezionare la riga che si desidera eliminare!', 300, 150, 'Attenzione', null, null);
            }

        }

        function visbilityImage() {
            if (navigator.userAgent.toLowerCase().indexOf("msie") != -1) {
                var obj = document.getElementById("Tab_Servizio_RadWindowServizi_C_cmbFreqPagamento");
                if (obj.options[obj.selectedIndex].innerText == 'Manuale') {
                    //oggetti visibili
                    document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'visible';
                    //document.getElementById("RBList1").attributes.item attributes.item(4).enabled='true';
                    //RBList1.Items(4).Enabled = False

                }
                else {
                    //oggetti NON visibili
                    document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'hidden';
                }
            }
            else {
                var obj = document.getElementById("Tab_Servizio_RadWindowServizi_C_cmbFreqPagamento");
                if (obj.options[obj.selectedIndex].text == 'Manuale') {
                    //oggetti visibili
                    document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'visible';
                }
                else {
                    //oggetti NON visibili
                    document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'hidden';
                }

            }

        };
        ////////// *********************** nuove funzioni variazioni

        //        function ApriVarAutoCan() {

        //            document.getElementById('Tab_VarAutomatica1_SpalmCanone').value = 1;
        //            document.getElementById('DivVarImpCan').style.visibility = 'hidden';

        //            document.getElementById('Tab_VarAutomatica1_lblImporto').style.visibility = 'visible';
        //            document.getElementById('Tab_VarAutomatica1_lblperc').style.visibility = 'visible';
        //            document.getElementById('Tab_VarAutomatica1_txtPercVarCanone').style.visibility = 'visible';


        //            document.getElementById('VarAutoCan').style.visibility = 'visible';

        //        };


        function ApriVarImporti() {

            document.getElementById('Tab_VariazioneImporti1_SpalmCanone').value = 1;
            document.getElementById('VarImporti').style.visibility = 'visible';

        };
        function ApriVarManCanone() {
            document.getElementById('Tab_VarAutomatica1_SpalmCanone').value = 0;
            document.getElementById('DivVarImpCan').style.visibility = 'visible';

            document.getElementById('Tab_VarAutomatica1_lblImporto').style.visibility = 'hidden';
            document.getElementById('Tab_VarAutomatica1_lblperc').style.visibility = 'hidden';
            document.getElementById('Tab_VarAutomatica1_txtPercVarCanone').style.visibility = 'hidden';
            document.getElementById('VarAutoCan').style.visibility = 'visible';

        };

        function ApriVarAutoCon() {
            var ConfConsumo

            //            ConfConsumo = window.confirm("ATTENZIONE...Consentire al sistema di ripartire automaticamente  per gli importi a Consumo?");
            //            if (ConfConsumo == true) {
            //            document.getElementById('Tab_VarAutomatica1_SpalmCons').value = 1;
            //            document.getElementById('DivVarImpCon').style.visibility = 'hidden';

            //            document.getElementById('Tab_VarAutomatica1_lblImportoCons').style.visibility = 'visible';
            //            document.getElementById('Tab_VarAutomatica1_txtPercVarCons').style.visibility = 'visible';
            //            document.getElementById('Tab_VarAutomatica1_lblpercCons').style.visibility = 'visible';

            //            }
            //            else {
            //                document.getElementById('Tab_VarAutomatica1_SpalmCons').value = 0;
            //                document.getElementById('DivVarImpCon').style.visibility = 'visible';

            //                document.getElementById('Tab_VarAutomatica1_lblImportoCons').style.visibility = 'hidden';
            //                document.getElementById('Tab_VarAutomatica1_txtPercVarCons').style.visibility = 'hidden';
            //                document.getElementById('Tab_VarAutomatica1_lblpercCons').style.visibility = 'hidden';



            //            }
            document.getElementById('VarAutoCons').style.visibility = 'visible';


        };
        function ApriVarManConsumo() {
            document.getElementById('Tab_VarAutomatica1_SpalmCons').value = 0;
            document.getElementById('DivVarImpCon').style.visibility = 'visible';

            document.getElementById('Tab_VarAutomatica1_lblImportoCons').style.visibility = 'hidden';
            document.getElementById('Tab_VarAutomatica1_txtPercVarCons').style.visibility = 'hidden';
            document.getElementById('Tab_VarAutomatica1_lblpercCons').style.visibility = 'hidden';
            document.getElementById('VarAutoCons').style.visibility = 'visible';

        };
        function ConfElimVar() {
            var Conf
            if (document.getElementById("Tab_VariazioneImporti1_idSelected").value != 0) {
                Conf = window.confirm("ATTENZIONE...La variazione verrà cancellata!\nContinuare l\'operazione?");
                if (Conf == true) {
                    document.getElementById("Tab_VariazioneImporti1_hfElimina").value = '1';
                }
                else {
                    apriAlert('Selezionare la riga che si desidera eliminare!', 300, 150, 'Attenzione', null, null);
                }
            };
            window.onresize = setDimensioni;
            // Sys.Application.add_load(setDimensioni);
        }

    </script>
    <script type="text/javascript" src="../../../Condomini/prototype.lite.js"></script>
    <script type="text/javascript" src="../../../Condomini/moo.fx.js"></script>
    <script type="text/javascript" src="../../../Condomini/moo.fx.pack.js"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 174px;
        }

        .style2 {
            width: 75px;
        }

        .style5 {
            width: 58px;
        }

        .style6 {
            font-size: 10pt;
        }

        .style7 {
            font-family: Arial;
            font-weight: bold;
            font-size: 10pt;
        }
    </style>
</head>
<body class="sfondo">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="True" RestrictionZoneID="divRest"
            VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize" Skin="Web20">
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadWindowPrenotazioni" runat="server" CenterIfModal="true" Modal="True" RestrictionZoneID="divRest"
            VisibleStatusbar="False" Behavior="Pin, Move, Resize, Maximize" Width="800px"
            Height="600px" ShowContentDuringLoad="false">
        </telerik:RadWindow>
        <telerik:RadWindow ID="modalRadWindow" runat="server" CenterIfModal="true" Modal="True" RestrictionZoneID="divRest"
            VisibleStatusbar="False" Skin="Web20" ClientIDMode="Static" ShowContentDuringLoad="False">
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadWindow2" runat="server" CenterIfModal="true" Modal="True" RestrictionZoneID="divRest"
            VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize" Skin="Web20">
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadWindow3" runat="server" CenterIfModal="true" Modal="True" RestrictionZoneID="divRest"
            VisibleStatusbar="False" AutoSize="false" Behavior="Pin, Move, Resize" Skin="Web20"
            Height="600" Width="900">
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadWindowChiudiSegn" runat="server" CenterIfModal="true" Modal="True" RestrictionZoneID="divRest"
            Title="Annulla Manutenzione" VisibleStatusbar="False" AutoSize="false" Behavior="Pin, Move, Resize"
            Skin="Web20" Height="450px" Width="700px">
            <ContentTemplate>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadWindow4" runat="server" CenterIfModal="true" Modal="True" RestrictionZoneID="divRest"
            Title="Stampa anticipo" VisibleStatusbar="False" AutoSize="false" Behavior="Pin, Move, Resize"
            Skin="Web20" Height="450px" Width="700px">
            <ContentTemplate>
                <asp:Panel ID="PanelAnticipo" runat="server" CssClass="sfondo">
                    <table border="0" cellpadding="4" cellspacing="4" width="90%">
                        <tr>
                            <td colspan="2" class="TitoloModulo">
                                <asp:Label ID="ADP" runat="server" Text="Stampa anticipo contrattuale"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="ImgConferma" runat="server" Text="Conferma" Style="cursor: pointer"
                                    ToolTip="Stampa" />
                                <td>
                                    <asp:Button
                                        ID="btn_ChiudiAppalti" runat="server" Text="Esci"
                                        OnClientClick="closeWindow(null, null, 'RadWindow4');"
                                        Style="cursor: pointer" ToolTip="Esci" />
                                </td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Blue"
                                    Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="Label12" Text="Data di emissione*" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td style="width: 80%">
                                <asp:TextBox ID="DataEmissione" runat="server" Width="70px" MaxLength="10" Font-Names="Arial"
                                    Font-Size="9pt"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="DataEmissione"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="arial" Font-Size="12pt" ForeColor="#CC0000"
                                    ToolTip="Modificare la data" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" Text="Modalità di pagamento" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtModalitaPagamento" runat="server" Width="300px" Font-Names="Arial"
                                    Font-Size="9pt" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" Text="Condizione di pagamento" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtCondizionePagamento" runat="server" Width="300px" Font-Names="Arial"
                                    Font-Size="9pt" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label15" Text="Data di scadenza" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataScadenza" runat="server" Width="70px" MaxLength="10" Font-Names="Arial"
                                    Font-Size="9pt"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataScadenza"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="arial" Font-Size="12pt" ForeColor="#CC0000"
                                    ToolTip="Modificare la data" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label16" Text="Descrizione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescrizioneBreve" runat="server" Width="500px" MaxLength="1000"
                                    Font-Names="Arial" Font-Size="9pt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 200px;">
                                <asp:HiddenField runat="server" ID="idCondizione" Value="NULL" />
                                <asp:HiddenField runat="server" ID="idModalita" Value="NULL" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </telerik:RadWindow>
        <div>
            <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
                EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="100" Position="BottomRight"
                OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
            </telerik:RadNotification>
        </div>
        <div style="width: 100%; overflow: hidden">
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="left: 168px; position: absolute; top: 16px" Text="Label"
                Visible="False" Width="624px"></asp:Label>
            <div id="divRest">
                <table style="width: 100%">
                    <tr>
                        <td class="TitoloModulo">Contratto
                        </td>
                    </tr>
                    <tr>
                        <td id="Td1">

                            <telerik:RadButton ID="btnIndietro" runat="server" Text="Indietro" ToolTip="Indietro"
                                OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';Blocca_SbloccaMenu(0);}"
                                CausesValidation="False">
                            </telerik:RadButton>

                            <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}"
                                CausesValidation="False">
                            </telerik:RadButton>

                            <telerik:RadButton ID="btnElimina" runat="server" Text="Elimina" ToolTip="Elimina appalto visualizzato"
                                OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';  EliminaAppalto();}"
                                CausesValidation="False">
                            </telerik:RadButton>

                            <telerik:RadButton ID="imgEventi" runat="server" Text="Eventi" ToolTip="Eventi Scheda Appalto"
                                AutoPostBack="false" OnClientClicking="function(sender, args){apriEventi();}"
                                CausesValidation="False">
                            </telerik:RadButton>

                            <telerik:RadButton ID="RadButtonAllegati" runat="server" Text="Allegati" ToolTip="Visualizza allegati"
                                AutoPostBack="false" OnClientClicking="function(sender, args){AllegaFile();}"
                                CausesValidation="False">
                            </telerik:RadButton>

                            <%--<td>
                                <telerik:RadButton ID="Imgallegati" runat="server" Text="Allegati" ToolTip="Visualizza allegati"
                                    AutoPostBack="false" OnClientClicking="function(sender, args){window.open('ElencoAllegati.aspx?T=3&COD=' + document.getElementById('txtIdAppalto').value, 'Allegati', '');}"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="imginvioallegati" runat="server" Text="Allega" ToolTip="Allega documento"
                                    AutoPostBack="false" OnClientClicking="function(sender, args){window.open('../../../InvioAllegato.aspx?T=3&ID=' + document.getElementById('txtIdAppalto').value, 'Allegati', '');}"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>--%>

                            <telerik:RadButton ID="btnAttivaContratto" runat="server" Text="Attiva contratto"
                                ToolTip="Attiva il contratto" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}"
                                CausesValidation="False" Enabled="false">
                            </telerik:RadButton>

                            <telerik:RadButton ID="btnFineContratto" runat="server" Text="Fine Contratto" ToolTip="Fine Contratto ed emissione Lettere Pagamento"
                                OnClientClicking="function(sender, args){ScegliCC();return false;}" CausesValidation="False" AutoPostBack="false"
                                Enabled="false">
                            </telerik:RadButton>

                            <telerik:RadButton ID="btnVisRate" runat="server" Text="Vis. Prenotazioni" ToolTip="Visualizza le Prenotazioni dei pagamenti sul contratto"
                                OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';document.getElementById('txtModificato').value ='1';VisPrenotazioni();return false;}"
                                CausesValidation="False" AutoPostBack="false">
                            </telerik:RadButton>

                            <telerik:RadButton ID="btnImputazionePulizie" runat="server" Text="Imputazione pulizie" ToolTip="Visualizza le Prenotazioni dei pagamenti sul contratto"
                                OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';document.getElementById('txtModificato').value ='1';VisPulizie();return false;}"
                                CausesValidation="False" AutoPostBack="false">
                            </telerik:RadButton>

                            <telerik:RadButton ID="btnImputazioneAscensori" runat="server" Text="Imputazione ascensori" ToolTip="Visualizza le Prenotazioni dei pagamenti sul contratto"
                                OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';document.getElementById('txtModificato').value ='1';VisAscensori();return false;}"
                                CausesValidation="False" AutoPostBack="false">
                            </telerik:RadButton>

                            <telerik:RadButton ID="imgUscita" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="function(sender, args){Blocca_SbloccaMenu(0);document.getElementById('USCITA').value='1';}"
                                CausesValidation="False">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; overflow: hidden" class="FontTelerik">
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server">Numero Repertorio*</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtnumero" runat="server" MaxLength="50" Width="155px" Font-Size="8pt"></asp:TextBox>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server">Data Stipula*</asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtdatarepertorio" runat="server" WrapperTableCaption=""
                                MaxDate="01/01/9999" DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
                                Width="110">
                                <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa">
                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                </DateInput>
                                <Calendar ID="Calendar3" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server">Data inizio*</asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtannoinizio" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110"
                                ClientEvents-OnDateSelected="CalcolaD">
                                <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                </DateInput>
                                <Calendar ID="Calendar1" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server">Data fine</asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtannofine" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110"
                                ClientEvents-OnDateSelected="CalcolaD">
                                <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                </DateInput>
                                <Calendar ID="Calendar2" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server">Durata giorni</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdurata" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="3"
                                Style="z-index: 107; left: 109px; top: 67px" Width="25px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" Width="31px">CUP</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCup" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="15"
                                Style="z-index: 107; left: 109px; top: 67px" Width="155px"></asp:TextBox>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server">CIG*</asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCIG" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50"
                                Style="z-index: 107; left: 109px; top: 67px" Width="155px"></asp:TextBox>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblProgr" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td colspan="2">
                            <asp:Label runat="server" ID="lblEsercizioF" Font-Names="Arial" Font-Size="8pt" CssClass="txtMia"
                                Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server">Descrizione*</asp:Label>
                        </td>
                        <td colspan="10">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtdescrizione" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                            Height="40px" MaxLength="50" Style="z-index: 107; left: 109px; top: 67px"
                                            TextMode="MultiLine" Width="95%"></asp:TextBox>
                                    </td>
                                    <td style="width: 60%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td style="width: 15%">
                                                    <asp:Label ID="Label10" runat="server">Tipo*</asp:Label>
                                                </td>
                                                <td style="width: 85%">
                                                    <telerik:RadComboBox ID="DropDownListTipo" Width="95%" AppendDataBoundItems="true"
                                                        Enabled="false" Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label22" runat="server" Width="100%">Struttura</asp:Label>
                        </td>
                        <td colspan="10">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtFiliale" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50"
                                            ReadOnly="True" Style="z-index: 107; left: 109px; top: 67px" Width="95%"></asp:TextBox>
                                    </td>
                                    <td style="width: 60%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td style="width: 15%">
                                                    <asp:Label ID="Label1" runat="server" Width="100%">Lotto</asp:Label>
                                                </td>
                                                <td style="width: 85%">
                                                    <asp:TextBox ID="txtlotto" runat="server" Width="95%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server">Fornitore</asp:Label>
                        </td>
                        <td colspan="10">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 40%">
                                        <telerik:RadComboBox ID="cmbfornitore" Width="95%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                        <%--&nbsp;<asp:ImageButton ID="ImgNuovo" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/Plan/Immagini/40px-Crystal_Clear_action_edit_add.png"
                                                TabIndex="-1" OnClientClick="window.showModalDialog('SceltaFornitore.aspx?X=1',window,'status:no;dialogWidth:800px;dialogHeight:600px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');"
                                                ToolTip="Aggiungi nuovo fornitore" Style="height: 18px;" Visible="False" />--%>
                                    </td>
                                    <td style="width: 60%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td style="width: 15%">
                                                    <asp:Label ID="Label33" runat="server">Dir. Lav.</asp:Label>
                                                </td>
                                                <td style="width: 85%">
                                                    <telerik:RadComboBox ID="cmbGestore" Width="95%" AppendDataBoundItems="true" Filter="Contains"
                                                        runat="server" AutoPostBack="false" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                        LoadingMessage="Caricamento...">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Width="100%">Modalità Pag.</asp:Label>
                        </td>
                        <td colspan="10">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 40%">
                                        <telerik:RadComboBox ID="cmbModalitaPagamento" Width="95%" AppendDataBoundItems="true"
                                            Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                            HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="width: 60%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td style="width: 15%">
                                                    <asp:Label ID="Label8" runat="server" Width="100%">Cond. Pag.</asp:Label>
                                                </td>
                                                <td style="width: 85%">
                                                    <telerik:RadComboBox ID="cmbCondizionePagamento" Width="95%" AppendDataBoundItems="true"
                                                        Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label35" runat="server" Width="99px">Indirizzo Forn.</asp:Label>
                        </td>
                        <td colspan="10">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 40%">
                                        <telerik:RadComboBox ID="cmbIndirizzoF" Width="50%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelIBAN" runat="server">IBAN</asp:Label>
                                    </td>
                                    <td style="text-align: left" width="260">
                                        <asp:CheckBoxList ID="chkIbanF" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Style="left: 334px; top: 251px" Width="96%">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label23" runat="server" Width="80px">Stato Contratto</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStato" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="#1c2466" Text="BOZZA"></asp:Label>
                        </td>
                        <td colspan="8">
                            <table style="width: 80%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnTornBozza" runat="server" Text="Torna in BOZZA" ToolTip="Riporta in bozza il contratto, per apportarvi modifiche"
                                            OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';ReturnBozza();}"
                                            CausesValidation="False" Visible="false">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkRitenute" runat="server" Style="font-family: Segoe UI; font-size: 12px"
                                            Text="Rit. Legge" onClick="Fondo();" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkPenale" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Penale"
                                            onClick="Fondo();" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="ChkModuloFO" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            Text="Abil. Mod. Fornitori" onClick="Fondo();" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="ChkGestEsternaMF" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            Text="Gest. Est. M.F." onClick="Fondo();" />
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBoxAnticipo" AppendDataBoundItems="true" Filter="Contains" runat="server"
                                            AutoPostBack="true" ResolvedRenderMode="Classic"
                                            HighlightTemplatedItems="true" LoadingMessage="Caricamento..." Enabled="false"
                                            Width="120px">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="Nessun anticipo" />
                                                <%--<telerik:RadComboBoxItem Value="1" Text="Rate costanti" />--%>
                                                <telerik:RadComboBoxItem Value="2" Text="20% su SAL" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBoxVoci" AppendDataBoundItems="true" Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                            HighlightTemplatedItems="true" LoadingMessage="Caricamento..." Enabled="false"
                                            Width="250px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <%--<asp:Label ID="Label111" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                                                ForeColor="Black" Style="">Nr rate</asp:Label>--%>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxNumeroRate" runat="server" MinValue="1"
                                            Enabled="false" MaxValue="50" Width="50px" Font-Names="Arial" Font-Size="9pt"
                                            Type="Number" NumberFormat-DecimalDigits="0" Visible="false">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip runat="server" ID="RadTabStrip" Orientation="HorizontalTop"
                    Width="100%" MultiPageID="RadMultiPage1" ShowBaseLine="true" ScrollChildren="true"
                    OnClientTabSelected="tabSelezionato">
                    <Tabs>
                        <telerik:RadTab runat="server" PageViewID="RadPageView1" Text="Servizi" Value="Servizi">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageView2" Text="Fornitori" Value="Fornitori">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageView3" Text="Importi" Value="Importi">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageView6" Text="Penali" Value="Penali">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageView7" Text="Dati amministrativi"
                            Value="DatiAmministrativi">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageView8" Text="Composizione" Value="Composizione">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageView9" Text="Elenco prezzi " Value="ElencoPrezzi">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageView11" Text="SLA" Value="SLA">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageView12" Text="Variazioni" Value="Variazioni">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageView13" Text="Schede di imputazione" Value="SchedeImputazione">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage runat="server" ID="RadMultiPage1" CssClass="multiPage" Width="100%"
                    ScrollChildren="true">
                    <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab1">
                            <uc1:Tab_Servizio ID="Tab_Servizio" runat="server" />
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView2" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab2">
                            <uc11:Tab_AppaltiForn ID="Tab_Fornitori1" runat="server" />
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView3" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab3">
                            <uc2:Tab_Appalto_generale ID="Tab_Appalto_generale" runat="server" />
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView6" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab6">
                            <uc3:Tab_Penali ID="Tab_Penali1" runat="server" />
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView7" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab7">
                            <uc6:Tab_DatiAmminist ID="Tab_DatiAmminist1" runat="server" />
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView8" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab8">
                            <uc7:Tab_Composizione ID="Tab_Composizione1" runat="server" />
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView9" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab9">
                            <uc8:Tab_ElencoPrezzi ID="Tab_ElencoPrezzi1" runat="server" />
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView11" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab11">
                            <uc10:Tab_SLA ID="Tab_SLA" runat="server" />
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView12" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab12">
                            <uc12:Tab_VariazioneImporti ID="Tab_VariazioneImporti1" runat="server" />
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView13" CssClass="panelTabsStrip">
                        <asp:Panel runat="server" ID="tab13">
                            <table>
                                <tr>
                                    <td style="height: 15px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Elenco prezzi
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbElencoPrezzi" Width="300px" AppendDataBoundItems="true"
                                            Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                            HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>

            <telerik:RadWindow ID="RadWindowServizi" runat="server" CenterIfModal="true" Modal="true"
                Title="Gestione servizi" Width="680px" Height="330px" VisibleStatusbar="false"
                Behaviors="Pin, Maximize, Move, Resize">
                <ContentTemplate>
                    <telerik:RadWindowManager ID="RadWindowManager2" runat="server" Localization-OK="Ok"
                        Localization-Cancel="Annulla">
                    </telerik:RadWindowManager>
                    <asp:Panel runat="server" ID="PanelServiziVoci" Style="height: 100%;" class="sfondo">
                        <table class="sfondo">
                        </table>

                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadWindow>


            <div id="MyTab2">
            </div>
            <br />
            <br />
            <div style="border: thin solid #FF0000; position: absolute; z-index: 10; top: 15px; left: 4px; height: 500px; width: 781px; visibility: hidden; background-color: #E8EAEC;"
                id="DivContCorrente">
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td class="style7">SELEZIONA IL CONTO CORRENTE PER L'EMISSIONE DEL PAGAMENTO DELLE RITENUTE DI LEGGE(se
                        presenti)
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:DropDownList ID="CmbContoCorrente" runat="server" Width="350px">
                                <asp:ListItem>12000X01</asp:ListItem>
                                <asp:ListItem>13000X01</asp:ListItem>
                                <asp:ListItem>14000X01</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td align="right">
                                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                            <asp:ImageButton ID="btnPagamento" runat="server" ImageUrl="~/NuoveImm/Img_Conferma1.png"
                                                ToolTip="Emissione del Pagamento" OnClientClick="document.getElementById('USCITA').value='1';FinisciCont();" />
                                        </span></strong>
                                    </td>
                                    <td align="right">
                                        <img alt="Annulla" src="../../../NuoveImm/Img_AnnullaVal.png" onclick="myOpContoC.toggle();"
                                            style="cursor: pointer;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="display: none">
                <asp:Button ID="btnTerminaAttivazione" runat="server" />
            </div>

            <asp:HiddenField ID="ValoreAttualeIvaOrdinaria" runat="server" Value="" />
            <asp:HiddenField ID="ValoreAttualeIvaMinima" runat="server" Value="" />
            <asp:HiddenField ID="ValoreAttualeIvaRidotta" runat="server" Value="" />
            <asp:HiddenField ID="MessaggioIvaDisponibili" runat="server" Value="" />
            <asp:HiddenField ID="USCITA" runat="server" Value="0" />
            <asp:HiddenField ID="txtElimina" runat="server" Value="0" />
            <asp:HiddenField ID="txtIdAppalto" runat="server" Value="0" />
            <asp:HiddenField ID="txttab" runat="server" Value="0" />
            <asp:HiddenField ID="txtindietro" runat="server" Value="0" />
            <asp:HiddenField ID="txtConnessione" runat="server" Value="0" />
            <asp:HiddenField ID="txtIDA" runat="server" Value="0" />
            <asp:HiddenField ID="txtConfChiusura" runat="server" Value="0" />
            <asp:HiddenField ID="TipoLotto" runat="server" Value="" />
            <asp:HiddenField ID="IdStruttura" runat="server" Value="0" />
            <asp:HiddenField ID="idfornitore" runat="server" Value="0" />
            <asp:HiddenField ID="x" runat="server" Value="0" />
            <asp:HiddenField ID="txtIdPianoFinanziario" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="idStatoPf" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="div_Variaz_Visibility" runat="server" Value="0" />
            <br />
            <asp:HiddenField ID="txtidlotto" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="SOLO_LETTURA" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="txtModificato" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="trovato_cmbfornitore" runat="server" Value="-1" />
            <asp:HiddenField ID="SalvaAttiva" runat="server" Value="0" />
            <asp:HiddenField ID="ConfAttiva" runat="server" Value="0" />
            <asp:HiddenField ID="ModificaRitenute" runat="server" Value="0" EnableViewState="true" />
            <asp:HiddenField ID="idPagRitLegge" runat="server" Value="0" />
            <asp:HiddenField ID="ConfRitBozza" runat="server" Value="0" />
            <asp:HiddenField ID="HFGriglia" runat="server" />
            <asp:HiddenField ID="HFTAB" runat="server" />
            <asp:HiddenField ID="HFAltezzaTab" runat="server" />
            <asp:HiddenField ID="HiddenFieldAttivaContratto" runat="server" Value="" />
            <asp:HiddenField ID="HiddenTabSelezionato" runat="server" Value="0" />
            <asp:HiddenField ID="numTab" runat="server" Value="11" />
            <asp:HiddenField ID="HFAltezzaFGriglie" runat="server" />
            <asp:HiddenField ID="HiddenResiduoCanone" runat="server" Value="0" />
            <asp:HiddenField ID="HiddenResiduoConsumo" runat="server" Value="0" />
            <asp:HiddenField ID="TipoAllegato" runat="server" Value="0" />
            <asp:HiddenField ID="HiddenFieldIdPagamento" runat="server" Value="0" />
            <asp:HiddenField ID="HiddenFieldIdPrenotazione" runat="server" Value="0" />
        </div>
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();
        //************GESTIONE VISIBILITA CAMPO DI TESTO FONDO RITENUTE*********
        if (document.getElementById("chkRitenute").checked == true) {
            document.getElementById("Tab_Appalto_generale_txtfondoritenute").style.visibility = 'visible';
            document.getElementById("Tab_Appalto_generale_lbleurFond").style.visibility = 'visible';
            document.getElementById("Tab_Appalto_generale_lblFond").style.visibility = 'visible';
            document.getElementById("Tab_Appalto_generale_btnInfoRitLegge").style.visibility = 'visible';

        }
        else {
            document.getElementById("Tab_Appalto_generale_txtfondoritenute").style.visibility = 'hidden';
            document.getElementById("Tab_Appalto_generale_lbleurFond").style.visibility = 'hidden';
            document.getElementById("Tab_Appalto_generale_lblFond").style.visibility = 'hidden';
            document.getElementById("Tab_Appalto_generale_btnInfoRitLegge").style.visibility = 'hidden';

        }
        //********************************************************************

        //        if (document.getElementById("DIV_Appalti").style.visibility == 'visible') {
        //            document.getElementById("Tab_Servizio_imgAggiungiServ").style.visibility = 'hidden';
        //            document.getElementById("Tab_Servizio_btnEliminaAppalti").style.visibility = 'hidden';
        //            document.getElementById("Tab_Servizio_btnApriAppalti").style.visibility = 'hidden';
        //        }

        if (document.getElementById("DIV_Fornitori")) {
            if (document.getElementById("DIV_Fornitori").style.visibility == 'visible') {
                //  document.getElementById("Tab_Fornitori1_bntAggiungiFornitore").style.visibility = 'hidden';
                //document.getElementById("Tab_Fornitori1_btnEliminaFornitore").style.visibility = 'hidden';
                //document.getElementById("Tab_Fornitori1_btnModificaFornitore").style.visibility = 'hidden';
            }
        }
        if (document.getElementById("SOLO_LETTURA").value == 1) {
            //  document.getElementById("Tab_Servizio_imgAggiungiServ").style.visibility = 'hidden';
            // if (document.getElementById("Tab_Fornitori1_bntAggiungiFornitore")) { document.getElementById("Tab_Fornitori1_bntAggiungiFornitore").style.visibility = 'hidden'; }
            //if (document.getElementById("Tab_Fornitori1_btnEliminaFornitore")) { document.getElementById("Tab_Fornitori1_btnEliminaFornitore").style.visibility = 'hidden'; }
            //if (document.getElementById("Tab_Fornitori1_btnModificaFornitore")) { document.getElementById("Tab_Fornitori1_btnModificaFornitore").style.visibility = 'hidden'; }
            //    document.getElementById("Tab_Variazioni1_imgAggiungiServ").style.visibility = 'hidden';
            //   document.getElementById("Tab_Variazioni1_imgAggiungiLavori").style.visibility = 'hidden';
            //  document.getElementById("Tab_VariazioniLavori1_imgAggiungiLavCan").style.visibility = 'hidden';
            //  document.getElementById("Tab_VariazioniLavori1_imgAggiungiLavCons").style.visibility = 'hidden';
            document.getElementById("Tab_ElencoPrezzi1_imgAggiungiServ").style.visibility = 'hidden';

            //document.getElementById('imginvioallegati').style.visibility = 'hidden';
            //$("#imginvioallegati").attr("disabled", "disabled");


            // document.getElementById("Tab_VarAutomatica1_imgAddAutoCanone").style.visibility = 'hidden';
            //  document.getElementById("Tab_VarAutomatica1_imgAddAutoConsumo").style.visibility = 'hidden';

            // document.getElementById("Tab_VarAutomatica1_imgAddManCan").style.visibility = 'hidden';
            // document.getElementById("Tab_VarAutomatica1_imgAddManCons").style.visibility = 'hidden';

        }

        if (document.getElementById("lblStato").innerHTML == 'ATTIVO') {
            // document.getElementById("Tab_Servizio_imgAggiungiServ").style.visibility = 'hidden';
            //document.getElementById("Tab_Fornitori1_bntAggiungiFornitore").style.visibility = 'hidden';
        }

        //******************APPALTI LAVORI********************
        //        if (document.getElementById('Tab_VariazioniLavori1_txtAppare').value != '1') {
        //            document.getElementById('VariazioniLavori').style.visibility = 'hidden';
        //        }
        //        else {
        //            document.getElementById('VariazioniLavori').style.visibility = 'visible';
        //        }
        //document.getElementById('dvvvPre').style.visibility = 'hidden';


        if (document.getElementById('Tab_ElencoPrezzi1_divVisibility').value != '1') {
            document.getElementById('DivPrezzi').style.visibility = 'hidden';
        }
        else {
            document.getElementById('DivPrezzi').style.visibility = 'visible';
        }

        if (navigator.userAgent.toLowerCase().indexOf("msie") != -1) {
            var obj = document.getElementById("Tab_Servizio_RadWindowServizi_C_cmbFreqPagamento");
            if (obj.options[obj.selectedIndex].innerText == 'Manuale') {
                //oggetti visibili
                document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'visible';

            }
            else {
                //oggetti NON visibili
                document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'hidden';
            }
        }
        else {
            var obj = document.getElementById("Tab_Servizio_RadWindowServizi_C_cmbFreqPagamento");
            if (obj.options[obj.selectedIndex].text == 'Manuale') {
                //oggetti visibili
                document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'visible';
            }
            else {
                //oggetti NON visibili
                document.getElementById("Tab_Servizio_RadWindowServizi_C_btnDate").style.visibility = 'hidden';
            }

        }
            //        if (document.getElementById("SalvaAttiva").value != 0) {
            //            document.getElementById('HiddenFieldAttivaContratto').value = 1;
            //            var oWnd = $find('RadWindow1');
            //            oWnd.setUrl('RiepPrenotazioni.aspx?IdAppalto=<%=vIdAppalti %>&IDCON=<%= lIdConnessione %>&IMPMAS=' + document.getElementById("Tab_Appalto_generale_txtImpTotPlusOneriCan").value.replace('.', '') + '&ATTCONTR=' + document.getElementById('HiddenFieldAttivaContratto').value);
        //            oWnd.show();           
        //        }

        myOpContoC = new fx.Opacity('DivContCorrente', { duration: 200 });
        myOpContoC.hide();

        function ApriModalVarCof() {
            var oWnd = $find('RadWindow3');
            oWnd.setUrl('VariazConfig.aspx?IDLOTTO=' + document.getElementById('txtidlotto').value + '&TIPO=' + document.getElementById('TipoLotto').value + '&IdAppalto=<%=vIdAppalti%>&IDCON=<%=lIdConnessione %>');
                oWnd.show();
            };

            function VisPrenotazioni() {
                document.getElementById('HiddenFieldAttivaContratto').value = 0;
            	openModalInRadClose('RadWindowPrenotazioni', 'RiepPrenotazioni.aspx?IdAppalto=<%=vIdAppalti %>&IDCON=<%= lIdConnessione %>&SL=' + document.getElementById("SOLO_LETTURA").value + '&IMPMAS=' + document.getElementById("Tab_Appalto_generale_txtImpTotPlusOneriCan").value.replace('.', '') + '&ATTCONTR=' + document.getElementById('HiddenFieldAttivaContratto').value, 500, 500, 1);
            }
            function VisPulizie() {
                document.getElementById('HiddenFieldAttivaContratto').value = 0;
                var elencoPrezzi = $find('cmbElencoPrezzi').get_value();
                openModalInRadClose('RadWindowPrenotazioni', 'ImputazionePulizie.aspx?IdAppalto=<%=vIdAppalti %>&IDCON=<%= lIdConnessione %>&SL=' + document.getElementById("SOLO_LETTURA").value + '&IMPMAS=' + document.getElementById("Tab_Appalto_generale_txtImpTotPlusOneriCan").value.replace('.', '') + '&ATTCONTR=' + document.getElementById('HiddenFieldAttivaContratto').value + '&ELENCOPREZZI=' + elencoPrezzi, 500, 500, 1);
            }
            function VisAscensori() {
                document.getElementById('HiddenFieldAttivaContratto').value = 0;
                var elencoPrezzi = $find('cmbElencoPrezzi').get_value();
                openModalInRadClose('RadWindowPrenotazioni', 'ImputazioneAscensori.aspx?IdAppalto=<%=vIdAppalti %>&IDCON=<%= lIdConnessione %>&SL=' + document.getElementById("SOLO_LETTURA").value + '&IMPMAS=' + document.getElementById("Tab_Appalto_generale_txtImpTotPlusOneriCan").value.replace('.', '') + '&ATTCONTR=' + document.getElementById('HiddenFieldAttivaContratto').value + '&ELENCOPREZZI=' + elencoPrezzi, 500, 500, 1);
        }
        function ApriModalScadenze() {
            if (document.getElementById("Tab_Servizio_idvoce").value > 0) {
                var oWnd = $find('RadWindow1');
                oWnd.setUrl('ScadenzeAppalto.aspx?IDAPPALTO=<%=vIdAppalti %>&IDCONNECTION=<%=lIdConnessione %>&RATAMIN=' + document.getElementById("txtannoinizio").value + '&RATAMAX=' + document.getElementById("txtannofine").value + '&STATO=' + document.getElementById("lblStato").innerHTML + '&IDVOCE=' + document.getElementById("Tab_Servizio_idvoce").value);
                    oWnd.show();
            } else {
                apriAlert('Selezionare la voce di servizio!', 300, 150, 'Attenzione', null, null);

            }
        };
        function RiepRitLegge() {
            if (document.getElementById('ModificaRitenute').value == '1') {
                var oWnd = $find('RadWindow2');
                oWnd.setUrl('RiepRitLegge.aspx?SL=0&IDAPP=<%=vIdAppalti %>');
                    oWnd.show();
                } else {
                    var oWnd = $find('RadWindow2');
                    oWnd.setUrl('RiepRitLegge.aspx?SL=1&IDAPP=<%=vIdAppalti %>');
                    oWnd.show();
                };


            };
            //        if (document.getElementById('Tab_VarAutomatica1_hfRestaVisible') == 1) {
            //            document.getElementById('VarAutoCan').style.visibility = 'visible';
            //        };




    </script>
    <script language="javascript" type="text/javascript">
            function AllegaFile() {
                if ((document.getElementById('txtIdAppalto').value == '') || (document.getElementById('txtIdAppalto').value == '-1')) {
                    apriAlert('E\' necessario salvare il contratto prima di allegare documenti!', 300, 150, 'Attenzione', null, null);
                } else {
                    CenterPage('../../../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('txtIdAppalto').value, 'Allegati', 1000, 800);
                };

                //window.open('ElencoAllegati.aspx?T=3&COD=' + document.getElementById('txtIdAppalto').value, 'AllegatiContratto', 'scrollbars=1, width=800px, height=600px, resizable');
                return false;
            };
            function refreshPage(arg) {
                if (document.getElementById('Tab_Appalto_generale_btnInfoRitLegge2')) {
                    document.getElementById('Tab_Appalto_generale_btnInfoRitLegge2').click();
                };
            };
            function refreshPageComp(arg) {
                if (document.getElementById('Tab_Composizione1_ricaricaComposizione')) {
                    document.getElementById('Tab_Composizione1_ricaricaComposizione').click();
                };
            };

            function ApriVarImporti() {

                document.getElementById('Tab_VariazioneImporti1_SpalmCanone').value = 1;
                document.getElementById('VarImporti').style.visibility = 'visible';

            };
            function ConfElimVar() {
                var Conf;
                if (document.getElementById("Tab_VariazioneImporti1_idSelected").value != 0) {
                    Conf = window.confirm("ATTENZIONE...La variazione verrà cancellata!\nContinuare l\'operazione?");
                    if (Conf == true) {
                        document.getElementById("Tab_VariazioneImporti1_hfElimina").value = '1';
                    }
                    else {
                        apriAlert('Selezionare la riga che si desidera eliminare!', 300, 150, 'Attenzione', null, null);
                    }
                };
            };

            window.onresize = setDimensioni;
            Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
