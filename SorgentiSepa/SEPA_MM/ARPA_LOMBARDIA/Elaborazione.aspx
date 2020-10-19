<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="Elaborazione.aspx.vb" Inherits="ARPA_LOMBARDIA_Elaborazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadButton ID="btnFileXml" runat="server" Text="Crea/Scarica XML" ToolTip="Crea o Scarica il File XML dell'Elaborazione">
    </telerik:RadButton>
    &nbsp;
    <telerik:RadButton ID="btnVisualizzaAnomalie" runat="server" Text="Visualizza Anomalie"
        ToolTip="Visualizza Anomalie">
    </telerik:RadButton>
    &nbsp;
    <telerik:RadButton ID="btnExport" runat="server" Text="Esporta in Excel" ToolTip="Esporta in Excel">
    </telerik:RadButton>
    &nbsp;
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="TornaHome"
        AutoPostBack="false">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="RadGridElaborazione" runat="server" AllowSorting="false" AutoGenerateColumns="False"
        AllowFilteringByColumn="false" EnableLinqExpressions="False" IsExporting="False"
        Width="97%" AllowPaging="True" PageSize="100">
        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="Nessun fabbricato da visualizzare."
            ShowHeadersWhenNoRecords="true" TableLayout="Auto" ClientDataKeyNames="ID_EDIFICIO"
            DataKeyNames="ID_EDIFICIO" HierarchyLoadMode="ServerOnDemand" Name="Fabbricati">
            <DetailTables>
                <telerik:GridTableView DataKeyNames="ID_UNITA" Name="UnitaImmobiliari" AllowPaging="false"
                    AllowSorting="false" AllowFilteringByColumn="false" HierarchyLoadMode="ServerOnDemand"
                    NoDetailRecordsText="Nessuna unità immobiliare da visualizzare">
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="ID_CONTRATTO" Name="Nuclei" AllowPaging="false"
                            AllowSorting="false" AllowFilteringByColumn="false" HierarchyLoadMode="ServerOnDemand"
                            NoDetailRecordsText="Nessun nucleo da visualizzare">
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="ID_CONTRATTO" Name="Inquilini" AllowPaging="false"
                                    AllowSorting="false" AllowFilteringByColumn="false" HierarchyLoadMode="ServerOnDemand"
                                    NoDetailRecordsText="Nessun inquilino da visualizzare">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CODICE_INQUILINO" HeaderText="Codice Inquilino">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CODICE_FISCALE" HeaderText="Codice Fiscale">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOME" HeaderText="Nome">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COGNOME" HeaderText="Cognome">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SESSO" HeaderText="Sesso">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_NASCITA" HeaderText="Data di Nascita">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CITTADINANZA" HeaderText="Cittadinanza">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NAZIONE_NASCITA" HeaderText="Nazione di Nascita">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COMUNE_NASCITA" HeaderText="Comune di Nascita">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="INTESTATARIO_CONTRATTO" HeaderText="Intestatario del Contratto">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RAPPORTO_PARENTELA" HeaderText="Rapporto di Parentela">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CONDIZIONE_LAVORO" HeaderText="Condizione di Lavoro">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NUCLEO_FAMILIARE" HeaderText="Nucleo Familiare">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="false">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerik:GridBoundColumn DataField="TIPO_SOGGETTO_OCCUPANTE" HeaderText="Tipologia Soggetto Occupante">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CODICE_FISCALE" HeaderText="Codice Fiscale">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RAGIONE_SOCIALE" HeaderText="Ragione Sociale">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AREA_ISEE" HeaderText="Area ISEE">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FASCIA_ISEE" HeaderText="Fascia Area ISEE">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ISEE_ERP" HeaderText="ISEE ERP">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ISEE" HeaderText="ISEE">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ISR" HeaderText="ISR">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ISP" HeaderText="ISP">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PSE" HeaderText="PSE">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_STIPULA_CONTRATTO" HeaderText="Data Stipula Contratto">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CANONE_APPLICATO" HeaderText="Canone Applicato">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_EDIFICIO" HeaderText="ID_EDIFICIO" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="false">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID_ENTE_PROPRIETARIO" HeaderText="ID Ente Proprietario">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPO_ENTE_PROPRIETARIO" HeaderText="Tipo Ente Proprietario">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CD_ALLOGGIO" HeaderText="Codice Alloggio">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPOLOGIA_GESTORE" HeaderText="Tipologia del Gestore">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ID_ENTE_GESTORE" HeaderText="ID Ente Gestore">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CF_ENTE_GESTORE" HeaderText="Codice Fiscale Ente Gestore">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RAG_SOCIALE_ENTE_GESTORE" HeaderText="Ragione Sociale Ente Gestore">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CD_CATASTALE_ENTE_GESTORE" HeaderText="Codice Catastale Ente Gestore">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DENOMINAZIONE_ENTE_GESTORE" HeaderText="Denominazione Ente Gestore">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FOGLIO" HeaderText="Foglio">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PARTICELLA" HeaderText="Particella">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SUBALTERNO" HeaderText="Subalterno">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CATEGORIA" HeaderText="Categoria Catastale">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CLASSE" HeaderText="Classe">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CONSISTENZA" HeaderText="Consistenza">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RENDITA" HeaderText="Rendita Catastale">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PREFISSO_INDIRIZZO" HeaderText="Prefisso Indirizzo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="VIA_PIAZZA" HeaderText="Via/Piazza">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NUMERO_CIVICO" HeaderText="Numero Civico">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PIANO" HeaderText="Piano">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SCALA" HeaderText="Scala">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CAP" HeaderText="CAP">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LOCALITA" HeaderText="Localit&agrave;">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SUPERFICIE_UTILE" HeaderText="Superficie Utile">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_BARRIERE_ARCHITETTONICHE" HeaderText="Barriere architettoniche">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_ASCENSORE" HeaderText="Ascensore">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_CANTINA_SOLAIO" HeaderText="Cantina/Solaio di pertinenza">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_BOX_POSTO_AUTO" HeaderText="Box/Posto Auto di pertinenza">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DESTINAZIONE_USO" HeaderText="Destinazione d'Uso">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_ALLOGGIO_ESCLUSO" HeaderText="Alloggio Escluso">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="STATO_UNITA" HeaderText="Stato dell'Unita Immobiliare">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_ACCORPATO" HeaderText="Unità Immobiliare accorpata">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_FRAZIONATO" HeaderText="Unità Immobiliare frazionata">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_DEMOLITA" HeaderText="Unità Immobiliare demolita">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_VENDUTO" HeaderText="Unità Immobiliare venduta">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_PIANO_VENDITA" HeaderText="Piano di Vendita">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NUM_AUTORIZZAZIONE_REG_VEND" HeaderText="Autorizzazione Regionale (numero)">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DATA_AUTORIZZAZIONE_REG_VEND" HeaderText="Autorizzazione Regionale (data)">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FL_PIANO_VALORIZZAZIONE" HeaderText="Piano di Valorizzazione">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NUM_AUTORIZZAZIONE_REG_VAL" HeaderText="Autorizzazione Regionale (numero)">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DATA_AUTORIZZAZIONE_REG_VAL" HeaderText="Autorizzazione Regionale (data)">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ID_EDIFICIO" HeaderText="ID_EDIFICIO" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="false">
                        </telerik:GridBoundColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
            <Columns>
                <telerik:GridBoundColumn DataField="CD_FABBRICATO" HeaderText="Codice Fabbricato">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CF_ENTE_PROPRIETARIO" HeaderText="Codice Fiscale Ente Proprietario">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPO_PROPRIETA" HeaderText="Tipo Propriet&agrave; del Fabbricato">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NUM_ALLOGGI_ALTRA_PROPRIETA" HeaderText="Numero alloggi di altra proprietà">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ANNO_COSTRUZIONE" HeaderText="Anno di Costruzione">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CD_CATASTALE_COMUNE" HeaderText="Codice Catastale Comune">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CD_ISTAT_COMUNE" HeaderText="Codice ISTAT Comune">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ID_ENTE_PROPRIETARIO" HeaderText="ID Ente Proprietario">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ID_EDIFICIO" HeaderText="ID_EDIFICIO" Visible="false">
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="CD_FABBRICATO" SortOrder="Ascending" />
            </SortExpressions>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
    <telerik:RadWindow ID="RadWindowElaborazioneAnomalie" runat="server" CenterIfModal="true"
        Modal="true" Width="600px" Height="400px" VisibleStatusbar="false" Title="Elaborazione - Anomalie"
        Behaviors="Move, Close" RestrictionZoneID="RestrictionZoneID" ReloadOnShow="True"
        ShowContentDuringLoad="False">
        <ContentTemplate>
            <asp:Panel runat="server" ID="PanelRadWindowElaborazioneAnomalie" Style="height: 100%;">
                <br />
                <telerik:RadTextBox ID="txtAnomalieElaborazioni" runat="server" Width="97%" Height="320px"
                    ReadOnly="true" TextMode="MultiLine">
                </telerik:RadTextBox>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblElabValidazione" runat="server" Text="Elaborazione creata senza la procedura di validazione!"
        Font-Bold="True" Font-Italic="True" Font-Size="12pt" Font-Underline="True" Visible="False"
        ForeColor="Red" />
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Com"
        Height="85px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
        AutoCloseDelay="3500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
    <asp:HiddenField ID="HFIdElaborazione" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="90" ClientIDMode="Static" />
</asp:Content>
