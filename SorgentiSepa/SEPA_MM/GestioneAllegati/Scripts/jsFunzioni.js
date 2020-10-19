/* ********** MESSAGGGI PREDEFINITI ********** */
var Elimina = 'Sei sicuro di voler procedere con l\'eliminazione dell\'allegato?';
var Elimina2 = 'Sei sicuro di voler procedere con l\'eliminazione del dato?';
var NessunaSelezione = 'Nessun dato selezionato!';
/* ********** MESSAGGGI PREDEFINITI ********** */
/* ********** FUNZIONI GESTIONE DATAGRID ********** */
var Selezionato;
var OldColor;
var SelColo;
/* ********** FUNZIONI GESTIONE DATAGRID ********** */
/* ********** FUNZIONI GESTIONE JQUERY ********** */
function BeforeSubmit() {
    caricamento(1, 1);
    loading(0);
};
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
    var percorso = 'Immagini/load.gif';
    if (tipo == 0) {
        $(function () {
            $('#imageLoading').attr('src', percorso);
            $('#loading').append("<p>");
            $("#imageLoading").appendTo($("#loading"));
            $('#loading').append("</p><br />Attendere...L\'operazione potrebbe impiegare qualche secondo!");
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
            $('#imageLoading').attr('src', percorso);
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
function message(titolo, testo, close, redirect) {
    if ('undefined' === typeof close) {
        close = 0;
    };
    if ('undefined' === typeof redirect) {
        redirect = '';
    };
    $(function () {
        $('#dialog').html('<p><span class="ui-icon ui-icon-info" style="float: left; margin: 0 7px 50px 0;"></span>' + testo + '</p>');
        $('#dialog').dialog({
            closeOnEscape: false,
            draggable: false,
            resizable: false,
            dialogClass: 'loadingScreenWindow',
            title: titolo,
            modal: true,
            buttons: { Ok: function () {
                $(this).dialog('close');
                if (close == 1) {
                    validNavigation = true;
                    self.close();
                }
                if (redirect != '') {
                    validNavigation = true;
                    location.replace(redirect);
                }
            }
            }
        });
    });
    window.focus();
};
/* ********** FUNZIONI GESTIONE JQUERY ********** */
/* ********** FUNZIONI GESTIONE DATI ********** */
function downloadFile(filePath) {
    location.replace('' + filePath + '');
};
/* ********** FUNZIONI GESTIONE DATI ********** */
/* ********** FUNZIONI GESTIONE EVENTI PAGINE ********** */
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
                    document.getElementById('' + idoggetto + '').click();
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
        if (multiline != 'TEXTAREA') {
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
                            document.getElementById('' + idoggetto + '').click();
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
                            document.getElementById('' + idoggetto + '').click();
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
                    document.getElementById('' + idoggetto + '').click();
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
                            document.getElementById('' + idoggetto + '').click();
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
                            document.getElementById('' + idoggetto + '').click();
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
/* ********** FUNZIONI GESTIONE EVENTI PAGINE ********** */
function CenterPageModal(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2) - 15;
    var top = (screen.height / 2) - (h / 2) - 15;
    var targetWin = window.showModalDialog(pageURL, window, 'status:no;toolbar=no;dialogWidth:' + w + 'px;dialogHeight:' + h + 'px;dialogHide:true;help:no;scroll:no;dialogtop:' + top + ';dialogleft:' + left);
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

function closeWinAndRicaricaTipologie(pulsante) {
    GetRadWindow().close();
   // GetRadWindow().BrowserWindow.document.getElementById(pulsante).click;
    GetRadWindow().BrowserWindow.refreshPage(pulsante);
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