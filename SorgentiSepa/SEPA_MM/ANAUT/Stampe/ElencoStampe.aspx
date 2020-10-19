<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoStampe.aspx.vb" Inherits="ANAUT_Stampe_ElencoStampe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco stampe AU</title>
    <style type="text/css">
        .style1
        {
            font-family: "Times New Roman", Times, serif;
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <table style="width:100%;">
        <tr>
            <td bgcolor="Maroon" style="text-align: center">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                    Font-Size="12pt" ForeColor="White" Text="ELENCO STAMPE"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 28px;
                                    position: static; top: 203px" Width="100%" TabIndex="1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;"><tr><td width="60%">&nbsp;</td><td width="40%">&nbsp;</td></tr></table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
