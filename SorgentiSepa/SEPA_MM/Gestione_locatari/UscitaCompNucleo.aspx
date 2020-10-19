<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master"
    AutoEventWireup="false" CodeFile="UscitaCompNucleo.aspx.vb" Inherits="Gestione_locatari_UscitaCompNucleo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Eliminazione Componente"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClick="document.getElementById('frmModify').value='0';" />
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
        OnClientClick="ChiudiFinestra(document.getElementById('HFBtnToClick').value);" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                Cognome:
            </td>
            <td>
                <telerik:RadTextBox ID="txtCognome" runat="server" ReadOnly="True">
                </telerik:RadTextBox>
            </td>
            <td>
                Nome:
            </td>
            <td>
                <telerik:RadTextBox ID="txtNome" runat="server" ReadOnly="True">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Codice Fiscale:
            </td>
            <td>
                <telerik:RadTextBox ID="txtCF" runat="server" MaxLength="16" ReadOnly="True">
                </telerik:RadTextBox>
            </td>
            <td>
                Data Nascita:
            </td>
            <td>
                <telerik:RadDatePicker ID="txtDataNasc" runat="server" WrapperTableCaption="" MinDate="01/01/1000"
                    MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}" ShowPopupOnFocus="true">
                    <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa" ReadOnly="True">
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
            <td>
                Motivo uscita:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbMotivoUscita" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="200px">
                    <Items>
                        <telerik:RadComboBoxItem Text="--" Value="" Selected="true" />
                        <telerik:RadComboBoxItem Text="Provvisoria" Value="P" />
                        <telerik:RadComboBoxItem Text="Definitiva" Value="D" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td>
                Data Uscita:
            </td>
            <td>
                <telerik:RadDatePicker ID="txtDataUscita" runat="server" WrapperTableCaption="" MinDate="01/01/1000"
                    MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}" ShowPopupOnFocus="true">
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
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFBtnToClick" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idComp" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="iddich" runat="server" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>
