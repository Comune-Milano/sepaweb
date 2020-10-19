var nascondi = 1;
function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow) {
        oWindow = window.radWindow;
    } else {
        if (window.frameElement) {
            if (window.frameElement.radWindow) {
                oWindow = window.frameElement.radWindow;
            };
        };
    };
    return oWindow;
};
function caricamento(tipo) {
    if (nascondi != 0 || nascondi === "undefined") {
        if (typeof tipo === "undefined") {
            tipo = 1;
        } else {
            tipo = 0;
        };
    };
    if (GetRadWindow()) {
        GetRadWindow().BrowserWindow.loading(tipo);
        nascondi = 1;
    };
};
function CenterPage2(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
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
};

function Blocca_SbloccaMenu(opzione) {
    if (GetRadWindow()) {
        if (opzione == 1)
            GetRadWindow().BrowserWindow.document.getElementById('optMenu').value = 1;
        else if (opzione == 0)
            GetRadWindow().BrowserWindow.document.getElementById('optMenu').value = 0;
    };
};
var Messaggio = {
    Elemento_Elimina: "Eliminare l\'elemento selezionato?",
    Elemento_Modifica: "Modificare l\'elemento selezionato?",
    Elemento_No_Selezione: "Nessun elemento selezionato!",
    Elemento_Presente: "Elemento già presente!",
    Operazione_Eff: "Operazione effettuata!",
    Uscita_Messaggio: "Sono state apportate delle modifiche. Uscire senza salvare?",
    Campi_Obbligatori: "Campi obbligatori!",
    Funzione_Non_Disponibile: "Funzione non disponibile al momento!",
    Dato_Correlato: "Impossibile eliminare il dato poichè risulta vincolato ad altri dati!",
    Dato_Esistente: "Dato già esistente!",
    BlockDB: "Il dato è utilizzato da altri operatori, non saranno possibili altre modifiche",
    NoExport: "Nessun risultato da esportare!",
    NoGo: "Chiudere la pagina nella quale si sta operando tramite il pulsante esci!"
};

function CicloPassivo(valore) {
    if (document.getElementById('optMenu').value == 0) {
        switch (valore) {
            case 'Dashboard':
                Apri('pagina_home_ncp_dashboard.aspx');
                break;
            case 'RicFornitori':
                Apri('CicloPassivo/APPALTI/RicercaFornitore.aspx');
                break;
            //Contratti                                           
            case 'InserimentoAppalti':
                Apri('CicloPassivo/APPALTI/SceltaLotto.aspx');
                break;
            case 'RicAppalti':
                Apri('CicloPassivo/APPALTI/RicercaAppalti.aspx');
                break;
            case 'RitLegge':
                Apri('CicloPassivo/APPALTI/RitenuteLegge.aspx');
                break;
            case "GestionePU":
                Apri('CicloPassivo/APPALTI/GestionePU.aspx');
                break;
            //Manutenzioni                                           
            case 'InserimentoManutenzioneEdfifici':
                Apri('CicloPassivo/MANUTENZIONI/RicercaManutenzioniINS.aspx?TIPOR=0');
                break;
            case 'InserimentoManutenzioneImpianti':
                Apri('CicloPassivo/MANUTENZIONI/RicercaManutenzioniINS.aspx?TIPOR=1');
                break;
            case 'InserimentoManutenzioneEdfificiFuoriLotto':
                Apri('CicloPassivo/MANUTENZIONI/RicercaManutenzioniINS.aspx?TIPOR=2');
                break;
            case 'InserimentoManutenzioneImpiantiFuoriLotto':
                Apri('CicloPassivo/MANUTENZIONI/RicercaManutenzioniINS.aspx?TIPOR=3');
                break;
            case 'RicSelettiva':
                Apri('CicloPassivo/MANUTENZIONI/RicercaManutenzioni.aspx');
                break;
            case 'RicDiretta':
                Apri('CicloPassivo/MANUTENZIONI/RicercaManutenzioniD.aspx');
                break;
            case 'ConsSelettiva':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) {
                    alert('Utente non abilitato oppure non ha la struttura assegnata!');
                } else {
                    Apri('CicloPassivo/MANUTENZIONI/RicercaConsuntivi.aspx');
                };
                break;
            case 'ConsDiretta':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) {
                    alert('Utente non abilitato oppure non ha la struttura assegnata!');
                } else {
                    Apri('CicloPassivo/MANUTENZIONI/RicercaConsuntiviD.aspx');
                };
                break;
            case 'NuovoSal':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) {
                    alert('Utente non abilitato oppure non ha la struttura assegnata!');
                } else {
                    Apri('CicloPassivo/MANUTENZIONI/RicercaSAL.aspx');
                }
                break;
            case 'RicSal':
                Apri('CicloPassivo/MANUTENZIONI/RicercaSAL_FIRMA.aspx');
                break;
            case 'StampaPagamenti':
                Apri('CicloPassivo/MANUTENZIONI/RicercaPagamenti.aspx');
                break;
            case 'ChiusuraSegnalazioni':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) {
                    alert('Utente non abilitato oppure non ha la struttura assegnata!');
                }
                else { Apri('CicloPassivo/MANUTENZIONI/ChiusuraSegnalazioni.aspx'); }
                break;
            //STR    
            case 'ExportSTR':
                if (document.getElementById('HFSTR').value == null) {
                    alert('Utente non abilitato oppure non ha la struttura assegnata!');
                }
                else { Apri('CicloPassivo/MANUTENZIONI/RicercaManutenzioniSTR.aspx'); }
                break;
            case 'Patrimonio':
                if (document.getElementById('HFSTR').value == null) {
                    alert('Utente non abilitato oppure non ha la struttura assegnata!');
                }
                else { Apri('CicloPassivo/MANUTENZIONI/EstrazionePatrimonio.aspx'); }
                break;
            case 'Import':
                if (document.getElementById('HFSTR').value == null) {
                    alert('Utente non abilitato oppure non ha la struttura assegnata!');
                }
                else { Apri('CicloPassivo/MANUTENZIONI/ImportSTR.aspx'); }
                break;
            //Ordini e pagamenti                                       
            case 'Inserimento_Pagamenti':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) {
                    alert('Utente non abilitato oppure non ha la struttura assegnata!');
                }
                else { Apri('CicloPassivo/PAGAMENTI/SceltaVoce.aspx'); }
                break;
            case 'RicPagSelettiva':
                Apri('CicloPassivo/PAGAMENTI/RicercaPagamenti.aspx');
                break;
            case 'RicPagDiretta':

                Apri('CicloPassivo/PAGAMENTI/RicercaPagamentiD.aspx');

                break;
            //Pagam. a canone                                      
            case 'Pagam_Da_Approvare':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) {
                    alert('Utente non abilitato oppure non ha la struttura assegnata!');
                }
                else {
                    Apri('CicloPassivo/PAGAMENTI_CANONE/RicercaPagamenti.aspx?TIPO=DA_APPROVARE');
                }
                break;
            case 'PagamApprovati':
                Apri('CicloPassivo/PAGAMENTI_CANONE/RicercaPagamentiS.aspx?TIPO=APPROVATI');
                break;
            case 'PagamEmessoSal':
                Apri('CicloPassivo/PAGAMENTI_CANONE/RicercaPagamentiS.aspx?TIPO=DA_STAMPARE_PAG');
                break;
            //Gestione Escomi                                       
            case 'InserimentoRRS':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Utente non abilitato oppure non ha la struttura assegnata!'); } else { Apri('CicloPassivo/RRS/RicercaRRS_INS.aspx?'); }
                break;
            case 'RicRRS_Sel':
                Apri('CicloPassivo/RRS/RicercaRRS.aspx');
                break;
            case 'RicRRS_Dir':

                Apri('CicloPassivo/RRS/RicercaRRS_D.aspx');
                break;
            //Consuntivazione                                      
            case 'ConsSelettivaRRS':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Utente non abilitato oppure non ha la struttura assegnata!'); } else { Apri('CicloPassivo/RRS/RicercaConsuntiviRRS_S.aspx'); }
                break;
            case 'ConsDirettaRRS':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Utente non abilitato oppure non ha la struttura assegnata!'); } else { Apri('CicloPassivo/RRS/RicercaConsuntiviRRS_D.aspx'); }
                break;
            //SAL                                      
            case 'NuovoSAL_RRS':
                if ((document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Utente non abilitato oppure non ha la struttura assegnata!'); } else { Apri('CicloPassivo/RRS/RicercaSAL_RRS.aspx'); }
                break;
            case 'RicSAL_RRS':
                Apri('CicloPassivo/RRS/RicercaSAL_RRS_FIRMA.aspx');
                break;
            case 'StampaPag_RRS':
                Apri('CicloPassivo/RRS/RicercaPagamentiRRS.aspx');
                break;
            //Utenze                                      
            case 'UtenzeCaricamento':
                Apri('CicloPassivo/PAGAMENTI/FattureCaricamentoNew.aspx?TIPO=U');
                break;
            case 'UtenzeRicerca':
                Apri('CicloPassivo/PAGAMENTI/FattureUtenze.aspx');
                break;
            case 'RicCDP_Emessi':
                Apri('CicloPassivo/PAGAMENTI/RicercaPagamentiUtenza.aspx?TIPO=U');
                break;
            case 'Fatt_CDP':
                Apri('CicloPassivo/PAGAMENTI/FattureCDP.aspx');
                break;
            case 'ElencoPOD':
                Apri('CicloPassivo/PAGAMENTI/GestionePod.aspx');
                break;
            //Custodi                                      
            case 'CustodiCaricamento':
                Apri('CicloPassivo/PAGAMENTI/FattureCaricamentoNew.aspx?TIPO=C');
                break;
            case 'CustodiRicerca':
                Apri('CicloPassivo/PAGAMENTI/CustodiPagamenti.aspx?PAGATE=0');
                break;
            case 'CustRicCDP_Emessa':
                Apri('CicloPassivo/PAGAMENTI/RicercaPagamentiUtenza.aspx?TIPO=C');
                break;
            case 'Cust_CDP':
                Apri('CicloPassivo/PAGAMENTI/CustodiPagamenti.aspx?PAGATE=1');
                break;
            case 'AnaCust':
                Apri('GestCustodi.aspx');
                break;
            //Multe                                      
            case 'MulteCaricamento':
                Apri('CicloPassivo/PAGAMENTI/FattureCaricamentoNew.aspx?TIPO=M');
                break;
            case 'MulteRicerca':
                Apri('CicloPassivo/PAGAMENTI/MulteCaricate.aspx');
                break;
            case 'RicMulteCDP':
                Apri('CicloPassivo/PAGAMENTI/RicercaPagamentiUtenza.aspx?TIPO=M');
                break;
            case 'MulteCDP':

                Apri('CicloPassivo/PAGAMENTI/MulteCaricate.aspx?PAGATE=1');
                break;
            //Cosap
            case 'CosapCaricamento':
                Apri('CicloPassivo/PAGAMENTI/FattureCaricamentoNew.aspx?TIPO=COSAP');
                break;
            case 'CosapRicerca':
                Apri('CicloPassivo/PAGAMENTI/CosapCaricati.aspx');
                break;
            case 'RicCosapCDP':
                Apri('CicloPassivo/PAGAMENTI/RicercaPagamentiUtenza.aspx?TIPO=COSAP');
                break;
            case 'CosapCDP':
                Apri('CicloPassivo/PAGAMENTI/CosapCaricati.aspx?PAGATE=1');
                break;
            //Report                                      
            case 'ReportGenerale':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value === null) && (document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Non disponibile o Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/RicercaSitContabileGenerale.aspx'); }
                break;
            case 'ReportStruttura':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null) && (document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Non disponibile o Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/RicercaSitContabile.aspx'); }
                break;
            case 'ReportPagamenti':
                Apri('CicloPassivo/PAGAMENTI/PagamentiPerStruttura.aspx');
                break;
            case 'PagamentiODL':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null) && (document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Non disponibile o Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/ODLperStruttura.aspx'); }
                break;
            case 'RicercaPerServizi':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null)) { alert('Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/RicercaPerServizi.aspx'); }
                break;
            case 'ReportContab':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null)) { alert('Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/Contabilita.aspx'); }
                break;
            case 'ReportContabDett':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null)) { alert('Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/ContabilitaDett.aspx'); }
                break;
            case 'ReportEsercizio':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null) && (document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/SituazioneContabile.aspx'); }
                break;
            case 'ReportSintesi':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null) && (document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/SituazioneContabileSintesi.aspx'); }
                break;
            case 'ReportCompleto':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null) && (document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/ReportCompleto.aspx'); }
                break;
            case 'ReportEstrPag':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null) && (document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/EstrazioneCDP.aspx'); }
                break;
            case 'ReportContratti':
                if ((document.getElementById('HFBpGenerale').value == 0) || (document.getElementById('HFBpGenerale').value == null) && (document.getElementById('HFIdStruttura').value == -1) || (document.getElementById('HFIdStruttura').value == null)) { alert('Utente non abilitato!'); } else { Apri('CicloPassivo/PAGAMENTI/SituazioneContabilePerAppalti.aspx'); }
                break;
            case 'OdlSal':
                if (document.getElementById('HFBpMS').value == null) { alert('Non disponibile o Utente non abilitato!'); } else { Apri('RicercaODLSAL.aspx'); };
                break;
            //Residui                                     
            case 'ReportResidui':
                Apri('CicloPassivo/PAGAMENTI/RicercaResidui.aspx');
                break;
            case 'ReportUsciteCor':
                Apri('CicloPassivo/PAGAMENTI/RicercaCorrenti.aspx');
                break;
            //ReportPagamenti                                     
            case 'Report_Pagamenti':
                Apri('CicloPassivo/PAGAMENTI/EstrazionePagamenti.aspx');
                break;
            //Gestione Lotti                                     
            case 'Nuovo_Lotto_E':
                Apri('CicloPassivo/LOTTI/NuovoLotto.aspx?T=E');
                break;
            case 'Nuovo_Lotto_I':
                Apri('CicloPassivo/LOTTI/NuovoLotto.aspx?T=I');
                break;
            case 'Ricerca_Lotti':
                Apri('CicloPassivo/LOTTI/RicercaLotti.aspx');
                break;
            case 'GuidaLotti':
                CenterPage2('CicloPassivo/LOTTI/Report/GuidaLOTTI.pdf');
                break;
            //Parametri                                     
            case 'ParametriPag':
                Apri('GestioneModlitaPagamento.aspx');
                break;
            case 'CDP_Tracciati':
                Apri('GestioneFatUtenze.aspx');
                break;
            case 'Gest_Crediti':
                if (document.getElementById('HFParamCP').value == 0) { alert('Non disponibile o Utente non abilitato!'); } else { Apri('ParametriGestCredito.aspx'); }
                break;
            //Building Manager                                     
            case 'B_Manager':
                //if (document.getElementById('HFModBuildingManager').value == 1) { Apri('CicloPassivo/Plan/Prospetto.aspx'); } else { alert('Non disponibile o Utente non abilitato!'); }
                if (document.getElementById('HFModBuildingManager').value == 1) { Apri('RisultatoBuildingManager.aspx'); } else { alert('Non disponibile o Utente non abilitato!'); }
                break;
            //UploadFirma                                      
            case 'UploadFirma':
                Apri('CicloPassivo/Manutenzioni/UploadFirma.aspx');
                break;
            case 'CambioIVA':
                Apri('CicloPassivo/MANUTENZIONI/CambioIVAodl.aspx');
                break;
            case 'Home':
                self.close();
                break;
            case 'RicercaF':
                Apri('Default.aspx');
                break;
        };
    }
    else {
        alert(Messaggio.NoGo);
    };
};

//Chiude una RadWindow con all'interno una pagina aspx, richiamata da una pagina principale
function closeWin() {
    GetRadWindow().close();
};
//Chiude una RadWindow con all'interno una pagina aspx, richiamata da una pagina principale, e aggiorna la pagina chiamante
function closeWinAndAttivaContratto(pulsante) {
    GetRadWindow().close();
    GetRadWindow().BrowserWindow.document.getElementById(pulsante).click();


};

function setDimensioni() {
    var altezzaTab = 0;
    if (document.getElementById('HFAltezzaTab')) altezzaTab = document.getElementById('HFAltezzaTab').value;
    var griglie = document.getElementById('HFGriglia').value;
    //altezzasottrratta per le griglie fuori dai tab
    var altezzaSottratta
    if (document.getElementById('HFAltezzaSottratta')) {
        altezzaSottratta = document.getElementById('HFAltezzaSottratta').value;
    } else {
        altezzaSottratta = 220;
    };
    if (window.innerHeight == undefined) {
        var altezzaPagina = document.documentElement.clientHeight;
    } else {
        var altezzaPagina = window.innerHeight;
    };

    //se ci sono tab allora imposta la loro altezza

    if (griglie != '') {
        var griglia = griglie.split(",");
        if (document.getElementById('MyTab')) {
            //Griglie nei tab (Nei tab va definito sempre il div MyTab)
            for (i = 0; i < griglia.length; i++) {
                document.getElementById(griglia[i]).style.height = altezzaPagina - 420 + 'px';
            }
        } else {
            //Griglie fuori dai tab
            for (i = 0; i < griglia.length; i++) {
                document.getElementById(griglia[i]).style.height = altezzaPagina - altezzaSottratta + 'px';
            }
        };
    };

    if (document.getElementById('RadTabStrip')) {
        var tabs = document.getElementById('HFTAB').value;
        var tab = tabs.split(",");
        if (tab.length != 0) {
            for (i = 0; i < tab.length; i++) {
                document.getElementById(tab[i]).style.height = altezzaPagina - altezzaTab + 'px';
            };
        };
        var numTab = parseInt(document.getElementById('numTab'));
        for (i = 1; i <= numTab; i++) {
            document.getElementById('tab' + [i]).style.height = altezzaPagina - altezzaTab + 'px';
        };
        if (griglie != '') {
            var griglia = griglie.split(",");
            var altezzeGriglie = document.getElementById('HFAltezzaFGriglie').value;
            var altezzaSingolaGrid = altezzeGriglie.split(",");
            //Griglie nei tab (Nei tab va definito sempre il div MyTab)
            for (i = 0; i < griglia.length; i++) {
                document.getElementById(griglia[i]).style.height = altezzaPagina - altezzaSingolaGrid[i] + 'px';
            };
        };
    };
};

function aggiornaBar(sender, args) {
    var tempo = args._progressData.TimeElapsed.toString().replace('s', '').split(':');
    var hh = parseFloat(tempo[0]);
    var mm = parseFloat(tempo[1]);
    var ss = parseFloat(tempo[2]);
    var secondi = hh * 60 * 60 + mm * 60 + ss;
    if ((args.get_progressValue() >= 50)) {
        var secondiRimanenti = parseInt(100 * secondi / parseFloat(args.get_progressValue()) - secondi);
        setTimeout(function () { var win = $find('RadWindowProgress'); win.close(); }, (secondiRimanenti) * 1000);
    };
};
function Esporta() {
    var win = $find('RadWindowProgress');
    win.show();
    nascondi = 0;
};

function ChangeToUpperCase(sender, args) {
    var inputElement = sender.get_inputDomElement();
    inputElement.style.textTransform = "uppercase";
};

function tabSelezionato(sender, args) {
    var tab = sender._selectedIndex;
    document.getElementById('HiddenTabSelezionato').value = tab;
};

function refreshPage(btnToClik) {

    if (document.getElementById(btnToClik)) {
        var attr;
        attr = $('#' + btnToClik).attr('onclick');
        $('#' + btnToClik).attr('onclick', '');
        document.getElementById(btnToClik).click();
        $('#' + btnToClik).attr('onclick', attr);

    };
};
function CloseAndRefresh(pulsante) {
    GetRadWindow().close();
    GetRadWindow().BrowserWindow.refreshPage(pulsante);
};


function CloseRadWindow() {
    GetRadWindow().close();
};

function openModalInRadClose(RadWindow, Page, w, h, maximizeWin) {
    if ('undefined' === typeof maximizeWin) {
        maximizeWin = 0;
    };
    var larghezza = w;
    var altezza = h;
    var larghezzaPagina = 0;
    var altezzaPagina = 0;
    if (document.getElementById('divGenerale')) {
        larghezzaPagina = $telerik.$("#divGenerale").width();
        altezzaPagina = $telerik.$("#divGenerale").height();
    } else if (document.getElementById('generale')) {
        larghezzaPagina = $telerik.$("#generale").width();
        altezzaPagina = $telerik.$("#generale").height();
    } else if (document.getElementById('divBody')) {
        larghezzaPagina = $telerik.$("#divBody").width();
        altezzaPagina = $telerik.$("#divBody").height();
    } else if (document.getElementById('body')) {
        larghezzaPagina = $telerik.$("#body").width();
        altezzaPagina = $telerik.$("#body").height();
    } else {
        larghezzaPagina = screen.availWidth + 50;
        altezzaPagina = screen.availHeight;
    };
    if (larghezza == 0) {
        larghezza = larghezzaPagina;
    } else {
        if (larghezza >= (larghezzaPagina - 50)) {
            larghezza = larghezzaPagina - 100;
        };
    };
    if (altezza == 0) {
        altezza = altezzaPagina;
    } else {
        if (altezza >= (altezzaPagina - 50)) {
            altezza = altezzaPagina - 100;
        };
    };
    var oWnd = $find(RadWindow);
    var autoS = false;
    var beH = Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Close;
    oWnd.setUrl(Page);
    if (maximizeWin == 1) {
        oWnd.setSize(larghezza, altezza);
        oWnd.maximize();
    } else {
        oWnd.setSize(larghezza, altezza);
    };
    oWnd.set_autoSize(autoS);
    oWnd.set_behaviors(beH);
    oWnd.show();
};
function nascondiCaricamentoInCorso(sender, args) {
    nascondi = 0;
};

