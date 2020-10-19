<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master"
    AutoEventWireup="false" CodeFile="InserimentoOspiti.aspx.vb" Inherits="Gestione_locatari_InserimentoOspiti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Ospiti"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClick="document.getElementById('frmModify').value='0';" />
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
        ClientIDMode="Static" OnClientClick="ChiudiFinestra(document.getElementById('HFBtnToClick').value);" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 90%;">
        <tr>
            <td>
                Cognome:
            </td>
            <td>
                <telerik:RadTextBox ID="txtCognome" runat="server">
                </telerik:RadTextBox>
            </td>
            <td>
                Nome:
            </td>
            <td>
                <telerik:RadTextBox ID="txtNome" runat="server">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Codice Fiscale:
            </td>
            <td>
                <telerik:RadTextBox ID="txtCF" runat="server" MaxLength="16" ClientIDMode="Static"
                    AutoPostBack="true">
                </telerik:RadTextBox>
                 <asp:ImageButton ID="btnCalcoloCodFiscale" runat="server" ImageUrl="../StandardTelerik/Immagini/CodiceFiscale/codice_fiscale.gif"
                    Width="24px" Height="24px" CausesValidation="False" OnClientClick="CalcoloCodFiscale2(this);return false;" style="vertical-align:middle" />
            </td>
            <td>
                Data Nascita:
            </td>
            <td>
                <telerik:RadDatePicker ID="txtData" runat="server" WrapperTableCaption="" MinDate="01/01/1000"
                    MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}" ShowPopupOnFocus="true">
                    <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
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
        <tr>
            <td width="94px;">
                Data Inizio:
            </td>
            <td>
                <telerik:RadDatePicker ID="txtDataInizio" runat="server" WrapperTableCaption="" MinDate="01/01/1000"
                    MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}" ShowPopupOnFocus="true" AutoPostBack="true">
                    <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
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
            <td width="94px;">
                
                <asp:Label ID="lblDataFine" runat="server" Text="Data Fine:"></asp:Label>
            </td>
            <td>
                <telerik:RadDatePicker ID="txtDataFine" runat="server" WrapperTableCaption="" MinDate="01/01/1000"
                    MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}" ShowPopupOnFocus="true">
                    <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa">
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
        <tr>
            <td>
                Indirizzo:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbTipoVia" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="80px">
                </telerik:RadComboBox>
                <telerik:RadTextBox ID="txtVia" runat="server" Width="200px">
                </telerik:RadTextBox>
                Civico:
                <telerik:RadTextBox ID="txtCivico" runat="server" Width="50px">
                </telerik:RadTextBox>
            </td>
            <td>
                Parentela:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbParenti" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="200px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Comune:
            </td>
            <td>
                <telerik:RadTextBox ID="txtComune" runat="server">
                </telerik:RadTextBox>
            </td>
            <td>
                CAP:
            </td>
            <td>
                <telerik:RadTextBox ID="txtCap" runat="server">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Doc.Identità N.:
            </td>
            <td>
                <telerik:RadTextBox ID="txtDocIdent" runat="server">
                </telerik:RadTextBox>
                Data:
                <telerik:RadDatePicker ID="txtDataDocI" runat="server" WrapperTableCaption="" MinDate="01/01/1000"
                    MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}" ShowPopupOnFocus="true">
                    <DateInput ID="DateInput3" runat="server" EmptyMessage="gg/mm/aaaa">
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
            <td>
                Rilasciata da:
            </td>
            <td>
                <telerik:RadTextBox ID="txtRilasciata" runat="server">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Perm. di Sogg.:
            </td>
            <td>
                <telerik:RadTextBox ID="txtPermSogg" runat="server">
                </telerik:RadTextBox>
                Data:
                <telerik:RadDatePicker ID="txtDataPermSogg" runat="server" WrapperTableCaption=""
                    MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                    ShowPopupOnFocus="true">
                    <DateInput ID="DateInput4" runat="server" EmptyMessage="gg/mm/aaaa">
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
            <td>
                Ruolo:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbRuolo" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="200px">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFBtnToClick" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idComp" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="iddich" runat="server" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField ID="operazione" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="idTipologia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idMotivoIstanza" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idContratto" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>
