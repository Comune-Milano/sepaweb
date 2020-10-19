<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettagliListaEsclusi.aspx.vb" Inherits="ANAUT_DettagliListaEsclusi" %>

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
                    CONVOCABILI
                    ESCLUSI
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="8pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr valign="middle">
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        
                        Text="Seleziona i contratti esclusi che si vuole nuovamente includere quindi premere il pulsante SALVA"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
            </tr>
            <tr valign="middle">
                <td style="text-align: right">
                    <asp:ImageButton ID="btnSalva" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SalvaGrande.png" style="text-align: right" />
                </td>
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
                                    	<asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server" />
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="ID_LISTA" HeaderText="LISTA" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" 
                                            Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CONTRATTO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="QUARTIERE" HeaderText="QUARTIERE"></asp:BoundColumn>
            <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO"></asp:BoundColumn>
            <asp:BoundColumn DataField="INDIRIZZO_UI" HeaderText="INDIRIZZO UNITA">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="TIPO_COR" HeaderText="TIPO IND. CORR.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="VIA_COR" HeaderText="IND. CORR."></asp:BoundColumn>
            <asp:BoundColumn DataField="CIVICO_COR" HeaderText="CIVICO CORR.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="LUOGO_COR" HeaderText="LUOGO CORR.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="SIGLA_COR" HeaderText="PROVINCIA CORR.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="CAP_COR" HeaderText="CAP CORR."></asp:BoundColumn>
            <asp:BoundColumn DataField="FILIALE" HeaderText="SEDE T."></asp:BoundColumn>
            <asp:BoundColumn DataField="SEDE" HeaderText="SEDE"></asp:BoundColumn>
            <asp:BoundColumn DataField="TIPO_CONTRATTO" HeaderText="TIPOLOGIA CONTR.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="TIPO_SPECIFICO" HeaderText="TIPO SPECIFICO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="DATA_STIPULA" HeaderText="DATA STIPULA">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="DATA_SLOGGIO" HeaderText="DATA SLOGGIO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="UI_VENDUTA" HeaderText="UNITA VENDUTA">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="BOLL_SPESE" HeaderText="BOLLETTA SPESE">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="NUM_COMP" HeaderText="NUM.COMP."></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MINORI_15" HeaderText="MINORI 15 ANNI">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MAGGIORI_65" HeaderText="MAGGIORI 65 ANNI">
                                        </asp:BoundColumn>
            <asp:BoundColumn DataField="NUM_COMP_66" HeaderText="NUM. COMP. 66-99% INV.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="NUM_COMP_100" HeaderText="NUM. COMP. 100% INV.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="NUM_COMP_100_CON" HeaderText="NUM. COMP. 100% ACC.">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="PREVALENTE_DIPENDENTE" HeaderText="PREV.DIPENDENTE">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="REDDITI_IMMOBILIARI" HeaderText="REDD.IMMOBILIARI">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="AREA_ECONOMICA" HeaderText="AREA ECONOMICA">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="CLASSE" HeaderText="CLASSE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE ESCLUSIONE">
                                        </asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    
    </div>
    </form>
</body>
</html>
