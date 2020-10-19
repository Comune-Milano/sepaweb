<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContrattoLight.aspx.vb" Inherits="Contratti_CONTRATTI_LIGHT_ContrattoLight" %>

<%@ Register Src="Tab_SchemaBollette.ascx" TagName="Tab_SchemaBollette" TagPrefix="uc9" %>
<%@ Register Src="Tab_Bollette_New.ascx" TagName="Tab_Bollette" TagPrefix="uc8" %>
<%@ Register Src="Tab_Comunicazioni.ascx" TagName="Tab_Comunicazioni" TagPrefix="uc7" %>
<%@ Register Src="Tab_Conduttore.ascx" TagName="Tab_Conduttore" TagPrefix="uc6" %>
<%@ Register Src="Tab_Registrazione.ascx" TagName="Tab_Registrazione" TagPrefix="uc5" %>
<%@ Register Src="Tab_Canone.ascx" TagName="Tab_Canone" TagPrefix="uc4" %>
<%@ Register Src="Tab_UnitaImmLocate.ascx" TagName="Tab_UnitaImmLocate" TagPrefix="uc3" %>
<%@ Register Src="Tab_Contratto.ascx" TagName="Tab_Contratto" TagPrefix="uc2" %>
<%@ Register Src="Tab_Generale.ascx" TagName="TabGenerale" TagPrefix="uc1" %>
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
<script type="text/javascript" src="../jquery-1.8.2.js"></script>
<script type="text/javascript" src="../jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="../jquery.corner.js"></script>
<script type="text/javascript" src="../common.js"></script>
<script type="text/javascript" src="../prototype.lite.js"></script>
<script type="text/javascript" src="../moo.fx.js"></script>
<script type="text/javascript" src="../moo.fx.pack.js"></script>
<script type="text/javascript" src="../Funzioni.js">
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
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="../impromptu.css" />
    <style type="text/css">
        #TIPO1
        {
            font: normal 12px Verdana;
        }
        
        #dropmenudiv
        {
            position: absolute;
            border: 1px solid black;
            border-bottom-width: 0;
            font: normal 12px Verdana;
            line-height: 18px;
            z-index: 100;
        }
        
        #dropmenudiv a
        {
            width: 100%;
            display: block;
            text-indent: 3px;
            border-bottom: 1px solid black;
            padding: 1px 0;
            text-decoration: none;
            font-weight: bold;
        }
        
        #dropmenudiv a:hover
        {
            /*hover background color*/
            background-color: yellow;
        }
        
        #form1
        {
            width: 900px;
        }
    </style>
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
        menu1[6] = '<a href="javascript:ChiusuraCont();">Verbale Chiusura Contr./Tassa Rif.</a>'
        menu1[9] = '<a href="javascript:ModuloRifiuti();">Modulo Tassa Rifiuti</a>'
        menu1[10] = '<a href="javascript:DichMaggiorenni();">Dichiarazione Maggiorenni</a>'
        //****** 05-07-2012 fine NUOVI DOCUMENTI ****** 

        menu1[11] = '<a href="javascript:Apri();">Visualizza stampe Contratti</a>'
        menu1[12] = '<a href="javascript:AllegatiContratto();">Visualizza Allegati</a>'
        menu1[13] = '<a href="javascript:AllegaFile();">Allega File</a>'
                
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
            while (b.parentNode)
                if ((b = b.parentNode) == a)
                    return true;
            return false;
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
    <script type="text/javascript" src="../tabber.js"></script>
    <link rel="stylesheet" href="../example.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        window.onbeforeunload = confirmExit;
        window.onunload = Exit;


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
        function confirmExit() {
            if (document.getElementById("USCITA")) {
                if (document.getElementById("USCITA").value == '0') {
                    if (navigator.appName == 'Microsoft Internet Explorer') {

                        event.returnValue = "Attenzione...Uscendo dal contratto le modifiche non salvate andranno perse. Si consiglia di salvare prima di uscire!";
                    }
                }
            }
        }
        //            if (document.getElementById("USCITA").value == '0') {
        //                if (navigator.appName == 'Microsoft Internet Explorer') {
        //                    event.returnValue = "Attenzione...Uscire dal contratto premendo il pulsante ESCI. In caso contrario non sara più possibile accedere al contratto per un determinato periodo di tempo!";
        //                }
        //                else {
        //                }
        //            }
        //        }

        function Exit() {
            if (document.getElementById("USCITA")) {
                if (document.getElementById("USCITA").value == '0') {
                    if (document.getElementById('imgEsci') != null) {
                        document.getElementById('imgEsci').click();
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
            if (document.getElementById('Tab_Contratto1_txtDelibera').value == '' || document.getElementById('Tab_Contratto1_txtDataDelibera').value == '' || document.getElementById('Tab_Contratto1_txtDataDecorrenza').value == '' || document.getElementById('Tab_Contratto1_txtDataConsegna').value == '' || document.getElementById('Tab_Contratto1_txtDataStipula').value == '' || document.getElementById('Tab_Contratto1_txtEntroCuiDisdettare').value == '' || document.getElementById('Tab_Contratto1_txtDataScadenza').value == '' || document.getElementById('Tab_Contratto1_txtDataSecScadenza').value == '') {
                alert('Attenzione, per attivare il contratto è necessario che siano stati inseriti i valori relativi a:\nProvvedimento Assegnazione;\nData Provvedimento;\nData Decorrenza;\nData Consegna;\nData Stipula;\nData Scadenza;\nData Seconda Scadenza;\nMesi entro cui disdettare;\nUfficio Registrazione.');
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
                var sicuro = window.confirm('Sei sicuro di voler CHIUDERE questo contratto?\n In caso affermativo, sarà emessa una bolletta con le eventuali spese di recessione contratto, gli interessi maturati sul deposito cauzionale, il deposito cauzionale stesso e tutte le eventuali spese in pendenza.\n Eventuali importi causati da danni o altro, dovranno essere aggiunti manualmente!');
                if (sicuro == true) {
                    document.getElementById('TXTATTIVA').value = '1';
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


    </script>
</head>
<body style="background-attachment: fixed; background-image: url(../Immagini/SfondoContratto.png);
    background-repeat: no-repeat;">
    <div id="Attesa" style="position: absolute; width: 100%; height: 100%; top: 0px;
        left: 0px; background-color: #f0f0f0; visibility: visible; z-index: 500; display: block;">
        <img src="../../ImmDiv/DivUscitaInCorso2.jpg" alt="caricamento in corso..." style="position: absolute;
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
    <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
        ForeColor="#660000" Text="Contratto" Width="508px"></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
    <asp:Label ID="LBLErpModerato" runat="server" BackColor="#FFFF99" Font-Bold="True"
        Font-Names="ARIAL" Font-Size="16pt" ForeColor="Black" Text="E.R.P. Moderato"
        Visible="False"></asp:Label>
    <asp:Label ID="LBLABUSIVO" runat="server" BackColor="#FF3300" Font-Bold="True" Font-Names="ARIAL"
        Font-Size="16pt" ForeColor="White" Text="A  B  U  S  I  V  O" Visible="False"></asp:Label>
    &nbsp;<asp:Label ID="LBLVIRTUALE" runat="server" BackColor="#FFFF99" Font-Bold="True"
        Font-Names="ARIAL" Font-Size="16pt" ForeColor="Black" Text="V I R T U A L E"
        Visible="False"></asp:Label>
    <br />
    <p style="width: 1130px" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <a href="#" onmouseover="return dropdownmenu(this, event, menu1, '300px')" onmouseout="delayhidemenu()">
                    <asp:Image ID="ImageDocumentazione" runat="server" ImageUrl="~/NuoveImm/Img_Documentazione.png"
                        Visible="True" TabIndex="6" /></a>
            </td>
            <td id="imgStampe">
                &nbsp;</td>
            <td>
                <a href="javascript:VisEventi();">
                    <img border="0" alt="Eventi" id="ImgEventi" src="../../NuoveImm/Img_Eventi.png" style="cursor: pointer"
                        onclick="document.getElementById('USCITA').value='1';" /></a>
            </td>
            <td align="right">
                <asp:ImageButton ID="imgEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci1.png"
                    ToolTip="Esci" OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();"
                    TabIndex="1" />
            </td>
            <td align="right">
                <asp:Label ID="LBLSTATOC" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                    ForeColor="#C00000" Style="border-right: black 1px solid; border-top: black 1px solid;
                    border-left: black 1px solid; border-bottom: black 1px solid; background-color: white"
                    ToolTip="Indica lo Stato del Contratto"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="MyTab" class="tabber" style="width: 1145px;">
        <div class="tabbertab <%=Tab1 %>">
            <h2>
                Generale</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
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
            <h2>
                Contratto</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
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
            <h2>
                Unità I. Locata</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
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
            <h2>
                Conduttore</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
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
            <h2>
                Canone</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
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
            <h2>
                Registrazione</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
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
            <h2>
                Schema Bollette</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
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
            <h2>
                Partite Contabili</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
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
            <h2>
                Comunicazioni</h2>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
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

//        myOpacity = new fx.Opacity('InserimentoOspiti', { duration: 200 });
//        myOpacity.hide();

//        myOpacity1 = new fx.Opacity('InserimentoBolletta', { duration: 200 });
//        //myOpacity1.hide();

//        if (document.getElementById('Tab_Bollette1_txtAppare').value != '1') {
//            //document.getElementById('InserimentoBolletta').style.visibility = 'hidden';
//            myOpacity1.hide();
//        }

//        myOpacity2 = new fx.Opacity('InserimentoSchema', { duration: 200 });
//        if (document.getElementById('Tab_SchemaBollette1_txtAppare').value != '2') {
//            myOpacity2.hide();
//        }

        myOpacity10 = new fx.Opacity('InfoUtente', { duration: 200 });
        myOpacity10.hide();


//        myOpacityStorno = new fx.Opacity('Storno', { duration: 200 });

//        if (document.getElementById('Tab_Bollette1_txtAppare1').value != '1') {
//            myOpacityStorno.hide();
//        }
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
            window.open('../../InvioAllegato.aspx?T=1&ID=<%=CodContratto1 %>', 'Allegati', '');
            document.getElementById('USCITA').value = '0';
        }


        function ApriPromUtente() {
            window.open('../../CENSIMENTO/ModuloPromUtente.aspx?PROV=1&COD=<%=lIdContratto %>', 'ModuloRappSloggio', '');
            document.getElementById('USCITA').value = '0';

        }
        function Riconsegna() {
            window.open('../Comunicazioni/RiconsegnaImmobile.aspx?COD=<%=CodContratto1 %>', 'RiConsegna', '');
            document.getElementById('USCITA').value = '0';

        }

        function Consegna() {
            window.open('../Comunicazioni/ConsegnaChiavi.aspx?COD=<%=CodContratto1 %>', 'Consegna', '');
            document.getElementById('USCITA').value = '0';
            //document.getElementById('txtModificato').value = '1';
        }

        function Cessione() {
            window.open('../Comunicazioni/DenunciaCessione.aspx?COD=<%=CodContratto1 %>&L=<%=LetteraProvenienza %>', 'Cessione', '');
            document.getElementById('USCITA').value = '0';
            //document.getElementById('txtModificato').value = '1';
        }

        function Ospitalita() {
            window.open('../Comunicazioni/Ospitalita.aspx?T=1&COD=<%=CodContratto1 %>', 'Ospitalità', '');
            document.getElementById('USCITA').value = '0';
            //document.getElementById('txtModificato').value = '1';
        }

        function Disdetta() {
            window.open('../Comunicazioni/Disdetta.aspx?T=1&COD=<%=CodContratto1 %>', 'Disdetta', '');
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
            window.open('../Comunicazioni/ChiusuraContratto.aspx?T=1&COD=<%=CodContratto1 %>', 'Chiusura', '');
            document.getElementById('USCITA').value = '0';
            //document.getElementById('txtModificato').value = '1';
        }

        function MyDialogArguments() {
            this.Sender = null;
            this.StringValue = "";
        }





        function ApriRateizzazione() {
            if (document.getElementById('txtModificato').value == '1') {
                alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
            }
            else {
                if (document.getElementById('txtIdUnita').value == '0') {
                    alert('Impossibile procedere per mancanza dell\' UNITA\' IMMOBILIARE!');
                    return;
                }
                var dialogResults = window.showModalDialog('../../RATEIZZAZIONE/BolRateizzabili.aspx?IDCONTRATTO=' + document.getElementById('txtIdContratto').value, 'window', 'status:no;dialogWidth:920px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
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
            window.open('../../RATEIZZAZIONE/RateizzEmesse.aspx?idcont=' + document.getElementById('txtIdContratto').value, 'DettaglioRat', '')
        }


    </script>
    <script type="text/javascript">
        VisualizzaDivHtml();

        var codContratto;
        codContratto = document.getElementById('Tab_Contratto1_txtCodContratto').value;
       

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

       
       if (document.getElementById('Rateizza')) {
            if (document.getElementById('Rateizza').value == '0') {
                if (document.getElementById('ImageFunzioni')) {
                    menu2[0] = ''
                    menu2[1] = ''
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
        
        if (menu2[0] == '' && menu2[1] == '' && menu2[2] == '' && menu2[3] == '' && menu2[4] == '') {
            document.getElementById('ImageFunzioni').style.visibility = 'hidden';
            document.getElementById('ImageFunzioni').style.position = 'absolute';
            document.getElementById('ImageFunzioni').style.left = '-100px';
            document.getElementById('ImageFunzioni').style.display = 'none';
        }


        if (Stringa.substring(0, 11) == 'USI DIVERSI' || (Stringa.substring(0, 12) == 'LEGGE 431/98' && Stringa2 != 'D')) {

            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            if (document.getElementById('au_abusivi').value == '1') {
                menu2[4]='';
            }
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            else {
                           

                 if (Stringa.substring(0, 12) == 'LEGGE 431/98' && Stringa2 != 'D' && Stringa2 != 'P')
                {
//                    document.getElementById('tblLocatari').style.visibility = 'visible';
//                    document.getElementById('iconeVSA').style.visibility = 'visible';
//                    document.getElementById('imgNuovaDom').style.visibility = 'visible';
//                    document.getElementById('imgDichiarazioni').style.visibility = 'visible';

//                    document.getElementById('iconeBando').style.visibility = 'hidden';
//                    document.getElementById('iconeAU').style.visibility = 'hidden';
                }else
                {
//                    document.getElementById('tblLocatari').style.visibility = 'hidden';
//                    document.getElementById('tblLocatari').style.position = 'absolute';
//                    document.getElementById('tblLocatari').style.left = '-100px';
//                    document.getElementById('tblLocatari').style.display = 'none';

//                    document.getElementById('tblBando').style.visibility = 'hidden';
//                    document.getElementById('tblBando').style.position = 'absolute';
//                    document.getElementById('tblBando').style.left = '-100px';
//                    document.getElementById('tblBando').style.display = 'none';
                }
                 menu2[4]='';   
            }
        }




        //****** 11-05-2012 COD_TIPOLOGIA_CONTR_LOC='L43198', DEST_USO='D' ******
        if (Stringa.substring(0, 12) == 'LEGGE 431/98' && Stringa2 == 'D') {
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            if (document.getElementById('au_abusivi').value == '1') {

//                document.getElementById('rigaImgBando').style.visibility = 'hidden';
//                document.getElementById('rigaImgBando').style.position = 'absolute';
//                document.getElementById('rigaImgBando').style.left = '-100px';
//                document.getElementById('rigaImgBando').style.display = 'none';

//                document.getElementById('tblLocatari').style.visibility = 'hidden';
//                document.getElementById('tblLocatari').style.position = 'absolute';
//                document.getElementById('tblLocatari').style.left = '-100px';
//                document.getElementById('tblLocatari').style.display = 'none';

//                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//                document.getElementById('Generale1_imgCreaAU').style.display = 'none';

//                document.getElementById('imgScegliAU').style.visibility = 'visible';
                 menu2[4]='';
            }
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            else {


              

                document.getElementById('imgAnagrafe').style.visibility = 'hidden';
                document.getElementById('VisDichiarazione').style.visibility = 'hidden';
                document.getElementById('VisDichiarazione').style.position = 'absolute';
                document.getElementById('VisDichiarazione').style.left = '-100px';
                document.getElementById('VisDichiarazione').style.display = 'none';

                document.getElementById('VisDomanda').style.visibility = 'hidden';
                document.getElementById('VisDomanda').style.position = 'absolute';
                document.getElementById('VisDomanda').style.left = '-100px';
                document.getElementById('VisDomanda').style.display = 'none';
                                
                document.getElementById('iconeBando').style.visibility = 'hidden';
                
                document.getElementById('iconeAU').style.visibility = 'hidden';
                
                menu2[4]='';
            }

        }


        if (Stringa1.substring(0, 8) != 'ALLOGGIO') {
            if (document.getElementById('au_abusivi').value == '1') {

//                document.getElementById('rigaImgBando').style.visibility = 'hidden';
//                document.getElementById('rigaImgBando').style.position = 'absolute';
//                document.getElementById('rigaImgBando').style.left = '-100px';
//                document.getElementById('rigaImgBando').style.display = 'none';

//                document.getElementById('tblLocatari').style.visibility = 'hidden';
//                document.getElementById('tblLocatari').style.position = 'absolute';
//                document.getElementById('tblLocatari').style.left = '-100px';
//                document.getElementById('tblLocatari').style.display = 'none';

//              

//                document.getElementById('imgScegliAU').style.visibility = 'visible';
                 menu2[4]='';
            }
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            else {
               
//                document.getElementById('imgAnagrafe').style.visibility = 'hidden';
//                document.getElementById('imgAnagrafe').style.position = 'absolute';
//                document.getElementById('imgAnagrafe').style.left = '-100px';
//                document.getElementById('imgAnagrafe').style.display = 'none';

//                document.getElementById('VisDichiarazione').style.visibility = 'hidden';
//                document.getElementById('VisDichiarazione').style.position = 'absolute';
//                document.getElementById('VisDichiarazione').style.left = '-100px';
//                document.getElementById('VisDichiarazione').style.display = 'none';

//                document.getElementById('VisDomanda').style.visibility = 'hidden';
//                document.getElementById('VisDomanda').style.position = 'absolute';
//                document.getElementById('VisDomanda').style.left = '-100px';
//                document.getElementById('VisDomanda').style.display = 'none';

//                document.getElementById('tblBando').style.visibility = 'hidden';
//                document.getElementById('tblBando').style.position = 'absolute';
//                document.getElementById('tblBando').style.left = '-100px';
//                document.getElementById('tblBando').style.display = 'none';
                 menu2[4]='';
            }
        }

        if (Stringa.substring(0, 13) == 'NON ESISTENTE') {
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            if (document.getElementById('au_abusivi').value == '1') {

//                document.getElementById('rigaImgBando').style.visibility = 'hidden';
//                document.getElementById('rigaImgBando').style.position = 'absolute';
//                document.getElementById('rigaImgBando').style.left = '-100px';
//                document.getElementById('rigaImgBando').style.display = 'none';

//                document.getElementById('tblLocatari').style.visibility = 'hidden';
//                document.getElementById('tblLocatari').style.position = 'absolute';
//                document.getElementById('tblLocatari').style.left = '-100px';
//                document.getElementById('tblLocatari').style.display = 'none';

//                

//                document.getElementById('imgScegliAU').style.visibility = 'visible';
                 menu2[4]='';

            }
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            else {

                
//                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//                document.getElementById('Generale1_imgCreaAU').style.display = 'none';

//                document.getElementById('imgAnagrafe').style.visibility = 'hidden';
//                document.getElementById('imgAnagrafe').style.position = 'absolute';
//                document.getElementById('imgAnagrafe').style.left = '-100px';
//                document.getElementById('imgAnagrafe').style.display = 'none';

//                document.getElementById('VisDichiarazione').style.visibility = 'hidden';
//                document.getElementById('VisDichiarazione').style.position = 'absolute';
//                document.getElementById('VisDichiarazione').style.left = '-100px';
//                document.getElementById('VisDichiarazione').style.display = 'none';

//                document.getElementById('VisDomanda').style.visibility = 'hidden';
//                document.getElementById('VisDomanda').style.position = 'absolute';
//                document.getElementById('VisDomanda').style.left = '-100px';
//                document.getElementById('VisDomanda').style.display = 'none';

//                document.getElementById('tblLocatari').style.visibility = 'hidden';
//                document.getElementById('tblLocatari').style.position = 'absolute';
//                document.getElementById('tblLocatari').style.left = '-100px';
//                document.getElementById('tblLocatari').style.display = 'none';

//                document.getElementById('tblBando').style.visibility = 'hidden';
//                document.getElementById('tblBando').style.position = 'absolute';
//                document.getElementById('tblBando').style.left = '-100px';
//                document.getElementById('tblBando').style.display = 'none';
                 menu2[4]='';
            }
        }

        if (Stringa.substring(0, 11) == 'NESSUNA TIP') {
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            if (document.getElementById('au_abusivi').value == '1') {

//                document.getElementById('rigaImgBando').style.visibility = 'hidden';
//                document.getElementById('rigaImgBando').style.position = 'absolute';
//                document.getElementById('rigaImgBando').style.left = '-100px';
//                document.getElementById('rigaImgBando').style.display = 'none';
//                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//                document.getElementById('Generale1_imgCreaAU').style.display = 'none';

//                document.getElementById('imgScegliAU').style.visibility = 'visible';
                 menu2[4]='';

            }
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            else {
//                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//                document.getElementById('Generale1_imgCreaAU').style.display = 'none';

//                document.getElementById('imgAnagrafe').style.visibility = 'hidden';
//                document.getElementById('imgAnagrafe').style.position = 'absolute';
//                document.getElementById('imgAnagrafe').style.left = '-100px';
//                document.getElementById('imgAnagrafe').style.display = 'none';

//                document.getElementById('VisDichiarazione').style.visibility = 'hidden';
//                document.getElementById('VisDichiarazione').style.position = 'absolute';
//                document.getElementById('VisDichiarazione').style.left = '-100px';
//                document.getElementById('VisDichiarazione').style.display = 'none';

//                document.getElementById('VisDomanda').style.visibility = 'hidden';
//                document.getElementById('VisDomanda').style.position = 'absolute';
//                document.getElementById('VisDomanda').style.left = '-100px';
//                document.getElementById('VisDomanda').style.display = 'none';

//                document.getElementById('tblLocatari').style.visibility = 'hidden';
//                document.getElementById('tblLocatari').style.position = 'absolute';
//                document.getElementById('tblLocatari').style.left = '-100px';
//                document.getElementById('tblLocatari').style.display = 'none';

//                document.getElementById('tblBando').style.visibility = 'hidden';
//                document.getElementById('tblBando').style.position = 'absolute';
//                document.getElementById('tblBando').style.left = '-100px';
//                document.getElementById('tblBando').style.display = 'none';
                 menu2[4]='';
            }
        }

        //max
        if (Stringa.substring(0, 11) == 'CONCESSIONE') {
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            if (document.getElementById('au_abusivi').value == '1') {

//                document.getElementById('rigaImgBando').style.visibility = 'hidden';
//                document.getElementById('rigaImgBando').style.position = 'absolute';
//                document.getElementById('rigaImgBando').style.left = '-100px';
//                document.getElementById('rigaImgBando').style.display = 'none';

//                document.getElementById('tblLocatari').style.visibility = 'hidden';
//                document.getElementById('tblLocatari').style.position = 'absolute';
//                document.getElementById('tblLocatari').style.left = '-100px';
//                document.getElementById('tblLocatari').style.display = 'none';

//                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//                document.getElementById('Generale1_imgCreaAU').style.display = 'none';

//                document.getElementById('imgScegliAU').style.visibility = 'visible';
                 menu2[4]='';

            }
            //****** 25-06-2012 PRESENTE IN RAPPORTI_UTENZA_AU_ABUSIVI ******
            else {
//                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//                document.getElementById('Generale1_imgCreaAU').style.display = 'none';

//                document.getElementById('imgAnagrafe').style.visibility = 'hidden';
//                document.getElementById('imgAnagrafe').style.position = 'absolute';
//                document.getElementById('imgAnagrafe').style.left = '-100px';
//                document.getElementById('imgAnagrafe').style.display = 'none';

//                document.getElementById('VisDichiarazione').style.visibility = 'hidden';
//                document.getElementById('VisDichiarazione').style.position = 'absolute';
//                document.getElementById('VisDichiarazione').style.left = '-100px';
//                document.getElementById('VisDichiarazione').style.display = 'none';

//                document.getElementById('VisDomanda').style.visibility = 'hidden';
//                document.getElementById('VisDomanda').style.position = 'absolute';
//                document.getElementById('VisDomanda').style.left = '-100px';
//                document.getElementById('VisDomanda').style.display = 'none';

//                document.getElementById('tblLocatari').style.visibility = 'hidden';
//                document.getElementById('tblLocatari').style.position = 'absolute';
//                document.getElementById('tblLocatari').style.left = '-100px';
//                document.getElementById('tblLocatari').style.display = 'none';

//                document.getElementById('tblBando').style.visibility = 'hidden';
//                document.getElementById('tblBando').style.position = 'absolute';
//                document.getElementById('tblBando').style.left = '-100px';
//                document.getElementById('tblBando').style.display = 'none';
                 menu2[4]='';
            }
        }


        if (document.getElementById('lettura').value == '1') {
            
            
            if (document.getElementById('imgDefProroga')) {
                document.getElementById('imgDefProroga').style.visibility = 'hidden';
            }
            
            var menu1 = new Array()
            menu1[0] = '<a href="javascript:Apri();">Visualizza stampe Contratti</a>'
            menu1[1] = '<a href="javascript:AllegatiContratto();">Visualizza Allegati</a>'
            menu1[2] = '<a href="javascript:AllegatiContrattoEx();">Visualizza Allegati ex gestore</a>'

            menuAttivazione[0] = '';
            menuAttivazione[1] = '';
            
//            if (document.getElementById('ImgBolAttivazione')) {
//                document.getElementById('ImgBolAttivazione').style.visibility = 'hidden';
//            }
//            if (document.getElementById('Contact').value=='1') {
//                if (document.getElementById('ImageFunzioni')) {
//                    document.getElementById('ImageFunzioni').style.visibility = 'hidden';
//                }
//            }
        }

//        if (document.getElementById('AULETTURA')) {
//            if (document.getElementById('AULETTURA').value == '1') {
//                if (document.getElementById('Generale1_imgCreaAU')) {
//                    document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//                    document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//                    document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//                    document.getElementById('Generale1_imgCreaAU').style.display = 'none';
//                }
//            }
//        }

//        if (document.getElementById('GLLETTURA')) {
//            if (document.getElementById('GLLETTURA').value == '1') {
//                if (document.getElementById('tblLocatari')) {
//                    if (document.getElementById('imgNuovaDom')) {
//                        document.getElementById('imgNuovaDom').style.visibility = 'hidden';
//                        document.getElementById('imgNuovaDom').style.position = 'absolute';
//                        document.getElementById('imgNuovaDom').style.left = '-100px';
//                        document.getElementById('imgNuovaDom').style.display = 'none';
//                    }
//                }
//            }
//        }


        if (document.getElementById('HStatoContratto').value != 'CHIUSO' && document.getElementById('opfiliale').value == '1' && document.getElementById('VIRTUALE').value != '1' && document.getElementById('lettura').value != '1') {

           
            document.getElementById('ImageDocumentazione').style.visibility = 'visible';
//            document.getElementById('Tab_Contratto1_txtDataDisdetta').readOnly = false;
//            document.getElementById('Tab_Contratto1_txtDataDisdetta0').readOnly = false;
//            document.getElementById('Tab_Contratto1_txtNotificaDisdetta').readOnly = false;
//            document.getElementById('Tab_Contratto1_txtDataRiconsegna').readOnly = false;
        }

        if (document.getElementById('VIRTUALE').value == '1') {
                       
            
            document.getElementById('ImageDocumentazione').style.visibility = 'hidden';
            document.getElementById('ImageDocumentazione').style.position = 'absolute';
            document.getElementById('ImageDocumentazione').style.left = '-100px';
            document.getElementById('ImageDocumentazione').style.display = 'none';


//            document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//            document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//            document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//            document.getElementById('Generale1_imgCreaAU').style.display = 'none';

//            document.getElementById('tblBando').style.visibility = 'hidden';
//            document.getElementById('tblBando').style.position = 'absolute';
//            document.getElementById('tblBando').style.left = '-100px';
//            document.getElementById('tblBando').style.display = 'none';


//            document.getElementById('tblLocatari').style.visibility = 'hidden';
//            document.getElementById('tblLocatari').style.position = 'absolute';
//            document.getElementById('tblLocatari').style.left = '-100px';
//            document.getElementById('tblLocatari').style.display = 'none';

             menu2[4]='';

//            if (document.getElementById('imgRinnovoUSD')) {
//                document.getElementById('imgRinnovoUSD').style.visibility = 'hidden';
//                document.getElementById('imgRinnovoUSD').style.position = 'absolute';
//                document.getElementById('imgRinnovoUSD').style.left = '-100px';
//                document.getElementById('imgRinnovoUSD').style.display = 'none';
//            }

//            if (document.getElementById('imgCambioBox')) {
//                document.getElementById('imgCambioBox').style.visibility = 'hidden';
//                document.getElementById('imgCambioBox').style.position = 'absolute';
//                document.getElementById('imgCambioBox').style.left = '-100px';
//                document.getElementById('imgCambioBox').style.display = 'none';
//            }

//            if (document.getElementById('ImgCambioIntestazione')) {
//                document.getElementById('ImgCambioIntestazione').style.visibility = 'hidden';
//                document.getElementById('ImgCambioIntestazione').style.position = 'absolute';
//                document.getElementById('ImgCambioIntestazione').style.left = '-100px';
//                document.getElementById('ImgCambioIntestazione').style.display = 'none';
//            }

        }

        if (document.getElementById('speseunita').value == '0') {
//            document.getElementById('imgSpeseUnita').style.visibility = 'hidden';
//            document.getElementById('imgSpeseUnita').style.position = 'absolute';
//            document.getElementById('imgSpeseUnita').style.left = '-100px';
//            document.getElementById('imgSpeseUnita').style.display = 'none';
        }
        else {
//            document.getElementById('imgSpeseUnita').style.visibility = 'visible';
        }

        if (document.getElementById('HStatoContratto').value == 'CHIUSO') {
            
            var menu1 = new Array()
            menu1[0] = '<a href="javascript:Apri();">Visualizza stampe Contratti</a>'
            menu1[1] = '<a href="javascript:AllegatiContratto();">Visualizza Allegati</a>'
            menu1[2] = '<a href="javascript:AllegatiContrattoEx();">Visualizza Allegati ex gestore</a>'
            menu1[3] = '<a href="javascript:AllegaFile();">Allega File</a>'



//            document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//            document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//            document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//            document.getElementById('Generale1_imgCreaAU').style.display = 'none';


//            document.getElementById('imgNuovaDom').style.visibility = 'hidden';
//            document.getElementById('imgNuovaDom').style.position = 'absolute';
//            document.getElementById('imgNuovaDom').style.left = '-100px';
//            document.getElementById('imgNuovaDom').style.display = 'none';

             menu2[4]='';


        }

        if (document.getElementById('HStatoContratto').value == 'BOZZA') {

//            document.getElementById('tblLocatari').style.visibility = 'hidden';
//            document.getElementById('tblLocatari').style.position = 'absolute';
//            document.getElementById('tblLocatari').style.left = '-100px';
//            document.getElementById('tblLocatari').style.display = 'none';

             menu2[4]='';
        }



        if (document.getElementById('bloccato').value == '1') {
         menu2[4]='';
//            document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//            document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//            document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//            document.getElementById('Generale1_imgCreaAU').style.display = 'none';
//                                    
//                        if (document.getElementById('imgDefProroga')) {
//                            document.getElementById('imgDefProroga').style.visibility = 'hidden';
//                        }

//            if (document.getElementById('tblLocatari')) {
//                if (document.getElementById('imgNuovaDom')) {
//                    document.getElementById('imgNuovaDom').style.visibility = 'hidden';
//                    document.getElementById('imgNuovaDom').style.position = 'absolute';
//                    document.getElementById('imgNuovaDom').style.left = '-100px';
//                    document.getElementById('imgNuovaDom').style.display = 'none';
//                     menu2[4]='';
//                }
//            }
//            if (document.getElementById('spostaAnnulla').value == '1') {
//                if (document.getElementById('ImageFunzioni')) {
//                    menu2[2] = ''
//                }
//            }

           
        }

//        if (document.getElementById('RinnovoUSD').value == '0') {
//            document.getElementById('imgRinnovoUSD').style.visibility = 'hidden';
//            document.getElementById('imgRinnovoUSD').style.position = 'absolute';
//            document.getElementById('imgRinnovoUSD').style.left = '-100px';
//            document.getElementById('imgRinnovoUSD').style.display = 'none';
//        }
//        if (document.getElementById('CambioBox').value == '0') {
//            document.getElementById('imgCambioBox').style.visibility = 'hidden';
//            document.getElementById('imgCambioBox').style.position = 'absolute';
//            document.getElementById('imgCambioBox').style.left = '-100px';
//            document.getElementById('imgCambioBox').style.display = 'none';
//        }

//        if (document.getElementById('CambioBox').value == '1' || document.getElementById('HStatoContratto').value == 'CHIUSO' || document.getElementById('HStatoContratto').value == 'BOZZA') {
//            if (document.getElementById('ImgCambioIntestazione')) {
//                document.getElementById('ImgCambioIntestazione').style.visibility = 'hidden';
//                document.getElementById('ImgCambioIntestazione').style.position = 'absolute';
//                document.getElementById('ImgCambioIntestazione').style.left = '-100px';
//                document.getElementById('ImgCambioIntestazione').style.display = 'none';
//            }

//        }

        
//        if (document.getElementById('au_abusivi').value == '1') {

//            if (document.getElementById('Generale1_imgCreaAU').style.visibility != 'hidden') {
//                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//                document.getElementById('Generale1_imgCreaAU').style.display = 'none';
//                document.getElementById('imgScegliAU').style.visibility = 'visible';
//            }
//            else {
//                document.getElementById('iconeAU').style.visibility = 'visible';
//                document.getElementById('imgScegliAU').style.visibility = 'visible';
//                document.getElementById('tblLocatari').style.visibility = 'visible';
//                document.getElementById('iconeVSA').style.visibility = 'visible';
//                document.getElementById('imgNuovaDom').style.visibility = 'visible';
//                document.getElementById('imgDichiarazioni').style.visibility = 'visible';
//            }

//        }


       
        if (document.getElementById('Tab_Contratto1_chkTemporanea').checked == true) {
            if (document.getElementById('prorogati').value == '1' || document.getElementById('assegn_def').value == '1') {
//                document.getElementById('rigaImgAnagr').style.visibility = 'visible';
//                document.getElementById('Generale1_imgCreaAU').style.visibility = 'hidden';
//                document.getElementById('Generale1_imgCreaAU').style.position = 'absolute';
//                document.getElementById('Generale1_imgCreaAU').style.left = '-100px';
//                document.getElementById('Generale1_imgCreaAU').style.display = 'none';

//                document.getElementById('iconeAU').style.visibility = 'visible';
//                document.getElementById('imgAnagrafe').style.visibility = 'visible';
//                document.getElementById('imgScegliAU').style.visibility = 'visible';
            }
//            if (document.getElementById('tblLocatari').style.visibility == 'hidden') {
//                document.getElementById('tblLocatari').style.visibility = 'visible';
//                document.getElementById('tblLocatari').style.display = 'block';
//            }
//            document.getElementById('imgDefProroga').style.visibility = 'visible';
        }
        //CONDIZIONE PER ICONE GEST.LOCATARI ASS.TEMP


        function VisualizzaDettagliCanone() {
            document.getElementById('DettagliCanone').style.visibility = 'visible';
        }

        function NascondiDettagliCanone() {
            document.getElementById('DettagliCanone').style.visibility = 'hidden';
        }



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

        

        if (document.getElementById('dvvvPre')) {
            document.getElementById('dvvvPre').style.visibility = 'hidden';
        }

        if (document.getElementById('Attesa')) {
            document.getElementById('Attesa').style.visibility = 'hidden';
        }

        if (document.getElementById('RateizInCorso').value == '1') {
            menu2[1] = '<a href="javascript:ApriDetRat();">Visualizza Rateizzazioni</a>'
        }

        //21/10/2013 nasconde/visualizza header html del tab bollette

        function VisualizzaDivHtml() {


        }

        function ApriSchedaArchivio(C) {
            today = new Date();
            var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();
            popupWindow = window.open('DatiContratto.aspx?LT=' + C + '&ID=<%=lIdContratto %> &COD=<%=CodContratto1 %>', Titolo, 'height=550,width=700');
            popupWindow.focus();
        }
    </script>
    </form>
</body>
</html>

