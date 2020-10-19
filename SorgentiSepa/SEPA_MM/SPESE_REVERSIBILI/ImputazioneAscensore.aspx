<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="ImputazioneAscensore.aspx.vb" Inherits="SPESE_REVERSIBILI_ImputazioneAscensore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelEdifici">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelEdifici" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla" />
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="Buttons"
        DecorationZoneID="decorationZone"></telerik:RadFormDecorator>
    <div id="decorationZone">
        <telerik:RadTabStrip runat="server" ID="RadTabStrip" Orientation="HorizontalTop"
            OnClientTabSelecting="setIndicePulsanti" Width="100%" MultiPageID="RadMultiPage1"
            ShowBaseLine="false">
            <Tabs>
                <telerik:RadTab runat="server" PageViewID="RadPageView2" Text="Dettaglio Impianti" Selected="true"
                    Value="ImpAscensore">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" CssClass="multiPage" Width="100%"
            BorderColor="DarkGray" BorderStyle="Solid" BorderWidth="1px" ScrollChildren="true">
            <telerik:RadPageView runat="server" ID="RadPageView2" Selected="true">
                <asp:Panel runat="server" ID="PanelEdifici">
                    <telerik:RadGrid ID="dgvEdifici" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            TableLayout="Auto" DataKeyNames="ID" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <%--  <ColumnGroups>
                                <telerik:GridColumnGroup HeaderText="ANAGRAFICA COMPLESSO/EDIFICIO" Name="AnaComplessoEdificio"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                </telerik:GridColumnGroup>
                                <telerik:GridColumnGroup HeaderText="ANAGRAFICA IMPIANTO" Name="AnagraficaImpianto"
                                    HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                                <telerik:GridColumnGroup HeaderText="PRECEDENTE APPALTO ALER EREDITATO" Name="PrecAppalto"
                                    HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                                <telerik:GridColumnGroup HeaderText="PREZZI AL NETTO DELLO SCONTO COMPRESI ONERI E IVA AL 10%"
                                    Name="PrezziNetto" HeaderStyle-Font-Italic="true" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                            </ColumnGroups>--%>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO" AutoPostBackOnFilter="true"
                                                FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="47%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COD_EDIFICIO" HeaderText="COD. EDIFICIO" AutoPostBackOnFilter="true"
                                                FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="NOME_EDIFICIO" HeaderText="EDIFICIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" AutoPostBackOnFilter="true"
                                    FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MATRICOLA_IMPIANTO" HeaderText="MATRICOLA" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="INDIRIZZO_IMPIANTO" HeaderText="INDIRIZZO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SCALA_IMPIANTO" HeaderText="SCALA" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LOCALITA" HeaderText="LOCALITA'" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtTipologia" runat="server" NumberFormat-DecimalDigits="0"
                                            DataType="System.Decimal" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'
                                            Width="50px" MinValue="0" Style="text-align: right">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="FERMATE" HeaderText="FERMATE" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtFermate" runat="server" NumberFormat-DecimalDigits="0"
                                            DataType="System.Decimal" Text='<%# DataBinder.Eval(Container, "DataItem.FERMATE") %>'
                                            Width="50px" MinValue="0" Style="text-align: right">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                   <telerik:GridBoundColumn DataField="PU_VISITA_MENSILE" HeaderText="P.U. VISITA MENSILE"
                                     AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="EqualTo"
                                    DataFormatString="{0:C2}">
                                    <FooterStyle Wrap="false" />
                                </telerik:GridBoundColumn>
                               <%-- <telerik:GridTemplateColumn DataField="PU_VISITA_MENSILE" HeaderText="P.U. VISITA MENSILE"
                                    AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtPuVisitaMensile" runat="server" NumberFormat-DecimalDigits="0"
                                            DataType="System.Decimal" Text='<%# DataBinder.Eval(Container, "DataItem.PU_VISITA_MENSILE") %>'
                                            Width="50px" MinValue="0" Style="text-align: right">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                                <telerik:GridTemplateColumn DataField="N_VISITE_MENSILI" HeaderText="N° VISITE ANNUO"
                                    AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtNumVisiteMensili" runat="server" NumberFormat-DecimalDigits="0"
                                            DataType="System.Decimal" Text='<%# DataBinder.Eval(Container, "DataItem.N_VISITE_MENSILI") %>'
                                            Width="50px" MinValue="0" Style="text-align: right">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="TOTALE_IMPIANTO" HeaderText="TOTALE IMPIANTO"
                                    Aggregate="Sum" AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="EqualTo"
                                    DataFormatString="{0:C2}">
                                    <FooterStyle Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO" HeaderText="TOTALE ANNUO SCONTATO"
                                    Aggregate="Sum" AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="EqualTo"
                                    DataFormatString="{0:C2}">
                                    <FooterStyle Wrap="false" />
                                </telerik:GridBoundColumn>
                               
                            </Columns>
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh" OnClientClick="caricamento(2);"
                                                        CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClientClick="caricamento(2);"
                                                            OnClick="Esporta_Click" CommandName="ExportToExcel" CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                            ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" FrozenColumnsCount="1" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Width="400"
                        Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3600"
                        Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
                    </telerik:RadNotification>
                </asp:Panel>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFElencoGriglie" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightElencoGriglie" runat="server" Value="400,400" ClientIDMode="Static" />
    <asp:HiddenField ID="HFAltezzaFGriglie" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HFTAB" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HFAltezzaTab" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenSelTuttiScale" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenSelTuttiRotSacchi" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenSelTuttiResaSacchi" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenTabSelezionato" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenContratto" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenEsercizio" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenPrenotazione" runat="server" Value="" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="divImputazioneAscensore" runat="server" clientidmode="Static" style="display: none">
        <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" />
        <asp:Button ID="ButtonggiornaCanoni" runat="server" Text="Aggiorna canoni" ToolTip="Aggiorna i canoni delle voci dell' appalto" OnClientClick="chiudi('btnAggiorna');return false;" />
        <asp:Button ID="ButtonEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="GetRadWindow().close();return false;" />
    </div>
    <script type="text/javascript" language="javascript">
        function setIndicePulsanti(sender, args) {
            document.getElementById('hiddenTabSelezionato').value = args.get_tab().get_index();
            setVisibilitaPulsanti();

        };
        function setVisibilitaPulsanti() {
            switch (document.getElementById('hiddenTabSelezionato').value) {
                case '0':
                    document.getElementById('divImputazioneAscensore').style.display = "block";
                    break;
            };
        };
        function chiudi(pulsante) {
            GetRadWindow().BrowserWindow.document.getElementById(pulsante).click();
            GetRadWindow().close();
        };
    </script>
</asp:Content>
