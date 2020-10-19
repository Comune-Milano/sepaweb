<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="UtentiOperatori.aspx.vb" Inherits="RILEVAZIONI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Gestione Operatori
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Text="St. Professionale"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbUtenti" runat="server" Font-Bold="True" Font-Names="arial"
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
                &nbsp;
            </td>
        </tr>
        <tr valign="top">
            <td width="97%">
                <div id="divOverContentRisultati" style="overflow: auto;">
                    <asp:DataGrid ID="DataGridOperatori" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
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
                            <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE MM">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOME" HeaderText="NOME">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="headerDataGrid" />
                        <FooterStyle CssClass="footerDatagrid" />
                    </asp:DataGrid>
                </div>
            </td>
            <td valign="top">
                <input id="btnAggiungi" class="minibottone" type="button" value="Aggiungi" onclick="MostraDiv();document.getElementById('CPContenuto_TextBox1').value = '1';" /><br />
                <asp:Button ID="btnModifica" runat="server" CssClass="minibottone" OnClientClick="if (document.getElementById('CPContenuto_LBLID').value != '') { MostraDiv();  document.getElementById('CPContenuto_TextBox1').value='2';} else{  message('Attenzione',Messaggio.Elemento_No_Selezione); document.getElementById('CPContenuto_TextBox1').value='0';}"
                        Text="Modifica" />
                <asp:Button ID="btnElimina" runat="server" CssClass="minibottone" Text="Elimina"
                    OnClientClick="EliminaElemento();return false;" />
                <asp:Button ID="btnEliminaElemento" runat="server" Text="Button" CssClass="bottone" />
            </td>
        </tr>
    </table>
    <div class="dialA" id="divInsA" style="visibility: hidden">
    </div>
    <div class="dialB" id="divInsB" style="visibility: hidden">
        <div id="InserimentoP" class="dialCTransparent">
            <table width="100%" cellpadding="0" cellspacing="0" class="tblDiv">
                <tr class="divTitoloText">
                    <td>
                        Assegna Operatore
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Operatore" Font-Names="arial" Font-Size="8pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="cmbOperatore" runat="server" Font-Bold="False" Font-Names="arial"
                            Font-Size="8pt" Style="margin-left: 0px" Width="280px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Email" Font-Names="arial" Font-Size="8pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="280px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Telefono" Font-Names="arial"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtTel" runat="server" MaxLength="100" Width="280px"></asp:TextBox>
                    </td>
                </tr>
                <tr align="right">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSalvaDen" runat="server" CssClass="bottone" Text="Salva" />
                                </td>
                                <td style="text-align: right">
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
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="HfContenteDivHeight" runat="server" ClientIDMode="Static" Value="150" />
    <script src="Funzioni.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
</asp:Content>
