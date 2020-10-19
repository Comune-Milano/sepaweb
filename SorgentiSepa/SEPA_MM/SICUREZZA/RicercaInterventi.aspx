<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="RicercaInterventi.aspx.vb" Inherits="SICUREZZA_RicercaInterventi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function apriIntervento() {
            validNavigation = true;
            window.open('NuovoIntervento.aspx?NM=1&IDI=' + document.getElementById('HiddenFieldIntervento').value, '');
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="Label1" Text="Ricerca Interventi" runat="server" />
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
                                <td>
                                    <asp:Button ID="btnExport" runat="server" Text="Esporta interventi" ToolTip="Esporta in excel gli interventi" />
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
                                        &nbsp
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" Text="Assegnatario" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbAssegnatario" Width="250px" AppendDataBoundItems="true"
                                Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" Text="Co-assegnatario" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbCoAssegnatario" Width="250px" AppendDataBoundItems="true"
                                Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
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
                            <asp:Label ID="Label16" Text="Stato Intervento" runat="server" Font-Names="Arial"
                                Font-Size="8pt" />
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <div style="overflow: auto; width: 247px; border: 1px gray solid; background-color: White">
                                            <asp:CheckBoxList ID="CheckBoxListStato" runat="server">
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
                            <asp:Label ID="Label5" Text="Tipo intervento" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cmbTipoInterv" runat="server" Width="250px" EnableLoadOnDemand="true"
                                            OnClientLoad="OnClientLoadHandler">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%-- <tr>
                    <td>
                        <asp:Label ID="Label3" Text="Gruppo" runat="server" Font-Names="Arial" Font-Size="8pt" />
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cmbGruppo" runat="server" Width="250px" EnableLoadOnDemand="true"
                                        OnClientLoad="OnClientLoadHandler">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
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
                            <asp:Label ID="Label3" Text="Stato Segnalazione" runat="server" Font-Names="Arial"
                                Font-Size="8pt" />
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <div style="overflow: auto; width: 247px; border: 1px gray solid; background-color: White">
                                            <asp:CheckBoxList ID="chkListStatoSegn" runat="server">
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
                            <asp:Label ID="Label20" Text="Dal" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDal" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                            Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                            <DateInput ID="DateInput7" runat="server">
                                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                            </DateInput>
                                            <Calendar ID="Calendar1" runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label21" Text="Al" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtAl" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                            Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                            <DateInput ID="DateInput1" runat="server">
                                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                            </DateInput>
                                            <Calendar ID="Calendar2" runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" Text="Ordina risultati per" runat="server" Font-Bold="True"
                                Font-Names="Arial" Font-Size="8pt" Style="text-align: Left" Width="100%" Visible="False" />
                        </td>
                        <td>
                            <%--<div style="overflow: auto; width: 247px; border: 1px gray solid; background-color: White">
                            <asp:RadioButtonList ID="RadioButtonListOrdine" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Value="0" Text="Stato Segnalazione" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Urgenza"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Tipo Segnalazione"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>--%>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewRisultatiRicerca" runat="server">
                <asp:Label Text="" runat="server" ID="lblRisultati" />
                <asp:TextBox runat="server" ID="txtInterventoSelected" Text="" BackColor="Transparent"
                    BorderColor="Transparent" BorderWidth="0px" Font-Bold="True" Font-Names="arial"
                    Font-Size="9pt" ForeColor="Black" Width="95%" ReadOnly="true" ClientIDMode="Static" />
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 100%;">
                            <div id="divOverContent" style="width: 100%; overflow: auto;">
                                <telerik:RadGrid ID="RadGridInterventi" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" PageSize="100" Culture="it-IT"
                                    RegisterWithScriptManager="False" Font-Size="8pt" Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true"
                                    Width="95%">
                                    <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessun intervento presente"
                                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA">
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
                                             <telerik:GridBoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataField="DATA_ORA_INSERIM" HeaderText="DATA INSERIMENTO"
                                                HeaderStyle-Width="10%">
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn DataField="ASSEGNATARIO" HeaderText="ASSEGNATARIO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ASSEGNATARIO_2" HeaderText="CO-ASSEGNATARIO">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                        <HeaderStyle Wrap="True" />
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="false" />
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField runat="server" ID="idIntervento" ClientIDMode="Static" />
                <asp:HiddenField ID="HiddenFieldIntervento" runat="server" Value="0" ClientIDMode="Static" />
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
