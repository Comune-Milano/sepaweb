<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="NuovaSegnalazione.aspx.vb" Inherits="SICUREZZA_NuovaSegnalazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var SelezionatoChiamante;
        var SelezionatoIntestatario;
        var SelezionatoDanneggiante;
        var SelezionatoDanneggiato;
        var SelezionatoSegnalazioniUnita;
        var SelezionatoSegnalazioniEdificio;
        function ApriSegnalazioneEdificio() {
            validNavigation = true;
            window.open('Segnalazione.aspx?SL=1&IDS=' + document.getElementById('idSegnalazioneSelezionataEdificio').value, 'GestioneContatti' + document.getElementById('idSegnalazioneSelezionataEdificio').value, 'height=' + screen.height / 3 * 2 + ',top=0,left=0,width=' + screen.width / 3 * 2 + ',scrollbars=no,resizable=yes');
            //location.href = 'Segnalazione.aspx?IDS=' + document.getElementById('idSegnalazioneSelezionataEdificio').value;
        };
        function ApriSegnalazioneUnita() {
            validNavigation = true;
            window.open('Segnalazione.aspx?SL=1&IDS=' + document.getElementById('idSegnalazioneSelezionataUnita').value, 'GestioneContatti' + document.getElementById('idSegnalazioneSelezionataUnita').value, 'height=' + screen.height / 3 * 2 + ',top=0,left=0,width=' + screen.width / 3 * 2 + ',scrollbars=no,resizable=yes');
            //location.href = 'Segnalazione.aspx?IDS=' + document.getElementById('idSegnalazioneSelezionataUnita').value;
        };
        function FormaAnonima() {
            if ((document.getElementById('CPContenuto_TextBoxCognomeChiamante').value == '') || (document.getElementById('CPContenuto_TextBoxNomeChiamante').value == '')) {
                var chiediConferma = window.confirm('Non è stato identificato il chiamante.\nProcedere in forma anonima?');
                if (chiediConferma == true) {
                    document.getElementById('anonimo').value = '1';
                } else {
                    document.getElementById('anonimo').value = '0';
                    return false;
                };
            };
        };
        function apriAnagrafeSIPO() {

            if (document.getElementById('idSegnalazione').value == '') {
                document.getElementById('idSegnalazione').value = '0';
            }
            if (document.getElementById('idAnagraficaIntestatario').value != '') {
                if (document.getElementById('hfCodFiscaleSelected').value != '0' && document.getElementById('hfCodFiscaleSelected').value != '&nbsp;') {
                    document.getElementById('hfCodFiscale').value = document.getElementById('hfCodFiscaleSelected').value;
                    window.open('../Anagrafe.aspx?ID=' + document.getElementById('idSegnalazione').value + '&CF=' + document.getElementById('hfCodFiscale').value + '&T=7', 'Anagrafe', 'top=0,left=0,width=600,height=400');
                    return false;
                }
                else {
                    apriAlert('Codice fiscale mancante!', 300, 150, Messaggio.Titolo_Conferma, null, null); return false;
                }
            }
            else {
                apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
                return false;
            }

        }
        function apriMaschera() {
            var idSegn = document.getElementById('idSegnalazione').value;
            location.replace('Segnalazione.aspx?IDS=' + idSegn);
        }
        function ConfermaUnisciSegn(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    document.getElementById('ButtonUnisciSegnalazioni1').click();
                }
            });
            apriConfirm("Vuoi unire le segnalazioni selezionate?", callBackFunction, 300, 150, "Info", null);
            args.set_cancel(true);
        }
        function ConfermaUnisciEdif(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    document.getElementById('ButtonUnisciSegnalazioniEdifici2').click();
                }
            });
            apriConfirm("Vuoi unire le segnalazioni selezionate?", callBackFunction, 300, 150, "Info", null);
            args.set_cancel(true);
        }
    </script>
    <link rel="stylesheet" href="../AUTOCOMPLETE/cmbstyle/chosen.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Label ID="Label1" Text="Nuova Segnalazione - Individuazione del soggetto chiamante e dell'oggetto della richiesta"
                runat="server" />
        </asp:View>
        <asp:View ID="View4" runat="server">
            <asp:Label ID="Label2" Text="Nuova Segnalazione - Individuazione del soggetto chiamante"
                runat="server" />
        </asp:View>
        <asp:View ID="View10" runat="server">
            <asp:Label ID="Label4" Text="Nuova Segnalazione - Individuazione dell'intestatario"
                runat="server" />
        </asp:View>
        <asp:View ID="View16" runat="server">
            <asp:Label ID="Label7" Text="Nuova Segnalazione - Individuazione del danneggiante"
                runat="server" />
        </asp:View>
        <asp:View ID="View17" runat="server">
            <asp:Label ID="Label8" Text="Nuova Segnalazione - Individuazione del danneggiato"
                runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:MultiView ID="MultiView3" runat="server" ActiveViewIndex="0">
        <asp:View ID="View3" runat="server">
            <asp:Button ID="btnSvuota" runat="server" Text="Svuota" ToolTip="Svuota campi di ricerca" />
            <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva la segnalazione"
                ClientIDMode="Static" />
            <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="confermaEsci(0,document.getElementById('txtModificato').value);return false;" />
        </asp:View>
        <asp:View ID="View6" runat="server">
            <asp:Button ID="btnConfermaChiamante" runat="server" Text="Conferma" ToolTip="Conferma il chiamante selezionato"
                ClientIDMode="Static" />
            <asp:Button ID="btnIndietro" runat="server" Text="Indietro" ToolTip="Torna alla pagina precedente" />
        </asp:View>
        <asp:View ID="View12" runat="server">
            <%--<telerik:RadButton ID="btnAnagrafeSipo" runat="server" Text="Verifica SIPO" OnClientClicking="function(sender, args){if (document.getElementById('idAnagraficaIntestatario').value != '') {document.getElementById('hfCodFiscale').value=document.getElementById('hfCodFiscaleSelected').value;}else{  apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);}}">
            </telerik:RadButton>--%>
            <asp:Button ID="btnAnagrafeSipo" runat="server" Text="Verifica SIPO" OnClientClick="apriAnagrafeSIPO();"
                CausesValidation="False"></asp:Button>
            <asp:Button ID="btnConfermaIntestatario" runat="server" Text="Conferma" ToolTip="Conferma l'intestatario selezionato"
                ClientIDMode="Static" />
            <asp:Button ID="btnIndietro2" runat="server" Text="Indietro" ToolTip="Torna alla pagina precedente" />
        </asp:View>
        <asp:View ID="View20" runat="server">
            <asp:Button ID="btnConfermaDanneggiante" runat="server" Text="Conferma" ToolTip="Conferma l'intestatario selezionato"
                ClientIDMode="Static" />
            <asp:Button ID="btnIndietro5" runat="server" Text="Indietro" ToolTip="Torna alla pagina precedente" />
        </asp:View>
        <asp:View ID="View21" runat="server">
            <asp:Button ID="btnConfermaDanneggiato" runat="server" Text="Conferma" ToolTip="Conferma l'intestatario selezionato"
                ClientIDMode="Static" />
            <asp:Button ID="btnIndietro6" runat="server" Text="Indietro" ToolTip="Torna alla pagina precedente" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
        <asp:View ID="View2" runat="server">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 25%; vertical-align: top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 100%">
                                    <fieldset>
                                        <legend>Soggetto chiamante</legend>
                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td style="width: 25%">
                                                    Cognome / Ragione sociale
                                                </td>
                                                <td style="width: 75%">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="TextBoxCognomeChiamante" runat="server" MaxLength="500" Width="250px"
                                                                    TabIndex="1"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButtonCercaChiamante" runat="server" ImageUrl="Immagini/user.png"
                                                                    TabIndex="-1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Nome
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxNomeChiamante" runat="server" MaxLength="50" Width="250px"
                                                        TabIndex="2"></asp:TextBox>
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
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:ImageButton ID="ImageButtonCopiaDati" runat="server" Height="32px" ImageUrl="~/NuoveImm/down-icon.png"
                                        Width="32px" ToolTip="Copia dati dal soggetto chiamante" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>Definizione categoria segnalazione </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 30%">
                                        Cerca categoria
                                    </td>
                                    <td style="width: 70%">
                                        <asp:DropDownList runat="server" ID="DropDownListTipologia" AutoPostBack="True" Width="300px"
                                            CssClass="chzn-select">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Categoria segnalazione
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello0" AutoPostBack="True"
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 1" runat="server" ID="lblLivello1" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello1" AutoPostBack="True"
                                            Visible="False" Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 2" runat="server" ID="lblLivello2" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello2" AutoPostBack="True"
                                            Visible="False" Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 3" runat="server" ID="lblLivello3" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello3" Visible="False"
                                            Width="300px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Categoria 4" runat="server" ID="lblLivello4" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="cmbTipoSegnalazioneLivello4" AutoPostBack="True"
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="width: 25%; vertical-align: top">
                        <fieldset>
                            <legend>Numeri Utili</legend>
                            <telerik:RadGrid ID="DataGridNumeriUtili" runat="server" GroupPanelPosition="Top"
                                ResolvedRenderMode="Classic" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
                                IsExporting="False" AllowPaging="false" Height="180px">
                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                        ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="VALORE" HeaderText="CONTATTO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FASCIA" HeaderText="FASCIA ORARIA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SEDE_TERRITORIALE" HeaderText="S.T.">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    <Selecting AllowRowSelect="True" />
                                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                        AllowResizeToFit="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%; vertical-align: top">
                        <fieldset>
                            <legend>Oggetto della chiamata </legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td colspan="2" style="width: 25%">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True">Alloggio</asp:ListItem>
                                            <asp:ListItem Value="1">Parte comune</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <asp:Label ID="lblRagioneSocialeIntestario" runat="server" Text="Cognome intestatario/Rag.sociale"></asp:Label>
                                    </td>
                                    <td style="width: 75%">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCognomeIntestatario" runat="server" MaxLength="500" Width="250px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonCercaIntestatario" runat="server" ImageUrl="Immagini/user.png" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIntestatario" runat="server" Text="Nome intestatario"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNomeIntestatario" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCodiceContrattoIntestatario" runat="server" Text="Codice contratto"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCodiceContrattoIntestatario" runat="server" MaxLength="19"
                                            Width="150px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCodiceUnitaIntestatario" runat="server" Text="Codice unità immobiliare"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCodiceUnitaImmobiliare" runat="server" MaxLength="19" Width="150px"
                                            Enabled="False" TabIndex="9"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblComplessoIntestatario" runat="server" Text="Complesso immobiliare"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListComplessoImmobiliare" runat="server" Width="250px"
                                            AutoPostBack="True" CssClass="chzn-select" TabIndex="10">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEdificio" runat="server" Text="Edificio"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListEdificio" runat="server" Width="250px" AutoPostBack="True"
                                            CssClass="chzn-select" TabIndex="11">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblScala" runat="server" Text="Scala"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListScala" runat="server" Width="50px" AutoPostBack="True"
                                            TabIndex="12">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPiano" runat="server" Text="Piano"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListPiano" runat="server" Width="100px" AutoPostBack="True"
                                            TabIndex="13">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblInterno" runat="server" Text="Interno"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListInterno" runat="server" Width="50px" AutoPostBack="True"
                                            TabIndex="14">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSedeTerritoriale" runat="server" Text="Sede territoriale"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListSedeTerritoriale" runat="server" Width="250px"
                                            TabIndex="15">
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
                                <tr>
                                    <td>
                                        <asp:Label Text="Abusivo" runat="server" ID="lblAbusivo" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblAbusivoSiNo" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label Text="Moroso" runat="server" ID="lblMoroso" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblMorosoSiNo" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <asp:Panel ID="panelDann" runat="server" Visible="false">
                            <fieldset>
                                <legend>Danneggiante/danneggiato </legend>
                                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td style="width: 25%">
                                            Danneggiante
                                        </td>
                                        <td style="width: 75%">
                                            <asp:TextBox ID="TextBoxDanneggiante" runat="server" Width="70%" TabIndex="16"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButtonDanneggiante" runat="server" Height="16px" ImageUrl="../NuoveImm/user-icon.png"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Danneggiato
                                        </td>
                                        <td style="width: 75%">
                                            <asp:TextBox ID="TextBoxDanneggiato" runat="server" Width="70%" TabIndex="17"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButtonDanneggiato" runat="server" Height="16px" ImageUrl="../NuoveImm/user-icon.png"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                        <asp:HiddenField runat="server" ID="anonimo" Value="0" ClientIDMode="Static" />
                    </td>
                    <td style="width: 33%; vertical-align: top;">
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="PanelCanale">
                                        <fieldset style="width: 50%">
                                            <legend>Canale </legend>
                                            <asp:DropDownList ID="DropDownListCanale" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Width="150px" TabIndex="24">
                                            </asp:DropDownList>
                                        </fieldset>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                    Descrizione richiesta
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDescrizione" runat="server" MaxLength="4000" Width="95%" Font-Names="Arial"
                                        Font-Size="8pt" Height="100px" TextMode="MultiLine" Rows="5" TabIndex="25"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PanelSegnalazioniUnita" runat="server">
                            <fieldset>
                                <legend>Segnalazioni unità selezionata</legend>
                                <div style="overflow: auto; height: 220px; width: 100%">
                                    <asp:Label Text="" runat="server" ID="lblDataGridSegnalazioniUnitaSelezionata" />
                                    <telerik:RadGrid ID="RadDataGridSegnalazioniUnitaSelezionata" runat="server" AllowSorting="True"
                                        GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                                        PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                                        Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Width="97%" Height="200px"
                                        ShowHeadersWhenNoRecords="False">
                                        <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessuna segnalazione da visualizzare."
                                            HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckBoxTutteSegnalazioni" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxTutteSegnalazioni_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBoxSegnalazioni" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False" EmptyDataText="">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CRITICITA" HeaderText="CRITICITA'">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CAT 1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CAT 2">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO3" HeaderText="CAT 3">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO4" HeaderText="CAT 4">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true">
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
                                    <asp:DataGrid runat="server" ID="DataGridSegnalazioniUnitaSelezionata" AutoGenerateColumns="False"
                                        CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                                        <ItemStyle BackColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                            Position="TopAndBottom" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="CheckBoxTutteSegnalazioni" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxTutteSegnalazioni_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxSegnalazioni" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NUM" HeaderText="N°"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CRITICITA" HeaderText="CRITICITA'"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO1" HeaderText="CAT 1"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO2" HeaderText="CAT 2"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO3" HeaderText="CAT 3"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO4" HeaderText="CAT 4"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO" HeaderText="TIPO" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="White" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                    <asp:HiddenField runat="server" ID="idSegnalazioneSelezionataUnita" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="selezionateTutte" Value="0" ClientIDMode="Static" />
                                </div>
                            </fieldset>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="ButtonUnisciSegnalazioni" runat="server" Text="Unisci segnalazioni"
                                            ClientIDMode="Static" Visible="False" OnClientClicking="function(sender, args){ConfermaUnisciSegn(sender, args);}" />
                                        <asp:Button ID="ButtonUnisciSegnalazioni1" runat="server" Text="Ok" ClientIDMode="Static"
                                            CssClass="nascondiPulsante" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSegnalazioniUnitaDaunire" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelSegnalazioniEdificio" runat="server" Visible="False">
                            <fieldset>
                                <legend>Segnalazioni edificio selezionato</legend>
                                <div style="overflow: auto; height: 220px; width: 100%">
                                    <asp:Label Text="" runat="server" ID="lblDataGridSegnalazioniEdificioSelezionato" />
                                    <telerik:RadGrid ID="RadDataGridSegnalazioniEdificioSelezionato" runat="server" AllowSorting="True"
                                        GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                                        PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                                        Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Width="97%" Height="200px"
                                        ShowHeadersWhenNoRecords="False">
                                        <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessuna segnalazione da visualizzare."
                                            HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckBoxTutteSegnalazioniEdificio" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="CheckBoxTutteSegnalazioniEdificio_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBoxSegnalazioni" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False" EmptyDataText="">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CRITICITA" HeaderText="CRITICITA'">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CAT 1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CAT 2">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO3" HeaderText="CAT 3">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO4" HeaderText="CAT 4">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true">
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
                                    <asp:DataGrid runat="server" ID="DataGridSegnalazioniEdificioSelezionato" AutoGenerateColumns="False"
                                        CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                                        <ItemStyle BackColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                            Position="TopAndBottom" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="CheckBoxTutteSegnalazioniEdificio" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="CheckBoxTutteSegnalazioniEdificio_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxSegnalazioni" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NUM" HeaderText="N°"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CRITICITA" HeaderText="CRITICITA'"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO1" HeaderText="CAT 1"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO2" HeaderText="CAT 2"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO3" HeaderText="CAT 3"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO4" HeaderText="CAT 4"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO" HeaderText="TIPO" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="White" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                    <asp:HiddenField runat="server" ID="idSegnalazioneSelezionataEdificio" Value="" ClientIDMode="Static" />
                                </div>
                            </fieldset>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="ButtonUnisciSegnalazioniEdifici" runat="server" Text="Unisci segnalazioni"
                                            ClientIDMode="Static" Visible="False" OnClientClicking="function(sender, args){ConfermaUnisciEdif(sender, args);}" />
                                        <asp:Button ID="ButtonUnisciSegnalazioniEdifici2" runat="server" Text="Ok" ClientIDMode="Static"
                                            CssClass="nascondiPulsante" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSegnalazioniEdificiDaUnire" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="width: 33%; vertical-align: top; height: 150px; overflow: auto">
                        <asp:Panel runat="server" ID="panelAmministratore" Visible="false">
                            <fieldset>
                                <legend>Amministratore di condominio </legend>
                                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td style="width: 25%">
                                            Amministratore
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblAmministratore" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Indirizzo
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblIndirizzo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Recapito telefonico 1
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblTelefono1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Recapito telefonico 2
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblTelefono2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Recapito telefonico 3
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblTelefono3" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Fax
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblFax" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Email
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblEmail" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Note
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblNote" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Partita IVA
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblPartitaIVA" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
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
        </asp:View>
        <asp:View ID="View5" runat="server">
            <asp:Label ID="lblChiamanti" Text="Elenco inquilini" runat="server" Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridChiamanti" AutoGenerateColumns="False" CellPadding="2"
                Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_INTE" HeaderText="ID_INTE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CODICE_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA NAS."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO U.I."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                    </asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <br />
            <br />
            <asp:Label ID="Label3" Text="Elenco custodi" runat="server" Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridCustodi" AutoGenerateColumns="False" CellPadding="2"
                Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="MATRICOLA" HeaderText="MATRICOLA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DIPENDENTE_MM" HeaderText="DIPENDENTE MM"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CELLULARE" HeaderText="CELLULARE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <br />
            <br />
            <asp:Label ID="Label6" Text="Elenco chiamanti non noti" runat="server" Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridChiamantiNonNoti" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CELLULARE" HeaderText="CELLULARE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <asp:HiddenField Value="" runat="server" ID="idSelectedChiamante" ClientIDMode="Static" />
            <asp:HiddenField Value="" runat="server" ID="idAnagraficaChiamante" ClientIDMode="Static" />
            <asp:HiddenField Value="" runat="server" ID="idContrattoChiamante" ClientIDMode="Static" />
        </asp:View>
        <asp:View ID="View11" runat="server">
            <asp:Label ID="Label5" Text="" runat="server" />
            <asp:DataGrid runat="server" ID="DataGridIntestatari" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_INTE" HeaderText="ID_INTE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CODICE_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="cod_fiscale" HeaderText="COD. FISCALE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA NAS."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO U.I."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                    </asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <asp:HiddenField Value="" runat="server" ID="idSelectedIntestatario" ClientIDMode="Static" />
            <asp:HiddenField Value="" runat="server" ID="copiato" ClientIDMode="Static" />
            <asp:HiddenField Value="" runat="server" ID="idAnagraficaIntestatario" ClientIDMode="Static" />
            <asp:HiddenField Value="" runat="server" ID="idContrattoIntestatario" ClientIDMode="Static" />
        </asp:View>
        <asp:View ID="View18" runat="server">
            <asp:Label ID="Label9" Text="" runat="server" />
            <asp:DataGrid runat="server" ID="DataGridDanneggiante" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_INTE" HeaderText="ID_INTE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CODICE_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA NAS."></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO U.I."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                    </asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <asp:HiddenField ID="Danneggiante" runat="server" Value="" ClientIDMode="Static" />
        </asp:View>
        <asp:View ID="View19" runat="server">
            <asp:Label ID="Label10" Text="" runat="server" />
            <asp:DataGrid runat="server" ID="DataGridDanneggiato" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_INTE" HeaderText="ID_INTE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CODICE_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA NAS."></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO U.I."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                    </asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <asp:HiddenField ID="Danneggiato" runat="server" Value="" ClientIDMode="Static" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <script type="text/javascript">
        validNavigation = false;
    </script>
    <asp:HiddenField ID="flCustode" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCodFiscaleSelected" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCodFiscale" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="idSegnalazione" runat="server" Value="-1" ClientIDMode="Static" />
</asp:Content>
