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

/*-----------FUNZIONE PER DISATTIVARE ALCUNI TASTI 'DANNOSI' nelle maschere-------------------*/
function TastoInvio(e) {
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

function ConfermaEsci() {
    if (document.getElementById('noClose')) { document.getElementById('noClose').value = 1 }

    if (document.getElementById('MainContent_frmModify')) {
        if (document.getElementById('MainContent_frmModify').value == '1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche!Proseguire l\'uscita senza salvare?");
            if (chiediConferma == false) {
                document.getElementById('MainContent_frmModify').value = '111';
                if (document.getElementById('noClose')) { document.getElementById('noClose').value = 1 }
                return false;
            }
        }
    }
    else if (document.getElementById('frmModify')) {

        if (document.getElementById('frmModify').value == '1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche!Proseguire l\'uscita senza salvare?");
            if (chiediConferma == false) {
                document.getElementById('frmModify').value = '111';
                if (document.getElementById('noClose')) { document.getElementById('noClose').value = 1 }
                return false;
            }
        }
    }
}


/* inizio codice per intercettare il click sul chiudi*/
window.onbeforeunload = ConfChiudi;
window.onunload = Exit;

document.onmousemove = getMouse;

var myclose = false;

var posx = 0; var posy = 0;
function getMouse(e) {
    posx = 0; posy = 0;
    var ev = (!e) ? window.event : e; //IE:Moz
    if (ev.pageX) {//Moz
        posx = ev.pageX + window.pageXOffset;
        posy = ev.pageY + window.pageYOffset;
    }
    else if (ev.clientX) {//IE
        posx = ev.clientX //+ document.body.scrollLeft;
        posy = ev.clientY //+ document.body.scrollTop;
    }
    //    window.document.title = posx.toString() + ' ' + posy.toString() ;
}


function ConfChiudi(evt) {
    var e;
    if (!evt) { e = window.event; } else { e = evt; }
    if (document.getElementById('noClose')) {
        if ((posy < 20 || posx < 5 || posy >= document.documentElement.clientHeight || posx >= document.documentElement.clientWidth) && document.getElementById('noClose').value == 1) {
            return "Attenzione...E\' preferibile chiudere la finestra utilizzando il pulsante ESCI!";
        }
    }

}



function Exit() {
    if (document.getElementById('noClose')) {
        if ((posy < 20 || posx < 5 || posy >= document.documentElement.clientHeight || posx >= document.documentElement.clientWidth) && document.getElementById('noClose').value == 1) {

            if (document.getElementById('btnEsci') != null) {
                document.getElementById('btnEsci').click();
            }
            else if (document.getElementById('MainContent_btnEsci') != null) {
                document.getElementById('MainContent_btnEsci').click();

            }
        }
    }
}
/* fine codice per intercettare il click sul chiudi*/

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
        }
        obj.value += ',';
        obj.value = obj.value.replace('.', '');
    }

};
function downloadFile(filePath) {

    location.replace('' + filePath + '');

};
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

function EuroToLire(obj, where) {
    obj.value = obj.value.replace('.', '');

    if (obj.value.replace(',', '.') != 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(2)
        a = (a * 1936.27);
        a = parseFloat(a).toFixed(2);
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
                document.getElementById(where.id).value = risultato

            }
            else {
                document.getElementById(where.id).value = a.replace('.', ',')
            }


        }


    }

}


/* - - - - END funzioni gestione decimali e euro - - - */




function Inserimento() {
    document.getElementById("divInsIndirizzoA").style.visibility = 'visible';
    document.getElementById("divInsIndirizzoB").style.visibility = 'visible';
}
function UpdateIndirizzi() {
    if (document.getElementById("MainContent_Tab_Ubicazione1_idSelected").value != 0) {
        document.getElementById("divInsIndirizzoA").style.visibility = 'visible';
        document.getElementById("divInsIndirizzoB").style.visibility = 'visible';

    }
    else {
        alert('Seleziona una riga dalla lista');
    }
}
function scegliEdificio() {
    document.getElementById('divEdificioA').style.visibility = 'visible';
    document.getElementById('divEdificioB').style.visibility = 'visible';

}
function NuovoEdificio() {
    document.getElementById('divEdificioA').style.visibility = 'hidden';
    document.getElementById('divEdificioB').style.visibility = 'hidden';
    window.open('Edificio.aspx?IdConnessione=' + document.getElementById("MainContent_idConnessione").value + '&SESCON=IMMOBILE', 'NuovoEd', 'toolbar=no,resizable=yes,scrollbars=no')

}
function RicercaEdificio() {
    document.getElementById('divEdificioA').style.visibility = 'hidden';
    document.getElementById('divEdificioB').style.visibility = 'hidden';
    window.showModalDialog('RicercaEdificio.aspx?IdConnessione=' + document.getElementById("MainContent_idConnessione").value + '&SESCON=IMMOBILE', window, 'status:no;dialogWidth:800px;dialogHeight:600px;toolbar=no;dialogHide:true;help:no;scroll:no');

}
function AnnullaEdificio() {
    document.getElementById('divEdificioA').style.visibility = 'hidden';
    document.getElementById('divEdificioB').style.visibility = 'hidden';
}
function VisualizzaEdificio() {
    if (document.getElementById("idSelected").value != 0) {
        if (navigator.appName == "Microsoft Internet Explorer") {

            dialogArguments.window.open('Edificio.aspx?IdConnessione=' + document.getElementById("idConnessione").value + '&SESCON=IMMOBILE&ID=' + document.getElementById("idSelected").value, 'VisualEdificio', 'toolbar=no,resizable=yes,scrollbars=no');
        }
        else {
            window.open('Edificio.aspx?IdConnessione=' + document.getElementById("idConnessione").value + '&SESCON=IMMOBILE&ID=' + document.getElementById("idSelected").value, 'VisualEdificio', 'toolbar=no,resizable=yes,scrollbars=no');

        }
    }
    else alert('Seleziona un edificio dalla lista');
}

function ApriEdificio() {
    if (document.getElementById("MainContent_Tab_Fabbricati1_idSelected").value != 0) {
        window.open('Edificio.aspx?IdConnessione=' + document.getElementById("MainContent_idConnessione").value + '&SESCON=IMMOBILE&ID=' + document.getElementById("MainContent_Tab_Fabbricati1_idSelected").value + '&UPDATE=1', 'VisualEdificio', 'toolbar=no,resizable=yes,scrollbars=no');
        document.getElementById("MainContent_Tab_Fabbricati1_idSelected").value = 0;
    }
    else { alert('Seleziona un edificio dalla lista'); }
}
function UpdateIndirizziEdificio() {
    if (document.getElementById("Tab_IndEdificio1_idSelected").value != 0) {
        document.getElementById("divInsIndirizzoA").style.visibility = 'visible';
        document.getElementById("divInsIndirizzoB").style.visibility = 'visible';

    }
    else {
        alert('Seleziona una riga dalla lista');
    }


}
function DeleteConfirmFabbricati() {
    if (document.getElementById('MainContent_Tab_Fabbricati1_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare l\'edificio selezionato?");
        if (Conferma == false) {
            document.getElementById('MainContent_Tab_Fabbricati1_ConfElimina').value = '0';
        }
        else {
            document.getElementById('MainContent_Tab_Fabbricati1_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}

function NuovaUnitaImmob() {
    window.open('UnitaIm.aspx?IdConnessione=' + document.getElementById('idConnessione').value + '&IDEDIFICIO=' + document.getElementById('idEdificio').value + '&SESCON=IMMOBILE&X=1&LE=0&ID=0', 'NuovaUi');
}

function ApriUnitaImmob() {
    if (document.getElementById("Tab_UnImmob1_idSelected").value != 0) {
        window.open('UnitaIm.aspx?IdConnessione=' + document.getElementById("idConnessione").value + '&SESCON=IMMOBILE&X=1&LE=0&ID=' + document.getElementById("Tab_UnImmob1_idSelected").value + '&IDEDIFICIO=' + document.getElementById('idEdificio').value, 'DettaglioUi');
        document.getElementById("Tab_UnImmob1_idSelected").value = 0;
    }
    else { alert('Seleziona un unita\' immobiliare dalla lista'); }
}

function ApriImpianti() {
    if (document.getElementById("Tab_Impianti1_idSelected").value != 0) {
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'ME') {
            window.open('../IMPIANTI/Imp_Meteoriche.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,toolbar=no,top=0,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'AN') {
            window.open('../IMPIANTI/Imp_Antincendio.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,toolbar=no,top=0,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'CF') {
            window.open('../IMPIANTI/Imp_CannaFumaria.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,toolbar=no,top=0,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'EL') {
            window.open('../IMPIANTI/Imp_Elettrico.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,toolbar=no,top=0,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'ID') {
            window.open('../IMPIANTI/Imp_Idrico.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,toolbar=no,top=0,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'SO') {
            window.open('../IMPIANTI/Imp_Sollevamento.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,toolbar=no,top=0,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'TR') {
            window.open('../IMPIANTI/Imp_Teleriscaldamento.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,toolbar=no,top=0,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'TA') {
            window.open('../IMPIANTI/Imp_RiscaldamentoA.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,top=0,toolbar=no,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'TE') {
            window.open('../IMPIANTI/Imp_Riscaldamento.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,top=0,toolbar=no,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'TU') {
            window.open('../IMPIANTI/Imp_Tutela.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,top=0,toolbar=no,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'GA') {
            window.open('../IMPIANTI/Imp_Gas.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,top=0,toolbar=no,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'CI') {
            window.open('../IMPIANTI/Imp_Citofonico.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,top=0,toolbar=no,left=0,width=816');
        }
        if (document.getElementById("Tab_Impianti1_idTipoSelected").value == 'TV') {
            window.open('../IMPIANTI/Imp_TV.aspx?CO=-1&ED=' + document.getElementById("Tab_Impianti1_idEdificio").value + '&IM=-1&ID=' + document.getElementById("Tab_Impianti1_idSelected").value + '&ORD=COMPLESSO&SL=1', 'Impianto', 'height=625,top=0,toolbar=no,left=0,width=816');
        }
    }
    else { alert('Seleziona un edificio dalla lista'); }
}
function ApriCondomini() {
    window.open('../Condomini/Condominio.aspx?IdCond=' + document.getElementById('Tab_Condomini1_idSelected').value + '&SL=1', 'CONDOMINIO', 'height=580,top=0,toolbar=no,left=0,width=800');
}


function scegliTerreno() {
    document.getElementById('divTerrenoA').style.visibility = 'visible';
    document.getElementById('divTerrenoB').style.visibility = 'visible';

}

function AnnullaTerreno() {
    document.getElementById('divTerrenoA').style.visibility = 'hidden';
    document.getElementById('divTerrenoB').style.visibility = 'hidden';
}

function NuovoTerreno() {
    document.getElementById('divTerrenoA').style.visibility = 'hidden';
    document.getElementById('divTerrenoB').style.visibility = 'hidden';
    window.open('Terreno.aspx?IdConnessione=' + document.getElementById("MainContent_idConnessione").value + '&SESCON=IMMOBILE', 'NuovoTerr', 'toolbar=no,resizable=yes,scrollbars=no');


}

function RicercaTerreno() {
    document.getElementById('divTerrenoA').style.visibility = 'hidden';
    document.getElementById('divTerrenoB').style.visibility = 'hidden';
    window.showModalDialog('RicercaTerreno.aspx?IdConnessione=' + document.getElementById("MainContent_idConnessione").value + '&SESCON=IMMOBILE', window, 'status:no;toolbar=no;dialogWidth:1000px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');

}

function VisualizzaTerreno() {
    if (document.getElementById("idSelected").value != 0) {

        if (navigator.appName == "Microsoft Internet Explorer") {

            dialogArguments.window.open('Terreno.aspx?IdConnessione=' + document.getElementById("idConnessione").value + '&SESCON=IMMOBILE&ID=' + document.getElementById("idSelected").value, 'NuovoTerr', 'toolbar=no,resizable=yes,scrollbars=no');
        }
        else {
            window.open('Terreno.aspx?IdConnessione=' + document.getElementById("idConnessione").value + '&SESCON=IMMOBILE&ID=' + document.getElementById("idSelected").value, 'NuovoTerr', 'toolbar=no,resizable=yes,scrollbars=no');
        }
    }
    else alert('Seleziona un terreno dalla lista');
}
function ApriTerreno() {
    if (document.getElementById("MainContent_Tab_Terreni1_idSelected").value != 0) {
        window.open('Terreno.aspx?IdConnessione=' + document.getElementById("MainContent_idConnessione").value + '&SESCON=IMMOBILE&ID=' + document.getElementById("MainContent_Tab_Terreni1_idSelected").value, 'NuovoTerr', 'toolbar=no,resizable=yes,scrollbars=no');
        document.getElementById("MainContent_Tab_Terreni1_idSelected").value = 0;
    }
    else { alert('Seleziona un terreno dalla lista'); }
}

function DeleteConfirmTerreni() {
    if (document.getElementById('MainContent_Tab_Terreni1_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare il terreno selezionato?");
        if (Conferma == false) {
            document.getElementById('MainContent_Tab_Terreni1_ConfElimina').value = '0';
        }
        else {
            document.getElementById('MainContent_Tab_Terreni1_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}
//**********PROPRIETA

function InsertServitu() {
    document.getElementById("divInsVincoliA").style.visibility = 'visible';
    document.getElementById("divInsVincoliB").style.visibility = 'visible';
}
function UpdatePropieta() {
    if (document.getElementById("MainContent_Tab_Propieta1_idSelected").value != 0) {
        document.getElementById("divInsVincoliA").style.visibility = 'visible';
        document.getElementById("divInsVincoliB").style.visibility = 'visible';

    }
    else {
        alert('Seleziona una riga dalla lista');
    }
}
//**********TITOLI di PROPRIETA

function InsertTitolo() {
    document.getElementById("divPreTitoloA").style.visibility = 'visible';
    document.getElementById("divPreTitoloB").style.visibility = 'visible';
}
function ModTitolo() {
    if (document.getElementById("MainContent_Tab_Propieta1_idTitoloSelected").value != 0) {
        window.showModalDialog('TitoloPropieta.aspx?id=' + document.getElementById("MainContent_Tab_Propieta1_idTitoloSelected").value + '&IdConnessione=' + document.getElementById("MainContent_idConnessione").value + '&SESCON=IMMOBILE&IDIMMOBILE=' + document.getElementById("MainContent_idImmobile").value, window, 'status:no;toolbar=no;dialogWidth:1000px;dialogHeight:640px;dialogHide:true;help:no;scroll:no');

    }
    else {
        alert('Seleziona una riga dalla lista');
    }
}

function ShowItem(obj) {
    if (obj == 1) {

        document.getElementById("MainContent_Tab_Propieta1_lblDesc1").value = 'Notaio (Nome e Cognome)';
        document.getElementById("MainContent_Tab_Propieta1_lblDesc2").value = 'N.Rep/Racc.';
        document.getElementById("MainContent_Tab_Propieta1_lblDesc2").style.visibility = 'visible';
        document.getElementById("MainContent_Tab_Propieta1_txtNumTitolo").style.visibility = 'visible';

    }

    if (obj == 2) {

        document.getElementById("MainContent_Tab_Propieta1_lblDesc1").value = 'Autorità';
        document.getElementById("MainContent_Tab_Propieta1_lblDesc2").value = 'Numero';
        document.getElementById("MainContent_Tab_Propieta1_lblDesc2").style.visibility = 'visible';
        document.getElementById("MainContent_Tab_Propieta1_txtNumTitolo").style.visibility = 'visible';


    }

    if (obj == 3) {
        document.getElementById("MainContent_Tab_Propieta1_lblDesc1").value = 'Organo';
        document.getElementById("MainContent_Tab_Propieta1_lblDesc2").value = 'N.Delibera/Anno';
        document.getElementById("MainContent_Tab_Propieta1_lblDesc2").style.visibility = 'visible';
        document.getElementById("MainContent_Tab_Propieta1_txtNumTitolo").style.visibility = 'visible';



    }

    if (obj == 4) {
        document.getElementById("MainContent_Tab_Propieta1_lblDesc1").value = 'Spec. Documento';
        document.getElementById("MainContent_Tab_Propieta1_lblDesc2").style.visibility = 'hidden';
        document.getElementById("MainContent_Tab_Propieta1_txtNumTitolo").style.visibility = 'hidden';



    }

}

function ApriTitoloProp(id) {

    if (id != -1) {

        var chiediConferma
        chiediConferma = window.confirm("Attenzione...Titolo di Propietà esistente!Procedere con la sua modifica?");
        if (chiediConferma == false) {
            return false;
        }
        else {
            window.showModalDialog('TitoloPropieta.aspx?id=' + id + '&IdConnessione=' + document.getElementById("MainContent_idConnessione").value + '&SESCON=IMMOBILE&TIPODIR=' + document.getElementById("MainContent_Tab_Propieta1_cmbTipoDirReale").value + '&N=' + document.getElementById("MainContent_Tab_Propieta1_txtNumTitolo").value + '&ORIGINE=' + document.getElementById("MainContent_Tab_Propieta1_txtOrigineTitolo").value + '&IDIMMOBILE=' + document.getElementById("MainContent_idImmobile").value, window, 'status:no;toolbar=no;dialogWidth:1000px;dialogHeight:640px;dialogHide:true;help:no;scroll:no');

        }

    }
    else {
        window.showModalDialog('TitoloPropieta.aspx?id=' + id + '&IdConnessione=' + document.getElementById("MainContent_idConnessione").value + '&SESCON=IMMOBILE&TIPODIR=' + document.getElementById("MainContent_Tab_Propieta1_cmbTipoDirReale").value + '&N=' + document.getElementById("MainContent_Tab_Propieta1_txtNumTitolo").value + '&ORIGINE=' + document.getElementById("MainContent_Tab_Propieta1_txtOrigineTitolo").value + '&IDIMMOBILE=' + document.getElementById("MainContent_idImmobile").value, window, 'status:no;toolbar=no;dialogWidth:1000px;dialogHeight:640px;dialogHide:true;help:no;scroll:no');
    }
    document.getElementById("MainContent_Tab_Propieta1_txtNumTitolo").value = '';
    document.getElementById("MainContent_Tab_Propieta1_txtOrigineTitolo").value = '';
}


function CallScriviTesto(daAggiornare) {

    if (daAggiornare) {
        if (document.getElementById(daAggiornare).value != '') {
            document.getElementById('txtTesto').value = document.getElementById(daAggiornare).value;
        }

        document.getElementById('divTestoA').style.visibility = 'visible';
        document.getElementById('divTestoB').style.visibility = 'visible';
        document.getElementById('txtToUpdate').value = daAggiornare;

    }

}

function SettaViaCod(source, eventArgs) {
    //document.getElementById('<%= MainContent_Tab_Ubicazione1_txtCodIndirizzo.ClientID %>').value = "PUCCETTONE"
    document.getElementById("MainContent_Tab_Ubicazione1_txtCodIndirizzo").value = eventArgs.get_value();
    document.getElementById("MainContent_Tab_Ubicazione1_codVia").value = eventArgs.get_value();
}
function SettaComuCod(source, eventArgs) {

    document.getElementById("MainContent_Tab_Ubicazione1_txtCodComu").value = eventArgs.get_value();
    $find("AutoCompleteEx").set_contextKey(document.getElementById("MainContent_Tab_Ubicazione1_txtCodComu").value);
    document.getElementById("MainContent_Tab_Ubicazione1_txtCodIndirizzo").value = '';
    document.getElementById("MainContent_Tab_Ubicazione1_myTextBox").value = '';

}
function SettaViaCodEdif(source, eventArgs) {
    //document.getElementById('<%= MainContent_Tab_Ubicazione1_txtCodIndirizzo.ClientID %>').value = "PUCCETTONE"
    document.getElementById("Tab_IndEdificio1_txtCodIndirizzo").value = eventArgs.get_value();
    document.getElementById("Tab_IndEdificio1_codVia").value = eventArgs.get_value();
}
function SettaComuCodEdif(source, eventArgs) {

    document.getElementById("Tab_IndEdificio1_txtCodComu").value = eventArgs.get_value();
    $find("AutoCompleteEx").set_contextKey(document.getElementById("Tab_IndEdificio1_txtCodComu").value);
    document.getElementById("Tab_IndEdificio1_txtCodIndirizzo").value = '';
    document.getElementById("Tab_IndEdificio1_myTextBox").value = '';

}

function SettaViaCodTerr(source, eventArgs) {
    document.getElementById("Tab_IndTerreno1_txtCodIndirizzo").value = eventArgs.get_value();
    document.getElementById("Tab_IndTerreno1_codVia").value = eventArgs.get_value();
}

function SettaViaCodRicerca(source, eventArgs) {
    //document.getElementById('<%= MainContent_Tab_Ubicazione1_txtCodIndirizzo.ClientID %>').value = "PUCCETTONE"
    document.getElementById("MainContent_Tab_InfoGenerali1_txtCodIndirizzo").value = eventArgs.get_value();
    document.getElementById("MainContent_Tab_InfoGenerali1_codVia").value = eventArgs.get_value();
}
function SettaComuCodRicerca(source, eventArgs) {

    document.getElementById("MainContent_Tab_InfoGenerali1_txtCodComu").value = eventArgs.get_value();
    $find("AutoCompleteEx").set_contextKey(document.getElementById("MainContent_Tab_InfoGenerali1_txtCodComu").value);
    document.getElementById("MainContent_Tab_InfoGenerali1_txtCodIndirizzo").value = '';
    document.getElementById("MainContent_Tab_InfoGenerali1_myTextBox").value = '';

}

function UpdateIndViario() {
    if (document.getElementById("MainContent_idSelected").value != 0) {
        document.getElementById("divInsIndirizzoA").style.visibility = 'visible';
        document.getElementById("divInsIndirizzoB").style.visibility = 'visible';

    }
    else {
        alert('Seleziona una riga dalla lista');
    }
}

/* ANTONELLO */
function InsPropUnitImm1() {
    document.getElementById('divInsIndirizzoA').style.visibility = 'visible';
    document.getElementById('divInsIndirizzoB').style.visibility = 'visible';
}
function InsPropUnitImm2() {
    document.getElementById('divInsIndirizzoC').style.visibility = 'visible';
    document.getElementById('divInsIndirizzoD').style.visibility = 'visible';
}
function InsPropUnitImm3() {
    document.getElementById('divInsIndirizzoE').style.visibility = 'visible';
    document.getElementById('divInsIndirizzoF').style.visibility = 'visible';
}
function InsPropUnitImm4() {
    document.getElementById('divInsIndirizzoG').style.visibility = 'visible';
    document.getElementById('divInsIndirizzoH').style.visibility = 'visible';
}
function InsPropUnitImm41() {
    document.getElementById('divInsIndirizzoG').style.visibility = 'hidden';
    document.getElementById('divInsIndirizzoH').style.visibility = 'hidden';
}
function InsPropUnitImm5() {
    document.getElementById('divInsIndirizzoI').style.visibility = 'visible';
    document.getElementById('divInsIndirizzoL').style.visibility = 'visible';
}
function InsPropUnitImm6() {
    document.getElementById('divInsIndirizzoM').style.visibility = 'visible';
    document.getElementById('divInsIndirizzoN').style.visibility = 'visible';
}
function ConfermaEsciUI() {
    if (document.getElementById('frmModify')) {
        if (document.getElementById('frmModify').value == '1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione... Sono state apportate delle modifiche! Proseguire l\'uscita senza salvare?");
            if (chiediConferma == true) {
                document.getElementById('frmModify').value = '2';
            }
            else {
                document.getElementById('frmModify').value = '1';
            }
        }
    }
}
function SalvaconModify() {
    if (document.getElementById('frmModify')) {
        if (document.getElementById('frmModify').value == '1') {
            alert('Le modifiche effettuate saranno conservate solo se salvate su Edificio, altrimenti saranno perse!.')
        }
    }
}
function DeleteConfirmAdDimens() {
    if (document.getElementById('Tab_AdDimens1_HFtxtId').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
        if (Conferma == false) {
            document.getElementById('Tab_AdDimens1_txtConfElimina').value = '0';
        }
        else {
            document.getElementById('Tab_AdDimens1_txtConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}
function DeleteConfirmAdVarConf() {
    if (document.getElementById('Tab_AdVarConf1_HFtxtId').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
        if (Conferma == false) {
            document.getElementById('Tab_AdVarConf1_txtConfElimina').value = '0';
        }
        else {
            document.getElementById('Tab_AdVarConf1_txtConfElimina').value = '1';
        }
    }
}
function DeleteConfirmAdNorm() {
    if (document.getElementById('Tab_AdNormativo1_HFtxtId').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
        if (Conferma == false) {
            document.getElementById('Tab_AdNormativo1_txtConfElimina').value = '0';
        }
        else {
            document.getElementById('Tab_AdNormativo1_txtConfElimina').value = '1';
        }
    }
}
function ApriEventiUI() {
    window.open('EventiUI.aspx?IdUnitaImm=' + document.getElementById("hfIdUnitaImm").value + '&SESCON=' + document.getElementById("sescon").value);
}
function DeleteConfirmCategoriaCat() {
    if (document.getElementById('MainContent_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare la categoria selezionata?");
        if (Conferma == false) {
            document.getElementById('MainContent_ConfElimina').value = '0';
        }
        else {
            document.getElementById('MainContent_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}
function UpCatCatastale() {
    if (document.getElementById('MainContent_idSelected').value != 0) {
        document.getElementById('divInsIndirizzoA').style.visibility = 'visible';
        document.getElementById('divInsIndirizzoB').style.visibility = 'visible';
    }
    else {
        alert('Nessuna categoria selezionata!');
    }
}
function InsCatCatastale() {
    document.getElementById('divInsIndirizzoA').style.visibility = 'visible';
    document.getElementById('divInsIndirizzoB').style.visibility = 'visible';
}


function ApriImmobile() {
    if (document.getElementById('MainContent_idSelezionato').value != 0) {
        location.replace('Proprieta.aspx?ID=' + document.getElementById('MainContent_idSelezionato').value);
    }
    else {
        alert('Selezionare l\'immobile da visualizzare!');
    }
}

// Maschere Supporto Tab Unita Immobiliari
function DeleteConfirmMSUI() {
    if (document.getElementById('MainContent_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare la riga selezionata?");
        if (Conferma == false) {
            document.getElementById('MainContent_ConfElimina').value = '0';
        }
        else {
            document.getElementById('MainContent_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}
function UpMSUI() {
    if (document.getElementById('MainContent_idSelected').value != 0) {
        document.getElementById('divInsIndirizzoA').style.visibility = 'visible';
        document.getElementById('divInsIndirizzoB').style.visibility = 'visible';
    }
    else {
        alert('Nessuna riga selezionata!');
    }
}
function InsMSUI() {
    document.getElementById('divInsIndirizzoA').style.visibility = 'visible';
    document.getElementById('divInsIndirizzoB').style.visibility = 'visible';
}
function hideMSUI() {
    document.getElementById('divInsIndirizzoA').style.visibility = 'hidden';
    document.getElementById('divInsIndirizzoB').style.visibility = 'hidden';
}
//

function DeleteConfirmTitolo() {
    if (document.getElementById('MainContent_Tab_Propieta1_idTitoloSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare il titolo selezionato?");
        if (Conferma == false) {
            document.getElementById('MainContent_Tab_Propieta1_ConfElimina').value = '0';
        }
        else {
            document.getElementById('MainContent_Tab_Propieta1_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}

function DeleteConfirmServ() {
    if (document.getElementById('MainContent_Tab_Propieta1_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare il vincolo selezionato?");
        if (Conferma == false) {
            document.getElementById('MainContent_Tab_Propieta1_ConfElimina').value = '0';
        }
        else {
            document.getElementById('MainContent_Tab_Propieta1_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}

function DeleteConfirmUbicazione() {
    if (document.getElementById('MainContent_Tab_Ubicazione1_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare l'indirizzo selezionato?");
        if (Conferma == false) {
            document.getElementById('MainContent_Tab_Ubicazione1_ConfElimina').value = '0';
        }
        else {
            document.getElementById('MainContent_Tab_Ubicazione1_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}

function NascondiButton() {
    document.getElementById('MainContent_Tab_Ubicazione1_btnaddinv').style.visibility = 'hidden';
    document.getElementById('MainContent_Tab_Fabbricati1_btnaddedi').style.visibility = 'hidden';
    document.getElementById('MainContent_Tab_Terreni1_btnaddter').style.visibility = 'hidden';
    document.getElementById('MainContent_Tab_Propieta1_btnaddprop').style.visibility = 'hidden';
    document.getElementById('MainContent_Tab_Propieta1_btnaddvin').style.visibility = 'hidden';
}

function DeleteConfirmViario() {
    if (document.getElementById('MainContent_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare l\'indirizzo selezionato?");
        if (Conferma == false) {
            document.getElementById('MainContent_ConfElimina').value = '0';
        }
        else {
            document.getElementById('MainContent_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}


function DeleteConfirmIndEdificio() {
    if (document.getElementById('Tab_IndEdificio1_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare l\'indirizzo selezionato?");
        if (Conferma == false) {
            document.getElementById('Tab_IndEdificio1_ConfElimina').value = '0';
        }
        else {
            document.getElementById('Tab_IndEdificio1_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}


function DeleteConfirmIndTerreno() {
    if (document.getElementById('Tab_IndTerreno1_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare l\'indirizzo selezionato?");
        if (Conferma == false) {
            document.getElementById('Tab_IndTerreno1_ConfElimina').value = '0';
        }
        else {
            document.getElementById('Tab_IndTerreno1_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}
function DeleteConfirmInfoGenerali() {
    if (document.getElementById('MainContent_Tab_InfoGenerali1_idSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare l\'indirizzo selezionato?");
        if (Conferma == false) {
            document.getElementById('MainContent_Tab_InfoGenerali1_ConfElimina').value = '0';
        }
        else {
            document.getElementById('MainContent_Tab_InfoGenerali1_ConfElimina').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}

// FUNZIONE PER RENDERE UN NUMERO CON APPROSSIMAZIONE DECIMILLESIMALE

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
                risultato = dascrivere + risultato + ',' + decimali
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
// *************************
function RicercaLiberaTerreno() {
    window.showModalDialog('RicercaLiberaTerreno.aspx', window, 'status:no;toolbar=no;dialogWidth:1000px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');

}
// CALCOLO VALORE CATASTALE IN UNITA IMMOBILIARE

function CalcoloValCat(obj, where) {
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') != 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(2)
        a = (a * 151.30);
        a = parseFloat(a).toFixed(2);
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
                document.getElementById(where.id).value = risultato
            }
            else {
                document.getElementById(where.id).value = a.replace('.', ',')
            }
        }
    }
}

// FINE CALCOLO VALORE CATASTALE IN UNITA IMMOBILIARE
function RicercaLiberaEdificio() {
    window.showModalDialog('RicercaLiberaEdificio.aspx', window, 'status:no;toolbar=no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');

}
// CALCOLO VALORE INVENTARIO IN UNITA IMMOBILIARE

function CalcoloValInv(where, dataacq, valcatastale, quotaposs, valacquisto) {
    var dataacquisizione = dataacq.value;
    var valorecatastale = valcatastale.value.replace(/\./gi, '');
    var quotapossesso = quotaposs.value.replace(/\./gi, '');
    var valoreacquisto = valacquisto.value.replace(/\./gi, '');
    var a;
    if (dataacquisizione != 0) {
        if (dataacquisizione < 1995) {
            a = (valorecatastale.replace(',', '.')) * (quotapossesso.replace(',', '.'));
        }
        else {
            if (valoreacquisto != '') {
                a = valoreacquisto.replace(',', '.');
            }
            else {
                a = valorecatastale.replace(',', '.');
            }
        }
    }
    else {
        if (valoreacquisto != '') {
            a = valoreacquisto.replace(',', '.');
        }
        else {
            a = valorecatastale.replace(',', '.');
        }
    }
    if (a != '') {
        if (a.substring(a.length - 3, 0).length >= 4) {
            var decimali = a.substring(a.length, a.length - 2);
            var dascrivere = a.substring(a.length - 3, 0);
            var risultato = '';
            while (dascrivere.replace('-', '').length >= 4) {
                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
            }
            risultato = dascrivere + risultato + ',' + decimali
            document.getElementById(where.id).value = risultato
        }
        else {
            document.getElementById(where.id).value = a.replace('.', ',')
        }
    }
}

// FINE CALCOLO VALORE INVENTARIO IN UNITA IMMOBILIARE

// FUNZIONE PER LA SELEZIONE ONCLICK

function selectall(txtselezione) {
    var text_val = eval("txtselezione");
    text_val.focus();
    text_val.select();
}
function DeleteConfirmTerreniCatasto() {
    if (document.getElementById('MainContent_Tab_Catasto1_idTerrenoSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare il terreno selezionato?");
        if (Conferma == false) {
            document.getElementById('MainContent_Tab_Catasto1_ConfEliminaTerreno').value = '0';
        }
        else {
            document.getElementById('MainContent_Tab_Catasto1_ConfEliminaTerreno').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}

function DeleteConfirmEdificiCatasto() {
    if (document.getElementById('MainContent_Tab_Catasto1_idEdificioSelected').value != 0) {
        var Conferma;
        Conferma = window.confirm("Attenzione...Confermi di voler eliminare l'edificio selezionato?");
        if (Conferma == false) {
            document.getElementById('MainContent_Tab_Catasto1_ConfEliminaEdificio').value = '0';
        }
        else {
            document.getElementById('MainContent_Tab_Catasto1_ConfEliminaEdificio').value = '1';
        }
    }
    else { alert('Non hai selezionato nessuna riga!.') }
}
function ApriModuloStandard(percorso, title) {
    var w = 1300;
    var h = 700;
    var left = ((screen.width / 2) - (w / 2)) - 15;
    var top = ((screen.height / 2) - (h / 2)) - 15;
    var targetWin = window.open(percorso, title.replace(/ /g, ''), 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};