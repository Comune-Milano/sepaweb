<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false"
    CodeFile="SegnalazioniCanone.aspx.vb" Inherits="FORNITORI_SegnalazioniCanone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../CICLO_PASSIVO/CicloPassivo.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ClickUscita(sender, args) {
            location.href = 'Home.aspx';
        };
        function RowSelecting(sender, args) {
            document.getElementById('idSelected').value = args.getDataKeyValue("ID");
        };
        function ModificaDblClick() {
            document.getElementById('MasterPage_CPMenu_btnVisualizza').click();
        };
        function ApriSegnalazione() {
            if (document.getElementById('idSelected').value != '') {
                CenterPage2('../GESTIONE_CONTATTI/Segnalazione.aspx?TIPO=1&NM=1&IDS=' + document.getElementById('idSelected').value, 'Segnalazione' + document.getElementById('idSelected').value, 1200, 800);
            } else {
                alert('Selezionare una segnalazione!');
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Segnalazioni a canone"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <table cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <asp:Button Text="Visualizza" runat="server" ToolTip="Visualizza segnalazione" ID="btnVisualizza" OnClientClick="ApriSegnalazione();return false;" />
                <asp:Button Text="Esporta" runat="server" ToolTip="Esporta le segnalazioni" ID="btnEsporta" />
                <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
                    AutoPostBack="False" CausesValidation="False" OnClientClicking="ClickUscita"
                    TabIndex="3">
                </telerik:RadButton>
            </td>

        </tr>
        <tr>
            <td>
                <telerik:RadButton ID="RadButtonDirettoreLavori" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton ID="RadButtonBuildingManager" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>

            </td>
            <td>
                <telerik:RadButton ID="RadButtonFieldQualityManager" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Coordinatore qualità" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Coordinatore qualità" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton ID="RadButtonTecnicoAmministrativo" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="height: 50px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="chkSoloChiuse" runat="server" Text="Mostra solo segnalazioni chiuse" AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="dgvSegnalazioni" runat="server" AutoGenerateColumns="False"
                    AllowFilteringByColumn="true" EnableLinqExpressions="False" IsExporting="False"
                    Width="97%" AllowPaging="true" PageSize="100">
                    <MasterTableView CommandItemDisplay="none" AllowSorting="true" AllowMultiColumnSorting="true"
                        TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                        Width="200%" ClientDataKeyNames="ID, STATO,ID_STATO" DataKeyNames="ID,STATO,ID_STATO">
                        <CommandItemSettings ShowAddNewRecordButton="False" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" Visible="true" Exportable="true"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="" Exportable="false" Visible="false" EmptyDataText=" ">
                            </telerik:GridBoundColumn>



                            <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">

                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxFiltroFO" Width="100%" AppendDataBoundItems="true"
                                        runat="server" OnClientSelectedIndexChanged="FilterFOIndexChanged" HighlightTemplatedItems="true"
                                        Filter="Contains" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlockFO" runat="server">
                                        <script type="text/javascript">
                                            function FilterFOIndexChanged(sender, args) {
                                                var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                var filtro = args.get_item().get_value();
                                                document.getElementById('HFFiltroFO').value = filtro;
                                                if (filtro != 'Tutti') {
                                                    tableView.filter("FORNITORE", filtro, "EqualTo");
                                                } else {
                                                    tableView.filter("FORNITORE", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                };
                                            };
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>

                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="REPERTORIO" Visible="true" SortExpression="REP_ORD"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxFiltroRE" Width="100%" AppendDataBoundItems="true"
                                        runat="server" OnClientSelectedIndexChanged="FilterREIndexChanged" HighlightTemplatedItems="true"
                                        Filter="Contains" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlockRE" runat="server">
                                        <script type="text/javascript">
                                            function FilterREIndexChanged(sender, args) {
                                                var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                var filtro = args.get_item().get_value();
                                                document.getElementById('HFFiltroRE').value = filtro;
                                                if (filtro != 'Tutti') {
                                                    tableView.filter("NUM_REPERTORIO", filtro, "EqualTo");
                                                } else {
                                                    tableView.filter("NUM_REPERTORIO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                };
                                            };
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="TIPO_INT" DataField="TIPO_INT" HeaderText="CRITICITA'" Visible="true"
                                Exportable="false" >
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxFiltroPericolo" Width="100px" AppendDataBoundItems="true" RenderMode="Classic"
                                        runat="server" OnClientSelectedIndexChanged="FilterPericoloIndexChanged" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlockPericolo" runat="server">
                                        <script type="text/javascript">
                                            function FilterPericoloIndexChanged(sender, args) {
                                                var tableView = $find("<%# TryCast(Container, GridItem).OwnerTableView.ClientID %>");
                                                var filtro = args.get_item().get_value();
                                                document.getElementById('HFFiltroPericolo').value = filtro;
                                                if (filtro != 'Tutti') {
                                                    tableView.filter("PERICOLO_SEGNALAZIONE", filtro, "EqualTo");
                                                } else {
                                                    tableView.filter("PERICOLO_SEGNALAZIONE", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                };
                                            };
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                                <ItemTemplate>
                                    <%# getImgPericoloRichiesto(DataBinder.Eval(Container.DataItem, "ID_PERICOLO_SEGNALAZIONE")) %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PERICOLO_SEGNALAZIONE" HeaderText="PRIORITA'" Visible="true"  UniqueName="PERICOLO_SEGNALAZIONE"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>



                            <telerik:GridBoundColumn DataField="TIPO0" HeaderText="CATEGORIA" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CATEGORIA 1" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>



                            <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CATEGORIA 2" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO3" HeaderText="CATEGORIA 3" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO4" HeaderText="CATEGORIA 4" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="true" Exportable="true" SortExpression="ID_STATO"
                                DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxFiltroStato" Width="100%" AppendDataBoundItems="true"
                                        runat="server" OnClientSelectedIndexChanged="FilterStatoIndexChanged" HighlightTemplatedItems="true"
                                        Filter="Contains" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlockStato" runat="server">
                                        <script type="text/javascript">
                                            function FilterStatoIndexChanged(sender, args) {
                                                var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                var filtro = args.get_item().get_value();
                                                document.getElementById('HFFiltroStato').value = filtro;
                                                if (filtro != 'Tutti') {
                                                    tableView.filter("STATO", filtro, "EqualTo");
                                                } else {
                                                    tableView.filter("STATO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                };
                                            };
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="FILIALE" HeaderText="ST" Visible="true" Exportable="true" SortExpression="FILALE"
                                DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxFiltroST" Width="100%" AppendDataBoundItems="true"
                                        runat="server" OnClientSelectedIndexChanged="FilterSTIndexChanged" HighlightTemplatedItems="true"
                                        Filter="Contains" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlockST" runat="server">
                                        <script type="text/javascript">
                                            function FilterSTIndexChanged(sender, args) {
                                                var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                var filtro = args.get_item().get_value();
                                                document.getElementById('HFFiltroST').value = filtro;
                                                if (filtro != 'Tutti') {
                                                    tableView.filter("FILIALE", filtro, "EqualTo");
                                                } else {
                                                    tableView.filter("FILIALE", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                };
                                            };
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>

                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO_SEGNALANTE" HeaderText="TIPO SEGNALANTE" Visible="true" Exportable="true" SortExpression="TIPO_SEGNALANTE"
                                DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CODICE_RU" HeaderText="CODICE CONTRATTO" Visible="false"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PIANO" HeaderText="PIANO" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="INTERNO" HeaderText="INTERNO" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TELEFONO1" HeaderText="TELEFONO 1" Visible="true"
                                Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TELEFONO2" HeaderText="TELEFONO 2" Visible="true"
                                Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO"
                                FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>

                                                        <telerik:GridDateTimeColumn DataField="DATA_PROGRAMMATA_INT2" HeaderText="DATA ULTIMO INT. ESEGUITO"
                                FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="DATA_PROGRAMMATA_INT" HeaderText="DATA PROGRAMMATA INTERVENTO"
                                FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="DATA_SOPRALLUOGO" HeaderText="DATA SOPRALLUOGO"
                                FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="DATA_EFFETTIVA_INT" HeaderText="DATA EFFETTIVA INTERVENTO"
                                FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridDateTimeColumn>
                                                      
                            <telerik:GridBoundColumn DataField="NOTE_C" HeaderText="NOTE CHIUSURA" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OPERATORE_CH" HeaderText="OERATORE CHIUSURA" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="N_SOLLECITI" HeaderText="N° SOLLECITI" Visible="true"
                                Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FIGLI2" HeaderText="FIGLI2" Visible="false" Exportable="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="false"
                                Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE"
                                Visible="false" EmptyDataText=" " Exportable="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_SEGNALAZIONE_PADRE" HeaderText="N° SEGN. PADRE"
                                Visible="false" Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CRONOPROGRAMMA" HeaderText="CRONOPROGRAMMA"
                                Visible="true" Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ALLEGATI" HeaderText="ALLEGATI" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxFiltroAL" Width="100%" AppendDataBoundItems="true"
                                        runat="server" OnClientSelectedIndexChanged="FilterALIndexChanged" HighlightTemplatedItems="true"
                                        Filter="Contains" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlockAL" runat="server">
                                        <script type="text/javascript">
                                            function FilterALIndexChanged(sender, args) {
                                                var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                var filtro = args.get_item().get_value();
                                                document.getElementById('HFFiltroAL').value = filtro;
                                                if (filtro != 'Tutti') {
                                                    tableView.filter("ALLEGATI", filtro, "Contains");
                                                } else {
                                                    tableView.filter("ALLEGATI", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                };
                                            };
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" Visible="False" Exportable="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_STATO" HeaderText="ID_STATO" Visible="false"
                                Exportable="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                        <SortExpressions>
                            <%--<telerik:GridSortExpression FieldName="ID_PERICOLO_SEGNALAZIONE" SortOrder="Descending" />
                            <telerik:GridSortExpression FieldName="ID_STATO" SortOrder="Ascending" />
                            <telerik:GridSortExpression FieldName="N_SOLLECITI" SortOrder="Descending" />
                            <telerik:GridSortExpression FieldName="FIGLI" SortOrder="Descending" />--%>
                            <telerik:GridSortExpression FieldName="NUM" SortOrder="Descending" />
                        </SortExpressions>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                        <ClientEvents OnRowSelecting="RowSelecting" OnRowClick="RowSelecting" OnRowDblClick="ModificaDblClick" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenField1" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivHeight" Value="100" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivWidth" Value="1" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="Modificato" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="isExporting" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="IndiceFornitore" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="OPS" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HFFiltroPericolo" />
    <asp:HiddenField ID="HFFiltroStato" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroAL" runat="server" Value="Tutti" ClientIDMode="Static" />

    <asp:HiddenField ID="HFFiltroRE" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroST" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroFO" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="idSelected" runat="server" Value="" ClientIDMode="Static" />
    <script type="text/javascript">
                                            $(document).ready(function () {
                                                Ridimensiona();
                                            });
                                            $(window).resize(function () {
                                                Ridimensiona();
                                            });
                                            function Ridimensiona() {
                                                var altezzaRad = $(window).height() - 350;
                                                var larghezzaRad = $(window).width() - 47;
                                                $("#MasterPage_CPContenuto_dgvSegnalazioni").width(larghezzaRad);
                                                $("#MasterPage_CPContenuto_dgvSegnalazioni").height(altezzaRad);
                                                document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
                                                document.getElementById('AltezzaRadGrid').value = altezzaRad;
                                            }
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
