<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master"
    AutoEventWireup="false" CodeFile="ElencoIstanze.aspx.vb" Inherits="Gestione_locatari_ElencoIstanze" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        };
        function CancelEdit() {
            GetRadWindow().close();
        };
        function apriIstanza() {
            today = new Date();

            if (document.getElementById('idSelectedPG').value != 0) {
                var Titolo = 'Istanza' + today.getMinutes() + today.getSeconds();

                CancelEdit();
                CenterPage2('Istanza.aspx?IDD=' + document.getElementById('idSelectedPG').value + '', Titolo, 1300, 750);
                
            }
            if (document.getElementById('idSelectedDom').value != 0) {
                var Titolo2 = 'Domanda' + today.getMinutes() + today.getSeconds();
               

                CenterPage2('../VSA/NuovaDomandaVSA/domandaNuova.aspx?CH=1&ID=' + document.getElementById('idSelectedDom').value + '', Titolo2, 1300, 750);
            }
        };
    </script>
    <style type="text/css">
        html
        {
            overflow: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Elenco Istanze"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnVisualizzaPG" runat="server" Text="Visualizza" ToolTip="Visualizza"
        OnClientClick="apriIstanza();" />
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
        OnClientClick="CancelEdit();" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        SelectedIndex="0" ShowBaseLine="true" OnClientTabSelecting="setResizeTabs"
        Width="100%">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="RadPageVecchiaNorm" Text="R.R. 1/2004 e s.m.i."
                ToolTip="Vecchia Normativa" Value="vecchia_normativa">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="RadPageNuovaNorm" Text="R.R. 4/2017 e s.m.i."
                ToolTip="Nuova Normativa" Value="nuova_normativa">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
        <telerik:RadPageView ID="RadPageVecchiaNorm" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <strong>VECCHIA NORMATIVA</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="dgvDich" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
                            EnableLinqExpressions="False" IsExporting="False" AllowPaging="false">
                            <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                ClientDataKeyNames="ID_DOM" DataKeyNames="ID_DOM">
                                <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                <CommandItemSettings ShowAddNewRecordButton="False" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID_DICH" HeaderText="ID" Visible="false" Exportable="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="TIPO DOMANDA" Exportable="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_PG" HeaderText="DATA PG" PickerType="DatePicker"
                                        EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false"
                                        Visible="true" Exportable="true">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="PG_DOMANDA" HeaderText="NUM. DOMANDA">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO_DOM" HeaderText="STATO DOMANDA">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PG_DICHIARAZIONE" HeaderText="NUM. DICHIARAZ.">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO_DICH" HeaderText="STATO DICHIARAZ.">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CAMBIO">
                                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="300" AllowScroll="true" />
                                <ClientEvents OnRowSelecting="RowSelectingDom" OnRowClick="RowSelectingDom" OnRowDblClick="ModificaDblClickDom" />
                            </ClientSettings>
                        </telerik:RadGrid>
                         <asp:HiddenField ID="idSelectedDom" runat="server" Value="0" ClientIDMode="Static" />
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageNuovaNorm" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <strong>NUOVA NORMATIVA</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="dgvIstanze" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
                            EnableLinqExpressions="False" IsExporting="False" AllowPaging="false">
                            <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                ClientDataKeyNames="ID_DICH" DataKeyNames="ID_DICH">
                                <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True"  />
                                <CommandItemSettings ShowAddNewRecordButton="False" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID_DICH" HeaderText="ID" Visible="false" Exportable="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PG" HeaderText="PROTOCOLLO" Exportable="false">
                                    <ItemStyle HorizontalAlign ="Center" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="TIPO_ISTANZA" HeaderText="TIPO ISTANZA" Exportable="false">
                                      <ItemStyle HorizontalAlign ="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="DATA_PRESENTAZIONE" HeaderText="DATA PRESENTAZIONE"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}"
                                        ShowFilterIcon="false" Visible="true" Exportable="true">
                                         <ItemStyle HorizontalAlign ="Center" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO ISTANZA">
                                     <ItemStyle HorizontalAlign ="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DECISIONE" HeaderText="DECISIONE">
                                     <ItemStyle HorizontalAlign ="Center" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="300" AllowScroll="true" />
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                <ClientEvents OnRowSelecting="RowSelectingPG" OnRowClick="RowSelectingPG" OnRowDblClick="ModificaDblClickPG" />
                            </ClientSettings>
                        </telerik:RadGrid>
                        <asp:HiddenField ID="idSelectedPG" runat="server" Value="0" ClientIDMode="Static" />
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="idContratto" runat="server" Value="" ClientIDMode="Static" />
</asp:Content>
