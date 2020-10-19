<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElCrediti.aspx.vb" Inherits="Contratti_Pagamenti_ElCrediti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crediti generati da Pagamenti Extra M.A.V</title>
        <script type="text/javascript" language="javascript" >
        window.name = "modal";
        </script>
    <style type="text/css">
        .style1
        {
            font-family: arial;
            font-weight: bold;
            font-size: 8pt;
            color: #0000CC;
        }
    </style>
</head>
<body style="width: 351px">
    <form id="form1" runat="server" target ="modal">

                    <table style="width:100%;">
                        <tr>
                            <td class="style1">
                                Crediti derivanti da Pagamenti extra M.A.V. eccedenti il dovuto</td>
                        </tr>
                        <tr>
                            <td>

                    <div style="width: 338px; height: 300px; overflow: auto;">
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                <asp:DataGrid ID="DataGridBollette" runat="server" AutoGenerateColumns="False"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" 
                    Style="z-index: 105; left: 193px; top: 54px" Width="97%" BorderColor="Gray" 
                            BorderWidth="1px">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle ForeColor="Black" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA" HeaderText="DATA">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO €.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                </asp:DataGrid>
                        </span></strong>
                    </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>

    </form>
</body>
</html>
