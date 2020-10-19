function openModalInRad(RadWindow, Page, w, h, autosize, behavior, res, maximizeWin) {
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
    if (larghezza >= (larghezzaPagina - 50)) {
        larghezza = larghezzaPagina - 100;
    };
    if (altezza >= (altezzaPagina - 50)) {
        altezza = altezzaPagina - 100;
    };
    var oWnd = $find(RadWindow);
    var autoS = false;
    if (typeof res != "undefined") {
        res = Telerik.Web.UI.WindowBehaviors.Resize;
    };
    var beH = Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Maximize + res;
    oWnd.setUrl(Page);
    if (maximizeWin == 1) {
        oWnd.setSize(larghezza, altezza);
        oWnd.maximize();
    } else {
        oWnd.setSize(larghezza, altezza);
    };
    if (typeof autosize != "undefined") {
        autoS = autosize;
    };
    oWnd.set_autoSize(autoS);
    if (typeof behavior != "undefined") {
        beH = behavior;
    };
    oWnd.set_behaviors(beH);
    oWnd.show();
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
    var beH = Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize;
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
function CancelEdit() {
    GetRadWindow().close();
};
function reopenModlInExistingRad(Page, w, h, autosize, behavior) {
    CancelEdit();
    var oWnd = null;
    oWnd = GetRadWindow();
    oWnd.setUrl(Page);
    oWnd.setSize(w, h);
    oWnd.moveTo(10, 10);
    oWnd.show();
};
function refreshPage(btnToClik) {
    if (document.getElementById(btnToClik)) {
        var attr;
        attr = $telerik.$('#' + btnToClik).attr('onclick');
        $telerik.$('#' + btnToClik).attr('onclick', '');
        document.getElementById(btnToClik).click();
        $telerik.$('#' + btnToClik).attr('onclick', attr);
    };
};
function CloseAndNext(btnClick) {
    /*da utilizzare senza ausilio bottone nascosto*/
    GetRadWindow().BrowserWindow.refreshPage(btnClick);
    GetRadWindow().close();
};
function CloseAndRefresh(btnClick) {
    /*da utilizzare con ausilio bottone nascosto*/
    GetRadWindow().BrowserWindow.document.getElementById(btnClick).click();
    GetRadWindow().close();
};
function refreshPageJS(btnToClik) {
    if (document.getElementById(btnToClik)) {
        var attr;
        attr = $telerik.$('#' + btnToClik).attr('onclick');
        $telerik.$('#' + btnToClik).attr('onclick', '__doPostBack("' + document.getElementById(btnToClik).name + '", "")');
        document.getElementById(btnToClik).click();
        $telerik.$('#' + btnToClik).attr('onclick', attr);
    };
};
function CloseAndNextJS(btnClick) {
    GetRadWindow().BrowserWindow.refreshPageJS(btnClick);
    GetRadWindow().close();
};
function CloseModalRad(returnParameter, fName) {
    /*da utilizzare per chiudere la radwindow passando funzione di callback e parametri al parent*/
    var oWnd = GetRadWindow();
    oWnd.close();
    if (returnParameter != '') {
        window.parent.eval("" + fName + "('" + returnParameter + "')");
    };
};