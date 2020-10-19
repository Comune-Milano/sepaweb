<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="Segnalazione.aspx.vb" Inherits="SICUREZZA_Segnalazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function EliminaAppuntamento(id) {
            if (document.getElementById('daElimina') != null) {
                document.getElementById('daElimina').value = '1';
            };
            var chiediConferma = window.confirm("L\'appuntamento verrà eliminato definitivamente.\nVuoi continuare?");
            if (chiediConferma == true) {
                if (document.getElementById('confermaGenerica') != null) {
                    document.getElementById('confermaGenerica').value = '1';
                };
                if (document.getElementById('idSelected') != null) {
                    document.getElementById('idSelected').value = id;
                };
                if (document.getElementById('CPFooter_btnElimina') != null) {
                    document.getElementById('CPFooter_btnElimina').click();
                };
            } else {
                if (document.getElementById('daElimina') != null) {
                    document.getElementById('daElimina').value = '0';
                };
                if (document.getElementById('confermaGenerica') != null) {
                    document.getElementById('confermaGenerica').value = '0';
                };
                if (document.getElementById('idSelected') != null) {
                    document.getElementById('idSelected').value = '-1';
                };
            };
        };
        //        function ConfermaAnnulla() {
        //            var chiediConferma = window.confirm("Sei sicuro di voler annullare questa segnalazione?");
        //            if (chiediConferma == true) {
        //                document.getElementById('confermaAnnullaSegnalazione').value = '1';
        //            } else {
        //                document.getElementById('confermaAnnullaSegnalazione').value = '0';
        //            };
        //        };

        function ConfermaAnnulla(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    this.click();
                }
            });
            apriConfirm("Sei sicuro di voler procedere con l\'annullo della segnalazione?", callBackFunction, 300, 150, "Info", null);
            args.set_cancel(true);
        }


        function ConfermaChiusura(sender, args) {
            //            var chiediConferma = window.confirm("Sei sicuro di voler chiudere questa segnalazione?");
            //            if (chiediConferma == true) {
            //                document.getElementById('confermaChiusura').value = '1';
            //            } else {
            //                document.getElementById('confermaChiusura').value = '0';
            //            }
            //            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
            //                if (shouldSubmit) {
            //                    this.click();
            //                }
            //            });
            //            apriConfirm("Sei sicuro di voler chiudere questa segnalazione?", callBackFunction, 300, 150, Messaggio.Titolo_Conferma, null);


        };
        function Stampa() {
            window.open('StampaSegnalazione.aspx?ID=' + document.getElementById('idSegnalazione').value, 'Stampa', '');
        };
        function EliminaPadre() {
            var chiediConferma = window.confirm('Sei sicuro di voler eliminare la relazione selezionata?');
            if (chiediConferma == true) {
                document.getElementById('HiddenFieldConfermaEliminaPadre').value = '1';
            } else {
                document.getElementById('HiddenFieldConfermaEliminaPadre').value = '0';
            };
        };
        function EliminaFiglia() {
            var chiediConferma = window.confirm('Sei sicuro di voler eliminare la relazione selezionata?');
            if (chiediConferma == true) {
                document.getElementById('HiddenFieldConfermaEliminaFiglia').value = '1';
            } else {
                document.getElementById('HiddenFieldConfermaEliminaFiglia').value = '0';
            };
        };
        function apriFiglia() {
            validNavigation = true;
            window.open('Segnalazione.aspx?NM=1&IDS=' + document.getElementById('HiddenFieldFigliaSelezionata').value, 'apri' + document.getElementById('HiddenFieldFigliaSelezionata').value, 'height=' + screen.height / 3 * 2 + ',top=0,left=0,width=' + screen.width / 3 * 2 + ',scrollbars=no,resizable=yes');
        };
        function apriPadre() {
            validNavigation = true;
            window.open('Segnalazione.aspx?NM=1&IDS=' + document.getElementById('HiddenFieldPadreSelezionato').value, 'apri' + document.getElementById('HiddenFieldPadreSelezionato').value, 'height=' + screen.height / 3 * 2 + ',top=0,left=0,width=' + screen.width / 3 * 2 + ',scrollbars=no,resizable=yes');
        };
        function apriIntervento() {
            validNavigation = true;
            window.open('NuovoIntervento.aspx?NM=1&IDI=' + document.getElementById('HiddenFieldIntervento').value, '');
        };

        function apriNuovoInt(sender, args) {
            validNavigation = true;
            window.open('NuovoIntervento.aspx?NM=1&IDI=' + document.getElementById('idIntervento').value + '', 'nuov', 'height=' + screen.height / 3 * 2 + ',top=100,left=100,width=' + screen.width / 3 * 2 + ',scrollbars=no,resizable=yes');
        }

        function svuota() {
            var assegnatario = $find("<%= cmbAssegnatario.ClientID %>");
            assegnatario.clearSelection();
            var tipoIntervento = $find("<%= cmbTipoInterv.ClientID %>");
            tipoIntervento.clearSelection();
        }
        function controlloCreaIntervento() {
            var tipoIntervento = $find("<%= cmbTipoInterv.ClientID %>");
            if (tipoIntervento.SelectedValue == '-1') {
                apriAlert('Campi obbligatori!', 300, 150, 'Attenzione', null, null);
            }
            var assegnatario = $find("<%= cmbAssegnatario.ClientID %>");
            if (assegnatario.SelectedValue == '-1') {
                apriAlert('Campi obbligatori!', 300, 150, 'Attenzione', null, null);
            }
        }
        function apriSegnaz(sender, args) {
            validNavigation = true;
            window.open('Segnalazione.aspx?NM=1&IDI=' + document.getElementById('idSegnalazione').value + '', 'nuov', 'height=' + screen.height / 3 * 2 + ',top=100,left=100,width=' + screen.width / 3 * 2 + ',scrollbars=no,resizable=yes');
        }

    </script>
    <link rel="stylesheet" href="../AUTOCOMPLETE/cmbstyle/chosen.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:MultiView ID="MultiView3" runat="server" ActiveViewIndex="0">
        <asp:View ID="View5" runat="server">
            <asp:Label ID="lblTitolo" Text="Segnalazione" runat="server" />
        </asp:View>
        <asp:View ID="View6" runat="server">
            <asp:Label ID="Label1" Text="Sollecito" runat="server" />
        </asp:View>
        <asp:View ID="View11" runat="server">
            <asp:Label ID="Label2" Text="Nota di chiusura" runat="server" />
        </asp:View>
        <asp:View ID="View12" runat="server">
            <asp:Label ID="Label3" Text="Allegati" runat="server" />
        </asp:View>
        <asp:View ID="View16" runat="server">
            <asp:Label ID="Label6" Text="Relazione segnalazioni padre/figlio" runat="server" />
        </asp:View>
        <asp:View ID="View19" runat="server">
            <asp:Label ID="Label9" Text="Elenco Interventi" runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
        <asp:View ID="View3" runat="server">
            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <%-- <asp:Button ID="btnAnnulla" runat="server" Text="Annulla Segnalaz." ToolTip="Annulla segnalazione"
                            OnClientClick="ConfermaAnnulla();" Width="140px" />--%>
                        <telerik:RadButton ID="btnAnnulla" runat="server" Text="Annulla Segnalaz." AutoPostBack="false"
                            ToolTip="Annulla segnalazione" OnClientClicking="function(sender, args){document.getElementById('CPContenuto_TextBox1').value='1';openWindow(sender, args, 'MasterPage_CPFooter_RadWindowAnnullaSegn');}">
                        </telerik:RadButton>
                    </td>
                    <td>
                        <asp:Button ID="btnSollecito" runat="server" Text="Sollecito" ToolTip="Sollecito" />
                    </td>
                    <td>
                        <asp:Button ID="imgChiudiSegnalazione" runat="server" Text="Chiudi Segnalaz." ToolTip="Chiudi Segnalazione"
                            Width="140px" />
                    </td>
                    <td>
                        <%-- <asp:Button ID="btnInterventi" runat="server" Text="Crea Intervento" ToolTip="Crea intervento"
                            Width="125px" ClientIDMode="Static" />--%>
                        <%-- <input id="btnInterventi" type="button" value="Crea intervento" onclick="MostraDiv();document.getElementById('CPContenuto_TextBox1').value = '1';"
                            style="cursor: pointer;" />
                        <asp:Button ID="btnInterventi1" runat="server" Text="" ClientIDMode="Static" ToolTip="Crea intervento"
                            CssClass="nascondiPulsante" />
                        --%>
                        <telerik:RadButton ID="btnInterventi" runat="server" Text="Crea Intervento" AutoPostBack="false"
                            OnClientClicking="function(sender, args){document.getElementById('CPContenuto_TextBox1').value='1';openWindow(sender, args, 'MasterPage_CPFooter_RadWindowAggiungi');}">
                        </telerik:RadButton>
                    </td>
                    <td>
                        <asp:Button ID="btnElencoInterventi" runat="server" Text="Elenco Interventi" ToolTip="Elenco interventi"
                            Width="125px" ClientIDMode="Static" />
                    </td>
                    <td>
                        <asp:Button ID="imgAllega" runat="server" Text="Allega File" ToolTip="Inserisci allegato" />
                    </td>
                    <td>
                        <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" Width="80px" />
                    </td>
                    <td>
                        <asp:Button ID="btnStampa" runat="server" Text="Stampa" OnClientClick="Stampa();"
                            ToolTip="Stampa" Width="80px" Visible="False" />
                    </td>
                    <td>
                        <asp:Button ID="btnRelazionePadreFiglio" runat="server" Text="Relazioni Padre/figlio"
                            ToolTip="Elenco segnalazioni padre e figlio" Width="150px" />
                    </td>
                    <td>
                        <asp:Button ID="btnEventi" runat="server" Text="Eventi" OnClientClick="" ToolTip="Eventi"
                            Width="80px" />
                    </td>
                    <td>
                        <asp:Button ID="imgEsci" runat="server" Text="Esci" OnClientClick="confermaEsci(document.getElementById('tipoAperturaFinestra').value,document.getElementById('txtModificato').value);return false;"
                            ToolTip="Esci" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <asp:Button ID="btnIndietro" runat="server" Text="Indietro" ToolTip="Indietro" />
                    </td>
                    <td>
                        <asp:Button ID="btnSalvaSollecito" runat="server" Text="Salva sollecito" ToolTip="Salva sollecito" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View9" runat="server">
            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <asp:Button ID="btnIndietro2" runat="server" Text="Indietro" ToolTip="Indietro" />
                    </td>
                    <td>
                        <asp:Button ID="btnChiudiSegnalazione" runat="server" Text="Chiudi segnalazione"
                            ToolTip="Chiudi segnalazione" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View10" runat="server">
            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <asp:Button ID="btnIndietro3" runat="server" Text="Indietro" ToolTip="Indietro" />
                    </td>
                    <td>
                        <asp:Button ID="btnAllegaFile" runat="server" Text="Allega" ToolTip="Allega file" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View18" runat="server">
            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <asp:Button ID="btnIndietro5" runat="server" Text="Indietro" ToolTip="Indietro" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View14" runat="server">
            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <asp:Button ID="btnVisualizza6" runat="server" Text="Visualizza" ToolTip="Visualizza" />
                    </td>
                    <td>
                        <asp:Button ID="btnIndietro6" runat="server" Text="Indietro" ToolTip="Indietro" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>Soggetto chiamante</legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 25%">
                                        Cognome
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="TextBoxCognomeChiamante" runat="server" MaxLength="500" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nome
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNomeChiamante" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Telefono 1
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxTelefono1Chiamante" runat="server" MaxLength="35"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Telefono 2
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxTelefono2Chiamante" runat="server" MaxLength="35"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxEmailChiamante" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>Informazioni generiche della segnalazione </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td>
                                        Numero segnalazione
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblNrich" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Stato
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblStato" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Data inserimento
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblDataInserimento" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ora inserimento
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblOraInserimento" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label Text="" runat="server" ID="lblSollecito" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Oggetto
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblOggetto" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Contratto
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblContratto" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Segnalazione padre
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblPadre" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Numero segnalazioni figlie
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblFiglie" />
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        Impianto
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblImpianto" />
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td>
                                        Appuntamento
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblAppuntamento" />
                                    </td>
                                </tr>--%>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset>
                            <legend>Oggetto della chiamata </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 25%">
                                        Cognome intestatario
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="TextBoxCognomeIntestatario" runat="server" MaxLength="500" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nome intestatario
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNomeIntestatario" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Codice contratto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCodiceContrattoIntestatario" runat="server" MaxLength="19"
                                            Width="150px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Codice unità immobiliare
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCodiceUnitaImmobiliare" runat="server" MaxLength="19" Width="150px"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        Complesso immobiliare
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListComplessoImmobiliare" runat="server" Width="250px"
                                            AutoPostBack="True" CssClass="chzn-select">
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        Edificio
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListEdificio" runat="server" Width="250px" AutoPostBack="True"
                                            CssClass="chzn-select">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Scala
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListScala" runat="server" Width="50px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Piano
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListPiano" runat="server" Width="100px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interno
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListInterno" runat="server" Width="50px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sede territoriale
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListSedeTerritoriale" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblInCondominio" />
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblInCondominioSiNo" />
                                        <asp:HiddenField runat="server" ID="idCondominio" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <asp:HiddenField runat="server" ID="anonimo" Value="0" ClientIDMode="Static" />
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>Danneggiante/danneggiato </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 25%">
                                        Danneggiante
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="TextBoxDanneggiante" runat="server" Width="70%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        Danneggiato
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="TextBoxDanneggiato" runat="server" Width="70%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset style="width: 70%">
                            <legend>Definizione categoria segnalazione </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 35%">
                                        Cerca categoria
                                    </td>
                                    <td style="width: 65%">
                                        <asp:DropDownList runat="server" ID="DropDownListTipologia" AutoPostBack="True" Width="90%"
                                            CssClass="chzn-select">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label Text="Categoria segnalazione" runat="server" ID="Label5" />
                                    </td>
                                    <td style="width: 50%">
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello0" AutoPostBack="True"
                                            Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label Text="Categoria 1" runat="server" ID="lblLivello1" />
                                    </td>
                                    <td style="width: 50%">
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello1" AutoPostBack="True"
                                            Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 2" runat="server" ID="lblLivello2" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello2" AutoPostBack="True"
                                            Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 3" runat="server" ID="lblLivello3" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello3" AutoPostBack="True"
                                            Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 4" runat="server" ID="lblLivello4" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello4" Width="90%" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <asp:Panel runat="server" ID="PanelCanale">
                            <table border="0" cellpadding="0" cellspacing="2">
                                <tr>
                                    <td style="vertical-align:top">
                                        <fieldset style="width: 200px; text-align: center;">
                                            <legend>Canale </legend>
                                            <asp:DropDownList ID="DropDownListCanale" runat="server" Font-Names="Arial" Font-Size="8pt">
                                            </asp:DropDownList>
                                        </fieldset>
                                    </td>
                                    <td style="vertical-align:top">
                                        <fieldset style="width: 200px; text-align: center;">
                                            <legend>Segnalazione falsa </legend>
                                            <asp:CheckBox ID="chkSegnalazioneFalsa" runat="server" />
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <script src="../AUTOCOMPLETE/cmbscript/chosen.jquery.js" type="text/javascript"></script>
            <script type="text/javascript">
                $(".chzn-select").chosen({
                    disable_search_threshold: 10,
                    no_results_text: "Nessun risultato trovato!",
                    placeholder_text_single: "- - -",
                    width: "95%"
                });
            </script>
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 50%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    Descrizione richiesta
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDescrizione" runat="server" MaxLength="4000" Width="95%" Font-Names="Arial"
                                        Font-Size="8pt" Height="100px" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Elenco allegati
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="Allegati" style="border: 1px solid #000000; height: 70px; background-color: #E4E4E4;
                                        overflow: scroll; visibility: visible; width: 95%;">
                                        <asp:Label Text="" runat="server" ID="TabellaAllegatiCompleta" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    Nuova nota
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxNota" runat="server" MaxLength="4000" Width="95%" Font-Names="Arial"
                                        Font-Size="8pt" Height="100px" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Note precedenti
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="NOTE" style="border: 1px solid #000000; height: 70px; background-color: #E4E4E4;
                                        overflow: scroll; width: 95%;">
                                        <asp:Label Text="" runat="server" ID="TabellaNoteComplete" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Button runat="server" value="0" ID="apriPaginaRelazioni" CssClass="nascondiPulsante"
                ClientIDMode="Static" />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 25%">
                        Note
                    </td>
                </tr>
                <tr>
                    <td style="width: 75%">
                        <asp:TextBox ID="txtNoteSoll" runat="server" TextMode="MultiLine" Width="100%" Height="300px"
                            MaxLength="4000"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View7" runat="server">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td>
                        <b>Note di chiusura segnalazione</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDescNoteChiusura" runat="server" MaxLength="4000" Width="95%"
                            Height="300px" TextMode="MultiLine" Rows="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Data e ora chiusura intervento</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label36" runat="server">Data:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDataCInt" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataCInt"
                                        ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" ToolTip="Inserire una data valida"
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        SetFocusOnError="True" ValidationGroup="a"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:Label ID="Label37" runat="server">Ora:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOraCInt" runat="server" MaxLength="5" Width="40px" ToolTip="Ora segnalazione in formato HH:MM"></asp:TextBox>
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
        </asp:View>
        <asp:View ID="View8" runat="server">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
                            TabIndex="4" Width="50%" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDescrizioneA" runat="server" MaxLength="15" Width="300px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Solo lettere (a-z) Max 15 caratteri senza spazi."
                            Font-Names="arial" Font-Size="8pt" ValidationExpression="^[|a-zA-Z]{1,15}$" ControlToValidate="txtDescrizioneA"
                            Font-Bold="True" ForeColor=""></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Descrizione
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDescrizioneAll" runat="server" MaxLength="500" Width="95%" Height="300px"
                            Rows="10" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View17" runat="server">
            <asp:Label ID="Label8" Text="Segnalazione padre" runat="server" Font-Names="Arial"
                Font-Bold="True" Font-Size="8pt" />
            <div id="divOverContentC1" style="width: 99%; overflow: auto;">
                <telerik:RadGrid ID="RadGridSegnalazionePadre" runat="server" AllowSorting="True"
                    GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                    PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                    Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Width="95%">
                    <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessuna segnalazione padre presente."
                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False" EmptyDataText="">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CRITICITA" HeaderText="CRITICITA'" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CAT 1" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CAT 2" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO3" HeaderText="CAT 3" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO4" HeaderText="CAT 4" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" HeaderStyle-Width="10%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO"
                                HeaderStyle-Width="10%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" HeaderStyle-Width="35%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true"
                                HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" Visible="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                        <HeaderStyle Wrap="True" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                </telerik:RadGrid>
            </div>
            <br />
            <br />
            <asp:TextBox ID="TextBoxPadre" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8" BorderStyle="None" BackColor="Transparent" BorderColor="Transparent"
                ClientIDMode="Static" Width="80%"></asp:TextBox>
            <table width="95%">
                <tr>
                    <td style="text-align: right">
                        <asp:Button ID="ButtonEliminaPadre" runat="server" Text="Elimina relazione selezionata"
                            ToolTip="Elimina la relazione selezionata" OnClientClick="EliminaPadre();" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HiddenFieldPadreSelezionato" runat="server" Value="0" ClientIDMode="Static" />
            <br />
            <br />
            <asp:Label ID="Label7" Text="Segnalazioni figlie" runat="server" Font-Names="Arial"
                Font-Bold="True" Font-Size="8pt" />
            <div id="radgrid" style="overflow: auto; width: 100%;">
                <telerik:RadGrid ID="RadGridSegnalazioniFiglie" runat="server" AllowSorting="True"
                    GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                    PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                    Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Height="95%">
                    <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessuna segnalazione figlia presente."
                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False" EmptyDataText="">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CRITICITA" HeaderText="CRITICITA'" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CAT 1" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CAT 2" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO3" HeaderText="CAT 3" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO4" HeaderText="CAT 4" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO"
                                HeaderStyle-Width="10%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" HeaderStyle-Width="35%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true"
                                HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" Visible="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                        <HeaderStyle Wrap="True" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                </telerik:RadGrid>
            </div>
            <table width="95%">
                <tr>
                    <td style="text-align: right">
                        <asp:Button ID="ButtonEliminaFiglie" runat="server" Text="Elimina relazione selezionata"
                            ToolTip="Elimina relazione selezionata" OnClientClick="EliminaFiglia();" />
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="TextBoxFiglie" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8" BorderStyle="None" BackColor="Transparent" BorderColor="Transparent"
                ClientIDMode="Static" Width="80%"></asp:TextBox>
            <asp:HiddenField ID="HiddenFieldFigliaSelezionata" runat="server" Value="0" ClientIDMode="Static" />
        </asp:View>
        <asp:View ID="View13" runat="server">
            <div id="Div1" style="width: 99%; overflow: auto;">
                <telerik:RadGrid ID="RadGridInterventi" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" PageSize="100" Culture="it-IT"
                    RegisterWithScriptManager="False" Font-Size="8pt" Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true"
                    Width="95%">
                    <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessun intervento presente"
                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_APERTURA" HeaderText="DATA APERTURA" HeaderStyle-Width="10%">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                        <HeaderStyle Wrap="True" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                </telerik:RadGrid>
            </div>
            <asp:TextBox ID="txtInterventoSelected" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8" BorderStyle="None" BackColor="Transparent" BorderColor="Transparent"
                ClientIDMode="Static" Width="80%"></asp:TextBox>
            <asp:HiddenField ID="HiddenFieldIntervento" runat="server" Value="0" ClientIDMode="Static" />
        </asp:View>
    </asp:MultiView>
    <%-- <div class="dialA" id="divInsA" style="visibility: hidden">
    </div>
    <div class="dialB" id="divInsB" style="visibility: hidden">
        <div id="InserimentoP" class="dialCTransparent">
            <table width="100%" cellpadding="0" cellspacing="0" class="tblDiv">
                <tr class="divTitoloText">
                    <td>
                        Seleziona Tipo Intervento
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Tipo Intervento" Font-Names="arial" Font-Size="8pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="cmbTipoInterv" runat="server" Width="250px" EnableLoadOnDemand="true"
                            OnClientLoad="OnClientLoadHandler">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr align="right">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSalvaTipoInt" runat="server" Text="Conferma" />
                                </td>
                                <td style="text-align: right">
                                    <asp:Button ID="btnEsciTipoInt" runat="server" OnClientClick="document.getElementById('CPContenuto_TextBox1').value='0';"
                                        Text="Esci" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>--%>
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField runat="server" ID="idSegnalazione" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idTipoSegnalazione" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idTipoSegnalazioneIniz" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="unita" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="descrizioneOLD" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="tipoLivello1old" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="tipoLivello2old" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="tipoLivello3old" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="tipoLivello4old" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="pericoloSegnalazioneOld" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="HiddenFieldConfermaEliminaPadre" ClientIDMode="Static"
        Value="0" />
    <asp:HiddenField runat="server" ID="HiddenFieldConfermaEliminaFiglia" ClientIDMode="Static"
        Value="0" />
    <asp:Button runat="server" ID="presaInCarico" value="0" ClientIDMode="Static" CssClass="nascondiPulsante" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField runat="server" ID="confermaTicketAmministrativo" ClientIDMode="Static"
        Value="0" />
    <asp:Button ID="imgChiudiSegnalazione1" runat="server" Text="" ClientIDMode="Static"
        ToolTip="" CssClass="nascondiPulsante" />
    <asp:HiddenField runat="server" ID="confermaUscita" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="confermaAnnullaSegnalazione" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="confermaChiusura" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSelected" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSelectedData" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSelectedFiliale" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="daElimina" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="confermaGenerica" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idIntervento" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="tipoAperturaFinestra" Value="0" ClientIDMode="Static" />
    <asp:Button runat="server" ID="btnElimina" CssClass="nascondiPulsante" />
    <script type="text/javascript">
        validNavigation = false;
    </script>
    <telerik:RadWindow ID="RadWindowAggiungi" runat="server" CenterIfModal="true" Modal="true"
        Width="500px" Height="180px" VisibleStatusbar="false" Behaviors="Pin,Close" RestrictionZoneID="RestrictionZoneID"
        Title="Crea Intervento" OnClientClose="svuota">
        <ContentTemplate>
            <table style="width: 100%;" class="tblDiv">
                <tr>
                    <td style="width: 25%">
                        Tipo Intervento
                    </td>
                    <td style="width: 75%">
                        <telerik:RadComboBox ID="cmbTipoInterv" runat="server" Width="250px" EnableLoadOnDemand="true"
                            OnClientLoad="OnClientLoadHandler">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        Assegnatario
                    </td>
                    <td style="width: 75%">
                        <telerik:RadComboBox ID="cmbAssegnatario" runat="server" EnableLoadOnDemand="true"
                            IsCaseSensitive="false" Filter="Contains" Width="250px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td style="width: 75%">
                        &nbsp
                    </td>
                    <td style="width: 25%" align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSalvaTipoInt" runat="server" Text="Salva" OnClientClick="controlloCreaIntervento();" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="RadButtonEsci" runat="server" Text="Esci" AutoPostBack="false"
                                        OnClientClicking="function(sender, args){closeWindow(sender, args, 'MasterPage_CPFooter_RadWindowAggiungi', '');}">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowAnnullaSegn" runat="server" CenterIfModal="true"
        Modal="true" Width="500px" Height="150px" VisibleStatusbar="false" Behaviors="Pin,Close"
        RestrictionZoneID="RestrictionZoneID" Title="Annulla segnalazione" OnClientClose="svuota">
        <ContentTemplate>
            <table style="width: 100%;" class="tblDiv">
                <tr>
                    <td style="width: 25%">
                        Motivo annullo
                    </td>
                    <td style="width: 75%">
                        <telerik:RadComboBox ID="cmbTipiAnnullo" runat="server" Width="250px" EnableLoadOnDemand="true"
                            OnClientLoad="OnClientLoadHandler">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td style="width: 75%">
                        &nbsp
                    </td>
                    <td style="width: 25%" align="right">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnSalvaAnnullo" runat="server" Text="Salva" ToolTip="Annulla segnalazione"
                                        OnClientClicking="function(sender, args){ConfermaAnnulla(sender, args);}">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="RadButtonEsci2" runat="server" Text="Esci" AutoPostBack="false"
                                        OnClientClicking="function(sender, args){closeWindow(sender, args, 'MasterPage_CPFooter_RadWindowAnnullaSegn', '');}">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>
