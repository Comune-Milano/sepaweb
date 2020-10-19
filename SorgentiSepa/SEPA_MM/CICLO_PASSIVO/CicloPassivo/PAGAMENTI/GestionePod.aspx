<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestionePod.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_GestionePod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco POD</title>
    <script src="../../../Standard/Scripts/jsMessage.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <link href="../../../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function NewPOD() {
            var oWnd = $find('RadWindow1');
            oWnd.setUrl('GestModPod.aspx');
            oWnd.show();
        };
        function refreshGrid(arg) {
            if (document.getElementById('Button1')) {
                document.getElementById('Button1').click();
            };
        };
        function ModPOD() {
            if (document.getElementById('idSel').value != '') {
                var oWnd = $find('RadWindow1');
                oWnd.setUrl('GestModPod.aspx?idPod=' + document.getElementById('idSel').value);
                oWnd.show();
            } else {
                apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
            };
        };
        function ModFor() {
            var oWnd = $find('RadWindow2');
            oWnd.setUrl('GestCambioFornitura.aspx');
            oWnd.show();
        };
        function ModGestPOD() {
            if (document.getElementById('idSel').value != '') {
                if (document.getElementById('flAttivo').value == '0') {
                    apriAlert('Non è possibile gestire POD disattivati!', 300, 150, 'Attenzione', null, null);
                    return false;
                };
                var oWnd = $find('RadWindow1');
                oWnd.setUrl('GestPod.aspx?IDPOD=' + document.getElementById('idSel').value);
                oWnd.show();
            } else {
                apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
                return false;
            };
        };
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ExportToExcel") >= 0) {
                args.set_enableAjax(false);
            }
        };
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadStyleSheetManager runat="server">
    </telerik:RadStyleSheetManager>
    <script type="text/javascript">
        window.onresize = ResizeGrid;
        Sys.Application.add_load(ResizeGrid);
        function ResizeGrid() {
            var scrollArea = document.getElementById("<%= RadGridPOD.ClientID %>" + "_GridData");
            scrollArea.style.height = window.screen.height - 570 + 'px';
        };
    </script>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    <Scripts>
       <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
       <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
   </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons"
        ControlsToSkip="Zone" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Transparency="100">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="requestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Button1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridPod" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonSalvaAggregazioneImpianto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonSalvaAggregazioneScala">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonSalvaAggregazioneUnita">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonSalvaAggregazioneEdificio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonSalvaAggregazioneComplesso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnModificaAggregazione">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Aggregazione" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonEliminaAggregazione">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridPod" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Aggregazione" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonSalvaAggregazione">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridPod" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Aggregazione" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonEsciAgg">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridPod" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDelPod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridPod" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonDisattiva">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridPod" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PanelRadGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridPod" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="contenutoEdificio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoEdificio" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="contenutoScala">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoScala" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="contenutoUnita">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoUnita" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="contenutoImpianto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenutoImpianto" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="contenutoAggregazione" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <asp:Button ID="Button1" runat="server" Text="" CssClass="nascondiPulsante" />
    <div>
        <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
            EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="BottomRight"
            OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
        </telerik:RadNotification>
    </div>
    <table border="0" cellpadding="2" cellspacing="2" width="100%" class="FontTelerik">
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="90%">
                <asp:Panel runat="server" ID="PanelRadGrid">
                    <telerik:RadGrid ID="RadGridPod" runat="server" GroupPanelPosition="Top" AutoGenerateColumns="False" HeaderStyle-Width="15%"
                        Culture="it-IT" RegisterWithScriptManager="False" AllowFilteringByColumn="True"
                        EnableLinqExpressions="False" Width="100%" AllowSorting="True" IsExporting="False"
                        AllowPaging="True">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top" HierarchyLoadMode="ServerOnDemand">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <DetailTables>
                                <telerik:GridTableView Name="Dettagli" Width="100%" AllowPaging="false" BackColor="Azure"
                                    HierarchyDefaultExpanded="true">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_CUSTODE" HeaderText="ID_CUSTODE" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_AGGREGAZIONE" HeaderText="ID_AGGREGAZIONE"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_COMPLESSO" HeaderText="ID_COMPLESSO" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_EDIFICIO" HeaderText="ID_EDIFICIO" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO">
                                            <HeaderStyle Width="15%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOME_AGGREGAZIONE" HeaderText="NOME">
                                            <HeaderStyle Width="55%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO">
                                            <HeaderStyle Width="15%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_FINE" HeaderText="DATA FINE" EmptyDataText="">
                                            <HeaderStyle Width="15%" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CONTRATTO" HeaderText="CONTRATTO" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="POD" HeaderText="POD" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORNITURA" HeaderText="FORNITURA" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ATTIVO" HeaderText="ATTIVO" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings OpenInNewWindow="true" IgnorePaging="true">
                            <Excel FileExtension="xls" Format="Biff" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                    </telerik:RadGrid>
                </asp:Panel>
            </td>
            <td style="vertical-align: top" width="10%">
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnCreaCdp" runat="server" Text="Aggiungi" Width="104px" ClientIDMode="Static"
                                OnClientClicking="NewPOD" AutoPostBack="false" ToolTip="Aggiungi un POD">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnModPod" runat="server" Text="Modifica" Width="104px" ClientIDMode="Static"
                                ToolTip="Modifica il POD selezionato" OnClientClicking="ModPOD" AutoPostBack="false">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="RadButtonCambioFornitore" runat="server" Text="Cambio Fornitore"
                                Width="104px" ClientIDMode="Static" ToolTip="Cambio Fornitore" OnClientClicking="ModFor"
                                AutoPostBack="false">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="RadButtonDisattiva" runat="server" Text="Disattiva" Width="104px"
                                ClientIDMode="Static" OnClientClicking="function(sender, args){disabledElementTelerik(sender, args, 'idSel','flAttivo');}"
                                ToolTip="Disattiva il POD selezionato">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnDelPOD" runat="server" Text="Elimina" Width="104px" OnClientClicking="function(sender, args){deleteElementTelerik(sender, args, 'idSel');}"
                                ToolTip="Elimina il POD selezionato">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="RadButtonAggregazioni" runat="server" Text="Gestione Aggr."
                                AutoPostBack="false" Width="104px" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioni');}"
                                ToolTip="Gestione aggregazione">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnPOD" runat="server" Text="Gestione" ToolTip="Gestione del POD"
                                AutoPostBack="false" Width="104px" OnClientClicking="ModGestPOD">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnEstraiTuttiAttuali" runat="server" Text="Estrai attuali" ToolTip="Estrazione POD attuale"
                                Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnEstraiTutti" runat="server" Text="Estrai tutti" ToolTip="Estrazione POD completa"
                                Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="ButtonEstraiAggregazioni" runat="server" Text="Estrai Aggr." ToolTip="Estrazione della composizione delle aggregazioni"
                                Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idSel" runat="server" Value="" />
    <asp:HiddenField ID="flAttivo" runat="server" Value="0" />
    <telerik:RadWindow ID="RadWindowAggregazioni" runat="server" CenterIfModal="true"
        Modal="True" VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize"
        Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="Aggregazione">
                <table border="0" cellpadding="2" cellspacing="2" class="FontTelerik">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGridAggregazioni" runat="server" AutoGenerateColumns="False"
                                Culture="it-IT" IsExporting="False" GroupPanelPosition="Top" Height="300px" Width="300px">
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="false">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                        <td style="vertical-align: top">
                            <table border="0" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnAggiungiAggregazione" runat="server" Text="Aggiungi" AutoPostBack="false"
                                            Width="104" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggiungiAggregazione', '');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnModificaAggregazione" runat="server" Text="Modifica" AutoPostBack="true"
                                            Width="104" OnClientClicking="function(sender, args){ModificaElemento(sender, args, 'RadWindowAggregazioneDettaglio', 'idSelAggr');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEliminaAggregazione" runat="server" Text="Elimina"
                                            Width="104px" OnClientClicking="function(sender, args){deleteElementTelerik(sender, args, 'idSelAggr');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadTextBox ID="RadTextBoxSelezioneAggregazione" runat="server" Width="90%"
                                ClientIDMode="Static" Style="border: none; background-color: transparent">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">
                            <telerik:RadButton ID="RadButtonEsciAgg" runat="server" Text="Esci" Width="104px"
                                AutoPostBack="true" OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowAggregazioni');}">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="idSelAggr" runat="server" Value="" ClientIDMode="Static" />
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowAggiungiAggregazione" runat="server" CenterIfModal="true"
        Modal="True" VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize"
        Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="AggiungiAggregazione">
                <table border="0" cellpadding="2" cellspacing="2" class="FontTelerik">
                    <tr>
                        <td>
                            Denominazione
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <telerik:RadTextBox ID="txtNomeAggregazione" runat="server" Width="300px" MaxLength="500">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 150px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonSalvaAggregazione" runat="server" Text="Salva">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciAggregazione" runat="server" Text="Esci" AutoPostBack="false"
                                            OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowAggiungiAggregazione', '');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowAggregazioneDettaglio" runat="server" CenterIfModal="true"
        Modal="True" VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize"
        Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="contenutoAggregazione">
                <table border="0" cellpadding="2" cellspacing="2" class="FontTelerik">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="RadGridAggregazione" runat="server" AutoGenerateColumns="False"
                                Culture="it-IT" IsExporting="False" GroupPanelPosition="Top" Height="340px" Width="400px">
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="false">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                        <td style="vertical-align: top">
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <telerik:RadButton ID="RadButtonAggregazioneComplesso" runat="server" Text="Agg. Complesso"
                                            Width="100px" AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneComplesso');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <telerik:RadButton ID="RadButtonAggregazioneEdificio" runat="server" Text="Agg. Edificio"
                                            Width="100px" AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneEdificio');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <telerik:RadButton ID="RadButtonAggregazioneScala" runat="server" Text="Agg. Scala"
                                            Width="100px" AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneScala');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <telerik:RadButton ID="RadButtonAggregazioneUnita" runat="server" Text="Agg. Unità"
                                            Width="100px" AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneUnita');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <telerik:RadButton ID="RadButtonAggregazioneImpianto" runat="server" Text="Agg. Impianto"
                                            Width="100px" AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneImpianto');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <telerik:RadButton ID="RadButtonEsciAggregazioneDettaglio" runat="server" Text="Esci"
                                Width="100px" AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioni');closeWindow(sender, args, 'RadWindowAggregazioneDettaglio', '');}">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowAggregazioneComplesso" runat="server" CenterIfModal="true"
        Modal="True" VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize"
        Skin="Web20">
        <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="2" class="FontTelerik">
                <tr>
                    <td>
                        Complesso
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="RadComboBoxAggregazioneComplesso" runat="server" EnableLoadOnDemand="true"
                            IsCaseSensitive="false" Filter="Contains" AutoPostBack="false" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 150px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="RadButtonSalvaAggregazioneComplesso" runat="server" Text="Salva">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="RadButtonEsciAggregazioneComplesso" runat="server" Text="Esci"
                                        AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');closeWindow(sender, args, 'RadWindowAggregazioneComplesso', '');}">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowAggregazioneEdificio" runat="server" CenterIfModal="true"
        Modal="True" VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize"
        Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="contenutoEdificio">
                <table border="0" cellpadding="2" cellspacing="2" class="FontTelerik">
                    <tr>
                        <td>
                            Complesso
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneEdificioComplesso" runat="server"
                                EnableLoadOnDemand="true" IsCaseSensitive="false" Filter="Contains" AutoPostBack="true"
                                Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Edificio
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneEdificio" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="false" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 120px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonSalvaAggregazioneEdificio" runat="server" Text="Salva">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciAggregazioneEdificio" runat="server" Text="Esci"
                                            AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');closeWindow(sender, args, 'RadWindowAggregazioneEdificio', '');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowAggregazioneScala" runat="server" CenterIfModal="true"
        Modal="True" VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize"
        Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="contenutoScala">
                <table border="0" cellpadding="2" cellspacing="2" class="FontTelerik">
                    <tr>
                        <td>
                            Complesso
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneScalaComplesso" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="true" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Edificio
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneScalaEdificio" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="true" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Scala
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneScala" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="false" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 60px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonSalvaAggregazioneScala" runat="server" Text="Salva">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciAggregazioneScala" runat="server" Text="Esci"
                                            AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');closeWindow(sender, args, 'RadWindowAggregazioneScala', '');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowAggregazioneUnita" runat="server" CenterIfModal="true"
        Modal="True" VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize"
        Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="contenutoUnita">
                <table border="0" cellpadding="2" cellspacing="2" class="FontTelerik">
                    <tr>
                        <td>
                            Complesso
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneUnitaComplesso" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="true" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Edificio
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneUnitaEdificio" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="true" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Unità
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneUnita" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="false" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 60px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonSalvaAggregazioneUnita" runat="server" Text="Salva">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciAggregazioneUnita" runat="server" Text="Esci"
                                            AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');closeWindow(sender, args, 'RadWindowAggregazioneUnita', '');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowAggregazioneImpianto" runat="server" CenterIfModal="true"
        Modal="True" VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize"
        Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="contenutoImpianto">
                <table border="0" cellpadding="2" cellspacing="2" class="FontTelerik">
                    <tr>
                        <td>
                            Complesso
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneImpiantoComplesso" runat="server"
                                EnableLoadOnDemand="true" IsCaseSensitive="false" Filter="Contains" AutoPostBack="true"
                                Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Impianto
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazioneImpianto" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="false" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 120px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonSalvaAggregazioneImpianto" runat="server" Text="Salva">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciAggregazioneImpianto" runat="server" Text="Esci"
                                            AutoPostBack="false" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');closeWindow(sender, args, 'RadWindowAggregazioneImpianto', '');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize" Skin="Web20">
    </telerik:RadWindow>
  <telerik:RadWindow ID="RadWindow2" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" AutoSize="true" Behavior="Pin, Move" Skin="Web20" ShowContentDuringLoad="false">
    </telerik:RadWindow>
    <asp:HiddenField ID="HFGriglia" runat="server" />
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
    </form>
</body>
</html>
