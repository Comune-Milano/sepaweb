<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiEstrazioneCDP.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiEstrazioneCDP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Estrazione Pagamenti</title>

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
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">

        <tr>
            <td class="TitoloModulo">
                Estrazione Pagamenti
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" OnClientClicking="function(sender, args){self.close();}"
                                ToolTip="Esci" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
            </td>
        </tr>
    </table>
 
    <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%">
       <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                        PageSize="100" IsExporting="False">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <div style="float: right; padding: 4px;">
                                        <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                            CssClass="rgRefresh" />
                                        <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                            CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                    </div>
                                </div>
                            </CommandItemTemplate>
                <Columns>
                    <telerik:GridBoundColumn DataField="ADP" HeaderText="ADP">
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridNumericColumn DataField="IMPORTO" HeaderText="IMPORTO" DecimalDigits="2"
                        DataType="System.Decimal" DataFormatString="{0:###,##0.00}" UniqueName="IMPORTO">
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </telerik:GridNumericColumn>
                    <telerik:GridBoundColumn DataField="TIPO_PAGAMENTO" HeaderText="TIPO PAGAMENTO">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
            <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                <Excel FileExtension="xls" Format="Xlsx" />
            </ExportSettings>
            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                ClientEvents-OnCommand="onCommand">
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                <Selecting AllowRowSelect="True" />
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                    AllowResizeToFit="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </asp:Panel>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="AltezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    </form>
    <script src="../../../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
  
</body>
<script language="javascript" type="text/javascript">
    window.onresize = setDimensioni;
    Sys.Application.add_load(setDimensioni);
    window.focus();
    self.focus();
</script>
</html>
