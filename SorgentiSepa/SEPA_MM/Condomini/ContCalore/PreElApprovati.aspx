<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PreElApprovati.aspx.vb"
    Inherits="Condomini_ContCalore_PreElApprovati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 783px;
        }
        .style1
        {
            text-align: center;
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server">
    <table style="width: 90%;">
        <tr>
            <td>
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
                    ForeColor="#801F1C" Text="ELENCO APPROVATI PER CONTRIBUTO CALORE"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <center><table><tr><td>
                    <asp:RadioButtonList ID="rblTipoStampe" runat="server" Font-Names="arial" 
                        Font-Size="8pt" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">Approvati</asp:ListItem>
                        <asp:ListItem Value="0">Non Approvati</asp:ListItem>
                    </asp:RadioButtonList>
                </td></tr></table></center>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td class="style1" style="text-align: left">
                            <strong>ANNO</strong>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="cmbContCalore" runat="server" Width="100px" AutoPostBack="True"
                                Font-Names="Arial" Font-Size="10pt">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <strong>CONDOMINIO</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbcondomini" runat="server" Width="400px" Font-Names="Arial"
                                Font-Size="10pt">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <center>
                                <asp:Button ID="btnVisualizza" runat="server" Text="VISUALIZZA" BackColor="#507CD1"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" /></center>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
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
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                    OnClientClick="parent.main.location.replace('../pagina_home.aspx');return false;" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
