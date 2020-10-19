<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptAzLegali.aspx.vb" Inherits="Contratti_RptAzLegali" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Rpt Decreto Decadenza</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <script type="text/javascript">
            window.onresize = ResizeGrid;
            Sys.Application.add_load(ResizeGrid);
            function ResizeGrid() {
                var griglie = document.getElementById('HFGriglia').value;
                var altezzaPagina = myHeight = window.innerHeight;

                if (griglie != '') {
                    var griglia = griglie.split(",");

                    for (i = 0; i < griglia.length; i++) {
                        document.getElementById(griglia[i]).style.height = altezzaPagina - 100 + 'px';
                    }

                }
            };


            function requestStart(sender, args) {
                //            if (args.get_eventTarget().indexOf("ExportToExcel") >= 0) {
                //                args.set_enableAjax(false);
                //            }
            };
        </script>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
        </telerik:RadAjaxLoadingPanel>
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ClientEvents OnRequestStart="requestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="PanelRadGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridAzLegali" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <table>
            <tr>
                <td>&nbsp</td>
                <td style="text-align: center; width: 100%; font-size: 10pt; font-family: Arial; color: #416094; font-weight: bold;">
                    <asp:Label ID="lblTitolo" runat="server" Text="Elenco Contratti Decreto Decadenza" Font-Size="20px"></asp:Label>
                </td>
                <td>&nbsp</td>
            </tr>
        </table>
        <table>
            <tr>
                <td>&nbsp
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel runat="server" ID="PanelRadGrid">
                        <telerik:RadGrid ID="RadGridAzLegali" runat="server" GroupPanelPosition="Top" resolvedrendermode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
                            AllowPaging="True" isexporting="True" PageSize="100">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                    ShowRefreshButton="false" />
                                <CommandItemTemplate>
                                    <div style="display: inline-block; width: 100%;">
                                        <div style="float: right; padding: 4px;">
                                            <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh" Visible="false"
                                                CssClass="rgRefresh" />
                                            <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                                CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                        </div>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID_CONTRATTO" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_RU" HeaderText="TIPO CONTRATTO" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DEST_D_USO" HeaderText="DEST. D'USO" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_DECORRENZA" HeaderText="DATA DECORR." CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_CONSEGNA" HeaderText="DATA CONSEGNA" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridDateTimeColumn>

                                    <telerik:GridDateTimeColumn DataField="DATA_DISDETTA_LOCATARIO" HeaderText="DATA DISDETTA" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_SCADENZA" HeaderText="DATA SCADENZA" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_SCADENZA_RINNOVO" HeaderText="DATA RINNOVO" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="DURATA_ANNI" HeaderText="DURATA" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DURATA_RINNOVO" HeaderText="DURATA RINNOVO" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DELIBERA" HeaderText="PROVVED. ASSEGN." CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_DELIBERA" HeaderText="DATA PROVVED." CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="MOROSITA_PREGR_DATA_DECORR" HeaderText="DATA DECORR. DECRETO"
                                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="MOROSITA_PREGR_NUM_ID" HeaderText="NUM. IDENT. DECRETO"
                                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <ClientSettings EnableRowHoverStyle="True" AllowColumnsReorder="False" ReorderColumnsOnClient="True">
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                <Selecting AllowRowSelect="True" />
                                <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="False" EnableRealTimeResize="False"
                                    AllowResizeToFit="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
