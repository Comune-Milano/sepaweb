<%@ Page Title="Ricerca scritture gesionali" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="RicercaGestDaElaborare.aspx.vb" Inherits="Contratti_Spalmatore_RicercaGestDaElaborare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function RowSelecting(sender, args) {
            document.getElementById('idSelected').value = args.getDataKeyValue("ID");
        };
        function ModificaDblClick() {
            document.getElementById('CPMenu_btnVisualizza').click();
        };
        function VisualizzaElab(sender, args) {
            VisualizzaElaborazione();
        };
        function selezionaTutti(sender, args) {
            nascondi = 0;
            if (sender._checked)
                document.getElementById('hiddenSelTutti').value = "1";
            else
                document.getElementById('hiddenSelTutti').value = "0";
        };
         function apriMaschera() {
            location.replace('Procedure.aspx');
        }
    </script>
    <style type="text/css">
        .nascondi {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="">&nbsp;
            </td>
            <td style="width: 50%;" class="TitoloModulo">
                <asp:Label ID="lblTitolo" runat="server" Text="Ricerca"></asp:Label>
            </td>
            <td style="" align="right">&nbsp
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Button ID="btnAvviaRicerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca" />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:Button ID="btnIndietro" runat="server" Text="Indietro" ToolTip="Torna Indietro" />
            <asp:Button ID="btnNuovaRicerca" runat="server" Text="Nuova Ricerca" ToolTip="Nuova Ricerca" />
            <asp:Button ID="btnProcedi" runat="server" Text="Procedi" ToolTip="Procedi con elaborazione" />
        </asp:View>
    </asp:MultiView>
    <asp:Button ID="btnEsci" runat="server" Text="Esci" CausesValidation="false" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvElaborazioni">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvElaborazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
        <asp:View ID="View3" runat="server">
            <fieldset>
                <legend>&nbsp;&nbsp;&nbsp;<strong>Info Contrattuali</strong>&nbsp;&nbsp;&nbsp;</legend>
                <div style="width: 100%; height: 100%; overflow: auto;">
                    <div style="float: left; width: 100%;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="Tipologia rapporto" runat="server" ID="CheckBoxTipologiaRapporto"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                        Width="100%" AutoPostBack="True" />
                                </td>
                                <td style="vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="Tipologia specifica RU" runat="server" ID="CheckBoxContrattoSpecifico"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                        Width="100%" AutoPostBack="True" />
                                </td>
                                <td style="vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="Tipologia UI" runat="server" ID="CheckBoxTipologiaUI"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                                <td style="vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="Stato contratto" runat="server" ID="CheckBoxStatoContratto"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                                <td style="vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="Eccezioni" runat="server" ID="CheckBoxEccezioni"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelTipologiaRapporti" runat="server" Style="border: 1px solid #6B8DC2; height: 160px; overflow: auto">
                                        <asp:CheckBoxList ID="CheckBoxListTipologiaRapporti" runat="server" Font-Names="Arial"
                                            Font-Size="7pt" Width="90%" AutoPostBack="True">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelContrattoSpecifico" runat="server" Style="border: 1px solid #6B8DC2; height: 160px; overflow: auto">
                                        <asp:CheckBoxList ID="CheckBoxListContrattoSpecifico" runat="server" Font-Names="Arial"
                                            Font-Size="7pt" Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelTipologiaUI" runat="server" Style="border: 1px solid #6B8DC2; height: 160px; overflow: auto">
                                        <asp:CheckBoxList ID="CheckBoxListTipologiaUI" runat="server" Font-Names="Arial"
                                            Font-Size="7pt" Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelStatoContratto" runat="server" Style="border: 1px solid #6B8DC2; height: 160px; overflow: auto">
                                        <asp:CheckBoxList ID="CheckBoxListStatoContratto" runat="server" Font-Names="Arial"
                                            Font-Size="7pt" Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelEccezioni" runat="server" Style="border: 1px solid #6B8DC2; height: 160px; overflow: auto">
                                        <asp:CheckBoxList ID="CheckBoxListEccez" runat="server" Font-Names="Arial"
                                            Font-Size="7pt" Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>

                            </tr>
                        </table>
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <legend>&nbsp;&nbsp;&nbsp;<strong>Info Contabili</strong>&nbsp;&nbsp;&nbsp;</legend>
                <div style="width: 100%; height: 100%; overflow: auto;">
                    <div style="float: left; width: 970px;">
                        <table class="classedivbody">
                            <tr>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td>Tipo importo
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbTipoImporto" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                    OnClientLoad="OnClientLoadHandler">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="Credito" Value="1" Owner="cmbTipoImporto" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Debito" Value="0" Owner="cmbTipoImporto"></telerik:RadComboBoxItem>
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Piano di rateizzazione

                                            </td>

                                            <td>

                                                <telerik:RadComboBox ID="cmbPianoRateizzo" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                    OnClientLoad="OnClientLoadHandler">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="- - -" Value="-1" Owner="cmbPianoRateizzo" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Sì" Value="1" Owner="cmbPianoRateizzo" />
                                                        <telerik:RadComboBoxItem runat="server" Text="No" Value="0" Owner="cmbPianoRateizzo"></telerik:RadComboBoxItem>
                                                    </Items>
                                                </telerik:RadComboBox>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Semaforo</td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbSemaforo" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                    OnClientLoad="OnClientLoadHandler">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="- - -" Value="-1" Owner="cmbSemaforo" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Verde" Value="1" Owner="cmbSemaforo" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Rosso" Value="0" Owner="cmbSemaforo"></telerik:RadComboBoxItem>
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Data scadenza boll. compensabili</td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtDataScad" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataScad"
                                                    Display="Static" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>


                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox Text="Tipologia scritture gestionali" runat="server" ID="CheckBoxTipoDoc"
                                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                    Width="100%" AutoPostBack="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server" Style="border: 1px solid #6B8DC2; height: 160px; width: 500px; overflow: auto">
                                                    <asp:CheckBoxList ID="CheckBoxListTipoDoc" runat="server" Font-Names="Arial"
                                                        Font-Size="7pt" Width="90%">
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </fieldset>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <telerik:RadGrid ID="dgvElaborazioni" runat="server" GroupPanelPosition="Top" resolvedrendermode="Classic"
                AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                AllowFilteringByColumn="False" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
                AllowPaging="True" isexporting="True" PageSize="100">
                <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="Nessun dato da visualizzare."
            ShowHeadersWhenNoRecords="true" TableLayout="Auto" CommandItemSettings-ShowAddNewRecordButton="false">
            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                ShowRefreshButton="false" />
            <CommandItemTemplate>
                <div style="display: inline-block; width: 100%;">
                    <div style="float: right; padding: 4px;">

                        <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                            CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                    </div>
                </div>
            </CommandItemTemplate>
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID_BOLL" Visible="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="" UniqueName="ODL" FilterControlWidth="85%"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="8%" />
                            <ItemTemplate>

                                <telerik:RadButton ID="CheckBox1" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    OnCheckedChanged="CheckBox1_CheckedChanged" Checked='<%# DataBinder.Eval(Container, "DataItem.CHECKALL") %>' />

                            </ItemTemplate>
                            <HeaderTemplate>
                                <div style="width: 100%; text-align: center;">
                                    <telerik:RadButton ID="chkSelTutti" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                        AutoPostBack="true" OnClientCheckedChanged="selezionaTutti" OnClick="chkSelTutti_CheckedChanged" />
                                </div>
                            </HeaderTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPO_CONT" HeaderText="TIPO CONTRATTO">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPO_SPEC" HeaderText="TIPO SPECIFICO">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPO_UI" HeaderText="TIPOLOGIA UNITA'">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPO_DOC" HeaderText="TIPO DOC. GEST.">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IMP_EMESSO" HeaderText="IMP. EMESSO €">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="DATA_EMISS" HeaderText="DATA EMISSIONE"
                            FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridDateTimeColumn DataField="DATA_RIFERIM1" HeaderText="DATA RIFERIM. DAL"
                            FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridDateTimeColumn DataField="DATA_RIFERIM2" HeaderText="DATA RIFERIM. AL"
                            FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="CREDITO" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NOTE" HeaderText="NOTE">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="True" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ID" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="importoTOT" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPO_APPL" Visible="False"></telerik:GridBoundColumn>
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
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblAsterisco" runat="server" CssClass="TitoloH1" />
    <asp:HiddenField ID="HiddenCheck" runat="server" Value="1" />
    <asp:HiddenField ID="HFMultiView1" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="HFMultiView2" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="180" ClientIDMode="Static" />
    <asp:HiddenField ID="HFbtnClickGo" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="idSelected" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFCodComune" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HFCodIndirizzo" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HFCodCivico" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HFCodCap" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenSelTutti" runat="server" ClientIDMode="Static" />

</asp:Content>
