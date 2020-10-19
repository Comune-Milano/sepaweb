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
        if (document.activeElement.localName != "input" || document.activeElement.isTextEdit == false || document.activeElement.isContentEditable == false ) {
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

function mask(str, textbox, loc, delim) {
    var locs = loc.split(',');

    for (var i = 0; i <= locs.length; i++) {
        for (var k = 0; k <= str.length; k++) {
            if (k == locs[i]) {
                if (str.substring(k, k + 1) != delim) {
                    str = str.substring(0, k) + delim + str.substring(k, str.length);
                }
            }
        }
    }

    primacoppia = str.substr(0, 2)
    secondacoppia = str.substr(3, 2)
    quadrupla = str.substr(6, 4)
    //CONVERTO I VALORI STRINGA IN NUMERI
    numero = parseInt(primacoppia, 10)
    numero1 = parseInt(secondacoppia, 10)
    numero2 = parseInt(quadrupla, 10)
    //estraggo le posizioni relative agli slash
    primoslash = str.substr(2, 1)
    secondoslash = str.substr(5, 1)
    //CALCOLO LA LUNGHEZZA DELLE VARIABILE CHE CONTENGONO I NUMERI
    primalunghezza = primacoppia.length
    secondalunghezza = secondacoppia.length
    terzalunghezza = quadrupla.length
    if ((primalunghezza == 2) && (primoslash == "/") && (numero >= 1) && (numero <= 31) && (secondalunghezza == 2) && (secondoslash == "/") && (numero1 >= 1) && (numero1 <= 12) && (terzalunghezza == 4) && (numero2 >= 1800) && (numero2 <= 3000)) {
        textbox.value = str;
    }
    else {
        if (textbox.value != '') {
            textbox.value = '';
            alert("Digitare gg/mm/aaaa oppure ggmmaaaa");
        }
    }
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
            //$('#loading').html('<table width="100%" style="text-align:center;"><tr><td><br /><img src="Immagini/load.gif" alt="" /></td></tr><tr><td><br />Attendere...L\'operazione potrebbe impiegare qualche minuto!</td></tr></table>');
            $('#imageLoading').attr('src', 'load.gif');
            $('#loading').append("<p>");
            $("#imageLoading").appendTo($("#loading"));
            $('#loading').append("</p><br />Attendere...L\'operazione potrebbe impiegare qualche minuto!");
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
        //        $(function () {
        //            $('#loading').html('<table width="100%" style="text-align:center;"><tr><td><br /><img src="Immagini/load.gif" /></td></tr><tr><td><br />Attendere...</td></tr></table>');
        //            $('#loading').dialog({
        //                closeOnEscape: false,
        //                draggable: false,
        //                resizable: false,
        //                title: 'Caricamento in corso',
        //                dialogClass: 'loadingScreenWindow',
        //                modal: true, buttons: {}
        //            });
        //        });

    }

}