<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoProposteDec.aspx.vb" Inherits="ANAUT_ElencoProposteDec" %>

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
                    ELENCO 
                    PROPOSTE DECADENZA AU
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
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
            AllowPaging="True" PageSize="200">
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
            <asp:BoundColumn DataField="TIPO_DECADENZA" HeaderText="TIPO DECADENZA">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="SOTTO_AREA" HeaderText="CLASSE"></asp:BoundColumn>
            <asp:BoundColumn DataField="ANNOTAZIONI" HeaderText="NOTE">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ISEE" HeaderText="ISEE ERP">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ISE" HeaderText="ISE">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ISP" HeaderText="ISP">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ISR" HeaderText="ISR"></asp:BoundColumn>
            <asp:BoundColumn DataField="PSE" HeaderText="PSE"></asp:BoundColumn>
            <asp:BoundColumn DataField="VSE" HeaderText="VSE">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ISEE_27" HeaderText="ISEE L.27"></asp:BoundColumn>
            <asp:BoundColumn DataField="REDD_MOBILIARI" HeaderText="REDD. MOBILIARI">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="REDD_IMMOBILIARI" HeaderText="REDD. IMMOBILIARI">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="REDD_MOBILIARI_FR" 
                HeaderText="REDD.MOBILIARI CON FRANCHIGIA"></asp:BoundColumn>
            <asp:BoundColumn DataField="REDD_IMMOBILIARI_FR" 
                HeaderText="REDD.IMMOBILIARI CON FRANCHIGIA"></asp:BoundColumn>
            <asp:BoundColumn DataField="PATRIMONIO_DEC" HeaderText="PATRIMONIO DECADENZA">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ISEE_DECADENZA" HeaderText="ISEE DECADENZA">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="LIMITE_PATRIMONIO" HeaderText="LIMITE PATRIMONIO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
            <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
            <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
            <asp:BoundColumn DataField="CAP" HeaderText="CAP"></asp:BoundColumn>
            <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    </div>
    </form>
</body>
</html>

