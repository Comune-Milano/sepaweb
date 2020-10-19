<%@ Page Title="" Language="VB" MasterPageFile="BasePage.master" AutoEventWireup="false"
    CodeFile="Ricerca.aspx.vb" Inherits="SIRAPER_Ricerca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/jscript" language="javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
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
                <strong>RICERCA ELABORAZIONE</strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="12">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                Sigla Ente Proprietario
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtSiglaEnte" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                    MaxLength="8" Width="125px"></asp:TextBox>
            </td>
            <td style="width: 15px">
                &nbsp;
            </td>
            <td>
                Tipo Ente Proprietario
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoEnte" runat="server" Font-Names="Arial" Font-Size="8">
                </asp:DropDownList>
            </td>
            <td style="width: 15px">
                &nbsp;
            </td>
            <td colspan="3">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            Data Riferimento Da
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDataRiferimentoDa" runat="server" CssClass="CssMaiuscolo" EnableViewState="False"
                                            Font-Names="Arial" Font-Size="8pt" MaxLength="10" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataRiferimentoDa"
                                            ErrorMessage="!" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Font-Names="arial" Font-Size="8pt" ForeColor="#CC0000" ToolTip="Modificare la data di Fusione"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Data Riferimento A
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDataRiferimentoA" runat="server" CssClass="CssMaiuscolo" EnableViewState="False"
                                            Font-Names="Arial" Font-Size="8pt" MaxLength="10" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataRiferimentoA"
                                            ErrorMessage="!" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Font-Names="arial" Font-Size="8pt" ForeColor="#CC0000" ToolTip="Modificare la data di Fusione"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                Codice Fiscale Ente
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtCodFiscale" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                    MaxLength="16" Width="150px"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                Partita Iva Ente
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtPartitaIva" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                    MaxLength="11" Width="150px"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                Ragione Sociale Ente
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtRagioneSociale" runat="server" Font-Names="Arial" Font-Size="8"
                    CssClass="CssMaiuscolo" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 250px">
            <td colspan="12">
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td align="right">
                <table>
                    <tr>
                        <td>
                            <center>
                                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="Immagini/logout.png" ToolTip="Torna alla Home"
                                    Width="32px" CausesValidation="False" OnClientClick="caricamentoincorso();" /></center>
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td style="text-align: center;">
                            <asp:ImageButton ID="BtnCerca" runat="server" ImageUrl="Immagini/Search_big.png"
                                ToolTip="Avvia Ricerca" OnClientClick="caricamentoincorso();" />
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <strong>TORNA ALLA HOME</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            <strong>AVVIA RICERCA</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        initialize();
        function initialize() {
            $("#MasterPage_MainContent_txtDataRiferimentoDa").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
            $("#MasterPage_MainContent_txtDataRiferimentoA").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        };
        function EnterInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {
                document.getElementById("MasterPage_MainContent_BtnCerca").click();
                return false;
            };
        };
        function $keyPress() {
            if (event.keyCode == 13) {
                document.getElementById("MasterPage_MainContent_BtnCerca").click();
                return false;
            };
        };
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $keyPress;
        }
        else {
            window.document.addEventListener("keydown", EnterInvio, true);
        };
    </script>
</asp:Content>
