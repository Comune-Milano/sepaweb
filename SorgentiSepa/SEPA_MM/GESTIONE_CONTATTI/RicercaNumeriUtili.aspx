<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false" CodeFile="RicercaNumeriUtili.aspx.vb" Inherits="GESTIONE_CONTATTI_RicercaNumeriUtili" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label runat="server" ID="lblTitolo" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 15%">
                        Sede territoriale
                    </td>
                    <td style="width: 85%">
                        <div style="width: 250px; height: 150px; overflow: auto;border:1px solid #507cd1; background-color: #FFFFFF;">
                            <asp:CheckBoxList ID="CheckBoxListSedi" runat="server" AutoPostBack="True">
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Indirizzo
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListIndirizzo" runat="server">
                        </asp:DropDownList>
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
                        Fascia oraria
                    </td>
                    <td>
                    <asp:DropDownList ID="DropDownListFasce" runat="server">
                        </asp:DropDownList>
                        <%--<table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="DropDownListOraInizio" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListMinutiInizio" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    -
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListOraFine" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListMinutifine" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>--%>
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
                <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View3" runat="server">
                        <asp:Button ID="btnRicerca" runat="server" Text="Avvia ricerca" CssClass="bottone" />
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                    <asp:Button ID="btnNuovaRicerca" runat="server" Text="Nuova ricerca" CssClass="bottone" />
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
