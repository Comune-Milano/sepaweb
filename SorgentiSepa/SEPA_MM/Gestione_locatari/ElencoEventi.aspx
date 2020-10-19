<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master" AutoEventWireup="false" CodeFile="ElencoEventi.aspx.vb" Inherits="Gestione_locatari_ElencoEventi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Gestione Eventi"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="dgvEventi" runat="server" AutoGenerateColumns="False"
        AllowFilteringByColumn="true" EnableLinqExpressions="False" IsExporting="False"
        Width="97%" AllowPaging="true" PageSize="100">
        <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
            NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
            TableLayout="Auto">
            <CommandItemSettings ShowAddNewRecordButton="False" />
            <Columns>
                <telerik:GridBoundColumn DataField="ID_OPERATORE" HeaderText="ID_OPERATORE" Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DATA_ORA" HeaderText="DATA & ORA"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" CurrentFilterFunction="EqualTo" ShowFilterIcon="true"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true" SortExpression="DATA_ORA">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" FilterControlWidth="90%"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE" FilterControlWidth="90%"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE" FilterControlWidth="90%"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true" AutoPostBackOnFilter="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="DATA_ORA" SortOrder="Descending" />
            </SortExpressions>
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

