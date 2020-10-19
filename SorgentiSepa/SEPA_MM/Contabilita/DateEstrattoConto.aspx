<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DateEstrattoConto.aspx.vb"
    Inherits="Contabilita_EstrattoConto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estratto Conto</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12pt;
            width: 54px;
        }
        .style5
        {
            width: 135px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; width: 100%;">
            <tr>
                <td>
                    <div style="height: 32px">
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                            Date per elaborazione Estratto Conto
                            <asp:Label ID="lblTipoEstratto" runat="server"></asp:Label>
                        </span></strong>
                    </div>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ul style="color: blue; margin-left: 15px; margin-bottom: 0px; padding: 0px;">
                                    <li><strong><span style="color: blue; font-family: Arial; text-align: center; font-size: 10pt;
                                        background-color: #E8FFFF;" class="style2">Emissione delle bollette da estrarre</span></strong>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 35px;">
                                <table>
                                    <tr>
                                        <td style="vertical-align: top">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt">Dal</asp:Label>
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:TextBox ID="TxtDal" runat="server" Font-Names="Arial" Font-Size="10pt" Width="80px"
                                                ToolTip="Formato data dd/mm/yyyy" BackColor="#E8FFFF" TabIndex="1"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtDal" ErrorMessage="!"
                                                    Font-Bold="True" Height="16px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="15px" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="style1" style="vertical-align: top; text-align: right;">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt">Al</asp:Label>
                                                &nbsp;</strong>
                                        </td>
                                        <td class="style10" style="vertical-align: top">
                                            <strong>
                                                <asp:TextBox ID="TxtAl" runat="server" Font-Names="Arial" Font-Size="10pt" Width="80px"
                                                    TabIndex="2" ToolTip="Formato data dd/mm/yyyy" BackColor="#E8FFFF"></asp:TextBox>&nbsp;
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtAl"
                                                    ErrorMessage="!" Font-Bold="True" Height="16px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="16px" ForeColor="Red"></asp:RegularExpressionValidator></strong>
                                        </td>
                                        <td>
                                            <%--<asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Style="z-index: 10;" Text="N.B.: Se la data finale di emissione è vuota il sistema considererà la data odierna!"></asp:Label>
                                            --%>&nbsp
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul style="color: blue; margin-left: 15px; margin-bottom: 0px; padding: 0px;">
                                    <li><strong><span style="color: blue; font-family: Arial; text-align: center; font-size: 10pt;
                                        background-color: #FFFFCC;">Competenza delle bollette da estrarre</span></strong>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 35px;">
                                <table>
                                    <tr>
                                        <td style="vertical-align: top">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt">Dal</asp:Label>
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:TextBox ID="TxtRifDal" runat="server" Font-Names="Arial" Font-Size="10pt" Width="80px"
                                                TabIndex="3" ToolTip="Formato data dd/mm/yyyy" BackColor="#FFFFCC"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtRifDal"
                                                ErrorMessage="!" Font-Bold="True" Height="16px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                Width="15px" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="style1" style="vertical-align: top; text-align: right;">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt">Al</asp:Label>
                                                &nbsp;</strong>
                                        </td>
                                        <td class="style10" style="vertical-align: top">
                                            <strong>
                                                <asp:TextBox ID="TxtRifAl" runat="server" Font-Names="Arial" 
                                                Font-Size="10pt" Width="80px"
                                                    TabIndex="4" ToolTip="Formato data dd/mm/yyyy" BackColor="#FFFFCC"></asp:TextBox>&nbsp;
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtRifAl"
                                                    ErrorMessage="!" Font-Bold="True" Height="16px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="16px" ForeColor="Red"></asp:RegularExpressionValidator></strong>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul style="color: blue; margin-left: 15px; margin-bottom: 0px; padding: 0px;">
                                    <li><strong><span style="color: blue; font-family: Arial; text-align: center; font-size: 10pt;
                                        background-color: #DDFFCC;">Incasso delle bollette da estrarre</span></strong>
                                        <asp:Label ID="lblNota" runat="server" Font-Names="Arial" Font-Size="8pt" Font-Bold="False"
                                            Font-Italic="True" ForeColor="Black">(valido solo per Eccedenze Scarti MAV)</asp:Label>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 35px;">
                                <table id="txtDateIncasso">
                                    <tr>
                                        <td>
                                            &nbsp
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="False"
                                                Font-Italic="True" ForeColor="Black">- Data Pagamento:</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt">Dal</asp:Label>
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:TextBox ID="txtIncDal" runat="server" Font-Names="Arial" Font-Size="10pt" Width="80px"
                                                TabIndex="5" ToolTip="Formato data dd/mm/yyyy" BackColor="#DDFFCC"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtIncDal"
                                                ErrorMessage="!" Font-Bold="True" Height="16px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                Width="15px" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="style1" style="vertical-align: top; text-align: right;">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt">Al</asp:Label>
                                                &nbsp;</strong>
                                        </td>
                                        <td class="style10" style="vertical-align: top">
                                            <strong>
                                                <asp:TextBox ID="txtIncAl" runat="server" Font-Names="Arial" 
                                                Font-Size="10pt" Width="80px"
                                                    TabIndex="6" ToolTip="Formato data dd/mm/yyyy" BackColor="#DDFFCC"></asp:TextBox>&nbsp;
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtIncAl"
                                                    ErrorMessage="!" Font-Bold="True" Height="16px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="16px" ForeColor="Red"></asp:RegularExpressionValidator></strong>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="False"
                                                Font-Italic="True" ForeColor="Black">- Data Valuta:</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt">Dal</asp:Label>
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:TextBox ID="txtValutaDal" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                Width="80px" TabIndex="7" ToolTip="Formato data dd/mm/yyyy" 
                                                BackColor="#DDFFCC"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtValutaDal"
                                                ErrorMessage="!" Font-Bold="True" Height="16px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                Width="15px" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="style1" style="vertical-align: top; text-align: right;">
                                            <strong>
                                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt">Al</asp:Label>
                                                &nbsp;</strong>
                                        </td>
                                        <td class="style10" style="vertical-align: top">
                                            <strong>
                                                <asp:TextBox ID="txtValutaAl" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                    Width="80px" TabIndex="8" ToolTip="Formato data dd/mm/yyyy" 
                                                BackColor="#DDFFCC"></asp:TextBox>&nbsp;
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtValutaAl"
                                                    ErrorMessage="!" Font-Bold="True" Height="16px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="16px" ForeColor="Red"></asp:RegularExpressionValidator></strong>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ChStornate" runat="server" Font-Bold="True" 
                                    Font-Names="arial" Font-Size="10pt" ForeColor="#0000CC" 
                                    Text="Escludi Bollette Stornate" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td width="150px">
                                            &nbsp
                                        </td>
                                        <td class="style5">
                                            <strong>
                                                <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                                                    ToolTip="Visualizza Estratto Conto" Style="vertical-align: bottom; height: 20px;"
                                                    TabIndex="9" OnClientClick="ConfermaVisualizza()" />
                                            </strong>
                                        </td>
                                        <td>
                                            <strong>
                                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_Grande.png"
                                                    ToolTip="Chiude la finestra" Style="vertical-align: bottom" 
                                                TabIndex="10" />
                                            </strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="tipologia" style="visibility: visible;">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 45%;">
                        <ul style="color: blue; margin-left: 22px; margin-bottom: 0px; padding: 0px;">
                            <li>
                                <asp:Label ID="lbltipologia" runat="server" Text="Tipologia Documento" Font-Size="10pt"
                                    Font-Names="Arial" ForeColor="Blue" Visible="False" Font-Bold="True" BackColor="#FFDFFF"></asp:Label>
                            </li>
                        </ul>
                    </td>
                    <td>
                        &nbsp
                    </td>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 35px; padding-top: 6px;">
                        <div style="border: 2px solid #FFDFFF; width: 100%; overflow: auto;">
                            <asp:CheckBoxList ID="cblTipologia" Font-Names="Arial" Font-Size="8pt" runat="server"
                                CellPadding="2" CellSpacing="2" RepeatColumns="3" RepeatDirection="Horizontal"
                                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Width="100%" 
                                Font-Bold="False" TabIndex="8">
                            </asp:CheckBoxList>
                        </div>
                    </td>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <center>
                            <asp:Button ID="btntipologia" runat="server" Text="Seleziona/Deseleziona" Font-Names="Arial"
                                Font-Size="8" Visible="False" OnClientClick="Seleziona_deseleziona()" /></center>
                    </td>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <br />
                        <asp:ImageButton ID="btnVisualizza2" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                            ToolTip="Visualizza Estratto Conto" Style="vertical-align: bottom; height: 20px;"
                            TabIndex="9" OnClientClick="controllaCheck();" />
                        <asp:ImageButton ID="btnAnnulla2" runat="server" ImageUrl="~/NuoveImm/Img_Esci_Grande.png"
                            ToolTip="Chiude la finestra" Style="vertical-align: bottom" 
                            TabIndex="10" />
                    </td>
                    <td>
                        &nbsp
                    </td>
                </tr>
            </table>
        </div>
    </div>
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
    <br />
    &nbsp;<br />
    &nbsp;<br />
    <br />
    <br />
    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Style="z-index: 10; left: 13px; position: absolute; top: 474px;
        height: 13px; width: 719px;" Text="Label" Visible="False"></asp:Label>
    <br />
    <br />
    <br />
    &nbsp;
    <br />
    <br />
    <br />
    <asp:HiddenField ID="tipoEstratto" runat="server" />
    <asp:HiddenField ID="conferma" runat="server" Value="0" />
    </form>
    <script type="text/javascript">

        if (document.getElementById('tipoEstratto').value == 'G') {
            document.getElementById('txtIncDal').disabled = true;
            document.getElementById('txtIncAl').disabled = true;
            document.getElementById('txtValutaDal').disabled = true;
            document.getElementById('txtValutaAl').disabled = true;
        }

        function ConfermaVisualizza() {
            if (document.getElementById('TxtAl').value == '') {
                var chiediConferma;
                chiediConferma = window.confirm('La data finale di emissione non è stata impostata, verrà considerata la data attuale. Continuare?');
                if (chiediConferma == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    document.getElementById('conferma').value = '0';
                }
            }
            else {

                document.getElementById('conferma').value = '1';
                return true;
            }
        }

        function controllaCheck() {
            var checkSelezionata = 0
            var i = 0;
            var modulo = document.getElementById('form1').elements;
            for (i = 0; i < modulo.length; i++) {
                if (modulo[i].type == "checkbox") {
                    if (modulo[i].checked == true) {
                        checkSelezionata = 1;
                    }
                }
            }
            if (checkSelezionata == 0) {
                alert('Selezionare almeno una tipologia di documento!');
                return false;
            }
            else {
                ConfermaVisualizza();
            }
        }

//        function controllaTipoCheck() {
//            var selezioneEcc = 0;
//            var i = 0;
//            var modulo = document.getElementById('form1').elements;
//            for (i = 0; i < modulo.length; i++) {
//                if (modulo[i].type == "checkbox") {
//                    if (modulo[i].checked == true && modulo[i].value == '4') {
//                        selezioneEcc = 1;
//                    }
//                }
//            }
//            if (selezioneEcc == 1) {
//                document.getElementById('txtIncDal').disabled = false;
//                document.getElementById('txtIncAl').disabled = false;
//                document.getElementById('txtValutaDal').disabled = false;
//                document.getElementById('txtValutaAl').disabled = false;
//            }
//            else {
//                document.getElementById('txtIncDal').disabled = true;
//                document.getElementById('txtIncAl').disabled = true;
//                document.getElementById('txtValutaDal').disabled = true;
//                document.getElementById('txtValutaAl').disabled = true;
//            }
//        }

        function controllaTipoCheck(selezioneEcc) {

            if (document.getElementById('txtIncDal').disabled == true) {
                document.getElementById('txtIncDal').disabled = false;
                document.getElementById('txtIncAl').disabled = false;
                document.getElementById('txtValutaDal').disabled = false;
                document.getElementById('txtValutaAl').disabled = false;
            }
            else {
                document.getElementById('txtIncDal').disabled = true;
                document.getElementById('txtIncAl').disabled = true;
                document.getElementById('txtValutaDal').disabled = true;
                document.getElementById('txtValutaAl').disabled = true;
            }

        }


        if (document.getElementById('tipoEstratto').value == "G") {
            document.getElementById('tipologia').style.visibility = 'visible';
        }
        else {
            document.getElementById('tipologia').style.visibility = 'hidden';
        }

        function Seleziona_deseleziona() {
            var i = 0;
            var modulo = document.getElementById('form1').elements;
            for (i = 0; i < modulo.length; i++) {
                if (modulo[i].type == "checkbox") {
                    if (modulo[i].checked == true) {
                        modulo[i].checked = false;
                    } else {
                        modulo[i].checked = true;
                    }
                }
            }
        }
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
