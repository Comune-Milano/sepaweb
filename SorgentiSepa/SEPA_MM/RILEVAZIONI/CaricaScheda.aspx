<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="CaricaScheda.aspx.vb" Inherits="RILEVAZIONI_CaricaScheda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Upload Schede
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnUploadMassivo" runat="server" CssClass="bottone" Text="Upload Schede Massivo" />
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Text="Lotto:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbLotto" runat="server" AutoPostBack="True" CausesValidation="True"
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Text="Denominazione Edificio:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" CausesValidation="True"
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Text="Indirizzo:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" CausesValidation="True"
                                Width="200px">
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
                <asp:Button ID="btnUpload1" runat="server" Style="display: none;" />
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 97%;">
                <div id="divOverContentRisultati" style="overflow: auto;">
                    <asp:DataGrid ID="DataGridUIDisponibili" runat="server" AutoGenerateColumns="False"
                        CssClass="styleDataGrid" GridLines="None" Width="100%" PageSize="100" onclick="validNavigation=true;"
                        AllowPaging="true">
                        <ItemStyle CssClass="itemDataGrid" />
                        <PagerStyle CssClass="pagerDataGrid" Mode="NumericPages" Position="Bottom" />
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
                            <asp:BoundColumn DataField="LOTTO" HeaderText="DENOMINAZIONE LOTTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
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
                            <asp:BoundColumn DataField="UPLOAD_SCHEDA" HeaderText="UPLOAD SCHEDA" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="headerDataGrid" />
                        <FooterStyle CssClass="footerDatagrid" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
    </table>
    <div class="dialA" id="divInsA" style="visibility: hidden">
    </div>
    <div class="dialB" id="divInsB" style="visibility: hidden">
        <div id="InserimentoP" class="dialLargoTrasparent">
            <table style="width: 100%;" class="tblDiv">
                <tr style="width: 100%;">
                    <td style="text-align: center" class="divTitoloText">
                        Carica Scheda
                    </td>
                </tr>
                <tr>
                    <td class="tbTitolo">
                        <asp:Label ID="lblFileDaCaric" runat="server" Font-Bold="True" Font-Names="arial"
                            Font-Size="10pt" Text='Selezionare il file excel da caricare tramite il pulsante "Sfoglia"'></asp:Label>
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
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtRisultati" runat="server" Width="810px" Height="100px" TextMode="MultiLine"
                                                    Visible="False" MaxLength="5000" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr align="right">
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAllega" runat="server" Text="Allega" CssClass="bottone" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnChiudi" runat="server" CssClass="bottone" Text="Esci" />
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
    <asp:HiddenField ID="uploadMassivo" runat="server" Value="0" />
    <asp:HiddenField ID="superUtente" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
    <asp:HiddenField ID="IDui" runat="server" Value="" />
    <asp:HiddenField ID="IDLotto" runat="server" Value="-1" />
    <asp:HiddenField ID="IDRilievo" runat="server" Value="-1" />
    <asp:HiddenField ID="IDUtente" runat="server" Value="" />
    <asp:HiddenField ID="HfContenteDivHeight" runat="server" ClientIDMode="Static" Value="150" />
    <script src="Funzioni.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
</asp:Content>
