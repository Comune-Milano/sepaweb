<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DateBlocco.aspx.vb" Inherits="Contratti_DateBlocco" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Impostazione date di blocco</title>
    <link href="css/StilePaginaContratti.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CompletaData(e, obj) {
            var sKeyPressed;
            var n;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if ((sKeyPressed < 48) || (sKeyPressed > 57)) {
                if ((sKeyPressed != 8) && (sKeyPressed != 0)) {
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    };
                };
            }
            else {
                if (obj.value.length == 0) {
                    if ((sKeyPressed < 48) || (sKeyPressed > 51)) {
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        };
                    };
                }
                else if (obj.value.length == 1) {
                    if (obj.value == 3) {
                        if (sKeyPressed < 48 || sKeyPressed > 49) {
                            if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            };
                        };
                    };
                }
                else if (obj.value.length == 2) {
                    if ((sKeyPressed < 48) || (sKeyPressed > 49)) {
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        };
                    }
                    else {
                        obj.value += "/";
                    };
                }
                else if (obj.value.length == 4) {
                    n = obj.value.substr(3, 1);
                    if (n == 1) {
                        if ((sKeyPressed < 48) || (sKeyPressed > 50)) {
                            if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            };
                        };
                    };
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        };
                    };
                };
            };
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="titolo">
        <asp:Label ID="TitoloPagina" runat="server" Text="Impostazione date di blocco"></asp:Label>
    </div>
    <div id="contenuto">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td>
                    Data di blocco per emissione
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="TextBoxDataEmissione" runat="server" Width="70px"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxDataEmissione"
                        ErrorMessage="Modificare la data di blocco per emissione" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ForeColor="#CC0000"
                        ToolTip="Modificare la data di blocco per emissione" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Data di blocco per pagamento
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="TextBoxDataPagamento" runat="server" Width="70px"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxDataPagamento"
                        ErrorMessage="Modificare la data di blocco per pagamento" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ForeColor="#CC0000"
                        ToolTip="Modificare la data di blocco per pagamento" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </div>
    <div id="footer" style="text-align: right">
        <asp:Button ID="ButtonApplicaModifiche" Text="Salva" runat="server" CssClass="bottone"
            ToolTip="Applica modifica" />
        <asp:Button ID="ButtonEsci" Text="Esci" runat="server" CssClass="bottone" />
    </div>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('divLoading') != null) { document.getElementById('divLoading').style.display = 'none'; };
    </script>
    </form>
</body>
</html>
