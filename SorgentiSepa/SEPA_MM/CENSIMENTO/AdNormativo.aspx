<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdNormativo.aspx.vb" Inherits="CENSIMENTO_AdNormativo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<title>Tipologie Adeguamento</title>
	</head>
	<body bgColor="white">
    <form id="form1" runat="server">
            &nbsp; &nbsp;&nbsp;
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                Style="z-index: 101; left: 446px; position: absolute; top: 170px" 
                ToolTip="Esci" TabIndex="3" />
            &nbsp;
            <asp:ImageButton ID="BtnElimina" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/minus_icon.png"
                
                Style="z-index: 103; left: 492px; position: absolute; top: 70px; width: 18px;" 
                ToolTip="Elimina elemento selezionato dalla lista" TabIndex="2" />
            <asp:label id="LBLPROGR" style="Z-INDEX: 104; LEFT: 192px; POSITION: absolute; TOP: 472px" runat="server" Width="57px" Height="23px" Visible="False">Label</asp:label>
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 8px; position: absolute;
                top: 169px" Width="352px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox>
            <asp:TextBox ID="txtid" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                MaxLength="100" Style="left: 168px; position: absolute; top: 216px" Width="152px" ForeColor="White" TabIndex="-1"></asp:TextBox>
            <asp:TextBox ID="txtdesc" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="None" MaxLength="100" Style="left: 9px; position: absolute; top: 215px"
                Width="152px" ForeColor="White" TabIndex="-1"></asp:TextBox>
                        <asp:label id="Label2" runat="server" Font-Names="Arial" Font-Bold="True" style="z-index: 107; left: 24px; position: absolute; top: 472px">Nessuna selezione</asp:label>
            <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/40px-Crystal_Clear_action_edit_add.png"
                
                Style="z-index: 103; left: 491px; position: absolute; top: 38px; bottom: 582px;" 
                ToolTip="Aggiungi elemento alla lista" TabIndex="1" />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Style="z-index: 107;
                left: 8px; position: absolute; top: 8px">TIPOLOGIE ADEGUAMENTO</asp:Label>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="left: 8px; position: absolute; top: 196px" Text="Label"
                Visible="False" Width="448px"></asp:Label>
        <div style="left: 8px; vertical-align: top; overflow: auto; width: 480px; position: absolute;
            top: 39px; height: 128px; text-align: left">
            <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                AutoGenerateColumns="False" Font-Size="8pt" Width="462px" PageSize="4" 
                style="z-index: 105; left: 8px; top: 32px" BackColor="White" Font-Bold="False" 
                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                Font-Underline="False" BorderColor="Black" GridLines="None">
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
                                <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ID_ADEGUAMENTO" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:TextBox>
                                    </EditItemTemplate>
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
        </form>
	</body>
</html>