<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModificaListaConv.aspx.vb" Inherits="ANAUT_ModificaListaConv" %>

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
                    MODIFICA LISTA DI CONVOCAZIONE
                    -&nbsp;
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
                        
                        Text="Seleziona i contratti da escludere da questa lista e premere il pulsante Salva."></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
            </tr>
            <tr valign="middle">
                <td style="text-align: right">
                    <asp:ImageButton ID="btnSalva" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SalvaGrande.png" />
                </td>
            </tr>
        </table>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" Font-Bold="False" Font-Italic="False" 
        Font-Names="Arial" Font-Overline="False" Font-Size="XX-Small" 
        Font-Strikeout="False" Font-Underline="False">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
        <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
            Font-Italic="False" Font-Names="ARIAL" Font-Overline="False" 
            Font-Strikeout="False" Font-Underline="False" />
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
                                        <asp:BoundColumn DataField="NOME_GRUPPO" HeaderText="GRUPPO DI CONV.">
                                        </asp:BoundColumn>
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
        </Columns>
    </asp:DataGrid>
    
    </div>
    </form>
</body>
</html>

