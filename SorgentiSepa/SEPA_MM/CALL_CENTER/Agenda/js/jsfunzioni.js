var Selezionato;
var OldColor;
var SelColo;
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
if (navigator.appName == 'Microsoft Internet Explorer') {
    document.onkeydown = $onkeydown;
}
else {
    window.document.addEventListener("keydown", TastoInvio, true);
};
function caricamentoincorso() {
    if (document.getElementById('caricamento') != null) {
        document.getElementById('caricamento').style.visibility = 'visible';
    };
};
function DettagliAppuntamenti(giorno, mese, anno, filiale, idSegnalazione) {
    window.open('DettagliAppuntamenti.aspx?DATA=' + anno + mese + giorno + '&FILIALE=' + filiale + '&IDS=' + idSegnalazione, 'DettagliAppuntamenti', 'resizable=yes,statusbar=no,toolbar=no,width=1000px,height=770px,scrollbar=no');
};
function InserisciAppuntamento(giorno) {
    //alert(giorno);
};
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
    