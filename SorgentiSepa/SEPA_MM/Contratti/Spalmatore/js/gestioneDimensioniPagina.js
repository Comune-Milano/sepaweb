window.onresize = AggiornaDimensioni;
Sys.Application.add_load(AggiornaDimensioni);

function AggiornaDimensioni() {

    var altezzaPaginaIntera = $(window).height() - 5;
    var larghezzaPaginaIntera = $(window).width();
    var altezzaHeader = 35;
    var altezzaFooter = 20;
    var altezzaTitolo = 40;
    var altezzaMenu = 35;
    var altezzaContenuto = altezzaPaginaIntera - altezzaTitolo - altezzaFooter - altezzaHeader - altezzaMenu - 20;
    $("#divGenerale").height(altezzaPaginaIntera);
    $("#divGenerale").width(larghezzaPaginaIntera);
    $("#divHeader").height(altezzaHeader);
    $("#divHeader").width(larghezzaPaginaIntera);
    $("#divTitolo").height(altezzaTitolo);
    $("#divTitolo").width(larghezzaPaginaIntera);
    $("#divMenu").height(altezzaMenu);
    $("#divMenu").width(larghezzaPaginaIntera);
    $("#divContenuto").height(altezzaContenuto);
    $("#divContenuto").width(larghezzaPaginaIntera);
    if (document.getElementById('yPosRadgrid')) {
        if (document.getElementById('divContenuto') != null) { document.getElementById('divContenuto').scrollTop = document.getElementById('yPosRadgrid').value; }
    }
    document.getElementById("AltezzaContenuto").value = altezzaContenuto;
    $("#divFooter").height(altezzaFooter);
    $("#divFooter").width(larghezzaPaginaIntera);
    var altezzaDivGC = altezzaContenuto - 100;
    $("#divOverContent").height(altezzaDivGC);
    var altezzaDivGC2 = 0;
    if (document.getElementById('altezzaDivOverContent') != null) {
        altezzaDivGC2 = altezzaDivGC - document.getElementById('altezzaDivOverContent').value;
    } else {
        altezzaDivGC2 = altezzaDivGC - 50;
    };
    $("#divOverContent2").height(altezzaDivGC2);
    $("#divOverContentC1").height(altezzaDivGC / 2 - 100);
    $("#divOverContentC2").height(altezzaDivGC / 2 - 100);
    //GESTIONE DELLE DIMENSIONE DELLA PAGINA HOME
    var altezzatrDiv1 = (altezzaDivGC / 100) * 60 - 5;
    var altezzatrDiv2 = (altezzaDivGC / 100) * 40 - 10;
    $("#trDiv1").height(altezzatrDiv1);
    $("#tdtrDiv1").height(altezzatrDiv1);
    $("#trDiv2").height(altezzatrDiv2);
};
