<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="ScaricaScheda.aspx.vb" Inherits="RILEVAZIONI_ScaricaScheda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Download Schede
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                    Text="St. Professionale:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbUtenti" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="10pt" Style="margin-left: 0px" AutoPostBack="True" CausesValidation="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                    Text="Lotto:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbLotto" runat="server" Font-Bold="True" Font-Names="arial"
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
                </td>
                <td valign="top">
                    <asp:Button ID="btnDownload1" runat="server" Style="display: none;" />
                </td>
            </tr>
            <tr valign="top">
                <td style="width: 97%;">
                    <div id="divOverContentRisultati" style="overflow: auto; width: 97%;">
                        <asp:DataGrid ID="DataGridSchede" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
                            GridLines="None" Width="97%" AllowPaging="True" PageSize="30" onclick="validNavigation=true;">
                            <ItemStyle CssClass="itemDataGrid" />
                            <PagerStyle CssClass="pagerDataGrid" Mode="NumericPages" Position="Top" />
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
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DENOMINAZIONE LOTTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="UTENTE" HeaderText="ST. PROFESS."></asp:BoundColumn>
                                <asp:BoundColumn DataField="DOWNLOAD_SCHEDA" HeaderText="DOWNLOAD SCHEDA" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="headerDataGrid" />
                            <FooterStyle CssClass="footerDatagrid" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="dialA" id="divInsA" style="visibility: hidden">
    </div>
    <div class="dialB" id="divInsB" style="visibility: hidden">
        <div id="InserimentoP" class="dialLargoTrasparent2">
            <table style="width: 100%;" class="tblDiv">
                <tr style="width: 100%;">
                    <td style="text-align: center" class="divTitoloText">
                        Elenco Unità
                    </td>
                </tr>
                <tr>
                    <td class="tbTitolo">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                            Text="Selezionare le unità per cui scaricare la scheda di rilievo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Denominazione Edificio"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Indirizzo"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" CausesValidation="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" CausesValidation="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <div id="div1" style="overflow: auto; height: 400px;">
                            <asp:DataGrid ID="DataGridUIDisponibili" runat="server" AutoGenerateColumns="False"
                                CssClass="styleDataGrid" GridLines="None" Width="97%" PageSize="100" onclick="validNavigation=true;">
                                <ItemStyle CssClass="itemDataGrid" />
                                <PagerStyle CssClass="pagerDataGrid" Mode="NumericPages" />
                                <AlternatingItemStyle CssClass="alternateDataGrid" />
                                <Columns>
                                    <asp:TemplateColumn Visible="True">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChSelezionato" runat="server" />
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD. UNITA IMMOBILIARE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CAP" HeaderText="CAP"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="headerDataGrid" />
                                <FooterStyle CssClass="footerDatagrid" />
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp;
                    </td>
                </tr>
                <tr align="right">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSelezionaTutti" runat="server" CssClass="bottone" Text="Seleziona Tutti" />
                                </td>
                                <td>
                                    <asp:Button ID="btnDeselezionaTutti" runat="server" CssClass="bottone" Text="Deseleziona Tutti" />
                                </td>
                                <td>
                                    <asp:Button ID="btnScaricaScheda" runat="server" CssClass="bottone" Text="Download Scheda" />
                                </td>
                                <td>
                                    <asp:Button ID="btnChiudi" runat="server" CssClass="bottone" Text="Esci" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="LBLID" runat="server" Value="" />
    <asp:HiddenField ID="LBLNomeLotto" runat="server" Value="" />
    <asp:HiddenField ID="HfContenteDivHeight" runat="server" ClientIDMode="Static" Value="250" />
    <script src="Funzioni.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
</asp:Content>
