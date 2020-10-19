<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScegliEdifici.aspx.vb" Inherits="Contratti_ScegliEdifici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript">
        window.name = "modal";
    </script>
    <title>Ricerca Edifici</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.css" rel="stylesheet"
        type="text/css" />
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/gestioneDimensioniPagina.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
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
    <form id="form1" runat="server" target="modal">
    <div id="divGenerale">
        <div id="divHeader" style="overflow: auto; height: 32px;">
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 90%;">
                        <asp:Button ID="btnSeleziona" runat="server" CssClass="bottone" Text="Associa nella ricerca" />
                        <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" OnClientClick="self.close();" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divBody">
            <div id="divTitolo">
                <table id="tbTitolo2">
                    <tr>
                        <td style="width: 5px;">
                            &nbsp;
                        </td>
                        <td style="text-align: center;">
                            Ricerca Edifici
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%; height: 100%; overflow: auto;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="98%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        Cod. Edificio
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodEdificio" CssClass="CssMaiuscolo" runat="server" Width="250px"
                                            TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        Denominazione
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="CssMaiuscolo" ID="txtDenominazione" runat="server" Width="250px"
                                            TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnCercaUnit" runat="server" ImageUrl="../NuoveImm/search-icon.png"
                                            ToolTip="Cerca Unita" TabIndex="-1" />
                                    </td>
                                    <td>
                                        Cerca
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divOverContent" style="width: 100%; overflow: auto;">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <div style="height: 220px; overflow: auto;">
                                                <asp:DataGrid ID="DataGridEdifici" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
                                                    GridLines="None" AllowPaging="True" PageSize="10" onclick="validNavigation=true;"
                                                    CellPadding="0" Width="97%">
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
                                                        <asp:BoundColumn DataField="COD_EDIFICIO" HeaderText="CODICE">
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Width="20%" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE">
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Width="20%" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="headerDataGrid" />
                                                    <FooterStyle CssClass="footerDatagrid" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 95%">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: right">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95%">
                                                        <asp:Label ID="txtmia" runat="server" Width="100%" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="9pt">Nessuna Selezione</asp:Label>
                                                    </td>
                                                    <td style="text-align: right">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95%">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: right">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
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
                            &nbsp
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
            </div>
        </div>
    </div>
    <asp:HiddenField ID="idSelected" runat="server" Value="0" />
    <asp:HiddenField ID="noClose" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="noCloseRead" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="optMenu" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="tipoSubmit" runat="server" Value="0" />
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
