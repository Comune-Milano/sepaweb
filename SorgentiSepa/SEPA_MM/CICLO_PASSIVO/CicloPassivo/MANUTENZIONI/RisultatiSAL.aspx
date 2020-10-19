<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiSAL.aspx.vb" Inherits="MANUTENZIONI_RisultatiSAL" %>

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
    <style type="text/css">
        .style1 {
            font-size: 15pt;
            color: #1c2466;
            font-family: Arial;
            vertical-align: middle;
            font-weight: bold;
            text-align: center;
            height: 28px;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server"
        onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DataGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="txtmia" LoadingPanelID="RadAjaxLoadingPanel2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1">
        </telerik:RadAjaxLoadingPanel>
         <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" Transparency="100" > 
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <div style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td class="TitoloModulo">Ordini - Manutenzioni e servizi - SAL - Nuovo
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnProcedi" runat="server" Text="Procedi" ToolTip="Visualizza scheda SAL" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnRicerca" runat="server" Text="Nuova ricerca" ToolTip="Nuova Ricerca"
                                        Style="top: 0px; left: 0px" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnHome" runat="server" Text="Esci" ToolTip="Home" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AllowPaging="FALSE" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                            IsExporting="False">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
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
                                    <telerik:GridTemplateColumn HeaderText="ODL/ANNO" UniqueName="ODL" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemTemplate>
                                            <div style="text-align: center;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadButton ID="CheckBox1" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                                OnCheckedChanged="CheckBox1_CheckedChanged" OnClientCheckedChanged="function(sender, args){ nascondi = 0;}" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ODL_ANNO") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                        <FilterTemplate>
                                            <div style="width: 100%; text-align: center;">
                                                <telerik:RadButton ID="chkSelTutti" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                    AutoPostBack="true" OnClientCheckedChanged="selezionaTutti" OnClick="ButtonSelAll_Click" />
                                            </div>
                                        </FilterTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ID_MANUTENZIONE" HeaderText="ID_MANUTENZIONE"
                                        Visible="False">
                                        <HeaderStyle Width="0%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_INIZIO_ORDINE" HeaderText="DATA" AllowFiltering="false">
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Width="10%" Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="UBICAZIONE" HeaderText="UBICAZIONE" AutoPostBackOnFilter="true"
                                        FilterControlWidth="95%" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Width="15%" Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO" AutoPostBackOnFilter="true"
                                        FilterControlWidth="95%" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Width="25%" Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SERVIZIO_VOCI" HeaderText="VOCE DGR" AutoPostBackOnFilter="true"
                                        FilterControlWidth="95%" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="25%" Font-Bold="false" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="False">
                                        <HeaderStyle Width="0%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PROGR" HeaderText="ODL_NUM" Visible="False">
                                        <HeaderStyle Width="0%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO" Visible="False">
                                        <HeaderStyle Width="0%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_PRENOTAZIONE_PAGAMENTO" HeaderText="ID_PRENOTAZIONE_PAGAMENTO"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                                          </MasterTableView>
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                <Excel FileExtension="xls" Format="Xlsx" />
                            </ExportSettings>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                                ClientEvents-OnCommand="onCommand">
                                <ClientEvents OnCommand="onCommand"></ClientEvents>
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
                        <asp:Label ID="txtmia" runat="server" CssClass="txtMia" Width="768px">Nessuna selezione</asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="isExporting" runat="server" Value="0" />
        <asp:HiddenField ID="hiddenSelTutti" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        function selezionaTutti(sender, args) {
            nascondi = 0;
            if (sender._checked)
                document.getElementById('hiddenSelTutti').value = "1";
            else
                document.getElementById('hiddenSelTutti').value = "0";
        };
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
