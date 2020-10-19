<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoVerificheChiusura.aspx.vb" Inherits="ANAUT_ElencoVerificheChiusura" %>

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
                    VERIFICA PRE-CHIUSURA ANAGRAFE UTENZA
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                &nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">
                    <asp:Label ID="Label1" runat="server" Text="Label" Font-Bold="False" 
                        Font-Names="arial" Font-Size="8pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt" ForeColor="#CC0000" Visible="False"></asp:Label>
                </td>
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
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            AllowPaging="True" PageSize="100">
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
            <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" 
                Visible="False">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="SPORTELLO" HeaderText="SPORTELLO/SEDE T.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
            <asp:BoundColumn DataField="DATA_APP" HeaderText="DATA APP.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ORE_APP" HeaderText="ORE APP.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="DIFFIDA" HeaderText="TIPO DIFFIDA">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="DATA_GENERAZIONE_DIFFIDA" 
                HeaderText="DATA GENERAZIONE DIFFIDA">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="PG_AU" HeaderText="PROTOCOLO AU"></asp:BoundColumn>
            <asp:BoundColumn DataField="DATA_INSERIMENTO_AU" 
                HeaderText="DATA INSERIMENTO AU"></asp:BoundColumn>
            <asp:BoundColumn DataField="STATO_AU" HeaderText="STATO AU">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="NUM_COMPONENTI" HeaderText="NU. COMPONENTI">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_INV_100_CON" 
                HeaderText="N.INVALIDI 100% INDENNITA'"></asp:BoundColumn>
            <asp:BoundColumn DataField="N_INV_100_SENZA" HeaderText="N.INVALIDI 100%">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_INV_100_66" HeaderText="N.INVALIDI TRA 66%-99%">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ISEE" HeaderText="ISEE"></asp:BoundColumn>
            <asp:BoundColumn DataField="ISE_ERP" HeaderText="ISE"></asp:BoundColumn>
            <asp:BoundColumn DataField="ISR_ERP" HeaderText="ISR"></asp:BoundColumn>
            <asp:BoundColumn DataField="ISP_ERP" HeaderText="ISP"></asp:BoundColumn>
            <asp:BoundColumn DataField="PSE" HeaderText="PSE"></asp:BoundColumn>
            <asp:BoundColumn DataField="VSE" HeaderText="VSE"></asp:BoundColumn>
            <asp:BoundColumn DataField="PRESENZA_MIN_15" HeaderText="MINORI 15 ANNI">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="PRESENZA_MAG_65" HeaderText="MAGGIORI 65 ANNI">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="PATR_SUPERATO" HeaderText="LIMITE PATR.SUPERATO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="PREVALENTE_DIP" 
                HeaderText="REDD. PREVAL.DIPENDENTE"></asp:BoundColumn>
            <asp:BoundColumn DataField="INIZIO_B1" HeaderText="CLASSE B1"></asp:BoundColumn>
            <asp:BoundColumn DataField="FL_DA_VERIFICARE" HeaderText="AU DA VERIFICARE">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FL_SOSP_1" HeaderText="SOSP. PER VAR.INTESTAZIONE">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FL_SOSP_2" 
                HeaderText="SOSP. PER CAMBIO INTESTAZIONE"></asp:BoundColumn>
            <asp:BoundColumn DataField="FL_SOSP_3" HeaderText="SOSP. PER VER.TITOLARITA'">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FL_SOSP_4" HeaderText="SOSP. PER DECRETO RILASCIO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FL_SOSP_5" HeaderText="SOSP. PER AMPLIAMENTO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FL_SOSP_7" HeaderText="SOSP.PER DOC.MANCANTE">
            </asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('divLoading').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>

