<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoODLSAL.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_ElencoODLSAL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>Elenco ODL su SAL</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <script type="text/javascript">
            window.onresize = setDimensioni;
            Sys.Application.add_load(setDimensioni);

        </script>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="1">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnExport">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="height: 7px;"></td>
            </tr>
            <tr>
                <td class="TitoloModulo">Elenco ODL su SAL
                
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnExport" runat="server" Text="Export in Excel" ToolTip="Export in Excel" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnNuovaRicerca" runat="server" Text="Nuova ricerca" ToolTip="Nuova ricerca" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 15px">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="PanelRadGrid">
                        <telerik:RadGrid ID="RadGrid1" runat="server" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                            EnableLinqExpressions="False" Culture="it-IT" RegisterWithScriptManager="False"
                            PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="True" Width="96%" AllowSorting="True"
                            Height="440px" IsExporting="False" AllowPaging="True" PageSize="100">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                CommandItemDisplay="Top" Width="250%" AllowMultiColumnSorting="true">
                                <CommandItemSettings ShowExportToExcelButton="false" ShowExportToWordButton="false"
                                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                    ShowRefreshButton="true" />
                                                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" Visible="false" Exportable="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PROGRESSIVO" HeaderText="PROGRESSIVO" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" Exportable="true">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" AutoPostBackOnFilter="true" Exportable="TRUE"
                                        CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" AutoPostBackOnFilter="true" SortExpression="REP_ORD" Exportable="true"
                                        CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_INIZIO_ORDINE" HeaderText="DATA INIZIO ORDINE" PickerType="DatePicker"
                                        Exportable="true" EnableRangeFiltering="true" DataFormatString="{0:dd/MM/yyyy}"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Between">
                                        <HeaderStyle Width="330px"></HeaderStyle>
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_INIZIO_INTERVENTO" HeaderText="DATA INIZIO INTERVENTO" PickerType="DatePicker"
                                        Exportable="true" EnableRangeFiltering="true" DataFormatString="{0:dd/MM/yyyy}"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Between">
                                        <HeaderStyle Width="330px"></HeaderStyle>
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_FINE_INTERVENTO" HeaderText="DATA FINE INTERVENTO" PickerType="DatePicker"
                                        Exportable="true" EnableRangeFiltering="true" DataFormatString="{0:dd/MM/yyyy}"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Between">
                                        <HeaderStyle Width="330px"></HeaderStyle>
                                    </telerik:GridDateTimeColumn>

                                    <telerik:GridHyperLinkColumn DataTextField="NUMERO_SAL" DataNavigateUrlFields="APRI_SAL"
                                        HeaderText="NUMERO SAL" SortExpression="NUMERO_SAL" FilterControlWidth="90%"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="5%" />

                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn DataField="CODICE_PROGETTO_VISION" HeaderText="COD. PROGETTO VISION" AutoPostBackOnFilter="true" Exportable="true"
                                        CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUMERO_SAL_VISION" HeaderText="NUM. SAL VISION" AutoPostBackOnFilter="true" Exportable="true"
                                        CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUMERO_SAL" HeaderText="NUMERO SAL" Visible="false" Exportable="false"
                                        UniqueName="NRSAL">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_SAL" HeaderText="DATA SAL" PickerType="DatePicker"
                                        Exportable="true" EnableRangeFiltering="true" DataFormatString="{0:dd/MM/yyyy}"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Between">
                                        <HeaderStyle Width="330px"></HeaderStyle>
                                    </telerik:GridDateTimeColumn>


                                    <telerik:GridBoundColumn DataField="STATO_SAL" HeaderText="STATO SAL" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="true" HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <FilterTemplate>
                                            <telerik:RadComboBox ID="RadComboBoxStatoSal" Width="100%" AppendDataBoundItems="true"
                                                runat="server" OnClientSelectedIndexChanged="FilterStatoSalIndexChanged" DropDownAutoWidth="Enabled"
                                                ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                LoadingMessage="Caricamento...">
                                            </telerik:RadComboBox>
                                            <telerik:RadScriptBlock ID="RadScriptBlockStatoSal" runat="server">
                                                <script type="text/javascript">
                                                    function FilterStatoSalIndexChanged(sender, args) {
                                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                        var filtro = args.get_item().get_value();
                                                        document.getElementById('HFFiltroEventoStatoSal').value = filtro;
                                                        if (filtro != 'Tutti') {
                                                            tableView.filter("STATO_SAL", filtro, "EqualTo");
                                                        } else {
                                                            tableView.filter("STATO_SAL", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                        };
                                                    };
                                                </script>
                                            </telerik:RadScriptBlock>
                                        </FilterTemplate>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO_LIQUIDAZIONE" HeaderText="STATO LIQUIDAZIONE" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="true" HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <FilterTemplate>
                                            <telerik:RadComboBox ID="RadComboBoxFiltroLiquidazione" Width="100%" AppendDataBoundItems="true"
                                                runat="server" OnClientSelectedIndexChanged="FilterLiquidazioneIndexChanged" DropDownAutoWidth="Enabled"
                                                ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                LoadingMessage="Caricamento...">
                                            </telerik:RadComboBox>
                                            <telerik:RadScriptBlock ID="RadScriptBlockLiquidazione" runat="server">
                                                <script type="text/javascript">
                                                    function FilterLiquidazioneIndexChanged(sender, args) {
                                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                        var filtro = args.get_item().get_value();
                                                        document.getElementById('HFFiltroEventoLiquidazione').value = filtro;
                                                        if (filtro != 'Tutti') {
                                                            tableView.filter("STATO_LIQUIDAZIONE", filtro, "EqualTo");
                                                        } else {
                                                            tableView.filter("STATO_LIQUIDAZIONE", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                        };
                                                    };
                                                </script>
                                            </telerik:RadScriptBlock>
                                        </FilterTemplate>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn DataTextField="ODL" DataNavigateUrlFields="APRI_ODL"
                                        HeaderText="ODL" SortExpression="ODL" FilterControlWidth="90%"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="5%" />
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="true" HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <FilterTemplate>
                                            <telerik:RadComboBox ID="RadComboBoxStato" Width="100%" AppendDataBoundItems="true"
                                                runat="server" OnClientSelectedIndexChanged="FilterStatoIndexChanged" DropDownAutoWidth="Enabled"
                                                ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                LoadingMessage="Caricamento...">
                                            </telerik:RadComboBox>
                                            <telerik:RadScriptBlock ID="RadScriptBlockStato" runat="server">
                                                <script type="text/javascript">
                                                    function FilterStatoIndexChanged(sender, args) {
                                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                        var filtro = args.get_item().get_value();
                                                        document.getElementById('HFFiltroEventoStato').value = filtro;
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
                                    <telerik:GridDateTimeColumn DataField="DATA_ORDINE" HeaderText="DATA ORDINE" DataFormatString="{0:dd/MM/yyyy}" Exportable="true"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_CONSUNTIVO" HeaderText="DATA CONSUNTIVO" Exportable="true"
                                        DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="UBICAZIONE" HeaderText="UBICAZIONE" AutoPostBackOnFilter="true" Exportable="true"
                                        CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_CONSUNTIVO" HeaderText="IMP.CONSUNTIVO" Exportable="true"
                                        DataFormatString="{0:C2}">
                                        <HeaderStyle Width="4%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_LORDO_ONERI" HeaderText="TOT.LORDO COMPRESI ONERI" Exportable="true"
                                        DataFormatString="{0:C2}">
                                        <HeaderStyle Width="4%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_LORDO" HeaderText="TOT.LORDO ESCLUSO ONERI" Exportable="true"
                                        DataFormatString="{0:C2}">
                                        <HeaderStyle Width="4%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMP_NETTO_DI_ONERI" HeaderText="TOT.NETTO DI ONERI" Exportable="true"
                                        DataFormatString="{0:C2}">
                                        <HeaderStyle Width="4%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IVA" HeaderText="IVA" DataFormatString="{0:C2}" Exportable="true">
                                        <HeaderStyle Width="4%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMP_NETTO_DI_ONERI_E_IVA" HeaderText="TOT.NETTO DI ONERI E IVA" Exportable="true"
                                        DataFormatString="{0:C2}">
                                        <HeaderStyle Width="4%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE" Exportable="true"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Width="9%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BUILDING_MANAGER" HeaderText="BUILDING MANAGER" Exportable="true"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Width="9%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MAIL_PER_APPUNTAMENTO" HeaderText="MAIL PER APPUNTAMENTO" Exportable="true"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Width="9%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PATRIMONIO" HeaderText="PATRIMONIO" Exportable="true"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Width="9%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <SortExpressions>
                                    <telerik:GridSortExpression FieldName="REP_ORD" SortOrder="Ascending" />
                                    <telerik:GridSortExpression FieldName="PROGRESSIVO" SortOrder="Ascending" />
                                </SortExpressions>
                            </MasterTableView>
                            <HeaderStyle HorizontalAlign="Center" />
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false" />
                            <ExportSettings OpenInNewWindow="true" IgnorePaging="true" ExportOnlyData="true"
                                HideStructureColumns="true">
                                <Excel FileExtension="xlsx" Format="Xlsx" />
                            </ExportSettings>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                <Selecting AllowRowSelect="True" />
                                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                    AllowResizeToFit="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                        <asp:HiddenField ID="HFFiltroEventoLiquidazione" runat="server" />
                        <asp:HiddenField ID="HFFiltroEventoStatoSal" runat="server" />
                        <asp:HiddenField ID="HFFiltroEventoStato" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <div style="visibility: hidden; height: 10px">
                <asp:DataGrid runat="server" ID="DataGridODL" CellPadding="1" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="#000000" GridLines="None" CellSpacing="1" ShowFooter="false"
                    AutoGenerateColumns="false" Width="99%" UseAccessibleHeader="True">
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PROGRESSIVO" HeaderText="PROGRESSIVO">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE" DataFormatString="{0:@}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" DataFormatString="{0:@}">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CODICE_PROGETTO_VISION" HeaderText="COD. PROGETTO VISION" DataFormatString="{0:@}">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUMERO_SAL_VISION" HeaderText="NUM. SAL VISION" DataFormatString="{0:@}">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUMERO_SAL" HeaderText="NUMERO SAL" Visible="false">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO" DataFormatString="{0:@}">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_SAL" HeaderText="DATA SAL" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="330px"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ODL" HeaderText="ODL" DataFormatString="{0:@}">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_ORDINE" HeaderText="DATA ORDINE" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_CONSUNTIVO" HeaderText="DATA CONSUNTIVO" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UBICAZIONE" HeaderText="UBICAZIONE" DataFormatString="{0:@}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_CONSUNTIVO" HeaderText="IMP. CONSUNTIVO" DataFormatString="{0:C2}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TOT_LORDO_ESCLUSO_ONERI" HeaderText="TOT. LORDO ESCLUSO ONERI"
                            DataFormatString="{0:C2}">
                            <HeaderStyle Width="4%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOT_NETTO_DI_ONERI" HeaderText="TOT. NETTO DI ONERI"
                            DataFormatString="{0:C2}">
                            <HeaderStyle Width="4%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IVA" HeaderText="IVA" DataFormatString="{0:C2}">
                            <HeaderStyle Width="4%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOT_NETTO_DI_ONERI_E_IVA" HeaderText="TOT. NETTO DI ONERI E IVA"
                            DataFormatString="{0:C2}">
                            <HeaderStyle Width="4%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE" DataFormatString="{0:@}">
                            <HeaderStyle Width="9%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BUILDING_MANAGER" HeaderText="BUILDING MANAGER" DataFormatString="{0:@}">
                            <HeaderStyle Width="9%"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MAIL_PER_APPUNTAMENTO" HeaderText="MAIL PER APPUNTAMENTO"
                            DataFormatString="{0:@}"></asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" />
                    <ItemStyle ForeColor="#000000" />
                    <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                </asp:DataGrid>
            </div>
        </table>
    </form>
</body>
</html>
