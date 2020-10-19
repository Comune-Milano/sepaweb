<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="Segnalazione.aspx.vb" Inherits="GESTIONE_CONTATTI_Segnalazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function AllegaFile() {
            if ((document.getElementById('idSegnalazione').value == '') || (document.getElementById('idSegnalazione').value == '-1')) {
                alert('E\' necessario salvare il contratto prima di allegare documenti!');
            } else {
                CenterPage('../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('idSegnalazione').value, 'Allegati', 1000, 800);
            };
        };


        //function ConfermaEsci() {
        //    //            var chiediConferma = window.confirm("Sei sicuro di voler uscire?");
        //    //            if (chiediConferma == true) {
        //    //                document.getElementById('confermaUscita').value = '1';
        //    //            } else {
        //    //                document.getElementById('confermaUscita').value = '0';
        //    //            }
        //    document.getElementById('confermaUscita').value = '1';
        //};
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
        function ConfermaAnnulla() {
            var chiediConferma = window.confirm("Sei sicuro di voler annullare questa segnalazione?");
            if (chiediConferma == true) {
                document.getElementById('confermaAnnullaSegnalazione').value = '1';
            } else {
                document.getElementById('confermaAnnullaSegnalazione').value = '0';
            };
        };
        function ConfermaChiusura() {

            
            var continua = true;
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
                if (Page_IsValid) {
                    continua = true;
                }
                else {
                    continua = false;
                };
            }
            else {
                continua = true;
            };
            if (continua == true) {
                var chiediConferma = window.confirm("Sei sicuro di voler chiudere questa segnalazione?");

                if (chiediConferma == true) {
                    document.getElementById('confermaChiusura').value = '1';
                } else {
                    document.getElementById('confermaChiusura').value = '0';
                }

            }
            else {
                alert('ATTENZIONE! Ci sono delle incongruenze dati della pagina!');
                return false;
            };

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

        function showImageOnSelectedItemChanging(sender, eventArgs) {
            var input = sender.get_inputDomElement();
            input.style.background = "url(" + eventArgs.get_item(sender._selectedIndex).get_imageUrl() + ") no-repeat";
        };
        function showFirstItemImage(sender) {
            var input = sender.get_inputDomElement();
            input.style.background = "url(" + sender.get_items().getItem(sender._selectedIndex).get_imageUrl() + ") no-repeat";
        };
        function apriNotaGestionale() {
            var oWnd = $find('MasterPage_CPContenuto_RadWindow1');
            oWnd.show();
        };
        function chiudiNotaGestionale() {
            var radwindow = $find('MasterPage_CPContenuto_RadWindow1');
            radwindow.close();
        };

        function ConfermaEsci() {
            if (document.getElementById('frmModify').value == '1') {
                var chiediConferma;
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('frmModify').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        };
    </script>
    <style type="text/css">
        .nascondiButton {
            display: none;
        }
    </style>
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
        <asp:View ID="View13" runat="server">
            <asp:Label ID="Label4" Text="Appuntamenti" runat="server" />
        </asp:View>
        <asp:View ID="View16" runat="server">
            <asp:Label ID="Label6" Text="Relazione segnalazioni padre/figlio" runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 42%; vertical-align: top;">
                        <fieldset>
                            <legend>Soggetto chiamante</legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 25%">Tipologia segnalante
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbTipologiaSegnalante" runat="server" AutoPostBack="false"
                                            Filter="Contains" TabIndex="-1" Width="250px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">Cognome
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="TextBoxCognomeChiamante" runat="server" MaxLength="500" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nome
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNomeChiamante" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Telefono 1
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxTelefono1Chiamante" runat="server" MaxLength="35"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Telefono 2
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxTelefono2Chiamante" runat="server" MaxLength="35"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Email
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxEmailChiamante" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <legend>Informazioni generiche della segnalazione </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td>Numero segnalazione
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblNrich" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Stato
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblStato" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Data inserimento
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblDataInserimento" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ora inserimento
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
                                    <td>Oggetto
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblOggetto" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Contratto
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblContratto" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Segnalazione padre
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblPadre" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Numero segnalazioni figlie
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblFiglie" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Allegati presenti
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblAllegatiPresenti" />
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
                    <td style="width: 58%; vertical-align: top;">
                        <fieldset>
                            <legend>Oggetto della chiamata </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 25%">Cognome intestatario
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="TextBoxCognomeIntestatario" runat="server" MaxLength="500" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nome intestatario
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNomeIntestatario" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Codice contratto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCodiceContrattoIntestatario" runat="server" MaxLength="19"
                                            Width="150px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Codice unità immobiliare
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCodiceUnitaImmobiliare" runat="server" MaxLength="19" Width="150px"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Edificio
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListEdificio" runat="server" Width="250px" AutoPostBack="True"
                                            CssClass="chzn-select">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Scala
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListScala" runat="server" Width="50px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Piano
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListPiano" runat="server" Width="100px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Interno
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListInterno" runat="server" Width="50px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Sede territoriale
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
                        <fieldset>
                            <legend>Danneggiante/danneggiato </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 25%">Danneggiante
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="TextBoxDanneggiante" runat="server" Width="70%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">Danneggiato
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="TextBoxDanneggiato" runat="server" Width="70%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <asp:HiddenField runat="server" ID="anonimo" Value="0" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="width: 70%">
                            <legend>Definizione categoria segnalazione </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 35%">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblConfermaTipologiaFornitore" runat="server" Text="Conferma tipologia"></asp:Label>
                                    </td>
                                                <td>
                                                    &nbsp&nbsp;&nbsp;<asp:Image ID="imgAlert" runat="server" ImageUrl="../Standard/Immagini/Alert.gif" />
                                    </td>
                                </tr>
                                        </table>
                                        
                                    </td>
                                    <td style="width: 65%">
                                        <telerik:RadComboBox runat="server" ID="cmbConfermaTipologiaFornitore" AutoPostBack="true"
                                            Width="300px" Filter="Contains">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="-1" Text="" />
                                                <telerik:RadComboBoxItem Value="1" Text="Conferma modifica fornitore" />
                                                <telerik:RadComboBoxItem Value="2" Text="Ripristina tipologia predefinita" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:Label ID="lblTipologiaConfermata" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 35%">Cerca categoria
                                    </td>
                                    <td style="width: 65%">
                                        <telerik:RadComboBox runat="server" ID="DropDownListTipologia" AutoPostBack="True"
                                            Width="300px" Filter="Contains" TabIndex="18">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label Text="Tipologia segnalazione" runat="server" ID="lblTipologiaManutenzione" />
                                    </td>
                                    <td style="width: 50%">
                                        <telerik:RadComboBox runat="server" ID="cmbTipologiaManutenzione" AutoPostBack="true"
                                            Width="300px" Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label Text="Categoria segnalazione" runat="server" ID="Label5" />
                                    </td>
                                    <td style="width: 50%">
                                        <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello0" AutoPostBack="True"
                                            Width="300px" TabIndex="19" Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label Text="Categoria 1" runat="server" ID="lblLivello1" />
                                    </td>
                                    <td style="width: 50%">
                                        <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello1" AutoPostBack="True"
                                            Filter="Contains" Width="90%">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 2" runat="server" ID="lblLivello2" />
                                    </td>
                                    <td>
                                        <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello2" AutoPostBack="True"
                                            Filter="Contains" Width="90%">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 3" runat="server" ID="lblLivello3" />
                                    </td>
                                    <td>
                                        <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello3" AutoPostBack="True"
                                            Filter="Contains" Width="90%">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 4" runat="server" ID="lblLivello4" />
                                    </td>
                                    <td>
                                        <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello4" Width="90%"
                                            AutoPostBack="True" Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <asp:Panel runat="server" ID="PanelUrgenzaCriticita">
                            <fieldset style="width: 50%">
                                <legend>Criticità </legend>
                                <table border="0" cellpadding="5" cellspacing="5" width="100%">
                                    <tr>
                                        <td>Iniziale
                                        </td>
                                        <td>Attuale
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox ID="cmbUrgenzaIniz" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Width="100px" onchange="document.getElementById('CPContenuto_txtDescrizione').focus();"
                                                OnClientSelectedIndexChanging="showImageOnSelectedItemChanging" OnClientLoad="showFirstItemImage"
                                                TabIndex="24" Filter="Contains">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbUrgenza" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Width="100px" onchange="document.getElementById('CPContenuto_txtDescrizione').focus();"
                                                OnClientSelectedIndexChanging="showImageOnSelectedItemChanging" OnClientLoad="showFirstItemImage"
                                                TabIndex="24" Filter="Contains">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel runat="server" ID="PanelCanale">
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel runat="server" ID="Panel1">
                                            <fieldset style="width: 50%">
                                                <legend>Canale </legend>
                                                <telerik:RadComboBox ID="DropDownListCanale" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Filter="Contains" Width="150px" TabIndex="24">
                                                </telerik:RadComboBox>
                                            </fieldset>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="CheckBoxDVCA" Text="Seguito da DVCA" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="CheckBoxAttoVandalico" Text="Atto vandalico" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="CheckBoxFalsa" Text="Falsa segnalazione" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <asp:CheckBox ID="CheckBoxContattatoFornitore" Text="Contattato Fornitore Emergenza"
                                            runat="server" AutoPostBack="True" />
                                    </td>
                                    <td style="width: 15%">
                                        <asp:Label ID="Label9" Text="data" runat="server" Width="40px" />
                                        <asp:TextBox runat="server" ID="TextBoxContattatoFornitore" Width="70px" Enabled="false" />
                                    </td>
                                    <td style="width: 20%">
                                        <telerik:RadTimePicker ID="TextBoxOraContattatoFornitore" runat="server" WrapperTableCaption=""
                                            Enabled="false" Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                            SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                            TimeView-HeaderText="Orario">
                                            <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                EndTime="19:15:00">
                                            </TimeView>
                                            <DateInput ID="DateInput3" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                DisplayDateFormat="HH:mm:ss">
                                                <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <asp:CheckBox ID="CheckBoxVerificaFornitore" Text="Verifica Fornitore Emergenza"
                                            runat="server" AutoPostBack="True" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" Text="data" runat="server" Width="40px" />
                                        <asp:TextBox runat="server" ID="TextBoxVerificaFornitore" Width="70px" Enabled="false" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="TextBoxOraVerificaFornitore" runat="server" WrapperTableCaption=""
                                            Enabled="false" Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                            SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                            TimeView-HeaderText="Orario">
                                            <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                EndTime="19:15:00">
                                            </TimeView>
                                            <DateInput ID="DateInput1" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                DisplayDateFormat="HH:mm:ss">
                                                <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <asp:Label ID="lblDataOraSopralluogo" Text="Data e ora sopralluogo" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" Text="" runat="server" Width="40px" />
                                        <asp:TextBox runat="server" ID="txtDataSopralluogo" Width="70px" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtOraSopralluogo" runat="server" WrapperTableCaption=""
                                            Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                            SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                            TimeView-HeaderText="Orario">
                                            <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                EndTime="19:15:00">
                                            </TimeView>
                                            <DateInput ID="DateInput2" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                DisplayDateFormat="HH:mm:ss">
                                                <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                    <td style="width: 5%">&nbsp;<img runat="server" id="imgBallYellowSopralluogo" alt="Criticità gialla" src="Immagini/alert.png" />
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="vertical-align: middle; width: 20%">Note sopralluogo
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNoteSopralluogo" runat="server" Width="100%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        <asp:Label ID="lblDataOraProgrammataIntervento" Text="Data programmata intervento"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label12" Text="" runat="server" Width="40px" />
                                        <asp:TextBox runat="server" ID="txtDataProgrammataIntervento" Width="70px" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtOraProgrammataIntervento" runat="server" WrapperTableCaption=""
                                            Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                            SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                            TimeView-HeaderText="Orario">
                                            <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                EndTime="19:15:00">
                                            </TimeView>
                                            <DateInput ID="DateInput4" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                DisplayDateFormat="HH:mm:ss">
                                                <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        <asp:Label ID="lblDataOraUltimoInterventoEseguito" Text="Data ultimo intervento eseguito"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label16" Text="" runat="server" Width="40px" />
                                        <asp:TextBox runat="server" ID="txtDataProgrammataUltimoIntervento" Width="70px" Enabled="false" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <asp:Label ID="lblDataOraEffettivaIntervento" Text="Data e ora effettiva intervento" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" Text="" runat="server" Width="40px" />
                                        <asp:TextBox runat="server" ID="txtDataEffettivaIntervento" Width="70px" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtOraEffettivaIntervento" runat="server" WrapperTableCaption=""
                                            Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                            SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                            TimeView-HeaderText="Orario">
                                            <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                EndTime="19:15:00">
                                            </TimeView>
                                            <DateInput ID="DateInput5" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                DisplayDateFormat="HH:mm:ss">
                                                <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                    <td>
                                        <img runat="server" id="imgBallYellowDataEffettiva" alt="Criticità gialla" src="Immagini/alert.png" />
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="vertical-align: middle; width: 20%">Note effettivo intervento
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNoteEffIntervento" runat="server" Width="100%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="PanelElencoDocumentiRichiesti" Visible="false" Width="100%">
                            <fieldset>
                                <legend>Elenco dei documenti richiesti in eventuale fase di appuntamento</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label Text="Nessun documento richiesto" runat="server" ID="lblDocumentiRichiesti" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid runat="server" ID="DataGridDocumentiRichiesti" AutoGenerateColumns="False"
                                                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                                                <ItemStyle BackColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                    Position="TopAndBottom" />
                                                <AlternatingItemStyle BackColor="Gainsboro" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="DOCUMENTI_RICHIESTI" HeaderText="DOCUMENTI RICHIESTI"></asp:BoundColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="White" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnNotaGestionale" Text="Nota Gestionale" OnClientClick="apriNotaGestionale();return false;" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>Nuova nota
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxNota" runat="server" MaxLength="4000" Width="95%" Font-Names="Arial"
                                        Font-Size="8pt" Height="100px" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Note precedenti
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="NOTE" style="border: 1px solid #000000; height: 70px; background-color: #E4E4E4; overflow: scroll; width: 95%;">
                                        <asp:Label Text="" runat="server" ID="TabellaNoteComplete" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>Descrizione richiesta
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDescrizione" runat="server" MaxLength="4000" Width="95%" Font-Names="Arial"
                                        Font-Size="8pt" Height="100px" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Elenco allegati
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="Allegati" style="border: 1px solid #000000; height: 70px; background-color: #E4E4E4; overflow: scroll; visibility: visible; width: 95%;">
                                        <asp:Label Text="" runat="server" ID="TabellaAllegatiCompleta" />
                                    </div>
                                </td>
                            </tr>
                        </table>
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
            <asp:Button runat="server" value="0" ID="apriPaginaRelazioni" CssClass="nascondiButton"
                ClientIDMode="Static" />
            <telerik:RadFormDecorator DecoratedControls="Buttons" runat="server" ID="RadFormDecorator1" />
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="OK"
                Localization-Cancel="Annulla">
            </telerik:RadWindowManager>
            <telerik:RadWindow runat="server" ID="RadWindow1" CenterIfModal="true" Modal="true"
                VisibleStatusbar="false" AutoSize="true" Behaviors="Pin, Move, Resize" Skin="Web20"
                ShowContentDuringLoad="false">
                <ContentTemplate>
                    <table width="500px">
                        <tr>
                            <td width="100%">Nota gestionale
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <telerik:RadTextBox ID="RadTextBox1" runat="server" TextMode="MultiLine" Rows="6"
                                    Width="98%" MaxLength="4000">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <table border="0" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" ID="btnAggiungiNotaGestionale" Text="Aggiungi" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnEsciNota" Text="Esci" OnClientClick="chiudiNotaGestionale();return false;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 25%">Note
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
                                        SetFocusOnError="True" ValidationGroup="chiudiSegnalazione"></asp:RegularExpressionValidator>
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
                    <td>Descrizione
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
        <asp:View ID="View14" runat="server">
            <asp:DataGrid runat="server" ID="DataGridElencoAppuntamenti" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" AllowPaging="false" PageSize="50"
                ShowFooter="false">
                <ItemStyle BackColor="White" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_SEGNALAZIONE" HeaderText="N° SEGN."></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_APPUNTAMENTO" HeaderText="DATA APPUNTAMENTO" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SPORTELLO" HeaderText="SPORTELLO" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ORARIO" HeaderText="ORARIO" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CELLULARE" HeaderText="CELLULARE" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ELIMINA" HeaderText="ELIMINAZIONE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FL_ELIMINA" HeaderText="" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_APP" HeaderText="" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_FILIALE" HeaderText="" Visible="false"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
        </asp:View>
        <asp:View ID="View17" runat="server">
            <asp:Label ID="Label8" Text="Segnalazione padre" runat="server" Font-Names="Arial"
                Font-Bold="True" Font-Size="8pt" />
            <div id="divOverContentC1" style="width: 99%; overflow: auto;">
                <telerik:RadGrid ID="RadGridSegnalazionePadre" runat="server" AllowSorting="True"
                    GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                    PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                    Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Height="95%" Width="95%">
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
                            CssClass="bottone" ToolTip="Elimina la relazione selezionata" OnClientClick="EliminaPadre();" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HiddenFieldPadreSelezionato" runat="server" Value="0" ClientIDMode="Static" />
            <br />
            <br />
            <asp:Label ID="Label7" Text="Segnalazioni figlie" runat="server" Font-Names="Arial"
                Font-Bold="True" Font-Size="8pt" />
            <div id="divOverContentC2" style="width: 99%; overflow: auto;">
                <telerik:RadGrid ID="RadGridSegnalazioniFiglie" runat="server" AllowSorting="True"
                    GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                    PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                    Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Height="95%" Width="95%">
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
                            CssClass="bottone" ToolTip="Elimina relazione selezionata" OnClientClick="EliminaFiglia();" />
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="TextBoxFiglie" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8" BorderStyle="None" BackColor="Transparent" BorderColor="Transparent"
                ClientIDMode="Static" Width="80%"></asp:TextBox>
            <asp:HiddenField ID="HiddenFieldFigliaSelezionata" runat="server" Value="0" ClientIDMode="Static" />
        </asp:View>
    </asp:MultiView>
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
    <asp:HiddenField runat="server" ID="flCustode" ClientIDMode="Static" Value="0" />
    <asp:HiddenField runat="server" ID="HiddenFieldConfermaEliminaPadre" ClientIDMode="Static"
        Value="0" />
    <asp:HiddenField runat="server" ID="HiddenFieldConfermaEliminaFiglia" ClientIDMode="Static"
        Value="0" />
    <asp:HiddenField ID="operatoreCC" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="operatoreFiliale" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="operatoreFilialeTecnico" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="operatoreComune" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="TipoAllegato" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenNotaSopralluogo" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenNotaEffIntervento" runat="server" Value="0" ClientIDMode="Static" />

    <asp:Button runat="server" ID="presaInCarico" value="0" ClientIDMode="Static" CssClass="nascondiButton" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField runat="server" ID="confermaTicketAmministrativo" ClientIDMode="Static"
        Value="0" />
    <asp:Button ID="imgChiudiSegnalazione1" runat="server" Text="" CssClass="nascondiButton"
        ClientIDMode="Static" ToolTip="" />
    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
        <asp:View ID="View3" runat="server">
            <asp:Button ID="btnAnnulla" runat="server" Text="Annulla segnalazione" CssClass="bottone"
                ToolTip="Annulla segnalazione" OnClientClick="ConfermaAnnulla();" />
            <asp:Button ID="btnSollecito" runat="server" Text="Sollecito" CssClass="bottone"
                ToolTip="Sollecito" />
            <asp:Button ID="imgChiudiSegnalazione" runat="server" Text="Chiudi segnalazione"
                CssClass="bottone" ToolTip="Chiudi segnalazione" />
            <asp:Button ID="imgAllega" runat="server" Text="Allegati" CssClass="bottone" ToolTip="Gestione allegati"
                OnClientClick="AllegaFile();return false;" />
            <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="bottone" ToolTip="Salva" />
            <asp:Button ID="btnStampa" runat="server" Text="Stampa" CssClass="bottone" OnClientClick="Stampa();"
                ToolTip="Stampa" />
            <asp:Button ID="btnStampSopr" runat="server" Text="Richiesta di Intervento a Canone"
                CssClass="bottone" ToolTip="Stampa il documento di sopralluogo" />
            <asp:Button ID="btnAppuntamento" runat="server" Text="Appuntamenti" CssClass="bottone"
                ToolTip="Elenco appuntamenti" />
            <asp:Button ID="btnRelazionePadreFiglio" runat="server" Text="Relazioni padre/figlio"
                CssClass="bottone" ToolTip="Elenco segnalazioni padre e figlio" />
            <asp:Button ID="btnEventi" runat="server" Text="Eventi" CssClass="bottone" OnClientClick=""
                ToolTip="Eventi" />
            <asp:Button ID="imgEsci" runat="server" Text="Esci" CssClass="bottone" OnClientClick="ConfermaEsci();"
                ToolTip="Esci" />
        </asp:View>
        <asp:View ID="View4" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnIndietro" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Indietro" />
                    </td>
                    <td>
                        <asp:Button ID="btnSalvaSollecito" runat="server" Text="Salva sollecito" CssClass="bottone"
                            ToolTip="Salva sollecito" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View9" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnIndietro2" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Indietro" />
                    </td>
                    <td>
                        <asp:Button ID="btnChiudiSegnalazione" runat="server" Text="Chiudi segnalazione"
                            OnClientClick="return ConfermaChiusura();" CssClass="bottone" ToolTip="Chiudi segnalazione" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View10" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnIndietro3" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Indietro" />
                    </td>
                    <td>
                        <asp:Button ID="btnAllegaFile" runat="server" Text="Allega" CssClass="bottone" ToolTip="Allega file" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View15" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnIndietro4" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Indietro" />
                    </td>
                    <td>
                        <asp:Button ID="btnAggiungiAppuntamento" runat="server" Text="Aggiungi" CssClass="bottone"
                            ToolTip="Aggiungi appuntamento" />
                    </td>
                    <td>
                        <asp:Button Text="" runat="server" ID="btnModifica" Style="display: none" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View18" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnIndietro5" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Indietro" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField runat="server" ID="confermaUscita" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="confermaAnnullaSegnalazione" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="confermaChiusura" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSelected" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSelectedData" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSelectedFiliale" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="daElimina" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="confermaGenerica" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSelectedIntestatario" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSelectedChiamante" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="flCondominio" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="fornitoreEsterno" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
    <asp:Button runat="server" ID="btnElimina" CssClass="nascondiButton" />
    <asp:HiddenField ID="HFbtnClickGo" runat="server" ClientIDMode="Static" Value="" />
    <script type="text/javascript">
                validNavigation = false;
                $(function () {
                    $("#CPContenuto_TextBoxContattatoFornitore").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                    $("#CPContenuto_TextBoxVerificaFornitore").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                    $("#CPContenuto_txtDataSopralluogo").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                    $("#CPContenuto_txtDataProgrammataIntervento").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                    $("#CPContenuto_txtDataEffettivaIntervento").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                });


    </script>
</asp:Content>
