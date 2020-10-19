<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master"
    AutoEventWireup="false" CodeFile="Redditi_Componenti.aspx.vb" Inherits="Gestione_locatari_Redditi_Componenti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function SaveBatchEditDipend() {
            var grid = $find("<%=RadGridDipend.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var batchManager = grid.get_batchEditingManager();
            var hasChanges = batchManager.hasChanges(masterTable);
            document.getElementById('operazione').value = "1";
            if (hasChanges) {
                batchManager.saveChanges(masterTable);
            };
        };
        function SaveBatchEditAuton() {
            var grid = $find("<%=RadGridAutonomo.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var batchManager = grid.get_batchEditingManager();
            var hasChanges = batchManager.hasChanges(masterTable);
            if (hasChanges) {
                batchManager.saveChanges(masterTable);
            };
        };
        function SaveBatchEditPens() {
            var grid = $find("<%=RadGridPensioni.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var batchManager = grid.get_batchEditingManager();
            var hasChanges = batchManager.hasChanges(masterTable);
            if (hasChanges) {
                batchManager.saveChanges(masterTable);
            };
        };
        function SaveBatchEditPensEs() {
            var grid = $find("<%=RadGridPensEsenti.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var batchManager = grid.get_batchEditingManager();
            var hasChanges = batchManager.hasChanges(masterTable);
            if (hasChanges) {
                batchManager.saveChanges(masterTable);
            };
        };
        function SaveBatchEditNoISEE() {
            var grid = $find("<%=RadGridNoISEE.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var batchManager = grid.get_batchEditingManager();
            var hasChanges = batchManager.hasChanges(masterTable);
            if (hasChanges) {
                batchManager.saveChanges(masterTable);
            };
        };
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Dettaglio reddito"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClick="document.getElementById('frmModify').value='0';SaveBatchEditDipend();SaveBatchEditAuton();SaveBatchEditPens();SaveBatchEditPensEs();SaveBatchEditNoISEE();return false;" />
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
        OnClientClick="ChiudiFinestra(document.getElementById('HFBtnToClick').value);" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 97%">
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 80%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            Componente del Nucleo:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbComponente" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                                Width="400px" AutoPostBack="True">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" ShowBaseLine="true" ScrollChildren="true" OnClientTabSelecting="setResizeTabs"
                    Width="100%">
                    <Tabs>
                        <telerik:RadTab runat="server" PageViewID="RadPageDipendente" Text="Dipendente" ToolTip="Dipendente"
                            Value="Dipendente">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageAutonomo" Text="Autonomo" ToolTip="Autonomo"
                            Value="Autonomo">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPagePensioni" Text="Pensioni" ToolTip="Pensioni"
                            Value="Pensioni">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPagePensEsenti" Text="Pensioni Esenti"
                            ToolTip="Pensioni Esenti" Value="Pensioni_Esenti">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="RadPageNoISEE" Text="Importi non rilevanti ai fini ISEE"
                            ToolTip="Importi non rilevanti ai fini ISEE" Value="NoISEE">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="RadPageDipendente" runat="server">
                        <asp:Panel ID="PanelRadPageDipendente" runat="server" Style="width: 100%; height: 60%;
                            overflow: hidden;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <telerik:RadGrid ID="RadGridDipend" runat="server" PageSize="20" AllowSorting="True"
                                            AutoGenerateColumns="False" ShowStatusBar="True" AllowAutomaticDeletes="True"
                                            AllowAutomaticUpdates="True" Width="100%" Culture="it-IT" GroupPanelPosition="Top"
                                            IsExporting="False" BorderWidth="0px" ShowFooter="True">
                                            <MasterTableView CommandItemDisplay="Top" EditMode="Batch" runat="server">
                                                <BatchEditingSettings EditType="Cell" />
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowSaveChangesButton="false" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ReadOnly="True">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NUM_GG" HeaderText="NUM. GIORNI">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridNumericColumn DataField="IMPORTO" HeaderText="IMPORTO" ShowFilterIcon="false"
                                                        Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2"
                                                        Aggregate="Sum">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </telerik:GridNumericColumn>
                                                    <telerik:GridBoundColumn DataField="IDIMPORTI" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageAutonomo" runat="server">
                        <asp:Panel ID="PanelRadPageAutonomo" runat="server" Style="width: 100%; height: 60%;
                            overflow: hidden;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <telerik:RadGrid ID="RadGridAutonomo" runat="server" PageSize="20" AllowSorting="True"
                                            AutoGenerateColumns="False" ShowStatusBar="True" AllowAutomaticDeletes="True"
                                            AllowAutomaticUpdates="True" Width="100%" Culture="it-IT" GroupPanelPosition="Top"
                                            IsExporting="False" BorderWidth="0px" ShowFooter="True">
                                            <MasterTableView CommandItemDisplay="Top" EditMode="Batch" runat="server">
                                                <BatchEditingSettings EditType="Cell" />
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowSaveChangesButton="false" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ReadOnly="True">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NUM_GG" HeaderText="NUM. GIORNI">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridNumericColumn DataField="IMPORTO" HeaderText="IMPORTO" ShowFilterIcon="false"
                                                        Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2"
                                                        Aggregate="Sum">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </telerik:GridNumericColumn>
                                                    <telerik:GridBoundColumn DataField="IDIMPORTI" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageViewPensioni" runat="server">
                        <asp:Panel ID="PanelRadPagePensioni" runat="server" Style="width: 100%; height: 60%;
                            overflow: hidden;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <telerik:RadGrid ID="RadGridPensioni" runat="server" PageSize="20" AllowSorting="True"
                                            AutoGenerateColumns="False" ShowStatusBar="True" AllowAutomaticDeletes="True"
                                            AllowAutomaticUpdates="True" Width="100%" Culture="it-IT" GroupPanelPosition="Top"
                                            IsExporting="False" BorderWidth="0px" ShowFooter="True">
                                            <MasterTableView CommandItemDisplay="Top" EditMode="Batch" runat="server">
                                                <BatchEditingSettings EditType="Cell" />
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowSaveChangesButton="false" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ReadOnly="True">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NUM_GG" HeaderText="NUM. GIORNI">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridNumericColumn DataField="IMPORTO" HeaderText="IMPORTO" ShowFilterIcon="false"
                                                        Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2"
                                                        Aggregate="Sum">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </telerik:GridNumericColumn>
                                                    <telerik:GridBoundColumn DataField="IDIMPORTI" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPagePensEsenti" runat="server">
                        <asp:Panel ID="PanelRadPagePensEsenti" runat="server" Style="width: 100%; height: 60%;
                            overflow: hidden;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <telerik:RadGrid ID="RadGridPensEsenti" runat="server" PageSize="20" AllowSorting="True"
                                            AutoGenerateColumns="False" ShowStatusBar="True" AllowAutomaticDeletes="True"
                                            AllowAutomaticUpdates="True" Width="100%" Culture="it-IT" GroupPanelPosition="Top"
                                            IsExporting="False" BorderWidth="0px" ShowFooter="True">
                                            <MasterTableView CommandItemDisplay="Top" EditMode="Batch" runat="server">
                                                <BatchEditingSettings EditType="Cell" />
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowSaveChangesButton="false" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ReadOnly="True">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NUM_GG" HeaderText="NUM. GIORNI">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridNumericColumn DataField="IMPORTO" HeaderText="IMPORTO" ShowFilterIcon="false"
                                                        Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2"
                                                        Aggregate="Sum">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </telerik:GridNumericColumn>
                                                    <telerik:GridBoundColumn DataField="IDIMPORTI" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageNoISEE" runat="server">
                        <asp:Panel ID="PanelRadPageNoISEE" runat="server" Style="width: 100%; height: 60%;
                            overflow: hidden;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <telerik:RadGrid ID="RadGridNoISEE" runat="server" PageSize="20" AllowSorting="True"
                                            AutoGenerateColumns="False" ShowStatusBar="True" AllowAutomaticDeletes="True"
                                            AllowAutomaticUpdates="True" Width="100%" Culture="it-IT" GroupPanelPosition="Top"
                                            IsExporting="False" BorderWidth="0px" ShowFooter="True">
                                            <MasterTableView CommandItemDisplay="Top" EditMode="Batch" runat="server">
                                                <BatchEditingSettings EditType="Cell" />
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowSaveChangesButton="false" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ReadOnly="True">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NUM_GG" HeaderText="NUM. GIORNI">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridNumericColumn DataField="IMPORTO" HeaderText="IMPORTO" ShowFilterIcon="false"
                                                        Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2"
                                                        Aggregate="Sum">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </telerik:GridNumericColumn>
                                                    <telerik:GridBoundColumn DataField="IDIMPORTI" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFBtnToClick" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idRedd" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="iddich" runat="server" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField ID="operazione" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="dipendente" runat="server" Value="0" />
    <asp:HiddenField ID="autonomo" runat="server" Value="0" />
    <asp:HiddenField ID="pensione" runat="server" Value="0" />
    <asp:HiddenField ID="pens_esente" runat="server" Value="0" />
    <asp:HiddenField ID="noIsee" runat="server" Value="0" />
    <asp:HiddenField ID="svuotaTxt" runat="server" Value="0" />
    <asp:HiddenField ID="importoDip" runat="server" Value="0" />
    <asp:HiddenField ID="importoAuton" runat="server" Value="0" />
    <asp:HiddenField ID="importoPens" runat="server" Value="0" />
    <asp:HiddenField ID="importoPensEs" runat="server" Value="0" />
    <asp:HiddenField ID="importoNoIsee" runat="server" Value="0" />
    <asp:HiddenField ID="salvaRedditi" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="azzeraTxt" runat="server" Value="0" />
    <asp:HiddenField ID="tipoDomanda" runat="server" Value="" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>
