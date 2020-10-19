<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Indici.aspx.vb" Inherits="ANAUT_Indici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Indici</title>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoStatoDomanda.jpg);
    background-repeat: no-repeat">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr bgcolor="Maroon">
                <td>
                    <asp:Label ID="Label18" runat="server" ForeColor="White" 
                        Text="Indici Dichiarazione Protocollo" Font-Names="arial" Font-Size="12pt"></asp:Label>
                    <span style="font-family: Arial">&nbsp;
                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Style="position: static"
                            Text="Label" ForeColor="White"></asp:Label>
                    </span>
                </td>
            </tr>
            </table>
        <br />
        <table style="font-weight: normal" width="100%" cellpadding="0" cellspacing="0">
            <tr bgcolor="#E8E8E8">
                <td width="60%" style="height: 20px">
                    <span style="font-size: 11pt">Isee ERP</span>
                </td>
                <td width="40%" style="font-size: 12pt; height: 20px;">
                    <asp:Label ID="Label1" runat="server" Style="position: static" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="height: 21px">
                    Ise
                </td>
                <td style="height: 21px">
                    <asp:Label ID="Label2" runat="server" Style="position: static" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#E8E8E8" style="font-size: 12pt">
                <td style="height: 21px">
                    Isr</td>
                <td style="height: 21px">
                    <asp:Label ID="lblisr" runat="server" Style="position: static" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="height: 21px">
                    Isp</td>
                <td style="height: 21px">
                    <asp:Label ID="lblisp" runat="server" Style="position: static" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#E8E8E8" style="font-size: 12pt">
                <td style="height: 20px">
                    PSE
                </td>
                <td style="height: 20px">
                    <asp:Label ID="Label3" runat="server" Style="position: static" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td>
                    VSE
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Style="position: static" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#E8E8E8" style="font-size: 12pt">
                <td>
                    Anno Reddito
                </td>
                <td>
                    <asp:Label ID="lblAnnoReddito" runat="server" Style="position: static" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td>
    <asp:Label ID="Label11" runat="server" Font-Bold="True" Style="position: static"
        Text="Elaborazioni Isee per Decadenza" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr style="font-size: 12pt">
                <td>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt"
        Style="position: static"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr style="font-size: 12pt">
                <td>
                    &nbsp;</td>
                <td align="right">
                    <img id="imgEsci" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" onclick="javascript:self.close();" style="cursor:pointer"/></td>
            </tr>
            </table>
        </span>
    </div>
    <br />
    <br />
    <br />
    </form>
</body>
</html>
