<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false" CodeFile="GestioneNumeriUtili.aspx.vb" Inherits="GESTIONE_CONTATTI_GestioneNumeriUtili" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function EliminaNumeroUtile(id) {
            var chiediConferma = window.confirm('Vuoi eliminare questo numero?');
            if (chiediConferma == true) {
                document.getElementById('idEliminazione').value = id;
                document.getElementById('CPFooter_btnElimina').click();
            } else {
                document.getElementById('idEliminazione').value = '';
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label runat="server" ID="lblTitolo" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
        <asp:View ID="View1" runat="server">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 15%">
                        Sede territoriale
                    </td>
                    <td style="width: 85%">
                        <div style="width: 250px; height: 150px; overflow: auto; border: 1px solid #507cd1; background-color: #FFFFFF;">
                            <asp:CheckBoxList ID="CheckBoxListSedi" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipologia numero
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListTipologiaNumeroUtile" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Categoria
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListCategoria" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fascia oraria LUN-VEN 9-18
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="LUN 9-18 /// MAR 9-18 /// MER 9-18 /// GIO 9-18 /// VEN 9-18" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fascia oraria LUN-VEN 18-9
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="LUN 18-MAR 9 /// MAR 18-MER 9 /// MER 18-GIO 9 /// GIO 18-VEN 9 /// VEN 18-SAB 9" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fascia oraria SAB-LUN 9-9
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox3" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="SAB 9-LUN 9" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Numero
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxValore" runat="server" MaxLength="100" Width="150px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:DataGrid runat="server" ID="DataGridNumeriUtili" AutoGenerateColumns="False" CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="None" Width="98%" CellSpacing="2"
                AllowPaging="false" PageSize="50" ShowFooter="false">
                <ItemStyle BackColor="White" />
                <AlternatingItemStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO" HeaderText="TIPO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="VALORE" HeaderText="CONTATTO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FASCIA" HeaderText="FASCIA ORARIA"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA SEGNALAZIONE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SEDE_TERRITORIALE" HeaderText="SEDE TERRITORIALE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ELIMINA" HeaderText=""></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="1">
                    <asp:View ID="View3" runat="server">
                        <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="bottone" />
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                        <asp:Button ID="ButtonAggiungi" runat="server" Text="Aggiungi numero utile" CssClass="bottone" />
                        <asp:Button ID="btnElimina" runat="server" Text="Button" Style="display: none" />
                    </asp:View>
                </asp:MultiView>
            </td>
            <td>
                <asp:Button ID="btnEsci" runat="server" Text="Esci" CssClass="bottone" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idEliminazione" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">
        validNavigation = false;
    </script>
</asp:Content>
