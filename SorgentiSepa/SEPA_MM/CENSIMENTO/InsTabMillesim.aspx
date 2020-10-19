<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InsTabMillesim.aspx.vb" Inherits="CENSIMENTO_InsTabMillesim" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>RisultatoRicercaD</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body bgColor="white">
		<form id="Form1" method="post" runat="server">
			<asp:label id="LBLID" style="Z-INDEX: 100; LEFT: 208px; POSITION: absolute; TOP: 449px" runat="server" Width="78px" Height="21px" Visible="False">Label</asp:label>
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                Style="z-index: 101; left: 552px; position: absolute; top: 211px" 
                ToolTip="Esci" TabIndex="4" />
            <asp:ImageButton ID="BtnElimina" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/minus_icon.png"
                Style="z-index: 103; left: 603px; position: absolute; top: 68px" 
                ToolTip="Elimina elemento selezionato dalla lista" TabIndex="2" 
                Visible="False" />
            <asp:label id="LBLPROGR" style="Z-INDEX: 104; LEFT: 152px; POSITION: absolute; TOP: 448px" runat="server" Width="57px" Height="23px" Visible="False">Label</asp:label>
                        <asp:label id="Label2" runat="server" Font-Names="Arial" Font-Bold="True" style="z-index: 107; left: 8px; position: absolute; top: 448px">Nessuna selezione</asp:label>
            <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/40px-Crystal_Clear_action_edit_add.png"
                Style="z-index: 103; left: 603px; position: absolute; top: 39px" 
                Height="16px" Width="16px" ToolTip="Aggiungi elemento alla lista" 
                TabIndex="1" Visible="False" />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Style="z-index: 107;
                left: 8px; position: absolute; top: 8px">TABELLE MILLESIMIALI</asp:Label>
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 10px; position: absolute;
                top: 255px" Width="352px"></asp:TextBox>
            <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                MaxLength="100" Style="left: 427px; position: absolute; top: 257px; background-color: white;" Width="21px" ForeColor="White" TabIndex="-1"></asp:TextBox>
            <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" MaxLength="100" Style="left: 400px; position: absolute; top: 257px; background-color: white;"
                Width="14px" ForeColor="White" TabIndex="-1"></asp:TextBox>
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_VisualizzaPiccolo.png"
                
                Style="z-index: 101; left: 472px; position: absolute; top: 210px; right: 350px;" 
                ToolTip="Visualizza" TabIndex="3" />
            <div style="left: 11px; vertical-align: top; width: 580px; position: absolute; top: 39px;
                height: 166px; text-align: left; overflow: auto;">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                    AutoGenerateColumns="False" Font-Size="8pt" Width="563px" 
                    style="z-index: 105; left: 8px; top: 32px; vertical-align: top; text-align: left;" 
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                    Font-Strikeout="False" Font-Underline="False" BorderColor="Black" 
                    GridLines="None">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="MediumBlue" HorizontalAlign="Left"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="ROWNUM" Visible="False" DataField="ROWNUM"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TIPO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DETTAGLI">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE_TABELLA") %>'></asp:Label>
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
            &nbsp;<br />
            <br />
            <br />
            <br />
            <p>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" 
                    Style="left: 12px; position: absolute; top: 230px; width: 502px; right: 368px;" Text="Label"
                Visible="False"></asp:Label>
                        </p>
        </form>
	</body>
</html>