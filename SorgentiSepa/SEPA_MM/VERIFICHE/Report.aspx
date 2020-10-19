<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Report.aspx.vb" Inherits="VERIFICHE_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Select</title>
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <style type="text/css">
        div.RadGrid_WebBlue .rgCommandTable td
        {
            text-align: left;
            color: transparent;
            padding-left: 0;
        }
        div.RadGrid_WebBlue .rgCommandTable td:first-child
        {
            width: 0;
            padding: 0;
        }
        .RadGrid_WebBlue input.rgExpXLS
        {
            margin-left: -27px;
        }
        </style>
    <script type="text/javascript" language="javascript">
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 120) {
                document.getElementById('Button2').click();
                //                e.preventDefault();
            }
        }

        function $onkeydown() {
            if (event.keyCode == 120) {
                //                event.keyCode = '9';
                document.getElementById('Button2').click();

            }
        }
    </script>
</head>
<body>
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" defaultbutton="Button1" defaultfocus="TextBox1">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>    
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="Timer1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridReport" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

            <telerik:RadAjaxLoadingPanel ID="LoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="Buttons"
        Skin="Web20"></telerik:RadFormDecorator>
    <div>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="CONFERMA" />
        &nbsp;<br />
    </div>
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%">
                <asp:Panel runat="server" ID="PanelRadGrid">
                    <telerik:RadGrid ID="RadGridReport" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="100%" AllowSorting="True" Height="500px"
                        IsExporting="False" AllowPaging="True">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top" HierarchyLoadMode="ServerOnDemand">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                            
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
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
                                <telerik:GridBoundColumn DataField="QUERY" HeaderText="QUERY" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ESITO" HeaderText="ESITO" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AVANZAMENTO" HeaderText="AVANZAMENTO" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ERRORE" HeaderText="ERRORE" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NOMEFILE" HeaderText="NOMEFILE" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                           </Columns>
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings OpenInNewWindow="true" IgnorePaging="true">
                            <Pdf PageWidth="">
                            </Pdf>
                            <Excel FileExtension="xls" Format="Biff" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
            </asp:Timer>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    </form>
</body>
</html>
