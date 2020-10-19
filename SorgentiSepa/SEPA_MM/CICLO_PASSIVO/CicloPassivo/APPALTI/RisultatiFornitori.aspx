<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiFornitori.aspx.vb"
    Inherits="APPALTI_RisultatiFornitori" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Selezionato;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fornitori / Ricerca</title>
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript">
    function ApriSchedaFornitore() {
        if (document.getElementById('IDFornitoreSelezionato').value != '-1') {
            location.replace('Fornitori.aspx?ID=' + document.getElementById('IDFornitoreSelezionato').value + '&CF=' + document.getElementById('CODFIS').value + '&CO=' + document.getElementById('CODFOR').value + '&RA=' + document.getElementById('RAGSOC').value + '&PI=' + document.getElementById('PARIVA').value);
        } else {
            radalert('Non hai selezionato alcuna riga!', '300', '150');
        };
    };


</script>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <table width="98%">
            <tr>
                <td style="width: 100%" class="TitoloModulo">Fornitori - Ricerca
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="RadButtonApri" runat="server" Text="Visualizza" OnClientClicking="function(sender, args){ApriSchedaFornitore();}"
                                    AutoPostBack="False" ToolTip="Visualizza scheda fornitore">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="RadButtonNuovaRicerca" runat="server" Text="Nuova Ricerca"
                                    ToolTip="Effettua una nuova ricerca">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="RadButtonEsci" runat="server" Text="Esci" ToolTip="Esci">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="labelNORisultati" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                    <telerik:RadGrid ID="RadGrid1" runat="server" GroupPanelPosition="Top" HeaderStyle-Width="15%"
                        AllowPaging="true" PagerStyle-AlwaysVisible="true" PageSize="50" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                        IsExporting="False">
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
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COD_FORNITORE" HeaderText="CODICE FORNITORE"
                                    UniqueName="COD_FORNITORE" ItemStyle-HorizontalAlign="Center"
                                    AutoPostBackOnFilter="true" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RAGIONE_SOCIALE" HeaderText="REGIONE SOCIALE"
                                    ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" FilterCheckListEnableLoadOnDemand="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COD_FISCALE" HeaderText="CODICE FISCALE" ItemStyle-HorizontalAlign="Left"
                                    FilterControlWidth="85%" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PARTITA_IVA" HeaderText="PARTITA IVA" ItemStyle-HorizontalAlign="Left"
                                    FilterControlWidth="85%" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MAIL" HeaderText="INDIRIZZO EMAIL" ItemStyle-HorizontalAlign="Left"
                                    FilterControlWidth="85%" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RIT_ACCONTO" HeaderText="ALIQUOTA RITENUTA DI ACCONTO"
                                    FilterControlWidth="85%" ItemStyle-HorizontalAlign="Center"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
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

            <tr>
                <td>
                    <asp:TextBox ID="txtmia" runat="server" CssClass="txtMia" ReadOnly="true" Text="Nessuna selezione"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="CODFIS" runat="server" />
        <asp:HiddenField ID="PARIVA" runat="server" />
        <asp:HiddenField ID="RAGSOC" runat="server" />
        <asp:HiddenField ID="CODFOR" runat="server" />
        <asp:HiddenField ID="isExporting" runat="server" Value="0" />
        <asp:HiddenField ID="IDFornitoreSelezionato" runat="server" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <asp:HiddenField ID="HFFiltroEvento" runat="server" />
        <asp:HiddenField ID="HFFiltroEvento2" runat="server" />
        <asp:HiddenField ID="HFAltezzaSottratta" runat="server" Value="250" />

    </form>
</body>
<script language="javascript" type="text/javascript">
    window.onresize = setDimensioni;
    Sys.Application.add_load(setDimensioni);


</script>
</html>
