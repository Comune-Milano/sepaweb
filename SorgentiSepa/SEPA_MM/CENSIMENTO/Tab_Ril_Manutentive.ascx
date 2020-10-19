<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Ril_Manutentive.ascx.vb"
    Inherits="CENSIMENTO_Tab_Ril_Manutentive" %>
<table style="width: 645px; height: 177px">
    <tr>
        <td style="vertical-align: top; width: 589px; height: 81px; text-align: left">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top; overflow: auto;
                width: 703px; top: 0px; height: 130px; text-align: left">
                <asp:DataGrid ID="dataGridRilevazioni" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                    Font-Size="8pt" Width="97%" PageSize="13" Style="z-index: 105; left: 0px; top: 48px"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" GridLines="None">
                    <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="White"
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="ORIGINE" HeaderText="ORIGINE" Visible="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO" Visible="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_OPERAZIONE" HeaderText="DATA" Visible="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COSTO_INTERVENTO" HeaderText="COSTO INTERVENTO" Visible="True"
                            ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="COLLEGAMENTO" HeaderText="" Visible="True" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                    <ItemStyle Height="20px" />
                    <PagerStyle Mode="NumericPages"></PagerStyle>
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
        </td>
    </tr>
</table>
