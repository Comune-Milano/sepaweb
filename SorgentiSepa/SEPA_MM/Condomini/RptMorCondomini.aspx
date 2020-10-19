<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptMorCondomini.aspx.vb"
    Inherits="Condomini_RptMorCondomini" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 8pt;
            font-weight: bold;
        }
        .style2
        {
            font-family: Arial;
            font-size: 10pt;
            font-weight: bold;
            height: 24px;
        }
        .style3
        {
            font-family: Arial;
            font-size: 12pt;
            font-weight: bold;
            height: 24px;
        }
        .style_intestazione
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 8pt;
            background-color: #E6E6FA;
            color: #0000C0;
        }
        .style_risultati
        {
            font-family: Arial;
            font-size: 8pt;
            background-color: #ffffff;
        }
        #form1
        {
            width: 97%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Elenco delle
            Morosità<br />
        </span></strong>
    </div>
    <div>
        <table width="98%">
            <tr>
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3" width="100px">
                    <asp:Label ID="Label1" runat="server" Text="CONDOMINIO:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="lblCondominio" runat="server" Text=""></asp:Label>
                </td>
                <td class="style3" width="180px">
                    <asp:Label ID="Label2" runat="server" Text="PERIODO DI GESTIONE:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="lblPeriodoGestione" runat="server" Text=""></asp:Label>
                </td>
                <td class="style3" width="100px">
                    <asp:Label ID="Label3" runat="server" Text="AMMINISTRATORE:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="lblAmministratore" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table width="98%" style="margin-left: 3%;">
            <tr>
                <td>
                    <asp:Label ID="rigaMorosita" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr align="right">
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Height="18px" Style="z-index: 104; left: 121px; width: 580px;"
                        Visible="False"></asp:Label>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="Immagini/Img_Export_Grande.png"
                            Style="z-index: 10; left: 12px;" ToolTip="Esporta in Excel" />
                    </span></strong>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
