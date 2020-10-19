/* FUNZIONI LOCK PAGE */
var idPageLock;
function getIdPageLock() {
    $.ajax({
        url: '../SepacomLock.svc/getPageLock',
        dataType: "json",
        data: JSON.stringify(''),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        success: onSuccessLock,
        dataFilter: function (data) { return data; },
        error: undefined
    });
};
function onSuccessLock(results, context, methodName) {
    idPageLock = results.d;
    idPageLock = idPageLock.replace('"', '').replace('"', '').replace('null', '');
};
/* FUNZIONI LOCK PAGE */
/* FUNZIONI MENU */
function Ricerca() {
    getIdPageLock();
    loadingMenu();
    CallPageFromMenu('Ricerca.aspx?KEY=' + idPageLock);
};
function RicercaOA() {
    getIdPageLock();
    loadingMenu();
    CallPageFromMenu('RicercaOA.aspx?KEY=' + idPageLock);
};
function Gestione(tipo) {
    getIdPageLock();
    loadingMenu();
    CallPageFromMenu('Gestione.aspx?T=' + tipo + '&KEY=' + idPageLock);
};
function Parametri() {
    getIdPageLock();
    loadingMenu();
    CallPageFromMenu('Parametri.aspx?KEY=' + idPageLock);
};
function ParametriGestore() {
    getIdPageLock();
    loadingMenu();
    CallPageFromMenu('ParametriGestore.aspx?KEY=' + idPageLock);
};
function Log() {
    getIdPageLock();
    loadingMenu();
    CallPageFromMenu('Log.aspx?KEY=' + idPageLock);
};
function Procedure() {
    getIdPageLock();
    loadingMenu();
    CallPageFromMenu('Procedure.aspx?KEY=' + idPageLock);
};
function ApriElaborazione() {
    getIdPageLock();
    loadingMenu();
    CallPageFromMenu('Elaborazione.aspx?ID=' + document.getElementById('HFIdElaborazione').value + '&KEY=' + idPageLock);
};
function ApriElaborazioneOA() {
    getIdPageLock();
    loadingMenu();
    CallPageFromMenu('ElaborazioneOA.aspx?ID=' + document.getElementById('HFIdElaborazione').value + '&KEY=' + idPageLock);
};
/* FUNZIONI MENU */
function closeNuovaElaborazione(sender, args) {
    closeWindow(sender, args, 'MasterHomePage_RadWindowElaborazione');
};
function closeNuovaElaborazioneOA(sender, args) {
    closeWindow(sender, args, 'MasterHomePage_RadWindowElaborazioneOA');
};
function GestioneCodifiche() {
    getIdPageLock();
    var w = $(window).width() - 20;
    var h = $(window).height() - 50;
    openWinUrl('MasterHomePage_CPContenuto_RadWindow1', 'GestioneCodifiche.aspx?T=' + document.getElementById('HFTipoGestione').value + '&KEY=' + idPageLock, w, h);
};
function closeRadWindowGestCodifiche() {
    GetRadWindow().close();
};
function ApriAnomalie(tipo) {
    getIdPageLock();
    CenterPage2('Anomalie.aspx?T=' + tipo + '&KEY=' + idPageLock, 'Anomalie', 1000, 700);
};
function ApriAnomalieF(tipo) {
    getIdPageLock();
    CenterPage2('AnomalieF.aspx?T=' + tipo + '&KEY=' + idPageLock, 'AnomalieF', 1000, 700);
};
function ApriAnomalieUI(id) {
    CenterPage('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=' + id, 'UI' + id, 800, 600)
};
function ApriAnomalieEdifici(id) {
    CenterPage('../CENSIMENTO/InserimentoEdifici.aspx?X=1&SLE=1&ID=' + id, 'ED' + id, 800, 620)
};