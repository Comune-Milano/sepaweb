<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Serramenti.ascx.vb"
    Inherits="CENSIMENTO_Tab_Serramenti" %>
<style type="text/css">
    .style1
    {
        color: #0000CC;
        font-size: 8pt;
    }
    .style2
    {
        height: 52px;
    }
</style>
<table width="97%">
    <tr>
        <td class="style1" style="font-family: Arial">
            <strong>SERRAMENTI INTERNI ED ESTERNI</strong>
        </td>
    </tr>
    <tr>
        <td style="border: 1px solid #0066FF">
            <table style="margin-left: 10px; width: 99%;">
                <tr>
                    <td>
                        <table id="table1" align="center" runat="server" width="100%" cellpadding="0">
                            <tr>
                                <td>
                                    <div style="overflow: auto; height: 500px;">
                                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                            <asp:DataGrid ID="dgDatiUI" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="None" Width="97%">
                                                <ItemStyle BackColor="#EFF3FB" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                    Position="Top" VerticalAlign="Top" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="White" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_STATO_MAN" HeaderText="ID_STATO_MAN" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT1" HeaderText="ID_MANUT1" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT2" HeaderText="ID_MANUT2" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT3" HeaderText="ID_MANUT3" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT4" HeaderText="ID_MANUT4" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STATO" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="STATO1">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato1" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT1")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO2">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato2" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT2")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO3">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato3" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT3")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO4">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato4" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT4")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="UM" HeaderText="U.M."></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="QTA'">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="quantita_txt" runat="server" Width="40px" Style="text-align: right;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="COSTO" HeaderText="COSTO UNITARIO"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="ADDEBITO">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="addebito_txt" runat="server" Width="55px" Style="text-align: right;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </span></strong>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%--<asp:HiddenField ID="id_stato" runat="server" />
<asp:HiddenField ID="id_sloggio" runat="server" />
<asp:HiddenField ID="stato_verb" runat="server" />--%>
<asp:HiddenField ID="sola_lettura" runat="server" Value="0" />
