<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Procedure.aspx.vb" Inherits="SIRAPER_Procedure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Procedure Siraper</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/menu.css" rel="stylesheet" type="text/css" />
    <script src="js/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
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
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 90%;">
                            <strong>ELENCO PROCEDURE</strong>
                        </td>
                        <td style="width: 10%;">
                            <asp:ImageButton ID="btnAggiorna" runat="server" ImageUrl="../NuoveImm/Refresh.png"
                                AlternateText="Aggiorna" ToolTip="Aggiorna" Height="32px" Width="32px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr style="height: 60%">
            <td>
                <div id="divProcedure" style="overflow: auto; width: 100%; height: 450px;">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="dgvElaborazioni" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            GridLines="None" PageSize="100" Style="z-index: 105; left: 193px; top: 54px"
                            Width="98%">
                            <ItemStyle BackColor="#EFF3FB" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                Position="TopAndBottom" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INIZIO" HeaderText="INIZIO" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FINE" HeaderText="FINE" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ESITO" HeaderText="ESITO" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PERCENTUALE" HeaderText="PERCENTUALE" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
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
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        initialize();
        function initialize() {
            document.getElementById('caricamento').style.visibility = 'hidden';
        };
    </script>
    </form>
</body>
</html>
