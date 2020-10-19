<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportCompleto.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_ReportCompleto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/modalTelerik.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript">
        function tornaHome() {
            document.location.href = '../../Pagina_home_ncp.aspx';
        };
        function ApriDettaglio(id_voce, id_voce_Servizio, tipo, anno, anno2) {
            openModalInRadClose('RadWindow1', 'ReportCompletoDettaglio.aspx?idv=' + id_voce + '&idvs=' + id_voce_Servizio + '&tipo=' + tipo + '&ida=' + anno + '&ida2=' + anno2, 800, 500, 1);
        };
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadComboBoxCapitolo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadComboBoxVoce" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RadComboBoxServizio" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadComboBoxVoce">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadComboBoxServizio" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadComboBoxServizio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CheckBoxListFiltroAnno">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Web20"
        Transparency="100">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Buttons" />
    <div id="RestrictedZone" style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo" style="width: 100%">
                    Report - Situazione contabile - Completo
                    <asp:Label Text="" ID="ultimoAggiornamento" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="ButtonAggiorna" Text="Aggiorna" runat="server" />
                    <asp:Button ID="ButtonEsci" Text="Esci" runat="server" OnClientClick="tornaHome();return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset style="border: 1px solid #1c2466;">
                        <legend>Filtri ricerca</legend>
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                    Anno
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="CheckBoxListFiltroAnno" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnClick="nascondi=0;">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Capitolo
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxCapitolo" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="500px" OnClientSelectedIndexChanging="nascondiCaricamentoInCorso"
                                        Height="350px">
                                        <HeaderTemplate>
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td style="width: 100px">
                                                        Capitolo
                                                    </td>
                                                    <td style="width: 400px">
                                                        Descrizione
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td style="width: 100px">
                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                    </td>
                                                    <td style="width: 400px">
                                                        <%# DataBinder.Eval(Container, "Attributes['DESCRIZIONE']")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Voce
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxVoce" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="500px" OnClientSelectedIndexChanging="nascondiCaricamentoInCorso"
                                        Height="350px">
                                        <HeaderTemplate>
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td style="width: 100px">
                                                        Codice
                                                    </td>
                                                    <td style="width: 400px">
                                                        Descrizione
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td style="width: 100px">
                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                    </td>
                                                    <td style="width: 400px">
                                                        <%# DataBinder.Eval(Container, "Attributes['DESCRIZIONE']")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Servizio
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxServizio" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="500px" OnClientSelectedIndexChanging="nascondiCaricamentoInCorso"
                                        Height="350px">
                                        <HeaderTemplate>
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td style="width: 400px">
                                                        Servizio
                                                    </td>
                                                    <td style="width: 100px">
                                                        Codice
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td style="width: 400px">
                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                    </td>
                                                    <td style="width: 100px">
                                                        <%# DataBinder.Eval(Container, "Attributes['DESCRIZIONE']")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Data report al
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtDataAl" runat="server" WrapperTableCaption="" MinDate="01/01/1000"
                                        MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}" ShowPopupOnFocus="true">
                                        <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                        </DateInput>
                                        <Calendar ID="Calendar1" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today">
                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Data pagamento al
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtDataPagamento" runat="server" WrapperTableCaption="" MinDate="01/01/1000"
                                        MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}" ShowPopupOnFocus="true">
                                        <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                        </DateInput>
                                        <Calendar ID="Calendar2" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today">
                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
            PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
            AllowFilteringByColumn="True" EnableLinqExpressions="False" AllowSorting="True"
            PageSize="100" IsExporting="False" Width="99%">
            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                CommandItemDisplay="Top" AllowMultiColumnSorting="true">
                <SortExpressions>
                    <telerik:GridSortExpression FieldName="ANNO" SortOrder="Descending" />
                    <telerik:GridSortExpression FieldName="CODICE" SortOrder="Ascending" />
                    <telerik:GridSortExpression FieldName="VOCE" SortOrder="Ascending" />
                    <telerik:GridSortExpression FieldName="SERVIZIO" SortOrder="Ascending" />
                </SortExpressions>
                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                    ShowRefreshButton="true" />
                <CommandItemTemplate>
                    <div style="display: inline-block; width: 100%;">
                        <div style="float: right; padding: 4px;">
                            <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                OnClientClick="nascondi=0;" CssClass="rgRefresh" />
                            <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                        </div>
                    </div>
                </CommandItemTemplate>
                <HeaderStyle Wrap="false" Width="150px" />
                <ItemStyle Width="150px" />
                <Columns>
                    <telerik:GridBoundColumn DataField="ID_VOCE" HeaderText="ID_VOCE" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CODICE" HeaderText="CODICE" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VOCE" HeaderText="VOCE" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                        ItemStyle-Wrap="true">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                        ItemStyle-Wrap="true">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                        <FilterTemplate>
                            <telerik:RadComboBox ID="RadComboBoxFiltroAnno" Width="100%" AppendDataBoundItems="true"
                                runat="server" OnClientSelectedIndexChanged="FilterAnnoIndexChanged" DropDownAutoWidth="Enabled"
                                ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                            <telerik:RadScriptBlock ID="RadScriptBlockAnno" runat="server">
                                <script type="text/javascript">
                                    function FilterAnnoIndexChanged(sender, args) {
                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                        var filtro = args.get_item().get_value();
                                        document.getElementById('HFFiltroAnno').value = filtro;
                                        if (filtro != 'Tutti') {
                                            tableView.filter("ANNO", filtro, "EqualTo");
                                        } else {
                                            tableView.filter("ANNO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                        };
                                    };
                                </script>
                            </telerik:RadScriptBlock>
                        </FilterTemplate>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BUDGET_ASSESTATO" HeaderText="BUDGET ASSESTATO"
                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataFormatString="{0:C2}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PRENOTATO" HeaderText="PRENOTATO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataFormatString="{0:C2}"
                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="PRENOTATO_IMPONIBILE" HeaderText="IMPONIBILE PRENOTATO"
                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataFormatString="{0:C2}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PRENOTATO_IVA" HeaderText="IVA PRENOTATO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataFormatString="{0:C2}"
                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn DataField="CONSUNTIVATO" HeaderText="CONSUNTIVATO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataFormatString="{0:C2}"
                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CERTIFICATO" HeaderText="CERTIFICATO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataFormatString="{0:C2}"
                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CERTIFICATO_IMPONIBILE" HeaderText="CERTIFICATO IMPONIBILE"
                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataFormatString="{0:C2}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CERTIFICATO_IVA" HeaderText="CERTIFICATO IVA"
                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataFormatString="{0:C2}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FATTURATO" HeaderText="FATTURATO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataFormatString="{0:C2}"
                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PAGATO" HeaderText="PAGATO" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataFormatString="{0:C2}"
                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PAGATO_IVA" HeaderText="IVA PAGATA" FilterControlWidth="85%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataFormatString="{0:C2}"
                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PAGATO_RITENUTE" HeaderText="RITENUTA PAGATA"
                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataFormatString="{0:C2}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CONSUNTIVATO_RIT" HeaderText="RITENUTA CONSUNTIVATA"
                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataFormatString="{0:C2}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CONSUNTIVATO_RIT_IMPONIBILE" HeaderText="IMPONIBILE RITENUTA"
                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataFormatString="{0:C2}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CONSUNTIVATO_RIT_IVA" HeaderText="IVA RITENUTA"
                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataFormatString="{0:C2}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DISPONIBILITA_RESIDUA" HeaderText="DISPONIBILITA' RESIDUA"
                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        DataFormatString="{0:C2}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DATA_AL" HeaderText="DATA_AL" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DATA_PAGAMENTO" HeaderText="DATA_PAGAMENTO" Visible="false">
                    </telerik:GridBoundColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
            </MasterTableView>
            <HeaderStyle Width="150px" />
            <HeaderStyle Width="150px" />
            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
            <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                <Excel FileExtension="xls" Format="Xlsx" />
            </ExportSettings>
            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                ClientEvents-OnCommand="onCommand">
                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="5">
                </Scrolling>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
    <asp:HiddenField ID="HFGriglia" runat="server" />
    <asp:HiddenField ID="HFFiltroAnno" runat="server" />
    <asp:HiddenField ID="HFAltezzaSottratta" runat="server" Value="300" />
    <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" Behavior="Pin, Move, Resize, Maximize" Width="900px"
        Height="700px" ShowContentDuringLoad="false">
    </telerik:RadWindow>
    </form>
</body>
<script language="javascript" type="text/javascript">
    window.onresize = setDimensioni;
    Sys.Application.add_load(setDimensioni);
</script>
</html>
