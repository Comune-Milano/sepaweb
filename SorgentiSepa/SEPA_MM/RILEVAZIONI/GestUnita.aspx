<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="GestUnita.aspx.vb" Inherits="RILEVAZIONI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Gestione Unità
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                    Text="Rilevazione Stato di fatto:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cmbRilievo" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="10pt" Style="margin-left: 0px" AutoPostBack="True" CausesValidation="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="width: 97%;">
        <tr>
            <td style="width: 97%;">
                <div id="divOverContentRisultati" style="overflow: auto;">
                    <asp:DataGrid ID="DataGridUnita" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
                        GridLines="None" Width="97%" AllowPaging="True" PageSize="100" onclick="validNavigation=true;"
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
                        </Columns>
                        <HeaderStyle CssClass="headerDataGrid" />
                        <FooterStyle CssClass="footerDatagrid" />
                    </asp:DataGrid>
                </div>
            </td>
            <td style="width: 3%" valign="top">
                <input id="btnAggiungi" class="minibottone" type="button" value="Aggiungi" onclick="MostraDiv();document.getElementById('CPContenuto_TextBox1').value = '1';" /><br />
                <asp:Button ID="btnElimina" runat="server" CssClass="minibottone" Text="Elimina"
                    OnClientClick="EliminaElemento();return false;" />
                <asp:Button ID="btnEliminaElemento" runat="server" Text="Button" CssClass="bottone"
                    Style="display: none;" />
            </td>
        </tr>
    </table>
    <div class="dialA" id="divInsA" style="visibility: hidden;">
    </div>
    <div class="dialB" id="divInsB" style="visibility: hidden;">
        <div id="InserimentoP" class="dialLargoTrasparent">
            <table style="width: 100%;" class="tblDiv">
                <tr style="width: 100%;">
                    <td style="text-align: center" class="divTitoloText">
                        Carica Unità
                    </td>
                </tr>
                <tr>
                    <td class="tbTitolo">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                            Text='Selezionare il file* txt da caricare tramite il pulsante "Sfoglia".'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 95%;">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Height="20" Width="97%" size="60" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="8pt" Text='*Il file deve essere strutturato come mostrato in Figura 1, con la lista dei codici delle unità. Massimo 1000 elementi per file.'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/NuoveImm/EsempiocodUI.PNG" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 120px;">
                                    <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="True"
                                        Text='Figura 1'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr align="right">
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSalvaFile" runat="server" Text="Salva" CssClass="bottone" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnChiudi" runat="server" CssClass="bottone" Text="Esci" OnClientClick="document.getElementById('CPContenuto_TextBox1').value='0';" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
    <asp:HiddenField ID="LBLID" runat="server" Value="" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="HfContenteDivHeight" runat="server" ClientIDMode="Static" Value="150" />
    <script src="Funzioni.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
</asp:Content>
