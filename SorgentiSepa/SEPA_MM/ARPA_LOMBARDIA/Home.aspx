<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="Home.aspx.vb" Inherits="ARPA_LOMBARDIA_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <table style="width: 100%; font-size: 9pt; color: #000;">
        <tr>
            <td style="width: 90%;">&nbsp;
            </td>
            <td style="width: 10%;">
                <table align="right">
                    <tr>
                        <td>
                            <img alt="Guida per l'Utente" src="../Standard/Immagini/guida.png" title="Guida per l'Utente"
                                style="cursor: pointer" onclick="apriAlert(Messaggio.Funzione_Non_Disponibile, 300, 150, MessaggioTitolo.Attenzione, null, '../StandardTelerik/Immagini/Messaggi/alert.png');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center;">
                <img alt="Logo Sepa" src="../immagini/SepaWeb.png" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center;">
                <img alt="Logo SES" src="../immagini/sistemiesoluzionisrl.gif" />
            </td>
        </tr>
    </table>
</asp:Content>
