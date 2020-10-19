<%@ Page Title="Log Elaborazioni" Language="VB" MasterPageFile="~/Contratti/Spalmatore/HomePage.master" AutoEventWireup="false" CodeFile="Log_Spalmatore.aspx.vb" Inherits="Contratti_Spalmatore_Log_Spalmatore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Log Elaborazioni"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci"
        AutoPostBack="true">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="dgvLogSpalmatore" runat="server" GroupPanelPosition="Top" resolvedrendermode="Classic"
        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
        AllowFilteringByColumn="True" Width="100%" AllowSorting="True"
        AllowPaging="True" isexporting="True" PageSize="100">
        <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="Nessun dato da visualizzare."
            ShowHeadersWhenNoRecords="true" TableLayout="Auto" CommandItemSettings-ShowAddNewRecordButton="false">
            <CommandItemSettings ShowExportToExcelButton="False" ShowExportToWordButton="false"
                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                ShowRefreshButton="false" />
            <CommandItemTemplate>
                <div style="display: inline-block; width: 100%;">
                    <div style="float: right; padding: 4px;">

                        <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                            CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                    </div>
                </div>
            </CommandItemTemplate>
            <Columns>
                <telerik:GridBoundColumn DataField="ID_ELABORAZIONE" HeaderText="#" DataFormatString="{0:N0}" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="True" AllowFiltering="true">
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridDateTimeColumn DataField="DATA_ELABORAZIONE" HeaderText="DATA ELABORAZ."
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="ID_RU" HeaderText="ID RU" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="COD_RU" HeaderText="COD RU" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPO_RU" HeaderText="TIPO RU" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPO_RU_SPECIFICO" HeaderText="TIPO RU SPECIFICO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DATA_SCAD_BOLL" HeaderText="DATA SCAD. BOLL."
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="SALDO" HeaderText="SALDO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SALDO_SPALM" HeaderText="SALDO PER SPALM." CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ID_BOLL_GEST" HeaderText="ID BOLL. GEST" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ID_TIPO_GESTIONALE" HeaderText="ID TIPO GESTIONALE" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ACRONIMO" HeaderText="ACRONIMO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="IMPORTO_GESTIONALE" HeaderText="IMP. GESTIONALE" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SEMAFORO" HeaderText="SEMAFORO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IMPORTO_PROCESSATO" HeaderText="IMP. PROCESSATO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="KPI1" HeaderText="KPI1" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="VENDUTO" HeaderText="VENDUTO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ART_15_C2BIS" HeaderText="ART.15 C.2 BIS" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RU_CON_RATEIZZ" HeaderText="RU CON RATEIZZ." CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PRESENZA_MOR" HeaderText="RU CON MOROSITA'" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NUOVO_SALDO" HeaderText="NUOVO SALDO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NUOVO_SALDO_SPALM" HeaderText="NUOVO SALDO PER SPALM." CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ID_CREDITO_RESIDUO" HeaderText="ID CREDITO RESIDUO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IMP_GEST_RESIDUO" HeaderText="IMP. CREDITO RESIDUO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ESITO_LAVORAZIONE" HeaderText="ESITO LAVORAZIONE" CurrentFilterFunction="Contains" AutoPostBackOnFilter="True">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>  
                <telerik:GridSortExpression FieldName="ID_ELABORAZIONE" SortOrder="Descending"  />
           </SortExpressions>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true" AllowDragToGroup="false"
            AllowAutoScrollOnDragDrop="false" AllowRowsDragDrop="false">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <Selecting AllowRowSelect="True" />
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                AllowResizeToFit="true" />
            
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" />
    </telerik:RadGrid>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="240" ClientIDMode="Static" />
</asp:Content>



