<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="SchedaAlloggioExcel.aspx.vb" Inherits="RILEVAZIONI_SchedaAlloggioExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Modello Scheda Manutentiva
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <div>
        <table style="width: 100%;">
            <tr valign="top">
                <td style="width: 97%;">
                    <div id="divOverContentRisultati" style="overflow: auto;">
                        <asp:DataGrid ID="DataGridParam0" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
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
                                <asp:BoundColumn DataField="DATA_CARICAMENTO" HeaderText="DATA CARICAMENTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FILE_EXCEL" HeaderText="FILE EXCEL">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IN_USO" HeaderText="ATTIVO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
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
                    <asp:Button ID="btnEliminaElemento" runat="server" Text="Button" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dialA" id="divInsA" style="visibility: hidden">
    </div>
    <div class="dialB" id="divInsB" style="visibility: hidden">
        <div id="InserimentoP" class="dialLargoTrasparent">
            <table style="width: 100%;" class="tblDiv">
                <tr style="width: 100%;">
                    <td style="text-align: center" class="divTitoloText">
                        Carica Modello Scheda
                    </td>
                </tr>
                <tr>
                    <td class="tbTitolo">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                            Text='Selezionare il file excel da caricare tramite il pulsante "Sfoglia"'></asp:Label>
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
                                    &nbsp;
                                </td>
                            </tr>
                            <tr align="right">
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAllega" runat="server" Text="Salva Modello" CssClass="bottone" />
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
                        <asp:Button ID="btnScaricaXls" runat="server" ClientIDMode="Static" Style="display: none;" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="LBLID" runat="server" Value="" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="HfContenteDivHeight" runat="server" ClientIDMode="Static" Value="150" />
    <script src="Funzioni.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ScaricaFileXLS() {
            document.getElementById('btnScaricaXls').click();
        };
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
