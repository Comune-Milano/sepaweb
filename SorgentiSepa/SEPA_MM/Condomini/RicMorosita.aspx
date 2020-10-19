<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicMorosita.aspx.vb" Inherits="Condomini_RicMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RicercaMorosita</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 8pt;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
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

        function gestisciCheckBox() {
            if (document.getElementById('chkInquilino').checked) {
                document.getElementById('checkbox').style.visibility = 'visible';
            }
            else {
                document.getElementById('checkbox').style.visibility = 'hidden';
                document.getElementById('chkContrSolid').checked = false;
                document.getElementById('chkInfoMav').checked = false;
                document.getElementById('chkFonSocial').checked = false;
            }
        }


    </script>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg)">
    <form id="form1" runat="server">
    <table style="width: 78%;">
        <tr>
            <td>
                <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
                    ForeColor="#660000" TabIndex="9" Text="Ricerca Morosità" Width="758px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                Elenco Condomini
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="cmbCondominio" runat="server" Style="width: 90%;" Font-Names="Arial"
                    Font-Size="9pt" TabIndex="4">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="style1" width="150px">
                            Periodo Riferimento
                        </td>
                        <td class="style1" width="30px">
                            Dal
                        </td>
                        <td>
                            <asp:TextBox ID="txtRifDa" runat="server" Font-Names="Arial" Font-Size="8pt" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRifDa"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                    left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td class="style1" width="60px" align="center">
                            al
                        </td>
                        <td>
                            <asp:TextBox ID="txtRifAl" runat="server" Font-Names="Arial" Font-Size="8pt" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtRifAl"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                    left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="style1">
                            Informazioni Inquilino
                            <asp:CheckBox ID="chkInquilino" runat="server" onclick="gestisciCheckBox()" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style1">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div id="checkbox" style="visibility:hidden;">
                <table>
                    <tr>
                        <td class="style1" width="180px">
                            Contributo Solidarietà
                            <asp:CheckBox ID="chkContrSolid" runat="server" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style1" width="180px">
                            Informazioni MAV
                            <asp:CheckBox ID="chkInfoMav" runat="server" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style1">
                            Contributo Solidarietà - Fondo Sociale
                            <asp:CheckBox ID="chkFonSocial" runat="server" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                </div>
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
        <tr>
            <td align="right">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="Immagini/Img_AvviaRicerca.png"
                                TabIndex="1" ToolTip="Avvia Ricerca"/>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Image ID="Home" runat="server" onclick="parent.main.location.replace('pagina_home.aspx');"
                                Style="cursor: pointer" ImageUrl="Immagini/Img_Home.png" />
                        </td>
                        <td>
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
