<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElIncassiIng.aspx.vb"
    Inherits="Contratti_Pagamenti_ElIncassiIng" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Incassi Manuali Ingiunzioni</title>
    <style type="text/css">
        .style2 {
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
                <td class="style2">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/NuoveImm/Img_Q_rosso.png" />
                    &nbsp;<span class="style2"><strong>= INCASSO ANNULLATO</strong></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="dgvIncassi" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                            Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105; left: 193px; top: 54px"
                            Width="97%" CellPadding="4" GridLines="None" ForeColor="#333333">
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_ORA" HeaderText="DATA ORA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_PAGAMENTO" HeaderText="DATA PAGAMENTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_PAGAMENTO" HeaderText="TIPO PAGAMENTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MOTIVO_PAGAMENTO" HeaderText="MOTIVO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="IMPORTO" HeaderText="IMP. TOTALE €.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FL_ANNULLATA" HeaderText="FL_ANNULLATA" Visible="False"></asp:BoundColumn>
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
                <td>
                    <asp:TextBox ID="txtmia" runat="server" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" MaxLength="100"
                        ReadOnly="True" Style="z-index: 500;" Width="100%">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table style="width: 100%;">
                        <tr>
                            <td width="30%" align="right">
                                <table>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/Contratti/Pagamenti/image/View-icon.png"
                                                OnClientClick="VisualIncassoIng();return false;" Style="text-align: center" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <strong>Visualizza</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="70%" id="tblBtnElInca" runat="server">
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Contratti/Pagamenti/image/EditIcon.png"
                                                OnClientClick="ModIncassoIng();return false;" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Contratti/Pagamenti/image/deleteIcon.png"
                                                OnClientClick="ConfAnnullo();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2" align="center">
                                            <strong>Modifica Incasso</strong>
                                        </td>
                                        <td class="style2" align="center">
                                            <strong>Annulla Incasso</strong>
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
                    <asp:HiddenField ID="vIdConnessione" runat="server" />
                    <asp:HiddenField ID="idContratto" runat="server" Value="0" />
                    <asp:HiddenField ID="vIdAnagrafica" runat="server" Value="0" />
                    <asp:HiddenField ID="idSelected" runat="server" Value="0" />
                    <asp:HiddenField ID="flAnnullata" runat="server" Value="0" />
                    <asp:HiddenField ID="flReload" runat="server" Value="0" />
                    <asp:HiddenField ID="confAnnullo" runat="server" Value="0" />
                    <asp:HiddenField ID="idBollettaRic" runat="server" Value="0" />
                    <asp:HiddenField ID="flEditDelable" runat="server" Value="0" />
                    <asp:HiddenField ID="SoloLett" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
