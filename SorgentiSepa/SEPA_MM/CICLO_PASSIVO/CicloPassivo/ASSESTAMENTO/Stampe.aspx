<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Stampe.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_Stampe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="DgvStAler" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto);
                    direction: ltr; top: 40px; border-collapse: separate" TabIndex="-1" Width="100%"
                    BorderColor="#666666" CellPadding="1">
                    <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                    <EditItemStyle Wrap="False" BackColor="#2461BF" />
                    <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                    <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" BackColor="#2461BF"
                        ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" Wrap="False" />
                    <ItemStyle Wrap="False" BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            <HeaderStyle Height="1px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_PIANO_FINANZIARIO" HeaderText="ID_PIANO_FINANZIARIO"
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CODICE" HeaderText="CODICE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" Width="60px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                            <HeaderStyle Width="540px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ASSESTAMENTO" HeaderText="RICHIESTO">
                            <HeaderStyle Width="80px" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="APPROVATO" HeaderText="APPROVATO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="80px" Wrap="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                        Wrap="False" BorderStyle="None" BackColor="#507CD1" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="DgvApprAssCapitoli" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto);
                    direction: ltr; top: 40px; border-collapse: separate" TabIndex="-1" Width="750px"
                    BorderColor="#666666" CellPadding="1">
                    <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                    <EditItemStyle Wrap="False" BackColor="#2461BF" />
                    <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                    <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" BackColor="#2461BF"
                        ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" Wrap="False" />
                    <ItemStyle Wrap="False" BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="COD" HeaderText="CODICE" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderStyle-HorizontalAlign="Center" HeaderText="DESCRIZIONE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ASSESTAMENTO" HeaderText="ASSESTAMENTO" HeaderStyle-HorizontalAlign="Center">
                            <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                        Wrap="False" BorderStyle="None" BackColor="#507CD1" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="datagrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto);
                    direction: ltr; top: 40px; border-collapse: separate" TabIndex="-1" Width="750px"
                    BorderColor="#666666" CellPadding="1">
                    <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                    <EditItemStyle Wrap="False" BackColor="#2461BF" />
                    <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                    <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" BackColor="#2461BF"
                        ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" Wrap="False" />
                    <ItemStyle Wrap="False" BackColor="#EFF3FB" />
                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                        Wrap="False" BorderStyle="None" BackColor="#006699" />
                    <Columns>
                        <asp:BoundColumn DataField="CODICE" HeaderText="CODICE" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VOCE" HeaderText="VOCE" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RICHIESTO" HeaderText="ASSESTAMENTO RICHIESTO" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="APPROVATO" HeaderText="ASSESTAMENTO APPROVATO" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="IdAssestamento" runat="server" Value="0" />
    <asp:HiddenField ID="IdStato" runat="server" Value="0" />
    </form>
    <script type="text/javascript" language="javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
