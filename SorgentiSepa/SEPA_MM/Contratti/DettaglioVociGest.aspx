<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioVociGest.aspx.vb"
    Inherits="Contratti_DettaglioVociGest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dettaglio Voci</title>
</head>
<body style="background-attachment: fixed; background-color: #E5E5E5; background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <table style="left: 0px; width: 100%; top: 0px;">
        <tr>
            <td>
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp Dettagli
                    documento gestionale</strong></span>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 15px;">
                <asp:Label ID="lblDettGest" runat="server" Text="Label" Font-Bold="True" Font-Names="Arial"
                    Font-Size="9pt" ForeColor="Black" Width="100%"></asp:Label>
                <table width="100%">
                    <tr>
                        <td colspan="3" style="padding-left: 10px;">
                            <asp:DataGrid ID="DataGrDettaglio" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                                Font-Size="8pt" Width="100%" PageSize="3" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CellPadding="2"
                                ForeColor="#333333" BorderStyle="Solid" BorderWidth="0.5" CellSpacing="1" BackColor="#708ECB"
                                BorderColor="#3399FF">
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <ItemStyle ForeColor="Black" BackColor="#DEDFDE" />
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCR_VOCE" HeaderText="DESCRIZIONE VOCE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO VOCE €.">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="#507CD1" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                                <SelectedItemStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                        <td style="width: 100%; height: 60px;" valign="bottom" align="right">
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                ToolTip="Home" OnClientClick="self.close();" />
                        </td>
                        <td style="width: 20%" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
