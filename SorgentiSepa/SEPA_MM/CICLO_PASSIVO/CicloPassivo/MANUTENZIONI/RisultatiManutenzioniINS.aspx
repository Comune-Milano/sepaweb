<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiManutenzioniINS.aspx.vb"
    Inherits="MANUTENZIONI_RisultatiManutenzioniINS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<head id="Head1" runat="server">
    <title>RISULTATI RICERCA</title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
</head>
<body class="sfondo">
    <form id="form1" runat="server"  onsubmit="caricamento();return true;" >
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="DataGrid2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGrid2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td class="TitoloModulo">
                  Ordini - Manutenzioni e servizi - Inserimento
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnVisualizza" runat="server" Text="Procedi" ToolTip="Visualizza la maschera di inserimento ordine" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnRicerca" runat="server" Text="Nuova Ricerca" ToolTip="Nuova Ricerca" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Esci" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="90%">
                <asp:Panel runat="server" ID="PanelRadGrid">
                    <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PagerStyle-Visible="true" AllowPaging="true" AllowFilteringByColumn="True" EnableLinqExpressions="False"
                        Width="99%" AllowSorting="True" PageSize="100" IsExporting="False">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <div style="float: right; padding: 4px;">
                                        <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh1_Click" CommandName="Refresh"
                                            CssClass="rgRefresh" OnClientClick="nascondi=0;" />
                                        <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta1_Click"
                                            CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_LOTTO" HeaderText="ID_LOTTO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_PF_VOCE_IMPORTO" HeaderText="ID_PF_VOCE_IMPORTO"
                                    Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_UBICAZIONE" HeaderText="ID_UBICAZIONE" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="REPERTORIO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="9%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE_APPALTI" HeaderText="DESCRIZIONE"
                                    FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="11%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="15%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESC_EDIFICIO" HeaderText="EDIFICIO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO EDIFICIO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="20%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="15%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO_VOCE" HeaderText="VOCE" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
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
                           <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                            AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <telerik:RadGrid ID="DataGrid2" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PagerStyle-Visible="true" AllowPaging="true" AllowFilteringByColumn="True" EnableLinqExpressions="False"
                        Width="99%" AllowSorting="True" PageSize="100" IsExporting="False">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top" Width="150%">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <div style="float: right; padding: 4px;">
                                        <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh2_Click" CommandName="Refresh"
                                            OnClientClick="nascondi=0;" CssClass="rgRefresh" />
                                        <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta2_Click"
                                            CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_LOTTO" HeaderText="ID_LOTTO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_PF_VOCE_IMPORTO" HeaderText="ID_PF_VOCE_IMPORTO"
                                    Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_UBICAZIONE" HeaderText="ID_UBICAZIONE" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="REPERTORIO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE_APPALTI" HeaderText="DESCRIZIONE"
                                    FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="10%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESC_EDIFICIO" HeaderText="EDIFICIO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO EDIFICIO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="20%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESC_IMPIANTO" HeaderText="IMPIANTO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TIPO_IMPIANTO" HeaderText="TIPO IMPIANTO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COD_IMPIANTO" HeaderText="COD. IMPIANTO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DETTAGLIO_IMPIANTO" HeaderText="Num. MATRICOLA e/o Num. IMPIANTO"
                                    FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Width="10%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO_VOCE" HeaderText="VOCE" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
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
            <td class="FontTelerik">
                <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                    CssClass="txtMia" MaxLength="100" Width="768px" ReadOnly="True" Font-Bold="true">Nessuna selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                    Style="visibility: hidden" MaxLength="100" Width="152px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="txtIdComplesso" runat="server" Value="0" />
    <asp:HiddenField ID="txtIdEdificio" runat="server" Value="0" />
    <asp:HiddenField ID="txtIdLotto" runat="server" Value="0" />
    <asp:HiddenField ID="txtIdAppalto" runat="server" Value="0" />
    <asp:HiddenField ID="txtIdServizio" runat="server" Value="0" />
    <asp:HiddenField ID="txtIdPF_VoceServizio" runat="server" Value="0" />
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
    </form>
</body>
</html>
