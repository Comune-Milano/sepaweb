<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoDichiarazioni.aspx.vb" Inherits="VSA_RisultatoDichiarazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>RisultatoDichiarazioni</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body bgColor="#f2f5f1">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="LBLID" style="Z-INDEX: 100; LEFT: 108px; POSITION: absolute; TOP: 399px" runat="server" Height="21px" Width="78px" Visible="False">Label</asp:Label>
			<asp:Label id="LBLPROGR" style="Z-INDEX: 101; LEFT: 206px; POSITION: absolute; TOP: 399px" runat="server" Height="23px" Width="57px" Visible="False">Label</asp:Label>
            &nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                            Dichiarazioni Complete </strong>
                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                        </span><br />
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
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:TextBox ID="TextBox6" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                            Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                            border-bottom: white 1px solid; left: -1px; top: 45px;" Width="633px" 
                            BackColor="#F7F7F7">Nessuna Selezione</asp:TextBox>
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
						<asp:Label id="Label1" runat="server" Font-Names="Arial" Font-Bold="True" style="z-index: 104; left: 7px; position: absolute; top: 59px">hai cercato</asp:Label><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" Font-Names="Arial" Font-Size="8pt" Width="664px" style="z-index: 105; left: 7px; position: absolute; top: 86px" BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" GridLines="None">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PROGR" HeaderText="PROGRESSIVO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PG" HeaderText="PROTOCOLLO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="COGNOME" HeaderText="COGNOME">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="NOME" HeaderText="NOME">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA_NASCITA" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
								<asp:TemplateColumn HeaderText="DICHIARAZIONE">
									<ItemTemplate>
										<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:TextBox>
									</EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="DATA PR.">
									<ItemTemplate>
										<asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PG") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PG") %>'>
										</asp:TextBox>
									</EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="CODICE FISCALE">
									<ItemTemplate>
										<asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
										</asp:TextBox>
									</EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="COGNOME">
									<ItemTemplate>
										<asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:TextBox>
									</EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="NOME">
									<ItemTemplate>
										<asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:TextBox>
									</EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
								</asp:TemplateColumn>
								<asp:TemplateColumn>
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
						</asp:datagrid><asp:label id="lbl1" runat="server" 
                Font-Names="Arial" Font-Size="XX-Small" Font-Bold="True" BorderStyle="None" 
                BorderColor="Black" ForeColor="Blue" 
                style="z-index: 106; left: 98px; position: absolute; top: 344px">Selezionare il componente al quale verrà intestata la domanda e premere il pulsante "VISUALIZZA"</asp:label>
            <asp:ImageButton ID="Button3" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 108; left: 402px; position: absolute; top: 436px" 
                ToolTip="Visualizza" />
            <asp:ImageButton ID="Button2" runat="server" ImageUrl="~/NuoveImm/Img_Home.png" Style="z-index: 111;
                left: 533px; position: absolute; top: 436px" ToolTip="Home" />
        </form>
            <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
	</body>
</html>
