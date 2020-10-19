<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master" AutoEventWireup="false" CodeFile="ElencoSegnaposto.aspx.vb" Inherits="Gestione_locatari_ElencoSegnaposto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Elenco Segnaposto Utilizzabili"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="dgvMarcatori" runat="server" AutoGenerateColumns="False"
        AllowFilteringByColumn="true" EnableLinqExpressions="False" IsExporting="False"
        Width="97%" AllowPaging="true" PageSize="100">
        <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
            NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
            TableLayout="Auto">
            <CommandItemSettings ShowAddNewRecordButton="False" />
            <Columns>
                <telerik:GridBoundColumn DataField="COD" HeaderText="COD" FilterControlWidth="90%"
                    CurrentFilterFunction="Contains" ShowFilterIcon="True" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" FilterControlWidth="90%"
                    CurrentFilterFunction="Contains" ShowFilterIcon="True" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>

