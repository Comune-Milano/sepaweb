<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IntroProcessoDec.aspx.vb"
    Inherits="ASS_IntroProcessoDec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .stileComponenti
        {
            font-family: Arial;
            font-size: 12pt;
            padding-left: 70px;
        }
        
        
        
        .CssMaiuscolo
        {
            text-transform: uppercase;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="background-image: url(../NuoveImm/SfondoMaschere.jpg); background-repeat: no-repeat;
            left: 0px; top: 0px; position: absolute;" width="670px">
            <tr>
                <td colspan="2">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Assegnazioni Alloggi</strong></span><br />
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
                    <img alt='Approssima Ricerca' src="../ImmMaschere/alert2_ricercad.gif" />
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <asp:Label ID="Label1" runat="server" Text="Cognome" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Width="180px" CssClass="CssMaiuscolo"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <asp:Label ID="Label2" runat="server" Text="Nome" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="180px"
                        CssClass="CssMaiuscolo"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <asp:Label ID="Label3" runat="server" Text="Protocollo" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPG" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="180px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <asp:Label ID="Label4" runat="server" Text="Num. Offerta" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumOff" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Width="180px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <asp:Label ID="Label5" runat="server" Text="Tipologia" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbTipo" runat="server" Width="185px">
                        <asp:ListItem Selected="True" Value="-1"> --- </asp:ListItem>
                        <asp:ListItem Value="1">BANDO ERP</asp:ListItem>
                        <asp:ListItem Value="2">BANDO CAMBI</asp:ListItem>
                        <asp:ListItem Value="3">CAMBIO EMERGENZA</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <asp:Label ID="Label7" runat="server" Text="Contratto Generato" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbContratto" runat="server" Width="60px">
                        <asp:ListItem Value="-1" Text="TUTTI"></asp:ListItem>
                        <asp:ListItem Value="1">SI</asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">NO</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <asp:Label ID="Label6" runat="server" Text="Assegnazione Revocata" Font-Size="8pt"
                        Font-Names="Arial"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="cmbRevoca" runat="server" />
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
                <td align="right">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../NuoveImm/Img_AvviaRicerca.png" />&nbsp
                </td>
                <td>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../NuoveImm/Img_Home.png" />&nbsp
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
        </table>
    </div>
    </form>
</body>
</html>
