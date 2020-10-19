<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DestinazioniUsoRL.aspx.vb"
    Inherits="CENSIMENTO_DestinazioniUsoRL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>Gestione Destinazioni d'uso RL</title>
    <style type="text/css">
        .rgAltRow, .rgRow
        {
            cursor: pointer !important;
        }
        .sfondo
        {
            /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#e8e8e8+0,fefefe+100 */
            background: rgb(232,232,232); /* Old browsers */
            background: -moz-linear-gradient(top, rgba(232,232,232,1) 0%, rgba(254,254,254,1) 100%); /* FF3.6-15 */
            background: -webkit-linear-gradient(top, rgba(232,232,232,1) 0%,rgba(254,254,254,1) 100%); /* Chrome10-25,Safari5.1-6 */
            background: linear-gradient(to bottom, rgba(232,232,232,1) 0%,rgba(254,254,254,1) 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#e8e8e8', endColorstr='#fefefe',GradientType=0 ); /* IE6-9 */
            background-size: 100% 1000px;
        }
        
        .TitoloModulo
        {
            font-size: 15pt;
            color: #1c2466;
            font-family: Segoe UI;
            vertical-align: middle;
            font-weight: bold;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function setDimensioni() {
            var griglie = document.getElementById('HFGriglia').value;
            var altezzaPagina = myHeight = window.innerHeight;
            if (document.getElementById('MyTab')) {
                var tabs = document.getElementById('HFTAB').value;
                var tab = tabs.split(",");
                if (tab.length != 0) {
                    for (i = 0; i < tab.length; i++) {
                        document.getElementById(tab[i]).style.height = altezzaPagina - 320 + 'px';
                    };
                };
            }
            if (griglie != '') {
                var griglia = griglie.split(",");
                if (document.getElementById('MyTab')) {
                    //Griglie nei tab (Nei tab va definito sempre il div MyTab)
                    for (i = 0; i < griglia.length; i++) {
                        document.getElementById(griglia[i]).style.height = altezzaPagina - 320 + 'px';
                    }
                } else {
                    //Griglie fuori dai tab
                    for (i = 0; i < griglia.length; i++) {
                        if (document.getElementById(griglia[i])) {
                            document.getElementById(griglia[i]).style.height = altezzaPagina - 200 + 'px';
                        };
                    }
                };
            }
        };
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelView">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelView" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PanelTest">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelTest" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PanelDestUso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelDestUso" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAbbinamento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelTest" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="Buttons"
        DecorationZoneID="decorationZone"></telerik:RadFormDecorator>
    <asp:Panel runat="server" ID="PanelView" Style="height: 100%">
        <div id="decorationZone">
            <telerik:RadMultiPage ID="MultiViewRicerca" runat="server" SelectedIndex="0">
                <telerik:RadPageView ID="ViewGestioneDestUsoRL" runat="server">
                    <div>
                        <table style="width: 100%;">
                            <tr>
                                <td class="TitoloModulo">
                                    <asp:Label ID="Label1" Text="Gestione Destinazioni d'uso RL" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAbbinamento" runat="server" Text="Abbina" Style="cursor: pointer" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnVisualizzaUnita" runat="server" Text="Visualizza unità" Style="cursor: pointer" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="PanelTest">
                                        <telerik:RadGrid ID="dgvDestUsoRL" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                            PageSize="100" AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%"
                                            AllowSorting="True" IsExporting="False" AllowPaging="True" PagerStyle-AlwaysVisible="true">
                                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                                CommandItemDisplay="Top">
                                                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                                    ShowRefreshButton="true" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="id" HeaderText="ID" Visible="False">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="False" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridButtonColumn HeaderStyle-Width="50px" CommandName="Delete" Text="Elimina"
                                                        UniqueName="DeleteColumn" ButtonType="ImageButton">
                                                        <ItemStyle Width="24px" Height="24px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <CommandItemTemplate>
                                                    <div style="display: inline-block; width: 100%;">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <a style="cursor: pointer" onclick="document.getElementById('RadWindowDestUsoRL_C_txtDestUsoRL').value = '';openWindow(null, null, 'RadWindowDestUsoRL')">
                                                                        <img style="border: 0px" alt="" src="IMMCENSIMENTO/addRecord.gif" />&nbsp;&nbsp;Aggiungi
                                                                        nuovo record</a>
                                                                </td>
                                                                <td>
                                                                    <div style="float: right; padding: 4px;">
                                                                        <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                                                            CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcel" Text="text" runat="server"
                                                                                OnClick="Esporta_Click" CommandName="ExportToExcel" CssClass="rgExpXLS" /></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </CommandItemTemplate>
                                            </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                            <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                                <Excel FileExtension="xls" Format="Xlsx" />
                                            </ExportSettings>
                                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
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
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblErrore" runat="server" Visible="False" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="10pt" ForeColor="Red" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <telerik:RadWindow ID="RadWindowDestUsoRL" runat="server" CenterIfModal="true" Modal="true"
                        CssClass="sfondo" Title="Gestione Destinazioni d'uso RL" Width="630px" Height="140px"
                        VisibleStatusbar="false" Behaviors="Pin, Maximize, Move, Resize">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="PanelDestUso" Style="height: 100%;" class="sfondo">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="TitoloModulo">
                                            <asp:Label ID="Label2" Text="Gestione Destinazioni d'uso RL" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <telerik:RadButton ID="btnSalvaDestUsoRL" runat="server" Text="Salva" ToolTip="Salva" />
                                                    </td>
                                                    <td>
                                                        &#160;&#160;
                                                    </td>
                                                    <td style="text-align: right">
                                                        <telerik:RadButton ID="btnChiudiDestUsoRL" runat="server" Text="Esci" ToolTip="Esci"
                                                            OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowDestUsoRL');}" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 25%">
                                                        Destinazione d&apos;uso RL*
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtDestUsoRL" runat="server" Width="90%">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </telerik:RadPageView>
                <telerik:RadPageView ID="ViewAbbinamentoDestUsoRL" runat="server">
                    <div>
                        <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
                            EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="BottomRight"
                            OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
                        </telerik:RadNotification>
                    </div>
                    <div>
                        <table style="width: 100%">
                            <tr>
                                <td class="TitoloModulo">
                                    <asp:Label ID="Label3" Text="Abbinamento destinazione d'uso RL" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnIndietro" runat="server" Text="Indietro" Style="cursor: pointer" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Button ID="btnConfermaAbbinamento" runat="server" Text="Conferma Abbinamento"
                                                    Style="cursor: pointer" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDestinazioneRL" runat="server" Font-Size="8pt" Font-Names="Segoe UI"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="Panel1" Style="width: 100%">
                                        <telerik:RadGrid ID="dgvUnitaImmobiliari" runat="server" GroupPanelPosition="Top"
                                            AllowPaging="true" PagerStyle-AlwaysVisible="true" PageSize="50" ResolvedRenderMode="Classic"
                                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                            AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                            IsExporting="False">
                                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                                Width="200%" CommandItemDisplay="Top">
                                                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                                    ShowRefreshButton="true" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="SELEZIONA" AllowFiltering="true" ShowFilterIcon="true">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="13%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false" 
                                                                Checked='<%# DataBinder.Eval(Container,"DataItem.CHECKALL") %>' /></ItemTemplate>
                                                        <FilterTemplate>
                                                            <div style="width: 100%; text-align: center;">
                                                                <asp:Button ID="chkSelTutti" runat="server" AutoPostBack="true" OnClick="chkSelTutti_CheckedChanged"
                                                                    Text="SELEZIONA TUTTI" />
                                                            </div>
                                                        </FilterTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="COD_UI" HeaderText="CODICE UNITA'" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="15%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIPO_CONTRATTO" HeaderText="TIPO CONTRATTO" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="17%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                        <FilterTemplate>
                                                            <telerik:RadComboBox ID="RadComboBoxFiltroTipoContratto" Width="100%" AppendDataBoundItems="true"
                                                                runat="server" OnClientSelectedIndexChanged="FilterTipoContrattoIndexChanged"
                                                                ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                                LoadingMessage="Caricamento...">
                                                            </telerik:RadComboBox>
                                                            <telerik:RadScriptBlock ID="RadScriptBlockTipoContratto" runat="server">
                                                                <script type="text/javascript">
                                                                    function FilterTipoContrattoIndexChanged(sender, args) {
                                                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                        var filtro = args.get_item().get_value();
                                                                        document.getElementById('HFFiltrotipocontratto').value = filtro;
                                                                        if (filtro != 'Tutti') {
                                                                            tableView.filter("TIPO_CONTRATTO", filtro, "EqualTo");
                                                                        } else {
                                                                            tableView.filter("TIPO_CONTRATTO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                        };
                                                                    };
                                                                </script>
                                                            </telerik:RadScriptBlock>
                                                        </FilterTemplate>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIPO_SPECIFICO" HeaderText="TIPO SPECIFICO" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="16%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                        <FilterTemplate>
                                                            <telerik:RadComboBox ID="RadComboBoxFiltroTipoContrattoSpecifico" Width="100%" AppendDataBoundItems="true"
                                                                runat="server" OnClientSelectedIndexChanged="FilterTipoContrattoSpecificoIndexChanged"
                                                                ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                                LoadingMessage="Caricamento...">
                                                            </telerik:RadComboBox>
                                                            <telerik:RadScriptBlock ID="RadScriptBlockTipoContrattoSpecifico" runat="server">
                                                                <script type="text/javascript">
                                                                    function FilterTipoContrattoSpecificoIndexChanged(sender, args) {
                                                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                        var filtro = args.get_item().get_value();
                                                                        document.getElementById('HFFiltrotipocontrattoSpecifico').value = filtro;
                                                                        if (filtro != 'Tutti') {
                                                                            tableView.filter("TIPO_SPECIFICO", filtro, "EqualTo");
                                                                        } else {
                                                                            tableView.filter("TIPO_SPECIFICO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                        };
                                                                    };
                                                                </script>
                                                            </telerik:RadScriptBlock>
                                                        </FilterTemplate>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="STATO_CONTRATTO" HeaderText="STATO" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="16%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                        <FilterTemplate>
                                                            <telerik:RadComboBox ID="RadComboBoxFiltroStatoContratto" Width="100%" AppendDataBoundItems="true"
                                                                runat="server" OnClientSelectedIndexChanged="FilterStatoContrattoIndexChanged"
                                                                ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                                LoadingMessage="Caricamento...">
                                                            </telerik:RadComboBox>
                                                            <telerik:RadScriptBlock ID="RadScriptBlockStatoContratto" runat="server">
                                                                <script type="text/javascript">
                                                                    function FilterStatoContrattoIndexChanged(sender, args) {
                                                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                        var filtro = args.get_item().get_value();
                                                                        document.getElementById('HFFiltroStatoContratto').value = filtro;
                                                                        if (filtro != 'Tutti') {
                                                                            tableView.filter("STATO_CONTRATTO", filtro, "EqualTo");
                                                                        } else {
                                                                            tableView.filter("STATO_CONTRATTO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                        };
                                                                    };
                                                                </script>
                                                            </telerik:RadScriptBlock>
                                                        </FilterTemplate>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DESTINAZIONE_USO_RL" HeaderText="DESTINAZIONE D'USO RL"
                                                        AutoPostBackOnFilter="true" FilterControlWidth="80%" CurrentFilterFunction="Contains"
                                                        DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="23%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                        <FilterTemplate>
                                                            <telerik:RadComboBox ID="RadComboBoxFiltroTipoDestinazioneRL" Width="100%" AppendDataBoundItems="true"
                                                                runat="server" OnClientSelectedIndexChanged="FilterDestUsoRLIndexChanged" ResolvedRenderMode="Classic"
                                                                HighlightTemplatedItems="true" Filter="Contains" LoadingMessage="Caricamento...">
                                                            </telerik:RadComboBox>
                                                            <telerik:RadScriptBlock ID="RadScriptBlockTipoDestinazioneRL" runat="server">
                                                                <script type="text/javascript">
                                                                    function FilterDestUsoRLIndexChanged(sender, args) {
                                                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                        var filtro = args.get_item().get_value();
                                                                        document.getElementById('HFFiltroDestUsoRL').value = filtro;
                                                                        if (filtro != 'Tutti') {
                                                                            tableView.filter("DESTINAZIONE_USO_RL", filtro, "EqualTo");
                                                                        } else {
                                                                            tableView.filter("DESTINAZIONE_USO_RL", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                        };
                                                                    };
                                                                </script>
                                                            </telerik:RadScriptBlock>
                                                        </FilterTemplate>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIPOLOGIA_UI" HeaderText="TIPOLOGIA UNITA'" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="18%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                        <FilterTemplate>
                                                            <telerik:RadComboBox ID="RadComboBoxFiltroTipologiaUI" Width="100%" AppendDataBoundItems="true"
                                                                runat="server" OnClientSelectedIndexChanged="FilterTipologiaUIIndexChanged" ResolvedRenderMode="Classic"
                                                                HighlightTemplatedItems="true" Filter="Contains" LoadingMessage="Caricamento...">
                                                            </telerik:RadComboBox>
                                                            <telerik:RadScriptBlock ID="RadScriptBlockTipologiaUI" runat="server">
                                                                <script type="text/javascript">
                                                                    function FilterTipologiaUIIndexChanged(sender, args) {
                                                                        var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                        var filtro = args.get_item().get_value();
                                                                        document.getElementById('HFFiltroTipologiaUI').value = filtro;
                                                                        if (filtro != 'Tutti') {
                                                                            tableView.filter("TIPOLOGIA_UI", filtro, "EqualTo");
                                                                        } else {
                                                                            tableView.filter("TIPOLOGIA_UI", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                        };
                                                                    };
                                                                </script>
                                                            </telerik:RadScriptBlock>
                                                        </FilterTemplate>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="18%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CIVICO" HeaderText="CIVICO" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="8%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="COMUNE_UNITA" HeaderText="COMUNE" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="13%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="INTERNO" HeaderText="INTERNO" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="10%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="8%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PIANO" HeaderText="PIANO" AutoPostBackOnFilter="true"
                                                        FilterControlWidth="80%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Width="13%" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" Wrap="true" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <PagerStyle AlwaysVisible="True" />
                                                <CommandItemTemplate>
                                                    <div style="display: inline-block; width: 100%;">
                                                        <div style="float: right; padding: 4px;">
                                                            <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                                                CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcel" Text="text" runat="server"
                                                                    OnClick="EsportaUnita_Click" CommandName="ExportToExcel" CssClass="rgExpXLS" /></div>
                                                    </div>
                                                </CommandItemTemplate>
                                            </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                            <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                                <Excel FileExtension="xls" Format="Xlsx" />
                                            </ExportSettings>
                                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="300px" />
                                                <Selecting AllowRowSelect="True" />
                                                <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                                    AllowResizeToFit="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Visible="False" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="10pt" ForeColor="Red" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
            <asp:HiddenField ID="HiddenFieldIdDestUsoRL" runat="server" Value="-1" />
            <asp:HiddenField ID="HiddenFieldDescrizioneDestUsoRL" runat="server" Value="-1" />
            <asp:HiddenField ID="HFGriglia" runat="server" />
            <asp:HiddenField ID="HiddenCheck" runat="server" Value="0" />
            <asp:HiddenField ID="HiddenValoriCheck" runat="server" Value="0" />
            <asp:HiddenField ID="HFFiltrotipocontratto" runat="server" Value="Tutti" ClientIDMode="Static" />
            <asp:HiddenField ID="HFFiltrotipocontrattoSpecifico" runat="server" Value="Tutti"
                ClientIDMode="Static" />
            <asp:HiddenField ID="HFFiltroStatoContratto" runat="server" Value="Tutti" ClientIDMode="Static" />
            <asp:HiddenField ID="HFFiltroDestUsoRL" runat="server" Value="Tutti" ClientIDMode="Static" />
            <asp:HiddenField ID="HFFiltroTipologiaUI" runat="server" Value="Tutti" ClientIDMode="Static" />
            <asp:HiddenField ID="isExporting" runat="server" Value="0" />
        </div>
    </asp:Panel>
    </form>
</body>
<script type="text/javascript">
    window.onresize = setDimensioni;
    Sys.Application.add_load(setDimensioni);
    function setDimensioni() {
        var griglie = document.getElementById('HFGriglia').value;
        var altezzaPagina = myHeight = window.innerHeight;
        if (document.getElementById('MyTab')) {
            var tabs = document.getElementById('HFTAB').value;
            var tab = tabs.split(",");
            if (tab.length != 0) {
                for (i = 0; i < tab.length; i++) {
                    document.getElementById(tab[i]).style.height = altezzaPagina - 320 + 'px';
                };
            };
        }
        if (griglie != '') {
            var griglia = griglie.split(",");
            if (document.getElementById('MyTab')) {
                //Griglie nei tab (Nei tab va definito sempre il div MyTab)
                for (i = 0; i < griglia.length; i++) {
                    document.getElementById(griglia[i]).style.height = altezzaPagina - 320 + 'px';
                }
            } else {
                //Griglie fuori dai tab
                for (i = 0; i < griglia.length; i++) {

                    if (document.getElementById(griglia[i])) {
                        document.getElementById(griglia[i]).style.height = altezzaPagina - 200 + 'px';

                    };
                }
            };
        }
    };
</script>
</html>
