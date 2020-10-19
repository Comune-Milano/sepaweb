<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Alloggio.ascx.vb"
    Inherits="SIRAPER_Tab_Alloggio" %>
<table style="width: 100%; height: 98%;" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 98%;">
            <div id="divAlloggio" style="overflow: auto; height: 425px;">
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <asp:DataGrid ID="dgvAlloggi" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                        GridLines="None" PageSize="50" Width="97%" AllowPaging="True">
                        <ItemStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" Mode="NumericPages"
                            Position="TopAndBottom" CssClass="pager" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CANONE_SOCIALE" HeaderText="CANONE SOCIALE" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ROWNUM" HeaderText="RIGA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CODICE_MIR" HeaderText="CODICE MIR">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CODICE" HeaderText="CODICE ALLOGGIO DELL'ENTE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CONTRATTO" HeaderText="CONTRATTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DETTAGLIO">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDettaglioSirAlloggio" runat="server" ImageUrl="Immagini/patrimonio_immobiliare.png"
                                        OnClick="btnDettaglioSirAlloggio_Click" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </span></strong>
            </div>
        </td>
        <td style="text-align: center; width: 2%;">
            <table>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="Immagini/search-icon.png"
                            ToolTip="Ricerca un'Unita Immobiliare" OnClientClick="RicercaOggetto(2,1);return false;"
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 380px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: bottom;">
                        <asp:ImageButton ID="btnExportXlsAlloggio" runat="server" ImageUrl="Immagini/xlsExport.png"
                            ToolTip="Esporta in formato Excel i risultati" OnClientClick="caricamentoincorso();return ControlModExport();" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div class="dialMiniA" id="divRicercaAlloggioA" style="visibility: hidden">
</div>
<div class="dialMiniB" id="divRicercaAlloggioB" style="visibility: hidden">
    <div class="dialMiniC">
        <table style="width: 100%">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <strong>RICERCA ALLOGGIO</strong>
                </td>
            </tr>
            <tr style="height: 50px">
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                Codice Unit&#224; Immobiliare
                            </td>
                            <td style="width: 10px">
                                &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodiceUnita" runat="server" Font-Names="Arial" Font-Size="8"
                                    CssClass="CssMaiuscolo" Width="200px" MaxLength="25"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 100px">
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td style="text-align: center; width: 5%;">
                                <asp:ImageButton ID="btnCercaAlloggio" runat="server" ImageUrl="Immagini/Search_big.png"
                                    ToolTip="Ricerca un'Unita Immobiliare" OnClientClick="caricamentoincorso();" />
                            </td>
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                            <td style="text-align: center; width: 5%;">
                                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="Immagini/logout.png" ToolTip="Esci dalla Ricerca"
                                    OnClientClick="RicercaOggetto(2,0);return false;" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td style="text-align: center">
                                CERCA
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="text-align: center">
                                ESCI
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
