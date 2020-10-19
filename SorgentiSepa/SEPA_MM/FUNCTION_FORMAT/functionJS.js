function TastoInvio(e) {
    sKeyPressed1 = e.which;
    if (sKeyPressed1 == 13) {
        e.preventDefault();
    }
}
function $onkeydown() {
    if (event.keyCode == 13) {
        event.keyCode = '9';
    }
}
var r = {
    'special': /[\W]/g,
    'quotes': /['\''&'\"']/g,
    'notnumbersOnlyPositive': /[^\d\,]/g,//Modifica Marco 20/09/2012
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
function controlloBrowser() {
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
}