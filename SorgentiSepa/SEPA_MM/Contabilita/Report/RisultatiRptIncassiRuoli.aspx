<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiRptIncassiRuoli.aspx.vb"
    Inherits="Contabilita_Report_RisultatiRptIncassiRuoli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Situazione Incassi Ruoli</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:ImageButton ID="ImageButtonExcel" runat="server" ImageUrl="../../NuoveImm/Img_ExportExcel.png"
                                ToolTip="Export in Excel" />
                        </td>
                        <td style="width: 8%">
                            <asp:ImageButton ID="ImageButtonStampa" runat="server" ImageUrl="../../NuoveImm/Img_Stampa.png"
                                ToolTip="Stampa in PDF" />
                        </td>
                        <td style="width: 10%; text-align: right">
                            <asp:ImageButton ID="ImageButtonEsci" runat="server" ImageUrl="../../NuoveImm/Img_Esci.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center;">
                <asp:Label ID="LabelErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center">
                <asp:Label ID="LabelTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"></asp:Label>
            </td>
        </tr>
        <tr><td>&nbsp</td></tr>
        <tr>
            <td>
                <table id="infoRU" style="width: 100%;">
                    <tr>
                        <td colspan="7" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label Text="Nominativo" runat="server" ID="Label8" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="#3366CC" />
                                    </td>
                                    <td>
                                        <asp:Label Text="Codice contratto" runat="server" ID="Label9" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="#3366CC" />
                                    </td>
                                    <td>
                                        <asp:Label Text="Stato contratto" runat="server" ID="Label10" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="#3366CC" />
                                    </td>
                                    <td>
                                        <asp:Label Text="Data inizio" runat="server" ID="Label11" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="#3366CC" />
                                    </td>
                                    <td>
                                        <asp:Label Text="Data fine" runat="server" ID="Label12" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="#3366CC" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Nominativo" Width="100%" Font-Names="Arial" Font-Size="9pt" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Cod_Contratto" Width="100%" Font-Names="Arial" Font-Size="9pt" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Stato_ru" Width="100%" Font-Names="Arial" Font-Size="9pt" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="DataInizio" Width="100%" Font-Names="Arial" Font-Size="9pt" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="DataFine" Width="100%" Font-Names="Arial" Font-Size="9pt" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:DataGrid runat="server" ID="DataGridIncassi" AutoGenerateColumns="False" CellPadding="0"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                    Width="100%" ShowFooter="True" BackColor="#999999" BorderColor="#507CD1" BorderWidth="1px"
                    Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False">
                    <AlternatingItemStyle BackColor="#EEEEEE" ForeColor="#000000" />
                    <Columns>
                        <asp:BoundColumn DataField="BOLLETTAZIONE" HeaderText="BOLLETTAZIONE" ItemStyle-Width="10%"
                            HeaderStyle-Width="10%">
                            <HeaderStyle Width="10%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO" ItemStyle-Width="5%"
                            HeaderStyle-Width="5%">
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="COMPETENZA_ACC" HeaderText="COMPETENZA ACCERTAMENTO"
                            ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ANNO" HeaderText="ANNO" ItemStyle-Width="4%" HeaderStyle-Width="4%">
                            <HeaderStyle Width="4%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BIMESTRE" HeaderText="BIMESTRE" ItemStyle-Width="5%"
                            HeaderStyle-Width="5%">
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="COMPETENZA" HeaderText="COMPETENZA" ItemStyle-Width="7%"
                            HeaderStyle-Width="7%">
                            <HeaderStyle Width="7%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MACROCATEGORIA" HeaderText="MACROCATEGORIA" ItemStyle-Width="10%"
                            HeaderStyle-Width="10%">
                            <HeaderStyle Width="10%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA" ItemStyle-Width="10%"
                            HeaderStyle-Width="10%">
                            <HeaderStyle Width="10%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VOCE" HeaderText="VOCE" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            <HeaderStyle Width="10%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="USI_ABITATIVI" HeaderText="USO UI" ItemStyle-Width="7%"
                            HeaderStyle-Width="7%">
                            <HeaderStyle Width="7%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO_UI" HeaderText="TIPOLOGIA UI" ItemStyle-Width="8%"
                            HeaderStyle-Width="8%">
                            <HeaderStyle Width="8%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" ItemStyle-Width="6%" HeaderStyle-Width="6%">
                            <HeaderStyle Width="6%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Red" Font-Italic="False"
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
    <asp:HiddenField ID="codContratto" runat="server" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">

        if (document.getElementById('divLoading') != null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
        }
        if (document.getElementById('divLoading5') != null) {
            document.getElementById('divLoading5').style.visibility = 'hidden';
        }

        if (document.getElementById('codContratto').value != '0' && document.getElementById('codContratto').value != '') {
            document.getElementById('infoRU').style.display = 'block';
        } else {
            document.getElementById('infoRU').style.display = 'none';
        }
    </script>
</body>
</html>
