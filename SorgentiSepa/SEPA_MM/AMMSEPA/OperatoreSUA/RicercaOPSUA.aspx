<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaOPSUA.aspx.vb" Inherits="AMMSEPA_OperatoreSUA_RicercaOPSUA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca Operatori</title>
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
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;">
            <tr>
                <td style="width: 1%;  height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Ricerca Operatori" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="../Immagini/SfondoHome.jpg" height="75px" width="101%" />
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
                            <asp:Label ID="Label9" runat="server" Font-Size="10pt" Font-Names="Arial" 
                                Font-Bold="False">Cognome</asp:Label>
                                        </td>
                                        <td>
                            <asp:TextBox ID="txtCognome" TabIndex="2" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                                            </td>
                                        <td rowspan="4">
                            <img src="../../ImmMaschere/alert2_ricercad.gif" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Nome</asp:Label>
                                        </td>
                                        <td>
                            <asp:TextBox ID="txtNome" TabIndex="2" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Codice Fiscale</asp:Label>
                                        </td>
                                        <td>
                            <asp:TextBox ID="txtCF" TabIndex="3" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt">Operatore</asp:Label>
                                        </td>
                                        <td>
                            <asp:TextBox ID="txtOperatore" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                TabIndex="4" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Ente</asp:Label>
                                        </td>
                                        <td>
                            <asp:DropDownList ID="cmbEnte" TabIndex="5" runat="server" Width="187px" 
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
                                            <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                Font-Size="10pt">Fornitore Esterno</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbFEsterno" runat="server" Font-Names="Arial" 
                                                Font-Size="10pt" 
                                                Style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" 
                                                TabIndex="6" Width="187px">
                                                <asp:ListItem Value="1">SI</asp:ListItem>
                                                <asp:ListItem Value="0">NO</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="2">TUTTI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt">Bando FSA</asp:Label>
                                        </td>
                                        <td>
                                <asp:DropDownList ID="cmbBando" TabIndex="6" runat="server" Width="187px" AutoPostBack="True"
                                    Style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                    border-bottom: black 1px solid;" Enabled="False" Font-Names="Arial" Font-Size="10pt">
                                </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt">Da Registrare</asp:Label>
                                        </td>
                                        <td>
                                <asp:DropDownList ID="DropDownList1" TabIndex="5" runat="server" Width="187px" 
                                                Style="border-right: black 1px solid;
                                    border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" 
                                                Font-Names="Arial" Font-Size="10pt">
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">TUTTI</asp:ListItem>
                                </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
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
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="../../NuoveImm/Img_AvviaRicerca.png"
                                                TabIndex="8" ToolTip="Avvia Ricerca" />
                                        </td>
                                        <td style="width: 15%">
                                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                                TabIndex="9" ToolTip="Home" />
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
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