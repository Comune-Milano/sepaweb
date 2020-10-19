<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Condominio.aspx.vb" Inherits="Condomini_Condominio" %>

<%@ Register Src="TabConvocazione.ascx" TagName="TabConvocazione" TagPrefix="uc1" %>
<%@ Register Src="TabAmministratori.ascx" TagName="TabAmministratori" TagPrefix="uc2" %>
<%@ Register Src="TabMillesimalil.ascx" TagName="TabMillesimalil" TagPrefix="uc3" %>
<%@ Register Src="TabDatiTecnici.ascx" TagName="TabDatiTecnici" TagPrefix="uc4" %>
<%@ Register Src="TabInquilini.ascx" TagName="TabInquilini" TagPrefix="uc5" %>
<%@ Register Src="Tab_Contabilita.ascx" TagName="Tab_Contabilita" TagPrefix="uc6" %>
<%@ Register Src="TabMorosita.ascx" TagName="TabMorosita" TagPrefix="uc7" %>
<%@ Register Src="Tab_Pagamenti.ascx" TagName="Tab_Pagamenti" TagPrefix="uc8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Condominio</title>
    <style type="text/css">
        #form1
        {
            width: 785px;
        }
        .style1
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
    <script type="text/javascript">
        var Uscita;
        Uscita = 0;
        var Selezionato;
        function cerca() {
            if (document.all) {
                finestra = showModelessDialog('Find.htm', window, 'dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
                finestra.focus;
                finestra.document.close();
            }
            else if (document.getElementById) {
                self.find();
            }
            else window.alert('Il tuo browser non supporta questo metodo');
        };
        function PaymentStamp() {
            if (document.getElementById('txtModificato').value == '1') {
                alert('Prima di stampare, salvare le modifiche apportate al condominio!')
                return;
            }
            if (document.getElementById('Tab_Pagamenti1_txtidPagamento').value.replace('&nbsp;', '') != 0) {
                var Conferma
                Conferma = window.confirm("Elaborare la stampa del pagamento?");
                if (Conferma == true) {
                    document.getElementById("Tab_Pagamenti1_txtidPagamento").value == '';
                    document.getElementById("Tab_Pagamenti1_txtmia").value == '';
                    document.getElementById("Tab_Pagamenti1_txtDescrizione").value == '';
                    window.open('CreaPagamento.aspx?ID=' + document.getElementById("Tab_Pagamenti1_txtidPagamento").value + '&DESCRIZIONE=' + document.getElementById('Tab_Pagamenti1_txtDescrizione').value + '&ID_COND=<%=vIdCondominio %>', '');
                }
            }
            else {
                alert('Selezionare un pagamento con stato "PAGATO" per visualizzarne la stampa!');
            }
        };
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {

                e.preventDefault();
                document.getElementById('txtModificato').value = '111';
            }
        };
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {
                e.preventDefault();
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '111';
            }
        };
        function $onkeydown() {
            if (event.keyCode == 13) {
                //alert('ATTENZIONE!E\'stato premuto erroneamente il tasto invio! Utilizzare il mouse o il tasto TAB per spostarsi nei campi di testo!');
                //history.go(0);
                document.getElementById('txtModificato').value = '111';
                event.keyCode = 0;
            }
        };
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');
        var tabberOptions = {
            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;
            },
            'addLinkId': true
        };
        function Aprimodale() {
            document.getElementById('copri').style.visibility = 'visible';

            if (document.getElementById('Tab_Contabilita1_txtidGest').value > 0) {
                window.showModalDialog('RiepGestione.aspx?IDCONDOMINIO=<%=vIdCondominio %>&IDCON=<%=vIdConnessione %>&IDGEST=' + document.getElementById('Tab_Contabilita1_txtidGest').value + '&IDVISUAL=' + document.getElementById('ImgVisibility').value + '&MODIFICATO=' + document.getElementById('txtModificato').value, 'window', 'status:no;dialogWidth:900px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
            }
            else {
                alert('Selezionare un elemento dalla lista!')
            }
        };
        function NuovoGestione() {
            document.getElementById('copri').style.visibility = 'visible';

            if (document.getElementById('Tab_Contabilita1_txtAnnoInizio').value > 1900) {
                window.showModalDialog('RiepGestione.aspx?IDCONDOMINIO=<%=vIdCondominio %>&IDCON=<%=vIdConnessione %>&ANNO=' + document.getElementById('Tab_Contabilita1_txtAnnoInizio').value + '&TIPO=' + document.getElementById('Tab_Contabilita1_cmbTipoGest').value, 'window', 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
            }
            else {
                alert('Definire l\'anno!')
            }
        };
        function ApriRptCont() {
            if (document.getElementById('Tab_Contabilita1_txtidGest').value > 0) {
                if (document.getElementById('txtModificato').value == '1') {
                    alert('Prima di stampare, salvare le modifiche apportate al condominio!');
                    return;
                }
                window.open('RptContabilita.aspx?IDCONDOMINIO=<%=vIdCondominio %>&IDGEST=' + document.getElementById('Tab_Contabilita1_txtidGest').value, 'RptCont', '');
                document.getElementById('Tab_Contabilita1_txtidGest').value = 0;
                document.getElementById('Tab_Contabilita1_txtmia').value = 'Nessuna Selezione';
            }
            else {
                alert('Selezionare la contabilità da stampare!')
            }
        };
        function ApriModalMorosita() {
            document.getElementById('copri').style.visibility = 'visible';

            window.showModalDialog('Morosita.aspx?IDCONDOMINIO=<%=vIdCondominio %>&IDCON=<%=vIdConnessione %>&IDVISUAL=' + document.getElementById('ImgVisibility').value, 'window', 'status:no;dialogWidth:900px;dialogHeight:560px;dialogHide:true;help:no;scroll:no');
        };
        function ModificaModalMorosita() {
            document.getElementById('copri').style.visibility = 'visible';

            if (document.getElementById('TabMorosita1_txtidMorosita').value > 0) {
                window.showModalDialog('Morosita.aspx?IDCONDOMINIO=<%=vIdCondominio %>&IDCON=<%=vIdConnessione %>&IDMOROSITA=' + document.getElementById('TabMorosita1_txtidMorosita').value + '&IDVISUAL=' + document.getElementById('ImgVisibility').value + '&MODIFICATO=' + document.getElementById('txtModificato').value, 'window', 'status:no;dialogWidth:900px;dialogHeight:560px;dialogHide:true;help:no;scroll:no');
            }
            else {
                alert('Selezionare un elemento dalla lista!');
            }
        };
        function ApriDeleteMorosita() {
            document.getElementById('copri').style.visibility = 'visible';
            window.showModalDialog('DeleteMorosita.aspx?IDCONDOMINIO=<%=vIdCondominio %>&IDCON=<%=vIdConnessione %>&IDMOROSITA=' + document.getElementById('TabMorosita1_txtidMorosita').value + '&IDVISUAL=' + document.getElementById('ImgVisibility').value, 'window', 'status:no;dialogWidth:500px;dialogHeight:200px;dialogHide:true;help:no;scroll:no');
        };
        function ApriRptMorosita() {
            if (document.getElementById('TabMorosita1_txtidMorosita').value > 0) {

                if (document.getElementById('txtModificato').value == '1') {
                    alert('Prima di stampare, salvare le modifiche apportate al condominio!');
                    return;
                }
                window.open('RptMorosita.aspx?IDCONDOMINIO=<%=vIdCondominio %>&IDMOROSITA=' + document.getElementById('TabMorosita1_txtidMorosita').value, 'RptMoros', '');
                document.getElementById('TabMorosita1_txtidMorosita').value = 0;
            }
            else {
                alert('Selezionare la morosità da stampare!');
            }
        };
        function ApriAllegati() {
            if (document.getElementById('imgCambiaAmm').style.visibility != 'hidden') {
                window.open('ElencoAllegati.aspx?LT=0&COD=<%=vIdCondominio %>', 'Allegati', '');
            }
            else {
                window.open('ElencoAllegati.aspx?LT=1&COD=<%=vIdCondominio %>', 'Allegati', '');
            }
        };
        function PrintInquilini() {
            if (document.getElementById('txtModificato').value == '1') {
                alert('Prima di stampare, salvare le modifiche apportate al condominio!');
                return;
            }
            window.open('RptInquilini.aspx?IDCONDOMINIO= <%=vIdCondominio %>&CHIAMA=INQ', 'RptInquilini', '');
        };
        function chkToccato(e, obj) {
        };
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
        };
        function PrimaDiStampare() {
            if (document.getElementById('txtModificato').value == '1') {
                alert('Prima di stampare, salvare le modifiche apportate al condominio!');
                document.getElementById('txtModificato').value = '111';

            }
        };
        function ConfermaEsci() {
            document.getElementById('btnEsci').style.visibility = 'hidden';
            document.getElementById('copriExit').style.visibility = 'visible';
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma;
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Continuare l\'operazione senza aver salvato?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                    document.getElementById('btnEsci').style.visibility = 'visible';
                    document.getElementById('copriExit').style.visibility = 'hidden';
                    //document.getElementById('USCITA').value='0';
                }
            }
        };
        function ConfermaElimina() {

            var chiediConferma;
            chiediConferma = window.confirm("Attenzione...Verrà eliminata una scala!Continuare l\'operazione?");
            if (chiediConferma == true) {
                document.getElementById('TabMillesimalil1_ConfElimina').value = '1';

            }
        };
        function CalcolaPercentuale(obj) {
            var risultato
            if (obj.value.replace(',', '.') > 0 && (document.getElementById(obj.id + 'Comune').value.replace(',', '.') > 0)) {
                risultato = (100 * (document.getElementById(obj.id + 'Comune').value.replace(',', '.'))) / obj.value.replace(',', '.');
                risultato = risultato.toFixed(4);
                document.getElementById(obj.id + 'Comune' + 'Perc').value = risultato.replace('.', ',');

                if (obj.id.indexOf('Comp') >= 0) {
                    document.getElementById('TabConvocazione1_txtPercMillComp').value = risultato.replace('.', ',');
                }
                if (obj.id.indexOf('Prop') >= 0) {
                    document.getElementById('TabConvocazione1_txtPercMilProp').value = risultato.replace('.', ',');
                }
                if (obj.id.indexOf('Pres') >= 0) {
                    document.getElementById('TabConvocazione1_txtPercMillPresAss').value = risultato.replace('.', ',');
                }
            }
        };
        function CalcolaPercentuale2(obj) {
            var risultato
            if (obj.value.replace(',', '.') > 0 && (document.getElementById(obj.id.replace('Comune', '')).value.replace(',', '.') > 0)) {
                risultato = (100 * obj.value.replace(',', '.')) / document.getElementById(obj.id.replace('Comune', '')).value.replace(',', '.');
                risultato = risultato.toFixed(4);
                document.getElementById(obj.id + 'Perc').value = risultato.replace('.', ',');

                if (obj.id.indexOf('Comp') >= 0) {
                    document.getElementById('TabConvocazione1_txtPercMillComp').value = risultato.replace('.', ',');
                }
                if (obj.id.indexOf('Prop') >= 0) {
                    document.getElementById('TabConvocazione1_txtPercMilProp').value = risultato.replace('.', ',');
                }
                if (obj.id.indexOf('Pres') >= 0) {
                    document.getElementById('TabConvocazione1_txtPercMillPresAss').value = risultato.replace('.', ',');
                }
            }
        };
        function CalcolaPercentualeMill(obj) {
            var risultato
            if (obj.value.replace(',', '.') > 0 && (document.getElementById(obj.id + 'Comune').value.replace(',', '.') > 0)) {
                risultato = (100 * (document.getElementById(obj.id + 'Comune').value.replace(',', '.'))) / obj.value.replace(',', '.');
                risultato = risultato.toFixed(4);
                document.getElementById(obj.id + 'Comune' + 'Perc').value = risultato.replace('.', ',');
                document.getElementById('TabConvocazione1_txtpercSup').value = risultato.replace('.', ',');
            }
        };
        function CalcolaPercentualeMill2(obj) {
            var risultato
            if (obj.value.replace(',', '.') > 0 && (document.getElementById(obj.id.replace('Comune', '')).value.replace(',', '.') > 0)) {
                risultato = (100 * obj.value.replace(',', '.')) / document.getElementById(obj.id.replace('Comune', '')).value.replace(',', '.');
                risultato = risultato.toFixed(4);
                document.getElementById(obj.id + 'Perc').value = risultato.replace('.', ',');
                document.getElementById('TabConvocazione1_txtpercSup').value = risultato.replace('.', ',');
            }
        };
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        };
        function MaxMillesimo(obj) {
            if (obj.value.replace(',', '.') > 1000) {
                obj.value = '';
                alert('Il valore millesimale non può essere superiore a 1000');
                value = 'Confirmation Alert';
            }
        };
        function AutoDecimal(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(4);
                document.getElementById(obj.id).value = a.replace('.', ',');
            }
        };
        function AutoDecimal2(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2);
                document.getElementById(obj.id).value = a.replace('.', ',');
            }
        };
        function PulisciCampi() {
            document.getElementById('TabInquilini1_txtMil_Pro').value = "";
            document.getElementById('TabInquilini1_txtPosBil').value = "";
            document.getElementById('TabInquilini1_txtAsc').value = "";
            document.getElementById('TabInquilini1_txtMil_Compro').value = "";
            document.getElementById('TabInquilini1_txtMil_Gest').value = "";
            document.getElementById('TabInquilini1_txt_Mil_Risc').value = "";
            document.getElementById('TabInquilini1_txtMillPres').value = "";
            document.getElementById('TabInquilini1_txtNote').value = "";
            document.getElementById('TextBox4').value = "0";
            document.getElementById('txtidInquilini').value = "0";
        };
        function PulisciCampiAmminist() {
            //document.getElementById('txtDataInizio').value = ""
            //document.getElementById('txtDataFine').value = ""
            //document.getElementById('txtNuovaDataInizio').value = ""
        };
        function PulisciCampiConvocazione() {
            document.getElementById('TabConvocazione1_txtDataArrivo').value = "";
            document.getElementById('TabConvocazione1_txtProtAler').value = "";
            document.getElementById('TabConvocazione1_TxtDataArrivoAler').value = "";
            document.getElementById('TabConvocazione1_txtDataConv').value = "";
            document.getElementById('TabConvocazione1_txthh').value = "";
            document.getElementById('TabConvocazione1_txtMM').value = "";
            //document.getElementById('TabConvocazione1_txtPercMilProp').value = document.getElementById('TabMillesimalil1_txtMilPropComunePerc').value
            //document.getElementById('TabConvocazione1_txtpercSup').value = document.getElementById('TabMillesimalil1_TxtMillSupComunePerc').value
            //document.getElementById('TabConvocazione1_txtPercMillComp').value = document.getElementById('TabMillesimalil1_txtMillCompComunePerc').value
            document.getElementById('TabConvocazione1_txtPercMillPresAss').value = document.getElementById('TabMillesimalil1_txtMillPresComunePerc').value;
            document.getElementById('TabConvocazione1_txtDelegato').value = "";
            document.getElementById('TabConvocazione1_txtAltrePresenze').value = "";
            document.getElementById('TabConvocazione1_txtVerbNProtAler').value = "";
            document.getElementById('TabConvocazione1_txtDataArrivoVerbAler').value = "";
            document.getElementById('TabConvocazione1_txtDataArrivoVerb').value = "";
            document.getElementById('TabConvocazione1_txtNote').value = "";
            document.getElementById('TabConvocazione1_txtidConv').value = "0";
            document.getElementById('TextBox2').value = "1";
            document.getElementById('TabConvocazione1_vModifica').value = '0';
        };
        function selezDaListDeleg() {
            if (navigator.userAgent.toLowerCase().indexOf("msie") != -1) {
                var obj = document.getElementById("TabConvocazione1_listDelegati");
                document.getElementById("TabConvocazione1_txtDelegato").value = obj.options[obj.selectedIndex].innerText;
                document.getElementById("txtModificato").value = 1;

            }
            else {
                var obj = document.getElementById("TabConvocazione1_listDelegati");
                document.getElementById("TabConvocazione1_txtDelegato").value = obj.options[obj.selectedIndex].text;
                document.getElementById("txtModificato").value = 1;

            }
        };
        function ControlChange() {
            if (document.getElementById('txtDataFine').value != '' && document.getElementById('txtNuovaDataInizio').value != '') {
            }
            else {
                alert('Inserire, negli appositi campi, la data fine e la nuova data inizio per il cambio di amministrazione!');
                return false;
            }
        };
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }

        function ApriFornitori() {
            var e = document.getElementById("cmbFornitori");
            var strUser = e.options[e.selectedIndex].value;
            document.getElementById('idFornitore').value = e.options[e.selectedIndex].value;
            if (strUser != '-1') {
                window.open('../CICLO_PASSIVO/CicloPassivo/APPALTI/Fornitori.aspx?ID=' + document.getElementById('idFornitore').value + '&CO=---&RA=---&CALL=COND', 'window', 'height=700,width=1300,resizable=1');
               
            }
            else {
                //window.open('../CICLO_PASSIVO/CicloPassivo/APPALTI/Fornitori.aspx?ID=' + document.getElementById('idFornitore').value + '&CO=---&RA=---&CALL=COND', 'Fornitore', 'height=550,width=800')
                window.showModalDialog('../CICLO_PASSIVO/CicloPassivo/APPALTI/Fornitori.aspx?ID=-1&CO=---&RA=---&CALL=COND', 'window', 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
            }
            document.getElementById('splash').style.visibility = 'hidden';
        };
        function ApriOrdGiorno() {
            if (document.getElementById('TabConvocazione1_txtidConv').value != 0) {

                window.showModalDialog('OrdineDelGiorno.aspx?IDCONDOM=<%=vIdCondominio %>&IDCONV=' + document.getElementById('TabConvocazione1_txtidConv').value + '&IDCON=<%=vIdConnessione %>&SL=' + document.getElementById('ImgVisibility').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');

            }

        };
    </script>
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat">
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 100%; height: 95%; vertical-align: top;
        line-height: normal; top: 22px; left: 10px; background-color: #FFFFFF;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>
    <form id="form1" runat="server">
    <asp:HiddenField ID="ImgVisibility" runat="server" Value="0" />
    <asp:HiddenField ID="ScegCompVis" runat="server" Value="1" />
    <asp:HiddenField ID="ScegSuperCondVis" runat="server" Value="1" />
    <asp:HiddenField ID="TextBox1" runat="server" Value="1" />
    <asp:HiddenField ID="TextBox2" runat="server" />
    <asp:HiddenField ID="TextBox3" runat="server" />
    <asp:HiddenField ID="TextBox4" runat="server" />
    <asp:HiddenField ID="idConvoc" runat="server" />
    <asp:HiddenField ID="idMillScale" runat="server" />
    <asp:HiddenField ID="idMillFabb" runat="server" />
    <asp:HiddenField ID="TextBox5" runat="server" />
    <asp:HiddenField ID="txtidInquilini" runat="server" />
    <asp:HiddenField ID="chkEvent" runat="server" />
    <asp:HiddenField ID="ConfEliminaEdifici" runat="server" />
    <asp:HiddenField ID="EdificiToDelete" runat="server" />
    <asp:HiddenField ID="txttab" runat="server" Value="1" />
    <asp:HiddenField ID="AggPercent" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="idFornitore" runat="server" Value="0" />
    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Height="18px" Style="z-index: 104; left: 9px; position: absolute;
        top: 222px" Visible="False" Width="776px"></asp:Label>
    <table style="width: 100%;">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnIndietro" runat="server" ImageUrl="../NuoveImm/Img_Indietro.png"
                                Style="cursor: pointer;" ToolTip="Torna ai risultati della ricerca" OnClientClick="ConfermaEsci();"
                                CausesValidation="False" Visible="False" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="Immagini/Img_Salva.png" Style="cursor: pointer;"
                                ToolTip="Salva" TabIndex="-1" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                        </td>
                        <td>
                            <img id="imgCambiaAmm" alt="Cambia Amministratore di Condominio" onclick="document.getElementById('TextBox1').value='2';myOpacity.toggle();"
                                src="Immagini/Img_Cambia_Amministr.png" style="cursor: pointer;" />
                        </td>
                        <td>
                            <img id="ImgEventi0" alt="Allega File" border="0" onclick="window.open('../InvioAllegato.aspx?T=2&amp;ID=<%=vIdCondominio %>', 'Allegati', '');"
                                src="Immagini/Img_InviaAllegato.png" style="cursor: pointer;" />
                        </td>
                        <td style="text-align: right">
                            <img border="0" alt="Allegati" id="ImgEventi1" src="Immagini/Img_allegati.png" style="cursor: pointer"
                                onclick="ApriAllegati();" />
                        </td>
                        <td style="text-align: right">
                            <img border="0" alt="Eventi" id="ImgEventi" src="../NuoveImm/Img_Eventi.png" style="cursor: pointer;"
                                onclick="window.open('Eventi.aspx?IDCOND=<%=vIdCondominio %>','Eventi', '');" />
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="Immagini/Img_Esci.png" Style="cursor: pointer;"
                                ToolTip="Esci" OnClientClick="ConfermaEsci();" CausesValidation="False" TabIndex="-1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 98%;">
                    <tr>
                        <td class="style1">
                            Denominazione*
                        </td>
                        <td class="style1">
                            Codice*
                        </td>
                        <td class="style1">
                            Città
                        </td>
                        <td class="style1">
                            Prov.
                        </td>
                        <td class="style1">
                            Data Costituzione
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:TextBox ID="txtDenCondominio" runat="server" MaxLength="50" Font-Names="Arial"
                                Font-Size="8pt" Width="312px" TabIndex="1"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCodCondominio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                MaxLength="100" ReadOnly="True" Width="88px" TabIndex="-1"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="cmbComune" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="8pt" Height="20px" TabIndex="-1" Width="178px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtProvincia" runat="server" MaxLength="2" TabIndex="-1" Font-Names="Arial"
                                Font-Size="8pt" ReadOnly="True" Width="36px"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtDataCost" runat="server" MaxLength="10" TabIndex="2" Font-Names="Arial"
                                Font-Size="8pt" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Amministratore *
                        </td>
                        <td class="style1">
                            Tipo Gestione
                        </td>
                        <td class="style1">
                            Gestione*
                        </td>
                        <td class="style1">
                            Tipologia
                        </td>
                        <td class="style1">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:DropDownList ID="cmbAmministratori" runat="server" Font-Names="Arial" Font-Size="8pt"
                                TabIndex="3" Width="318px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="cmbTipoGestione" runat="server" Font-Names="Arial" Font-Size="8pt"
                                TabIndex="4" Width="98px">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGestione" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="5"
                                            TabIndex="5" Width="62px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGestioneAl" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="5" TabIndex="6" Width="62px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style1" colspan="2">
                            <asp:DropDownList ID="cmbTipoCond" runat="server" Font-Names="Arial" Font-Size="8pt"
                                TabIndex="7" Width="120px">
                                <asp:ListItem Value="C">Condominio</asp:ListItem>
                                <asp:ListItem Value="S">Super Cond.</asp:ListItem>
                                <asp:ListItem Value="T">C. Termica</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Codice Fornitore
                        </td>
                        <td class="style1" colspan="4">
                            <table cellpadding="0" cellspacing="2" width="100%">
                                <tr>
                                    <td class="style1" style="width: 80px">
                                        &nbsp;
                                    </td>
                                    <td class="style1" style="width: 100px">
                                        Codice Fiscale
                                    </td>
                                    <td class="style1">
                                        IBAN
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="5">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cmbFornitori" runat="server" BackColor="White" Font-Names="arial"
                                            Font-Size="8pt" Height="20px" TabIndex="8" Width="320px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="margin-left: 40px">
                                        &nbsp;
                                    </td>
                                    <td style="margin-left: 40px">
                                        <asp:ImageButton ID="btnFornitore" runat="server" ImageUrl="Immagini/img_AnFornitore.png"
                                            Style="cursor: pointer;" ToolTip="Apri anagrafe fornitore" OnClientClick="ApriFornitori();"
                                            CausesValidation="False" Visible="False" />
                                    </td>
                                    <td style="margin-left: 45px">
                                        <asp:TextBox ID="TxtCodFiscale" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="11" Width="100px" TabIndex="9"></asp:TextBox>
                                    </td>
                                    <td style="margin-left: 40px">
                                        <asp:DropDownList ID="cmbIban" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="200px" TabIndex="10">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Note
                        </td>
                        <td class="style1">
                            Edificio
                        </td>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td class="style1">
                        </td>
                        <td class="style1">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:TextBox ID="txtNote" runat="server" MaxLength="200" TabIndex="11" TextMode="MultiLine"
                                Font-Names="Arial" Font-Size="8pt" Width="280px" Height="44px"></asp:TextBox>
                        </td>
                        <td class="style1" colspan="4" style="vertical-align: top; text-align: left">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <div id="DivEdif" style="width: 420px; top: 178px; height: 48px; border-right: #6699ff thin solid;
                                            border-top: #6699ff thin solid; border-left: #6699ff thin solid; border-bottom: #6699ff thin solid;
                                            vertical-align: top; text-align: left; overflow: auto;">
                                            <asp:Label ID="lblEdifici" runat="server" Font-Names="Arial" Font-Size="9pt" Width="99%"></asp:Label>
                                        </div>
                                    </td>
                                    <td style="vertical-align: top; text-align: left">
                                        <asp:Image ID="imgAddEdif" runat="server" ImageUrl="Immagini/pencil-icon.png" onclick="document.getElementById('ScegCompVis').value!='1';myOpacityEdif.toggle();"
                                            Style="cursor: pointer" ToolTip="Scegli un Edificio" />
                                    </td>
                                </tr>
                            </table>
                            <div style="border: thin solid #6699ff; z-index: 300; left: 340px; width: 433px;
                                position: absolute; visibility: hidden; top: 199px; height: 315px; vertical-align: top;
                                background-color: #DCDCDC; text-align: left;" id="ScegliComp">
                                <table style="width: 99%; height: 99%">
                                    <tr>
                                        <td style="vertical-align: top; width: 426px; height: 98%; text-align: left">
                                            <div style="overflow: auto; width: 100%; height: 99%">
                                                <asp:CheckBoxList ID="ListEdifici" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                    Style="left: 334px; top: 251px" Width="403px">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; width: 426px; text-align: right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Immagini/Aggiungi.png"
                                                Style="z-index: 103; left: 744px; cursor: pointer; top: 26px" ToolTip="Esci"
                                                CausesValidation="False" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="copriExit" style="z-index: 900; position: absolute; width: 800px; height: 600px;
                                top: 0px; left: 0px; visibility: hidden; background-repeat: no-repeat; background-attachment: fixed;
                                background-color: #FFFFFF;">
                                <asp:Image ID="Image2" runat="server" BackColor="White" ImageUrl="../ImmDiv/DivUscitaInCorso2.jpg"
                                    Style="z-index: 100; left: 42px; position: absolute; top: 66px;" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="MyTab" class="tabber " style="visibility: <%=tabvisibility%>; width: 97%;">
                    <div class="tabbertab <%=tabdefault1%>" title="Convocazioni">
                        <uc1:TabConvocazione ID="TabConvocazione1" runat="server" />
                    </div>
                    <div class="tabbertab <%=tabdefault2%>" title="Amministratori">
                        <uc2:TabAmministratori ID="TabAmministratori1" runat="server" />
                    </div>
                    <div class="tabbertab <%=tabdefault3%>" title="Millesimi">
                        <uc3:TabMillesimalil ID="TabMillesimalil1" runat="server" />
                    </div>
                    <div class="tabbertab <%=tabdefault4%>" title="Dati Tecnici">
                        <uc4:TabDatiTecnici ID="TabDatiTecnici1" runat="server" />
                    </div>
                    <div class="tabbertab <%=tabdefault5%>" title="Inquilini">
                        <uc5:TabInquilini ID="TabInquilini1" runat="server" />
                    </div>
                    <div class="tabbertab <%=tabdefault6%>" title="Contabilità">
                        <uc6:Tab_Contabilita ID="Tab_Contabilita1" runat="server" />
                    </div>
                    <div class="tabbertab <%=tabdefault7%>" title="Morosità">
                        <uc7:TabMorosita ID="TabMorosita1" runat="server" />
                    </div>
                    <div class="tabbertab <%=tabdefault8%>" title="Pagamenti">
                        <uc8:Tab_Pagamenti ID="Tab_Pagamenti1" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="border: thin none #3366ff; position: absolute; top: 0px; left: 0px; width: 802px;
                    height: 582px; background-color: #dedede; z-index: 201; visibility: hidden; vertical-align: top;
                    text-align: left; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
                    margin-right: 10px;" id="CambiaAmm" visible="false">
                    <br />
                    <asp:Image ID="Image1" runat="server" BackColor="White" Height="172px" ImageUrl="../ImmDiv/DivMGrande.png"
                        Style="z-index: 100; left: 15px; position: absolute; top: 69px" Width="759px" />
                    <br />
                    <table style="z-index: 200; left: 33px; width: 710px; position: absolute; top: 82px;">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 361px">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                Text="Amministratore corrente" Width="162px"></asp:Label>
                                        </td>
                                        <td style="width: 151px">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                Text="Data Inizio" Width="81px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                Text="Data Fine *" Width="81px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 361px">
                                            <asp:TextBox ID="txtAmmCorrente" runat="server" Width="331px" BackColor="White" TabIndex="20"></asp:TextBox>
                                        </td>
                                        <td style="width: 151px">
                                            <asp:TextBox ID="txtDataInizio" runat="server" Width="100px" BackColor="White" TabIndex="21"></asp:TextBox>&nbsp;<asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataInizio"
                                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="16px"
                                                Style="z-index: 600; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                Width="16px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDataFine" runat="server" Width="100px" BackColor="White" TabIndex="22"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataFine"
                                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="16px"
                                                Style="z-index: 600; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                Width="16px"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 361px; height: 21px;">
                                        </td>
                                        <td style="width: 151px; height: 21px;">
                                        </td>
                                        <td style="height: 21px">
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 361px">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                Text="Nuovo Amministratore" Width="162px"></asp:Label>
                                        </td>
                                        <td style="width: 152px">
                                            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                Text="Data Inizio*" Width="81px"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 361px">
                                            <asp:DropDownList ID="CmbAmministratori2" runat="server" Style="top: 109px; left: 9px;
                                                right: 481px;" Font-Names="Arial" Font-Size="9pt" TabIndex="23" Width="336px"
                                                BackColor="White">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 152px">
                                            <asp:TextBox ID="txtNuovaDataInizio" runat="server" Width="100px" BackColor="White"
                                                TabIndex="24"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNuovaDataInizio"
                                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="16px"
                                                Style="z-index: 600; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                Width="16px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 361px; height: 22px;">
                                        </td>
                                        <td style="width: 152px; height: 22px">
                                        </td>
                                        <td style="height: 22px">
                                            <asp:ImageButton ID="btnSalvaCambioAmm" runat="server" ImageUrl="../NuoveImm/Img_SalvaVal.png"
                                                ToolTip="Salva le informazioni" TabIndex="25" />
                                            <img id="imgCambiaAmm0" onclick="document.getElementById('TextBox1').value=='1';myOpacity.toggle();PulisciCampiAmminist()"
                                                src="../NuoveImm/Img_AnnullaVal.png" alt="ANNULLA" style="left: 185px; cursor: pointer;
                                                top: 23px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        myOpacity = new fx.Opacity('CambiaAmm', { duration: 200 });
        if (document.getElementById('TextBox1').value != '2') {
            myOpacity.hide();
            document.getElementById('CambiaAmm').style.visibility = 'hidden';
        }

        myOpacityEdif = new fx.Opacity('ScegliComp', { duration: 200 });
        if (document.getElementById('ScegCompVis').value != '2') {
            myOpacityEdif.hide();
        }

        myOpacitySuperCond = new fx.Opacity('SuperCond', { duration: 200 });
        if (document.getElementById('ScegSuperCondVis').value != '2') {
            myOpacitySuperCond.hide();
        }


        if (document.getElementById('ImgVisibility').value != '1') {
            document.getElementById('imgCambiaAmm').style.visibility = 'hidden';
            document.getElementById('Tab_Contabilita1_imgAddConv').style.visibility = 'hidden';
            document.getElementById('ImgEventi0').style.visibility = 'hidden';
            document.getElementById('TabDatiTecnici1_imgAddSuperCond').style.visibility = 'hidden';


        }
    </script>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
        document.getElementById('splash').style.visibility = 'hidden';
    </script>
    <div id="copri" style="z-index: 800; position: absolute; width: 800px; height: 600px;
        top: 0px; left: 0px; visibility: hidden; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    </div>
</body>
</html>
