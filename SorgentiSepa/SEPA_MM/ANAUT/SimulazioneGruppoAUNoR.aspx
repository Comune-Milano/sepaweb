﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SimulazioneGruppoAUNoR.aspx.vb" Inherits="ANAUT_SimulazioneGruppoAU" %>

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
                <td align="left" 
                    
                    style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold">
                    SIMULAZIONE APPLICAZIONE AU 
                    NON RISPONDENTI</td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="imgExport0" runat="server" 
                        ImageUrl="~/NuoveImm/Img_ExportExcel.png" />
                        &nbsp;&nbsp;&nbsp;
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
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                        CellPadding="4" ForeColor="#333333">
							<EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="White" />
							<Columns>
                            	<asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server" onclick="Conta();"/>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
								<asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CONTRATTO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_STIPULA" HeaderText="D. STIPULA">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_AREA_ECONOMICA" HeaderText="AREA">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SOTTO_AREA" HeaderText="CLASSE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ANNO_COSTRUZIONE" HeaderText="ANNO COSTRUZ.">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NUMERO_PIANO" HeaderText="PIANO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PRESENTE_ASCENSORE" HeaderText="ASCENSORE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PIANO" HeaderText="COEFF.PIANO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DEM" HeaderText="COEFF.DEM.">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ZONA" HeaderText="COEFF.ZONA">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COSTOBASE" HeaderText="COSTO BASE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VETUSTA" HeaderText="COEFF.VETUSTA">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CONSERVAZIONE" HeaderText="COEFF.CONSER.">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SUP_NETTA" HeaderText="SUP.NETTA">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SUPCONVENZIONALE" HeaderText="SUP.CONV.">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ALTRE_SUP" HeaderText="ALTRE SUP.">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SUP_ACCESSORI" HeaderText="SUP.ACCESSORI">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VALORE_LOCATIVO" HeaderText="VAL.LOCATIVO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PERC_VAL_LOC" HeaderText="% VAL.LOCATIVO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CANONE_CLASSE" HeaderText="CANONE CLASSE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PERC_ISTAT_APPLICATA" HeaderText="% ISTAT APPL.">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CANONE_CLASSE_ISTAT" HeaderText="CAN. CLASSE ISTAT">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INC_MAX" HeaderText="INC. MAX">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CANONE_SOPPORTABILE" HeaderText="CAN.SOPPORTABILE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CANONE_MINIMO_AREA" HeaderText="CAN.MINIMO AREA">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CANONE" HeaderText="CAN. DA APPLICARE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CANONE_ATTUALE" HeaderText="CAN.ATTUALE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ADEGUAMENTO" HeaderText="ADEG. ATTUALI">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ISTAT" HeaderText="ISTAT ATTUALE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ANNOTAZIONI" HeaderText="ANNOTAZIONI">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
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
                    <asp:ImageButton ID="imgExport" runat="server" 
                        ImageUrl="~/NuoveImm/Img_ExportExcel.png" style="height: 12px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
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
