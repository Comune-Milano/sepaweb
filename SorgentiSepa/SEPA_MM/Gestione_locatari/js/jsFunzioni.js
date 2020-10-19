function chiudiMaschera(sender, args) {
    CancelEdit();
}


function ChiudiPopUp(btnClick) {
    GetRadWindow().BrowserWindow.refreshPageJS(btnClick);
    GetRadWindow().close();
}

function ChiudiFinestra(btnClick) {

    if (document.getElementById('frmModify')) {
        if (document.getElementById('frmModify').value != '0') {
            apriConfirm(Messaggio.Uscita_Messaggio, function callbackFn(arg) { if (arg == true) { ChiudiPopUp(btnClick); } }, 300, 150, MessaggioTitolo.Attenzione, null);
        } else {
            ChiudiPopUp(btnClick);
        }
    } else {
        ChiudiPopUp(btnClick);
    }

}

function CalcoloCodFiscale(btn) {
    Gestione(btn, 'modalRadWindow', 1, '../SERVICES/CodiceFiscale.aspx?T=1', 645, 550);
}

function ReturnValori(tipo) {
    tipo = parseInt(tipo);
    if (tipo == 1) {
        return 'MasterPage_CPContenuto_txtCodiceFiscale';
    } else if (tipo == '2') {
        return document.getElementById('MasterPage_CPContenuto_txtCognome').value;
    } else if (tipo == 3) {
        return 'MasterPage_CPContenuto_txtCognome';
    } else if (tipo == 4) {
        return document.getElementById('MasterPage_CPContenuto_txtNome').value;
    } else if (tipo == 5) {
        return 'MasterPage_CPContenuto_txtNome';
    } else if (tipo == 6) {
        return document.getElementById('MasterPage_CPContenuto_ddlSesso').control._value;
    } else if (tipo == 7) {
        return 'MasterPage_CPContenuto_ddlSesso';
    } else if (tipo == 8) {
        return document.getElementById('MasterPage_CPContenuto_ddlNazione').control._value;
    } else if (tipo == 9) {
        return 'MasterPage_CPContenuto_ddlNazione';
    } else if (tipo == 10) {
        return document.getElementById('HFCodComuneSimple').value;
    } else if (tipo == 11) {
        return 'acbComune';
    } else if (tipo == 12) {
        return document.getElementById('MasterPage_CPContenuto_txtDataNascita').value;
    } else if (tipo == 13) {
        return 'MasterPage_CPContenuto_txtDataNascita';
    } else if (tipo == 14) {
        return 'CPContenuto_divComuneNascita';
    } else if (tipo == 15) {
        return document.getElementById('MasterPage_CPContenuto_ddlNazione').control._text;
    } else if (tipo == 16) {
        return document.getElementById('acbComune').value;
    } else if (tipo == 17) {
        return 'HFCodComuneSimple';
    } else if (tipo == 18) {
        return 'MasterPage_CPContenuto_txtProvincia';
    };
};

function changeContent(name, value) {

    var combo = $find(name);

    var itm = combo.findItemByValue(value);
    itm.select();

};

function SetDataNascita(controllo, data) {
    document.getElementById('HFDisableResetComune').value = '1';
    var datepicker = $find(controllo);
    setDataTelerikJS(datepicker, data);
};
function SetNazioneNascita(controllo, value) {
    var attr;
    attr = $telerik.$('#' + controllo).attr('onchange');
    $telerik.$('#' + controllo).attr('onchange', '');
    var combo = $find(controllo);
    setComboTelerikJS(combo, value);
    $telerik.$('#' + controllo).attr('onchange', attr);
};
function SetComuneNascita(controllo, value) {
    var autocomplete = $find(controllo);
    setAutoCompleteTelerikJS(autocomplete, value);
};

function NascondiBottoni() {
    if (document.getElementById('idMotivoIstanza').value != '2') {
        if (document.getElementById('idMotivoIstanza').value == '1') {
            document.getElementById('bottoniAmpliamento').style.display = 'block';
        }
        else {
            document.getElementById('bottoniAmpliamento').style.display = 'none';
        }

    } else {

        document.getElementById('bottoniAmpliamento').style.display = 'block';
    }
};

function RowSelectingNucleo(sender, args) {
    document.getElementById('idSelectedNucleo').value = args.getDataKeyValue("ID");
};
function ModificaDblClickNucleo() {
    if (document.getElementById('solaLettura').value == '0') {
        document.getElementById('MasterPage_CPContenuto_btnModificaNucleo').click();
    };
};

function RowSelectingOspite(sender, args) {
    document.getElementById('idSelectedOspite').value = args.getDataKeyValue("ID");
};
function ModificaDblClickOspite() {
    if (document.getElementById('solaLettura').value == '0') {
        document.getElementById('MasterPage_CPContenuto_btnModificaOspite').click();
    };
};

function RowSelectingRedditi(sender, args) {
    document.getElementById('idSelectedCompRedd').value = args.getDataKeyValue("IDCOMP");
    document.getElementById('idSelectedRedditi').value = args.getDataKeyValue("IDREDD");
};
function ModificaDblClickRedditi() {
    if (document.getElementById('solaLettura').value == '0') {
        document.getElementById('MasterPage_CPContenuto_btnModificaRedditi').click();
    };
};

function RowSelectingSpese(sender, args) {
    document.getElementById('idSelectedSpese').value = args.getDataKeyValue("ID");
};
function ModificaDblClickSpese() {
    if (document.getElementById('solaLettura').value == '0') {
        document.getElementById('MasterPage_CPContenuto_btnModificaSpese').click();
    };
};

function RowSelectingDetrazioni(sender, args) {
    document.getElementById('idSelectedDetrazioni').value = args.getDataKeyValue("IDDETR");
};
function ModificaDblClickDetrazioni() {
    if (document.getElementById('solaLettura').value == '0') {
        document.getElementById('MasterPage_CPContenuto_btnModificaDetrazioni').click();
    };
};

function RowSelectingPatrMobiliare(sender, args) {
    document.getElementById('idSelectedPatrMobiliare').value = args.getDataKeyValue("IDMOB");
};
function ModificaDblClickPatrMobiliare() {
    if (document.getElementById('solaLettura').value == '0') {
        document.getElementById('MasterPage_CPContenuto_btnModificaPatrimonioMobiliare').click();
    };
};

function RowSelectingPatrImmobiliare(sender, args) {

    document.getElementById('idSelectedPatrImmobiliare').value = args.getDataKeyValue("IDIMMOB");

};
function ModificaDblClickPatrImmobiliare() {
    if (document.getElementById('solaLettura').value == '0') {
        document.getElementById('MasterPage_CPContenuto_btnModificaPatrimonioImmobiliare').click();
    };
};

function RowSelectingPG(sender, args) {
    document.getElementById('idSelectedDom').value = '0';
    document.getElementById('idSelectedPG').value = args.getDataKeyValue("ID_DICH");

};
function ModificaDblClickPG() {
    document.getElementById('MasterPage_CPMenu_btnVisualizzaPG').click();
};

function RowSelectingDom(sender, args) {
    document.getElementById('idSelectedPG').value = '0';
    document.getElementById('idSelectedDom').value = args.getDataKeyValue("ID_DOM");

};
function ModificaDblClickDom() {
    document.getElementById('MasterPage_CPMenu_btnVisualizzaPG').click();
};

function onSalvaClicking() {
    mostraErroriValidazione();
};
function Validate(sender, args) {
    if (document.getElementById(sender.controltovalidate).value != "") {
        args.IsValid = true;
    } else {
        args.IsValid = false;
    }
}
function mostraErroriValidazione() {
    if (Page_Validators) {
        for (i = 0; i < Page_Validators.length; i++) {
            var v = Page_Validators[i];
            var control = document.getElementById(Page_Validators[i].controltovalidate);
            if (!v.isvalid) {

                control.className = "ErrorControl";
            } else {
                control.className = "";
            }
        }
        return false;
    }
    return true;
};

function NuovoComponente(sender, args) {

    var nuovo_comp = $find("cmbNuovoComp");
    var fl_nuovo_comp = nuovo_comp.get_value();

    if (fl_nuovo_comp == '1') {

        document.getElementById('NuovoComp').style.visibility = 'visible';

    } else {

        document.getElementById('NuovoComp').style.visibility = 'hidden';

    };
};

function CalcoloCodFiscale2(btn) {
    Gestione(btn, 'modalRadWindow', 1, '../SERVICES/CodiceFiscale.aspx', 645, 550);
};


function RowSelectingDoc(sender, args) {
    document.getElementById('idSelectedDoc').value = args.getDataKeyValue("ID");
};
function ModificaDblClickDoc() {
    if (document.getElementById('idSelectedDoc').value != '') {
        document.getElementById('MasterPage_CPContenuto_btnStampaDoc').click();
    }
    else {
        apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, '', '', '');
    }
};

function RowSelectingStampa(sender, args) {
    document.getElementById('idSelectedStampa').value = args.getDataKeyValue("ID");
};
function ModificaDblClickStampa() {
    if (document.getElementById('idSelectedStampa').value != '') {
        document.getElementById('MasterPage_CPContenuto_btnDownload').click();
    }
    else {
        apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, '', '', '');
    }
};

