<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioImpPagati.aspx.vb" Inherits="Contabilita_DettaglioImpPagati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            text-align: center;
            color: #000000;
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <table style="width:100%;">
        <tr>
            <td class="style1">
                        <strong>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:Label ID="TxtTitolo"
                            runat="server"></asp:Label>
                        </span>
                        </span></strong></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="TBL_DETTAGLI" runat="server" Font-Names="ARIAL" 
                                                    Font-Size="8pt" TabIndex="24" Width="98%"></asp:Label>
                </td>
        </tr>
        <tr>
            <td style="font-weight: 700">
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" 
                        Text="Label" Visible="False" Width="100%"></asp:Label>
                </td>
        </tr>
        </table>

    </form>
</body>
</html>
