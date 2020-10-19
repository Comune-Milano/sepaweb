<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AppaltiNP.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_AppaltiNP" %>

<%@ Register Src="Tab_ImportiNP.ascx" TagName="Tab_ImportiNP" TagPrefix="uc1" %>

<%@ Register Src="Tab_VociNPl.ascx" TagName="Tab_VociNPl" TagPrefix="uc2" %>

<%@ Register Src="Tab_DatiAmminist.ascx" TagName="Tab_DatiAmminist" TagPrefix="uc3" %>

<%@ Register Src="Tab_Penali.ascx" TagName="Tab_Penali" TagPrefix="uc4" %>


<%@ Register Src="Tab_VariazioniNP.ascx" TagName="Tab_VariazioniNP" TagPrefix="uc5" %>
<%@ Register Src="Tab_VariazLavNPl.ascx" TagName="Tab_VariazLavNPl" TagPrefix="uc6" %>
<%@ Register Src="Tab_SLA.ascx" TagName="Tab_SLA" TagPrefix="uc7" %>

<%@ Register Src="Tab_VariazioneImportiNP.ascx" TagName="Tab_VariazioneImporti" TagPrefix="uc12" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appalti Non Patrimoniali</title>
    <script type="text/javascript" src="../../../Condomini/prototype.lite.js"></script>
    <script type="text/javascript" src="../../../Condomini/moo.fx.js"></script>
    <script type="text/javascript" src="../../../Condomini/moo.fx.pack.js"></script>

    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>

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
        function apriEventi() {
            window.open('EventiAppalti.aspx?ID_APPALTO=' + document.getElementById('txtIdAppalto').value, "WindowPopup", "scrollbars=1, width=800px, height=600px, resizable");
        };
        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        }

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
            if (document.getElementById('Tab_vociNPl1_txtIdComponente').value == "") {
                alert('Attenzione...Non hai selezionato alcuna riga!');
                return false;
            }
            var sicuro = confirm('Sei sicuro di voler eliminare questa voce ?');
            if (sicuro == true) {
                document.getElementById('Tab_vociNPl1_txtannullo').value = '1';
            }
            else {
                document.getElementById('Tab_vociNPl1_txtannullo').value = '0';
            }
        }

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
        function AutoDecimalPercentage(obj) {
            if (obj.value.replace(',', '.') != '') {
                if ((obj.value.replace(',', '.') >= 0) && (obj.value.replace(',', '.') <= 100)) {
                    var a = obj.value.replace(',', '.');
                    a = parseFloat(a).toFixed(4)
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }
                else {
                    document.getElementById(obj.id).value = ''
                    alert('La percentuale non può essere superiore a 100!')
                }
            }

        }
        function AutodecPercVariaz(obj) {
            if (obj.value.replace(',', '.') != '') {

                if ((obj.value.replace(',', '.') <= 20) && (obj.value.replace(',', '.') >= -20)) {
                    var a = obj.value.replace(',', '.');
                    a = parseFloat(a).toFixed(2);
                    document.getElementById(obj.id).value = a.replace('.', ',');
                }
                else {
                    document.getElementById(obj.id).value = ''
                    alert('La percentuale della variazione non può essere superiore a +/- 20%!');
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
                document.getElementById('Tab_VociNPl1_RadWindowServizi_C_perconsumo').value = risultato.replace('.', ',');
            }

            else {
                document.getElementById(scriviin.id).value = 0;
                document.getElementById('Tab_VociNPl1_RadWindowServizi_C_perconsumo').value = 0;
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
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {

                e.preventDefault();
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '111';
            }
        }
        function AutoDecimalPercentage2(obj) {
            if (obj.value.replace(',', '.') != '') {
                if ((obj.value.replace(',', '.') >= 0) && (obj.value.replace(',', '.') <= 100)) {
                    var a = obj.value.replace(',', '.');
                    a = parseFloat(a).toFixed(0)
                    if (a == 0 || a == 4 || a == 10 || a == 22) {
                        document.getElementById(obj.id).value = a.replace('.', ',')
                    }
                    else {
                        document.getElementById(obj.id).value = ''
                        alert('Inserire uno tra i seguenti valori :\n         0 ; 4 ; 10 ; 22')

                    }
                }
                else {
                    document.getElementById(obj.id).value = ''
                    alert('La percentuale non può essere superiore a 100!')
                }
            }

        }

        function $onkeydown() {

            if (event.keyCode == 13) {
                event.keyCode = 0;
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '111';
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
                document.getElementById("Tab_ImportiNP1_txtfondoritenute").style.visibility = 'visible';
                document.getElementById("Tab_ImportiNP1_lbleurFond").style.visibility = 'visible';
                document.getElementById("Tab_ImportiNP1_lblFond").style.visibility = 'visible';

            }
            else {
                document.getElementById("Tab_ImportiNP1_txtfondoritenute").style.visibility = 'hidden';
                document.getElementById("Tab_ImportiNP1_lbleurFond").style.visibility = 'hidden';
                document.getElementById("Tab_ImportiNP1_lblFond").style.visibility = 'hidden';
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
        function Nascondimi() {
            if (document.getElementById("Voci").style.visibility == 'visible') {

                document.getElementById("Tab_VociNPl1_btnEliminaAppalti").style.visibility = 'hidden';
                document.getElementById("Tab_VociNPl1_btnApriAppalti").style.visibility = 'hidden';
            }

        }

        function CalcolaD(sender, args) {
            var dtInizio = document.getElementById('txtannoinizio').value.substr(8, 2) + '/' + document.getElementById('txtannoinizio').value.substr(5, 2) + '/' + document.getElementById('txtannoinizio').value.substr(0, 4);
            var dtFine = document.getElementById('txtannofine').value.substr(8, 2) + '/' + document.getElementById('txtannofine').value.substr(5, 2) + '/' + document.getElementById('txtannofine').value.substr(0, 4);
            CalcolaDurata(dtInizio, dtFine);
        };


        //durata in mesi
        function CalcolaDurata2(inizio, fine) {

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
        };


        //durata in mesi
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

                    document.getElementById('Tab_ImportiNP1_durata').value = Math.round(difference_ms / ONE_DAY) + 1;
                    document.getElementById('txtdurata').value = Math.round(difference_ms / ONE_DAY) + 1;
                    document.getElementById('Tab_ImportiNP1_durataMesi').value = 0

                    if (fine[2] == inizio[2]) {
                        //aggiunto +1 perchè la funzione contava un mese in meno sempre
                        document.getElementById('Tab_ImportiNP1_durataMesi').value = fine[1] - inizio[1] + 1;
                    }
                    else {
                        var anni = fine[2] - inizio[2]
                        document.getElementById('Tab_ImportiNP1_durataMesi').value = (12 - inizio[1])
                        if (anni > 1) {
                            for (i = 1; i < anni; i++) {

                                document.getElementById('Tab_ImportiNP1_durataMesi').value = parseInt(document.getElementById('Tab_ImportiNP1_durataMesi').value) + 12

                            }
                        }
                        //aggiunto +1 perchè la funzione contava un mese in meno sempre
                        document.getElementById('Tab_ImportiNP1_durataMesi').value = parseInt(document.getElementById('Tab_ImportiNP1_durataMesi').value) + parseInt(fine[1]) + 1

                    }


                }
                else {
                    document.getElementById('Tab_ImportiNP1_durata').value = "";
                    document.getElementById('txtdurata').value = "";
                    alert('La data finale non può essere inferiore a quella iniziale!')
                }

            }
            else {
                document.getElementById('Tab_ImportiNP1_durata').value = "";
                document.getElementById('txtdurata').value = "";
            }
        }

        function FinisciCont() {
            var Conferma
            if (document.getElementById("chkRitenute").checked = true) {
                Conferma = window.confirm("ATTENZIONE...L\'operazione di chiusura contratto non è reversibile!\nVerranno emessi i pagamenti!\nProcedere con l\'operazione?");
                if (Conferma == true) {
                    document.getElementById("txtConfChiusura").value = '1';
                }
                else
                { document.getElementById("txtConfChiusura").value = '0'; }
            }

        }


        function ConfSpalmVariazioneCA() {
            ////+++++++++controllo l'ammontare della percentuale di variazione inserita!+++++++++++++
            //if (document.getElementById("Tab_VariazioniNP1_PercUsataCanone").value == 20) {
            //    alert('Hai raggiunto il limite!');
            //    return;
            //}
            //if (document.getElementById("Tab_VariazioniNP1_PercUsataCanone").value > 20) {
            //    alert('Hai raggiunto e SUPERATO il limite!ATTENZIONE!!!');
            //    return;
            //}

            //var ConfCanone
            ////      *********Richiesta conferma CANONE
            //if (document.getElementById("Tab_VariazioniNP1_Spalm_Canone").value == 1) {
            //    ConfCanone = window.confirm("ATTENZIONE...Consentire al sistema di ripartire automaticamente la variazione sul totale dei servizi per gli importi a Canone?");
            //    if (ConfCanone == true) {
            //        document.getElementById("Tab_VariazioniNP1_ConfermaSp_Canone").value = '1';
            //        //****nascondo la datagrid con i servizi
            //        document.getElementById("DivImpServ").style.visibility = 'hidden';

            //    }
            //    else {
            //        document.getElementById("Tab_VariazioniNP1_ConfermaSp_Canone").value = '0';
            //        document.getElementById("Tab_VariazioniNP1_lblImporto").style.visibility = 'hidden';
            //        document.getElementById("Tab_VariazioniNP1_txtPercVarCanone").style.visibility = 'hidden';
            //        document.getElementById("Tab_VariazioniNP1_lblperc").style.visibility = 'hidden';

            //    }

            //}
            //else {
            //    document.getElementById("Tab_VariazioniNP1_lblImporto").style.visibility = 'hidden';
            //    document.getElementById("Tab_VariazioniNP1_txtPercVarCanone").style.visibility = 'hidden';
            //    document.getElementById("Tab_VariazioniNP1_lblperc").style.visibility = 'hidden';

            //}
            //document.getElementById('USCITA').value = '1';
            //document.getElementById('Tab_VariazioniNP1_txtAppareV').value = '0';
            //document.getElementById('Variazioni').style.visibility = 'visible';
            //document.getElementById('Tab_VariazioniNP1_lblTitle').innerHTML = 'VARIAZIONE SERVIZI (QUINTO D\'OBBLIGO) SULL\'IMPORTO A CANONE';
            //document.getElementById('Tab_VariazioniNP1_Tipo').value = '0';

        }


        function ConfSpalmVariazioneCO() {
            //+++++++++controllo l'ammontare della percentuale di variazione inserita!+++++++++++++
            //if (document.getElementById("Tab_VariazioniNP1_PercUsataConsumo").value == 20) {
            //    alert('Hai raggiunto il limite!');
            //    return;
            //}
            //if (document.getElementById("Tab_VariazioniNP1_PercUsataConsumo").value > 20) {
            //    alert('Hai raggiunto e SUPERATO il limite!ATTENZIONE!!!');
            //    return;
            //}

            //var ConfConsumo

            ////      *********Richiesta conferma CONSUMO

            //if (document.getElementById("Tab_VariazioniNP1_Spalm_Consumo").value == 1) {
            //    ConfConsumo = window.confirm("ATTENZIONE...Consentire al sistema di ripartire automaticamente la variazione sul totale dei servizi per gli importi a Consumo?");
            //    if (ConfConsumo == true) {
            //        document.getElementById("Tab_VariazioniNP1_ConfermaSp_Consumo").value = '1';
            //        //****nascondo la datagrid con i servizi
            //        document.getElementById("DivImpServCons").style.visibility = 'hidden';

            //    }
            //    else {
            //        document.getElementById("Tab_VariazioniNP1_ConfermaSp_Consumo").value = '0';
            //        document.getElementById("Tab_VariazioniNP1_lblImportoCons").style.visibility = 'hidden';
            //        document.getElementById("Tab_VariazioniNP1_txtPercVarCons").style.visibility = 'hidden';
            //        document.getElementById("Tab_VariazioniNP1_lblpercCons").style.visibility = 'hidden';


            //    }
            //}

            //else {
            //    document.getElementById("Tab_VariazioniNP1_lblImportoCons").style.visibility = 'hidden';
            //    document.getElementById("Tab_VariazioniNP1_txtPercVarCons").style.visibility = 'hidden';
            //    document.getElementById("Tab_VariazioniNP1_lblpercCons").style.visibility = 'hidden';
            //}


            //document.getElementById('USCITA').value = '1';
            //document.getElementById('Tab_VariazioniNP1_txtAppareVC').value = '0';
            //document.getElementById('VariazioniConsumo').style.visibility = 'visible';
            //document.getElementById('Tab_VariazioniNP1_lblTitle0').innerHTML = 'VARIAZIONE SERVIZI (QUINTO D\'OBBLIGO) SULL\'IMPORTO A CONSUMO';
            //document.getElementById('Tab_VariazioniNP1_Tipo').value = '0'

        }


        function CongElimVariaz() {
            //var Conf
            //if (document.getElementById("Tab_VariazioniNP1_id_selected").value != 0) {
            //    Conf = window.confirm("ATTENZIONE...Verranno eliminate tutte le quote della Variazione!\nContinuare l\'operazione?");
            //    if (Conf == true) {
            //        document.getElementById("Tab_VariazioniNP1_txtElimina").value = '1';

            //    }

            //}
            //else { alert('Selezionare la variazione che si desidera eliminare!') }

        }


        //***************** SCRIPT GESTIONE TAB VARIAZIOINE LAVORI

        function CaricaVarLavoriCanone() {

            document.getElementById('USCITA').value = '1';
            //document.getElementById('Tab_VariazLavNPl1_txtAppare').value = '0';
            //document.getElementById('VariazioniLavori').style.visibility = 'visible';
            //document.getElementById('Tab_VariazLavNPl1_lblTitle').innerHTML = 'VARIAZIONE LAVORI (QUINTO D\'OBBLIGO) SULL\'IMPORTO A CANONE';
            //document.getElementById('Tab_VariazLavNPl1_txtTipo').value = '3';

        }

        function CaricaVarLavoriConsumo() {
            document.getElementById('USCITA').value = '1';
            //document.getElementById('Tab_VariazLavNPl1_txtAppare').value = '0';
            //document.getElementById('VariazioniLavori').style.visibility = 'visible';
            //document.getElementById('Tab_VariazLavNPl1_lblTitle').innerHTML = 'VARIAZIONE LAVORI (QUINTO D\'OBBLIGO) SULL\'IMPORTO A CONSUMO';
            //document.getElementById('Tab_VariazLavNPl1_txtTipo').value = '4';

        }


        function CongElimVariazLavori() {

            //var Conf
            //if (document.getElementById("Tab_VariazLavNPl1_id_selected").value != 0) {
            //    Conf = window.confirm("ATTENZIONE...Verranno eliminate tutte le quote della Variazione!\nContinuare l\'operazione?");
            //    if (Conf == true) {
            //        document.getElementById("Tab_VariazLavNPl1_txtElimina").value = '1';

            //    }

            //}
            //else { alert('Selezionare la variazione che si desidera eliminare!') }

        };

        function ReturnBozza() {
            var sicuro = confirm('Sei sicuro di voler riportare il contratto in BOZZA?');
            if (sicuro == true) {
                document.getElementById('ConfRitBozza').value = '1';
            }

        };

        function AllegaFile() {
            if ((document.getElementById('txtIdAppalto').value == '') || (document.getElementById('txtIdAppalto').value == '0')) {
                //alert('E\' necessario salvare il contratto prima di allegare documenti!');
                apriAlert('E\' necessario salvare il contratto prima di allegare documenti!', 300, 150, '', '', '');

            } else {
                CenterPage('../../../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('txtIdAppalto').value, 'Allegati', 1000, 800);
            };
        };
        function CenterPage(pageURL, title, w, h) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        };




    </script>


    <script type="text/javascript" src="tabber.js"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
    <style type="text/css">
        .CssMaiuscolo {
            TEXT-TRANSFORM: uppercase
        }

        .style4 {
            width: 21px;
        }
    </style>

</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
            <Windows>
                <telerik:RadWindow ID="RadWindow4" runat="server" CenterIfModal="true" Modal="True"
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
                                                Style="cursor: pointer" TabIndex="57" ToolTip="Esci" />
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
            </Windows>
        </telerik:RadWindowManager>


        <table style="width: 99%">
            <tr>
                <td class="TitoloModulo">Contratto non patrimoniale
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnIndietro" runat="server" Style="cursor: pointer"
                                    Text="Indietro" OnClientClick="ConfermaEsci();" />
                            </td>
                            <td>
                                <asp:Button ID="btnSalva" runat="server" Text="Salva"
                                    OnClientClick="document.getElementById('USCITA').value='1'" Style="z-index: 100; left: 584px; position: static; top: 32px; cursor: pointer"
                                    TabIndex="-1" ToolTip="Salva" />
                            </td>
                            <td>&nbsp;</td>

                            <td>
                                <asp:Button ID="btnAttivaContratto" runat="server" Text="Attiva contratto"
                                    OnClientClick="document.getElementById('USCITA').value='1'" Style="z-index: 100; left: 584px; position: static; top: 32px; cursor: pointer"
                                    TabIndex="-1"
                                    ToolTip="Attiva il contratto " Enabled="False" />
                            </td>
                            <td>
                                <asp:Button ID="btnFineContratto" runat="server" Text="Chiudi contratto"
                                    OnClientClick="document.getElementById('USCITA').value='1';FinisciCont();" Style="z-index: 100; left: 584px; position: static; top: 32px; cursor: pointer"
                                    TabIndex="-1"
                                    ToolTip="Fine Contratto ed emissione Lettere Pagamento" Enabled="False" />
                            </td>
                            <td>
                                <telerik:RadButton ID="RadButtonAllegati" runat="server" Text="Allegati" ToolTip="Visualizza allegati"
                                    AutoPostBack="false" OnClientClicking="function(sender, args){AllegaFile();}"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="imgEventi" runat="server" Text="Eventi" ToolTip="Eventi Scheda Appalto"
                                    AutoPostBack="false" OnClientClicking="function(sender, args){apriEventi();}"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <asp:Button ID="imgUscita" runat="server" CausesValidation="False" Text="Esci"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 125; left: 600px; position: static; top: 32px; cursor: pointer" TabIndex="-1" ToolTip="Esci" />
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="99px">Numero Repertorio*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtnumero" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" Style="z-index: 107; left: 109px; top: 67px"
                                    TabIndex="1" Width="90px" CssClass="CssMaiuscolo"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px"
                                    TabIndex="-1">Data</asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtdatarepertorio" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                    DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110"
                                    ClientEvents-OnDateSelected="CalcolaD">
                                    <DateInput ID="DateInput3" runat="server" EmptyMessage="gg/mm/aaaa">
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
                                <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="50px">Data inizio</asp:Label>
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
                                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left;" Width="46px" Height="16px">Data fine</asp:Label>
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
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left"
                                    Width="64px">Durata giorni</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdurata" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="3" Style="z-index: 107; left: 109px; top: 67px"
                                    TabIndex="-1" Width="36px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="Label25" runat="server"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black"
                                    Style="z-index: 106; left: 19px; top: -374px" Width="99px">CUP</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCup" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="15" Style="z-index: 107; left: 109px; top: 67px"
                                    TabIndex="4" Width="155px" CssClass="CssMaiuscolo"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td class="style4">
                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">CIG*</asp:Label>
                            </td>
                            <td>

                                <asp:TextBox ID="txtCIG" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" Style="z-index: 107; left: 109px; top: 67px"
                                    TabIndex="4" Width="155px" CssClass="CssMaiuscolo"></asp:TextBox>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="10">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 5%">
                                <asp:Label ID="Label21" runat="server"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black"
                                    Style="z-index: 106; left: 19px; top: -374px" Width="99px">Descrizione*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdescrizione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" Height="32px" MaxLength="50" Style="z-index: 107; left: 109px; top: 67px"
                                    TabIndex="5" TextMode="MultiLine" Width="95%"
                                    CssClass="CssMaiuscolo"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 5%">
                                <asp:Label ID="Label1" runat="server"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black"
                                    Style="z-index: 106; left: 19px; top: -374px" Width="99px">Direttore Lavori</asp:Label>
                            </td>
                            <td style="width: 25%">
                                <telerik:RadComboBox ID="cmbGestore" Width="95%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="false" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 5%">
                                <asp:Label ID="Label33" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="99px">Fornitore*</asp:Label>
                            </td>
                            <td style="width: 25%">
                                <telerik:RadComboBox ID="cmbfornitore" Width="95%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="false" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>

                            <td style="width: 5%">
                                <asp:Label ID="Label34" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="99px">IBAN*</asp:Label>
                            </td>
                            <td style="width: 25%">

                                <telerik:RadComboBox ID="cmbIbanFornitore" Width="320px" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="false" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label23" runat="server"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px"
                                    Width="99px">Stato Contratto</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblStato" runat="server" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="9pt"
                                    ForeColor="Black" Text="BOZZA" Width="90px"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnTornBozza" runat="server" CausesValidation="False" Text="TORNA IN BOZZA"
                                    OnClientClick="document.getElementById('USCITA').value='1';ReturnBozza();"
                                    Style="z-index: 100; left: 584px; position: static; top: 32px; cursor: pointer" TabIndex="-1"
                                    ToolTip="Riporta in bozza il contratto, per apportarvi modifiche"
                                    Visible="False" /></td>
                            <td>
                                <asp:CheckBox ID="chkRitenute" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    Text="Ritenute di Legge" onClick="Fondo();" TabIndex="8" />
                            </td>
                            <td>
                                <asp:CheckBox ID="ChkModuloFO" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    Text="Abil. Mod. Fornitori" onClick="Fondo();" TabIndex="9" />
                            </td>
                            <td>
                                <asp:CheckBox ID="ChkGestEsternaMF" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    Text="Gest. Est. M.F." onClick="Fondo();" TabIndex="9" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBoxAnticipo" AppendDataBoundItems="true" Font-Names="Arial"
                                    Font-Size="8pt" Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." Enabled="false"
                                    Width="100px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="0" Text="Nessun anticipo" />
                                        <%-- <telerik:RadComboBoxItem Value="1" Text="Rate costanti" />--%>
                                        <telerik:RadComboBoxItem Value="2" Text="20% su SAL" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBoxVoci" AppendDataBoundItems="true" Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." Enabled="false"
                                    Width="350px">
                                </telerik:RadComboBox>
                            </td>
                            <td>&nbsp;&nbsp;
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
            <tr>
                <td>
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True"
                        Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Text="Label"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadTabStrip runat="server" ID="RadTabStrip" Orientation="HorizontalTop"
                        Width="100%" MultiPageID="RadMultiPage1" ShowBaseLine="true" ScrollChildren="true"
                        OnClientTabSelected="tabSelezionato">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="RadPageView1" Text="Voci P.F." Value="VociPF">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView2" Text="Importi" Value="Importi">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView3" Text="Dati Amministrativi" Value="DatiAmministrativi">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView6" Text="Penali" Value="Penali">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView7" Text="Variazioni"
                                Value="Variazioni">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView9" Text="S.L.A. " Value="S.L.A.">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" CssClass="multiPage" Width="100%"
                        ScrollChildren="true">
                        <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab1">
                                <uc2:Tab_VociNPl ID="Tab_VociNPl1" runat="server" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView2" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab2">
                                <uc1:Tab_ImportiNP ID="Tab_ImportiNP1" runat="server" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView3" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab3">
                                <uc3:Tab_DatiAmminist ID="Tab_DatiAmminist1" runat="server" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView6" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab6">
                                <uc4:Tab_Penali ID="Tab_Penali1" runat="server" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView7" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab7">
                                <uc12:Tab_VariazioneImporti ID="Tab_VariazioneImportiNP1" runat="server" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView9" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab9">
                                <uc7:Tab_SLA ID="Tab_SLA" runat="server" />
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="txttab" runat="server" Value="0" />
                    <asp:HiddenField ID="USCITA" runat="server" Value="0" />
                    <asp:HiddenField ID="txtModificato" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="txtindietro" runat="server" Value="0" />
                    <asp:HiddenField ID="SOLO_LETTURA" runat="server" Value="0"></asp:HiddenField>
                    <asp:HiddenField ID="trovato_cmbfornitore" runat="server" Value="-1" />
                    <asp:HiddenField ID="txtIdPianoFinanziario" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="txtConfChiusura" runat="server" Value="0" />
                    <asp:HiddenField ID="idStruttura" runat="server" Value="0" />
                    <asp:HiddenField ID="ConfRitBozza" runat="server" Value="0" />
                    <asp:HiddenField ID="HFGriglia" runat="server" />
                    <asp:HiddenField ID="HFTAB" runat="server" />
                    <asp:HiddenField ID="HFAltezzaTab" runat="server" />
                    <asp:HiddenField ID="TipoAllegato" runat="server" Value="" />
                    <asp:HiddenField ID="HiddenTabSelezionato" runat="server" Value="0" />
                    <asp:HiddenField ID="numTab" runat="server" Value="11" />
                    <asp:HiddenField ID="HFAltezzaFGriglie" runat="server" />
                    <asp:HiddenField ID="txtIdAppalto" runat="server" Value="0" />
                    <asp:HiddenField ID="idPagRitLegge" runat="server" Value="0" />
                    <asp:HiddenField ID="HiddenFieldIdPagamento" runat="server" Value="0" />
                    <asp:HiddenField ID="HiddenFieldIdPrenotazione" runat="server" Value="0" />
                    <asp:HiddenField ID="HiddenResiduoConsumo" runat="server" Value="0" />
                </td>
            </tr>
        </table>
        <%--<asp:Panel ID="Panel1" runat="server" Width="800px" Height="540px" Style="left: 0px;
        background-image: url(../../../NuoveImm/SfondoMascheraContratti.jpg); background-color: #000000;
        position: absolute; top: 0px; z-index: 10000;">
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            <br />
            &nbsp;&nbsp;<asp:Label ID="ADP" runat="server" Text="Stampa anticipo contrattuale"></asp:Label>
            &nbsp;</span></strong><br />
        <br />
        <table border="0" cellpadding="4" cellspacing="4" width="90%">
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
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="DataEmissione"
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
            <tr>
                <td colspan="2">
                    <asp:ImageButton ID="ImgConferma" runat="server" ImageUrl="../../../NuoveImm/Img_Stampa_Grande.png"
                        ToolTip="Stampa" />
                    <asp:ImageButton ID="ImgAnnulla" runat="server" ImageUrl="../../../NuoveImm/Img_Annulla_Stampa.png"
                        ToolTip="Annulla Stampa" />

            </td>
        </tr>
    </table>
    </asp:Panel>--%>
        <div>
            <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
                EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="100" Position="BottomRight"
                OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
            </telerik:RadNotification>
        </div>
    </form>

    <script type="text/javascript">
        window.focus();
        self.focus();
        //************GESTIONE VISIBILITA CAMPO DI TESTO FONDO RITENUTE*********
        if (document.getElementById("chkRitenute").checked == true) {
            document.getElementById("Tab_ImportiNP1_txtfondoritenute").style.visibility = 'visible';
            document.getElementById("Tab_ImportiNP1_lbleurFond").style.visibility = 'visible';
            document.getElementById("Tab_ImportiNP1_lblFond").style.visibility = 'visible';
        }
        else {
            document.getElementById("Tab_ImportiNP1_txtfondoritenute").style.visibility = 'hidden';
            document.getElementById("Tab_ImportiNP1_lbleurFond").style.visibility = 'hidden';
            document.getElementById("Tab_ImportiNP1_lblFond").style.visibility = 'hidden';
        }
        //********************************************************************

        if (document.getElementById("Voci").style.visibility == 'visible') {

            document.getElementById("Tab_VociNPl1_btnEliminaAppalti").style.visibility = 'hidden';
            document.getElementById("Tab_VociNPl1_btnApriAppalti").style.visibility = 'hidden';
        }

        if (document.getElementById("SOLO_LETTURA").value == 1) {

            //document.getElementById("Tab_VariazioniNP1_imgAggiungiServ").style.visibility = 'hidden';
            //document.getElementById("Tab_VariazioniNP1_imgAggiungiLavori").style.visibility = 'hidden';
            //document.getElementById("Tab_VariazLavNPl1_imgAggiungiLavCan").style.visibility = 'hidden';
            //document.getElementById("Tab_VariazLavNPl1_imgAggiungiLavCons").style.visibility = 'hidden';

            //  document.getElementById('imginvioallegati').style.visibility = 'hidden';
        }

        if (document.getElementById("lblStato").innerHTML == 'ATTIVO') {

        }

        //******************APPALTI LAVORI********************
        //if (document.getElementById('Tab_VariazLavNPl1_txtAppare').value != '1') {
        //    document.getElementById('VariazioniLavori').style.visibility = 'hidden';
        //}
        //else {
        //    document.getElementById('VariazioniLavori').style.visibility = 'visible';
        //}
       // document.getElementById('dvvvPre').style.visibility = 'hidden';

    </script>

</body>
</html>
