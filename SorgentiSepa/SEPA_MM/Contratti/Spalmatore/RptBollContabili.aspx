<%@ Page Title="Report Bollette Contabili" Language="VB" MasterPageFile="~/Contratti/Spalmatore/HomePage.master" AutoEventWireup="false" CodeFile="RptBollContabili.aspx.vb" Inherits="Contratti_Spalmatore_RptBollContabili" %>

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
    <fieldset style="width:500px;">
        <legend>Filtro tipologia</legend>
        <table style="width: 70%;">
            <tr><td>&nbsp</td></tr>
            <tr>
                <td>Tipo bollette
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbTipoBoll" runat="server" Width="250px" EnableLoadOnDemand="true" OnClientLoad="OnClientLoadHandler">
                        <Items>
                            <telerik:RadComboBoxItem Value="-1" Text="Tutte" />
                            <telerik:RadComboBoxItem Value="0" Text="Pre SEPA" />
                            <telerik:RadComboBoxItem Value="1" Text="Post SEPA" />
                        </Items>
                    </telerik:RadComboBox>
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

