<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoComune.aspx.vb" Inherits="AMMSEPA_NuovoComune" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Nuovo Comune</title>
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
                    <asp:Label ID="Label1" runat="server" Text="Inserimento Nuovo Comune" Style="font-size: 24pt;
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
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 70%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
                                        ForeColor="Black">Nome Comune</asp:Label></span>
                                        </td>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:TextBox ID="txtComune" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        TabIndex="1" Width="193px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
                                        ForeColor="Black">Provincia</asp:Label></span>
                                        </td>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:TextBox ID="txtProvincia" TabIndex="2" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Width="29px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:Label ID="Label4" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False"
                                        ForeColor="Black">CAP</asp:Label></span>
                                        </td>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:TextBox ID="txtCAP" runat="server" BorderStyle="Solid" BorderWidth="1px" TabIndex="3"
                                        Width="72px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox></span>
                            &nbsp;
                                    <asp:Label ID="Label7" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False"
                                        ForeColor="Black" Width="500px">(se non si conosce, inserire il cap del capoluogo di provincia di appartenenza)</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label3" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Codice Catastale</asp:Label>
                                        </td>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:TextBox ID="txtCod" runat="server" BorderStyle="Solid" BorderWidth="1px" TabIndex="4"
                                        Width="73px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label2" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Entro 70 km da Milano (Vecchia Normativa)</asp:Label>
                                        </td>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:CheckBox ID="chEntro" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Black"
                                        TabIndex="5" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                    <asp:Label ID="Label8" runat="server" Text="Distanza in KM (Nuova Normativa da 01/01/2012)" 
                        Font-Names="Arial" Font-Size="10pt" Width="184px"></asp:Label>
                                        </td>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:TextBox ID="txtkm" runat="server" BorderStyle="Solid" BorderWidth="1px" TabIndex="3"
                                        Width="72px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                    <asp:Label ID="Label9" runat="server" Text="Popolazione" 
                        Font-Names="Arial" Font-Size="10pt" Width="184px"></asp:Label>
                                        </td>
                                        <td>
                                <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                    <asp:TextBox ID="txtPopolazione" runat="server" BorderStyle="Solid" BorderWidth="1px" TabIndex="3"
                                        Width="72px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox></span>
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
                                        <td style="width: 50%">
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
                                        <td style="width: 10%">
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