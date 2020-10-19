<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/OpenPage.master" AutoEventWireup="false" CodeFile="EdificiNonValorizzati.aspx.vb" Inherits="FORNITORI_EdificiNonValorizzati" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        if (window.attachEvent) {
            window.attachEvent("onload", initDialog);
        }
        else if (window.addEventListener) {
            window.addEventListener("load", initDialog, false);
        }

        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function initDialog() {
            getRadWindow().maximize();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Lista degli edifici non valorizzati"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="RadGridEdifici" runat="server" GroupPanelPosition="Top" AllowPaging="true"
        PagerStyle-AlwaysVisible="true" PageSize="50" AutoGenerateColumns="TRUE" Culture="it-IT"
        AllowFilteringByColumn="True" EnableLinqExpressions="False" AllowSorting="True"
        IsExporting="False" ShowFooter="true">
        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
            Width="100%"
            AllowMultiColumnSorting="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="false" ShowExportToWordButton="false"
                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                ShowRefreshButton="true" />
            <HeaderStyle HorizontalAlign="Center" Wrap="true" />
            <PagerStyle AlwaysVisible="True" />
            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <ExportSettings OpenInNewWindow="true" IgnorePaging="true" ExportOnlyData="true"
            HideStructureColumns="true">
            <Excel FileExtension="xlsx" Format="Xlsx" />
        </ExportSettings>
        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 250;
            var larghezzaRad = $(window).width() - 47;
            $("#OpenPage_CPContenuto_RadGridEdifici").width(larghezzaRad);
            $("#OpenPage_CPContenuto_RadGridEdifici").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }
    </script>
</asp:Content>

