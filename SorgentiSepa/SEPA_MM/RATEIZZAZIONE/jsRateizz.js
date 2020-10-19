function CreaDomandaLocatari() {
    // var chiediConferma;
    // chiediConferma = window.confirm("Attenzione...Sei sicuro di volere creare una nuova domanda?");
    // if (chiediConferma == true) {
    window.open('../VSA/Locatari/nuova_domanda.aspx?INTEST=' + document.getElementById('sIntestatario').value + '&COD=' + document.getElementById('sCodice').value + '&ID=' + document.getElementById('idContratto').value + '&IDRAT=' + document.getElementById('idRateizzo').value + '&DATAPROT=' + document.getElementById('txtDataProt').value, 'Nuova_domanda', 'height=550,top=0,left=0,width=820,scrollbars=no');
    // }
    // else {
    //     alert('Operazione annullata!');
    // }
};

function VisualizzaDomandaLocatari(Codice) {
    today = new Date();
    var Titolo = 'Dichiarazione' + today.getMinutes() + today.getSeconds();
    popupWindow = window.open('../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?GLocat=1&CH=2&ID=' + Codice, Titolo, '');
    popupWindow.focus();
};

function ConfirmSalva() {
    if (document.getElementById('idRateizzo').value == "0") {
        var chiediConferma;
        chiediConferma = window.confirm('Attenzione...\nSi è sicuri di voler procedere?');
        if (chiediConferma == true) {
            document.getElementById('ConfSave').value = '1';
        }
        else {
            document.getElementById('ConfSave').value = '0';
        }
    }
    else { document.getElementById('ConfSave').value = '1'; };
};
function CenterPage(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
function AllegaFile() {
    if (document.getElementById('idRateizzo').value != 0) {
        CenterPage('../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('idRateizzo').value, 'Allegati', 1000, 800);

    }
    else {
        alert('Salvare il rateizzo prima di allegare dei files!');
    };
};
function CompletaData(e, obj) {
    var sKeyPressed;
    var n;
    sKeyPressed = (window.event) ? event.keyCode : e.which;
    if ((sKeyPressed < 48) || (sKeyPressed > 57)) {
        if ((sKeyPressed != 8) && (sKeyPressed != 0)) {
            if (window.event) {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                };
            }
            else {
                e.preventDefault();
            };
        };
    }
    else {
        if (obj.value.length == 0) {
            if ((sKeyPressed < 48) || (sKeyPressed > 51)) {
                if (window.event) {
                    if (navigator.appName == 'Microsoft Internet Explorer') {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    };
                }
                else {
                    e.preventDefault();
                };
            };
        }
        else if (obj.value.length == 1) {
            if (obj.value == 3) {
                if (sKeyPressed < 48 || sKeyPressed > 49) {
                    if (window.event) {
                        if (navigator.appName == 'Microsoft Internet Explorer') {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        };
                    }
                    else {
                        e.preventDefault();
                    };
                };
            };
        }
        else if (obj.value.length == 2) {
            if ((sKeyPressed < 48) || (sKeyPressed > 49)) {
                if (window.event) {
                    if (navigator.appName == 'Microsoft Internet Explorer') {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    };
                }
                else {
                    e.preventDefault();
                };
            }
            else {
                obj.value += "/";
            };
        }
        else if (obj.value.length == 4) {
            n = obj.value.substr(3, 1);
            if (n == 1) {
                if ((sKeyPressed < 48) || (sKeyPressed > 50)) {
                    if (window.event) {
                        if (navigator.appName == 'Microsoft Internet Explorer') {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        };
                    }
                    else {
                        e.preventDefault();
                    };
                };
            };
        }
        else if (obj.value.length == 5) {
            obj.value += "/";
        }
        else if (obj.value.length > 9) {
            var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
            if (selText.length == 0) {
                if (window.event) {
                    if (navigator.appName == 'Microsoft Internet Explorer') {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    };
                }
                else {
                    e.preventDefault();
                };
            };
        };
    };
};


function SostPuntVirg(e, obj) {
    var keyPressed;
    keypressed = (window.event) ? event.keyCode : e.which;
    if (keypressed == 46) {
        if (navigator.appName == 'Microsoft Internet Explorer') {
            event.keyCode = 0;
        }
        else {
            e.preventDefault();
        };
        obj.value += ',';
        obj.value = obj.value.replace('.', '');
    };
}; ;
var r = {
    'special': /[\W]/g,
    'quotes': /['\''&'\"']/g,
    'notnumbers': /[^\d\-\,]/g
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
};

function Scelta() {
    if (document.getElementById("rdbType_1").checked == true) {
        document.getElementById("lblN").style.visibility = 'hidden';
        document.getElementById("txtNRate").style.visibility = 'hidden';
        document.getElementById("txtNRate").value = '';

        document.getElementById("lblImp").style.visibility = 'visible';
        document.getElementById("lblEur").style.visibility = 'visible';
        document.getElementById("txtImpRate").style.visibility = 'visible';


    }
    else {
        document.getElementById("lblN").style.visibility = 'visible';
        document.getElementById("txtNRate").style.visibility = 'visible';
        document.getElementById("lblImp").style.visibility = 'hidden';
        document.getElementById("lblEur").style.visibility = 'hidden';
        document.getElementById("txtImpRate").style.visibility = 'hidden';
        document.getElementById("txtImpRate").value = '';

    }
};

function Sottrai() {
    if (parseFloat(document.getElementById("txtVersAnticipo").value.replace('.', '').replace(',', '.')) < document.getElementById("AnticipoMinimo").value.replace('.', '').replace(',', '.')) {
        alert('ATTENZIONE!Stai inserendo un anticipo inferiore al 10% (' + document.getElementById("AnticipoMinimo").value + ')!');
        //document.getElementById("txtVersAnticipo").value = document.getElementById("AnticipoMinimo").value;
    };
    if (parseFloat(document.getElementById("txtVersAnticipo").value.replace('.', '').replace(',', '.')) <= parseFloat(document.getElementById("txtImporto").value.replace('.', '').replace(',', '.')) && parseFloat(document.getElementById("txtVersAnticipo").value.replace('.', '').replace(',', '.')) >= 0) {
        var risultato
        risultato = document.getElementById("txtImporto").value.replace('.', '').replace(',', '.') - document.getElementById("txtVersAnticipo").value.replace('.', '').replace(',', '.');
        risultato = risultato.toFixed(2);

        if (risultato.substring(risultato.length - 3, 0).length >= 4) {
            var decimali = risultato.substring(risultato.length, risultato.length - 2);
            var dascrivere = risultato.substring(risultato.length - 3, 0);
            var risultNew = '';
            while (dascrivere.replace('-', '').length >= 4) {
                risultNew = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultNew;
                dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
            }
            risultNew = dascrivere + risultNew + ',' + decimali;
            //document.getElementById(obj.id).value = a.replace('.', ',')
            //document.getElementById(obj.id).value = risultNew
        }
        else {
            risultNew = risultato.replace('.', ',');
        }

        document.getElementById("txtCapitale").value = risultNew;
        document.getElementById("CapitaleRateiz").value = risultNew;

    }

    else {

        alert('Inserisci un importo valido!');
        document.getElementById("txtVersAnticipo").value = document.getElementById("AnticipoMinimo").value;
        //                Sottrai();
    };

}

function Simulazione() {

    if (document.getElementById("txtData").value == '') {
        alert('Inserisci la data emissione!');
        return;
    }
    var esentInteressi = 0
    if (document.getElementById("chkInteressi").checked) {
        esentInteressi = 1;
    };
    if (document.getElementById("txtNRate").value > 0) {
        document.getElementById("NumRate").value = document.getElementById("txtNRate").value

        ////        if (document.getElementById("txtNRate").value > 72) {
        ////            //document.getElementById("NumRate").value = '72';
        ////        }
        ////        else {


        ////        }
        window.open('RateizDati.aspx?CAPITALE=' + document.getElementById("txtCapitale").value + '&NRATE=' + document.getElementById("NumRate").value + '&EMISSIONE=' + document.getElementById("txtData").value + '&STIPULA=' + document.getElementById("txtDataStipula").value + '&IDCONTRATTO=' + document.getElementById("idContratto").value + '&ESINT=' + esentInteressi + '&tipo=' + document.getElementById("tipoPiano").value + '&TOT=' + document.getElementById('txtImporto').value + '&ANTICIPO=' + document.getElementById('txtVersAnticipo').value + '&IDR=' + document.getElementById('idRateizzo').value, 'Rateizz', 'height=598,width=920,scrollbars=no');
        //window.open('RateizDati.aspx?CAPITALE=' + document.getElementById("txtCapitale").value + '&NRATE=' + document.getElementById("NumRate").value + '&EMISSIONE=' + document.getElementById("txtData").value + '&STIPULA=' + document.getElementById("txtDataStipula").value + '&IDCONTRATTO=' + document.getElementById("idContratto").value + '&ESINT=' + esentInteressi + '&tipo=' + document.getElementById("tipoPiano").value + '&TOT=' + document.getElementById('txtImporto').value + '&ANTICIPO=' + document.getElementById('txtVersAnticipo').value, 'Rateizz', 'height=598,width=920,scrollbars=no');

        //window.open('RateizDati.aspx?CAPITALE=' + document.getElementById("txtCapitale").value + '&NRATE=' + document.getElementById("NumRate").value, 'Rateizz', 'height=598,width=920,scrollbars=no');

    }

    if (document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.') > 0) {
        MaxImporto();
        window.open('RateizDati.aspx?CAPITALE=' + document.getElementById("txtCapitale").value + '&IMPRATA=' + document.getElementById("txtImpRate").value + '&EMISSIONE=' + document.getElementById("txtData").value + '&STIPULA=' + document.getElementById("txtDataStipula").value + '&IDCONTRATTO=' + document.getElementById("idContratto").value + '&ESINT=' + esentInteressi + '&tipo=' + document.getElementById("tipoPiano").value + '&TOT=' + document.getElementById('txtImporto').value + '&ANTICIPO=' + document.getElementById('txtVersAnticipo').value, 'Rateizz', 'height=598,width=920,scrollbars=no');

    };


    if ((document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.') == '' && document.getElementById("txtNRate").value == '') || (document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.') == '0' && document.getElementById("txtNRate").value == '') || (document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.') == '' && document.getElementById("txtNRate").value == '0')) {
        //alert('Inserisci il numero delle rate o l\'importo della singola rata!');
        alert('Inserisci il numero delle rate!');
    };





};
//function MaxRata() {
//    if (parseInt(document.getElementById("txtNRate").value) > parseInt(document.getElementById("MaxNumRate").value)) {
//        document.getElementById("txtNRate").value = document.getElementById("MaxNumRate").value;
//        document.getElementById("NumRate").value = document.getElementById("txtNRate").value;
//        alert('Il numero massimo di rate è ' + document.getElementById("MaxNumRate").value);

//    }

//    else {

//        document.getElementById("NumRate").value = document.getElementById("txtNRate").value;
//    };
//};
function MaxRata() {
    if (document.getElementById("chkGaranzie").checked) {

        if (parseInt(document.getElementById("txtNRate").value) > document.getElementById("MaxNumRatePart").value) {
            document.getElementById("txtNRate").value = document.getElementById("MaxNumRate").value;
            document.getElementById("NumRate").value = document.getElementById("txtNRate").value;
            alert('Il numero massimo di rate è ' + document.getElementById("MaxNumRatePart").value );
        }
        else {

            document.getElementById("NumRate").value = document.getElementById("txtNRate").value;
        };
    }
    else {

        if (parseInt(document.getElementById("txtNRate").value) > parseInt(document.getElementById("MaxNumRate").value)) {
            document.getElementById("txtNRate").value = document.getElementById("MaxNumRate").value;
            document.getElementById("NumRate").value = document.getElementById("txtNRate").value;
            alert('Il numero massimo di rate è ' + document.getElementById("MaxNumRate").value);

        }

        else {

            document.getElementById("NumRate").value = document.getElementById("txtNRate").value;
        };
    }
};

function MaxImporto() {
    if (parseFloat(document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.')) > parseFloat(document.getElementById("txtCapitale").value.replace('.', '').replace(',', '.'))) {
        alert('L\'importo della rata non può superare quello da rateizzare!');
        document.getElementById("txtImpRate").value = '';
    };

};
function ControlDate(obj) {
    if (obj.value != '') {
        var currentTime = new Date();
        var myDate = obj.value.substring(6, 11) + obj.value.substring(3, 5) + obj.value.substring(0, 2)
        if (myDate < currentTime.getFullYear() + '0' + (currentTime.getMonth() + 1) + '0' + currentTime.getDate()) {
            alert('La data deve essere uguale o superiore alla data odierna!');
            document.getElementById(obj.id).value = '';
        }
    }
};

function PrintDoc() {
    //window.open('PrintLetter.aspx?IDBOLL=<%=vIdBolletta %>', 'letDebt', '');
    window.open('PrintPianoDilazioneDebito.aspx?IDCONTRATTO=' + document.getElementById("idContratto").value + '&IDRAT=' + document.getElementById("idRateizzo").value + '&ALTRAT=' + document.getElementById("AltrRat").value, 'dilazDebt', '');
};
function ConfirmSave() {

    var chiediConferma
    if (document.getElementById('tipoPiano').value == "M") {
        chiediConferma = window.confirm("Attenzione...\nSi è sicuri di voler salvare il piano di RIENTRO MOROSITA\'?");

    }
    else {
        chiediConferma = window.confirm("Attenzione...\nSi è sicuri di voler salvare il piano di AMMORTAMENTO?\nVerrà generata una bolletta pari al prezzo di vendita");

    }
    if (chiediConferma == true) {
        document.getElementById('ConfermaSalva').value = '1';

    }
    else {

        document.getElementById('ConfermaSalva').value = '0';

    }
};

function ctrlModificato() {
    var modificato = 0;
    if (document.getElementById('frmModify').value == '1') {

        modificato = 1;
    }
    return modificato;
};
function confExit() {
    var mod = ctrlModificato();
    if (mod == 1) {
        var chiediConferma;
        chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche!\nUscire senza salvare?");
        if (chiediConferma == true) {
            self.close();
        }
        else { return false; };

    }
    else {
        self.close();
    };
};
function confPrint() {
    var mod = ctrlModificato();
    if (mod == 1 || document.getElementById('flSalvato').value == 0) {
        var chiediConferma;
        chiediConferma = window.confirm("Attenzione...Stampare senza aver salvato?");
        if (chiediConferma == true) {
            PrintDoc();
        }
        else { return false; };

    }
    else {
        PrintDoc();
    };
};
function caricamentoincorso() {
    if (typeof (Page_ClientValidate) == 'function') {
        Page_ClientValidate();
        if (Page_IsValid) {
            if (document.getElementById('caricamento') != null) {
                document.getElementById('caricamento').style.visibility = 'visible';
            };
        }
        else {
            alert('ATTENZIONE! Ci sono delle incongruenze dati della pagina e/o eventuali TAB!');
        };
    }
    else {
        if (document.getElementById('caricamento') != null) {
            document.getElementById('caricamento').style.visibility = 'visible';
        };
    };
};
