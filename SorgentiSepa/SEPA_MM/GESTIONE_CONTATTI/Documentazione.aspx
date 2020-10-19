<%@ Page Title="" Language="VB" MasterPageFile="~/GESTIONE_CONTATTI/HomePage.master"
    AutoEventWireup="false" CodeFile="Documentazione.aspx.vb" Inherits="GESTIONE_CONTATTI_Documentazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript">
         function ConfermaEsci() {
             //            var chiediConferma = window.confirm("Sei sicuro di voler uscire?");
             //            if (chiediConferma == true) {
             //                document.getElementById('confermaUscita').value = '1';
             //            } else {
             //                document.getElementById('confermaUscita').value = '0';
             //            }
             document.getElementById('confermaUscita').value = '1';
         };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:DataGrid runat="server" ID="DataGridElenco" AutoGenerateColumns="False" CellPadding="2"
        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
        GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
        <ItemStyle BackColor="White" />
        <AlternatingItemStyle BackColor="Gainsboro" />
        <Columns>
            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
            <asp:BoundColumn DataField="IDLIVELLO2" HeaderText="IDLIVELLO2" Visible="false">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="IDLIVELLO1" HeaderText="IDLIVELLO1" Visible="false">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></asp:BoundColumn>
            <asp:BoundColumn DataField="CATEGORIA1" HeaderText="CATEGORIA 1"></asp:BoundColumn>
            <asp:BoundColumn DataField="CATEGORIA2" HeaderText="CATEGORIA 2"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="DOCUMENTAZIONE">
                <ItemTemplate>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="3">
                    </asp:CheckBoxList>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
        <EditItemStyle BackColor="White" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" HorizontalAlign="Center" />
        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:DataGrid>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="bottone" ToolTip="Salva" />
            </td>
            <td>
                <asp:Button ID="imgEsci" runat="server" Text="Esci" CssClass="bottone" OnClientClick="ConfermaEsci();"
                    ToolTip="Esci" />
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="confermaUscita" Value="0" ClientIDMode="Static" />
</asp:Content>