<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Impianti.ascx.vb" Inherits="CENSIMENTO_Tab_Impianti" %>
<table style="width: 645px; height: 177px">
    <tr>
        <td style="vertical-align: top; width: 589px; height: 81px; text-align: left">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 703px; top: 0px; height: 130px; text-align: left">
            <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                    AutoGenerateColumns="False" Font-Size="8pt" Width="97%" 
                    PageSize="13" style="z-index: 105; left: 0px; top: 48px" BackColor="White" 
                    Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                    Font-Strikeout="False" Font-Underline="False" GridLines="None">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000C0"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COD. IMPIANTO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_IMPIANTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
						</asp:datagrid></div>
            </td>
    </tr>
    </table>
