<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="NuovoIntervento.aspx.vb" Inherits="SICUREZZA_NuovoIntervento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function apriProcedimento() {
            validNavigation = true;
            window.open('NuovoProcedimento.aspx?NM=1&TIPO=' + document.getElementById('tipoProc').value + '&IDP=' + document.getElementById('HiddenFieldProc').value, '');
        };
        function apriOperatore() {
            if (document.getElementById('gruppo').value != '0') {
                var radwindow = $find('MasterPage_CPFooter_RadWindowOperatori');
                radwindow.show();
            }
        };
        function prova() {
            document.getElementById('HFBeforeLoading').value = '1';
        };
        function RowDblClick(sender, eventArgs) {
            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
        }
        ;
        //        function requestStart(sender, args) {
        //            if (args.get_eventTarget().indexOf("InitInsert") >= 0) {
        //                args.set_enableAjax(false);
        //            }
        //        };
        function ConfermaInterServ(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    this.click();
                }
            });
            apriConfirm("Attenzione...procedendo sarà aperto un intervento di servizio. Confermare?", callBackFunction, 300, 150, "Info", null);
            args.set_cancel(true);
        }

        
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:MultiView ID="MultiViewTitoli" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewPrincipale" runat="server">
            <asp:Label ID="lblTitolo2" runat="server" />
            <asp:Label ID="lblTitolo" Text="Intervento" runat="server" />
        </asp:View>
        <asp:View ID="ViewOperatori" runat="server">
            <asp:Label ID="Label7" Text="Intervento - Scelta operatore" runat="server" />
        </asp:View>
        <asp:View ID="ViewProcedimenti" runat="server">
            <asp:Label ID="Label1" Text="Elenco Procedimenti" runat="server" />
        </asp:View>
        <asp:View ID="ViewAllegati" runat="server">
            <asp:Label ID="Label2" Text="Allegati" runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:MultiView ID="MultiViewPulsanti" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewPulsantiHome" runat="server">
            <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva l'intervento" />
            <asp:Button ID="imgChiudiSegnalazione" runat="server" Text="Chiudi Intervento" ToolTip="Chiudi Intervento"
                CausesValidation="False" />
            <telerik:RadButton ID="btnCreaIntervServ" runat="server" Text="Crea Interv. Servizio"
                ToolTip="Crea Interv.servizio" OnClientClicking="function(sender, args){ConfermaInterServ(sender, args);}">
            </telerik:RadButton>
            <telerik:RadButton ID="btnApriProc" runat="server" Text="Crea Procedimento" AutoPostBack="false"
                OnClientClicking="function(sender, args){openWindow(sender, args, 'MasterPage_CPFooter_RadWindowAggiungi');}">
            </telerik:RadButton>
            <asp:Button ID="btnElencoProc" runat="server" Text="Elenco procedimenti" CausesValidation="False"
                ToolTip="Elenco procedimenti" ClientIDMode="Static" />
            <asp:Button ID="imgAllega" runat="server" Text="Elenco file" CausesValidation="False"
                ToolTip="Allega file" ClientIDMode="Static" />
            <asp:Button ID="btnStampa" runat="server" Text="Stampa" ToolTip="Stampa" ClientIDMode="Static" />
            <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="confermaEsci(1,document.getElementById('txtModificato').value);return false;" />
        </asp:View>
        <asp:View ID="ViewOp" runat="server">
            <asp:Button ID="btnConfermaOp" runat="server" Text="Conferma" ToolTip="Conferma l'alloggio selezionato"
                ClientIDMode="Static" />
            <asp:Button ID="btnIndietroOp" runat="server" Text="Indietro" ToolTip="Torna alla pagina precedente"
                CausesValidation="False" />
        </asp:View>
        <asp:View ID="ViewProc" runat="server">
            <asp:Button ID="btnVisualizzaProc" runat="server" Text="Visualizza" ToolTip="Visualizza" />
            <asp:Button ID="btnIndietro" runat="server" Text="Indietro" ToolTip="Indietro" />
        </asp:View>
        <asp:View ID="View10" runat="server">
            <asp:Button ID="btnIndietroAll" runat="server" Text="Indietro" ToolTip="Indietro"
                CausesValidation="False" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiViewCorpo" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewCorpo" runat="server">
            <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="Panel1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                            <telerik:AjaxUpdatedControl ControlID="PanelElencoInterventi" LoadingPanelID="RadAjaxLoadingPanel2" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="Panel2">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="Panel2" LoadingPanelID="RadAjaxLoadingPanel1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManagerProxy>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="5">
            </telerik:RadAjaxLoadingPanel>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Transparency="100">
            </telerik:RadAjaxLoadingPanel>
            <telerik:RadWindowManager ID="RadWindowManager01" runat="server" Localization-OK="Ok"
                Localization-Cancel="Annulla">
            </telerik:RadWindowManager>
            <table width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td style="width: 50%; vertical-align: top">
                                    <fieldset>
                                        <legend>Informazioni Intervento</legend>
                                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                            <tr>
                                                <%--<td>
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label36" runat="server">Data</asp:Label>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <telerik:RadDatePicker ID="txtDataCInt" runat="server" Culture="it-IT" Skin="Web20">
                                                                    <Calendar ID="Calendar8" UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False"
                                                                        EnableWeekends="True" Culture="it-IT" FastNavigationNextText="&amp;lt;&amp;lt;"
                                                                        EnableKeyboardNavigation="True" Skin="Web20">
                                                                    </Calendar>
                                                                    <DateInput ID="DateInput8" DisplayDateFormat="dd/MM/yyyy" runat="server" DateFormat="dd/MM/yyyy"
                                                                        LabelWidth="25%">
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
                                                            <td>
                                                                <asp:Label ID="Label37" runat="server">Ora</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOraCInt" runat="server" MaxLength="5" Width="40px" ToolTip="Ora segnalazione in formato HH:MM"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtOraCInt"
                                                                    ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ToolTip="Inserire orario formato HH:MM"
                                                                    ValidationExpression="([01]?[0-9]|2[0-3])(.|:)[0-5][0-9]"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <td style="width: 25%">
                                                    Data inserimento
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:TextBox ID="txtDataCInt" runat="server" Width="130px" ToolTip="Data intervento"
                                                        ReadOnly="True"></asp:TextBox>
                                                    <asp:Label ID="Label37" runat="server" Style="vertical-align: middle;">Ora</asp:Label>
                                                    <asp:TextBox ID="txtOraCInt" runat="server" MaxLength="5" Width="40px" ToolTip="Ora intervento in formato HH:MM"
                                                        ReadOnly="True"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtOraCInt"
                                                        ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ToolTip="Inserire orario formato HH:MM"
                                                        ValidationExpression="([01]?[0-9]|2[0-3])(.|:)[0-5][0-9]"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    Stato
                                                </td>
                                                <td style="width: 75%">
                                                    <telerik:RadComboBox ID="cmbStatoInterv" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                        OnClientLoad="OnClientLoadHandler">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Tipo
                                                </td>
                                                <td style="width: 75%">
                                                    <telerik:RadComboBox ID="cmbTipo" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                        OnClientLoad="OnClientLoadHandler" AutoPostBack="True">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    Assegnatario
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:TextBox ID="txtAssegnatario" runat="server" MaxLength="500" Width="220px" CssClass="CssMaiuscolo"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <asp:ImageButton ID="btnCercaOp2" runat="server" ImageUrl="../NuoveImm/user-icon.png"
                                                        OnClientClick="if (document.getElementById('txtAssegnatario').value == '') {apriAlert('Campo obbligatorio!', 300, 150, 'Attenzione', null, null); return false;}"
                                                        Style="width: 16px; height: 16px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    Co-assegnatario
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:TextBox ID="txtAssegnatario2" runat="server" MaxLength="500" Width="220px" CssClass="CssMaiuscolo"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <asp:ImageButton ID="btnCercaOp2B" runat="server" ImageUrl="../NuoveImm/user-icon.png"
                                                        OnClientClick="if (document.getElementById('txtAssegnatario2').value == '') {apriAlert('Campo obbligatorio!', 300, 150, 'Attenzione', null, null); return false;}"
                                                        Style="width: 16px; height: 16px;" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel runat="server" ID="Panel2">
                                            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        Codice unità immobiliare
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:TextBox ID="txtCodUI" runat="server" MaxLength="19" Width="240px" AutoPostBack="True"
                                                            CssClass="CssMaiuscolo" Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Indirizzo
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbIndirizzo" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                            Filter="Contains" AutoPostBack="True" Width="250px">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Edificio
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbEdificio" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                            Filter="Contains" AutoPostBack="True" Width="250px">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Scala
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbScala" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                            Filter="Contains" AutoPostBack="True" Width="250px">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Piano
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="DropDownListPiano" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            OnClientLoad="OnClientLoadHandler">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Interno
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="DropDownListInterno" runat="server" EnableLoadOnDemand="true"
                                                            IsCaseSensitive="false" Filter="Contains" AutoPostBack="True" Width="250px">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                            <tr>
                                                <td>
                                                    Data intervento
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtDataApertura" runat="server" WrapperTableCaption=""
                                                        MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                                        <DateInput ID="DateInput9" runat="server">
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
                                                    <asp:Label ID="Label3" runat="server" Style="vertical-align: middle;">Ora inizio</asp:Label>
                                                    <asp:TextBox ID="txtOraInterv" runat="server" MaxLength="5" Width="40px" ToolTip="Ora intervento in formato HH:MM"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtOraInterv"
                                                        ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ToolTip="Inserire orario formato HH:MM"
                                                        ValidationExpression="([01]?[0-9]|2[0-3])(.|:)[0-5][0-9]"></asp:RegularExpressionValidator>
                                                    <asp:Label ID="Label4" runat="server" Style="vertical-align: middle;">Ora fine</asp:Label>
                                                    <asp:TextBox ID="txtOraFineInterv" runat="server" MaxLength="5" Width="40px" ToolTip="Ora intervento in formato HH:MM"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOraFineInterv"
                                                        ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ToolTip="Inserire orario formato HH:MM"
                                                        ValidationExpression="([01]?[0-9]|2[0-3])(.|:)[0-5][0-9]"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    Data pre-assegnato
                                                </td>
                                                <td style="width: 75%">
                                                    <telerik:RadDatePicker ID="txtDataPreAss" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                        Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                                        <DatePopupButton ToolTip="" />
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
                                                    <%--<telerik:RadDatePicker ID="txtDataPresuntaUltimazione" runat="server" WrapperTableCaption=""
                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px"  
                                                        DataFormatString="{0:dd/MM/yyyy}" Font-Size="9pt" ShowPopupOnFocus="true">
                                                        <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                        </DateInput>
                                                    </telerik:RadDatePicker>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    Data di programmazione
                                                </td>
                                                <td style="width: 75%">
                                                    <telerik:RadDatePicker ID="txtDataProgrammaz" runat="server" WrapperTableCaption=""
                                                        MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                                        <DatePopupButton ToolTip="" />
                                                        <DateInput ID="DateInput8" runat="server">
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
                                            </tr>
                                            <tr>
                                                <td>
                                                    Data di presa in carico
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtDataPresaInCarico" runat="server" WrapperTableCaption=""
                                                        MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                                        <DateInput ID="DateInput2" runat="server">
                                                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                        </DateInput>
                                                        <Calendar ID="Calendar5" runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Data di chiusura
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtDataChiusura" runat="server" WrapperTableCaption=""
                                                        MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                                        <DateInput ID="DateInput3" runat="server">
                                                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                        </DateInput>
                                                        <Calendar ID="Calendar6" runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="richiedente">
                                                        Richiedente
                                                    </div>
                                                </td>
                                                <td>
                                                    <div id="tdcmbrichiedente">
                                                        <telerik:RadComboBox ID="cmbRichiedente" runat="server" Width="250px" EnableLoadOnDemand="true">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="verifica">
                                                        Modalità di verifica
                                                    </div>
                                                </td>
                                                <td>
                                                    <div id="tdcmbverifica">
                                                        <telerik:RadComboBox ID="cmbVerifica" runat="server" Width="250px" EnableLoadOnDemand="true">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    <div id="segnalante">
                                                        Segnalante
                                                    </div>
                                                </td>
                                                <td style="width: 75%">
                                                    <div id="tdcmbSoggCoinvolti">
                                                        <telerik:RadComboBox ID="cmbSoggCoinvolti" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            OnClientLoad="OnClientLoadHandler">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <div id="tipiInterv">
                                        <fieldset>
                                            <legend>Interventi Esterni</legend>
                                            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                <tr>
                                                    <td id="Td1" style="width: 25%">
                                                        Servizi sociali
                                                    </td>
                                                    <td id="td4" style="width: 75%">
                                                        <telerik:RadComboBox ID="cmbServiziSociali" runat="server" Width="250px" EnableLoadOnDemand="true">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td2" style="width: 25%">
                                                        Fornitore gas
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:CheckBoxList ID="cmbTipoIntGas" runat="server" CellPadding="0" CellSpacing="0"
                                                            RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td3" style="width: 25%">
                                                        Fornitore elettricità
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:CheckBoxList ID="cmbTipoIntLuce" runat="server" CellPadding="0" CellSpacing="0"
                                                            RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div id="divInfoSicurezza">
                                        <fieldset>
                                            <legend>Informazioni Sicurezza</legend>
                                            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        Alloggio in Sicurezza
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:CheckBox ID="chkSecurity" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        Tipologia messa in sicurezza
                                                    </td>
                                                    <td style="width: 75%">
                                                        <%--<telerik:RadComboBox ID="cmbTipoMessaInSicurezza" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            OnClientLoad="OnClientLoadHandler">
                                                        </telerik:RadComboBox>
                                                        --%>
                                                        <asp:CheckBoxList ID="cmbTipoMessaInSicurezza" runat="server" CellPadding="0" CellSpacing="0"
                                                            RepeatColumns="3">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        Data di messa in sicurezza
                                                    </td>
                                                    <td style="width: 75%">
                                                        <telerik:RadDatePicker ID="txtDataMessaInSicurezza" runat="server" WrapperTableCaption=""
                                                            MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                                            <DateInput ID="DateInput5" runat="server">
                                                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                            </DateInput>
                                                            <Calendar ID="Calendar8" runat="server">
                                                                <SpecialDays>
                                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                    </telerik:RadCalendarDay>
                                                                </SpecialDays>
                                                            </Calendar>
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td style="width: 25%">
                                                        Data consegna nuove chiavi a SEC
                                                    </td>
                                                    <td style="width: 75%">
                                                        <telerik:RadDatePicker ID="txtDataConsegnaChiavi" runat="server" WrapperTableCaption=""
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
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        Data consegna chiavi a ST
                                                    </td>
                                                    <td style="width: 75%">
                                                        <telerik:RadDatePicker ID="txtDataConsegnaChiaviST" runat="server" WrapperTableCaption=""
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
                                                <tr>
                                                    <td>
                                                        Sede consegna chiavi
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbSedeConsegnaChiavi" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            OnClientLoad="OnClientLoadHandler">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        Operatore che consegna le chiavi
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:TextBox ID="txtOperatConsegnaChiavi" runat="server" MaxLength="500" Width="220px"
                                                            CssClass="CssMaiuscolo" ClientIDMode="Static"></asp:TextBox>
                                                        <asp:ImageButton ID="btnCercaOp1" runat="server" ImageUrl="../NuoveImm/user-icon.png" />
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div id="Div2">
                                        <fieldset>
                                            <legend>Descrizione intervento</legend>
                                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtDescrizIntervento" runat="server" MaxLength="4000" Width="95%"
                                                                        Font-Names="Arial" Font-Size="8pt" Height="70px" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                </td>
                                <td style="width: 50%; vertical-align: top;">
                                    <div id="divSoggInfoRU" style="display: block;">
                                        <fieldset>
                                            <legend>Informazioni Contratto</legend>
                                            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        Cod. contratto
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:Label Text="" runat="server" ID="lblContratto" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        Tipo contratto
                                                    </td>
                                                    <td style="width: 75%">
                                                        <telerik:RadComboBox ID="cmbTipoRU" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            OnClientLoad="OnClientLoadHandler" Enabled="False">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        Stato contratto
                                                    </td>
                                                    <td style="width: 75%">
                                                        <telerik:RadComboBox ID="cmbStatoRU" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            OnClientLoad="OnClientLoadHandler" Enabled="False">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        <div id="recupero" style="display: none;">
                                                            Motivo recupero
                                                        </div>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <div id="cmbRecupero" style="display: none;">
                                                            <telerik:RadComboBox ID="cmbMotivoRecupero" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                                OnClientLoad="OnClientLoadHandler" Enabled="True">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div id="tdlblDescrizioneSegn">
                                        <fieldset>
                                            <legend>Descrizione segnalazione</legend>
                                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtDescrizioneSegn" runat="server" MaxLength="4000" Width="95%"
                                                                        Font-Names="Arial" Font-Size="8pt" Height="70px" TextMode="MultiLine" Rows="10"
                                                                        Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div id="divAltreInfo">
                                        <fieldset>
                                            <legend>Informazioni di Chiusura</legend>
                                            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        <b>Stato alloggio all'arrivo</b>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <telerik:RadComboBox ID="cmbStatoAll" runat="server" Width="250px" EnableLoadOnDemand="True"
                                                            Culture="it-IT">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td5" style="width: 25%">
                                                        <b>Nuovo stato UI</b>
                                                    </td>
                                                    <td id="td7" style="width: 75%">
                                                        <telerik:RadComboBox ID="cmbNuovoStatoUI" runat="server" Width="250px" EnableLoadOnDemand="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text=" " Value="-1" />
                                                            </Items>
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Occupato" Value="0" />
                                                            </Items>
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Libero" Value="1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td9" style="width: 25%">
                                                        <b>Nuovo stato contratt. nucleo</b>
                                                    </td>
                                                    <td id="td10" style="width: 75%">
                                                        <telerik:RadComboBox ID="cmbNewStatoRU" runat="server" Width="250px" EnableLoadOnDemand="true">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td11" style="width: 25%">
                                                        <b>Servizi</b>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <asp:CheckBoxList ID="chkElencoServizi" runat="server" CellPadding="0" CellSpacing="0"
                                                            RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="tipiServizio" style="display: none;">
                                                            <b>Tipologia di servizio</b></div>
                                                    </td>
                                                    <td>
                                                        <div id="tdchktipiServizio" style="display: none;">
                                                            <asp:CheckBoxList ID="chkTipiServizio" runat="server" CellPadding="0" CellSpacing="0"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="12">Distacco contatore</asp:ListItem>
                                                                <asp:ListItem Value="9">Trasloco</asp:ListItem>
                                                                <asp:ListItem Value="10">Sgombero</asp:ListItem>
                                                                <asp:ListItem Value="1">Installaz.porta blindata/lastratura</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="ingressoAll" style="display: none;">
                                                            <b>Ingresso alloggio</b>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div id="tdcmbIngressoAll" style="display: none;">
                                                            <telerik:RadComboBox ID="cmbIngressoAlloggio" runat="server" Width="250px" EnableLoadOnDemand="true">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                <%--<tr>
                                                    <td id="statoAll" style="width: 25%">
                                                        Stato alloggio recuperato
                                                    </td>
                                                    <td id="tdcmbStatoAllRec" style="width: 75%">
                                                        <telerik:RadComboBox ID="cmbStatoAllRec" runat="server" Width="250px" EnableLoadOnDemand="True"
                                                            OnClientLoad="OnClientLoadHandler" Culture="it-IT">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Pessimo" Value="1" />
                                                            </Items>
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Discreto" Value="2" />
                                                            </Items>
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Buono" Value="3" />
                                                            </Items>
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Ottimo" Value="4" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td id="protocollo" style="width: 25%">
                                                        <b>Attivazione protocollo</b>
                                                    </td>
                                                    <td id="tdchkAttivoProtocollo" style="width: 75%">
                                                        <asp:CheckBox ID="chkAttivoProtocollo" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="abbAlloggio" style="width: 25%">
                                                        <b>Abbandono dell'alloggio</b>
                                                    </td>
                                                    <td id="tdchkAbbandonoAlloggio" style="width: 75%">
                                                        <asp:CheckBox ID="chkAbbandonoAlloggio" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div id="divSoggChiamante">
                                        <fieldset>
                                            <legend>Informazioni Nucleo Identificato</legend>
                                            <asp:Panel runat="server" ID="Panel1">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" AllowPaging="True"
                                                                PageSize="20" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                                                AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                                                Skin="Web20" Width="97%" OnItemDataBound="OnItemDataBoundHandler" RegisterWithScriptManager="true">
                                                                <MasterTableView CommandItemDisplay="Top" GridLines="None" AllowFilteringByColumn="False"
                                                                    AllowSorting="True" DataKeyNames="ID">
                                                                    <CommandItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="InitInsert"><img style="border:0px" alt="" src="Immagini/AddNewRecord.png" />Aggiungi nuovo record</asp:LinkButton>
                                                                    </CommandItemTemplate>
                                                                    <CommandItemSettings ShowExportToExcelButton="False" ShowExportToWordButton="false"
                                                                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="True"
                                                                        ShowRefreshButton="False" />
                                                                    <Columns>
                                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                                                        </telerik:GridEditCommandColumn>
                                                                        <telerik:GridBoundColumn HeaderText="ID" DataField="ID" Visible="false">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="COGNOME" DataField="COGNOME_SOGG_COINVOLTO">
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="NOME" DataField="NOME_SOGG_COINVOLTO">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="DATA NASCITA" DataField="DATA_NASC_SOGG_COINVOLTO">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="LUOGO NASCITA" DataField="LUOGO_NASC">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="TIPO OCCUPANTE" DataField="OCCUPANTE">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridButtonColumn CommandName="Delete" Text="Elimina" ConfirmText="Eliminare l'elemento selezionato?"
                                                                            ConfirmDialogType="RadWindow" ConfirmTitle="Elimina" ButtonType="ImageButton">
                                                                        </telerik:GridButtonColumn>
                                                                        <telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="CodiceFiscale" Value='<%# Bind( "COD_FISC_SOGG_COINVOLTO") %>'
                                                                                    ClientIDMode="Static" />
                                                                                <asp:Button ID="btnVerificaSipo" runat="server" Text="Verifica SIPO" />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                    <EditFormSettings EditFormType="Template">
                                                                        <FormTemplate>
                                                                            <table id="Table2" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                                                                                width="100%">
                                                                                <tr class="EditFormHeader">
                                                                                    <td colspan="2">
                                                                                        <b>Dettagli soggetti coinvolti</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table id="Table3" border="0" class="module" width="650px">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Cognome:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtCognome" runat="server" ClientIDMode="Static" CssClass="CssMaiuscolo"
                                                                                                        Text='<%# Bind( "COGNOME_SOGG_COINVOLTO") %>' Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Nome:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtNome" runat="server" ClientIDMode="Static" CssClass="CssMaiuscolo"
                                                                                                        Text='<%# Bind( "NOME_SOGG_COINVOLTO") %>' Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Sesso
                                                                                                </td>
                                                                                                <td>
                                                                                                    <telerik:RadComboBox ID="cmbSesso" runat="server" ClientIDMode="Static" Width="250px"
                                                                                                        EnableLoadOnDemand="True" OnClientLoad="OnClientLoadHandler" Culture="it-IT">
                                                                                                        <Items>
                                                                                                            <telerik:RadComboBoxItem runat="server" Text=" " Value="-1" />
                                                                                                        </Items>
                                                                                                        <Items>
                                                                                                            <telerik:RadComboBoxItem runat="server" Text="M" Value="0" />
                                                                                                        </Items>
                                                                                                        <Items>
                                                                                                            <telerik:RadComboBoxItem runat="server" Text="F" Value="1" />
                                                                                                        </Items>
                                                                                                    </telerik:RadComboBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Data nascita:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <telerik:RadDatePicker ID="txtDataNascSoggCoinv" runat="server" ClientIDMode="Static"
                                                                                                        DataFormatString="{0:dd/MM/yyyy}" DbSelectedDate='<%# Bind("DATA_NASC_SOGG_COINVOLTO") %>'
                                                                                                        MaxDate="01/01/9999" MinDate="1900-01-01" Skin="Web20" WrapperTableCaption="">
                                                                                                        <DateInput ID="DateInput4" runat="server">
                                                                                                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                                                                        </DateInput>
                                                                                                        <Calendar ID="Calendar7" runat="server">
                                                                                                            <SpecialDays>
                                                                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                                                                    <ItemStyle BackColor="#FFFF99" Font-Bold="True" />
                                                                                                                </telerik:RadCalendarDay>
                                                                                                            </SpecialDays>
                                                                                                        </Calendar>
                                                                                                    </telerik:RadDatePicker>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Luogo Nascita
                                                                                                </td>
                                                                                                <td>
                                                                                                    <telerik:RadComboBox ID="cmbLuogoNascSoggCoinv" runat="server" ClientIDMode="Static"
                                                                                                        AutoPostBack="True" Enabled="False" EnableLoadOnDemand="true" Filter="Contains"
                                                                                                        IsCaseSensitive="false" OnSelectedIndexChanged="RadGrid1_Clicking">
                                                                                                    </telerik:RadComboBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Cod. fiscale:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtCodFiscale" runat="server" ClientIDMode="Static" AutoPostBack="True"
                                                                                                        CssClass="CssMaiuscolo" onblur="document.getElementById('HFBeforeLoading').value='1';"
                                                                                                        OnTextChanged="RadGrid1_TextChanged" Text='<%# Bind( "COD_FISC_SOGG_COINVOLTO") %>'
                                                                                                        Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Tipologia Occupante:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <telerik:RadComboBox ID="cmbTipoOccupante" runat="server" EnableLoadOnDemand="True">
                                                                                                    </telerik:RadComboBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Indirizzo Residenza:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtIndirizzoResidenza" runat="server" CssClass="CssMaiuscolo" Text='<%# Bind("indirizzo_residenza") %>'
                                                                                                        Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Indirizzo Recapito:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtPressoCor" runat="server" CssClass="CssMaiuscolo" Text='<%# Bind("presso_cor") %>'
                                                                                                        Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Telefono:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="CssMaiuscolo" Text='<%# Bind("telefono") %>'
                                                                                                        Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Disabilità:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtDisabilita" runat="server" CssClass="CssMaiuscolo" Text='<%# Bind("disabilita") %>'
                                                                                                        Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" colspan="2">
                                                                                        <asp:Button ID="btnUpdate" runat="server" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                                                                            OnClientClick="document.getElementById('HFBeforeLoading').value='1';" Text='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />
                                                                                        &nbsp;
                                                                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                            OnClientClick="document.getElementById('HFBeforeLoading').value='1';" Text="Chiudi" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </FormTemplate>
                                                                    </EditFormSettings>
                                                                </MasterTableView>
                                                                <ClientSettings>
                                                                    <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </fieldset>
                                    </div>
                                    <asp:Panel ID="PanelElencoInterventi" runat="server" Visible="false">
                                        <fieldset>
                                            <legend>Interventi esistenti per il nucleo identificato</legend>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label Text="Nessun intervento" runat="server" ID="lblIntervento" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="overflow: auto; height: 150px; width: 100%;">
                                                            <telerik:RadGrid ID="RadGridInterventiAperti" runat="server" AllowSorting="True"
                                                                GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                                                                PageSize="5" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                                                                Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Width="97%" Height="95%"
                                                                IsExporting="False">
                                                                <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessun intervento presente"
                                                                    HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="num_int" HeaderText="NUM. INTERVENTO">
                                                                            <HeaderStyle Width="20%" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="COGNOME" DataField="COGNOME_SOGG_COINVOLTO">
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="NOME" DataField="NOME_SOGG_COINVOLTO">
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="DATA_APERTURA" HeaderText="DATA INSERIMENTO">
                                                                            <HeaderStyle Width="40%" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO">
                                                                            <HeaderStyle Width="40%" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                                    <HeaderStyle Wrap="True" />
                                                                </MasterTableView>
                                                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                                <ClientSettings EnableRowHoverStyle="true">
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                    <Selecting AllowRowSelect="True" />
                                                                </ClientSettings>
                                                                <PagerStyle AlwaysVisible="false" />
                                                                <ItemStyle Wrap="False" />
                                                            </telerik:RadGrid>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </asp:Panel>
                                    <div id="entiCoinvolti">
                                        <fieldset>
                                            <legend>Informazioni Enti Coinvolti</legend>
                                            <asp:Panel runat="server" ID="Panel3">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadGrid ID="RadGridEnteCoinv" runat="server" GridLines="None" AllowPaging="True"
                                                                PageSize="20" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                                                AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                                                Skin="Web20" Width="97%" OnItemDataBound="OnItemDataBoundHandler2">
                                                                <MasterTableView CommandItemDisplay="Top" GridLines="None" AllowFilteringByColumn="False"
                                                                    AllowSorting="True" DataKeyNames="ID">
                                                                    <CommandItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="InitInsert"><img style="border:0px" alt="" src="Immagini/AddNewRecord.png" />Aggiungi nuovo record</asp:LinkButton>
                                                                    </CommandItemTemplate>
                                                                    <CommandItemSettings ShowExportToExcelButton="False" ShowExportToWordButton="false"
                                                                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="True"
                                                                        ShowRefreshButton="False" />
                                                                    <Columns>
                                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                                                        </telerik:GridEditCommandColumn>
                                                                        <telerik:GridBoundColumn HeaderText="ID" DataField="ID" Visible="false">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="COGNOME" DataField="COGNOME_ENTE_COINVOLTO">
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="NOME" DataField="NOME_ENTE_COINVOLTO">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="ENTE" DataField="DESCRIZIONE_ENTE">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="RUOLO" DataField="RUOLO">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="EMAIL" DataField="EMAIL">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="TELEFONO" DataField="TELEFONO">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridButtonColumn CommandName="Delete" Text="Elimina" ConfirmText="Eliminare l'elemento selezionato?"
                                                                            ConfirmDialogType="RadWindow" ConfirmTitle="Elimina" ButtonType="ImageButton">
                                                                        </telerik:GridButtonColumn>
                                                                    </Columns>
                                                                    <EditFormSettings EditFormType="Template">
                                                                        <FormTemplate>
                                                                            <table id="Table2" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                                                                                width="100%">
                                                                                <tr class="EditFormHeader">
                                                                                    <td colspan="2">
                                                                                        <b>Dettagli enti coinvolti</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table id="Table3" border="0" class="module" width="650px">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Cognome:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtCognome" runat="server" ClientIDMode="Static" CssClass="CssMaiuscolo"
                                                                                                        Text='<%# Bind("COGNOME_ENTE_COINVOLTO") %>' Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Nome:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtNome" runat="server" ClientIDMode="Static" CssClass="CssMaiuscolo"
                                                                                                        Text='<%# Bind("NOME_ENTE_COINVOLTO") %>' Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Tipo Ente:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <telerik:RadComboBox ID="cmbTipoEnte" runat="server" EnableLoadOnDemand="True">
                                                                                                    </telerik:RadComboBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Ruolo:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtRuolo" runat="server" CssClass="CssMaiuscolo" Text='<%# Bind("ruolo") %>'
                                                                                                        Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Email:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("email") %>' Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Telefono:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="CssMaiuscolo" Text='<%# Bind("telefono") %>'
                                                                                                        Width="350px">
                                                                                                    </asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" colspan="2">
                                                                                        <asp:Button ID="btnUpdate" runat="server" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                                                                            OnClientClick="document.getElementById('HFBeforeLoading').value='1';" Text='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />
                                                                                        &nbsp;
                                                                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                            OnClientClick="document.getElementById('HFBeforeLoading').value='1';" Text="Chiudi" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </FormTemplate>
                                                                    </EditFormSettings>
                                                                </MasterTableView>
                                                                <ClientSettings>
                                                                    <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </fieldset>
                                    </div>
                                    <div id="tblNote">
                                        <fieldset>
                                            <legend>Note </legend>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        Nuova nota
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxNota" runat="server" MaxLength="4000" Width="95%" Font-Names="Arial"
                                                            Font-Size="8pt" Height="70px" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Note precedenti
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="NOTE" style="border: 1px solid #000000; height: 70px; background-color: #E4E4E4;
                                                            overflow: scroll; width: 95%;">
                                                            <asp:Label Text="" runat="server" ID="TabellaNoteComplete" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewElencoOperatori" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <telerik:RadGrid ID="RadDataGridOperatori" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                            ResolvedRenderMode="Classic" AutoGenerateColumns="False" PageSize="100" Culture="it-IT"
                            RegisterWithScriptManager="False" Font-Size="8pt" Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true"
                            Width="97%" Height="200px" ShowHeadersWhenNoRecords="False">
                            <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessun operatore da visualizzare."
                                HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False" EmptyDataText="">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE">
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
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewElencoProc" runat="server">
            <div id="Div1" style="width: 99%; overflow: auto;">
                <telerik:RadGrid ID="RadGridProc" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" PageSize="100" Culture="it-IT"
                    RegisterWithScriptManager="False" Font-Size="8pt" Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true"
                    Width="95%">
                    <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessun procedimento presente"
                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="REFERENTE" HeaderText="REFERENTE">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
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
            <asp:TextBox ID="txtProcSelected" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8" BorderStyle="None" BackColor="Transparent" BorderColor="Transparent"
                ClientIDMode="Static" Width="80%"></asp:TextBox>
            <asp:HiddenField ID="HiddenFieldProc" runat="server" Value="0" ClientIDMode="Static" />
        </asp:View>
        <asp:View ID="View8" runat="server">
            <table width="100%">
                <tr>
                    <td width="1%">
                        &nbsp;&nbsp;
                    </td>
                    <td width="99%">
                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="ELENCO ALLEGATI"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="RadGridAllegati" runat="server" AllowAutomaticDeletes="True"
                                        AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" Culture="it-IT" GroupPanelPosition="Top"
                                        IsExporting="False" PageSize="20" ShowStatusBar="True" Height="500" Width="97%">
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ExportSettings>
                                            <Pdf PageWidth="">
                                            </Pdf>
                                        </ExportSettings>
                                        <MasterTableView AllowFilteringByColumn="False" AllowSorting="True" CommandItemDisplay="Top"
                                            DataKeyNames="ID" GridLines="None">
                                            <CommandItemTemplate>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="InitInsert"><img alt="" src="Immagini/AddNewRecord.png" 
                                                                    style="border:0px" />Aggiungi nuovo Allegato</asp:LinkButton></CommandItemTemplate>
                                            <CommandItemSettings ShowAddNewRecordButton="True" ShowExportToCsvButton="false"
                                                ShowExportToExcelButton="False" ShowExportToPdfButton="false" ShowExportToWordButton="false"
                                                ShowRefreshButton="False" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DATA_ORA2" HeaderText="DATA">
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                    <HeaderStyle Width="60%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOME_FILE" HeaderText="ALLEGATO">
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn ConfirmText="Eliminare l'elemento selezionato?" ConfirmDialogType="RadWindow"
                                                    ConfirmTitle="Elimina" HeaderText="" HeaderStyle-Width="50px" CommandName="Delete"
                                                    UniqueName="DeleteColumn" ButtonType="ImageButton">
                                                    <HeaderStyle Width="50px"></HeaderStyle>
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="Template" InsertCaption="INSERIMENTO ALLEGATO" PopUpSettings-CloseButtonToolTip="Chiudi"
                                                PopUpSettings-Height="300px" PopUpSettings-Modal="True" PopUpSettings-ShowCaptionInEditForm="True"
                                                PopUpSettings-Width="500px">
                                                <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                                <FormTemplate>
                                                    <table id="Table2" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                                                        width="500px">
                                                        <tr class="EditFormHeader">
                                                            <td width="100%">
                                                                <b></b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" width="100%">
                                                                <table id="Table3" border="0" class="module" width="100%">
                                                                    <tr valign="top">
                                                                        <td valign="top" width="10%">
                                                                            Descrizione:
                                                                        </td>
                                                                        <td valign="top" width="70%">
                                                                            <telerik:RadTextBox ID="txtDescrizioneAllegato" runat="server" Height="100px" Rows="5"
                                                                                Text='<%# Bind("DESCRIZIONE") %>' TextMode="MultiLine" Width="400px">
                                                                            </telerik:RadTextBox>
                                                                        </td>
                                                                        <td width="20%">
                                                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="txtDescrizioneAllegato"
                                                                                Display="Static" ErrorMessage="Descrizione obbligatoria!" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr valign="top">
                                                                        <td valign="top" width="10%">
                                                                            Allegato:
                                                                        </td>
                                                                        <td width="80%">
                                                                            <telerik:RadAsyncUpload ID="RadUploadAllegato" runat="server" AllowedFileExtensions="rtf,doc,docx,tiff,pdf,zip,xls,xlsx,jpg,png"
                                                                                MaxFileInputsCount="1" />
                                                                        </td>
                                                                        <td width="10%">
                                                                        </td>
                                                                    </tr>
                                                                    <tr valign="top">
                                                                        <td>
                                                                            &#160;&#160;
                                                                        </td>
                                                                        <td>
                                                                            &#160;&#160;
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &#160;&#160;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &#160;&#160;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &#160;&#160;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="100%">
                                                                <asp:Button ID="btnUpdate" runat="server" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                                                    Text='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />&#160;&#160;<asp:Button
                                                                        ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Chiudi" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FormTemplate>
                                                <PopUpSettings CloseButtonToolTip="Chiudi" Height="300px" Modal="True" Width="500px" />
                                            </EditFormSettings>
                                        </MasterTableView><FilterMenu>
                                        </FilterMenu>
                                        <HeaderContextMenu>
                                        </HeaderContextMenu>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <telerik:RadWindow ID="RadWindowAggiungi" runat="server" CenterIfModal="true" Modal="true"
        Width="300px" Height="180px" VisibleStatusbar="false" Behaviors="Pin,Close" RestrictionZoneID="RestrictionZoneID"
        Title="Seleziona Tipologia">
        <ContentTemplate>
            <table style="width: 100%;" class="tblDiv">
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Tipo Procedimento" Font-Names="arial"
                            Font-Size="8pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="cmbTipoProc" runat="server" Width="250px" EnableLoadOnDemand="true"
                            OnClientLoad="OnClientLoadHandler">
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
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr align="right">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSalvaTipoProc" runat="server" Text="Salva" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="RadButtonEsci" runat="server" Text="Esci" AutoPostBack="false"
                                        OnClientClicking="function(sender, args){closeWindow(sender, args, 'MasterPage_CPFooter_RadWindowAggiungi', '');}">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindowOperatori" runat="server" CenterIfModal="true" Modal="true"
        Width="850px" Height="410px" VisibleStatusbar="false" Behaviors="Pin,Close" RestrictionZoneID="RestrictionZoneID"
        Title="Dettaglio Gruppo">
        <ContentTemplate>
            <table style="width: 100%;" class="tblDiv">
                <tr>
                    <td>
                        <telerik:RadGrid ID="RadGridOperatori" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                            ResolvedRenderMode="Classic" AutoGenerateColumns="False" PageSize="100" Culture="it-IT"
                            RegisterWithScriptManager="False" Font-Size="8pt" Font-Names="Arial" Width="100%"
                            IsExporting="False">
                            <MasterTableView NoMasterRecordsText="Nessun operatore presente" ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_GRUPPO" HeaderText="ID_GRUPPO" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                                        <HeaderStyle Width="33%" />
                                        <ItemStyle Width="33%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME">
                                        <HeaderStyle Width="33%" />
                                        <ItemStyle Width="33%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME">
                                        <HeaderStyle Width="33%" />
                                        <ItemStyle Width="33%" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <PagerStyle AlwaysVisible="True"></PagerStyle>
                            </MasterTableView>
                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="false" />
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr align="right">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="RadButton1" runat="server" Text="Esci" AutoPostBack="false"
                                        OnClientClicking="function(sender, args){closeWindow(sender, args, 'MasterPage_CPFooter_RadWindowOperatori', '');}">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <asp:HiddenField ID="idIntervento" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="idSegnalazione" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="idFascicolo" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="idUnita" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="tipoIntervento" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="statoIntervento" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="operatoreSelected" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="operatoreSelected2" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="operatoreSelected3" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="tipoOperatore" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="idProcedim" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="tipoProc" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="gruppo" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="prNasc" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="sesso" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="dataNasc" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="luogoNasc" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="yPosRadgrid" runat="server" Value="0" ClientIDMode="Static" />
    <script type="text/javascript">
        validNavigation = false;
        function cambiaStatoIntervento() {

            if (document.getElementById('statoIntervento').value == '4') {

                if (document.getElementById('tipoIntervento').value == '1') {

                    //                    if (document.getElementById('segnalante')) {
                    //                        document.getElementById('segnalante').style.display = 'none';

                    //                    }
                    if (document.getElementById('tipoInterv')) {
                        document.getElementById('tipoInterv').style.display = 'block';

                    }

                    if (document.getElementById('protocollo')) {
                        document.getElementById('protocollo').style.display = 'none';

                    }
                    if (document.getElementById('abbAlloggio')) {
                        document.getElementById('abbAlloggio').style.display = 'none';

                    }

                    if (document.getElementById('tdcmbTipoIntSoggCoinv')) {
                        document.getElementById('tdcmbTipoIntSoggCoinv').style.display = 'block';

                    }
                    if (document.getElementById('tdchkAttivoProtocollo')) {
                        document.getElementById('tdchkAttivoProtocollo').style.display = 'none';

                    }
                    if (document.getElementById('tdchkAbbandonoAlloggio')) {
                        document.getElementById('tdchkAbbandonoAlloggio').style.display = 'none';

                    }
                    if (document.getElementById('statoAll')) {
                        document.getElementById('statoAll').style.display = 'none';

                    }
                    if (document.getElementById('tdcmbStatoAllRec')) {
                        document.getElementById('tdcmbStatoAllRec').style.display = 'none';

                    }

                    if (document.getElementById('divInfoSicurezza')) {
                        document.getElementById('divInfoSicurezza').style.display = 'block';

                    }

                    if (document.getElementById('divSoggInfoRU')) {
                        document.getElementById('divSoggInfoRU').style.display = 'block';

                    }

                    if (document.getElementById('tdlblDescrizioneSegn')) {
                        document.getElementById('tdlblDescrizioneSegn').style.display = 'none';

                    }
                }
                if (document.getElementById('tipoIntervento').value == '2') {

                    if (document.getElementById('tipoInterv')) {
                        document.getElementById('tipoInterv').style.display = 'block';

                    }

                    if (document.getElementById('protocollo')) {
                        document.getElementById('protocollo').style.display = 'none';

                    }
                    if (document.getElementById('abbAlloggio')) {
                        document.getElementById('abbAlloggio').style.display = 'none';

                    }

                    if (document.getElementById('tdcmbTipoIntSoggCoinv')) {
                        document.getElementById('tdcmbTipoIntSoggCoinv').style.display = 'block';

                    }
                    if (document.getElementById('tdchkAttivoProtocollo')) {
                        document.getElementById('tdchkAttivoProtocollo').style.display = 'none';

                    }
                    if (document.getElementById('tdchkAbbandonoAlloggio')) {
                        document.getElementById('tdchkAbbandonoAlloggio').style.display = 'none';

                    }
                    if (document.getElementById('statoAll')) {
                        document.getElementById('statoAll').style.display = 'none';

                    }
                    if (document.getElementById('tdcmbStatoAllRec')) {
                        document.getElementById('tdcmbStatoAllRec').style.display = 'none';

                    }



                    if (document.getElementById('divInfoSicurezza')) {
                        document.getElementById('divInfoSicurezza').style.display = 'block';

                    }


                    if (document.getElementById('tdlblDescrizioneSegn')) {
                        document.getElementById('tdlblDescrizioneSegn').style.display = 'none';

                    }
                }
            }
        }


        function visibleOggetti() {
            switch (document.getElementById('tipoIntervento').value) {
                case "1":



                    if (document.getElementById('segnalante')) {
                        document.getElementById('segnalante').style.display = 'block';

                    }
                    if (document.getElementById('tipoInterv')) {
                        document.getElementById('tipoInterv').style.display = 'none';

                    }

                    if (document.getElementById('protocollo')) {
                        document.getElementById('protocollo').style.display = 'none';

                    }
                    if (document.getElementById('abbAlloggio')) {
                        document.getElementById('abbAlloggio').style.display = 'none';

                    }
                    if (document.getElementById('tdcmbSoggCoinvolti')) {
                        document.getElementById('tdcmbSoggCoinvolti').style.display = 'block';

                    }
                    if (document.getElementById('tdcmbTipoIntSoggCoinv')) {
                        document.getElementById('tdcmbTipoIntSoggCoinv').style.display = 'none';

                    }
                    if (document.getElementById('tdchkAttivoProtocollo')) {
                        document.getElementById('tdchkAttivoProtocollo').style.display = 'none';

                    }
                    if (document.getElementById('tdchkAbbandonoAlloggio')) {
                        document.getElementById('tdchkAbbandonoAlloggio').style.display = 'none';

                    }

                    if (document.getElementById('tdlblDescrizioneSegn')) {
                        document.getElementById('tdlblDescrizioneSegn').style.display = 'block';

                    }

                    //05-07-2016 tdcmbverifica
                    if (document.getElementById('richiedente')) {
                        document.getElementById('richiedente').style.display = 'block';

                    }
                    if (document.getElementById('tdcmbrichiedente')) {
                        document.getElementById('tdcmbrichiedente').style.display = 'block';

                    }
                    if (document.getElementById('verifica')) {
                        document.getElementById('verifica').style.display = 'block';

                    }
                    if (document.getElementById('tdcmbverifica')) {
                        document.getElementById('tdcmbverifica').style.display = 'block';

                    }
                    break;
                case "2", "5":
                    //05-07-2016
                    if (document.getElementById('divAltreInfo')) {
                        document.getElementById('divAltreInfo').style.display = 'block';


                    }
                    if (document.getElementById('tipoIntervento').value == '5') {
                        if (document.getElementById('recupero')) {
                            document.getElementById('recupero').style.display = 'block';
                        }
                        if (document.getElementById('cmbRecupero')) {
                            document.getElementById('cmbRecupero').style.display = 'block';
                        }
                    }

                    if (document.getElementById('verifica')) {
                        document.getElementById('verifica').style.display = 'block';

                    }
                    if (document.getElementById('ingressoAll')) {
                        document.getElementById('ingressoAll').style.display = 'block';

                    }
                    if (document.getElementById('tdcmbIngressoAll')) {
                        document.getElementById('tdcmbIngressoAll').style.display = 'block';

                    }
                    if (document.getElementById('tdcmbverifica')) {
                        document.getElementById('tdcmbverifica').style.display = 'block';

                    }

                    if (document.getElementById('tipoInterv')) {
                        document.getElementById('tipoInterv').style.display = 'block';

                    }

                    if (document.getElementById('protocollo')) {
                        document.getElementById('protocollo').style.display = 'none';

                    }
                    if (document.getElementById('abbAlloggio')) {
                        document.getElementById('abbAlloggio').style.display = 'none';

                    }

                    if (document.getElementById('tdcmbTipoIntSoggCoinv')) {
                        document.getElementById('tdcmbTipoIntSoggCoinv').style.display = 'block';

                    }
                    if (document.getElementById('tdchkAttivoProtocollo')) {
                        document.getElementById('tdchkAttivoProtocollo').style.display = 'none';

                    }
                    if (document.getElementById('tdchkAbbandonoAlloggio')) {
                        document.getElementById('tdchkAbbandonoAlloggio').style.display = 'none';

                    }
                    if (document.getElementById('statoAll')) {
                        document.getElementById('statoAll').style.display = 'none';

                    }
                    if (document.getElementById('tdcmbStatoAllRec')) {
                        document.getElementById('tdcmbStatoAllRec').style.display = 'none';

                    }

                    if (document.getElementById('richiedente')) {
                        document.getElementById('richiedente').style.display = 'none';

                    }
                    if (document.getElementById('tdcmbrichiedente')) {
                        document.getElementById('tdcmbrichiedente').style.display = 'none';

                    }

                    if (document.getElementById('divInfoSicurezza')) {
                        document.getElementById('divInfoSicurezza').style.display = 'block';

                    }

                    if (document.getElementById('tdlblDescrizioneSegn')) {
                        document.getElementById('tdlblDescrizioneSegn').style.display = 'block';

                    }


                    break;

                case "3":

                    //                    if (document.getElementById('tipiServizio')) {
                    //                        document.getElementById('tipiServizio').style.display = 'block';

                    //                    }
                    //                    if (document.getElementById('tdchktipiServizio')) {
                    //                        document.getElementById('tdchktipiServizio').style.display = 'block';

                    //                    }

                    if (document.getElementById('tipoInterv')) {
                        document.getElementById('tipoInterv').style.display = 'block';

                    }

                    if (document.getElementById('protocollo')) {
                        document.getElementById('protocollo').style.display = 'none';

                    }
                    if (document.getElementById('abbAlloggio')) {
                        document.getElementById('abbAlloggio').style.display = 'none';

                    }

                    if (document.getElementById('tdcmbTipoIntSoggCoinv')) {
                        document.getElementById('tdcmbTipoIntSoggCoinv').style.display = 'block';

                    }
                    if (document.getElementById('tdchkAttivoProtocollo')) {
                        document.getElementById('tdchkAttivoProtocollo').style.display = 'none';

                    }
                    if (document.getElementById('tdchkAbbandonoAlloggio')) {
                        document.getElementById('tdchkAbbandonoAlloggio').style.display = 'none';

                    }
                    if (document.getElementById('statoAll')) {
                        document.getElementById('statoAll').style.display = 'none';

                    }
                    if (document.getElementById('tdcmbStatoAllRec')) {
                        document.getElementById('tdcmbStatoAllRec').style.display = 'none';

                    }

                    if (document.getElementById('divInfoSicurezza')) {
                        document.getElementById('divInfoSicurezza').style.display = 'block';

                    }

                    if (document.getElementById('tdlblDescrizioneSegn')) {
                        document.getElementById('tdlblDescrizioneSegn').style.display = 'block';

                    }
                    break;
                case "4":


                    if (document.getElementById('divInfoSicurezza')) {
                        document.getElementById('divInfoSicurezza').style.display = 'none';

                    }
                    if (document.getElementById('tipiInterv')) {
                        document.getElementById('tipiInterv').style.display = 'none';

                    }


                    if (document.getElementById('tdlblDescrizioneSegn')) {
                        document.getElementById('tdlblDescrizioneSegn').style.display = 'block';

                    }


                    //05-07-2016
                    if (document.getElementById('richiedente')) {
                        document.getElementById('richiedente').style.display = 'block';

                    }
                    if (document.getElementById('tdcmbrichiedente')) {
                        document.getElementById('tdcmbrichiedente').style.display = 'block';

                    }
                    if (document.getElementById('verifica')) {
                        document.getElementById('verifica').style.display = 'block';

                    }
                    if (document.getElementById('tdcmbverifica')) {
                        document.getElementById('tdcmbverifica').style.display = 'block';

                    }

                    break;
            }

        }

        function pageLoad(sender, args) {

            visibleOggetti();
            cambiaStatoIntervento();

        };

    </script>
</asp:Content>
