<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="Eventi.aspx.vb" Inherits="SPESE_REVERSIBILI_Eventi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvEventi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvEventi" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table style="width: 100">
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle; text-align: center">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Elenco completo di tutti gli eventi riguardati l'elaborazione selezionata.</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="dgvEventi" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                    ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                    PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                    EnableLinqExpressions="False" Width="97%" AllowSorting="True" IsExporting="False"
                    PageSize="100">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                        TableLayout="Auto" CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                            ShowRefreshButton="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE" AutoPostBackOnFilter="true"
                                FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA OPERAZIONE" AutoPostBackOnFilter="true"
                                FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CAMPO" HeaderText="CAMPO" AutoPostBackOnFilter="true"
                                FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="30%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="VAL_PRECEDENTE" HeaderText="VALORE PRECEDENTE"
                                AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                DataFormatString="{0:@}">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="15%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="VAL_IMPOSTATO" HeaderText="VALORE IMPOSTATO"
                                AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                DataFormatString="{0:@}">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="15%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OPERAZIONE" HeaderText="OPERAZIONE" AutoPostBackOnFilter="true"
                                FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <div style="display: inline-block; width: 100%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <div style="float: right; padding: 4px;">
                                                <asp:Button ID="ButtonRefreshEventi" runat="server" OnClick="RefreshEventi_Click"
                                                    CommandName="Refresh" CssClass="rgRefresh" OnClientClick="caricamento(2);" /><asp:Button ID="ButtonExportExcelEventi"
                                                        Text="text" runat="server" OnClick="EsportaAppalti_Click" CommandName="ExportToExcel"
                                                        CssClass="rgExpXLS" OnClientClick="caricamento(2);" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                        <Excel FileExtension="xls" Format="Xlsx" />
                    </ExportSettings>
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                        <Scrolling AllowScroll="true" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                            AllowResizeToFit="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
</asp:Content>
