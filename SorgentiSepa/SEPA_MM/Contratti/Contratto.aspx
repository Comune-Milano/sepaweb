<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Contratto.aspx.vb" Inherits="Contratti_Contratto" %>

<%@ Register Src="Tab_SchemaBollette.ascx" TagName="Tab_SchemaBollette" TagPrefix="uc9" %>
<%@ Register Src="Tab_Bollette_New.ascx" TagName="Tab_Bollette" TagPrefix="uc8" %>
<%@ Register Src="Tab_Comunicazioni.ascx" TagName="Tab_Comunicazioni" TagPrefix="uc7" %>
<%@ Register Src="Tab_Conduttore.ascx" TagName="Tab_Conduttore" TagPrefix="uc6" %>
<%@ Register Src="Tab_Registrazione.ascx" TagName="Tab_Registrazione" TagPrefix="uc5" %>
<%@ Register Src="Tab_Canone.ascx" TagName="Tab_Canone" TagPrefix="uc4" %>
<%@ Register Src="Tab_UnitaImmLocate.ascx" TagName="Tab_UnitaImmLocate" TagPrefix="uc3" %>
<%@ Register Src="Tab_Contratto.ascx" TagName="Tab_Contratto" TagPrefix="uc2" %>
<%@ Register Src="Tab_Generale.ascx" TagName="TabGenerale" TagPrefix="uc1" %>
<%@ Register Src="Tab_Sicurezza.ascx" TagName="Tab_Sicurezza" TagPrefix="uc10" %>
<%@ Register Src="Tab_Azioni_Legali.ascx" TagName="Tab_Azioni_Legali" TagPrefix="uc11" %>
<%@ Register Src="~/Contratti/Tab_OccupazioneAbusiva.ascx" TagPrefix="uc12" TagName="Tab_OccupazioneAbusiva" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '222';
        }
    }


    function $onkeydown() {


        if (event.keyCode == 13) {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '222';
        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="jquery-1.8.2.js"></script>
<script type="text/javascript" src="jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="jquery.corner.js"></script>
<script type="text/javascript" src="common.js"></script>
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
<script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 0;
    var CFGlobale;
    var IDGlobale;
    var Selezionato;


    var OldColor;
    var SelColo;
// -->
</script>
<head runat="server">
    <link rel="stylesheet" type="text/css" href="impromptu.css" />
    <style type="text/css">
        #TIPO1 {
            font: normal 12px Verdana;
        }

        #dropmenudiv {
            position: absolute;
            border: 1px solid black;
            border-bottom-width: 0;
            font: normal 12px Verdana;
            line-height: 18px;
            z-index: 100;
        }

            #dropmenudiv a {
                width: 100%;
                display: block;
                text-indent: 3px;
                border-bottom: 1px solid black;
                padding: 1px 0;
                text-decoration: none;
                font-weight: bold;
            }

                #dropmenudiv a:hover {
                    /*hover background color*/
                    background-color: yellow;
                }

        #form1 {
            width: 900px;
        }
    </style>
    <telerik:RadCodeBlock ID="t" runat="server">
        <script language="javascript" type="text/javascript">
            function MakeStaticHeader(gridId, height, width, headerHeight, isFooter, Tabella) {
                var tbl = document.getElementById(gridId);
                if (tbl) {
                    if (Tabella == 0) {
                        var DivHR = document.getElementById('DivHeaderRow');
                        var DivMC = document.getElementById('DivMainContent');
                        var DivFR = document.getElementById('DivFooterRow');
                    }
                    if (Tabella == 1) {
                        var DivHR = document.getElementById('DivHeaderRow1');
                        var DivMC = document.getElementById('DivMainContent1');
                        var DivFR = document.getElementById('DivFooterRow1');
                    }
                    if (Tabella == 2) {
                        var DivHR = document.getElementById('DivHeaderRow3');
                        var DivMC = document.getElementById('DivMainContent3');
                        var DivFR = document.getElementById('DivFooterRow3');
                    }

                    //*** Set divheaderRow Properties ****
                    DivHR.style.height = headerHeight + 'px';
                    //DivHR.style.left = '3px';
                    DivHR.style.width = (parseInt(width) - 16) + 'px';
                    DivHR.style.position = 'relative';
                    DivHR.style.top = '0px';
                    DivHR.style.zIndex = '2';
                    DivHR.style.verticalAlign = 'top';

                    //*** Set divMainContent Properties ****
                    DivMC.style.width = width + 'px';
                    DivMC.style.height = height + 'px';
                    DivMC.style.position = 'relative';
                    DivMC.style.top = -headerHeight + 'px';
                    DivMC.style.zIndex = '1';

                    //*** Set divFooterRow Properties ****
                    DivFR.style.width = (parseInt(width) - 16) + 'px';
                    DivFR.style.position = 'relative';
                    DivFR.style.top = -headerHeight + 'px';
                    DivFR.style.verticalAlign = 'top';
                    DivFR.style.paddingtop = '2px';

                    if (isFooter) {
                        var tblfr = tbl.cloneNode(true);
                        tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                        var tblBody = document.createElement('tbody');
                        tblfr.style.width = '100%';
                        tblfr.cellSpacing = "0";
                        tblfr.border = "0px";
                        tblfr.rules = "none";
                        //*****In the case of Footer Row *******
                        tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                        tblfr.appendChild(tblBody);
                        DivFR.appendChild(tblfr);
                    }
                    //****
                    DivHR.appendChild(tbl.cloneNode(true));
                }
            }



            function OnScrollDiv(Scrollablediv) {
                document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
            }

            function OnScrollDiv1(Scrollablediv) {
                document.getElementById('DivHeaderRow1').scrollLeft = Scrollablediv.scrollLeft;
                document.getElementById('DivFooterRow1').scrollLeft = Scrollablediv.scrollLeft;
            }
            function OnScrollDiv3(Scrollablediv) {
                document.getElementById('DivHeaderRow3').scrollLeft = Scrollablediv.scrollLeft;
                document.getElementById('DivFooterRow3').scrollLeft = Scrollablediv.scrollLeft;
            }

        </script>
        <script type="text/javascript">





            function PROVA() {
                document.getElementById('USCITA').value = '1';
            }

            //Contents for menu 1
            var menu1 = new Array()
            menu1[0] = '<a href="javascript:Cessione();">Denuncia Cessione Fabbricato</a>'
            menu1[1] = '<a href="javascript:Consegna();">Verbale di Consegna</a>'
            menu1[2] = '<a href="javascript:Riconsegna();">Verbale Ricons. Chiavi/Disdetta/Promemoria</a>'
            //menu1[3] = '<a href="javascript:Ospitalita();">Dichiarazione di Ospitalità</a>'
            //menu1[4] = '<a href="javascript:Disdetta();">Disdetta Immobile</a>'
            //menu1[5] = '<a href="javascript:Consistenza();">Verbale di Consistenza</a>'
            menu1[6] = '<a href="javascript:ChiusuraCont();">Verbale Chiusura Contr./Tassa Rif.</a>'
            //****** 05-07-2012 NUOVI DOCUMENTI ****** 
            //menu1[7] = '<a href="javascript:Privacy();">Normativa Privacy</a>'
            //menu1[8] = '<a href="javascript:TassaRifiuti();">Dichiarazione Per Tassa Rifiuti</a>'
            menu1[9] = '<a href="javascript:ModuloRifiuti();">Modulo Tassa Rifiuti</a>'
            menu1[10] = '<a href="javascript:DichMaggiorenni();">Dichiarazione Maggiorenni</a>'
            //****** 05-07-2012 fine NUOVI DOCUMENTI ****** 

            menu1[11] = '<a href="javascript:Apri();">Visualizza stampe Contratti</a>'
            menu1[12] = '<a href="javascript:AllegatiContratto();">Visualizza Allegati</a>'
            menu1[13] = '<a href="javascript:AllegaFile();">Allega File</a>'
            //menu1[10] = '<a href="javascript:ApriTrasloco();">Trasloco/Magazzinaggio</a>'


            //menu1[14] = '<a href="javascript:ApriPromUtente();">Promemoria Utente</a>'
            menu1[15] = '<a href="javascript:AllegatiContrattoEx();">Visualizza Allegati ex Gestore</a>'
            //Contents for menu 2, and so on
            var menu2 = new Array()
            menu2[0] = '<a href="javascript:ApriRateizzazione();">Rateizzazione</a>'
            menu2[2] = '<a href="javascript:ScegliSpostamAnnull();">Variaz.Decorr./Spostam./Annullam.</a>'

            menu2[3] = '<a href="javascript:TrasferimBoll();">Trasferimento Bollette</a>'
            menu2[4] = '<a href="javascript:AggiornamentoNucleo();">Aggiorna Nucleo</a>'

            var menuwidth = '300px' //default menu width
            var menubgcolor = 'lightyellow'  //menu bgcolor
            var disappeardelay = 250  //menu disappear speed onMouseout (in miliseconds)
            var hidemenu_onclick = "yes" //hide menu when user clicks within menu?

            /////No further editting needed
            var menuAttivazione = new Array()
            menuAttivazione[0] = '<a href="javascript:CreaBollettino1();">Bollettino N.1</a>'
            menuAttivazione[1] = '<a href="javascript:CreaBollettino2();">Bollettino N.2</a>'

            var ie4 = document.all
            var ns6 = document.getElementById && !document.all

            if (ie4 || ns6)
                document.write('<div id="dropmenudiv" onclick="javascript:PROVA();" style="visibility:hidden;width:' + menuwidth + ';background-color:' + menubgcolor + '" onMouseover="clearhidemenu()" onMouseout="dynamichide(event)"></div>')

            function getposOffset(what, offsettype) {
                var totaloffset = (offsettype == "left") ? what.offsetLeft : what.offsetTop;
                var parentEl = what.offsetParent;
                while (parentEl != null) {
                    totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop;
                    parentEl = parentEl.offsetParent;
                }
                return totaloffset;
            }

            function showhide(obj, e, visible, hidden, menuwidth) {
                if (ie4 || ns6)
                    dropmenuobj.style.left = dropmenuobj.style.top = -500
                if (menuwidth != "") {
                    dropmenuobj.widthobj = dropmenuobj.style
                    dropmenuobj.widthobj.width = menuwidth
                }
                if (e.type == "click" && obj.visibility == hidden || e.type == "mouseover")
                    obj.visibility = visible
                else if (e.type == "click")
                    obj.visibility = hidden
            }

            function iecompattest() {
                return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
            }

            function clearbrowseredge(obj, whichedge) {
                var edgeoffset = 0
                if (whichedge == "rightedge") {
                    var windowedge = ie4 && !window.opera ? iecompattest().scrollLeft + iecompattest().clientWidth - 15 : window.pageXOffset + window.innerWidth - 15
                    dropmenuobj.contentmeasure = dropmenuobj.offsetWidth
                    if (windowedge - dropmenuobj.x < dropmenuobj.contentmeasure)
                        edgeoffset = dropmenuobj.contentmeasure - obj.offsetWidth
                }
                else {
                    var topedge = ie4 && !window.opera ? iecompattest().scrollTop : window.pageYOffset
                    var windowedge = ie4 && !window.opera ? iecompattest().scrollTop + iecompattest().clientHeight - 15 : window.pageYOffset + window.innerHeight - 18
                    dropmenuobj.contentmeasure = dropmenuobj.offsetHeight
                    if (windowedge - dropmenuobj.y < dropmenuobj.contentmeasure) { //move up?
                        edgeoffset = dropmenuobj.contentmeasure + obj.offsetHeight
                        if ((dropmenuobj.y - topedge) < dropmenuobj.contentmeasure) //up no good either?
                            edgeoffset = dropmenuobj.y + obj.offsetHeight - topedge
                    }
                }
                return edgeoffset
            }

            function populatemenu(what) {
                if (ie4 || ns6)
                    dropmenuobj.innerHTML = what.join("")
            }

            function dropdownmenu(obj, e, menucontents, menuwidth) {
                if (window.event) event.cancelBubble = true
                else if (e.stopPropagation) e.stopPropagation()
                clearhidemenu()
                dropmenuobj = document.getElementById ? document.getElementById("dropmenudiv") : dropmenudiv
                populatemenu(menucontents)

                if (ie4 || ns6) {
                    showhide(dropmenuobj.style, e, "visible", "hidden", menuwidth)
                    dropmenuobj.x = getposOffset(obj, "left")
                    dropmenuobj.y = getposOffset(obj, "top")
                    dropmenuobj.style.left = dropmenuobj.x - clearbrowseredge(obj, "rightedge") + "px"
                    dropmenuobj.style.top = dropmenuobj.y - clearbrowseredge(obj, "bottomedge") + obj.offsetHeight + "px"
                }

                return clickreturnvalue()
            }

            function clickreturnvalue() {
                if (ie4 || ns6) return false
                else return true
            }

            function contains_ns6(a, b) {
                if (b) {
                    while (b.parentNode)
                        if ((b = b.parentNode) == a)
                            return true;
                    return false;
                }
            }

            function dynamichide(e) {
                if (ie4 && !dropmenuobj.contains(e.toElement))
                    delayhidemenu()
                else if (ns6 && e.currentTarget != e.relatedTarget && !contains_ns6(e.currentTarget, e.relatedTarget))
                    delayhidemenu()
            }

            function hidemenu(e) {
                if (typeof dropmenuobj != "undefined") {
                    if (ie4 || ns6)
                        dropmenuobj.style.visibility = "hidden"
                }
            }

            function delayhidemenu() {
                if (ie4 || ns6)
                    delayhide = setTimeout("hidemenu()", disappeardelay)
            }

            function clearhidemenu() {
                if (typeof delayhide != "undefined")
                    clearTimeout(delayhide)
            }

            function AggiungiAnno(MiaData, Durata) {
                alert(MiaData);
                if (MiaData.length == 10) {
                    Scadenza = eval(MiaData.substr(6, 4)) + eval(Durata);
                    AggiungiAnno = MiaData.substr(0, 6) + Scadenza;
                    return AggiungiAnno;
                }
                else {
                    AggiungiAnno = '';
                    return AggiungiAnno;
                }
            }

            if (hidemenu_onclick == "yes")
                document.onclick = hidemenu

        </script>
        <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
        <title>Contratto</title>
        <script type="text/javascript">
            document.write('<style type="text/css">.tabber{display:none;}<\/style>');

            //var tabberOptions = {'onClick':function(){alert("clicky!");}};
            var tabberOptions = {


                /* Optional: code to run when the user clicks a tab. If this
                function returns boolean false then the tab will not be changed
                (the click is canceled). If you do not return a value or return
                something that is not boolean false, */

                'onClick': function (argsObj) {

                    var t = argsObj.tabber; /* Tabber object */
                    var id = t.id; /* ID of the main tabber DIV */
                    var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                    var e = argsObj.event; /* Event object */

                    document.getElementById('txttab').value = i + 1;

                },
                'addLinkId': true

            };


        </script>
        <script type="text/javascript" src="tabber.js"></script>
        <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
        <script language="javascript" type="text/javascript">

            window.onbeforeunload = function () {
                if (document.getElementById("USCITA")) {
                    if (document.getElementById("USCITA").value == '0') {
                        if (navigator.appName != 'Microsoft Internet Explorer') {
                            return 'ATTENZIONE!\nPer evitare il blocco temporaneo di alcuni dati chiudere la finestra utilizzato il pulsante Esci.';
                        }
                    }
                }
            };
            window.onunload = Exit;

            function MostraDivAttesa() {
                if (document.getElementById('Attesa')) {
                    document.getElementById('Attesa').style.visibility = 'visible';
                }
            }

            function apriMorosita(indice) {
                var win = null;
                LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
                TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
                LeftPosition = LeftPosition - 20;
                TopPosition = TopPosition - 20;
                window.open('Dettagli.aspx?ID=' + indice.value, 'Dettagli', 'height=200,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                document.getElementById('USCITA').value = '0';
            }

            function ApriDettRifContratto() {
                var win = null;
                LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
                TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
                LeftPosition = LeftPosition - 20;
                TopPosition = TopPosition - 20;
                window.open('DettagliContr.aspx?ID=<%=lIdContratto %>', 'Dettagli', 'height=200,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
                document.getElementById('USCITA').value = '0';
            }

            function ConfermaEsci() {

                if (document.getElementById('txtModificato').value == '1') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?\nUSCIRE SENZA SALVARE CAUSERA\' LA PERDITA DELLE MODIFICHE! PER NON USCIRE PREMERE IL PULSANTE ANNULLA.");
                    if (chiediConferma == false) {
                        document.getElementById('txtModificato').value = '111';
                    }
                    else {
                        if (document.getElementById('Attesa')) {
                            document.getElementById('Attesa').style.visibility = 'visible';
                        }
                    }
                }
                else {
                    if (document.getElementById('Attesa')) {
                        document.getElementById('Attesa').style.visibility = 'visible';
                    }
                }
            }

            function ConfermaSalvataggio() {
                if (document.getElementById('txtModificato').value == '1') {
                    alert("Attenzione...Sono state apportate delle modifiche. Salvare prima di proseguire!");
                    document.getElementById('txtModificato').value = '111';
                }
            }

            function Exit() {
                if (document.getElementById("USCITA")) {
                    if (document.getElementById("USCITA").value == '0') {
                        if (document.getElementById('imgEsci') != null) {
                            document.getElementById('imgEsci').click();
                            alert('Attenzione...in futuro si consiglia di uscire dalla finstra premendo il pulsante ESCI');
                        }
                    }
                }
            }

            function VisEventi() {

                document.getElementById('USCITA').value = '1';
                window.open('Eventi_1.aspx?COD=' + document.getElementById("LBLcodUI").value + '&ID=<%=lIdContratto %>', '<%=lIdContratto %>', '');
                document.getElementById('USCITA').value = '0';
            }

            function VisSpese() {
                document.getElementById('USCITA').value = '1';
                window.open('SpeseUnita.aspx?T=<%=lIdConnessione %>&IDC=<%=lIdContratto %>&ID=' + document.getElementById('txtIdUnita').value, 'Spese', 'height=450,top=0,left=0,width=550');
                document.getElementById('USCITA').value = '0';
            }

            function ConfermaAttivazione() {
                //    if (document.getElementById('Tab_Registrazione1_cmbUfficioRegistro').value == '-1' || document.getElementById('Tab_Contratto1_txtDelibera').value == '' || document.getElementById('Tab_Contratto1_txtDataDelibera').value == '' || document.getElementById('Tab_Contratto1_txtDataDecorrenza').value == '' || document.getElementById('Tab_Contratto1_txtDataConsegna').value == '' || document.getElementById('Tab_Contratto1_txtDataStipula').value == '' || document.getElementById('Tab_Contratto1_txtEntroCuiDisdettare').value == '' || document.getElementById('Tab_Contratto1_txtDataScadenza').value == '' || document.getElementById('Tab_Contratto1_txtDataSecScadenza').value == '') {
                if (document.getElementById('Tab_Contratto1_txtDataTrasST').value == '' || document.getElementById('Tab_Contratto1_txtDelibera').value == '' || document.getElementById('Tab_Contratto1_txtDataDelibera').value == '' || document.getElementById('Tab_Contratto1_txtDataDecorrenza').value == '' || document.getElementById('Tab_Contratto1_txtDataConsegna').value == '' || document.getElementById('Tab_Contratto1_txtDataStipula').value == '' || document.getElementById('Tab_Contratto1_txtEntroCuiDisdettare').value == '' || document.getElementById('Tab_Contratto1_txtDataScadenza').value == '' || document.getElementById('Tab_Contratto1_txtDataSecScadenza').value == '') {
                    alert('Attenzione, per attivare il contratto è necessario che siano stati inseriti i valori relativi a:\nProvvedimento Assegnazione;\nData Provvedimento;\nData trasmissione provvedimento;\nData Decorrenza;\nData Consegna;\nData Stipula;\nData Scadenza;\nData Seconda Scadenza;\nMesi entro cui disdettare;\nUfficio Registrazione.');
                    document.getElementById('TXTATTIVA').value = '0';

                }
                else {

                    var sicuro = window.confirm('Sei sicuro di voler attivare questo contratto? Si ricorda che la modifica di tutti i parametri del contratto è possibile quando lo stato è impostato su BOZZA.');
                    if (sicuro == true) {
                        document.getElementById('TXTATTIVA').value = '1';
                    }
                    else {
                        document.getElementById('TXTATTIVA').value = '0';
                    }
                }
            }

            function ConfermaCambio() {
                var sicuro = window.confirm('Attenzione...Sei sicuro di voler cambiare l\'indirizzo dell\'unità immobiliare con l\'indirizzo riservato alle comunicazioni?\nPremere OK per confermare o Annulla per tornare indietro!');
                if (sicuro == true) {
                    document.getElementById('confermacambio').value = '1';
                }
                else {
                }
            }
            function ConfermaMAV() {
                var sicuro = window.confirm('Sei sicuro di voler creare le bollette di pre-attivazione? Si ricorda che saranno create le bollette e successivamente i mav relativi alla cauzione, bolli, affitti intermedi, etc.');
                if (sicuro == true) {
                    document.getElementById('TXTATTIVA').value = '1';
                    if (document.getElementById('Attesa')) {
                        document.getElementById('Attesa').style.visibility = 'visible';
                    }
                }
                else {
                    document.getElementById('TXTATTIVA').value = '0';
                    if (document.getElementById('Attesa')) {
                        document.getElementById('Attesa').style.visibility = 'hidden';
                    }
                }
            }

            function ConfermaAnnulloInvio() {

                var sicuro = window.confirm('Sei sicuro di voler annullare e permettere al sistema di includere nuovamente il contratto in un file di prima registrazione?');
                if (sicuro == true) {
                    document.getElementById('USCITA').value = '1';
                    document.getElementById('Tab_Registrazione1_HConferma').value = '1';
                }
                else {
                    document.getElementById('USCITA').value = '1';
                    document.getElementById('Tab_Registrazione1_HConferma').value = '0';
                }
            }


            function ConfermaRicalcolo() {

                var sicuro = window.confirm('Sei sicuro di Calcolare nuovamente il canone?');
                if (sicuro == true) {
                    document.getElementById('USCITA').value = '1';
                    document.getElementById('Tab_Canone1_HH1').value = '1';
                }
                else {
                    document.getElementById('USCITA').value = '1';
                    document.getElementById('Tab_Canone1_HH1').value = '0';
                }
            }
            function ConfermaAnnullo() {
                var sicuro = window.confirm('Sei sicuro di voler annullare questa bolletta?\nContattare l\'intestatario e ASSICURARSI che non abbia già provveduto al pagamento.\nPremere ANNULLA per annullare l\'operazione!');
                if (sicuro == true) {
                    document.getElementById('Tab_Bollette1_txtannullo').value = '1';
                }
                else {
                    document.getElementById('Tab_Bollette1_txtannullo').value = '0';
                }
            }
            function ConfermaChiusura() {
                if (document.getElementById('txtModificato').value == '1') {
                    alert("Attenzione...Sono state apportate delle modifiche. Salvare prima di proseguire!");
                    document.getElementById('TXTATTIVA').value = '0';
                }
                else {
                    var sicuro = window.confirm('Sei sicuro di voler CHIUDERE questo contratto?\n In caso affermativo, sarà emessa una bolletta con le eventuali spese di recessione contratto, gli interessi maturati sul deposito cauzionale e tutte le eventuali spese in pendenza.\nSarà creata una scrittura gestionale per il deposito cauzionale se presente.');
                    if (sicuro == true) {
                        document.getElementById('TXTATTIVA').value = '1';
                        if (document.getElementById('Attesa')) {
                            document.getElementById('Attesa').style.visibility = 'visible';
                        }
                    }
                    else {
                        document.getElementById('TXTATTIVA').value = '0';
                    }
                }
            }
            var data;
            function dateAdd(date, tipo, valore) {
                (typeof (date) == "number") ? 1 == 1 : data = new Date(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(), 0);

                switch (tipo) {
                    case "s":
                        si = div(data.getSeconds() + valore, 60);
                        s = (data.getSeconds() + valore) % 60;
                        si ? addInterval(data.setSeconds(s), "m", si) : data.setSeconds(s);
                        break;
                    case "m":
                        mi = div(data.getMinutes() + valore, 60);
                        m = (data.getMinutes() + valore) % 60;
                        mi ? addInterval(data.setMinutes(m), "h", mi) : data.setMinutes(m);
                        break;
                    case "h":
                        hi = div(data.getHours() + valore, 24);
                        h = (data.getHours() + valore) % 24;
                        hi ? addInterval(data.setHours(h), "dd", hi) : data.setHours(h);
                        break;
                    case "dd":
                        mod = getDaysInMonth(data);
                        ddi = div(data.getDate() + valore, mod);
                        dd = (data.getDate() + valore) % mod;
                        ddi ? addInterval(data.setDate(dd), "mm", ddi) : data.setDate(dd);
                        break;
                    case "mm":
                        mmi = div(data.getMonth() + valore, 12);
                        mm = (data.getMonth() + valore) % 12;
                        mmi ? addInterval(data.setMonth(mm), "yy", mmi) : data.setMonth(mm);
                        break;
                    case "yy":
                        yy = (data.getFullYear() + valore);
                        data.setFullYear(yy);
                        break;
                    default:
                }
                return data;
            }
            function getDaysInMonth(aDate) {
                var m = new Number(aDate.getMonth());
                var y = new Number(aDate.getYear());

                var tmpDate = new Date(y, m, 28);
                var checkMonth = tmpDate.getMonth();
                var lastDay = 27;

                while (lastDay <= 31) {
                    temp = tmpDate.setDate(lastDay + 1);
                    if (checkMonth != tmpDate.getMonth())
                        break;
                    lastDay++
                }
                return lastDay;
            }

            function div(op1, op2) {
                return (op1 / op2 - op1 % op2 / op2)
            }

            function refreshPage(arg) {
                if (document.getElementById('imgSalva')) {
                    document.getElementById('imgSalva').click();
                };
            };

        </script>
    </telerik:RadCodeBlock>
    <script type="text/javascript">





        function PROVA() {
            document.getElementById('USCITA').value = '1';
        }

        //Contents for menu 1
        var menu1 = new Array()
        menu1[0] = '<a href="javascript:Cessione();">Denuncia Cessione Fabbricato</a>'
        menu1[1] = '<a href="javascript:Consegna();">Verbale di Consegna</a>'
        menu1[2] = '<a href="javascript:Riconsegna();">Verbale Ricons. Chiavi/Disdetta/Promemoria</a>'
        //menu1[3] = '<a href="javascript:Ospitalita();">Dichiarazione di Ospitalità</a>'
        //menu1[4] = '<a href="javascript:Disdetta();">Disdetta Immobile</a>'
        //menu1[5] = '<a href="javascript:Consistenza();">Verbale di Consistenza</a>'
        menu1[6] = '<a href="javascript:ChiusuraCont();">Verbale Chiusura Contr./Tassa Rif.</a>'
        //****** 05-07-2012 NUOVI DOCUMENTI ****** 
        //menu1[7] = '<a href="javascript:Privacy();">Normativa Privacy</a>'
        //menu1[8] = '<a href="javascript:TassaRifiuti();">Dichiarazione Per Tassa Rifiuti</a>'
        menu1[9] = '<a href="javascript:ModuloRifiuti();">Modulo Tassa Rifiuti</a>'
        menu1[10] = '<a href="javascript:DichMaggiorenni();">Dichiarazione Maggiorenni</a>'
        //****** 05-07-2012 fine NUOVI DOCUMENTI ****** 

        menu1[11] = '<a href="javascript:Apri();">Visualizza stampe Contratti</a>'
        menu1[12] = '<a href="javascript:AllegatiContratto();">Visualizza Allegati</a>'
        menu1[13] = '<a href="javascript:AllegaFile();">Allega File</a>'
        //menu1[10] = '<a href="javascript:ApriTrasloco();">Trasloco/Magazzinaggio</a>'


        //menu1[14] = '<a href="javascript:ApriPromUtente();">Promemoria Utente</a>'
        menu1[15] = '<a href="javascript:AllegatiContrattoEx();">Visualizza Allegati ex Gestore</a>'
        //Contents for menu 2, and so on
        var menu2 = new Array()
        menu2[0] = '<a href="javascript:ApriRateizzazione();">Rateizzazione</a>'
        menu2[2] = '<a href="javascript:ScegliSpostamAnnull();">Variaz.Decorr./Spostam./Annullam.</a>'

        menu2[3] = '<a href="javascript:TrasferimBoll();">Trasferimento Bollette</a>'
        menu2[4] = '<a href="javascript:AggiornamentoNucleo();">Aggiorna Nucleo</a>'

        var menuwidth = '300px' //default menu width
        var menubgcolor = 'lightyellow'  //menu bgcolor
        var disappeardelay = 250  //menu disappear speed onMouseout (in miliseconds)
        var hidemenu_onclick = "yes" //hide menu when user clicks within menu?

        /////No further editting needed
        var menuAttivazione = new Array()
        menuAttivazione[0] = '<a href="javascript:CreaBollettino1();">Bollettino N.1</a>'
        menuAttivazione[1] = '<a href="javascript:CreaBollettino2();">Bollettino N.2</a>'

        var ie4 = document.all
        var ns6 = document.getElementById && !document.all

        if (ie4 || ns6)
            document.write('<div id="dropmenudiv" onclick="javascript:PROVA();" style="visibility:hidden;width:' + menuwidth + ';background-color:' + menubgcolor + '" onMouseover="clearhidemenu()" onMouseout="dynamichide(event)"></div>')

        function getposOffset(what, offsettype) {
            var totaloffset = (offsettype == "left") ? what.offsetLeft : what.offsetTop;
            var parentEl = what.offsetParent;
            while (parentEl != null) {
                totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop;
                parentEl = parentEl.offsetParent;
            }
            return totaloffset;
        }

        function showhide(obj, e, visible, hidden, menuwidth) {
            if (ie4 || ns6)
                dropmenuobj.style.left = dropmenuobj.style.top = -500
            if (menuwidth != "") {
                dropmenuobj.widthobj = dropmenuobj.style
                dropmenuobj.widthobj.width = menuwidth
            }
            if (e.type == "click" && obj.visibility == hidden || e.type == "mouseover")
                obj.visibility = visible
            else if (e.type == "click")
                obj.visibility = hidden
        }

        function iecompattest() {
            return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
        }

        function clearbrowseredge(obj, whichedge) {
            var edgeoffset = 0
            if (whichedge == "rightedge") {
                var windowedge = ie4 && !window.opera ? iecompattest().scrollLeft + iecompattest().clientWidth - 15 : window.pageXOffset + window.innerWidth - 15
                dropmenuobj.contentmeasure = dropmenuobj.offsetWidth
                if (windowedge - dropmenuobj.x < dropmenuobj.contentmeasure)
                    edgeoffset = dropmenuobj.contentmeasure - obj.offsetWidth
            }
            else {
                var topedge = ie4 && !window.opera ? iecompattest().scrollTop : window.pageYOffset
                var windowedge = ie4 && !window.opera ? iecompattest().scrollTop + iecompattest().clientHeight - 15 : window.pageYOffset + window.innerHeight - 18
                dropmenuobj.contentmeasure = dropmenuobj.offsetHeight
                if (windowedge - dropmenuobj.y < dropmenuobj.contentmeasure) { //move up?
                    edgeoffset = dropmenuobj.contentmeasure + obj.offsetHeight
                    if ((dropmenuobj.y - topedge) < dropmenuobj.contentmeasure) //up no good either?
                        edgeoffset = dropmenuobj.y + obj.offsetHeight - topedge
                }
            }
            return edgeoffset
        }

        function populatemenu(what) {
            if (ie4 || ns6)
                dropmenuobj.innerHTML = what.join("")
        }

        function dropdownmenu(obj, e, menucontents, menuwidth) {
            if (window.event) event.cancelBubble = true
            else if (e.stopPropagation) e.stopPropagation()
            clearhidemenu()
            dropmenuobj = document.getElementById ? document.getElementById("dropmenudiv") : dropmenudiv
            populatemenu(menucontents)

            if (ie4 || ns6) {
                showhide(dropmenuobj.style, e, "visible", "hidden", menuwidth)
                dropmenuobj.x = getposOffset(obj, "left")
                dropmenuobj.y = getposOffset(obj, "top")
                dropmenuobj.style.left = dropmenuobj.x - clearbrowseredge(obj, "rightedge") + "px"
                dropmenuobj.style.top = dropmenuobj.y - clearbrowseredge(obj, "bottomedge") + obj.offsetHeight + "px"
            }

            return clickreturnvalue()
        }

        function clickreturnvalue() {
            if (ie4 || ns6) return false
            else return true
        }

        function contains_ns6(a, b) {
            if (b) {
                while (b.parentNode)
                    if ((b = b.parentNode) == a)
                        return true;
            return false;
        }
    }

    function dynamichide(e) {
        if (ie4 && !dropmenuobj.contains(e.toElement))
            delayhidemenu()
        else if (ns6 && e.currentTarget != e.relatedTarget && !contains_ns6(e.currentTarget, e.relatedTarget))
            delayhidemenu()
    }

    function hidemenu(e) {
        if (typeof dropmenuobj != "undefined") {
            if (ie4 || ns6)
                dropmenuobj.style.visibility = "hidden"
        }
    }

    function delayhidemenu() {
        if (ie4 || ns6)
            delayhide = setTimeout("hidemenu()", disappeardelay)
    }

    function clearhidemenu() {
        if (typeof delayhide != "undefined")
            clearTimeout(delayhide)
    }

    function AggiungiAnno(MiaData, Durata) {
        alert(MiaData);
        if (MiaData.length == 10) {
            Scadenza = eval(MiaData.substr(6, 4)) + eval(Durata);
            AggiungiAnno = MiaData.substr(0, 6) + Scadenza;
            return AggiungiAnno;
        }
        else {
            AggiungiAnno = '';
            return AggiungiAnno;
        }
    }

    if (hidemenu_onclick == "yes")
        document.onclick = hidemenu

    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>Contratto</title>
    <script type="text/javascript">
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');

        //var tabberOptions = {'onClick':function(){alert("clicky!");}};
        var tabberOptions = {


            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;

            },
            'addLinkId': true

        };


    </script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoContratto.png);
    background-repeat: no-repeat;">
    <div id="Attesa" style="position: absolute; width: 100%; height: 100%; top: 0px;
        left: 0px; background-color: #f0f0f0; visibility: visible; z-index: 500; display: block;">
        <img src="../ImmDiv/DivUscitaInCorso2.jpg" alt="caricamento in corso..." style="position: absolute;
            top: 125px; left: 203px" />
    </div>
    <script type="text/javascript">

            if (navigator.appName == 'Microsoft Internet Explorer') {
                document.onkeydown = $onkeydown;
            }
            else {
                window.document.addEventListener("keydown", TastoInvio, true);

            }
        </script>

    <form id="form1" runat="server" submitdisabledcontrols="True">
        <telerik:RadScriptManager ID="script" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager runat="server" ID="RadWindowManager1" Localization-OK="OK" Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
    <telerik:RadFormDecorator RenderMode="Classic" Skin="Web20" ID="FormDecorator1" runat="server"
        DecoratedControls="Buttons" />
    <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="true"
        VisibleStatusbar="false" Width="650px" Height="550px" Behaviors="Pin, Move, Close"
        RestrictionZoneID="RestrictionZoneID">
    </telerik:RadWindow>
    
    <telerik:RadWindow ID="RadWindowAggiungi" runat="server" CenterIfModal="true" Modal="true"
        Width="400px" Height="300px" VisibleStatusbar="false" Behaviors="Pin, Move, Close"
        RestrictionZoneID="RestrictionZoneID" Title="Gestione Locatari">
        
    </telerik:RadWindow>


        <p style="width: 1130px" />
        <table width="100%" cellpadding="0" cellspacing="0">

            <tr>
                <td width="60%">

                    <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
                        ForeColor="#660000" Text="Contratto" Width="508px"></asp:Label>
                </td>
                <td width="15%">&nbsp;

                </td>
                <td width="25%" style="background-color: #FFFF99; text-align: center;" id="tipoRU">
                    <asp:Label ID="LBLErpModerato" runat="server" Font-Bold="True"
                        Font-Names="ARIAL" Font-Size="16pt" ForeColor="Black" Text="E.R.P. Moderato"
                        Visible="False"></asp:Label>

                    <asp:Label ID="LBLABUSIVO" runat="server" Font-Bold="True" Font-Names="ARIAL"
                        Font-Size="16pt" ForeColor="Black" Text="ABUSIVO" Visible="False"></asp:Label>&nbsp;
                    <asp:Label ID="LBLVIRTUALE" runat="server" Font-Bold="True"
                        Font-Names="ARIAL" Font-Size="16pt" ForeColor="Black" Text="VIRTUALE"
                        Visible="False"></asp:Label></td>

            </tr>
            <tr>

                <td width="60%">&nbsp;
                </td>
                <td width="15%">&nbsp;

                </td>
                <td width="25%" style="background-color: #FFFF99; text-align: center;" id="tipoRU2">


                    <asp:Label ID="lblOrigineContratto" runat="server" Font-Bold="True"
                        Font-Names="ARIAL" Font-Size="12pt" ForeColor="Black"
                        ToolTip="Origine del contratto"></asp:Label></td>
            </tr>
        </table>


        <table width="100%" cellpadding="0" cellspacing="0">

            <tr>
                <td>
                    <asp:ImageButton ID="btnRinnovoUSD" runat="server" ImageUrl="~/NuoveImm/Img_Salva1.png"
                        ToolTip="Rinnovo USD" OnClientClick="document.getElementById('USCITA').value='1'"
                        TabIndex="2" />
                    <asp:ImageButton ID="imgSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva1.png"
                        ToolTip="Salva" OnClientClick="document.getElementById('USCITA').value='1'" TabIndex="2" />
                    <asp:ImageButton ID="btnCambioIntBox" runat="server" ImageUrl="~/NuoveImm/Img_Salva1.png"
                        ToolTip="Cambio Box" OnClientClick="document.getElementById('USCITA').value='1'"
                        TabIndex="2" />
                </td>
                <td>
                    <a href="javascript:VisSpese();">
                        <img border="0" alt="Calcola le spese per l'unita immobiliare locata" id="imgSpeseUnita"
                            src="../NuoveImm/Img_SpeseUnita.png" style="cursor: pointer" onclick="document.getElementById('USCITA').value='1';" /></a>
                </td>
                <td>
                    <a href="#" onmouseover="return dropdownmenu(this, event, menuAttivazione, '150px')"
                        onmouseout="delayhidemenu()">
                        <asp:Image ID="ImgBolAttivazione" runat="server" ImageUrl="~/NuoveImm/Img_MavAttivazione.png"
                            Visible="True" TabIndex="3" /></a>
                </td>
                <td>
                    <asp:ImageButton ID="btnAttivazione2" runat="server" ImageUrl="~/NuoveImm/Img_Salva1.png"
                        OnClientClick="document.getElementById('USCITA').value='1'" />
                    <asp:ImageButton ID="btnAttivazione1" runat="server" ImageUrl="~/NuoveImm/Img_Salva1.png"
                        OnClientClick="document.getElementById('USCITA').value='1'" />
                </td>
                <td>&nbsp;<asp:ImageButton ID="btnAttivaContratto" runat="server" ImageUrl="~/NuoveImm/Img_AttivaContratto.png"
                    ToolTip="Salva e passa il contratto dallo stato BOZZA allo stato ATTIVO" OnClientClick="document.getElementById('USCITA').value='1';ConfermaAttivazione();"
                    Visible="False" TabIndex="3" />
                </td>
                <td>
                    <asp:ImageButton ID="imgChiudiContratto" runat="server" ImageUrl="~/NuoveImm/Img_FineContratto1.png"
                        ToolTip="Chiude il contratto ed effettua i calcoli finali" OnClientClick="document.getElementById('USCITA').value='1';ConfermaChiusura();"
                        Visible="False" TabIndex="4" />
                </td>
                <td>
                    <asp:ImageButton ID="btnStampaContratto" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
                        ToolTip="Stampa del Contratto" OnClientClick="document.getElementById('USCITA').value='1';ConfermaSalvataggio();"
                        Visible="False" TabIndex="5" />
                </td>
                <td>
                    <a href="javascript:ApriRinnovoUSD();">
                        <img border="0" alt="Rinnovo Contratto USD" id="imgRinnovoUSD" src="../NuoveImm/Img_RinnovoUSD.png"
                            style="cursor: pointer" onclick="document.getElementById('USCITA').value='1';" /></a>
                </td>
                <td>
                    <a href="javascript:ApriCambioBOX();">
                        <img border="0" alt="Cambio Intestazione BOX" id="imgCambioBox" src="../NuoveImm/Img_CambioIntestazione.png"
                            style="cursor: pointer" onclick="document.getElementById('USCITA').value='1';" /></a>
                </td>
                <td>
                    <a href="#" onmouseover="return dropdownmenu(this, event, menu2, '250px')" onmouseout="delayhidemenu()">
                        <asp:Image ID="ImageFunzioni" runat="server" ImageUrl="~/NuoveImm/Img_Funzioni.png" /></a>
                </td>
                <td>
                    <a href="#" onmouseover="return dropdownmenu(this, event, menu1, '300px')" onmouseout="delayhidemenu()">
                        <asp:Image ID="ImageDocumentazione" runat="server" ImageUrl="~/NuoveImm/Img_Documentazione.png"
                            Visible="True" TabIndex="6" /></a>
                </td>
                <td id="imgStampe">
                    <asp:Image ID="ImgCambioIntestazione" runat="server" ImageUrl="~/NuoveImm/Img_CambioIntestazione.png"
                        Visible="true" TabIndex="7" Style="cursor: pointer" onclick="document.getElementById('USCITA').value='1';" />
                </td>
                <td id="imgStampe1">
                    <a href="javascript:VisEventi();">
                        <img border="0" alt="Eventi" id="ImgEventi" src="../NuoveImm/Img_Eventi.png" style="cursor: pointer"
                            onclick="document.getElementById('USCITA').value='1';" /></a>
                </td>
                <td>
                    <asp:ImageButton ID="imgEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci1.png"
                        ToolTip="Esci" OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();"
                        TabIndex="1" />
                </td>
                <td>
                    <asp:Label ID="LBLSTATOC" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                        ForeColor="#C00000" Style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; background-color: white"
                        ToolTip="Indica lo Stato del Contratto"></asp:Label>
                </td>
            </tr>
        </table>

        <div id="MyTab" class="tabber" style="width: 1145px;">
            <div class="tabbertab <%=Tab1 %>">
                <h2>Generale</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc1:TabGenerale ID="Generale1" runat="server" />
            </div>
            <div class="tabbertab <%=Tab2 %>">
                <h2>Contratto</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc2:Tab_Contratto ID="Tab_Contratto1" runat="server" />
            </div>
            <div class="tabbertab <%=Tab3 %>">
                <h2>Unità I. Locata</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc3:Tab_UnitaImmLocate ID="Tab_UnitaImmLocate1" runat="server" />
            </div>
            <div class="tabbertab <%=Tab4 %>">
                <h2>Conduttore</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc6:Tab_Conduttore ID="Tab_Conduttore1" runat="server" />
            </div>
            <div class="tabbertab <%=Tab5 %>">
                <h2>Canone</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc4:Tab_Canone ID="Tab_Canone1" runat="server" />
            </div>
            <div class="tabbertab <%=Tab6 %>">
                <h2>Registrazione</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc5:Tab_Registrazione ID="Tab_Registrazione1" runat="server" />
            </div>
            <div style="visibility: visible" class="tabbertab <%=Tab7 %>">
                <h2>Schema Bollette</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc9:Tab_SchemaBollette ID="Tab_SchemaBollette1" runat="server" />
            </div>
            <div <%=Visibile %> class="tabbertab <%=Tab8 %>">
                <h2>Partite Contabili</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc8:Tab_Bollette ID="Tab_Bollette1" runat="server" />
            </div>
            <div class="tabbertab <%=Tab9 %>">
                <h2>Comunicazioni</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc7:Tab_Comunicazioni ID="Tab_Comunicazioni1" runat="server" />
            </div>
        <div class="tabbertab <%=Tab10 %>">
            <h2>
                Segnalazioni</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <uc10:Tab_Sicurezza ID="Tab_Sicurezza1" runat="server" />
        </div>
        <div class="tabbertab <%=Tab11 %>">
            <h2>
                Az. Legali</h2>
            <uc11:Tab_Azioni_Legali ID="Tab_Azioni_Legali1" runat="server" />
        </div>
            <div <%=VisibileOA %> class="tabbertab <%=Tab12 %>">
                <h2>O.A</h2>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <uc12:Tab_OccupazioneAbusiva runat="server" ID="Tab_OccupazioneAbusiva" />
            </div>
        </div>
        <p>
            <asp:HiddenField ID="contaBollette" runat="server" Value="0" />
            <asp:HiddenField ID="controllaEsistBozza" runat="server" />
            <asp:HiddenField ID="DataScadCont" runat="server" />
            <asp:HiddenField ID="DataScadRinn" runat="server" />
            <asp:HiddenField ID="dataScad2" runat="server" Value="0" />
            <asp:HiddenField ID="dataScad1" runat="server" Value="0" />
            <asp:HiddenField ID="assegn_def" runat="server" Value="0" />
            <asp:HiddenField ID="prorogati" runat="server" Value="0" />
            <asp:HiddenField ID="au_abusivi" runat="server" Value="0" />
            <asp:HiddenField ID="spostaAnnulla" runat="server" Value="0" />
            <asp:HiddenField ID="txttab" runat="server" Value="1" />
            <asp:HiddenField ID="LBLNOMEFILECANONE" runat="server" />
            <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
            <asp:HiddenField ID="txtIdContratto" runat="server" Value="0" />
            <asp:HiddenField ID="txtconduttore" runat="server" Value="0" />
            <asp:HiddenField ID="txtCodAffittuario" runat="server" Value="0" />
            <asp:HiddenField ID="TXTATTIVA" runat="server" Value="0" />
            <asp:HiddenField ID="txtIdUnita" runat="server" Value="0" />
            <asp:HiddenField ID="USCITA" runat="server" Value="0" />
            <asp:HiddenField ID="txtConguaglio" runat="server" Value="0" />
            <asp:HiddenField ID="txtInvioBollette" runat="server" Value="0" />
            <asp:HiddenField ID="VIRTUALE" runat="server" Value="0" />
            <asp:HiddenField ID="HStatoContratto" runat="server" />
            <asp:HiddenField ID="lettura" runat="server" />
            <asp:HiddenField ID="opfiliale" runat="server" />
            <asp:HiddenField ID="speseunita" runat="server" Value="0" />
            <asp:HiddenField ID="cmbintestazione" runat="server" Value="0" />
            <asp:HiddenField ID="disdettato" runat="server" Value="0" />
            <asp:HiddenField ID="bloccato" runat="server" Value="0" />
            <asp:HiddenField ID="confermacambio" runat="server" Value="0" />
            <asp:HiddenField ID="annoschema" runat="server" Value="0" />
            <asp:HiddenField ID="CambioBox" runat="server" Value="0" />
            <asp:HiddenField ID="RinnovoUSD" runat="server" Value="0" />
            <asp:HiddenField ID="RinnovoDataChiusura" runat="server" Value="0" />
            <asp:HiddenField ID="RinnovoDataPG" runat="server" Value="0" />
            <asp:HiddenField ID="RinnovoNumeroPG" runat="server" Value="0" />
            <asp:HiddenField ID="RinnovoCanone" runat="server" Value="0" />
            <asp:HiddenField ID="FaiRinnovoUSD" runat="server" Value="0" />
            <asp:HiddenField ID="FaiCambioBox" runat="server" Value="0" />
            <asp:HiddenField ID="CFRinnovoBoxUSD" runat="server" Value="" />
            <asp:HiddenField ID="IDRinnovoBoxUSD" runat="server" Value="" />
            <asp:HiddenField ID="Rateizza" runat="server" Value="0" />
            <asp:HiddenField ID="MostrMsgSalva" runat="server" Value="1" />
            <asp:HiddenField ID="LBLcodUI" runat="server" />
            <asp:HiddenField ID="LBLintest" runat="server" />
            <asp:HiddenField ID="LBLcodF" runat="server" />
            <asp:HiddenField ID="RateizInCorso" runat="server" Value="0" />
            <asp:HiddenField ID="AULETTURA" runat="server" Value="0" />
            <asp:HiddenField ID="GLLETTURA" runat="server" Value="0" />
            <asp:HiddenField ID="nuovocanone" runat="server" Value="0" />
            <asp:HiddenField ID="pressoCOR" runat="server" />
            <asp:HiddenField ID="idSloggio" runat="server" />
            <asp:HiddenField ID="VisNuovoContratto" runat="server" />
            <asp:HiddenField ID="Contact" runat="server" />
            <asp:HiddenField ID="idNota" runat="server" Value="-1" />
            <asp:HiddenField ID="idAvviso" runat="server" Value="-1" />
            <asp:HiddenField ID="EscludiS" runat="server" Value="0" />
            <asp:HiddenField ID="NumElementiVisualizzati" runat="server" Value="0" />
            <asp:HiddenField ID="FL_PAG_MANUALI" runat="server" Value="0" />
            <asp:HiddenField ID="FL_PAG_MAN_RUOLI" runat="server" Value="0" />
            <asp:HiddenField ID="FL_PAG_MAN_ING" runat="server" Value="0" />
            <asp:HiddenField ID="AGGBOLL" runat="server" Value="0" />
            <asp:HiddenField ID="idAU392" runat="server" Value="0" />
            <asp:HiddenField ID="dataChisura392" runat="server" Value="0" />
            <asp:HiddenField ID="contrattoST" runat="server" Value="0" />
            <asp:HiddenField ID="traformaRUstep2" runat="server" Value="0" />
            <asp:HiddenField ID="durataMesi" runat="server" Value="0" />
            <asp:HiddenField ID="idDomandaOrigine" runat="server" Value="0" />
            <asp:HiddenField ID="idDichOrigine" runat="server" Value="0" />
            <asp:HiddenField ID="mostraStoricoReg" runat="server" Value="0" />
            <asp:HiddenField ID="eventomodificadati" runat="server" Value="0" />
            <asp:HiddenField ID="sceltaDestEcc" runat="server" Value="0" />
        </p>
        <script type="text/javascript">
            // document.getElementById('MyTab').tabber.tabHide(0);


            function ApriContratto(id, codice) {

                today = new Date();
                var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();

                popupWindow = window.open('Contratto.aspx?LT=1CC=1&ID=' + id + '&COD=' + codice, Titolo, 'height=780,width=1160');
                popupWindow.focus();


            }

            window.focus();
            self.focus();

            myOpacity = new fx.Opacity('InserimentoOspiti', { duration: 200 });
            myOpacity.hide();

            myOpacity1 = new fx.Opacity('InserimentoBolletta', { duration: 200 });
            //myOpacity1.hide();

            if (document.getElementById('Tab_Bollette1_txtAppare').value != '1') {
                //document.getElementById('InserimentoBolletta').style.visibility = 'hidden';
                myOpacity1.hide();
            }

            myOpacity2 = new fx.Opacity('InserimentoSchema', { duration: 200 });
            if (document.getElementById('Tab_SchemaBollette1_txtAppare').value != '2') {
                myOpacity2.hide();
            }

            myOpacity10 = new fx.Opacity('InfoUtente', { duration: 200 });
            myOpacity10.hide();


            myOpacityStorno = new fx.Opacity('Storno', { duration: 200 });

            if (document.getElementById('Tab_Bollette1_txtAppare1').value != '1') {
                myOpacityStorno.hide();
            }
            function Privacy() {
                window.open('Comunicazioni/Normativa_Privacy.aspx?COD=<%=CodContratto1 %>', 'Privacy', '');
                document.getElementById('USCITA').value = '0';
            }
            function TassaRifiuti() {
                window.open('Comunicazioni/Dich_TassaRifiuti.aspx?COD=<%=CodContratto1 %>', 'Rifuti', '');
                document.getElementById('USCITA').value = '0';
            }
            function ModuloRifiuti() {
                window.open('Comunicazioni/ModuloRifiuti.aspx?COD=<%=CodContratto1 %>', 'Modulo', '');
                document.getElementById('USCITA').value = '0';
            }
            function DichMaggiorenni() {
                window.open('Comunicazioni/Dich_Maggiorenni.aspx?COD=<%=CodContratto1 %>', 'Maggiorenni', '');
                document.getElementById('USCITA').value = '0';
            }

            function Apri() {
                window.open('ElencoStampeContratti.aspx?COD=<%=CodContratto1 %>', 'Contratto', '');
                document.getElementById('USCITA').value = '0';
            }

            function AllegatiContratto() {
                window.open('ElencoAllegati.aspx?LT=' + document.getElementById('lettura').value + '&COD=<%=CodContratto1 %>', 'Allegati', '');
                document.getElementById('USCITA').value = '0';
            }

            function AllegatiContrattoEx() {
                window.open('ElencoAllegatiExGestori.aspx?LT=' + document.getElementById('lettura').value + '&COD=<%=CodContratto1 %>', 'Allegati', '');
                document.getElementById('USCITA').value = '0';
            }

            function AllegaFile() {
                window.open('../InvioAllegato.aspx?T=1&ID=<%=CodContratto1 %>', 'Allegati', '');
                document.getElementById('USCITA').value = '0';
            }


            function ApriPromUtente() {
                window.open('../CENSIMENTO/ModuloPromUtente.aspx?PROV=1&COD=<%=lIdContratto %>', 'ModuloRappSloggio', '');
                document.getElementById('USCITA').value = '0';

            }
            function Riconsegna() {
                window.open('Comunicazioni/RiconsegnaImmobile.aspx?COD=<%=CodContratto1 %>', 'RiConsegna', '');
                document.getElementById('USCITA').value = '0';

            }

            function Consegna() {
                window.open('Comunicazioni/ConsegnaChiavi.aspx?COD=<%=CodContratto1 %>', 'Consegna', '');
                document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '1';
            }

            function Cessione() {
                window.open('Comunicazioni/DenunciaCessione.aspx?COD=<%=CodContratto1 %>&L=<%=LetteraProvenienza %>', 'Cessione', '');
                document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '1';
            }

            function Ospitalita() {
                window.open('Comunicazioni/Ospitalita.aspx?T=1&COD=<%=CodContratto1 %>', 'Ospitalità', '');
                document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '1';
            }

            function Disdetta() {
                window.open('Comunicazioni/Disdetta.aspx?T=1&COD=<%=CodContratto1 %>', 'Disdetta', '');
                document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '1';
            }

            function Consistenza() {
                alert('Non disponibile!');
            //alert('Attenzione, utilizzare il campo note per inserire la descrizione di eventuali danni o altri fattori rilevanti. La situazione sarà storicizzata al momento della chiusura del contratto!');
            //window.open('../CENSIMENTO/VerificaSManutentivo.aspx?C=<%=CodContratto1 %>&T=1&L=1&ID=' + document.getElementById('txtIdUnita').value, 'Consistenza', '');
                document.getElementById('USCITA').value = '0';
            }

            function ChiusuraCont() {
                window.open('Comunicazioni/ChiusuraContratto.aspx?T=1&COD=<%=CodContratto1 %>', 'Chiusura', '');
                document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '1';
            }

            function MyDialogArguments() {
                this.Sender = null;
                this.StringValue = "";
            }

            function TrasferimBoll() {
                if (document.getElementById('txtModificato').value == '1') {
                    alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
                }

                else {
                    if (document.getElementById('txtIdUnita').value == '0') {
                        alert('Impossibile procedere per mancanza dell\' UNITA\' IMMOBILIARE!');
                        return;
                    }

                    dialogArgs = new MyDialogArguments();

                    dialogArgs.Sender = window;
                    var dialogResults = window.showModalDialog('TrasferimBollette.aspx?IDCONT=' + document.getElementById('txtIdContratto').value + '&CODRUA=' + document.getElementById('controllaEsistBozza').value, window, 'status:no;dialogWidth:940px;dialogHeight:560px;dialogHide:true;help:no;scroll:no;resizable:no;');
                    if ((dialogResults != undefined) && (dialogResults == '1') && (dialogResults != false)) {
                        document.getElementById('USCITA').value = '0';
                        document.getElementById('MostrMsgSalva').value = '0';
                        if (document.getElementById('imgSalva')) {
                            document.getElementById('imgSalva').click();
                        }
                        else {
                            //                document.getElementById('USCITA').value = '1';
                            //                __doPostBack();
                            //                document.getElementById('USCITA').value = '0';
                        }
                    }
                }
            }

            function TrasformaDa392() {
                dialogArgs = new MyDialogArguments();
                dialogArgs.StringValue = '';
                dialogArgs.Sender = window;

                if (document.getElementById('idAU392') != '0') {
                    var dialogResults = window.showModalDialog('TrasformaContratto.aspx?IDCONT=' + document.getElementById('txtIdContratto').value + '&IDDICH=' + document.getElementById('idAU392').value, 'window392', 'status:no;dialogWidth:600px;dialogHeight:300px;dialogHide:true;help:no;scroll:no;resizable:no;');
                    document.getElementById('USCITA').value = '0';
                }

                if ((dialogResults != undefined) && (dialogResults != '1') && (dialogResults != false)) {
                    if (document.getElementById('Attesa')) {
                        document.getElementById('Attesa').style.visibility = 'visible';
                    }
                    document.getElementById('USCITA').value = '0';
                    //document.getElementById('dataChisura392').value = dialogResults;
                    document.getElementById('MostrMsgSalva').value = '0';
                    if (document.getElementById('imgSalva')) {
                        document.getElementById('imgSalva').click();
                    };

                };

            }

            function TrasformaDa392_bis() {
                var mesi = new Array();
                mesi[0] = "01";
                mesi[1] = "02";
                mesi[2] = "03";
                mesi[3] = "04";
                mesi[4] = "05";
                mesi[5] = "06";
                mesi[6] = "07";
                mesi[7] = "08";
                mesi[8] = "09";
                mesi[9] = "10";
                mesi[10] = "11";
                mesi[11] = "12";

                var giorni = new Array();

                giorni[1] = "01";
                giorni[2] = "02";
                giorni[3] = "03";
                giorni[4] = "04";
                giorni[5] = "05";
                giorni[6] = "06";
                giorni[7] = "07";
                giorni[8] = "08";
                giorni[9] = "09";
                giorni[10] = "10";
                giorni[11] = "11";
                giorni[12] = "12";
                giorni[13] = "13";
                giorni[14] = "14";
                giorni[15] = "15";
                giorni[16] = "16";
                giorni[17] = "17";
                giorni[18] = "18";
                giorni[19] = "19";
                giorni[20] = "20";
                giorni[21] = "21";
                giorni[22] = "22";
                giorni[23] = "23";
                giorni[24] = "24";
                giorni[25] = "25";
                giorni[26] = "26";
                giorni[27] = "27";
                giorni[28] = "28";
                giorni[29] = "29";
                giorni[30] = "30";
                giorni[31] = "31";

                dialogArgs = new MyDialogArguments();
                dialogArgs.StringValue = '';
                dialogArgs.Sender = window;
                if (document.getElementById('traformaRUstep2') != '0') {
                    var dialogResults = window.showModalDialog('TrasformaContratto2.aspx?IDCONT=' + document.getElementById('txtIdContratto').value + '&IDDICH=' + document.getElementById('idAU392').value, 'window392b', 'status:no;dialogWidth:600px;dialogHeight:300px;dialogHide:true;help:no;scroll:no;resizable:no;');
                    document.getElementById('USCITA').value = '0';
                    var currentdate = new Date();
                    currentdate.setDate(new Date().getDate() - 1);
                    var dataodierna = new String((giorni[currentdate.getDate()]) + "/" + (mesi[currentdate.getMonth()]) + "/" + (currentdate.getFullYear()));

                    if (dialogResults != 'undefined' && dialogResults < 0) {
                        today = new Date();
                        var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();
                        popupWindow = window.open('Contratto.aspx?ID=' + dialogResults * -1, Titolo, 'height=780,width=1160');
                        popupWindow.focus();
                        document.getElementById('dataChisura392').value = dataodierna;
                        document.getElementById('MostrMsgSalva').value = '0';
                        if (document.getElementById('imgSalva')) {
                            document.getElementById('imgSalva').click();
                        };
                    }
                    else {
                        if (document.getElementById('Attesa')) {
                            document.getElementById('Attesa').style.visibility = 'visible';
                        }
                        document.getElementById('nuovocanone').value = dialogResults;
                        if (document.getElementById('nuovocanone').value != 'undefined') {
                            document.getElementById('MostrMsgSalva').value = '0';
                            if (document.getElementById('imgSalva') && document.getElementById('nuovocanone').value != '0') {
                                document.getElementById('dataChisura392').value = dataodierna;
                                document.getElementById('contrattoST').value = '1';
                                document.getElementById('imgSalva').click();
                            }
                        } else {
                            document.getElementById('nuovocanone').value = '0';
                            document.getElementById('MostrMsgSalva').value = '0';
                            if (document.getElementById('imgSalva')) {
                                document.getElementById('imgSalva').click();
                            };
                        }
                    }

                }
            }

            function AggiornamentoNucleo() {
                if (document.getElementById('txtModificato').value == '1') {
                    alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
                }

                else {

                    dialogArgs = new MyDialogArguments();

                    dialogArgs.Sender = window;
                    var dialogResults = window.showModalDialog('AggNucleoSingoloRU.aspx?IDCONT=' + document.getElementById('txtIdContratto').value, window, 'status:no;dialogWidth:500px;dialogHeight:320px;dialogHide:true;help:no;scroll:no;resizable:no;');
                    if ((dialogResults != undefined) && (dialogResults == '1') && (dialogResults != false)) {
                        document.getElementById('USCITA').value = '0';
                        document.getElementById('MostrMsgSalva').value = '0';
                        if (document.getElementById('imgSalva')) {
                            document.getElementById('imgSalva').click();
                        };

                    };
                };
            };

            function ApriRateizzazione() {
                if (document.getElementById('txtModificato').value == '1') {
                    alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
                }
                else {
                    if (document.getElementById('txtIdUnita').value == '0') {
                        alert('Impossibile procedere per mancanza dell\' UNITA\' IMMOBILIARE!');
                        return;
                    }
                    var dialogResults = window.showModalDialog('../RATEIZZAZIONE/BolRateizzabili.aspx?IDCONTRATTO=' + document.getElementById('txtIdContratto').value, 'window', 'status:no;dialogWidth:920px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
                    if ((dialogResults != undefined) && (dialogResults == '1') && (dialogResults != false)) {
                        document.getElementById('USCITA').value = '0';
                        document.getElementById('MostrMsgSalva').value = '0';
                        if (document.getElementById('imgSalva')) {
                            document.getElementById('imgSalva').click();
                        }
                        else {
                            //                document.getElementById('USCITA').value = '1';
                            //                __doPostBack();
                            //                document.getElementById('USCITA').value = '0';
                        }
                    }


                    // }
                }
            }

            function ElencoPagamenti() {
                window.open('ElencoPagamenti.aspx?ID=' + document.getElementById('txtIdContratto').value, 'ElencoPagamenti', '');
            }

            function ApriDepositoCauz() {

                window.open('DepositoCauzionale.aspx?IDC=<%=lIdContratto %>', 'DepCauz', 'height=550,top=0,left=0,width=800');
                document.getElementById('USCITA').value = '0';
            }
            var data;

            function dateAdd(date, tipo, valore) {
                (typeof (date) == "number") ? 1 == 1 : data = new Date(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(), 0);

                switch (tipo) {
                    case "s":
                        si = div(data.getSeconds() + valore, 60);
                        s = (data.getSeconds() + valore) % 60;
                        si ? addInterval(data.setSeconds(s), "m", si) : data.setSeconds(s);
                        break;
                    case "m":
                        mi = div(data.getMinutes() + valore, 60);
                        m = (data.getMinutes() + valore) % 60;
                        mi ? addInterval(data.setMinutes(m), "h", mi) : data.setMinutes(m);
                        break;
                    case "h":
                        hi = div(data.getHours() + valore, 24);
                        h = (data.getHours() + valore) % 24;
                        hi ? addInterval(data.setHours(h), "dd", hi) : data.setHours(h);
                        break;
                    case "dd":
                        mod = getDaysInMonth(data);
                        ddi = div(data.getDate() + valore, mod);
                        dd = (data.getDate() + valore) % mod;
                        ddi ? addInterval(data.setDate(dd), "mm", ddi) : data.setDate(dd);
                        break;
                    case "mm":
                        mmi = div(data.getMonth() + valore, 12);
                        mm = (data.getMonth() + valore) % 12;
                        mmi ? addInterval(data.setMonth(mm), "yy", mmi) : data.setMonth(mm);
                        break;
                    case "yy":
                        yy = (data.getFullYear() + valore);
                        data.setFullYear(yy);
                        break;
                    default:
                }
                return data;
            }

            function getDaysInMonth(aDate) {
                var m = new Number(aDate.getMonth());
                var y = new Number(aDate.getYear());

                var tmpDate = new Date(y, m, 28);
                var checkMonth = tmpDate.getMonth();
                var lastDay = 27;

                while (lastDay <= 31) {
                    temp = tmpDate.setDate(lastDay + 1);
                    if (checkMonth != tmpDate.getMonth())
                        break;
                    lastDay++
                }
                return lastDay;
            }

            function div(op1, op2) {
                return (op1 / op2 - op1 % op2 / op2)
            }




            function ScriviDate() {
                document.getElementById('txtModificato').value = '1';
                a = document.getElementById('Tab_Contratto1_txtDataDecorrenza').value;
                b = document.getElementById('Tab_Contratto1_txtDurata').value;
                if (b.length <= 0) {
                    miaData = '';
                    alert('Definire la durata del contratto!');
                    document.getElementById('Tab_Contratto1_txtDataDecorrenza').value = '';
                    return;
                };
                if (a.length == 10) {
                    anno1 = parseInt(a.substr(6), 10);
                    mese1 = parseInt(a.substr(3, 2), 10);
                    giorno1 = parseInt(a.substr(0, 2), 10);
                    var dataok1 = new Date(anno1, mese1 - 1, giorno1);
                    dateAdd(dataok1, 'dd', -1);
                    a = data;
                    var G = a.getDate();
                    var M = (a.getMonth() + 1);
                    if (G < 10) {
                        var gg = "0" + a.getDate();
                    }
                    else {
                        var gg = a.getDate();
                    }
                    if (M < 10) {
                        var mm = "0" + (a.getMonth() + 1);
                    }
                    else {
                        var mm = (a.getMonth() + 1);
                    }
                    var aa = a.getFullYear();
                    miaData = gg + "/" + mm + "/" + aa;
                    //miaData = a;

                    Scadenza = eval(miaData.substr(6, 4)) + eval(b);
                    miaData = miaData.substr(0, 6) + Scadenza;
                }
                else {
                    miaData = '';
                }

                document.getElementById('Tab_Contratto1_txtDataScadenza').value = miaData;

                a = document.getElementById('Tab_Contratto1_txtDataScadenza').value;
                b = document.getElementById('Tab_Contratto1_txtDurataRinnovo').value;
                if (b.length <= 0) {
                    miaData = '';
                    alert('Definire la durata del rinnovo contratto!');
                    document.getElementById('Tab_Contratto1_txtDataDecorrenza').value = '';
                    return;
                };
                if (a.length == 10) {
                    Scadenza = eval(a.substr(6, 4)) + eval(b);
                    miaData = a.substr(0, 6) + Scadenza;
                }
                else {
                    miaData = '';
                }



                document.getElementById('Tab_Contratto1_txtDataSecScadenza').value = miaData;
                document.getElementById('Tab_Contratto1_txtDataDecAE').value = document.getElementById('Tab_Contratto1_txtDataDecorrenza').value;
                document.getElementById('DataScadCont').value = document.getElementById('Tab_Contratto1_txtDataScadenza').value
                document.getElementById('DataScadRinn').value = document.getElementById('Tab_Contratto1_txtDataSecScadenza').value

            }



            function CompletaData(e, obj) {
                // Check if the key is a number
                var sKeyPressed;

                sKeyPressed = (window.event) ? event.keyCode : e.which;

                if (sKeyPressed < 48 || sKeyPressed > 57) {
                    if (sKeyPressed != 8 && sKeyPressed != 0) {
                        // don't insert last non-numeric character
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
                else {
                    if (obj.value.length == 2) {
                        obj.value += "/";
                    }
                    else if (obj.value.length == 5) {
                        obj.value += "/";
                    }
                    else if (obj.value.length > 9) {
                        var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                        if (selText.length == 0) {
                            // make sure the field doesn't exceed the maximum length
                            if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            }
                        }
                    }
                }
            }

            function confronta_data(data1, data2) {	// controllo validità formato data    

                //trasformo le date nel formato aaaammgg (es. 20081103)        
                data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
                data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
                //controllo se la seconda data è successiva alla prima
                if (data2str - data1str < 0) {
                    alert("La data iniziale deve essere precedente quella finale");
                    document.getElementById('Tab_Conduttore1_txtDal').value = '';
                    document.getElementById('Tab_Conduttore1_txtAl').value = '';
                } else {
                    //alert("ok");
                }
            }

            function confronta_dataDelibera(data1, data2) {	// controllo validità formato data    

                //trasformo le date nel formato aaaammgg (es. 20081103)        
                data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
                data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
                //controllo se la seconda data è successiva alla prima
                if (data2str - data1str < 0) {
                    alert("La data deve essere precedente alla data odierna!");
                    document.getElementById('Tab_Contratto1_txtDataDelibera').value = '';
                } else {
                    //alert("ok");
                }
            }

            function confronta_date(data1, data2, d1, d2, CAMPO) {	// controllo validità formato data    

                //trasformo le date nel formato aaaammgg (es. 20081103)        
                data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
                data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
                if (data2str != '') {

                    //controllo se la seconda data è successiva alla prima
                    if (data2str - data1str < 0) {
                        alert('La data ' + d1 + ' deve essere precedente o uguale alla data ' + d2 + '!');
                        CAMPO.value = '';
                    } else {
                        //alert("ok");
                    }
                }
            }

            function ApriDetRat() {
                window.open('../RATEIZZAZIONE/RateizzEmesse.aspx?idcont=' + document.getElementById('txtIdContratto').value, 'DettaglioRat', '')
            }

            function PgManuale() {
                if (document.getElementById('txtCodAffittuario').value != 0) {
                    var left = (screen.width / 2) - (1024 / 2);
                    var top = (screen.height / 2) - (768 / 2);
                    window.open('Pagamenti/PagaManuale.aspx?IDCON=<%= lIdConnessione %>&IDANA=' + document.getElementById('txtCodAffittuario').value + '&IDCONT=' + document.getElementById('txtIdContratto').value + '&OPRU=1', 'PagaManuale', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);

                }
            }
            function PgManualeRuoli() {
                if (document.getElementById('txtCodAffittuario').value != 0) {
                    var left = (screen.width / 2) - (1024 / 2);
                    var top = (screen.height / 2) - (768 / 2);
                    window.open('Pagamenti/IncassiRuolo.aspx?IDCON=<%= lIdConnessione %>&IDANA=' + document.getElementById('txtCodAffittuario').value + '&IDCONT=' + document.getElementById('txtIdContratto').value + '&OPRU=1', 'PagaManuale', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);

                }
            }

            function PgManualeIng() {
                if (document.getElementById('txtCodAffittuario').value != 0) {
                    var left = (screen.width / 2) - (1024 / 2);
                    var top = (screen.height / 2) - (768 / 2);
                    window.open('Pagamenti/IncassiIngiunzioni.aspx?IDCON=<%= lIdConnessione %>&IDANA=' + document.getElementById('txtCodAffittuario').value + '&IDCONT=' + document.getElementById('txtIdContratto').value + '&OPRU=1', 'PagaIng', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);

                }
            }

        </script>
        <script type="text/javascript">
            VisualizzaDivHtml();
            if (document.getElementById('Tab_Registrazione1_imgVisAvviso')) {
                document.getElementById('Tab_Registrazione1_imgVisAvviso').style.visibility = 'hidden';
            }

            //        if (document.getElementById('contaBollette').value<=8)
            //        {
            //            //document.getElementById('divContab').style.height='220px';
            //            if (document.getElementById('divContSenzaInfo') && document.getElementById('divContConInfo')){
            //            document.getElementById('divContSenzaInfo').style.display='none';
            //            document.getElementById('divContConInfo').style.display='none';
            //           }
            //        }
            //        else
            //        {
            //            //document.getElementById('divContab').style.height='220px';
            //        }

            if (document.getElementById('durataMesi').value == '18') {
                if (document.getElementById('tdDurata')) {
                    document.getElementById('tdDurata').style.display = 'none';
                }
                if (document.getElementById('tdDurataMesi')) {
                    document.getElementById('tdDurataMesi').style.display = 'block';
                }
            }


            if (document.getElementById('Tab_Registrazione1_imgVisAvviso')) {
                document.getElementById('Tab_Registrazione1_imgVisAvviso').style.visibility = 'hidden';
            }


            var codContratto;
            codContratto = document.getElementById('Tab_Contratto1_txtCodContratto').value;
            document.getElementById('imgScegliAU').style.visibility = 'hidden';

            if (codContratto.substring(0, 2) == '41' || codContratto.substring(0, 2) == '42' || codContratto.substring(0, 2) == '43') {
                document.getElementById('tblLocatari').style.visibility = 'hidden';
                document.getElementById('tblLocatari').style.position = 'absolute';
                document.getElementById('tblLocatari').style.left = '-100px';
                document.getElementById('tblLocatari').style.display = 'none';
            }

            if (document.getElementById('txtIdContratto').value > '400000000' && document.getElementById('txtIdContratto').value < '500000000') {
                document.getElementById('tblLocatari').style.visibility = 'hidden';
                document.getElementById('tblLocatari').style.position = 'absolute';
                document.getElementById('tblLocatari').style.left = '-100px';
                document.getElementById('tblLocatari').style.display = 'none';
            }
            var Stringa;
            var Stringa1;
            var Stringa2;

            Stringa = document.getElementById('Tab_Contratto1_txtDescrcontratto').value;
            Stringa = Stringa.toUpperCase();

            Stringa2 = document.getElementById('Tab_Contratto1_cmbDestUso').value;

            if (document.all) {
                Stringa1 = document.getElementById('Generale1_lblTipoImmobile').innerText;
            }
            else {
                Stringa1 = document.getElementById('Generale1_lblTipoImmobile').textContent;
            }
            Stringa1 = Stringa1.toUpperCase();


            if (Stringa.substring(0, 11) == 'USI DIVERSI') {
                menu1[3] = '';
            }

            document.getElementById('btnRinnovoUSD').style.visibility = 'hidden';
            document.getElementById('btnRinnovoUSD').style.position = 'absolute';
            document.getElementById('btnRinnovoUSD').style.left = '-100px';
            document.getElementById('btnRinnovoUSD').style.display = 'none';

            document.getElementById('btnCambioIntBox').style.visibility = 'hidden';
            document.getElementById('btnCambioIntBox').style.position = 'absolute';
            document.getElementById('btnCambioIntBox').style.left = '-100px';
            document.getElementById('btnCambioIntBox').style.display = 'none';


            if (document.getElementById('btnAttivazione1')) {
                document.getElementById('btnAttivazione1').style.visibility = 'hidden';
                document.getElementById('btnAttivazione1').style.position = 'absolute';
                document.getElementById('btnAttivazione1').style.left = '-100px';
                document.getElementById('btnAttivazione1').style.display = 'none';
            }

            if (document.getElementById('btnAttivazione2')) {
                document.getElementById('btnAttivazione2').style.visibility = 'hidden';
                document.getElementById('btnAttivazione2').style.position = 'absolute';
                document.getElementById('btnAttivazione2').style.left = '-100px';
                document.getElementById('btnAttivazione2').style.display = 'none';
            }

            if (document.getElementById('Rateizza')) {
                if (document.getElementById('Rateizza').value == '0') {
                    if (document.getElementById('ImageFunzioni')) {
                        menu2[0] = ''
                        menu2[1] = ''
                        //document.getElementById('ImageFunzioni').style.visibility = 'hidden';
                        //document.getElementById('ImageFunzioni').style.position = 'absolute';
                        //document.getElementById('ImageFunzioni').style.left = '-100px';
                        //document.getElementById('ImageFunzioni').style.display = 'none';
                    }
                }
            }

            //********************** 17-07-2012 ***********************
            if (document.getElementById('spostaAnnulla')) {
                if (document.getElementById('spostaAnnulla').value == '0') {
                    if (document.getElementById('ImageFunzioni')) {
                        menu2[2] = ''
                    }
                }
            }

            if (menu2[0] == '' && menu2[1] == '' && menu2[2] == '') {
                document.getElementById('ImageFunzioni').style.visibility = 'hidden';
                document.getElementById('ImageFunzioni').style.position = 'absolute';
                document.getElementById('ImageFunzioni').style.left = '-100px';
                document.getElementById('ImageFunzioni').style.display = 'none';
            }
            //********************** FINE 17-07-2012 ***********************


            // 16-10-2013 TRASFERIMENTO DA RU REGOLARE
            if (document.getElementById('controllaEsistBozza')) {
                if (document.getElementById('controllaEsistBozza').value == '0' || document.getElementById('lettura').value == '1') {
                    if (document.getElementById('ImageFunzioni')) {
                        menu2[3] = ''
                    }
                }
            }

            if (document.getElementById('idAU392')) {
                if (document.getElementById('idAU392').value == '0') {
                    if (document.getElementById('ImageFunzioni')) {
                        menu2[6] = ''
                    }
                }
            }



            if (menu2[0] == '' && menu2[1] == '' && menu2[2] == '' && menu2[3] == '' && menu2[4] == '') {
                document.getElementById('ImageFunzioni').style.visibility = 'hidden';
                document.getElementById('ImageFunzioni').style.position = 'absolute';
                document.getElementById('ImageFunzioni').style.left = '-100px';
                document.getElementById('ImageFunzioni').style.display = 'none';
            }


            //----------- 29-09-2017 Rendo invisibile la cella gialla se non c'è il tipo specifico -----------
            if (!document.getElementById('LBLErpModerato') && !document.getElementById('LBLABUSIVO') && !document.getElementById('LBLVIRTUALE')) {
                document.getElementById('tipoRU').style.visibility = 'hidden';
                document.getElementById('tipoRU2').style.visibility = 'hidden';
            }
            //-----------


            if (Stringa.substring(0, 11) == 'USI DIVERSI' || (Stringa.substring(0, 12) == 'LEGGE 431/98' && Stringa2 != 'D')) {

                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                if (document.getElementById('au_abusivi').value == '1') {

                    document.getElementById('rigaImgBando').style.visibility = 'hidden';
                    document.getElementById('rigaImgBando').style.position = 'absolute';
                    document.getElementById('rigaImgBando').style.left = '-100px';
                    document.getElementById('rigaImgBando').style.display = 'none';

                    document.getElementById('tblLocatari').style.visibility = 'hidden';
                    document.getElementById('tblLocatari').style.position = 'absolute';
                    document.getElementById('tblLocatari').style.left = '-100px';
                    document.getElementById('tblLocatari').style.display = 'none';

                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                    menu2[4] = '';

                }
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                else {

                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    //document.getElementById('imgAnagrafe').style.visibility = 'hidden';

                    document.getElementById('VisDichiarazione').style.visibility = 'hidden';
                    document.getElementById('VisDichiarazione').style.position = 'absolute';
                    document.getElementById('VisDichiarazione').style.left = '-100px';
                    document.getElementById('VisDichiarazione').style.display = 'none';

                    document.getElementById('VisDomanda').style.visibility = 'hidden';
                    document.getElementById('VisDomanda').style.position = 'absolute';
                    document.getElementById('VisDomanda').style.left = '-100px';
                    document.getElementById('VisDomanda').style.display = 'none';



                    if (Stringa.substring(0, 12) == 'LEGGE 431/98' && Stringa2 != 'D' /*&& Stringa2 != 'P'*/) {
                        document.getElementById('rigaImgBando').style.visibility = 'hidden';
                        document.getElementById('rigaImgBando').style.position = 'absolute';
                        document.getElementById('rigaImgBando').style.left = '-100px';
                        document.getElementById('rigaImgBando').style.display = 'none';

                        document.getElementById('tblLocatari').style.visibility = 'visible';
                        document.getElementById('iconeVSA').style.visibility = 'visible';
                        document.getElementById('Generale1_btnNewIstanza').style.visibility = 'visible';

                        document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                        document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                        document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                        document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                        // Caricamento AU massive per inserire Rat Straord
                        document.getElementById('imgScegliAU').style.visibility = 'visible';
                    } else {


                        document.getElementById('tblLocatari').style.visibility = 'hidden';
                        document.getElementById('tblLocatari').style.position = 'absolute';
                        document.getElementById('tblLocatari').style.left = '-100px';
                        document.getElementById('tblLocatari').style.display = 'none';

                        document.getElementById('tblBando').style.visibility = 'hidden';
                        document.getElementById('tblBando').style.position = 'absolute';
                        document.getElementById('tblBando').style.left = '-100px';
                        document.getElementById('tblBando').style.display = 'none';
                    }





                    menu2[4] = '';








                }
                //nuovoospite
            }





            //****** 11-05-2012 COD_TIPOLOGIA_CONTR_LOC='L43198', DEST_USO='D' ******
            if (Stringa.substring(0, 12) == 'LEGGE 431/98' && Stringa2 == 'D') {
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                if (document.getElementById('au_abusivi').value == '1') {

                    document.getElementById('rigaImgBando').style.visibility = 'hidden';
                    document.getElementById('rigaImgBando').style.position = 'absolute';
                    document.getElementById('rigaImgBando').style.left = '-100px';
                    document.getElementById('rigaImgBando').style.display = 'none';

                    document.getElementById('tblLocatari').style.visibility = 'hidden';
                    document.getElementById('tblLocatari').style.position = 'absolute';
                    document.getElementById('tblLocatari').style.left = '-100px';
                    document.getElementById('tblLocatari').style.display = 'none';

                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                    menu2[4] = '';

                }
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                else {


                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    //                document.getElementById('imgAnagrafe').style.visibility = 'hidden';
                    //                document.getElementById('imgAnagrafe').style.position = 'absolute';
                    //                document.getElementById('imgAnagrafe').style.left = '-100px';
                    //                document.getElementById('imgAnagrafe').style.display = 'none';

                    document.getElementById('VisDichiarazione').style.visibility = 'hidden';
                    document.getElementById('VisDichiarazione').style.position = 'absolute';
                    document.getElementById('VisDichiarazione').style.left = '-100px';
                    document.getElementById('VisDichiarazione').style.display = 'none';

                    document.getElementById('VisDomanda').style.visibility = 'hidden';
                    document.getElementById('VisDomanda').style.position = 'absolute';
                    document.getElementById('VisDomanda').style.left = '-100px';
                    document.getElementById('VisDomanda').style.display = 'none';


                    document.getElementById('iconeBando').style.visibility = 'hidden';
                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                    //                        document.getElementById('iconeBando').style.position = 'absolute';
                    //                        document.getElementById('iconeBando').style.left = '-100px';
                    //                        document.getElementById('iconeBando').style.display = 'none';

                    //document.getElementById('iconeAU').style.visibility = 'hidden';
                    //                        document.getElementById('iconeAU').style.position = 'absolute';
                    //                        document.getElementById('iconeAU').style.left = '-100px';
                    //                        document.getElementById('iconeAU').style.display = 'none';
                    menu2[4] = '';
                }

                //nuovoospite

            }


            if (Stringa1.substring(0, 8) != 'ALLOGGIO') {
                if (document.getElementById('au_abusivi').value == '1') {

                    document.getElementById('rigaImgBando').style.visibility = 'hidden';
                    document.getElementById('rigaImgBando').style.position = 'absolute';
                    document.getElementById('rigaImgBando').style.left = '-100px';
                    document.getElementById('rigaImgBando').style.display = 'none';

                    document.getElementById('tblLocatari').style.visibility = 'hidden';
                    document.getElementById('tblLocatari').style.position = 'absolute';
                    document.getElementById('tblLocatari').style.left = '-100px';
                    document.getElementById('tblLocatari').style.display = 'none';

                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                    menu2[4] = '';

                }
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                else {
                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgAnagrafe').style.visibility = 'hidden';
                    //                document.getElementById('imgAnagrafe').style.position = 'absolute';
                    //                document.getElementById('imgAnagrafe').style.left = '-100px';
                    //                document.getElementById('imgAnagrafe').style.display = 'none';

                    document.getElementById('VisDichiarazione').style.visibility = 'hidden';
                    document.getElementById('VisDichiarazione').style.position = 'absolute';
                    document.getElementById('VisDichiarazione').style.left = '-100px';
                    document.getElementById('VisDichiarazione').style.display = 'none';

                    document.getElementById('VisDomanda').style.visibility = 'hidden';
                    document.getElementById('VisDomanda').style.position = 'absolute';
                    document.getElementById('VisDomanda').style.left = '-100px';
                    document.getElementById('VisDomanda').style.display = 'none';

                    //                        document.getElementById('nuovoospite').style.visibility = 'hidden';
                    //                        document.getElementById('nuovoospite').style.position = 'absolute';
                    //                        document.getElementById('nuovoospite').style.left = '-100px';
                    //                        document.getElementById('nuovoospite').style.display = 'none';
                    document.getElementById('tblBando').style.visibility = 'hidden';
                    document.getElementById('tblBando').style.position = 'absolute';
                    document.getElementById('tblBando').style.left = '-100px';
                    document.getElementById('tblBando').style.display = 'none';
                    menu2[4] = '';

                }
            }

            if (Stringa.substring(0, 13) == 'NON ESISTENTE') {
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                if (document.getElementById('au_abusivi').value == '1') {

                    document.getElementById('rigaImgBando').style.visibility = 'hidden';
                    document.getElementById('rigaImgBando').style.position = 'absolute';
                    document.getElementById('rigaImgBando').style.left = '-100px';
                    document.getElementById('rigaImgBando').style.display = 'none';

                    document.getElementById('tblLocatari').style.visibility = 'hidden';
                    document.getElementById('tblLocatari').style.position = 'absolute';
                    document.getElementById('tblLocatari').style.left = '-100px';
                    document.getElementById('tblLocatari').style.display = 'none';

                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                    menu2[4] = '';

                }
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                else {

                    //                        document.getElementById('VisDichiarazione').style.visibility = 'hidden';
                    //                        document.getElementById('VisDomanda').style.visibility = 'hidden';
                    //                        document.getElementById('imgAnagrafe').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgAnagrafe').style.visibility = 'hidden';
                    //                document.getElementById('imgAnagrafe').style.position = 'absolute';
                    //                document.getElementById('imgAnagrafe').style.left = '-100px';
                    //                document.getElementById('imgAnagrafe').style.display = 'none';

                    document.getElementById('VisDichiarazione').style.visibility = 'hidden';
                    document.getElementById('VisDichiarazione').style.position = 'absolute';
                    document.getElementById('VisDichiarazione').style.left = '-100px';
                    document.getElementById('VisDichiarazione').style.display = 'none';

                    document.getElementById('VisDomanda').style.visibility = 'hidden';
                    document.getElementById('VisDomanda').style.position = 'absolute';
                    document.getElementById('VisDomanda').style.left = '-100px';
                    document.getElementById('VisDomanda').style.display = 'none';

                    document.getElementById('tblLocatari').style.visibility = 'hidden';
                    document.getElementById('tblLocatari').style.position = 'absolute';
                    document.getElementById('tblLocatari').style.left = '-100px';
                    document.getElementById('tblLocatari').style.display = 'none';

                    document.getElementById('tblBando').style.visibility = 'hidden';
                    document.getElementById('tblBando').style.position = 'absolute';
                    document.getElementById('tblBando').style.left = '-100px';
                    document.getElementById('tblBando').style.display = 'none';
                    menu2[4] = '';
                }
            }

            if (Stringa.substring(0, 11) == 'NESSUNA TIP') {
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                if (document.getElementById('au_abusivi').value == '1') {

                    document.getElementById('rigaImgBando').style.visibility = 'hidden';
                    document.getElementById('rigaImgBando').style.position = 'absolute';
                    document.getElementById('rigaImgBando').style.left = '-100px';
                    document.getElementById('rigaImgBando').style.display = 'none';

                    //                document.getElementById('tblLocatari').style.visibility = 'hidden';
                    //                document.getElementById('tblLocatari').style.position = 'absolute';
                    //                document.getElementById('tblLocatari').style.left = '-100px';
                    //                document.getElementById('tblLocatari').style.display = 'none';

                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                    menu2[4] = '';

                }
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                else {
                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgAnagrafe').style.visibility = 'hidden';
                    //                document.getElementById('imgAnagrafe').style.position = 'absolute';
                    //                document.getElementById('imgAnagrafe').style.left = '-100px';
                    //                document.getElementById('imgAnagrafe').style.display = 'none';

                    document.getElementById('VisDichiarazione').style.visibility = 'hidden';
                    document.getElementById('VisDichiarazione').style.position = 'absolute';
                    document.getElementById('VisDichiarazione').style.left = '-100px';
                    document.getElementById('VisDichiarazione').style.display = 'none';

                    document.getElementById('VisDomanda').style.visibility = 'hidden';
                    document.getElementById('VisDomanda').style.position = 'absolute';
                    document.getElementById('VisDomanda').style.left = '-100px';
                    document.getElementById('VisDomanda').style.display = 'none';

                    document.getElementById('tblLocatari').style.visibility = 'hidden';
                    document.getElementById('tblLocatari').style.position = 'absolute';
                    document.getElementById('tblLocatari').style.left = '-100px';
                    document.getElementById('tblLocatari').style.display = 'none';

                    document.getElementById('tblBando').style.visibility = 'hidden';
                    document.getElementById('tblBando').style.position = 'absolute';
                    document.getElementById('tblBando').style.left = '-100px';
                    document.getElementById('tblBando').style.display = 'none';
                    menu2[4] = '';
                }
            }

            //max
            if (Stringa.substring(0, 11) == 'CONCESSIONE') {
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                if (document.getElementById('au_abusivi').value == '1') {

                    document.getElementById('rigaImgBando').style.visibility = 'hidden';
                    document.getElementById('rigaImgBando').style.position = 'absolute';
                    document.getElementById('rigaImgBando').style.left = '-100px';
                    document.getElementById('rigaImgBando').style.display = 'none';

                    document.getElementById('tblLocatari').style.visibility = 'hidden';
                    document.getElementById('tblLocatari').style.position = 'absolute';
                    document.getElementById('tblLocatari').style.left = '-100px';
                    document.getElementById('tblLocatari').style.display = 'none';

                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                    menu2[4] = '';

                }
                //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
                else {
                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('imgAnagrafe').style.visibility = 'hidden';
                    //                document.getElementById('imgAnagrafe').style.position = 'absolute';
                    //                document.getElementById('imgAnagrafe').style.left = '-100px';
                    //                document.getElementById('imgAnagrafe').style.display = 'none';

                    document.getElementById('VisDichiarazione').style.visibility = 'hidden';
                    document.getElementById('VisDichiarazione').style.position = 'absolute';
                    document.getElementById('VisDichiarazione').style.left = '-100px';
                    document.getElementById('VisDichiarazione').style.display = 'none';

                    document.getElementById('VisDomanda').style.visibility = 'hidden';
                    document.getElementById('VisDomanda').style.position = 'absolute';
                    document.getElementById('VisDomanda').style.left = '-100px';
                    document.getElementById('VisDomanda').style.display = 'none';

                    document.getElementById('tblLocatari').style.visibility = 'hidden';
                    document.getElementById('tblLocatari').style.position = 'absolute';
                    document.getElementById('tblLocatari').style.left = '-100px';
                    document.getElementById('tblLocatari').style.display = 'none';

                    document.getElementById('tblBando').style.visibility = 'hidden';
                    document.getElementById('tblBando').style.position = 'absolute';
                    document.getElementById('tblBando').style.left = '-100px';
                    document.getElementById('tblBando').style.display = 'none';
                    menu2[4] = '';
                }
            }

//            if (Stringa.substring(0, 12) == 'LEGGE 431/98' && Stringa2 == 'P') {
//                document.getElementById('imgAnagrafe').style.visibility = 'visible';
//                document.getElementById('tblBando').style.visibility = 'visible';
//                document.getElementById('tblBando').style.position = 'absolute';
//                document.getElementById('tblBando').style.left = '10px';
//                document.getElementById('iconeBando').style.visibility = 'hidden';


//            }

            if (document.getElementById('lettura').value == '1') {
                document.getElementById('DRA').style.visibility = 'hidden';
                document.getElementById('DRA0').style.visibility = 'hidden';
                if (document.getElementById('imgDefProroga')) {
                    document.getElementById('imgDefProroga').style.visibility = 'hidden';
                }
                document.getElementById('IMG1NuovaBolletta').style.visibility = 'hidden';
                //                        document.getElementById('nuovoospite').style.visibility = 'hidden';
                document.getElementById('nuovoschema').style.visibility = 'hidden';


                var menu1 = new Array()
                menu1[0] = '<a href="javascript:Apri();">Visualizza stampe Contratti</a>'
                menu1[1] = '<a href="javascript:AllegatiContratto();">Visualizza Allegati</a>'
                menu1[2] = '<a href="javascript:AllegatiContrattoEx();">Visualizza Allegati ex gestore</a>'

                menuAttivazione[0] = '';
                menuAttivazione[1] = '';

                if (document.getElementById('ImgBolAttivazione')) {
                    document.getElementById('ImgBolAttivazione').style.visibility = 'hidden';
                }
                if (document.getElementById('Contact').value == '1') {
                    if (document.getElementById('ImageFunzioni')) {
                        document.getElementById('ImageFunzioni').style.visibility = 'hidden';
                    }
                }


                if (document.getElementById('Tab_Conduttore1_btnAggiungiCond')) {
                    document.getElementById('Tab_Conduttore1_btnAggiungiCond').style.visibility = 'hidden';
                }
                if (document.getElementById('Tab_Conduttore1_imgEliminaCond')) {
                    document.getElementById('Tab_Conduttore1_imgEliminaCond').style.visibility = 'hidden';
                }
                if (document.getElementById('Tab_Conduttore1_Img_DiventaComp')) {
                    document.getElementById('Tab_Conduttore1_Img_DiventaComp').style.visibility = 'hidden';
                }
                if (document.getElementById('Tab_Conduttore1_btnAggiungiComp')) {
                    document.getElementById('Tab_Conduttore1_btnAggiungiComp').style.visibility = 'hidden';
                }
                if (document.getElementById('Tab_Conduttore1_img_EliminaComp')) {
                    document.getElementById('Tab_Conduttore1_img_EliminaComp').style.visibility = 'hidden';
                }
                if (document.getElementById('Tab_Conduttore1_imgDiventaINT')) {
                    document.getElementById('Tab_Conduttore1_imgDiventaINT').style.visibility = 'hidden';
                }
                //if (document.getElementById('Tab_Conduttore1_img_EliminaOspite')) {
                //    document.getElementById('Tab_Conduttore1_img_EliminaOspite').style.visibility = 'hidden';
                //}

                if (document.getElementById('spostaAnnulla').value == '0') {
                    if (document.getElementById('ImageFunzioni')) {
                        menu2[2] = ''
                    }
                }

                if (document.getElementById('Tab_Registrazione1_imgAggAvviso')) {
                    document.getElementById('Tab_Registrazione1_imgAggAvviso').style.visibility = 'hidden';
                }
                if (document.getElementById('Tab_Registrazione1_ImgModifyAvviso')) {
                    document.getElementById('Tab_Registrazione1_ImgModifyAvviso').style.visibility = 'hidden';
                }
                if (document.getElementById('Tab_Registrazione1_ImgDeleteAvviso')) {
                    document.getElementById('Tab_Registrazione1_ImgDeleteAvviso').style.visibility = 'hidden';
                }
                if (document.getElementById('Tab_Registrazione1_imgVisAvviso')) {
                    document.getElementById('Tab_Registrazione1_imgVisAvviso').style.visibility = 'visible';
                }

            }

            if (document.getElementById('AULETTURA')) {
                if (document.getElementById('AULETTURA').value == '1') {
                    if (document.getElementById('Generale1_imgCreaAU')) {
                        document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                        document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                        document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                        document.getElementById('Generale1_imgCreaAU').style.display = 'none';
                    }
                }
            }

            if (document.getElementById('GLLETTURA')) {
                if (document.getElementById('GLLETTURA').value == '1') {
                    if (document.getElementById('tblLocatari')) {
                        if (document.getElementById('Generale1_btnNewIstanza')) {
                            document.getElementById('Generale1_btnNewIstanza').style.visibility = 'hidden';
                            document.getElementById('Generale1_btnNewIstanza').style.position = 'absolute';
                            document.getElementById('Generale1_btnNewIstanza').style.left = '-100px';
                            document.getElementById('Generale1_btnNewIstanza').style.display = 'none';
                        }
                    }
                }
            }


            if (document.getElementById('HStatoContratto').value != 'CHIUSO' && document.getElementById('opfiliale').value == '1' && document.getElementById('VIRTUALE').value != '1' && document.getElementById('lettura').value != '1') {

                document.getElementById('DRA0').style.visibility = 'visible';
                document.getElementById('ImageDocumentazione').style.visibility = 'visible';
                document.getElementById('Tab_Contratto1_txtDataDisdetta').readOnly = false;
                document.getElementById('Tab_Contratto1_txtDataDisdetta0').readOnly = false;
                document.getElementById('Tab_Contratto1_txtNotificaDisdetta').readOnly = false;
                document.getElementById('Tab_Contratto1_txtDataRiconsegna').readOnly = false;
            }

            if (document.getElementById('VIRTUALE').value == '1') {
                document.getElementById('DRA').style.visibility = 'hidden';
                document.getElementById('DRA0').style.visibility = 'hidden';


                //            document.getElementById('ImageDocumentazione').style.visibility = 'hidden';
                //            document.getElementById('ImageDocumentazione').style.position = 'absolute';
                //            document.getElementById('ImageDocumentazione').style.left = '-100px';
                //            document.getElementById('ImageDocumentazione').style.display = 'none';
                var menu1 = new Array()
                menu1[0] = '<a href="javascript:AllegatiContratto();">Visualizza Allegati</a>'
                menu1[1] = '<a href="javascript:AllegaFile();">Allega File</a>'


                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                document.getElementById('tblBando').style.visibility = 'hidden';
                document.getElementById('tblBando').style.position = 'absolute';
                document.getElementById('tblBando').style.left = '-100px';
                document.getElementById('tblBando').style.display = 'none';


                document.getElementById('tblLocatari').style.visibility = 'hidden';
                document.getElementById('tblLocatari').style.position = 'absolute';
                document.getElementById('tblLocatari').style.left = '-100px';
                document.getElementById('tblLocatari').style.display = 'none';

                menu2[4] = '';

                if (document.getElementById('imgRinnovoUSD')) {
                    document.getElementById('imgRinnovoUSD').style.visibility = 'hidden';
                    document.getElementById('imgRinnovoUSD').style.position = 'absolute';
                    document.getElementById('imgRinnovoUSD').style.left = '-100px';
                    document.getElementById('imgRinnovoUSD').style.display = 'none';
                }

                if (document.getElementById('imgCambioBox')) {
                    document.getElementById('imgCambioBox').style.visibility = 'hidden';
                    document.getElementById('imgCambioBox').style.position = 'absolute';
                    document.getElementById('imgCambioBox').style.left = '-100px';
                    document.getElementById('imgCambioBox').style.display = 'none';
                }

                if (document.getElementById('ImgCambioIntestazione')) {
                    document.getElementById('ImgCambioIntestazione').style.visibility = 'hidden';
                    document.getElementById('ImgCambioIntestazione').style.position = 'absolute';
                    document.getElementById('ImgCambioIntestazione').style.left = '-100px';
                    document.getElementById('ImgCambioIntestazione').style.display = 'none';
                }

            }

            if (document.getElementById('speseunita').value == '0') {
                document.getElementById('imgSpeseUnita').style.visibility = 'hidden';
                document.getElementById('imgSpeseUnita').style.position = 'absolute';
                document.getElementById('imgSpeseUnita').style.left = '-100px';
                document.getElementById('imgSpeseUnita').style.display = 'none';
            }
            else {
                document.getElementById('imgSpeseUnita').style.visibility = 'visible';
            }

            if (document.getElementById('HStatoContratto').value == 'CHIUSO') {
                //                        document.getElementById('ImageDocumentazione').style.visibility = 'hidden';
                //                        document.getElementById('ImageDocumentazione').style.position = 'absolute';
                //                        document.getElementById('ImageDocumentazione').style.left = '-100px';
                //                        document.getElementById('ImageDocumentazione').style.display = 'none';

                var menu1 = new Array()
                menu1[0] = '<a href="javascript:Apri();">Visualizza stampe Contratti</a>'
                menu1[1] = '<a href="javascript:AllegatiContratto();">Visualizza Allegati</a>'
                menu1[2] = '<a href="javascript:AllegatiContrattoEx();">Visualizza Allegati ex gestore</a>'
                menu1[3] = '<a href="javascript:AllegaFile();">Allega File</a>'



                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                document.getElementById('Generale1_imgCreaAU').style.display = 'none';


                //******** 27-07/2017 Segnalazione 1431/2017 Sblocco dell'inserimento istanze Gestione Locatari a contratto CHIUSO
                //document.getElementById('imgNuovaDom').style.visibility = 'hidden';
                //document.getElementById('imgNuovaDom').style.position = 'absolute';
                //document.getElementById('imgNuovaDom').style.left = '-100px';
                //document.getElementById('imgNuovaDom').style.display = 'none';



                menu2[4] = '';




            }

            if (document.getElementById('HStatoContratto').value == 'BOZZA') {

                document.getElementById('tblLocatari').style.visibility = 'hidden';
                document.getElementById('tblLocatari').style.position = 'absolute';
                document.getElementById('tblLocatari').style.left = '-100px';
                document.getElementById('tblLocatari').style.display = 'none';

                menu2[4] = '';
            }



            if (document.getElementById('bloccato').value == '1') {
                menu2[4] = '';
                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                if (document.getElementById('imgDefProroga')) {
                    document.getElementById('imgDefProroga').style.visibility = 'hidden';
                }

                if (document.getElementById('tblLocatari')) {
                    if (document.getElementById('Generale1_btnNewIstanza')) {
                        document.getElementById('Generale1_btnNewIstanza').style.visibility = 'hidden';
                        document.getElementById('Generale1_btnNewIstanza').style.position = 'absolute';
                        document.getElementById('Generale1_btnNewIstanza').style.left = '-100px';
                        document.getElementById('Generale1_btnNewIstanza').style.display = 'none';
                        menu2[4] = '';
                    }
                }
                if (document.getElementById('spostaAnnulla').value == '1') {
                    if (document.getElementById('ImageFunzioni')) {
                        menu2[2] = ''
                    }
                }

                //                        document.getElementById('imgAnagrafe').style.visibility = 'hidden';
                //                        document.getElementById('imgAnagrafe').style.position = 'absolute';
                //                        document.getElementById('imgAnagrafe').style.left = '-100px';
                //                        document.getElementById('imgAnagrafe').style.display = 'none';
                //                        document.getElementById('rigaImgAnagr').style.visibility = 'hidden';
                //                        document.getElementById('rigaImgAnagr').style.position = 'absolute';
                //                        document.getElementById('rigaImgAnagr').style.left = '-100px';
                //                        document.getElementById('rigaImgAnagr').style.display = 'none';
            }

            if (document.getElementById('RinnovoUSD').value == '0') {
                document.getElementById('imgRinnovoUSD').style.visibility = 'hidden';
                document.getElementById('imgRinnovoUSD').style.position = 'absolute';
                document.getElementById('imgRinnovoUSD').style.left = '-100px';
                document.getElementById('imgRinnovoUSD').style.display = 'none';
            }
            if (document.getElementById('CambioBox').value == '0') {
                document.getElementById('imgCambioBox').style.visibility = 'hidden';
                document.getElementById('imgCambioBox').style.position = 'absolute';
                document.getElementById('imgCambioBox').style.left = '-100px';
                document.getElementById('imgCambioBox').style.display = 'none';
            }

            if (document.getElementById('CambioBox').value == '1' || document.getElementById('HStatoContratto').value == 'CHIUSO' || document.getElementById('HStatoContratto').value == 'BOZZA') {
                if (document.getElementById('ImgCambioIntestazione')) {
                    document.getElementById('ImgCambioIntestazione').style.visibility = 'hidden';
                    document.getElementById('ImgCambioIntestazione').style.position = 'absolute';
                    document.getElementById('ImgCambioIntestazione').style.left = '-100px';
                    document.getElementById('ImgCambioIntestazione').style.display = 'none';
                }

            }

            //CONDIZIONE PER ABUSIVI
            if (document.getElementById('au_abusivi').value == '1') {

                if (document.getElementById('Generale1_imgCreaAU').style.visibility != 'hidden') {
                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';
                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                }
                else {
                    document.getElementById('iconeAU').style.visibility = 'visible';
                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                    document.getElementById('tblLocatari').style.visibility = 'visible';
                    document.getElementById('iconeVSA').style.visibility = 'visible';
                    document.getElementById('Generale1_btnNewIstanza').style.visibility = 'visible';
                }

            }

            if (Stringa.substring(15, 12) == '392') {
                document.getElementById('imgScegliAU').style.visibility = 'visible';
            }


            //CONDIZIONE PER ICONE ASS.TEMP e PROROGA
            if (document.getElementById('Tab_Contratto1_chkTemporanea').checked == true) {
                if (document.getElementById('prorogati').value == '1' || document.getElementById('assegn_def').value == '1') {
                    document.getElementById('rigaImgAnagr').style.visibility = 'visible';
                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';

                    document.getElementById('iconeAU').style.visibility = 'visible';
                    document.getElementById('imgAnagrafe').style.visibility = 'visible';
                    //                            document.getElementById('imgAnagrafe').style.display = 'block';
                    //                            document.getElementById('imgAnagrafe').style.position = 'absolute';
                    //                            document.getElementById('imgAnagrafe').style.left = '200px';
                    document.getElementById('imgScegliAU').style.visibility = 'visible';
                }
                if (document.getElementById('tblLocatari').style.visibility == 'hidden') {
                    document.getElementById('tblLocatari').style.visibility = 'visible';
                    document.getElementById('tblLocatari').style.display = 'block';
                }
                document.getElementById('imgDefProroga').style.visibility = 'visible';
            }

            document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
            document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
            document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
            document.getElementById('Generale1_imgCreaAU').style.display = 'none';

            //CONDIZIONE PER ICONE GEST.LOCATARI ASS.TEMP


            function VisualizzaDettagliCanone() {
                document.getElementById('DettagliCanone').style.visibility = 'visible';
            }

            function NascondiDettagliCanone() {
                document.getElementById('DettagliCanone').style.visibility = 'hidden';
            }
            function MostraStoricoReg() {
                if (document.getElementById('mostraStoricoReg').value == '1') {
                    document.getElementById('Tab_Registrazione1_lblStoricoReg').style.display = 'block';

                }
                else {
                    document.getElementById('Tab_Registrazione1_lblStoricoReg').style.display = 'none';

                }
            }


            function mycallbackform(e, v, m, f) {
                var espressione = /^[0-9]{2}\/[0-9]{2}\/[0-9]{4}$/;
                var Controlla_Importo = /^\d+(\,\d{1,2})?$/;
                if (v != undefined) {
                    if (v == 1) {
                        var errore;
                        an = m.children('#RDataSloggio');
                        if (f.RDataSloggio == "") {
                            an.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an.css("border", "solid #7f9db9 1px");
                        }
                        if (!espressione.test(f.RDataSloggio)) {
                            an.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an.css("border", "solid #7f9db9 1px");
                        }
                        if (f.RDataSloggio != "") {
                            var data1 = document.getElementById('Tab_Contratto1_txtDataDecorrenza').value;
                            var data2 = f.RDataSloggio;
                            data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
                            data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
                            if (data2str - data1str < 0) {
                                errore = '1';
                                an.css("border", "solid #ff0000 1px");
                            }
                            else {
                                an.css("border", "solid #7f9db9 1px");
                            }
                        }
                        an1 = m.children('#RPgData');
                        if (f.RPgData == "") {
                            an1.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an1.css("border", "solid #7f9db9 1px");
                        }
                        if (!espressione.test(f.RPgData)) {
                            an1.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an1.css("border", "solid #7f9db9 1px");
                        }
                        an2 = m.children('#RPgMilano');
                        if (f.RPgMilano == "") {
                            an2.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an2.css("border", "solid #7f9db9 1px");
                        }
                        an3 = m.children('#RImportoCanone');
                        if (!Controlla_Importo.test(f.RImportoCanone)) {
                            an3.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an3.css("border", "solid #7f9db9 1px");
                        }
                        if (errore == '1') {
                            return false;
                        }
                        document.getElementById('RinnovoDataChiusura').value = f.RDataSloggio;
                        document.getElementById('RinnovoDataPG').value = f.RPgData;
                        document.getElementById('RinnovoNumeroPG').value = f.RPgMilano;
                        document.getElementById('RinnovoCanone').value = f.RImportoCanone;
                        document.getElementById('FaiRinnovoUSD').value = "1";
                        document.getElementById('btnRinnovoUSD').click();
                        return true;
                    }
                    if (v == 2) {
                        document.getElementById('FaiRinnovoUSD').value = "0";
                    }
                }
            }


            function mycallbackform1(e, v, m, f) {
                var espressione = /^[0-9]{2}\/[0-9]{2}\/[0-9]{4}$/;
                var Controlla_Importo = /^\d+(\,\d{1,2})?$/;


                if (v != undefined) {
                    if (v == 1) {
                        var errore;
                        an = m.children('#RDataSloggio');
                        if (f.RDataSloggio == "") {
                            an.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an.css("border", "solid #7f9db9 1px");
                        }
                        if (!espressione.test(f.RDataSloggio)) {
                            an.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an.css("border", "solid #7f9db9 1px");
                        }
                        if (f.RDataSloggio != "") {
                            var data1 = document.getElementById('Tab_Contratto1_txtDataDecorrenza').value;
                            var data2 = f.RDataSloggio;
                            data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
                            data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
                            if (data2str - data1str < 0) {
                                errore = '1';
                                an.css("border", "solid #ff0000 1px");
                            }
                            else {
                                an.css("border", "solid #7f9db9 1px");
                            }
                        }
                        an1 = m.children('#RPgData');
                        if (f.RPgData == "") {
                            an1.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an1.css("border", "solid #7f9db9 1px");
                        }
                        if (!espressione.test(f.RPgData)) {
                            an1.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an1.css("border", "solid #7f9db9 1px");
                        }
                        an2 = m.children('#RPgMilano');
                        if (f.RPgMilano == "") {
                            an2.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an2.css("border", "solid #7f9db9 1px");
                        }
                        an3 = m.children('#RImportoCanone');
                        if (!Controlla_Importo.test(f.RImportoCanone)) {
                            an3.css("border", "solid #ff0000 1px");
                            errore = '1';
                        }
                        else {
                            an3.css("border", "solid #7f9db9 1px");
                        }
                        if (errore == '1') {
                            return false;
                        }
                        document.getElementById('RinnovoDataChiusura').value = f.RDataSloggio;
                        document.getElementById('RinnovoDataPG').value = f.RPgData;
                        document.getElementById('RinnovoNumeroPG').value = f.RPgMilano;
                        document.getElementById('RinnovoCanone').value = f.RImportoCanone;
                        document.getElementById('FaiCambioBox').value = "1";
                        document.getElementById('btnCambioIntBox').click();
                        return true;
                    }
                    if (v == 0) {
                        jQuery.prompt.goToState('state0');
                        return false
                    }

                }
            }

            function ScegliSpostamAnnull() {
                var txt = 'SELEZIONARE L\'AZIONE CHE DI DESIDERA SVOLGERE:<br /><br /><input type="radio" id = "variazDecorr" name="rdbScegli" value="variazioneDecorr"/>Variazione decorrenza<br /><input type="radio" id = "sposta" name="rdbScegli" value="sposta"/>Spostamento contratto da UI ad altra UI<br /><input type="radio" id = "annullam" name="rdbScegli" value="annullam"/>Annullamento Contratto';
                jQuery.prompt(txt, {
                    submit: mycallSpostamAnnull,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }
            function mycallSpostamAnnull(e, v, m, f) {
                var scelta;
                var Stringa;
                var Stringa2;
                var prov_ass;
                var tipo;

                Stringa = document.getElementById('Tab_Contratto1_txtDescrcontratto').value;
                Stringa2 = document.getElementById('Tab_Contratto1_cmbDestUso').value;
                prov_ass = <%= TipoContratto %>

            if (Stringa == "Equo canone 392/78") {
                    tipo = "EQC392"
                }
                if (Stringa == "E.R.P.") {
                    if (prov_ass == "10" || prov_ass == "12") {
                        tipo = prov_ass
                    }
                    else {
                        tipo = "ERP"
                    }
                }
                if (Stringa == "Legge 431/98") {
                    if (Stringa2 == "D") {
                        tipo = "L43198_ART15"
                    }
                    else {
                        tipo = "L43198"
                    }
                }
                if (Stringa == "Nessuna Tipologia (O.A.)") {
                    tipo = "NONE"
                }
                if (Stringa == "Usi diversi") {
                    tipo = "USD"
                }


                if (v != undefined)

                    if (v != '2') {
                        if (f.rdbScegli != undefined) {
                            if (f.rdbScegli == "sposta") {
                                scelta = "1"
                            }
                            if (f.rdbScegli == "annullam") {
                                scelta = "2"
                            }
                            if (f.rdbScegli == "variazioneDecorr") {
                                scelta = "3"
                            }
                            if (scelta == "1") {
//                            if (tipo == 'ERP')
//                            {
//                                window.open('SpostamentoRU/Sposta_Annulla_ERP.aspx?COD=<%=CodContratto %>&TIPO=' + tipo + '&PROV='+ prov_ass +'&SCELTA=' + scelta + '', 'SpostaAnnullE', 'height=450,top=150,left=250,width=800');
//                            }
//                            else
//                            {
//                                window.open('SpostamentoRU/Sposta_Annulla.aspx?COD=<%=CodContratto %>&TIPO=' + tipo + '&SCELTA=' + scelta + '', 'SpostaAnnull', 'height=450,top=150,left=250,width=800');
                                //                            }
                                window.open('SpostamentoRU/Sposta_Annulla_ERP.aspx?COD=<%=CodContratto1 %>&TIPO=' + tipo + '&PROV=' + prov_ass + '&SCELTA=' + scelta + '', 'Spostam', 'height=550,top=150,left=250,width=800');

                            }
                            if (scelta == "2") {
                                window.open('SpostamentoRU/Sposta_Annulla_ERP.aspx?COD=<%=CodContratto1 %>&TIPO=' + tipo + '&PROV=' + prov_ass + '&SCELTA=' + scelta + '', 'Spostam', 'height=550,top=150,left=250,width=800');
                            }
                            if (scelta == "3") {
                                //alert('Funzione non disponibile!');
                                window.open('SpostamentoRU/Sposta_Annulla_ERP.aspx?COD=<%=CodContratto1 %>&TIPO=' + tipo + '&PROV=' + prov_ass + '&SCELTA=' + scelta + '', 'Spostam', 'height=580,top=150,left=250,width=800');
                            }
                        }
                        else {
                            alert('Selezionare la funzione che si intende eseguire!');
                        }
                    }

                return true;
            }

            function ApriRinnovoUSD() {
                var txt = 'RINNOVO CONTRATTO <%=CodContratto1 %><br />Eventuali somme ancora dovute saranno inserite nella bolletta di fine contratto.<br /><br />Data Chiusura del contratto (gg/mm/aaaa) NON PRIMA DEL ' + document.getElementById('Tab_Contratto1_txtDataDecorrenza').value + ':<br /><input type="text" id="RDataSloggio" name="RDataSloggio" value="" size="10" onkeypress="CompletaData(event,this);"/><br />Importo Canone di Locazione Annuo (2 cifre decimali separati da ,):<br /><input type="text" id="RImportoCanone" name="RImportoCanone" value="" size ="10" onchange="valid(this);AutoDecimal2(this);SostPuntVirg(event, this);"/><br />P.G. Comune di Milano Autorizzazione al Rinnovo:<br /><input type="text" id="RPgMilano" name="RPgMilano" value="' + document.getElementById('Tab_Contratto1_txtDelibera').value + '" size="10"/><br />Data Ricevimento al Gestore autorizzazione al Rinnovo (gg/mm/aaaa):<br /><input type="text" id="RPgData" name="RPgData" value="' + document.getElementById('Tab_Contratto1_txtDataDelibera').value + '" size="10" onkeypress="CompletaData(event,this);"/>';
                jQuery.prompt(txt, {
                    submit: mycallbackform,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }

            //28-05-2014 Cambio Intestazione USD
            function ApriCambioUSD() {

                //disabled readonly - Selezione opzionale come da rich. SD 1935/2017
                var txt = 'CAMBIO INTESTAZIONE CONTRATTO <%=CodContratto1 %><br /><br />Data Avvenuta Cessione Azienda (gg/mm/aaaa):<br /><input type="text" id="RDataRicez" name="RDataRicez" value="" size="10" onkeypress="CompletaData(event,this);"/><br />Importo Canone di Locazione Annuo (2 cifre decimali separati da ,):<br /><input type="text" id="RImpCanone" name="RImpCanone" value="" size ="10" onchange="valid(this);AutoDecimal2(this);SostPuntVirg(event, this);"/><br /><br /><input id="chkRestituzDepos" name="chkRestituzDepos" type="checkbox" checked="checked" value="" /> Restituzione Deposito Cauzionale<br /><br /><input id="chkNuovoDepos" name="chkNuovoDepos" type="checkbox" checked="checked" value=""  /> Emissione Nuova Bolletta di Deposito Cauzionale';

                jQuery.prompt(txt, {
                    submit: mycallbackformCAIN,
                    buttons: { Procedi: '1', Annulla: '2' },
                    show: 'slideDown',
                    focus: 0
                });
            }

            function mycallbackformCAIN(e, v, m, f) {
                var restDepCauz;
                var newDepCauz;
                var errore = '0';
                if (v != undefined) {
                    if (v != '2') {
                        if (f.RDataRicez == "") {
                            errore = '1';
                        }
                        if (f.RImpCanone == "") {
                            errore = '1';
                        }
                        if (chkRestituzDepos.checked == true) {
                            restDepCauz = "1";
                        }
                        else {
                            restDepCauz = "0";
                        }
                        if (chkNuovoDepos.checked == true) {
                            newDepCauz = "1";
                        }
                        else {
                            newDepCauz = "0";
                        }
                        //Eliminata obbligatorietà come da rich. SD 1935/2017
                        /*if (errore == '1') {
                            alert('Dati mancanti!');
                            return false;
                        }*/
                        //else {
                            if (restDepCauz == '1') {
                                window.open('CambioIntUSDFase1.aspx?IDT=<%= lIdConnessione %>&IDC=<%= lIdContratto %>&DATARIC=' + f.RDataRicez + '&IMPC=' + f.RImpCanone + '&RESTDEP=' + restDepCauz + '&NEWDEP=' + newDepCauz + '', 'Cambio', 'height=598,top=0,left=0,width=800,scrollbars=no');
                            }
                            else {
                                window.open('CambioIntUSD.aspx?IDT=<%= lIdConnessione %>&IDC=<%= lIdContratto %>&DATARIC=' + f.RDataRicez + '&IMPC=' + f.RImpCanone + '&RESTDEP=' + restDepCauz + '&NEWDEP=' + newDepCauz + '', 'Cambio', 'height=598,top=0,left=0,width=800,scrollbars=no');
                            }
                        //}
                    }
                }
            }
            //28-05-2014 FINE Cambio Intestazione USD

            function SettaCFGlobale() {

            }

            function CercaCodiceF() {
                CFGlobale = '';
                window.showModalDialog('Anagrafica/RicercaINT.aspx', window, 'status:no;dialogWidth:500px;dialogHeight:420px;dialogHide:true;help:no;scroll:no');
                document.getElementById('CFCambioBox').value = CFGlobale;
                document.getElementById('IDRinnovoBoxUSD').value = IDGlobale;

            }


            function codiceFISCALE(cfins) {
                var cf = cfins.toUpperCase();
                var cfReg = /^[A-Z]{6}\d{2}[A-Z]\d{2}[A-Z]\d{3}[A-Z]$/;
                if (!cfReg.test(cf))
                    return false;
                var set1 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var set2 = "ABCDEFGHIJABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var setpari = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var setdisp = "BAKPLCQDREVOSFTGUHMINJWZYX";
                var s = 0;
                for (i = 1; i <= 13; i += 2)
                    s += setpari.indexOf(set2.charAt(set1.indexOf(cf.charAt(i))));
                for (i = 0; i <= 14; i += 2)
                    s += setdisp.indexOf(set2.charAt(set1.indexOf(cf.charAt(i))));
                if (s % 26 != cf.charCodeAt(15) - 'A'.charCodeAt(0))
                    return false;
                return true;
            }

            function ApriCambioBOX() {
                var MioTxt = 'CAMBIO INTESTAZIONE BOX CONTRATTO <%=CodContratto1 %><br />Inserisci il codice fiscale dell\'Intestatario del rapporto ERP a cui si deve intestare il nuovo rapporto.<br /><br />Codice Fiscale:<br /><input type="text" id="CFCambioBox" name="CFCambioBox" value="" style="font-family: arial; font-size: 10pt; width: 150px;" readonly="readonly"/><img id="CercaCodiceF" alt="Clicca per cercare un intestatario" src="../Contratti/Immagini/Search_16x16.png" style="cursor: pointer;" onclick="CercaCodiceF();"/>';
                var MioTxt1 = 'CAMBIO INTESTAZIONE BOX CONTRATTO <%=CodContratto1 %><br />Eventuali somme ancora dovute saranno inserite nella bolletta di fine contratto.<br /><br />Data Chiusura del contratto (gg/mm/aaaa) NON PRIMA DEL ' + document.getElementById('Tab_Contratto1_txtDataDecorrenza').value + ':<br /><input type="text" id="RDataSloggio" name="RDataSloggio" value="" size="10" onkeypress="CompletaData(event,this);"/><br />Importo Canone di Locazione Annuo (2 cifre decimali separati da ,):<br /><input type="text" id="RImportoCanone" name="RImportoCanone" value="" size ="10" onchange="valid(this);AutoDecimal2(this);SostPuntVirg(event, this);"/><br />P.G. Comune di Milano Autorizzazione al Cambio Intestazione:<br /><input type="text" id="RPgMilano" name="RPgMilano" value="' + document.getElementById('Tab_Contratto1_txtDelibera').value + '" size="10"/><br />Data Ricevimento al Gestore autorizzazione al cambio Intestazione (gg/mm/aaaa):<br /><input type="text" id="RPgData" name="RPgData" value="' + document.getElementById('Tab_Contratto1_txtDataDelibera').value + '" size="10" onkeypress="CompletaData(event,this);"/>';
                var errore;
                errore = '0';
                var EseguiPassaggi = {
                    state0: {
                        html: MioTxt,
                        buttons: { Annulla: 0, Prosegui: 1 },
                        focus: 0,
                        submit: function (e, v, m, f) {
                            if (v == 1) {
                                var Controlla_CF;
                                an3 = m.children('#CFCambioBox');
                                Controlla_CF = codiceFISCALE(f.CFCambioBox);
                                if (Controlla_CF == false) {
                                    an3.css("border", "solid #ff0000 1px");
                                    errore = '1'
                                }
                                else {
                                    an3.css("border", "solid #7f9db9 1px");
                                    errore = '0';
                                }

                                if (errore == '1') {
                                    //return false;
                                }
                                else {
                                    document.getElementById('CFRinnovoBoxUSD').value = f.CFCambioBox;
                                    jQuery.prompt.goToState('state1');
                                }
                                return false;
                            }
                            else {

                                return true;
                            }
                        }
                    },
                    state1: {
                        html: MioTxt1,
                        buttons: { Indietro: 0, Salva: 1 },
                        focus: 0,
                        submit: mycallbackform1
                    }
                };
                jQuery.prompt(EseguiPassaggi);
            }

            if (document.getElementById('dvvvPre')) {
                document.getElementById('dvvvPre').style.visibility = 'hidden';
            }

            //if (document.getElementById('Attesa')) {
            //    document.getElementById('Attesa').style.visibility = 'hidden';
            //}

            if (document.getElementById('RateizInCorso').value == '1') {
                menu2[1] = '<a href="javascript:ApriDetRat();">Visualizza Rateizzazioni</a>'
            }

            if (document.getElementById('FL_PAG_MANUALI').value == '1') {
                menu2[5] = '<a href="javascript:PgManuale();">Pagamento Manuale</a>'
            }

            if (document.getElementById('FL_PAG_MAN_RUOLI').value == '1') {
                menu2[8] = '<a href="javascript:PgManualeRuoli();">Pagamento Ruoli</a>'

            }

            if (document.getElementById('FL_PAG_MAN_ING').value == '1') {
                menu2[9] = '<a href="javascript:PgManualeIng();">Pagamento Ingiunzioni</a>'

            }

            if (document.getElementById('idAU392').value != '0' && document.getElementById('traformaRUstep2').value == '0') {
                menu2[6] = '<a href="javascript:TrasformaDa392();">Trasforma Contratto</a>'
            }
            if (document.getElementById('traformaRUstep2').value != '0') {
                menu2[7] = '<a href="javascript:TrasformaDa392_bis();">Prosegui Trasforma Contratto</a>'
            }

            //21/10/2013 nasconde/visualizza header html del tab bollette

            function VisualizzaDivHtml() {

                //           if (document.getElementById('Tab_Bollette1_divGestCon').value=='0'){
                //              if (document.getElementById('divIntestPartGestSenzaInfo') && document.getElementById('divIntestPartGestConInfo')){
                //              document.getElementById('divIntestPartGestSenzaInfo').style.display='block';
                //              document.getElementById('divIntestPartGestConInfo').style.display='none';

                //              }
                //           }
                //           else {
                //              if (document.getElementById('divIntestPartGestSenzaInfo') && document.getElementById('divIntestPartGestConInfo')){
                //              document.getElementById('divIntestPartGestSenzaInfo').style.display='none';
                //              document.getElementById('divIntestPartGestConInfo').style.display='block';
                //              }
                //           }

                //          if (document.getElementById('Tab_Bollette1_divContabCon').value=='0'){
                //              if (document.getElementById('divContSenzaInfo') && document.getElementById('divContConInfo')){
                //              document.getElementById('divContSenzaInfo').style.display='block';
                //              document.getElementById('divContConInfo').style.display='none';
                //              }
                //           }
                //           else {
                //               if (document.getElementById('divContSenzaInfo') && document.getElementById('divContConInfo')){
                //              document.getElementById('divContSenzaInfo').style.display='none';
                //              document.getElementById('divContConInfo').style.display='block';
                //              }
                //           }
            }
            function CreaBollettino1() {
                var chiediConferma;
                chiediConferma = window.confirm("Attenzione...Sei sicuro di voler procedere con l\'emissione del bollettino n. 1 (Imposta di registro, Cauzione, Spese Istruttoria, Anticipo Spese)?");
                if (chiediConferma == true) {
                    document.getElementById('TXTATTIVA').value = '1';
                    document.getElementById('btnAttivazione1').click();
                }
                else {
                    document.getElementById('TXTATTIVA').value = '0';
                }
            }

            function CreaBollettino2() {
                var chiediConferma;
                chiediConferma = window.confirm("Attenzione...Sei sicuro di voler procedere con l\'emissione del bollettino n. 2 (Bollo, Canone, Voci schema)?");
                if (chiediConferma == true) {
                    document.getElementById('TXTATTIVA').value = '1';
                    document.getElementById('btnAttivazione2').click();
                }
                else {
                    document.getElementById('TXTATTIVA').value = '0';
                }
            }

            function StampaCdp(idb) {
                window.open('StampaCDP_New.aspx?ID=' + idb, 'Cdp', 'height=380,width=550,scrollbars=no');
            }

            function ApriSchedaArchivio(C) {
                today = new Date();
                var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();
                popupWindow = window.open('../ARCHIVIO/DatiContratto.aspx?LT=' + C + '&ID=<%=lIdContratto %> &COD=<%=CodContratto1 %>', Titolo, 'height=550,width=700');
                popupWindow.focus();
            }

            function AggiungiAvviso() {

                dialogArgs = new MyDialogArguments();
                dialogArgs.StringValue = '';
                dialogArgs.Sender = window;
                var dialogResults = window.showModalDialog('InserimentoAvviso.aspx?IDCONN=' + <%=lIdConnessione %> + '&PROV=1' + '&IDRIF=' + <%=lIdContratto %> , window, 'status:no;dialogWidth:750px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');

                if (dialogResults != undefined) {
                    if (dialogResults == '1') {
                        document.getElementById('imgSalva').click();
                    }
                    if (dialogResults == '2') {
                        document.getElementById('txtModificato').value = '1';

                    }
                }
            }

            function ModificaAvviso() {

                if (document.getElementById('idAvviso').value == '-1') {
                    alert('Selezionare una riga dalla lista!');
                }
                else {
                    dialogArgs = new MyDialogArguments();
                    dialogArgs.StringValue = '';
                    dialogArgs.Sender = window;
                    var dialogResults = window.showModalDialog('InserimentoAvviso.aspx?MOD=1&IDCONN=' + <%=lIdConnessione %> + '&IDAVVISO=' + document.getElementById('idAvviso').value + '&PROV=1' + '&IDRIF=' + <%=lIdContratto %> , window, 'status:no;dialogWidth:750px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');

                    if (dialogResults != undefined) {
                        if (dialogResults == '1') {
                            document.getElementById('imgSalva').click();
                        }
                        if (dialogResults == '2') {
                            document.getElementById('txtModificato').value = '1';

                        }
                    }
                }
            }

            function VisualizzaAvviso() {
                if (document.getElementById('idAvviso').value == '-1') {
                    alert('Selezionare una riga dalla lista!');
                }
                else {
                    dialogArgs = new MyDialogArguments();
                    dialogArgs.StringValue = '';
                    dialogArgs.Sender = window;
                    var dialogResults = window.showModalDialog('InserimentoAvviso.aspx?MOD=3&IDCONN=' + <%=lIdConnessione %> + '&IDAVVISO=' + document.getElementById('idAvviso').value + '&PROV=1' + '&IDRIF=' + <%=lIdContratto %> , window, 'status:no;dialogWidth:750px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');


                }
            }

            function EliminaAvviso() {
                if ((document.getElementById('idAvviso').value != '-1') && (document.getElementById('idAvviso').value != '')) {
                    var sicuro = window.confirm('Sei sicuro di voler eliminare?');
                    if (sicuro == true) {
                        document.getElementById('USCITA').value = '1';
                        document.getElementById('Tab_Registrazione1_HConferma').value = '1';
                    }
                    else {
                        document.getElementById('USCITA').value = '1';
                        document.getElementById('Tab_Registrazione1_HConferma').value = '0';
                    }
                }
                else {
                    document.getElementById('USCITA').value = '1';
                    document.getElementById('Tab_Registrazione1_HConferma').value = '0';
                    alert('Selezionare un elemento dalla lista!');
                }
            }

            function AggiungiNote() {

                dialogArgs = new MyDialogArguments();
                dialogArgs.StringValue = '';
                dialogArgs.Sender = window;
                var dialogResults = window.showModalDialog('../InserimentoNote.aspx?IDCONN=' + <%=lIdConnessione %> + '&PROV=1' + '&IDRIF=' + <%=lIdContratto %> , window, 'status:no;dialogWidth:450px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');

                if (dialogResults != undefined) {
                    if (dialogResults == '1') {
                        document.getElementById('imgSalva').click();
                    }
                    if (dialogResults == '2') {
                        document.getElementById('txtModificato').value = '1';
                    }
                }
            }

            function ModificaNote() {

                if (document.getElementById('idNota').value == '-1') {
                    alert('Selezionare una riga dalla lista!');
                }
                else {
                    dialogArgs = new MyDialogArguments();
                    dialogArgs.StringValue = '';
                    dialogArgs.Sender = window;
                    var dialogResults = window.showModalDialog('../InserimentoNote.aspx?MOD=1&IDCONN=' + <%=lIdConnessione %> + '&IDNOTA=' + document.getElementById('idNota').value + '&PROV=1' + '&IDRIF=' + <%=lIdContratto %> , window, 'status:no;dialogWidth:450px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');

                    if (dialogResults != undefined) {
                        if (dialogResults == '1') {
                            document.getElementById('imgSalva').click();
                        }
                        if (dialogResults == '2') {
                            document.getElementById('txtModificato').value = '1';
                        }
                    }
                }
            }

            var r = {
                'special': /[\W]/g,
                'quotes': /['\''&'\"']/g,
                'notnumbers': /[^\d\,]/g
            }

            function valid(o) {
                //o.value = o.value.replace(r['notnumbers'], '');
                o.value = o.value.replace('.', ',');
            }

            function AutoDecimal2(obj) {

                obj.value = obj.value.replace('.', '');
                if (obj.value.replace(',', '.') != 0) {
                    var a = obj.value.replace(',', '.');
                    a = parseFloat(a).toFixed(2)
                    if (a != 'NaN') {
                        if (a.substring(a.length - 3, 0).length >= 4) {
                            var decimali = a.substring(a.length, a.length - 2);
                            var dascrivere = a.substring(a.length - 3, 0);
                            var risultato = '';
                            while (dascrivere.replace('-', '').length >= 4) {
                                //risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                                risultato = dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                                dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                            }
                            risultato = dascrivere + risultato + ',' + decimali;
                            //document.getElementById(obj.id).value = a.replace('.', ',')
                            document.getElementById(obj.id).value = risultato;
                        }
                        else {
                            document.getElementById(obj.id).value = a.replace('.', ',');
                        }

                    }
                    else
                        document.getElementById(obj.id).value = '';
                }
            }
            function SostPuntVirg(e, obj) {
                var keyPressed;
                keypressed = (window.event) ? event.keyCode : e.which;
                if (keypressed == 46) {
                    if (navigator.appName == 'Microsoft Internet Explorer') {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                    obj.value += ',';
                    obj.value = obj.value.replace('.', '');
                }

            }
            function ApriStoricoDatiReg() {
                document.getElementById('USCITA').value = '1';
                if (document.getElementById('txtModificato').value == '1') {
                    alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
                }
                else {

                    window.open('StoricoDatiRegistrazione.aspx?IDC=' + document.getElementById('txtIdContratto').value, 'StoricoDati', 'height=350,top=150,left=250,width=670');

                }
            }
            MostraStoricoReg();

            function apriAlert(testo, larghezza, altezza, titolo, callback, img) {
    if (img == null) {
        img = '../StandardTelerik/Immagini/Messaggi/alert.png';
    };
    var alertTelerik = radalert(testo, larghezza, altezza, titolo, callback, img);
    alertTelerik = alertTelerik.set_behaviors();
};

            
            function closeWindow(sender, args, nomeRad) {
                var radwindow = $find(nomeRad);
                radwindow.close();
            };

            
            function elenco_istanze()
            {
                 var oWnd = $find('RadWindow1');
                 oWnd.setUrl('../Gestione_locatari/ElencoIstanze.aspx?IDC=' + document.getElementById('txtIdContratto').value);
                 oWnd.setSize(700, 600);
                 oWnd.show();
            };

            function apriPaginaScelta()
            {
                        var oWnd = $find('RadWindowAggiungi');
                        oWnd.setUrl('../Gestione_locatari/ScegliIstanza.aspx?ID=' + document.getElementById('txtIdContratto').value + '&COD=' + document.getElementById('Tab_Contratto1_txtCodContratto').value + '&INTEST=' + document.getElementById('LBLintest').value);
                        oWnd.setSize(400, 300);
                        oWnd.show();
            };

            function CreaAUabusivi() {
                var oWnd = $find('RadWindowAggiungi');
                var chiediConferma
                   chiediConferma = window.confirm("Attenzione...Sei sicuro di volere creare una nuova scheda anagrafe utenza?");
                   
                   if (chiediConferma == true) {
                       document.getElementById('USCITA').value='1';
                       if (document.getElementById('au_abusivi').value=='1')
                       {
                         window.open('AU_abusivi/ScegliAU.aspx?T=<%=lIdConnessione %>&IDC='+document.getElementById('txtIdContratto').value +'&COD='+document.getElementById('Tab_Contratto1_txtCodContratto').value,'SceltaAU','width=450,top=220,left=600,height=320,scroll=no');
                       }else
                       {
                            oWnd.setUrl('AU_temporanee/ScegliAU.aspx?T=<%=lIdConnessione %>&IDC='+document.getElementById('txtIdContratto').value +'&COD='+document.getElementById('Tab_Contratto1_txtCodContratto').value);
                            oWnd.setSize(480, 400);
                            oWnd.show();
                       }
                    }
                       else {
                       document.getElementById('USCITA').value='1';
                       alert('Operazione annullata!');
                   }
            }
        </script>
    </form>
</body>
</html>
