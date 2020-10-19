/*-----------FUNZIONE PER DISATTIVARE ALCUNI TASTI 'DANNOSI' nelle maschere-------------------*/
function TastoInvio(e) {
    var sKeyPressed1;
    if (document.activeElement.isTextEdit == true && document.activeElement.isContentEditable == false) {
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
    if (document.activeElement.isTextEdit == true && document.activeElement.isContentEditable == false) {
        if (event.keyCode == 13 || event.keyCode == 8) {
            event.keyCode = 0;
            event.cancelBubble = true;
            event.returnValue = false;
        }
    }
    if (event.keyCode == 116) {
        event.keyCode = 0;
        event.cancelBubble = true;
        event.returnValue = false;
    }
}
if (navigator.appName == 'Microsoft Internet Explorer') {
    document.onkeydown = $onkeydown;
}
else {
    window.document.addEventListener("keydown", TastoInvio, true);
}
/*-----------FUNZIONE TastoInvio per evitare la pressione erronea del tasto INVIO nelle maschere-------------------*/
function downloadFile(filePath) {
    location.replace('' + filePath + '');
};
/*---------------FUNZIONE CompletaData per l'autocompletamento delle date---------------------------*/

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
var r = {
    'special': /[\W]/g,
    'quotes': /['\''&'\"']/g,
    'notnumbers': /[^\d]/g,
    'onlynumbers': /[^\d\-\,\.]/g
}
function valid(o, w) {
    o.value = o.value.replace(r[w], '');
}
function componentenucleo(opzione) {
    if (opzione == 0) {
        document.getElementById('divComponenteA').style.visibility = 'visible';
        document.getElementById('divComponenteB').style.visibility = 'visible';
    }
    else {
        document.getElementById('divComponenteA').style.visibility = 'hidden';
        document.getElementById('divComponenteB').style.visibility = 'hidden';
    }
}
function componentenucleocoabitante(opzione) {
    if (opzione == 0) {
        document.getElementById('divComponenteC').style.visibility = 'visible';
        document.getElementById('divComponenteD').style.visibility = 'visible';
    }
    else {
        document.getElementById('divComponenteC').style.visibility = 'hidden';
        document.getElementById('divComponenteD').style.visibility = 'hidden';
    }
}
function redditonucleo(opzione) {
    if (opzione == 0) {
        document.getElementById('divRedditoA').style.visibility = 'visible';
        document.getElementById('divRedditoB').style.visibility = 'visible';
    }
    else {
        document.getElementById('divRedditoA').style.visibility = 'hidden';
        document.getElementById('divRedditoB').style.visibility = 'hidden';
    }
}
function StampaDomanda() {
    window.open('StampaRicevuta.aspx?ID=' + document.getElementById('ContentPlaceHolder1_idDomanda').value, 'StampaRicevuta', 'width=800,height=600,top=30,left=30,toolbar=no,resizable=yes,scrollbars=yes,menubar=yes');
    window.open('StampaDomanda.aspx?id=' + document.getElementById('ContentPlaceHolder1_idDomanda').value, 'StampaDomanda', 'width=800,height=600,top=40,left=40,toolbar=no,resizable=yes,scrollbars=yes,menubar=yes');
}
function ApriCF() {
    window.showModalDialog('../cf/CodFiscale.aspx?Cog=' + document.getElementById('ContentPlaceHolder1_TabContainer1_TabPanel1_txtCognome').value + '&Nom=' + document.getElementById('ContentPlaceHolder1_TabContainer1_TabPanel1_txtNome').value, '_blank', '')
}
function ApriCFNR() {
    window.showModalDialog('../cf/CodFiscale.aspx?Cog=' + document.getElementById('ContentPlaceHolder1_TabContainer1_TabPanel2_Tab_Nucleo1_txtCog').value + '&Nom=' + document.getElementById('ContentPlaceHolder1_TabContainer1_TabPanel2_Tab_Nucleo1_txtNom').value, '_blank', '')
}
function ApriCFNC() {
    window.showModalDialog('../cf/CodFiscale.aspx?Cog=' + document.getElementById('ContentPlaceHolder1_TabContainer1_TabPanel4_Tab_Dichiara1_txtCog').value + '&Nom=' + document.getElementById('ContentPlaceHolder1_TabContainer1_TabPanel4_Tab_Dichiara1_txtNom').value, '_blank', '')
}
if (document.all) {
    function blink_show() {
        blink_tags = document.all.tags('blink');
        blink_count = blink_tags.length;
        for (i = 0; i < blink_count; i++) {
            blink_tags[i].style.visibility = 'visible';
        }
        window.setTimeout('blink_hide()', 700);
    }
    function blink_hide() {
        blink_tags = document.all.tags('blink');
        blink_count = blink_tags.length;
        for (i = 0; i < blink_count; i++) {
            blink_tags[i].style.visibility = 'hidden';
        }
        window.setTimeout('blink_show()', 250);
    }
    window.onload = blink_show;
}