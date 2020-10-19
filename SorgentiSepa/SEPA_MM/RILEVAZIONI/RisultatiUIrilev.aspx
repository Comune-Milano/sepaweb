<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="RisultatiUIrilev.aspx.vb" Inherits="RILEVAZIONI_RisultatiUIrilev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Risultati Unità
    <asp:Label ID="lblNumRisult" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnExport" runat="server" CssClass="bottone" Text="Export Excel" />
    <asp:Button ID="btnNewRicerca" runat="server" CssClass="bottone" Text="Nuova Ricerca" />
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="width: 97%;">
                <div id="divOverContentRisultati" style="overflow: auto;">
                    <asp:DataGrid ID="DataGridUnita" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
                        GridLines="None" Width="100%" AllowPaging="True" PageSize="100" onclick="validNavigation=true;"
                        CellPadding="0">
                        <ItemStyle CssClass="itemDataGrid" />
                        <PagerStyle CssClass="pagerDataGrid" Mode="NumericPages" />
                        <AlternatingItemStyle CssClass="alternateDataGrid" />
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FILIALE" HeaderText="SEDE TERR."></asp:BoundColumn>
                            <asp:BoundColumn DataField="STUDIOPROF" HeaderText="STUDIO PROF."></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD.UNITA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="EDIFICIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="INDIRIZZO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CAP" HeaderText="CAP">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RILEVATA" HeaderText="RILEVATA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_CARICAMENTO" HeaderText="DATA INSERIM."></asp:BoundColumn>
                            <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOWNLOAD_SCHEDA" HeaderText="DOWNLOAD SCHEDA" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ELIMINA_SCHEDA" HeaderText="ELIMINA SCHEDA" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="ID_ALL_SFITTO"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="headerDataGrid" />
                        <FooterStyle CssClass="footerDatagrid" />
                    </asp:DataGrid>
                </div>
                <asp:Button ID="btnDownload1" runat="server" Style="display: none;" />
                <asp:Button ID="btnCancella" runat="server" OnClientClick="EliminaElemento();return false;"
                    Style="display: none;" />
                <asp:Button ID="btnEliminaElemento" runat="server" Style="display: none;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
    <asp:HiddenField ID="IDui" runat="server" Value="" />
    <asp:HiddenField ID="IDLotto" runat="server" Value="" />
    <asp:HiddenField ID="LBLID" runat="server" Value="" />
    <asp:HiddenField ID="IDRilievo" runat="server" Value="" />
    <asp:HiddenField ID="IDUtente" runat="server" Value="" />
    <asp:HiddenField ID="HfContenteDivHeight" runat="server" ClientIDMode="Static" Value="150" />
    <script src="Funzioni.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
</asp:Content>
