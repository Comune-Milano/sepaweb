<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptStato.aspx.vb" Inherits="CALL_CENTER_RptStato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0;">
                <table style="width:100%;">
                    <tr>
                        <td style="text-align: left" width="20%">
                            <img alt="" src="../NuoveImm/MM_113_84.png" /></td>
                        <td width="80%">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                <asp:Label ID="lbTitolo" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="14pt" style="text-align: center"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                <asp:Label ID="lblSpiega" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="12pt" style="text-align: center"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                <asp:Label ID="lblFiltri" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="12pt" style="text-align: center"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                    &nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td>
                    <asp:ImageButton ID="btnExport" runat="server" 
                        ImageUrl="~/NuoveImm/Img_ExportExcel.png" />
            </td>
        </tr>
        <tr>
            <td>
                    <asp:DataGrid ID="DataGridRptTipo" runat="server" AutoGenerateColumns="False"
                        CellPadding="2" ForeColor="#333333" Style="z-index: 11;
                        left: 18px; top: 81px" Width="100%" BorderColor="Gray" 
                    BorderWidth="2px">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="TIPO" HeaderText="TIPO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="APERTI" HeaderText="APERTE">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IN_CARICO" HeaderText="IN CARICO">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IN_SOPRALLUOGO" HeaderText="IN SOPRALLUOGO">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IN_ORDINE" HeaderText="IN ORDINE">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RESPINTI" HeaderText="RESPINTI">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CHIUSI" HeaderText="CHIUSI">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ANNULLATI" HeaderText="ANNULLATI">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE" HeaderText="TOTALE">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                    Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="Aqua" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="White" />
                    </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    </form>
</body>
</html>
