<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiPagamenti.aspx.vb"
    Inherits="PAGAMENTI_RisultatiPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<head id="Head1" runat="server" onsubmit="caricamento();return true;">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>RISULTATI RICERCA</title>
</head>
<body class="sfondo">
    <form id="form2" runat="server" onsubmit="caricamento();return true;">
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
                <td class="TitoloModulo">
                     Ordini - Ordini e pagamenti - Ricerca
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
                                <telerik:RadButton ID="btnRicerca" runat="server" Text="Nuova ricerca" ToolTip="Nuova Ricerca"
                                    Style="top: 0px; left: 0px" />
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
                    <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%; overflow: auto">
                        <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                            AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                            PageSize="100" IsExporting="False">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                CommandItemDisplay="Top" Width="200%">
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
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                        <HeaderStyle Width="0%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PROG_ANNO" HeaderText="ODL/ANNO" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_ORDINE" HeaderText="DATA ORDINE" DataFormatString="{0:dd/MM/yyyy}"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="BENEFICIARIO" HeaderText="BENEFICIARIO" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="15%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PREN_LORDO" HeaderText="IMP. PRENOTATO" AutoPostBackOnFilter="true"
                                        DataFormatString="{0:C2}" FilterControlWidth="85%" CurrentFilterFunction="EqualTo">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CONS_LORDO" HeaderText="IMP. CONSUNTIVATO" AutoPostBackOnFilter="true"
                                        DataFormatString="{0:C2}" FilterControlWidth="85%" CurrentFilterFunction="EqualTo">
                                        <HeaderStyle Width="10%" Font-Bold="false" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Width="20%" Font-Bold="false" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CODICE_BP" HeaderText="COD. BP" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="5%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="VOCE BP" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="20%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO PAGAMENTO" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="5%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_VOCE_PF" HeaderText="ID_VOCE_PF" Visible="False">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Width="0%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
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
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        CssClass="txtMia" Width="100%" Font-Names="Arial" Font-Size="12pt" MaxLength="100"
                        ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtStatoPagamento" runat="server" BackColor="#F2F5F1" BorderColor="White"
                        BorderStyle="None" MaxLength="100" Style="left: 544px; position: absolute; top: 576px;
                        visibility: hidden" Width="152px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                        MaxLength="100" Style="left: 544px; position: absolute; visibility: hidden; top: 576px"
                        Width="152px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtIdVoce" runat="server" BackColor="#F2F5F1" BorderColor="White"
                        BorderStyle="None" MaxLength="100" Style="left: 544px; position: absolute; top: 576px;
                        visibility: hidden" Width="152px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
</body>
<script type="text/javascript" language="javascript">
    window.onresize = setDimensioni;
    Sys.Application.add_load(setDimensioni);
</script>
</html>
