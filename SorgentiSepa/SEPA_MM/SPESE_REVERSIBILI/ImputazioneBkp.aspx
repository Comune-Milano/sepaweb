<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="ImputazioneBkp.aspx.vb" Inherits="SPESE_REVERSIBILI_ImputazioneBkp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvComplessi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvComplessi" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dgvAppalti">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvAppalti" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dgvEdifici">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvEdifici" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dgvTotale05">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvTotale05" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dgvTotale06">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvTotale06" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dgvTotale07">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvTotale07" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dgvTotale15">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvTotale15" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dgvTotale">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvTotale" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static" 
        Localization-Cancel="Annulla" />
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="Buttons"
        DecorationZoneID="decorationZone"></telerik:RadFormDecorator>
    <div id="decorationZone">
        <telerik:RadTabStrip runat="server" ID="RadTabStrip" Orientation="HorizontalTop" OnClientTabSelecting="setIndicePulsanti"
            Width="100%" MultiPageID="RadMultiPage1" ShowBaseLine="false">
            <Tabs>
                <telerik:RadTab runat="server" PageViewID="RadPageView1" Text="Imputazione Contratti" Value="ImpContratti"
                    Selected="true">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageView2" Text="Edifici" Value="Edifici">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageView3" Text="Scale" Value="Scale">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageView4" Text="05. Parti comuni" Value="Totale05">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageView5" Text="06. Rotazione sacchi" Value="Totale06">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageView6" Text="07. Lavaggio contenitori" Value="Totale07">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageView7" Text="15. Sanificazione" Value="Totale15">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageView8" Text="Totale" Value="Totale">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" CssClass="multiPage" Width="100%"
            BorderColor="DarkGray" BorderStyle="Solid" BorderWidth="1px"
            ScrollChildren="true">
            <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="panelTabsStrip" Selected="true">
                <div>
                    <table>
                        <tr>
                            <td style="height: 10px">&nbsp;
                            </td>
                        </tr>
                    </table>

                </div>
                <asp:Panel runat="server" ID="PanelAppalti">
                    <telerik:RadGrid ID="dgvAppalti" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            TableLayout="Auto" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID_GRUPPO" HeaderText="ID_GRUPPO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_GRUPPO_COMP" HeaderText="ID_GRUPPO_COMP" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_FORNITORE" HeaderText="ID_FORNITORE" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="05. PARTI COMUNI" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtMese05" runat="server" NumberFormat-DecimalDigits="2" TEXT='<%# DataBinder.Eval(Container, "DataItem.MESE05") %>'
                                            Width="80px" MinValue="0" Style="text-align: right">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="06. ROTAZIONE SACCHI" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtMese06" runat="server" NumberFormat-DecimalDigits="2" text='<%# DataBinder.Eval(Container, "DataItem.MESE06") %>'
                                            Width="80px" MinValue="0" Style="text-align: right">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="07. LAVAGGIO CONTENITORI" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtMese07" runat="server" NumberFormat-DecimalDigits="2" text='<%# DataBinder.Eval(Container, "DataItem.MESE07") %>'
                                            Width="80px" MinValue="0" Style="text-align: right">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="15. SANIFICAZIONE" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtMese15" runat="server" NumberFormat-DecimalDigits="2" text='<%# DataBinder.Eval(Container, "DataItem.MESE15") %>'
                                            Width="80px" MinValue="0" Style="text-align: right">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO" HeaderText="IMPORTO CANONI EMESSI" AutoPostBackOnFilter="true" Aggregate="Sum"
                                    ColumnGroupName="PrecAppalto" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SCONTO" HeaderText="SCONTO" AutoPostBackOnFilter="true"
                                    ColumnGroupName="PrecAppalto" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:P2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IVA" HeaderText="IVA" AutoPostBackOnFilter="true"
                                    ColumnGroupName="PrecAppalto" FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:P2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="SELEZIONA" AllowFiltering="false" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false" Checked='<%# DataBinder.Eval(Container,"DataItem.CHECKALL") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefreshAppalti" runat="server" OnClick="RefreshAppalti_Click"
                                                         OnClientClick="caricamento(2);"
                                                        CommandName="Refresh" CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcelAppalti"
                                                            OnClientClick="caricamento(2);" Text="text" runat="server" OnClick="EsportaAppalti_Click" CommandName="ExportToExcel"
                                                            CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
                <table>
                    <tr>
                        <td style="height: 10px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>

                    </tr>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView2" CssClass="panelTabsStrip">
                <div>
                    <table>
                        <tr>
                            <td style="height: 10px">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel runat="server" ID="PanelEdifici">
                    <telerik:RadGrid ID="dgvComplessi" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" FooterStyle-Wrap="false"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            DataKeyNames="ID" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="MQ_ESTERNI" HeaderText="MQ ESTERNI" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtMqEsterni" runat="server" NumberFormat-DecimalDigits="2"
                                            Width="80px" MinValue="0" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.MQ_ESTERNI") %>'>
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="MQ_PILOTY" HeaderText="MQ PILOTY" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtMqPiloty" runat="server" NumberFormat-DecimalDigits="2"
                                            DataType="System.Decimal" Width="80px" MinValue="0" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.MQ_PILOTY") %>'>
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="NUMERO_BIDONI_CARTA" HeaderText="NUMERO BIDONI CARTA"
                                    AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtNumBidoniCarta" runat="server" NumberFormat-DecimalDigits="0"
                                            Width="80px" MinValue="0" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.NUMERO_BIDONI_CARTA") %>'>
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="NUMERO_BIDONI_VETRO" HeaderText="NUMERO BIDONI VETRO"
                                    AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtNumBidoniVetro" runat="server" NumberFormat-DecimalDigits="0"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NUMERO_BIDONI_VETRO") %>' Width="80px"
                                            MinValue="0" Style="text-align: right">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="NUMERO_BIDONI_UMIDO" HeaderText="NUMERO BIDONI UMIDO"
                                    AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtNumBidoniUmido" runat="server" NumberFormat-DecimalDigits="0"
                                            Width="80px" MinValue="0" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.NUMERO_BIDONI_UMIDO") %>'>
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE"
                                    AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU2" HeaderText="PU2" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU3" HeaderText="PU3" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU9A" HeaderText="PU9A" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU9B" HeaderText="PU9B" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefresh" runat="server" OnClick="RefreshComplessi_Click" CommandName="Refresh" OnClientClick="caricamento(2);"
                                                        CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcel" Text="text" runat="server"
                                                            OnClientClick="caricamento(2);" OnClick="EsportaComplessi_Click" CommandName="ExportToExcel" CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" FrozenColumnsCount="1" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
                <table>
                    <tr>
                        <td style="height: 10px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>

                    </tr>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView3" CssClass="panelTabsStrip">
                <div>
                    <table>
                        <tr>
                            <td style="height: 10px">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel runat="server" ID="PanelScale">
                    <telerik:RadGrid ID="dgvEdifici" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" FooterStyle-Wrap="false"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            DataKeyNames="ID" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="PULIZIA_SCALE" HeaderText="PULIZIA SCALE">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkPuliziaScale" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.PULIZIA_SCALE") %>' />
                                    </ItemTemplate>
                                    <FilterTemplate>
                                        <table style="text-align: center">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="headerChkScale" runat="server" AutoPostBack="true" OnCheckedChanged="headerChkScale_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                    </FilterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ROTAZIONE_SACCHI" HeaderText="ROTAZIONE SACCHI">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRotSacchi" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.ROTAZIONE_SACCHI") %>' />
                                    </ItemTemplate>
                                    <FilterTemplate>
                                        <table style="text-align: center">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="headerChkRotSacchi" runat="server" AutoPostBack="true" OnCheckedChanged="headerChkRotSacchi_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                    </FilterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="RESA_SACCHI" HeaderText="RESA SACCHI">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkResaSacchi" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.RESA_SACCHI") %>' />
                                    </ItemTemplate>
                                    <FilterTemplate>
                                        <table style="text-align: center">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="headerChkResaSacchi" runat="server" AutoPostBack="true" OnCheckedChanged="headerChkResaSacchi_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                    </FilterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="SEDE_TERRITORIALE" HeaderText="ST"
                                    AutoPostBackOnFilter="true" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU1" HeaderText="PU1" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU7" HeaderText="PU7" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU8" HeaderText="PU8" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU10" HeaderText="PU10" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU12A" HeaderText="PU12A" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU12B" HeaderText="PU12B" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU12C" HeaderText="PU12C" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PU13" HeaderText="PU13" AutoPostBackOnFilter="true" DataFormatString="{0:C2}" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="EqualTo">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <PagerStyle AlwaysVisible="True" />
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefresh" runat="server" OnClick="RefreshEdifici_Click" CommandName="Refresh" OnClientClick="caricamento(2);"
                                                        CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcel" Text="text" runat="server"
                                                            OnClientClick="caricamento(2);" OnClick="EsportaEdifici_Click" CommandName="ExportToExcel" CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" FrozenColumnsCount="1" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="True" />
                    </telerik:RadGrid>
                    <telerik:RadNotification ID="RadNotification1" runat="server" Height="140px" Width="400"
                        Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3600"
                        Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
                    </telerik:RadNotification>
                </asp:Panel>
                <table>
                    <tr>
                        <td style="height: 10px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>

                    </tr>
                </table>
            </telerik:RadPageView>


            <telerik:RadPageView runat="server" ID="RadPageView4" CssClass="panelTabsStrip">
                <div>
                    <table>
                        <tr>
                            <td style="height: 10px">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel runat="server" ID="PanelTot05">
                    <telerik:RadGrid ID="dgvTotale05" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" FooterStyle-Wrap="false"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            TableLayout="Auto" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID_GRUPPO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="EDIFICIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_EDIFICIO" HeaderText="IMPORTO EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    ColumnGroupName="PrecAppalto" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_SCALA" HeaderText="IMPORTO SCALA" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOTALE_EDIFICIO" HeaderText="TOTALE EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO" HeaderText="TOTALE ANNUO SCONTATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO_RETT" HeaderText="TOTALE ANNUO SCONTATO RETTIFICATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefreshTotale" runat="server" OnClick="RefreshTot05_Click" OnClientClick="caricamento(2);"
                                                        CommandName="Refresh" CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcelTotale"
                                                            OnClientClick="caricamento(2);" Text="text" runat="server" OnClick="EsportaTot05_Click" CommandName="ExportToExcel"
                                                            CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
                <table>
                    <tr>
                        <td style="height: 10px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>

                    </tr>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView5" CssClass="panelTabsStrip">
                <div>
                    <table>
                        <tr>
                            <td style="height: 10px">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel runat="server" ID="PanelTot06">
                    <telerik:RadGrid ID="dgvTotale06" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" FooterStyle-Wrap="false"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            TableLayout="Auto" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID_GRUPPO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="EDIFICIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_EDIFICIO" HeaderText="IMPORTO EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    ColumnGroupName="PrecAppalto" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_SCALA" HeaderText="IMPORTO SCALA" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOTALE_EDIFICIO" HeaderText="TOTALE EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO" HeaderText="TOTALE ANNUO SCONTATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO_RETT" HeaderText="TOTALE ANNUO SCONTATO RETTIFICATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefreshTotale" runat="server" OnClick="RefreshTot06_Click" OnClientClick="caricamento(2);"
                                                        CommandName="Refresh" CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcelTotale"
                                                            OnClientClick="caricamento(2);" Text="text" runat="server" OnClick="EsportaTot06_Click" CommandName="ExportToExcel"
                                                            CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
                <table>
                    <tr>
                        <td style="height: 10px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>

                    </tr>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView6" CssClass="panelTabsStrip">
                <div>
                    <table>
                        <tr>
                            <td style="height: 10px">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel runat="server" ID="PanelTot07">
                    <telerik:RadGrid ID="dgvTotale07" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" FooterStyle-Wrap="false"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            TableLayout="Auto" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID_GRUPPO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="EDIFICIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_EDIFICIO" HeaderText="IMPORTO EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    ColumnGroupName="PrecAppalto" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_SCALA" HeaderText="IMPORTO SCALA" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOTALE_EDIFICIO" HeaderText="TOTALE EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO" HeaderText="TOTALE ANNUO SCONTATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO_RETT" HeaderText="TOTALE ANNUO SCONTATO RETTIFICATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefreshTotale" runat="server" OnClick="RefreshTot07_Click" OnClientClick="caricamento(2);"
                                                        CommandName="Refresh" CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcelTotale"
                                                            OnClientClick="caricamento(2);" Text="text" runat="server" OnClick="EsportaTot07_Click" CommandName="ExportToExcel"
                                                            CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
                <table>
                    <tr>
                        <td style="height: 10px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>

                    </tr>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView7" CssClass="panelTabsStrip">
                <div>
                    <table>
                        <tr>
                            <td style="height: 10px">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel runat="server" ID="PanelTot15">
                    <telerik:RadGrid ID="dgvTotale15" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" FooterStyle-Wrap="false"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            TableLayout="Auto" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID_GRUPPO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="EDIFICIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_EDIFICIO" HeaderText="IMPORTO EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    ColumnGroupName="PrecAppalto" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_SCALA" HeaderText="IMPORTO SCALA" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOTALE_EDIFICIO" HeaderText="TOTALE EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO" HeaderText="TOTALE ANNUO SCONTATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO_RETT" HeaderText="TOTALE ANNUO SCONTATO RETTIFICATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefreshTotale" runat="server" OnClick="RefreshTot15_Click" OnClientClick="caricamento(2);"
                                                        CommandName="Refresh" CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcelTotale"
                                                            OnClientClick="caricamento(2);" Text="text" runat="server" OnClick="EsportaTot15_Click" CommandName="ExportToExcel"
                                                            CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
                <table>
                    <tr>
                        <td style="height: 10px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>

                    </tr>
                </table>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView8" CssClass="panelTabsStrip">
                <div>
                    <table>
                        <tr>
                            <td style="height: 10px">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel runat="server" ID="PanelTotale">
                    <telerik:RadGrid ID="dgvTotale" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        ShowFooter="True" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" FooterStyle-Wrap="false"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="99%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            TableLayout="Auto" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID_GRUPPO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="EDIFICIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" AutoPostBackOnFilter="true"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_EDIFICIO" HeaderText="IMPORTO EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    ColumnGroupName="PrecAppalto" FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_SCALA" HeaderText="IMPORTO SCALA" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOTALE_EDIFICIO" HeaderText="TOTALE EDIFICIO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO" HeaderText="TOTALE ANNUO SCONTATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TOT_ANNUO_SCONTATO_RETT" HeaderText="TOTALE ANNUO SCONTATO RETTIFICATO" AutoPostBackOnFilter="true" Aggregate="Sum" ItemStyle-HorizontalAlign="Right"
                                    FilterControlWidth="84%" CurrentFilterFunction="Contains" DataFormatString="{0:C2}">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefreshTotale" runat="server" OnClick="RefreshTotale_Click" OnClientClick="caricamento(2);"
                                                        CommandName="Refresh" CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcelTotale"
                                                            OnClientClick="caricamento(2);" Text="text" runat="server" OnClick="EsportaTotale_Click" CommandName="ExportToExcel"
                                                            CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
                <table>
                    <tr>
                        <td style="height: 10px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>

                    </tr>
                </table>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Width="400"
            Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3600"
            Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
        </telerik:RadNotification>
    </div>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFElencoGriglie" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightElencoGriglie" runat="server" Value="350,350,350,350,350,350,350,350" ClientIDMode="Static" />
    <asp:HiddenField ID="HFAltezzaFGriglie" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HFTAB" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HFAltezzaTab" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenSelTuttiScale" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenSelTuttiRotSacchi" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenSelTuttiResaSacchi" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenTabSelezionato" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="divImputazioneContratti" runat="server" clientidmode="Static" style="display: none">
        <asp:Button ID="btnApplica" runat="server" Text="Applica" ToolTip="Applica" />
        <asp:Button ID="ButtonEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
    </div>
    <div id="divEdifici" runat="server" clientidmode="Static" style="display: none">
        <asp:Button ID="btnSalvaComplessi" runat="server" Text="Salva" ToolTip="Salva" />
        <asp:Button ID="Button1" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
    </div>
    <div id="divScale" runat="server" clientidmode="Static" style="display: none">
        <asp:Button ID="btnSalvaEdifici" runat="server" Text="Salva" ToolTip="Salva" />
        <asp:Button ID="Button2" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
    </div>
    <div id="divTotale" runat="server" clientidmode="Static" style="display: none">

        <asp:Button ID="btnSalvaTot" runat="server" Text="Salva" ToolTip="Salva" />
        <asp:Button ID="btnCalcolaCons" runat="server" Text="Calcola Consuntivo" ToolTip="Calcola Consuntivo" />
        <asp:Button ID="Button3" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
    </div>
     
    <script type="text/javascript" language="javascript">
        function setIndicePulsanti(sender, args) {
            document.getElementById('hiddenTabSelezionato').value = args.get_tab().get_index();
            setVisibilitaPulsanti();

        };
        function setVisibilitaPulsanti(sender, args) {
            switch (document.getElementById('hiddenTabSelezionato').value) {
                case '0':
                    document.getElementById('divImputazioneContratti').style.display = "block";
                    document.getElementById('divEdifici').style.display = "none";
                    document.getElementById('divScale').style.display = "none";
                    document.getElementById('divTotale').style.display = "none";
                    break;
                case '1':
                    document.getElementById('divImputazioneContratti').style.display = "none";
                    document.getElementById('divEdifici').style.display = "block";
                    document.getElementById('divScale').style.display = "none";
                    document.getElementById('divTotale').style.display = "none";
                    break;
                case '2':
                    document.getElementById('divImputazioneContratti').style.display = "none";
                    document.getElementById('divEdifici').style.display = "none";
                    document.getElementById('divScale').style.display = "block";
                    document.getElementById('divTotale').style.display = "none";
                    break;
                case '3':
                    document.getElementById('divImputazioneContratti').style.display = "none";
                    document.getElementById('divEdifici').style.display = "none";
                    document.getElementById('divScale').style.display = "none";
                    document.getElementById('divTotale').style.display = "block";
                    break;
                case '7':
                    document.getElementById('divImputazioneContratti').style.display = "none";
                    document.getElementById('divEdifici').style.display = "none";
                    document.getElementById('divScale').style.display = "none";
                    document.getElementById('divTotale').style.display = "block";
                    break;
            };
        };
    </script>
</asp:Content>
