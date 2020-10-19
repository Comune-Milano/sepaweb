var WidthFinestraStandard = 1300;
var HeightFinestraStandard = 750;
function CenterPage(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2) - 15;
    var top = (screen.height / 2) - (h / 2) - 15;
    var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
function CenterPage2(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2) - 15;
    var top = (screen.height / 2) - (h / 2) - 15;
    var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
function CenterPageModal(pageURL, title, w, h) {
    var left = (screen.width / 2) - (w / 2) - 15;
    var top = (screen.height / 2) - (h / 2) - 15;
    var targetWin = window.showModalDialog(pageURL, title, 'status:no;toolbar=no;dialogWidth:' + w + 'px;dialogHeight:' + h + 'px;dialogHide:true;help:no;scroll:no;dialogtop:' + top + ';dialogleft:' + left);
};
/* ***** AMBIENTE ***** */
var c1 = '#E0E4E3';
var c2 = '#006600';
var testo = '';
function colore1() {
    codice = '<font size="4" color=' + c1 + '><b>' + testo + '</b></font>';
    if (document.getElementById("testo")) {
        document.getElementById("testo").innerHTML = codice;
    };
    attesa = window.setTimeout("colore2();", 500);
};
function colore2() {
    codice = '<font size="4" color=' + c2 + '><b>' + testo + '</b></font>';
    if (document.getElementById("testo")) {
        document.getElementById("testo").innerHTML = codice;
    };
    attesa = window.setTimeout("colore1();", 500);
};
function ParametroAmbiente() {
    if (document.getElementById('HFSepaTest').value == '1') {
        testo = 'AMBIENTE DI TEST';
    } else if (document.getElementById('HFSepaTest').value == '2') {
        testo = 'AMBIENTE DI PRE - PRODUZIONE';
    };
    if (document.getElementById("testo")) {
        document.getElementById("testo").style.visibility = 'visible';
        attesa = window.setTimeout("colore1();", 500);
    };
    var TitlePage = 'Sep@Com';
    if (document.getElementById('HFModulo')) {
        TitlePage = TitlePage + ' - ' + document.getElementById('HFModulo').value
    } else {
        TitlePage = document.title;
    };
    var indextesto = TitlePage.indexOf(testo);
    if (indextesto == -1) {
        TitlePage = TitlePage + ' - ' + testo;
    };
    document.title = TitlePage;
};
/* ***** AMBIENTE ***** */
/* ***** FUNZIONI LOCK PAGE ***** */
var idPageLock;
function getIdPageLock(par) {
    var urlService = document.getElementById('HFPathLock').value + '/getPageLock';
    var dataToSend = '';
    if ('undefined' === typeof par) {
        dataToSend = { parametroLockPage: '' };
    } else {
        dataToSend = { parametroLockPage: par };
    };
    $.ajax({
        url: urlService,
        dataType: "json",
        data: JSON.stringify(dataToSend),
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
/* ***** FUNZIONI LOCK PAGE ***** */
/* ***** MENU REDIRECT ***** */
function TornaHome(sender, args) {
    if (document.getElementById('optMenu').value == '0') {
        var path = window.location.pathname;
        var page = path.substring(path.lastIndexOf('/') + 1);
        if (page == 'Home.aspx') {
            validNavigation = true;
            self.close();
        } else {
            if (getIdPageLock != null) {
                getIdPageLock();
                validNavigation = true;
                if (document.getElementById('HFPathExit')) {
                    location.href = document.getElementById('HFPathExit').value + 'Home.aspx?KEY=' + idPageLock;
                } else {
                location.href = 'Home.aspx?KEY=' + idPageLock;
                };
            } else {
                validNavigation = true;
                if (document.getElementById('HFPathExit')) {
                    location.href = document.getElementById('HFPathExit').value + 'Home.aspx';
                } else {
                location.href = 'Home.aspx';
            };
        };
        };
    } else {
        apriAlert(Messaggio.NoGo, 300, 150, 'Attenzione', null, null);
    };
};
function TornaHomeLock(sender, args) {
    var path = window.location.pathname;
    var page = path.substring(path.lastIndexOf('/') + 1);
    if (page == 'Home.aspx') {
        if (document.getElementById('solaLettura')) {
            if (document.getElementById('solaLettura').value == '0') {
                gestioneLock('releaselock');
            };
        };
        validNavigation = true;
        self.close();
    } else {
        if (getIdPageLock != null) {
            if (document.getElementById('solaLettura')) {
                if (document.getElementById('solaLettura').value == '0') {
                    gestioneLock('releaselock');
                };
            };
            getIdPageLock();
            validNavigation = true;
            if (document.getElementById('HFPathExit')) {
                location.href = document.getElementById('HFPathExit').value + 'Home.aspx?KEY=' + idPageLock;
            } else {
                location.href = 'Home.aspx?KEY=' + idPageLock;
            };
        } else {
            if (document.getElementById('solaLettura')) {
                if (document.getElementById('solaLettura').value == '0') {
                    gestioneLock('releaselock');
                };
            };
            validNavigation = true;
            if (document.getElementById('HFPathExit')) {
                location.href = document.getElementById('HFPathExit').value + 'Home.aspx';
            } else {
                location.href = 'Home.aspx';
            };
        };
    };
};
function CallPageFromMenu(Page) {
    if (document.getElementById('optMenu') != null) {
        if (document.getElementById('optMenu').value == '0') {
            validNavigation = true;
            location.href = Page;
        } else {
            apriAlert(Messaggio.NoGo, 300, 150, 'Attenzione', null, null);
        };
    } else {
        validNavigation = true;
        if (document.getElementById('HFPathExit')) {
            var percorsoCompleto = Page.indexOf('/');
            if (percorsoCompleto <= 0) {
                location.href = document.getElementById('HFPathExit').value + Page;
            } else {
                location.href = Page;
            };
        } else {
            location.href = Page;
        };
    };
};
function CallPageFromMenuLock(Page) {
    if (document.getElementById('optMenu') != null) {
        if (document.getElementById('optMenu').value == '0') {
            var index = Page.indexOf('?');
            var parametroLock;
            if (index > 0) {
                parametroLock = Page.substr(Page.indexOf("?") + 1);
            } else {
                parametroLock = '';
            };
            getIdPageLock(parametroLock);
            if (index > 0) {
                Page = Page + '&KEY=' + idPageLock;
            } else {
                Page = Page + '?KEY=' + idPageLock;
            };
            validNavigation = true;
            if (document.getElementById('HFPathExit')) {
                var percorsoCompleto = Page.indexOf('/');
                if (percorsoCompleto <= 0) {
                    location.href = document.getElementById('HFPathExit').value + Page;
                } else {
                    location.href = Page;
                };
            } else {
                location.href = Page;
            };
        } else {
            apriAlert(Messaggio.NoGo, 300, 150, 'Attenzione', null, null);
        };
    } else {
        var index = Page.indexOf('?');
        var parametroLock;
        if (index > 0) {
            parametroLock = Page.substr(Page.indexOf("?") + 1);
        } else {
            parametroLock = '';
        };
        getIdPageLock(parametroLock);
        if (index > 0) {
            Page = Page + '&KEY=' + idPageLock;
        } else {
            Page = Page + '?KEY=' + idPageLock;
        };
        validNavigation = true;
        if (document.getElementById('HFPathExit')) {
            var percorsoCompleto = Page.indexOf('/');
            if (percorsoCompleto <= 0) {
                location.href = document.getElementById('HFPathExit').value + Page;
            } else {
        location.href = Page;
    };
        } else {
            location.href = Page;
        };
};
};
/* ***** MENU REDIRECT ***** */
/* ***** FUNZIONI OPEN PAGE ***** */
function ApriModuloStandard(percorso, title) {
    var w = 1300;
    var h = 700;
    var left = ((screen.width / 2) - (w / 2)) - 15;
    var top = ((screen.height / 2) - (h / 2)) - 15;
    var index = percorso.indexOf('?');
    var parametroLock;
    if (index > 0) {
        parametroLock = percorso.substr(percorso.indexOf("?") + 1);
    } else {
        parametroLock = '';
    };
    getIdPageLock(parametroLock);
    if (index > 0) {
        percorso = percorso + '&KEY=' + idPageLock;
    } else {
        percorso = percorso + '?KEY=' + idPageLock;
    };
    var targetWin = window.open(percorso, title.replace(/ /g, ''), 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
/* ***** FUNZIONI OPEN PAGE ***** */
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
function AutoDecimal(obj, numdec) {
    if (numdec == null) numdec = 2;
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') != 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(numdec);
        if (a != 'NaN') {
            if (numdec > 0) {
                if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - numdec);
                    var dascrivere = a.substring(a.length - (numdec + 1), 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    };
                    risultato = dascrivere + risultato + ',' + decimali;
                    document.getElementById(obj.id).value = risultato;
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',');
                };
            }
            else {
                if (a.substring(a.length - (numdec + 1), 0).length >= 3) {
                    var dascrivere = a.substring(a.length, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    };
                    risultato = dascrivere + risultato;
                    document.getElementById(obj.id).value = risultato;
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',');
                };

            };
        }
        else {
            document.getElementById(obj.id).value = '';
        };
    };
};
function AutoDecimaljs(obj, numdec) {
    if (numdec == null) numdec = 2;
    obj = obj.replace('.', '');
    if (obj.replace(',', '.') != 0) {
        var a = obj.replace(',', '.');
        a = parseFloat(a).toFixed(numdec);
        if (a != 'NaN') {
            if (numdec > 0) {
                if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - numdec);
                    var dascrivere = a.substring(a.length - (numdec + 1), 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    };
                    risultato = dascrivere + risultato + ',' + decimali;
                    obj = risultato;
                }
                else {
                    obj = a.replace('.', ',');
                };
            }
            else {
                if (a.substring(a.length - (numdec + 1), 0).length >= 3) {
                    var dascrivere = a.substring(a.length, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    };
                    risultato = dascrivere + risultato;
                    obj = risultato;
                }
                else {
                    obj = a.replace('.', ',');
                };

            };
        }
        else {
            obj = '';
        };
    };
    return obj;
};
function EuroToLire(obj, where, numdec) {
    if (numdec == null) numdec = 2;
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') >= 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(numdec);
        a = (a * 1936.27);
        a = parseFloat(a).toFixed(0);
        if (a != 'NaN') {
            if (a.substring(a.length, 0).length >= 4) {
                var dascrivere = a.substring(a.length, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {
                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                };
                risultato = dascrivere + risultato;
                document.getElementById(where.id).value = risultato;
            }
            else {
                document.getElementById(where.id).value = a.replace('.', ',');
            };
        }
        else {
            if (obj.value == '') {
                document.getElementById(where.id).value = '';
            };
        };
    };
};
function LireToEuro(obj, where, numdec) {
    if (numdec == null) numdec = 2;
    obj.value = obj.value.replace('.', '');
    if (obj.value.replace(',', '.') >= 0) {
        var a = obj.value.replace(',', '.');
        a = parseFloat(a).toFixed(0);
        a = (a / 1936.27);
        a = parseFloat(a).toFixed(numdec);
        if (a != 'NaN') {
            if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                var decimali = a.substring(a.length, a.length - numdec);
                var dascrivere = a.substring(a.length - (numdec + 1), 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {
                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                };
                risultato = dascrivere + risultato + ',' + decimali;
                document.getElementById(where.id).value = risultato;
            }
            else {
                document.getElementById(where.id).value = a.replace('.', ',');
            };
        }
        else {
            if (obj.value == '') {
                document.getElementById(where.id).value = '';
            };
        };
    };
};
function selectall(txtselezione) {
    var text_val = eval("txtselezione");
    text_val.focus();
    text_val.select();
};
/* ***** FUNZIONI TELERIK ***** */
function apriAlert(testo, larghezza, altezza, titolo, callback, img) {
    if (img == null) {
        img = '../StandardTelerik/Immagini/Messaggi/alert.png';
    };
    var alertTelerik = radalert(testo, larghezza, altezza, titolo, callback, img);
    alertTelerik = alertTelerik.set_behaviors();
};
function apriConfirm(testo, funzione, larghezza, altezza, titolo, img) {
    if (img == null) {
        img = '../StandardTelerik/Immagini/Messaggi/alert.png';
    };
    var confirmTelerik = radconfirm(testo, funzione, larghezza, altezza, null, titolo, img);
    confirmTelerik = confirmTelerik.set_behaviors();
};
function SelfCloseNoPostback(sender, args) {
    args.set_cancel(self.close());
};
function CalendarDatePicker(sender, args) {
    sender.get_owner().showPopup();
};
function CalendarDatePickerHide(sender, eventArgs) {
    if (eventArgs.keyCode == 9) {
        var dateInput = $telerik.findDateInput(sender.id);
        dateInput.get_owner().hidePopup();
    };
};
function CompletaDataTelerik(sender, args) {
    var keyCode = args.get_keyCode();
    if (keyCode != 9) {
    if ((keyCode < 48) || (keyCode > 57)) {
        if (keyCode != 8) {
            args.set_cancel(true);
        };
    };
    var testo = sender._textBoxElement.value;
    var testolen = testo.length;
    if (testolen == 10) {
        var selinizio = sender._textBoxElement.selectionStart;
        var selfine = sender._textBoxElement.selectionEnd;
        if ((selinizio == 0 && selfine == 10) == false) {
            if (keyCode != 8) {
                args.set_cancel(true);
            };
        } else {
            if ((keyCode < 48) || (keyCode > 51)) {
                if (keyCode != 8) {
                    args.set_cancel(true);
                };
            };
        };
    } else {
        if (testolen == 0) {
            if ((keyCode < 48) || (keyCode > 51)) {
                if (keyCode != 8) {
                    args.set_cancel(true);
                };
            };
        } else if (testolen == 1) {
            if (testo == 0) {
                if ((keyCode < 49) || (keyCode > 57)) {
                    if (keyCode != 8) {
                        args.set_cancel(true);
                    };
                };
            } else if (testo == 3) {
                if ((keyCode < 48) || (keyCode > 49)) {
                    if (keyCode != 8) {
                        args.set_cancel(true);
                    };
                };
            };
        } else if (testolen == 2) {
            if ((keyCode < 48) || (keyCode > 49)) {
                if (keyCode != 8) {
                    args.set_cancel(true);
                };
            } else {
                sender._textBoxElement.value = testo + '/';
            };
        } else if (testolen == 3) {
            if ((keyCode < 48) || (keyCode > 49)) {
                if (keyCode != 8) {
                    args.set_cancel(true);
                };
            };
        } else if (testolen == 4) {
            var n = testo.substr(3, 1);
            if (n == 0) {
                if ((keyCode < 49) || (keyCode > 57)) {
                    if (keyCode != 8) {
                        args.set_cancel(true);
                    };
                };
            } else if (n == 1) {
                if ((keyCode < 48) || (keyCode > 50)) {
                    if (keyCode != 8) {
                        args.set_cancel(true);
                    };
                };
            };
        } else if (testolen == 5) {
            if (keyCode != 8) {
                sender._textBoxElement.value = testo + '/';
                };
            };
        };
    };
};
function CompletaAnnoTelerik(sender, args) {
    var keyCode = args.get_keyCode();
    if (keyCode != 9) {
        var testo = sender._textBoxElement.value;
        var testolen = testo.length;
        if (testolen >= 4) {
            args.set_cancel(true);
        };
    };
};
function CompletaPeriodoTelerik(sender, args) {
    var keyCode = args.get_keyCode();
    if (keyCode != 9) {
        var testo = sender._textBoxElement.value;
        var testolen = testo.length;
        if (testolen == 2) {
            sender._textBoxElement.value = testo + '/';
        } else if (testolen >= 7) {
            args.set_cancel(true);
        };
    };
};
function CompletaOrarioTelerik(sender, args) {
    var keyCode = args.get_keyCode();
    if (keyCode != 9) {
        var testo = sender._textBoxElement.value;
        var testolen = testo.length;
        if (testolen == 2) {
            sender._textBoxElement.value = testo + ':';
        } else if (testolen == 5) {
            sender._textBoxElement.value = testo + ':';
        } else if (testolen >= 8) {
            args.set_cancel(true);
        };
    };
};
function CompletaOrarioTelerikShort(sender, args) {
    var keyCode = args.get_keyCode();
    if (keyCode != 9) {
        var testo = sender._textBoxElement.value;
        var testolen = testo.length;
        if (testolen == 2) {
            sender._textBoxElement.value = testo + ':';
        } else if (testolen == 5) {
            sender._textBoxElement.value = testo + ':';
        } else if (testolen > 5) {
            args.set_cancel(true);
        };
    };
};
function openWindow(sender, args, nomeRad) {
    var radwindow = $find(nomeRad);
    radwindow.show();
};
function openWindowASP(nomeRad) {
    var radwindow = $find(nomeRad);
    radwindow.show();
};
function closeWindow(sender, args, nomeRad) {
    var radwindow = $find(nomeRad);
    radwindow.close();
};
function closeWindowASP(nomeRad) {
    var radwindow = $find(nomeRad);
    radwindow.close();
};
function deleteElementTelerik(sender, args, elementoSelezionato) {
    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            this.click();
        }
    });
    if (document.getElementById(elementoSelezionato).value != '') {
        apriConfirm(Messaggio.Elemento_Elimina, callBackFunction, 300, 150, Messaggio.Titolo_Conferma, '');
    } else {
        apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, '', '', '');
    };
    args.set_cancel(true);
};
function OnClientLoadHandler(sender) {
    sender.get_inputDomElement().readOnly = "readonly";
};
function openWinUrl(RadWindow, Page, w, h) {
    var oWnd = $find(RadWindow);
    oWnd.setUrl(Page);
    oWnd.setSize(w, h);
    oWnd.moveTo(10, 10);
    oWnd.show();
};
function openNewWinUrl(Page, Title, w, h) {
    var oWnd = radopen(Page, Title, w, h, 10, 10);
};
function GetRadWindow() {
    var oWindow = null;
    try {
        if (window.radWindow)
        oWindow = window.radWindow;
        else if (window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    }
    catch (err) {
        oWindow = null;
    }
    return oWindow;
            };
function apriConfirmSubmit(sender, args, titolo, testo, larghezza, altezza, button, img) {
    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            if (document.getElementById(button)) {
                args.set_cancel(true);
                document.getElementById(button).click();
        };
    };
    });
    apriConfirm(testo, callBackFunction, larghezza, altezza, titolo, img);
    args.set_cancel(true);
};
function disabledElementTelerik(sender, args, elementoSelezionato, flAttivo) {
    if (document.getElementById(flAttivo).value == '1') {
        if (document.getElementById('NavigationMenu')) {
            document.getElementById('NavigationMenu').style.visibility = 'hidden';
        };
        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
            if (shouldSubmit) {
                this.click();
            }
        });
        if (document.getElementById(elementoSelezionato).value != '') {
            apriConfirm(Messaggio.Elemento_Disattiva, callBackFunction, 300, 150, Messaggio.Titolo_Conferma, null);
        } else {
            apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
        };
    } else {
        if (document.getElementById(elementoSelezionato).value != '') {
            apriAlert(Messaggio.Elemento_Gia_Disabilitato, 300, 150, Messaggio.Titolo_Conferma, null, null);
        } else {
            apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
};
    };
    args.set_cancel(true);
};
/* ***** RADCONTEXTMENU ***** */
function OnClientMouseOverRadContextMenuButton(sender, args, radContextMenu, radButton) {
    var contextMenu = $find(radContextMenu);
    var button = $find(radButton);
    var top = button._element.offsetTop + 21;
    var left = button._element.offsetLeft;
    contextMenu.showAt(left, top);
};
function OnClientShowingHandlerRadContextMenuButton(sender, args) {
    var element = sender.get_contextMenuElement();
    var handler = function (e) {
        var relatedTarget = e.rawEvent.relatedTarget || e.rawEvent.toElement;
        if (!isDescendantOrSelf(element, relatedTarget)) {
            sender.hide();
            $removeHandler(element, "mouseout", handler);
            return;
        }
    };
    $addHandler(element, "mouseout", handler);
};
/* ***** RADCONTEXTMENU ***** */
/* ***** USCITA TELERIK ***** */
function ConfermaEsciTelerik(sender, args, typeW, typeC) {
    if ('undefined' === typeof typeC) {
        typeC = '';
    };
    if (document.getElementById('frmModify')) {
        if (document.getElementById('frmModify').value != '0') {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    if (typeC != '') {
                        if (document.getElementById(typeC)) {
                            args.set_cancel(true);
                            document.getElementById(typeC).click();
                        };
                    } else {
                        if (typeW == 0) {
                            validNavigation = true;
                            self.close();
                        } else {
                            GetRadWindow().close();
                        };
                    };
                }
            });
            var confirmExitTelerik = radconfirm(Messaggio.messaggioChiusuraMod, callBackFunction, 300, 150, null, Messaggio.Titolo_Conferma, '../StandardTelerik/Immagini/Messaggi/alert.png')
            confirmExitTelerik = confirmExitTelerik.set_behaviors();
            args.set_cancel(true);
        } else {
            if (typeC != '') {
                if (document.getElementById(typeC)) {
                    args.set_cancel(true);
                    document.getElementById(typeC).click();
                };
            } else {
                if (typeW == 0) {
                    validNavigation = true;
                    self.close();
                } else {
                    GetRadWindow().close();
                };
            };
        };
    } else {
        if (typeC != '') {
            if (document.getElementById(typeC)) {
                args.set_cancel(true);
                document.getElementById(typeC).click();
            };
        } else {
            if (typeW == 0) {
                validNavigation = true;
                self.close();
            } else {
                GetRadWindow().close();
            };
        };
    };
};
function ConfermaEsciLock() {
    if (document.getElementById('frmModify')) {
        if (document.getElementById('frmModify').value != '0') {
            apriConfirm(Messaggio.Uscita_Messaggio, function callbackFn(arg) { if (arg == true) { ConfermaEsciLockExit(); } }, 300, 150, MessaggioTitolo.Attenzione, null);
        } else {
            ConfermaEsciLockExit();
        };
    } else {
        ConfermaEsciLockExit();
    };
};
function ConfermaEsciLockExit() {
    loadingMenu();
    gestioneLock('releaselock');
    if (document.getElementById('HFBtnToClick')) {
        if (document.getElementById('HFBtnToClick').value != '') {
            if (!parent.window.opener.closed) {
                if (parent.window.opener.document.getElementById(document.getElementById('HFBtnToClick').value)) {
                    var attr;
                    attr = $('#' + document.getElementById('HFBtnToClick').value, parent.window.opener.document).attr('onclick');
                    $('#' + document.getElementById('HFBtnToClick').value, parent.window.opener.document).attr('onclick', '__doPostBack("' + parent.window.opener.document.getElementById(document.getElementById('HFBtnToClick').value).name + '", "")');
                    parent.window.opener.document.getElementById(document.getElementById('HFBtnToClick').value).click();
                    $('#' + document.getElementById('HFBtnToClick').value, parent.window.opener.document).attr('onclick', attr);
                };
            };
        };
    };
    validNavigation = true;
    self.close();
};
function ConfermaEsciLockRad() {
    if (document.getElementById('frmModify')) {
        if (document.getElementById('frmModify').value != '0') {
            apriConfirm(Messaggio.Uscita_Messaggio, function callbackFn(arg) { if (arg == true) { ConfermaEsciLockExitRad(); } }, 300, 150, MessaggioTitolo.Attenzione, null);
        } else {
            ConfermaEsciLockExitRad();
        };
    } else {
        ConfermaEsciLockExitRad();
    };
};
function ConfermaEsciLockExitRad() {
    loadingMenu();
    gestioneLock('releaselock');
    if (document.getElementById('HFBtnToClick')) {
        if (document.getElementById('HFBtnToClick').value != '') {
            validNavigation = true;
            CloseAndNextJS(document.getElementById('HFBtnToClick').value);
        } else {
            validNavigation = true;
            GetRadWindow().close();
        };
    } else {
        validNavigation = true;
        GetRadWindow().close();
    };
};
function ExitPage() {
    loadingMenu();
    validNavigation = true;
    self.close();
};
/* ***** USCITA TELERIK ***** */
/* ***** RADGRID TELERIK ***** */
function RowDblClick(sender, eventArgs) {
    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
};
function requestStartAjax(sender, args) {
    if (args.get_eventTarget().indexOf("ExportToExcel") >= 0) {
        document.getElementById('HFBlockExit').value = '1';
        args.set_enableAjax(false);
    } else if (args.get_eventTarget().indexOf("AddNewRecordButton") >= 0 || args.get_eventTarget().indexOf("InitInsertButton") >= 0) {
        try {
            if (InserRecord()) {
            InserRecord();
            args.set_enableAjax(false);
            args.set_cancel(true);
            };
        }
        catch (err) {
            //alert(err);
        }
    } else if (args.get_eventTarget().indexOf("EditButton") >= 0) {
        try {
            if (EditRecord()) {
            EditRecord();
            args.set_enableAjax(false);
            args.set_cancel(true);
            };
        }
        catch (err) {
            //alert(err);
        }
    };
};
/* ***** RADGRID TELERIK ***** */
/* ***** FUNZIONI TELERIK ***** */
/* ***** NOTIFICA ***** */
function NotificaMsg(testomessaggio, tipo) {
    if (tipo == 1) {
        $.notify(testomessaggio.replace("'", "\'"), 'success');
    } else if (tipo == 2) {
        $.notify(testomessaggio.replace("'", "\'"), 'warn');
    } else if (tipo == 3) {
        $.notify(testomessaggio.replace("'", "\'"), 'error');
    } else if (tipo == 4) {
        $.notify(testomessaggio.replace("'", "\'"), 'info');
    };
};
/* ***** NOTIFICA ***** */
/* ***** NOTIFICA TELERIK ***** */
function NotificaTelerik(ntf, titolo, messaggio) {
    if ('undefined' === typeof titolo) {
        titolo = MessaggioTitolo.Attenzione;
    };
    var notification = $find(ntf);
    if (notification) {
        notification.set_title(titolo);
        notification.set_text(messaggio);
        notification.show();
    };
};
/* ***** NOTIFICA TELERIK ***** */
/* FUNZIONI ADD/EDIT/DEL */
function Aggiungi(btnToClik, idradwindow, tipo, page, w, h, maximizeWin) {
    if ('undefined' === typeof maximizeWin) {
        maximizeWin = 0;
    };
    var index = page.indexOf('?');
    var parametroLock;
    if (index > 0) {
        parametroLock = page.substr(page.indexOf("?") + 1);
    } else {
        parametroLock = '';
    };
    getIdPageLock(parametroLock + '&BTN=' + btnToClik.id + '&ID=-1');
    var concatenazione = '?';
    if (page.indexOf("?") >= 0) {
        concatenazione = '&';
    };
    if (tipo == 0) {
        openModalInRad(idradwindow, page + concatenazione + 'KEY=' + idPageLock + '&BTN=' + btnToClik.id + '&ID=-1', w, h, undefined, undefined, undefined, maximizeWin);
    } else {
        openModalInRadClose(idradwindow, page + concatenazione + 'KEY=' + idPageLock + '&BTN=' + btnToClik.id + '&ID=-1', w, h, maximizeWin);
    };
};
function AggiungiNewPage(btnToClik, page, title) {
    ApriModuloStandard(page + '&BTN=' + btnToClik.id + '&ID=-1', title + '-1');
};
function Modifica(btnToClik, idradwindow, tipo, page, hfidsel, ntf, w, h, maximizeWin) {
    if ('undefined' === typeof maximizeWin) {
        maximizeWin = 0;
    };
    if (document.getElementById(hfidsel)) {
        if (document.getElementById(hfidsel).value != '') {
            var index = page.indexOf('?');
            var parametroLock;
            if (index > 0) {
                parametroLock = page.substr(page.indexOf("?") + 1);
            } else {
                parametroLock = '';
            };
            getIdPageLock(parametroLock + '&BTN=' + btnToClik.id + '&ID=' + document.getElementById(hfidsel).value);
            var concatenazione = '?';
            if (page.indexOf("?") >= 0) {
                concatenazione = '&';
            };
            if (tipo == 0) {
                openModalInRad(idradwindow, page + concatenazione + 'KEY=' + idPageLock + '&BTN=' + btnToClik.id + '&ID=' + document.getElementById(hfidsel).value, w, h, undefined, undefined, undefined, maximizeWin);
            } else {
                openModalInRadClose(idradwindow, page + concatenazione + 'KEY=' + idPageLock + '&BTN=' + btnToClik.id + '&ID=' + document.getElementById(hfidsel).value, w, h, maximizeWin);
            };
        } else {
            var notification = $find(ntf);
            notification.set_title(MessaggioTitolo.Attenzione);
            notification.set_text(Messaggio.Elemento_No_Selezione);
            notification.show();
        };
    };
};
function ModificaNewPage(btnToClik, page, title, hfidsel, ntf) {
    if (document.getElementById(hfidsel)) {
        if (document.getElementById(hfidsel).value != '') {
            ApriModuloStandard(page + '&BTN=' + btnToClik.id + '&ID=' + document.getElementById(hfidsel).value, title + document.getElementById(hfidsel).value);
        } else {
            var notification = $find(ntf);
            notification.set_title(MessaggioTitolo.Attenzione);
            notification.set_text(Messaggio.Elemento_No_Selezione);
            notification.show();
        };
    };
};
function Elimina(btnToClik, hfidsel, ntf) {
    if (document.getElementById(hfidsel)) {
        if (document.getElementById(hfidsel).value != '') {
            apriConfirm(Messaggio.Elemento_Elimina, function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, 300, 150, MessaggioTitolo.Attenzione, null);
        } else {
            var notification = $find(ntf);
            notification.set_title(MessaggioTitolo.Attenzione);
            notification.set_text(Messaggio.Elemento_No_Selezione);
            notification.show();
        };
    };
};
function Gestione(btnToClik, idradwindow, tipo, page, w, h, maximizeWin) {
    if ('undefined' === typeof maximizeWin) {
        maximizeWin = 0;
    };
    var index = page.indexOf('?');
    var parametroLock;
    if (index > 0) {
        parametroLock = page.substr(page.indexOf("?") + 1);
    } else {
        parametroLock = '';
    };
    var idBtnClick = 'null';
    if (btnToClik != null) {
        idBtnClick = btnToClik.id;
    };
    getIdPageLock(parametroLock + '&BTN=' + idBtnClick);
    var concatenazione = '?';
    if (page.indexOf("?") >= 0) {
        concatenazione = '&';
    };
    if (tipo == 0) {
        openModalInRad(idradwindow, page + concatenazione + 'KEY=' + idPageLock + '&BTN=' + idBtnClick, w, h, undefined, undefined, undefined, maximizeWin);
    } else {
        openModalInRadClose(idradwindow, page + concatenazione + 'KEY=' + idPageLock + '&BTN=' + idBtnClick, w, h, maximizeWin);
    };
};
function Notifica(ntf, Titolo, Messaggio) {
    if ('undefined' === typeof Titolo) {
        Titolo = MessaggioTitolo.Attenzione;
    };
    if (Titolo == '') {
        Titolo = MessaggioTitolo.Attenzione;
    };
    var notification = $find(ntf);
    notification.set_title(Titolo);
    notification.set_text(Messaggio);
    notification.show();
};
function Conferma(btnToClik, testo, hfidsel, ntf) {
    if (document.getElementById(hfidsel)) {
        if (document.getElementById(hfidsel).value != '') {
            apriConfirm(testo, function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, 300, 150, MessaggioTitolo.Attenzione, null);
        } else {
            var notification = $find(ntf);
            notification.set_title(MessaggioTitolo.Attenzione);
            notification.set_text(Messaggio.Elemento_No_Selezione);
            notification.show();
        };
    };
};
function MultiElimina(btnToClik, messaggio) {
    if ('undefined' === typeof messaggio) {
        messaggio = Messaggio.Elemento_Elimina;
};
    apriConfirm(messaggio, function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, 300, 150, MessaggioTitolo.Attenzione, null);
};
function MultiConferma(btnToClik, titolo, messaggio, w, h) {
    if ('undefined' === typeof w) {
        w = 400;
    };
    if ('undefined' === typeof h) {
        h = 150;
    };
    apriConfirm(messaggio, function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, w, h, titolo, null);
};
function MultiConfermaJS(btnToClik, titolo, messaggio, w, h) {
    if ('undefined' === typeof w) {
        w = 400;
    };
    if ('undefined' === typeof h) {
        h = 150;
    };
    apriConfirm(messaggio, function callbackFn(arg) { if (arg == true) { clickEliminaJS(btnToClik.id) } }, w, h, titolo, null);
};
function clickElimina(btnToClik) {
    var attr;
    attr = $('#' + btnToClik).attr('onclick');
    $('#' + btnToClik).attr('onclick', '__doPostBack("' + document.getElementById(btnToClik).name + '", "")');
    document.getElementById(btnToClik).click();
    $('#' + btnToClik).attr('onclick', attr);
};
function clickEliminaJS(btnToClik) {
    document.getElementById(btnToClik).click();
};
function CheckSelezione(btn, hfidsel, ntf) {
    if (document.getElementById(hfidsel)) {
        if (document.getElementById(hfidsel).value != '') {
            document.getElementById(btn).click();
        } else {
            var notification = $find(ntf);
            notification.set_title(MessaggioTitolo.Attenzione);
            notification.set_text(Messaggio.Elemento_No_Selezione);
            notification.show();
            return false;
        };
    };
};
/* FUNZIONI ADD/EDIT/DEL */
/* ***** FUNZIONI TELERIK ***** */
/* ***** GESTIONE INVIO ***** */
if (navigator.appName == 'Microsoft Internet Explorer') {
    document.onkeydown = $onkeydown;
}
else {
    window.document.addEventListener("keydown", TastoInvio, true);
};
function TastoInvio(e) {
    sKeyPressed1 = e.which;
    if (sKeyPressed1 == 13) {
        if (document.getElementById('HFbtnClickGo') != null) {
            if (document.getElementById('HFbtnClickGo').value != '') {
                var idoggetto = document.getElementById('HFbtnClickGo').value;
                if (document.getElementById('' + idoggetto + '')) {
                    document.getElementById('' + idoggetto + '').click();
                };
            };
        };
    };
};
function $onkeydown() {
    var alt = window.event.altKey;
    if (event.keyCode == 13) {
        if (document.getElementById('HFbtnClickGo') != null) {
            if (document.getElementById('HFbtnClickGo').value != '') {
                var idoggetto = document.getElementById('HFbtnClickGo').value;
                if (document.getElementById('' + idoggetto + '')) {
                    document.getElementById('' + idoggetto + '').click();
                };
            };
        };
    };
};
/* ***** GESTIONE INVIO ***** */
/* ***** GESTIONE TABS TELERIK ***** */
function setResizeTabs(sender, args) {
    ForceSetDimensioniPostback();
};
/* ***** GESTIONE TABS TELERIK ***** */
/* ***** SERVICES ***** */
function GestioneAllegati(oggetto, id) {
    ApriModuloStandard('../SERVICES/GestioneAllegati.aspx?T=2&O=' + oggetto + '&I=' + id, 'Allegati_' + oggetto + '_' + id);
};
function GestioneEventi(oggetto, id) {
    ApriModuloStandard('../SERVICES/GestioneEventi.aspx?O=' + oggetto + '&I=' + id, 'Eventi_' + oggetto + '_' + id);
};
function GestioneEventiDett(oggetto, id) {
    ApriModuloStandard('../SERVICES/GestioneEventiDettaglio.aspx?O=' + oggetto + '&I=' + id, 'EventiDett_' + oggetto + '_' + id);
};
/* ***** SERVICES ***** */
/* ***** RESIZE MASCHERE ***** */
function ForceSetDimensioniPostback() {
    IntervalResizeCount = 0;
    IntervalResize = setInterval(function () {
        try {
            if (IntervalResizeFunction) {
                IntervalResizeFunction();
            };
        } catch (err) {
            //NULL
        };
    }, 100);
};
/* ***** RESIZE MASCHERE ***** */
/* ***** COMBO TELERIK ***** */
function setComboTelerikJS(combo, value) {
    if (combo) {
        combo.set_value(value);
        var item = combo.findItemByValue(value);
        if (item) { item.select(); }
    };
};
/* ***** COMBO TELERIK ***** */
/* ***** AUTOCOMPLETE TELERIK ***** */
function setAutoCompleteTelerikJS(autocomplete, value) {
    var entry = new Telerik.Web.UI.AutoCompleteBoxEntry();
    entry.set_text(value);
    if (autocomplete) {
        autocomplete.get_entries().add(entry);
    };
};
/* ***** AUTOCOMPLETE TELERIK ***** */
/* ***** DATAPICKER TELERIK ***** */
function setDataTelerikJS(datapicker, data) {
    var dateVar = new Date(data);
    if (datapicker) {
        datapicker.set_selectedDate(dateVar);
    };
};
/* ***** DATAPICKER TELERIK ***** */
/* FUNZIONI GENERALE NOTE */
function RowSelectingNote(sender, args) {
    document.getElementById('idSelectedNote').value = args.getDataKeyValue("ID");
};
function ModificaDblClickNote() {
    if (document.getElementById('solaLettura').value === '0') {
        document.getElementById('CPContenuto_btnModificaNote').click();
    };
};
/* FUNZIONI GENERALE NOTE */

/* FUNZIONI TOOLTIP */
function VisualizzaTooltipTelerik(idtooltip) {
    var tooltip = $find(idtooltip);
    if (tooltip) {
        tooltip.show();
    };
};
function NascondiTooltipTelerik(idtooltip) {
    var tooltip = $find(idtooltip);
    if (tooltip) {
        tooltip.hide();
    };
};
/* FUNZIONI TOOLTIP */