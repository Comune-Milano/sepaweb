<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AggIBAN.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_AggIBAN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>IBAN</title>
    <script type="text/javascript">
        window.name = "modal";
        function disabilitaMinore(e) {
            var key;
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 226)
                return false;
            else
                return true;
        }
    </script>
</head>
<body >


    <form id="form1" runat="server" target ="modal">
    <div>
        <asp:HiddenField ID="modificaIBAN" runat="server" Value="0" />
        <br />
        <strong>&nbsp;
        <asp:Label runat="server" ID="nomeFORN" style="color: #660000; font-family: Arial; font-size:11px; vertical-align: text-top;">
        </asp:Label>
        </strong> 
        <br />
        <br />
        <table width="100%">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="IBAN*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIBAN" runat="server" Width="180px" MaxLength="27"
                        onkeydown="return disabilitaMinore(event)" Font-Names="Arial" 
                        Font-Size="8pt"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="labelerrore" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#CC0000"
                        Text="IBAN obbligatorio"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Stato*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStato" runat="server" Font-Names="Arial" 
                        Font-Size="8pt">
                        <asp:ListItem Value="1">Attivo</asp:ListItem>
                        <asp:ListItem Value="0">Non attivo</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Metodo pagamento"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMetodo" runat="server" Width="180px" MaxLength="100"
                        onkeydown="return disabilitaMinore(event)" Font-Names="Arial" 
                        Font-Size="8pt"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo pagamento"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTipo" runat="server" Width="180px" MaxLength="100"
                        onkeydown="return disabilitaMinore(event)" Font-Names="Arial" 
                        Font-Size="8pt"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: right">
                    <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="../../../NuoveImm/Img_SalvaVal.png"
                        Style="text-align: right" />
                </td>
                <td style="text-align: right">
                    <asp:Image ID="btnAnnulla" runat="server" ImageUrl="../../../NuoveImm/Img_AnnullaVal.png"
                        Style="text-align: right; cursor:pointer;" onclick="window.top.close();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
