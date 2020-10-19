<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InfoUtente.aspx.vb" Inherits="InfoUtente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Informazioni Utente</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: small;
        }
    </style>
</head>
<body bgcolor="#c8c8c8">
    <form id="form1" runat="server" defaultbutton="btnSalva" defaultfocus="txtPw">
    <div style="text-align: center">
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <table style="background-color: white" width="750">
            <tr>
                <td style="height: 44px; font-size: x-small;">
                    <img src="Images/sepa_header_InfoUtente.png" />
                </td>
            </tr>
            <tr>
                <td style="height: 44px">
                    <br />
                    <asp:Label ID="Label5" runat="server" Enabled="False" Font-Bold="True" Font-Names="Arial"
                        Font-Size="10pt" Height="43px" Width="858px" Style="z-index: 109; left: 20px;
                        position: static; top: 106px; color: black; background-color: white;">Inserire e/o modificare le informazioni personali relative alla Vostra utenza ai fini di una migliore ottimizzazione delle funzioni del sistema e del loro utilizzo.</asp:Label><br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <table style="text-align: left" width="300">
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                    Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Operatore</asp:Label>
                            </td>
                            <td style="width: 3px">
                                <asp:Label ID="lblOperatore" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                    Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                    Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Cognome</asp:Label>
                            </td>
                            <td style="width: 3px">
                                <asp:TextBox ID="txtCognome" runat="server" Height="22px" Rows="1" Width="204px"
                                    Style="z-index: 103; left: 243px; position: static; top: 165px" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="arial" Font-Size="10pt" Wrap="False" TabIndex="1"
                                    MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                    Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Nome</asp:Label>
                            </td>
                            <td style="width: 3px">
                                <asp:TextBox ID="txtNome" runat="server" Height="22px" TabIndex="2" Width="204px"
                                    Style="z-index: 104; left: 244px; position: static; top: 198px" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                    Style="z-index: 102; left: 63px; position: static; top: 231px" Width="100px">Codice Fiscale</asp:Label>
                            </td>
                            <td style="width: 3px">
                                <asp:TextBox ID="txtCF" runat="server" Height="22px" TabIndex="3" Width="204px" Style="z-index: 105;
                                    left: 244px; position: static; top: 230px" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="arial" Font-Size="10pt" MaxLength="16"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                    Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Codice Aziendale</asp:Label>
                            </td>
                            <td style="width: 3px">
                                <asp:TextBox ID="txtCA" runat="server" Height="22px" TabIndex="4" Width="204px" Style="z-index: 105;
                                    left: 244px; position: static; top: 230px" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="arial" Font-Size="10pt" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                    Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Ufficio/Filiale</asp:Label>
                            </td>
                            <td style="width: 3px">
                                <asp:DropDownList ID="cmbUfficio" runat="server" Width="480px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="10pt" Height="18px"
                                    Style="z-index: 101; left: 63px; position: static; top: 199px" Width="115px">Tipo Operatore</asp:Label>
                            </td>
                            <td style="width: 3px">
                                <asp:DropDownList ID="cmbTipoOp" runat="server" Width="204px">
                                    <asp:ListItem Value="1">AMMINISTRATIVO</asp:ListItem>
                                    <asp:ListItem Value="2">TECNICO</asp:ListItem>
                                    <asp:ListItem Value="3">AMM. E TEC.</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    I dati saranno modificabili in qualsiasi momento tramite apposita funzione.
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblmessaggio" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
                        Height="17px" Width="651px" Style="z-index: 106; left: 12px; position: static;
                        top: 272px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSalva" runat="server" Height="30px" TabIndex="4" Text="Memorizza e procedi"
                        Width="180px" Style="z-index: 107; left: 432px; position: static; top: 359px" />
                    <asp:HiddenField ID="idCaf" runat="server" Value="" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; height: 32px;">
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
    </div>
    </form>
</body>
</html>
