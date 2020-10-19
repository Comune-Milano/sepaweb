<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TabMenuEventi.ascx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_RRS_TabMenuEventi" %>
<div style="width: 100%; height: 350px">
    <table id="TABLE1" style="width: 100%">
        <tr>
            <td style="height: 200px">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 100%; height: 100%;">
                                <telerik:RadGrid ID="DataGridEventi" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                    AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                    IsExporting="False" PagerStyle-AlwaysVisible="true">
                                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                        Width="100%">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="DATA_EVENTO" HeaderText="DATA ORA" HeaderStyle-Width="10%">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EVENTO" HeaderText="DESCRIZIONE" HeaderStyle-Width="20%">
                                                <HeaderStyle Width="20%"></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE" HeaderStyle-Width="60%">
                                                <HeaderStyle Width="60%"></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE" HeaderStyle-Width="10%">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ENTE_DITTA" HeaderText="ENTE/DITTA" HeaderStyle-Width="10%">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                        <Excel FileExtension="xls" Format="Xlsx" />
                                    </ExportSettings>
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        <Selecting AllowRowSelect="True" />
                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                            AllowResizeToFit="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
