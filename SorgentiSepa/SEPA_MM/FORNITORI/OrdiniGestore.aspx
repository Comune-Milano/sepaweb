<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false"
    CodeFile="OrdiniGestore.aspx.vb" Inherits="FORNITORI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Calendario Interventi e Lavori"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <table cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <telerik:RadButton ID="btnAvviaRicerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia la ricerca in base ai filtri impostati"
                    AutoPostBack="True" CausesValidation="True" ClientIDMode="Static" Visible="false">
                </telerik:RadButton>
                <telerik:RadButton ID="btnAzzeraFiltri" runat="server" Text="Pulisci Filtri" ToolTip="Reimposta i filtri alla situazione iniziale"
                    AutoPostBack="True" CausesValidation="True" TabIndex="1" Visible = "false">
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton runat="server" Text="Export Calendario" ID="btnExpor" TabIndex="2" Visible = false> 
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton runat="server" Text="Export elenco interventi" ID="btnExportGriglia" TabIndex="200" AutoPostBack="true">
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton ID="btnVisualizza" runat="server" Text="Visualizza Intervento" ToolTip="Visualizza Intervento" Visible="false"
                    AutoPostBack="false" CausesValidation="True" OnClientClicking="VisualizzaIntervento">
                </telerik:RadButton>
            </td>

            <td>
                <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
                    AutoPostBack="False" CausesValidation="False" OnClientClicking="ClickUscita"
                    TabIndex="3">
                </telerik:RadButton>
            </td>
            <td>
                            <telerik:RadButton ID="RadButtonBuildingManager" runat="server" ToggleType="Radio"
                                ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadioChecked" />
                                    <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadio" />
                                </ToggleStates>
                            </telerik:RadButton>
            </td>
                        <td>
                            <telerik:RadButton ID="RadButtonDirettoreLavori" runat="server" ToggleType="Radio"
                                ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadioChecked" />
                                    <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadio" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="RadButtonFieldQualityManager" runat="server" ToggleType="Radio"
                                ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                                <ToggleStates>
                        <telerik:RadButtonToggleState Text="Coordinatore qualità" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Coordinatore qualità" PrimaryIconCssClass="rbToggleRadio" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="RadButtonTecnicoAmministrativo" runat="server" ToggleType="Radio"
                                ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadioChecked" />
                                    <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadio" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function ClickUscita(sender, args) {
            location.href = 'Home.aspx';
        }

        function VisualizzaIntervento(sender, args) {
            if (document.getElementById('idSegnalazione').value == '0') {
                $.notify('Selezionare un intervento dalla lista!', 'warn');
            }
            else {
                if (document.getElementById('IndiceFornitore').value == '0') {
                    //sStrSql = sStrSql & ", '<a href=''javascript:void(0);'' onclick=""window.open(''DettaglioOrdine.aspx?T=X&D='||MANUTENZIONI.PROGR||'_'||MANUTENZIONI.ANNO||''',''Intervento_'||MANUTENZIONI.ID||''','''');"">'||MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO||'</a>' AS ODL "
                    //alert(document.getElementById('ManutenzioniProgr').value)
                    ApriModuloStandard('DettaglioOrdine.aspx?T=X&D=' + document.getElementById('ManutenzioniProgr').value, 'Intervento_' + document.getElementById('ManutenzioniProgr').value);

                }
                else 
                {
                    ApriModuloStandard('Intervento.aspx?D=' + document.getElementById('idSegnalazione').value, 'Intervento_' + document.getElementById('idSegnalazione').value);
                }
            }
        }

        function Visualizza_Clik() {
            //alert("xxx");
            var itemText = "Calendario";  //$(this).parent().find(".rpText").text();
            var panelBar = $find("<%= RadPanelBar1.ClientID %>");
            var panelItem = panelBar.findItemByText(itemText);
            //panelBar.get_items().getItem(2).expand();
            panelBar.get_items().getItem(1).expand();
            //panelItem.click(); 
        }

        function CollapseCal() {
            var itemText = "Calendario";  
            var panelBar = $find("<%= RadPanelBar1.ClientID %>");
            var panelItem = panelBar.findItemByText(itemText);
            panelBar.get_items().getItem(1).collapse();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadButtonEsciAgg">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAgenda">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" LoadingPanelID="RadAjaxLoadingPanel2">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAvviaRicerca">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" LoadingPanelID="RadAjaxLoadingPanel2">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonConfermaExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" LoadingPanelID="RadAjaxLoadingPanel2">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAzzeraFiltri">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelRadGrid" LoadingPanelID="RadAjaxLoadingPanel2">
                    </telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" LoadingPanelID="RadAjaxLoadingPanel2">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButtonBOZZA">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadAgenda" LoadingPanelID="RadAjaxLoadingPanel2">
                    </telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" LoadingPanelID="RadAjaxLoadingPanel2">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridRisultatiTab">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridRisultatiTab" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <script type="text/javascript" id="telerikClientEvents1">
        //<![CDATA[

        function RadAgenda_TimeSlotClick(sender, args) {
            var data = args.get_time();
            var yyyy = data.getFullYear().toString();
            var mm = (data.getMonth() + 1).toString();
            var dd = data.getDate().toString();
            var dataC = yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
            apriAlert('Nessun ordine da visualizzare!', 300, 150, 'Attenzione', null, null);
        }

        function OnClientAppointmentClick(sender, args) {
            var apt = args.get_appointment();
            if (document.getElementById('OPS').value != '1') {
                ApriModuloStandard('DettaglioOrdine.aspx?T=X&D=' + apt.get_subject(), 'Dettagli Ordine');
            }
            else {
                ApriModuloStandard('Intervento.aspx?S=1&D=' + apt.get_subject(), 'Intervento_' + apt.get_subject());
            }
        }

        function n(n) {
            return n > 9 ? "" + n : "0" + n;
        }

        function VisualizzaGiorno(a, b, c) {
            var valore = a.toString() + '-' + n(b.toString()) + '-' + n(c.toString());
            ApriModuloStandard('DettaglioOrdine.aspx?D=X&T=' + valore, 'Dettagli Ordine');
        }

        function RowDblClick(sender, eventArgs) {
            document.getElementById('RadButtonEsciAgg1').click();
        }

        //]]>
    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ConfermaProposta(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                apriConfirm(" Richiedere il consuntivo ODL per tutti gli elementi selezionati che attualmente risultano in stato EVASO? Gli elementi selezionati e non in stato EVASO saranno comunque ignorati.", callBackFunction, 300, 150, "Info", null);
                args.set_cancel(true);
            }

            function ConfermaAccetta(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                apriConfirm(" Accettare le date pianificate di inizio e fine lavori per tutti gli elementi selezionati che attualmente risultino non accettate? Gli elementi selezionati con date in 'VERDE' saranno comunque ignorati.", callBackFunction, 300, 150, "Info", null);
                args.set_cancel(true);
            }

            function ConfermaContesta(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                apriConfirm(" Contestare le date pianificate di inizio e fine lavori per tutti gli elementi selezionati che attualmente risultino non accettate? Gli elementi selezionati con date in 'VERDE' saranno comunque ignorati.", callBackFunction, 300, 150, "Info", null);
                args.set_cancel(true);
            }

            function openWin() {
                var radwindow = $find('MasterPage_CPContenuto_RadWindowExportXLS');
                radwindow.show();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="RadWindowExportXLS" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" Behavior="Pin, Move, Resize" Title="Info" Skin="Web20"
        Height="200px" Width="250px">
        <ContentTemplate>
            <asp:Panel runat="server" ID="contenuto">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="font-family: arial, Helvetica, sans-serif; font-size: 10pt; font-weight: bold">
                            Export dati in formato Excel.
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonVistaCorrente" runat="server" Checked="True" Font-Names="arial"
                                Font-Size="9pt" GroupName="A" Text="Vista corrente" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonVistaIntervallo" runat="server" Font-Names="arial"
                                Font-Size="9pt" GroupName="A" Text="Elenco completo" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="right">
                        <td align="right">
                            <telerik:RadButton ID="RadButtonConfermaExport" runat="server" Text="Conferma">
                            </telerik:RadButton>
                        </td>
                        <td align="right">
                            <telerik:RadButton ID="RadButtonEsciConferma" runat="server" Text="Esci" AutoPostBack="false"
                                OnClientClicking="function(sender, args){closeWindow(sender, args, 'MasterPage_CPContenuto_RadWindowExportXLS', '');}">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <div class="data-container">
        <telerik:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%">
            <Items>
                <telerik:RadPanelItem Text="Filtri" Width="100%">
                    <Items>
                        <telerik:RadPanelItem>
                            <ItemTemplate>
                                <table width="100%" border="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Fornitore" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadComboBox ID="cmbFornitori" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                Filter="Contains" AutoPostBack="False" Width="400px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Stato Intervento" Font-Names="arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                        <td align="center" width="50px" style="display: none">
                                            <asp:CheckBox ID="chANNULLATO" runat="server" Font-Names="arial" Font-Size="8" Text="ANNULLATO" Checked="false" Visible="false"></asp:CheckBox>
                                        </td>
                                        <td colspan="4">
                                            
                                            <asp:CheckBoxList ID="CheckBoxListStato" runat="server" Font-Names="arial" Font-Size="8"
                                                RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Data Richiesta" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Da" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtRicDA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput7" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" OnBlur="CalendarDatePickerHide" />
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
                                            <asp:Label ID="Label10" runat="server" Text="A" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtRicA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
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
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td width ="25%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="Gravità" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadDropDownList ID="cmbGravita" runat="server" AutoPostBack="False" Width="400px"
                                                Font-Names="arial" Font-Size="8">
                                                <Items>
                                                    <telerik:DropDownListItem Text="---" Value="-1" runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-white-128.png" Text="Bianco" Value="1"
                                                        runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-green-128.png" Text="Verde" Value="2"
                                                        runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-yellow-128.png" Text="Giallo" Value="3"
                                                        runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-red-128.png" Text="Rosso" Value="4"
                                                        runat="server"></telerik:DropDownListItem>
                                                    <telerik:DropDownListItem ImageUrl="Immagini/Ball-blue-128.png" Text="Blu" Value="0"
                                                        runat="server"></telerik:DropDownListItem>
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Data termine lavorazione" Font-Names="arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label122" runat="server" Text="Da" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtFineLavoriDA" runat="server" WrapperTableCaption=""
                                                MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput2" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" OnBlur="CalendarDatePickerHide" />
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
                                        <td valign="top">
                                            <asp:Label ID="Label12" runat="server" Text="A" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtFineLavoriA" runat="server" WrapperTableCaption=""
                                                MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="Data DPIL" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label14" runat="server" Text="Da" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtPGIDA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput4" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" OnBlur="CalendarDatePickerHide" />
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
                                        <td valign="top">
                                            <asp:Label ID="Label15" runat="server" Text="A" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtPGIA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput5" runat="server">
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="Data DPFL" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label18" runat="server" Text="Da" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtTDLDA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput6" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" OnBlur="CalendarDatePickerHide" />
                                                </DateInput>
                                                <Calendar ID="Calendar7" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label17" runat="server" Text="A" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtTDLA" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                                                <DateInput ID="DateInput8" runat="server">
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Scostamento Data DPIL >= di N° Giorni: " Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtScostamentoDPIL" runat="server" Skin="Web20" NAME="txtScostamentoDPIL" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text="Scostamento Data DPFL >= di N° Giorni: " Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtScostamentoDPFL" runat="server" Skin="Web20" NAME="txtScostamentoDPFL" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Num.Ordine " Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtNumIntDa" runat="server" Width="95px" Font-Names="arial"
                                                Font-Size="8">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Num.Repertorio" Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtNumRepertorio" runat="server" Width="95px" Font-Names="arial"
                                                Font-Size="8">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Interventi " Font-Names="arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkNonRegolare" runat="server" Font-Names="arial" Font-Size="8"
                                                Text="Non Conformità" Enabled="True" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Direttore Lavori" Font-Names="arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadComboBox ID="cmbDL" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                Filter="Contains" AutoPostBack="False" Width="400px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Building Manager" Font-Names="arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadComboBox ID="cmbBM" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                Filter="Contains" AutoPostBack="False" Width="400px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Data Fine Lavori" Font-Names="arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label12" runat="server" Text="Da" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtFineLavoriDA" runat="server" MaxDate="01/01/9999"
                                                DataFormatString="{0:dd/MM/yyyy}" Calendar-Visible="false" >
                                                
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
                                        
                                        <td valign="top">
                                            <asp:Label ID="Label3" runat="server" Text="A" Font-Names="arial" Font-Size="8"></asp:Label>
                                            <telerik:RadDatePicker ID="txtFineLavoriA" runat="server" MaxDate="01/01/9999"
                                                DataFormatString="{0:dd/MM/yyyy}"  >
                                                <DateInput ID="DateInput2" runat="server">
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
                                    </tr>--%>
                                </table>
                            </ItemTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Elenco Interventi" Width="100%">
                    <Items>
                        <telerik:RadPanelItem>
                            <ItemTemplate>
                                <table width="99%" style="border: 1px solid #000080">
                                    <tr>
                                        <td>
                                            <div id="divOverContent" style="width: 99%; overflow: auto; visibility: visible;">
                                            <telerik:RadGrid ID="RadGridRisultatiTab" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    Culture="it-IT" IsExporting="False" PageSize="100" Width="200%" Height="500px"
                                                AllowPaging="True" GroupPanelPosition="Top" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                                PagerStyle-PageButtonCount="4" OnNeedDataSource="CaricaTab" OnItemDataBound="DataBoundTab"
                                                    AllowMultiRowSelection="False" ShowStatusBar="true">
                                                <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false" />
                                                <ExportSettings>
                                                    <Pdf PageWidth="">
                                                    </Pdf>
                                                </ExportSettings>
                                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="False" ReorderColumnsOnClient="False">
                                                    <ClientEvents OnRowDblClick="RowDblClick" />
                                                    <Selecting AllowRowSelect="True"></Selecting>
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                                                        ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
                                                </ClientSettings>
                                                <MasterTableView AllowFilteringByColumn="True" AllowSorting="True" GridLines="None"
                                                    PageSize="100" PagerStyle-PageButtonCount="4" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                                    PagerStyle-PageSizeControlType="None" HierarchyLoadMode="Client" Name="ElencoTab">
                                                    <Columns>
                                                        <telerik:GridBoundColumn UniqueName="ID_MANUTENZIONE" ReadOnly="True" Display="False"
                                                            DataField="ID_MANUTENZIONE" />
                                                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" ShowFilterIcon="false" Exportable="false"
                                                            FilterControlWidth="0px" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckChanged" 
                                                                        AutoPostBack="false" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                                    AutoPostBack="True" />
                                                            </HeaderTemplate>
                                                                <HeaderStyle Width="15px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="STATO_S" HeaderText="STATO INTERVENTO" Visible="false" Exportable="false">
                                                        </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="IDS" HeaderText="NUM. SEGNALAZIONE"
                                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%" Visible="true" Exportable="true">
                                                                <HeaderStyle Width="33px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ODL" HeaderText="ODL" CurrentFilterFunction="Contains" Exportable="false"
                                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%">
                                                                <HeaderStyle Width="30px" />
                                                        </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ODL1" HeaderText="ODL" CurrentFilterFunction="Contains" Exportable="true"
                                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%" ItemStyle-Width="0px">
                                                                <HeaderStyle Width="0px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridDateTimeColumn DataField="DATA_INIZIO_RICERCA" HeaderText="DATA ODL" Exportable="true"
                                                                DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" ShowFilterIcon="true"
                                                                CurrentFilterFunction="EqualTo" FilterControlWidth="90%">
                                                                <HeaderStyle Width="40px" />
                                                            </telerik:GridDateTimeColumn>
                                                        <telerik:GridBoundColumn DataField="DESCR_STATO" HeaderText="STATO INTERVENTO" CurrentFilterFunction="Contains"
                                                            AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%" Visible="true" Exportable="true">
                                                                <HeaderStyle Width="40px" />

                                                                <FilterTemplate>
                                                                    <telerik:RadComboBox ID="RadComboBoxFiltroStato" Width="100%" AppendDataBoundItems="true"
                                                                        runat="server" OnClientSelectedIndexChanged="FilterStatoIndexChanged" HighlightTemplatedItems="true"
                                                                        Filter="Contains" LoadingMessage="Caricamento...">
                                                                    </telerik:RadComboBox>
                                                                    <telerik:RadScriptBlock ID="RadScriptBlockStato" runat="server">
                                                                        <script type="text/javascript">
                                                                        function FilterStatoIndexChanged(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroStato').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("DESCR_STATO", filtro, "EqualTo");
                                                                            } else {
                                                                                tableView.filter("DESCR_STATO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                        </script>
                                                                    </telerik:RadScriptBlock>
                                                                </FilterTemplate>

                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="COD_COMPLESSO" HeaderText="COD. COMPLESSO" CurrentFilterFunction="Contains" Exportable="true"
                                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%" Visible="false">
                                                                <HeaderStyle Width="30px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="COD_EDIFICIO" HeaderText="COD. EDIFICIO" CurrentFilterFunction="Contains" Exportable="true"
                                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%" Visible="false">
                                                                <HeaderStyle Width="30px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" CurrentFilterFunction="Contains" Exportable="true"
                                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%">
                                                                <HeaderStyle Width="85px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="BUILDING_MANAGER" HeaderText="BM" CurrentFilterFunction="Contains" Exportable="true"
                                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%">
                                                                <HeaderStyle Width="50px" />

                                                                <FilterTemplate>
                                                                    <telerik:RadComboBox ID="RadComboBoxFiltroBM" Width="100%" AppendDataBoundItems="true"
                                                                        runat="server" OnClientSelectedIndexChanged="FilterBMIndexChanged" HighlightTemplatedItems="true"
                                                                        Filter="Contains" LoadingMessage="Caricamento...">
                                                                    </telerik:RadComboBox>
                                                                    <telerik:RadScriptBlock ID="RadScriptBlockBM" runat="server">
                                                                        <script type="text/javascript">
                                                                        function FilterBMIndexChanged(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroBM').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("BUILDING_MANAGER", filtro, "Contains");
                                                                            } else {
                                                                                tableView.filter("BUILDING_MANAGER", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                        </script>
                                                                    </telerik:RadScriptBlock>
                                                                </FilterTemplate>

                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ST" HeaderText="ST" CurrentFilterFunction="Contains" Exportable="true"
                                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%">
                                                                <HeaderStyle Width="20px" />

                                                                <FilterTemplate>
                                                                    <telerik:RadComboBox ID="RadComboBoxFiltroST" Width="100%" AppendDataBoundItems="true"
                                                                        runat="server" OnClientSelectedIndexChanged="FilterSTIndexChanged" HighlightTemplatedItems="true"
                                                                        Filter="Contains" LoadingMessage="Caricamento...">
                                                                    </telerik:RadComboBox>
                                                                    <telerik:RadScriptBlock ID="RadScriptBlockST" runat="server">
                                                                        <script type="text/javascript">
                                                                        function FilterSTIndexChanged(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroST').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("ST", filtro, "EqualTo");
                                                                            } else {
                                                                                tableView.filter("ST", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                        </script>
                                                                    </telerik:RadScriptBlock>
                                                                </FilterTemplate>

                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="RAGIONE_SOCIALE" HeaderText="FORNITORE" CurrentFilterFunction="Contains" Exportable="true"
                                                            AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%">
                                                            <FilterTemplate>
                                                                <telerik:RadComboBox ID="RadComboBoxFiltroFO" Width="100%" AppendDataBoundItems="true"
                                                                    runat="server" OnClientSelectedIndexChanged="FilterFOIndexChanged" HighlightTemplatedItems="true"
                                                                    Filter="Contains" LoadingMessage="Caricamento...">
                                                                </telerik:RadComboBox>
                                                                <telerik:RadScriptBlock ID="RadScriptBlockFO" runat="server">
                                                                    <script type="text/javascript">
                                                                        function FilterFOIndexChanged(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroFO').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("RAGIONE_SOCIALE", filtro, "EqualTo");
                                                                            } else {
                                                                                tableView.filter("RAGIONE_SOCIALE", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                    </script>
                                                                </telerik:RadScriptBlock>
                                                            </FilterTemplate>
                                                                <HeaderStyle Width="60px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="RIFERIMENTO" HeaderText="RIFERIMENTO" CurrentFilterFunction="Contains" Exportable="false"
                                                            AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%" Visible="false">
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NUM_ODL" HeaderText="ODL" CurrentFilterFunction="Contains" Exportable="true"
                                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%" Visible="false">
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="N. REPERTORIO" Exportable="true"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                                                FilterControlWidth="90%">
                                                                <HeaderStyle Width="30px" />

                                                                <FilterTemplate>
                                                                    <telerik:RadComboBox ID="RadComboBoxFiltroRE" Width="100%" AppendDataBoundItems="true"
                                                                        runat="server" OnClientSelectedIndexChanged="FilterREIndexChanged" HighlightTemplatedItems="true"
                                                                        Filter="Contains" LoadingMessage="Caricamento...">
                                                                    </telerik:RadComboBox>
                                                                    <telerik:RadScriptBlock ID="RadScriptBlockRE" runat="server">
                                                                        <script type="text/javascript">
                                                                        function FilterREIndexChanged(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroRE').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("NUM_REPERTORIO", filtro, "EqualTo");
                                                                            } else {
                                                                                tableView.filter("NUM_REPERTORIO", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                        </script>
                                                                    </telerik:RadScriptBlock>
                                                                </FilterTemplate>

                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="DESCRIZIONE_ANOMALIA" HeaderText="DESCRIZIONE" CurrentFilterFunction="Contains" Exportable="true"
                                                            AutoPostBackOnFilter="true" ShowFilterIcon="false" FilterControlWidth="90%">
                                                                <HeaderStyle Width="150px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridDateTimeColumn DataField="DATA_INIZIO_INTERVENTO" HeaderText="DATA INIZIO LAVORI MM" Exportable="true"
                                                                DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" ShowFilterIcon="true"
                                                                CurrentFilterFunction="EqualTo" FilterControlWidth="90%">
                                                                <HeaderStyle Width="40px" />
                                                            </telerik:GridDateTimeColumn>
                                                            <telerik:GridDateTimeColumn DataField="DATA_FINE_INTERVENTO" HeaderText="DATA TERMINE LAVORAZIONE MM" Exportable="true"
                                                                DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" ShowFilterIcon="true"
                                                                CurrentFilterFunction="EqualTo" FilterControlWidth="90%">
                                                                <HeaderStyle Width="40px" />
                                                            </telerik:GridDateTimeColumn>

                                                            <telerik:GridDateTimeColumn DataField="DATAPGI" HeaderText="DPIL" Exportable="true"
                                                                DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" ShowFilterIcon="true"
                                                                CurrentFilterFunction="EqualTo" FilterControlWidth="98%">
                                                                <HeaderStyle Width="30px" />

                                                                <FilterTemplate>
                                                                    <telerik:RadComboBox ID="RadComboBoxFiltroPericolo" Width="100px" AppendDataBoundItems="true" RenderMode="Classic"
                                                                        runat="server" OnClientSelectedIndexChanged="FilterPericoloIndexChanged" LoadingMessage="Caricamento...">
                                                                    </telerik:RadComboBox>
                                                                    <telerik:RadScriptBlock ID="RadScriptBlockPericolo" runat="server">
                                                                        <script type="text/javascript">
                                                                        function FilterPericoloIndexChanged(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container, GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroPericolo').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("DATAPGI_1", filtro, "EqualTo");
                                                                            } else {
                                                                                tableView.filter("DATAPGI_1", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                        </script>
                                                                    </telerik:RadScriptBlock>
                                                                </FilterTemplate>

                                                            </telerik:GridDateTimeColumn>

                                                            <telerik:GridBoundColumn DataField="DATAPGI_1" HeaderText="DPIL" Exportable="true"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                                                FilterControlWidth="90%">
                                                                <HeaderStyle Width="10px" />
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridDateTimeColumn DataField="DATATDL" HeaderText="DPFL" Exportable="true"
                                                                DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" ShowFilterIcon="true"
                                                                CurrentFilterFunction="EqualTo" FilterControlWidth="90%">
                                                                <HeaderStyle Width="30px" />

                                                                <FilterTemplate>
                                                                    <telerik:RadComboBox ID="RadComboBoxFiltroPericolo_1" Width="100px" AppendDataBoundItems="true" RenderMode="Classic"
                                                                        runat="server" OnClientSelectedIndexChanged="FilterPericoloIndexChanged_1" LoadingMessage="Caricamento...">
                                                                    </telerik:RadComboBox>
                                                                    <telerik:RadScriptBlock ID="RadScriptBlockPericolo_1" runat="server">
                                                                        <script type="text/javascript">
                                                                        function FilterPericoloIndexChanged_1(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container, GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroPericolo_1').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("DATATDL_1", filtro, "EqualTo");
                                                                            } else {
                                                                                tableView.filter("DATATDL_1", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                        </script>
                                                                    </telerik:RadScriptBlock>
                                                                </FilterTemplate>

                                                            </telerik:GridDateTimeColumn>

                                                            <telerik:GridBoundColumn DataField="DATATDL_1" HeaderText="DPFL" Exportable="true"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                                                FilterControlWidth="90%">
                                                                <HeaderStyle Width="10px" />
                                                        </telerik:GridBoundColumn>
                                                            <telerik:GridDateTimeColumn DataField="DATA_FINE_DITTA" HeaderText="DATA FINE INTERVENTO" Exportable="true"
                                                            DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" ShowFilterIcon="true"
                                                                CurrentFilterFunction="EqualTo" FilterControlWidth="90%">
                                                                <HeaderStyle Width="40px" />
                                                        </telerik:GridDateTimeColumn>
                                                        <telerik:GridBoundColumn DataField="DATE_ACCETTATE" HeaderText="APPROVAZIONE DATE" Exportable="true"
                                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                                            FilterControlWidth="90%">

                                                            <FilterTemplate>
                                                                <telerik:RadComboBox ID="RadComboBoxFiltroDate" Width="100%" AppendDataBoundItems="true"
                                                                    runat="server" OnClientSelectedIndexChanged="FilterDateIndexChanged" HighlightTemplatedItems="true"
                                                                    Filter="Contains" LoadingMessage="Caricamento...">
                                                                </telerik:RadComboBox>
                                                                <telerik:RadScriptBlock ID="RadScriptBlockDate" runat="server">
                                                                    <script type="text/javascript">
                                                                        function FilterDateIndexChanged(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroDate').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("DATE_ACCETTATE", filtro, "Contains");
                                                                            } else {
                                                                                tableView.filter("DATE_ACCETTATE", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                    </script>
                                                                </telerik:RadScriptBlock>
                                                            </FilterTemplate>

                                                                <HeaderStyle Width="30px" />
                                                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="IRREGOLARITA" HeaderText="NON CONF." Exportable="true"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                                                FilterControlWidth="90%">
                                                            <FilterTemplate>
                                                                <telerik:RadComboBox ID="RadComboBoxFiltroDate1" Width="100%" AppendDataBoundItems="true"
                                                                    runat="server" OnClientSelectedIndexChanged="FilterDate1IndexChanged" HighlightTemplatedItems="true"
                                                                    Filter="Contains" LoadingMessage="Caricamento...">
                                                                </telerik:RadComboBox>
                                                                <telerik:RadScriptBlock ID="RadScriptBlockDate1" runat="server">
                                                                    <script type="text/javascript">
                                                                        function FilterDate1IndexChanged(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroDate1').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("IRREGOLARITA", filtro, "Contains");
                                                                            } else {
                                                                                tableView.filter("IRREGOLARITA", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                    </script>
                                                                </telerik:RadScriptBlock>
                                                            </FilterTemplate>

                                                                <HeaderStyle Width="30px" />
                                                        </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ALLEGATI" HeaderText="ALLEGATI" Exportable="true"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                                                FilterControlWidth="90%">
                                                                <HeaderStyle Width="50px" />

                                                                <FilterTemplate>
                                                                    <telerik:RadComboBox ID="RadComboBoxFiltroAL" Width="100%" AppendDataBoundItems="true"
                                                                        runat="server" OnClientSelectedIndexChanged="FilterALIndexChanged" HighlightTemplatedItems="true"
                                                                        Filter="Contains" LoadingMessage="Caricamento...">
                                                                    </telerik:RadComboBox>
                                                                    <telerik:RadScriptBlock ID="RadScriptBlockAL" runat="server">
                                                                        <script type="text/javascript">
                                                                        function FilterALIndexChanged(sender, args) {
                                                                            var tableView = $find("<%# TryCast(Container,GridItem).OwnerTableView.ClientID %>");
                                                                            var filtro = args.get_item().get_value();
                                                                            document.getElementById('HFFiltroAL').value = filtro;
                                                                            if (filtro != 'Tutti') {
                                                                                tableView.filter("ALLEGATI", filtro, "Contains");
                                                                            } else {
                                                                                tableView.filter("ALLEGATI", "", Telerik.Web.UI.GridFilterFunction.NoFilter);
                                                                            };
                                                                        };
                                                                        </script>
                                                                    </telerik:RadScriptBlock>
                                                                </FilterTemplate>

                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn DataField="IDENTIFICATIVO" HeaderText="IDENTIFICATIVO" Visible="true"
                                                                FilterControlWidth="0px" ItemStyle-Width="0px" HeaderStyle-Width="0px" EmptyDataText=" "
                                                                Exportable="false">
                                                                <HeaderStyle Width="0px"></HeaderStyle>
                                                                <ItemStyle Width="0px"></ItemStyle>
                                                            </telerik:GridBoundColumn>

                                                    </Columns>
                                                    <SortExpressions>
                                                            <telerik:GridSortExpression FieldName="DATA_INIZIO_RICERCA" SortOrder="Descending" />
                                                    </SortExpressions>
                                                </MasterTableView>
                                                <HeaderContextMenu>
                                                </HeaderContextMenu>
                                            </telerik:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <telerik:RadButton ID="RadButtonContab" runat="server" Text="Richiesta Consuntivo ODL"
                                                            Width="190px" AutoPostBack="True" ClientIDMode="Static" OnClientClicking="function(sender, args){ConfermaProposta(sender, args);}"
                                                            OnClick="RichiestaMassivaContabilita">
                                                        </telerik:RadButton>

                                                        <telerik:RadButton ID="RadButtonAccetta" runat="server" Text="Accetta Modifica Date"
                                                            Width="190px" AutoPostBack="True" ClientIDMode="Static" OnClientClicking="function(sender, args){ConfermaAccetta(sender, args);}"
                                                            OnClick="RichiestaMassivaAccetta">
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="RadButtonContesta" runat="server" Text="Contesta Modifica Date"
                                                            Width="190px" AutoPostBack="True" ClientIDMode="Static" OnClientClicking="function(sender, args){ConfermaContesta(sender, args);}"
                                                            OnClick="RichiestaMassivaContesta">
                                                        </telerik:RadButton>

                                                        <telerik:RadButton ID="RadButtonEsciAgg" runat="server" Text="Visualizza in calendario" Width="190px"
                                                            AutoPostBack="true" ClientIDMode="Static" OnClick="CaricaInAgenda" OnClientClicking="Visualizza_Clik">
                                                        </telerik:RadButton>
                                                    </td>
                                                    <td style="text-align: left">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>

                <telerik:RadPanelItem Text="Calendario" Width="100%">
                    <Items>
                        <telerik:RadPanelItem>
                            <ItemTemplate>
                                <table width="99%" style="border: 1px solid #000080">
                                    <tr>
                                        <td>
                                            <div id="divOverContent2" style="width: 99%; overflow: auto; visibility: visible;">
            <table>
                                                    <tr>
                    <td valign="middle">
                        <asp:Label ID="Label2" runat="server" Text="Visualizza" Font-Names="arial" Font-Size="8"></asp:Label>
                    </td>
                    <td colspan="4" valign="middle">
                                                            <telerik:RadDropDownList ID="cmbGiorni" runat="server" AutoPostBack="true" Width="50px" OnSelectedIndexChanged="cmbGiorni_SelectedIndexChanged">
                        </telerik:RadDropDownList>
                        <asp:Label ID="Label3" runat="server" Text=" giorni nella griglia" Font-Names="arial"
                            Font-Size="8"></asp:Label>
                    </td>
                    <td>
                        <table style="width: 1120px;" cellpadding="1" cellspacing="1">
                                                                <tr style="display: none">
                                <td style="width: 180px; text-align: center;">
                                                                        <asp:ImageButton ID="ImageButtonEMESSO" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoEmesso.png" OnClick="ImageButtonEMESSO_Click"
                                        ToolTip="DA VERIFICARE" Height="20" Width="20" Style="cursor: pointer" />
                                </td>
                                <td style="width: 180px; text-align: center;">
                                                                        <asp:ImageButton ID="ImageButtonINTEGRATO" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoIntegrato.png" OnClick="ImageButtonINTEGRATO_Click"
                                        ToolTip="IN CARICO" Height="20" Width="20" Style="cursor: pointer" />
                                </td>
                                <td style="width: 180px; text-align: center;">
                                                                        <asp:ImageButton ID="ImageButtonANNULLATO" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoAnnullato.png" OnClick="ImageButtonANNULLATO_Click"
                                                                            ToolTip="IN CARICO MA IN RITARDO" Height="20" Width="20" Style="cursor: pointer" />
                                </td>
                                <td style="width: 180px; text-align: center;">
                                                                        <asp:ImageButton ID="ImageButtonFINE" runat="server" ImageUrl="~/FORNITORI/Immagini/OrdineRG.png" OnClick="ImageButtonFINE_Click"
                                        ToolTip="FINE LAVORI" Height="20" Width="20" Style="cursor: pointer" />
                                </td>
                                <td style="width: 240px; text-align: center;">
                                                                        <asp:ImageButton ID="ImageButtonRC" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoRC.png" OnClick="ImageButtonRC_Click"
                                        ToolTip="RICH. CONSUNTIVAZIONE ODL" Height="20" Width="20" Style="cursor: pointer" />
                                </td>
                                <td style="width: 180px; text-align: center;">
                                                                        <asp:ImageButton ID="ImageButtonContab" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoDaContabilizzare.png" OnClick="ImageButtonContab_Click"
                                        ToolTip="DA CONSUNTIVARE" Height="20" Width="20" Style="cursor: pointer" />
                                </td>
                                
                            </tr>
                            <tr>
                               
                                <td style="width: 180px; text-align: center;">
                                                                        <asp:Image ID="ImageButton1" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoEmesso.png"
                                                                            ToolTip="DA VERIFICARE" Height="20" Width="20" Style="cursor: normal" />
                                                                    </td>
                                                                    <td style="width: 180px; text-align: center;">
                                                                        <asp:Image ID="ImageButton2" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoIntegrato.png"
                                                                            ToolTip="IN CARICO" Height="20" Width="20" Style="cursor: normal" />
                                                                    </td>
                                                                    <td style="width: 180px; text-align: center;">
                                                                        <asp:Image ID="ImageButton3" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoAnnullato.png"
                                                                            ToolTip="IN CARICO MA IN RITARDO" Height="20" Width="20" Style="cursor: normal" />
                                                                    </td>
                                                                    <td style="width: 180px; text-align: center;">
                                                                        <asp:Image ID="ImageButton4" runat="server" ImageUrl="~/FORNITORI/Immagini/OrdineRG.png"
                                                                            ToolTip="FINE LAVORI" Height="20" Width="20" Style="cursor: normal" />
                                                                    </td>
                                                                    <td style="width: 240px; text-align: center;">
                                                                        <asp:Image ID="ImageButton5" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoRC.png"
                                                                            ToolTip="RICH. CONSUNTIVAZIONE ODL" Height="20" Width="20" Style="cursor: normal" />
                                                                    </td>
                                                                    <td style="width: 180px; text-align: center;">
                                                                        <asp:Image ID="ImageButton6" runat="server" ImageUrl="~/FORNITORI/Immagini/StatoDaContabilizzare.png"
                                                                            ToolTip="DA CONSUNTIVARE" Height="20" Width="20" Style="cursor: normal" />
                                                                    </td>

                                                                </tr>

                                                                <tr>

                                                                    <td style="width: 180px; text-align: center;">
                                    <asp:Label ID="lblNumEmesso" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                                        Text="DA VERIFICARE"></asp:Label>
                                </td>
                                <td style="width: 180px; text-align: center;">
                                    <asp:Label ID="lblNumIntegrato" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                        Font-Size="8pt" Text="IN CARICO"></asp:Label>
                                </td>
                               
                                <td style="width: 180px; text-align: center;">
                                    <asp:Label ID="lblNumAnnullato" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                        Font-Size="8pt" Text="ANNULLATO"></asp:Label>
                                </td>
                                <td style="width: 180px; text-align: center;">
                                    <asp:Label ID="lblFineLavori" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                        Font-Size="8pt" Text="EVASO"></asp:Label>
                                </td>
                                <td style="width: 240px; text-align: center;">
                                    <asp:Label ID="lblRichiestaC" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                        Font-Size="8pt" Text="RICH. CONSUNTIVAZIONE ODL" Visible="True"></asp:Label>
                                </td>
                                <td style="width: 180px; text-align: center;">
                                    <asp:Label ID="lblDaContabilizzare" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                        Font-Size="8pt" Text="DA CONSUNTIVARE" Visible="True"></asp:Label>
                                </td>
                                
                            </tr>
                        </table>
                    </td>

                                                        <td valign="middle">
                                                            <telerik:RadButton ID="Previous" runat="server" Text="<<< " Width="60px" ToolTip="Salta all'ODL Precedente"
                                                                AutoPostBack="true" ClientIDMode="Static" OnClick="Previous_Appointment">
                                                            </telerik:RadButton>
                                                        </td>
                                                        <td valign="middle">
                                                            <telerik:RadButton ID="Next" runat="server" Text=">>> " Width="60px" ToolTip="Salta all'ODL Successivo"
                                                                AutoPostBack="true" ClientIDMode="Static" OnClick="Next_Appointment">
                                                            </telerik:RadButton>
                                                        </td>

                                                        <tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="divOverContent1" style="width: 99%; overflow: auto; visibility: visible;">
                                                <telerik:RadScheduler runat="server" ID="RadAgenda" SelectedView="MonthView" RenderMode="Lightweight"
                                                    OnAppointmentDataBound="RadAgenda_AppointmentDataBound" OnNavigationComplete="RadAgenda_NavigationComplete" OnPreRender="RadAgenda_PreRender"
                                                    ColumnWidth="90px" DayEndTime="23:59:59" DayStartTime="00:00:00" RowHeight="40px"
                                                    DataKeyField="ID_MANUTENZIONE" DataSubjectField="NUM_ODL" DataStartField="DATA_INIZIO_INTERVENTO"
                                                    DataEndField="DATA_FINE_INTERVENTO_AGENDA" Localization-HeaderMultiDay="Work Week"
                                                    AllowDelete="False" Culture="it-IT" DisplayDeleteConfirmation="False" AppointmentStyleMode="Simple"
                                                    Skin="Web20" ShowViewTabs="true" ShowHoursColumn="False" OnClientTimeSlotClick="RadAgenda_TimeSlotClick"
                                                    OnClientAppointmentClick="OnClientAppointmentClick" AllowEdit="False" AllowInsert="False"
                                                    ReadOnly="True">
                                                    <ExportSettings FileName="OrdiniExport" OpenInNewWindow="True">
                                                        <Pdf PageTopMargin="1in" PageBottomMargin="1in" PageLeftMargin="1in" PageRightMargin="1in"
                                                            Author="SepaWeb" Title="Elenco Ordini" Creator="SepaWeb" PageHeight="297mm" PageTitle="Elenco Ordini"
                                                            PageWidth="210mm" PaperSize="A4" Producer="SepaWeb"></Pdf>
                                                    </ExportSettings>
                                                    <AdvancedForm Modal="true"></AdvancedForm>
                                                    <Localization HeaderMultiDay="Work Week"></Localization>
                                                    <MultiDayView UserSelectable="false"></MultiDayView>
                                                    <DayView UserSelectable="false"></DayView>
                                                    <TimelineView ShowInsertArea="True" ReadOnly="True" SlotDuration="1.00:00:00" />
                                                    <WeekView UserSelectable="false"></WeekView>
                                                    <MonthView UserSelectable="true" ReadOnly="True"></MonthView>
                                                    <TimeSlotContextMenuSettings EnableEmbeddedBaseStylesheet="False" EnableEmbeddedScripts="False"
                                                        EnableEmbeddedSkins="False"></TimeSlotContextMenuSettings>
                                                    <AgendaView UserSelectable="False" />
                                                    <AppointmentContextMenuSettings EnableEmbeddedBaseStylesheet="False" EnableEmbeddedScripts="False"
                                                        EnableEmbeddedSkins="False"></AppointmentContextMenuSettings>
                                                </telerik:RadScheduler>

                                            </div>
                                        </td>
                </tr>
            </table>

                            </ItemTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
            </Items>
            <ExpandAnimation Type="Linear" />
            <CollapseAnimation Type="Linear" />
        </telerik:RadPanelBar>
    </div>
    <asp:Panel runat="server" ID="PanelRadGrid">
        <div id="divOverContent" style="width: 99%; overflow: auto;">
        </div>
    </asp:Panel>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenField1" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivHeight" Value="100" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivWidth" Value="1" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="idSelRisultati" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="idSelRisultatiODL" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="idSelRisultatiREP" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="IndiceFornitore" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="OPS" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="idSegnalazione" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="ManutenzioniProgr" Value="0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HFFiltroPericolo" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HFFiltroPericolo_1" />

    <asp:HiddenField ID="HFFiltroStato" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="isExporting" />

    <asp:HiddenField ID="HFFiltroFO" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroBM" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroST" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroRE" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroAL" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroDate" runat="server" Value="Tutti" ClientIDMode="Static" />
    <asp:HiddenField ID="HFFiltroDate1" runat="server" Value="Tutti" ClientIDMode="Static" />


    <telerik:RadButton ID="RadButtonEsciAgg1" runat="server" Text="Visualizza" Width="104px"
        AutoPostBack="true" ClientIDMode="Static" Style="visibility: hidden">
    </telerik:RadButton>
    <script type="text/javascript">
        validNavigation = false;
        $(document).ready(function () {
            //Ridimensiona();
            AggiungiLink();
            CollapseCal();
        });
        $(window).resize(function () {
            //Ridimensiona();
            AggiungiLink();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 350;
            var larghezzaRad = $(window).width() - 37;
            //var altezzaRad = $(window).height() - 400;
            //var larghezzaRad = $(window).width() - 40;
            $("#MasterPage_CPContenuto_RadAgenda").width(larghezzaRad);
            //$("#MasterPage_CPContenuto_RadPanelBar1_i2_i0_RadAgenda").width(larghezzaRad);

            $("#MasterPage_CPContenuto_RadAgenda").height(altezzaRad);
            //$("#MasterPage_CPContenuto_RadPanelBar1_i2_i0_RadAgenda").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }

        function AggiungiLink() {
            if (document.getElementById('IndiceFornitore').value == '0') {
                $('.rsHorizontalHeaderTable th').each(function () {
                    var link = $(this).html().toString();
                    link = link.replace(/\t/g, '');
                    var data = link.substr(6, 10);
                    var dataC = data.trim();
                    var valore = dataC.substr(6, 4);
                    var valore1 = dataC.substr(3, 2);
                    var valore2 = dataC.substr(0, 2);
                    $(this).contents().wrap('<a href="#" onclick="VisualizzaGiorno(' + valore + ', ' + valore1 + ', ' + valore2 + ');"></a>');
                });
            }
        }
        function OnClientClicked() {
            var w = 200;
            var h = 200;
            var left = ((screen.width / 2) - (w / 2)) - 15;
            var top = ((screen.height / 2) - (h / 2)) - 15;
            var targetWin = window.open('exportPDF.aspx', 'export', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            //var targetWin = window.open('exportXLS.aspx', 'export', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
