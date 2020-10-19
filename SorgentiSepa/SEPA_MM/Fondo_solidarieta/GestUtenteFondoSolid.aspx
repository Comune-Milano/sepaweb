<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestUtenteFondoSolid.aspx.vb"
    Inherits="Fondo_solidarieta_GestUtenteFondoSolid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione Utenti Fondo Solidarietà</title>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Gestione Utenti Fondo Solidarietà" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="../AMMSEPA/Immagini/SfondoHome.jpg" height="85px" width="100%" />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="1" cellspacing="0" style="padding:10 10 10 10">
                        <tr>
                            <td style="width: 60%;">
                                <table style="text-align: left" width="300">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                                Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Utente:</asp:Label>
                                        </td>
                                        <td style="width: 3px">
                                            <asp:DropDownList ID="cmbUtente" runat="server" Width="210px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                                Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Password Attuale:</asp:Label>
                                        </td>
                                        <td style="width: 3px">
                                            <asp:TextBox ID="txtPw" runat="server" Rows="1" TextMode="Password" Width="204px"
                                                Style="z-index: 103; left: 243px; position: static; top: 165px" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" Wrap="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                                Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Password Nuova:</asp:Label>
                                        </td>
                                        <td style="width: 3px">
                                            <asp:TextBox ID="txtNPw" runat="server" TextMode="Password" Width="204px" Style="z-index: 104;
                                                left: 244px; position: static; top: 198px" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="10pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                                Style="z-index: 102; left: 63px; position: static; top: 231px" Width="175px">Conferma Password Nuova:</asp:Label>
                                        </td>
                                        <td style="width: 3px">
                                            <asp:TextBox ID="txtCNPw" runat="server" TextMode="Password" Width="204px" Style="z-index: 105;
                                                left: 244px; position: static; top: 230px" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="10pt"></asp:TextBox>
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
                        <tr><td>&nbsp</td></tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 70%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 18%">
                                            <asp:ImageButton ID="imgAzzera" runat="server" ImageUrl="../NuoveImm/Img_Azzera.png"
                                                ToolTip="Azzera la Password corrente" />
                                        </td>
                                        <td style="width: 22%">
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
                                        <td>
                                            &nbsp
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="lblMessaggio" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                                ForeColor="Red" Visible="False" Width="100%"></asp:Label>
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
