/* ********** MESSAGGGI PREDEFINITI ********** */
var Elimina = 'Sei sicuro di voler procedere con l\'eliminazione del dato?';
var NessunaSelezione = 'Nessun dato selezionato!';
/* ********** MESSAGGGI PREDEFINITI ********** */
/* ********** FUNZIONI GESTIONE DATAGRID ********** */
var Selezionato;
var OldColor;
var SelColo;
var validNavigation = false;
/* ********** FUNZIONI GESTIONE DATAGRID ********** */
/* ********** FUNZIONI GESTIONE JQUERY ********** */
function BeforeSubmit() {
    if (document.getElementById('HFnoRefresh')) {
        if (document.getElementById('HFnoRefresh').value == '1') {
            document.getElementById('HFnoRefresh').value = '0';
            validNavigation = false;
        } else {
            caricamento(1, 1);
            loading(0);
            if (validNavigation != null) {
                validNavigation = true;
            };
            if (document.getElementById('noClose').value == 1) {
                document.getElementById('noCloseRead').value = 1;
                document.getElementById('noClose').value = 0;
            };
        }
    } else {
        caricamento(1, 1);
        loading(0);
        if (validNavigation != null) {
            validNavigation = true;
        };
        if (document.getElementById('noClose').value == 1) {
            document.getElementById('noCloseRead').value = 1;
            document.getElementById('noClose').value = 0;
        };
    };
};
function AfterSubmit() {
    validNavigation = false;
    if (document.getElementById('HFExitForce')) {
        if (document.getElementById('HFExitForce').value == '1') {
            validNavigation = true;
        };
    };
    if (document.getElementById('noCloseRead').value == 1) {
        document.getElementById('noClose').value = 1;
        document.getElementById('noCloseRead').value = 0;
    };
};
function caricamento(valore, tipo) {
    if ('undefined' === typeof tipo) {
        tipo = 0;
    };
    if (tipo == 1) {
        if (document.getElementById('divOscura') != null) {
            document.getElementById('divOscura').style.display = 'block';
        };
    };
    if (document.getElementById('tipoSubmit') != null) {
        document.getElementById('tipoSubmit').value = valore;
    };
    window.focus();
};
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
    var percorso = 'image/load.gif';
    if (tipo == 0) {
        $(function () {
            $('#imageLoading').attr('src', percorso);
            $('#loading').append("<p>");
            $("#imageLoading").appendTo($("#loading"));
            $('#loading').append("</p><br />Attendere...L\'operazione potrebbe impiegare qualche secondo!");
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
            $('#imageLoading').attr('src', percorso);
            $('#loading').append("<p>");
            $("#imageLoading").appendTo($("#loading"));
            $('#loading').append("</p><br />Attendere...L\'operazione potrebbe impiegare qualche secondo!");
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
function message(titolo, testo, close, redirect) {
    if ('undefined' === typeof close) {
        close = 0;
    };
    if ('undefined' === typeof redirect) {
        redirect = '';
    };
    $(function () {
        $('#dialog').html('<p><span class="ui-icon ui-icon-info" style="float: left; margin: 0 7px 50px 0;"></span>' + testo + '</p>');
        $('#dialog').dialog({
            closeOnEscape: false,
            draggable: false,
            resizable: false,
            dialogClass: 'loadingScreenWindow',
            title: titolo,
            modal: true,
            buttons: { Ok: function () {
                $(this).dialog('close');
                if (close == 1) {
                    validNavigation = true;
                    self.close();
                }
                if (redirect != '') {
                    validNavigation = true;
                    location.replace(redirect);
                }
            }
            }
        });
    });
    window.focus();
};
/* ********** FUNZIONI GESTIONE JQUERY ********** */
/* ********** FUNZIONI GESTIONE DATI ********** */
function downloadFile(filePath) {
    location.replace('' + filePath + '');
};
/* ********** FUNZIONI GESTIONE DATI ********** */
/* ********** FUNZIONI GESTIONE EVENTI PAGINE ********** */
if (navigator.appName == 'Microsoft Internet Explorer') {
    document.onkeydown = $onkeydown;
}
else {
    window.document.addEventListener("keydown", TastoInvio, true);
};
function TastoInvio(e) {
    sKeyPressed1 = e.which;
    if (sKeyPressed1 == 112 || sKeyPressed1 == 115 || sKeyPressed1 == 116 || sKeyPressed1 == 117 || sKeyPressed1 == 118 || sKeyPressed1 == 122 || sKeyPressed1 == 123 || sKeyPressed1 == 60 || sKeyPressed1 == 91 || sKeyPressed1 == 92) {
        sKeyPressed1 = 0;
        e.preventDefault();
        e.stopPropagation();
    };
    if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
        if (sKeyPressed1 == 8) {
            sKeyPressed1 = 0;
            e.preventDefault();
            e.stopPropagation();
        };
        if (sKeyPressed1 == 13) {
            if (document.getElementById('HFbtnClickGo') != null) {
                if (document.getElementById('HFbtnClickGo').value != '') {
                    var idoggetto = document.getElementById('HFbtnClickGo').value;
                    document.getElementById('' + idoggetto + '').click();
                } else {
                    sKeyPressed1 = 0;
                    e.preventDefault();
                    e.stopPropagation();
                };
            } else {
                sKeyPressed1 = 0;
                e.preventDefault();
                e.stopPropagation();
            };
        };
    }
    else {
        if (sKeyPressed1 == 226) {
            sKeyPressed1 = 0;
            e.preventDefault();
            e.stopPropagation();
        };
        var el = document.activeElement;
        var multiline = el.tagName;
        if (multiline != 'TEXTAREA') {
            if (document.activeElement.readOnly == true) {
                if (sKeyPressed1 == 8) {
                    sKeyPressed1 = 0;
                    e.preventDefault();
                    e.stopPropagation();
                };
                if (sKeyPressed1 == 13) {
                    if (document.getElementById('HFbtnClickGo') != null) {
                        if (document.getElementById('HFbtnClickGo').value != '') {
                            var idoggetto = document.getElementById('HFbtnClickGo').value;
                            document.getElementById('' + idoggetto + '').click();
                        } else {
                            sKeyPressed1 = 0;
                            e.preventDefault();
                            e.stopPropagation();
                        };
                    } else {
                        sKeyPressed1 = 0;
                        e.preventDefault();
                        e.stopPropagation();
                        return false;
                    };
                };
            } else {
                if (sKeyPressed1 == 13) {
                    if (document.getElementById('HFbtnClickGo') != null) {
                        if (document.getElementById('HFbtnClickGo').value != '') {
                            var idoggetto = document.getElementById('HFbtnClickGo').value;
                            document.getElementById('' + idoggetto + '').click();
                        } else {
                            sKeyPressed1 = 0;
                            e.preventDefault();
                            e.stopPropagation();
                        };
                    } else {
                        sKeyPressed1 = 0;
                        e.preventDefault();
                        e.stopPropagation();
                        return false;
                    };
                };
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
        if (event.keyCode == 8) {
            event.keyCode = 0;
            return false;
        };
        if (event.keyCode == 13) {
            if (document.getElementById('HFbtnClickGo') != null) {
                if (document.getElementById('HFbtnClickGo').value != '') {
                    var idoggetto = document.getElementById('HFbtnClickGo').value;
                    document.getElementById('' + idoggetto + '').click();
                } else {
                    event.keyCode = 0;
                    return false;
                };
            } else {
                event.keyCode = 0;
                return false;
            };
        };
    }
    else {
        if (event.keyCode == 226) {
            event.keyCode = 0;
            return false;
        };
        if (document.activeElement.isMultiLine == false) {
            if (document.activeElement.readOnly == true) {
                if (event.keyCode == 8) {
                    event.keyCode = 0;
                    return false;
                };
                if (event.keyCode == 13) {
                    if (document.getElementById('HFbtnClickGo') != null) {
                        if (document.getElementById('HFbtnClickGo').value != '') {
                            var idoggetto = document.getElementById('HFbtnClickGo').value;
                            document.getElementById('' + idoggetto + '').click();
                        } else {
                            event.keyCode = 0;
                            return false;
                        };
                    } else {
                        event.keyCode = 0;
                        return false;
                    };
                };
            } else {
                if (event.keyCode == 13) {
                    if (document.getElementById('HFbtnClickGo') != null) {
                        if (document.getElementById('HFbtnClickGo').value != '') {
                            var idoggetto = document.getElementById('HFbtnClickGo').value;
                            document.getElementById('' + idoggetto + '').click();
                        } else {
                            event.keyCode = 0;
                            return false;
                        };
                    } else {
                        event.keyCode = 0;
                        return false;
                    };
                };
            };
        };
    };
};
/* ********** FUNZIONI GESTIONE EVENTI PAGINE ********** */
function CenterPageModal(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2) - 15;
    var top = (screen.height / 2) - (h / 2) - 15;
    var targetWin = window.showModalDialog(pageURL, window, 'status:no;toolbar=no;dialogWidth:' + w + 'px;dialogHeight:' + h + 'px;dialogHide:true;help:no;scroll:no;dialogtop:' + top + ';dialogleft:' + left);
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
    'quotes': /['\''&'\"']/g,
    'notnumbers': /[^\d]/g,
    'onlynumbers': /[^\d\-\,\.]/g,
    'numbers': /[^\d]/g
};
function valid(o, w) {
    o.value = o.value.replace(r[w], '');
};

function CompletaOra(e, obj) {
    var sKeyPressed;
    sKeyPressed = (window.event) ? event.keyCode : e.which;
    if (sKeyPressed < 48 || sKeyPressed > 57) {
        if (sKeyPressed != 8 && sKeyPressed != 0) {
            if (window.event) {
                event.keyCode = 0;
            } else {
                e.preventDefault();
            };
        };
    } else {
        if (obj.value.length == 2) {
            obj.value += ":";
        }
        else if (obj.value.length > 5) {
            var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
            if (selText.length == 0) {
                if (window.event) {
                    event.keyCode = 0;
                } else {
                    e.preventDefault();
                };
            };
        };
    };
};
function AutoDecimal(obj, numdec) {
    if (numdec == null) numdec = 2;
    obj.value = obj.value.replace(/\./g, '');
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
                    document.getElementById(obj.id).value = a.replace(/\./g, ',');
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
                    document.getElementById(obj.id).value = a.replace(/\./g, ',');
                };

            };
        }
        else {
            document.getElementById(obj.id).value = '';
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
        obj.value = obj.value.replace(/\./g, '');
    };
};
var oldValue;
function ModSommaAutomatica(obj, residuo) {
    var inserted;
    residuo = parseFloat(residuo);
    var Somma;
    var risultato;
    if (oldValue == '') { oldValue = 0 } else { oldValue = parseFloat(oldValue.replace(/\./g, '').replace(',', '.')) };
    if (obj.value == '') { inserted = 0 } else { inserted = parseFloat(obj.value.replace(/\./g, '').replace(',', '.')) };
    residuo = residuo + oldValue;
    if (obj.value != '') {
        if (inserted > residuo) {
            alert('Impossibile inserire un importo maggiore del residuo!');
            document.getElementById(obj.id).value = '';
            inserted = 0;
        }
    };

    if (oldValue != inserted) {
        Somma = parseFloat(document.getElementById('SumSelected').value.replace(/\./g, '').replace(',', '.'));
        risultato = parseFloat((Somma - oldValue) + inserted).toFixed(2);
        document.getElementById('SumSelected').value = formatEuro(risultato);
        document.getElementById('txtSommaSel').value = document.getElementById('SumSelected').value
        GestTableResidui(risultato);
    };
    oldValue = 0;
};

function SommaAutomatica(obj, residuo) {
    var inserted;
    residuo = parseFloat(residuo)
    if (obj.value == '') { inserted = 0 } else { inserted = parseFloat(obj.value.replace(/\./g, '').replace(',', '.')) };
    if (obj.value != '') {
        if (inserted > residuo) {
            alert('Impossibile inserire un importo maggiore del residuo!');
            document.getElementById(obj.id).value = '';
            inserted = 0;
        }
    };
    var Somma;
    var risultato;
    if (oldValue == '') { oldValue = 0 } else { oldValue = parseFloat(oldValue.replace(/\./g, '').replace(',', '.')) };
    if (oldValue != inserted) {
        Somma = parseFloat(document.getElementById('SumSelected').value.replace(/\./g, '').replace(',', '.'));
        risultato = parseFloat(((Somma - oldValue) + inserted).toFixed(2));
        document.getElementById('SumSelected').value = formatEuro(risultato);
        document.getElementById('txtSommaSel').value = document.getElementById('SumSelected').value
        GestTableResidui(risultato);
    };
    oldValue = 0;
};

function sommaChek(obj, residuo) {
    var sumSelect;
    var pagamento;
    pagamento = parseFloat(document.getElementById('txtImpPagamento').value.replace(/\./g, '').replace(',', '.'));
    document.getElementById('txtPagResoconto').value = document.getElementById('txtImpPagamento').value;

    if (pagamento > 0) {
        sumSelect = parseFloat(document.getElementById('SumSelected').value.replace(/\./g, '').replace(',', '.'));
        if (obj.checked == true) { sumSelect = sumSelect + parseFloat(residuo); } else { sumSelect = sumSelect - parseFloat(residuo); }
        document.getElementById('SumSelected').value = formatEuro(sumSelect);
        document.getElementById('txtSommaSel').value = formatEuro(sumSelect);
        GestTableResidui(residuo);
    }
    else {
        if (obj.checked == true) { obj.checked = false; alert('Prima di selezionare, definire un importo!'); };
    };


};

function GestTableResidui(residuo) {
    var pagamento = 0;
    var sumSelect;
    var resPagam;
    if (document.getElementById('txtPagResoconto')) {
        document.getElementById('txtPagResoconto').value = document.getElementById('txtImpPagamento').value;
        if (document.getElementById('txtImpPagamento').value.replace(/\./g, '').replace(',', '.') != '') {
            pagamento = parseFloat(document.getElementById('txtImpPagamento').value.replace(/\./g, '').replace(',', '.'));
        }

        if (!residuo) { residuo = 0; };
        sumSelect = parseFloat(document.getElementById('SumSelected').value.replace(/\./g, '').replace(',', '.'));
        resPagam = pagamento - sumSelect;
        //if (resPagam < 0) { resPagam = 0 };
        document.getElementById('txtSommaSel').value = formatEuro(sumSelect);
        document.getElementById('txtResResoconto').value = formatEuro(resPagam);
        document.getElementById('ResCredito').value = formatEuro(resPagam);
    }
};




function formatEuro(risultato) {
    risultato = parseFloat(risultato).toFixed(2) + '';
    var resulDecimal = '';
    if (risultato.substring(risultato.length - 3, 0).length >= 4) {
        var decimali = risultato.substring(risultato.length, risultato.length - 2);
        var dascrivere = risultato.substring(risultato.length - 3, 0);
        while (dascrivere.replace('-', '').length >= 4) {
            resulDecimal = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + resulDecimal
            dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
        };
        resulDecimal = dascrivere + resulDecimal + ',' + decimali;

    }
    else {
        resulDecimal = risultato.replace(/\./g, ',')
    }
    return resulDecimal;
};
function IsAssegno(obj) {

    if (obj.value == 5) {
        document.getElementById('lblNumAssegno').style.visibility = 'visible';
        document.getElementById('txtNumAssegno').style.visibility = 'visible';
        if (document.getElementById('lblDataAssegno') && document.getElementById('txtDataAssegno')) {
            document.getElementById('lblDataAssegno').style.visibility = 'visible';
            document.getElementById('txtDataAssegno').style.visibility = 'visible';
        }

    }
    else {

        document.getElementById('lblNumAssegno').style.visibility = 'hidden';
        document.getElementById('txtNumAssegno').style.visibility = 'hidden';
        if (document.getElementById('lblDataAssegno') && document.getElementById('txtDataAssegno')) {
            document.getElementById('lblDataAssegno').style.visibility = 'hidden';
            document.getElementById('txtDataAssegno').style.visibility = 'hidden';
            document.getElementById('txtDataAssegno').value = '';
            document.getElementById('txtNumAssegno').value = '';
        }

    };
};
function ConfPagam() {


    if (parseFloat(parseFloat(document.getElementById('ResCredito').value).toFixed(2)) > 0 && document.getElementById('tipoPagValue').value != -1) {
        msg = 'Procedere con il pagamento,\ne la scrittura del CREDITO in Partita Gestionale?'
    }
    else {
        if (parseFloat(parseFloat(document.getElementById('ResCredito').value).toFixed(2)) > parseFloat(parseFloat(document.getElementById('totResiduo').value).toFixed(2))) {
            msg = 'Procedere con il pagamento,\ne la scrittura del CREDITO in Partita Gestionale?'

        }
        else {
            msg = 'Procedere con il pagamento?'
        };

    };
    if (document.getElementById('containsRat')) {
        if (document.getElementById('containsRat').value == 1 && document.getElementById('tipoPagValue').value <= 0) {
            //msg = 'ATTENZIONE!A seguito dell\'incasso AUTOMATICO potrebbero venire pagate PARZIALMENTE delle bollette di RATEIZZAZIONE!\n' + msg
            msg = 'ATTENZIONE!\n' + msg;
        };
    }
    if (window.confirm(msg)) {
        document.getElementById("confPagamento").value = 1;
    }
    else {
        document.getElementById("confPagamento").value = 0;
    }
    GestTableResidui(document.getElementById('txtImpPagamento'));
};
function ConfMod() {
    if (parseFloat(parseFloat(document.getElementById('ResCredito').value).toFixed(2)) > 0 && document.getElementById('tipoPagValue').value != -1) {
        msg = 'Procedere con la modifica,\ne la scrittura del CREDITO in Partita Gestionale?'
    }
    else {
        if (parseFloat(parseFloat(document.getElementById('ResCredito').value).toFixed(2)) > parseFloat(parseFloat(document.getElementById('totResiduo').value).toFixed(2))) {
            msg = 'Procedere con la modifica,\ne la scrittura del CREDITO in Partita Gestionale?'

        }
        else {
            msg = 'Procedere con la modifica?'
        };
    };
    if (window.confirm(msg)) {
        document.getElementById("confPagamento").value = 1;
    }
    else {
        document.getElementById("confPagamento").value = 0;
    }

};


function AutoFill(obj, residuo) {
    document.getElementById(obj.id).value = residuo;
};

function ApriEventiIncassi() {
    window.open('ElIncassi.aspx?IDCONT=' + document.getElementById('vIdContratto').value + '&IDANA=' + document.getElementById('vIdAnagrafica').value + '&IDCONN=' + document.getElementById('vIdConnessione').value + '&SL=' + document.getElementById('SoloLettura').value, 'Eventi', '');
};

function ApriEventiIncassiRuoli() {
    window.open('ElIncassiRuoli.aspx?IDCONT=' + document.getElementById('vIdContratto').value + '&IDANA=' + document.getElementById('vIdAnagrafica').value + '&IDCONN=' + document.getElementById('vIdConnessione').value + '&SL=' + document.getElementById('SoloLettura').value, 'EventiR', '');
};

function ApriEventiIncassiIng() {
    window.open('ElIncassiIng.aspx?IDCONT=' + document.getElementById('vIdContratto').value + '&IDANA=' + document.getElementById('vIdAnagrafica').value + '&IDCONN=' + document.getElementById('vIdConnessione').value + '&SL=' + document.getElementById('SoloLettura').value, 'EventiI', '');
};


function ModIncasso() {
    if (document.getElementById('flEditDelable').value == 1) {
        var left = (screen.width / 2) - (1024 / 2);
        var top = (screen.height / 2) - (768 / 2);
        if (document.getElementById('idSelected').value != '0') {
            window.open('PagaModifica.aspx?IDINCASSO=' + document.getElementById('idSelected').value + '&IDANA=' + document.getElementById('vIdAnagrafica').value + '&IDCONT=' + document.getElementById('idContratto').value + '&IDCONN=' + document.getElementById('vIdConnessione').value + '&FLANNULLATO=' + document.getElementById('flAnnullata').value, 'ModifPagManuale', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);
        }
        else {
            alert('Nessun incasso selezionato!');
        };
    }
    else {
        alert('Impossibile modificare un incasso derivante da partita gestionale!');
    };

};

function ModIncassoRuoli() {

    var left = (screen.width / 2) - (1024 / 2);
    var top = (screen.height / 2) - (768 / 2);
    if (document.getElementById('idSelected').value != '0') {
        window.open('PagaModificaRuolo.aspx?IDINCASSO=' + document.getElementById('idSelected').value + '&IDANA=' + document.getElementById('vIdAnagrafica').value + '&IDCONT=' + document.getElementById('idContratto').value + '&IDCONN=' + document.getElementById('vIdConnessione').value + '&FLANNULLATO=' + document.getElementById('flAnnullata').value, 'ModifPagManuale', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);
    }
    else {
        alert('Nessun incasso selezionato!');
    };


};

function ModIncassoIng() {

    var left = (screen.width / 2) - (1024 / 2);
    var top = (screen.height / 2) - (768 / 2);
    if (document.getElementById('idSelected').value != '0') {
        window.open('PagaModificaIng.aspx?IDINCASSO=' + document.getElementById('idSelected').value + '&IDANA=' + document.getElementById('vIdAnagrafica').value + '&IDCONT=' + document.getElementById('idContratto').value + '&IDCONN=' + document.getElementById('vIdConnessione').value + '&FLANNULLATO=' + document.getElementById('flAnnullata').value, 'ModifPagManuale', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);
    }
    else {
        alert('Nessun incasso selezionato!');
    };


};

function VisualIncasso() {
    var left = (screen.width / 2) - (1024 / 2);
    var top = (screen.height / 2) - (768 / 2);
    if (document.getElementById('idSelected').value != '0') {
        window.showModalDialog('VisualIncasso.aspx?IDINCASSO=' + document.getElementById('idSelected').value + '&IDANA=' + document.getElementById('vIdAnagrafica').value + '&IDCONT=' + document.getElementById('idContratto').value + '&IDCONN=' + document.getElementById('vIdConnessione').value + '&FLANNULLATO=' + document.getElementById('flAnnullata').value + '&SL=' + document.getElementById('SoloLett').value, 'VisualPagManuale', 'status:no;dialogWidth:1024px;dialogHeight:768px;dialogHide:true;help:no;');
    }
    else {
        alert('Nessun incasso selezionato!');
    };

};

function VisualIncassoRuoli() {
    var left = (screen.width / 2) - (1024 / 2);
    var top = (screen.height / 2) - (768 / 2);
    if (document.getElementById('idSelected').value != '0') {
        window.open('VisualIncassoRuoli.aspx?IDINCASSO=' + document.getElementById('idSelected').value + '&IDANA=' + document.getElementById('vIdAnagrafica').value + '&IDCONT=' + document.getElementById('idContratto').value + '&IDCONN=' + document.getElementById('vIdConnessione').value + '&FLANNULLATO=' + document.getElementById('flAnnullata').value + '&SL=' + document.getElementById('SoloLett').value, 'VisualPagManualeR', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);
    }
    else {
        alert('Nessun incasso selezionato!');
    };

};

function VisualIncassoIng() {
    var left = (screen.width / 2) - (1024 / 2);
    var top = (screen.height / 2) - (768 / 2);
    if (document.getElementById('idSelected').value != '0') {
        window.open('VisualIncassoIng.aspx?IDINCASSO=' + document.getElementById('idSelected').value + '&IDANA=' + document.getElementById('vIdAnagrafica').value + '&IDCONT=' + document.getElementById('idContratto').value + '&IDCONN=' + document.getElementById('vIdConnessione').value + '&FLANNULLATO=' + document.getElementById('flAnnullata').value + '&SL=' + document.getElementById('SoloLett').value, 'VisualPagManualeI', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);
    }
    else {
        alert('Nessun incasso selezionato!');
    };

};

function FiltroBollette() {
    if (document.getElementById('ObbFiltro').value == 1) {
        var radioButtonlist = document.getElementsByName('rdbTipoIncasso');
        var selValue;
        for (var x = 0; x < radioButtonlist.length; x++) {
            if (radioButtonlist[x].checked) {
                if (document.getElementById('OldSelTipoPag').value != radioButtonlist[x].value) {
                    selValue = radioButtonlist[x].value;
                    document.getElementById('OldSelTipoPag').value = selValue;
                    //alert('Selezionato = ' + selValue);
                };
            };
        };
        if (selValue > 0) {
            var DDal = '';
            var DAl = '';
            var EmDal = '';
            var EmAl = '';
            if (document.getElementById('txtDataDal')) { DDal = document.getElementById('txtDataDal').value; };
            if (document.getElementById('txtDataAl')) { DAl = document.getElementById('txtDataAl').value; };
            if (document.getElementById('txtEmesDal')) { EmDal = document.getElementById('txtEmesDal').value; };
            if (document.getElementById('txtEmesAl')) { EmAl = document.getElementById('txtEmesAl').value; };
            CenterPageModal('ObbligoFiltro.aspx?IDCONT=' + document.getElementById('vIdContratto').value + '&DAL=' + DDal + '&AL=' + DAl + '&EDAL=' + EmDal + '&EAL=' + EmAl + '&NUMBOL=' + document.getElementById('FiltNumBol').value, 'ObbligoFiltro', 400, 230)
        };
    };
};

function ApriIncassiNonAttribuiti() {
    alert('Si sta per utilizzare la maschera per la gestione degli incassi non\nattribuiti.Una volta selezionato l\'assegno non sarà possibile\n modificare nè il suo importo nè il metodo di pagamento.\nPer modificare uno di questi due campi sarà necessario\nuscire dalla maschera cliccando sul tasto \"Esci\"!');
    window.showModalDialog('IncassiNonAttribuiti.aspx', 'IncassiNonAttribuiti', 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
};
function ConfAnnullo() {
    msg = 'Sei sicuro di voler annullare l\'operazione selezionata?'

    if (window.confirm(msg)) {
        document.getElementById("confAnnullo").value = 1;
    }
    else {
        document.getElementById("confAnnullo").value = 0;
    }

};
$(document).ready(function () {
    // al caricamento della pagina (document ready) eseguo gli script
    var altezzaPaginaIntera = $(window).height() - 5;
    var altezzaIncasso = 430;
    var altezzaContenuto = 0;
    if ($.browser.chrome) {
        altezzaContenuto = altezzaPaginaIntera - altezzaIncasso - 10;
    } else if ($.browser.mozilla) {
        altezzaContenuto = altezzaPaginaIntera - altezzaIncasso - 45;
    } else if ($.browser.msie) {
        altezzaContenuto = altezzaPaginaIntera - altezzaIncasso - 45;
    };
    $("#divBo").height(altezzaContenuto);
});
$(window).resize(function () {
    // ridimensiono il div anche al cambiamento della window
    // al caricamento della pagina (document ready) eseguo gli script
    var altezzaPaginaIntera = $(window).height() - 5;
    var altezzaIncasso = 430;
    var altezzaContenuto = 0;
    if ($.browser.chrome) {
        altezzaContenuto = altezzaPaginaIntera - altezzaIncasso - 10;
    } else if ($.browser.mozilla) {
        altezzaContenuto = altezzaPaginaIntera - altezzaIncasso - 45;
    } else if ($.browser.msie) {
        altezzaContenuto = altezzaPaginaIntera - altezzaIncasso - 45;
    };
    $("#divBo").height(altezzaContenuto);
});