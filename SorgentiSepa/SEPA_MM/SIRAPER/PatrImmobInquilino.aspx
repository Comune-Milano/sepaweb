<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PatrImmobInquilino.aspx.vb"
    Inherits="SIRAPER_PatrImmobInquilino" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        window.name = "modal";
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jsFunzioni.js"></script>
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <title>Patrimonio Immobiliare Inquilino</title>
</head>
<body style="background-image: url('Immagini/Sfondo.png'); background-repeat: repeat-x;">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="Immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server" target="modal">
    <table style="width: 100%;">
        <tr style="height: 35px;">
            <td>
                <asp:Label ID="lblTitolo" runat="server" Text="Label" Font-Names="Arial" Font-Size="10pt"
                    Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <div style="width: 950px; height: 325px; overflow: auto;">
                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                    <asp:DataGrid ID="dgvPatrImmoInqui" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="None" PageSize="15" Width="97%" AllowPaging="True">
                                        <ItemStyle BackColor="#EFF3FB" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" Mode="NumericPages"
                                            Position="TopAndBottom" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="ANNO RIFERIMENTO*">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtannorif" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                        Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_RIFERIMENTO") %>'
                                                        Style="text-align: right;" MaxLength="4"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="TIPO PATRIMONIO IMMOBILIARE*">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddltipopatr" runat="server" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.TIPO_PATRIMONIO") %>'
                                                        Font-Names="Arial" Font-Size="8">
                                                        <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="FABBRICATI"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="TERRENI EDIFICABILI"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="ALTRO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="QUOTA DI PROPRIETA' DELL'IMMOBILE*">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtquota" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                        Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.QUOTA_PROPRIETA") %>'
                                                        Style="text-align: right;"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="VALORE ICI IMMOBILE*">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtvalore" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                        Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.VALORE_ICI") %>'
                                                        Style="text-align: right;"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="QUOTA MUTUO RESIDUO DELL'IMMOBILE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtmutuo" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                        Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.QUOTA_MUTUO_RESIDUA") %>'
                                                        Style="text-align: right;"></asp:TextBox>
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
                        <td style="vertical-align: top; text-align: center; width: 35px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnInserisci" runat="server" ImageUrl="Immagini/add_ico.png"
                                            OnClientClick="caricamentoincorso();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="Immagini/delete_ico.png"
                                            OnClientClick="caricamentoincorso();return ConfEliminaRigaPatrInq();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtmia" runat="server" BorderColor="White" BorderStyle="None" Font-Bold="True"
                    Font-Names="Arial" Font-Size="10pt" MaxLength="500" ReadOnly="True" Style="z-index: 500;
                    background-color: transparent;" Width="97%">Nessuna Selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 40%">
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="Immagini/confirm.png" OnClientClick="caricamentoincorso();return ConfProcediPatrInq();" />
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            <img alt="Uscita" src="Immagini/logout.png"  onclick ="caricamentoincorso();return ConfEsciPatrInquilino();" style ="cursor:pointer"/></td>
                        <td style="width: 40%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            CONFERMA
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
    <asp:HiddenField ID="idConnessione" runat="server" Value="" />
    <asp:HiddenField ID="sescon" runat="server" Value="" />
    <asp:HiddenField ID="IdInquilino" runat="server" Value="0" />
    <asp:HiddenField ID="idSiraper" runat="server" Value="-1" />
    <asp:HiddenField ID="idSiraperVersione" runat="server" Value="1" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="idSelected" runat="server" Value="0" />
    <asp:HiddenField ID="ConfEliminaPatrimonio" runat="server" Value="0" />
    <script language="javascript" type="text/javascript">
        initialize();
        function initialize() {
            window.focus();
            document.getElementById('caricamento').style.visibility = 'hidden';
        };
    </script>
    </form>
</body>
</html>
