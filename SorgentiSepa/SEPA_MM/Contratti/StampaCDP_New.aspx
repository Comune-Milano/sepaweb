<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaCDP_New.aspx.vb" Inherits="Contratti_StampaCDP_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stampa CDP</title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Arial;
            font-size: medium;
            font-weight: bold;
        }
        .style2
        {
            width: 121px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function AutoDecimal(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(4)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
        function AutoDecimal2(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }

        function AskConfirm() {
            var Conferma
            if (document.getElementById('stampato').value == '0') {
                Conferma = window.confirm("Attenzione...Confermi di voler emettere un pagamento?");
                if (Conferma == false) {
                    document.getElementById('txtConferma').value = '0';
                }
                else {
                    document.getElementById('txtConferma').value = '1';
                    if (document.getElementById('btnPagamento')) {
                        document.getElementById('btnPagamento').style.visibility = 'hidden';
                        document.getElementById('btnPagamento').style.position = 'absolute';
                        document.getElementById('btnPagamento').style.left = '-100px';
                        document.getElementById('btnPagamento').style.display = 'none';
                    }
                }
            }
            else {
                document.getElementById('txtConferma').value = '1';
            }
        };
        function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        };
    </script>
    <div>
        <table style="width: 100%;">
            <tr bgcolor="Maroon">
                <td class="style1" style="text-align: center">
                    STAMPA CDP
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescrizione" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="10pt" Height="35px" Width="450px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Data Emissione"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDataEmissione" runat="server" Font-Names="arial" 
                                    Font-Size="8pt" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="8pt" Text="Data Scadenza"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDScadenza" runat="server" Width="83px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="8pt" Text="Iban del Fornitore"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbIbanFornitore" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="350px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style2">
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Text="Note"
                                    Width="168px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Height="68px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style2">
                                &nbsp;
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDescrizione"
                                    ErrorMessage="E' possibile inserire al massimo 100 caratteri in questo campo di testo"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ToolTip="E' possibile inserire al massimo 150 caratteri in questo campo di testo!"
                                    ValidationExpression="^[\s\S]{0,100}$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        ForeColor="#CC0000" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr style="text-align: right">
                <td>
                    <asp:HiddenField ID="txtConferma" runat="server" />
                    <asp:HiddenField ID="stampato" runat="server" />
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: right;">
                        <asp:ImageButton ID="btnPagamento" runat="server" ImageUrl="~/NuoveImm/Img_Conferma1.png"
                            ToolTip="Emissione del Pagamento" OnClientClick="AskConfirm();" 
                        style="height: 16px" />
                        &nbsp;
                        <img alt="Annulla" src="../NuoveImm/Img_AnnullaVal.png" onclick="self.close();" style="cursor: pointer;" /></span></strong>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
