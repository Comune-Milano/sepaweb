<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPagamenti.aspx.vb"
    Inherits="PAGAMENTI_CANONE_RicercaPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<script language="javascript" type="text/javascript">


   
</script>
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>RICERCA</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:radscriptmanager id="RadScriptManager1" runat="server">
    </telerik:radscriptmanager>
    <telerik:radajaxmanager id="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Web20">
    </telerik:radajaxloadingpanel>
    <telerik:radwindowmanager id="RadWindowManager1" runat="server" localization-ok="Ok"
        localization-cancel="Annulla">
    </telerik:radwindowmanager>
    <div style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">
                     Ordini - Pagamenti a canone - Da approvare
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:radbutton id="btnCerca" runat="server" text="Procedi" tooltip="Procedi" />
                            </td>
                            <td>
                                <telerik:radbutton id="btnAnnulla" runat="server" text="Esci" tooltip="Home" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%; overflow: auto">
                        <telerik:radgrid id="DataGrid1" runat="server" grouppanelposition="Top" resolvedrendermode="Classic"
                            autogeneratecolumns="False" culture="it-IT" registerwithscriptmanager="False"
                            pagerstyle-visible="true" pagerstyle-alwaysvisible="true" allowpaging="true"
                            allowfilteringbycolumn="True" enablelinqexpressions="False" width="99%" allowsorting="True"
                            pagesize="100" isexporting="False">
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
                                    <telerik:GridBoundColumn DataField="ID_FORNITORE" HeaderText="ID_FORNITORE" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_GRUPPO" HeaderText="ID_APPALTO" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="COD_FORNITORE" HeaderText="COD. FORNITORE" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="10%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="50%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPERTORIO"
                                        FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="40%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
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
                        </telerik:radgrid>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        CssClass="txtMia" Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100"
                        ReadOnly="True" Width="752px">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="txtID_APPALTO" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtID_FORNITORE" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="HFGriglia" runat="server" />
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
    </form>
</body>
</html>
