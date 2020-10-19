<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportRisultatoManuINS.aspx.vb"
    Inherits="ReportRisultatoManuINS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report</title>
    <script language="javascript" type="text/javascript">
        // <!CDATA[



        // ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <script type="text/javascript">
        window.onresize = ResizeGrid;
        Sys.Application.add_load(ResizeGrid);
        function ResizeGrid() {
            var scrollArea = document.getElementById("<%= DataGrid1.ClientID %>" + "_GridData");
            scrollArea.style.height = window.screen.height - 750 + 'px';
        };
    </script>
    <div>
        <table width="100%">
            <tr>
                <td>
                    <img src="../../../../IMG/logo.gif" style="z-index: 100; left: 0px; position: static;
                        top: 0px" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 21px">
                    <span style="font-size: 10pt"><strong>Settore Manutenzioni</strong></span>
                </td>
                <td style="height: 21px">
                </td>
                <td style="height: 21px">
                </td>
            </tr>
        </table>
    </div>
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td>
                <strong><span style="font-size: 14pt">
                    <asp:Label ID="lblTitolo" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px; font-size: 11pt; font-family: Arial;" Text="Label" Width="100%" BorderStyle="Solid"
                        BorderWidth="1px"></asp:Label></span></strong>
            </td>
        </tr>
        <tr>
            <td width="90%">
                <asp:Panel runat="server" ID="PanelRadGrid">
                    <telerik:RadGrid ID="DataGrid1" runat="server" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                        Culture="it-IT" RegisterWithScriptManager="False" EnableLinqExpressions="False"
                        Width="100%" AllowSorting="True" IsExporting="False" AllowPaging="true">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            HierarchyLoadMode="ServerOnDemand">
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
                                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPERTORIO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE_APPALTI" HeaderText="DESCRIZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="15%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO">
                                    <HeaderStyle Width="15%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESC_EDIFICIO" HeaderText="DESCRIZIONE EDIFICIO">
                                    <HeaderStyle Width="15%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO EDIFICIO">
                                    <HeaderStyle Width="20%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE_LOTTO" HeaderText=" LOTTO">
                                    <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO">
                                    <HeaderStyle Width="15%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO_VOCE" HeaderText="VOCE DGR">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
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
                    </telerik:RadGrid>
                    <telerik:RadGrid ID="DataGrid2" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
                        IsExporting="False" AllowPaging="True" Style="z-index: 101; left: 0px; top: 8px;
                        table-layout: auto; clip: rect(auto auto auto auto); direction: ltr; border-collapse: separate;
                        position: absolute;">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top" HierarchyLoadMode="ServerOnDemand">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
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
                                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPERTORIO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE_APPALTI" HeaderText="DESCRIZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO">
                                    <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESC_EDIFICIO" HeaderText="DESCRIZIONE EDIFICIO">
                                    <HeaderStyle Width="15%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO EDIFICIO">
                                    <HeaderStyle Width="20%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESC_IMPIANTO" HeaderText="IMPIANTO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TIPO_IMPIANTO" HeaderText="TIPO IMPIANTO">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COD_IMPIANTO" HeaderText="COD. IMPIANTO">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DETTAGLIO_IMPIANTO" HeaderText="Num. MATRICOLA e/o Num. IMPIANTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="10%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE_LOTTO" HeaderText=" LOTTO">
                                    <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO">
                                    <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO_VOCE" HeaderText="VOCE DGR">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="10%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
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
                    </telerik:RadGrid>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 14pt">
                    <asp:Label ID="lblTotale" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="font-size: 11pt;
                        z-index: 100; left: 0px; width: 100%; font-family: Arial; position: static; top: 0px;
                        text-align: left" Text="Label" Width="736px"></asp:Label></span></strong>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
