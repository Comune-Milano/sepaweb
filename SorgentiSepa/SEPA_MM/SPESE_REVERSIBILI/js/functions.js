//function TastoInvio(e) {
//    sKeyPressed1 = e.which;
//    if (sKeyPressed1 == 13) {
//        e.preventDefault();
//    }
//}
//function $onkeydown() {
//    if (event.keyCode == 13) {
//        event.keyCode = '9';
//    }
//}
var Selezionato;
var sKeyPressed1;
function TastoInvio(e) {
    if (document.activeElement.isTextEdit == false) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13 || sKeyPressed1 == 8) {
            e.preventDefault();
        }
    }
    if (sKeyPressed1 == 116) {
        e.preventDefault();
    }
}
function $onkeydown() {
    if (event.keyCode == 13 || event.keyCode == 8) {
        if (document.activeElement.localName != "input" || document.activeElement.isTextEdit == false || document.activeElement.isContentEditable == false) {
            return false;
        }
    }
}


//    if (event.keyCode == 116) {
//        event.keyCode = 0;
//        event.cancelBubble = true;
//        event.returnValue = false;
//    }

//}
var r = {
    'special': /[\W]/g,
    'quotes': /['\''&'\"']/g,
    'notnumbersOnlyPositive': /[^\d\,]/g, //Modifica Marco 20/09/2012
    'notnumbers': /[^\d\,-]/g
}
function valid(o, w) {
    o.value = o.value.replace(r[w], '');
    return true;
}
function CompletaData(e, obj) {
    var sKeyPressed;
    sKeyPressed = (window.event) ? event.keyCode : e.which;
    if (sKeyPressed < 48 || sKeyPressed > 57) {
        if (sKeyPressed != 8 && sKeyPressed != 0) {
            if (window.event) {
                event.keyCode = 0;
            } else {
                e.preventDefault();
            }
        }
    } else {
        if (obj.value.length == 2) {
            obj.value += "/";
        } else if (obj.value.length == 5) {
            obj.value += "/";
        }
        else if (obj.value.length > 9) {
            var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
            if (selText.length == 0) {
                if (window.event) {
                    event.keyCode = 0;
                } else {
                    e.preventDefault();
                }
            }
        }
    }
}
function SostPuntVirg(e, obj) {
    var sKeyPressed;
    sKeyPressed = (window.event) ? event.keyCode : e.which;
    if ((sKeyPressed < 48 || sKeyPressed > 57) && (sKeyPressed != 44) && (sKeyPressed != 45) && (sKeyPressed != 46)) {
        if (sKeyPressed != 8 && sKeyPressed != 0) {
            if (window.event) {
                event.keyCode = 0;
            } else {
                e.preventDefault();
            }
        }
    } else {
        if (sKeyPressed == 46) {
            if (navigator.appName == 'Microsoft Internet Explorer') {
                event.keyCode = 0;
            } else {
                e.preventDefault();
            }
            obj.value += ',';
            obj.value = obj.value.replace('.', '');
        }
    }
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
                risultato = dascrivere + risultato + ',' + decimali;
                document.getElementById(obj.id).value = risultato;
            } else {
                document.getElementById(obj.id).value = a.replace('.', ',');
            }
        } else
            document.getElementById(obj.id).value = '';
    }
}
function AutoDecimal4(obj) {
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') != 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(4)
        if (a != 'NaN') {
            if (a.substring(a.length - 5, 0).length >= 4) {
                var decimali = a.substring(a.length, a.length - 4);
                var dascrivere = a.substring(a.length - 5, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {
                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                }
                risultato = dascrivere + risultato + ',' + decimali;
                document.getElementById(obj.id).value = risultato;
            } else {
                document.getElementById(obj.id).value = a.replace('.', ',');
            }
        } else
            document.getElementById(obj.id).value = '';
    }
}

function AutoDecimal6(obj) {
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') != 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(6)
        if (a != 'NaN') {
            if (a.substring(a.length - 7, 0).length >= 6) {
                var decimali = a.substring(a.length, a.length - 6);
                var dascrivere = a.substring(a.length - 7, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 6) {
                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 5) + risultato
                    dascrivere = dascrivere.substring(dascrivere.length - 5, 0)
                }
                risultato = dascrivere + risultato + ',' + decimali;
                document.getElementById(obj.id).value = risultato;
            } else {
                document.getElementById(obj.id).value = a.replace('.', ',');
            }
        } else
            document.getElementById(obj.id).value = '';
    }
}

function controlloBrowser() {
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
}


function caricamento(valore) {
    document.getElementById('MasterPage_tipoSubmit').value = valore;
}

function loading(tipo) {

    if (document.getElementById('MasterPage_tipoSubmit').value == 1) {
        tipo = 1;
    }
    if (document.getElementById('MasterPage_tipoSubmit').value == 2) {
        tipo = 2;
    }
    if (tipo == 0) {
        $(function () {
            //$('#loading').html('<table width="100%" style="text-align:center;"><tr><td><br /><img src="Immagini/load.gif" alt="" /></td></tr><tr><td><br />Attendere...</td></tr></table>');
            $('#imageLoading').attr('src', 'load.gif');
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
    }
    if (tipo == 1) {
        $(function () {
            //$('#loading').html('<table width="100%" style="text-align:center;"><tr><td><br /><img src="Immagini/load.gif" alt="" /></td></tr><tr><td><br />Attendere...Operazione in corso...</td></tr></table>');
            $('#imageLoading').attr('src', 'load.gif');
            $('#loading').append("<p>");
            $("#imageLoading").appendTo($("#loading"));
            $('#loading').append("</p><br />Attendere...");
            $('#loading').dialog({
                closeOnEscape: false,
                draggable: false,
                resizable: false,
                title: 'Operazione in corso',
                dialogClass: 'loadingScreenWindow',
                modal: true, buttons: {}
            });

        });
    }
    if (tipo == 2) {
    }
    document.getElementById('MasterPage_tipoSubmit').value = 0;
}

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
//function confirm(titolo, testo, bottone1, tipo1, funzione1, bottone2, tipo2, funzione2) {
//    $(function () {
//        $('#confirm').html('<p><span class=""ui-icon ui-icon-info"" style=""float: left; margin: 0 7px 50px 0;""></span>' + testo + '</p>');
//        $('#confirm').dialog({
//            autoOpen: true,
//            modal: true,
//            show: 'blind',
//            closeOnEscape: false,
//            draggable: false,
//            resizable: false,
//            title: titolo,
//            buttons: {
//                btn1: function () { { $(this).dialog('close'); if (tipo1 == 1) { message('Attenzione', funzione1); } else if (tipo1 == 2) { document.getElementById('' + funzione1 + '').click(); } else if (tipo1 == 0) { validNavigation = true; self.close(); } else { return false; }; } },
//                btn2: function () { $(this).dialog('close'); if (tipo2 == 1) { message('Attenzione', funzione2); } else if (tipo2 == 2) { document.getElementById('' + funzione2 + '').click(); } else if (tipo2 == 0) { validNavigation = true; self.close(); } else { return false; }; }
//            },
//            dialogClass: 'my-dialog'
//        });
//    });
//    $('.my-dialog .ui-button-text:contains(btn1)').text(bottone1);
//    $('.my-dialog .ui-button-text:contains(btn2)').text(bottone2);
//};
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

function onCommand(sender, args) {
    var com = args.get_commandName();
    switch (com) {
        case 'ExportToExcel':
            nascondi = 0;
            break;
        case 'Filter':
            nascondi = 0;
            break;
        case 'Sort':
            nascondi = 0;
            break;
        case 'PageSize':
            nascondi = 0;
            break;
        case 'Page':
            nascondi = 0;
            break;
    };
    caricamento(2);
};