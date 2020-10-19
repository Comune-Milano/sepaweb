<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="RicercaSegnalazioni.aspx.vb" Inherits="SICUREZZA_RicercaSegnalazioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="js/HGridScript.js"></script>
    <script type="text/javascript">
        var selezionato;
        var noCaricamento = 0;
        function Apri() {
            document.getElementById('CPMenu_btnVisualizza').click();
        };
    </script>
    <link rel="stylesheet" href="../AUTOCOMPLETE/cmbstyle/chosen.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label Text="Ricerca Segnalazioni" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPMenu" runat="Server">
    <table border="0" cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <asp:MultiView ID="MultiViewBottoni" runat="server" ActiveViewIndex="0">
                    <asp:View ID="ViewBottoniRicerca" runat="server">
                        <table border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    <asp:Button ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca"
                                        OnClientClick="document.getElementById('HiddenFieldVista').value='1'" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExport3" runat="server" Text="Esporta tutte le segnalazioni" ToolTip="Esporta in excel tutte le segnalazioni" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="ViewBottoniRisultati" runat="server">
                        <table border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    <asp:Button ID="btnVisualizza" runat="server" Text="Visualizza" ToolTip="Apri la segnalazione selezionata" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" runat="server" Text="Esporta segnalazioni" ToolTip="Esporta in excel le segnalazioni"
                                        OnClientClick="document.getElementById('HiddenFieldVista').value='1'" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExport2" runat="server" Text="Esporta segnalazioni con figli"
                                        ToolTip="Esporta in excel le segnalazioni con relativi figli" OnClientClick="document.getElementById('HiddenFieldVista').value='1'" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNuovaRicerca" runat="server" Text="Nuova Ricerca" ToolTip="Effettua una nuova ricerca"
                                        OnClientClick="document.getElementById('HiddenFieldVista').value='0'" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
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
                        <asp:Label ID="Label9" Text="Segnalante" runat="server" Font-Names="Arial" Font-Size="8pt" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSegnalante" runat="server" MaxLength="100" Font-Names="Arial"
                            Font-Size="8pt" Width="240px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label16" Text="Stato Segnalazione" runat="server" Font-Names="Arial"
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
                        <asp:Label ID="Label5" Text="Categoria segnalazione" runat="server" Font-Names="Arial"
                            Font-Size="8pt" />
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cmbTipoSegnalazione" runat="server" Width="250px" EnableLoadOnDemand="true"
                                        OnClientLoad="OnClientLoadHandler">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label17" Text="Categoria 1" runat="server" Font-Names="Arial" Font-Size="8pt" />
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbTipoSegnalazioneLivello1" runat="server" Width="250px"
                            EnableLoadOnDemand="true" OnClientLoad="OnClientLoadHandler">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label19" Text="Numero" runat="server" Font-Names="Arial" Font-Size="8pt" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxNumero" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="100px"></asp:TextBox>
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
                                    <telerik:RadDatePicker ID="txtDal" runat="server" WrapperTableCaption="" MaxDate="9999-01-01"
                                        Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Culture="it-IT">
                                        <DateInput ID="DateInput7" runat="server">
                                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                        </DateInput>
                                        <Calendar runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today">
                                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <asp:TextBox ID="TextBoxOreDal" runat="server" ToolTip="ore" Width="25px" Font-Names="Arial"
                                        Font-Size="8pt"></asp:TextBox>
                                    <asp:Label ID="Label6" runat="server" Width="10px" TabIndex="-1" Font-Names="Arial"
                                        Style="text-align: center" Font-Size="8pt">:</asp:Label>
                                    <asp:TextBox ID="TextBoxMinutiDal" runat="server" ToolTip="minuti" Width="25px" Font-Names="Arial"
                                        Font-Size="8pt"></asp:TextBox>
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
                                    </telerik:RadDatePicker>
                                    <asp:TextBox ID="TextBoxOreAl" runat="server" ToolTip="ore" Width="25px" Font-Names="Arial"
                                        Font-Size="8pt"></asp:TextBox>
                                    <asp:Label ID="Label8" runat="server" Width="10px" TabIndex="-1" Font-Names="Arial"
                                        Style="text-align: center" Font-Size="8pt">:</asp:Label>
                                    <asp:TextBox ID="TextBoxMinutiAl" runat="server" ToolTip="minuti" Width="25px" Font-Names="Arial"
                                        Font-Size="8pt"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Solo segnalazioni con ticket figli
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBoxSegnalazioniFigli" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" Text="Ordina risultati per" runat="server" Font-Bold="True"
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
            <script src="../AUTOCOMPLETE/cmbscript/chosen.jquery.js" type="text/javascript"></script>
            <script type="text/javascript">
                $(".chzn-select").chosen({
                    disable_search_threshold: 10,
                    no_results_text: "Nessun risultato trovato!",
                    placeholder_text_single: "- - -",
                    width: "95%"
                });
            </script>
        </asp:View>
        <asp:View ID="ViewRisultatiRicerca" runat="server">
            <asp:Label Text="" runat="server" ID="lblRisultati" />
            <asp:TextBox runat="server" ID="TextBox1" Text="" BackColor="Transparent" BorderColor="Transparent"
                BorderWidth="0px" Font-Bold="True" Font-Names="arial" Font-Size="9pt" ForeColor="Black"
                Width="95%" ReadOnly="true" />
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <%-- <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                            <AjaxSettings>
                                <telerik:AjaxSetting AjaxControlID="RadGridSegnalazioni">
                                    <UpdatedControls>
                                        <telerik:AjaxUpdatedControl ControlID="RadGridSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1">
                                        </telerik:AjaxUpdatedControl>
                                    </UpdatedControls>
                                </telerik:AjaxSetting>
                            </AjaxSettings>
                        </telerik:RadAjaxManager>
                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
                        </telerik:RadAjaxLoadingPanel>--%>
                        <div id="divOverContent" style="width: 100%; overflow: auto;">
                            <telerik:RadGrid ID="RadGridSegnalazioni" runat="server" AllowPaging="True" AllowSorting="True"
                                GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                                PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                                Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Width="1000px" Height="400px"
                                ShowHeadersWhenNoRecords="False">
                                <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessuna segnalazione da visualizzare."
                                    HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false">
                                    <DetailTables>
                                        <telerik:GridTableView Name="Dettagli" AllowPaging="false" BackColor="Azure" HierarchyDefaultExpanded="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" EmptyDataText="">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="3%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO0" HeaderText="CATEGORIA" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CATEGORIA 1" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="23%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CODICE_RU" HeaderText="CODICE CONTRATTO" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INS." ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle Width="4%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="true">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" Wrap="True" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FILIALE" HeaderText="SEDE TERRITORIALE" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="4%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOTE_C" HeaderText="NOTA DI CHIUSURA" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="4%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FIGLI2" HeaderText="" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE"
                                                    Visible="false" EmptyDataText="">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_SEGNALAZIONE_PADRE" HeaderText="N° SEGN. PADRE"
                                                    EmptyDataText="">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE ORDINE">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DATA_CHIUSURA" HeaderText="DATA CHIUSURA SEGNALAZIONE">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <PagerStyle AlwaysVisible="True" />
                                        </telerik:GridTableView></DetailTables>
                                    <RowIndicatorColumn Visible="False">
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Created="True">
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" EmptyDataText=" ">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="3%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="" Visible="false" EmptyDataText=" ">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO0" HeaderText="CATEGORIA" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="7%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CATEGORIA 1" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="23%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CODICE_RU" HeaderText="CODICE CONTRATTO" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INS." ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle Width="4%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Wrap="true">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="True" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FILIALE" HeaderText="SEDE TERRITORIALE" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="4%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOTE_C" HeaderText="NOTA DI CHIUSURA" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="4%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FIGLI2" HeaderText="" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE"
                                            Visible="false" EmptyDataText=" ">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_SEGNALAZIONE_PADRE" HeaderText="N° SEGN. PADRE"
                                            EmptyDataText=" ">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE ORDINE">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_CHIUSURA" HeaderText="DATA CHIUSURA SEGNALAZIONE">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                    <HeaderStyle Wrap="True" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowDragToGroup="false"
                                    AllowAutoScrollOnDragDrop="false" AllowRowsDragDrop="false">
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
            <asp:HiddenField runat="server" ID="idSegnalazione" ClientIDMode="Static" />
            <asp:HiddenField ID="HiddenFieldRigaSelezionata" runat="server" Value="0" ClientIDMode="Static" />
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenFieldVista" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="AltezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <script type="text/javascript">
        validNavigation = false;
        $(document).ready(function () {
            var altezzaRad = $(window).height() - 305;
            var larghezzaRad = $(window).width() - 50;
            $("#MasterPage_CPContenuto_RadGridSegnalazioni").width(larghezzaRad);
            $("#MasterPage_CPContenuto_RadGridSegnalazioni").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;

        });
        $(window).resize(function () {
            var altezzaRad = $(window).height() - 305;
            var larghezzaRad = $(window).width() - 50;
            $("#MasterPage_CPContenuto_RadGridSegnalazioni").width(larghezzaRad);
            $("#MasterPage_CPContenuto_RadGridSegnalazioni").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        });
//        $(function () {
//            $("#CPContenuto_txtDal").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
//            $("#CPContenuto_txtAl").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
//        });
    </script>
</asp:Content>
