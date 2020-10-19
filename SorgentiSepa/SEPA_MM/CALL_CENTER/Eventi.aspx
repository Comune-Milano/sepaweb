<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Eventi.aspx.vb" Inherits="Condomini_Eventi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Eventi Call Center</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <div>
        <table width="95%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    
    </div>
        <br />
        <table width="95%">
            <tr>
                <td>
                    <asp:DataGrid ID="DataGridPagamenti" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                        GridLines="None" Height="147px" Style="z-index: 11; left: 18px; top: 81px" Width="100%">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White"
                            Wrap="False" />
                        <EditItemStyle BackColor="#2461BF" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                            Wrap="False" />
                        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            Wrap="False" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="12px" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="White" Wrap="False" />
                    </asp:DataGrid></td>
            </tr>
        </table>
    </form>
</body>
</html>
