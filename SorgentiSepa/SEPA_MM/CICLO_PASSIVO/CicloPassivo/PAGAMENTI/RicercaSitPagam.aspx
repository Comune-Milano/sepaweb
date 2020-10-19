<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaSitPagam.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RicercaSitPagam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Sit. Pagamenti</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:Label ID="lbltitolo" runat="server" Text="Label">Ricerca Situazione Pagamenti</asp:Label></strong></span>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Eserc. finanziario"></asp:Label>
                    &nbsp;&nbsp;<asp:DropDownList ID="cmbEserFinanz" runat="server" Font-Names="arial" Font-Size="10pt"
                        TabIndex="2" Width="600px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                     &nbsp;</td>
            </tr>
            <tr>
                <td>
                     &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Struttura"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="cmbStrutture" runat="server" Font-Names="arial" Font-Size="10pt"
                        TabIndex="2" Width="600px">
                    </asp:DropDownList>
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
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnAvanti" runat="server" ImageUrl="../../../NuoveImm/Img_Procedi.png"
                        TabIndex="3" ToolTip="Procedi" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../../../NuoveImm/Img_Home.png"
                        TabIndex="-1" ToolTip="Home" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
