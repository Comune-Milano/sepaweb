<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptPgIngGenerale.aspx.vb"
    Inherits="Contratti_Pagamenti_RptPgIngGenerale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report bollette ingiunte</title>
</head>
<body onload="self.focus()">
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <span style="font-family: Arial"><strong>
                                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../NuoveImm/Img_ExportExcel.png"
                                        TabIndex="-1" ToolTip="Esporta in Excel" />
                                </strong></span>
                            </td>
                            <td>
                                <img alt="" src="../../NuoveImm/Img_Stampa.png" onclick="Stampa();" style="cursor: pointer; visibility: hidden;" />
                            </td>
                            <td align="right" width="100%">&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTitolo" runat="server" Font-Names="Arial" Font-Size="14pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="dgvRptPagExtraMav" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                        CellPadding="4" ForeColor="#333333" Width="99%" BorderColor="#507CD1" BorderStyle="Solid"
                        PageSize="50" AllowPaging="True">
                        <AlternatingItemStyle BackColor="#EBE9ED" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="Black" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="RAPPORTO UTENZA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INGIUNZIONE" HeaderText="INGIUNZIONE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_INGIUNZIONE" HeaderText="IMPORTO ING."></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMP_INGIUNZIONE_PAG" HeaderText="IMP. ING. PAGATO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RESIDUO" HeaderText="RESIDUO"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" HorizontalAlign="Center" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" />
                        <PagerStyle BackColor="White" ForeColor="#507CD1" HorizontalAlign="Center" Mode="NumericPages"
                            Position="TopAndBottom" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
        </table>
        <script language="javascript" type="text/javascript">
            document.getElementById('divPre').style.visibility = 'hidden';
        </script>
    </form>
</body>
</html>
