<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_SLA.ascx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_SLA" %>
<style type="text/css">
    .style1
    {
        height: 14px;
        width: 88px;
    }
    .style2
    {
        width: 707px;
    }
</style>
<table>
    <tr>
        <td>
            <table class="FontTelerik">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" CssClass="TitoloH1" Text="Priorità"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbPriorita" Width="95%" AppendDataBoundItems="true" Enabled="false"
                            Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                            HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </td>
        <td>
        </td>
    </tr>
  
    <tr>
        <td style="vertical-align: top; text-align: left" class="style2">
            <div style="visibility: visible; overflow: auto; width: 100%; height: 100%">
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <telerik:RadGrid ID="DataGridSLA" runat="server" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                        Culture="it-IT" RegisterWithScriptManager="False" AllowFilteringByColumn="false"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PagerStyle-AlwaysVisible="true">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID_VERIFICA" HeaderText="ID_VERIFICA" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ORE" HeaderText="ORE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="GIORNI" HeaderText="GIORNI">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <PagerStyle AlwaysVisible="True"></PagerStyle>
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                            ClientEvents-OnCommand="onCommand">
                            <ClientEvents OnCommand="onCommand"></ClientEvents>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                    </telerik:RadGrid>
                </span></strong>
            </div>
        </td>
        <td style="vertical-align: top; text-align: left">
            <table>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
