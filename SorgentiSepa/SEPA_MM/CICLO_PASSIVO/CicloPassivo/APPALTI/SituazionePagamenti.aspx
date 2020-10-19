<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SituazionePagamenti.aspx.vb"
    Inherits="SituazionePagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    function vediDiv() {
        document.getElementById('dvvvPre').style.visibility = 'visible';
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>Situazione Pagamenti</title>
</head>
<body >
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="height: 7px;">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                    ForeColor="Maroon" Width="100%">Situazione Pagamenti</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 18%">
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Esercizio Finanziario"></asp:Label>
                        </td>
                        <td style="width: 27%">
                            <asp:DropDownList ID="ddlEsercizio" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="9pt">
                                <asp:ListItem Selected="True">Qualsiasi</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 5%">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Anno"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            <asp:DropDownList ID="ddlAnno" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="9pt">
                                <asp:ListItem Selected="True">Qualsiasi</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Stato pagamento"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlStatoPagamento" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="9pt">
                                <asp:ListItem Selected="True">Qualsiasi</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 400px">
                <asp:Label runat="server" ID="lblRisultati" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                <div id="Result" style="z-index: -1; overflow: auto; width: 769px; height: 380px;
                    border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                    border-bottom: black 1px solid; border-color: #99CCFF; border-width: 2px; visibility: <%=RisultatiVisibility%>;">
                    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#99CCFF" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="Black" Height="200px" PageSize="30" Style="table-layout: auto;
                        clip: rect(auto auto auto auto); direction: ltr; border-collapse: separate" Width="1400px"
                        GridLines="Vertical">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" Mode="NumericPages" ForeColor="Blue" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="NUM" HeaderText="NUM" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ANNO" HeaderText="ANNO" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_STAMPA" HeaderText="DATA" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_CONSUNTIVATO" HeaderText="IMPORTO CONSUNTIVATO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STATO_PAGAMENTO" HeaderText="STATO PAGAMENTO" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STRUTTURA" HeaderText="STRUTTURA" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" Wrap="False" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 44%">
                            &nbsp;
                        </td>
                        <td style="width: 28%">
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../../NuoveImm/Img_Export_XLS.png"
                                ToolTip="Export in Excel" />
                        </td>
                        <td style="width: 28%">
                            <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../../../NuoveImm/Img_HomeModelli.png"
                                ToolTip="Torna alla Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
