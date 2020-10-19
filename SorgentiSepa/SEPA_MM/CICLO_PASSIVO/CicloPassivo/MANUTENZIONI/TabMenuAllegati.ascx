<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TabMenuAllegati.ascx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_RRS_TabMenuAllegati" %>
<div style="width: 100%;" >
    <table id="TABLE1" style="width: 100%">
        <tr>
            <td >
                <table style="width: 100%">
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 100%; height: 100%;">
                                <telerik:RadGrid ID="DataGridAllegati" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                    AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                    IsExporting="False" PagerStyle-AlwaysVisible="true">
                                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                        Width="100%">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPO DOC.">
                                                <HeaderStyle Width="20%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                <HeaderStyle Width="50%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NOME_FILE" HeaderText="ALLEGATO">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                                                <HeaderStyle Width="10%" />
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
