<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_AppaltiForn.ascx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_AppaltiForn" %>
<link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
<telerik:RadWindow ID="RadWindowFornitori" runat="server" CenterIfModal="true" Modal="true"
    Title="Gestione fornitori" Width="680px" Height="330px" VisibleStatusbar="false"
    Behaviors="Pin, Maximize, Move, Resize,close">
    <ContentTemplate>
        <asp:Panel runat="server" ID="PanelFornitori" Style="height: 100%;" class="sfondo">
            <table style="width: 100%" class="sfondo">
                <tr>
                    <td colspan="2" class="TitoloModulo">
                        GESTIONE FORNITORI
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnInserisciFornitore" runat="server" Text="Aggiorna" ToolTip="Salva"
                                        OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}"
                                        CausesValidation="False">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="imgEsciForn" runat="server" Text="Esci" ToolTip="Esci senza inserire"
                                        CausesValidation="False" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';closeWindow(sender, args, 'Tab_Fornitori1_RadWindowFornitori');}">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        <asp:Label ID="lblFornitore" Text="Fornitore" runat="server" Font-Names="Arial" Font-Size="9pt" />
                    </td>
                    <td style="width: 85%">
                        <telerik:RadComboBox ID="cmbAggFornitori" Width="95%" AppendDataBoundItems="true"
                            Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                            HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        <asp:Label ID="Label9" Text="Capofila" runat="server" Font-Names="Arial" Font-Size="9pt" />
                    </td>
                    <td style="width: 85%">
                        <asp:CheckBox Text="" runat="server" ID="chkCapofila" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 90px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="vertical-align: top; text-align: right">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</telerik:RadWindow>
<table style="width: 100%">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblAPPALTI" runat="server" CssClass="TitoloH1" Text="Elenco fornitori"></asp:Label>
        </td>
    </tr>
    <tr>
        <td >
            <div style="overflow: auto; width: 100%; height: 100%;">
                <telerik:RadGrid ID="DataGridFornitori" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                    AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" 
                    AllowSorting="True" IsExporting="False"  PagerStyle-AlwaysVisible="true">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                        CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                            ShowRefreshButton="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CODICE" HeaderText="CODICE" HeaderStyle-Width="12%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CAPOFILA" HeaderText="CAPOFILA" HeaderStyle-Width="12%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="RAGIONE SOCIALE" HeaderStyle-Width="20%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" HeaderStyle-Width="30%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IBAN" HeaderText="IBAN" HeaderStyle-Width="20%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_TIPO_PAGAMENTO" HeaderText="ID_TIPO_PAGAMENTO"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_TIPO_MODALITA_PAG" HeaderText="ID_TIPO_MODALITA_PAG"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="modificaFornitore" HeaderText="" ButtonType="ImageButton"
                                ShowInEditForm="true" CommandName="myEdit" ConfirmDialogType="RadWindow" ButtonCssClass="rgEdit">
                                <HeaderStyle Width="3%" />
                                <ItemStyle Width="24px" Height="24px" />
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn HeaderStyle-Width="3%" CommandName="Delete" ImageUrl="../Immagini/Delete.gif"
                                Text="Elimina" UniqueName="DeleteColumn" ButtonType="ImageButton" ButtonCssClass="rgDel">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="24px" Height="24px" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <a id="addServizio" style="cursor: pointer" onclick="openWindow(null, null, 'Tab_Fornitori1_RadWindowFornitori')">
                                <img style="border: 0px" alt="" src="../../Immagini/addRecord.gif" />
                                Aggiungi nuovo record</a>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                        <Excel FileExtension="xls" Format="Xlsx" />
                    </ExportSettings>
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                         <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                            AllowResizeToFit="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
            <telerik:RadButton ID="btnApriFornitoreAppalto" runat="server" Style="visibility: hidden">
            </telerik:RadButton>
            <asp:TextBox ID="txtSelFornitori" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" TabIndex="-1" Width="670px"></asp:TextBox>
        </td>
    </tr>
</table>
<asp:HiddenField ID="txtIdFornitore" runat="server" />
<asp:HiddenField ID="txtannulloFornitore" runat="server" />
<asp:HiddenField ID="txtAppareF" runat="server" />
<asp:HiddenField ID="txtTipoF" runat="server" />
