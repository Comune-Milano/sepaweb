<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElApprovati.aspx.vb" Inherits="Condomini_ContCalore_ElApprovati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Contributo Calore</title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 70%">
                            <asp:Label ID="lblTitolo" runat="server" Style="text-align: center; font-weight: 700;
                                font-family: Arial; font-size: 12pt" Text="titolo"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="../../NuoveImm/Img_Stampa.png" OnClientClick = "document.getElementById('btnStampa').style.visibility='hidden'" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../NuoveImm/Img_ExportExcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblRisultati" runat="server" Style="text-align: left;" Text="lblRisultati"
                    Width="100%" Font-Names="Arial" Font-Size="10pt"></asp:Label>
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
                        <td style="font-family: Arial; font-size: 12pt; font-weight: 700; color: #0033CC;
                            text-align: center">
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
                </table>
            </td>
        </tr>
    </table>
    </form>
    <script language ="javascript" type = "text/javascript"  >
        document.getElementById('btnStampa').style.visibility = 'visible'
    </script>
</body>
</html>
