<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CruscottoElabCauzT.aspx.vb"
    Inherits="Contratti_CruscottoElabCauzT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Totali Interessi Depositi Cauzionali</title>
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 200;
            var larghezzaRad = $(window).width() - 27;
            $("#dgvDocumenti").width(larghezzaRad);
            $("#dgvDocumenti").height(altezzaRad);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvDocumenti">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvDocumenti" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <div>
        <table width="97%">
            <tr bgcolor="#CCCCCC">
                <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt;
                    font-weight: bold; text-align: center;">
                    <asp:Label ID="Label1" runat="server" Text="INTERESSI SU DEP. CAUZIONALE: TOTALI PER TIPOLOGIA CONTRATTO" ForeColor="Maroon"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt;
                    font-weight: bold; text-align: center;">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt;
                    font-weight: bold; text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <telerik:RadGrid ID="dgvDocumenti" ShowStatusBar="True" runat="server" PageSize="200"
                        AllowPaging="True" AllowSorting="True" ShowGroupPanel="True" AutoGenerateColumns="False"
                        Culture="it-IT" IsExporting="False" AllowFilteringByColumn="True" Width="99%">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false">
                        </GroupingSettings>
                        <ExportSettings FileName="ExportDettagli_" IgnorePaging="True" OpenInNewWindow="True"
                            ExportOnlyData="True" HideStructureColumns="True">
                            <Pdf PageWidth="">
                            </Pdf>
                            <Csv ColumnDelimiter="Semicolon" EncloseDataWithQuotes="False" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True"></Selecting>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                                ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
                        </ClientSettings>
                        <PagerStyle Mode="NumericPages" AlwaysVisible="True"></PagerStyle>
                        <MasterTableView EnableHierarchyExpandAll="true" DataKeyNames="COD" AllowMultiColumnSorting="True"
                            CommandItemDisplay="Top">
                            <DetailTables>
                                <telerik:GridTableView EnableHierarchyExpandAll="true" DataKeyNames="ANNO,IMPORTO"
                                    Width="100%" runat="server" Name="Dettagli" AllowFilteringByColumn="False" 
                                    AllowPaging="False">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ANNO" 
                                            FilterControlAltText="Filter ANNO column" HeaderText="ANNO" 
                                            UniqueName="ANNO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IMPORTO" 
                                            FilterControlAltText="Filter IMPORTO column" HeaderText="IMPORTO" 
                                            UniqueName="IMPORTO">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                </telerik:GridTableView>
                            </DetailTables>
                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToExcelButton="True" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="COD" 
                                    FilterCheckListEnableLoadOnDemand="True" 
                                    FilterControlAltText="Filter COD column" HeaderText="COD. TIPOLOGIA" 
                                    UniqueName="COD">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE_TIPO" 
                                    FilterControlAltText="Filter DESCRIZIONE_TIPO column" 
                                    HeaderText="DESCRIZIONE TIPOLOGIA" UniqueName="DESCRIZIONE_TIPO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_TOTALE" 
                                    HeaderText="TOTALE INTERESSI" 
                                    FilterControlAltText="Filter IMPORTO_TOTALE column" UniqueName="IMPORTO_TOTALE">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <PagerStyle AlwaysVisible="True" />
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        
        
    </script>
</body>
</html>
