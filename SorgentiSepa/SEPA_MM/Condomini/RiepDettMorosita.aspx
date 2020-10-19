<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RiepDettMorosita.aspx.vb" Inherits="Condomini_RiepDettMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Riepilogo Perfezionamento Documentazione</title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="10pt" Text="Label"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; height: 250px;">
                <asp:DataGrid ID="DgvDettInqMorosita" runat="server" 
                        AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="100%" CellPadding="4" ForeColor="#333333">
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#2461BF" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_MOROSITA" HeaderText="ID_MOROSITA" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_INTESTATARIO" HeaderText="ID_INTESTATARIO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA" HeaderText="DATA">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                </div>
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
