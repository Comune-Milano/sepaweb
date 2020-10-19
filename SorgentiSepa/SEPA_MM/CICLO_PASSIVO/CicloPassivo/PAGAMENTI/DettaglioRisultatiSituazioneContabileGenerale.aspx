<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioRisultatiSituazioneContabileGenerale.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_DettaglioRisultatiSituazioneContabileGenerale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Situazione Contabile Generale per Esercizio</title>
</head>
<body style="overflow:auto;height:100%;width:100%">
    <form id="form1" runat="server">
    <div style="overflow:auto;height:100%;width:99%">
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="width:100%">
                    <asp:ImageButton ID="ImageButtonEsporta" runat="server" 
                        ImageUrl="../../../NuoveImm/Img_ExportExcel.png" />
                    <asp:ImageButton ID="ImageButtonEsci" runat="server" 
                        ImageUrl="../../../NuoveImm/Img_Esci.png" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        Text="Dettaglio Prenotazioni Situazione Contabile Generale Esercizio Contabile"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRis" runat="server" Text="Nessun risultato trovato" Font-Bold="True"
                        Font-Names="Arial" Font-Size="10pt"></asp:Label>
                    <div style="overflow:auto;height:100%;width:100%">
                        <asp:DataGrid runat="server" ID="DataGridEs" CellPadding="1" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#000000" GridLines="None" CellSpacing="1" ShowFooter="True"
                            AutoGenerateColumns="false" Width="95%">
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CODICE" HeaderText="CODICE" ItemStyle-HorizontalAlign="Left" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VOCE" HeaderText="VOCE" ItemStyle-HorizontalAlign="Left" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_PAGAMENTO" HeaderText="TIPO PAGAMENTO" ItemStyle-HorizontalAlign="Left" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE" ItemStyle-HorizontalAlign="Left" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="IMPORTO" ItemStyle-HorizontalAlign="Right" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE" ItemStyle-HorizontalAlign="Left" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" ItemStyle-HorizontalAlign="Left" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ODL" HeaderText="ODL" ItemStyle-HorizontalAlign="Left" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="DATA SCADENZA" ItemStyle-HorizontalAlign="Left" Visible="true"></asp:BoundColumn>
                            </Columns>
                            <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Center" />
                            <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                            <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
