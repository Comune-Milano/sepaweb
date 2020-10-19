<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_InfoRateizz.ascx.vb" Inherits="VSA_NuovaDomandaVSA_Tab_InfoRateizz" %>
<table width="97%">
    <tr>
        <td>
            <table style="margin-left: 10px; width: 99%; height: 99%;">
                <tr>
                    <td style="border: 1px solid #0066FF; vertical-align: top;">
                        <div style="overflow: auto;">
                            <table width="100%" style="font-family: Arial; font-size: 9pt;" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td colspan="5" align="center" style="background-color: #FFFFB3; font-family: Arial; font-size: 9pt;">
                                        <b>DATI CAPIENZA</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>a) Debito reale
                                    </td>
                                    <td>b) Debito ricalcolato (120%) 
                                    </td>
                                    <td>c) Somma valori patrimoniali
                                    </td>

                                    <td>Capienza
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDebitoReale" runat="server" Width="150px" Font-Names="arial" Font-Size="9pt" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDeb120" runat="server" Width="150px" Font-Names="arial" Font-Size="9pt" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotPatrim" runat="server" Width="150px" Font-Names="arial"
                                            Font-Size="9pt" Font-Bold="true"></asp:Label>
                                    </td>

                                    <td>

                                        <asp:Label ID="lblCapienza" runat="server" Width="280px" Font-Names="arial"
                                            Font-Size="9pt" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp</td>
                </tr>
                <tr>
                    <asp:Panel ID="panelVerificaRedd" runat="server">
                        <td style="border: 1px solid #0066FF; vertical-align: top;">

                            <table width="100%" style="font-family: Arial; font-size: 9pt;" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td colspan="4" align="center" style="background-color: #FFFFB3; font-family: Arial; font-size: 9pt;">
                                        <b>DATI VERIFICA REDDITI</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Paramentro di confronto (b - c/ 24)
                                    </td>
                                    <td>Reddito netto mensile
                                    </td>
                                    <td>20% del redd. netto mensile
                                    </td>

                                    <td>Verifica redditi
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblParamConfronto" runat="server" Width="150px" Font-Names="arial" Font-Size="9pt" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReddMensile" runat="server" Width="150px" Font-Names="arial" Font-Size="9pt" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRedd20" runat="server" Width="150px" Font-Names="arial"
                                            Font-Size="9pt" Font-Bold="true"></asp:Label>
                                    </td>

                                    <td>

                                        <asp:Label ID="lblVerificaRedd" runat="server" Width="300px" Font-Names="arial"
                                            Font-Size="9pt" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </asp:Panel>

                </tr>
            </table>
        </td>
    </tr>
</table>
