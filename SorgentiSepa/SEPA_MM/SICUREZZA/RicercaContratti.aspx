<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="RicercaContratti.aspx.vb" Inherits="SICUREZZA_RicercaContratti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function apriRU() {
            validNavigation = true;
            if (document.getElementById('HiddenFieldIdContr').value != '0') {
                window.open('../Contratti/Contratto.aspx?ID=' + document.getElementById('HiddenFieldIdContr').value + '&COD=' + document.getElementById('codContratto').value, '', 'height=780,width=1160');
            }
            else {
                apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null);
            }
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="Label1" Text="Ricerca Contratti" runat="server" />
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
                                    <asp:Button ID="btnVisualizza" runat="server" Text="Visualizza" ToolTip="Apre il contratto selezionato"
                                        OnClientClick="apriRU();return false;" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" runat="server" Text="Esporta contratti" ToolTip="Esporta in excel i contratti" />
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
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        <asp:Label ID="Label0" Text="Cognome" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td style="width: 80%">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCognome" runat="server" MaxLength="100" Font-Names="Arial" Font-Size="8pt"
                                                        Width="365px" CssClass="CssMaiuscolo"></asp:TextBox>
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
                                        <asp:Label ID="Label11" Text="Nome" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNome" runat="server" MaxLength="100" Font-Names="Arial" Font-Size="8pt"
                                            Width="365px" CssClass="CssMaiuscolo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label22" Text="Cod.fiscale" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCF" runat="server" MaxLength="100" Font-Names="Arial" Font-Size="8pt"
                                            Width="365px" CssClass="CssMaiuscolo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" Text="Partita IVA" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpiva" runat="server" MaxLength="100" Font-Names="Arial" Font-Size="8pt"
                                            Width="365px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" Text="Ragione sociale" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRagione" runat="server" MaxLength="100" Font-Names="Arial" Font-Size="8pt"
                                            Width="365px" CssClass="CssMaiuscolo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" Text="Stipula Dal" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtStipulaDal" runat="server" WrapperTableCaption="" MinDate="1900-01-01"
                                                        MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
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
                                                <td>
                                                    <asp:Label ID="Label12" Text="Stipula al" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        Width="55px" />
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtStipulaAl" runat="server" WrapperTableCaption="" MinDate="1900-01-01"
                                                        MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
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
                                        <asp:Label ID="Label21" Text="Sloggio dal" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtSloggioDal" runat="server" WrapperTableCaption="" MinDate="1900-01-01"
                                                        MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                                        <DateInput ID="DateInput2" runat="server">
                                                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                        </DateInput>
                                                        <Calendar ID="Calendar3" runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" Text="Sloggio al" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        Width="55px" />
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtSloggioAl" runat="server" WrapperTableCaption="" MinDate="1900-01-01"
                                                        MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                                        <DateInput ID="DateInput3" runat="server">
                                                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                        </DateInput>
                                                        <Calendar ID="Calendar4" runat="server">
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
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; vertical-align: top">
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        <asp:Label ID="Label16" Text="Stato rapporto" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td style="width: 80%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbStato" runat="server" Width="370px" EnableLoadOnDemand="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text=" " Value="-1" />
                                                        </Items>
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="Bozza" Value="Bozza" />
                                                        </Items>
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="In Corso" Value="In Corso" />
                                                        </Items>
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="In Corso S.T." Value="In Corso S.T." />
                                                        </Items>
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="Chiuso" Value="Chiuso" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" Text="Codice rapporto" runat="server" Font-Names="Arial"
                                            Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCod" runat="server" MaxLength="100" Font-Names="Arial" Font-Size="8pt"
                                            Width="365px" CssClass="CssMaiuscolo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label54" Text="Codice unità" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtUnita" runat="server" MaxLength="100" Font-Names="Arial" Font-Size="8pt"
                                                        Width="365px" CssClass="CssMaiuscolo"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" Text="Tipo rapporto" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbTipo" runat="server" Width="370px" EnableLoadOnDemand="true">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; vertical-align: top">
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
                                                Filter="Contains" IsCaseSensitive="false" Width="370px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewRisultatiRicerca" runat="server">
                <asp:Label Text="" runat="server" ID="lblRisultati" />
                <asp:TextBox runat="server" ID="txtRUSelected" Text="" BackColor="Transparent" BorderColor="Transparent"
                    BorderWidth="0px" Font-Bold="True" Font-Names="arial" Font-Size="9pt" ForeColor="Black"
                    Width="95%" ReadOnly="true" ClientIDMode="Static" />
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 100%;">
                            <div id="divOverContent" style="width: 100%; overflow: auto;">
                                <telerik:RadGrid ID="RadGridContratti" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                    AllowPaging="True" IsExporting="False" Skin="Web20" AllowFilteringByColumn="False"
                                    PageSize="100" Width="95%">
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                        <Selecting AllowRowSelect="True"></Selecting>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessun contratto presente"
                                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NOME_INTEST" HeaderText="INTESTATARIO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="STATO_DEL_CONTRATTO" HeaderText="STATO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COD_TIPOLOGIA_CONTR_LOC" HeaderText="TIPO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TIPO_SPECIFICO" HeaderText="TIPO CONTR. SPEC.">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CIVICO" HeaderText="CIVICO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COMUNE_UNITA" HeaderText="COMUNE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PIANO" HeaderText="PIANO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="INTERNO" HeaderText="INTERNO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA">
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
        <asp:HiddenField runat="server" ID="codContratto" ClientIDMode="Static" />
        <asp:HiddenField ID="HiddenFieldIdContr" runat="server" Value="0" ClientIDMode="Static" />
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
