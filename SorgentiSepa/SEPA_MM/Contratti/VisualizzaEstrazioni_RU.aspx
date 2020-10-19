<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VisualizzaEstrazioni_RU.aspx.vb"
    Inherits="Contratti_VisualizzaEstrazioni_RU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visualizza Estrazioni RU</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="LoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <table id="tbTitolo">
        <tr>
            <td style="width: 5px;">
                &nbsp;
            </td>
            <td>
                Elenco Estrazioni
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%">
                <asp:Panel runat="server" ID="PanelRadGrid">
                    <telerik:RadGrid ID="RadGridReport" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
                        Height="500px" IsExporting="False" AllowPaging="True" PageSize="50">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top" HierarchyLoadMode="ServerOnDemand">
                            <CommandItemSettings ShowExportToExcelButton="False" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                               <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TIPO_ESTRAZIONE" HeaderText="TIPO ESTRAZIONE"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INIZIO" HeaderText="INIZIO" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FINE" HeaderText="FINE" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ESITO" HeaderText="STATO" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <%--   <telerik:GridBoundColumn DataField="AVANZAMENTO" HeaderText="AVANZAMENTO" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="ERRORE" HeaderText="ERRORE" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NOMEFILE" HeaderText="NOME FILE" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
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
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    </form>
</body>
</html>
