<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestionePU.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_GestionePU" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Contratti - Gestione PU</title>
    <script type="text/javascript">
        var Selezionato;

    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="dgvPrezziUnitari">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvPrezziUnitari" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbEsercizio">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvPrezziUnitari" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">Contratti - Gestione prezzi unitari
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server">Elenco prezzi</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEsercizio" runat="server" AppendDataBoundItems="true" OnClientSelectedIndexChanging="function(sender,args){nascondi=0;}"
                                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="dgvPrezziUnitari" runat="server" GroupPanelPosition="Top"
                                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                    PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                                    EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                                    PageSize="100">
                                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                        DataKeyNames="ID" CommandItemDisplay="Top">
                                        <SortExpressions>
                                            <telerik:GridSortExpression FieldName="ORDINE" SortOrder="Ascending" />
                                        </SortExpressions>
                                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                            ShowRefreshButton="true" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PREZZO_UNITARIO" HeaderText="PREZZO UNITARIO" AutoPostBackOnFilter="true" SortExpression="ORDINE" HeaderStyle-Width="15%"
                                                FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" AutoPostBackOnFilter="true" HeaderStyle-Width="60%"
                                                FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UM" HeaderText="U.M" AutoPostBackOnFilter="true" HeaderStyle-Width="10%"
                                                FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="IMPORTO" HeaderText="IMPORTO A BASE D'ASTA" AutoPostBackOnFilter="true" HeaderStyle-Width="15%"
                                                FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtPrezzoUnitario" runat="server" NumberFormat-DecimalDigits="2" DataType="System.Decimal"
                                                        Width="80px" MinValue="0" Style="text-align: right" Text='<%# par.VirgoleInPunti(DataBinder.Eval(Container, "DataItem.IMPORTO")) %>'>
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CommandItemTemplate>
                                            <div style="display: inline-block; width: 100%;">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div style="float: right; padding: 4px;">
                                                                <asp:Button ID="ButtonRefresh" runat="server" OnClick="RefreshPrezziUnitari_Click" CommandName="Refresh" OnClientClick="nascondi=0;"
                                                                    CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcel" Text="text" runat="server"
                                                                        OnClick="EsportaPrezziUnitari_Click" CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </CommandItemTemplate>
                                    </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                        <Excel FileExtension="xls" Format="Xlsx" />
                                    </ExportSettings>
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                                        <Scrolling AllowScroll="true" UseStaticHeaders="True" FrozenColumnsCount="1" />
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
        <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Width="400"
            Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3600"
            Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
        </telerik:RadNotification>
        <asp:HiddenField ID="isExporting" runat="server" Value="0" />
        <asp:HiddenField ID="idLotto" runat="server" />
        <asp:HiddenField ID="HFGriglia" runat="server" />

    </form>
    <script type="text/javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>

</html>
