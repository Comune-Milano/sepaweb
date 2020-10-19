window.onbeforeunload = ConfChiudi;
//window.onunload = Exit;
document.onmousemove = getMouse;
var myclose = false;
var posx = 0;
var posy = 0;
var exitClick = 0;
function ConfermaEsci() {
    if (document.getElementById('noClose')) {
        document.getElementById('noClose').value = 0;
    };
    if (exitClick == 0) {
        if (document.getElementById('MainContent_frmModify')) {
            if (document.getElementById('MainContent_frmModify').value == '1') {
                var chiediConferma;
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche!Proseguire l\'uscita senza salvare?");
                if (chiediConferma == false) {
                    document.getElementById('MainContent_frmModify').value = '111';
                    if (document.getElementById('noClose')) {
                        document.getElementById('noClose').value = 1;
                    };
                    return false;
                }
                else {
                    if (document.getElementById('noClose')) {
                        document.getElementById('noClose').value = 0;
                    };
                };
            }
            else {
                if (document.getElementById('noClose')) {
                    document.getElementById('noClose').value = 0;
                };
            };
        }
        else if (document.getElementById('frmModify')) {
            if (document.getElementById('frmModify').value == '1') {
                var chiediConferma;
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche!Proseguire l\'uscita senza salvare?");
                if (chiediConferma == false) {
                    document.getElementById('frmModify').value = '111';
                    if (document.getElementById('noClose')) {
                        document.getElementById('noClose').value = 1;
                    };
                    return false;
                }
                else {
                    if (document.getElementById('noClose')) {
                        document.getElementById('noClose').value = 0;
                    };
                };
            }
            else {
                if (document.getElementById('noClose')) {
                    document.getElementById('noClose').value = 0;
                };
            };
        };
    };
};
function getMouse(e) {
    var ev = (!e) ? window.event : e; //IE:Moz
    if (ev.pageX) {//Moz
        posx = ev.pageX + window.pageXOffset;
        posy = ev.pageY + window.pageYOffset;
    }
    else if (ev.clientX) {//IE
        posx = ev.clientX;
        posy = ev.clientY;
    }
    else {
        posx = 0;
        posy = 0;
    };
};
function ConfChiudi(evt) {
    var scartW = 15;
    var scartH = 15;
    var e;
    if (!evt) {
        e = window.event;
    }
    else {
        e = evt;
    };
    //getMouse(e);
    if (document.getElementById('noClose')) {
        if ((posy <= 20 || posx <= 5 || posy >= document.documentElement.clientHeight - scartH || posx >= document.documentElement.clientWidth - scartW) && document.getElementById('noClose').value == 1) {
            if (document.getElementById('noClose').value == 1) {
                if (document.getElementById('closeWait') != null && document.getElementById('closeWait').value == 1) {
                    document.getElementById('closeWait').value = 2;
                    if (document.getElementById('caricamento') != null) {
                        document.getElementById('caricamento').style.visibility = 'visible';
                    };
                };
                Exit();
                alert('La finestra è stata chiusa in modo anomalo.\nTutti i dati non salvati andranno persi.\nRicordati di usare il Pulsante ESCI!');
                if (document.getElementById('closeWait') != null && document.getElementById('closeWait').value == 2) {
                    sleep(15000);
                };
            };
        };
    };
};
function Exit() {
    var scartW = 15;
    var scartH = 15;
    if (document.getElementById('noClose')) {
        if ((posy <= 20 || posx <= 5 || posy >= document.documentElement.clientHeight - scartH || posx >= document.documentElement.clientWidth - scartW) && document.getElementById('noClose').value == 1) {
            if (document.getElementById('noClose').value == 1) {
                if (document.getElementById('btnEsci') != null) {
                    exitClick = 1;
                    document.getElementById('btnEsci').click();
                }
                else if (document.getElementById('MasterPage_MainContent_btnEsci') != null) {
                    exitClick = 1;
                    document.getElementById('MasterPage_MainContent_btnEsci').click();
                };
            };
        };
    };
};
function sleep(delay) {
    var start = new Date().getTime();
    while (new Date().getTime() < start + delay);
};