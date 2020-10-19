<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioDC.aspx.vb" Inherits="Contratti_DettaglioDC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td align="center">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                    <asp:Label ID="lblVoceBp" runat="server" 
                    Style="font-size: 12pt; color: #CC3300"></asp:Label>
                </strong></span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDispIniziale" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="10pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblRicevuti" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="10pt">Elenco incassi ricevuti</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <div style="overflow: auto;">
                        <asp:DataGrid ID="dgvDepCauz" runat="server" AutoGenerateColumns="False"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                            PageSize="150" Style="z-index: 105; left: 193px; top: 54px" Width="99%" CellPadding="4"
                            CellSpacing="2" ForeColor="#333333">
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. RAPPORTO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO €.">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_INS" HeaderText="DATA">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="White" Wrap="False" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>
                    </div>
                </span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblRestituiti" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="10pt">restituiti</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDisponibilita" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="10pt">Disponibilità</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
