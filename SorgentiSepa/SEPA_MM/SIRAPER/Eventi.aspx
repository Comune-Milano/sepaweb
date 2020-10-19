<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Eventi.aspx.vb" Inherits="SIRAPER_Eventi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elenco Eventi</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function downloadFile(filePath) {
            location.replace('' + filePath + '');
        };
    </script>
</head>
<body style="background-image: url('Immagini/Sfondo.png'); background-repeat: repeat-x;">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr style="height: 30px; vertical-align: bottom">
                <td style="width: 10%; text-align: center; vertical-align: middle;">
                    <asp:Button ID="btnExport" runat="server" CssClass="bottone" Text="Export Excel"
                        ToolTip="Export in formato XLS" CausesValidation="False" />
                </td>
                <td style="width: 80%; height: 25px; text-align: center; vertical-align: middle;">
                    <asp:Label ID="lblTitolo" runat="server" Text="EVENTI SIRAPER" 
                        Font-Names="arial" Font-Bold="True"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td style="width: 10%; text-align: center; vertical-align: middle;">
                    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" ToolTip="Esci"
                        OnClientClick="self.close();return false;" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="overflow: auto; width: 100%; height: 600px; vertical-align: top;">
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                            <asp:DataGrid ID="dgvEventi" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                GridLines="None" PageSize="25" Width="98%">
                                <ItemStyle BackColor="#EFF3FB" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundColumn DataField="DATA_ORA" HeaderText="DATA / ORA"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CODICE" HeaderText="CODICE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO" HeaderText="TIPO EVENTO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="MOTIVAZIONE"></asp:BoundColumn>
                                </Columns>
                                <EditItemStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            </asp:DataGrid>
                        </span></strong>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
