<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResiduiFinali.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_ResiduiFinali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Dettagli Residui Finali</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <telerik:RadButton ID="btnExport" runat="server" Text="Export XLS" ToolTip="Esporta in Excel" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            <telerik:RadButton ID="btnStampa" runat="server" Text="Stampa" ToolTip="Stampa" />
                        </td>
                        <td style="width: 70%">
                            &nbsp;
                        </td>
                        <td style="width: 10%; text-align: right">
                            <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" OnClientClicking="function(sender, args){self.close();}"
                                ToolTip="Esci" />
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
        <%--<tr>
            <td style="width: 400px; text-align: left">
                <table width="20%">
                    <tr style="height: 25px">
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="Legenda" Font-Names="Arial" Font-Size="8pt"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="width: 85%">
                            <asp:Label ID="Label2" runat="server" Text="Variazione" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                        </td>
                        <td style="width: 15%;">
                            <div style="background: Bisque; border: 1px solid #000;height:15px;">
                                &nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td style="width: 85%">
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Variazione e Liquidazione"></asp:Label>
                        </td>
                        <td style="width: 15%;">
                            <div style="background: YellowGreen; border: 1px solid #000;height:15px;">
                                &nbsp;
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr>
            <td style="width: 100%">
                <asp:DataGrid runat="server" ID="DataGrid" AutoGenerateColumns="False" CellPadding="1"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                    Width="100%" ShowFooter="True">
                    <AlternatingItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                    <Columns>
                        <asp:BoundColumn DataField="CODICE" HeaderText="CODICE VOCE B.P.">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VOCE" HeaderText="VOCE">
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
                        <asp:BoundColumn DataField="FILIALE" HeaderText="SEDE T.">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="IMPORTO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_LIQUIDATO" HeaderText="IMPORTO LIQUIDATO" Visible="False">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DIFFERENZA" HeaderText="IMPORTO VARIAZIONE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO_PRENOTAZIONE" HeaderText="STATO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_REPERTORIO" HeaderText="REPERTORIO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" />
                    <ItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                    <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td style="height: 20px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <telerik:RadButton ID="btnExcelRit" runat="server" Text="Export XLS" ToolTip="Esporta in Excel" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            <telerik:RadButton ID="btnStampaRit" runat="server" Text="Stampa" ToolTip="Stampa" />
                        </td>
                        <td style="width: 70%">
                            &nbsp;
                        </td>
                        <td style="width: 10%; text-align: right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center;">
                <asp:Label ID="ErroreRitLeggeCertificate" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="11pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center">
                <asp:Label ID="TitoloRitLeggeCertificate" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="12pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:DataGrid runat="server" ID="DataGridRitLegge" AutoGenerateColumns="False" CellPadding="1"
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
                        <asp:BoundColumn DataField="STATOA" HeaderText="STATO ATTUALE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RIT_LEGGE_IVATA" HeaderText="RIT. LEGGE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
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
                    <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
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
