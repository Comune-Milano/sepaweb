var validNavigation = false
$(document).ready(function () {
    CloseFunction();
});
var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_endRequest(function () {
    CloseFunction();
});
function CloseFunction() {
    window.onbeforeunload = function () {
        if (!validNavigation) {
            validNavigation = false;
            return 'ATTENZIONE!\nPer evitare il blocco temporaneo di alcuni dati chiudere la finestra utilizzato il pulsante Esci.';
        };
    };
};
function ConfermaEsci() {
    if (document.getElementById('frmModify')) {
        if (document.getElementById('frmModify').value != '0') {
            var chiediConferma;
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche!Proseguire l\'uscita senza salvare?");
            if (chiediConferma == true) {
                document.getElementById('frmModify').value = '0';
            };
        };
    };
};