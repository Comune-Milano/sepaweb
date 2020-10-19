<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VisualIncassoIng.aspx.vb"
    Inherits="Contratti_Pagamenti_VisualIncassoIng" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Incassi Manuali Ingiunzioni</title>
    <style type="text/css">
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="js/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="js/jsFunzioni.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td align="center">
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                    Text="lblTitolo" Style="color: #CC3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <asp:DataGrid ID="dgvIncasso" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                        Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;
                        left: 193px; top: 54px" Width="97%" CellPadding="4" GridLines="None" ForeColor="#333333">
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUM. BOLLETTA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO" HeaderText="TIPO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RIF_DA" HeaderText="RIFERIMENTO DA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RIF_A" HeaderText="RIFERIMENTO A">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAG_INCASSO" HeaderText="INCASSATO €.">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </span></strong>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table cellpadding="0" cellspacing="0" width="70%">
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Contratti/Pagamenti/image/EditIcon.png" />
                        </td>
                        <td align="center">
                            <asp:ImageButton ID="btnEdit0" runat="server" ImageUrl="~/Contratti/Pagamenti/image/excel-icon.png" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" align="center">
                            <strong>Modifica Incasso</strong>
                        </td>
                        <td class="style2" align="center">
                            <strong>Export XLS</strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="vIdConnessione" runat="server" />
                <asp:HiddenField ID="idContratto" runat="server" Value="0" />
                <asp:HiddenField ID="vIdAnagrafica" runat="server" Value="0" />
                <asp:HiddenField ID="idIncasso" runat="server" Value="0" />
                <asp:HiddenField ID="flAnnullata" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
