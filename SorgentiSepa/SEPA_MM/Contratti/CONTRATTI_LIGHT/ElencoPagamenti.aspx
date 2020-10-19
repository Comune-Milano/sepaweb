<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoPagamenti.aspx.vb" Inherits="Contratti_CONTRATTI_LIGHT_ElencoPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elenco Pagamenti</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
    <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" 
            Font-Size="10pt" Text="SCARICHI EFFETTUATI"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
    <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                AutoGenerateColumns="False" AllowPaging="True" Font-Size="8pt" 
                PageSize="8" style="z-index: 105; width: 100%;" 
                    Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" GridLines="None" CellPadding="4" 
                    ForeColor="#333333">
							<EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="White" />
							<Columns>
								<asp:BoundColumn DataField="ANNO" HeaderText="ANNO">
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="COD_TRIBUTO" HeaderText="CODICE TRIBUTO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_AE" HeaderText="INVIO AE">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO_CANONE" HeaderText="IMP.CANONE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO_TRIBUTO" HeaderText="IMP. TRIBUTO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="GIORNI_SANZIONE" HeaderText="GIORNI RITARDO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO_SANZIONE" HeaderText="IMPORTO SANZIONI"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO_INTERESSI" HeaderText="IMPORTO INTERESSI">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="FILE_SCARICATO" HeaderText="SCARICATO NEL FILE">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid>
    </td>
    </tr>
    <tr>
    <td>
        <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="ARIAL" 
            Font-Size="10pt" Text="RICEVUTE INSERITE"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" Font-Bold="False" 
            Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" 
            Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" 
            GridLines="None" PageSize="8" style="z-index: 105; width: 100%;">
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" 
                Font-Size="8pt" ForeColor="White" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="ANNO" HeaderText="ANNO"></asp:BoundColumn>
                <asp:BoundColumn DataField="VALIDA_FINO_AL" HeaderText="VALIDA FINO AL">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_TRIBUTO" HeaderText="CODICE TRIBUTO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_AE" HeaderText="DATA INVIO AE">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PG_AE" HeaderText="PROTOCOLLO AE"></asp:BoundColumn>
                <asp:BoundColumn DataField="REGISTRO" HeaderText="IMP. DI REGISTRO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SOSTITUTIVA" HeaderText="IMP. SOSTITUTIVA">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SANZIONI" HeaderText="IMP. SANZIONI">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INTERESSI" HeaderText="IMP. INTERESSI">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TOTALE" HeaderText="TOTALE"></asp:BoundColumn>
                <asp:BoundColumn DataField="NOME_FILE_REL" HeaderText="RICEVUTA REL">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOME_FILE_PDF" HeaderText="RICEVUTA PDF">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
            </Columns>
            <ItemStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:DataGrid>
    </td>
    </tr>
    </table>
    
    </div>
    <asp:HiddenField ID="LBLID" runat="server" Value="-1" />
    <asp:HiddenField ID="lblCodUfficio" runat="server" Value="-1" />
    <asp:HiddenField ID="lblNumReg" runat="server" Value="-1" />
    </form>
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script> 
</body>
</html>

