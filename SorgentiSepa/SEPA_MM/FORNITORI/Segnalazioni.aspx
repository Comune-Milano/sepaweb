<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false"
    CodeFile="Segnalazioni.aspx.vb" Inherits="FORNITORI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Ricerca Interventi"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <table cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <telerik:RadButton ID="btnExport" runat="server" Text="Esporta in Excel" ToolTip="Esport in Excel"
                    AutoPostBack="True" CausesValidation="True">
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton ID="btnAzzeraFiltri" runat="server" Text="Pulisci Filtri" ToolTip="Reimposta i filtri alla situazione iniziale"
                    AutoPostBack="True" CausesValidation="True">
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton ID="btnAvviaRicerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia la ricerca in base ai filtri impostati"
                    AutoPostBack="True" CausesValidation="True">
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton ID="btnVisualizza" runat="server" Text="Visualizza" ToolTip="Visualizza Segnalazione"
                    AutoPostBack="false" CausesValidation="True" OnClientClicking="VisualizzaIntervento">
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
                    AutoPostBack="False" CausesValidation="False" OnClientClicking="ClickUscita">
                </telerik:RadButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">

        function ClickUscita(sender, args) {
            location.href = 'Home.aspx';
        }

        function RowDblClick(sender, eventArgs) {
            var grid = sender;
            var MasterTable = grid.get_masterTableView();
            var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var cell = MasterTable.getCellByColumnUniqueName(row, "IDENTIFICATIVO");
            var IDS = cell.innerHTML;
            ApriModuloStandard('Intervento.aspx?D=' + IDS, 'Intervento_' + IDS);
        }

        function VisualizzaIntervento(sender, args) {
            if (document.getElementById('idSegnalazione').value == '0') {
                $.notify('Selezionare un intervento dalla lista!', 'warn');
            }
            else {

                ApriModuloStandard('Intervento.aspx?D=' + document.getElementById('idSegnalazione').value, 'Intervento_' + document.getElementById('idSegnalazione').value);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadPersistenceManager ID="RadPersistenceManager1" runat="server">
        <PersistenceSettings>
            <telerik:PersistenceSetting ControlID="dgvSegnalazioni" />
        </PersistenceSettings>
    </telerik:RadPersistenceManager>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvSegnalazioni">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAvviaRicerca">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAzzeraFiltri">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <div class="data-container">
        <telerik:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%">
            <Items>
                <telerik:RadPanelItem Text="Filtri" Width="100%">
                    <Items>
                        <telerik:RadPanelItem>
                            <ItemTemplate>
                                <table width="70%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Stato Intervento" Font-Names="arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <asp:CheckBoxList ID="CheckBoxListStato" runat="server" Font-Names="arial" Font-Size="8"
                                                RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Data Richiesta" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Da" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtRicDA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput7" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" OnBlur="CalendarDatePickerHide" />
                                                </DateInput>
                                                <Calendar ID="Calendar1" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="A" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtRicA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput1" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                </DateInput>
                                                <Calendar ID="Calendar2" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Fornitore" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadComboBox ID="cmbFornitori" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                Filter="Contains" AutoPostBack="False" Width="400px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="Gravità" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadDropDownList ID="cmbGravita" runat="server" AutoPostBack="False" Width="400px"
                                                Font-Names="arial" Font-Size="8">
                                                <Items>
                                                    <telerik:DropDownListItem Text="---" Value="-1" runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-white-128.png" Text="Bianco" Value="1"
                                                        runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-green-128.png" Text="Verde" Value="2"
                                                        runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-yellow-128.png" Text="Giallo" Value="3"
                                                        runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-red-128.png" Text="Rosso" Value="4"
                                                        runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-blue-128.png" Text="Blu" Value="0"
                                                        runat="server"></telerik:DropDownListItem>
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Data termine lavorazione" Font-Names="arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label12" runat="server" Text="Da" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtFineLavoriDA" runat="server" WrapperTableCaption=""
                                                MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput2" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" OnBlur="CalendarDatePickerHide" />
                                                </DateInput>
                                                <Calendar ID="Calendar3" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label11" runat="server" Text="A" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtFineLavoriA" runat="server" WrapperTableCaption=""
                                                MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput3" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                </DateInput>
                                                <Calendar ID="Calendar4" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="Data DPIL" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label14" runat="server" Text="Da" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtPGIDA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput4" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" OnBlur="CalendarDatePickerHide" />
                                                </DateInput>
                                                <Calendar ID="Calendar5" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label15" runat="server" Text="A" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtPGIA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput5" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                </DateInput>
                                                <Calendar ID="Calendar6" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="Data DPFL" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label18" runat="server" Text="Da" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtTDLDA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput6" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" OnBlur="CalendarDatePickerHide" />
                                                </DateInput>
                                                <Calendar ID="Calendar7" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label17" runat="server" Text="A" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtTDLA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput8" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                </DateInput>
                                                <Calendar ID="Calendar8" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Scostamento Data DPIL >= di N° Giorni: " Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtScostamentoDPIL" runat="server" Skin="Web20" NAME="txtScostamentoDPIL" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Scostamento Data DPFL >= di N° Giorni: " Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtScostamentoDPFL" runat="server" Skin="Web20" NAME="txtScostamentoDPFL"/>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                
            </Items>
            <ExpandAnimation Type="Linear" />
            <CollapseAnimation Type="Linear" />
        </telerik:RadPanelBar>
    </div>
    <div id="divOverContent" style="width: 99%; overflow: auto; visibility: visible;">
        <telerik:RadGrid ID="dgvSegnalazioni" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            PageSize="100" Culture="it-IT" AllowPaging="True" IsExporting="False" AllowFilteringByColumn="True"
            Width="300%">
            <ExportSettings OpenInNewWindow="true" IgnorePaging="true">
                <Pdf PageWidth="">
                </Pdf>
                <Excel FileExtension="xls" Format="Biff" />
            </ExportSettings>
            <MasterTableView Name="ElencoSegnalazioni" CommandItemDisplay="Top" HierarchyLoadMode="Client">
                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                    ShowRefreshButton="true" />
                <Columns>
                    <telerik:GridBoundColumn DataField="IDS" HeaderText="NUM.SEGNALAZIONE" FilterControlWidth="50px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ODL" HeaderText="ODL" FilterControlWidth="50px"
                        EmptyDataText="" Exportable="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ODL1" HeaderText="ODL" Visible="true" FilterControlWidth="0px"
                        ItemStyle-Width="0px" HeaderStyle-Width="0px" Exportable="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="DATA_RICHIESTA" HeaderText="DATA OdL" FilterControlWidth="100px"
                        DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" ShowFilterIcon="true"
                        CurrentFilterFunction="EqualTo">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" FilterControlWidth="50px">
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
                    <telerik:GridBoundColumn DataField="COD_COMPLESSO" HeaderText="COD.COMPLESSO" FilterControlWidth="50px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="COD_EDIFICIO" HeaderText="COD.EDIFICIO" FilterControlWidth="50px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" FilterControlWidth="100px">
                        <FilterTemplate>
                            <telerik:RadComboBox ID="RadComboBoxFiltroIndirizzo" Width="100%" AppendDataBoundItems="true"
                                runat="server" OnClientSelectedIndexChanged="FilterIndirizzoIndexChanged" HighlightTemplatedItems="true"
                                Filter="Contains" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                            <telerik:RadScriptBlock ID="RadScriptBlockIndirizzo" runat="server">
                                <script type="text/javascript">
                                    function FilterIndirizzoIndexChanged(sender, args) {
                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                        var filtro = args.get_item().get_value();
                                        document.getElementById('HFFiltroIndirizzo').value = filtro;
                                        if (filtro != 'Tutti') {
                                            tableView.filter("INDIRIZZO", filtro, "EqualTo");
                                        } else {
                                            tableView.filter("INDIRIZZO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                        };
                                    };
                                </script>
                            </telerik:RadScriptBlock>
                        </FilterTemplate>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BUILDING_MANAGER" HeaderText="BUILDING MANAGER" FilterControlWidth="50px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ST" HeaderText="SEDE TERRITORIALE" FilterControlWidth="50px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" FilterControlWidth="50px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="N_APPALTO" HeaderText="CONTRATTO" FilterControlWidth="50px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DESCRIZIONE_ANOMALIA" HeaderText="DESCRIZIONE" FilterControlWidth="90%">
                        <HeaderStyle width="500px" />
                        <ItemStyle width="500px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="DATA_INIZIO_INTERVENTO" HeaderText="DATA INIZIO LAVORI MM"
                        FilterControlWidth="100px" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                        ShowFilterIcon="true" CurrentFilterFunction="EqualTo">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="DATA_FINE_INTERVENTO" HeaderText="DATA TERMINE LAVORAZIONE MM"
                        FilterControlWidth="100px" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                        ShowFilterIcon="true" CurrentFilterFunction="EqualTo">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="DATAPGI" HeaderText="DATA DPIL" FilterControlWidth="100px"
                        DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" ShowFilterIcon="true"
                        CurrentFilterFunction="EqualTo">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="DATATDL" HeaderText="DATA DPFL" FilterControlWidth="100px"
                        DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" ShowFilterIcon="true"
                        CurrentFilterFunction="EqualTo">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="DATA_FINE_DITTA" HeaderText="DATA FINE INTERVENTO"
                        FilterControlWidth="100px" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                        ShowFilterIcon="true" CurrentFilterFunction="EqualTo">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="IRREGOLARITA" HeaderText="NON CONF." FilterControlWidth="50px">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="ALLEGATI" HeaderText="ALLEGATI" FilterControlWidth="50px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE"
                        Visible="false" EmptyDataText=" ">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" EmptyDataText=" " Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="IDENTIFICATIVO" HeaderText="IDENTIFICATIVO" Visible="true"
                        FilterControlWidth="0px" ItemStyle-Width="0px" HeaderStyle-Width="0px" EmptyDataText=" "
                        Exportable="false">
                        <HeaderStyle Width="0px"></HeaderStyle>
                        <ItemStyle Width="0px"></ItemStyle>
                    </telerik:GridBoundColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
            <ClientSettings AllowDragToGroup="True" EnableRowHoverStyle="true" AllowColumnsReorder="True"
                ReorderColumnsOnClient="True">
                <ClientEvents OnRowDblClick="RowDblClick" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                <Selecting AllowRowSelect="True" />
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" EnableRealTimeResize="true"
                    AllowResizeToFit="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="True" />
        </telerik:RadGrid>
    </div>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenField1" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivHeight" Value="100" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivWidth" Value="1" />
    <asp:HiddenField ID="HFFiltroStato" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroIndirizzo" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroSopr" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="idSegnalazione" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="isExporting" />
    <script type="text/javascript">
        validNavigation = false;
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 330;
            var larghezzaRad = $(window).width() - 27;
            //$("#MasterPage_CPContenuto_dgvSegnalazioni").width(larghezzaRad);
            $("#MasterPage_CPContenuto_dgvSegnalazioni").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }
        //        $(function () {
        //            $("#CPContenuto_txtDal").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        //            $("#CPContenuto_txtAl").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        //        });



        function OnClientDragStart(sender, args) {
            args.get_htmlElement().style.backgroundColor = "Red";
        }
        function OnClientDropped(sender, args) {
            args.get_sourceItem()._element.style.backgroundColor = "";
        }




    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
