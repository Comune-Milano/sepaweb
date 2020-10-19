<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ControllaComponenti.aspx.vb"
    Inherits="VSA_ControllaComponenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Controlla Componenti</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="vertical-align: top; height: 41px;">
                    <asp:Label ID="lblMessaggio" runat="server" Font-Names="Arial" Font-Size="10pt"
                        Width="100%" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="codFiscale" runat="server" />
                    <asp:HiddenField ID="iddich" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        ForeColor="Red" Text="" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
