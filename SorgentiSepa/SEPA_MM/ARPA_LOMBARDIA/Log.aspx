<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="Log.aspx.vb" Inherits="ARPA_LOMBARDIA_Log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Log Elaborazioni"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="TornaHome"
        AutoPostBack="false">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridLog">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridLog" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="RadGridLog" runat="server" AllowSorting="false" AutoGenerateColumns="False"
        AllowFilteringByColumn="false" EnableLinqExpressions="False" IsExporting="False"
        Width="97%" AllowPaging="true" PageSize="20">
        <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="Nessun dato da visualizzare."
            ShowHeadersWhenNoRecords="true" TableLayout="Auto" AllowMultiColumnSorting="true"
            CommandItemSettings-ShowAddNewRecordButton="false">
            <Columns>
                <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA & ORA" SortExpression="VALORE_ORDER">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE LOG">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="DATA_ORA_ORDER" SortOrder="Descending" />
            </SortExpressions>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="120" ClientIDMode="Static" />
</asp:Content>
