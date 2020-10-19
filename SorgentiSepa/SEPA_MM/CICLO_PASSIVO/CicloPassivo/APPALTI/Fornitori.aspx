<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Fornitori.aspx.vb" Inherits="Fornitori" %>

<%@ Register Src="Tab_Indirizzi.ascx" TagName="Tab_Indirizzi" TagPrefix="uc1" %>
<%@ Register Src="Tab_IBAN.ascx" TagName="Tab_IBAN" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../GestioneAllegati/Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../GestioneAllegati/Scripts/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../../../GestioneAllegati/Scripts/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
    <title>Fornitori / Scheda anagrafica</title>
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
        var Uscita;
        Uscita = 0;

        function disabilitaMinore(e) {
            document.getElementById("modificheEffettuate").value = "1";
            var key;
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if ((key == 226) || (key == 13)) {
                return false;
            } else {
                return true;
            }
        }

        function confermaEsci() {
            if (document.getElementById('modificheEffettuate').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Le modifiche non salvate verranno perse. Uscire lo stesso?");
                if (chiediConferma == true) {
                    document.getElementById('confermaUscita').value = '1';
                }
            } else {
                document.getElementById('confermaUscita').value = '1';
            }
        }

        function IndietroConferma() {
            if ((document.getElementById('daInserimento').value != '3') && (document.getElementById('modalitaSOLALETTURA').value != '1')) {
                //NON SOLA LETTURA
                if (document.getElementById('modificheEffettuate').value == '1') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Le modifiche non salvate verranno perse. Proseguire?");
                    if (chiediConferma == true) {
                        document.getElementById('confermaIndietro').value = '1';
                    }
                } else {
                    document.getElementById('confermaIndietro').value = '1';
                }
            } else {
                if (document.getElementById('modalitaSOLALETTURA').value != '3') {

                    document.getElementById('confermaIndietro').value = '1';
                }

            }
        }

        function EliminaFornitore() {
            var chiediConferma
            chiediConferma = window.confirm("Sei sicuro di voler eliminare questo fornitore? Tutti i dati andranno persi.");
            if (chiediConferma == true) {
                document.getElementById('confermaEliminazioneFornitore').value = '1';
            } else {
                document.getElementById('confermaEliminazioneFornitore').value = '0';
            }
        }

        function ModFornitore() {
            if (document.getElementById('Tab_Indirizzi_INDIRIZZOselezionato').value != -1) {
                var radwindow = $find('Tab_Indirizzi_RadWindow1');
                radwindow.setUrl('AggIndirizzi.aspx?M=1&IDINDS=' + document.getElementById('Tab_Indirizzi_INDIRIZZOselezionato').value + '&IDFORN=' + document.getElementById('Tab_Indirizzi_idFORNITORE1').value + '&IDCONN=' + document.getElementById('Tab_Indirizzi_idCONNESSIONE1').value);
                radwindow.show();
            } else {
                //alert(1);
                radalert('Non hai selezionato alcuna riga!', '300', '150');
            };

        };
        
    </script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <style type="text/css">
        .style6
        {
            width: 395px;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" DecoratedControls="Buttons" />
    <div>
        <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
            EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="BottomRight"
            OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
        </telerik:RadNotification>
    </div>
    <table width="95%">
        <tr>
            <td style="width: 100%" class="TitoloModulo">
                Fornitori - Scheda anagrafica
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnIndietro" runat="server" Text="Indietro" OnClientClick="IndietroConferma();"
                                Style="cursor: pointer" ToolTip="Torna indietro" />
                        </td>
                        <td>
                            <asp:Button ID="btnSalva" runat="server" Text="Salva" Style="cursor: pointer" ToolTip="Salva le modifiche effettuate" />
                        </td>
                        <td>
                            <asp:Button ID="btnElimina" runat="server" Text="Elimina" OnClientClick="EliminaFornitore();"
                                Style="cursor: pointer" Enabled="false" ToolTip="Elimina il fornitore selezionato" />
                        </td>
                        <td>
                            <asp:Button ID="btnEsci" runat="server" Text="Esci" OnClientClick="confermaEsci();"
                                Style="cursor: pointer" ToolTip="Esci" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%;">
                <table width="100%">
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Codice fornitore*"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:TextBox ID="txtCodice" runat="server" MaxLength="20" onkeydown="return disabilitaMinore(event)"
                                Width="100px" Font-Names="Arial" Font-Size="8pt" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="errorecodicefornitore" runat="server" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Text="Codice fornitore errato"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Denominazione*"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:TextBox ID="txtDenominazione" runat="server" MaxLength="100" onkeydown="return disabilitaMinore(event)"
                                Width="366px" Font-Names="Arial" Font-Size="8pt" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="erroredenominazione" runat="server" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Text="Denominazione errata"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Codice fiscale"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:TextBox ID="txtCodiceFiscale" runat="server" MaxLength="16" onkeydown="return disabilitaMinore(event)"
                                Width="170px" Font-Names="Arial" Font-Size="8pt" Enabled="False"></asp:TextBox>
                            <%--                            <a href="../../../cf/codice.htm" target="_blank">
                                <img alt="Calcolo Codice Fiscale" src="../../../NuoveImm/codice_fiscale.gif" style="border-style: none;
                                    border-color: inherit; border-width: 0; vertical-align: middle; cursor: pointer;
                                    visibility: <%=codice_fiscale%>;" />
                            </a>
                            --%>
                        </td>
                        <td>
                            <asp:Label ID="errorecodicefiscale" runat="server" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Text="Codice fiscale errato"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Partita IVA"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:TextBox ID="txtPiva" runat="server" MaxLength="11" onkeydown="return disabilitaMinore(event)"
                                Width="170px" Font-Names="Arial" Font-Size="8pt" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="errorepartitaiva" runat="server" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Text="Partita IVA errata"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aliquota rit. acconto"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:TextBox ID="txtRitAcconto" runat="server" Columns="10" onkeydown="return disabilitaMinore(event)"
                                Font-Names="Arial" Font-Size="8pt" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="errorealiquota" runat="server" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Text="Aliquota deve essere un numero compreso tra 0 e 100"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Modalità pagamento"></asp:Label>
                        </td>
                        <td class="style6">
                            <telerik:RadComboBox ID="cmbModalitaPagamento" runat="server" AppendDataBoundItems="true" Enabled="false"
                                AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Condizione pagamento"></asp:Label>
                        </td>
                        <td class="style6">
                            <telerik:RadComboBox ID="cmbCondizionePagamento" runat="server" AppendDataBoundItems="true" Enabled="false"
                                AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo collaborazione"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:TextBox runat="server" ID="TipoCollaborazione" Font-Names="Arial" Font-Size="8pt"
                                MaxLength="1" Width="20px" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ritenuta"></asp:Label>
                        </td>
                        <td class="style6">
                            <telerik:RadComboBox ID="cmbRitenuta" runat="server" AppendDataBoundItems="true" Enabled="false"
                                AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            &nbsp;
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
            <telerik:RadTab runat="server" PageViewID="RadPageView1" Text="Indirizzi" Value="Indirizzi"
                Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="RadPageView2" Text="IBAN" Value="IBAN">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" CssClass="multiPage" Width="100%"
        ScrollChildren="true">
        <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="panelTabsStrip" Selected="true">
            <asp:Panel runat="server" ID="tab1">
                <uc1:Tab_Indirizzi ID="Tab_Indirizzi" runat="server" />
            </asp:Panel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="RadPageView2" CssClass="panelTabsStrip">
            <asp:Panel runat="server" ID="tab2">
                <uc2:Tab_IBAN ID="Tab_IBAN" runat="server" />
            </asp:Panel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <br />
    <asp:HiddenField ID="modalitaSOLALETTURA" runat="server" Value="0" />
    <asp:HiddenField ID="IDFornitore" runat="server" Value="-1" />
    <asp:HiddenField ID="IDConnessione" runat="server" Value="-1" />
    <asp:HiddenField ID="confermaUscita" runat="server" Value="0" />
    <asp:HiddenField ID="modificheEffettuate" runat="server" Value="0" />
    <asp:HiddenField ID="txttab" runat="server" Value="1" />
    <asp:HiddenField ID="confermaEliminazioneFornitore" runat="server" Value="0" />
    <asp:HiddenField ID="daModifica" runat="server" Value="0" />
    <asp:HiddenField ID="daInserimento" runat="server" Value="0" />
    <asp:HiddenField ID="indietro" runat="server" Value="0" />
    <asp:HiddenField ID="confermaIndietro" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenTabSelezionato" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    <asp:HiddenField ID="HFTAB" runat="server" />
    <asp:HiddenField ID="HFAltezzaTab" runat="server" />
    <asp:HiddenField ID="numTab" runat="server" Value = "2" />
      <asp:HiddenField ID="HFAltezzaFGriglie" runat="server" Value="1" />
    <script type="text/javascript">
        window.focus();
        self.focus();
    </script>
    </form>
    <script type="text/javascript">
        //        document.getElementById('dvvvPre').style.visibility = 'hidden';
        //        this.form1.target = '<%=modale %>';

        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
