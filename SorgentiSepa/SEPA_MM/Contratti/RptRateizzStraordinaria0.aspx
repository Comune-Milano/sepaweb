<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptRateizzStraordinaria0.aspx.vb"
    Inherits="Contratti_RptRateizzStraordinaria0" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Rpt rateizzazione straordinaria</title>
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
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table>
        <tr>
            <td>
                &nbsp
            </td>
            <td style="text-align: center; width: 100%; font-size: 10pt; font-family: Arial;
                color: #416094; font-weight: bold;">
                <asp:Label ID="lblTitolo" runat="server" Text="Rpt Rateizz. Straordinaria" Font-Size="20px"></asp:Label>
            </td>
            <td>
                &nbsp
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel runat="server" ID="PanelRadGrid">
                    <telerik:RadGrid ID="RadGrid1" runat="server" GroupPanelPosition="Top" resolvedrendermode="Classic"
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
                                        <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                            CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <%--<telerik:GridBoundColumn DataField="ID" HeaderText="ID_CONTRATTO" Visible="False">
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PG_DOMANDA" HeaderText="PG DOM." CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STATO_DOMANDA" HeaderText="STATO DOMANDA" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PG_DICHIARAZIONE" HeaderText="PG DICH." CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STATO_DICHIARAZIONE" HeaderText="STATO DICH." CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NOTE" HeaderText="NOTE" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="DATA_AUTORIZZAZIONE" HeaderText="DATA AUTORIZZAZIONE"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="AUTORIZZATA" HeaderText="AUTORIZZATA" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="DATA_PRESENTAZIONE" HeaderText="DATA PRESENTAZIONE"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataField="DATA_EVENTO" HeaderText="DATA EVENTO" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridDateTimeColumn>
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
