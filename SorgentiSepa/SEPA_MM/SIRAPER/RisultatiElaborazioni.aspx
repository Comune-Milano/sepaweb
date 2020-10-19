<%@ Page Title="" Language="VB" MasterPageFile="BasePage.master" AutoEventWireup="false"
    CodeFile="RisultatiElaborazioni.aspx.vb" Inherits="SIRAPER_RisultatiElaborazioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <strong>RISULTATI ELABORAZIONI SIRAPER</strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr style="height: 60%">
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div id="divSearchImmobili" style="overflow: auto; width: 100%; height: 450px;">
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:DataGrid ID="dgvElaborazioni" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                    GridLines="None" PageSize="100" Style="z-index: 105; left: 193px; top: 54px"
                                    Width="98%" AllowPaging="True">
                                    <ItemStyle BackColor="#EFF3FB" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                        Position="TopAndBottom" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="#"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SIGLA_ENTE" HeaderText="SIGLA ENTE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPO_ENTE" HeaderText="TIPO ENTE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="COD_FISCALE_ENTE" HeaderText="CODICE FISCALE ENTE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="P_IVA_ENTE" HeaderText="PARTITA IVA ENTE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="RAG_SOCIALE" HeaderText="RAGIONE SOCIALE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_RIFERIMENTO" HeaderText="DATA DI RIFERIMENTO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ANNO_RIFERIMENTO" HeaderText="ANNO DI RIFERIMENTO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_TRASMISSIONE" HeaderText="DATA DI TRASMISSIONE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="VERSIONE" HeaderText="VERSIONE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_ELABORAZIONE" HeaderText="DATA ELABORAZIONE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="White" />
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </span></strong>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnExportXls" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 95%">
                            <asp:TextBox ID="txtmia" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" MaxLength="100"
                                ReadOnly="True" Style="z-index: 500;" Width="100%">Nessuna Selezione</asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnExportXls" runat="server" ImageUrl="Immagini/xlsExport.png"
                                ToolTip="Esporta in formato Excel i risultati" Style="text-align: right" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <table>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <center>
                                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="Immagini/logout.png" ToolTip="Torna alla Home"
                                    OnClientClick="caricamentoincorso();" /></center>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <center>
                                <asp:ImageButton ID="BtnCerca" runat="server" ImageUrl="Immagini/Search_big.png"
                                    ToolTip="Nuova Ricerca" OnClientClick="caricamentoincorso();" /></center>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <center>
                                <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="Immagini/Search.png"
                                    Style="text-align: center; width: 32px;" OnClientClick="caricamentoincorso();" /></center>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <strong>TORNA ALLA HOME</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            <strong>NUOVA RICERCA</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            <strong>VISUALIZZA</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="idSelezionato" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
