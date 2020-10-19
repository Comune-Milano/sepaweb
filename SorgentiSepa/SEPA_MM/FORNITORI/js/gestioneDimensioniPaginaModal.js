$(document).ready(function () {
    // al caricamento della pagina (document ready) eseguo gli script
    var altezzaPaginaIntera = $(window).height() - 5;
    var larghezzaPaginaIntera = $(window).width();
    var altezzaFooter = 40;
    var altezzaTitolo = 35;
    var altezzaContenuto = altezzaPaginaIntera - altezzaTitolo - altezzaFooter - 20;
    $("#divGeneraleModal").height(altezzaPaginaIntera);
    $("#divGeneraleModal").width(larghezzaPaginaIntera);
    $("#divTitolo").height(altezzaTitolo);
    $("#divTitolo").width(larghezzaPaginaIntera);
    $("#divBodyModal").height(altezzaContenuto);
    $("#divBodyModal").width(larghezzaPaginaIntera);
    $("#divFooter").height(altezzaFooter);
    $("#divFooter").width(larghezzaPaginaIntera);
    var altezzaDivGC = altezzaContenuto - 40;
    $("#divOverContent").height(altezzaDivGC);
    var altezzaDivGC2 = 0;
    if (document.getElementById('altezzaDivOverContent') != null) {
        altezzaDivGC2 = altezzaDivGC - document.getElementById('altezzaDivOverContent').value;
    } else {
        altezzaDivGC2 = altezzaDivGC - 50;
    };
    $("#divOverContent2").height(altezzaDivGC2);
});
$(window).resize(function () {
    // ridimensiono il div anche al cambiamento della window
    var altezzaPaginaIntera = $(window).height() - 5;
    var larghezzaPaginaIntera = $(window).width();
    var altezzaFooter = 40;
    var altezzaTitolo = 35;
    var altezzaContenuto = altezzaPaginaIntera - altezzaTitolo - altezzaFooter - 20;
    $("#divGeneraleModal").height(altezzaPaginaIntera);
    $("#divGeneraleModal").width(larghezzaPaginaIntera);
    $("#divTitolo").height(altezzaTitolo);
    $("#divTitolo").width(larghezzaPaginaIntera);
    $("#divBodyModal").height(altezzaContenuto);
    $("#divBodyModal").width(larghezzaPaginaIntera);
    $("#divFooter").height(altezzaFooter);
    $("#divFooter").width(larghezzaPaginaIntera);
    var altezzaDivGC = altezzaContenuto - 40;
    $("#divOverContent").height(altezzaDivGC);
    var altezzaDivGC2 = 0;
    if (document.getElementById('altezzaDivOverContent') != null) {
        altezzaDivGC2 = altezzaDivGC - document.getElementById('altezzaDivOverContent').value;
    } else {
        altezzaDivGC2 = altezzaDivGC - 50;
    };
    $("#divOverContent2").height(altezzaDivGC2);
});