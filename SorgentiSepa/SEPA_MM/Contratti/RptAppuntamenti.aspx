<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptAppuntamenti.aspx.vb" Inherits="Contratti_RptAppuntamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Appuntamenti Pre-Sloggio/Sloggio</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Height="16px" Style="z-index: 104; left: 574px; position: absolute;
        top: 14px" Visible="False" Width="525px"></asp:Label>
    <table width="100%">
        <tr>
            <td>
                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                    TabIndex="2" ToolTip="Esporta in Excel" style="height: 12px" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: center">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
                    Style="font-size: small; z-index: 100; left: 215px; top: 11px" Width="558px">REPORT APPUNTAMENTI PRE-SLOGGIO/SLOGGIO</asp:Label><br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left">
            <div id="Contenitore" style="overflow: auto; width: 1180px; height: 700px;">
                <asp:DataGrid ID="dgvRptAppSL" runat="server" AutoGenerateColumns="False" CellPadding="2"
                    ForeColor="#333333" Style="z-index: 11; left: 18px; top: 81px" 
                    Width="100%" BorderColor="Gray"
                    BorderWidth="2px" ViewStateMode="Enabled">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="Aqua" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" />
                    <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
  
                        <asp:BoundColumn DataField="QUARTIERE" HeaderText="QUARTIERE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="COD_UNITA" HeaderText="COD. UNITA' IMMOBILIARE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_APP_PRESL" HeaderText="DATA APP. PRE-SLOGGIO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ORA_APP_PRESL" HeaderText="ORA APP. PRE-SLOGGIO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_APP_SLOGGIO" HeaderText="DATA APP. SLOGGIO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ORA_APP_SLOGGIO" HeaderText="ORA APP. SLOGGIO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                       
                        <asp:BoundColumn DataField="COD_CONTR" HeaderText="COD_CONTR" Visible="False">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>


                    </Columns>
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="White" />
                </asp:DataGrid>
                </div>
            </td>
        </tr>



    </table>


    <br />
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    <p>
        <%--<asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                        
                        
            Style="z-index: 102; right: 778px; left: 11px; position: absolute; top: 16px" TabIndex="2"
                        ToolTip="Esporta in Excel" />--%>
    </p>
    </form>
</body>
</html>
