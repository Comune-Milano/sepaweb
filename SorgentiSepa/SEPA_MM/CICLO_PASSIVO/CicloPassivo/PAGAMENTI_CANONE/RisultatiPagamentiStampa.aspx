<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiPagamentiStampa.aspx.vb"
    Inherits="PAGAMENTI_CANONE_RisultatiPagamentiStampa" %>

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
                <td class="TitoloModulo">
                      <asp:Label ID="lblTitolo" runat="server"></asp:Label>
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
                                <telerik:RadButton ID="btnStampaPagamento" runat="server" Text="Stampa Pagamento"
                                    ToolTip="Stampa Pagamento" />
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
                                <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" AllowFiltering="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PROG_ANNO" HeaderText="CDP" AllowFiltering="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SAL_ANNO" HeaderText="SAL" AllowFiltering="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                                    </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="DATA_PRENOTAZIONE" HeaderText="PRENOTAZIONE"
                                    DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE" DataFormatString="{0:dd/MM/yyyy}"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="BENEFICIARIO" HeaderText="BENEFICIARIO" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="IMPORTO PRENOTATO"
                                    Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_CONSUNTIVATO" HeaderText="IMPORTO CONSUNTIVATO"
                                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" FilterControlWidth="85%"
                                    CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" FilterControlWidth="85%"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO PAGAMENTO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_FORNITORE" HeaderText="ID_FORNITORE" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="False">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                            <PagerStyle AlwaysVisible="True"></PagerStyle>
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
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        CssClass="txtMia" Width="100%" Font-Names="Arial" Font-Size="12pt" MaxLength="100"
                        ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                        MaxLength="100" Style="left: 544px; position: absolute; top: 576px; visibility: hidden"
                        Width="152px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="txtStampa" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdFornitore" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="txtIdAppalto" runat="server"></asp:HiddenField>
    </div>
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
</body>
<script type="text/javascript">

    function StampaPagamento() {

        if (document.getElementById('txtid').value == '') {
            alert('Nessuna riga selezionata!')
            return;
        }


        vIdPrenotazioni = document.getElementById('txtid').value;

        document.getElementById('txtStampa').value = '1';

        //var sicuro = confirm('Attenzione...Confermi di voler emettere un pagamento?');
        //if (sicuro == true) {
        //    document.getElementById('txtStampa').value = '1';
        //}
        //else {
        //    document.getElementById('txtStampa').value = '0';
        //}
    }

    window.onresize = setDimensioni;
    Sys.Application.add_load(setDimensioni);

</script>
</html>
