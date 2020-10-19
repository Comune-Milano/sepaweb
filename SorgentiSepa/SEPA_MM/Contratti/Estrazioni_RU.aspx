<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Estrazioni_RU.aspx.vb" Inherits="Contratti_Estrazioni_RU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estrazioni Rapporti Utenza</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.css" rel="stylesheet"
        type="text/css" />
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/gestioneDimensioniPagina.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsMessage.js" type="text/javascript"></script>
    <script src="Funzioni.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="BeforeSubmit();return true;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divGenerale">
        <div id="divHeader" style="overflow: auto; height: 32px;">
            <table style="width: 100%" cellpadding="1" cellspacing="1">
                <tr>
                    <td style="width: 90%;">
                        <asp:Button ID="btnAvviaRicerca" runat="server" CssClass="bottone" Text="Avvia Ricerca" />
                        <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" OnClientClick="self.close();" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divBody">
            <div id="divTitolo">
                <table id="tbTitolo">
                    <tr>
                        <td style="width: 5px;">
                            &nbsp;
                        </td>
                        <td>
                            Estrazioni Rapporti Utenza
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%; height: 100%; overflow: auto;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div style="width: 100%; overflow: auto;">
                                <fieldset>
                                    <legend>Tipologie Estrazioni</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdbTipoEstrazione" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                                Width="1040px">
                                                                <asp:ListItem Value="RU" Selected="True">Contratto</asp:ListItem>
                                                                <asp:ListItem Value="UI">Unità Immobiliare</asp:ListItem>
                                                                <asp:ListItem Value="COND">Conduttore</asp:ListItem>
                                                                <asp:ListItem Value="CANONE">Canone</asp:ListItem>
                                                                <asp:ListItem Value="REG">Registrazione</asp:ListItem>
                                                                <asp:ListItem Value="SBOL">Schema Bollette</asp:ListItem>
                                                                <asp:ListItem Value="CONTAB">Partite Contabili</asp:ListItem>
                                                                <asp:ListItem Value="SALDO">Saldo Contabile</asp:ListItem>
                                                                <asp:ListItem Value="COMUNIC">Comunicazioni</asp:ListItem>
                                                                <asp:ListItem Value="INTERESSI">Interessi dep.cauz</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div style="width: 100%; overflow: auto;">
                                <fieldset>
                                    <legend>Paramentri di Ricerca</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Importa Unità da foglio Excel
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdbImportaUI" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True">
                                                                <asp:ListItem Value="1">SI</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="0">NO</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td id="sfoglia">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="datiRU">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Cod. Contratto
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCodRU" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Cod. UI
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCodUI" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td width="100%">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            Edifici
                                                                            <div style="overflow: auto;">
                                                                                <asp:DataGrid ID="DataGridEdifici" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
                                                                                    GridLines="None" AllowPaging="True" PageSize="10" onclick="validNavigation=true;"
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
                                                                                        <asp:BoundColumn DataField="CODICE" HeaderText="CODICE">
                                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                                Font-Underline="False" Width="5%" />
                                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                                Font-Underline="False" />
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE">
                                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                                Font-Underline="False" Width="10%" />
                                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                                Font-Underline="False" />
                                                                                        </asp:BoundColumn>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="headerDataGrid" />
                                                                                    <FooterStyle CssClass="footerDatagrid" />
                                                                                </asp:DataGrid>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 3%" valign="top">
                                                                            <br />
                                                                            <asp:Button ID="btnAddEdificio" runat="server" CssClass="minibottone" Text="Aggiungi"
                                                                                OnClientClick="RicercaEdifici();" />
                                                                            <br />
                                                                            <asp:Button ID="btnEliminaEdificio" runat="server" CssClass="minibottone" Text="Elimina"
                                                                                OnClientClick="DeleteConfirmEdificio();return false;" />
                                                                            <asp:Button ID="btnEliminaEd" runat="server" Style="display: none;" />
                                                                        </td>
                                                                        <td valign="top">
                                                                            Indirizzo
                                                                            <div style="overflow: auto;">
                                                                                <asp:DataGrid ID="DataGridIndirizzi" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
                                                                                    GridLines="None" AllowPaging="True" PageSize="10" onclick="validNavigation=true;"
                                                                                    CellPadding="0">
                                                                                    <ItemStyle CssClass="itemDataGrid" />
                                                                                    <PagerStyle CssClass="pagerDataGrid" Mode="NumericPages" />
                                                                                    <AlternatingItemStyle CssClass="alternateDataGrid" />
                                                                                    <Columns>
                                                                                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="INDIRIZZO">
                                                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                                Font-Underline="False" Width="10%" />
                                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                                Font-Underline="False" />
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn Visible="False" DataField="PROGR"></asp:BoundColumn>
                                                                                        <asp:BoundColumn Visible="False" DataField="NOME_INDIRIZZO"></asp:BoundColumn>
                                                                                        <asp:BoundColumn Visible="False" DataField="CIVICO_INDIRIZZO"></asp:BoundColumn>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="headerDataGrid" />
                                                                                    <FooterStyle CssClass="footerDatagrid" />
                                                                                </asp:DataGrid>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 3%" valign="top">
                                                                            <br />
                                                                            <asp:Button ID="btnAddVia" runat="server" CssClass="minibottone" Text="Aggiungi"
                                                                                OnClientClick="RicercaIndirizzi();" />
                                                                            <br />
                                                                            <asp:Button ID="btnEliminaIndirizzo" runat="server" CssClass="minibottone" Text="Elimina"
                                                                                OnClientClick="DeleteConfirmVia();return false;" />
                                                                            <asp:Button ID="btnEliminaVia" runat="server" Text="Button" Style="display: none;" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            Stato Rapporto
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbStato" runat="server" Width="205px">
                                                                <asp:ListItem>BOZZA</asp:ListItem>
                                                                <asp:ListItem>IN CORSO</asp:ListItem>
                                                                <asp:ListItem>IN CORSO (S.T.)</asp:ListItem>
                                                                <asp:ListItem>CHIUSO</asp:ListItem>
                                                                <asp:ListItem Selected="True">TUTTI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            Tipo Rapporto
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbTipo" runat="server" Width="220px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            Tipologia Unità
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbTipoUnita" runat="server" Width="218px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            Decorrenza dal
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataDecorrDAL" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDecorrDAL"
                                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td align="right">
                                                            Al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataDecorrAL" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataDecorrAL"
                                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Scadenza dal
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataScadenzaDAL" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataScadenzaDAL"
                                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataScadenzaAL" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataScadenzaAL"
                                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Disdetta/R.Forzoso Dal
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDisdettaDal" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDisdettaDal"
                                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDisdettaAl" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDisdettaAl"
                                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Sloggio dal
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSloggioDal" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtSloggioDal"
                                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSloggioAl" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtSloggioAl"
                                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divConduttore" style="width: 100%; overflow: auto;">
                                <fieldset>
                                    <legend>Filtri Conduttore</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td valign="top">
                                                            Tipologia Domanda
                                                        </td>
                                                        <td>
                                                            <asp:CheckBoxList ID="chkListTipoDomanda" runat="server" RepeatDirection="Vertical"
                                                                Width="500px">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                        <td valign="top">
                                                            Presenza Ospiti
                                                        </td>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chkOspiti" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divUI" style="width: 100%; overflow: auto;">
                                <fieldset>
                                    <legend>Filtri Unità</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Disponibilità
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbDispon" runat="server" Width="160px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            Superificie netta da
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNettaDa" runat="server" Width="60px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            A
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNettaA" runat="server" Width="60px"></asp:TextBox>
                                                        </td>
                                                        <td valign="top">
                                                            Pertinenza
                                                        </td>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="chkPertinenza" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divCanone" style="width: 100%; overflow: auto;">
                                <fieldset>
                                    <legend>Filtri Canone</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            Estrai Dett. Canoni
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbDettagliCanone" runat="server" Width="50px" AutoPostBack="True">
                                                                <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                                <asp:ListItem Value="1">SI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td id="sfogliaDich">
                                                            Importa dichiarazioni da file Excel
                                                            <asp:FileUpload ID="FileUploadDich" runat="server" />
                                                        </td>
                                                        <td>
                                                            Area
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbAreaEcon" runat="server" Width="150px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            Classe
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbClasse" runat="server" Width="50px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            Inizio Val. dal
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInizioValDal" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtInizioValDal"
                                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td align="right">
                                                            Al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInizioValAl" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                ControlToValidate="txtInizioValAl" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                                                                Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Fine Val. dal
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFineValDal" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                ControlToValidate="txtFineValDal" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                                                                Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFineValAl" runat="server" Width="60px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                                ControlToValidate="txtFineValAl" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                                                                Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Provenienza
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbProvenienza" runat="server" Width="250px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divRegistrazione" style="width: 100%; overflow: auto;">
                                <fieldset>
                                    <legend>Filtri Registrazione</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Ufficio del Registro
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbUffReg" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divSchemaBoll" style="width: 100%; overflow: auto;">
                                <fieldset>
                                    <legend>Filtri Schema Bollette</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            Sindacato di riferimento
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbSindacato" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="50px">
                                                            &nbsp
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdbListAnnoSchema" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True">
                                                                <asp:ListItem Value="1" Selected="True">Attuale Bollettazione</asp:ListItem>
                                                                <asp:ListItem Value="2">Voci Schema Anno Prec.</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td width="25px">
                                                            &nbsp
                                                        </td>
                                                        <td id="annoR">
                                                            Anno di riferimento
                                                        </td>
                                                        <td id="annoR2">
                                                            <asp:TextBox ID="txtAnnoRiferim" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divPartContab" style="width: 100%; overflow: auto;">
                                <fieldset>
                                    <legend>Filtri Partite Contab.</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Estratto Conto
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbEstrattoConto" runat="server">
                                                                <asp:ListItem Value="1" Selected="True">Contabile</asp:ListItem>
                                                                <asp:ListItem Value="2">Gestionale</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divSaldoContabile" style="width: 100%; overflow: auto;">
                                <fieldset>
                                    <legend>Filtri Saldo Contab.</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Emissione Al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataAl" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                                ControlToValidate="txtDataAl" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                                                                Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Pagamento Al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataPagamAl" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                                ControlToValidate="txtDataPagamAl" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                                                                Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td width="80px">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            Considera importi a RUOLO
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbRuolo" runat="server" Width="50px">
                                                                <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                                                <asp:ListItem Value="1">SI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divFooter">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div id="dialog" style="display: none;">
                </div>
                <div id="confirm" style="display: none;">
                </div>
                <div id="loading" style="display: none; text-align: center;">
                </div>
                <div id="divLoading" style="width: 0px; height: 0px; display: none;">
                    <img src="../Standard/Immagini/load.gif" id="imageLoading" alt="" />
                </div>
                <div id="divOscura" style="display: none; text-align: center; width: 100%; height: 100%;
                    position: absolute; top: 0px; left: 0px; background-color: #cccccc;">
                </div>
                <asp:HiddenField ID="tipoSubmit" runat="server" Value="0" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="noClose" runat="server" Value="0" />
    <asp:HiddenField ID="noCloseRead" runat="server" Value="0" />
    <asp:HiddenField ID="optMenu" runat="server" Value="0" />
    <asp:HiddenField ID="Hf_edificio" runat="server" Value="0" />
    <asp:HiddenField ID="Hf_via" runat="server" Value="0" />
    <script type="text/javascript">
        initialize();
        function initialize() {
            AfterSubmit();
            window.focus();
        };
        FiltroEstrazioni();
        FiltroImportaUI();
        FiltroAnno();
        NascondiConduttore();
        FiltroDich();

        function NascondiConduttore() {
            var radioButtonTipo = document.getElementById('cmbTipo');
            if (document.getElementById('divConduttore')) {
                if (radioButtonTipo.value != 'ERP' && radioButtonTipo.value != '-1') {
                    document.getElementById('divConduttore').style.display = 'none';
                }
            } else {
                document.getElementById('divConduttore').style.display = 'block';
            }

            if (document.getElementById('divCanone')) {
                if (radioButtonTipo.value != 'ERP' && radioButtonTipo.value != '-1') {
                    document.getElementById('divCanone').style.display = 'none';
                }
            } else {
                document.getElementById('divCanone').style.display = 'block';
            }
        };


        function FiltroAnno() {
            var radioButtonlist = document.getElementsByName('rdbListAnnoSchema');
            var selValue;
            for (var x = 0; x < radioButtonlist.length; x++) {
                if (radioButtonlist[x].checked) {
                    selValue = radioButtonlist[x].value;
                    if (selValue == '1') {
                        document.getElementById('annoR').style.visibility = 'hidden';
                        document.getElementById('annoR2').style.visibility = 'hidden';
                    } else {
                        document.getElementById('annoR').style.visibility = 'visible';
                        document.getElementById('annoR2').style.visibility = 'visible';
                    }
                }
            }
        };

        function FiltroDich() {
            var selValue;

            selValue = $("#cmbDettagliCanone").val();
            if (selValue == '1') {
                document.getElementById('sfogliaDich').style.display = 'block';
                //document.getElementById('datiRU').style.display = 'none';
            } else {
                document.getElementById('sfogliaDich').style.display = 'none';
                //document.getElementById('datiRU').style.display = 'block';
            }

        };

        function FiltroEstrazioni() {
            var radioButtonlist = document.getElementsByName('rdbTipoEstrazione');
            var selValue;
            for (var x = 0; x < radioButtonlist.length; x++) {
                if (radioButtonlist[x].checked) {

                    selValue = radioButtonlist[x].value;
                    switch (selValue) {

                        case 'COND':
                            document.getElementById('divConduttore').style.display = 'block';
                            break;
                        case 'CANONE':
                            document.getElementById('divCanone').style.display = 'block';
                            break;
                        case 'REG':
                            document.getElementById('divRegistrazione').style.display = 'block';
                            break;
                        case 'SBOL':
                            document.getElementById('divSchemaBoll').style.display = 'block';
                            break;
                        case 'CONTAB':
                            document.getElementById('divPartContab').style.display = 'block';
                            break;
                        case 'UI':
                            document.getElementById('divUI').style.display = 'block';
                            break;
                        case 'SALDO':
                            document.getElementById('divSaldoContabile').style.display = 'block';
                            break;
                        default:

                    };
                }
                else {
                    selValue = radioButtonlist[x].value;
                    switch (selValue) {
                        case 'COND':
                            document.getElementById('divConduttore').style.display = 'none';
                            break;
                        case 'CANONE':
                            document.getElementById('divCanone').style.display = 'none';
                            break;
                        case 'REG':
                            document.getElementById('divRegistrazione').style.display = 'none';
                            break;
                        case 'SBOL':
                            document.getElementById('divSchemaBoll').style.display = 'none';
                            break;
                        case 'CONTAB':
                            document.getElementById('divPartContab').style.display = 'none';
                            break;
                        case 'UI':
                            document.getElementById('divUI').style.display = 'none';
                            break;
                        case 'SALDO':
                            document.getElementById('divSaldoContabile').style.display = 'none';
                            break;
                        default:
                    }
                }
            };

        };
        function FiltroImportaUI() {
            var radioButtonlist2 = document.getElementsByName('rdbImportaUI');
            var selValue2;
            for (var x = 0; x < radioButtonlist2.length; x++) {
                if (radioButtonlist2[x].checked) {

                    selValue2 = radioButtonlist2[x].value;
                    switch (selValue2) {

                        case '0':
                            document.getElementById('sfoglia').style.display = 'none';
                            document.getElementById('datiRU').style.display = 'block';
                            break;
                        case '1':
                            document.getElementById('sfoglia').style.display = 'block';
                            document.getElementById('datiRU').style.display = 'none';
                            break;

                        default:

                    };
                }

            };

        };

        function RicercaEdifici() {
            window.showModalDialog('ScegliEdifici.aspx', 'Edif', 'status:no;toolbar=no;dialogWidth:1000px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
        };

        function RicercaIndirizzi() {
            window.showModalDialog('ScegliIndirizzi.aspx', 'Indirizzi', 'status:no;toolbar=no;dialogWidth:1000px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
        };

        function DeleteConfirmEdificio() {
            if (document.getElementById('Hf_edificio')) {
                if (document.getElementById('Hf_edificio').value != '') {
                    confirm('Attenzione', Messaggio.Elemento_Elimina, 'SI', 2, 'btnEliminaEd', 'NO', 3, '');
                }
                else {
                    message('Attenzione', Messaggio.Elemento_No_Selezione);
                }
            }
        };

        function DeleteConfirmVia() {
            if (document.getElementById('Hf_via')) {
                if (document.getElementById('Hf_via').value != '') {
                    confirm('Attenzione', Messaggio.Elemento_Elimina, 'SI', 2, 'btnEliminaVia', 'NO', 3, '');
                }
                else {
                    message('Attenzione', Messaggio.Elemento_No_Selezione);
                }
            }
        };
    </script>
    </form>
</body>
</html>
