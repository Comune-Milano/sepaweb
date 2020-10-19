<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StatiAppuntamenti.aspx.vb" Inherits="SEGNALAZIONI_Agenda_StatiAppuntamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stati appuntamenti</title>
    <link href="Style/StilePaginaCall.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Elimina(id) {
            var chiediConferma = window.confirm('Confermi di voler eliminare lo stato selezionato?');
            if (chiediConferma) {
                document.getElementById('StatoSelezionato').value = id;
            };
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="titolo">
        <asp:Label ID="TitoloPagina" runat="server" Text="Appuntamenti stati"></asp:Label>
    </div>
    <div id="contenuto">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <asp:DataGrid runat="server" ID="DataGridStatiAppuntamenti" AutoGenerateColumns="False"
                    CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="None" Width="100%" CellSpacing="2">
                    <ItemStyle BackColor="#EFF3FB" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="ELIMINA" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButtonElimina" runat="server" OnClick="ImageButton1_Click"
                                    ImageUrl="../../NuoveImm/Elimina.png" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <EditItemStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:DataGrid>
                <asp:Label ID="TestoErrore" runat="server" Text="Label"></asp:Label>
                <asp:HiddenField ID="StatoSelezionato" runat="server" />
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td>
                            Descrizione
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="TextBoxDescrizione" MaxLength="50" Width="100%" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
    <div id="footer" style="text-align:right">
        <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
            <asp:View ID="View3" runat="server">
                <br />
                <asp:Button ID="ButtonNuovoStato" runat="server" Text="Nuovo Stato Appuntamento"
                    CssClass="bottone" />
            </asp:View>
            <asp:View ID="View4" runat="server">
                <br />
                <asp:Button ID="ButtonIndietro" runat="server" Text="Indietro" CssClass="bottone" />
                <asp:Button ID="ButtonInserisciStatoAppuntamento" runat="server" Text="Inserisci Stato Appuntamento"
                    CssClass="bottone" />
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>