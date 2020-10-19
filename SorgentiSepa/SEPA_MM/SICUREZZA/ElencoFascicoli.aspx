<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="ElencoFascicoli.aspx.vb" Inherits="SICUREZZA_ElencoFascicoli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Storico Stampe
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 99%">
        <tr>
            <td style="width: 99%">
                <fieldset>
                    <legend style="font-family: Arial; font-size: 9pt; font-weight: bold; color: Black;">
                        Elenco Fascicoli</legend>
                    <br />
                    <asp:Label ID="lblTbl" runat="server" Font-Names="arial" Font-Size="10pt"></asp:Label>
                    <div style="overflow: auto; height: 320px;">
                        <asp:Label ID="lblFascicoli" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="1"></asp:Label>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
