$(document).ready(function () {
    var larghezzaPaginaIntera = $(window).width();
    var larghezzatabs = larghezzaPaginaIntera - 20;
    var larghezzaDiv = larghezzaPaginaIntera - 72;
    $("#tabs").width(larghezzatabs);
    $("#divFabbricato").width(larghezzaDiv);
    $("#divAlloggio").width(larghezzaDiv);
    $("#divInquilino").width(larghezzaDiv);
    $("#divProgrammazione").width(larghezzaDiv);
});
$(window).resize(function () {
    var larghezzaPaginaIntera = $(window).width();
    var larghezzatabs = larghezzaPaginaIntera - 20;
    var larghezzaDiv = larghezzaPaginaIntera - 72;
    $("#tabs").width(larghezzatabs);
    $("#divFabbricato").width(larghezzaDiv);
    $("#divAlloggio").width(larghezzaDiv);
    $("#divInquilino").width(larghezzaDiv);
    $("#divProgrammazione").width(larghezzaDiv);
});