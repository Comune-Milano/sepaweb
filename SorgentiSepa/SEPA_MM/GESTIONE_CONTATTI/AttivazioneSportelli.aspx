<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="AttivazioneSportelli.aspx.vb" Inherits="GESTIONE_CONTATTI_AttivazioneSportelli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label runat="server" ID="lblTitolo" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td style="width: 15%">
                            Sede territoriale
                        </td>
                        <td style="width: 85%">
                            <asp:DropDownList ID="DropDownListSedeTerritoriale" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sportello 1
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sportello 2
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sportello 3
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox3" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sportello 4
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox4" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sportello 5
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox5" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sportello 6
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox6" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <asp:DataGrid runat="server" ID="DataGridAttivazioneSportelli" AutoGenerateColumns="False"
                    CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="None" Width="98%" CellSpacing="2" AllowPaging="false" PageSize="50"
                    ShowFooter="false">
                    <ItemStyle BackColor="White" />
                    <AlternatingItemStyle BackColor="Gainsboro" />
                    <Columns>
                        <asp:BoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SPORTELLO" HeaderText="SPORTELLO" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ATTIVO" HeaderText="ATTIVO" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="bottone" />
            </td>
            <td>
                <asp:Button ID="btnEsci" runat="server" Text="Esci" CssClass="bottone" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        validNavigation = false;
    </script>
</asp:Content>
