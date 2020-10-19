<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioVariazioni.aspx.vb"
    Inherits="DettaglioVariazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dettaglio Variazioni</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="FIN" Value="0" />
    <table style="width: 100%;">
        <tr>
            <td style="text-align: left; height: 21px;">
                <span style="font-family: Arial"><strong>
                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../../NuoveImm/Img_ExportExcel.png"
                        TabIndex="2" ToolTip="Esporta in Excel" />
                </strong>
                    <asp:ImageButton ID="btnStampaPDF" runat="server" ImageUrl="../../../NuoveImm/Img_Stampa.png"
                        ToolTip="Stampa in PDF" />
                </span>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <span style="font-family: Arial"><strong style="text-align: center">
                                <asp:Label ID="lbltitolo" runat="server" Font-Names="Arial" Font-Size="13pt">Dettaglio Variazioni tra Voci (Esercizio Finanziario <%=Titolo %>)</asp:Label>
                            </strong></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblErrore" runat="server" Text="Errore nel caricamento dei dati" Font-Names="Arial"
                    Font-Size="10pt" ForeColor="Red"></asp:Label>
                <br />
                <!--
                    <table cellspacing="0" cellpadding="4" rules="all" border="1" id="DataGrid1" style="color: #333333;
                    border-color: #507CD1; border-width: 1px; border-style: Solid; width: 100%; border-collapse: collapse;">
                -->
                <%= Tabellavocipositive%><%= TabellaRiepilogo%>
                <!--
                    </table>
                -->
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
        if (document.getElementById('FIN').value == '0') {
            window.focus();
            self.focus();
        }
    </script>
</body>
</html>
