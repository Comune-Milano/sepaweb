var Selezionato;
var OldColor;
var SelColo;
var validNavigation = true;
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
var r = {
    'special': /[\W]/g,
    'codice': /[\W\_]/g,
    'quotes': /['\''&'\"']/g,
    'notnumbers': /[^\d]/g,
    'onlynumbers': /[^\d\-\,\.]/g,
    'numbers': /[^\d]/g
};
function valid(o, w) {
    o.value = o.value.replace(r[w], '');
};
/*-----------FUNZIONE PER DISATTIVARE ALCUNI TASTI 'DANNOSI' nelle maschere-------------------*/
if (navigator.appName == 'Microsoft Internet Explorer') {
    document.onkeydown = $onkeydown;
}
else {
    window.document.addEventListener("keydown", TastoInvio, true);
};
function TastoInvio(e) {
    sKeyPressed1 = e.which;
    if (sKeyPressed1 == 13) {
        if (document.getElementById('HFbtnClickGo') != null) {
            if (document.getElementById('HFbtnClickGo').value != '') {
                var idoggetto = document.getElementById('HFbtnClickGo').value;
                if (document.getElementById('' + idoggetto + '')) {
                    document.getElementById('' + idoggetto + '').click();
                };
            };
        };
    };
};
function $onkeydown() {
    var alt = window.event.altKey;
    if (event.keyCode == 13) {
        if (document.getElementById('HFbtnClickGo') != null) {
            if (document.getElementById('HFbtnClickGo').value != '') {
                var idoggetto = document.getElementById('HFbtnClickGo').value;
                if (document.getElementById('' + idoggetto + '')) {
                    document.getElementById('' + idoggetto + '').click();
                };
            };
        };
    };
};
//function TastoInvio(e) {
//    sKeyPressed1 = e.which;
//    if (sKeyPressed1 == 112 || sKeyPressed1 == 65 || sKeyPressed1 == 115 || sKeyPressed1 == 116 || sKeyPressed1 == 117 || sKeyPressed1 == 118 || sKeyPressed1 == 122 || sKeyPressed1 == 123 || sKeyPressed1 == 60 || sKeyPressed1 == 91 || sKeyPressed1 == 92) {
//        sKeyPressed1 = 0;
//        e.preventDefault();
//        e.stopPropagation();
//    };
//    if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
//        if (sKeyPressed1 == 8 || sKeyPressed1 == 13) {
//            sKeyPressed1 = 0;
//            e.preventDefault();
//            e.stopPropagation();
//        }
//    }
//    else {
//        if (document.activeElement.isMultiLine == false) {
//            if (sKeyPressed1 == 13) {
//                sKeyPressed1 = 0;
//                e.preventDefault();
//                e.stopPropagation();
//            };
//        };
//    };
//    var alt = window.event.altKey;
//    if (alt && sKeyPressed1 == 115) {
//        if (document.getElementById('noClose').value == 1) {
//            if (document.getElementById('btnEsci') != null) {
//                exitClick = 1;
//                document.getElementById('btnEsci').click();
//            }
//            else if (document.getElementById('MainContent_btnEsci') != null) {
//                exitClick = 1;
//                document.getElementById('MainContent_btnEsci').click();
//            };
//            alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
//        };
//    };
//};
//function $onkeydown() {
//    var alt = window.event.altKey;
//    if (alt && event.keyCode == 115) {
//        if (document.getElementById('noClose').value == 1) {
//            if (document.getElementById('btnEsci') != null) {
//                exitClick = 1;
//                document.getElementById('btnEsci').click();
//            }
//            else if (document.getElementById('MainContent_btnEsci') != null) {
//                exitClick = 1;
//                document.getElementById('MainContent_btnEsci').click();
//            };
//            alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
//        };
//    };
//    if (event.keyCode == 112 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 122 || event.keyCode == 123 || event.keyCode == 60 || event.keyCode == 91 || event.keyCode == 92) {
//        event.keyCode = 0;
//        return false;
//    };
//    if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
//        if (event.keyCode == 8 || event.keyCode == 13) {
//            event.keyCode = 0;
//            return false;
//        }
//    }
//    else {
//        if (document.activeElement.isMultiLine == false) {
//            if (event.keyCode == 13) {
//                event.keyCode = 0;
//                return false;
//            };
//        };
//    };
//};
/*-----------FUNZIONE PER DISATTIVARE ALCUNI TASTI 'DANNOSI' nelle maschere-------------------*/
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
};
function DettagliAppuntamenti(giorno, mese, anno, filiale, idSegnalazione, indiceOrario, indiceSportello) {
    //window.open('DettagliAppuntamenti.aspx?DATA=' + anno + mese + giorno + '&FILIALE=' + filiale, 'DettagliAppuntamentiSegnalazioni', 'resizable=yes,statusbar=no,toolbar=no,width=1000px,height=770px,scrollbar=no');
    location.href = 'DettagliAppuntamentiGestioneContatti.aspx?DATA=' + anno + mese + giorno + '&FILIALE=' + filiale + '&IDS=' + idSegnalazione + '&INDORA=' + indiceOrario + '&INDSPO=' + indiceSportello;
};
function InserisciAppuntamento(giorno) {
    //alert(giorno);
};
function downloadFile(filePath) {
    location.replace('' + filePath + '');
};
function AutoDecimal(obj, numdec) {
    if (obj.value == '') {
        obj.value = '0,00';
    };
    if (numdec == null) numdec = 2;
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') != 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(numdec);
        if (a != 'NaN') {
            if (numdec > 0) {
                if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - numdec);
                    var dascrivere = a.substring(a.length - (numdec + 1), 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    };
                    risultato = dascrivere + risultato + ',' + decimali;
                    document.getElementById(obj.id).value = risultato;
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',');
                };
            }
            else {
                if (a.substring(a.length - (numdec + 1), 0).length >= 3) {
                    var dascrivere = a.substring(a.length, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    };
                    risultato = dascrivere + risultato;
                    document.getElementById(obj.id).value = risultato;
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',');
                };

            };
        }
        else {
            document.getElementById(obj.id).value = '';
        };
    };
};
function AutoDecimaljs(obj, numdec) {
    if (numdec == null) numdec = 2;
    obj = obj.replace('.', '');
    if (obj.replace(',', '.') != 0) {
        var a = obj.replace(',', '.');
        a = parseFloat(a).toFixed(numdec);
        if (a != 'NaN') {
            if (numdec > 0) {
                if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - numdec);
                    var dascrivere = a.substring(a.length - (numdec + 1), 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    };
                    risultato = dascrivere + risultato + ',' + decimali;
                    obj = risultato;
                }
                else {
                    obj = a.replace('.', ',');
                };
            }
            else {
                if (a.substring(a.length - (numdec + 1), 0).length >= 3) {
                    var dascrivere = a.substring(a.length, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    };
                    risultato = dascrivere + risultato;
                    obj = risultato;
                }
                else {
                    obj = a.replace('.', ',');
                };

            };
        }
        else {
            obj = '';
        };
    };
    return obj;
};
function EuroToLire(obj, where, numdec) {
    if (numdec == null) numdec = 2;
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') >= 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(numdec);
        a = (a * 1936.27);
        a = parseFloat(a).toFixed(0);
        if (a != 'NaN') {
            if (a.substring(a.length, 0).length >= 4) {
                var dascrivere = a.substring(a.length, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {
                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                };
                risultato = dascrivere + risultato;
                document.getElementById(where.id).value = risultato;
            }
            else {
                document.getElementById(where.id).value = a.replace('.', ',');
            };
        }
        else {
            if (obj.value == '') {
                document.getElementById(where.id).value = '';
            };
        };
    };
};
function LireToEuro(obj, where, numdec) {
    if (numdec == null) numdec = 2;
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') >= 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(0);
        a = (a / 1936.27);
        a = parseFloat(a).toFixed(numdec);
        if (a != 'NaN') {
            if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                var decimali = a.substring(a.length, a.length - numdec);
                var dascrivere = a.substring(a.length - (numdec + 1), 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {
                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                };
                risultato = dascrivere + risultato + ',' + decimali;
                document.getElementById(where.id).value = risultato;
            }
            else {
                document.getElementById(where.id).value = a.replace('.', ',');
            };
        }
        else {
            if (obj.value == '') {
                document.getElementById(where.id).value = '';
            };
        };
    };
};
function selectall(txtselezione) {
    var text_val = eval("txtselezione");
    text_val.focus();
    text_val.select();
};
/* ------------------- JQUERY ------------------------------- */
//function caricamento(valore, tipo) {
//    if ('undefined' === typeof tipo) {
//        tipo = 0;
//    };
//    if (tipo == 1) {
//        if (document.getElementById('divOscura') != null) {
//            document.getElementById('divOscura').style.display = 'block';
//        };
//    };
//    if (document.getElementById('tipoSubmit') != null) {
//        document.getElementById('tipoSubmit').value = valore;
//    };
//    window.focus();
//};
function caricamento(valore) {
    document.getElementById('tipoSubmit').value = valore;
}
function loading(tipo) {
    if (document.getElementById('NavigationMenu') != null) {
        document.getElementById('NavigationMenu').style.display = 'none';
    };
    if (document.getElementById('tipoSubmit') != null) {
        if (document.getElementById('tipoSubmit').value == 1) {
            tipo = 1;
        }
        else if (document.getElementById('tipoSubmit').value == 2) {
            tipo = 2;
        };
    };
    if (tipo == 0) {
        $(function () {
            $('#imageLoading').attr('src', 'Immagini/load.gif');
            $('#loading').append("<p>");
            $("#imageLoading").appendTo($("#loading"));
            $('#loading').append("</p><br />Attendere...");
            $('#loading').dialog({
                closeOnEscape: false,
                draggable: false,
                resizable: false,
                title: 'Caricamento in corso',
                dialogClass: 'loadingScreenWindow',
                modal: true, buttons: {}
            });
        });
    };
    if (tipo == 1) {
        $(function () {
            $('#imageLoading').attr('src', 'Immagini/load.gif');
            $('#loading').append("<p>");
            $("#imageLoading").appendTo($("#loading"));
            $('#loading').append("</p><br />Attendere...Operazione in corso!");
            $('#loading').dialog({
                closeOnEscape: false,
                draggable: false,
                resizable: false,
                title: 'Operazione in corso',
                dialogClass: 'loadingScreenWindow',
                modal: true, buttons: {}
            });
        });
    };
    window.focus();
};
function onCommand(sender, args) {
    var com = args.get_commandName();
    switch (com) {
        case 'ExportToExcel':
            tipo =2;
            break;
        case 'Filter':
            tipo = 2;
            break;
        case 'Sort':
            tipo = 2;
            break;
        case 'PageSize':
            tipo = 2;
            break;
        case 'Page':
            tipo = 2;
            break;
    };
    caricamento(2);
};
function onCommandRadCombobox(sender, args) {
    tipo = 2;
    caricamento(2);
};
function message(titolo, testo) {
    $(function () {
        $('#dialog').html('<p><span class="ui-icon ui-icon-info" style="float: left; margin: 0 7px 50px 0;"></span>' + testo + '</p>');
        $('#dialog').dialog({
            closeOnEscape: false,
            draggable: false,
            resizable: false,
            dialogClass: 'loadingScreenWindow',
            title: titolo,
            modal: true,
            buttons: { Ok: function () { $(this).dialog('close'); } }
        });
    });
    window.focus();
};
function CenterPage(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2) - 15;
    var top = (screen.height / 2) - (h / 2) - 15;
    var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
function CenterPage2(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2) - 15;
    var top = (screen.height / 2) - (h / 2) - 15;
    var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
function CenterPageModal(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2) - 15;
    var top = (screen.height / 2) - (h / 2) - 15;
    var targetWin = window.showModalDialog(pageURL, window, 'status:no;toolbar=no;dialogWidth:' + w + 'px;dialogHeight:' + h + 'px;dialogHide:true;help:no;scroll:no;dialogtop:' + top + ';dialogleft:' + left);
};
function Eventi(tipo) {
    //    if (tipo == 'PIANO') {
    //        CenterPage('Eventi.aspx?T=P&ID=' + document.getElementById('idPiano').value, 'Eventi', 900, 700);
    //    };
};
function MyDialogArguments2() {
    this.Sender = null;
    this.StringValue = "";
}
/*function BeforeSubmit() {
loading(0);
if (validNavigation != null) {
validNavigation = true;
};
if (document.getElementById('noClose').value == 1) {
document.getElementById('noCloseRead').value = 1;
document.getElementById('noClose').value = 0;
};
};*/
function BeforeSubmit() {
    if (typeof noCaricamento === "undefined") {
        document.getElementById('HFBeforeLoading').value = '0';
    } else {
        if (noCaricamento == '1') {
            document.getElementById('HFBeforeLoading').value = '1';
        } else {
            document.getElementById('HFBeforeLoading').value = '0';
        };
    };
    if (document.getElementById('HFBeforeLoading')) {
        if (document.getElementById('HFBeforeLoading').value == '1') {
            document.getElementById('HFBeforeLoading').value = '0';
        } else {
            caricamento(1, 1);
            loading(0);
        };
    } else {
        caricamento(1, 1);
        loading(0);
    };
    if (validNavigation != null) {
        validNavigation = true;
    };
    if (document.getElementById('noClose').value == 1) {
        document.getElementById('noCloseRead').value = 1;
        document.getElementById('noClose').value = 0;
    };
};

function AfterSubmit() {
    validNavigation = false;
    if (document.getElementById('noCloseRead').value == 1) {
        document.getElementById('noClose').value = 1;
        document.getElementById('noCloseRead').value = 0;
    };
};
function NoBeforeLoading(sender, args) {
    if (document.getElementById('HFBeforeLoading')) {
        document.getElementById('HFBeforeLoading').value = '1';
    };
};
/* ***** NOTIFICA TELERIK ***** */
function NotificaTelerik(ntf, titolo, messaggio) {
    if ('undefined' === typeof titolo) {
        titolo = MessaggioTitolo.Attenzione;
    };
    var notification = $find(ntf);
    if (notification) {
        notification.set_title(titolo);
        notification.set_text(messaggio);
        notification.show();
    };
};
/* ***** NOTIFICA TELERIK ***** */
function ApriModuloStandard(percorso, title) {
    var w = 1300;
    var h = 700;
    var left = ((screen.width / 2) - (w / 2)) - 15;
    var top = ((screen.height / 2) - (h / 2)) - 15;
    var targetWin = window.open(percorso, title.replace(/ /g, ''), 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};

function ConfermaEsciTelerik(sender, args, typeW, typeC) {
    if ('undefined' === typeof typeC) {
        typeC = '';
    };
    if (document.getElementById('frmModify')) {
        if (document.getElementById('frmModify').value != '0') {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    if (typeC != '') {
                        if (document.getElementById(typeC)) {
                            //args.set_cancel(true);
                            
                            document.getElementById(typeC).click();
                            
                        };
                    } else {
                        if (typeW == 0) {
                            validNavigation = true;
                            self.close();
                        } else {
                            GetRadWindow().close();
                        };
                    };
                }
            });
            var confirmExitTelerik = radconfirm(Messaggio.messaggioChiusuraMod, callBackFunction, 300, 150, null, Messaggio.Titolo_Conferma, '../StandardTelerik/Immagini/Messaggi/alert.png')
            confirmExitTelerik = confirmExitTelerik.set_behaviors();
            return false;
            //args.set_cancel(true);
        } else {
            if (typeC != '') {
                if (document.getElementById(typeC)) {
                    //args.set_cancel(true);
                    document.getElementById(typeC).click();
                };
            } else {
                if (typeW == 0) {
                    validNavigation = true;
                    self.close();
                } else {
                    GetRadWindow().close();
                };
            };
        };
    } else {
        if (typeC != '') {
            if (document.getElementById(typeC)) {
                //args.set_cancel(true);
                document.getElementById(typeC).click();
            };
        } else {
            if (typeW == 0) {
                validNavigation = true;
                self.close();
            } else {
                GetRadWindow().close();
            };
        };
    };
};