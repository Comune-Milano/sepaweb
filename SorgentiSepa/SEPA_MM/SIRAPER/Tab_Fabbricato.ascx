<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Fabbricato.ascx.vb"
    Inherits="SIRAPER_Tab_Fabbricato" %>
<table style="width: 100%; height: 98%;" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 98%;">
            <div id="divFabbricato" style="overflow: auto; height: 425px;">
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <asp:DataGrid ID="dgvFabbricati" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                        GridLines="None" PageSize="15" Width="200%" AllowPaging="True">
                        <ItemStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" Mode="NumericPages"
                            Position="TopAndBottom" CssClass="pager" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ROWNUM" HeaderText="RIGA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_FABBRICATO" HeaderText="CODICE FABBRICATO DELL'ENTE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_PROPRIETA" HeaderText="TIPO PROPRIETA' DEL FABBRICATO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="GESTIONE*">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlgestionedificio" runat="server" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.GESTIONE") %>'
                                        Font-Names="Arial" Font-Size="8">
                                        <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="TOTALE ALER"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="TOTALE COMUNE"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="TOTALE ALTRO"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="MISTA"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="COD_ISTAT_COMUNE" HeaderText="CODICE ISTAT COMUNE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="UBICAZIONE DEL FABBRICATO*">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlubicazionedificio" runat="server" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.UBICAZIONE") %>'
                                        Font-Names="Arial" Font-Size="8">
                                        <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="COMUNE CON POPOLAZIONE SUPERIORE A 20000 ABITANTI, ZONA AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="POPOLAZIONE SUPERIORE A 20000 ABITANTI, ZONA EDIFICATA PERIFERICA"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="POPOLAZIONE SUPERIORE A 20000 ABITANTI, ZONA COMPRESA TRA QUELLA PERIFERICA ED IL CENTRO STORICO"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="POPOLAZIONE SUPERIORE A 20000 ABITANTI, ZONE DI PREGIO PART. SITE NELLA ZONA EDIFIC. PERIF. O NELLA ZONA AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="POPOLAZIONE SUPERIORE A 20000 ABITANTI, ZONA CENTRO STORICO"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="COMUNE CON POPOLAZIONE NON SUPERIORE A 20000 ABITANTI, ZONA AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="COMUNE CON POPOLAZIONE NON SUPERIORE A 20000 ABITANTI, CENTRO EDIFICATO"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="COMUNE CON POPOLAZIONE NON SUPERIORE A 20000 ABITANTI, CENTRO STORICO"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="EDIFICIO DEGRADATO"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="COMUNE SENZA ZONIZZAZIONE"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="NON SOGGETTO ALLA LEGGE 392/78"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COEFFICIENTE UBICAZIONE (L.R n°27/2009)*">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlcoeffubicazione" runat="server" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.COEFF_UBICAZIONE") %>'
                                        Font-Names="Arial" Font-Size="8">
                                        <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="COMUNI CON POPOLAZIONE SUP. A 400.000 - PER LA ZONA EDIFICATA PERIFERICA ED AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="COMUNI CON POPOLAZIONE SUP. A 400.000 - PER LA ZONA EDIFICATA COMPRESA FRA QUELLA PERIFERICA E IL CENTRO STORICO"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="COMUNI CON POPOLAZIONE SUP. A 400.000 - PER LE ZONE DI PREGIO PARTICOLARE SITE NELLA ZONA EDIFICATA PERIFERICA O NELLA ZONA AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="COMUNI CON POPOLAZIONE SUP. A 400.000 - PER IL CENTRO STORICO"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="COMUNI CAPOLUOGO - PER LA ZONA AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="COMUNI CAPOLUOGO - PER LA ZONA EDIFICATA PERIFERICA"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="COMUNI CAPOLUOGO - PER LA ZONA EDIFICATA COMPRESA TRA QUELLA PERIFERICA E IL CENTRO STORICO"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="COMUNI CAPOLUOGO - PER LA ZONA EDIFICATA COMPRESA TRA QUELLA PERIFERICA E IL CENTRO STORICO AI SENSI DELL'ART. 3 COMMA 6"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="COMUNI CAPOLUOGO - PER LE ZONE DI PREGIO PARTICOLARE SITE NELLA ZONA EDIFICATA PERIFERICA O NELLA ZONA AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="COMUNI CAPOLUOGO - PER IL CENTRO STORICO"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="COMUNI CON POPOLAZIONE SUP. A 20.000 - PER LA ZONA AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="COMUNI CON POPOLAZIONE SUP. A 20.000 - PER LA ZONA EDIFICATA PERIFERICA"></asp:ListItem>
                                        <asp:ListItem Value="13" Text="COMUNI CON POPOLAZIONE SUP. A 20.000 - PER LA ZONA EDIFICATA COMPRESA FRA QUELLA PERIFERICA E IL CENTRO STORICO"></asp:ListItem>
                                        <asp:ListItem Value="14" Text="COMUNI CON POPOLAZIONE SUP. A 20.000 - PER LE ZONE DI PREGIO PARTICOLARE SITE NELLA ZONA EDIFICATA PERIFERICA O NELLA ZONA AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="15" Text="COMUNI CON POPOLAZIONE SUP. A 20.000 - PER IL CENTRO STORICO"></asp:ListItem>
                                        <asp:ListItem Value="16" Text="COMUNI CON POPOLAZIONE NON SUP. AI 20.000 - PER LA ZONA AGRICOLA"></asp:ListItem>
                                        <asp:ListItem Value="17" Text="COMUNI CON POPOLAZIONE NON SUP. AI 20.000 - PER IL CENTRO EDIFICATO"></asp:ListItem>
                                        <asp:ListItem Value="18" Text="COMUNI CON POPOLAZIONE NON SUP. AI 20.000 - PER IL CENTRO STORICO"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ANNO_COSTRUZIONE" HeaderText="ANNO DI COSTRUZIONE DEL FABBRICATO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ANNO_RISTRUTTURAZIONE" HeaderText="ANNO DI RISTRUTTURAZIONE DEL FABBRICATO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO DEL FABBRICATO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA'">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NUM_ALL_RISCATTO" HeaderText="NUMERO ALLOGGI A RISCATTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
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
                            ToolTip="Ricerca un'Unita Immobiliare" OnClientClick="RicercaOggetto(1,1);return false;" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 380px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:ImageButton ID="btnExportXlsFabbricato" runat="server" ImageUrl="Immagini/xlsExport.png"
                            ToolTip="Esporta in formato Excel i risultati" OnClientClick="caricamentoincorso();return ControlModExport();" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div class="dialMiniA" id="divRicercaFabbricatoA" style="visibility: hidden">
</div>
<div class="dialMiniB" id="divRicercaFabbricatoB" style="visibility: hidden">
    <div class="dialMiniC">
        <table style="width: 100%">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <strong>RICERCA FABBRICATO</strong>
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
                                Codice Fabbricato
                            </td>
                            <td style="width: 10px">
                                &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodiceFabbricato" runat="server" Font-Names="Arial" Font-Size="8"
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
                                <asp:ImageButton ID="btnCercaFabbricato" runat="server" ImageUrl="Immagini/Search_big.png"
                                    ToolTip="Ricerca un Fabbricato" OnClientClick="caricamentoincorso();" />
                            </td>
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                            <td style="text-align: center; width: 5%;">
                                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="Immagini/logout.png" ToolTip="Esci dalla Ricerca"
                                    OnClientClick="RicercaOggetto(1,0);return false;" />
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
