<%@ Page Title="" Language="VB" MasterPageFile="BasePage.master" AutoEventWireup="false"
    CodeFile="Gestione.aspx.vb" Inherits="SIRAPER_Gestione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <strong>GESTIONE PARAMETRI</strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td>
                <table>
                    <tr style="height: 35px; vertical-align: middle;">
                        <td>
                            <asp:Button ID="btnSalva" runat="server" CssClass="bottone" Text="Salva" ToolTip="Salva"
                                OnClientClick="caricamentoincorso();" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" ToolTip="Esci"
                                OnClientClick="caricamentoincorso();" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                Sigla Ente Proprietario*
            </td>
            <td style="width: 5px;">
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtSiglaEnte" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                    MaxLength="8" Width="125px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                Tipo Ente Proprietario*
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoEnte" runat="server" Font-Names="Arial" Font-Size="8">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                Tipo Amministrazione*
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoAmministrazione" runat="server" Font-Names="Arial" Font-Size="8">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                Codice Istat*
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtCodiceIstat" runat="server" Font-Names="Arial" Font-Size="8"
                    CssClass="CssMaiuscolo" MaxLength="6" Width="125px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                Ragione Sociale*</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtRagioneSociale" runat="server" Font-Names="Arial" Font-Size="8"
                    CssClass="CssMaiuscolo" MaxLength="6" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                Codice Fiscale</td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtCodFiscale" runat="server" Font-Names="Arial" Font-Size="8"
                    CssClass="CssMaiuscolo" MaxLength="16" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                Partita Iva Ente*</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtPiva" runat="server" Font-Names="Arial" Font-Size="8"
                    CssClass="CssMaiuscolo" MaxLength="16" Width="150px"></asp:TextBox>
            </td>
        </tr>
        </table>
</asp:Content>
