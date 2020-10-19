<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FondoSolidarieta.aspx.vb"
    Inherits="Fondo_solidarieta_FondoSolidarieta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fondo solidarietà</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../StandardTelerik/Scripts/jsFunzioni.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <script type="text/javascript">
        window.onresize = ResizeGrid;
        Sys.Application.add_load(ResizeGrid);
        function ResizeGrid() {
            var scrollArea = document.getElementById("<%= RadGrid.ClientID %>" + "_GridData");
            scrollArea.style.height = window.screen.height - 350 + 'px';
        };
        function apriDettaglio() {

            document.getElementById('btnCaricaDati').click();

        };
        function closeWindow(sender, args, nomeRad) {
            var radwindow = $find(nomeRad);
            radwindow.close();
        };
        function PrintDoc() {

            window.open('PrintPresentazione.aspx?DPR=' + document.getElementById('RadWindowInfoRU_C_txtDataPr').value.replace('-', '').replace('-', '') + '&NZ=' + document.getElementById('nomeZona').value + '&IDC=' + document.getElementById('idContr').value, 'letPrDoc', '');
        };
        function DisconettiUtente() {
            self.close();
            window.open('LoginFondoSolidarieta.aspx', 'loginFS', '');
        };
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ExportToExcel") >= 0) {
                args.set_enableAjax(false);
            }
        };
    </script>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="requestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelRadGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 90%;">
                <asp:Label ID="lblTitolo" runat="server" Text="Fondo solidarietà" CssClass="testoGrassettoMaiuscoloBlu"
                    Font-Size="20px"></asp:Label>
            </td>
            <td style="font-size: 10pt; font-family: Arial; color: #416094; font-weight: bold;">
                <asp:Label ID="lblUtente" runat="server" Text="Utente:"></asp:Label>
                <asp:Label ID="lblUtente2" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="LinkLogOut" runat="server" ToolTip="Disconnetti utente" OnClientClick="DisconettiUtente();">Logout</asp:LinkButton>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel runat="server" ID="PanelRadGrid">
                    <telerik:RadGrid ID="RadGrid" runat="server" GroupPanelPosition="Top" resolvedrendermode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
                        AllowPaging="true" isexporting="True" PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top" HierarchyLoadMode="ServerOnDemand">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <%--<DetailTables>
                                <telerik:GridTableView Name="Dettagli" Width="100%" AllowPaging="false" BackColor="Azure"
                                    HierarchyDefaultExpanded="true" AllowFilteringByColumn="false">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TIPO_RU" HeaderText="TIPO RU" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO_SPECIFICO" HeaderText="TIPO SPECIFICO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DECORRENZA" HeaderText="DECORRENZA" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DISDETTA" HeaderText="DISDETTA" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AREA_AU_2013" HeaderText="AREA AU 2013" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLASSE_AU_2013" HeaderText="CLASSE AU 2013" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AREA_AU_2015" HeaderText="AREA AU 2015" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLASSE_AU_2015" HeaderText="CLASSE AU 2015" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ISEE_ERP_AU_2015" HeaderText="ISEE ERP AU 2015" Visible="true">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>--%>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID_CONTRATTO" HeaderText="ID" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME" Visible="true"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME" Visible="true" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COD_FISCALE" HeaderText="CODICE FISCALE" Visible="true"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE RAPPORTO" Visible="true"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="true" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" Visible="true"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CIVICO" HeaderText="CIVICO" Visible="true" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_PR" HeaderText="DATA PRES." Visible="true"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ORA_PR" HeaderText="ORA PRES." Visible="true"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings OpenInNewWindow="true" IgnorePaging="true">
                            <Excel FileExtension="xls" Format="Biff" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="true" />
                            <ClientEvents OnRowDblClick="apriDettaglio" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <telerik:RadWindow ID="RadWindowInfoRU" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize" Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="contenutoImpianto">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="Label1" runat="server" Text="Dettagli RU" CssClass="testoGrassettoMaiuscoloBlu"
                                Font-Size="15px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="Table3" border="0" class="module" width="480px">
                                <tr>
                                    <td width="320px">
                                        Tipo RU:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtTipoRU" runat="server" ClientIDMode="Static" CssClass="CssMaiuscolo"
                                            Text="" Width="150px" BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tipo Specifico:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtTipoSpecifico" runat="server" ClientIDMode="Static" CssClass="CssMaiuscolo"
                                            Text="" Width="150px" BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Stato RU:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtStatoRU" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Indirizzo:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtIndirizzo" runat="server" CssClass="CssMaiuscolo" Text="" Width="320px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Data decorrenza:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtDataDecorr" runat="server" Text="" Width="150px" BorderStyle="None"
                                            Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Data disdetta:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtDataDisdetta" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Area AU 2013:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtAreaAU2013" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Classe AU 2013:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtClasseAU2013" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Area AU 2015:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtAreaAU2015" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Classe AU 2015:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtClasseAU2015" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Isee ERP AU 2015:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtISEE" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Area RECA:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtAreaReca" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Classe RECA:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtClasseReca" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Isee ERP RECA:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtISEEreca" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Saldo attuale (scaduto):
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSaldoAttuale" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Saldo al 07/09/2016:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSaldo2015" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True" ForeColor="#416094">
                                        </asp:Label>+
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Saldo condominio:
                                    </td>
                                    <td style="border-bottom: 1px solid #000000">
                                        <asp:Label ID="txtSaldoCond" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True" ForeColor="#416094">
                                        </asp:Label>=
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tot. saldo:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtTotSaldo" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True" ForeColor="#416094">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Rateizzazione:
                                    </td>
                                    <td>
                                        <asp:Label ID="txtRateizzazione" runat="server" CssClass="CssMaiuscolo" Text="" Width="150px"
                                            BorderStyle="None" Font-Bold="True">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Data presentazione:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtDataPr" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                        Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" DatePopupButton-Visible="false"
                                                        Width="100px">
                                                        <DateInput ID="DateInput1" runat="server">
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
                                                    <asp:ImageButton ID="btnCanc" runat="server" ImageUrl="Delete.png" ToolTip="Cancella"
                                                        Visible="False" />
                                                    <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="print-icon.png" ToolTip="Stampa" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciAggregazioneImpianto" runat="server" Text="Esci"
                                            AutoPostBack="false" OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowInfoRU', '');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <asp:HiddenField ID="idSel" runat="server" Value="" />
    <asp:HiddenField ID="idContr" runat="server" Value="0" />
    <asp:HiddenField ID="idZona" runat="server" Value="" />
    <asp:HiddenField ID="nomeZona" runat="server" Value="" />
    <asp:Button ID="btnCaricaDati" runat="server" Text="" Style="display: none;" />
    </form>
</body>
</html>
