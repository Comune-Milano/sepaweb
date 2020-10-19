<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDepCauzionali.aspx.vb"
    Inherits="Contratti_RicercaDepCauzionali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca Restituzioni Dep. Cauzionali</title>
    <style type="text/css">
        .style1
        {
            left: 450px;
            width: 170px;
            padding: 1px 1px 1px 20px;
        }
        
        .style2
        {
            width: 242px;
        }
        .style3
        {
            width: 62px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; background-repeat: no-repeat; top: 0px;">
        <table width="100%">
            <tr>
                <td style="padding-left: 10px;">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Ricerca Depositi
                        Cauzionali</strong></span>
                </td>
            </tr>
            <tr>
                <td height="10px">
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="1" style="width: 96%">
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp; &nbsp;
                </td>
                <td style="padding-left: 10px" class="style3">
                    &nbsp; &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label5" runat="server" Text="Codice Rapporto" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtCodContr" runat="server" TabIndex="1" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Width="150px"></asp:TextBox>
                </td>
                <td style="padding-left: 10px" class="style3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label25" runat="server" Text="Cognome" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtCognome" runat="server" TabIndex="2" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Width="150px"></asp:TextBox>
                </td>
                <td style="padding-left: 10px" class="style3">
                    <asp:Label ID="Label26" runat="server" Text="Nome" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNome" runat="server" TabIndex="3" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label27" runat="server" Text="Ragione Sociale" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtRagioneSociale" runat="server" TabIndex="4" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Width="150px"></asp:TextBox>
                </td>
                <td style="padding-left: 10px" class="style3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr valign="middle">
                <td align="left" class="style1" style="left: 150px">
                    &nbsp;</td>
                <td valign="middle" class="style2">
                    &nbsp;&nbsp;&nbsp;
                </td>
                <td style="padding-left: 10px" class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr valign="middle">
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label28" runat="server" Text="Data Rimborso dal" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td valign="middle" class="style2">
                    <asp:TextBox ID="txtDataRimborso" runat="server" Width="80px" TabIndex="7" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                        ControlToValidate="txtDataRimborso" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                        Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
                <td style="padding-left: 10px" class="style3">
                    <asp:Label ID="Label29" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataRimborsoAL" runat="server" Width="80px" TabIndex="8" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                        ControlToValidate="txtDataRimborsoAL" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                        Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr valign="middle">
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label30" runat="server" Text="Data Restituzione dal" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td valign="middle" class="style2">
                    <asp:TextBox ID="txtDataRestituzione" runat="server" Width="80px" TabIndex="9" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                        ControlToValidate="txtDataRestituzione" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                        Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
                <td style="padding-left: 10px" class="style3">
                    <asp:Label ID="Label31" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataRestituzioneAL" runat="server" Width="80px" TabIndex="10"
                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server"
                        ControlToValidate="txtDataRestituzioneAL" Display="Dynamic" ErrorMessage="!!"
                        Font-Bold="True" Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1"
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr valign="middle">
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label18" runat="server" Text="Importo Deposito C. da" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td valign="middle" class="style2">
                    <asp:TextBox ID="txtImportoDa" runat="server" Width="80px" TabIndex="11" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                        ControlToValidate="txtImportoDa" ErrorMessage="decimali richiesti, sep. da ,"
                        Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ValidationExpression="^(-?)\b\d*,\d{2}\b"
                        Font-Overline="False" Font-Strikeout="False" ForeColor="#CC0000"></asp:RegularExpressionValidator>
                </td>
                <td style="padding-left: 10px" class="style3">
                    <asp:Label ID="Label19" runat="server" Text="A" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtImportoA" runat="server" Width="80px" TabIndex="12" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                        ControlToValidate="txtImportoA" ErrorMessage="decimali richiesti, sep. da ,"
                        Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ValidationExpression="^(-?)\b\d*,\d{2}\b"
                        Font-Overline="False" Font-Strikeout="False" ForeColor="#CC0000"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label3" runat="server" Text="Libro" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtLibro" runat="server" TabIndex="13" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px"></asp:TextBox>
                </td>
                <td align="left" style="padding-left: 10px" class="style3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label23" runat="server" Text="Bolla" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtBolla" runat="server" TabIndex="14" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px"></asp:TextBox>
                </td>
                <td align="left" style="padding-left: 10px" class="style3">
                    <asp:Label ID="Label32" runat="server" Text="Restituibile" Font-Names="Arial" 
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbRestituibile" runat="server" Height="20px" Style="border: 1px solid black; z-index: 118; left: 163px;"
                        TabIndex="14" Width="90px">
                        <asp:ListItem Selected="True" Value="-1">--</asp:ListItem>
                        <asp:ListItem Value="1">SI</asp:ListItem>
                        <asp:ListItem Value="0">NO</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td align="left" style="padding-left: 10px" class="style3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <%--            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label12" runat="server" Text="Tipologia Domanda" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbTipoDom" runat="server" Width="250px" TabIndex="13" Style="border: 1px solid black;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="lblMotivo" runat="server" Text="Motivo present.domanda" Font-Names="Arial"
                        Font-Size="8pt" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbMotivo" runat="server" Width="250px" TabIndex="13" Visible="False"
                        Style="border: 1px solid black;">
                    </asp:DropDownList>
                </td>
            </tr>
            --%>
        </table>
        <table  width="80%">
            <tr>
                <td align="left" width="5%">
                    <asp:Label ID="Label21" runat="server" Text="Provenienza" Font-Names="Arial" 
                        Font-Size="8pt" Width="150px"></asp:Label>
                </td>
                <td rowspan="3" width="95%">
                    <asp:CheckBoxList ID="ChProvenienza" runat="server" Font-Names="arial" Font-Size="8pt"
                        TabIndex="15" RepeatDirection="Horizontal" Width="127%" RepeatColumns="3">
                    </asp:CheckBoxList>
                </td>
                
            </tr>
        </table>
        <br />
        <br />
        <table width="95%">
            <tr>
                <td align="right" height="50px">
                    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                        TabIndex="30" ToolTip="Avvia Ricerca" />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        TabIndex="31" ToolTip="Home" />
                </td>
            </tr>
        </table>
    </div>
    </form>
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


    </script>
</body>
</html>
