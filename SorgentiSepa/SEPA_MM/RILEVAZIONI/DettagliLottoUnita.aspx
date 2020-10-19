<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettagliLottoUnita.aspx.vb"
    Inherits="RILEVAZIONI_DettagliLottoUnita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dettagli Associazione Lotti-Unità</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.css" rel="stylesheet"
        type="text/css" />
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/gestioneDimensioniPagina.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsMessage.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
    <style type="text/css">
        .style1
        {
            width: 150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" onsubmit="BeforeSubmit();return true;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divGenerale">
        <div id="divHeader" style="overflow: auto; height: 32px;">
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 90%;">
                        <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="False"
                            IncludeStyleBlock="false" Orientation="Horizontal" RenderingMode="List">
                            <Items>
                                <asp:MenuItem Text="Esci" Value="Esci"></asp:MenuItem>
                            </Items>
                        </asp:Menu>
                    </td>
                    <td>
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
                            Lotto
                            <asp:Label ID="lblLotto" runat="server"></asp:Label>
                            &nbsp; - Utente
                            <asp:Label ID="lblUtente" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td>
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
                                                <asp:DataGrid ID="DataGridUnita" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
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
                                                        <asp:BoundColumn DataField="CAP" HeaderText="CAP">
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" HorizontalAlign="Right" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA"></asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerDataGrid" />
                                                    <FooterStyle CssClass="footerDatagrid" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <td style="width: 3%" valign="top">
                                            <input id="btnAggiungi" class="minibottone" type="button" value="Aggiungi" onclick="MostraDiv();document.getElementById('TextBox1').value = '1';"/><br />
                                            <asp:Button ID="btnElimina" runat="server" CssClass="minibottone" Text="Elimina"
                                                OnClientClick="EliminaElemento();return false" />
                                            <asp:Button ID="btnEliminaElemento" runat="server" Text="Button" CssClass="bottone" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
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
                                Assegna Unità al Lotto&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Denominazione Edificio" Font-Names="arial"
                                                Font-Size="8pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Indirizzo" Font-Names="arial" Font-Size="8pt"></asp:Label>
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
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <div id="div1" style="overflow: auto; height: 200px;">
                                    <asp:DataGrid ID="DataGridUIDisponibili" runat="server" AutoGenerateColumns="False"
                                        CssClass="styleDataGrid" GridLines="None" Width="97%" PageSize="100" onclick="validNavigation=true;">
                                        <ItemStyle CssClass="itemDataGrid" />
                                        <PagerStyle CssClass="pagerDataGrid" Mode="NumericPages" Position="Top" />
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
                                            <asp:BoundColumn DataField="CAP" HeaderText="CAP">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" />
                                            </asp:BoundColumn>
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
                                            <asp:Button ID="btnSalvaDen" runat="server" CssClass="bottone" Text="Salva" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnChiudi" runat="server" CssClass="bottone" OnClientClick="document.getElementById('TextBox1').value='0';"
                                                Text="Esci" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="divFooter">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblErrore" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Maroon"
                                Visible="False"></asp:Label>
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
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="LBLID" runat="server" />
    <asp:HiddenField ID="IDLotto" runat="server" />
    <asp:HiddenField ID="IDRilievo" runat="server" />
    <asp:HiddenField ID="noClose" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="noCloseRead" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="optMenu" runat="server" Value="0" ClientIDMode="Static" />
    <script src="../Standard/Scripts/jsfunzioniExit.js" type="text/javascript"></script>
    <script src="Funzioni.js" type="text/javascript"></script>
    <script type="text/javascript">
        initialize();
        function initialize() {
            document.getElementById('divHeader').style.overflow = '';
            AfterSubmit();
            window.focus();
        };
    </script>
    </form>
</body>
</html>
