<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoPagamenti.aspx.vb"
    Inherits="Contratti_ElencoPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Pagamenti</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow: auto">
        <table width="100%">
            <tr>
                <td width="100%">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                        Text="SCARICHI EFFETTUATI"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:DataGrid ID="DataGrid1" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                        AllowPaging="True" Font-Size="8pt" Style="z-index: 105; width: 100%;" Font-Bold="False"
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                        CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
                        Width="99%">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="#003399"
                            ForeColor="#CCCCFF"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="DATA_GENERAZIONE" HeaderText="DATA GENERAZIONE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ANNO" HeaderText="ANNO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_TRIBUTO" HeaderText="CODICE TRIBUTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_AE" HeaderText="INVIO AE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_CANONE" HeaderText="IMP.CANONE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_TRIBUTO" HeaderText="IMP. TRIBUTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GIORNI_SANZIONE" HeaderText="GIORNI RITARDO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_SANZIONE" HeaderText="IMPORTO SANZIONI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_INTERESSI" HeaderText="IMPORTO INTERESSI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FILE_SCARICATO" HeaderText="SCARICATO NEL FILE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_STATO_REGISTRAZIONE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnAnnullaInvio" runat="server" CommandName="Edit" ImageUrl="../NuoveImm/Elimina.png"
                                        ToolTip="Annulla invio" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle BackColor="White" ForeColor="#003399" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" Mode="NumericPages">
                        </PagerStyle>
                        <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <br />
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                        Text="RICEVUTE INSERITE"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 105;
                        width: 100%;" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
                        Width="99%">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#CCCCFF" />
                        <Columns>
                            <asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ANNO" HeaderText="ANNO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VALIDA_FINO_AL" HeaderText="VALIDA FINO AL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_TRIBUTO" HeaderText="CODICE TRIBUTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_AE" HeaderText="DATA INVIO AE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PG_AE" HeaderText="PROTOCOLLO AE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REGISTRO" HeaderText="IMP. DI REGISTRO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SOSTITUTIVA" HeaderText="IMP. SOSTITUTIVA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SANZIONI" HeaderText="IMP. SANZIONI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INTERESSI" HeaderText="IMP. INTERESSI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOME_FILE_REL" HeaderText="RICEVUTA REL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOME_FILE_PDF" HeaderText="RICEVUTA PDF"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                        </Columns>
                        <ItemStyle BackColor="White" ForeColor="#003399" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <br />
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                        Text="RICEVUTE SCARTATE"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:DataGrid ID="DataGridScarti" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 105;
                        width: 100%;" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
                        Width="99%">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#CCCCFF" />
                        <Columns>
                            <asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOME_FILE_REL" HeaderText="RICEVUTA REL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOME_FILE_PDF" HeaderText="RICEVUTA PDF"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                        </Columns>
                        <ItemStyle BackColor="White" ForeColor="#003399" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <br />
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                        Text="AVVISI DI LIQUIDAZIONE"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:DataGrid ID="DataGrid3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 105;
                        width: 100%;" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
                        Width="99%">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#CCCCFF" />
                        <Columns>
                            <asp:BoundColumn DataField="CODICE" HeaderText="CODICE TRIBUTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SANZIONI" HeaderText="SANZIONI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INTERESSI" HeaderText="INTERESSI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SPESE_NOTIFICA" HeaderText="SPESE NOTIFICA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE" HeaderText="TOTALE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_PG" HeaderText="DATA PROTOCOLLO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_PAG" HeaderText="DATA PAGAMENTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RICEVUTA" HeaderText="RICEVUTA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="QUIETANZA" HeaderText="QUIETANZA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                        </Columns>
                        <ItemStyle BackColor="White" ForeColor="#003399" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="LBLID" runat="server" Value="-1" />
    <asp:HiddenField ID="lblCodUfficio" runat="server" Value="-1" />
    <asp:HiddenField ID="lblNumReg" runat="server" Value="-1" />
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
