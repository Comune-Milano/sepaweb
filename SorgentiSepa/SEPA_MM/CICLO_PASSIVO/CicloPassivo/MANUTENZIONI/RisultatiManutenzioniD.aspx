<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiManutenzioniD.aspx.vb"
    Inherits="MANUTENZIONI_RisultatiManutenzioniD" %>

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
<body>
    <form id="form1" runat="server" onsubmit="caricamento();return true;" >
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnEsporta">
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
    <table style="width: 100%">
        <tr>
            <td class="TitoloModulo">
                Ordini - Manutenzioni e servizi - Ricerca - Diretta
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
                                    <telerik:RadButton ID="btnEsporta" runat="server" Text="Export in Excel" ToolTip="Export in Excel" 
                                       OnClientClicking="function(sender,args){ nascondi=0;}" />
                                </td>
                        <td>
                            <telerik:RadButton ID="btnRicerca" runat="server" Text="Nuova ricerca" ToolTip="Nuova Ricerca" />
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
                <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 99%">
                    <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PagerStyle-Visible="true" AllowPaging="true" AllowFilteringByColumn="True" EnableLinqExpressions="False"
                        Width="99%" AllowSorting="True" PageSize="100" IsExporting="False">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            Width="200%" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <div style="float: right; padding: 4px;">
                                        <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                            CssClass="rgRefresh" OnClientClick="nascondi=0;" />
                                        <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                            CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" Visible="false" />
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID_MANUTENZIONE" HeaderText="ID_MANUTENZIONE" Exportable="false"
                                    Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPERTORIO" SortExpression="REP_ORD"
                                    HeaderStyle-Width="10%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ODL_ANNO" HeaderText="ODL/ANNO" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-Width="10%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="DATA_INIZIO_ORDINE" HeaderText="DATA" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderStyle-Width="13%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" Exportable="true">
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="UBICAZIONE" HeaderText="UBICAZIONE" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="15%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_PRE" HeaderText="IMPORTO PREVENTIVO" Exportable="true"
                                    HeaderStyle-Width="15%" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                                    DataFormatString="{0:C2}" FilterControlWidth="85%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_CON" HeaderText="IMPORTO CONSUNTIVO" Exportable="true"
                                    CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" DataFormatString="{0:C2}"
                                    FilterControlWidth="85%" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO" ItemStyle-HorizontalAlign="Left" Exportable="true"
                                    HeaderStyle-Width="15%" FilterControlWidth="85%" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO_VOCI" HeaderText="VOCE DGR" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="15%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="20%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CODICE_BP" HeaderText="CODICE BP" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="10%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="VOCE BP" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="30%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STRUTTURA_ALER" HeaderText="STRUTTURA" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="10%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="10%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PROGR" HeaderText="ODL_NUM" Visible="False" Exportable="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO" Visible="False" Exportable="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_ODL" HeaderText="DATA_ODL" Visible="False" Exportable="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="10%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AUTORIZZAZIONE" HeaderText="AUTORIZZAZIONE" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="10%" FilterControlWidth="85%" AutoPostBackOnFilter="true" Exportable="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_ANNULLO" HeaderText="DATA ANNULLAMENTO" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderStyle-Width="13%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" Exportable="true">
                                    </telerik:GridDateTimeColumn>
                            </Columns>
                             <SortExpressions>
                                        <telerik:GridSortExpression FieldName="REP_ORD" SortOrder="Descending" />
                                    </SortExpressions>
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
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                    Font-Names="Arial" Font-Size="12pt" MaxLength="100" Width="768px" ReadOnly="True"
                    CssClass="txtMia" Font-Bold="True">Nessuna Selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                    Style="visibility: hidden" MaxLength="100" Width="152px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
    </form>
</body>
</html>
