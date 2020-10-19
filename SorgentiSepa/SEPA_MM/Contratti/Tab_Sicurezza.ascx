<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Sicurezza.ascx.vb"
    Inherits="Contratti_Tab_Sicurezza" %>
<div style="left: 8px; position: absolute; top: 168px; height: 520px">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                    ForeColor="#993333" Text="ELENCO SEGNALAZIONI"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="div1" style="width: 100%; overflow: auto;">
                    <telerik:RadGrid ID="RadGridSegn" runat="server" GroupPanelPosition="Top" resolvedrendermode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        Width="1135px" EnableLinqExpressions="False" AllowSorting="True" AllowPaging="True"
                        PageSize="50" Height="240px">
                        <MasterTableView EnableHierarchyExpandAll="false" NoMasterRecordsText="Nessun intervento da visualizzare."
                            HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false" Font-Names="Arial" Font-Size="8pt">
                            <CommandItemSettings ShowExportToExcelButton="True" ShowExportToWordButton="False"
                                ShowExportToPdfButton="False" ShowExportToCsvButton="False" ShowAddNewRecordButton="False"
                                ShowRefreshButton="True" RefreshText="Aggiorna" />
                            <DetailTables>
                                <telerik:GridTableView Name="Dettagli" BackColor="Azure">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CATEGORIA1" HeaderText="CATEGORIA1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CRITICITA" HeaderText="CRITICITA'">
                                        <ItemStyle HorizontalAlign="center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_APERTURA" HeaderText="DATA APERTURA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_IN_CORSO" HeaderText="DATA IN CORSO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_EVASIONE" HeaderText="DATA EVASIONE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_CHIUSURA" HeaderText="DATA CHIUSURA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NUM_SOLLECITI" HeaderText="NUM. SOLLECITI">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <RowIndicatorColumn Visible="False">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Created="True">
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID">
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE" Visible="False">
                                        </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_SEGNALAZIONE_PADRE" HeaderText="ID_SEGNALAZIONE_PADRE"
                                    Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CATEGORIA1" HeaderText="CATEGORIA1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CRITICITA" HeaderText="CRITICITA'">
                                <ItemStyle HorizontalAlign="center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_APERTURA" HeaderText="DATA APERTURA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_IN_CORSO" HeaderText="DATA IN CORSO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_EVASIONE" HeaderText="DATA EVASIONE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_CHIUSURA" HeaderText="DATA CHIUSURA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUM_SOLLECITI" HeaderText="NUM. SOLLECITI">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Wrap="False" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                        <ItemStyle Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                        <ClientSettings EnableRowHoverStyle="True" AllowColumnsReorder="False" ReorderColumnsOnClient="True">
                            <Scrolling AllowScroll="True" UseStaticHeaders="False" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
        <tr><td>&nbsp</td></tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                    ForeColor="#993333" Text="ELENCO INTERVENTI ARCHIVIATI"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divOverContent" style="width: 100%; overflow: auto;">
                    <telerik:RadGrid ID="RadGridInterventi" runat="server" GroupPanelPosition="Top" resolvedrendermode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        Width="1135px" EnableLinqExpressions="False" AllowSorting="True" AllowPaging="True"
                        PageSize="50" Height="240px">
                        <MasterTableView EnableHierarchyExpandAll="false" NoMasterRecordsText="Nessun intervento da visualizzare."
                            HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false">
                            <CommandItemSettings ShowExportToExcelButton="True" ShowExportToWordButton="False"
                                ShowExportToPdfButton="False" ShowExportToCsvButton="False" ShowAddNewRecordButton="False"
                                ShowRefreshButton="True" RefreshText="Aggiorna" />
                            <DetailTables>
                                <telerik:GridTableView Name="Dettagli" BackColor="Azure">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="TIPO RU" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COGNOME_SOGG_COINVOLTO" HeaderText="COGNOME"
                                            Visible="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOME_SOGG_COINVOLTO" HeaderText="NOME" Visible="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COD_FISC_SOGG_COINVOLTO" HeaderText="COD. FISCALE"
                                            Visible="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_NASC_SOGG_COINVOLTO" HeaderText="DATA NASCITA"
                                            Visible="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COD_LUOGO_NASCITA" HeaderText="LUOGO NASCITA"
                                            Visible="True">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <RowIndicatorColumn Visible="False">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Created="True">
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID_INTERVENTO_SICUREZZA" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_APERTURA" HeaderText="DATA INTERV.">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD UI">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PIANO" HeaderText="PIANO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INTERNO" HeaderText="INTERNO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERR.">
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="DATA_ORA_INSERIM" HeaderText="DATA INSERIM.">
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="STATO_ALLOGGIO_ARRIVO" HeaderText="STATO ALL. ARRIVO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUOVO_STATO_NUCLEO" HeaderText="NUOVO STATO NUCLEO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUOVO_STATO_UI" HeaderText="NUOVO STATO UI">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MESSO_IN_SICUREZZA" HeaderText="MESSO IN SICUREZZA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SLOGGIATO" HeaderText="SLOGGIATO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ASSEGNATARIO" HeaderText="ASSEGN.">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ASSEGNATARIO_2" HeaderText="CO-ASSEGN.">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Wrap="False" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                        <ItemStyle Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                        <ClientSettings EnableRowHoverStyle="True" AllowColumnsReorder="False" ReorderColumnsOnClient="True">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
</div>
