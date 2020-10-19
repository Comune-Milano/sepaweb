<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoConvocazioni.aspx.vb" Inherits="ANAUT_ElencoConvocazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">
                    ELENCO CONVOCAZIONI</td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/NuoveImm/Img_ExportExcel.png" /></td>
            </tr>
        </table>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="3" GridLines="Vertical" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" Font-Bold="False" Font-Italic="False" 
        Font-Names="Arial" Font-Overline="False" Font-Size="XX-Small" 
        Font-Strikeout="False" Font-Underline="False" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
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
            <asp:BoundColumn DataField="DESCRIZIONE_CON" HeaderText="LISTA CONVOCAZIONE">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FILIALE" HeaderText="SEDE T." ReadOnly="True">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_CONVOCAZIONE" HeaderText="N.CONVOCAZIONE" 
                ReadOnly="True"></asp:BoundColumn>
            <asp:BoundColumn DataField="GIORNO" HeaderText="GIORNO" ReadOnly="True">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="OPERATORE" HeaderText="SPORTELLO" ReadOnly="True" 
                Visible="False">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="INIZIO" HeaderText="ORA INIZIO" ReadOnly="True">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FINE" HeaderText="ORA FINE" ReadOnly="True" 
                Visible="False">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD.CONTRATTO" 
                ReadOnly="True"></asp:BoundColumn>
            <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO"></asp:BoundColumn>
            <asp:BoundColumn DataField="NOME" HeaderText="NOME" ReadOnly="True" 
                Visible="False">
            </asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    </div>
    </form>
</body>
</html>
