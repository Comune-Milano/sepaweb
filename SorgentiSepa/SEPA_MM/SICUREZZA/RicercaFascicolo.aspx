<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="RicercaFascicolo.aspx.vb" Inherits="SICUREZZA_RicercaFascicolo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function StampaFrontespizio() {
            validNavigation = true;
            window.open('StampaFascicolo.aspx?IDF=' + document.getElementById('HiddenFieldFascicolo').value, '');
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:MultiView ID="MultiViewBottoni" runat="server" ActiveViewIndex="0">
                    <asp:View ID="ViewBottoniRicerca" runat="server">
                        <table border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    <asp:Button ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="ViewBottoniRisultati" runat="server">
                        <table border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    <asp:Button ID="btnVisualizza" runat="server" Text="Visualizza" ToolTip="Apre l'intervento selezionato" />
                                </td>
                                <td class="nascondiPulsante">
                                    <asp:Button ID="btnStampa" runat="server" Text="Stampa frontespizio" ToolTip="Stampa frontespizio" />
                                </td>
                                <td class="nascondiPulsante">
                                    <asp:Button ID="btnElencoStampe" runat="server" Text="Elenco stampe" ToolTip="Visualizza stampe"
                                        Style="display: none;" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNuovaRicerca" runat="server" Text="Nuova Ricerca" ToolTip="Effettua una nuova ricerca" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
            <td>
                <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:Panel runat="server" ID="Multi">
        <asp:MultiView ID="MultiViewRicerca" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewParametriRicerca" runat="server">
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:Label ID="Label0" Text="Sede Territoriale" runat="server" Font-Names="Arial"
                                Font-Size="8pt" />
                        </td>
                        <td style="width: 80%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <div style="overflow: auto; width: 247px; border: 1px gray solid; background-color: White">
                                            <asp:CheckBoxList ID="CheckBoxListSedi" runat="server" AutoPostBack="True">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" Text="Complesso" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbComplesso" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                Filter="Contains" AutoPostBack="True" Width="250px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label22" Text="Edificio" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbEdificio" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                Filter="Contains" AutoPostBack="True" Width="250px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" Text="Cod. unità" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodUI" runat="server" Font-Names="Arial" Font-Size="8pt" Width="240px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label19" Text="Num. segnalazione" runat="server" Font-Names="Arial"
                                Font-Size="8pt" />
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxNumero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="68px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" Text="Num. intervento" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumIntervento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="68px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" Text="Num. fascicolo" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumFascicolo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="68px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewRisultatiRicerca" runat="server">
                <asp:Label Text="" runat="server" ID="lblRisultati" />
                <asp:TextBox runat="server" ID="txtFascicoloSelected" Text="" BackColor="Transparent"
                    BorderColor="Transparent" BorderWidth="0px" Font-Bold="True" Font-Names="arial"
                    Font-Size="9pt" ForeColor="Black" Width="95%" ReadOnly="true" ClientIDMode="Static" />
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 100%;">
                            <div id="divOverContent" style="width: 100%; overflow: auto;">
                                <telerik:RadGrid ID="RadGridFascicoli" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" PageSize="100" Culture="it-IT"
                                    RegisterWithScriptManager="False" Font-Size="8pt" Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true"
                                    Width="95%">
                                    <MasterTableView Name="TableInterv" EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessun fascicolo presente"
                                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                                        <DetailTables>
                                            <telerik:GridTableView Name="Dettagli" AllowPaging="false" BackColor="Azure" HierarchyDefaultExpanded="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" EmptyDataText="">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridDateTimeColumn DataField="DATA_ORA_INSERIM" HeaderText="DATA INSERIMENTO"
                                                        HeaderStyle-Width="10%">
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridBoundColumn DataField="ASSEGNATARIO" HeaderText="ASSEGNATARIO">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ASSEGNATARIO_2" HeaderText="CO-ASSEGNATARIO">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <PagerStyle AlwaysVisible="True" />
                                            </telerik:GridTableView></DetailTables>
                                        <RowIndicatorColumn Visible="False">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Created="True">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD. UNITA IMMOBILIARE"
                                                EmptyDataText=" ">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" EmptyDataText=" ">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                        <HeaderStyle Wrap="True" />
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true" AllowDragToGroup="false" AllowAutoScrollOnDragDrop="false"
                                        AllowRowsDragDrop="false">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        <Selecting AllowRowSelect="True" />
                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                            AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="True" />
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField runat="server" ID="idIntervento" ClientIDMode="Static" />
                <asp:HiddenField ID="HiddenFieldFascicolo" runat="server" Value="0" ClientIDMode="Static" />
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
