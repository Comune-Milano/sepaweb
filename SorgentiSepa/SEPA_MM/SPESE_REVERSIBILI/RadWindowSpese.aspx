<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/PageModal.master"
    AutoEventWireup="false" CodeFile="RadWindowSpese.aspx.vb" Inherits="SPESE_REVERSIBILI_RadWindowSpese" %>

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
            <telerik:AjaxSetting AjaxControlID="RadGridSpese">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridSpese" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <asp:Button ID="btnChiudiDettaglio" runat="server" Text="Esci" OnClientClick="GetRadWindow().close();return false;"
        ToolTip="Esci" />
    <table cellpadding="2" cellspacing="2" width="98%" style="">
        <tr>
            <td>
                <telerik:RadGrid ID="RadGridSpese" runat="server" GroupPanelPosition="Top" AllowPaging="true"
                    PagerStyle-AlwaysVisible="true" PageSize="50" AutoGenerateColumns="False" Culture="it-IT"
                    AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                    IsExporting="False">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                        CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                            ShowRefreshButton="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <SortExpressions>
                            <telerik:GridSortExpression FieldName="DESCRIZIONE" SortOrder="Ascending" />
                        </SortExpressions>
                        <PagerStyle AlwaysVisible="True" />
                        <CommandItemTemplate>
                            <div style="display: inline-block; width: 100%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <div style="float: right; padding: 4px;">
                                                <asp:Button ID="ButtonRefreshDettagli" runat="server" OnClick="RefreshSpese_Click" OnClientClick="caricamento(2);"
                                                    CommandName="Refresh" CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcelDettagli"
                                                        Text="text" runat="server" OnClick="EsportaSpese_Click" CommandName="ExportToExcel" OnClientClick="caricamento(2);"
                                                        CssClass="rgExpXLS" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <ExportSettings OpenInNewWindow="true" IgnorePaging="true" ExportOnlyData="true"
                        HideStructureColumns="true">
                        <Excel FileExtension="xlsx" Format="Xlsx" />
                    </ExportSettings>
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="idRipartizione" ClientIDMode="Static" />
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="250" ClientIDMode="Static" />

</asp:Content>
