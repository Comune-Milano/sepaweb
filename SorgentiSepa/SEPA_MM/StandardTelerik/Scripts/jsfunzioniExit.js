var validNavigation = false;
$(document).ready(function () {
    CloseFunction();
});
var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_endRequest(function () {
    CloseFunction();
});
var messaggioChiusura = '';
var messaggioChiusuraMod = '';
function CloseFunction() {
    window.onbeforeunload = function () {
        if (!validNavigation) {
            if (document.getElementById('HFBlockExit')) {
                validNavigation = false;
                if (document.getElementById('HFBlockExit').value == '0') {
                    return Messaggio.messaggioChiusura;
                } else if (document.getElementById('HFBlockExit').value == '1') {
                    validNavigation = true;
                    document.getElementById('HFBlockExit').value = '0';
                };
            } else {
                validNavigation = false;
                return Messaggio.messaggioChiusura;
            };
        };
    };
};