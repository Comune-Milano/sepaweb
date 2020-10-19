<%@ Page Title="" Language="VB" MasterPageFile="BasePage.master" AutoEventWireup="false"
    CodeFile="Home.aspx.vb" Inherits="SIRAPER_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4 {
            font-size: 32pt;
        }

        .style5 {
            color: #0c7fb0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center; color: #801f1c; font-family: Arial; font-size: 20pt; font-weight: bold;">
                <center>
                    <asp:Label ID="lblTitolo" runat="server" Text="Siraper"></asp:Label></center>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <table style="width: 100%;">
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td style="text-align: right">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td style="text-align: right">
                            <img alt="Guida per l'Utente" src="Immagini/button-bubble-question-icon.png" title="Guida per l'Utente"
                                style="cursor: pointer" onclick="alert('Funzione non disponibile al momento!');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
