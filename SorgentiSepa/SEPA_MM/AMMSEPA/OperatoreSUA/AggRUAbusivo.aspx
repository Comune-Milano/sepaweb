<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AggRUAbusivo.aspx.vb" Inherits="AMMSEPA_OperatoreSUA_AggRUAbusivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Aggiunta Rapporto Utenza Abusiva</title>
    <script type="text/javascript">
        window.name = "modal";
    </script>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <div>
        <table class="style1">
            <tr>
                <td class="style2" colspan="2">
                    <asp:Label ID="Label1" runat="server" Text="Nuova Utenza Abusiva" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2" colspan="2">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">
                    <asp:Label ID="lblid" runat="server" Text="Cod. Contratto" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtcontratto" runat="server" Width="180px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="../Immagini/text_view.png" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblintestatario" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
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
        </table>
    </div>
    <asp:HiddenField ID="cerca" runat="server" Value="0" />
    </form>
</body>
</html>
