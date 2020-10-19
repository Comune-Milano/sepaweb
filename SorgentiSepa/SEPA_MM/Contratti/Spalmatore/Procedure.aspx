<%@ Page Title="Elenco Elaborazioni" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="Procedure.aspx.vb" Inherits="Contratti_Spalmatore_Procedure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Elenco Elaborazioni"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci"
        AutoPostBack="true">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="dgvProcedure" runat="server" GroupPanelPosition="Top" resolvedrendermode="Classic"
        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
        AllowFilteringByColumn="False" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
        AllowPaging="True" isexporting="True" PageSize="100">
        <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="Nessun dato da visualizzare."
            ShowHeadersWhenNoRecords="true" TableLayout="Auto" CommandItemSettings-ShowAddNewRecordButton="false">
            <Columns>
                <telerik:GridBoundColumn DataField="ID" HeaderText="#">
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_ORA_INIZIO" HeaderText="DATA - ORA INIZIO"
                    SortExpression="VALORE_ORDER_I">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_ORA_FINE" HeaderText="DATA - ORA FINE"
                    SortExpression="VALORE_ORDER_F">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ESITO" HeaderText="ESITO">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PERCENTUALE" HeaderText="PERCENTUALE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="VALORE_ORDER_I" SortOrder="Descending" />
            </SortExpressions>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="180" ClientIDMode="Static" />
</asp:Content>

