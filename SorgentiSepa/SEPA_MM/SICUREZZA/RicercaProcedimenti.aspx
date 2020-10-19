<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="RicercaProcedimenti.aspx.vb" Inherits="SICUREZZA_RicercaProcedimenti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function apriProc() {
            validNavigation = true;
            window.open('NuovoProcedimento.aspx?NM=1&IDP=' + document.getElementById('HiddenFieldProcedimento').value, '');
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="Label1" Text="Ricerca Procedimenti" runat="server" />
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
                                    <asp:Button ID="btnVisualizza" runat="server" Text="Visualizza" ToolTip="Apre il procedimento selezionato" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" runat="server" Text="Esporta procedimenti" ToolTip="Esporta in excel i procedimenti" />
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
    <asp:MultiView ID="MultiViewRicerca" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewParametriRicerca" runat="server">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Tipo Procedimento" Font-Names="arial"
                            Font-Size="8pt"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbTipoProc" runat="server" Width="250px" EnableLoadOnDemand="true"
                            OnClientLoad="OnClientLoadHandler">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="- - -" Value="-1" />
                            </Items>
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Penale" Value="penale" />
                            </Items>
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Civile" Value="civile" />
                            </Items>
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Amministrativo" Value="ammvo" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label16" Text="Stato Procedimento" runat="server" Font-Names="Arial"
                            Font-Size="8pt" />
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cmbStatoProc" runat="server" Width="250px" EnableLoadOnDemand="true"
                                        OnClientLoad="OnClientLoadHandler">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="- - -" Value="-1" Owner="cmbStatoProc" />
                                            <telerik:RadComboBoxItem runat="server" Text="In Corso" Value="1" Owner="cmbStatoProc" />
                                            <telerik:RadComboBoxItem runat="server" Text="Chiuso" Value="2" Owner="cmbStatoProc">
                                            </telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" Text="Referente" runat="server" Font-Names="Arial" Font-Size="8pt" />
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtReferente" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="240px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label19" Text="Num. intervento" runat="server" Font-Names="Arial"
                            Font-Size="8pt" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxNumero" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="68px"></asp:TextBox>
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
            </table>
        </asp:View>
        <asp:View ID="ViewRisultatiRicerca" runat="server">
            <asp:Label Text="" runat="server" ID="lblRisultati" />
            <asp:TextBox runat="server" ID="txtProcedimSelected" Text="" BackColor="Transparent"
                BorderColor="Transparent" BorderWidth="0px" Font-Bold="True" Font-Names="arial"
                Font-Size="9pt" ForeColor="Black" Width="95%" ReadOnly="true" ClientIDMode="Static" />
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <div id="divOverContent" style="width: 100%; overflow: auto;">
                            <telerik:RadGrid ID="RadGridProcedimenti" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                                ResolvedRenderMode="Classic" AutoGenerateColumns="False" PageSize="100" Culture="it-IT"
                                RegisterWithScriptManager="False" Font-Size="8pt" Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true"
                                Width="95%">
                                <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessun procedimento presente"
                                    HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="REFERENTE" HeaderText="REFERENTE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_ORA_INSERIMENTO" HeaderText="DATA INSERIMENTO"
                                            HeaderStyle-Width="10%">
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
            <asp:HiddenField runat="server" ID="idProcedimento" ClientIDMode="Static" />
            <asp:HiddenField ID="HiddenFieldProcedimento" runat="server" Value="0" ClientIDMode="Static" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
