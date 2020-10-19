<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="AnomalieConguagli.aspx.vb" Inherits="SPESE_REVERSIBILI_AnomalieConguagli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static" 
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadGrid ID="RadGrid1" runat="server" GroupPanelPosition="Top" AllowPaging="true"
        PagerStyle-AlwaysVisible="true" PageSize="50" AutoGenerateColumns="False" Culture="it-IT"
        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
        IsExporting="False">
        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
            CommandItemDisplay="Top" AllowMultiColumnSorting="true">
            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                ShowRefreshButton="true" />
            <Columns>
                <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" FilterControlWidth="85%"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RIPARTIZIONE" HeaderText="RIPARTIZIONE" FilterControlWidth="85%"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DIVISIONE" HeaderText="DIVISIONE" FilterControlWidth="85%"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OGGETTO" HeaderText="OGGETTO" FilterControlWidth="85%"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                </telerik:GridBoundColumn>
            </Columns>
            <PagerStyle AlwaysVisible="True" />
            <CommandItemTemplate>
                <div style="display: inline-block; width: 100%;">
                    <div style="float: right; padding: 4px;">
                        <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh" OnClientClick="caricamento(2);"
                            CssClass="rgRefresh" />
                        <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click" OnClientClick="caricamento(2);"
                            CommandName="ExportToExcel" CssClass="rgExpXLS" />
                    </div>
                </div>
            </CommandItemTemplate>
        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <ExportSettings OpenInNewWindow="true" IgnorePaging="true" ExportOnlyData="true"
            HideStructureColumns="true">
            <Excel FileExtension="xlsx" Format="Xlsx" />
        </ExportSettings>
        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
            ClientEvents-OnCommand="onCommand">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <Selecting AllowRowSelect="True" />

        </ClientSettings>
    </telerik:RadGrid>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="350" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="ButtonEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
</asp:Content>
