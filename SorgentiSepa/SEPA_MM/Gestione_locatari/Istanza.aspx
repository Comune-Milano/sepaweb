<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master"
    AutoEventWireup="false" CodeFile="Istanza.aspx.vb" Inherits="Gestione_locatari_Istanza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClick="document.getElementById('frmModify').value='0';" />
                <asp:Button ID="btnEventi" runat="server" Text="Eventi" ToolTip="Eventi" OnClientClick=" CenterPage2('ElencoEventi.aspx?IDDOM=' + document.getElementById('lIdIstanza').value, 'Eventi', 1200, 800);" />
                <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="ConfermaEsciLock();return false;" />
            </td>
            <td style="width: 45px;">&nbsp
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <fieldset style="width: 97%;">
        <legend>&nbsp;&nbsp;&nbsp;<strong>Area Istanza</strong>&nbsp;&nbsp;&nbsp;</legend>
        <div style="overflow: hidden; height: 97%;">
            <div style="float: left; width: 67%;">
                <fieldset>
                    <legend>&nbsp;&nbsp;&nbsp;<strong>Dati Istanza</strong>&nbsp;&nbsp;&nbsp;</legend>
                    <div style="clear: both; float: left;">
                        <table>
                            <tr>
                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Num.Istanza:
                                </td>
                                <td style="width: 5px;">&nbsp;
                                </td>
                                <td style="width: 185px;" class="tdNoWrapWidthBlock">
                                    <asp:Label ID="lblIstanza" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: left;">
                        <table>
                            <tr>
                                <td style="width: 150px;" class="tdNoWrapWidthBlock">Data apertura:
                                </td>
                                <td style="width: 5px;">&nbsp;
                                </td>
                                <td style="width: 190px;">
                                    <telerik:RadTextBox ID="txtDataApertura" runat="server" Width="85px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 50px;" class="tdNoWrapWidthBlock">Stato:
                                </td>
                                <td style="width: 25px;">&nbsp;
                                </td>
                                <td style="width: 150px;">
                                    <telerik:RadComboBox ID="RadComboStato" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                                        Filter="None" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="clear: both; float: left;">
                        <table>
                            <tr>
                                <td style="width: 130px;" class="tdNoWrapWidthBlock">Data di presentazione:
                                </td>
                                <td style="width: 185px;">
                                    <telerik:RadDatePicker ID="RadDatePresentaz" runat="server" WrapperTableCaption=""
                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                        ShowPopupOnFocus="true">
                                        <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                        </DateInput>
                                        <Calendar ID="Calendar1" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today">
                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: left;">
                        <table>
                            <tr>
                                <td style="width: 150px;" class="tdNoWrapWidthBlock">Modalità di presentazione:
                                </td>
                                <td style="width: 5px;">&nbsp;
                                </td>
                                <td style="width: 185px;">
                                    <telerik:RadComboBox ID="RadComboModPres" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                                        Filter="None" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="clear: both; float: left;">
                        <table>
                            <tr>
                                <td style="width: 130px;" class="tdNoWrapWidthBlock">Data evento:
                                </td>
                                <td style="width: 185px;">
                                    <telerik:RadDatePicker ID="RadDateEvento" runat="server" WrapperTableCaption=""
                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                        ShowPopupOnFocus="true">
                                        <DateInput ID="DateInput7" runat="server" EmptyMessage="gg/mm/aaaa">
                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                        </DateInput>
                                        <Calendar ID="Calendar6" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today">
                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style="float: left;">
                        <table>
                            <tr>
                                <td style="width: 80px;" class="tdNoWrapWidthBlock">Provenienza:
                                </td>
                                <td class="tdNoWrapWidthBlock">
                                    <asp:Label ID="lblDichOrigine" runat="server"></asp:Label>
                                </td>
                                <td style="width: 5px;" class="tdNoWrapWidthBlock">
                                    <asp:Label ID="lblLinkOrigine" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="clear: both; float: left;">
                        <table>
                            <tr>

                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Giorni Preavviso Diniego:
                                </td>
                                <td style="width: 5px;">&nbsp;
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtGiorniPD" runat="server" Width="85px" MaxLength="50" ReadOnly="true" ToolTip="Data Preavv. – Data Presentaz.">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="tdNoWrapWidthBlock">Giorni Diniego:
                                </td>
                                <td style="width: 5px;">&nbsp;
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtGiorniD" runat="server" Width="85px" MaxLength="50" ReadOnly="true" ToolTip="Data Diniego – Data Controded. (se presente, altrimenti) Data Preavv.">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdNoWrapWidthBlock">Giorni Accoglimento:
                                </td>
                                <td style="width: 5px;">&nbsp;
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtGiorniAcc" runat="server" Width="85px" MaxLength="50"
                                        ReadOnly="true" ToolTip="Data Accoglimento – Data Preavv. (se presente, altrimenti) Data Presentaz.">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="tdNoWrapWidthBlock">Giorni Chiusura:
                                </td>
                                <td style="width: 5px;">&nbsp;
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtGiorniCh" runat="server" Width="85px" MaxLength="50" ReadOnly="true" ToolTip="Data Autorizzazione – Data Presentaz.">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="tdNoWrapWidthBlock">Giorni Archiviazione:
                                </td>
                                <td style="width: 5px;">&nbsp;
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtGiorniArch" runat="server" Width="85px" MaxLength="50"
                                        ReadOnly="true" ToolTip="Data Archiviazione – Data Autorizzazione">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                </fieldset>
            </div>
            <div style="float: right; width: 30%;">
                <asp:Panel ID="PanelDatiOspiti" runat="server" Style="display: block;">
                    <fieldset>
                        <legend>&nbsp;&nbsp;&nbsp;<strong>Dati Specifici Istanza</strong>&nbsp;&nbsp;&nbsp;</legend>
                        <div style="clear: both; float: left;">
                            <table>
                                <tr>
                                    <td style="width: 150px;" class="tdNoWrapWidthBlock">Tipologia:
                                    </td>
                                    <td style="width: 5px;">&nbsp;
                                    </td>
                                    <td style="width: 185px;">
                                        <telerik:RadComboBox ID="RadComboTipo" runat="server" Culture="it-IT"
                                            AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                            ResolvedRenderMode="Classic">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </asp:Panel>

            </div>

            <div style="float: right; width: 30%;">
                <asp:Panel ID="PanelDatiSubentro" runat="server" Style="display: none;">
                    <fieldset>
                        <legend>&nbsp;&nbsp;&nbsp;<strong>Dati Specifici Istanza</strong>&nbsp;&nbsp;&nbsp;</legend>
                        <div style="clear: both; float: left;">
                            <table>
                                <tr>
                                    <td style="width: 155px;" class="tdNoWrapWidthBlock">Motivazione:
                                    </td>

                                    <td style="width: 185px;">
                                        <telerik:RadComboBox ID="RadComboMotiviSub" runat="server" Culture="it-IT"
                                            Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                            ResolvedRenderMode="Classic">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px;" class="tdNoWrapWidthBlock">Data provvedimento CdM:
                                    </td>
                                    <td style="width: 185px;">
                                        <telerik:RadDatePicker ID="RadDateProvv" runat="server" WrapperTableCaption=""
                                            MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                            ShowPopupOnFocus="true">
                                            <DateInput ID="DateInput8" runat="server" EmptyMessage="gg/mm/aaaa">
                                                <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                            </DateInput>
                                            <Calendar ID="Calendar7" runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                        <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px;" class="tdNoWrapWidthBlock">Data ricorso al Tar:
                                    </td>
                                    <td style="width: 185px;">
                                        <telerik:RadDatePicker ID="RadDateTar" runat="server" WrapperTableCaption=""
                                            MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                            ShowPopupOnFocus="true">
                                            <DateInput ID="DateInput9" runat="server" EmptyMessage="gg/mm/aaaa">
                                                <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                            </DateInput>
                                            <Calendar ID="Calendar8" runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                        <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </asp:Panel>
            </div>
        </div>
        <fieldset style="width: 99%;">
            <legend>&nbsp;&nbsp;&nbsp;<strong>Dati Dichiarante</strong>&nbsp;&nbsp;&nbsp;</legend>
            <div style="clear: both; float: left;">
                <table>
                    <tr>
                        <td style="width: 100px;" class="tdNoWrapWidthBlock">Cognome:
                        </td>
                        <td style="width: 5px;">&nbsp;
                        </td>
                        <td style="width: 185px;">
                            <telerik:RadTextBox ID="txtCognome" runat="server" Width="180px" MaxLength="50" ReadOnly="True">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left;">
                <table>
                    <tr>
                        <td style="width: 100px;" class="tdNoWrapWidthBlock">Nome:
                        </td>
                        <td style="width: 185px;">
                            <telerik:RadTextBox ID="txtNome" runat="server" Width="180px" MaxLength="50" ReadOnly="True">
                            </telerik:RadTextBox>
                        </td>
                        <td style="width: 5px;">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left;">
                <table>
                    <tr>
                        <td style="width: 100px;" class="tdNoWrapWidthBlock">Codice Fiscale:
                        </td>
                        <td style="width: 185px;">
                            <telerik:RadTextBox ID="txtCodiceFiscale" runat="server" Width="180px" MaxLength="16" ReadOnly="True">
                            </telerik:RadTextBox>
                        </td>
                        <td style="display: none;">
                            <asp:ImageButton ID="btnCalcoloCodFiscale" runat="server" ImageUrl="../StandardTelerik/Immagini/CodiceFiscale/codice_fiscale.gif"
                                Width="24px" Height="24px" CausesValidation="False" OnClientClick="CalcoloCodFiscale(this);return false;" />
                        </td>
                        <td style="width: 5px;">&nbsp;
                        </td>
                        <td>&nbsp
                        </td>
                        <td style="width: 3px;">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div style="clear: both; float: left;">
                <table>
                    <tr>
                        <td style="width: 108px;" class="tdNoWrapWidthBlock">Sesso:
                        </td>
                        <td style="width: 80px;">
                            <telerik:RadComboBox ID="ddlSesso" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                                Filter="None" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="40px" Enabled="False">
                                <Items>
                                    <telerik:RadComboBoxItem Text="M" Value="M" />
                                    <telerik:RadComboBoxItem Text="F" Value="F" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 5px;">&nbsp;
                        </td>
                        <td>&nbsp
                        </td>
                        <td style="width: 85px;">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left;">
                <table>
                    <tr>
                        <td style="width: 100px;" class="tdNoWrapWidthBlock">Data di Nascita:
                        </td>
                        <td style="width: 70px;">
                            <telerik:RadDatePicker ID="txtDataNascita" runat="server" WrapperTableCaption=""
                                MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                ShowPopupOnFocus="true" ReadOnly="True" Enabled="False">
                                <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                    <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                    <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                </DateInput>
                                <Calendar ID="Calendar9" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 5px;">&nbsp;
                        </td>
                        <td>&nbsp
                        </td>
                        <td style="width: 3px;">&nbsp;
                        </td>
                        <td style="width: 5px;">&nbsp;
                        </td>
                        <td>&nbsp
                        </td>
                        <td style="width: 3px;">&nbsp;
                        </td>
                        <td style="width: 5px;">&nbsp;
                        </td>
                        <td>&nbsp
                        </td>
                        <td style="width: 3px;">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left;">
                <table>
                    <tr>
                        <td style="width: 10px;">&nbsp;
                        </td>
                        <td style="width: 102px;" class="tdNoWrapWidthBlock">Parentela:
                        </td>
                        <td style="width: 185px;">
                            <telerik:RadComboBox ID="ddlRuolo" runat="server" Culture="it-IT"
                                Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Enabled="False">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td style="width: 100px;" class="tdNoWrapWidthBlock">Nazione di Nascita:
                        </td>
                        <td style="width: 5px;">&nbsp;
                        </td>
                        <td style="width: 185px;">
                            <telerik:RadComboBox ID="ddlNazione" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                                Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="200px" Enabled="False">
                            </telerik:RadComboBox>
                        </td>
                        <td>Comune di Nascita:
                        </td>
                        <td style="width: 185px;">
                            <telerik:RadAutoCompleteBox runat="server" ID="acbComune" OnClientRequesting="requestingComune"
                                InputType="Text" Width="350px" ClientIDMode="Static" TextSettings-SelectionMode="Single"
                                OnClientTextChanged="OnClientEntryAddingHandlerComuneSimple" DropDownHeight="150" Enabled="False">
                                <WebServiceSettings Path="../SepacomAutoComplete.asmx" Method="GetListaComuniIndirizzo" />
                                <TextSettings SelectionMode="Single" />
                            </telerik:RadAutoCompleteBox>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </fieldset>
    <fieldset style="width: 97%;">
        <legend>&nbsp;&nbsp;&nbsp;<strong>Area Procedimento</strong>&nbsp;&nbsp;&nbsp;</legend>
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
            SelectedIndex="0" ShowBaseLine="true" ScrollChildren="true" OnClientTabSelecting="setResizeTabs"
            Width="100%">
            <Tabs>
                <telerik:RadTab runat="server" PageViewID="RadPageNucleo" Text="Nucleo" ToolTip="Nucleo"
                    Value="Nucleo">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageISEE" Text="ISEE" ToolTip="ISEE"
                    Value="ISEE">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageOspiti" Text="Ospiti" ToolTip="Ospiti"
                    Value="Ospiti">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageDatiRedd" Text="Redditi" ToolTip="Redditi"
                    Value="Redditi">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageRequisiti" Text="Requisiti" ToolTip="Requisiti"
                    Value="Requisiti">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageStampe" Text="Modulistica" ToolTip="Modulistica"
                    Value="Modulistica">
                </telerik:RadTab>
                <telerik:RadTab runat="server" PageViewID="RadPageDecisione" Text="Decisioni" ToolTip="Decisioni"
                    Value="Decisioni">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="RadPageNucleo" runat="server">
                <asp:Panel ID="PanelRadPageNucleo" runat="server" Style="width: 100%; height: 80%; overflow: hidden;">
                    <table>
                        <tr>
                            <td>
                                <strong>COMPONENTI DEL NUCLEO - <i>Richiedente, componenti e altri soggetti considerati
                                    a carico IRPEF</i></strong>
                            </td>
                        </tr>
                        <tr>
                            <td id="bottoniAmpliamento">
                                <asp:Button ID="btnInserisciNucleo" runat="server" Text="Inserisci" ToolTip="Inserisci"
                                    OnClientClick="Aggiungi(this, 'modalRadWindow', 1, 'ComponentiNucleo.aspx?O=0&IDD=' + document.getElementById('lIdDichiarazione').value + '', 1000, 500);return false;" />
                                <asp:Button ID="btnModificaNucleo" runat="server" Text="Modifica" ToolTip="Modifica"
                                    OnClientClick="Modifica(this, 'modalRadWindow', 1, 'ComponentiNucleo.aspx?O=1&C=' + document.getElementById('idSelectedNucleo').value + '&IDD=' + document.getElementById('lIdDichiarazione').value + '', 'idSelectedNucleo', 'MasterPage_RadNotificationMsg', 1000,700);return false;" />
                                <asp:Button ID="btnEliminaNucleo" runat="server" Text="Elimina" ToolTip="Elimina"
                                    OnClientClick="Modifica(this, 'modalRadWindow', 1, 'UscitaCompNucleo.aspx?C=' + document.getElementById('idSelectedNucleo').value + '&IDD=' + document.getElementById('lIdDichiarazione').value + '', 'idSelectedNucleo', 'MasterPage_RadNotificationMsg', 800, 350);return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="dgvNucleo" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
                                    EnableLinqExpressions="False" IsExporting="False" AllowPaging="false" PageSize="10">
                                    <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                        TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                        ClientDataKeyNames="ID" DataKeyNames="ID">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <CommandItemSettings ShowAddNewRecordButton="False" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PROGR" HeaderText="PROGR" Visible="false" Exportable="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataField="DATA_NASC" HeaderText="DATA NASCITA" PickerType="DatePicker"
                                                EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false"
                                                Visible="true" Exportable="true">
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="GRADO PARENTELA">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridNumericColumn DataField="PERC_INVAL" HeaderText="PERC. INVALIDITA">
                                            </telerik:GridNumericColumn>
                                            <telerik:GridBoundColumn DataField="USL" HeaderText="ASL">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TIPO_INVALIDITA" HeaderText="TIPO INVALIDITA">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NATURA_INVALIDITA" HeaderText="NATURA INVALIDITA">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IND_ACCOMP" HeaderText="IND. ACC.">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NUOVO_COMP" HeaderText="NUOVO COMP.">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataField="DATA_INGRESSO" HeaderText="DATA INGR." PickerType="DatePicker"
                                                EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false"
                                                Visible="true" Exportable="true">
                                            </telerik:GridDateTimeColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="90" AllowScroll="true" />
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                        <ClientEvents OnRowSelecting="RowSelectingNucleo" OnRowClick="RowSelectingNucleo"
                                            OnRowDblClick="ModificaDblClickNucleo" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="idSelectedNucleo" runat="server" Value="" ClientIDMode="Static" />
                </asp:Panel>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageISEE" runat="server">
                <asp:Panel ID="PanelRadPageISEE" runat="server" CssClass="panelGrigliaTab">
                    <div style="float: left; height: 140px;">
                        <table width="100%">

                            <tr>
                                <td style="width: 150px;" class="tdNoWrapWidthBlock">Numero Protocollo DSU:
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtProtocolloDSU" runat="server" Width="100px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px;" class="tdNoWrapWidthBlock">ISEE Ordinario:
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtISEE" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                                        MinValue="0" EnabledStyle-HorizontalAlign="Right">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px;" class="tdNoWrapWidthBlock">ISR:
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtISR" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                                        MinValue="0" EnabledStyle-HorizontalAlign="Right">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px;" class="tdNoWrapWidthBlock">ISP:
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtISP" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                                        MinValue="0" EnabledStyle-HorizontalAlign="Right">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px;" class="tdNoWrapWidthBlock">ISE:
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtISE" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                                        MinValue="0" EnabledStyle-HorizontalAlign="Right">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                </asp:Panel>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageOspiti" runat="server">
                <asp:Panel ID="PanelRadPageOspiti" runat="server" Style="width: 100%; height: 80%; overflow: hidden;">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnInserisciOspite" runat="server" Text="Inserisci" ToolTip="Inserisci"
                                    OnClientClick="Aggiungi(this, 'modalRadWindow', 1, 'InserimentoOspiti.aspx?IDM=' + document.getElementById('idMotivoIstanza').value + '&TOS=' + document.getElementById('idTipoProcesso').value  + '&IDC=' + document.getElementById('lIdContratto').value + '&O=0&IDD=' + document.getElementById('lIdDichiarazione').value + '', 1000, 500);return false;" />
                                <asp:Button ID="btnModificaOspite" runat="server" Text="Modifica" ToolTip="Modifica"
                                    OnClientClick="Modifica(this, 'modalRadWindow', 1, 'InserimentoOspiti.aspx?IDM=' + document.getElementById('idMotivoIstanza').value + '&TOS=' + document.getElementById('idTipoProcesso').value + '&IDC=' + document.getElementById('lIdContratto').value + '&O=1&C=' + document.getElementById('idSelectedOspite').value + '&IDD=' + document.getElementById('lIdDichiarazione').value + '', 'idSelectedOspite', 'MasterPage_RadNotificationMsg', 1000, 500);return false;" />
                                <asp:Button ID="btnElminaOspite" runat="server" Text="Elimina" ToolTip="Elimina"
                                    OnClientClick="Elimina(this, 'idSelectedOspite', 'MasterPage_RadNotificationMsg');return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="dgvOspiti" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
                                    EnableLinqExpressions="False" IsExporting="False" AllowPaging="false" PageSize="10">
                                    <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                        TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                        ClientDataKeyNames="ID" DataKeyNames="ID">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <CommandItemSettings ShowAddNewRecordButton="False" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataField="DATA_NASC" HeaderText="DATA NASCITA" PickerType="DatePicker"
                                                EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false"
                                                Visible="true" Exportable="true">
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridDateTimeColumn DataField="DATA_INIZIO_OSPITE" HeaderText="DATA INIZIO"
                                                PickerType="DatePicker" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}"
                                                ShowFilterIcon="false" Visible="true" Exportable="true">
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridDateTimeColumn DataField="DATA_FINE_OSPITE" HeaderText="DATA FINE" PickerType="DatePicker"
                                                EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false"
                                                Visible="true" Exportable="true">
                                            </telerik:GridDateTimeColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="90" AllowScroll="true" />
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                        <ClientEvents OnRowSelecting="RowSelectingOspite" OnRowClick="RowSelectingOspite"
                                            OnRowDblClick="ModificaDblClickOspite" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="idSelectedOspite" runat="server" Value="" ClientIDMode="Static" />
                </asp:Panel>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageDatiRedd" runat="server">
                <asp:Panel ID="PanelRedditi" runat="server">
                    <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage2"
                        SelectedIndex="0" ShowBaseLine="true" ScrollChildren="true" OnClientTabSelecting="setResizeTabs">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="RadPageView10" Text="Redditi" ToolTip="Redditi"
                                Value="Redditi">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView11" Text="Spese e Detrazioni"
                                ToolTip="Spese e Detrazioni" Value="Spese_Detrazioni">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView12" Text="Patrimonio Mobiliare e Immobiliare"
                                ToolTip="Patrimonio Mobiliare e Immobiliare" Value="Patr_Mob_Immob">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage2" runat="server" Width="100%" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <asp:Panel ID="PanelRadPageRedditi" runat="server" Style="width: 100%; height: 80%; overflow: hidden;">
                                <table>
                                    <tr>
                                        <td>
                                            <div style="clear: both; float: left;">
                                                <table>
                                                    <tr>
                                                        <td>Numero di figli fiscalmente a carico presenti nel nucleo familiare:
                                                        </td>
                                                        <td style="width: 5px;">&nbsp;
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtNumFigliCarico" runat="server" Width="100px" Value="0"
                                                                NumberFormat-DecimalDigits="0" Style="text-align: right;">
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnInserisciRedditi" runat="server" Text="Inserisci" ToolTip="Inserisci"
                                                OnClientClick="Aggiungi(this, 'modalRadWindow', 1, 'Redditi_Componenti.aspx?O=0&TD=' + document.getElementById('idMotivoIstanza').value + '&IDD=' + document.getElementById('lIdDichiarazione').value + '', 880, 780);return false;" />
                                            <asp:Button ID="btnModificaRedditi" runat="server" Text="Modifica" ToolTip="Modifica"
                                                OnClientClick="Modifica(this, 'modalRadWindow', 1, 'Redditi_Componenti.aspx?O=1&TD=' + document.getElementById('idMotivoIstanza').value + '&IDREDD=' + document.getElementById('idSelectedRedditi').value + '&IDD=' + document.getElementById('lIdDichiarazione').value + '', 'idSelectedRedditi', 'MasterPage_RadNotificationMsg', 880, 780);return false;" />
                                            <asp:Button ID="btnEliminaRedditi" runat="server" Text="Elimina" ToolTip="Elimina"
                                                OnClientClick="Elimina(this, 'idSelectedRedditi', 'MasterPage_RadNotificationMsg');return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="dgvRedditi" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
                                                EnableLinqExpressions="False" IsExporting="False" AllowPaging="false" PageSize="100">
                                                <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                                    TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                                    ClientDataKeyNames="IDCOMP, IDREDD" DataKeyNames="IDCOMP, IDREDD">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <CommandItemSettings ShowAddNewRecordButton="False" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="IDCOMP" HeaderText="ID" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="IDREDD" HeaderText="ID" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridNumericColumn DataField="DIPENDENTE" HeaderText="DIPENDENTE" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn DataField="PENSIONE" HeaderText="PENSIONE" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn DataField="PENSIONE2" HeaderText="PENSIONE ESENTE" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn DataField="AUTONOMO1" HeaderText="AUTONOMO" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn DataField="NO_ISEE" HeaderText="ASSEGN. MANT. FIGLI E ONERI"
                                                            ShowFilterIcon="false" Exportable="true" Visible="true" DataFormatString="{0:C2}"
                                                            DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn DataField="TOT_REDDITI" HeaderText="TOT. REDDITI" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="90" AllowScroll="true" />
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                                    <ClientEvents OnRowSelecting="RowSelectingRedditi" OnRowClick="RowSelectingRedditi"
                                                        OnRowDblClick="ModificaDblClickRedditi" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="idSelectedRedditi" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="idSelectedCompRedd" runat="server" Value="" ClientIDMode="Static" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <asp:Panel ID="PanelRadPageView3" runat="server" Style="width: 100%; height: 80%; overflow: hidden;">
                                <table>
                                    <tr>
                                        <td>
                                            <strong>SPESE DOCUMENTATE SUPERIORI A € 10.000,00 <i>(solo componenti con indennità
                                                accompagnamento)</i></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnModificaSpese" runat="server" Text="Modifica" ToolTip="Modifica"
                                                OnClientClick="Modifica(this, 'modalRadWindow', 1, 'SpeseComponenti.aspx?IDS=' + document.getElementById('idSelectedSpese').value  + '', 'idSelectedSpese', 'MasterPage_RadNotificationMsg', 500, 250);return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="dgvSpese" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
                                                EnableLinqExpressions="False" IsExporting="False" AllowPaging="false" PageSize="100">
                                                <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                                    TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                                    ClientDataKeyNames="ID" DataKeyNames="ID">
                                                    <CommandItemSettings ShowAddNewRecordButton="False" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridNumericColumn DataField="IMPORTO" HeaderText="IMPORTO" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:N2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="90" AllowScroll="true" />
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                                    <ClientEvents OnRowSelecting="RowSelectingSpese" OnRowClick="RowSelectingSpese" OnRowDblClick="ModificaDblClickSpese" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="idSelectedSpese" runat="server" Value="" ClientIDMode="Static" />
                            </asp:Panel>
                            <asp:Panel ID="PanelRadPageView5" runat="server" Style="width: 100%; height: 80%; overflow: hidden;">
                                <table>
                                    <tr>
                                        <td>
                                            <strong>DETRAZIONI</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnInserisciDetrazioni" runat="server" Text="Inserisci" ToolTip="Inserisci"
                                                OnClientClick="Aggiungi(this, 'modalRadWindow', 1, 'DetrazioniComponenti.aspx?O=0&IDD=' + document.getElementById('lIdDichiarazione').value + '', 500, 300);return false;" />
                                            <asp:Button ID="btnModificaDetrazioni" runat="server" Text="Modifica" ToolTip="Modifica"
                                                OnClientClick="Modifica(this, 'modalRadWindow', 1, 'DetrazioniComponenti.aspx?O=1&DET=' + document.getElementById('idSelectedDetrazioni').value + '&IDD=' + document.getElementById('lIdDichiarazione').value + '', 'idSelectedDetrazioni', 'MasterPage_RadNotificationMsg', 500, 300);return false;" />
                                            <asp:Button ID="btnEliminaDetrazioni" runat="server" Text="Elimina" ToolTip="Elimina"
                                                OnClientClick="Elimina(this, 'idSelectedDetrazioni', 'MasterPage_RadNotificationMsg');return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="dgvDetrazioni" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
                                                EnableLinqExpressions="False" IsExporting="False" AllowPaging="false" PageSize="100">
                                                <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                                    TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                                    ClientDataKeyNames="IDDETR" DataKeyNames="IDDETR">
                                                    <CommandItemSettings ShowAddNewRecordButton="False" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="IDCOMP" HeaderText="ID" Visible="false" Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="IDDETR" HeaderText="ID_COMPONENTE" Visible="false"
                                                            Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridNumericColumn DataField="IMPORTO" HeaderText="IMPORTO" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="TIPO DETRAZIONE" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridNumericColumn DataField="TOT_DETRAZ" HeaderText="TOT. DETRAZ." ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="90" AllowScroll="true" />
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                                    <ClientEvents OnRowSelecting="RowSelectingDetrazioni" OnRowClick="RowSelectingDetrazioni"
                                                        OnRowDblClick="ModificaDblClickDetrazioni" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="idSelectedDetrazioni" runat="server" Value="" ClientIDMode="Static" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server">
                            <asp:Panel ID="PanelRadPageView6" runat="server" Style="width: 100%; height: 80%; overflow: hidden;">
                                <table>
                                    <tr>
                                        <td>
                                            <strong>PATRIMONIO MOBILIARE</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnInserisciPatrimonioMobiliare" runat="server" Text="Inserisci"
                                                ToolTip="Inserisci" OnClientClick="Aggiungi(this, 'modalRadWindow', 1, 'PatrimonioMobComponenti.aspx?O=0&IDD=' + document.getElementById('lIdDichiarazione').value + '', 500, 300);return false;" />
                                            <asp:Button ID="btnModificaPatrimonioMobiliare" runat="server" Text="Modifica" ToolTip="Modifica"
                                                OnClientClick="Modifica(this, 'modalRadWindow', 1, 'PatrimonioMobComponenti.aspx?O=1&IDMOB=' + document.getElementById('idSelectedPatrMobiliare').value + '&IDD=' + document.getElementById('lIdDichiarazione').value + '','idSelectedPatrMobiliare', 'MasterPage_RadNotificationMsg', 500, 300);return false;" />
                                            <asp:Button ID="btnEliminaPatrimonioMobiliare" runat="server" Text="Elimina" ToolTip="Elimina"
                                                OnClientClick="Elimina(this, 'idSelectedPatrMobiliare', 'MasterPage_RadNotificationMsg');return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="dgvPatrMobiliare" runat="server" AutoGenerateColumns="False"
                                                AllowFilteringByColumn="false" EnableLinqExpressions="False" IsExporting="False"
                                                AllowPaging="false" PageSize="100">
                                                <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                                    TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                                    ClientDataKeyNames="IDMOB" DataKeyNames="IDMOB">
                                                    <CommandItemSettings ShowAddNewRecordButton="False" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="IDCOMP" HeaderText="ID" Visible="false" Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="IDMOB" HeaderText="ID" Visible="false" Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TIPO_MOB" HeaderText="TIPOLOGIA" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="COD_INTERMEDIARIO" HeaderText="CODICE" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="INTERMEDIARIO" HeaderText="INTERMEDIARIO" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridNumericColumn DataField="IMPORTO" HeaderText="IMPORTO" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:C2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="90" AllowScroll="true" />
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                                    <ClientEvents OnRowSelecting="RowSelectingPatrMobiliare" OnRowClick="RowSelectingPatrMobiliare"
                                                        OnRowDblClick="ModificaDblClickPatrMobiliare" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="idSelectedPatrMobiliare" runat="server" Value="" ClientIDMode="Static" />
                            </asp:Panel>
                            <asp:Panel ID="PanelRadPageView7" runat="server" Style="width: 100%; height: 80%; overflow: hidden;">
                                <table>
                                    <tr>
                                        <td>
                                            <strong>PATRIMONIO IMMOBILIARE</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnInserisciPatrimonioImmobiliare" runat="server" Text="Inserisci"
                                                ToolTip="Inserisci" OnClientClick="Aggiungi(this, 'modalRadWindow', 1, 'PatrimonioImmobComponenti.aspx?O=0&IDD=' + document.getElementById('lIdDichiarazione').value + '',1000,380);return false;" />
                                            <asp:Button ID="btnModificaPatrimonioImmobiliare" runat="server" Text="Modifica"
                                                ToolTip="Modifica" OnClientClick="Modifica(this, 'modalRadWindow', 1, 'PatrimonioImmobComponenti.aspx?O=1&IDIMMOB=' + document.getElementById('idSelectedPatrImmobiliare').value + '&IDD=' + document.getElementById('lIdDichiarazione').value + '', 'idSelectedPatrImmobiliare', 'MasterPage_RadNotificationMsg', 1000, 380);return false;" />
                                            <asp:Button ID="btnEliminaPatrimonioImmobiliare" runat="server" Text="Elimina" ToolTip="Elimina"
                                                OnClientClick="Elimina(this, 'idSelectedPatrImmobiliare', 'MasterPage_RadNotificationMsg');return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="dgvPatrImmobiliare" runat="server" AutoGenerateColumns="False"
                                                AllowFilteringByColumn="false" EnableLinqExpressions="False" IsExporting="False"
                                                AllowPaging="false" PageSize="100">
                                                <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                                    TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                                    ClientDataKeyNames="IDIMMOB" DataKeyNames="IDIMMOB">
                                                    <CommandItemSettings ShowAddNewRecordButton="False" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="IDCOMP" HeaderText="ID" Visible="false" Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="IDIMMOB" HeaderText="ID_COMPONENTE" Visible="false"
                                                            Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TIPO_IMMOB" HeaderText="TIPO IMMOBILE" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TIPO_PROPR" HeaderText="TIPO PROPRIETA'" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridNumericColumn DataField="VALORE" HeaderText="VALORE" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:N2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn DataField="MUTUO" HeaderText="MUTUO" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:N2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn DataField="SUP_UTILE" HeaderText="SUPERFICIE" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:N2}" DecimalDigits="2">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn DataField="PERC_PATR_IMMOBILIARE" HeaderText="% PROPRIETA'"
                                                            ShowFilterIcon="false" Exportable="true" Visible="true" DataFormatString="{0:N0}"
                                                            DecimalDigits="0">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn DataField="N_VANI" HeaderText="VANI" ShowFilterIcon="false"
                                                            Exportable="true" Visible="true" DataFormatString="{0:N0}" DecimalDigits="0">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridBoundColumn DataField="COMUNE" HeaderText="COMUNE" ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CAT_CATASTALE" HeaderText="CATEGORIA CATASTALE"
                                                            ShowFilterIcon="false">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="REND_CATAST_DOMINICALE" HeaderText="ID" Visible="false"
                                                            Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VALORE_MERCATO" HeaderText="ID" Visible="false"
                                                            Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="90" AllowScroll="true" />
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                                    <ClientEvents OnRowSelecting="RowSelectingPatrImmobiliare" OnRowClick="RowSelectingPatrImmobiliare"
                                                        OnRowDblClick="ModificaDblClickPatrImmobiliare" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="idSelectedPatrImmobiliare" runat="server" Value="" ClientIDMode="Static" />
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageRequisiti" runat="server">
                <asp:Panel ID="PanelRadPageRequisiti" runat="server" CssClass="panelGrigliaTab">
                    <div style="float: left;">
                        <table width="100%">
                            <tr>
                                <td>
                                    <strong>REQUISITI</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="chkRequisiti" runat="server" Width="700px" AutoPostBack="true">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: left;">
                        <table width="100%">
                            <tr>
                                <td>
                                    <strong>DOCUMENTI</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="chkDocumenti" runat="server">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageStampe" runat="server">
                <asp:Panel ID="PanelStampe" runat="server">
                    <telerik:RadTabStrip ID="RadTabStrip3" runat="server" MultiPageID="RadMultiPage3"
                        SelectedIndex="0" ShowBaseLine="true" ScrollChildren="true" OnClientTabSelecting="setResizeTabs">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="RadPageView10" Text="Genera File" ToolTip="Genera File"
                                Value="Stampe">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView11" Text="Elenco File" ToolTip="Elenco File"
                                Value="Elenco_file">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage3" runat="server" Width="100%" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView10" runat="server">
                            <asp:Panel ID="PanelStampe2" runat="server" Style="width: 100%; height: 80%; overflow: hidden;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnStampaDoc" runat="server" Text="Stampa" ToolTip="Stampa" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="dgvStampe" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
                                                EnableLinqExpressions="False" IsExporting="False" AllowPaging="false" PageSize="50">
                                                <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                                    TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                                    ClientDataKeyNames="ID" DataKeyNames="ID">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <CommandItemSettings ShowAddNewRecordButton="False" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="TIPO DOCUMENTO">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="120" AllowScroll="true" />
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                                    <ClientEvents OnRowSelecting="RowSelectingDoc" OnRowClick="RowSelectingDoc" OnRowDblClick="ModificaDblClickDoc" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="idSelectedDoc" runat="server" Value="" ClientIDMode="Static" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView11" runat="server">
                            <asp:Panel ID="Panel1" runat="server" Style="width: 100%; height: 80%; overflow: hidden;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnDownload" runat="server" Text="Download" ToolTip="Download" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="dgvElencoFile" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
                                                EnableLinqExpressions="False" IsExporting="False" AllowPaging="false" PageSize="50">
                                                <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
                                                    TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                                                    ClientDataKeyNames="ID" DataKeyNames="ID">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <CommandItemSettings ShowAddNewRecordButton="False" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="TIPO DOCUMENTO">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA ORA" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Scrolling UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="120" AllowScroll="true" />
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                                                    <ClientEvents OnRowSelecting="RowSelectingStampa" OnRowClick="RowSelectingStampa"
                                                        OnRowDblClick="ModificaDblClickStampa" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="idSelectedStampa" runat="server" Value="" ClientIDMode="Static" />
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageDecisione" runat="server">
                <asp:Panel ID="PanelRadPageDecisione" runat="server" CssClass="panelGrigliaTab">
                    <fieldset style="width: 97%;">
                        <legend>&nbsp;&nbsp;&nbsp;<strong>Iter Autorizzativo</strong>&nbsp;&nbsp;&nbsp;</legend>
                        <div style="overflow: hidden; height: 97%;">
                            <div style="float: left; width: 49%;">
                                <asp:Button ID="btnSottoponi" runat="server" Text="Sottoponi a Decisione" ToolTip="Sottoponi a Decisione"
                                    Enabled="false" />
                                <asp:Button ID="btnApprova" runat="server" Text="Approva" ToolTip="Approva" Enabled="false" />
                                <asp:Button ID="btnRespingi" runat="server" Text="Respingi" ToolTip="Respingi" Enabled="false" />
                            </div>
                            <div style="clear: both; float: left; width: 49%;">
                                <fieldset>
                                    <legend>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkPreavviso" runat="server" Text=""></asp:CheckBox><strong>Preavviso
                                        di diniego</strong>&nbsp;&nbsp;&nbsp;</legend>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 130px;" class="tdNoWrapWidthBlock">Data:
                                                </td>
                                                <td style="width: 185px;">
                                                    <telerik:RadDatePicker ID="RadDatePreavvDiniego" runat="server" WrapperTableCaption=""
                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                        ShowPopupOnFocus="true">
                                                        <DateInput ID="DateInput3" runat="server" EmptyMessage="gg/mm/aaaa">
                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                        </DateInput>
                                                        <Calendar ID="Calendar2" runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Motivo:
                                                </td>
                                                <td style="width: 5px;">&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbMotivoPD" runat="server" Culture="it-IT"
                                                        ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                        CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Font-Size="9pt" LoadingMessage="Caricamento..."
                                                        RenderMode="Lightweight" Width="362px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp
                                                </td>
                                                <td>&nbsp
                                                </td>
                                                <td style="display: none;">
                                                    <telerik:RadTextBox ID="RadTextBoxMotivoPD" runat="server" Width="365px" TextMode="MultiLine"
                                                        Height="30px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Nota integrativa:
                                                </td>
                                                <td style="width: 5px;">&nbsp;
                                                </td>
                                                <td style="width: 185px;">
                                                    <telerik:RadTextBox ID="RadTextBoxNotaPD" runat="server" Width="365px" TextMode="MultiLine"
                                                        Height="50px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: left; width: 22%;">
                                    </div>
                                </fieldset>
                            </div>
                            <div style="float: left; width: 48%;">
                                <fieldset>
                                    <legend>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkControdeduz" runat="server" Text=""></asp:CheckBox><strong>Controdeduzioni</strong>&nbsp;&nbsp;&nbsp;</legend>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 130px;" class="tdNoWrapWidthBlock">Data:
                                                </td>
                                                <td style="width: 185px;">
                                                    <telerik:RadDatePicker ID="RadDateControdeduzioni" runat="server" WrapperTableCaption=""
                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                        ShowPopupOnFocus="true">
                                                        <DateInput ID="DateInput4" runat="server" EmptyMessage="gg/mm/aaaa">
                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                        </DateInput>
                                                        <Calendar ID="Calendar3" runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%-- <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Motivo:
                                                </td>
                                                <td style="width: 5px;">&nbsp;
                                                </td>
                                                <td style="width: 500px;">
                                                    <telerik:RadTextBox ID="RadTextBoxMotivoC" runat="server" Width="365px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>--%>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Nota integrativa:
                                                </td>
                                                <td style="width: 5px;">&nbsp;
                                                </td>
                                                <td style="width: 185px;">
                                                    <telerik:RadTextBox ID="RadTextBoxNInt" runat="server" Width="365px" TextMode="MultiLine"
                                                        Height="54px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: left; width: 22%;">
                                    </div>
                                </fieldset>
                            </div>
                            <div style="clear: both; float: left; width: 49%;">
                                <fieldset>
                                    <legend>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkAccogli" runat="server" Text=""></asp:CheckBox><strong>Accoglimento</strong>&nbsp;&nbsp;&nbsp;</legend>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 130px;" class="tdNoWrapWidthBlock">Data:
                                                </td>
                                                <td style="width: 185px;">
                                                    <telerik:RadDatePicker ID="RadDateAccoglimento" runat="server" WrapperTableCaption=""
                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                        ShowPopupOnFocus="true">
                                                        <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa">
                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                        </DateInput>
                                                        <Calendar ID="Calendar4" runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Motivo:
                                                </td>
                                                <td style="width: 5px;">&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbMotivoAcc" runat="server" Culture="it-IT" ResolvedRenderMode="Classic"
                                                        HighlightTemplatedItems="true" Filter="Contains" CheckBoxes="true"
                                                        EnableCheckAllItemsCheckBox="true" Font-Size="9pt" LoadingMessage="Caricamento..."
                                                        RenderMode="Lightweight" Width="362px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp
                                                </td>
                                                <td>&nbsp
                                                </td>
                                                <td style="display: none;">
                                                    <telerik:RadTextBox ID="RadTextBoxMotivoAcc" runat="server" Width="365px" TextMode="MultiLine"
                                                        Height="30px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Nota integrativa:
                                                </td>
                                                <td style="width: 5px;">&nbsp;
                                                </td>
                                                <td style="width: 185px;">
                                                    <telerik:RadTextBox ID="RadTextBoxAccogliAcc" runat="server" Width="365px" TextMode="MultiLine"
                                                        Height="50px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: left; width: 22%;">
                                    </div>
                                </fieldset>
                            </div>
                            <div style="float: left; width: 48%;">
                                <fieldset>
                                    <legend>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkDiniego" runat="server" Text=""></asp:CheckBox><strong>Diniego</strong>&nbsp;&nbsp;&nbsp;</legend>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 130px;" class="tdNoWrapWidthBlock">Data:
                                                </td>
                                                <td style="width: 185px;">
                                                    <telerik:RadDatePicker ID="RadDateDiniego" runat="server" WrapperTableCaption=""
                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                        ShowPopupOnFocus="true">
                                                        <DateInput ID="DateInput6" runat="server" EmptyMessage="gg/mm/aaaa">
                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                        </DateInput>
                                                        <Calendar ID="Calendar5" runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>

                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Motivo:
                                                </td>
                                                <td style="width: 5px;">&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbMotivoDin" runat="server" Culture="it-IT"
                                                        ResolvedRenderMode="Classic" HighlightTemplatedItems="true" Filter="Contains"
                                                        CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Font-Size="9pt" LoadingMessage="Caricamento..."
                                                        RenderMode="Lightweight" Width="362px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp
                                                </td>
                                                <td>&nbsp
                                                </td>
                                                <td style="display: none;">
                                                    <telerik:RadTextBox ID="RadTextBoxMotDin" runat="server" Width="365px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="clear: both; float: left;">
                                        <table>
                                            <tr>
                                                <td style="width: 120px;" class="tdNoWrapWidthBlock">Nota integrativa:
                                                </td>
                                                <td style="width: 5px;">&nbsp;
                                                </td>
                                                <td style="width: 185px;">
                                                    <telerik:RadTextBox ID="RadTextBoxNotaDin" runat="server" Width="365px" TextMode="MultiLine"
                                                        Height="50px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: left; width: 22%;">
                                    </div>
                                </fieldset>
                            </div>
                            <div style="clear: both; float: left; width: 49%;">
                                <telerik:RadTextBox ID="txtDataArchiviazione" runat="server" Width="85px" ReadOnly="True">
                                </telerik:RadTextBox>
                                <asp:Button ID="btnArchivia" runat="server" Text="Archivia" ToolTip="Archivia" Enabled="false" />
                            </div>
                        </div>
                    </fieldset>
                </asp:Panel>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </fieldset>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFBlockExit" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="lIdDichiarazione" runat="server" ClientIDMode="Static" Value="-1" />
    <asp:HiddenField ID="lIdContratto" runat="server" ClientIDMode="Static" Value="-1" />
    <asp:HiddenField ID="lIdIstanza" runat="server" ClientIDMode="Static" Value="-1" />
    <asp:HiddenField ID="idUnita" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="idMotivoIstanza" runat="server" ClientIDMode="Static" Value="-1" />
    <asp:HiddenField ID="HFCodComune" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HFCodComuneSimple" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HFDisableResetComune" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="HFAutoCompletamentoAdvanced" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="idTipoProcesso" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="dataApertura" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="dataAutorizz" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="PageID" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenLockCorrenti" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSlLocked" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="primaApertura" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="forzaRequisito" runat="server" Value="0" ClientIDMode="Static" />
    <script type="text/javascript">
        $(function () {
            if (lockTimer == 0)
                lockTimer = setInterval(KeepSessionAliveMaster, 60000);
        });
        NascondiBottoni();
    </script>
</asp:Content>
