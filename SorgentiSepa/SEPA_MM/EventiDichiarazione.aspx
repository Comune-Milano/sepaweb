<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EventiDichiarazione.aspx.vb" Inherits="EventiDichiarazione" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Eventi</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL"
                            Font-Size="12pt" Text="Label"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="DataGrid1" runat="server" Font-Names="Arial"
                            AutoGenerateColumns="False" Font-Size="8pt"
                            PageSize="8" Style="z-index: 105; width: 100%;"
                            Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" GridLines="None" CellPadding="4"
                            ForeColor="#333333">
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="DATA_ORA_1" HeaderText="DATA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ENTE" HeaderText="ENTE"></asp:BoundColumn>
                            </Columns>
                            <ItemStyle BackColor="#EFF3FB" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
