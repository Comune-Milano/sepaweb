<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SAL.aspx.vb" Inherits="MANUTENZIONI_SAL" %>

<%@ Register Src="Tab_SAL_Riepilogo.ascx" TagName="Tab_SAL_Riepilogo" TagPrefix="uc1" %>
<%@ Register Src="Tab_SAL_Dettagli.ascx" TagName="Tab_SAL_Dettagli" TagPrefix="uc2" %>
<%@ Register Src="Tab_SAL_RiepilogoProg.ascx" TagName="Tab_SAL_RiepilogoProg" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">

    function ApriEventi() {
        window.open('Report/EventiPagamento.aspx?ID_PAGAMENTO=' + document.getElementById('txtid').value, "WindowPopup", "scrollbars=1, width=800px, height=600px, resizable");
    };

    function AllegaFile() {
        if ((document.getElementById('txtid').value == '') || (document.getElementById('txtid').value == '0')) {
            apriAlert('E\' necessario salvare il SAL prima di allegare documenti!', 300, 150, 'Attenzione', null, null);
        } else {
            CenterPage('../../../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('txtid').value, 'Allegati', 1000, 800);
        };

        //window.open('ElencoAllegati.aspx?T=3&COD=' + document.getElementById('txtIdAppalto').value, 'AllegatiContratto', 'scrollbars=1, width=800px, height=600px, resizable');
        return false;
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


    function $onkeydown() {
        if (event.keyCode == 46) {
            event.keyCode = 0;
        }
    }




</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MODULO GESTIONE SAL MANUTENZIONI</title>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <link href="../../../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
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

        function EliminaManutenzione() {
            var sicuro = confirm('Sei sicuro di voler eliminare questa manutenzione? Tutti i dati andranno persi.');
            if (sicuro == true) {
                document.getElementById('txtElimina').value = '1';
            }
            else {
                document.getElementById('txtElimina').value = '0';
            }
        }

        function AnnullaPagamento() {
            var sicuro = confirm('Sei sicuro di voler annullare questo pagamento?');
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
                if (document.getElementById('SOLO_LETTURA').value == '0') {
                    if (document.getElementById('txtSTATO').value < 2) {
                        chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                        if (chiediConferma == false) {
                            document.getElementById('txtModificato').value = '111';
                            //document.getElementById('USCITA').value='0';
                        }
                    }
                }
            }
        }



        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda pagamento premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda pagamento premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
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


        //        function AttesaDIV() {
        //            document.getElementById('ATTESA').style.visibility = 'visible';
        //        }

        function ControlloStampa() {
            //            var chiediConferma = window.confirm('La data di emissione è stata modificata! Stampare con la data modificata?');
            //            if (chiediConferma == true) {

            //            }

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
        .style4 {
            width: 19px;
        }

        .style5 {
            width: 130px;
        }

        .test {
            font-family: Segoe UI;
            font-size: 8pt;
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
        <div>
            <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
                EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="500" Position="BottomRight"
                OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
            </telerik:RadNotification>
        </div>
        <div class="FontTelerik">
            <table style="width: 100%">
                <tr>
                    <td class="TitoloModulo">SAL
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnINDIETRO" runat="server" Text="Indietro" ToolTip="Indietro"
                                        OnClientClicking="function(sender, args){Blocca_SbloccaMenu(0);document.getElementById('USCITA').value='1';ConfermaEsci();}" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnAnnulla" runat="server" Text="Annulla" ToolTip="Annulla il SAL visualizzato"
                                        OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';  AnnullaPagamento();}" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnStampaSAL" runat="server" Text="Stampa SAL" ToolTip="Stampa il SAL"
                                        OnClientClicking="function(sender, args){ControlloStampa();}" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnRielbSal" runat="server" Text="Rielabora SAL"
                                        Style="z-index: 100; left: 584px; position: static; top: 32px"
                                        ToolTip="Rielabora il SAL" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnStampa" runat="server" Text="Stampa Pagamento" ToolTip="Stampa il Pagamento" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnRielaboraPagamento" runat="server" Text="Rielabora Pagamento"
                                        Style="z-index: 100; left: 584px; position: static; top: 32px"
                                        ToolTip="Rielabora il Pagamento" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="ImgEventi" runat="server" Text="Eventi" ToolTip="Eventi Scheda Pagamenti"
                                        OnClientClicking="function(sender, args){ApriEventi();}">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="ImgAllegaFile" runat="server" Text="Allegati" ToolTip="Allegati" AutoPostBack="False"
                                        OnClientClicking="function(sender, args){AllegaFile();}" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="imgUscita" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="function(sender, args){Blocca_SbloccaMenu(0);document.getElementById('USCITA').value='1';ConfermaEsci();}" />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%" class="FontTelerik">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td class="TitoloH1" style="text-align: left">
                                                            <asp:Label ID="Label2" runat="server" Font-Size="8pt" Font-Bold="false">STATO DI AVANZAMENTO LAVORI N.</asp:Label>
                                                        </td>
                                                        <td style="width: 2%">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblProgAnnoPagamento" runat="server" Font-Bold="true" CssClass="TitoloH1"
                                                                Style="text-align: left"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDataDel" runat="server" CssClass="TitoloH1" Style="text-align: left">a tutto il</asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtDataSAL" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                                DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                                                <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa" Width="70px">
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
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="False" CssClass="TitoloH1" Style="text-align: left">data emissione</asp:Label>
                                            </td>
                                            <td class="style5">
                                                <telerik:RadDatePicker ID="txtDataDel" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                    DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                                    <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa" Width="70px">
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
                                            <td>
                                                <asp:Label ID="lblEsercizioFinanziario" runat="server" CssClass="TitoloH1" Style="text-align: left"
                                                    Font-Bold="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAppalto" runat="server" Font-Bold="false" CssClass="TitoloH1" Style="text-align: left">Num. Repertorio:</asp:Label>
                                                &nbsp;
                                            <asp:HyperLink ID="HLink_Appalto" runat="server" CssClass="TitoloH1" Font-Underline="True"
                                                Style="cursor: pointer; text-align: left">123456789 123456789  </asp:HyperLink>
                                                &nbsp;
                                            <asp:Label ID="Label3" runat="server" Font-Bold="false" CssClass="TitoloH1" Style="text-align: left">del</asp:Label>
                                                &nbsp;
                                            <asp:Label ID="lblDataAppalto" runat="server" CssClass="TitoloH1" Style="text-align: left"
                                                Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%">
                                                <asp:Label ID="lblDescAppalto" runat="server" ForeColor="Black">Descrizione:</asp:Label>
                                            </td>
                                            <td style="width: 30%">
                                                <asp:TextBox ID="txtDescAppalto" runat="server" MaxLength="2000" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFornitore" runat="server" ForeColor="Black">Fornitore:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HLink_Fornitore" runat="server" Font-Underline="True" CssClass="TitoloH1"
                                                    Style="text-align: left; cursor: pointer; font-size: 8pt">123456789 123456789 123456789  </asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblIBAN" runat="server" ForeColor="Black">IBAN:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIBAN" runat="server" EnableTheming="True" MaxLength="1000"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server">Descrizione attestato pagamento</asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtDescAttPagamento" runat="server" Height="60px" MaxLength="2000"
                                                    TextMode="MultiLine" Width="100%" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblManutenzioni" runat="server">Descrizione ODL</asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadTextBox ID="txtDescManutenzioni" runat="server" Height="60px" MaxLength="2000"
                                                    TextMode="MultiLine" Width="100%" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblStato" runat="server" Style="z-index: 100; left: 8px; top: 88px;"
                                                    Width="80px">STATO SAL:</asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbStato" Width="50%" AppendDataBoundItems="true" Filter="Contains" Enabled="false"
                                                    runat="server" AutoPostBack="false" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                    LoadingMessage="Caricamento...">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="0" Text="NON FIRMATO" Selected="true" />
                                                        <telerik:RadComboBoxItem Value="1" Text="FIRMATO CON RISERVA" />
                                                        <telerik:RadComboBoxItem Value="2" Text="FIRMATO" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:HyperLink ID="HLink_ElencoMandati" runat="server" Font-Underline="True" CssClass="TitoloH1"
                                                    Style="text-align: left; cursor: pointer; font-size: 8pt" ToolTip="Visualizza tutti i mandati di pagamento">Mandati di Pagamento</asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatoLiquidazione" runat="server">Stato Liquidazione</asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmb_Liquidazione" Width="50%" AppendDataBoundItems="true"
                                                    Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="0" Text="DA LIQUIDARE" Selected="true" />
                                                        <telerik:RadComboBoxItem Value="1" Text="PARZIALE" />
                                                        <telerik:RadComboBoxItem Value="3" Text="COMPLETO" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <telerik:RadTabStrip runat="server" ID="RadTabStrip" Orientation="HorizontalTop"
                            Width="100%" MultiPageID="RadMultiPage1" ShowBaseLine="true" ScrollChildren="true"
                            OnClientTabSelected="tabSelezionato">
                            <Tabs>
                                <telerik:RadTab runat="server" PageViewID="RadPageView1" Text="Riepilogo SAL" Value="Riepilogo_SAL"
                                    Selected="true">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" PageViewID="RadPageView2" Text="Pagamenti ODL" Value="Pagamenti_ODL">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" PageViewID="RadPageView3" Text="Situazione Progressiva"
                                    Value="Situazione_Progressiva">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" CssClass="multiPage" Width="100%"
                            ScrollChildren="true">
                            <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="panelTabsStrip" Selected="true">
                                <asp:Panel runat="server" ID="tab1">
                                    <uc1:Tab_SAL_Riepilogo ID="Tab_SAL_Riepilogo" runat="server" Visible=" true" />
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="RadPageView2" CssClass="panelTabsStrip">
                                <asp:Panel runat="server" ID="tab2">
                                    <uc2:Tab_SAL_Dettagli ID="Tab_SAL_Dettagli" runat="server" Visible=" true" />
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="RadPageView3" CssClass="panelTabsStrip">
                                <asp:Panel runat="server" ID="tab3">
                                    <uc3:Tab_SAL_RiepilogoProg ID="Tab_SAL_RiepilogoProg" runat="server" Visible=" true" />
                                </asp:Panel>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:TextBox ID="USCITA" runat="server" Style="left: 0px; position: absolute; top: 200px; visibility: hidden; z-index: -1;">0</asp:TextBox>
            <asp:TextBox ID="txtModificato" runat="server" BackColor="White" BorderStyle="None"
                ForeColor="White" Style="left: 0px; visibility: hidden; position: absolute; top: 200px; z-index: -1;">0</asp:TextBox>
            <asp:TextBox ID="txtElimina" runat="server" BackColor="White" BorderStyle="None"
                ForeColor="White" Style="z-index: -1; visibility: hidden; left: 0px; position: absolute; top: 200px"
                ViewStateMode="Disabled">0</asp:TextBox>
            <asp:TextBox ID="txttab" runat="server" ForeColor="White" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;">1</asp:TextBox>
            <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" MaxLength="100" Style="z-index: -1; left: 0px; position: absolute; visibility: hidden; top: 200px"
                Width="48px">0</asp:TextBox>
            <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute; visibility: hidden; top: 200px; z-index: -1;"></asp:TextBox>
            <asp:TextBox ID="SOLO_LETTURA" runat="server" Style="z-index: -1; left: 0px; position: absolute; visibility: hidden; top: 415px"
                Width="24px">0</asp:TextBox>
            <asp:HiddenField ID="txtSTATO" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtID_STRUTTURA" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPAGAMENTI_PROGR_APPALTO" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtPAGAMENTI_PROGR" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="HFGriglia" runat="server" />
            <asp:HiddenField ID="HFTAB" runat="server" />
            <asp:HiddenField ID="HFAltezzaTab" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="ANNULLO" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="Trasmesso" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="TipoAllegato" runat="server" Value="" />
            <asp:HiddenField ID="idSAL" runat="server" Value="" />
            <asp:HiddenField ID="txtIdAppalto" runat="server" Value="" />
            <asp:HiddenField ID="HiddenFieldRielabPagam" runat="server" Value="" />
        </div>
        <telerik:RadWindow ID="RadWindowStampa" runat="server" CenterIfModal="true" Modal="true"
            Title="Stampa Pagamento" Width="800px" Height="530px" VisibleStatusbar="false"
            Behaviors="Pin, Maximize, Move, Resize" RestrictionZoneID="RestrictionZoneID">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" Width="800px" Height="530px">
                    <table border="0" cellpadding="4" cellspacing="4" width="90%">
                        <tr>
                            <td class="TitoloModulo" colspan="2">
                                <asp:Label ID="ADP" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadButton ID="ImgConferma" runat="server" Text="Stampa" ToolTip="Stampa" />
                                <telerik:RadButton ID="ImgAnnulla" runat="server" Text="Annulla Stampa" ToolTip="Annulla Stampa" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Blue"
                                    Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="Label6" Text="Data di emissione*" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td style="width: 80%">
                                <asp:TextBox ID="DataEmissione" runat="server" Width="70px" MaxLength="10" Font-Names="Arial"
                                    Font-Size="9pt"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="DataEmissione"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="arial" Font-Size="12pt" ForeColor="#CC0000"
                                    ToolTip="Modificare la data" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" Text="Modalità di pagamento" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtModalitaPagamento" runat="server" Width="300px" Font-Names="Arial"
                                    Font-Size="9pt" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" Text="Condizione di pagamento" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtCondizionePagamento" runat="server" Width="300px" Font-Names="Arial"
                                    Font-Size="9pt" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" Text="Data di scadenza" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataScadenza" runat="server" Width="70px" MaxLength="10" Font-Names="Arial"
                                    Font-Size="9pt"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataScadenza"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="arial" Font-Size="12pt" ForeColor="#CC0000"
                                    ToolTip="Modificare la data" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" Text="Descrizione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    Font-Bold="False" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescrizioneBreve" runat="server" Width="500px" MaxLength="1000"
                                    Font-Names="Arial" Font-Size="9pt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 200px;">
                                <asp:HiddenField runat="server" ID="idCondizione" Value="NULL" />
                                <asp:HiddenField runat="server" ID="idModalita" Value="NULL" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </telerik:RadWindow>
        <asp:HiddenField ID="tipo" runat="server" Value="0" />
        <asp:HiddenField ID="voce" runat="server" Value="0" />
        <asp:HiddenField ID="txtid" runat="server" Value="0" />
        <asp:HiddenField ID="bloccato" runat="server" Value="0" />
        <asp:HiddenField ID="cambiata" runat="server" Value="1" />
        <asp:HiddenField ID="HiddenTabSelezionato" runat="server" Value="0" />
        <asp:HiddenField ID="numTab" runat="server" Value="3" />
        <asp:HiddenField ID="HFAltezzaFGriglie" runat="server" Value="1" />
        <asp:HiddenField ID="HiddenField3" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="ImportoResiduoDaTrattenere" runat="server" Value="0" />
        <asp:HiddenField ID="tipoAnticipo" runat="server" Value="0" />
        <asp:HiddenField ID="importoDaProporre" runat="server" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        //document.getElementById('ATTESA').style.visibility = 'hidden';
    </script>
    <script type="text/javascript">

        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
