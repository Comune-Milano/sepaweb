<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoResidui.aspx.vb" Inherits="Contabilita_Report_ElencoResidui" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; width: 770px;">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 90%">
                <br />
                <asp:Label ID="Label1" Text="Elenco elaborazione residui" runat="server" ForeColor="Maroon"
                    Font-Size="14pt" Font-Bold="true" Font-Names="Arial" />
            </td>
            <td style="width: 10%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 90%">
                &nbsp;
            </td>
            <td style="width: 10%">
                <asp:ImageButton ID="ImageButton" runat="server" ImageUrl="../../NuoveImm/Refresh.png"
                    AlternateText="Aggiorna" ToolTip="Aggiorna" Height="32px" Width="32px" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="width: 770px; height: 500px; overflow: auto;">
                    <asp:DataGrid ID="DataGridElenco" runat="server" CellPadding="2" Font-Bold="False"
                        Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="None"
                        Width="100%" CellSpacing="2" ShowFooter="True" AutoGenerateColumns="False">
                        <ItemStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left"
                                Visible="false">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INIZIO" HeaderText="INIZIO" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FINE" HeaderText="FINE" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ESITO" HeaderText="ESITO" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PARAMETRI_RICERCA" HeaderText="PARAMETRI RICERCA" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DETTAGLIO" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PARZIALE" HeaderText="PARZIALE" ItemStyle-HorizontalAlign="Left"
                                Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE" HeaderText="TOTALE" ItemStyle-HorizontalAlign="Left"
                                Visible="false"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="Red" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <br />
                    <br />
                    <asp:Label ID="LabelRis" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" /><br />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
