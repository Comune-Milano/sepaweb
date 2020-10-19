<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovaTipologiaAnno.aspx.vb" Inherits="Contratti_Pagamenti_NuovaTipologiaAnno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Getione Anni Competenza Accertamento</title>
    <script type="text/javascript">
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbersOnlyPositive': /[^\d\,]/g, //Modifica Marco 20/09/2012
            'notnumbers': /[^\d\,-]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            return true;
        }
        function CompletaData(e, obj) {
            var sKeyPressed;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    if (window.event) {
                        event.keyCode = 0;
                    } else {
                        e.preventDefault();
                    }
                }
            } else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                } else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        if (window.event) {
                            event.keyCode = 0;
                        } else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }
    </script>
</head>
<body style="background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);">
    <form id="form1" runat="server">
    <div>
        <div style="height: 10px;">
        </div>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="Gestione anno competenza aggiornamento" runat="server" Font-Bold="True"
                        Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon" />
                </td>
            </tr>
        </table>
        <div style="height: 15px;">
        </div>
        <div style="height: 450px; vertical-align: top">
            <table border="0" cellpadding="3" cellspacing="3" width="100%">
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label2" Text="Validita da" runat="server" Font-Names="Arial"
                            Font-Size="10pt" />
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="TextBoxValiditaDa" runat="server" Width="70px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label3" Text="Validita a" runat="server" Font-Names="Arial"
                            Font-Size="10pt" />
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="TextBoxValiditaA" runat="server" Width="70px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label4" Text="Anno" runat="server" Font-Names="Arial"
                            Font-Size="10pt" />
                    </td>
                    <td style="width: 80%">
                        <asp:DropDownList ID="DropDownListAnno" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 80%">
                </td>
                <td style="width: 10%">
                <asp:ImageButton ID="ImageButtonNuova" runat="server" ToolTip="Aggiungi"
                        ImageUrl="../../NuoveImm/NewAggiungi.png" />
                   <asp:ImageButton ID="ImageButtonModifica" runat="server" ToolTip="Modifica"
                        ImageUrl="../../NuoveImm/NewModifica.png" />
                </td>
                <td style="width: 10%">
                    <asp:ImageButton ID="ImageButtonEsci" runat="server" ToolTip="Esci"
                        ImageUrl="../../NuoveImm/newEsci.png" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
