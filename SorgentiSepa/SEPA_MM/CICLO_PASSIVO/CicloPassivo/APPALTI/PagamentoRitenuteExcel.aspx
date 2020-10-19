<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PagamentoRitenuteExcel.aspx.vb"
    Inherits="PagamentoRitenuteExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    function vediDiv() {
        document.getElementById('dvvvPre').style.visibility = 'visible';
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>Import pagamento certificati</title>
</head>
<body >
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="height: 5px">
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:Label ID="lblTitoloPagina" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="12pt" ForeColor="Maroon" Text="Elaborazione ritenute d'acconto">
                </asp:Label>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 30%">
                            &nbsp;</td>
                        <td style="width: 70%">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 70%">
                            <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
                                Width="580px" />
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            <asp:ImageButton ID="btnElabora" runat="server" ImageUrl="../../../NuoveImm/Img_Elabora.png"
                                OnClientClick="vediDiv();" ToolTip="Elabora il file Excel" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Width="100%">Elenco Contenuto file Excel</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 250px">
                <div id="Result" style="z-index: -1; overflow: auto; width: 100%; height: 230px;
                    border-right: black 1px solid; visibility: <%=RisultatiVisibility%>; border-top: black 1px solid;
                    border-left: black 1px solid; border-bottom: black 1px solid; border-color: #99CCFF;
                    border-width: 2px;">
                    <asp:DataGrid ID="DataGridExcel" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#99CCFF" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="Black" Height="200px" PageSize="1" Style="table-layout: auto;
                        clip: rect(auto auto auto auto); direction: ltr; border-collapse: separate" Width="100%">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="#" HeaderText="#" HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                            <asp:BoundColumn DataField="N_FORN" HeaderText="N° FORN." HeaderStyle-Width="5%"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE" HeaderStyle-Width="17%"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" HeaderStyle-Width="10%"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CDP" HeaderText="C.D.P." HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VOCE" HeaderText="VOCE B.P." HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ANNO_MAE" HeaderText="ANNO M.A.E." HeaderStyle-Width="5%"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MAE" HeaderText="M.A.E." HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_PAG" HeaderText="DATA PAGAMENTO MANDATO" HeaderStyle-Width="10%"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ERRORE" HeaderText="ERRORE" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" Wrap="False" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 20%; text-align: center" height="90px">
                            <asp:Label ID="lblNote" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" TabIndex="-1" Style="text-align: center">
                            </asp:Label>
                        </td>
                        <td style="width: 80%" height="90px">
                            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="500"
                                TextMode="MultiLine" Width="100%" ReadOnly="True" Rows="6">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 40%; text-align: right;">
                            <asp:ImageButton ID="btnConvalida" runat="server" ImageUrl="../../../CICLO_PASSIVO/CicloPassivo/Plan/Immagini/img_Convalida.png"
                                Style="text-align: right" Visible="False" />
                        </td>
                        <td style="width: 20%">
                            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="../../../NuoveImm/Img_SalvaContinua.png"
                                OnClientClick="vediDiv();" TabIndex="2" ToolTip="Salva le modifiche apportate" />
                        </td>
                        <td style="width: 12%">
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../../../NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="vediDiv();" TabIndex="3" ToolTip="Annulla" />
                        </td>
                        <td style="width: 28%">
                            <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../../../NuoveImm/Img_HomeModelli.png"
                                OnClientClick="vediDiv();" TabIndex="3" ToolTip="Esci senza inserire o modificare" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="NumeroRigheImportate" runat="server" Value="0" />
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();
    </script>
</body>
<script language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility = 'hidden';
</script>
</html>
