<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettagliGruppoAU.aspx.vb" Inherits="ANAUT_DettagliGruppoAU" %>

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
                <td align="left" 
                    
                    style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold">
                    ELENCO AU GRUPPO</td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SpostaRegistroSelezionati.png" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Names="ARIAL" Font-Size="8pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        
                        style="z-index: 105; left: 0px; width: 1768px;" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" 
                        CellPadding="3" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                        BorderWidth="1px">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#000084" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="#DCDCDC" />
							<Columns>
                            	<asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server" onclick="Conta();"/>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CONTRATTO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="PG_AU" HeaderText="PG AU">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOME" HeaderText="NOME">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO CONTRATTO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DECORRENZA" HeaderText="DECORRENZA">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SCADENZA" HeaderText="SCADENZA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="INDIRIZZO_UNITA" HeaderText="INDIRIZZO UN.">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CIVICO_UNITA" HeaderText="CIVICO UN.">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COMUNE_UNITA" HeaderText="COMUNE UN.">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CAP_UNITA" HeaderText="CAP UN."></asp:BoundColumn>
                                <asp:BoundColumn DataField="FILIALE" HeaderText="SEDE T./SPORTELLO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PREVALENTE" HeaderText="REDD. PREV.DIP.">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PRESENZA_15" HeaderText="NUM.MINORI 15">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PRESENZA_65" HeaderText="NUM MAGGIORI 65">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_INV_100_CON" HeaderText="INV.100% CON IND.">
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="INV. 100% SENZA IND."></asp:BoundColumn>
                                <asp:BoundColumn DataField="N_INV_66_99" HeaderText="INV. 66-99%">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="ID_DICHIARAZIONE" HeaderText="IDPG" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_GRUPPO" HeaderText="IDGRUPPO" Visible="False">
                                </asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
							<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                                Mode="NumericPages"></PagerStyle>
						    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
    
                </td>
            </tr>
            <tr>
            <td><asp:label id="Label2" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True"                 
                style="z-index: 106; left: 14px;  top: 436px; width: 130px; height: 16px; right: 1426px;" 
                ForeColor="Black">Elementi Selezionati:</asp:label><asp:label id="lblSelezionati" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                
                style="z-index: 106; left: 139px;  width: 97px; height: 16px; right: 1301px;" 
                ForeColor="Black">0</asp:label></td>
            </tr>
            <tr>
            <td>&nbsp; &nbsp;</td>
            </tr>
            <tr>
            <td>
                    &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SpostaRegistroSelezionati.png" />
                </td>
            </tr>
        </table>
        <br />
    
    </div>
    </form>
     <script  language="javascript" type="text/javascript">
         document.getElementById('dvvvPre').style.visibility = 'hidden';

         function Conta() {
             var contatore;
             contatore = 0;
             re = new RegExp(':' + document.getElementById('ChSelezionato') + '$')  //generated control
             for (i = 0; i < document.forms[0].elements.length; i++) {
                 elm = document.forms[0].elements[i]
                 if (elm.type == 'checkbox') {
                     if (elm.checked == true) {
                         contatore = contatore + 1;
                     }
                 }
             }
             if (document.all) {
                 document.getElementById('lblSelezionati').innerText = contatore;
             }
             else {
                 document.getElementById('lblSelezionati').textContent = contatore;
             }

         }
     </script>
</body>
</html>

