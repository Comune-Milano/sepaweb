$(document).ready(function () {
    // al caricamento della pagina (document ready) eseguo gli script
    var altezzaPaginaIntera = $(window).height() - 5;
    var larghezzaPaginaIntera = $(window).width();
    var percdivisione = 5;
    if (document.getElementById('HfContenteDivWidth')) {
        percdivisione = document.getElementById('HfContenteDivWidth').value;
    };
    var divisione = (parseInt(larghezzaPaginaIntera) / 100) * percdivisione;
    var larghezzaPaginaContenuto = larghezzaPaginaIntera - divisione;
    var altezzaFooter = 25;
    var altezzaTitolo = 45;
    var altezzaMenu = 32;
    var altezzaContenuto = 0;
    if ($.browser.chrome) {
        altezzaContenuto = altezzaPaginaIntera - altezzaFooter - altezzaTitolo - altezzaMenu - 3;
    } else if ($.browser.mozilla) {
        altezzaContenuto = altezzaPaginaIntera - altezzaFooter - altezzaTitolo - altezzaMenu - 15;
    } else if ($.browser.msie) {
        altezzaContenuto = altezzaPaginaIntera - altezzaFooter - altezzaTitolo - altezzaMenu - 20;
    };
    $("#divGenerale").height(altezzaPaginaIntera);
    $("#divGenerale").width(larghezzaPaginaIntera);
    $("#divTitolo").height(altezzaTitolo);
    $("#divTitolo").width(larghezzaPaginaIntera);
    $("#divMenu").height(altezzaMenu);
    $("#divMenu").width(larghezzaPaginaIntera);
    $("#divBody").height(altezzaContenuto);
    $("#divBody").width(larghezzaPaginaIntera);
    $("#divFooter").height(altezzaFooter);
    $("#divFooter").width(larghezzaPaginaIntera);
    var altezzaDivContent = 0;
    if (document.getElementById('HfContenteDivHeight')) {
        altezzaDivContent = altezzaContenuto - document.getElementById('HfContenteDivHeight').value;
    } else {
        altezzaDivContent = altezzaContenuto - 30;
    };
    $("#divOverContent").width(larghezzaPaginaContenuto);
    $("#divOverContent").height(altezzaDivContent);
    var larghezzaPaginaContenutoRisultati = larghezzaPaginaContenuto - 25;
    $("#divOverContentRisultati").width(larghezzaPaginaContenutoRisultati);
    $("#divOverContentRisultati").height(altezzaDivContent);
    $("#divOverContentRisultatiOn").width(larghezzaPaginaContenuto);
    $("#divOverContentRisultatiOn").height(altezzaDivContent);
    if (document.getElementById('HfContenteHeight')) {
        var altezzaTabs = altezzaContenuto - document.getElementById('HfContenteHeight').value;
        $("#tabs").height(altezzaTabs);
        var altezzaContentTabs = 0;
        if (document.getElementById('HfContenteHeightTabs')) {
            altezzaContentTabs = altezzaTabs - document.getElementById('HfContenteHeightTabs').value;
        } else {
            altezzaContentTabs = altezzaTabs - 60;
        };
        var NTabs = $("div[id^='tabs']").length - 1;
        for (var i = 1; i <= NTabs; i++) {
            $("#divOverContentTabs_" + i).height(altezzaContentTabs);
        };
    };
    /*GESTIONE DIAL TRASPARENT */
    var larghezzadivDialOpen = $('.divDialOpen').width();
    var altezzadivDialOpen = $('.divDialOpen').height();
    if ((altezzadivDialOpen == 0) || (altezzadivDialOpen > altezzaPaginaIntera)) {
        altezzadivDialOpen = 600;
        $('.divDialOpen').height(altezzadivDialOpen);
    };
    var larghezzaDiv = $('.dialCTransparent').width();
    var altezzaDiv = $('.dialCTransparent').height();
    var left = larghezzaDiv / 2;
    var top = altezzaDiv / 2;
    $('.dialCTransparent').css({
        'position': 'absolute',
        'left': '50%',
        'top': '50%',
        'margin-left': -left,
        'margin-top': -top
    });
    left = larghezzadivDialOpen / 2;
    top = altezzadivDialOpen / 2;
    $('.dialCTransparentRisultati').css({
        'position': 'absolute',
        'left': '50%',
        'top': '50%',
        'margin-left': -left,
        'margin-top': -top
    });
    /*GESTIONE DIAL TRASPARENT */
});
$(window).resize(function () {
    // ridimensiono il div anche al cambiamento della window
    var altezzaPaginaIntera = $(window).height() - 5;
    var larghezzaPaginaIntera = $(window).width();
    var percdivisione = 5;
    if (document.getElementById('HfContenteDivWidth')) {
        percdivisione = document.getElementById('HfContenteDivWidth').value;
    };
    var divisione = (parseInt(larghezzaPaginaIntera) / 100) * percdivisione;
    var larghezzaPaginaContenuto = larghezzaPaginaIntera - divisione;
    var altezzaFooter = 25;
    var altezzaTitolo = 45;
    var altezzaMenu = 32;
    var altezzaContenuto = 0;
    if ($.browser.chrome) {
        altezzaContenuto = altezzaPaginaIntera - altezzaFooter - altezzaTitolo - altezzaMenu - 3;
    } else if ($.browser.mozilla) {
        altezzaContenuto = altezzaPaginaIntera - altezzaFooter - altezzaTitolo - altezzaMenu - 15;
    } else if ($.browser.msie) {
        altezzaContenuto = altezzaPaginaIntera - altezzaFooter - altezzaTitolo - altezzaMenu - 20;
    };
    $("#divGenerale").height(altezzaPaginaIntera);
    $("#divGenerale").width(larghezzaPaginaIntera);
    $("#divTitolo").height(altezzaTitolo);
    $("#divTitolo").width(larghezzaPaginaIntera);
    $("#divMenu").height(altezzaMenu);
    $("#divMenu").width(larghezzaPaginaIntera);
    $("#divBody").height(altezzaContenuto);
    $("#divBody").width(larghezzaPaginaIntera);
    $("#divFooter").height(altezzaFooter);
    $("#divFooter").width(larghezzaPaginaIntera);
    var altezzaDivContent = 0;
    if (document.getElementById('HfContenteDivHeight')) {
        altezzaDivContent = altezzaContenuto - document.getElementById('HfContenteDivHeight').value;
    } else {
        altezzaDivContent = altezzaContenuto - 30;
    };
    $("#divOverContent").width(larghezzaPaginaContenuto);
    $("#divOverContent").height(altezzaDivContent);
    var larghezzaPaginaContenutoRisultati = larghezzaPaginaContenuto - 25;
    $("#divOverContentRisultati").width(larghezzaPaginaContenutoRisultati);
    $("#divOverContentRisultati").height(altezzaDivContent);
    $("#divOverContentRisultatiOn").width(larghezzaPaginaContenuto);
    $("#divOverContentRisultatiOn").height(altezzaDivContent);
    if (document.getElementById('HfContenteHeight')) {
        var altezzaTabs = altezzaContenuto - document.getElementById('HfContenteHeight').value;
        $("#tabs").height(altezzaTabs);
        var altezzaContentTabs = 0;
        if (document.getElementById('HfContenteHeightTabs')) {
            altezzaContentTabs = altezzaTabs - document.getElementById('HfContenteHeightTabs').value;
        } else {
            altezzaContentTabs = altezzaTabs - 60;
        };
        var NTabs = $("div[id^='tabs']").length - 1;
        for (var i = 1; i <= NTabs; i++) {
            $("#divOverContentTabs_" + i).height(altezzaContentTabs);
        };
    };
    /*GESTIONE DIAL TRASPARENT */
    var larghezzadivDialOpen = $('.divDialOpen').width();
    var altezzadivDialOpen = $('.divDialOpen').height();
    if ((altezzadivDialOpen == 0) || (altezzadivDialOpen > altezzaPaginaIntera)) {
        altezzadivDialOpen = 600;
        $('.divDialOpen').height(altezzadivDialOpen);
    };
    var larghezzaDiv = $('.dialCTransparent').width();
    var altezzaDiv = $('.dialCTransparent').height();
    var left = larghezzaDiv / 2;
    var top = altezzaDiv / 2;
    $('.dialCTransparent').css({
        'position': 'absolute',
        'left': '50%',
        'top': '50%',
        'margin-left': -left,
        'margin-top': -top
    });
    left = larghezzadivDialOpen / 2;
    top = altezzadivDialOpen / 2;
    $('.dialCTransparentRisultati').css({
        'position': 'absolute',
        'left': '50%',
        'top': '50%',
        'margin-left': -left,
        'margin-top': -top
    });
    /*GESTIONE DIAL TRASPARENT */
});