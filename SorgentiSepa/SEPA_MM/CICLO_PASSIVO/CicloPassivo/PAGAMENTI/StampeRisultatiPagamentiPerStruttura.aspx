<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampeRisultatiPagamentiPerStruttura.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_StampeRisultatiPagamentiPerStruttura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Situazione Contabile</title>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td colspan="2" style="height: 7px; width: 100%">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" class="style1">
                <asp:Label ID="lblTitolo" runat="server" Font-Names="Arial" Font-Size="14pt" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 7px;" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="lblErrore" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
        CellPadding="4" ForeColor="#333333" Width="100%" BorderColor="#507CD1" 
        BorderStyle="Solid">
        <AlternatingItemStyle BackColor="WHITE" />
        <Columns>
            <asp:BoundColumn DataField="ID_PAG" HeaderText="ID PAGAMENTO" Visible="False">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="CODICE" HeaderText="CODICE">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" Width="6%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="VOCE" HeaderText="VOCE">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="9%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="True" Width="9%" HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="DESCRIZIONE_TIPO" HeaderText="TIPO PAGAMENTO">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="9%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="True" Width="9%" HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="DESCRIZIONE_PAG" HeaderText="DESCRIZIONE PAGAMENTO">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="8%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="true" Width="8%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FILIALE" HeaderText="SEDE T.">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="8%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" Width="8%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="IMPORTO CONSUNTIVATO">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" Width="6%" HorizontalAlign="right" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="10%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" Width="10%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ADP" HeaderText="ADP">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="DATA_ADP" HeaderText="DATA ADP">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="6%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="IMPORTO_ADP" HeaderText="IMPORTO ADP">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" Width="6%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="RIT_LEGGE" HeaderText="RIT.LEGGE ADP">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" Width="6%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="MAE" HeaderText="MAE">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="14%" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" Width="14%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="TIPO_ADP" HeaderText="TIPO_ADP" Visible="False">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ID_PRE" HeaderText="ID PRENOTAZIONE" Visible="False">
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundColumn>
        </Columns>
        <EditItemStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="White" HorizontalAlign="Center" />
        <ItemStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" />
        <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
            Position="TopAndBottom" />
        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:DataGrid>
    <asp:HiddenField ID="codicevoce" runat="server" />
    <asp:HiddenField ID="idPagamentoPrec" runat="server" Value="-1" />
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
