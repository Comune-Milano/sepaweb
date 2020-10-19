<%@ Page Title="Elenco Report" Language="VB" MasterPageFile="~/Contratti/Spalmatore/HomePage.master"
    AutoEventWireup="false" CodeFile="VisualizzazioneRpt.aspx.vb" Inherits="Contratti_Spalmatore_VisualizzazioneRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Elenco Report"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" AutoPostBack="true">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="RadGridReport" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" AllowAutomaticDeletes="True" 
        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
       IsExporting="False" AllowPaging="True" PageSize="100">
        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
            CommandItemDisplay="Top" DataKeyNames="ID">
            <CommandItemSettings ShowExportToExcelButton="False" ShowExportToWordButton="false"
                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                ShowRefreshButton="true" />
            <Columns>
                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPO_ESTRAZIONE" HeaderText="TIPO ESTRAZIONE"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="INIZIO" HeaderText="INIZIO" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FINE" HeaderText="FINE" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ESITO" HeaderText="STATO" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ERRORE" HeaderText="ERRORE" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NOMEFILE" HeaderText="NOME FILE" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn ConfirmText="Eliminare la riga selezionata?" ConfirmDialogType="RadWindow"
                    ConfirmTitle="Elimina" HeaderText="" HeaderStyle-Width="50px" CommandName="Delete"
                    UniqueName="DeleteColumn" ButtonType="ImageButton" Exportable="false">
                    <HeaderStyle Width="50px"></HeaderStyle>
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <Selecting AllowRowSelect="True" />
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                AllowResizeToFit="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="180" ClientIDMode="Static" />
</asp:Content>
