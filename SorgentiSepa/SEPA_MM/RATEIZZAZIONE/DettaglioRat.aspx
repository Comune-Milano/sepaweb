<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioRat.aspx.vb" Inherits="RATEIZZAZIONE_DettaglioRat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style2
        {
            text-align: center;
            font-family: Arial;
            font-size: 12pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <table style="width: 100%;">
        <tr>
            <td class="style2">
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="11pt" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid runat="server" ID="DataGrid" AutoGenerateColumns="False" CellPadding="1"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                    Width="100%" ShowFooter="True">
                    <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
                    <Columns>
                        <asp:BoundColumn DataField="RATA" HeaderText="RATA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MESE" HeaderText="MESE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_RATA" HeaderText="IMPORTO RATA">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="QUOTA_CAPITALI" HeaderText="QUOTA CAPITALI">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="QUOTA_INTERESSI" HeaderText="QUOTA INTERESSI">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUM. BOLLETTA">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_BOLLETTA" HeaderText="IMPORTO BOLLETTA">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
<asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE">
    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
</asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="DATA SCADENZA">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PAGATA" HeaderText="PAGATA">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FL_ANNULLATA" HeaderText="FL_ANNULLATA" 
                            Visible="False"></asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" 
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="False" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" />
                    <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                    <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                </asp:DataGrid>
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
