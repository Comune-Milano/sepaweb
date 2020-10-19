<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoRicAbbinamento.aspx.vb" Inherits="ASS_RisultatoRicAbbinamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Domande Idonee per Abbinamento Alloggi</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server" defaultbutton="btnVisualizza" 
    defaultfocus="DataGrid1">
    <div>
        <asp:Label ID="LBLID" runat="server" Height="21px" Style="z-index: 100; left: 88px;
            position: absolute; top: 405px" Visible="False" Width="78px">Label</asp:Label>
        <asp:Label ID="LBLPROGR" runat="server" Height="23px" Style="z-index: 101; left: 27px;
            position: absolute; top: 403px" Visible="False" Width="57px">Label</asp:Label>
                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" Width="662px" style="z-index: 103; left: 2px; position: absolute; top: 87px" BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" GridLines="None">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Navy" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PG" HeaderText="PG" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="POS">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PROTOCOLLO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
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
                                    <asp:Label ID="Label1" runat="server" Font-Names="ARIAL" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
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
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ISBARC/R">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ISBARC_R") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ISEE">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REDDITO_ISEE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Style="z-index: 100;
                                        left: 0px; position: static; top: 0px" Width="51px">Seleziona</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                        Domande Trovate&nbsp; </strong>
                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
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
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
                    <asp:Label ID="Label5" runat="server" BorderColor="Black" BorderStyle="None" Font-Bold="True"
                        Font-Names="Arial" Font-Size="9pt" ForeColor="Red" Style="z-index: 104; left: 44px;
                        position: absolute; top: 55px">LE DOMANDE CON OFFERTA IN CORSO NON SONO VISUALIZZATE</asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" 
            Font-Names="Arial" 
            style="z-index: 105; left: 4px; position: absolute; top: 407px; width: 659px;" 
            Font-Size="12pt">Nessuna selezione</asp:Label>
        <img src="../ImmMaschere/alert_elencodom.gif" style="z-index: 110; left: 13px; position: absolute;
            top: 438px" />
        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            Style="z-index: 106; left: 232px; position: absolute; top: 491px" 
            ToolTip="Visualizza" TabIndex="1" />
        <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
            Style="z-index: 107; left: 404px; position: absolute; top: 491px" 
            ToolTip="Nuova Ricerca" TabIndex="2" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 108; left: 536px; position: absolute; top: 491px" 
            ToolTip="Home" TabIndex="3" />
    
                    <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" Height="86px" 
            PageSize="12" style="z-index: 108; left: 4px; position: absolute; top: 77px; width: 658px;" 
            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Strikeout="False" Font-Underline="False" GridLines="None" 
            TabIndex="1" Visible="False">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PG" HeaderText="PG" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PG">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COGNOME">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NOME">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="AI">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.AI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RU">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RU") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RI">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.AI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RI") %>'></asp:Label>
                                    <asp:LinkButton ID="LinkButton4" runat="server" 
                                        CommandName="Update" Style="z-index: 100;
                                        left: 0px; position: static; top: 0px" Width="52px">Seleziona</asp:LinkButton>
                                
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Style="z-index: 100;
                                        left: 0px; position: static; top: 0px" Width="52px">Seleziona</asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </asp:DataGrid>
    
    </div>
    </form>
        <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
</body>
</html>
