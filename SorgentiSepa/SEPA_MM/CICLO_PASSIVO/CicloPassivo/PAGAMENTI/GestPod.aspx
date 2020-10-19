<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestPod.aspx.vb" Inherits="CICLO_PASSIVO_GestPod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione POD</title>
    <script src="../../../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jsMessage.js" type="text/javascript"></script>
    <link href="../../../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/jscript">
        window.name = "modal";
        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        };
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
            return oWindow;
        };
        function CancelEdit() {
            GetRadWindow().close();
        };
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" target="modal">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons"
        ControlsToSkip="Zone" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Transparency="100">
    </telerik:RadAjaxLoadingPanel>
    <table style="width: 730px;" class="FontTelerik">
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lblTitolo" CssClass="testoGrassettoMaiuscoloBlu" runat="server">Gestione POD</asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Contratto &nbsp;&nbsp;&nbsp;
                <telerik:RadTextBox ID="txtContratto" runat="server" Width="150px" ReadOnly="True">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                POD
            </td>
            <td>
                Descrizione
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadTextBox ID="txtPOD" runat="server" Width="300px" ReadOnly="True">
                </telerik:RadTextBox>
            </td>
            <td>
                <telerik:RadTextBox ID="txtDescrizione" runat="server" Width="300px" ReadOnly="True">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="dgvPATRIMONIO" runat="server" AutoGenerateColumns="False" Culture="it-IT"
                                IsExporting="False" GroupPanelPosition="Top" Width="580px" Height="240px">
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                                <MasterTableView EnableHierarchyExpandAll="false" NoMasterRecordsText="Nessun elemento da visualizzare."
                                    HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false">
                                    <DetailTables>
                                        <telerik:GridTableView Name="Dettagli" Width="100%" AllowPaging="false" BackColor="Azure"
                                            HierarchyDefaultExpanded="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" FilterControlWidth="85%">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                        ShowRefreshButton="true" />
                                    <CommandItemTemplate>
                                        <div style="display: inline-block; width: 100%;">
                                            <div style="float: right; padding: 4px;">
                                                <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                                    CssClass="rgRefresh" />
                                                <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                                    CommandName="ExportToExcel" CssClass="rgExpXLS" />
                                            </div>
                                        </div>
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_POD" HeaderText="ID_POD" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_AGGREGAZIONE" HeaderText="ID_AGGREGAZIONE"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_COMPLESSO" HeaderText="ID_COMPLESSO" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_EDIFICIO" HeaderText="ID_EDIFICIO" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" DataFormatString="{0:@}" FilterControlWidth="85%">
                                            <HeaderStyle Width="15%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOME_AGGREGAZIONE" HeaderText="DENOMINAZIONE"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                                            FilterControlWidth="85%">
                                            <HeaderStyle Width="55%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO" HeaderStyle-Width="15%"
                                            DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="DATA_FINE" HeaderText="DATA FINE" HeaderStyle-Width="15%"
                                            DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                        </telerik:GridDateTimeColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                        <td style="vertical-align: top">
                            <table>
                                <tr>
                                    <td style="text-align: center">
                                        <telerik:RadButton ID="RadButtonAggiungiComplesso" runat="server" Text="Agg. Complesso"
                                            AutoPostBack="false" Width="104px" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowComplesso');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <telerik:RadButton ID="RadButtonAggiungiEdificio" runat="server" Text="Agg. Edificio"
                                            AutoPostBack="false" Width="104px" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowEdificio');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <telerik:RadButton ID="RadButtonDettaglioAggregazione" runat="server" Text="Agg. Aggreg."
                                            AutoPostBack="false" Width="104px" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowAggregazione');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <telerik:RadButton ID="RadButtonElimina" runat="server" Text="Elimina" Width="104px"
                                            OnClientClicking="function(sender, args){deleteElementTelerik(sender, args, 'idSelezionato');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadTextBox ID="RadTextBoxSelezione" runat="server" Width="90%" Style="border: none;">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">
                            <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="CloseAndRebind();return false;"
                                Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadWindow ID="RadWindowComplesso" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize" Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="Complesso">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            Complesso
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxComplesso" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="false" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data Inizio
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="RadDatePickerDataInizioComplesso" runat="server" Culture="it-IT"
                                Skin="Web20">
                                <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False"
                                    EnableWeekends="True" Culture="it-IT" FastNavigationNextText="&amp;lt;&amp;lt;"
                                    EnableKeyboardNavigation="True" Skin="Web20">
                                </Calendar>
                                <DateInput ID="DateInput1" DisplayDateFormat="dd/MM/yyyy" runat="server" DateFormat="dd/MM/yyyy"
                                    LabelWidth="40%">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                    <FocusedStyle Resize="None"></FocusedStyle>
                                    <DisabledStyle Resize="None"></DisabledStyle>
                                    <InvalidStyle Resize="None"></InvalidStyle>
                                    <HoveredStyle Resize="None"></HoveredStyle>
                                    <EnabledStyle Resize="None"></EnabledStyle>
                                    <ClientEvents OnFocus="CalendarDatePicker" />
                                </DateInput>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 100px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonSalvaComplesso" runat="server" Text="Salva">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciComplesso" runat="server" Text="Esci" AutoPostBack="false"
                                            OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowComplesso', '');}">
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
    <telerik:RadWindow ID="RadWindowEdificio" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize" Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="Edificio">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            Edificio
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxEdificio" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="false" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data Inizio
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="RadDatePickerDataInizioEdificio" runat="server" Culture="it-IT"
                                Skin="Web20">
                                <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False"
                                    EnableWeekends="True" Culture="it-IT" FastNavigationNextText="&amp;lt;&amp;lt;"
                                    EnableKeyboardNavigation="True" Skin="Web20">
                                </Calendar>
                                <DateInput ID="DateInput2" DisplayDateFormat="dd/MM/yyyy" runat="server" DateFormat="dd/MM/yyyy"
                                    LabelWidth="40%">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                    <FocusedStyle Resize="None"></FocusedStyle>
                                    <DisabledStyle Resize="None"></DisabledStyle>
                                    <InvalidStyle Resize="None"></InvalidStyle>
                                    <HoveredStyle Resize="None"></HoveredStyle>
                                    <EnabledStyle Resize="None"></EnabledStyle>
                                    <ClientEvents OnFocus="CalendarDatePicker" />
                                </DateInput>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 100px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonSalvaEdificio" runat="server" Text="Salva">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciEdificio" runat="server" Text="Esci" AutoPostBack="false"
                                            OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowEdificio', '');}">
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
    <telerik:RadWindow ID="RadWindowAggregazione" runat="server" CenterIfModal="true"
        Modal="True" VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize"
        Skin="Web20">
        <ContentTemplate>
            <asp:Panel runat="server" ID="Aggregazione">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            Nome aggregazione
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxAggregazione" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="false" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data Inizio
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="RadDatePickerDataInizioAggregazione" runat="server" Culture="it-IT"
                                Skin="Web20">
                                <Calendar ID="Calendar3" UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False"
                                    EnableWeekends="True" Culture="it-IT" FastNavigationNextText="&amp;lt;&amp;lt;"
                                    EnableKeyboardNavigation="True" Skin="Web20">
                                </Calendar>
                                <DateInput ID="DateInput3" DisplayDateFormat="dd/MM/yyyy" runat="server" DateFormat="dd/MM/yyyy"
                                    LabelWidth="40%">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                    <FocusedStyle Resize="None"></FocusedStyle>
                                    <DisabledStyle Resize="None"></DisabledStyle>
                                    <InvalidStyle Resize="None"></InvalidStyle>
                                    <HoveredStyle Resize="None"></HoveredStyle>
                                    <EnabledStyle Resize="None"></EnabledStyle>
                                    <ClientEvents OnFocus="CalendarDatePicker" />
                                </DateInput>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 100px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonSalvaAggregazione" runat="server" Text="Salva">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciAggregazione" runat="server" Text="Esci" AutoPostBack="false"
                                            OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowAggregazione', '');}">
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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Edificio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Edificio" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="dgvPATRIMONIO" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="hiddenpanel" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Complesso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Complesso" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="dgvPATRIMONIO" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="hiddenpanel" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Aggregazione">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Aggregazione" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="dgvPATRIMONIO" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="hiddenpanel" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <asp:Panel ID="hiddenpanel" runat="server">
        <asp:HiddenField ID="idPOD" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="idAggregazione" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="idSelezionato" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="tipo" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="dataFine" runat="server" Value="0" ClientIDMode="Static" />
    </asp:Panel>
    </form>
</body>
</html>
