<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false"
    CodeFile="ReportConsuntivati.aspx.vb" Inherits="FORNITORI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Modifica(sender, args) {
                validNavigation = false;
            }

            function Uscita() {
                validNavigation = true;
                location.href = 'Home.aspx';
            }

        </script>
    </telerik:RadCodeBlock>
    <table cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <telerik:RadButton ID="btnExport" runat="server" Text="Esporta in Excel" ToolTip="Esporta i risultati in Excel">
                </telerik:RadButton>
            </td>
            <td>&nbsp;
            </td>
            <td>
                <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
                    AutoPostBack="False" CausesValidation="False" OnClientClicking="Uscita">
                </telerik:RadButton>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="RadGridRPTConsuntivi" runat="server" AllowSorting="True"
        ResolvedRenderMode="Classic" ShowGroupPanel="True" AutoGenerateColumns="False"
        PageSize="300" Culture="it-IT" RegisterWithScriptManager="False" AllowPaging="True"
        IsExporting="False" Width="99%" AllowFilteringByColumn="True">
        <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
        <ExportSettings FileName="Export_Consuntivati" IgnorePaging="True" OpenInNewWindow="True"
            ExportOnlyData="True" HideStructureColumns="True">
            <Pdf PageWidth="">
            </Pdf>
            <Excel Format="Xlsx" />
            <Csv ColumnDelimiter="Semicolon" EncloseDataWithQuotes="False" />
        </ExportSettings>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
            <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
        </ClientSettings>
        <MasterTableView>
            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToPdfButton="false"
                ShowRefreshButton="False" />
            <Columns>
                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" HeaderStyle-Width="20%">
                    <HeaderStyle Width="20%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NUM_ODL" HeaderText="NUMERO ODL" HeaderStyle-Width="5%">
                    <HeaderStyle Width="8%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="REPERTORIO" HeaderStyle-Width="5%">
                    <HeaderStyle Width="8%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DESCR_MA" HeaderText="OGGETTO INTERVENTO" HeaderStyle-Width="5%">
                    <HeaderStyle Width="32%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_PGI" HeaderText="DATA PGI" HeaderStyle-Width="5%" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                    ShowFilterIcon="true" CurrentFilterFunction="EqualTo">
                    <HeaderStyle Width="8%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_TDL" HeaderText="DATA TDL" HeaderStyle-Width="5%" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                    ShowFilterIcon="true" CurrentFilterFunction="EqualTo">
                    <HeaderStyle Width="8%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_FINE_INTERVENTO" HeaderText="DATA FINE LAVORI" HeaderStyle-Width="5%" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                    ShowFilterIcon="true" CurrentFilterFunction="EqualTo">
                    <HeaderStyle Width="8%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NON_CONFORMITA" HeaderText=" NON CONFORMITA" HeaderStyle-Width="5%">
                    <HeaderStyle Width="8%"></HeaderStyle>
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="DATA_FINE_INTERVENTO" SortOrder="Ascending" />
            </SortExpressions>
            <PagerStyle AlwaysVisible="True" />
        </MasterTableView>
        <ClientSettings AllowDragToGroup="True">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" />
    </telerik:RadGrid>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenField1" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivHeight" Value="100" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivWidth" Value="1" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="Modificato" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="isExporting" />
    <script type="text/javascript">
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 200;
            var larghezzaRad = $(window).width() - 27;
            $("#MasterPage_CPContenuto_RadGridRPTConsuntivi").width(larghezzaRad);
            $("#MasterPage_CPContenuto_RadGridRPTConsuntivi").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }
    </script>
    <script src="../Standard/Scripts/jsfunzioniExit.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <p>
        &nbsp;
    </p>
</asp:Content>
