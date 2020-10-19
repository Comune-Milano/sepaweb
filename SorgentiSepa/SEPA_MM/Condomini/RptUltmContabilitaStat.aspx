<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptUltmContabilitaStat.aspx.vb" Inherits="Condomini_RptUltmContabilitaStat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:DataGrid ID="dgvExport" runat="server" AutoGenerateColumns="False" BackColor="White"
            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="20" Style="z-index: 105; left: 193px; top: 54px" Width="99%" CellPadding="1"
            CellSpacing="1" Visible="False">
            <PagerStyle Mode="NumericPages" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <ItemStyle ForeColor="Black" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CONDOMINIO">
                    <HeaderStyle Width="150px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO">
                    <HeaderStyle Width="60px" Font-Bold="False" Font-Italic="False" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_FINE" HeaderText="DATA FINE">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="STATO_BILANCIO" HeaderText="STATO BILANCIO">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="N_RATE" HeaderText="N° RATE">
                    <HeaderStyle Width="30px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SCADENZA_RATA_1" HeaderText="RATA 1">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SCADENZA_RATA_2" HeaderText="RATA 2">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SCADENZA_RATA_3" HeaderText="RATA 3">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SCADENZA_RATA_4" HeaderText="RATA 4">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SCADENZA_RATA_5" HeaderText="RATA 5">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SCADENZA_RATA_6" HeaderText="RATA 6">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                    <HeaderStyle Width="200px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="Lavender" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" />
        </asp:DataGrid>
    </span></strong>
    
    </div>
    </form>
</body>
</html>