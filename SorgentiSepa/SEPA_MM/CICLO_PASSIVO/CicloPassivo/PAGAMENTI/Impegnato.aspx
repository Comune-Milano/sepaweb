<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Impegnato.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_Impegnato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stampa Dettagli Impegnato</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../../NuoveImm/Img_ExportExcel.png" />
                        </td>
                        <td style="width: 10%">
                            <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="../../../NuoveImm/Img_Stampa.png" />
                        </td>
                        <td style="width: 70%">
                            &nbsp;
                        </td>
                        <td style="width: 10%; text-align: right">
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../../../NuoveImm/Img_Esci.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center;">
                <asp:Label ID="Errore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center">
                <asp:Label ID="Titolo" runat="server" Font-Bold="True" Font-Names="Arial" 
                Font-Size="12pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:DataGrid runat="server" ID="DataGrid" AutoGenerateColumns="False" CellPadding="1"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                    Width="100%" ShowFooter="True">
                    <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
                    <Columns>
                        <asp:BoundColumn DataField="CODICE" HeaderText="CODICE VOCE B.P.">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VOCE_DGR" HeaderText="VOCE DGR">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE_TIPO" HeaderText="TIPO PAGAMENTO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE_PAG" HeaderText="DESCRIZIONE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NOME" HeaderText="SEDE T.">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RIT_LEGGE_IVATA" HeaderText="RIT. LEGGE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RAGIONE_SOCIALE" HeaderText="FORNITORE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_REPERTORIO" HeaderText="REPERTORIO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" 
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="False" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" />
                    <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                    <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="FIN" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('FIN').value == '0') {
            window.focus();
            self.focus();
        }
        document.getElementById('divLoading').style.visibility = 'hidden';
    </script>
</body>
</html>
