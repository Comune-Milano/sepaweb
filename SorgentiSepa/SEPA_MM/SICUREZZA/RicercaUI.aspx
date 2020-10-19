<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="RicercaUI.aspx.vb" Inherits="SICUREZZA_RicercaUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function apriUI() {
            validNavigation = true;
            if (document.getElementById('HiddenFieldIdUI').value != '0') {
                window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=' + document.getElementById('HiddenFieldIdUI').value + '&COD=' + document.getElementById('codUI').value, '', 'height=580,width=780');
            }
            else {
                apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
            }
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="Label1" Text="Ricerca Unità" runat="server" />
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
                                    <asp:Button ID="btnVisualizza" runat="server" Text="Visualizza" ToolTip="Apre l'unità selezionato"
                                        OnClientClick="apriUI();return false;" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" runat="server" Text="Esporta unità" ToolTip="Esporta in excel le unità" />
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
                        <td style="width: 50%">
                            <asp:Panel runat="server" ID="Panel1">
                                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso" />
                                        </td>
                                        <td style="width: 80%">
                                            <telerik:RadComboBox ID="cmbComplesso" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                                Filter="Contains" IsCaseSensitive="false" Width="370px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio" />
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbEdificio" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                                Filter="Contains" IsCaseSensitive="false" Width="370px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo" />
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbIndirizzo" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                                Filter="Contains" IsCaseSensitive="false" Width="370px" Height="200">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Civico" />
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbCivico" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                                Filter="Contains" IsCaseSensitive="false" Width="370px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" Text="Scala" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbScala" runat="server" Width="100px" EnableLoadOnDemand="true"
                                                            AutoPostBack="true">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" Text="Interno" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbInterno" runat="server" Width="100px" EnableLoadOnDemand="true"
                                                AutoPostBack="true">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        <asp:Label ID="Label11" Text="Tipologia" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td style="width: 80%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <div style="overflow: auto; height: 190px; width: 365px; border: 1px gray solid;
                                                        background-color: White">
                                                        <asp:CheckBoxList ID="CheckBoxListTipo" runat="server">
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
                                        <asp:Label ID="Label12" Text="Condominio" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbCondominio" runat="server" Width="100px" EnableLoadOnDemand="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="" Value="-1" />
                                                        </Items>
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="NO" Value="0" />
                                                        </Items>
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="SI" Value="1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="Label13" Text="Per Handicap" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        Width="170px" />
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbHandicap" runat="server" Width="100px" EnableLoadOnDemand="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="" Value="-1" />
                                                        </Items>
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="NO" Value="0" />
                                                        </Items>
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="SI" Value="1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" Text="Dest. uso" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbDestUso" runat="server" Width="370px" EnableLoadOnDemand="true"
                                            OnClientLoad="OnClientLoadHandler">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" Text="Disponibilità" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbDisponibilita" runat="server" Width="370px" EnableLoadOnDemand="true"
                                            OnClientLoad="OnClientLoadHandler">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label54" Text="Codice unità" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodUI" runat="server" MaxLength="100" Font-Names="Arial" Font-Size="8pt"
                                            Width="365px" CssClass="CssMaiuscolo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label21" Text="Sup.Netta Da" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupNettaDa" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" Text="Sup.Netta A" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupNettaA" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewRisultatiRicerca" runat="server">
                <asp:Label Text="" runat="server" ID="lblRisultati" />
                <asp:TextBox runat="server" ID="txtUISelected" Text="" BackColor="Transparent" BorderColor="Transparent"
                    BorderWidth="0px" Font-Bold="True" Font-Names="arial" Font-Size="9pt" ForeColor="Black"
                    Width="95%" ReadOnly="true" ClientIDMode="Static" />
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 100%;">
                            <div id="divOverContent" style="width: 100%; overflow: auto;">
                                <telerik:RadGrid ID="RadGridUnita" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                    AllowPaging="True" IsExporting="False" Skin="Web20" AllowFilteringByColumn="False"
                                    PageSize="100" Width="95%">
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                        <Selecting AllowRowSelect="True"></Selecting>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessuna unità presente"
                                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD. UNITA'">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="EDIFICIO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FOGLIO" HeaderText="FOGLIO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NUMERO" HeaderText="PART.">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SUB" HeaderText="SUB">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COMUNE_UNITA" HeaderText="COMUNE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PIANO" HeaderText="PIANO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="INTERNO" HeaderText="INTERNO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="S_NETTA" HeaderText="SUP.NETTA">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <PagerStyle AlwaysVisible="True" />
                                    </MasterTableView><ClientSettings AllowDragToGroup="True">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        <Selecting AllowRowSelect="True" />
                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" EnableRealTimeResize="true"
                                            AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="True" />
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
        <asp:HiddenField runat="server" ID="codUI" ClientIDMode="Static" />
        <asp:HiddenField ID="HiddenFieldIdUI" runat="server" Value="0" ClientIDMode="Static" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
</asp:Content>
