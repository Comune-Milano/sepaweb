<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_SAL_Dettagli.ascx.vb"
    Inherits="Tab_SAL_Dettagli" %>
<table id="TABBLE_LISTA" style="width: 100%">
    <tr>
        <td class="TitoloH1" style="text-align: left">&nbsp;<asp:Label ID="lblELENCO_INTERVENTI" runat="server" TabIndex="-1" Text="ELENCO MANUTENZIONI"
            Width="248px"></asp:Label>&nbsp;
        </td>
        <td></td>
    </tr>
    <tr>
        <td>        
                <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" 
                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" 
                    AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="false"
                    IsExporting="False" AllowPaging="false" PagerStyle-AlwaysVisible="false">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                        CommandItemDisplay="None" Width="150%">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                            ShowRefreshButton="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID_MANUTENZIONE" HeaderText="ID_MANUTENZIONE"
                                Visible="False">
                                <HeaderStyle Width="0%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="ODL/ANNO">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ODL_ANNO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ODL_ANNO") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="7%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PROGR_APPALTO" HeaderText="SAL">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="6%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_INIZIO_ORDINE" HeaderText="DATA ORDINE">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="6%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_FINE_ORDINE" HeaderText="DATA CONSUNTIVO">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="6%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UBICAZIONE" HeaderText="UBICAZIONE">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="28%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMP_NETTO_ONERI_IVA" HeaderText="IMP. NETTO DI ONERI E IVA">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IVA" HeaderText="IVA">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="8%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RIMBORSI" HeaderText="TOT. RIMBORSI">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMP_NETTO_ONERI" HeaderText="IMP. NETTO DI ONERI">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="12%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PENALE" HeaderText="IMP. PENALE">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="8%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="False">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="0%" />
                                <ItemStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PROGR" HeaderText="ODL_NUM" Visible="False">
                                <HeaderStyle Width="0%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO" Visible="False">
                                <HeaderStyle Width="0%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_PRENOTAZIONE_PAGAMENTO" HeaderText="ID_PRENOTAZIONE_PAGAMENTO"
                                Visible="False">
                                <HeaderStyle Width="0%" />
                            </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PATRIMONIO" HeaderText="PATRIMONIO">
                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="30%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IMPORTO_CONSUNTIVATO" HeaderText="A LORDO COMPRESI ONERI">
                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="9%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
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
                        <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                            AllowResizeToFit="false" />
                    </ClientSettings>
                </telerik:RadGrid>
           
        </td>
        <td>&nbsp; &nbsp;
        </td>
    </tr>
</table>
<asp:TextBox ID="txtAppare1" runat="server" Style="left: 800px; position: absolute; top: 320px; visibility: hidden;"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente" runat="server" Style="left: 800px; position: absolute; top: 320px; visibility: hidden;"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo" runat="server" Style="left: 800px; position: absolute; top: 320px; visibility: hidden;"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdConnessione" runat="server" Style="left: 800px; position: absolute; top: 320px; visibility: hidden;"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:HiddenField ID="txt_FL_BLOCCATO" runat="server" />
