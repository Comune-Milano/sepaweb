<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="Home.aspx.vb" Inherits="SICUREZZA_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center; color: #801f1c; font-family: Arial; font-size: 20pt; font-weight: bold;">
                <center>
                    <asp:Label ID="lblTitolo" runat="server" Text="Sicurezza"></asp:Label></center>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="width: 90%;">
                <span id="testo" runat="server" clientidmode="Static" style="visibility: hidden;"><b><span style="color: #006600; font-size: 0px;"></span></b></span>
            </td>
            <td style="width: 10%;">
                <table align="right">
                    <tr>
                        <td>
                            <img alt="Guida per l'Utente" src="../Standard/Immagini/guida.png" title="Guida per l'Utente" style="cursor: pointer" onclick="alert('Funzione non disponibile al momento!');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<img alt="Logo Gestore" src="../LOGHI/image.png" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
