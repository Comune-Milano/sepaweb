<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="SituazioneIntervento.aspx.vb" Inherits="GESTIONE_CONTATTI_SituazioneIntervento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lbTitolo" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Data Inizio" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInizio" runat="server" MaxLength="10" Width="70px" onfocus="this.select()"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Data Fine" Width="80px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFine" runat="server" MaxLength="10" Width="70px" onfocus="this.select()"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Tipologia Intervento"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipo" runat="server" Style="width: 300px;">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Operatore" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbOpSegnalante" runat="server" Style="width: 300px;">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Sede territoriale" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbStruttura" runat="server" Style="width: 300px;">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStato" runat="server" Text="Stato Segnalazione" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbstato" runat="server" Style="width: 300px;">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblApertoDa" runat="server" Text="Aperto Da" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApertoDa" runat="server" MaxLength="10" Width="70px" onfocus="this.select()"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSpiega" runat="server" Style="text-align: left"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFiltri" runat="server" Style="text-align: left"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid runat="server" ID="DataGridRptSituaz" AutoGenerateColumns="False" CellPadding="2"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            GridLines="None" Width="98%" CellSpacing="2" AllowPaging="false" PageSize="50"
                            ShowFooter="false">
                            <ItemStyle BackColor="White" />
                            <AlternatingItemStyle BackColor="Gainsboro" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="N° SEGN."></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_ORA_RICHIESTA" HeaderText="DATA APERTURA" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_IN_CARICO" HeaderText="DATA PRESA IN CARICO" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_CHIUSURA" HeaderText="DATA CHIUSURA" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO" ItemStyle-HorizontalAlign="Left">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE_RIC" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left">
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
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
        <asp:View ID="View3" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnCerca" runat="server" CssClass="bottone" ToolTip="Avvia Ricerca"
                            Text="Avvia ricerca" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnExport" runat="server" CssClass="bottone" Text="Esporta in Excel"
                            ToolTip="Esporta in Excel" />
                    </td>
                    <td>
                        <asp:Button ID="btnIndietro" runat="server" CssClass="bottone" Text="Indietro" ToolTip="Indietro" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
