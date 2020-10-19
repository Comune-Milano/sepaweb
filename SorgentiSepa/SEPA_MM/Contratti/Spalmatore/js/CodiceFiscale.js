var strCodFis = "";
var strcognome = "";
var strnome = "";
var strgiornosex = "";
var chrcontrollo = '';



// Controls/Definitions:
// --------------------------------------------------------------------------
DefaultValues();

// --------------------------------------------------------------------------

// Functions:

// --------------------------------------------------------------------------
// Setta i prodotti ed i valori di default
// --------------------------------------------------------------------------
function DefaultValues() {
    strCodFis = "";
    strcognome = "";
    strnome = "";
    strgiornosex = "";
    chrcontrollo = '';
    Provincia = "";
    Cognome = "";
    Nome = "";
    Sesso = 0;
    Comune = "";
    CodiceFiscale = "";
    AnnoCento = 19;
    AnnoDieci = "0";
    AnnoZero = "0";
    Mese = "A";
    Giorno = 1;
    var conta = 0;

    return;
}


// --------------------------------------------------------------------------
function CheckCognome(cognome) {
    if (cognome.length < 1) {
        alert("Attenzione:\nManca il Cognome!");
        return (0);
    }
    //cognome = cognome.toUpperCase();
    return (1);
}
// --------------------------------------------------------------------------
function CheckNome(nome) {
    if (nome.length < 1) {
        alert("Attenzione:\nManca il Nome!");
        return (0);
    }
    //nome = nome.toUpperCase();
    return (1);
}
function CheckData(AC, AD, AZ, M) {
    if (AC.length < 1 || isNaN(AC)) {
        alert("Attenzione:\nData errata o mancante!");
        return (0);
    }
    if (AD.length < 1 || isNaN(AD)) {
        alert("Attenzione:\nData errata o mancante!");
        return (0);
    }
    if (AZ.length < 1 || isNaN(AZ)) {
        alert("Attenzione:\nData errata o mancante!");
        return (0);
    }
    if (M.length < 1) {
        alert("Attenzione:\nData errata o mancante!");
        return (0);
    }
    return (1);

}

function codDaComune(com) {
    var codice = '';
    conta = 0;
    $.ajax({
        url: 'comuni.txt',
        async: false,
        success: function (file) {
            var riga = file.split(";");
            $.each(riga, function (elem) {

                if (Provincia.length > 1) {
                    if (riga[elem].split('|')[2] == Provincia) {
                        if (riga[elem].split('|')[1] == com) {
                            codice = $.trim(riga[elem].split('|')[0]);
                            conta = conta + 1
                            return codice;
                        }
                    }
                }
                else {
                    if (riga[elem].split('|')[1] == com) {
                        codice = $.trim(riga[elem].split('|')[0]);
                        conta = conta + 1
                        return codice;
                    }
                }
            }
			);
            if (conta > 1) { alert("Definire comune e provincia!"); codice = ''; return (0); }
        }
    });
    return codice;
}
// --------------------------------------------------------------------------
function CheckComune(Comune) {
    if (Comune.length < 1) {
        if (conta == 0) {
            alert("Attenzione:\nComune non definito o errato!");
        }
        return (0);
    }

    //document.getElementById('Comune').value = document.getElementById('Comune').value.toUpperCase();
    return (1);
}
function letteraMese(mes) {

    if (mes == 1) { mes = 'A' }
    if (mes == 2) { mes = 'B' }
    if (mes == 3) { mes = 'C' }
    if (mes == 4) { mes = 'D' }
    if (mes == 5) { mes = 'E' }
    if (mes == 6) { mes = 'H' }
    if (mes == 7) { mes = 'L' }
    if (mes == 8) { mes = 'M' }
    if (mes == 9) { mes = 'P' }
    if (mes == 10) { mes = 'R' }
    if (mes == 11) { mes = 'S' }
    if (mes == 12) { mes = 'T' }
    return mes;
};
// --------------------------------------------------------------------------
function CalcolaCodiceFiscale(cognome, g, annoc, annod, annozero, mes, com, sex, nome, prov) {

    if (typeof (Page_ClientValidate) == 'function') {
        Page_ClientValidate();
        if (!Page_IsValid) {
            alert('ATTENZIONE! Ci sono delle incongruenze dati della pagina!');
            return false;
        }
        
    }
    var gs = 0;
    var i = 0;
    var somma = 0;
    strCodFis = "";
    strcognome = "";
    strnome = "";
    strgiornosex = "";
    chrcontrollo = '';

    Provincia = prov;
    Giorno = parseInt(g); //parseInt(document.getElementById('Giorno').options[document.getElementById('Giorno').selectedIndex].value, 10);
    AnnoCento = parseInt(annoc); //parseInt(document.getElementById('AnnoCento').options[document.getElementById('AnnoCento').selectedIndex].value, 10);
    AnnoDieci = parseInt(annod); //document.getElementById('AnnoDieci').options[document.getElementById('AnnoDieci').selectedIndex].value;
    AnnoZero = parseInt(annozero); //document.getElementById('AnnoZero').options[document.getElementById('AnnoZero').selectedIndex].value;
    Mese = letteraMese(mes); //document.getElementById('Mese').options[document.getElementById('Mese').selectedIndex].value;
    Comune = codDaComune(com); //document.getElementById('Comune').value;
    Sesso = parseInt(sex); //parseInt(document.getElementById('Sesso').options[document.getElementById('Sesso').selectedIndex].value, 10);
    if (CheckCognome(cognome) && CheckNome(nome)) {
        if (CheckComune(Comune)) {
            if (CheckData(AnnoCento, AnnoDieci, AnnoZero, Mese)) {

                // Processa il cognome
                //----------------------------------------------------------------
                for (i = 0; i < cognome.length; i++) {
                    switch (cognome.charAt(i)) {
                        case 'A':
                        case 'E':
                        case 'I':
                        case 'O':
                        case 'U': break;
                        default:
                            if ((cognome.charAt(i) <= 'Z') && (cognome.charAt(i) > 'A'))
                                strcognome = strcognome + cognome.charAt(i);
                    }
                }
                if (strcognome.length < 3) {
                    for (i = 0; i < cognome.length; i++) {
                        switch (cognome.charAt(i)) {
                            case 'A':
                            case 'E':
                            case 'I':
                            case 'O':
                            case 'U': strcognome = strcognome + cognome.charAt(i);
                        }
                    }
                    if (strcognome.length < 3) {
                        for (i = strcognome.length; i <= 3; i++)
                        { strcognome = strcognome + 'X'; }
                    }
                }
                strcognome = strcognome.substring(0, 3);
                //------------------------------------------------------------ 



                // processa il nome
                //----------------------------------------------------------------
                for (i = 0; i < nome.length; i++) {
                    switch (nome.charAt(i)) {
                        case 'A':
                        case 'E':
                        case 'I':
                        case 'O':
                        case 'U': break;
                        default:
                            if ((nome.charAt(i) <= 'Z') && (nome.charAt(i) > 'A'))
                                strnome = strnome + nome.charAt(i);
                    }
                }
                if (strnome.length > 3) {
                    strnome = strnome.substring(0, 1) + strnome.substring(2, 4);
                }
                else {
                    if (strnome.length < 3) {
                        for (i = 0; i < nome.length; i++) {
                            switch (nome.charAt(i)) {
                                case 'A':
                                case 'E':
                                case 'I':
                                case 'O':
                                case 'U': strnome = strnome + nome.charAt(i);
                            }
                        }
                        if (strnome.length < 3) {
                            for (i = strnome.length; i <= 3; i++)
                            { strnome = strnome + 'X'; }
                        }
                    }
                    strnome = strnome.substring(0, 3);
                }
                //--------------------------------------- Fine processa nome




                // processa giorno e sesso
                //--------------------------------------------
                gs = Giorno + (40 * Sesso);
                if (gs < 10) strgiornosex = "0" + gs;
                else strgiornosex = gs;
                //--------------------------------------------

                strCodFis = strcognome + strnome + AnnoDieci + AnnoZero + Mese + strgiornosex + Comune;

                // calcola la cifra di controllo
                //--------------------------------------------
                for (i = 0; i < 15; i++) {
                    if (((i + 1) % 2) != 0) //caratteri dispari
                    {
                        switch (strCodFis.charAt(i)) {
                            case '0':
                            case 'A': { somma += 1; break; }
                            case '1':
                            case 'B': { somma += 0; break; }
                            case '2':
                            case 'C': { somma += 5; break; }
                            case '3':
                            case 'D': { somma += 7; break; }
                            case '4':
                            case 'E': { somma += 9; break; }
                            case '5':
                            case 'F': { somma += 13; break; }
                            case '6':
                            case 'G': { somma += 15; break; }
                            case '7':
                            case 'H': { somma += 17; break; }
                            case '8':
                            case 'I': { somma += 19; break; }
                            case '9':
                            case 'J': { somma += 21; break; }
                            case 'K': { somma += 2; break; }
                            case 'L': { somma += 4; break; }
                            case 'M': { somma += 18; break; }
                            case 'N': { somma += 20; break; }
                            case 'O': { somma += 11; break; }
                            case 'P': { somma += 3; break; }
                            case 'Q': { somma += 6; break; }
                            case 'R': { somma += 8; break; }
                            case 'S': { somma += 12; break; }
                            case 'T': { somma += 14; break; }
                            case 'U': { somma += 16; break; }
                            case 'V': { somma += 10; break; }
                            case 'W': { somma += 22; break; }
                            case 'X': { somma += 25; break; }
                            case 'Y': { somma += 24; break; }
                            case 'Z': { somma += 23; break; }
                        }
                    }
                    else //caratteri pari
                    {
                        switch (strCodFis.charAt(i)) {
                            case '0':
                            case 'A': { somma += 0; break; }
                            case '1':
                            case 'B': { somma += 1; break; }
                            case '2':
                            case 'C': { somma += 2; break; }
                            case '3':
                            case 'D': { somma += 3; break; }
                            case '4':
                            case 'E': { somma += 4; break; }
                            case '5':
                            case 'F': { somma += 5; break; }
                            case '6':
                            case 'G': { somma += 6; break; }
                            case '7':
                            case 'H': { somma += 7; break; }
                            case '8':
                            case 'I': { somma += 8; break; }
                            case '9':
                            case 'J': { somma += 9; break; }
                            case 'K': { somma += 10; break; }
                            case 'L': { somma += 11; break; }
                            case 'M': { somma += 12; break; }
                            case 'N': { somma += 13; break; }
                            case 'O': { somma += 14; break; }
                            case 'P': { somma += 15; break; }
                            case 'Q': { somma += 16; break; }
                            case 'R': { somma += 17; break; }
                            case 'S': { somma += 18; break; }
                            case 'T': { somma += 19; break; }
                            case 'U': { somma += 20; break; }
                            case 'V': { somma += 21; break; }
                            case 'W': { somma += 22; break; }
                            case 'X': { somma += 23; break; }
                            case 'Y': { somma += 24; break; }
                            case 'Z': { somma += 25; break; }
                        }
                    }
                }
                somma %= 26;
                switch (somma) {
                    case 0: { chrcontrollo = 'A'; break; }
                    case 1: { chrcontrollo = 'B'; break; }
                    case 2: { chrcontrollo = 'C'; break; }
                    case 3: { chrcontrollo = 'D'; break; }
                    case 4: { chrcontrollo = 'E'; break; }
                    case 5: { chrcontrollo = 'F'; break; }
                    case 6: { chrcontrollo = 'G'; break; }
                    case 7: { chrcontrollo = 'H'; break; }
                    case 8: { chrcontrollo = 'I'; break; }
                    case 9: { chrcontrollo = 'J'; break; }
                    case 10: { chrcontrollo = 'K'; break; }
                    case 11: { chrcontrollo = 'L'; break; }
                    case 12: { chrcontrollo = 'M'; break; }
                    case 13: { chrcontrollo = 'N'; break; }
                    case 14: { chrcontrollo = 'O'; break; }
                    case 15: { chrcontrollo = 'P'; break; }
                    case 16: { chrcontrollo = 'Q'; break; }
                    case 17: { chrcontrollo = 'R'; break; }
                    case 18: { chrcontrollo = 'S'; break; }
                    case 19: { chrcontrollo = 'T'; break; }
                    case 20: { chrcontrollo = 'U'; break; }
                    case 21: { chrcontrollo = 'V'; break; }
                    case 22: { chrcontrollo = 'W'; break; }
                    case 23: { chrcontrollo = 'X'; break; }
                    case 24: { chrcontrollo = 'Y'; break; }
                    case 25: { chrcontrollo = 'Z'; break; }
                }
                //--------------------------------------------

                document.getElementById('CodiceFiscale').value = strCodFis + chrcontrollo;
                document.getElementById('HiddenOk').value = "1";
            }

        }

    }
    else {
        document.getElementById('CodiceFiscale').value = "";
        document.getElementById('HiddenOk').value = "0";
    }
    return;
}

// --------------------------------------------------------------------------
//  END OF SCRIPT
// --------------------------------------------------------------------------

