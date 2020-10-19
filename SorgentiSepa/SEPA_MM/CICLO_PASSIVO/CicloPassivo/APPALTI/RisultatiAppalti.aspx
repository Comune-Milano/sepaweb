<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiAppalti.aspx.vb"
    Inherits="MANUTENZIONI_RisultatiAppalti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
</script>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Contratti - Ricerca</title>
    <script type="text/javascript">
        var Selezionato;
        function ApriAppaltoSelezionato() {
            if (document.getElementById('btnVisualizza') != null) {
                document.getElementById('btnVisualizza').click();
            };
        };
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server"  onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DataGrid3">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGrid3" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">Contratti - Ricerca
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
                    <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%; height: 100%">
                        <telerik:RadGrid ID="DataGrid3" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                            AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                            PageSize="100" IsExporting="False">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"

                                CommandItemDisplay="Top">
                                <SortExpressions>
                                    <telerik:GridSortExpression FieldName="REP_ORD" SortOrder="Descending" />
                                </SortExpressions>
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
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUMERO REPERTORIO" FilterControlWidth="85%" SortExpression="REP_ORD"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" DataFormatString="{0:@}" FilterControlWidth="85%">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_REPERTORIO" HeaderText="DATA REPERTORIO"
                                        DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO CONTRATTO" DataFormatString="{0:dd/MM/yyyy}"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_FINE" HeaderText="DATA FINE CONTRATTO" DataFormatString="{0:dd/MM/yyyy}"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="true" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOT_CANONE" HeaderText="TOTALE CONTRATTUALE A CANONE" AutoPostBackOnFilter="true"
                                        DataFormatString="{0:C2}" FilterControlWidth="85%" CurrentFilterFunction="EqualTo">
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="true" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOT_CONSUMO" HeaderText="TOTALE CONTRATTUALE A CONSUMO" AutoPostBackOnFilter="true"
                                        FilterControlWidth="85%" CurrentFilterFunction="EqualTo" DataFormatString="{0:C2}">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="true" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_LOTTO" HeaderText="ID_LOTTO" Visible="False">
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
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Text="Label" Visible="False" Width="624px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="FontTelerik">
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        CssClass="txtMia" Width="100%" ReadOnly="True">Nessuna selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtid" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        Style="visibility: hidden" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
                        Visible="false" BorderStyle="None" MaxLength="100" Width="5px" Height="8px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBLID" runat="server" Height="21px" Style="visibility: hidden" Visible="False"
                        Width="78px">Label</asp:Label>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="isExporting" runat="server" Value="0" />
        <asp:HiddenField ID="idLotto" runat="server" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
</body>
<script language="javascript" type="text/javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
</script>
</html>
