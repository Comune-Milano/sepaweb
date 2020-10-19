<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="GestLotti.aspx.vb" Inherits="RILEVAZIONI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Gestione Lotti
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 97%;">
                    &nbsp;
                </td>
                <td style="width: 3%" valign="top">
                    &nbsp;
                </td>
            </tr>
            <tr valign="top">
                <td style="width: 97%;">
                    <div id="divOverContentRisultati" style="overflow: auto;">
                        <asp:DataGrid ID="DataGridLotti" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
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
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DENOMINAZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="UTENTE" HeaderText="ST. PROFESS."></asp:BoundColumn>
                                <asp:BoundColumn DataField="REFERENTE" HeaderText="REFERENTE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="UNITA" HeaderText="UNITA' ASSOCIATE">
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
                    <asp:Button ID="btnModifica" runat="server" CssClass="minibottone" OnClientClick="if (document.getElementById('CPContenuto_LBLID').value != '') { MostraDiv();  document.getElementById('CPContenuto_TextBox1').value='2';} else{  message('Attenzione',Messaggio.Elemento_No_Selezione); document.getElementById('CPContenuto_TextBox1').value='0';}"
                        Text="Modifica" />
                    <br />
                    <asp:Button ID="btnElimina" runat="server" CssClass="minibottone" Text="Elimina"
                        OnClientClick="EliminaElemento();return false" />
                    <asp:Button ID="btnEliminaElemento" runat="server" Text="Button" CssClass="bottone" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dialA" id="divInsA" style="visibility: hidden">
    </div>
    <div class="dialB" id="divInsB" style="visibility: hidden">
        <div id="InserimentoP" class="dialCTransparent">
            <table style="width: 100%;" class="tblDiv">
                <tr>
                    <td style="text-align: center" class="divTitoloText">
                        Gestione Lotto&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Denominazione" Font-Names="arial" Font-Size="8pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDenominazione" runat="server" MaxLength="100" Width="327px" TabIndex="105"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        St. Professionale
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="cmbUtenti" runat="server" Width="327px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Referente
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="cmbReferenti" runat="server" Width="327px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr align="right">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSalvaDen" runat="server" CssClass="bottone" Text="Salva" />
                                </td>
                                <td>
                                    <asp:Button ID="btnChiudi" runat="server" CssClass="bottone" OnClientClick="document.getElementById('CPContenuto_TextBox1').value='0';"
                                        Text="Esci" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="LBLID" runat="server" Value="" />
    <asp:HiddenField ID="LBLIDLOTTO" runat="server" Value="" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="HfContenteDivHeight" runat="server" ClientIDMode="Static" Value="150" />
    <script src="Funzioni.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function AssegnaUnita(IndiceUnita) {
            document.getElementById('CPContenuto_LBLIDLOTTO').value = IndiceUnita;
            today = new Date();
            var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();
            popupWindow = window.open('DettagliLottoUnita.aspx?ID=' + IndiceUnita, Titolo, 'height=780,width=1160,resizable=yes');
            popupWindow.focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
</asp:Content>
