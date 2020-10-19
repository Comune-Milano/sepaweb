<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiEstrazioniPagamenti.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiEstrazioniPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stampa Pagamenti</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="width: 100%">
                    <table width="100%">
                        <tr>
                            <td style="width: 10%">
                                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../../NuoveImm/Img_ExportExcel.png" Style="height: 12px" />
                            </td>
                            <td style="width: 10%">
                                &nbsp;
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
                    <asp:Label ID="Titolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <asp:DataGrid runat="server" ID="DataGridPagamenti" CellPadding="1" Font-Names="Arial" Font-Size="8pt" ForeColor="#000000" GridLines="None" CellSpacing="1" ShowFooter="false" AutoGenerateColumns="false" Width="99%" UseAccessibleHeader="True">
                        <Columns>
                            <asp:BoundColumn DataField="ID_PAGAMENTO" HeaderText="ID_PAGAMENTO" ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_FORNITORE" HeaderText="CODICE FORNITORE" ItemStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RAGIONE_SOCIALE" HeaderText="RAGIONE SOCIALE" ItemStyle-HorizontalAlign="Left" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE PARTITA IVA" ItemStyle-HorizontalAlign="Left" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NUMERO_CDP" HeaderText="NUMERO CDP" ItemStyle-HorizontalAlign="Right" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPONIBILE" HeaderText="IMPONIBILE" ItemStyle-HorizontalAlign="Right" Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IVA" HeaderText="IVA" ItemStyle-HorizontalAlign="Right" Visible="True" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOT" HeaderText="TOTALE" ItemStyle-HorizontalAlign="Right" Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_VOCE_PF" HeaderText="id_voce_pf" ItemStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VOCE_PF" HeaderText="VOCE" ItemStyle-HorizontalAlign="Left" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO" ItemStyle-HorizontalAlign="Left" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPONIBILE_D" HeaderText="IMPONIBILE DETTAGLIO" ItemStyle-HorizontalAlign="Right" Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IVA_D" HeaderText="IVA DETTAGLIO" ItemStyle-HorizontalAlign="Right" Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_D" HeaderText="TOTALE DETTAGLIO" ItemStyle-HorizontalAlign="Right" Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NUMERO_RDS" HeaderText="NUMERO RDS" ItemStyle-HorizontalAlign="Left" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="N_FATT_FORN" HeaderText="NUMERO FATTURA FORNITORE" ItemStyle-HorizontalAlign="Left" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_FATT" HeaderText="DATA FATTURA" ItemStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-CssClass="formatoData"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_OP_CONT" HeaderText="CODICE OPERAZIONE CONTABILE" ItemStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="IMPORTO" ItemStyle-HorizontalAlign="Right" Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NUMERO_PAG" HeaderText="NUMERO PAGAMENTO" ItemStyle-HorizontalAlign="Left" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_PAG" HeaderText="DATA PAGAMENTO" ItemStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-CssClass="formatoData"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_PAGATO" HeaderText="IMPORTO PAGATO" ItemStyle-HorizontalAlign="Right" Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_OP_CONTAB" HeaderText="CODICE OPERAZIONE CONTABILE" ItemStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CUP" HeaderText="CUP" ItemStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CIG" HeaderText="CIG" ItemStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle ForeColor="#000000" />
                        <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                    </asp:DataGrid>

                    <%--<asp:DataGrid runat="server" ID="DataGridPagamenti" CellPadding="1" Font-Names="Arial" Font-Size="8pt" ForeColor="#000000" GridLines="None" CellSpacing="1" ShowFooter="false" AutoGenerateColumns="false" Width="99%" UseAccessibleHeader="True">
                        <Columns>
                            <asp:BoundColumn DataField="ID_PAGAMENTO" HeaderText="ID_PAGAMENTO" ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_FORNITORE" HeaderText="CODICE FORNITORE" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="RAGIONE_SOCIALE" HeaderText="RAGIONE SOCIALE" ItemStyle-HorizontalAlign="Left" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE PARTITA IVA" ItemStyle-HorizontalAlign="Left" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="NUMERO_CDP" HeaderText="NUMERO CDP" ItemStyle-HorizontalAlign="Right" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPONIBILE" HeaderText="IMPONIBILE" ItemStyle-HorizontalAlign="Right" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="IVA" HeaderText="IVA" ItemStyle-HorizontalAlign="Right" Visible="True"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOT" HeaderText="TOTALE" ItemStyle-HorizontalAlign="Right" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_VOCE_PF" HeaderText="id_voce_pf" ItemStyle-HorizontalAlign="Left" Visible="false" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="VOCE_PF" HeaderText="VOCE" ItemStyle-HorizontalAlign="Left" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO" ItemStyle-HorizontalAlign="Left" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPONIBILE_D" HeaderText="IMPONIBILE DETTAGLIO" ItemStyle-HorizontalAlign="Right" Visible="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IVA_D" HeaderText="IVA DETTAGLIO" ItemStyle-HorizontalAlign="Right" Visible="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_D" HeaderText="TOTALE DETTAGLIO" ItemStyle-HorizontalAlign="Right" Visible="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NUMERO_RDS" HeaderText="NUMERO RDS" ItemStyle-HorizontalAlign="Left" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="N_FATT_FORN" HeaderText="NUMERO FATTURA FORNITORE" ItemStyle-HorizontalAlign="Left" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_FATT" HeaderText="DATA FATTURA" ItemStyle-HorizontalAlign="Center" Visible="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_OP_CONT" HeaderText="CODICE OPERAZIONE CONTABILE" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="IMPORTO" ItemStyle-HorizontalAlign="Right" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="NUMERO_PAG" HeaderText="NUMERO PAGAMENTO" ItemStyle-HorizontalAlign="Left" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_PAG" HeaderText="DATA PAGAMENTO" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_PAGATO" HeaderText="IMPORTO PAGATO" ItemStyle-HorizontalAlign="Right" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_OP_CONTAB" HeaderText="CODICE OPERAZIONE CONTABILE" ItemStyle-HorizontalAlign="Center" Visible="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CUP" HeaderText="CUP" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="CIG" HeaderText="CIG" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle ForeColor="#000000" />
                        <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                    </asp:DataGrid>--%>
                </td>
            </tr>
        </table>
    </div>
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
