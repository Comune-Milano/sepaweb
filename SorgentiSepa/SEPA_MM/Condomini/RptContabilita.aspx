<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptContabilita.aspx.vb" Inherits="Condomini_RptContabilita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Contabilita</title>
    <script type ="text/javascript" >
        function doit(){
        if (!window.print){
        alert("Browser non supportato!")
        return
        }
        window.print()
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        &nbsp;
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 13px; position: absolute;
            top: 12px" Visible="False" Width="525px"></asp:Label>
        <table cellpadding="0" cellspacing="0" style="vertical-align: top; width: 98%; height: 80%;
            text-align: left">
            <tr>
                <td style="vertical-align: top; text-align: center">
    
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="15pt"
                        Style="z-index: 100; left: 215px; top: 11px" 
                        Width="70%">CONTABILITA</asp:Label></td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:Label ID="lblPeriodo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                        Style="z-index: 100; left: 215px; top: 11px" 
                        Width="70%">Riepilogo</asp:Label></td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:Label ID="lblPreventivo" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" 
                        Width="15%">PREVENTIVO</asp:Label>
    
                    <asp:ImageButton ID="btnExportPrev" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                        
                        
                        Style="z-index: 102; right: 28px; left: 0px;top: 16px" TabIndex="2"
                        ToolTip="Esporta in Excel il Preventivo" />
                    </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left; height: 303px;">
                <asp:DataGrid ID="DataGridInquilini" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="100%" CellPadding="4" ForeColor="#333333" BorderWidth="1px">
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#2461BF" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CONGUAGLIO_GP" HeaderText="CONGUAGLIO GEST. PREC.">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PREVENTIVO" HeaderText="PREVENTIVO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RATA_1" HeaderText="RATA1">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RATA_2" HeaderText="RATA2">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RATA_3" HeaderText="RATA3">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RATA_4" HeaderText="RATA4">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RATA_5" HeaderText="RATA5">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RATA_6" HeaderText="RATA6">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid></td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:Label ID="lblConsuntivo" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" 
                        Width="15%">CONSUNTIVO</asp:Label>
    
                    <asp:ImageButton ID="btnExportCons" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                        
                        
                        Style="z-index: 102; right: 28px; left: 0px;top: 16px" TabIndex="2"
                        ToolTip="Esporta in Excel il Preventivo" />
                    </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
                <asp:DataGrid ID="DataGridConsuntivo" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="100%" CellPadding="4" ForeColor="#333333" BorderWidth="1px">
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#2461BF" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CONSUNTIVO" HeaderText="CONSUNTIVO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PREVENTIVO" HeaderText="PREVENTIVO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CONGUAGLIO" HeaderText="CONGUAGLIO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid></td>
            </tr>
        </table>
    
    </div>
    

                <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility='hidden';
           </script>


            <asp:HiddenField ID="idPianoF" runat="server" Value="0" />
    

    </form>
                

</body>
</html>
