<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoRicAss.aspx.vb" Inherits="ASS_RisultatoRicOfferta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Offerte Alloggio</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server" defaultbutton="btnVisualizza" 
    defaultfocus="DataGrid1">
    <div>
        <div>
            <asp:Label ID="lblScad" runat="server" Height="23px" Style="z-index: 102; left: 180px;
                position: absolute; top: 388px" Visible="False" Width="57px">Label</asp:Label>
            &nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                            Assegnazioni Trovate&nbsp; </strong>
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
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
                        <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                            
                            Style="border: 1px solid white; z-index: 5; left: 8px; position: absolute; top: 416px; width: 589px;" 
                            meta:resourcekey="TextBox3Resource1" Text="Nessuna Selezione"></asp:TextBox>
                        <br />
                        <asp:HiddenField ID="LBLID" runat="server" />
                        <asp:HiddenField ID="LBLPROGR" runat="server" />
                        <asp:HiddenField ID="lblIdUnita" runat="server" />
                        <asp:HiddenField ID="CFPIVA" runat="server" />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 103; left: 241px; position: absolute; top: 477px" 
                ToolTip="Visualizza" TabIndex="1" />
            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                Style="z-index: 104; left: 406px; position: absolute; top: 477px" 
                ToolTip="Nuova Ricerca" TabIndex="2" />
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 105; left: 538px; position: absolute; top: 477px" 
                ToolTip="Home" TabIndex="3" />
                                            <asp:DataGrid ID="DataGrid1" runat="server" 
                AllowPaging="True" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" Width="623px" 
            PageSize="12" style="z-index: 107; left: 4px; position: absolute; top: 77px" 
            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Strikeout="False" Font-Underline="False" GridLines="None" TabIndex="1">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                        <Columns>
                            <asp:BoundColumn DataField="N_OFFERTA" HeaderText="ID" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" ReadOnly="True" 
                                Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_DOMANDA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="id_unita" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cf_piva" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="OFFERTA">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.N_OFFERTA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA ASSEG.">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_ASSEGNAZIONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COD.UNITA">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COGNOME/R.S.">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME_RS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NOME">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </asp:DataGrid>
        </div>
    
    </div>
    </form>
        <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
</body>
</html>
