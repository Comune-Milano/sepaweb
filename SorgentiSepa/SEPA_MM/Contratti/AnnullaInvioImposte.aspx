<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnnullaInvioImposte.aspx.vb" Inherits="Contratti_AnnullaInvioImposte" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Annullo Invio</title>
    <style type="text/css">
        .auto-style1 {
            font-size: x-small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="COD.CONTRATTO"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="INTESTATARIO"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="COD.TRIBUTO"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="ANNO"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="DATA CREAZIONE"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="DATA INVIO A.E."></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="IMP. CANONE"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="IMP.TRIBUTO"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="IMP.SANZIONE"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="IMP.INTERESSI"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text="ESITO"></asp:Label>
                            </td>
                        </tr>
                        <asp:Label ID="lblTabella" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Text=""></asp:Label>

                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
