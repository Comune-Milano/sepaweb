<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="NuovaSegnalazione.aspx.vb" Inherits="GESTIONE_CONTATTI_NuovaSegnalazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var SelezionatoChiamante;
        var SelezionatoIntestatario;
        var SelezionatoDanneggiante;
        var SelezionatoDanneggiato;
        var SelezionatoSegnalazioniUnita;
        var SelezionatoSegnalazioniEdificio;
        function ApriSegnalazioneEdificio() {
            validNavigation = true;
            window.open('Segnalazione.aspx?SL=1&IDS=' + document.getElementById('idSegnalazioneSelezionataEdificio').value, 'GestioneContatti' + document.getElementById('idSegnalazioneSelezionataEdificio').value, 'height=' + screen.height / 3 * 2 + ',top=0,left=0,width=' + screen.width / 3 * 2 + ',scrollbars=no,resizable=yes');
            //location.href = 'Segnalazione.aspx?IDS=' + document.getElementById('idSegnalazioneSelezionataEdificio').value;
        };
        function ApriSegnalazioneUnita() {
            validNavigation = true;
            window.open('Segnalazione.aspx?SL=1&IDS=' + document.getElementById('idSegnalazioneSelezionataUnita').value, 'GestioneContatti' + document.getElementById('idSegnalazioneSelezionataUnita').value, 'height=' + screen.height / 3 * 2 + ',top=0,left=0,width=' + screen.width / 3 * 2 + ',scrollbars=no,resizable=yes');
            //location.href = 'Segnalazione.aspx?IDS=' + document.getElementById('idSegnalazioneSelezionataUnita').value;
        };
        function FormaAnonima() {
            if ((document.getElementById('CPContenuto_TextBoxCognomeChiamante').value == '') || (document.getElementById('CPContenuto_TextBoxNomeChiamante').value == '')) {
                var chiediConferma = window.confirm('Non è stato identificato il chiamante.\nProcedere in forma anonima?');
                if (chiediConferma == true) {
                    document.getElementById('anonimo').value = '1';
                } else {
                    document.getElementById('anonimo').value = '0';
                    return false;
                };
            };
        };
        function showImageOnSelectedItemChanging(sender, eventArgs) {
            var input = sender.get_inputDomElement();
            input.style.background = "url(" + eventArgs.get_item(sender._selectedIndex).get_imageUrl() + ") no-repeat";
        };
        function showFirstItemImage(sender) {
            var input = sender.get_inputDomElement();
            input.style.background = "url(" + sender.get_items().getItem(sender._selectedIndex).get_imageUrl() + ") no-repeat";
        };


        function CalendarDatePicker(sender, args) {
            sender.get_owner().showPopup();
        };
        function CalendarDatePickerHide(sender, eventArgs) {
            if (eventArgs.keyCode == 9) {
                var dateInput = $telerik.findDateInput(sender.id);
                dateInput.get_owner().hidePopup();
            };
        };
        function CompletaDataTelerik(sender, args) {
            var keyCode = args.get_keyCode();
            if (keyCode != 9) {
                if ((keyCode < 48) || (keyCode > 57)) {
                    if (keyCode != 8) {
                        args.set_cancel(true);
                    };
                };
                var testo = sender._textBoxElement.value;
                var testolen = testo.length;
                if (testolen == 10) {
                    var selinizio = sender._textBoxElement.selectionStart;
                    var selfine = sender._textBoxElement.selectionEnd;
                    if ((selinizio == 0 && selfine == 10) == false) {
                        if (keyCode != 8) {
                            args.set_cancel(true);
                        };
                    } else {
                        if ((keyCode < 48) || (keyCode > 51)) {
                            if (keyCode != 8) {
                                args.set_cancel(true);
                            };
                        };
                    };
                } else {
                    if (testolen == 0) {
                        if ((keyCode < 48) || (keyCode > 51)) {
                            if (keyCode != 8) {
                                args.set_cancel(true);
                            };
                        };
                    } else if (testolen == 1) {
                        if (testo == 0) {
                            if ((keyCode < 49) || (keyCode > 57)) {
                                if (keyCode != 8) {
                                    args.set_cancel(true);
                                };
                            };
                        } else if (testo == 3) {
                            if ((keyCode < 48) || (keyCode > 49)) {
                                if (keyCode != 8) {
                                    args.set_cancel(true);
                                };
                            };
                        };
                    } else if (testolen == 2) {
                        if ((keyCode < 48) || (keyCode > 49)) {
                            if (keyCode != 8) {
                                args.set_cancel(true);
                            };
                        } else {
                            sender._textBoxElement.value = testo + '/';
                        };
                    } else if (testolen == 3) {
                        if ((keyCode < 48) || (keyCode > 49)) {
                            if (keyCode != 8) {
                                args.set_cancel(true);
                            };
                        };
                    } else if (testolen == 4) {
                        var n = testo.substr(3, 1);
                        if (n == 0) {
                            if ((keyCode < 49) || (keyCode > 57)) {
                                if (keyCode != 8) {
                                    args.set_cancel(true);
                                };
                            };
                        } else if (n == 1) {
                            if ((keyCode < 48) || (keyCode > 50)) {
                                if (keyCode != 8) {
                                    args.set_cancel(true);
                                };
                            };
                        };
                    } else if (testolen == 5) {
                        if (keyCode != 8) {
                            sender._textBoxElement.value = testo + '/';
                        };
                    };
                };
            };
        };

        function ConfermaEsci() {
            if (document.getElementById('frmModify').value == '1') {
                var chiediConferma;
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('frmModify').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        };
    </script>
    <link href="../StandardTelerik/Style/Site.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Label ID="Label1" Text="Nuova Segnalazione - Individuazione del soggetto chiamante e dell'oggetto della richiesta"
                runat="server" />
        </asp:View>
        <asp:View ID="View4" runat="server">
            <asp:Label ID="Label2" Text="Nuova Segnalazione - Individuazione del soggetto chiamante"
                runat="server" />
        </asp:View>
        <asp:View ID="View10" runat="server">
            <asp:Label ID="Label4" Text="Nuova Segnalazione - Individuazione dell'intestatario"
                runat="server" />
        </asp:View>
        <asp:View ID="View16" runat="server">
            <asp:Label ID="Label7" Text="Nuova Segnalazione - Individuazione del danneggiante"
                runat="server" />
        </asp:View>
        <asp:View ID="View17" runat="server">
            <asp:Label ID="Label8" Text="Nuova Segnalazione - Individuazione del danneggiato"
                runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
        <asp:View ID="View2" runat="server">
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td style="width: 38%; vertical-align: top;">
                        <fieldset>
                            <legend>Soggetto chiamante</legend>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="vertical-align: top; height: 220px;">
                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td style="width: 25%">Cognome / Ragione sociale
                                                </td>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="TextBoxCognomeChiamante" runat="server" MaxLength="500" TabIndex="1"
                                                                    Width="250px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButtonCercaChiamante" runat="server" ImageUrl="Immagini/user.png"
                                                                    TabIndex="-1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nome
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxNomeChiamante" runat="server" MaxLength="50" Width="250px"
                                                        TabIndex="2"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">Tipologia segnalante
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTipologiaSegnalante" runat="server" AutoPostBack="false"
                                                        Filter="Contains" TabIndex="-1" Width="250px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Codice contratto
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtContrattoChiamante" runat="server" MaxLength="50" TabIndex="2"
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblComplessoChiamante" runat="server" Text="Complesso immobiliare"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListComplessoChiamante" runat="server" AutoPostBack="True"
                                                        Filter="Contains" TabIndex="10" Width="250px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEdificioChiamante" runat="server" Text="Edificio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListEdificioChiamante" runat="server" Width="250px"
                                                        AutoPostBack="True" Filter="Contains" TabIndex="11">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Indirizzo
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxIndirizzoChiamante" runat="server" MaxLength="500" TabIndex="6"
                                                        Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align: top; height: 220px;">
                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblScala0" runat="server" Text="Scala"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListScalaChiamante" runat="server" AutoPostBack="false"
                                                        TabIndex="12" Width="50px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPiano0" runat="server" Text="Piano"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListPianoChiamante" runat="server" AutoPostBack="false"
                                                        TabIndex="13" Width="100px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblInterno0" runat="server" Text="Interno"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListInternoChiamante" runat="server" AutoPostBack="false"
                                                        TabIndex="14" Width="50px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Telefono 1
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxTelefono1Chiamante" runat="server" MaxLength="35" TabIndex="3"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Telefono 2
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxTelefono2Chiamante" runat="server" MaxLength="35" TabIndex="4"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Email
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxEmailChiamante" runat="server" MaxLength="100" Width="150px"
                                                        TabIndex="5"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="width: 4%; vertical-align: middle; text-align: center">
                        <asp:ImageButton ID="ImageButtonCopiaDati" runat="server" Height="32px" ImageUrl="~/NuoveImm/right-icon.png"
                            Width="32px" ToolTip="Copia dati dal soggetto chiamante" TabIndex="-1" />
                    </td>
                    <td style="width: 58%">
                        <fieldset>
                            <legend>Oggetto della chiamata</legend>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="vertical-align: top; height: 220px;">
                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td colspan="2" style="width: 25%">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                        TabIndex="-1">
                                                        <asp:ListItem Value="0" Selected="True">Alloggio</asp:ListItem>
                                                        <asp:ListItem Value="1">Parte comune</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    <asp:Label ID="lblRagioneSocialeIntestario" runat="server" Text="Cognome attuale intestatario/Rag.sociale"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="TextBoxCognomeIntestatario" runat="server" MaxLength="500" Width="250px"
                                                                    TabIndex="6"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButtonCercaIntestatario" runat="server" ImageUrl="Immagini/user.png"
                                                                    TabIndex="-1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblIntestatario" runat="server" Text="Nome attuale intestatario"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxNomeIntestatario" runat="server" MaxLength="50" Width="250px"
                                                        TabIndex="7"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCodiceContrattoIntestatario" runat="server" Text="Codice attuale contratto"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCodiceContrattoIntestatario" runat="server" MaxLength="19"
                                                        Width="150px" Enabled="False" TabIndex="8"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCodiceUnitaIntestatario" runat="server" Text="Codice unità immobiliare"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCodiceUnitaImmobiliare" runat="server" MaxLength="19" Width="150px"
                                                        Enabled="False" TabIndex="9"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblComplessoIntestatario" runat="server" Text="Complesso immobiliare"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListComplessoImmobiliare" runat="server" Width="250px"
                                                        AutoPostBack="True" Filter="Contains" TabIndex="10">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEdificio" runat="server" Text="Edificio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListEdificio" runat="server" Width="250px" AutoPostBack="True"
                                                        Filter="Contains" TabIndex="11">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Indirizzo
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxIndirizzoIntestatario" runat="server" MaxLength="500" TabIndex="12"
                                                        Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align: top; height: 220px;">
                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblScala" runat="server" Text="Scala"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListScala" runat="server" Width="50px" AutoPostBack="True"
                                                        TabIndex="12">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPiano" runat="server" Text="Piano"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListPiano" runat="server" Width="100px" AutoPostBack="True"
                                                        TabIndex="13">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblInterno" runat="server" Text="Interno"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListInterno" runat="server" Width="50px" AutoPostBack="True"
                                                        TabIndex="14">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSedeTerritoriale" runat="server" Text="Sede territoriale"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="DropDownListSedeTerritoriale" runat="server" Width="100px"
                                                        TabIndex="15">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label Text="" runat="server" ID="lblInCondominio" />
                                                </td>
                                                <td>
                                                    <asp:Label Text="" runat="server" ID="lblInCondominioSiNo" />
                                                    <asp:HiddenField runat="server" ID="idCondominio" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label Text="Abusivo" runat="server" ID="lblAbusivo" Visible="False" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAbusivoSiNo" Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label Text="Moroso" runat="server" ID="lblMoroso" Visible="False" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblMorosoSiNo" Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="vertical-align: top; text-align: center">
                                                    <asp:Panel ID="panelDann" runat="server" Visible="false">
                                                        <fieldset>
                                                            <legend>Danni</legend>
                                                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                                <tr>
                                                                    <td>Danneggiante
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxDanneggiante" runat="server" Width="70%" TabIndex="16"></asp:TextBox>
                                                                        <asp:ImageButton ID="ImageButtonDanneggiante" runat="server" Height="16px" ImageUrl="../NuoveImm/user-icon.png"
                                                                            Width="16px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Danneggiato
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBoxDanneggiato" runat="server" Width="70%" TabIndex="17"></asp:TextBox>
                                                                        <asp:ImageButton ID="ImageButtonDanneggiato" runat="server" Height="16px" ImageUrl="../NuoveImm/user-icon.png"
                                                                            Width="16px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                    <asp:HiddenField runat="server" ID="anonimo" Value="0" ClientIDMode="Static" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td style="height: 250px; vertical-align: top">
                        <fieldset>
                            <legend>Definizione categoria segnalazione </legend>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 220px; vertical-align: top">
                                        <table border="0" cellpadding="2" cellspacing="2" width="100%;">
                                            <tr>
                                                <td style="width: 30%; vertical-align: top;">Cerca categoria
                                                </td>
                                                <td style="width: 70%; vertical-align: top;">
                                                    <telerik:RadComboBox runat="server" ID="DropDownListTipologia" AutoPostBack="True"
                                                        Width="300px" Filter="Contains" TabIndex="18">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Categoria segnalazione
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello0" AutoPostBack="True"
                                                        Width="300px" TabIndex="19" Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label Text="Categoria 1" runat="server" ID="lblLivello1" Visible="False" />
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello1" AutoPostBack="True"
                                                        Visible="False" Width="300px" TabIndex="20" Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label Text="Categoria 2" runat="server" ID="lblLivello2" Visible="False" />
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello2" AutoPostBack="True"
                                                        Visible="False" Width="300px" TabIndex="21" Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label Text="Categoria 3" runat="server" ID="lblLivello3" Visible="False" />
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello3" Visible="False"
                                                        Width="300px" AutoPostBack="True" TabIndex="22" Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label Text="Categoria 4" runat="server" ID="lblLivello4" />
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox runat="server" ID="cmbTipoSegnalazioneLivello4" AutoPostBack="True"
                                                        Width="300px" TabIndex="23" Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel runat="server" ID="PanelUrgenzaCriticita" Visible="false">
                                            <fieldset style="width: 50%">
                                                <legend>Criticità </legend>
                                                <telerik:RadComboBox ID="cmbUrgenza" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Width="100px" onchange="document.getElementById('CPContenuto_txtDescrizione').focus();"
                                                    OnClientSelectedIndexChanging="showImageOnSelectedItemChanging" OnClientLoad="showFirstItemImage"
                                                    TabIndex="24" Filter="Contains">
                                                </telerik:RadComboBox>
                                            </fieldset>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td></td>
                    <td style="height: 300px; vertical-align: top">
                        <fieldset>
                            <legend>Note</legend>
                            <div class="tabContainer">
                                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Width="99%" MultiPageID="RadMultiPage1" Skin="Default"
                                    SelectedIndex="0" ShowBaseLine="True" ScrollChildren="True">
                                    <Tabs>
                                        <telerik:RadTab PageViewID="RadPageView3" Text="Controlli aggiuntivi" Selected="True" />
                                        <telerik:RadTab PageViewID="RadPageView1" Text="Numeri Utili" Selected="True" />
                                        <telerik:RadTab PageViewID="RadPageView2" Text="Documenti richiesti" />
                                        <telerik:RadTab PageViewID="RadPageView4" Text="Amministratore" />
                                        <telerik:RadTab PageViewID="RadPageView5" Text="Note" />
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" CssClass="RadMultiPage" Height="250px" Style="overflow: auto"
                                    SelectedIndex="0" Width="100%">
                                    <telerik:RadPageView ID="RadPageView3" runat="server">
                                        <br />
                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel runat="server" ID="PanelCanale">
                                                        <fieldset style="width: 50%">
                                                            <legend>Canale </legend>
                                                            <telerik:RadComboBox ID="DropDownListCanale" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Width="150px" TabIndex="24" Filter="Contains">
                                                            </telerik:RadComboBox>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="CheckBoxDVCA" Text="Seguito da DVCA" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="CheckBoxAttoVandalico" Text="Atto vandalico" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="CheckBoxFalsa" Text="Falsa segnalazione" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:CheckBox ID="CheckBoxContattatoFornitore" Text="Contattato Fornitore Emergenza" AutoPostBack="true"
                                                        runat="server" />
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:Label ID="Label3" Text="data" runat="server" Width="40px" />
                                                    <asp:TextBox runat="server" ID="TextBoxContattatoFornitore" Width="70px" Enabled="false" />
                                                    <telerik:RadTimePicker ID="TextBoxOraContattatoFornitore" runat="server" WrapperTableCaption="" Enabled="false"
                                                        Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                                        SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                                        TimeView-HeaderText="Orario">
                                                        <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                            EndTime="19:15:00">
                                                        </TimeView>
                                                        <DateInput ID="DateInput3" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                            DisplayDateFormat="HH:mm:ss">

                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:CheckBox ID="CheckBoxVerificaFornitore" Text="Verifica Fornitore Emergenza"
                                                        AutoPostBack="true" runat="server" />
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:Label ID="Label6" Text="data" runat="server" Width="40px" />
                                                    <asp:TextBox runat="server" ID="TextBoxVerificaFornitore" Width="70px" Enabled="false" />
                                                    <telerik:RadTimePicker ID="TextBoxOraVerificaFornitore" runat="server" WrapperTableCaption="" Enabled="false"
                                                        Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                                        SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                                        TimeView-HeaderText="Orario">
                                                        <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                            EndTime="19:15:00">
                                                        </TimeView>
                                                        <DateInput ID="DateInput1" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                            DisplayDateFormat="HH:mm:ss">

                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblDataOraSopralluogo" Text="Data e ora sopralluogo" runat="server" />
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:Label ID="Label11" Text="" runat="server" Width="40px" />
                                                    <asp:TextBox runat="server" ID="txtDataSopralluogo" Width="70px" />
                                                    <telerik:RadTimePicker ID="txtOraSopralluogo" runat="server" WrapperTableCaption=""
                                                        Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                                        SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                                        TimeView-HeaderText="Orario">
                                                        <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                            EndTime="19:15:00">
                                                        </TimeView>
                                                        <DateInput ID="DateInput2" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                            DisplayDateFormat="HH:mm:ss">

                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblDataOraProgrammataIntervento" Text="Data e ora programmata intervento" runat="server" />
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:Label ID="Label12" Text="" runat="server" Width="40px" />
                                                    <asp:TextBox runat="server" ID="txtDataProgrammataIntervento" Width="70px" />
                                                    <telerik:RadTimePicker ID="txtOraProgrammataIntervento" runat="server" WrapperTableCaption=""
                                                        Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                                        SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                                        TimeView-HeaderText="Orario">
                                                        <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                            EndTime="19:15:00">
                                                        </TimeView>
                                                        <DateInput ID="DateInput4" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                            DisplayDateFormat="HH:mm:ss">
                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%;">
                                                    <asp:Label ID="Label15" Text="Data e ora programmata ultimo intervento" runat="server" />
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:Label ID="Label16" Text="" runat="server" Width="40px" />
                                                    <asp:TextBox runat="server" ID="txtDataProgrammataUltimoIntervento" Width="70px" Enabled="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="Label13" Text="Data e ora effettiva intervento" runat="server" />
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:Label ID="Label14" Text="" runat="server" Width="40px" />
                                                    <asp:TextBox runat="server" ID="txtDataEffettivaIntervento" Width="70px" />
                                                    <telerik:RadTimePicker ID="txtOraEffettivaIntervento" runat="server" WrapperTableCaption=""
                                                        Width="100px" TimeView-TimeFormat="HH:mm:ss" DataFormatString="{0:HH:mm:ss}"
                                                        SelectorFormat="HH:mm:ss" DisplayValueFormat="HH:mm:ss" Font-Size="9pt" ShowPopupOnFocus="true"
                                                        TimeView-HeaderText="Orario">
                                                        <TimeView TimeFormat="HH:mm:ss" runat="server" Interval="00:15:00" StartTime="08:00:00"
                                                            EndTime="19:15:00">
                                                        </TimeView>
                                                        <DateInput ID="DateInput5" runat="server" EmptyMessage="HH:mm:ss" DateFormat="HH:mm:ss"
                                                            DisplayDateFormat="HH:mm:ss">
                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#dcdcdc" />
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView1" runat="server">
                                        <br />
                                        <asp:DataGrid runat="server" ID="DataGridNumeriUtili" AutoGenerateColumns="False"
                                            CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                            GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                                            <ItemStyle BackColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                Position="TopAndBottom" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="VALORE" HeaderText="CONTATTO"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="FASCIA" HeaderText="FASCIA ORARIA"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE"></asp:BoundColumn>
                                            </Columns>
                                            <EditItemStyle BackColor="White" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        </asp:DataGrid>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView2" runat="server">
                                        <br />
                                        <asp:Panel runat="server" ID="PanelElencoDocumentiRichiesti" Visible="false" Width="100%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label Text="Nessun documento richiesto" runat="server" ID="lblDocumentiRichiesti" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="overflow: auto; height: 150px;">
                                                            <asp:DataGrid runat="server" ID="DataGridDocumentiRichiesti" AutoGenerateColumns="False"
                                                                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                                                                <ItemStyle BackColor="White" />
                                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                                    Position="TopAndBottom" />
                                                                <AlternatingItemStyle BackColor="Gainsboro" />
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="DOCUMENTI_RICHIESTI" HeaderText="DOCUMENTI RICHIESTI"></asp:BoundColumn>
                                                                </Columns>
                                                                <EditItemStyle BackColor="White" />
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" HorizontalAlign="Center" />
                                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            </asp:DataGrid>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView4" runat="server">
                                        <br />
                                        <asp:Panel runat="server" ID="panelAmministratore" Visible="false">
                                            <fieldset>
                                                <legend>Amministratore di condominio </legend>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                                <tr>
                                                                    <td style="width: 25%">Amministratore
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label Text="" runat="server" ID="lblAmministratore" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">Indirizzo
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label Text="" runat="server" ID="lblIndirizzo" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">Recapito telefonico 1
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label Text="" runat="server" ID="lblTelefono1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">Recapito telefonico 2
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label Text="" runat="server" ID="lblTelefono2" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">Recapito telefonico 3
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label Text="" runat="server" ID="lblTelefono3" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                                <tr>
                                                                    <td style="width: 25%">Fax
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label Text="" runat="server" ID="lblFax" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">Email
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label Text="" runat="server" ID="lblEmail" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">Note
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label Text="" runat="server" ID="lblNote" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">Partita IVA
                                                                    </td>
                                                                    <td style="width: 75%">
                                                                        <asp:Label Text="" runat="server" ID="lblPartitaIVA" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </asp:Panel>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView5" runat="server">
                                        <br />
                                        <table border="0" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="NOTE_CC" runat="server" Width="95%" />
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">
                        <fieldset>
                            <legend>Segnalazioni già aperte</legend>
                            <asp:Panel ID="PanelSegnalazioniUnita" runat="server">
                                <fieldset>
                                    <legend>Segnalazioni unità selezionata</legend>
                                    <asp:Label Text="" runat="server" ID="lblDataGridSegnalazioniUnitaSelezionata" />
                                    <telerik:RadGrid ID="RadDataGridSegnalazioniUnitaSelezionata" runat="server" AllowSorting="True"
                                        GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                                        PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                                        Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Height="200px" ShowHeadersWhenNoRecords="False"
                                        Width="600px">
                                        <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessuna segnalazione da visualizzare."
                                            HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckBoxTutteSegnalazioni" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxTutteSegnalazioni_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBoxSegnalazioni" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False" EmptyDataText="">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CRITICITA" HeaderText="CRITICITA'">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CAT 1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CAT 2">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO3" HeaderText="CAT 3">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO4" HeaderText="CAT 4">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" Visible="false">
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
                                    <asp:HiddenField runat="server" ID="idSegnalazioneSelezionataUnita" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="selezionateTutte" Value="0" ClientIDMode="Static" />
                                    <asp:HiddenField ID="operatoreCC" runat="server" Value="0" ClientIDMode="Static" />
                                    <asp:HiddenField ID="operatoreFiliale" runat="server" Value="0" ClientIDMode="Static" />
                                    <asp:HiddenField ID="operatoreFilialeTecnico" runat="server" Value="0" ClientIDMode="Static" />
                                    <asp:HiddenField ID="operatoreComune" runat="server" Value="0" ClientIDMode="Static" />
                                </fieldset>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="ButtonUnisciSegnalazioni" runat="server" Text="Unisci segnalazioni"
                                                CssClass="bottone" ClientIDMode="Static" Visible="False" />
                                            <asp:Button ID="ButtonUnisciSegnalazioni1" runat="server" Text="Ok" CssClass="bottone"
                                                ClientIDMode="Static" Style="display: none" />
                                            <asp:HiddenField ID="idSegnalazione" runat="server" Value="-1" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSegnalazioniUnitaDaunire" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="PanelSegnalazioniEdificio" runat="server" Visible="False">
                                <fieldset>
                                    <legend>Segnalazioni edificio selezionato</legend>
                                    <asp:Label Text="" runat="server" ID="lblDataGridSegnalazioniEdificioSelezionato" />
                                    <telerik:RadGrid ID="RadDataGridSegnalazioniEdificioSelezionato" runat="server" AllowSorting="True"
                                        GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                                        PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                                        Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Height="200px" ShowHeadersWhenNoRecords="False"
                                        Width="600px">
                                        <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessuna segnalazione da visualizzare."
                                            HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckBoxTutteSegnalazioniEdificio" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="CheckBoxTutteSegnalazioniEdificio_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBoxSegnalazioni" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False" EmptyDataText="">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUM" HeaderText="N°">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CRITICITA" HeaderText="CRITICITA'">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPOLOGIA">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CAT 1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CAT 2">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO3" HeaderText="CAT 3">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO4" HeaderText="CAT 4">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" Visible="false">
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
                                    <asp:HiddenField runat="server" ID="idSegnalazioneSelezionataEdificio" Value="" ClientIDMode="Static" />
                                </fieldset>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="ButtonUnisciSegnalazioniEdifici" runat="server" Text="Unisci segnalazioni"
                                                CssClass="bottone" ClientIDMode="Static" Visible="False" />
                                            <asp:Button ID="ButtonUnisciSegnalazioniEdifici2" runat="server" Text="Ok" CssClass="bottone"
                                                ClientIDMode="Static" Style="display: none" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSegnalazioniEdificiDaUnire" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset>
                    </td>
                    <td></td>
                    <td style="height: 300px; vertical-align: top">
                        <fieldset>
                            <legend>Descrizione della richiesta</legend>
                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDescrizione" runat="server" MaxLength="4000" Width="95%" Font-Names="Arial"
                                            Font-Size="8pt" Height="270px" TextMode="MultiLine" Rows="10" TabIndex="25"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
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
        <asp:View ID="View5" runat="server">
            <asp:Label ID="lblChiamanti" Text="Elenco inquilini" runat="server" Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridChiamanti" AutoGenerateColumns="False" CellPadding="2"
                Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_INTE" HeaderText="ID_INTE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CODICE_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA NAS."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO U.I."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <br />
            <br />
            <asp:Label ID="lblElencoCustodi" Text="Elenco custodi" runat="server" Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridCustodi" AutoGenerateColumns="False" CellPadding="2"
                Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="MATRICOLA" HeaderText="MATRICOLA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DIPENDENTE_MM" HeaderText="DIPENDENTE MM"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CELLULARE" HeaderText="CELLULARE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <br />
            <br />
            <asp:Label ID="lblElencoChiamantiNonNoti" Text="Elenco chiamanti non noti" runat="server"
                Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridChiamantiNonNoti" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CELLULARE" HeaderText="CELLULARE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>


            <br />
            <br />
            <asp:Label ID="lblElencoFornitori" Text="Elenco chiamanti fornitori" runat="server"
                Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridFornitori" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="RAGIONE SOCIALE/COGNOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>

            <br />
            <br />
            <asp:Label ID="lblGestAutonoma" Text="Elenco gestione autonoma" runat="server"
                Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridGestAutonoma" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="RAGIONE SOCIALE/COGNOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>

            <br />
            <br />
            <asp:Label ID="lblAmministratoreCond" Text="Elenco amministratore di condominio" runat="server"
                Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridAmministratoreCond" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CELLULARE" HeaderText="CELLULARE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO"></asp:BoundColumn>

                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>

            <br />
            <br />
            <asp:Label ID="lblSoggettiCoinvolti" Text="Elenco soggetti coinvolti" runat="server"
                Font-Bold="True" />
            <asp:DataGrid runat="server" ID="DataGridSoggCoinv" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="RAGIONE SOCIALE/COGNOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <asp:HiddenField Value="" runat="server" ID="idSelectedChiamante" ClientIDMode="Static" />
            <asp:HiddenField Value="" runat="server" ID="idAnagraficaChiamante" ClientIDMode="Static" />
            <asp:HiddenField Value="" runat="server" ID="idContrattoChiamante" ClientIDMode="Static" />
           
        </asp:View>
        <asp:View ID="View11" runat="server">
            <asp:Label ID="Label5" Text="" runat="server" />
            <asp:DataGrid runat="server" ID="DataGridIntestatari" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_INTE" HeaderText="ID_INTE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CODICE_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA NAS."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO U.I."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <asp:HiddenField Value="" runat="server" ID="idSelectedIntestatario" ClientIDMode="Static" />
            <asp:HiddenField Value="" runat="server" ID="idAnagraficaIntestatario" ClientIDMode="Static" />
            <asp:HiddenField Value="" runat="server" ID="idContrattoIntestatario" ClientIDMode="Static" />
        </asp:View>
        <asp:View ID="View18" runat="server">
            <asp:Label ID="Label9" Text="" runat="server" />
            <asp:DataGrid runat="server" ID="DataGridDanneggiante" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_INTE" HeaderText="ID_INTE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CODICE_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA NAS."></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO U.I."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <asp:HiddenField ID="Danneggiante" runat="server" Value="" ClientIDMode="Static" />
        </asp:View>
        <asp:View ID="View19" runat="server">
            <asp:Label ID="Label10" Text="" runat="server" />
            <asp:DataGrid runat="server" ID="DataGridDanneggiato" AutoGenerateColumns="False"
                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_INTE" HeaderText="ID_INTE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CODICE_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA NAS."></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO U.I."></asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
            <asp:HiddenField ID="Danneggiato" runat="server" Value="" ClientIDMode="Static" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:MultiView ID="MultiView3" runat="server" ActiveViewIndex="0">
        <asp:View ID="View3" runat="server">
            <asp:Button ID="btnSvuota" runat="server" Text="Svuota" CssClass="bottone" ToolTip="Svuota campi di ricerca" />
            <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="bottone" ToolTip="Salva la segnalazione"
                ClientIDMode="Static" />
            <asp:Button ID="ButtonNuovaSegnalazioneCustode" runat="server" Text="Nuova segnalazione custodi"
                CssClass="bottone" ToolTip="Nuova segnalazione custodi" ClientIDMode="Static"
                Visible="False" />
        </asp:View>
        <asp:View ID="View6" runat="server">
            <asp:Button ID="btnConfermaChiamante" runat="server" Text="Conferma" CssClass="bottone"
                ToolTip="Conferma il chiamante selezionato" ClientIDMode="Static" />
            <asp:Button ID="btnIndietro" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Torna alla pagina precedente" />
        </asp:View>
        <asp:View ID="View12" runat="server">
            <asp:Button ID="btnConfermaIntestatario" runat="server" Text="Conferma" CssClass="bottone"
                ToolTip="Conferma l'intestatario selezionato" ClientIDMode="Static" />
            <asp:Button ID="btnIndietro2" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Torna alla pagina precedente" />
        </asp:View>
        <asp:View ID="View20" runat="server">
            <asp:Button ID="btnConfermaDanneggiante" runat="server" Text="Conferma" CssClass="bottone"
                ToolTip="Conferma l'intestatario selezionato" ClientIDMode="Static" />
            <asp:Button ID="btnIndietro5" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Torna alla pagina precedente" />
        </asp:View>
        <asp:View ID="View21" runat="server">
            <asp:Button ID="btnConfermaDanneggiato" runat="server" Text="Conferma" CssClass="bottone"
                ToolTip="Conferma l'intestatario selezionato" ClientIDMode="Static" />
            <asp:Button ID="btnIndietro6" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Torna alla pagina precedente" />
        </asp:View>
    </asp:MultiView>
    <asp:Button ID="btnEsci" runat="server" Text="Esci" CssClass="bottone" ToolTip="Esci" OnClientClick="ConfermaEsci();" />
     <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
    <script type="text/javascript">
                validNavigation = false; $(function () {
                    $("#CPContenuto_TextBoxContattatoFornitore").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                    $("#CPContenuto_TextBoxVerificaFornitore").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                    $("#CPContenuto_txtDataSopralluogo").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                    $("#CPContenuto_txtDataProgrammataIntervento").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                    $("#CPContenuto_txtDataEffettivaIntervento").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
                });
    </script>
    <asp:HiddenField ID="flCustode" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="flCondominio" Value="0" runat="server" ClientIDMode="Static" />

</asp:Content>
