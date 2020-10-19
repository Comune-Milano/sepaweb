<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Password.aspx.vb" Inherits="AMMSEPA_RicercaOP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestione Password</title>
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
    <script type="text/javascript">
        document.onkeydown = $onkeydown;
    </script>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Gestione Password Operatori " Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="Immagini/SfondoHome.jpg" height="75px" width="101%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 60%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 30%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 45%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt">Lunghezza minima della password</asp:Label>
                                        </td>
                                        <td>
                                <asp:TextBox ID="txtLunghezza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    TabIndex="1" Width="29px"></asp:TextBox>
                                        </td>
                                        <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLunghezza"
                                    ErrorMessage="Solo numeri!" Font-Names="arial" Font-Size="10pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt">Deve contenere sia numeri che lettere</asp:Label>
                                        </td>
                                        <td>
                                        <asp:CheckBox ID="ChNumLet" runat="server" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label7" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">La password deve essere modificata ogni</asp:Label>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtNumGiorni" TabIndex="1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Width="29px"></asp:TextBox>
                                        </td>
                                        <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumGiorni"
                                    ErrorMessage="Solo numeri!" Font-Names="arial" Font-Size="10pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <asp:Label ID="Label2" runat="server" Font-Size="10pt" Font-Names="Arial" 
                                                Font-Bold="False">Numero di Tentativi prima della revoca</asp:Label>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtTentativi" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    TabIndex="1" Width="29px"></asp:TextBox>
                                        </td>
                                        <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTentativi"
                                    ErrorMessage="Solo numeri!" Font-Names="arial" Font-Size="10pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                Font-Size="10pt">Giorni di inattivitą prima della revoca</asp:Label>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtAttivita" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    TabIndex="1" Width="29px"></asp:TextBox>
                                        </td>
                                        <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtAttivita"
                                    ErrorMessage="Solo numeri!" Font-Names="arial" Font-Size="10pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
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
                                        <td style="width: 30%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                                <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="../NuoveImm/img_SalvaModelli.png"
                                                    TabIndex="6" ToolTip="Salva" /></span>
                                        </td>
                                        <td style="width: 20%">
                                            <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/img_HomeModelli.png"
                                                    TabIndex="7" ToolTip="Home" /></span>
                                        </td>
                                        <td style="width: 30%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                                                ForeColor="Red" Visible="False" Width="501px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>