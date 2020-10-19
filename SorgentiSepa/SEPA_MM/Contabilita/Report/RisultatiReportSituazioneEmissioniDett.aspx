<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiReportSituazioneEmissioniDett.aspx.vb"
    Inherits="Contabilita_Report_RisultatiReportSituazioneEmissioniDett" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Situazione Emissioni</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:ImageButton ID="ImageButtonExcel" runat="server" ImageUrl="../../NuoveImm/Img_ExportExcel.png"
                                ToolTip="Export in Excel" />
                        </td>
                        <td style="width: 10%">
                            &nbsp;</td>
                        <td style="width: 70%">
                            &nbsp;
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
                <asp:Label ID="LabelTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:DataGrid runat="server" ID="DataGridEmissioni" AutoGenerateColumns="False" CellPadding="0"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                    Width="100%" ShowFooter="True" BackColor="#999999" BorderColor="#507CD1" BorderWidth="1px"
                    Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" AllowPaging="True" PageSize="200">
                    <AlternatingItemStyle BackColor="#EEEEEE" ForeColor="#000000" />
                    <Columns>
                        <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE CONTRATTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUMERO BOLLETTA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BOLLETTAZIONE" HeaderText="BOLLETTAZIONE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ANNO_ES_CONTABILE" HeaderText="ANNO CONTABILE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ANNO" HeaderText="ANNO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BIMESTRE" HeaderText="BIMESTRE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COMPETENZA" HeaderText="COMPETENZA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MACROCATEGORIA" HeaderText="MACROCATEGORIA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="VOCE" HeaderText="VOCE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="USI_ABITATIVI" HeaderText="USO UI"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO_UI" HeaderText="TIPOLOGIA UI"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ANNULLI" HeaderText="ANNULLI"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE" HeaderText="TOTALE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ACCERTATO" HeaderText="ACCERTATO"></asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" />
                    <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                    <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Position="TopAndBottom" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="HiddenFieldPrimoPiano" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('HiddenFieldPrimoPiano').value == '0') {
            window.focus();
            self.focus();
        }
        if (document.getElementById('divLoading') != null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
        }
        if (document.getElementById('divLoading5') != null) {
            document.getElementById('divLoading5').style.visibility = 'hidden';
        }
    </script>
</body>
</html>
