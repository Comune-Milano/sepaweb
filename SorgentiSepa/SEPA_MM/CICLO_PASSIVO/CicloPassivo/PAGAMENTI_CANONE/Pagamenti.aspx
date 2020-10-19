<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pagamenti.aspx.vb" Inherits="PAGAMENTI_CANONE_Pagamenti" %>

<%@ Register Src="Tab_SAL_Riepilogo.ascx" TagName="Tab_SAL_Riepilogo" TagPrefix="uc1" %>
<%@ Register Src="Tab_SAL_RiepilogoProg.ascx" TagName="Tab_SAL_RiepilogoProg" TagPrefix="uc2" %>
<%@ Register Src="Tab_SAL_Dettagli.ascx" TagName="Tab_SAL_Dettagli" TagPrefix="uc3" %>
<%@ Register Src="Tab_SAL_Ripartizioni.ascx" TagName="Tab_SAL_Ripartizioni" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">

    function apriEventi() {
        if (document.getElementById('HiddenIDPagamento').value == '') {
            apriAlert('Impossibile visualizzare gli eventi!Salvare prima il pagamento', 300, 150, 'Attenzione', null, null);
        }
        else {
            window.open('Report/Eventi.aspx?ID_PAGAMENTO=' + document.getElementById('HiddenIDPagamento').value, "WindowPopup", "scrollbars=1, width=800px, height=600px, resizable");
        }

    };

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }


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


    function DelPointer(obj) {
        obj.value = obj.value.replace('.', '');
        document.getElementById(obj.id).value = obj.value;

    }


    function $onkeydown() {
        if (event.keyCode == 46) {
            event.keyCode = 0;
        }
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



</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>MODULO GESTIONE PAGAMENTI a CANONE</title>
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <link href="../../../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="Funzioni.js" type="text/javascript"></script>
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
    <script language="javascript" type="text/javascript">

        //window.onbeforeunload = confirmExit; 


        function ApprovaPagamento() {
            var sicuro = confirm('Sei sicuro di voler approvare questa pagamento?');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            }
            else {
                document.getElementById('txtElimina').value = '0';
            }
        }

        function StampaSAL() {

            if (document.getElementById('txtModificato').value == '1') {
                alert('Sono state apportate modifiche, salvare prima di stampare!')
                document.getElementById('txtElimina').value = '0';
                return;
            }

            var sicuro = confirm('Sei sicuro di voler emettere il SAL?');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            }
            else {
                document.getElementById('txtElimina').value = '0';
            }
        }

        function AnnullaSAL() {
            var sicuro = confirm('Sei sicuro di voler annullare il SAL emesso?');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            }
            else {
                document.getElementById('txtElimina').value = '0';
            }
        }


        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                if (document.getElementById('txtVisualizza').value != '1') {
                    chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                    if (chiediConferma == false) {
                        document.getElementById('txtModificato').value = '111';
                        //document.getElementById('USCITA').value='0';
                    }
                    else {
                        Blocca_SbloccaMenu(0);
                    }
                }

            }
            else {
                Blocca_SbloccaMenu(0);
            }
        }



        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda pagamenti premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda pagamenti premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
            }
        }



        function StampaPagamento() {
            var Conferma

            if (document.getElementById('txtModificato').value == '1') {
                alert('Sono state apportate modifiche, salvare prima di stampare!')
                return;
            }

            if (document.getElementById('txtStatoPagamento').value == '0') {

                Conferma = window.confirm("Attenzione...Confermi di voler emettere un pagamento?");
                if (Conferma == true) {

                    document.getElementById('txtElimina').value = '1';



                    //document.getElementById('txtModificato').value = '0';
                    //document.getElementById('txtStatoPagamento').value = '1';
                    //document.getElementById('txtSTATO').value = '3';

                    //document.getElementById('cmbStatoPAGAMENTO').text="3";

                    //document.getElementById('btnSalva').style.visibility = 'visible';  
                    //document.getElementById('btnApprovazione').style.visibility = 'hidden'; 

                    //document.getElementById('btnAnnulla').style.visibility = 'hidden';  
                    //document.getElementById('btnStampaSAL').style.visibility = 'visible';  

                    //document.getElementById('imgStampa').style.visibility = 'visible';

                }
                else {
                    document.getElementById('txtElimina').value = '0';
                }
            }
            else {
                document.getElementById('txtElimina').value = '1';


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

        function AllegaFile() {
            if ((document.getElementById('idSAL').value == '') || (document.getElementById('idSAL').value == '0')) {
                alert('E\' necessario salvare il SAL prima di allegare documenti!');
            } else {
                CenterPage('../../../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('idSAL').value, 'Allegati', 1000, 800);
            };
        };

        function CenterPage(pageURL, title, w, h) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        };




    </script>
    <style type="text/css">
        .style1 {
            width: 11%;
        }

        .style4 {
            width: 60px;
            height: 30px;
        }

        .style5 {
            width: 30px;
            height: 30px;
        }

        .style6 {
            width: 85px;
            height: 30px;
        }
    </style>
</head>
<body class="sfondo">
    <%-- <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>--%>
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <div>
            <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
                EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="500" Position="BottomRight"
                OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
            </telerik:RadNotification>
        </div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td class="TitoloModulo">Pagamenti a canone 
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnINDIETRO" runat="server" Text="Indietro" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';ConfermaEsci();}"
                                        ToolTip="Indietro" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1'}"
                                        ToolTip="Salva" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnApprovazione" runat="server" Text="Approva" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1'; document.getElementById('USCITA').value='1';  ApprovaPagamento();}"
                                        ToolTip="Approva il pagamento visualizzato" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnAnnulla" runat="server" Text="Annulla SAL" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1'; document.getElementById('USCITA').value='1';  AnnullaSAL();}"
                                        ToolTip="Annulla il SAL visualizzato" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnStampaSAL" runat="server" Text="Stampa SAL" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1'; document.getElementById('USCITA').value='1';  StampaSAL();}"
                                        ToolTip="Stampa il SAL" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnRielbSal" runat="server" Text="Rielabora SAL"
                                        Style="z-index: 100; left: 584px; position: static; top: 32px"
                                        ToolTip="Rielabora il SAL" />
                                </td>
                                <td></td>
                                <td>
                                    <telerik:RadButton ID="imgStampa" runat="server" Text="Stampa Pagamento" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1'; document.getElementById('USCITA').value='1';  StampaPagamento();}"
                                        ToolTip="Stampa Mandato di Pagamento" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnRielaboraPagamento" runat="server" Text="Rielabora Pagamento"
                                        Style="z-index: 100; left: 584px; position: static; top: 32px"
                                        ToolTip="Rielabora il Pagamento" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="imgEventi" runat="server" Text="Eventi" ToolTip="Eventi Scheda Pagamenti"
                                        AutoPostBack="false" OnClientClicking="function(sender, args){apriEventi();}"
                                        CausesValidation="False">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnAllegati" runat="server" Text="Allegati" AutoPostBack="False"
                                        ToolTip="Allegati SAL" OnClientClicking="function(sender,args){AllegaFile();}" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="imgUscita" runat="server" Text="Esci" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';ConfermaEsci();}"
                                        ToolTip="Esci" />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%" cellpadding="1" cellspacing="0">
                            <tr>
                                <td class="TitoloH1" style="text-align: left">Dettagli Voce
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 20%">
                                                <asp:Label ID="lblVal1" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                                    Width="230px">Importo Totale del contratto (compreso oneri)</asp:Label>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:TextBox ID="txtImporto" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                                    Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                    Width="120px" Enabled="False" Font-Size="8pt"></asp:TextBox>
                                                <asp:Label ID="lblEuro1" runat="server" Style="text-align: right" Text="&#8364;"
                                                    Width="16px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td style="width: 58px"></td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblVal3" runat="server" Style="z-index: 100; left: 8px; top: 88px">Totale Prenotazione</asp:Label>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtImporto2" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                                    Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                    Width="120px" Enabled="False" Font-Size="8pt"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEuro3" runat="server" Font-Bold="False" Style="text-align: right"
                                                    Text="&#8364;" Width="16px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <asp:Label ID="lblVal2" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                                    Width="230px" Visible="False">Budget assestato o consistenza assestante</asp:Label>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:TextBox ID="txtImporto1" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                                    Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                    Width="120px" Enabled="False" Font-Size="8pt" Visible="False"></asp:TextBox>
                                                <asp:Label ID="lblEuro2" runat="server" Style="text-align: right" Text="&#8364;"
                                                    Width="16px" Visible="False"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td style="width: 58px"></td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblVal4" runat="server" Style="z-index: 100; left: 8px; top: 88px">Totale Emesso</asp:Label>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtImporto3" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                                    Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                    Width="120px" Enabled="False" Font-Size="8pt"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEuro4" runat="server" Style="text-align: right" Text="&#8364;"
                                                    Width="16px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <asp:Label ID="lblval6" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                                    Width="230px">Disponibilità residua</asp:Label>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:TextBox ID="txtImporto5" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                                    Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                    Width="120px" Enabled="False" Font-Size="8pt"></asp:TextBox>
                                                <asp:Label ID="lblEuro6" runat="server" Style="text-align: right" Text="&#8364;"
                                                    Width="16px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td style="width: 58px">
                                                <asp:TextBox ID="txtIVA" runat="server" Enabled="False" Font-Bold="True" MaxLength="30"
                                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                    Width="20px" Visible="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblVal5" runat="server" Style="z-index: 100; left: 8px; top: 88px">Totale Liquidazione</asp:Label>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtImporto4" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                                    Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                    Width="120px" Enabled="False" Font-Size="8pt"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEuro5" runat="server" Style="text-align: right" Text="&#8364;"
                                                    Width="16px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td class="TitoloH1" style="text-align: left">Dettagli pagamento
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="vertical-align: middle; text-align: left" width="10%" class="TitoloH1">
                                                <asp:Label ID="lblPagamento" runat="server" Font-Size="8pt" Width="120px">PAGAMENTO N°</asp:Label>
                                            </td>
                                            <td style="vertical-align: middle; text-align: left" class="TitoloH1">
                                                <asp:Label ID="lblPROG_Pagamento" runat="server" Font-Bold="True" Font-Size="8pt"
                                                    Width="130px"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top; width: 20px; text-align: left">&nbsp;
                                            </td>
                                            <td style="vertical-align: middle; text-align: left" class="style1">&nbsp;
                                            </td>
                                            <td style="vertical-align: middle; text-align: left">&nbsp;
                                            </td>
                                            <td style="vertical-align: middle; text-align: left">&nbsp;
                                            </td>
                                            <td style="vertical-align: middle; text-align: left">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle; text-align: left" width="10%">
                                                <asp:Label ID="Label3" runat="server" Style="z-index: 100; left: -46px; top: -302px"
                                                    Width="110px">Fornitore</asp:Label>
                                            </td>
                                            <td style="vertical-align: middle; text-align: left" class="TitoloH1">
                                                <asp:HyperLink ID="HLink_Fornitore" runat="server" Font-Bold="True" Font-Underline="True"
                                                    Font-Size="8pt" Style="cursor: pointer" Width="200px">123456789 123456789 123456789 </asp:HyperLink>
                                            </td>
                                            <td style="vertical-align: top; width: 20px; text-align: left"></td>
                                            <td style="vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label7" runat="server" Style="z-index: 100; left: -46px; top: -302px">Descrizione Contratto</asp:Label>
                                            </td>
                                            <td style="vertical-align: middle; text-align: left">
                                                <asp:TextBox ID="txtDescrizioneAppalto" runat="server" MaxLength="20" Style="z-index: 10; left: 408px; top: 171px"
                                                    Width="320px" Font-Size="8pt" Enabled="False"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="vertical-align: middle; text-align: left">
                                                <asp:Label ID="lblAppalto" runat="server" Style="z-index: 100; left: 24px; top: 32px"
                                                    Width="110px">Num. Repertorio</asp:Label>
                                            </td>
                                            <td style="vertical-align: middle; text-align: left" class="TitoloH1">
                                                <asp:HyperLink ID="HLink_Appalto" runat="server" Font-Bold="True" Font-Underline="True"
                                                    Font-Size="8pt" Style="cursor: pointer" Width="170px">123456789 123456789 </asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle; text-align: left" width="10%">
                                                <asp:Label ID="lblDescrizione" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                                    Width="120px">Descrizione Pagamento</asp:Label>
                                            </td>
                                            <td style="vertical-align: middle; text-align: left">
                                                <asp:TextBox ID="txtDescrizioneP" runat="server" MaxLength="2000" Style="left: 80px; top: 88px"
                                                    Width="320px"></asp:TextBox>
                                            </td>
                                            <td style="vertical-align: top; width: 20px; text-align: left"></td>
                                            <td style="vertical-align: middle; text-align: left" colspan="4">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td class="TitoloH1" style="text-align: left">
                                                            <asp:Label ID="lblDataDel" runat="server" Font-Size="8pt" Style="text-align: left;"
                                                                Width="53px">a tutto il</asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtDataSal" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                                DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                                                <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa" LabelWidth="28px"
                                                                    Width="70px">
                                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                                </DateInput>
                                                                <Calendar ID="Calendar3" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                        </telerik:RadCalendarDay>
                                                                    </SpecialDays>
                                                                </Calendar>
                                                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td class="TitoloH1">
                                                            <asp:Label ID="Label2" runat="server" Font-Size="8pt" Style="text-align: left;"
                                                                Width="80px">data emissione</asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtDataDel" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                                DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                                                <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa" LabelWidth="28px"
                                                                    Width="70px">
                                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                                </DateInput>
                                                                <Calendar ID="Calendar1" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                        </telerik:RadCalendarDay>
                                                                    </SpecialDays>
                                                                </Calendar>
                                                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td class="TitoloH1">
                                                            <asp:Label ID="Label8" runat="server" Font-Size="8pt" Style="text-align: left;"
                                                                Width="80px">data scadenza</asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtScadenza" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                                DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                                                <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa" LabelWidth="28px"
                                                                    Width="70px">
                                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                                </DateInput>
                                                                <Calendar ID="Calendar2" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                        </telerik:RadCalendarDay>
                                                                    </SpecialDays>
                                                                </Calendar>
                                                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle; text-align: left" width="10%">
                                                <asp:Label ID="Label5" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                                    Width="110px">STATO PAGAMENTO</asp:Label>
                                            </td>
                                            <td style="vertical-align: middle; text-align: left">
                                                <telerik:RadComboBox ID="cmbStatoPAGAMENTO" Width="100%" AppendDataBoundItems="true" Enabled="false"
                                                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="0" Text="DA APPROVARE" />
                                                        <telerik:RadComboBoxItem Value="1" Text="APPROVATO" />
                                                        <telerik:RadComboBoxItem Value="2" Text="STAMPATO IL SAL" />
                                                        <telerik:RadComboBoxItem Value="3" Text="STAMPATO IL PAGAMENTO" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td style="vertical-align: top; width: 20px; text-align: left"></td>
                                            <td style="vertical-align: middle; text-align: left; width: 2%;">
                                                <asp:Label ID="lblSAL" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                                                    ToolTip="Indica lo stato ed il numero del SAL">STATO SAL</asp:Label>
                                            </td>
                                            <td style="vertical-align: middle; text-align: left">
                                                <telerik:RadComboBox ID="cmbStatoSAL" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                                    runat="server" AutoPostBack="false" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                    LoadingMessage="Caricamento...">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="0" Text="NON FIRMATO" Selected="true" />
                                                        <telerik:RadComboBoxItem Value="1" Text="FIRMATO CON RISERVA" />
                                                        <telerik:RadComboBoxItem Value="2" Text="FIRMATO" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td colspan="2" style="visibility: hidden">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="vertical-align: middle; text-align: left" width="12%" class="TitoloH1">
                                                            <asp:CheckBox ID="ChkRipartizioni" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                Text="Abilita Ripartizioni" Width="150px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <telerik:RadTabStrip runat="server" ID="RadTabStrip" Width="100%" ShowBaseLine="true" ScrollChildren="true"
                            MultiPageID="RadMultiPage1" OnClientTabSelected="tabSelezionato" SelectedIndex="2">
                            <Tabs>
                                <telerik:RadTab runat="server" PageViewID="RadPageView1" Text="Riepilogo SAL" Value="Riepilogo_SAL">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" PageViewID="RadPageView2" Text="Situazione Progressiva"
                                    Value="Situazione_Progressiva">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" PageViewID="RadPageView3" Text="Elenco Dettaglio Pagamenti"
                                    Value="Elenco Dettaglio Pagamenti" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" PageViewID="RadPageView4" Text="Elenco Ripartizioni"
                                    Value="Elenco_Ripartizioni">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" CssClass="multiPage" Width="100%"
                            ScrollChildren="true" SelectedIndex="2">
                            <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="panelTabsStrip">
                                <asp:Panel runat="server" ID="tab1">
                                    <uc1:Tab_SAL_Riepilogo ID="Tab_SAL_Riepilogo" runat="server" Visible=" true" />
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="RadPageView2" CssClass="panelTabsStrip">
                                <asp:Panel runat="server" ID="tab2">
                                    <uc2:Tab_SAL_RiepilogoProg ID="Tab_SAL_RiepilogoProg" runat="server" Visible=" true" />
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="RadPageView3" CssClass="panelTabsStrip">
                                <asp:Panel runat="server" ID="tab3">
                                    <uc3:Tab_SAL_Dettagli ID="Tab_SAL_Dettagli" runat="server" Visible=" true" />
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="RadPageView4" CssClass="panelTabsStrip">
                                <asp:Panel runat="server" ID="tab4">
                                    <uc4:Tab_SAL_Ripartizioni ID="Tab_SAL_Ripartizioni" runat="server" Visible=" true" />
                                </asp:Panel>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                        <asp:Label ID="Label6" runat="server" Style="z-index: 100; left: -46px; top: -302px"
                            Width="120px" Visible="False">Conto Corrente</asp:Label>
                        <asp:DropDownList ID="cmbContoCorrente" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="8pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 10; left: 142px; border-left: black 1px solid; border-bottom: black 1px solid; top: 224px"
                            Width="160px" Visible="False">
                            <asp:ListItem>12000X01</asp:ListItem>
                            <asp:ListItem>13000X01</asp:ListItem>
                            <asp:ListItem>14000X01</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="USCITA" runat="server" Style="left: 0px; position: absolute; top: 200px; visibility: hidden; z-index: -1;">0</asp:TextBox>
            <asp:TextBox ID="txtModificato" runat="server" BackColor="White" BorderStyle="None"
                ForeColor="White" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;">0</asp:TextBox>
            <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" MaxLength="100" Style="z-index: -1; visibility: hidden; left: 0px; position: absolute; top: 200px"
                Width="48px">0</asp:TextBox>
            <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;"></asp:TextBox>
            <asp:TextBox ID="txttab" runat="server" ForeColor="White" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;">1</asp:TextBox>
            <asp:TextBox ID="SOLO_LETTURA" runat="server" Style="z-index: -1; left: 0px; position: absolute; visibility: hidden; top: 415px"
                Width="24px">0</asp:TextBox>
            <asp:HiddenField ID="tipo" runat="server" Value="0" />
            <asp:HiddenField ID="voce" runat="server" Value="0" />
            <asp:HiddenField ID="txtVisualizza" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtSTATO" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtStatoPagamento" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtID_APPALTO" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtID_FORNITORE" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtID_PAGAMENTI" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtID_STRUTTURA" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtID_LOTTO" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtTipo_LOTTO" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtFL_RIT_LEGGE" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPERC_ONERI_SIC_CAN" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPAGAMENTI_PROGR_APPALTO" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtElimina" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="ANNULLO" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="HFGriglia" runat="server" />
            <asp:HiddenField ID="HFTAB" runat="server" />
            <asp:HiddenField ID="HFAltezzaTab" runat="server" />
            <asp:HiddenField ID="TipoAllegato" runat="server" Value="" />
            <asp:HiddenField ID="HiddenTabSelezionato" runat="server" Value="0" />
            <asp:HiddenField ID="numTab" runat="server" Value="4" />
            <asp:HiddenField ID="HFAltezzaFGriglie" runat="server" />
            <asp:HiddenField ID="HiddenIDPagamento" runat="server" />
            <asp:HiddenField ID="idSAL" runat="server" Value="" />
            <asp:HiddenField ID="HiddenFieldRielabPagam" runat="server" Value="" />
            <asp:HiddenField ID="HiddenFieldMostraRielPag" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="HiddenFieldMostraRielSAL" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="ImportoResiduoDaTrattenere" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="importoDaProporre" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="tipoAnticipo" runat="server" Value="0" />

        </div>
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();

        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);

    </script>
    <script language="javascript" type="text/javascript">
        // document.getElementById('dvvvPre').style.visibility = 'hidden';
        if (document.getElementById('ANNULLO').value == '1') {
            document.getElementById('btnAnnulla').style.visibility = 'hidden';
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("#txtDataDel").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
            $("#txtDataSAL").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
            $("#txtScadenza").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        });
    </script>
</body>
</html>
