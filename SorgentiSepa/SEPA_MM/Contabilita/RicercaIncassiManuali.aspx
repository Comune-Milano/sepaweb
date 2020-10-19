<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaIncassiManuali.aspx.vb"
    Inherits="Contabilita_RicercaIncassiManuali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Incassi Manuali</title>
    <link href="../FUNCTION_FORMAT/format.css" rel="stylesheet" type="text/css" />
    <script src="../FUNCTION_FORMAT/functionJS.js" type="text/javascript"></script>
</head>
<body onload="controlloBrowser();">
    <form id="form1" runat="server">
    <div id="titolo">
        Ricerca incassi manuali
    </div>
    <div id="contenuto">
        <table>
            <tr>
                <td style="width: 25%">
                    Nominativo
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBoxNominativo" runat="server" Width="100%" 
                        MaxLength="100" />
                </td>
                <td>
                &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    Importo incassato da
                </td>
                <td style="width: 15%">
                    <asp:TextBox ID="TextBoxImportoIncassatoDa" runat="server" class="numero" 
                        MaxLength="50" />
                </td>
                <td style="width: 10%; text-align: center;">
                    fino a
                </td>
                <td style="width: 15%">
                    <asp:TextBox ID="TextBoxImportoIncassatoA" runat="server" class="numero" 
                        Width="100%" MaxLength="50" />
                </td>
                <td style="width: 35%" colspan="3">
                    &nbsp;
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
                <td colspan="3">
                    <asp:TextBox ID="TextBoxCausale" runat="server" Width="100%" MaxLength="100" />
                </td>
                <td>
                &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Data incasso dal
                </td>
                <td style="width: 15%">
                    <asp:TextBox ID="TextBoxDataIncassoDa" runat="server" class="data" 
                        MaxLength="10" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorDataIncasso" runat="server"
                        ErrorMessage="!" ControlToValidate="TextBoxDataIncassoDa" Font-Bold="True" Font-Names="Arial"
                        Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
                <td style="width: 10%; text-align: center;">
                    fino al
                </td>
                <td style="width: 15%;">
                    <asp:TextBox ID="TextBoxDataIncassoA" runat="server" class="data" 
                        MaxLength="10" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="!"
                        ControlToValidate="TextBoxDataIncassoA" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
                <td style="width: 35%">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="bottoni">
        <table>
            <tr>
                <td style="width: 80%">
                    &nbsp;
                </td>
                <td style="width: 10%">
                    <asp:ImageButton ID="ImageButtonRicerca" runat="server" AlternateText="Ricerca" ImageUrl="../NuoveImm/Img_AvviaRicerca.png"
                        ToolTip="Ricerca" />
                </td>
                <td style="width: 10%">
                    <asp:ImageButton ID="ImageButtonEsci" runat="server" AlternateText="Torna alla Home"
                        ImageUrl="../NuoveImm/Img_Home.png" ToolTip="Torna alla Home" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        if (document.getElementById('divLoading') != null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
        }
    </script>
</body>
</html>
