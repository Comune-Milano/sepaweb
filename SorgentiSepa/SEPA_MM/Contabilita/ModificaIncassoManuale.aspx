﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModificaIncassoManuale.aspx.vb"
    Inherits="ModificaIncassoManuale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modifica Incasso Manuale</title>
    <link href="../FUNCTION_FORMAT/format.css" rel="stylesheet" type="text/css" />
    <script src="../FUNCTION_FORMAT/functionJS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        window.name = "modal";
    </script>
</head>
<body onload="controlloBrowser();">
    <br />
    <form id="form1" runat="server" target="modal" style="position: relative; left: 7px;
    width: 500px; height: 400px; font-family:Arial; font-size:9pt;">
    <div id="titolo">
        Nuovo incasso manuale
    </div>
    <div>
        <table>
            <tr>
                <td style="width: 15%">
                    Nominativo
                </td>
                <td style="width: 85%">
                    <asp:TextBox ID="TextBoxNominativo" runat="server" Width="300px" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td>
                    Importo incassato
                </td>
                <td>
                    <asp:TextBox ID="TextBoxImportoIncassato" runat="server" class="numero" Width="100px"
                        MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Tipologia pagamento
                </td>
                <td>
                    <asp:DropDownList ID="cmbTipoPagamento" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Causale
                </td>
                <td>
                    <asp:TextBox ID="TextBoxCausale" runat="server" Width="300px" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td>
                    Data incasso
                </td>
                <td>
                    <asp:TextBox ID="TextBoxDataIncasso" runat="server" class="data" MaxLength="10" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorDataIncasso" runat="server"
                        ErrorMessage="!" ControlToValidate="TextBoxDataIncasso" Font-Bold="True" Font-Names="Arial"
                        Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Note
                </td>
                <td>
                    <asp:TextBox ID="TextBoxNote" runat="server" Rows="6" TextMode="MultiLine" Width="300px"
                        MaxLength="200"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <br /><br />
    <div id="bottoni">
        <table>
            <tr>
                <td style="width: 80%">
                    &nbsp;
                </td>
                <td style="width: 10%">
                    <asp:ImageButton ID="ImageButtonProcedi" runat="server" AlternateText="Procedi" ImageUrl="../NuoveImm/img_Modifica2.png"
                        ToolTip="Procedi" />
                </td>
                <td style="width: 10%">
                    <asp:ImageButton ID="ImageButtonEsci" runat="server" AlternateText="Torna alla Home"
                        ImageUrl="../NuoveImm/Img_EsciCorto.png" ToolTip="Torna alla Home" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
