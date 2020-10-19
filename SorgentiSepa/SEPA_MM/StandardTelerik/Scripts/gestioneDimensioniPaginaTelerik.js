window.onresize = setDimensioniResize;
if (typeof (Sys) != 'undefined') {
    Sys.Application.add_load(setDimensioni);
};
var IntervalResize;
var IntervalResizeCount = 0;
function IntervalResizeFunction() {
    IntervalResizeCount = IntervalResizeCount + 1;
    setDimensioni(false);
    if (IntervalResizeCount > 10) {
        clearInterval(IntervalResize);
    };
};
function setDimensioniResize() {
    setDimensioni(true);
};
function setDimensioni(parametroResize) {
    if ('undefined' === typeof parametroResize) {
        parametroResize = false;
    };
    var altezzaPaginaIntera = 0;
    var larghezzaPaginaIntera = 0;
    if (GetRadWindow()) {
        var parametroRadWindowMaximized = GetRadWindow().isMaximized();
        if (parametroRadWindowMaximized) {
            if (parametroResize) {
                altezzaPaginaIntera = $(window).height() - 20;
                larghezzaPaginaIntera = GetRadWindow().getWindowBounds().width - 18;
            } else {
                altezzaPaginaIntera = $(window).height() - 20;
                larghezzaPaginaIntera = $(window).width();
            };
        } else {
            if (parametroResize) {
                altezzaPaginaIntera = $(window).height() - 5;
                larghezzaPaginaIntera = GetRadWindow().getWindowBounds().width - 18;
            } else {
                altezzaPaginaIntera = $(window).height() + 15;
                larghezzaPaginaIntera = $(window).width();
            };
        };
    } else {
        altezzaPaginaIntera = $(window).height() - 20;
        larghezzaPaginaIntera = $(window).width() - 2;
    };
    if (document.getElementById('HFSkin')) {
        $("#RestrictionZoneID").height(altezzaPaginaIntera - 50);
        $("#RestrictionZoneID").width(larghezzaPaginaIntera);
    } else {
        $("#RestrictionZoneID").height($(window).height());
        $("#RestrictionZoneID").width($(window).width());
    };
    var altezzaTop = 0;
    if (document.getElementById('divTop')) {
        altezzaTop = 60;
    };
    var altezzaHeader = 0;
    if (document.getElementById('divHeader')) {
        altezzaHeader = 40;
    };
    var altezzaFooter = 35;
    var altezzaFooterSkin = 0;
    if (document.getElementById('HFSkin')) {
        altezzaFooterSkin = $("#footer").height() + 3; //Default: 20
    };
    var altezzaBody = altezzaPaginaIntera - altezzaTop - altezzaHeader - altezzaFooter - altezzaFooterSkin - 3;
    var altezzaTitolo = 32;
    if (!document.getElementById('divTop')) {
        altezzaTitolo = altezzaTitolo + 20;
    };
    var altezzaMenu = 35;
    var Skin = 0;
    var altezzaSkin = 0;
    var widthSkin = 0;
    if (document.getElementById('HFSkin')) {
        Skin = 1;
        var SkinPar = (document.getElementById('HFSkin').value).split(',');
        altezzaSkin = SkinPar[0];
        widthSkin = SkinPar[1];
    };
    var altezzaContenuto = altezzaBody - altezzaTitolo - altezzaMenu - altezzaSkin;
    var altezzaNavBar = 0;
    if (Skin == 1) {
        $("#sezioneContent").height(altezzaPaginaIntera - (((altezzaPaginaIntera - altezzaBody) + altezzaTitolo)));
        altezzaNavBar = $("#navbar").height();  //Default: 50
        if (altezzaNavBar != 50) {
            altezzaContenuto = altezzaContenuto - (altezzaNavBar - 50);
        } else {
            var OffsetNavBar = getOffset(document.getElementById('navbar')).top; //Default: 0/50
            if (OffsetNavBar == 50) {
                altezzaContenuto = altezzaContenuto - 50;
            };
        };
        if (altezzaFooterSkin != 20) {
            altezzaContenuto = altezzaContenuto - (altezzaFooterSkin - 20);
        };
        larghezzaPaginaIntera = larghezzaPaginaIntera - widthSkin;
        if (larghezzaPaginaIntera >= 700) {
            var widthSideBar = $("#sidebar").width();
            if (widthSideBar == 78 || widthSideBar == 230) {
                larghezzaPaginaIntera = larghezzaPaginaIntera - (widthSideBar - 50);
            };
        } else {
            larghezzaPaginaIntera = larghezzaPaginaIntera + 50;
        };
    };
    var larghezzaDivInterni = larghezzaPaginaIntera - 20;
    if (document.getElementById('divMenu')) {
        var altezzaMenuPre = altezzaMenu;
        var altezzaMenuDiff = 0;
        var altezzaMenuPulsanti = CalcolaMenuPulsanti($("#divMenu")[0].children, larghezzaDivInterni);
        if (altezzaMenuPulsanti > 0) {
            var i = 0;
            for (i = 0; i < altezzaMenuPulsanti; i++) {
                altezzaMenu = altezzaMenu + 20;
            };
            altezzaMenuDiff = altezzaMenu - altezzaMenuPre;
            if (altezzaMenuDiff > 0) {
                altezzaContenuto = altezzaContenuto - altezzaMenuDiff;
            };
        };
    };
    $("#divTop").height(altezzaTop);
    $("#divTop").width(larghezzaPaginaIntera);
    $("#divHeader").height(altezzaHeader);
    $("#divHeader").width(larghezzaPaginaIntera);
    $("#divBody").width(larghezzaPaginaIntera);
    $("#divTitolo").height(altezzaTitolo);
    $("#divTitolo").width(larghezzaDivInterni);
    $("#divMenu").height(altezzaMenu);
    $("#divMenu").width(larghezzaDivInterni);
    $("#divContenuto").height(altezzaContenuto);
    $("#divContenuto").width(larghezzaDivInterni);
    $("#divFooter").height(altezzaFooter);
    $("#divFooter").width(larghezzaDivInterni);
    //GESTIONE DIMENSIONI MENU
    if (document.getElementById('HFMenuItemScroll')) {
        if (document.getElementById('HFMenuItemScroll').value == '1') {
            if (document.getElementById('MasterPage_NavigationMenu')) {
                var larghezzaMenuModulo = larghezzaPaginaIntera - 70;
                document.getElementById('divMenuPrincipale').style.width = larghezzaMenuModulo + 'px';
                document.getElementById('MasterPage_NavigationMenu').style.width = larghezzaMenuModulo + 'px';
                document.getElementById('MasterPage_NavigationMenu').children[0].style.width = larghezzaMenuModulo + 'px';
            };
        };
    };
    //GESTIONE DIMENSIONI MENU
    var altezzaTab = 0;
    if (document.getElementById('HFTab')) {
        if (document.getElementById('HFTabWidth')) {
            if (document.getElementById('HFTabWidth').value != '-0') {
                var tab = $find(document.getElementById('HFTab').value);
                if (tab) {
                    tab.get_element().style.width = (larghezzaDivInterni - document.getElementById('HFTabWidth').value) + "px";
                };
            };
        };
        if (document.getElementById('HFMultiPage')) {
            if (document.getElementById('HFHeightMultiPage')) {
                altezzaTab = altezzaContenuto - document.getElementById('HFHeightMultiPage').value;
            } else {
                altezzaTab = altezzaContenuto / 2;
            };
            if (altezzaTab < 150) {
                altezzaTab = 150;
            };
            var multiPage = $find(document.getElementById('HFMultiPage').value);
            if (multiPage) {
                multiPage.get_element().style.height = altezzaTab + "px";
                if (document.getElementById('HFWidthMultiPage')) {
                    multiPage.get_element().style.width = (larghezzaDivInterni - document.getElementById('HFWidthMultiPage').value) + "px";
                };
            };
            if (document.getElementById('HFElencoGriglieMultiPage')) {
                if (document.getElementById('HFWidthElencoGriglieMultiPage')) {
                    setDimensioneElencoGriglieMultiPageWidth((larghezzaDivInterni - 30), altezzaTab);
                } else {
                    setDimensioneElencoGriglieMultiPage((larghezzaDivInterni - 30), altezzaTab);
                };
            };
        };
    } else {
        if (document.getElementById('HFTabMulti')) {
            if (document.getElementById('HFMultiPageMulti')) {
                var altezzaTabMulti = altezzaTab;
                if (document.getElementById('HFHeightMultiPageMulti')) {
                    var elencoMultiPage = (document.getElementById('HFMultiPageMulti').value).split(',');
                    var riduzioneMultiPage = (document.getElementById('HFHeightMultiPageMulti').value).split(',');
                    for (i = 0; i < elencoMultiPage.length; i++) {
                        var nomeMultiPage = elencoMultiPage[i];
                        altezzaTab = altezzaContenuto - riduzioneMultiPage[i];
                        if (altezzaTab < 150) {
                            altezzaTab = 150;
                        };
                        if (i == 0) {
                            altezzaTabMulti = altezzaTab;
                        } else {
                            if (altezzaTab > altezzaTabMulti) {
                                altezzaTabMulti = altezzaTab;
                            };
                        };
                        var multiPage = $find(nomeMultiPage);
                        multiPage.get_element().style.height = altezzaTab + "px";
                    };
                } else {
                    altezzaTab = altezzaContenuto / 2;
                    if (altezzaTab < 150) {
                        altezzaTab = 150;
                    };
                    altezzaTabMulti = altezzaTab;
                    var elencoMultiPage = (document.getElementById('HFMultiPageMulti').value).split(',');
                    for (i = 0; i < elencoMultiPage.length; i++) {
                        var nomeMultiPage = elencoMultiPage[i];
                        var multiPage = $find(nomeMultiPage);
                        multiPage.get_element().style.height = altezzaTab + "px";
                    };
                };
                if (document.getElementById('HFElencoGriglieMultiPage')) {
                    if (document.getElementById('HFWidthElencoGriglieMultiPage')) {
                        setDimensioneElencoGriglieMultiPageWidth((larghezzaDivInterni - 30), altezzaTabMulti);
                    } else {
                        setDimensioneElencoGriglieMultiPage((larghezzaDivInterni - 30), altezzaTabMulti);
                    };
                };
            };
        } else {
            if (document.getElementById('RadMultiPage1')) {
                if (document.getElementById('HFHeightTab')) {
                    altezzaTab = altezzaContenuto - document.getElementById('HFHeightTab').value;
                } else {
                    altezzaTab = altezzaContenuto / 2;
                };
                if (altezzaTab < 150) {
                    altezzaTab = 150;
                };
                var multiPage = $find("RadMultiPage1");
                multiPage.get_element().style.height = altezzaTab + "px";
            };
        };
    };
    if (document.getElementById('yPosFiliali')) {
        document.getElementById('divContenuto').scrollTop = document.getElementById('yPosFiliali').value;
    };
    if (document.getElementById('HFElencoGriglie')) {
        if (document.getElementById('HFElencoGriglieNoPager')) {
            setDimensioneElencoGriglieNoPager(larghezzaPaginaIntera);
        } else {
            setDimensioneElencoGriglieTab(altezzaContenuto, larghezzaPaginaIntera);
        };
    } else {
        if (document.getElementById('HFGriglia')) {
            setDimensioneGrigliaRicerca(altezzaContenuto, larghezzaPaginaIntera);
        };
    };
};
function setDimensioneGrigliaRicerca(altezzaPagina, larghezzaPagina) {
    var riduzioneAltezza = 150;
    var riduzioneAltezzaAccordian = 0;
    if (document.getElementById('HFAccordian')) {
        $('#' + document.getElementById('HFAccordian').value).width(larghezzaPagina - 28);
        riduzioneAltezzaAccordian = $('#' + document.getElementById('HFAccordian').value).height()
    };
    if (document.getElementById('HFHeightGriglia')) {
        riduzioneAltezza = document.getElementById('HFHeightGriglia').value;
    };
    var altezzaGriglia = altezzaPagina - riduzioneAltezza - riduzioneAltezzaAccordian;
    if (document.getElementById('HFGriglia')) {
        //GESTIONE SCROLL GRIGLIA
        var riduzioneWidthGriglia = 30;
        if (document.getElementById('HFWidthGriglia')) {
            riduzioneWidthGriglia = riduzioneWidthGriglia + parseInt(document.getElementById('HFWidthGriglia').value);
        };
        $('#' + document.getElementById('HFGriglia').value).width(larghezzaPagina - riduzioneWidthGriglia);
        var scrollArea = document.getElementById(document.getElementById('HFGriglia').value + "_GridData");
        if (scrollArea) {
            var altezzaTopPager = $('#' + document.getElementById('HFGriglia').value + "_ctl00_TopPager").height(); //Default: 50
            var altezzaPager = $('#' + document.getElementById('HFGriglia').value + "_ctl00_Pager").height(); //Default: 42
            if (altezzaTopPager != 50 && altezzaTopPager > 0) {
                altezzaGriglia = altezzaGriglia - (altezzaTopPager - 50);
            };
            if (altezzaPager != 42 && altezzaPager > 0) {
                altezzaGriglia = altezzaGriglia - (altezzaPager - 42);
            };
            scrollArea.style.height = altezzaGriglia + 'px';
        };
    };
};
function setDimensioneElencoGriglieTab(altezzaContenuto, larghezzaPagina) {
    var riduzioneAltezza = 150;
    var riduzioneAltezzaAccordian = 0;
    if (document.getElementById('HFAccordian')) {
        $('#' + document.getElementById('HFAccordian').value).width(larghezzaPagina - 28);
        riduzioneAltezzaAccordian = $('#' + document.getElementById('HFAccordian').value).height()
    };
    var griglie = (document.getElementById('HFElencoGriglie').value).split(',');
    var riduzioneAltezzaGriglie = (document.getElementById('HFHeightElencoGriglie').value).split(',');
    var riduzioneLarghezzaGriglie = '';
    var riduzioneLarghezzaGriglieOp = false;
    if (document.getElementById('HFWidthElencoGriglie')) {
        riduzioneLarghezzaGriglie = (document.getElementById('HFWidthElencoGriglie').value).split(',');
        riduzioneLarghezzaGriglieOp = true;
    };
    for (i = 0; i < griglie.length; i++) {
        var nomeGriglia = griglie[i];
        var riduzioneAltezzaGriglia = riduzioneAltezzaGriglie[i];
        var altezzaGriglia = altezzaContenuto - riduzioneAltezzaGriglia - riduzioneAltezzaAccordian;
        if (document.getElementById(nomeGriglia)) {
            //GESTIONE SCROLL GRIGLIA
            if (riduzioneLarghezzaGriglieOp == false) {
                $('#' + nomeGriglia).width(larghezzaPagina - 30);
            } else {
                var riduzioneLarghezzaGriglia = riduzioneLarghezzaGriglie[i];
                var riduzioneTotaleLarghezzaGriglia = (parseInt(riduzioneLarghezzaGriglia) + 30);
                $('#' + nomeGriglia).width(larghezzaPagina - riduzioneTotaleLarghezzaGriglia);
            };
            var scrollArea = document.getElementById(nomeGriglia + "_GridData");
            if (scrollArea) {
                var altezzaTopPager = $('#' + nomeGriglia + "_ctl00_TopPager").height(); //Default: 50
                var altezzaPager = $('#' + nomeGriglia + "_ctl00_Pager").height(); //Default: 42
                if (altezzaTopPager != 50 && altezzaTopPager > 0) {
                    altezzaGriglia = altezzaGriglia - (altezzaTopPager - 50);
                };
                if (altezzaPager != 42 && altezzaPager > 0) {
                    altezzaGriglia = altezzaGriglia - (altezzaPager - 42);
                };
                scrollArea.style.height = altezzaGriglia + 'px';
            };
        };
    };
};
function setDimensioneElencoGriglieNoPager(larghezzaPaginaIntera) {
    var contenuto = $("#divContenuto").height();
    var griglie = (document.getElementById('HFElencoGriglie').value).split(',');
    var riduzioneAltezzaGriglie = (document.getElementById('HFHeightElencoGriglie').value).split(',');
    for (i = 0; i < griglie.length; i++) {
        var nomeGriglia = griglie[i];
        var riduzioneAltezzaGriglia = riduzioneAltezzaGriglie[i];
        var altezzaGriglia = contenuto - riduzioneAltezzaGriglia;
        if (document.getElementById(nomeGriglia)) {
            //GESTIONE SCROLL GRIGLIA
            var scrollArea = document.getElementById(nomeGriglia + "_GridData");
            if (scrollArea) {
                var altezzaTopPager = $('#' + nomeGriglia + "_ctl00_TopPager").height(); //Default: 50
                var altezzaPager = $('#' + nomeGriglia + "_ctl00_Pager").height(); //Default: 42
                scrollArea.style.height = altezzaGriglia + 'px';
            };
        };
    };
};
function setDimensioneElencoGriglieMultiPage(larghezzaPaginaIntera, altezzaTab) {
    var griglie = (document.getElementById('HFElencoGriglieMultiPage').value).split(',');
    var riduzioneAltezzaGriglie = (document.getElementById('HFHeightElencoGriglieMultiPage').value).split(',');
    for (i = 0; i < griglie.length; i++) {
        var nomeGriglia = griglie[i];
        $('#' + nomeGriglia).width(larghezzaPaginaIntera);
        var riduzioneAltezzaGriglia = riduzioneAltezzaGriglie[i];
        var altezzaGriglia = altezzaTab - riduzioneAltezzaGriglia;
        if (document.getElementById(nomeGriglia)) {
            //GESTIONE SCROLL GRIGLIA
            var scrollArea = document.getElementById(nomeGriglia + "_GridData");
            if (scrollArea) {
                var altezzaTopPager = $('#' + nomeGriglia + "_ctl00_TopPager").height(); //Default: 48
                var altezzaPager = $('#' + nomeGriglia + "_ctl00_Pager").height(); //Default: 42
                if (altezzaTopPager != 48 && altezzaTopPager > 0) {
                    altezzaGriglia = altezzaGriglia - (altezzaTopPager - 48);
                };
                if (altezzaPager != 42 && altezzaPager > 0) {
                    altezzaGriglia = altezzaGriglia - (altezzaPager - 42);
                };
                scrollArea.style.height = altezzaGriglia + 'px';
            };
        };
    };
};
function setDimensioneElencoGriglieMultiPageWidth(larghezzaPaginaIntera, altezzaTab) {
    var griglie = (document.getElementById('HFElencoGriglieMultiPage').value).split(',');
    var riduzioneAltezzaGriglie = (document.getElementById('HFHeightElencoGriglieMultiPage').value).split(',');
    var riduzioneLarghezzaGriglie = (document.getElementById('HFWidthElencoGriglieMultiPage').value).split(',');
    for (i = 0; i < griglie.length; i++) {
        var nomeGriglia = griglie[i];
        if (riduzioneLarghezzaGriglie[i] != '-0') {
            var larghezzaGriglia = larghezzaPaginaIntera - riduzioneLarghezzaGriglie[i];
            $('#' + nomeGriglia).width(larghezzaGriglia);
        };
        var riduzioneAltezzaGriglia = riduzioneAltezzaGriglie[i];
        var altezzaGriglia = altezzaTab - riduzioneAltezzaGriglia;
        if (document.getElementById(nomeGriglia)) {
            //GESTIONE SCROLL GRIGLIA
            var scrollArea = document.getElementById(nomeGriglia + "_GridData");
            if (scrollArea) {
                var altezzaTopPager = $('#' + nomeGriglia + "_ctl00_TopPager").height(); //Default: 48
                var altezzaPager = $('#' + nomeGriglia + "_ctl00_Pager").height(); //Default: 42
                if (altezzaTopPager != 48 && altezzaTopPager > 0) {
                    altezzaGriglia = altezzaGriglia - (altezzaTopPager - 48);
                };
                if (altezzaPager != 42 && altezzaPager > 0) {
                    altezzaGriglia = altezzaGriglia - (altezzaPager - 42);
                };
                scrollArea.style.height = altezzaGriglia + 'px';
            };
        };
    };
};
function getOffset(el) {
    var _x = 0;
    var _y = 0;
    while (el && !isNaN(el.offsetLeft) && !isNaN(el.offsetTop)) {
        _x += el.offsetLeft - el.scrollLeft;
        _y += el.offsetTop - el.scrollTop;
        el = el.offsetParent;
    }
    return { top: _y, left: _x };
};
function CalcolaMenuPulsanti(obj, larghezzaTotDiv) {
    var i;
    var moltiplicatoreMenu = 0;
    var larghezzaTotalePulsanti = 1;
    if (obj.length > 1) {
        for (i = 0; i < obj.length; i++) {
            var objSingle = obj[i];
            larghezzaTotalePulsanti = larghezzaTotalePulsanti + objSingle.clientWidth + 2;
        };
        if (larghezzaTotalePulsanti > larghezzaTotDiv) {
            var totLarghezzaDett = 0;
            for (i = 0; i < obj.length; i++) {
                var objSingle = obj[i];
                totLarghezzaDett = totLarghezzaDett + objSingle.clientWidth;
                if (totLarghezzaDett > larghezzaTotDiv) {
                    moltiplicatoreMenu = moltiplicatoreMenu + 1;
                    totLarghezzaDett = objSingle.clientWidth;
                };
            };
        };
    };
    return moltiplicatoreMenu;
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