<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnomalieRendicontazione.aspx.vb"
    Inherits="Contabilita_AnomalieRendicontazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca Log Operatori</title>
    <script type="text/javascript">
        //document.onkeydown=$onkeydown;
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
        }
    </script>
    <script type="text/javascript">
        var Uscita;
        Uscita = 0;

        function $onkeydown() {

            if (event.keyCode == 13) {
                alert('Usare il tasto <Avvia Ricerca>');
                history.go(0);
                event.keyCode = 0;
            }
        } 

    </script>
</head>
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 770px;">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="width: 1%;">
            </td>
            <td style="width: 99%">
                <asp:Label ID="Label1" runat="server" Text="Anomalie Rendicontazione" Font-Bold="True"
                    Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 1%;">
                &nbsp;</td>
            <td style="width: 99%">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 40%;">
                            <table width="500px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Data elaborazione dal</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtdatadal" runat="server" MaxLength="10" ToolTip="GG/MM/YYYY"
                                            Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtdatadal"
                                            ErrorMessage="Errata" Font-Bold="True" Height="19px" S ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Font-Size="10pt" Font-Names="arial" Display="Dynamic" Width="116px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Data elaborazione al</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtdataal" runat="server" MaxLength="10" ToolTip="GG/MM/YYYY"
                                            Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                            ControlToValidate="txtdataal" ErrorMessage="Errata" Font-Bold="True" ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Font-Size="10pt" Font-Names="arial" Width="116px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Font-Size="10pt" Font-Names="Arial" 
                                            Font-Bold="False">Data valuta dal</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtValutaDal" runat="server" MaxLength="10" ToolTip="GG/MM/YYYY"
                                            Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtValutaDal"
                                            ErrorMessage="Errata" Font-Bold="True" Height="19px" S ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Font-Size="10pt" Font-Names="arial" Display="Dynamic" Width="116px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Font-Size="10pt" Font-Names="Arial" 
                                            Font-Bold="False">Data valuta al</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtValutaAl" runat="server" MaxLength="10" ToolTip="GG/MM/YYYY"
                                            Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                            ControlToValidate="txtValutaAl" ErrorMessage="Errata" Font-Bold="True" ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Font-Size="10pt" Font-Names="arial" Width="116px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False"
                                            Font-Strikeout="False">Nome File</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfile" runat="server" Width="195px" Font-Names="Arial"
                                            Font-Size="10pt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">ID Bolletta</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbolletta" runat="server" MaxLength="10" Font-Names="Arial"
                                            Font-Size="10pt"></asp:TextBox>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtbolletta"
                                            ErrorMessage="Inserire caratteri numerici !" ValidationExpression="^\d+$" Font-Bold="True"
                                            Font-Names="Arial" Font-Size="10pt" Width="170px">Inserire caratteri numerici!</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Font-Size="10pt" Font-Names="Arial" 
                                            Font-Bold="False">Num. Conto</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNumConto" runat="server" Font-Names="Arial"
                                            Font-Size="10pt"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px;">
                            &nbsp;
                        </td>
                        <td style="height: 30px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 20%">
                                        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="../NuoveImm/Img_AvviaRicerca.png"
                                            TabIndex="8" ToolTip="Avvia Ricerca" />
                                    </td>
                                    <td style="width: 20%">
                                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                            TabIndex="9" ToolTip="Home" />
                                    </td>
                                    <td style="width: 10%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
