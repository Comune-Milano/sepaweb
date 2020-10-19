<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_UnComuni.ascx.vb" Inherits="CENSIMENTO_Tab_UnComuni" %>
<table style="width: 645px; height: 177px">
    <tr>
        <td style="vertical-align: top; width: 589px; height: 81px; text-align: left" 
            colspan ="4">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 703px; top: 0px; height: 130px; text-align: left">
            <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                    AutoGenerateColumns="False" Font-Size="8pt" Width="97%" PageSize="13" 
                                style="z-index: 105; left: 4px; top: 64px" BackColor="White" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False" GridLines="None">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000C0"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_UNITA_COMUNE" HeaderText="COD" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="CODICE UNITA COMUNE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_COMUNE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_COMUNE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CIVICO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COMUNE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<ItemTemplate>
										<asp:LinkButton id="LinkButton1" runat="server" Text="Modifica" CausesValidation="false" CommandName="Edit">Seleziona</asp:LinkButton>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="LinkButton3" runat="server" Text="Aggiorna" CommandName="Update"></asp:LinkButton>&nbsp;
										<asp:LinkButton id="LinkButton2" runat="server" Text="Annulla" CausesValidation="false" CommandName="Cancel"></asp:LinkButton>
									</EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
						</asp:datagrid></div>
            </td>
    </tr>
    <tr>
        <td style="vertical-align: top; width: 589px; text-align: left; height: 22px;">
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="10pt" ForeColor="Black" MaxLength="100" Style="left: 13px;
                top: 197px" Width="352px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox></td>
        <td style="vertical-align: top; width: 587px; text-align: center; height: 22px;">
<asp:ImageButton
                ID="btnNuovoUC" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/Img_Nuovo.png"
                Style="z-index: 102; left: 440px; top: 141px; " TabIndex="3" 
                ToolTip="Nuovo" CausesValidation="False" OnClientClick="ConfermaEsci();" /></td>
        <td style="vertical-align: top; width: 587px; text-align: center; height: 22px;">
<asp:ImageButton
                ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_VisualizzaPiccolo.png"
                Style="z-index: 102; left: 440px; top: 141px; " TabIndex="3" 
                ToolTip="Visualizza" CausesValidation="False" OnClientClick="ConfermaEsci();" /></td>
        <td style="vertical-align: top; width: 804px; height: 22px; text-align: left">
            <asp:HiddenField ID="txtid" runat="server" Value="0" />
        </td>
    </tr>
</table>
