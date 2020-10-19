<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultaiUtenzeEdifici.aspx.vb" Inherits="MANUTENZIONI_RisultaiUtenzeEdifici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="z-index: 2; left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg);
            width: 674px; position: absolute; top: 0px">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Risultati
                        Ricerca 
                        <asp:Label ID="LnlNumeroRisultati" runat="server" Text="Label"></asp:Label></strong></span><br />
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
                    <div style="z-index: 10; left: 8px; overflow: auto; width: 656px; position: absolute;
                        top: 49px; height: 293px">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" Style="z-index: 101; left: 3px; top: 65px" Width="864px" 
                            GridLines="Horizontal" PageSize="15">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Mode="NumericPages" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESC_FORNITORE" HeaderText="DESC_FORNITORE" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_IMMOBILE" HeaderText="ID_IMMOBILE" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="FORNITORE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESC_FORNITORE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CONTATORE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTATORE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CONTRATTO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                            Text="Annulla"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                            Text="Modifica">Seleziona</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                        </asp:DataGrid></div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Height="16px" Style="z-index: 104; left: 15px; position: absolute;
                        top: 482px" Visible="False" Width="648px"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
            Style="z-index: 10; left: 9px; position: absolute; top: 343px" Width="632px">Nessuna Selezione</asp:TextBox>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 539px; position: absolute; top: 452px" ValidationGroup="Home" />
        <img alt="Elenco2" src="Immagini/alert_elencod2.gif" style="z-index: 109; left: 104px;
            position: absolute; top: 384px" />
        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            Style="z-index: 106; left: 311px; position: absolute; top: 453px" ValidationGroup="Visualizza" />
        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 327px; position: absolute; top: 454px"
            Width="2px"></asp:TextBox>
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 208px; position: absolute; top: 422px" Width="9px"></asp:TextBox>
        <asp:TextBox ID="txtIdEdif" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 240px; position: absolute; top: 415px"
            Width="9px"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>
