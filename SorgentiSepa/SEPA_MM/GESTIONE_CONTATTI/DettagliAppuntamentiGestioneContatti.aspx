<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="DettagliAppuntamentiGestioneContatti.aspx.vb" Inherits="GESTIONE_CONTATTI_DettagliAppuntamentiGestioneContatti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
        function EliminaAppuntamento(id) {
            if (document.getElementById('daElimina') != null) {
                document.getElementById('daElimina').value = '1';
            };
            var chiediConferma = window.confirm("L\'appuntamento verrà eliminato definitivamente.\nVuoi continuare?");
            if (chiediConferma == true) {
                if (document.getElementById('confermaGenerica') != null) {
                    document.getElementById('confermaGenerica').value = '1';
                };
                if (document.getElementById('idSelected') != null) {
                    document.getElementById('idSelected').value = id;
                };
                if (document.getElementById('CPFooter_btnElimina') != null) {
                    document.getElementById('CPFooter_btnElimina').click();
                };
            } else {
                if (document.getElementById('daElimina') != null) {
                    document.getElementById('daElimina').value = '0';
                };
                if (document.getElementById('confermaGenerica') != null) {
                    document.getElementById('confermaGenerica').value = '0';
                };
                if (document.getElementById('idSelected') != null) {
                    document.getElementById('idSelected').value = '-1';
                };
            };
        };
        function ScrollPosTipo(obj) {
            if (document.getElementById('yPosTipo') != null) {
                document.getElementById('yPosTipo').value = obj.scrollTop;
            };
        };
        var Selezionato;
        var OldColor;
        var SelColo;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Label Text="Dettagli appuntamenti" runat="server" ID="lblTitolo1" />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:Label Text="Dettagli appuntamenti" runat="server" ID="lblTitolo2" />
        </asp:View>
        <asp:View ID="View3" runat="server">
            <asp:Label Text="Dettagli appuntamenti" runat="server" ID="lblTitolo3" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
        <asp:View ID="View4" runat="server">
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Label Text="Sede territoriale" runat="server" ID="lblFiliale" Font-Names="Arial"
                            Font-Bold="true" Font-Size="9pt" />
                        <br />
                        <asp:DropDownList runat="server" ID="cmbFilialeMod" AutoPostBack="True" Font-Names="Arial"
                            Font-Size="8pt" Width="350px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <table border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td colspan="2">
                                    <b>Legenda</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="border: medium solid #808080; background-color: orange; width: 15px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    Slot selezionato
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="border: medium solid #808080; background-color: #DDDDDD; width: 15px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    Slot libero
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="width: 100%;">
                            <tr>
                                <td style="vertical-align: top; width: 100%">
                                    <asp:Label ID="lblSportelloTotale" Text="Elenco appuntamenti" runat="server" Font-Names="Arial"
                                        Font-Size="8pt" Font-Bold="True" />
                                    <asp:Panel runat="server" ID="PanelTipo" Style="overflow: auto; height: 700px;" onscroll="ScrollPosTipo(this);">
                                        <asp:DataGrid runat="server" ID="DataGridElencoAppuntamentiTotale" AutoGenerateColumns="False"
                                            CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                            GridLines="None" Width="98%" CellSpacing="2">
                                            <ItemStyle BackColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SPORTELLO" HeaderText="SPORTELLO" ItemStyle-Width="8%">
                                                </asp:BoundColumn>
                                                <%--<asp:BoundColumn DataField="DATA_APPUNTAMENTO" HeaderText="DATA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                                <asp:BoundColumn DataField="ORA_APPUNTAMENTO" HeaderText="ORA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-Width="8%"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="NOME" HeaderText="SEDE TERRITORIALE" ItemStyle-Width="8%">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE" ItemStyle-Width="8%">
                                                </asp:BoundColumn>
                                                <%--<asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO/MODIFICA" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                                <asp:BoundColumn DataField="APPUNTAMENTO_CON" HeaderText="UTENTE" ItemStyle-Width="8%">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO 1" ItemStyle-Width="8%">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CELLULARE" HeaderText="TELEFONO 2" ItemStyle-Width="8%">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL" ItemStyle-Width="8%"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" ItemStyle-Width="8%"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SEGNALAZIONE" HeaderText="SEGNALAZIONE" ItemStyle-Width="8%">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="NUMERO" HeaderText="NR SEGN." ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right">
                                                </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="CONTRATTO" HeaderText="CONTRATTO" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ELIMINA" HeaderText="ELIMINAZIONE" ItemStyle-Width="12%">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FL_ELIMINA" HeaderText="" Visible="false"></asp:BoundColumn>
                                            </Columns>
                                            <EditItemStyle BackColor="White" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        </asp:DataGrid>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View5" runat="server">
            <table border="0" cellpadding="1" cellspacing="1" style="vertical-align: top">
                <tr>
                    <td style="width: 20%">
                        Segnalazione nr
                    </td>
                    <td style="width: 80%">
                        <asp:Label ID="lblNumeroSegnalazione" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Data appuntamento*
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="TextBoxDataAppuntamento" runat="server" Width="70px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxDataAppuntamento"
                            ErrorMessage="!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ForeColor="#CC0000"
                            ToolTip="Modificare la data di appuntamento" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sede territoriale
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbFiliale" Font-Names="Arial" Font-Size="8pt"
                            AutoPostBack="True" Width="300px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Orario appuntamento*
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbOrario" Font-Names="Arial" Font-Size="8pt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sportello*
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbSportello" Font-Names="Arial" Font-Size="8pt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cognome*
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxCognome" runat="server" MaxLength="100" Width="200px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nome
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxNome" runat="server" MaxLength="100" Width="200px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Telefono 1*
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxTelefono" runat="server" MaxLength="20" Width="150px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Telefono 2
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxCellulare" runat="server" MaxLength="20" Width="150px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        E-mail
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxEmail" runat="server" MaxLength="100" Width="200px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Note
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxNote" runat="server" MaxLength="100" Width="200px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Stato*
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbStato" Font-Names="Arial" Font-Size="8pt"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Esito*
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="cmbEsito" Font-Names="Arial" Font-Size="8pt"
                                        AutoPostBack="FALSE">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipologia nota segnalazione*
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="radioNotaGestionale" runat="server" Checked="true" Font-Names="Arial"
                                        Text="Nota Gestionale" Font-Bold="true" Font-Size="8pt" GroupName="notaSegnalazione" />
                                </td>
                                <td>
                                    <asp:RadioButton ID="radioNotaPubblica" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text="Nota Pubblica" Font-Bold="true" GroupName="notaSegnalazione" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nota segnalazione*
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoteSegnalazione" runat="server" TextMode="MultiLine" Height="100px"
                            Width="300px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel runat="server" ID="PanelElencoDocumentiRichiesti" Visible="false" Width="100%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label Text="Nessun documento richiesto" runat="server" ID="lblDocumentiRichiesti" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="overflow: auto; height: 150px;">
                                            <asp:DataGrid runat="server" ID="DataGridDocumentiRichiesti" AutoGenerateColumns="False"
                                                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                                                <ItemStyle BackColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                    Position="TopAndBottom" />
                                                <AlternatingItemStyle BackColor="Gainsboro" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="DOCUMENTI_RICHIESTI" HeaderText="DOCUMENTI RICHIESTI">
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="White" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View6" runat="server">
            <table border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td style="width: 20%">
                        Data appuntamento*
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="TextBoxDataAppuntamentoIns" runat="server" Width="70px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxDataAppuntamentoIns"
                            ErrorMessage="!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ForeColor="#CC0000"
                            ToolTip="Modificare la data di appuntamento" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sede territoriale
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbFilialeIns" Font-Names="Arial" Font-Size="8pt"
                            AutoPostBack="True" Width="300px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Orario appuntamento*
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbOrarioIns" Font-Names="Arial" Font-Size="8pt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sportello*
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="cmbSportelloIns" Font-Names="Arial" Font-Size="8pt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cognome*
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxCognomeIns" runat="server" MaxLength="100" Width="200px"
                            Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nome
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxNomeIns" runat="server" MaxLength="100" Width="200px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Telefono 1*
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxTelefonoIns" runat="server" MaxLength="20" Width="150px"
                            Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Telefono 2
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxCellulareIns" runat="server" MaxLength="20" Width="150px"
                            Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        E-mail
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxEmailIns" runat="server" MaxLength="100" Width="200px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Note
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxNoteIns" runat="server" MaxLength="100" Width="400px" Font-Names="Arial"
                            Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel runat="server" ID="PanelElencoDocumentiRichiestiIns" Visible="false" Width="100%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label Text="Nessun documento richiesto" runat="server" ID="lblDocumentiRichiestiIns" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="overflow: auto; height: 150px;">
                                            <asp:DataGrid runat="server" ID="DataGridDocumentiRichiestiIns" AutoGenerateColumns="False"
                                                CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
                                                <ItemStyle BackColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                    Position="TopAndBottom" />
                                                <AlternatingItemStyle BackColor="Gainsboro" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="DOCUMENTI_RICHIESTI" HeaderText="DOCUMENTI RICHIESTI">
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="White" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:MultiView ID="MultiView3" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View7" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Button ID="btnIndietro3" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Indietro" />
                                </td>
                                <td>
                                    <asp:Button ID="btnModificaAppuntamento" runat="server" Text="Modifica appuntamento"
                                        CssClass="bottone" ToolTip="Modifica appuntamento" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAggiungiAppuntamento" runat="server" Text="Aggiungi appuntamento"
                                        CssClass="bottone" ToolTip="Aggiungi appuntamento" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View8" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Button ID="btnIndietro" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Indietro" />
                                </td>
                                <td>
                                    <asp:Button ID="ButtonSalva" runat="server" Text="Aggiorna appuntamento" CssClass="bottone"
                                        ToolTip="Aggiorna appuntamento" />
                                </td>
                                <td>
                                    <asp:Button ID="btnChiudiSegnalazione" runat="server" Text="Chiudi segnalazione"
                                        CssClass="bottone" ToolTip="Chiudi segnalazione" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View9" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Button ID="btnIndietro2" runat="server" Text="Indietro" CssClass="bottone" ToolTip="Indietro" />
                                </td>
                                <td>
                                    <asp:Button ID="btnInserisciAppuntamento" runat="server" Text="Inserisci appuntamento"
                                        CssClass="bottone" ToolTip="Inserisci appuntamento" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
            <td>
                <asp:Button ID="btnEsci" runat="server" Text="Esci" CssClass="bottone" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnAggiornaForm" runat="server" Text="" CssClass="bottone" Style="display: none" />
    <asp:Button Text="" runat="server" ID="btnElimina" Style="display: none" />
    <asp:HiddenField runat="server" ID="HiddenFieldSportello" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="HiddenFieldOrario" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="daElimina" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="confermaGenerica" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSelected" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="yPosTipo" Value="" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idSegnalazione" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idStrutturaPredefinita" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="indiceSportello" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="indiceOrario" Value="-1" ClientIDMode="Static" />
    <script type="text/javascript">
        initialize();
        function initialize() {
            $("#CPContenuto_TextBoxDataAppuntamento").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
            $("#CPContenuto_TextBoxDataAppuntamentoIns").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        };
    </script>
    <script type="text/javascript">
        validNavigation = false;
    </script>
</asp:Content>
