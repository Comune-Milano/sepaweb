window.onresize = setDimensioni;
$(document).ready(setDimensioni);
function setDimensioni() {
    var altezzaPaginaIntera = $(window).height() - 20;
    var larghezzaPaginaIntera = $(window).width() - 2;
    $("#RestrictionZoneID").height($(window).height());
    $("#RestrictionZoneID").width($(window).width());
    var altezzaTop = 0;
    if (document.getElementById('top')) {
        altezzaTop = 60;
    };
    var altezzaHeader = 0;
    if (document.getElementById('header')) {
        altezzaHeader = 40;
    };
    var altezzaFooter = 35;
    var altezzaBody = altezzaPaginaIntera - altezzaTop - altezzaHeader - altezzaFooter;
    var altezzaTitolo = 28;
    if (!document.getElementById('top')) {
        altezzaTitolo = altezzaTitolo + 20;
    };
    var altezzaMenu = 35;
    var altezzaContenuto = altezzaBody - altezzaTitolo - altezzaMenu;
    var larghezzaDivInterni = larghezzaPaginaIntera - 20;
    $("#top").height(altezzaTop);
    $("#top").width(larghezzaPaginaIntera);
    $("#header").height(altezzaHeader);
    $("#header").width(larghezzaPaginaIntera);
    $("#body").width(larghezzaPaginaIntera);
    $("#titolo").height(altezzaTitolo);
    $("#titolo").width(larghezzaDivInterni);
    $("#menu").height(altezzaMenu);
    $("#menu").width(larghezzaDivInterni);
    $("#contenuto").height(altezzaContenuto);
    $("#contenuto").width(larghezzaDivInterni);
    $("#footer").height(altezzaFooter);
    $("#footer").width(larghezzaDivInterni);
    var altezzaTab = 0;
    if (document.getElementById('RadMultiPage1')) {
        if (document.getElementById('HFHeightTab')) {
            altezzaTab = altezzaContenuto - document.getElementById('HFHeightTab').value;
        } else {
            altezzaTab = altezzaContenuto / 2;
        };
        var multiPage = $find("RadMultiPage1");
        multiPage.get_element().style.height = altezzaTab + "px";
    };
    if (document.getElementById('yPosFiliali')) {
        document.getElementById('contenuto').scrollTop = document.getElementById('yPosFiliali').value;
    };
    if (document.getElementById('HFGriglia')) {
        setDimensioneGrigliaRicerca(altezzaContenuto, larghezzaPaginaIntera);
    };
    if (document.getElementById('HFElencoGriglie')) {
        setDimensioneElencoGriglie(larghezzaPaginaIntera);
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
        $('#' + document.getElementById('HFGriglia').value).width(larghezzaPagina - 30);
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

function setDimensioneElencoGriglie(larghezzaPaginaIntera) {
    var contenuto = $("#contenuto").height();
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

                if ((altezzaTopPager == null) || (altezzaTopPager == 0)) {
                    altezzaTopPager = 50;
                };
                if ((altezzaPager == null) || (altezzaPager == 0)) {
                    altezzaPager = 42;
                };
                if (altezzaTopPager != 50) {
                    altezzaGriglia = altezzaGriglia - (altezzaTopPager - 50);
                };
                if (altezzaPager != 42) {
                    altezzaGriglia = altezzaGriglia - (altezzaPager - 42);
                };

                scrollArea.style.height = altezzaGriglia + 'px';
            };
        };
    };
};