<%@ Page Title="Report Emesso Totale Completo" Language="VB" MasterPageFile="~/Contratti/Spalmatore/HomePage.master" AutoEventWireup="false" CodeFile="RptEmessoTotaleCompleto.aspx.vb" Inherits="Contratti_Spalmatore_RptEmessoTotaleCompleto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function apriMaschera() {
            location.replace('VisualizzazioneRpt.aspx');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">

    <asp:Button ID="btnProcedi" runat="server" Text="Avvia Report" ToolTip="Procedi con elaborazione" />

    <asp:Button ID="btnEsci" runat="server" Text="Esci" CausesValidation="false" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <fieldset style="width: 500px;">
        <legend>Filtro bollette</legend>
        <table style="width: 90%;">
            <tr>
                <td>&nbsp</td>
            </tr>
            <tr>
                <td>Data scadenza boll. compensabili
                </td>
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
    </fieldset>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
