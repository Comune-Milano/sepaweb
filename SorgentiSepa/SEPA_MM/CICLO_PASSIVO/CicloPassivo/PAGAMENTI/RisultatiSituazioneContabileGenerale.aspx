<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiSituazioneContabileGenerale.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiSituazioneContabileGenerale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Situazione Contabile Generale per Esercizio</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                        Text="Situazione Contabile Generale Esercizio Contabile"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRis" runat="server" Text="Nessun risultato trovato" Font-Bold="True"
                        Font-Names="Arial" Font-Size="10pt"></asp:Label>
                    <div>
                        <asp:DataGrid runat="server" ID="DataGridEs" CellPadding="1" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#000000" GridLines="None" CellSpacing="1" ShowFooter="false" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CODICE" HeaderText="CODICE" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BUDGET" HeaderText="BUDGET INIZIALE" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ASSESTATO" HeaderText="BUDGET ASSESTATO + VARIAZIONI" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RESIDUO" HeaderText="DISPONIBILITA' RESIDUA (E-O)" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RESIDUO_TOTALE" HeaderText="DISPONIBILITA' RESIDUA (E-(O+H))" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRENOTATO" HeaderText="TOTALE PRENOTATO" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRENOTATO_ODL" HeaderText="PRENOTATO ODL" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRENOTATO_CANONE" HeaderText="PRENOTATO A CANONE" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRENOTATO_CONDOMINI" HeaderText="PRENOTATO CONDOMINI" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRENOTATO_ALTRO" HeaderText="ALTRO PRENOTATO" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CONSUNTIVATO1" HeaderText="TOTALE CONSUNTIVATO ANNO1" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CONSUNTIVATO2" HeaderText="TOTALE CONSUNTIVATO ANNO2" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CONSUNTIVATO" HeaderText="TOTALE CONSUNTIVATO (M+N)" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RITCONSUNTIVATO1" HeaderText="RIT LEGGE CONSUNTIVATO ANNO1" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RITCONSUNTIVATO2" HeaderText="RIT LEGGE CONSUNTIVATO ANNO2" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RITCONSUNTIVATO" HeaderText="TOTALE RIT LEGGE CONSUNTIVATA (P+Q)" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CERTIFICATO1" HeaderText="TOTALE CERTIFICATO ANNO1" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CERTIFICATO2" HeaderText="TOTALE CERTIFICATO ANNO2" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CERTIFICATO" HeaderText="TOTALE CERTIFICATO (S+T)" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RITCERTIFICATO1" HeaderText="TOTALE RIT LEGGE CERTIFICATO ANNO1" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RITCERTIFICATO2" HeaderText="TOTALE RIT LEGGE CERTIFICATO ANNO2" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RITCERTIFICATO" HeaderText="TOTALE RIT LEGGE CERTIFICATO (V+W)" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PAGATO1" HeaderText="TOTALE PAGATO ANNO1" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PAGATO2" HeaderText="TOTALE PAGATO ANNO2" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PAGATO" HeaderText="TOTALE PAGATO (X+Y)" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RITPAGATO1" HeaderText="TOTALE RIT PAGATO ANNO1" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RITPAGATO2" HeaderText="TOTALE RIT PAGATO ANNO2" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RITPAGATO" HeaderText="TOTALE RIT PAGATO (Z+AB)" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
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
     <script type="text/javascript">
        window.focus();
        self.focus();
    </script>
</body>
</html>
