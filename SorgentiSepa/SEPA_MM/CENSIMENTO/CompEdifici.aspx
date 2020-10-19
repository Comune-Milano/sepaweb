<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompEdifici.aspx.vb" Inherits="CENSIMENTO_CompEdifici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>Elementi Associati</title>
	</head>
	<body bgcolor="#ffffff">
		<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
                width: 674px; position: absolute; top: 0px">
                <tr>
                    <td style="width: 670px; text-align: left">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Elenco Edifici
                            Trovati&nbsp;<asp:Label ID="LnlNumeroRisultati" runat="server" 
                            Text="Label"></asp:Label>
                        </strong></span><br />
                        <br />
                        <br />
                        <br />
                        <div style="left: 7px; overflow: auto; width: 665px; position: absolute; top: 49px;
                            height: 261px">
            <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" AutoGenerateColumns="False" AllowPaging="True" Font-Size="8pt" Width="643px" PageSize="13" style="z-index: 105; left: 0px; top: 48px" BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" GridLines="None">
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
                                <asp:TemplateColumn HeaderText="COD.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_EDIFICIO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_EDIFICIO") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DENOMINAZIONE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="left: 8px; position: absolute; top: 343px; width: 644px;" Text="Label"
                Visible="False"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            <img src="IMMCENSIMENTO/alert_elencod2.png" style="z-index: 109; left: 112px; position: absolute;
                top: 352px" />
            &nbsp;
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 102; left: 312px; position: absolute; top: 419px" ToolTip="Visualizza" />
            &nbsp; &nbsp; &nbsp;
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="12pt" MaxLength="100" Style="left: 8px; position: absolute;
                top: 319px; z-index: 10;" Width="352px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox>
            <asp:TextBox ID="txtid" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                MaxLength="100" Style="left: 0px; position: absolute; top: 520px" Width="152px" ForeColor="White"></asp:TextBox>
            <asp:TextBox ID="txtdesc" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="None" MaxLength="100" Style="left: 159px; position: absolute; top: 518px"
                Width="152px" ForeColor="White"></asp:TextBox><asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/NuoveImm/Img_IndietroGrande.png"
                Style="z-index: 102; left: 16px; position: absolute; top: 419px" ToolTip="Indietro" />
            <asp:ImageButton ID="btnNuovo" runat="server" ImageUrl="~/NuoveImm/Img_Nuovo.png"
                Style="z-index: 102; left: 538px; position: absolute; top: 419px" ToolTip="Nuovo" />
        </form>
	</body>
</html>

