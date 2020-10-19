$(document).ready(function () {
    // al caricamento della pagina (document ready) eseguo gli script
    var altezzaPaginaIntera = $(window).height()-5;
    var larghezzaPaginaIntera = $(window).width()-15;
    var altezzaHeader = 40;
    var altezzaFooter = 60;
    var altezzaTitolo = 30;
    var altezzaContenuto = altezzaPaginaIntera -altezzaTitolo- altezzaFooter - altezzaHeader - 50;
    $("#generale").height(altezzaPaginaIntera);
    $("#header").height(altezzaHeader);
    $("#titolo").height(altezzaTitolo);
    $("#contenuto").height(altezzaContenuto);
    $("#footer").height(altezzaFooter);
    $("#generale").width(larghezzaPaginaIntera);
    $("#header").width(larghezzaPaginaIntera);
    $("#titolo").width(larghezzaPaginaIntera);
    $("#footer").width(larghezzaPaginaIntera);
});
$(window).resize(function () {
    // ridimensiono il div anche al cambiamento della window
    var altezzaPaginaIntera = $(window).height()-5;
    var larghezzaPaginaIntera = $(window).width()-15;
    var altezzaHeader = 40;
    var altezzaFooter = 60;
    var altezzaTitolo = 30;
    var altezzaContenuto = altezzaPaginaIntera - altezzaTitolo - altezzaFooter - altezzaHeader - 50;
    $("#generale").height(altezzaPaginaIntera);
    $("#header").height(altezzaHeader);
    $("#titolo").height(altezzaTitolo);
    $("#contenuto").height(altezzaContenuto);
    $("#footer").height(altezzaFooter);
    $("#generale").width(larghezzaPaginaIntera);
    $("#header").width(larghezzaPaginaIntera);
    $("#titolo").width(larghezzaPaginaIntera);
    $("#footer").width(larghezzaPaginaIntera);
});
