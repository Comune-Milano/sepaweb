<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettagliSimulazione.aspx.vb" Inherits="ANAUT_DettagliSimulazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">
                    SIMULAZIONE CONVOCAZIONI AU</td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/NuoveImm/Img_ExportExcel.png" style="height: 12px" /></td>
            </tr>
        </table>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="3" GridLines="Vertical" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" Font-Bold="False" Font-Italic="False" 
        Font-Names="Arial" Font-Overline="False" Font-Size="XX-Small" 
        Font-Strikeout="False" Font-Underline="False" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            AllowPaging="True" PageSize="500">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <EditItemStyle Font-Names="Arial" Font-Size="9pt" />
        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" Font-Names="Arial" 
            ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
            Mode="NumericPages" />
        <AlternatingItemStyle BackColor="#DCDCDC" Font-Names="Arial" />
        <ItemStyle BackColor="#EEEEEE" Font-Names="Arial" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            Font-Names="ARIAL" />
        <Columns>
            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="GRUPPO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD.CONTRATTO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO"></asp:BoundColumn>
            <asp:BoundColumn DataField="DATA_APP" HeaderText="DATA APPUNTAMENTO"></asp:BoundColumn>
            <asp:BoundColumn DataField="ORE_APP" HeaderText="ORE APPUNTAMENTO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ORE_FINE_APP" HeaderText="FINE APPUNTAMENTO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_OPERATORE" HeaderText="NUM. OPERATORE"></asp:BoundColumn>
            <asp:BoundColumn DataField="SPORTELLO" HeaderText="SPORTELLO">
            </asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    </div>
    </form>
</body>
     <script language="javascript" type="text/javascript">
         document.getElementById('divLoading').style.visibility = 'hidden';
    </script>
</html>
