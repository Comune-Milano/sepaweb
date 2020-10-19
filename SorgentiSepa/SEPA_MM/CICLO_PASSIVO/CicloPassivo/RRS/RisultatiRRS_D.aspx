<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiRRS_D.aspx.vb"
    Inherits="RRS_RisultatiRRS_D" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>RISULTATI RICERCA</title>
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
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <div style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td class="TitoloModulo">Ordini - Gestione non patrimoniale - Ricerca - Diretta
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnVisualizza" runat="server" Text="Visualizza" ToolTip="Visualizza" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnRicerca" runat="server" Text="Nuova ricerca" ToolTip="Nuova Ricerca" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnHome" runat="server" Text="Esci" ToolTip="Home" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%">
                            <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                                AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                PageSize="100" IsExporting="False">
                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                    CommandItemDisplay="Top" Width="150%">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                        ShowRefreshButton="true" />
                                    <CommandItemTemplate>
                                        <div style="display: inline-block; width: 100%;">
                                            <div style="float: right; padding: 4px;">
                                                <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                                    CssClass="rgRefresh" OnClientClick="nascondi=0;" />
                                                <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                                    CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                            </div>
                                        </div>
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID_MANUTENZIONE" HeaderText="ID_MANUTENZIONE"
                                            Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPER." ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="8%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:@}" FilterControlWidth="85%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ODL_ANNO" HeaderText="ODL/ANNO" ItemStyle-HorizontalAlign="Right"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                                            FilterControlWidth="85%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="DATA_INIZIO_ORDINE" HeaderText="DATA" DataFormatString="{0:dd/MM/yyyy}"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="UBICAZIONE" HeaderText="UBICAZIONE" ItemStyle-HorizontalAlign="Left"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                                            FilterControlWidth="85%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IMPORTO_PRE" HeaderText="IMPORTO PREVENTIVO"
                                            HeaderStyle-Width="5%" AllowFiltering="false" ItemStyle-HorizontalAlign="Right">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IMPORTO_CON" HeaderText="IMPORTO CONSUNTIVO"
                                            HeaderStyle-Width="5%" AllowFiltering="false" ItemStyle-HorizontalAlign="Right">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="VOCE" HeaderText="VOCE P.F." ItemStyle-HorizontalAlign="Left"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                                            FilterControlWidth="85%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" ItemStyle-HorizontalAlign="Left"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                                            FilterControlWidth="85%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="7%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:@}" FilterControlWidth="85%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PROGR" HeaderText="ODL_NUM" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_ODL" HeaderText="DATA_ODL" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                                            FilterControlWidth="85%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AUTORIZZAZIONE" HeaderText="AUTORIZZAZIONE" ItemStyle-HorizontalAlign="Left"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                                            FilterControlWidth="85%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="DATA_ANNULLO" HeaderText="DATA ANNULLAMENTO" DataFormatString="{0:dd/MM/yyyy}"
                                            HeaderStyle-Width="13%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                        </telerik:GridDateTimeColumn>
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
                                    <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                        AllowResizeToFit="false" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                            CssClass="txtMia" Font-Names="Arial" Font-Size="12pt" MaxLength="100" Width="768px"
                            ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                            Style="visibility: hidden" MaxLength="100" Width="152px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="isExporting" runat="server" Value="0" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
