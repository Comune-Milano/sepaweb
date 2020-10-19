<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Segnalazioni.aspx.vb" Inherits="Segnalazioni" %>

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

    function getDropDownListvalue() {
        var e = document.getElementById("cmbNoteChiusura");
        var strUser = e.options[e.selectedIndex].value;
        document.getElementById("txtDescNoteChiusura").value = strUser

    }
       

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>MODULO GESTIONE SEGNALAZIONI</title>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        //window.onbeforeunload = confirmExit; 



        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        }


        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda segnalazioni premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda segnalazioni premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
            }
        }


        function SalvaVerifica() {
            var sicuro = confirm('Prendere in carico la segnalazione?');
            if (sicuro == true) {
                document.getElementById('txtannullo').value = '1';
            }
            else {
                document.getElementById('txtannullo').value = '0';
            }
        }

        function SalvaOrdine() {
            var sicuro = confirm('Visualizzare la manutenzione per emettere l\'ordine?');
            if (sicuro == true) {
                document.getElementById('txtannullo').value = '1';
            }
            else {
                document.getElementById('txtannullo').value = '0';
            }
        }

        function VisManutenzione() {
            var sicuro = confirm('Visualizzare la manutenzione in corso?');
            if (sicuro == true) {
                document.getElementById('txtannullo').value = '1';
            }
            else {
                document.getElementById('txtannullo').value = '0';
            }
        }

        function VisManutenzione2() {
            document.getElementById('txtannullo').value = '1';
            var oWnd = $find('RadWindow1');
            oWnd.setUrl('ListaManutenzioni.aspx?ID=' + document.getElementById('idSegnalazione').value);
            oWnd.show();
            //window.showModalDialog('ListaManutenzioni.aspx?ID=' + document.getElementById('idSegnalazione').value, window, 'status:no;dialogWidth:400px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');

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

        function VerificaCondomino() {
            var win = null;
            LeftPosition = (screen.width) ? (screen.width - 960) / 2 : 0;
            TopPosition = (screen.height) ? (screen.height - 550) / 2 : 0;
            LeftPosition = LeftPosition - 20;
            TopPosition = TopPosition - 20;
            window.open('../../../CALL_CENTER/VerificaCondomino.aspx?C=' + document.getElementById('txtRichiedente').value.replace(/\'/, '%27') + '&N=' + document.getElementById('txtNome').value.replace(/\'/, '%27') + '&T=' + document.getElementById('tipo').value + '&ID=' + document.getElementById('identificativo').value, 'Verifica', 'height=400,top=' + TopPosition + ',left=' + LeftPosition + ',width=400,scrollbars=no');
        }

        function ApriSopralluogo() {
            var Page = '../../../CALL_CENTER/Sopralluogo.aspx?IdSegn=' + document.getElementById('idSegnalazione').value + '';
            var oWnd = $find('RadWindowSopralluogo');
            oWnd.setUrl(Page);
            oWnd.show();
            //window.showModalDialog('../../../CALL_CENTER/Sopralluogo.aspx?IdSegn=' + document.getElementById('idSegnalazione').value + '', 'window', 'status:no;dialogWidth:700px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');
        }
        function ConfermaRespinta() {
            var chiediConferma;
            chiediConferma = window.confirm("Attenzione...La segnalazione verrà respinta!\nProcedere con l\'operazione?");
            if (chiediConferma == false) {
                document.getElementById('confRespinta').value = '0';
                //document.getElementById('USCITA').value='0';
            }
            else {
                document.getElementById('confRespinta').value = '1';


            }


        }
    </script>
    <style type="text/css">
  
        .style4
        {
            width: 12%;
            height: 24px;
        }
        .style5
        {
            width: 35%;
            height: 24px;
        }
        .style6
        {
            width: 10%;
        }
        .style7
        {
            width: 10%;
            height: 24px;
        }
        .style8
        {
            width: 12%;
        }
    </style>
</head>
<body class="sfondo">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelComposizione">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelChiudiSegn" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PanelEmOrdine">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelEmOrdine" LoadingPanelID="RadAjaxLoadingPanel2"
                        UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowEmOrdine" LoadingPanelID="RadAjaxLoadingPanel2"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
    </telerik:RadAjaxLoadingPanel>
         <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="true" RestrictionZoneID="divGenerale"
                    VisibleStatusbar="False" AutoSize="false" Height="500" Width="800" Behavior=" Move, Resize, Maximize,Close" Skin="Web20">
                </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowSopralluogo" runat="server" Behaviors="Pin, Maximize, Move, Resize, close"
        VisibleStatusbar="false" Width="700px" Height="300px" CenterIfModal="true">
        <ContentTemplate>
        <div class="FontTelerik">
        
         <table style="width: 100%;" >
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="TitoloModulo ">
                        Dettagli del sopralluogo
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnSalvaSopralluogo" runat="server" Text="Salva" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnStampaSopralluogo" runat="server" Text="Stampa" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnChiudiSopralluogo" runat="server" Text="Esci" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNomTec" runat="server" Text="Nome Tecnico Soprall."></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTecnico" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblNomTec1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                        Text="Pericolo?"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbPericolo" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblNomTec0" runat="server" Text="Rapporto di Sopralluogo"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblNomTec2" runat="server" Text="Data Sopralluogo"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDataS" runat="server"></asp:TextBox>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtRapporto" runat="server" MaxLength="2000" TextMode="MultiLine"
                            Width="600px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            </div>
           
        </ContentTemplate>
        
    </telerik:RadWindow>
    <div>
        <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
            EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="BottomRight"
            OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
        </telerik:RadNotification>
    </div>
    <table style="width: 100%" class="FontTelerik">
        <tr>
            <td class="TitoloModulo">
                Segnalazione
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnINDIETRO" runat="server" Text="Indietro" ToolTip="Indietro"
                                CausesValidation="False" OnClientClicking="function(sender, args){Blocca_SbloccaMenu('0');document.getElementById('USCITA').value='1';ConfermaEsci();}">
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnSalvaSegnalazione" runat="server" Text="Salva" ToolTip="Salva"
                                CausesValidation="False">
                            </telerik:RadButton>
                            <%--<asp:ImageButton ID="btnInoltra" runat="server" ImageUrl="~/CALL_CENTER/Immagini/ImgInoltra.png"
                                Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="3" ToolTip="Inoltra ad altra struttura di competenza"
                                OnClientClick="ConfermaRespinta();" />--%>
                            <%--OnClientClick="document.getElementById('divInoltra').style.visibility='visible';return false;"--%>
                        </td>
                        <td>
                            <telerik:RadButton ID="imgInCarSoprall" runat="server" Text="Apri Sopralluogo" ToolTip="Sopralluogo"
                                CausesValidation="False" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowSopralluogo');}"
                                AutoPostBack="false">
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnSalva" runat="server" Text="In Carico" ToolTip="Salva la Segnalazioni in Verifica"
                                CausesValidation="False" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1'; SalvaVerifica();}">
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnManutenzione" runat="server" Text="Emetti Ordine" ToolTip="Visualizza la manutenzione per emettere l'ordine"
                                CausesValidation="False" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowEmOrdine');document.getElementById('USCITA').value='1';}">
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnManutenzioneVis" runat="server" Text="Vis. Manutenzione" AutoPostBack="false"
                                ToolTip="Mostra elenco Ordini" CausesValidation="False" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1'; VisManutenzione2();}">
                            </telerik:RadButton>
                            <asp:Panel ID="panel1" runat="server" Style="display:none">
                                 <telerik:RadButton ID="btnManutenzioneVis1" runat="server" Text="Vis. Manutenzione" 
                                ToolTip="Mostra elenco Ordini" CausesValidation="False" >
                            </telerik:RadButton>
                            </asp:Panel>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnChiudiSegnalazione" runat="server" Text="Chiudi Segnalazione"
                                AutoPostBack="false" ToolTip="Chiude la segnalazione" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';openWindow(sender, args, 'RadWindowChiudiSegn');}"
                                CausesValidation="False">
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="imgUscita" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="function(sender, args){Blocca_SbloccaMenu('0');document.getElementById('USCITA').value='1';ConfermaEsci();}"
                                CausesValidation="False">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl2" runat="server">N°</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNum" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl1" runat="server">STATO</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSTATO" runat="server" ForeColor="Red" Width="250px" Font-Italic="True">ORDINE EMESSO</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInoltro" runat="server" ForeColor="Black" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="Label19" runat="server" ForeColor="Black" Width="60px">Data</asp:Label>
                        </td>
                        <td class="style5">
                            <asp:Label ID="lblDataIns" runat="server" ForeColor="Black" Width="100%"></asp:Label>
                        </td>
                        <td class="style4">
                            <asp:Label ID="Label39" runat="server" Text="Cat. segnalazione" Width="65%"></asp:Label>
                        </td>
                        <td class="style5">
                            <telerik:RadComboBox ID="cmbTipoRichiesta" Width="100%" AppendDataBoundItems="true"
                                Filter="Contains" Visible="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="SEGNALAZIONE GUASTI" />
                                    <telerik:RadComboBoxItem Value="1" Text="RECLAMO" />
                                    <telerik:RadComboBoxItem Value="2" Text="PROPOSTE" />
                                    <telerik:RadComboBoxItem Value="3" Text="VARIE" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadComboBox ID="cmbTipoSegn" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            <asp:Label ID="Label5" runat="server">Urgenza/pericolo</asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbUrgenza" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                LoadingMessage="Caricamento...">
                                <Items>
                                    <telerik:RadComboBoxItem Value="Blu" Text="Blu" />
                                    <telerik:RadComboBoxItem Value="Bianco" Text="Bianco" />
                                    <telerik:RadComboBoxItem Value="Verde" Text="Verde" />
                                    <telerik:RadComboBoxItem Value="Giallo" Text="Giallo" />
                                    <telerik:RadComboBoxItem Value="Rosso" Text="Rosso" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td class="style8">
                            <asp:Label ID="Label40" runat="server" Text="Categoria 1" Width="51%" 
                                Height="17px"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbTipoSegnalazioneLivello1" Width="100%" AppendDataBoundItems="true"
                                Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            <asp:Label ID="Label2" runat="server">Ubicazione</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTitolo" runat="server"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:Label ID="Label41" runat="server"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbTipoSegnalazioneLivello2" Width="100%" AppendDataBoundItems="true"
                                Filter="Contains" Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            <asp:Label ID="Label29" runat="server">INTESTATARIO:</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblIntestatario" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:Label ID="Label42" runat="server" Text="Categoria 3" Width="50%"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbTipoSegnalazioneLivello3" Width="100%" AppendDataBoundItems="true"
                                Filter="Contains" Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style8">
                            <asp:Label ID="Label43" runat="server" Text="Categoria 4"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbTipoSegnalazioneLivello4" Width="100%" AppendDataBoundItems="true"
                                Filter="Contains" Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label30" runat="server" Font-Bold="True">ESTREMI DI COLUI CHE EFFETTUA LA SEGNALAZIONE</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 760px">
                    <tr>
                        <td>
                            <asp:Label ID="Label20" runat="server">Cognome/Rag.S.</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRichiedente" runat="server" MaxLength="100" TabIndex="15" Width="287px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 30px">
                        </td>
                        <td>
                            <asp:Label ID="Label27" runat="server">Nome</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNome" runat="server" MaxLength="25" TabIndex="15" Width="287px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <img id="ImgVerifica" alt="Verifica Sel il richiedente è un condomino" height="16"
                                onclick="VerificaCondomino();" src="Immagini/search-icon.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 400px">
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server">Telefono 1</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTel1" runat="server" MaxLength="100" TabIndex="15" Width="150px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 30px">
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server">Telefono 2</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTel2" runat="server" MaxLength="25" TabIndex="15" Width="150px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label23" runat="server">e-mail</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtemail" runat="server" MaxLength="25" TabIndex="15" Width="200px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label24" runat="server" Font-Bold="True">DESCRIZIONE SEGNALAZIONE</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtDescrizione" runat="server" Height="65px" MaxLength="4000" TabIndex="15"
                    TextMode="MultiLine" Width="98%" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label38" runat="server">AGGIUNTA NOTE (uso interno)</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtNote" runat="server" Height="33px" MaxLength="4000" TabIndex="15"
                    TextMode="MultiLine" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label25" runat="server">STORICO NOTE (uso interno)</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="NOTE" style="border: 1px solid #000000; width: 98%; height: 65px; background-color: #E4E4E4;
                    overflow: scroll; visibility: visible; color: black;">
                    <asp:Label ID="TabellaNote" runat="server" Text=""></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="USCITA" runat="server" Style="left: 47px; visibility: hidden; z-index: -1;"
        TabIndex="-1" Height="1px" Width="1px">0</asp:TextBox>
    <asp:TextBox ID="txtModificato" runat="server" BackColor="White" BorderStyle="None"
        ForeColor="White" Style="visibility: hidden;" Height="1px" Width="1px">0</asp:TextBox>
    <asp:TextBox ID="txtElimina" runat="server" BackColor="White" BorderStyle="None"
        ForeColor="White" Style="visibility: hidden;" Height="1px" Width="1px">0</asp:TextBox>
    <asp:TextBox ID="txtIdSegnalazioni" runat="server" Style="visibility: hidden;" TabIndex="-1"
        Visible="False" Height="1px" Width="1px"></asp:TextBox>
    <asp:TextBox ID="txttab" runat="server" ForeColor="White" Style="visibility: hidden;"
        TabIndex="-1" Width="1px" Height="1px">1</asp:TextBox>
    <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
        BorderStyle="None" MaxLength="100" Style="visibility: hidden;" TabIndex="-1"
        Width="1px" Height="1px">0</asp:TextBox>
    <asp:TextBox ID="txtConnessione" runat="server" Style="visibility: hidden;" TabIndex="-1"
        Height="1px" Width="1px"></asp:TextBox>
    <asp:TextBox ID="SOLO_LETTURA" runat="server" Style="visibility: hidden;" TabIndex="-1"
        Width="1px" Height="1px">0</asp:TextBox>
    <asp:HiddenField ID="txtIntestatario" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtIntestatario1" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtSTATO" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="tipo" runat="server" />
    <asp:HiddenField ID="confRespinta" runat="server" />
    <asp:HiddenField ID="urgenzaOld" runat="server" />
    <asp:HiddenField ID="tipoOld" runat="server" />
    <asp:HiddenField ID="tipoOld1" runat="server" />
    <asp:HiddenField ID="tipoOld2" runat="server" />
    <asp:HiddenField ID="tipoOld3" runat="server" />
    <asp:HiddenField ID="tipoOld4" runat="server" />
    <div id="divInoltra" style="position: absolute; top: 6px; left: 5px; height: 498px;
        width: 795px; z-index: 500; background-image: url('../../../ImmDiv/DivMGrande2.png');
        visibility: hidden;">
        <table style="position: absolute; top: 194px; left: 62px; width: 693px;">
            <tr>
                <td>
                    <strong>Struttura a cui inoltrare la segnalazione:</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbFiliali" runat="server" Width="90%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSalvaInoltra" runat="server" ImageUrl="~/CALL_CENTER/Immagini/ImgInoltra.png"
                                    Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="3" ToolTip="Inoltra ad altra struttura di competenza" />
                            </td>
                            <td>
                                <asp:Image ID="imgEsci" runat="server" Style="cursor: pointer" ImageUrl="~/NuoveImm/Img_Esci.png"
                                    onclick="document.getElementById('divInoltra').style.visibility='hidden';" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="identificativo" runat="server" />
    <asp:HiddenField ID="txtannullo" runat="server" />
    <asp:HiddenField ID="txtEF" runat="server" Value="0" />
    <asp:HiddenField ID="txtSTATO_PF" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtAppare2" runat="server" Value="0" />
    <asp:HiddenField ID="txtTIPO" runat="server" />
    <telerik:RadWindow ID="RadWindowChiudiSegn" runat="server" CenterIfModal="true" Modal="true"
        Title="Chiudi Segnalazione" Width="800px" Height="500px" VisibleStatusbar="false"
        Behaviors="Pin, Maximize, Move, Resize">
        <ContentTemplate>
            <asp:Panel ID="PanelChiudiSegn" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td width="60%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="TitoloModulo">
                                        NOTE DI CHIUSURA SEGNALAZIONE
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <telerik:RadButton ID="btn_Inserisci1" runat="server" Text="Salva" ToolTip="Salva le modifiche apportate"
                                                        CausesValidation="False" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}">
                                                    </telerik:RadButton>
                                                </td>
                                                <td style="text-align: right">
                                                    <telerik:RadButton ID="btn_Chiudi1" runat="server" Text="Esci" ToolTip="Esci senza inserire o modificare"
                                                        CausesValidation="False" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}">
                                                    </telerik:RadButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cmbNoteChiusura" runat="server" Width="80%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <strong>Note libere</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDescNoteChiusura" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="9pt" MaxLength="500" TabIndex="4" Width="90%" Height="71px"
                                            Rows="3" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <strong>Data e Ora Chiusura Intervento</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label36" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        Width="30px" Height="16px">DATA:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDataCInt" runat="server" MaxLength="10" Width="70px" Font-Names="Arial"
                                                        Font-Size="8pt"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataCInt"
                                                        ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" ToolTip="Inserire una data valida"
                                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                        SetFocusOnError="True" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        Width="25px" Height="16px">ORA:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOraCInt" runat="server" MaxLength="5" Width="40px" Font-Names="Arial"
                                                        Font-Size="8pt" ToolTip="Ora segnalazione in formato HH:MM"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtOraCInt"
                                                        ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ToolTip="Inserire orario formato HH:MM"
                                                        ValidationExpression="([01]?[0-9]|2[0-3])(.|:)[0-5][0-9]"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <asp:HiddenField ID="txtAppare1" runat="server" Value="0" />
    <telerik:RadWindow ID="RadWindowEmOrdine" runat="server" CenterIfModal="true" Modal="true"
        Title="Emetti Ordine" Width="800px" Height="500px" VisibleStatusbar="false" Behaviors="Pin, Maximize, Move, Resize">
        <ContentTemplate>
            <asp:Panel ID="PanelEmOrdine" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" class="TitoloModulo">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                ForeColor="Blue">Selezionare tutte le voci</asp:Label>
                        </td>
                        <td style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnProcediEF" runat="server" Text="Procedi" ToolTip="Procede con la visualizzazione della manutenzione per emettere l'ordine"
                                            CausesValidation="False" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1'; SalvaOrdine();}">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnChiudiEF" runat="server" Text="Annulla" ToolTip="Esci senza inserire o modificare"
                                            CausesValidation="False" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 48px; top: 32px" Width="110px">Esercizio Finanziario</asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbEsercizio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTipoServizio" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
                                Width="110px">Servizio</asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbServizio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTipoServizioDett" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 32px"
                                Width="110px">Voce DGR</asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbServizioVoce" Width="100%" AppendDataBoundItems="true"
                                Filter="Contains" Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblAppalto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 48px; top: 64px" Width="110px">Num. Repertorio</asp:Label>
                        </td>
                        <td style="height: 21px">
                            <telerik:RadComboBox ID="cmbAppalto" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                Enabled="false" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <asp:HiddenField ID="idSegnalazione" runat="server" Value="0" />
    <asp:HiddenField ID="idSegnalazionePadre" runat="server" Value="0" />
    </form>
    <script type="text/javascript">

        if (document.getElementById('txtAppare1').value != '1') {
            //document.getElementById('DIV_C').style.visibility = 'hidden';
        }

        if (document.getElementById('txtAppare2').value != '1') {
            //document.getElementById('DIV_EF').style.visibility = 'hidden';
        }

        if (document.getElementById('idSegnalazionePadre').value != '0') {
            if (document.getElementById('btnManutenzione')) {
                document.getElementById('btnManutenzione').style.enabled = 'false';
            }
        } else {
            if (document.getElementById('btnManutenzione')) {
                document.getElementById('btnManutenzione').style.enabled = 'true';
            }
        };

    </script>
</body>
</html>
