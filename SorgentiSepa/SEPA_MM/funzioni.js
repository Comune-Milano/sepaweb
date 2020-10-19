/* FUNZIONI LOCK PAGE */
var idPageLock;
function getIdPageLock() {
    $.ajax({
        url: 'SepacomLock.svc/getPageLock',
        dataType: "json",
        data: JSON.stringify(''),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        success: onSuccessLock,
        dataFilter: function (data) { return data; },
        error: undefined
    });
};
function onSuccessLock(results, context, methodName) {
    idPageLock = results.d;
    idPageLock = idPageLock.replace('"', '').replace('"', '').replace('null', '');
};
/* FUNZIONI LOCK PAGE */
/* MODULO STANDARD */
function ApriModuloStandard(percorso, title) {
    var w = 1300;
    var h = 700;
    var left = ((screen.width / 2) - (w / 2)) - 15;
    var top = ((screen.height / 2) - (h / 2)) - 15;
    var targetWin = window.open(percorso, title.replace(/ /g, ''), 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
function ApriModuloStandardTelerik(percorso, title) {
    var w = 1300;
    var h = 700;
    var left = ((screen.width / 2) - (w / 2)) - 15;
    var top = ((screen.height / 2) - (h / 2)) - 15;
    getIdPageLock();
    var index = percorso.indexOf('?');
    if (index > 0) {
        percorso = percorso + '&KEY=' + idPageLock;
    } else {
        percorso = percorso + '?KEY=' + idPageLock;
    };
    var targetWin = window.open(percorso, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
/* MODULO STANDARD */
/* ARPA LOMBARIDIA */
function ApriArpaLombardia() {
    ApriModuloStandardTelerik('ARPA_LOMBARDIA/Home.aspx', 'ARPA_LOMBARDIA');
};
/* ARPA LOMBARIDIA */
function ApriAccessoCondominiTest() {
    ApriModuloStandardTelerik('CONDOMININEW/Home.aspx', 'CondominiNew');
};
function ApriAccessoCondomini() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 660) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('Condomini/menu.htm', 'Condomini', 'height=660,top=' + TopPosition + ',left=' + LeftPosition + ',width=960,scrollbars=no');
};
function ApriAccessoFornitori() {
    ApriModuloStandard('FORNITORI/Home.aspx', 'FORNITORI');
};
function ApriAccessoSicurezza() {
    window.open('SICUREZZA/Home.aspx', 'SICUREZZA', 'height=' + screen.height + ',top=0,left=0,width=' + screen.width + ',scrollbars=yes,resizable=yes,toolbar=no');
};
function ApriAccessoSiraper() {
    window.open('SIRAPER/Home.aspx', 'SIRAPER', 'height=' + screen.height + ',top=0,left=0,width=' + screen.width + ',scrollbars=yes,resizable=yes,toolbar=no');
};
function ApriAccessoRilievo() {
    var win = null;
    window.open('RILEVAZIONI/Home.aspx', 'GestioneContatti', 'height=' + screen.height + ',top=0,left=0,width=' + screen.width + ',scrollbars=no,resizable=yes');
};
function ApriAccessoArchivio() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('ARCHIVIO/menu.htm', 'Archivio', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=960,scrollbars=no');
    //alert('Non ancora Disponibile!');
}

function ApriAccessoPreSloggio() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('PRE_SLOGGIO/menu.htm', 'PreSloggio', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=920,scrollbars=no');
    //alert('Non ancora Disponibile!Modulo in fase di Analisi!');
}

function ApriAccessoMassive() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('STAMPE_MASSIVE/menu.htm', 'StampeMassive', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=960,scrollbars=no');
    //alert('Non ancora Disponibile!');
}

function ApriAccessoSatisfaction() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('SATISFACTION/menu.htm', 'Satisfaction', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=920,scrollbars=no');
    //alert('Non ancora Disponibile!');
}
function ApriAccessoMorosita() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 610) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('MOROSITA/menu.htm', 'GestMorosita', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=920,scrollbars=no');
}

function ApriAccessoGestAutonoma() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 610) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('GestioneAutonoma/menu.htm', 'GestAutonoma', 'height=608,top=' + TopPosition + ',left=' + LeftPosition + ',width=960,scrollbars=no');
    //alert('Non ancora Disponibile!');
}

function ApriAccessoGestioneContatti() {
    var win = null;
    window.open('GESTIONE_CONTATTI/Home.aspx', 'GestioneContatti', 'height=' + screen.height + ',top=0,left=0,width=' + screen.width + ',scrollbars=no,resizable=yes');
}

function ApriAccessoCicloPassivo() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('CICLO_PASSIVO/menu.htm', 'Ciclo', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=920,scrollbars=no');
}

function ApriAccessoNuovoCicloPassivo() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    //window.open('CICLO_PASSIVO/CicloPassivoHome.aspx', 'Ciclo Passivo', 'height=700,top=' + TopPosition + ',left=' + LeftPosition + ',width=1300,scrollbars=no');
    CenterPage('CICLO_PASSIVO/CicloPassivoHome.aspx', 'NCICLOPASSIVO', 1300, 700);
}

function ApriAccessoSpeseReversibili() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    //window.open('CICLO_PASSIVO/CicloPassivoHome.aspx', 'Ciclo Passivo', 'height=700,top=' + TopPosition + ',left=' + LeftPosition + ',width=1300,scrollbars=no');
    CenterPage('SPESE_REVERSIBILI/Default.aspx', 'SPESEREV', 1300, 700);
}

function CenterPage(pageURL, title, w, h) {
    var left = ((screen.width / 2) - (w / 2)) - 15;
    var top = ((screen.height / 2) - (h / 2)) - 15;
    var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};



function ApriAccessoContabilita() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('Contabilita/menu.htm', 'Contabilita', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=920,scrollbars=no');
}


function ApriAccessoImpianti() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('IMPIANTI/menu.htm', 'a', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=960,scrollbars=no');
}


function ApriANAUT(ID) {
    var win = null;
    alert(ID);
    //win=window.open('../ANAUT/max.aspx?LE=1&ID='+ID,'Dichiarazione','height=800,top=0,left=0,width=600,scrollbars=no');

}

function ApriStatoDomanda(indice) {
    var win = null;
    win = window.open('StatoDomanda.aspx?ID=' + indice, 'Stato', 'height=380,top=0,left=0,width=490,scrollbars=no');
}

function ApriHelp() {
    var win = null;
    win = window.open('Help_pw.htm', 'Accesso', 'height=380,top=0,left=0,width=490,scrollbars=no');
}

function ApriContatti() {
    //var win=null;
    //window.open('Contatti.htm',null,'height=480,top=0,left=0,width=490,scrollbars=yes');
    top.location.href = 'Contatti.htm';
}

function ApriModulistica() {
    var win = null;

    window.open('Public/Modulistica1.htm', null, 'height=430,top=0,left=0,width=490,scrollbars=yes');
}

function ApriAuto() {
    var win = null;
    //alert('Non Disponibile!');
    //window.open('AutoCompilazione/Auto.aspx','','');
    top.location.href = 'AutoCompilazione/Auto.aspx';
}

// Nuova funzione fondo solidarietà
function ApriFondoSolidarieta() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('Fondo_solidarieta/LoginFondoSolidarieta.aspx', 'LFondoS', '');
}


function ApriGrad() {
    var win = null;

    //window.open('Public/SceltaGrad.htm',null,'height=430,top=0,left=0,width=490,scrollbars=yes');
    top.location.href = 'Public/SceltaGrad.htm'
}

function PosizioneUtente() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    //window.open('Public/VerificaPos.aspx',null,'height=430,top='+TopPosition+',left='+LeftPosition+',width=400');
    window.open('VerificaPos.aspx', 'Posizione', '');
}

function Grad_Por() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('Grad_Por.aspx', 'Graduatoria', 'height=250,top=' + TopPosition + ',left=' + LeftPosition + ',width=650');
}

function Grad_Cambi() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('Grad_Cambi.aspx', 'Graduatoria', 'height=250,top=' + TopPosition + ',left=' + LeftPosition + ',width=650');
}


function ApriAccessoCensimento() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('CENSIMENTO/menu.htm', 'a', 'height=660,top=' + TopPosition + ',left=' + LeftPosition + ',width=960');
}

function ApriAccessoManutenzioni() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('MANUTENZIONI/menuInterventi.htm', 'a', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790');
    //alert('Attenzione...le funzioni del modulo manutenzioni e servizi sono state integrate nei moduli ANAGRAFE DEL PATRIMONIO e CICLO PASSIVO.\nContattare l\'amministratore di sistema se non si dispone delle autorizzazione per accedere a tali moduli.');
}

function ApriAccessoFSA() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    //alert('Non disponibile!');
    window.open('FSA/menu.htm', 'a', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790');
}


function ApriAccessoBollette() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('Bollette/menu.htm', 'a', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790,scrollbars=no');
}

function ApriAccessoContratti() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 610) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('Contratti/menu.htm', 'a', 'height=610,top=' + TopPosition + ',left=' + LeftPosition + ',width=960,scrollbars=no');
}

function ApriAccessoUI() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('Contratti/SelezionaTipoContratto.aspx', 'Tipologia', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=800,scrollbars=no');
}

function ApriAccessoBando() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('menu.htm', 'a', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790,scrollbars=no');
}

function ApriAccessoBandoCambi() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('CAMBI/menu.htm', 'a', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790');
}

function ApriAccessoAnagrafe() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('ANAUT/menu.htm', 'a', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790');
}

function ApriAccessoConsultazione() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('CONS/menu.htm', 'a', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790');
}

function ApriAccessoPED() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('PED/menu.htm', 'a', 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790');
}

function ApriAccessoAbbina() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('ASS/menu.htm', null, 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790');
}

function ApriAccessoAbbinaDec() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('ASS_ESTERNA/menu.htm', null, 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790');
}

function ApriAccessoVSA() {
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 790) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
    LeftPosition = LeftPosition - 20;
    TopPosition = TopPosition - 20;
    window.open('VSA/menu.htm', null, 'height=598,top=' + TopPosition + ',left=' + LeftPosition + ',width=790');
}


function AzzeraCF(C1, C2) {
    //C1.value='';
    C2.value = '0';
    if (document.getElementById('Dic_Dichiarazione1_txtCF').value != '') {
        document.getElementById('Dic_Dichiarazione1_messaggio').value = '>ATTENZIONE:Dati Anagrafici modificati! Verificare e modificare le eventuali occorrenze nei vari pannelli.<';
        document.getElementById('txtbinserito').value = '0';
        document.getElementById('Dic_Dichiarazione1_txtCF').value = '';
    }
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

function AnnullaCaratteri(obj) {
    if (event.keyCode < 48 || event.keyCode > 57) {

        event.keyCode = 0;
        alert('Carattere non Consentito!');
    }
}

function massimiliano(str, textbox, delim) {

    //var sKeyPressed;
    //sKeyPressed = new String(event.keyCode);

    //if (event.keyCode < 48 || event.keyCode > 57)
    //	{
    // don't insert last non-numeric character
    //		event.keyCode = 0;
    //		alert('Carattere non Consentito!');
    //	}
    //else	
    //{	
    if (str.length == 2) {
        textbox.value = str + delim;
    }
    if (str.length == 5) {
        textbox.value = str + delim;
    }
    //}

}

//function Aggiorna(questo,questo1,questo2) {
//alert(questo.value);
//questo.visibility='visible'; 
//questo1.visibility='hidden';
//questo2.visibility='hidden';
//}

function AggTabDom(tab, tabv, tabinv1, tabinv2, tabinv3, tabinv4, tabinv5, tabinv6) {
    if (tab == '1') {
        tabv.visibility = 'visible';
        tabinv1.visibility = 'hidden';
        tabinv2.visibility = 'hidden';
        tabinv3.visibility = 'hidden';
        tabinv4.visibility = 'hidden';
        tabinv5.visibility = 'hidden';
        tabinv6.visibility = 'hidden';
        document.getElementById('i1').src = 'p_menu/RICH_1.gif'
        document.getElementById('i2').src = 'p_menu/DICH_0.gif'
        document.getElementById('i3').src = 'p_menu/FAM_0.gif'
        document.getElementById('i4').src = 'p_menu/ABIT1_0.gif'
        document.getElementById('i5').src = 'p_menu/ABIT2_0.gif'
        document.getElementById('i6').src = 'p_menu/REC_0.gif'
        document.getElementById('i7').src = 'p_menu/NOTE_0.gif'
    }
    if (tab == '2') {
        tabv.visibility = 'hidden';
        tabinv1.visibility = 'visible';
        tabinv2.visibility = 'hidden';
        tabinv3.visibility = 'hidden';
        tabinv4.visibility = 'hidden';
        tabinv5.visibility = 'hidden';
        tabinv6.visibility = 'hidden';
        document.getElementById('i1').src = 'p_menu/RICH_0.gif'
        document.getElementById('i2').src = 'p_menu/DICH_1.gif'
        document.getElementById('i3').src = 'p_menu/FAM_0.gif'
        document.getElementById('i4').src = 'p_menu/ABIT1_0.gif'
        document.getElementById('i5').src = 'p_menu/ABIT2_0.gif'
        document.getElementById('i6').src = 'p_menu/REC_0.gif'
        document.getElementById('i7').src = 'p_menu/NOTE_0.gif'
    }
    if (tab == '3') {
        tabv.visibility = 'hidden';
        tabinv1.visibility = 'hidden';
        tabinv2.visibility = 'visible';
        tabinv3.visibility = 'hidden';
        tabinv4.visibility = 'hidden';
        tabinv5.visibility = 'hidden';
        tabinv6.visibility = 'hidden';
        document.getElementById('i1').src = 'p_menu/RICH_0.gif'
        document.getElementById('i2').src = 'p_menu/DICH_0.gif'
        document.getElementById('i3').src = 'p_menu/FAM_1.gif'
        document.getElementById('i4').src = 'p_menu/ABIT1_0.gif'
        document.getElementById('i5').src = 'p_menu/ABIT2_0.gif'
        document.getElementById('i6').src = 'p_menu/REC_0.gif'
        document.getElementById('i7').src = 'p_menu/NOTE_0.gif'
    }
    if (tab == '4') {
        tabv.visibility = 'hidden';
        tabinv1.visibility = 'hidden';
        tabinv2.visibility = 'hidden';
        tabinv3.visibility = 'visible';
        tabinv4.visibility = 'hidden';
        tabinv5.visibility = 'hidden';
        tabinv6.visibility = 'hidden';
        document.getElementById('i1').src = 'p_menu/RICH_0.gif'
        document.getElementById('i2').src = 'p_menu/DICH_0.gif'
        document.getElementById('i3').src = 'p_menu/FAM_0.gif'
        document.getElementById('i4').src = 'p_menu/ABIT1_1.gif'
        document.getElementById('i5').src = 'p_menu/ABIT2_0.gif'
        document.getElementById('i6').src = 'p_menu/REC_0.gif'
        document.getElementById('i7').src = 'p_menu/NOTE_0.gif'
    }
    if (tab == '5') {
        tabv.visibility = 'hidden';
        tabinv1.visibility = 'hidden';
        tabinv2.visibility = 'hidden';
        tabinv3.visibility = 'hidden';
        tabinv4.visibility = 'visible';
        tabinv5.visibility = 'hidden';
        tabinv6.visibility = 'hidden';
        document.getElementById('i1').src = 'p_menu/RICH_0.gif'
        document.getElementById('i2').src = 'p_menu/DICH_0.gif'
        document.getElementById('i3').src = 'p_menu/FAM_0.gif'
        document.getElementById('i4').src = 'p_menu/ABIT1_0.gif'
        document.getElementById('i5').src = 'p_menu/ABIT2_1.gif'
        document.getElementById('i6').src = 'p_menu/REC_0.gif'
        document.getElementById('i7').src = 'p_menu/NOTE_0.gif'
    }
    if (tab == '6') {
        tabv.visibility = 'hidden';
        tabinv1.visibility = 'hidden';
        tabinv2.visibility = 'hidden';
        tabinv3.visibility = 'hidden';
        tabinv4.visibility = 'hidden';
        tabinv5.visibility = 'visible';
        tabinv6.visibility = 'hidden';
        document.getElementById('i1').src = 'p_menu/RICH_0.gif'
        document.getElementById('i2').src = 'p_menu/DICH_0.gif'
        document.getElementById('i3').src = 'p_menu/FAM_0.gif'
        document.getElementById('i4').src = 'p_menu/ABIT1_0.gif'
        document.getElementById('i5').src = 'p_menu/ABIT2_0.gif'
        document.getElementById('i6').src = 'p_menu/REC_1.gif'
        document.getElementById('i7').src = 'p_menu/NOTE_0.gif'
    }
    if (tab == '7') {
        tabv.visibility = 'hidden';
        tabinv1.visibility = 'hidden';
        tabinv2.visibility = 'hidden';
        tabinv3.visibility = 'hidden';
        tabinv4.visibility = 'hidden';
        tabinv5.visibility = 'hidden';
        tabinv6.visibility = 'visible';
        document.getElementById('i1').src = 'p_menu/RICH_0.gif'
        document.getElementById('i2').src = 'p_menu/DICH_0.gif'
        document.getElementById('i3').src = 'p_menu/FAM_0.gif'
        document.getElementById('i4').src = 'p_menu/ABIT1_0.gif'
        document.getElementById('i5').src = 'p_menu/ABIT2_0.gif'
        document.getElementById('i6').src = 'p_menu/REC_0.gif'
        document.getElementById('i7').src = 'p_menu/NOTE_1.gif'

    }
    document.getElementById('txtTab').value = tab;
}


function AggTabDic(tab, tabv, tabinv1, tabinv2, tabinv3, tabinv4, tabinv5, tabinv6) {


    if (tab == '1') {

        tabv.visibility = 'visible';
        tabinv1.visibility = 'hidden';
        tabinv2.visibility = 'hidden';
        tabinv3.visibility = 'hidden';
        tabinv4.visibility = 'hidden';
        tabinv5.visibility = 'hidden';
        tabinv6.visibility = 'hidden';

        document.getElementById('i1').src = 'p_menu/D1_1.gif';
        document.getElementById('i2').src = 'p_menu/D2_0.gif';
        document.getElementById('i3').src = 'p_menu/D3_0.gif';
        document.getElementById('i4').src = 'p_menu/D4_0.gif';
        document.getElementById('i5').src = 'p_menu/D5_0.gif';
        document.getElementById('i6').src = 'p_menu/D6_0.gif';
        document.getElementById('i7').src = 'p_menu/D7_0.gif';

        document.getElementById('txtTab').value = tab;
    }
    if (tab == '2') {

        if (Valorizza() == 0) {

            if (document.getElementById('txtbinserito').value == '0') {
                mias = MiaFormat(document.getElementById('Dic_Dichiarazione1_txtCognome').value, 25) + ' ' + MiaFormat(document.getElementById('Dic_Dichiarazione1_txtNome').value, 25) + ' ' + MiaFormat(document.getElementById('Dic_Dichiarazione1_txtDataNascita').value, 10) + ' ' + MiaFormat(document.getElementById('Dic_Dichiarazione1_txtCF').value, 16) + ' ' + MiaFormat('CAPOFAMIGLIA', 25) + ' ' + MiaFormat('0', 6) + ' ' + MiaFormat('-----', 5) + ' ' + MiaFormat('NO', 2);
                var miaOption = new Option(mias, '0');
                document.getElementById('Dic_Nucleo1_ListBox1').options[0] = miaOption;
                document.getElementById('txtbinserito').value = '1';
            }

            tabv.visibility = 'hidden';
            tabinv1.visibility = 'visible';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_0.gif';
            document.getElementById('i2').src = 'p_menu/D2_1.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

            document.getElementById('txtTab').value = tab;
        }
        else {
            tabv.visibility = 'visible';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_1.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

        }
    }
    if (tab == '3') {
        if (Valorizza() == 0) {
            tabv.visibility = 'hidden';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'visible';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_0.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_1.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

            document.getElementById('txtTab').value = tab;
        }
        else {
            tabv.visibility = 'visible';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_1.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

        }
    }
    if (tab == '4') {
        if (Valorizza() == 0) {
            tabv.visibility = 'hidden';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'visible';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_0.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_1.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

            document.getElementById('txtTab').value = tab;
        }
        else {
            tabv.visibility = 'visible';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_1.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

        }
    }
    if (tab == '5') {
        if (Valorizza() == 0) {
            tabv.visibility = 'hidden';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'visible';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_0.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_1.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

            document.getElementById('txtTab').value = tab;
        }
        else {
            tabv.visibility = 'visible';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_1.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

        }
    }
    if (tab == '6') {
        if (Valorizza() == 0) {
            tabv.visibility = 'hidden';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'visible';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_0.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_1.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

            document.getElementById('txtTab').value = tab;
        }
        else {
            tabv.visibility = 'visible';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_1.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

        }
    }
    if (tab == '7') {
        if (Valorizza() == 0) {
            tabv.visibility = 'hidden';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'visible';

            document.getElementById('i1').src = 'p_menu/D1_0.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_1.gif';

            document.getElementById('txtTab').value = tab;
        }
        else {
            tabv.visibility = 'visible';
            tabinv1.visibility = 'hidden';
            tabinv2.visibility = 'hidden';
            tabinv3.visibility = 'hidden';
            tabinv4.visibility = 'hidden';
            tabinv5.visibility = 'hidden';
            tabinv6.visibility = 'hidden';

            document.getElementById('i1').src = 'p_menu/D1_1.gif';
            document.getElementById('i2').src = 'p_menu/D2_0.gif';
            document.getElementById('i3').src = 'p_menu/D3_0.gif';
            document.getElementById('i4').src = 'p_menu/D4_0.gif';
            document.getElementById('i5').src = 'p_menu/D5_0.gif';
            document.getElementById('i6').src = 'p_menu/D6_0.gif';
            document.getElementById('i7').src = 'p_menu/D7_0.gif';

        }
    }

}

function Valorizza() {
    valore = 0;
    if (document.getElementById('Dic_Dichiarazione1_txtCognome').value == '') {
        valore = 1;
    }
    if (document.getElementById('Dic_Dichiarazione1_txtNome').value == '') {
        valore = 1;
    }
    if (document.getElementById('Dic_Dichiarazione1_txtCF').value == '') {
        valore = 1;
    }
    if (document.getElementById('Dic_Dichiarazione1_txtIndRes').value == '') {
        valore = 1;
    }
    if (document.getElementById('Dic_Dichiarazione1_txtCivicoRes').value == '') {
        valore = 1;
    }
    if (valore == 1) {
        alert('Valorizzare tutti i campi!');
        return 1;
    }
    else {
        return 0;
    }
}

function MiaFormat(testo, lunghezza) {
    //par.MiaFormat(CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text, 25) & " " & par.MiaFormat(CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text, 25) & " " & par.MiaFormat(CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text, 10) & " " & par.MiaFormat(CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text, 16)

    ss = testo;
    if (ss.length != lunghezza) {
        for (i = ss.length + 1; i < lunghezza + 1; i++) {
            ss += String.fromCharCode(160);
        }
    }
    return ss;
}

function InserisciRiga() {

    if (document.getElementById('Dic_Nucleo1_txtprova').value != '') {
        mias = document.getElementById('Dic_Nucleo1_txtprova').value;
        var miaOption = new Option(mias, document.getElementById('Dic_Nucleo1_txtProgr').value);
        obj = document.getElementById('Dic_Nucleo1_ListBox1');
        obj.options[obj.length] = miaOption;
        document.getElementById('Dic_Nucleo1_txtprova').value = '';
        obj1 = document.getElementById('cmbComp');
        obj1.options[obj1.length] = miaOption;
    }
    if (document.getElementById('Dic_Nucleo1_txtprova1').value != '') {
        mias = document.getElementById('Dic_Nucleo1_txtprova1').value;
        var miaOption = new Option(mias, document.getElementById('Dic_Nucleo1_txtProgr').value);
        obj = document.getElementById('Dic_Nucleo1_ListBox2');
        obj.options[obj.length] = miaOption;
        document.getElementById('Dic_Nucleo1_txtprova1').value = '';
    }
}

function ModificaRiga(riga) {

    if (document.getElementById('Dic_Nucleo1_txtprova').value != '') {
        mias = document.getElementById('Dic_Nucleo1_txtprova').value;
        obj = document.getElementById('Dic_Nucleo1_ListBox1');
        obj.options[riga].text = mias;
        valore = obj.options[riga].value;
        document.getElementById('Dic_Nucleo1_txtprova').value = '';
        obj1 = document.getElementById('Dic_Nucleo1_ListBox2');
        if (obj1.length != 0) {
            for (i = 0; i <= obj1.length - 1; i++) {
                if (obj1.options[i].value == valore) {
                    obj1.options[i] = null;
                }
            }
        }
    }
    if (document.getElementById('Dic_Nucleo1_txtprova1').value != '') {
        mias = document.getElementById('Dic_Nucleo1_txtprova1').value;
        var miaOption = new Option(mias, document.getElementById('Dic_Nucleo1_txtProgr').value);
        obj = document.getElementById('Dic_Nucleo1_ListBox2');
        obj.options[obj.length] = miaOption;
        document.getElementById('Dic_Nucleo1_txtprova1').value = '';
    }
}
/* JSON */
if (typeof JSON !== 'object') {
    JSON = {};
}
(function () {
    'use strict';
    var rx_one = /^[\],:{}\s]*$/,
        rx_two = /\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g,
        rx_three = /"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g,
        rx_four = /(?:^|:|,)(?:\s*\[)+/g,
        rx_escapable = /[\\\"\u0000-\u001f\u007f-\u009f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
        rx_dangerous = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g;
    function f(n) {
        return n < 10 
            ? '0' + n 
            : n;
    }
    function this_value() {
        return this.valueOf();
    }
    if (typeof Date.prototype.toJSON !== 'function') {
        Date.prototype.toJSON = function () {
            return isFinite(this.valueOf())
                ? this.getUTCFullYear() + '-' +
                        f(this.getUTCMonth() + 1) + '-' +
                        f(this.getUTCDate()) + 'T' +
                        f(this.getUTCHours()) + ':' +
                        f(this.getUTCMinutes()) + ':' +
                        f(this.getUTCSeconds()) + 'Z'
                : null;
        };
        Boolean.prototype.toJSON = this_value;
        Number.prototype.toJSON = this_value;
        String.prototype.toJSON = this_value;
    }
    var gap,
        indent,
        meta,
        rep;
    function quote(string) {
        rx_escapable.lastIndex = 0;
        return rx_escapable.test(string) 
            ? '"' + string.replace(rx_escapable, function (a) {
                var c = meta[a];
                return typeof c === 'string'
                    ? c
                    : '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
            }) + '"' 
            : '"' + string + '"';
    }
    function str(key, holder) {
        var i,          // The loop counter.
            k,          // The member key.
            v,          // The member value.
            length,
            mind = gap,
            partial,
            value = holder[key];
        if (value && typeof value === 'object' &&
                typeof value.toJSON === 'function') {
            value = value.toJSON(key);
        }
        if (typeof rep === 'function') {
            value = rep.call(holder, key, value);
        }
        switch (typeof value) {
        case 'string':
            return quote(value);
        case 'number':
            return isFinite(value) 
                ? String(value) 
                : 'null';
        case 'boolean':
        case 'null':
            return String(value);
        case 'object':
            if (!value) {
                return 'null';
            }
            gap += indent;
            partial = [];
            if (Object.prototype.toString.apply(value) === '[object Array]') {
                length = value.length;
                for (i = 0; i < length; i += 1) {
                    partial[i] = str(i, value) || 'null';
                }
                v = partial.length === 0
                    ? '[]'
                    : gap
                        ? '[\n' + gap + partial.join(',\n' + gap) + '\n' + mind + ']'
                        : '[' + partial.join(',') + ']';
                gap = mind;
                return v;
            }
            if (rep && typeof rep === 'object') {
                length = rep.length;
                for (i = 0; i < length; i += 1) {
                    if (typeof rep[i] === 'string') {
                        k = rep[i];
                        v = str(k, value);
                        if (v) {
                            partial.push(quote(k) + (
                                gap 
                                    ? ': ' 
                                    : ':'
                            ) + v);
                        }
                    }
                }
            } else {
                for (k in value) {
                    if (Object.prototype.hasOwnProperty.call(value, k)) {
                        v = str(k, value);
                        if (v) {
                            partial.push(quote(k) + (
                                gap 
                                    ? ': ' 
                                    : ':'
                            ) + v);
                        }
                    }
                }
            }
            v = partial.length === 0
                ? '{}'
                : gap
                    ? '{\n' + gap + partial.join(',\n' + gap) + '\n' + mind + '}'
                    : '{' + partial.join(',') + '}';
            gap = mind;
            return v;
        }
    }
    if (typeof JSON.stringify !== 'function') {
        meta = {    // table of character substitutions
            '\b': '\\b',
            '\t': '\\t',
            '\n': '\\n',
            '\f': '\\f',
            '\r': '\\r',
            '"': '\\"',
            '\\': '\\\\'
        };
        JSON.stringify = function (value, replacer, space) {
            var i;
            gap = '';
            indent = '';
            if (typeof space === 'number') {
                for (i = 0; i < space; i += 1) {
                    indent += ' ';
                }
            } else if (typeof space === 'string') {
                indent = space;
            }
            rep = replacer;
            if (replacer && typeof replacer !== 'function' &&
                    (typeof replacer !== 'object' ||
                    typeof replacer.length !== 'number')) {
                throw new Error('JSON.stringify');
            }
            return str('', {'': value});
        };
    }
    if (typeof JSON.parse !== 'function') {
        JSON.parse = function (text, reviver) {
            var j;
            function walk(holder, key) {
                var k, v, value = holder[key];
                if (value && typeof value === 'object') {
                    for (k in value) {
                        if (Object.prototype.hasOwnProperty.call(value, k)) {
                            v = walk(value, k);
                            if (v !== undefined) {
                                value[k] = v;
                            } else {
                                delete value[k];
                            }
                        }
                    }
                }
                return reviver.call(holder, key, value);
            }
            text = String(text);
            rx_dangerous.lastIndex = 0;
            if (rx_dangerous.test(text)) {
                text = text.replace(rx_dangerous, function (a) {
                    return '\\u' +
                            ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
                });
            }
            if (
                rx_one.test(
                    text
                        .replace(rx_two, '@')
                        .replace(rx_three, ']')
                        .replace(rx_four, '')
                )
            ) {
                j = eval('(' + text + ')');
                return typeof reviver === 'function'
                    ? walk({'': j}, '')
                    : j;
            }
            throw new SyntaxError('JSON.parse');
        };
    }
}());
/* JSON */