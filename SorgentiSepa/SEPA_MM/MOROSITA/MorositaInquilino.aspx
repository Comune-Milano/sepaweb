<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MorositaInquilino.aspx.vb"
    Inherits="MOROSITA_MorositaInquilino" %>

<%@ Register Src="Tab_MorositaInquilino_Gen.ascx" TagName="Tab_Morosita_Generale"
    TagPrefix="uc1" %>
<%@ Register Src="Tab_MorositaInquilino_Bol.ascx" TagName="Tab_Morosita_Bollette"
    TagPrefix="uc2" %>
<%@ Register Src="Tab_MorositaInquilino_Bol_TUTTE.ascx" TagName="Tab_Morosita_BolletteTUTTE"
    TagPrefix="uc3" %>
<%@ Register Src="Tab_MorositaInquilino_STATO_MG.ascx" TagName="Tab_Morosita_STATO_MG"
    TagPrefix="uc4" %>
<%@ Register Src="Tab_MorositaInquilino_STATO_MA.ascx" TagName="Tab_Morosita_STATO_MA"
    TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">



    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }
    function Conferma() {
        chiediConferma = window.confirm("Attenzione...Procedere con l\'annullo?");
        if (chiediConferma == true) {

            document.getElementById('Confirm').value = '1';
        }

    };

    function $onkeydown() {

        if (event.keyCode == 13) {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }


    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
        //        o.value = o.value.replace('.', ',');
        document.getElementById('txtModificato').value = '1';
    }



    function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a.substring(a.length - 3, 0).length >= 4) {
                var decimali = a.substring(a.length, a.length - 2);
                var dascrivere = a.substring(a.length - 3, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {

                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                }
                risultato = dascrivere + risultato + ',' + decimali
                //document.getElementById(obj.id).value = a.replace('.', ',')
                document.getElementById(obj.id).value = risultato
            }
            else {
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
    }


    function DelPointer(obj) {
        obj.value = obj.value.replace('.', '');
        document.getElementById(obj.id).value = obj.value;

    }

    function $onkeydown() {
        if (event.keyCode == 46) {
            event.keyCode = 0;
        }
    }


</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>MODULO MOROSITA INQUILINO</title>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
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

        //window.onbeforeunload = confirmExit; 



        function ConfermaEsci() {
            //alert('ciao');
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                if (document.getElementById('txtVisualizza').value < 2) {
                    chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                    if (chiediConferma == false) {
                        document.getElementById('txtModificato').value = '111';
                        //document.getElementById('USCITA').value='0';
                    }
                }
            }
        }



        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda morosità premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda morosità premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
            }
        }



        //function StampaMorosita() {
        //    var sicuro = confirm('Attenzione...Confermi di voler Ristampare i MAV?');
        //    if (sicuro == true) {
        //    document.getElementById('txtAnnullo').value='1';
        //    }
        //    else
        //    {
        //    document.getElementById('txtAnnullo').value='0'; 
        //    }
        //}

        function ApriPagamentoManuale() {


            if (document.getElementById('txtModificato').value == '1') {
                alert('Sono state apportate modifiche, salvare prima di stampare!')
                return;
            }


            //document.getElementById('copri').style.visibility = 'visible';

            //if (document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value > 0) {
            window.showModalDialog('PagamentoManuale.aspx?ID_BOLLETTA=' + document.getElementById('txtIdBollette').value + '&IDCON=' + document.getElementById('txtConnessione').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');

            //window.showModalDialog('PagamentoManuale.aspx?', 'window', 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');

            //document.getElementById('txtIdManutenzione').value = 1
            //}
            //else { 
            //        alert('Selezionare un elemento dalla lista!')
            //        }
        }


        function ApriProcedureMG() {
            //document.getElementById('copri').style.visibility = 'visible';
            //&ID_ANAGRAFICA=' + document.getElementById('txtIdAnagrafica').value
            var dialogResults = window.showModalDialog('MorositaInquilino_Procedure.aspx?CONTA_MAV=3' + '&ID_MOROSITA=' + document.getElementById('txtIdMorosita').value + '&ID_CONTRATTO=' + document.getElementById('txtIdContratto').value + '&ID_MOR_LETTERA=' + document.getElementById('txtIdMorositaLett1').value + '&IDCON=' + document.getElementById('txtConnessione').value + '&IDVISUAL=' + document.getElementById('SOLO_LETTURA').value + '&FUNZIONE_1=' + document.getElementById('txtFUNZIONE_MG').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');

            if ((dialogResults != undefined) && (dialogResults != false)) {
                document.getElementById('txtIdMorosita').value = dialogResults;

                document.getElementById('txtContaEventiMG').value = 0;
                document.getElementById('txtContaEventiMA').value = 0;
            }

            //            if (document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value > 0) {
            //              window.showModalDialog('MorositaInquilino_Procedure.aspx?ID_MANUTENZIONE=' + document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value + '&IDCON=' + document.getElementById('Tab_Manu_Dettagli_txtIdConnessione').value + '&IDPADRE=' + document.getElementById('Tab_Manu_Dettagli_txtIdManuPadre').value + '&RESIDUO=' + document.getElementById('Tab_Manu_Dettagli_txtResiduoConsumo').value + '&IDVISUAL=' + document.getElementById('SOLO_LETTURA').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
            //                //document.getElementById('txtIdManutenzione').value = 1
            //            }
            //            else { 
            //                    alert('Selezionare un elemento dalla lista!')
            //                    }
        }

        function ApriProcedureMA() {
            if (document.getElementById('txtContaMAV').value == '2') {
                var dialogResults = window.showModalDialog('MorositaInquilino_Procedure.aspx?CONTA_MAV=2' + '&ID_MOROSITA=' + document.getElementById('txtIdMorosita').value + '&ID_CONTRATTO=' + document.getElementById('txtIdContratto').value + '&ID_MOR_LETTERA=' + document.getElementById('txtIdMorositaLett2').value + '&IDCON=' + document.getElementById('txtConnessione').value + '&IDVISUAL=' + document.getElementById('SOLO_LETTURA').value + '&FUNZIONE_1=' + document.getElementById('txtFUNZIONE_MA').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
            }
            else {
                var dialogResults = window.showModalDialog('MorositaInquilino_Procedure.aspx?CONTA_MAV=1' + '&ID_MOROSITA=' + document.getElementById('txtIdMorosita').value + '&ID_CONTRATTO=' + document.getElementById('txtIdContratto').value + '&ID_MOR_LETTERA=' + document.getElementById('txtIdMorositaLett2').value + '&IDCON=' + document.getElementById('txtConnessione').value + '&IDVISUAL=' + document.getElementById('SOLO_LETTURA').value + '&FUNZIONE_1=' + document.getElementById('txtFUNZIONE_MA').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
            }

            if ((dialogResults != undefined) && (dialogResults != false)) {
                document.getElementById('txtIdMorosita').value = dialogResults;

                document.getElementById('txtContaEventiMG').value = 0;
                document.getElementById('txtContaEventiMA').value = 0;
            }



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

        function ApriBolTutte() {
            if (document.getElementById('Tab_Morosita_BolletteTUTTE_txtIdComponente').value != '1') {
                var fin;
                fin = window.open('AnteprimaBolletta.aspx?ID=' + document.getElementById('Tab_Morosita_BolletteTUTTE_txtIdComponente').value, 'Anteprima' + document.getElementById('Tab_Morosita_BolletteTUTTE_txtIdComponente').value, 'top=0,left=0,resizable=yes,scrollbars=yes');
                fin.focus();
            }
            else {
                alert('Selezionare una bolletta dalla lista!')
            }
        }


        function ApriBolInquilino() {
            if (document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value != '1') {
                var fin;
                fin = window.open('AnteprimaBolletta.aspx?ID=' + document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value, 'Anteprima' + document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value, 'top=0,left=0,resizable=yes,scrollbars=yes');
                fin.focus();
            }
            else {
                alert('Selezionare una bolletta dalla lista!')
            }
        }

        function ApriBolSolleciti() {
            if (document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value != '1') {
                var fin;
                fin = window.open('../Contratti/ElencoSolleciti.aspx?ID=' + document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value, 'Solleciti', 'height=350,top=0,left=0,width=350');
                fin.focus();
            }
            else {
                alert('Selezionare una bolletta dalla lista!')
            }
        }

   
    </script>
</head>
<body bgcolor="#f2f5f1" text="#ede0c0">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px" id="TABLE1">
            <td style="width: 800px; height: 1px;" id="TD_Principale">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
                <table style="width: 760px">
                    <tr>
                        <td style="width: 76px">
                            &nbsp;<asp:ImageButton ID="btnINDIETRO" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                                OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 100;
                                left: 16px; position: static; top: 29px" TabIndex="-1" ToolTip="Indietro" />
                        </td>
                        <td style="width: 76px">
                            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                                OnClientClick="document.getElementById('USCITA').value='1'" Style="z-index: 100;
                                left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Salva" Visible="False" />
                        </td>
                        <td style="width: 76px;">
                            <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_RiStampaPiccolo.png"
                                OnClientClick="document.getElementById('USCITA').value='1'; " Style="z-index: 100;
                                left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Ri Stampa i MAV emessi con le relative lettere" />
                        </td>
                        <td style="width: 76px">
                            <asp:ImageButton ID="btnStampa0" runat="server" ImageUrl="~/MOROSITA/Immagini/Ristampa Solo MAV.png"
                                OnClientClick="document.getElementById('USCITA').value='1'; " Style="z-index: 100;
                                left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Ri Stampa solo MAV emessi o aggiornati " />
                        </td>
                        <td style="width: 76px">
                        </td>
                        <td style="width: 76px">
                        </td>
                        <td style="width: 76px">
                        </td>
                        <td style="width: 76px">
                        </td>
                        <td style="width: 76px">
                        </td>
                        <td style="width: 76px">
                        </td>
                        <td style="width: 76px">
                            <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                                OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 125;
                                left: 600px; position: static; top: 29px" TabIndex="-1" ToolTip="Esci" />
                        </td>
                    </tr>
                </table>
                &nbsp;<asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px"
                    TabIndex="-1" Width="760px">CONDUTTORE..................................................................................................................................................................................................................................</asp:Label>
                <table style="width: 760px">
                    <tr>
                        <td>
                            <asp:Label ID="lblCognome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Cognome:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCognome" runat="server" Enabled="False" MaxLength="50" Style="z-index: 10;
                                left: 408px; top: 171px" TabIndex="15" Width="250px"></asp:TextBox>
                        </td>
                        <td style="width: 30px">
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblNome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Nome:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNome" runat="server" Enabled="False" MaxLength="50" Style="z-index: 10;
                                left: 408px; top: 171px" TabIndex="15" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:HyperLink ID="HL_Anagrafica" runat="server" Style="font-size: 8pt; cursor: hand;
                                color: blue; font-style: italic; text-decoration: underline;" Width="60px" ToolTip="Dettagli Scheda Anagrafica">Anagrafica</asp:HyperLink>
                        </td>
                    </tr>
                </table>
                &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px"
                    TabIndex="-1" Width="760px">IMMOBILE........................................................................................................................................................................................................</asp:Label><br />
                <table style="width: 760px">
                    <tr>
                        <td>
                            <asp:Label ID="lblUbicazione" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                TabIndex="-1" Width="60px">Ubicazione:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIndirizzo" runat="server" Enabled="False" Height="50px" MaxLength="500"
                                Style="z-index: 10; left: 408px; top: 171px" TabIndex="15" TextMode="MultiLine"
                                Width="250px"></asp:TextBox>
                        </td>
                        <td style="width: 30px">
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCodUnita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Cod. Unità:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodUnita" runat="server" Enabled="False" MaxLength="50" Style="z-index: 10;
                                left: 408px; top: 171px" TabIndex="15" Width="150px">ABCFDRTYEDSXCZWER</asp:TextBox>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                            <asp:Label ID="lblTipoUnita" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                TabIndex="-1" Width="30px">Tipo:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTipologia" runat="server" Enabled="False" MaxLength="255" Style="z-index: 10;
                                left: 408px; top: 171px" TabIndex="15" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:HyperLink ID="HL_Complesso" runat="server" Style="font-size: 8pt; cursor: hand;
                                color: blue; font-style: italic; text-decoration: underline;" ToolTip="Dettagli Complesso/Edificio"
                                Width="60px">Complesso Edificio</asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                    Width="760px">CONTRATTO........................................................................................................................................................................................................</asp:Label><br />
                <table style="width: 760px">
                    <tr>
                        <td>
                            <asp:Label ID="lblCodice" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Rapporto Cod.:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodContratto" runat="server" Enabled="False" MaxLength="50" Style="z-index: 10;
                                left: 408px; top: 171px" TabIndex="15" Width="250px"></asp:TextBox>
                        </td>
                        <td style="width: 30px">
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCodiceGIMI" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                TabIndex="-1" Width="60px">Cod. GIMI:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodGIMI" runat="server" Enabled="False" MaxLength="50" Style="z-index: 10;
                                left: 408px; top: 171px" TabIndex="15" Width="150px">ABCFDRTYEDSXCZWER</asp:TextBox>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                            <asp:Label ID="lblTipologiaContratto" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                TabIndex="-1" Width="50px">Tipologia:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTipologiaContratto" runat="server" Enabled="False" MaxLength="255"
                                Style="z-index: 10; left: 408px; top: 171px" TabIndex="15" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:HyperLink ID="HL_Contratto" runat="server" Style="font-size: 8pt; cursor: hand;
                                color: blue; font-style: italic; text-decoration: underline;" ToolTip="Dettagli Cintratto"
                                Width="40px">Contratto</asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <table style="width: 760px">
                    <tr>
                        <td>
                            <asp:Label ID="lblStatoMG" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">STATO MG</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbStatoMG" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 56px" TabIndex="5" Width="300px" Enabled="False" Font-Bold="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40px">
                        </td>
                        <td>
                            <asp:Label ID="lblStatoMA" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">STATO MA</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbStatoMA" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 56px" TabIndex="5" Width="300px" Enabled="False" Font-Bold="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div class="tabber" id="tab1">
                    <div class="tabbertab <%=Tabber1%>" style="background-color: white; width: 775px">
                        <h2>
                            Morosità</h2>
                        <uc1:Tab_Morosita_Generale ID="Tab_Morosita_Generale" runat="server" Visible=" true" />
                    </div>
                    <div class="tabbertab <%=Tabber2%>" style="background-color: white; width: 775px">
                        <h2>
                            Bollette Morosità</h2>
                        <uc2:Tab_Morosita_Bollette ID="Tab_Morosita_Bollette" runat="server" Visible=" true" />
                    </div>
                    <div class="tabbertab <%=Tabber3%>" style="background-color: white; width: 775px">
                        <h2>
                            Tutte le Bollette</h2>
                        <uc3:Tab_Morosita_BolletteTUTTE ID="Tab_Morosita_BolletteTUTTE" runat="server" Visible=" true" />
                    </div>
                    <div class="<%=TabberHideMG%> <%=Tabber4%>" style="background-color: white; width: 775px">
                        <h2>
                            Procedure M.A.V. MG</h2>
                        <uc4:Tab_Morosita_STATO_MG ID="Tab_Morosita_STATO_MG" runat="server" Visible=" true" />
                    </div>
                    <div class="<%=TabberHideMA%> <%=Tabber5%>" style="background-color: white; width: 775px">
                        <h2>
                            Procedure M.A.V. MA</h2>
                        <uc5:Tab_Morosita_STATO_MA ID="Tab_Morosita_STATO_MA" runat="server" Visible=" true" />
                    </div>
                </div>
        </table>
        <br />
        <br />
        <asp:HiddenField ID="USCITA" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="SOLO_LETTURA" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtModificato" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtindietro" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtConnessione" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txttab" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtAnnullo" runat="server" />
        <asp:HiddenField ID="txtVisualizza" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdMorosita" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdAnagrafica" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdContratto" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdBollette1" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdBollette2" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdMorositaLett1" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdMorositaLett2" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="idMorLeft" runat="server" Value="-1"></asp:HiddenField>
        <asp:HiddenField ID="idMorRight" runat="server" Value="-1"></asp:HiddenField>
        <asp:HiddenField ID="txtContaMAV" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="EVENTO_MODIFICATO" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtContaEventiMG" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtContaEventiMA" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtFUNZIONE_MG" runat="server" Value="-1"></asp:HiddenField>
        <asp:HiddenField ID="txtFUNZIONE_MA" runat="server" Value="-1"></asp:HiddenField>
        <asp:HiddenField ID="Confirm" runat="server" Value="0"></asp:HiddenField>
    </div>
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();

      
    </script>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
