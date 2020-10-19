/*---------------FUNZIONE CompletaData per l'autocompletamento delle date---------------------------*/
var Selezionato;
var OldColor;
var SelColo;
function CompletaData(e, obj) {
    var sKeyPressed;
    var n;
    sKeyPressed = (window.event) ? event.keyCode : e.which;
    if ((sKeyPressed < 48) || (sKeyPressed > 57)) {
        if ((sKeyPressed != 8) && (sKeyPressed != 0)) {
            if (window.event) {
                event.keyCode = 0;
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
                    event.keyCode = 0;
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
                        event.keyCode = 0;
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
                    event.keyCode = 0;
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
                        event.keyCode = 0;
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
                    event.keyCode = 0;
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
    'onlynumbers': /[^\d\-\,\.]/g
};
function valid(o, w) {
    o.value = o.value.replace(r[w], '');
};
/*-----------FUNZIONE PER DISATTIVARE ALCUNI TASTI 'DANNOSI' nelle maschere-------------------*/
function TastoInvio(e) {
    sKeyPressed1 = e.which;
    if (sKeyPressed1 == 112 || sKeyPressed1 == 115 || sKeyPressed1 == 116 || sKeyPressed1 == 117 || sKeyPressed1 == 118 || sKeyPressed1 == 122 || sKeyPressed1 == 123 || sKeyPressed1 == 60 || sKeyPressed1 == 91 || sKeyPressed1 == 92) {
        sKeyPressed1 = 0;
        e.preventDefault();
        e.stopPropagation();
    };
    if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
        if (sKeyPressed1 == 8 || sKeyPressed1 == 13) {
            sKeyPressed1 = 0;
            e.preventDefault();
            e.stopPropagation();
        }
    }
    else {
        if (document.activeElement.isMultiLine == false) {
            if (sKeyPressed1 == 13) {
                sKeyPressed1 = 0;
                e.preventDefault();
                e.stopPropagation();
            };
        };
    };
    var alt = window.event.altKey;
    if (alt && sKeyPressed1 == 115) {
        if (document.getElementById('noClose').value == 1) {
            if (document.getElementById('btnEsci') != null) {
                exitClick = 1;
                document.getElementById('btnEsci').click();
            }
            else if (document.getElementById('MainContent_btnEsci') != null) {
                exitClick = 1;
                document.getElementById('MainContent_btnEsci').click();
            };
            alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
        };
    };
};
function $onkeydown() {
    var alt = window.event.altKey;
    if (alt && event.keyCode == 115) {
        if (document.getElementById('noClose').value == 1) {
            if (document.getElementById('btnEsci') != null) {
                exitClick = 1;
                document.getElementById('btnEsci').click();
            }
            else if (document.getElementById('MainContent_btnEsci') != null) {
                exitClick = 1;
                document.getElementById('MainContent_btnEsci').click();
            };
            alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
        };
    };
    if (event.keyCode == 112 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 122 || event.keyCode == 123 || event.keyCode == 60 || event.keyCode == 91 || event.keyCode == 92) {
        event.keyCode = 0;
        return false;
    };
    if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
        if (event.keyCode == 8 || event.keyCode == 13) {
            event.keyCode = 0;
            return false;
        }
    }
    else {
        if (document.activeElement.isMultiLine == false) {
            if (event.keyCode == 13) {
                event.keyCode = 0;
                return false;
            };
        };
    };
};
function CenterPage(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
function CenterPage2(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
if (navigator.appName == 'Microsoft Internet Explorer') {
    document.onkeydown = $onkeydown;
}
else {
    window.document.addEventListener("keydown", TastoInvio, true);
};
/*-----------FUNZIONE TastoInvio per evitare la pressione erronea del tasto INVIO nelle maschere-------------------*/
/* - - - - funzioni gestione decimali e euro - - - */
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
function downloadFile(filePath) {
    location.replace('' + filePath + '');
};
function AutoDecimal(obj, numdec) {
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
function caricamentoincorso() {
    if (typeof (Page_ClientValidate) == 'function') {
        Page_ClientValidate();
        if (Page_IsValid) {
            if (document.getElementById('caricamento') != null) {
                document.getElementById('caricamento').style.visibility = 'visible';
            };
        }
        else {
            alert('ATTENZIONE! Ci sono delle incongruenze nei dati della pagina!');
        };
    };
};
function ConfElaborazione() {
    if (document.getElementById("MasterPage_MainContent_idSiraper").value != -1 && document.getElementById("MasterPage_MainContent_Elaborazione").value == 1) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler elaborare i dati Siraper? I dati precedentemente calcolati andranno persi.");
        if (Conferma == false) {
            document.getElementById('MasterPage_MainContent_ConfermaElaborazione').value = '0';
        }
        else {
            document.getElementById('MasterPage_MainContent_ConfermaElaborazione').value = '1';
        };
    };
};
function Eventi() {
    CenterPage('Eventi.aspx?ID=' + document.getElementById('MasterPage_MainContent_idSiraper').value, 'Eventi', 900, 700);
};
function CreaFileXml() {
    if (document.getElementById('MasterPage_MainContent_FileCreato')) {
        if (document.getElementById('MasterPage_MainContent_FileCreato').value == 0) {
            if (document.getElementById('frmModify')) {
                if (document.getElementById('frmModify').value != 0) {
                    alert('Salvare le modifiche prima di creare il File Xml!');
                    if (document.getElementById('caricamento') != null) {
                        document.getElementById('caricamento').style.visibility = 'hidden';
                    };
                    return false;
                };
            };
            if (document.getElementById('MasterPage_MainContent_Controllo')) {
                if (document.getElementById('MasterPage_MainContent_Controllo').value == 0) {
                    alert('L\'elaborazione Siraper non ha ancora superato i controlli di validità!\n Completare i dati e salvare le modifiche!');
                    if (document.getElementById('caricamento') != null) {
                        document.getElementById('caricamento').style.visibility = 'hidden';
                    };
                    return false;
                };
            };
            window.showModalDialog('CreaFileXml.aspx?ID=' + document.getElementById('MasterPage_MainContent_idSiraper').value + '&IDV=' + document.getElementById('MasterPage_MainContent_idSiraperVersione').value + '&IdConnessione=' + document.getElementById("MasterPage_MainContent_idConnessione").value + '&SESCON=SIRAPER&DR=' + document.getElementById("MasterPage_MainContent_txtDataRiferimento").value, 'CreaFile', 'status:no;toolbar=no;dialogWidth:900px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
        };
    };
};
function ControlloCordinate(obj) {
    var stringa = obj.value;
    if (stringa != '') {
        var virgola = stringa.indexOf(',');
        if (virgola == '-1') {
            //if (stringa.length != 7) {
            obj.value = '';
            alert('Cordinata Errata!');
            return;
            //};
        }
        else {
            if (virgola != 7) {
                obj.value = '';
                alert('Cordinata Errata!');
                return;
            };
            if (stringa.length == 8) {
                obj.value = '';
                alert('Cordinata Errata!');
                return;
            };
            if (stringa.length > 15) {
                obj.value = '';
                alert('Cordinata Errata!');
                return;
            };
        };
    };
};
function ControlloMaxValore(obj, maxval) {
    var num = obj.value;
    num = num.replace('.', '').replace(',', '.');
    if (num != '') {
        if (num > maxval) {
            alert('Il valore non può essere maggiore di ' + maxval);
            obj.value = '';
        };
    };
};
function ApriPatrMobInquilino(id) {
    window.showModalDialog('PatrMobInquilino.aspx?ID=' + id + '&SLE=' + document.getElementById('MasterPage_MainContent_SLE').value + '&IdConnessione=' + document.getElementById("MasterPage_MainContent_idConnessione").value + '&SESCON=SIRAPER&IDS=' + document.getElementById('MasterPage_MainContent_idSiraper').value + '&IDSV=' + document.getElementById('MasterPage_MainContent_idSiraperVersione').value, window, 'status:no;toolbar=no;dialogWidth:1000px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
};
function ApriPatrImmobInquilino(id) {
    window.showModalDialog('PatrImmobInquilino.aspx?ID=' + id + '&SLE=' + document.getElementById('MasterPage_MainContent_SLE').value + '&IdConnessione=' + document.getElementById("MasterPage_MainContent_idConnessione").value + '&SESCON=SIRAPER&IDS=' + document.getElementById('MasterPage_MainContent_idSiraper').value + '&IDSV=' + document.getElementById('MasterPage_MainContent_idSiraperVersione').value, window, 'status:no;toolbar=no;dialogWidth:1000px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
};
function ConfEsciPatrInquilino() {
    if (document.getElementById('frmModify') != null) {
        if (document.getElementById('frmModify').value != 0) {
            var Conferma;
            Conferma = window.confirm("Attenzione...Sono state effettuate modifiche. Confermi di voler uscire?");
            if (Conferma == false) {
                if (document.getElementById('caricamento') != null) {
                    document.getElementById('caricamento').style.visibility = 'hidden';
                };
                return false;
            }
            else {
                self.close();
                return false;
            };
        }
        else {
            self.close();
            return false;
        };
    };
};
function ConfEliminaRigaPatrInq() {
    if (document.getElementById('idSelected') != null) {
        if (document.getElementById('idSelected').value != 0) {
            var Conferma;
            Conferma = window.confirm("Attenzione...Confermi di voler eliminare il patrimonio selezionato?");
            if (Conferma == false) {
                document.getElementById('ConfEliminaPatrimonio').value = '0';
                if (document.getElementById('caricamento') != null) {
                    document.getElementById('caricamento').style.visibility = 'hidden';
                };
                return false;
            }
            else {
                document.getElementById('ConfEliminaPatrimonio').value = '1';
            };
        }
        else {
            alert('Selezionare un patrimonio!');
            if (document.getElementById('caricamento') != null) {
                document.getElementById('caricamento').style.visibility = 'hidden';
            };
            return false;
        };
    };
};
function ConfProcediPatrInq() {
    if (document.getElementById('frmModify') != null) {
        if (document.getElementById('frmModify').value == 0) {
            alert('Nessuna modifica effettuata!');
            if (document.getElementById('caricamento') != null) {
                document.getElementById('caricamento').style.visibility = 'hidden';
            };
            return false;
        };
    };
};
function RicercaOggetto(oggetto, visible) { //0:NO_1:SI
    if (oggetto == 1) { //FABBRICATO
        if (visible == 0) {
            document.getElementById("divRicercaFabbricatoA").style.visibility = 'hidden';
            document.getElementById("divRicercaFabbricatoB").style.visibility = 'hidden';
        }
        else if (visible == 1) {
            document.getElementById("divRicercaFabbricatoA").style.visibility = 'visible';
            document.getElementById("divRicercaFabbricatoB").style.visibility = 'visible';
        };
    }
    else if (oggetto == 2) { //ALLOGGIO
        if (visible == 0) {
            document.getElementById("divRicercaAlloggioA").style.visibility = 'hidden';
            document.getElementById("divRicercaAlloggioB").style.visibility = 'hidden';
        }
        else if (visible == 1) {
            document.getElementById("divRicercaAlloggioA").style.visibility = 'visible';
            document.getElementById("divRicercaAlloggioB").style.visibility = 'visible';
        };
    }
    else if (oggetto == 3) { //INQUILINO
        if (visible == 0) {
            document.getElementById("divRicercaInquilinoA").style.visibility = 'hidden';
            document.getElementById("divRicercaInquilinoB").style.visibility = 'hidden';
        }
        else if (visible == 1) {
            document.getElementById("divRicercaInquilinoA").style.visibility = 'visible';
            document.getElementById("divRicercaInquilinoB").style.visibility = 'visible';
        };
    };
};
function ControlModExport() {
    if (document.getElementById('frmModify') != null) {
        if (document.getElementById('frmModify').value != 0) {
            alert('Salvare le modifiche prima di procedere con l\'Export dei Dati!');
            if (document.getElementById('caricamento') != null) {
                document.getElementById('caricamento').style.visibility = 'hidden';
            };
            return false;
        };
    };
};
function TornaHome() {
    var path = window.location.pathname;
    var page = path.substring(path.lastIndexOf('/') + 1);
    if (page == 'Home.aspx') {
        self.close();
    } else {
        location.href = 'Home.aspx';
    };
};
function Procedure() {
    CenterPage('Procedure.aspx', 'ProcedureSiraper', 900, 700);
};
function ApriDettaglioSirAlloggio(id) {
    window.showModalDialog('SirAlloggio.aspx?ID=' + id + '&SLE=' + document.getElementById('MasterPage_MainContent_SLE').value + '&IdConnessione=' + document.getElementById("MasterPage_MainContent_idConnessione").value + '&SESCON=SIRAPER&IDS=' + document.getElementById('MasterPage_MainContent_idSiraper').value + '&IDSV=' + document.getElementById('MasterPage_MainContent_idSiraperVersione').value, window, 'status:no;toolbar=no;dialogWidth:1200px;dialogHeight:800px;dialogHide:true;help:no;scroll:no');
};