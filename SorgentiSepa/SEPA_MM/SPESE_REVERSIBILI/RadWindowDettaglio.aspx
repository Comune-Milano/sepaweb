<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/PageModal.master"
    AutoEventWireup="false" CodeFile="RadWindowDettaglio.aspx.vb" Inherits="SPESE_REVERSIBILI_RadWindowDettaglio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ApriDettaglioSpese(sender, args) {
            var id = args.getDataKeyValue("ID");
            openModalInRad('MasterPage_ContentPlaceHolder1_RadWindowSpese', 'RadWindowSpese.aspx?id=' + id, 500, 500, null, null, null, 1);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridDettaglio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridDettaglio" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table cellpadding="2" cellspacing="2" width="98%" style="">
        <tr>
            <td colspan="6">
                <asp:Button ID="btnChiudiDettaglio" runat="server" Text="Esci" OnClientClick="GetRadWindow().close();return false;"
                    ToolTip="Esci" />
            </td>
        </tr>
        <tr>
            <td style="width: 13%">Codice unità immobiliare
            </td>
            <td style="width: 20%">
                <telerik:RadLabel ID="RadLabelCodiceUnitaImmobiliare" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
            <td style="width: 10%">Tipologia unità immobiliare
            </td>
            <td style="width: 10%">
                <telerik:RadLabel ID="RadLabelTipologia" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
            <td style="width: 10%">Edificio
            </td>
            <td style="width: 37%">
                <telerik:RadLabel ID="RadLabelEdificio" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
        </tr>
        <tr>
            <td>Scala
            </td>
            <td>
                <telerik:RadLabel ID="RadLabelScala" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
            <td>Piano
            </td>
            <td>
                <telerik:RadLabel ID="RadLabelPiano" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
            <td>Interno
            </td>
            <td>
                <telerik:RadLabel ID="RadLabelInterno" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
        </tr>
        <tr>
            <td>Codice contratto
            </td>
            <td>
                <telerik:RadLabel ID="RadLabelContratto" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
            <td>Giorni effettivi
            </td>
            <td>
                <telerik:RadLabel ID="RadLabelGiorni" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
            <td>Intestatario
            </td>
            <td>
                <telerik:RadLabel ID="RadLabelIntestatario" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
        </tr>
        <tr>
            <td>Totale consuntivo
            </td>
            <td>
                <telerik:RadLabel ID="RadLabelConsuntivo" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
            <td>Totale emesso
            </td>
            <td>
                <telerik:RadLabel ID="RadLabelEmesso" runat="server" CssClass="radLabelV">
                </telerik:RadLabel>
            </td>
            <td>Conguaglio
            </td>
            <td>
                <telerik:RadLabel ID="RadLabelConguaglio" runat="server" CssClass="radLabelConguaglioR">
                </telerik:RadLabel>
            </td>
        </tr>
        <tr>
            <td><telerik:RadLabel ID="lblTextTotConsuntivo" runat="server" Text="Totale consuntivo rettificato">
                </telerik:RadLabel>
            </td>
            <td colspan="3">
                <telerik:RadLabel ID="lblConsuntivoRett" runat="server" CssClass="radLabelConguaglioG">
                </telerik:RadLabel>
            </td>
         
            <td><telerik:RadLabel ID="lblTextTotConguaglio" runat="server"  Text="Conguaglio rettificato">
                </telerik:RadLabel>
            </td>
            <td>
                <telerik:RadLabel ID="lblConguaglioRett" runat="server" CssClass="radLabelConguaglioG">
                </telerik:RadLabel>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <telerik:RadGrid ID="RadGridDettaglio" runat="server" GroupPanelPosition="Top" AllowPaging="true"
                    PagerStyle-AlwaysVisible="true" PageSize="50" AutoGenerateColumns="False" Culture="it-IT"
                    AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                    IsExporting="False">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                        CommandItemDisplay="Top" ClientDataKeyNames="ID" DataKeyNames="ID">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                            ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SPESA" HeaderText="SPESA">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GRUPPO" HeaderText="GRUPPO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMPIANTO" HeaderText="IMPIANTO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO" HeaderText="IMPORTO" DataFormatString="{0:C2}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CENTRO_DI_COSTO" HeaderText="CENTRO DI COSTO" DataFormatString="{0:C2}">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <div style="display: inline-block; width: 100%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <div style="float: right; padding: 4px;">
                                                <asp:Button ID="ButtonRefreshDettagli" runat="server" OnClick="RefreshDettagli_Click" OnClientClick="caricamento(2);"
                                                    CommandName="Refresh" CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcelDettagli"
                                                        Text="text" runat="server" OnClick="EsportaDettagli_Click" CommandName="ExportToExcel" OnClientClick="caricamento(2);"
                                                        CssClass="rgExpXLS" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </CommandItemTemplate>
                        <SortExpressions>
                            <telerik:GridSortExpression FieldName="SPESA" SortOrder="Ascending" />
                        </SortExpressions>
                        <PagerStyle AlwaysVisible="True" />
                    </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <ExportSettings OpenInNewWindow="true" IgnorePaging="true" ExportOnlyData="true"
                        HideStructureColumns="true">
                        <Excel FileExtension="xlsx" Format="Xlsx" />
                    </ExportSettings>
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowDblClick="ApriDettaglioSpese" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="PanelDettaglio">
    <telerik:RadWindow ID="RadWindowSpese" runat="server" CenterIfModal="true" Modal="True"
            VisibleStatusbar="False" Behavior="Pin, Move, Resize, Maximize" Width="800px"
            Height="600px" ShowContentDuringLoad="false">
    </telerik:RadWindow>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="idUnita" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idContratto" ClientIDMode="Static" />
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="400" ClientIDMode="Static" />
</asp:Content>
