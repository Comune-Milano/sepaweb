<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiSituazioneContabilePerAppalti.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiSituazioneContabilePerAppalti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Contratti - Situazione contabile</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0"
            MinDisplayTime="100" Width="100%" Height="100%">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DataGridAppalti">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGridAppalti" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <%--<td>
                                <telerik:RadButton ID="ImageButtonEsporta" runat="server" Style="top: 0px; left: 0px"
                                    Text="Export XLS" ToolTip="Esporta in Excel" />
                            </td>--%>
                                <td>
                                    <telerik:RadButton ID="btnEsci" runat="server" Style="top: 0px; left: 0px" Text="Esci"
                                        ToolTip="Esci" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                            Text="Situazione Contabile Contratti*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="Label1" runat="server" Font-Bold="false" Font-Names="Arial" Font-Size="9pt"
                            Text="* Tutti gli importi sono da considerarsi IVA esclusa tranne dove esplicitamente dichiarato."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRis" runat="server" Text="Nessun risultato trovato" Font-Bold="True"
                            Font-Names="Arial" Font-Size="10pt"></asp:Label>

                        <telerik:RadGrid ID="DataGridAppalti" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" HeaderStyle-Width="15%"
                            PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                            AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                            PageSize="100" IsExporting="False">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true" Width="200%"
                                CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                    ShowRefreshButton="true" />
                                <CommandItemTemplate>
                                    <div style="display: inline-block; width: 100%;">
                                        <div style="float: right; padding: 4px;">
                                            <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh" OnClientClick="nascondi=0;"
                                                CssClass="rgRefresh" />
                                            <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                                CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                        </div>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID_GRUPPO" HeaderText="ID_GRUPPO" ItemStyle-HorizontalAlign="Left"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUMERO REPERTORIO" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO" DataFormatString="{0:@}" HeaderText="STATO" ItemStyle-HorizontalAlign="Left">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CIG" DataFormatString="{0:@}" HeaderText="CIG" ItemStyle-HorizontalAlign="Left">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_CONTRATTO" DataFormatString="{0:@}" HeaderText="TIPOLOGIA CONTRATTO" ItemStyle-HorizontalAlign="Left">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_REPERTORIO" HeaderText="DATA REPERTORIO" ItemStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" DataFormatString="{0:@}" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FORNITORE" DataFormatString="{0:@}" HeaderText="FORNITORE" ItemStyle-HorizontalAlign="Left">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DL" DataFormatString="{0:@}" HeaderText="DIRETTORE LAVORI" ItemStyle-HorizontalAlign="Left">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CONDIZIONE_PAGAMENTO" DataFormatString="{0:@}" HeaderText="CONDIZIONE DI PAGAMENTO" ItemStyle-HorizontalAlign="Left">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" ItemStyle-HorizontalAlign="Left">
                                </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn DataField="BUDGET" DataFormatString="{0:C2}" HeaderText="BASE ASTA CANONE" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOTALE_CANONE_IVA_ESCLUSA" DataFormatString="{0:C2}" HeaderText="TOTALE CONTRATTUALE CANONE"
                                        ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOTALE_CONTRATTUALE" DataFormatString="{0:C2}" HeaderText="TOTALE CONTRATTUALE CANONE ONERI ESCLUSI"
                                        ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ONERI" HeaderText="ONERI" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BUDGET_VARIAZIONI" DataFormatString="{0:C2}" HeaderText="VARIAZIONI CANONE" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOTALE" DataFormatString="{0:C2}" HeaderText="TOTALE CANONE IVA INCLUSA" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RESIDUO" DataFormatString="{0:C2}" HeaderText="RESIDUO CANONE IVA INCLUSA" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BUDGET2" DataFormatString="{0:C2}" HeaderText="BASE ASTA CONSUMO" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOTALE_CONSUMO_IVA_ESCLUSA" DataFormatString="{0:C2}" HeaderText="TOTALE CONTRATTUALE CONSUMO"
                                        ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOTALE_CONTRATTUALE2" DataFormatString="{0:C2}" HeaderText="TOTALE CONTRATTUALE CONSUMO ONERI ESCLUSI"
                                        ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ONERI2" DataFormatString="{0:C2}" HeaderText="ONERI CONSUMO" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BUDGET_VARIAZIONI2" DataFormatString="{0:C2}" HeaderText="VARIAZIONI CONSUMO" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOTALE2" DataFormatString="{0:C2}" HeaderText="TOTALE CONSUMO IVA INCLUSA" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RESIDUO2" DataFormatString="{0:C2}" HeaderText="RESIDUO CONSUMO IVA INCLUSA" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_SCADENZA" HeaderText="DATA SCADENZA CONTRATTO" ItemStyle-HorizontalAlign="Center">
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
        </div>
        <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>

</html>
