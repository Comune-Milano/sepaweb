var Selezionato;
var OldColor;
var SelColo;
var validNavigation = false;
var WidthFinestraStandard = 1300;
var HeightFinestraStandard = 750;
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

function CompletaDataTelerik(sender, args) {
    var keyCode = args.get_keyCode();
    if (keyCode != 9) {
        if ((keyCode < 48) || (keyCode > 57)) {
            if (keyCode != 8) {
                args.set_cancel(true);
            };
        };
        var testo = sender._textBoxElement.value;
        var testolen = testo.length;
        if (testolen == 10) {
            var selinizio = sender._textBoxElement.selectionStart;
            var selfine = sender._textBoxElement.selectionEnd;
            if ((selinizio == 0 && selfine == 10) == false) {
                if (keyCode != 8) {
                    args.set_cancel(true);
                };
            } else {
                if ((keyCode < 48) || (keyCode > 51)) {
                    if (keyCode != 8) {
                        args.set_cancel(true);
                    };
                };
            };
        } else {
            if (testolen == 0) {
                if ((keyCode < 48) || (keyCode > 51)) {
                    if (keyCode != 8) {
                        args.set_cancel(true);
                    };
                };
            } else if (testolen == 1) {
                if (testo == 0) {
                    if ((keyCode < 49) || (keyCode > 57)) {
                        if (keyCode != 8) {
                            args.set_cancel(true);
                        };
                    };
                } else if (testo == 3) {
                    if ((keyCode < 48) || (keyCode > 49)) {
                        if (keyCode != 8) {
                            args.set_cancel(true);
                        };
                    };
                };
            } else if (testolen == 2) {
                if ((keyCode < 48) || (keyCode > 49)) {
                    if (keyCode != 8) {
                        args.set_cancel(true);
                    };
                } else {
                    sender._textBoxElement.value = testo + '/';
                };
            } else if (testolen == 3) {
                if ((keyCode < 48) || (keyCode > 49)) {
                    if (keyCode != 8) {
                        args.set_cancel(true);
                    };
                };
            } else if (testolen == 4) {
                var n = testo.substr(3, 1);
                if (n == 0) {
                    if ((keyCode < 49) || (keyCode > 57)) {
                        if (keyCode != 8) {
                            args.set_cancel(true);
                        };
                    };
                } else if (n == 1) {
                    if ((keyCode < 48) || (keyCode > 50)) {
                        if (keyCode != 8) {
                            args.set_cancel(true);
                        };
                    };
                };
            } else if (testolen == 5) {
                if (keyCode != 8) {
                    sender._textBoxElement.value = testo + '/';
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
                    if (document.getElementById('' + idoggetto + '')) {
                        document.getElementById('' + idoggetto + '').click();
                    };
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
        if ((multiline != 'TEXTAREA') && (multiline != 'INPUT')) {
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
                            if (document.getElementById('' + idoggetto + '')) {
                                document.getElementById('' + idoggetto + '').click();
                            };
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
                            if (document.getElementById('' + idoggetto + '')) {
                                document.getElementById('' + idoggetto + '').click();
                            };
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
                    if (document.getElementById('' + idoggetto + '')) {
                        document.getElementById('' + idoggetto + '').click();
                    };
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
                            if (document.getElementById('' + idoggetto + '')) {
                                document.getElementById('' + idoggetto + '').click();
                            };
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
                            if (document.getElementById('' + idoggetto + '')) {
                                document.getElementById('' + idoggetto + '').click();
                            };
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
    if (tipo == 0) {
        $(function () {
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
function confirm(titolo, testo, bottone1, tipo1, funzione1, bottone2, tipo2, funzione2) {
    $(function () {
        $('#confirm').html('<p><span class=""ui-icon ui-icon-info"" style=""float: left; margin: 0 7px 50px 0;""></span>' + testo + '</p>');
        $('#confirm').dialog({
            autoOpen: true,
            modal: true,
            show: 'blind',
            closeOnEscape: false,
            draggable: false,
            resizable: false,
            title: titolo,
            buttons: {
                btn1: function () { { $(this).dialog('close'); if (tipo1 == 1) { message('Attenzione', funzione1); } else if (tipo1 == 2) { document.getElementById('' + funzione1 + '').click(); } else if (tipo1 == 0) { validNavigation = true; self.close(); } else { return false; }; } },
                btn2: function () { $(this).dialog('close'); if (tipo2 == 1) { message('Attenzione', funzione2); } else if (tipo2 == 2) { document.getElementById('' + funzione2 + '').click(); } else if (tipo2 == 0) { validNavigation = true; self.close(); } else { return false; }; }
            },
            dialogClass: 'my-dialog'
        });
    });
    $('.my-dialog .ui-button-text:contains(btn1)').text(bottone1);
    $('.my-dialog .ui-button-text:contains(btn2)').text(bottone2);
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
    var targetWin = window.showModalDialog(pageURL, title, 'status:no;toolbar=no;dialogWidth:' + w + 'px;dialogHeight:' + h + 'px;dialogHide:true;help:no;scroll:no;dialogtop:' + top + ';dialogleft:' + left);
};
function BeforeSubmit() {
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
/* ***** NEWS ***** */
function setNewsModulo() {
    if (document.getElementById('HFNewsModulo').value == '') {
        $('<li />', { html: 'Nessun evento.' }).appendTo('ul.ticker')
    } else {
        var news = document.getElementById('HFNewsModulo').value;
        var arraynews = news.split('♫');
        var idNews = '';
        var messaggioNews = '';
        var titleNews = '';
        for (var i = 0; i < arraynews.length - 1; i++) {
            if (arraynews[i] != '') {
                idNews = arraynews[i].substring(arraynews[i].lastIndexOf("♪") + 1);
                messaggioNews = arraynews[i].substring(0, arraynews[i].indexOf("♪"));
                titleNews = arraynews[i].substring(arraynews[i].indexOf("♪") + 1, arraynews[i].lastIndexOf("♪"));
                $('<li />', { html: '<a href="javascript:ApriNewsBacheca(' + idNews + ');"><span title=' + titleNews + '>' + messaggioNews + '</span></a>' }).appendTo('ul.ticker');
            };
        };
    };
};
function ApriNewsBacheca(ID) {
    CenterPage2('../NewsModulo.aspx?T=1&ID=' + ID, 'News' + ID, 800, 500);
};
/* ***** NEWS ***** */
/* ***** NOTIFY ***** */
function setNotifyModulo() {
    if (document.getElementById('HFNotifyNews').value == '') {
        $.notify('Nessuna notifica.', {
            clickToHide: true,
            autoHide: true,
            autoHideDelay: 5000,
            globalPosition: 'bottom right',
            className: 'info'
        });
    } else {

    };
};
/* ***** NOTIFY ***** */
/* ***** AMBIENTE ***** */
var c1 = '#E0E4E3';
var c2 = '#006600';
var testo = '';
function colore1() {
    codice = '<font size="4" color=' + c1 + '><b>' + testo + '</b></font>';
    if (document.getElementById("testo")) {
        document.getElementById("testo").innerHTML = codice;
    };
    attesa = window.setTimeout("colore2();", 500);
};
function colore2() {
    codice = '<font size="4" color=' + c2 + '><b>' + testo + '</b></font>';
    if (document.getElementById("testo")) {
        document.getElementById("testo").innerHTML = codice;
    };
    attesa = window.setTimeout("colore1();", 500);
};
function avvia() {
    if (document.getElementById('HFSepaTest').value == '1') {
        testo = 'AMBIENTE DI TEST';
    } else if (document.getElementById('HFSepaTest').value == '2') {
        testo = 'AMBIENTE DI PRE - PRODUZIONE';
    };
    if (document.getElementById("testo")) {
        document.getElementById("testo").style.visibility = 'visible';
    };
    attesa = window.setTimeout("colore1();", 500);
};
/* ***** AMBIENTE ***** */
/* ***** MENU REDIRECT ***** */
function TornaHome() {
    if (document.getElementById('optMenu').value == '0') {
        var path = window.location.pathname;
        var page = path.substring(path.lastIndexOf('/') + 1);
        if (page == 'Home.aspx') {
            self.close();
        } else {
            location.href = 'Home.aspx';
        };
    } else {
        message('Attenzione', Messaggio.NoGo);
    };
};
function CallPageFromMenu(Page) {
    if (document.getElementById('optMenu') != null) {
        if (document.getElementById('optMenu').value == '0') {
            location.href = Page;
        } else {
            apriAlert(Messaggio.NoGo, 300, 150, 'Attenzione', null, null);
            if (document.getElementById('NavigationMenu')) {
                document.getElementById('NavigationMenu').style.visibility = 'visible';
                document.getElementById('divLoadingHome').style.visibility = 'hidden';
            }
        };
    } else {
        location.href = Page;
    };

};
/* ***** MENU REDIRECT ***** */
/* ***** RAD ALERT ***** */
function apriAlert(testo, larghezza, altezza, titolo, callback, img) {
    var alertTelerik = radalert(testo, larghezza, altezza, titolo, callback, img);
    alertTelerik = alertTelerik.set_behaviors();
};
function apriConfirm(testo, funzione, larghezza, altezza, titolo, img) {
    var confirmTelerik = radconfirm(testo, funzione, larghezza, altezza, null, titolo, img);
    confirmTelerik = confirmTelerik.set_behaviors();
};
function MultiConfermaJS(btnToClik, titolo, messaggio, w, h) {
    if ('undefined' === typeof w) {
        w = 400;
    };
    if ('undefined' === typeof h) {
        h = 150;
    };
    apriConfirm(messaggio, function callbackFn(arg) { if (arg == true) { clickEliminaJS(btnToClik.id) } }, w, h, titolo, null);
};
 function clickEliminaJS(btnToClik) {
    document.getElementById(btnToClik).click();
};
/* ***** RAD ALERT ***** */
/* ***** FUNZIONI TELERIK ***** */
function SelfCloseNoPostback(sender, args) {
    args.set_cancel(self.close());
};
function NoBeforeLoading(sender, args) {
    if (document.getElementById('HFBeforeLoading')) {
        document.getElementById('HFBeforeLoading').value = '1';
    };
};
function openWindow(sender, args, nomeRad) {
    var radwindow = $find(nomeRad);
    radwindow.show();
};
function ModificaElemento(sender, args, nomeRad, elementoSelezionato) {
    if (document.getElementById(elementoSelezionato)) {
        if (document.getElementById(elementoSelezionato).value != '') {
            var radwindow = $find(nomeRad);
            radwindow.show();
        } else {
            apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
            args.set_cancel(true);
        };
    } else {
        apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
        args.set_cancel(true);
    };
};
function ModificaElementoJs(nomeRad, elementoSelezionato) {
    if (document.getElementById(elementoSelezionato)) {
        if (document.getElementById(elementoSelezionato).value != '') {
            var radwindow = $find(nomeRad);
            radwindow.show();
        } else {
            apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
        };
    } else {
        apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
    };
};
function delElement(sender, args, elementoSelezionato, bottoneDaCliccare) {
    if (document.getElementById(elementoSelezionato)) {
        if (document.getElementById(elementoSelezionato).value != '') {
            apriConfirm(Messaggio.Elemento_Elimina, function (arg) { eliminaElemento(arg, bottoneDaCliccare); }, 300, 150, Messaggio.Titolo_Conferma, null);
        } else {
            apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
        };
    };
};
function eliminaElemento(arg, bottoneDaCliccare) {
    if (arg == true) {
        document.getElementById(bottoneDaCliccare).click();
    };
};
function deleteElementTelerik(sender, args, elementoSelezionato) {
    if (document.getElementById('NavigationMenu')) {
        document.getElementById('NavigationMenu').style.visibility = 'hidden';
    }
    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            this.click();
        }
    });
    if (document.getElementById(elementoSelezionato).value != '') {

        apriConfirm(Messaggio.Elemento_Elimina, callBackFunction, 300, 150, Messaggio.Titolo_Conferma, null);
    } else {

        apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);

    };
    args.set_cancel(true);
};
function disabledElementTelerik(sender, args, elementoSelezionato, flAttivo) {
    if (document.getElementById(flAttivo).value == '1') {
        if (document.getElementById('NavigationMenu')) {
            document.getElementById('NavigationMenu').style.visibility = 'hidden';
        }
        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
            if (shouldSubmit) {
                this.click();
            }
        });
        if (document.getElementById(elementoSelezionato).value != '') {

            apriConfirm(Messaggio.Elemento_Disattiva, callBackFunction, 300, 150, Messaggio.Titolo_Conferma, null);
        } else {
            apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
        };
    } else {
        if (document.getElementById(elementoSelezionato).value != '') {
            apriAlert(Messaggio.Elemento_Gia_Disabilitato, 300, 150, Messaggio.Titolo_Conferma, null, null);
        } else {
            apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
        }
    };
    args.set_cancel(true);
};
function closeWindow(sender, args, nomeRad) {
    var radwindow = $find(nomeRad);
    radwindow.close();
};
function loadingAjaxComplete(sender, args) {
    //COMPLETE LOADING AJAX
};
/* ***** FUNZIONI TELERIK ***** */
//readonly sulle combo
function OnClientLoadHandler(sender) {
    sender.get_inputDomElement().readOnly = "readonly";
};
//sul focus apertura datepicker
function CalendarDatePicker(sender, args) {
    sender.get_owner().showPopup();
};
function CalendarDatePickerHide(sender, args) {
    sender.get_owner().hidePopup();
};
function daysBetween(dataIniziale, dataFinale) {
    if (dataIniziale > dataFinale) {
        return 0;
    } else {
        return Math.round(Math.abs((+dataIniziale) - (+dataFinale)) / 8.64e7);
    };
};
function maxValue(obj, maxVal) {
    if (maxVal == null) maxVal = 9999999999999;
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') != 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(2);
        if (a != 'NaN') {
            if (maxVal > 0) {
                if (a > maxVal) {
                    document.getElementById(obj.id).value = '';
                    message('Attenzione', 'Impossibile inserire un valore maggiore di ' + maxVal);
                };
            };
        }
        else {
            document.getElementById(obj.id).value = '';
        };
    };
};
/* ***** FUNZIONI TELERIK ***** */

function confermaEsci(tipo, modificato) {
    if (modificato == '1') {
        radconfirm("Sono state apportate delle modifiche. Uscire ugualmente?", function (sender, args) { confirmCallBackFn(sender, args, tipo); }, 300, 150, null, "Attenzione", null);

    } else {
        if ((modificato == 0) || (modificato == null)) {
            validNavigation = true;
            if (tipo == 1) {
                self.close();
            } else {
                location.href = 'Home.aspx';
            };
        }

    };
};
function confirmCallBackFn(sender, args, tipo) {
    if (sender == true) {
        validNavigation = true;
        if (tipo == 1) {
            self.close();
        } else {
            location.href = 'Home.aspx';
        };

    };
};