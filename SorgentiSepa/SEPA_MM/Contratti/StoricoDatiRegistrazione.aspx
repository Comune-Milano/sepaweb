<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StoricoDatiRegistrazione.aspx.vb"
    Inherits="Contratti_StoricoDatiRegistrazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dati registrazione contratti</title>

</head>
<body style="left: 0px; background-image: url('../NuoveImm/SfondoMaschere.jpg'); background-repeat: no-repeat;">
    <form id="form1" runat="server">

        <table>
            <tr>
                <td>

                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Storico Dati Registrazione</strong></span>

                    



                </td>
            </tr>
            <tr><td>&nbsp</td></tr>
            <tr>
                <td>
                    <asp:DataGrid runat="server" ID="DataGrid" AutoGenerateColumns="False" CellPadding="1"
                        Font-Names="Arial" Font-Size="10pt" ForeColor="#000000" GridLines="None" CellSpacing="1"
                        Width="650px" ShowFooter="True">
                        <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SERIE_REGISTRAZIONE" HeaderText="SERIE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NUM_REGISTRAZIONE" HeaderText="NRO. REG."></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_REG" HeaderText="DATA REG."></asp:BoundColumn>
                            <asp:BoundColumn DataField="NRO_ASSEGNAZIONE_PG" HeaderText="NRO. ASSEGN. PG"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_ASSEGNAZIONE_PG" HeaderText="DATA ASSEGN. PG"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#999999" />
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
                <table style="width: 100%;">
                    <tr>
                        <td>&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../NuoveImm/Img_EsciCorto.png"
                        ToolTip="Esci" OnClientClick="self.close();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>

        
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
</body>
</html>
