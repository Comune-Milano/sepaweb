<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaEventi.aspx.vb" Inherits="ARCHIVIO_RicercaEventi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        if (event.keyCode == 13) {
            alert('Usare il tasto <Avvia Ricerca>');
            history.go(0);
            event.keyCode = 0;
        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Ricerca Contratti</title>
</head>
<body bgcolor="#f2f5f1">
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
    <form id="Form1" method="post" runat="server" defaultbutton="btnCerca" defaultfocus="txtCognome">
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                    Eventi</strong></span><br />
                <br />
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            &nbsp;</td>
                                        <td style="width: 40%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 40%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Operatore</asp:Label>
                                        </td>
                                        <td>
                            <asp:DropDownList ID="cmbOperatore" TabIndex="5" runat="server" Width="250px" 
                                                Style="border-right: black 1px solid;
                                border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" 
                                                Font-Names="Arial" Font-Size="10pt">
                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                            <asp:Label ID="Label9" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Operazione</asp:Label>
                                        </td>
                                        <td>
                            <asp:DropDownList ID="cmbOperazione" TabIndex="5" runat="server" Width="250px" 
                                                Style="border-right: black 1px solid;
                                border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" 
                                                Font-Names="Arial" Font-Size="10pt">
                                <asp:ListItem>TUTTI</asp:ListItem>
                                <asp:ListItem Value="F55">INSERIMENTO</asp:ListItem>
                                <asp:ListItem Value="F02">MODIFICA</asp:ListItem>
                                <asp:ListItem Value="F56">CANCELLAZIONE</asp:ListItem>
                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                            <asp:Label ID="Label7" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Dal</asp:Label>
                                        </td>
                                        <td>
                            <asp:TextBox ID="txtDataDal0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                MaxLength="10" Width="83px" TabIndex="6" ToolTip="GG/MM/AAAA" Font-Names="Arial" 
                                                Font-Size="10pt"></asp:TextBox>
                            &nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataDal0"
                                ErrorMessage="!" Font-Bold="True" 
                                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                                                Font-Names="Arial" Font-Size="10pt"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                            <asp:Label ID="Label8" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Al</asp:Label>
                                        </td>
                                        <td>
                            <asp:TextBox ID="txtDataAl0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                MaxLength="10" Width="83px" TabIndex="7" ToolTip="GG/MM/AAAA" Font-Names="Arial" 
                                                Font-Size="10pt"></asp:TextBox>
                            &nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataAl0"
                                ErrorMessage="!" Font-Bold="True" 
                                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                                                Font-Names="Arial" Font-Size="10pt"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                <br />
                &nbsp;<br />
                                            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                                                ForeColor="Red" Visible="False" Width="501px"></asp:Label>
                <br />
                &nbsp;<br />
                &nbsp;<br />
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
                <br />
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 660px; position: absolute; top: 504px" TabIndex="26"
        ToolTip="Home" />
    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
        Style="z-index: 101; left: 527px; position: absolute; top: 504px" TabIndex="13"
        ToolTip="Avvia Ricerca" />
    &nbsp; &nbsp; &nbsp;&nbsp;
    </form>
</body>

</html>


