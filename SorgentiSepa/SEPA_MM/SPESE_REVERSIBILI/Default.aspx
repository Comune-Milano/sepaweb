<%@ Page Title="Spese reversibili" Language="VB" MasterPageFile="HomePage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="SPESE_REVERSIBILI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">

        window.onresize = setDimensioni;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGridelEborazioni">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGridelEborazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle; text-align: center">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>E' possibile disporre di una sola elaborazione per esercizio finanziario</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <telerik:RadGrid ID="DataGridelEborazioni" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic" HeaderStyle-HorizontalAlign="Center"
        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
        PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
        AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
        PageSize="100" IsExporting="False">
        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
            CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                ShowRefreshButton="false" />
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
            <Columns>
                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NOTE" HeaderText="NOTE">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OPERAZIONE" HeaderText="ULTIMA OPERAZIONE EFFETTUATA">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO ULTIMA OPERAZIONE EFFETTUATA">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_FINE" HeaderText="DATA FINE ULTIMA OPERAZIONE EFFETTUATA">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PERCENTUALE" HeaderText="PERCENTUALE">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NOME_OPERATORE" HeaderText="OPERATORE">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
            <Excel FileExtension="xls" Format="Xlsx" />
        </ExportSettings>
        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
            ClientEvents-OnCommand="onCommand">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <Selecting AllowRowSelect="True" />
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                AllowResizeToFit="true" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="350" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenFieldIdElaborazione" runat="server" Value="0" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <table style="width: 400px;">
        <tr>
            <td>
                <asp:Button ID="ButtonElaborazione" runat="server" Text="Imposta elaborazione"
                    ToolTip="Imposta elaborazione"
                    OnClientClick="caricamento(1);" />
            </td>
            <td>
                <asp:Button ID="ButtonCambiaElaborazione" runat="server" Text="Cambia elaborazione"
                    ToolTip="Cambia elaborazione"
                    OnClientClick="caricamento(1);" />
            </td>
            <td>
                <asp:Button ID="ButtonCreaNuovaElaborazione" runat="server" Text="Crea nuova elaborazione"
                    ToolTip="Crea nuova elaborazione" OnClientClick="caricamento(1);" />
            </td>
            <td>
                <asp:Button ID="ButtonClickElaborazione" runat="server" Text="Button" CssClass="nascondi"
                    OnClientClick="caricamento(1);" />
            </td>
            <td>
                <asp:Button ID="btnAggiornaElaborazione" runat="server" Text="Aggiorna Elaborazione" ToolTip="Aggiorna Elaborazione"
                    OnClientClick="caricamento(1);" />
            </td>
        </tr>
    </table>
</asp:Content>
