<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="ReportAbusivi.aspx.vb" Inherits="SICUREZZA_ReportAbusivi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="Label1" Text="Elenco abusivi" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <script type="text/javascript">
        //        window.onresize = ResizeGrid;
        //        Sys.Application.add_load(ResizeGrid);
        //        function ResizeGrid() {
        //            var scrollArea = document.getElementById("<%= RadGridContratti.ClientID %>" + "_GridData");
        //            scrollArea.style.height = window.innerHeight - 300 + 'px';
        //        };
        function apriRU() {
            validNavigation = true;
            if (document.getElementById('idSelezionato').value != '0') {
                window.open('../Contratti/Contratto.aspx?ID=' + document.getElementById('idSelezionato').value + '&COD=' + document.getElementById('codContratto').value, '', 'height=780,width=1160');
            }
            else {
                apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
            }
        };
        
    </script>
    <asp:Panel runat="server" ID="tabContainer">
        <asp:TextBox runat="server" ID="txtRUSelected" Text="" BackColor="Transparent" BorderColor="Transparent"
            BorderWidth="0px" Font-Bold="True" Font-Names="arial" Font-Size="9pt" ForeColor="Black"
            Width="95%" ReadOnly="true" ClientIDMode="Static" />
        <div id="divOverContent" style="width: 100%; overflow: auto;">
            <telerik:RadGrid ID="RadGridContratti" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                ResolvedRenderMode="Classic" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                AllowPaging="True" IsExporting="false" Skin="Web20" AllowFilteringByColumn="True"
                PageSize="100" Width="95%">
                <MasterTableView NoMasterRecordsText="Nessun contratto da visualizzare." ShowHeadersWhenNoRecords="true"
                    CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToExcelButton="True" ShowExportToWordButton="false"
                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                        ShowRefreshButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NOME_INTEST" HeaderText="INTESTATARIO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="STATO_DEL_CONTRATTO" HeaderText="STATO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COD_TIPOLOGIA_CONTR_LOC" HeaderText="TIPO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPO_SPECIFICO" HeaderText="TIPO CONTR. SPEC." CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CIVICO" HeaderText="CIVICO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COMUNE_UNITA" HeaderText="COMUNE" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PIANO" HeaderText="PIANO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="INTERNO" HeaderText="INTERNO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ExportSettings OpenInNewWindow="true" IgnorePaging="true">
                    <Excel FileExtension="xls" Format="Biff" />
                </ExportSettings>
                <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false" />
                <ExportSettings OpenInNewWindow="true" IgnorePaging="true">
                    <Excel FileExtension="xls" Format="Biff" />
                </ExportSettings>
                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    <Selecting AllowRowSelect="True" />
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                        AllowResizeToFit="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="idSelezionato" runat="server" ClientIDMode="Static" Value="-1" />
    <asp:HiddenField runat="server" ID="codContratto" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
