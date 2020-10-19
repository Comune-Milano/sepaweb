<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiODLperStruttura.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiODLperStruttura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        function ApriPagamenti(id_p, tipo) {
            var data = new Date();
            data = data.getFullYear().toString() + data.getMonth().toString() + data.getDay().toString() + data.getHours().toString() + data.getMinutes().toString() + data.getSeconds().toString();

            if (tipo == 7) {
                window.open('../RRS/SAL_RRS.aspx?ID=' + id_p + '&PROVENIENZA=CHIAMATA_DIRETTA', 'Dettagli' + data, 'height=550,top=0,left=0,width=800');
            }
            else {
                window.open('../MANUTENZIONI/SAL.aspx?ID=' + id_p + '&PROVENIENZA=CHIAMATA_DIRETTA', 'Dettagli' + data, 'height=560,top=0,left=0,width=800');
            }
        }
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div>
            <table width="100%">
                <tr>
                    <td colspan="2" style="height: 7px; width: 100%"></td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 21px; width: 90%;">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnExport" runat="server" Style="top: 0px; left: 0px" Text="Export XLS"
                                        ToolTip="Esporta in Excel" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnStampaPDF" runat="server" Style="top: 0px; left: 0px" Text="Stampa"
                                        ToolTip="Stampa" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: right; height: 21px; width: 10%;">
                        <telerik:RadButton ID="btnEsci" runat="server" Style="top: 0px; left: 0px" Text="Esci"
                            ToolTip="Esci" OnClientClicking="function(sender, args){self.close();}" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblTitolo" runat="server" Font-Names="Arial" Font-Size="14pt" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 7px;" colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblErrore" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                CellPadding="4" ForeColor="#333333" Width="100%" BorderColor="#507CD1" BorderStyle="Solid">
                <AlternatingItemStyle BackColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="CODICE" HeaderText="CODICE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="7%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" Width="7%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="VOCE" HeaderText="VOCE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="23%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" Width="23%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="N_ODL" HeaderText="NUM. ODL">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="7%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" Width="7%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="IMPORTO PRENOTATO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="10%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" Width="10%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_PRENOTAZIONE" HeaderText="DATA PRENOTAZIONE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="10%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="10%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="IMPORTO_APPROVATO" HeaderText="IMPORTO APPROVATO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="10%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" Width="10%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="26%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" Width="26%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="REPERTORIO" HeaderText="REPERTORIO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="7%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" Width="7%" />
                    </asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                    ForeColor="White" HorizontalAlign="Center" />
                <ItemStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
        </div>
        <asp:HiddenField ID="codicevoce" runat="server" />
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();
    </script>
</body>
</html>
