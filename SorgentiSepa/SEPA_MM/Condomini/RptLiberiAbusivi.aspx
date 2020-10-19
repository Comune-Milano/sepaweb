<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptLiberiAbusivi.aspx.vb" Inherits="Condomini_RptLiberiAbusivi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Liberi Abusivi</title>

</head>
<body>
    <form id="form1" runat="server">
    
            <%--<table cellpadding="0" cellspacing="0" style="vertical-align: top; width: 98%; height: 80%;
            text-align: left">--%>
            <table width="100%">
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                        
                        
            Style="z-index: 102; right: 778px; left: 11px;  top: 16px" TabIndex="2"
                        ToolTip="Esporta in Excel" />
                </td>
            </tr>
            
            <tr>
                <td style="vertical-align: top; text-align: center">
    
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="15pt"
                        Style="z-index: 100; left: 215px; top: 11px" 
                        Width="70%">ELENCO IMPORTI UNITA&#39; LIBERE</asp:Label>
                        </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:Label ID="lblSottotitolo" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" 
                        Width="70%"></asp:Label></td>
            </tr>
            
            <tr>
                <td style="vertical-align: top; text-align: center">
                <asp:DataGrid ID="DataGridLibeAbus" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="100%" CellPadding="3" ForeColor="#333333" BorderWidth="1px">
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#2461BF" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="POSIZIONE_BILANCIO" HeaderText="POSIZIONE BILANCIO">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO €">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid></td>
            </tr>
            
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:Label ID="lblSottotitolo1" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" 
                        Width="11%" Visible="False">Untià Abusive</asp:Label>
    
                    <asp:ImageButton ID="btnExport0" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                        
                        
            Style="z-index: 102; right: 778px; left: 11px;  top: 16px" TabIndex="2"
                        ToolTip="Esporta in Excel" Visible="False" />
                </td>
            </tr>
            
            <tr>
                <td style="vertical-align: top; text-align: center">
                <asp:DataGrid ID="DataGridAbus" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="97%" CellPadding="3" ForeColor="#333333" BorderWidth="1px">
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#2461BF" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="POSIZIONE_BILANCIO" HeaderText="POSIZIONE BILANCIO">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_SERVIZI" HeaderText="SERVIZI">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_ASCENSORE" HeaderText="ASCENSORE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_RISCALDAMENTO" HeaderText="RISCALDAMENTO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOT" HeaderText="TOTALE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid></td>
            </tr>
            
            <tr>
                <td style="vertical-align: top; text-align: left">  
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 148px;
            top: 361px" Visible="False" Width="525px"></asp:Label>   
      
                </td>
            </tr>
        </table>
    
   
                    </form>
                    <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility='hidden';
           </script>
    </body>
</html>
