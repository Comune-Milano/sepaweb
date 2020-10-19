<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportCompletoDettaglio.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_ReportCompletoDettaglio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script type="text/javascript">
        function tornaHome() {
            document.location.href = '../../Pagina_home_ncp.aspx';
        };
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Buttons" />
    <table style="width: 100%">
        <tr>
            <td class="TitoloModulo">
                Report - Situazione contabile - Completo - Dettaglio
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button Text="Esci" runat="server" OnClientClick="closeWin();return false;" />
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                                AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                PageSize="100" IsExporting="False" ShowFooter="true">
                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                    CommandItemDisplay="Top" AllowMultiColumnSorting="true" Width="100%">
                                    <SortExpressions>
                                        <telerik:GridSortExpression FieldName="ID_VOCE_PF" SortOrder="Descending" />
                                        <telerik:GridSortExpression FieldName="ID_VOCE_PF_IMPORTO" SortOrder="Ascending" />
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
                                    <ItemStyle Width="15%" Wrap="true" />
                                    <HeaderStyle Width="15%" Wrap="true" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_VOCE_PF" HeaderText="ID_VOCE_PF" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_VOCE_PF_IMPORTO" HeaderText="ID_VOCE_PF_IMPORTO"
                                            FilterControlWidth="85%" AutoPostBackOnFilter="true" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="VOCE" HeaderText="VOCE" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IMPORTO" HeaderText="IMPORTO" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataFormatString="{0:C2}"
                                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                            Aggregate="Sum">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ODL" HeaderText="ODL" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SAL" HeaderText="SAL" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CDP" HeaderText="CDP" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOTE" HeaderText="NOTE" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="STATO_ATTUALE" HeaderText="STATO ATTUALE" FilterControlWidth="85%"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                    <Excel FileExtension="xls" Format="Xlsx" />
                                </ExportSettings>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                                    ClientEvents-OnCommand="onCommand">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    <Selecting AllowRowSelect="True" />
                                    <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                        AllowResizeToFit="false" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    <asp:HiddenField ID="HFAltezzaSottratta" runat="server" Value="150" />
    </form>
</body>
<script language="javascript" type="text/javascript">
    window.onresize = setDimensioni;
    Sys.Application.add_load(setDimensioni);
</script>
</html>
