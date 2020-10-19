var validNavigation = false;

$(document).ready(function () {
    //    alert('write up events');
    wireUpEvents();
});
function endSession() {
    __doPostBack('ControlloChiusura', 'Chiusura');
}

function wireUpEvents() {
    // Attach the event keypress to exclude the F5 refresh
    $('html').bind('keypress', function (e) {
        if (e.keyCode == 116) {
            validNavigation = true;
        }
    });
    // Attach the event click for all links in the page
    $("#btnSalva").bind("click", function () {
        validNavigation = true;
    });
    $("#btnEsci").bind("click", function () {
        validNavigation = true;
    });
    $("#T1").bind("click", function () {
        validNavigation = true;
    });
    $("#btnAllegati").bind("click", function () {
        validNavigation = true;
    });
    $("#ContentPlaceHolder1_menudomanda").bind("click", function () {
        validNavigation = true;
    });
    $("#ContentPlaceHolder1_menudomandan1").bind("click", function () {
        validNavigation = true;
    });
    $("#ContentPlaceHolder1_menudomandan2").bind("click", function () {
        validNavigation = true;
    });
    // End Attach the event click for all links in the page
    // Attach the event submit for all forms in the page
    $("form").bind("submit", function () {
        validNavigation = true;
    });
    // Attach the event click for all inputs in the page
    $("input[type=submit]").bind("click", function () {
        validNavigation = true;
    });
//    $("form :input").bind("change", function (e) {
//        validNavigation = true;
//    });
    $(document).keydown(function (e) {
        if (e.keyCode == 116) { validNavigation = true; }
    });
    window.onbeforeunload = function () {
        if (!validNavigation) {
            validNavigation = false
            return 'E\' preferibile fare click prima sul pulsante esci!';
        }
    }
}
function wireUpEvents2() {
    var validNavigation = false;
    // Attach the event keypress to exclude the F5 refresh
    $('html').bind('keypress', function (e) {
        if (e.keyCode == 116) {
            validNavigation = true;
        }
    });
    // Attach the event click for all links in the page
    $("#btnSalva").bind("click", function () {
        validNavigation = true;
    });
    $("#btnEsci").bind("click", function () {
        validNavigation = true;
    });
    $("#T1").bind("click", function () {
        validNavigation = true;
    });
    $("#btnAllegati").bind("click", function () {
        validNavigation = true;
    });
    $("#ContentPlaceHolder1_menudomanda").bind("click", function () {
        validNavigation = true;
    });
    $("#ContentPlaceHolder1_menudomandan1").bind("click", function () {
        validNavigation = true;
    });
    $("#ContentPlaceHolder1_menudomandan2").bind("click", function () {
        validNavigation = true;
    });
    // End Attach the event click for all links in the page
    // Attach the event submit for all forms in the page
    $("form").bind("submit", function () {
        validNavigation = true;
    });
    // Attach the event click for all inputs in the page
    $("input[type=submit]").bind("click", function () {
        validNavigation = true;
    });
//    $("form :input").bind("change", function (e) {
//        validNavigation = true;
//    });
    $(document).keydown(function (e) {
        if (e.keyCode == 116) { validNavigation = true; }
    });
    window.onbeforeunload = function () {
        if (!validNavigation) {
            validNavigation = false
            return 'E\' preferibile fare click prima sul pulsante esci!';
        }
    }
}
/* inizio codice per il click sul chiudi*/
window.onunload = Exit;
function Exit() {
    if (!validNavigation) {
        if (document.getElementById('btnEsci') != null) {
            if (opener.document.getElementById('ContentPlaceHolder1_aggiornapunti') != null) {
                opener.document.getElementById('ContentPlaceHolder1_aggiornapunti').value = 1;
                opener.form1.submit();
            }
            document.getElementById('txtModificato').value = 0;
            document.getElementById('btnEsci').click();
        }
        else {
            if (document.getElementById('ContentPlaceHolder1_btnfunzesci') != null) {
                document.getElementById('ContentPlaceHolder1_btnfunzesci').click();
            }
        }
    }
}
/* fine codice per il click sul chiudi*/