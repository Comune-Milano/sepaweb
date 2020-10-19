<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RegistrazioneTelematicaNEW.aspx.vb"
    Inherits="Contratti_RegistrazioneTelematicaNEW" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

//        if (event.keyCode == 13) {
//            alert('Usare il tasto <Avvia Ricerca>');
//            history.go(0);
//            event.keyCode = 0;
//        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrazione Telematica</title>
</head>
<body>
    <script type="text/javascript">
        document.onkeydown = $onkeydown;


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
    <form id="Form1" method="post" runat="server">
    <div style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat;
        height: 540px; position: absolute; top: 0px; left: 0px;">
        <table width="800px">
            <tr>
                <td colspan="4">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Registrazione
                        Telematica - Prima Stipula</strong></span>
                </td>
            </tr>
            <tr>
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
                <td style="padding-left: 15px">
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Cod. contratto</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodContratto" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="arial" Font-Size="10pt" TabIndex="1" MaxLength="19" Style="text-transform: uppercase;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 15px">
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Stipula Dal</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStipulaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="arial" Font-Size="10pt" TabIndex="1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStipulaDal"
                        Display="Dynamic" ErrorMessage="??" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Al</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStipulaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="arial" Font-Size="10pt" TabIndex="1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStipulaAl"
                        Display="Dynamic" ErrorMessage="??" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 15px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Data di invio</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataInvio" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="arial" Font-Size="10pt" TabIndex="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataInvio"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataInvio"
                        Display="Dynamic" ErrorMessage="??" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt">Attenzione...La data di invio sarà usata per calcolare eventuali sanzioni!</asp:Label>
                </td>
            </tr>
            <tr>
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
                    &nbsp
                </td>
                <td>
                    &nbsp
                </td>
                <td>
                    &nbsp
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        TabIndex="4" ToolTip="Procedi" OnClientClick="ConfermaProcedi();" />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        TabIndex="5" ToolTip="Chiudi" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="dataOggi" runat="server" Value="" />
    <asp:HiddenField ID="confermaProcedi" runat="server" Value="0" />
    </form>
</body>
<script type="text/javascript">
        function ConfermaProcedi() {

            var data1 = document.getElementById('txtStipulaDal').value;
            var data2 = document.getElementById('txtStipulaAl').value;
            var data3 = document.getElementById('txtDataInvio').value;
            var data4 = document.getElementById('dataOggi').value;
            var chiediConferma;
            var errore1;
            var errore2;

            data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
            data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
            data3str = data3.substr(6) + data3.substr(3, 2) + data3.substr(0, 2);
            data4str = data4.substr(6) + data4.substr(3, 2) + data4.substr(0, 2);

            //if (data2str - data1str < 0) {
            //    alert('Errore intervallo date stipula!');
            //    errore1 = '1';
            //}
            //else {
            //    errore1 = '0';
            //}
            if (data3str - data1str < 0) {
                alert('Errore data invio!');
                errore2 = '1';
            }
            else {
                errore2 = '0';
            }
            if (data3str - data4str < 0) {
                alert('Errore data invio!');
                errore2 = '1';
            }
            else {
                errore2 = '0';
            }

            //if (errore1 == '0' && errore2 == '0') {
            //    chiediConferma = window.confirm("Attenzione...procedendo verrà creato un file XML valido per la registrazione telematica dei contratti. Continuare?");
            //    if (chiediConferma == false) {
            //        document.getElementById('confermaProcedi').value = '0';
            //    }
            //    else {
            //        document.getElementById('confermaProcedi').value = '1';
            //    }
            //}
        };

       
</script>
</html>
