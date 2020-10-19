<%@ Page Title="Risultati ricerca consuntivi" Language="VB" MasterPageFile="HomePage.master"
    AutoEventWireup="false" CodeFile="RisultatiRicercaConsuntivi.aspx.vb" Inherits="SPESE_REVERSIBILI_RisultatiRicercaConsuntivi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ApriDettaglio(sender, args) {
            var idUnita = args.getDataKeyValue("ID_UNITA");
            var idContratto = args.getDataKeyValue("ID_CONTRATTO");
            openModalInRad('MasterPage_ContentPlaceHolder1_RadWindowDettaglio', 'RadWindowDettaglio.aspx?idu=' + idUnita + '&idc=' + idContratto, 500, 500, null, null, null, 1);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelDettaglio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelDettaglio" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="DataGridUI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGridUI" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadGrid ID="DataGridUI" runat="server" GroupPanelPosition="Top" AllowPaging="true"
        PagerStyle-AlwaysVisible="true" PageSize="50" AutoGenerateColumns="False" Culture="it-IT"
        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
        IsExporting="False">
        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
            CommandItemDisplay="Top" AllowMultiColumnSorting="true" ClientDataKeyNames="ID_UNITA,ID_CONTRATTO"
            DataKeyNames="ID_UNITA,ID_CONTRATTO">
            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                ShowRefreshButton="true" />
            <Columns>
                <telerik:GridBoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="CODICE UNITA' IMMOBILIARE"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPOLOGIA_UNITA" HeaderText="STATO UNITA' IMMOBILIARE"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE CONTRATTO"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO CONTRATTO"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NUMERO_GIORNI" HeaderText="NUMERO GIORNI" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SUPERFICIE_NETTA" HeaderText="VALORE SUPERFICIE"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SUPERFICIE_CATASTALE" HeaderText="TIPOLOGIA SUPERFICIE"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="INTERNO" HeaderText="INTERNO" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PIANO" HeaderText="PIANO" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MILLESIMI_SERVIZI_COMPLESSO" HeaderText="CDR SERVIZI COMPLESSO"
                    DataFormatString="{0:N4}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MILLESIMI_SERVIZI_COMPLESSO_P" HeaderText="CDR SERVIZI COMPLESSO PERTINENZE"
                    DataFormatString="{0:N4}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MILLESIMI_SERVIZI_EDIFICIO" HeaderText="CDR SERVIZI EDIFICIO"
                    DataFormatString="{0:N4}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MILLESIMI_SERVIZI_EDIFICIO_P" HeaderText="CDR SERVIZI EDIFICIO PERTINENZE"
                    DataFormatString="{0:N4}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MILLESIMI_RISCALDAMENTO" HeaderText="CDR RISCALDAMENTO"
                    DataFormatString="{0:N4}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MILLESIMI_RISCALDAMENTO_P" HeaderText="CDR RISCALDAMENTO PERTINENZE"
                    DataFormatString="{0:N4}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MILLESIMI_SCALA_ASCENSORE" HeaderText="CDR ASCENSORE"
                    DataFormatString="{0:N4}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MILLESIMI_SCALA_ASCENSORE_P" HeaderText="CDR ASCENSORE PERTINENZE"
                    DataFormatString="{0:N4}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SERVIZI" HeaderText="SERVIZI" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ACQUA" HeaderText="SERVIZI ACQUA" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ALTRO" HeaderText="SERVIZI ALTRO" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CONDUZIONE" HeaderText="SERVIZI CONDUZIONE" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CUSTODI" HeaderText="SERVIZI CUSTODI" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CUSTODI_AUTOGESTIONE" HeaderText="SERVIZI CUSTODI AUTOGESTIONE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FOGNATURA" HeaderText="SERVIZI FOGNATURA" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PARTI_COMUNI" HeaderText="SERVIZI PARTI COMUNI"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PULIZIA" HeaderText="SERVIZI PULIZIA" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PULIZIA_AUTOGESTIONE" HeaderText="SERVIZI PULIZIA AUTOGESTIONE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PULIZIA_PARTI_COMUNI" HeaderText="SERVIZI PULIZIA PARTI COMUNI"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UTENZE_ELETTRICHE" HeaderText="SERVIZI UTENZE ELETTRICHE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UTENZE_IDRICHE" HeaderText="SERVIZI UTENZE IDRICHE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="VARIE" HeaderText="SERVIZI VARIE" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="VARIE_AUTOGESIONE" HeaderText="SERVIZI VARIE AUTOGESIONE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="VERDE" HeaderText="SERVIZI VERDE" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="VERDE_AUTOGESTIONE" HeaderText="SERVIZI VERDE AUTOGESTIONE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO" HeaderText="RISCALDAMENTO" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO_ACQUA" HeaderText="RISCALDAMENTO ACQUA"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO_APPALTO" HeaderText="RISCALDAMENTO APPALTO"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO_AUTOGESTIONE" HeaderText="RISCALDAMENTO AUTOGESTIONE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO_FORZA_MOTRICE" HeaderText="RISCALDAMENTO FORZA MOTRICE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO_GAS_AUTOGESTIONE" HeaderText="RISCALDAMENTO GAS AUTOGESTIONE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO_GAS_METANO" HeaderText="RISCALDAMENTO GAS METANO"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO_GESTIONE_CALORE" HeaderText="RISCALDAMENTO GESTIONE CALORE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO_TR_10" HeaderText="RISCALDAMENTO TELERISCALDAMENTO (IVA 10%)"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RISCALDAMENTO_TR_21" HeaderText="RISCALDAMENTO TELERISCALDAMENTO (IVA 21%)"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SCALA_ASCENSORE" HeaderText="ASCENSORE" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ASCENSORI_FORZA_MOTRICE" HeaderText="ASCENSORE FORZA MOTRICE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ASCENSORI_MANUTENZIONE" HeaderText="ASCENSORE MANUTENZIONE"
                    AutoPostBackOnFilter="true" DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MONTASCALE" HeaderText="MONTASCALE" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TOTALE_ONERI" HeaderText="TOTALE ONERI" AutoPostBackOnFilter="true"
                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TOTALE_BOLLETTATO" HeaderText="TOTALE BOLLETTATO"
                    DataFormatString="{0:C2}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TOTALE_CONGUAGLIO" HeaderText="TOTALE CONGUAGLIO"
                    DataFormatString="{0:C2}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="COD_UNITA_IMMOBILIARE" SortOrder="Ascending" />
            </SortExpressions>
            <PagerStyle AlwaysVisible="True" />
            <CommandItemTemplate>
                <div style="display: inline-block; width: 100%;">
                    <div style="float: right; padding: 4px;">
                        <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh" OnClientClick="caricamento(2);"
                            CssClass="rgRefresh" />
                        <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click" OnClientClick="caricamento(2);"
                            CommandName="ExportToExcel" CssClass="rgExpXLS" />
                </div>
                </div>
            </CommandItemTemplate>
        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <ExportSettings OpenInNewWindow="true" IgnorePaging="true" ExportOnlyData="true"
            HideStructureColumns="true">
            <Excel FileExtension="xlsx" Format="Xlsx" />
        </ExportSettings>
        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
            ClientEvents-OnCommand="onCommand">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <Selecting AllowRowSelect="True" />
            <ClientEvents OnRowDblClick="ApriDettaglio" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:Button runat="server" CssClass="nascondi" ID="ButtonCaricaDettaglio" ClientIDMode="Static" />
    <asp:Button runat="server" CssClass="nascondi" Style="display: none" ID="ButtonCaricaSpese"
        ClientIDMode="Static" />
    
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1" ClientIDMode="Static"> 
    </telerik:RadWindowManager>
    
    <asp:Panel runat="server" ID="PanelDettaglio">
        <telerik:RadWindow ID="RadWindowDettaglio" runat="server" CenterIfModal="true" Modal="True"
            VisibleStatusbar="False" Behavior="Pin, Move, Resize, Maximize" Width="900px"
            Height="700px" ShowContentDuringLoad="false">
        </telerik:RadWindow>
    </asp:Panel>
    
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="260" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="ButtonNuovaRicerca" runat="server" OnClientClick="caricamento(2);"
        Text="Nuova ricerca" ToolTip="Nuova ricerca" />
    <asp:Button ID="ButtonEsci" runat="server" OnClientClick="tornaHome();return false;" Text="Esci"
        ToolTip="Esci" />
</asp:Content>
