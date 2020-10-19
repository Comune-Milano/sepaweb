<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Cancella.aspx.vb" Inherits="AutoCompilazione_Cancella" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Cancella Domanda</title>
</head>
<body background="Immagini/Sfondo.gif">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 15%; height: 20px; text-align: center">
                    <img alt="Milano" src="Immagini/Milano.gif" /></td>
                <td style="border-left: #cc0000 1px solid; width: 85%; border-bottom: #cc0000 1px solid;
                    height: 20px; text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="border-right: #cc0000 1px solid; border-top: #cc0000 1px solid; width: 7%;
                    text-align: center" valign="top">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <img alt="Logo Comune" src="Immagini/LogoComune.gif" /></td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial"></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="left" style="width: 85%">
                    <table width="100%">
                        <tr>
                            <td style="background-image: url(Immagini/BarraSfondo.gif); height: 38px; text-align: left"
                                valign="top">
                                <span style="font-size: 14pt; color: #393a3a">Comune di Milano - Cancella Domanda di
                                    Bando E.R.P.</span></td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 253px">
                                <br />
                                <span style="font-size: 14pt; font-family: Arial">Utilizza questa funzione per cancellare
                                    i dati della Domanda on-line che hai inserito.<br />
                                </span>
                                <br />
                                <table cellpadding="0" cellspacing="0" width="80%">
                                    <tr>
                                        <td align="left" style="width: 25%">
                                            <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="12pt" Style="z-index: 100;
                                                left: 19px; position: static; top: 220px" Text="Codice Fiscale del Dichiarante"
                                                Width="235px"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCF" runat="server" Font-Names="arial" Font-Size="12pt" MaxLength="16"
                                                Style="z-index: 104; left: 271px; position: static; top: 218px" TabIndex="1"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                        </td>
                                        <td align="left" width="40%">
                                            <asp:Label ID="lblErrore" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Style="z-index: 109; left: 19px; position: static; top: 246px" Visible="False"
                                                Width="410px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 25%">
                                            <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="12pt" Style="z-index: 101;
                                                left: 19px; position: static; top: 278px" Text="Numero domanda" Width="179px"></asp:Label></td>
                                        <td align="left" width="40%">
                                            <asp:TextBox ID="txtNumero" runat="server" Font-Names="arial" Font-Size="12pt" MaxLength="100"
                                                Style="z-index: 105; left: 271px; position: static; top: 275px" Width="294px" TabIndex="2"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: left">
                                            <span style="font-size: 10pt; font-family: Arial"><em>(Indicare il numero della domanda
                                                ottenuto in fase di registrazione)</em></span></td>
                                        <td align="left" width="40%">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 19px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td align="left" style="height: 19px" width="40%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 19px; text-align: left">
                                            <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="12pt" Style="z-index: 101;
                                                left: 19px; position: static; top: 278px" Text="E-mail del Richiedente" Width="179px"></asp:Label></td>
                                        <td align="left" style="height: 19px" width="40%">
                                            <asp:TextBox ID="txtMail" runat="server" Font-Names="arial" Font-Size="12pt" MaxLength="100"
                                                Style="z-index: 105; left: 271px; position: static; top: 275px" Width="294px" TabIndex="3"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 19px; text-align: left">
                                        </td>
                                        <td align="left" style="height: 19px" width="40%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                        </td>
                                        <td align="center" width="40%">
                                            <asp:Button ID="Button1" runat="server" Style="z-index: 108; left: 497px; position: static;
                                                top: 414px" Text="Indietro" TabIndex="4" />&nbsp;
                                            <asp:Button ID="btnValidaCF" runat="server" Style="z-index: 108; left: 497px; position: static;
                                                top: 414px" Text="Visualizza" TabIndex="5" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 25%">
                                            &nbsp;</td>
                                        <td align="right" width="40%">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblErroreGen" runat="server" Font-Names="ARIAL" Font-Size="10pt" ForeColor="Red"
                                                Style="z-index: 112; left: 20px; position: static; top: 447px" Visible="False"
                                                Width="635px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="14pt"></asp:Label></td>
                                    </tr>
                                </table>
                                <asp:LinkButton ID="hp" runat="server" Visible="False" TabIndex="5">Clicca qui per cancellare definitivamente la Domanda e attendi qualche secondo.</asp:LinkButton>
                                <asp:LinkButton ID="hp1" runat="server" Visible="False" TabIndex="6">Clicca qui per tornare alla pagina principale</asp:LinkButton>
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

