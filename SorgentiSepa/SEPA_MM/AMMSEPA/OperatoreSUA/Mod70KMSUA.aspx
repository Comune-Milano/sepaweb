<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Mod70KMSUA.aspx.vb" Inherits="AMMSEPA_OperatoreSUA_Mod70KMSUA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Modifica Entro 70KM da Milano</title>
    <script type="text/javascript">
        window.name = "modal";
    </script>
    </head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <div>
        <table class="style1">
            <tr>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Text="Modifica campo" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label2" runat="server" Text="Nome" Font-Names="Arial" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Enabled="False" Width="150px" 
                        Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label3" runat="server" Text="Provincia" Font-Names="Arial" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Enabled="False" Width="150px" 
                        Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label4" runat="server" Text="CAP" Font-Names="Arial" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Enabled="False" Width="150px" 
                        Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label5" runat="server" Text="Cod. Catastale" Font-Names="Arial" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Enabled="False" Width="150px" 
                        Font-Names="arial" Font-Size="10pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label6" runat="server" Text="Entro 70KM (Vecchia Normativa)" 
                        Font-Names="Arial" Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="22px" Width="100px" 
                        Font-Names="arial" Font-Size="10pt">
                        <asp:ListItem Value="1">SI</asp:ListItem>
                        <asp:ListItem Value="0">NO</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label7" runat="server" Text="Distanza in KM (Nuova Normativa da 01/01/2012)" 
                        Font-Names="Arial" Font-Size="10pt" Width="184px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="70px" 
                        Font-Names="arial" Font-Size="10pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label8" runat="server" Text="Popolazione" 
                        Font-Names="Arial" Font-Size="10pt" Width="184px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtPopolazione" runat="server" Width="70px" 
                        Font-Names="arial" Font-Size="10pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    <center>
                        <asp:ImageButton ID="ImgSalva" runat="server" ImageUrl="../../NuoveImm/Img_SalvaGrande.png"
                            ToolTip="Salva i dati inseriti" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="ImgEsci" runat="server" ImageUrl="../../NuoveImm/Img_Esci_AMM.png"
                            ToolTip="Esci" /></center>
                </td>
            </tr>
            <tr>
                <td class="style2" colspan="2">
                                            <asp:Label ID="lblErrore" runat="server" 
                        Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                                                ForeColor="Red" Visible="False" Width="350px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
