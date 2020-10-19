<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="NuovoProcedimento.aspx.vb" Inherits="SICUREZZA_NuovoProcedimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" Text="Procedimento" runat="server" />
    <asp:Label ID="lblTitolo2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva il procedimento"
        ClientIDMode="Static" />
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="confermaEsci(1,document.getElementById('txtModificato').value);return false;" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td style="vertical-align: top">
                            <fieldset>
                                <legend>Informazioni Generali</legend>
                                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td>
                                            Num. segnalazione
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Label Text="" runat="server" ID="lblNsegn" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Data
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
                                            Tipo
                                        </td>
                                        <td style="width: 75%">
                                            <telerik:RadComboBox ID="cmbTipoProc" runat="server" Width="230px" EnableLoadOnDemand="true"
                                                OnClientLoad="OnClientLoadHandler" AutoPostBack="True">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="Penale" Value="Penale" Owner="cmbTipoProc" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Civile" Value="Civile" Owner="cmbTipoProc" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Amministrativo" Value="Amministrativo"
                                                        Owner="cmbTipoProc" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            Stato
                                        </td>
                                        <td style="width: 75%">
                                            <telerik:RadComboBox ID="cmbStatoProc" runat="server" Width="230px" EnableLoadOnDemand="true"
                                                OnClientLoad="OnClientLoadHandler">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="In Corso" Value="1" Owner="cmbStatoProc" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Chiuso" Value="2" Owner="cmbStatoProc">
                                                    </telerik:RadComboBoxItem>
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Referente
                                        </td>
                                        <td style="width: 75%">
                                            <asp:TextBox ID="txtReferente" runat="server" MaxLength="500" Width="225px" CssClass="CssMaiuscolo"
                                                ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Data di apertura
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
                                        <td id="tdCivile">
                                            Rich. decreto ingiuntivo
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkCivile" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="tdAmmvo">
                                            Rich. decreto ricorso
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkAmmvo" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="divPenale">
                                            <fieldset>
                                                <legend>Penale</legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="25%">
                                                            Tipo
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmbTipoPenale" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                                OnClientLoad="OnClientLoadHandler">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="Querela" Value="Querela" Owner="cmbTipoPenale" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="Remissione Querela" Value="Remissione Querela"
                                                                        Owner="cmbTipoPenale" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Autorità giudiziaria
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAutoritaGiud" runat="server" Width="200px" 
                                                                CssClass="CssMaiuscolo"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Data di convocazione
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtDataConvocazione" runat="server" WrapperTableCaption=""
                                                                MaxDate="01/01/9999" Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
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
                                                            Luogo di convocazione
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLuogoConv" runat="server" Width="200px" 
                                                                CssClass="CssMaiuscolo"></asp:TextBox>
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
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td style="width: 50%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        Nuova nota
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBoxNota" runat="server" MaxLength="4000" Width="100%" Font-Names="Arial"
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
                                            overflow: scroll; width: 100%;">
                                            <asp:Label Text="" runat="server" ID="TabellaNoteComplete" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="tipoProc" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="idProcedim" runat="server" Value="0" ClientIDMode="Static" />
    <script type="text/javascript">
        validNavigation = false;
        function visibleOggetti() {
            switch (document.getElementById('tipoProc').value) {
                case "Civile":
                    if (document.getElementById('divPenale')) {
                        document.getElementById('divPenale').style.display = 'none';
                        document.getElementById('CPContenuto_chkCivile').style.display = 'block';
                        document.getElementById('CPContenuto_chkAmmvo').style.display = 'none';
                        document.getElementById('tdCivile').style.display = 'block';
                        document.getElementById('tdAmmvo').style.display = 'none';
                    }
                    break;
                case "Penale":
                    if (document.getElementById('divPenale')) {
                        document.getElementById('divPenale').style.display = 'block';
                        document.getElementById('CPContenuto_chkCivile').style.display = 'none';
                        document.getElementById('CPContenuto_chkAmmvo').style.display = 'none';
                        document.getElementById('tdCivile').style.display = 'none';
                        document.getElementById('tdAmmvo').style.display = 'none';
                    }
                    break;
                case "Amministrativo":
                    if (document.getElementById('divPenale')) {
                        document.getElementById('divPenale').style.display = 'none';
                        document.getElementById('CPContenuto_chkCivile').style.display = 'none';
                        document.getElementById('CPContenuto_chkAmmvo').style.display = 'block';
                        document.getElementById('tdCivile').style.display = 'none';
                        document.getElementById('tdAmmvo').style.display = 'block';
                    }
                    break;
            }

        }
        visibleOggetti();
    </script>
</asp:Content>
