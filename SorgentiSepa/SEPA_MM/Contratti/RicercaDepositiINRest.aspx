<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDepositiINRest.aspx.vb" Inherits="Contratti_RicercaDepositiINRest" %>

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
            width: 18px;
        }
  
        .style4
        {
            left: 150px;
            width: 170px;
            padding: 1px 1px 1px 20px;
            height: 22px;
        }
        .style5
        {
            width: 242px;
            height: 22px;
        }
        .style6
        {
            width: 107px;
            height: 22px;
        }
        .style7
        {
            height: 22px;
        }
  
        .style8
        {
            width: 107px;
        }
        .style9
        {
            width: 234px;
        }
  
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; background-repeat: no-repeat;top:0px;">
        <table width="100%">
            <tr>
                <td style="padding-left: 10px;">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Ricerca 
                    Depositi Cauzionali IN Restituzione</strong></span>
                </td>
            </tr>
            <tr>
                <td height="10px">
             
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="1" cellspacing="1">
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    &nbsp;</td>
                <td class="style9" >
                    &nbsp; &nbsp;</td>
                <td style="padding-left: 10px" class="style3">
                    &nbsp; &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label5" runat="server" Text="Codice Rapporto" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td class="style9" >
                    <asp:TextBox ID="txtCodContr" runat="server" TabIndex="1" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px"></asp:TextBox>
                </td>
                <td style="padding-left: 10px" class="style3">
                    &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
            </tr>
            <tr valign="middle">
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label16" runat="server" Text="Data Attivazione funzione dal" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td valign="middle" class="style9">
                    <asp:TextBox ID="txtDataEventoDAL" runat="server" Width="80px" TabIndex="2" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataEventoDAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                &nbsp;&nbsp;
                </td>
                <td style="padding-left: 10px" class="style3">
                    <asp:Label ID="Label17" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td class="style8">
                    <asp:TextBox ID="txtDataEventoAL" runat="server" Width="80px" TabIndex="3" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataEventoAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr valign="middle">
                <td align="left" class="style4">
                    <asp:Label ID="Label18" runat="server" Text="Modalità di pagamento" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td valign="middle" class="style5" colspan="2">
                    <asp:CheckBoxList ID="chTipoPagamento" runat="server" Font-Names="arial" 
                        Font-Size="9pt">
                    </asp:CheckBoxList>
                </td>
                <td style="padding-left: 10px" class="style6">
                </td>
                <td class="style7">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label3" runat="server" Text="Struttura" Font-Names="Arial" 
                        Font-Size="8pt"></asp:Label>
                </td>
                <td class="style2" colspan="2">
                    <asp:DropDownList ID="cmbStruttura" runat="server" TabIndex="5">
                    </asp:DropDownList>
                </td>
                <td align="left" style="padding-left: 10px" class="style8">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td align="left" style="padding-left: 10px" class="style3">
                    &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
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
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Names="Arial" 
                        Font-Size="8pt" ForeColor="#CC0000" Visible="False"></asp:Label>
        <br />
        <br />
        <table width="95%">
            <tr>
                <td align="right" height="50px">
                    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                        TabIndex="6" ToolTip="Avvia Ricerca" />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        TabIndex="7" ToolTip="Home" />
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



