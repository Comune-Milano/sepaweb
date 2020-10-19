<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RitenuteLegge.aspx.vb" Inherits="RitenuteLegge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Ritenute di Legge</title>
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
    <table style="width: 100%">
        <tr>
            <td class="TitoloModulo">
                Contratti - Ritenute di legge
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnHome" runat="server" Text="Esci" ToolTip="Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel1" Style="width: 100%">
                    <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        AllowPaging="true" AllowFilteringByColumn="True" EnableLinqExpressions="False"
                        Width="99%" AllowSorting="True" PageSize="100" IsExporting="False">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top">
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
                                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUMERO REPERTORIO"
                                    AutoPostBackOnFilter="true" FilterControlWidth="85%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:@}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <FooterStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="DATA_REPERTORIO" HeaderText="DATA DI REPERTORIO"
                                    DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="RAGIONE_SOCIALE" HeaderText="FORNITORE" HeaderStyle-HorizontalAlign="Center"
                                    FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:@}">
                                    <FooterStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" HeaderStyle-HorizontalAlign="Center"
                                    FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:@}">
                                    <FooterStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Size="X-Large" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SOMMA_RIT_LEGGE_IVATA" HeaderText="IMPORTO MATURATO"
                                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" FilterControlWidth="85%"
                                    CurrentFilterFunction="EqualTo" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    <FooterStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Size="X-Large" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
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
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Text="Label" Visible="False" Width="624px"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
