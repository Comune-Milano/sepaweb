<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EventiVSA.aspx.vb" Inherits="VSA_EventiVSA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Eventi Domanda</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" BorderStyle="Solid" BorderWidth="1px" Text="ELENCO EVENTI DOMANDA"
                        Font-Size="11pt" Font-Names="Arial" Width="100%" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BackColor="White" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        PageSize="1" Style="table-layout: auto; z-index: 101; left: 16px; top: 200px;"
                        Width="100%" GridLines="Vertical" CellPadding="3">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="DATA_ORA" HeaderText="DATA ORA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_EVENTO" HeaderText="COD_EVENTO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_OPERATORE" HeaderText="ID_OPERATORE" Visible="False">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="White" Wrap="False" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Label ID="lblTotale" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Size="11pt"
                        Font-Names="Arial" Width="100%" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
