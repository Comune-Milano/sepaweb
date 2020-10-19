<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptPgExtraMavRuoli.aspx.vb"
    Inherits="Contratti_Pagamenti_RptPgExtraMavRuoli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Pagamenti Manuali Ruoli</title>
    <script type="text/javascript" src="HGridScript.js"></script>
    <script type="text/javascript">
        function Stampa() {

            window.open('Download.aspx?TITLE=' + document.getElementById('lblTitolo').innerText, '', '');
        }
    </script>
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
                            <img alt="" src="../../NuoveImm/Img_Stampa.png" onclick="Stampa();" style="cursor: pointer;
                                visibility: hidden;" />
                        </td>
                        <td align="right" width="100%">
                            &nbsp;
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
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="dgvRptPagExtraMav" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                    CellPadding="4" ForeColor="#333333" Width="99%" BorderColor="#507CD1" BorderStyle="Solid"
                    PageSize="100" AllowPaging="True">
                    <AlternatingItemStyle BackColor="#EBE9ED" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="Black" Wrap="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_ORA" HeaderText="DATA ORA EVENTO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" Font-Names="Arial"
                                Font-Size="8pt" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO" HeaderText="TIPO PAGAMENTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SGRAVIO" HeaderText="SGRAVIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_PAGAMENTO" HeaderText="DATA PAGAMENTO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RIFERIMENTO_DA_A" HeaderText="DATE RIFERIMENTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BOLLETTA" HeaderText="BOLLETTA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="RIFERIMENTO" HeaderText="RIFERIMENTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE" HeaderText="IMPORTO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
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
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        document.getElementById('divPre').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
