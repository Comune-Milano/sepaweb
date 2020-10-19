<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false"
    CodeFile="CreaPiano.aspx.vb" Inherits="FORNITORI_CreaPiano" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function ClickUscita(sender, args) {
            location.href = 'Home.aspx';
        };
        function Ricarica(sender, args) {
            location.href = 'CaricaPiani.aspx';
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Cronoprogramma attività a canone"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button Text="Crea" runat="server" ToolTip="Crea Programma Attività" ID="btnCrea" />
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
        AutoPostBack="False" CausesValidation="False" OnClientClicking="ClickUscita"
        TabIndex="3">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table border="0" cellpadding="2" cellspacing="2" width="50%">
        <tr>
            <td style="width: 15%">Fornitore*
            </td>
            <td style="width: 85%">
                <telerik:RadComboBox ID="cmbFornitore" runat="server" AutoPostBack="false" Filter="Contains"
                    Width="250px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Appalto*
            </td>
            <td>
                <telerik:RadComboBox ID="cmbAppalto" runat="server" AutoPostBack="false" Filter="Contains"
                    Width="250px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Tipologia di cronoprogramma*
            </td>
            <td>
                <telerik:RadComboBox ID="cmbTipoCronoprogramma" runat="server" AutoPostBack="false" Filter="Contains"
                    Width="250px">
                </telerik:RadComboBox>
            </td>
        </tr>
         <tr>
            <td>Attività cronoprogramma*
            </td>
            <td>
                <telerik:RadComboBox ID="cmbAttivitaCronoprogramma" runat="server" AutoPostBack="false" Filter="Contains"
                    Width="250px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Data inizio*
            </td>
            <td>
                 <telerik:RadDatePicker ID="txtPeriodoCronoprogrammaInizio" runat="server" WrapperTableCaption=""
                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                        ShowPopupOnFocus="true">
                        <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
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
                Data fine*
            </td>
            <td>
                 <telerik:RadDatePicker ID="txtPeriodoCronoprogrammaFine" runat="server" WrapperTableCaption=""
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField runat="server" ID="idFornitore" Value="0" />
    <asp:HiddenField runat="server" ID="idDirettoreLavori" Value="0" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
</asp:Content>
