<%@ Page Title="" Language="VB" MasterPageFile="~/Contratti/Spalmatore/HomePage.master"
    AutoEventWireup="false" CodeFile="TabellaKPI1.aspx.vb" Inherits="Contratti_Spalmatore_TabellaKPI1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Tabella KPI1"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnDownload" runat="server" Text="Download" ToolTip="Download" AutoPostBack="true"
        OnClick="Esporta_Click"></asp:Button>
    <telerik:RadButton ID="btnUpload" runat="server" Text="Upload" AutoPostBack="false"
        OnClientClicking="function(sender, args){openWindow(sender, args, 'MasterPage_CPFooter_RadWindowAllegati');}">
    </telerik:RadButton>
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" AutoPostBack="true">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
 <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelRadWindowAllegati">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelRadWindowAllegati" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="RadGridKPI1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
        AllowAutomaticDeletes="True" AllowFilteringByColumn="True" EnableLinqExpressions="False"
        Width="100%" AllowSorting="True" IsExporting="False" AllowPaging="True" PageSize="100">
        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
            CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="False" ShowExportToWordButton="false"
                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                ShowRefreshButton="true" />
            <Columns>
                <telerik:GridBoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="true">
                </telerik:GridBoundColumn>
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
    <telerik:RadWindow ID="RadWindowAllegati" runat="server" CenterIfModal="true" Modal="true"
        Width="400px" Height="220px" VisibleStatusbar="false" Title="Carica File" Behaviors="Move"
        RestrictionZoneID="RestrictionZoneID" ReloadOnShow="True" ShowContentDuringLoad="False">
        <ContentTemplate>
            <asp:Panel runat="server" ID="PanelRadWindowAllegati">
                <table>
                    <tr align="left">
                        <td style="font-size: 9pt;" width="100%">
                            <strong>File excel:</strong>
                        </td>
                    </tr>
                    <tr align="left">
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" width="100%">
                            <telerik:RadAsyncUpload runat="server" ID="FileUpload1" AllowedFileExtensions=".xlsx"
                                Width="350px" UploadedFilesRendering="AboveFileInput" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td width="400px">
                                        &nbsp
                                    </td>
                                    <td width="80px">
                                        <telerik:RadButton ID="btnSalvaAllegato" runat="server" Text="Salva" ToolTip="Salva">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnEsciAllegato" runat="server" Text="Esci" ToolTip="Esci">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="180" ClientIDMode="Static" />
</asp:Content>
