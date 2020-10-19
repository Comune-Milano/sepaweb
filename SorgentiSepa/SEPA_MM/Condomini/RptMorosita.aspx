<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptMorosita.aspx.vb" Inherits="Condomini_RptMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Morosita</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div>
        <table cellpadding="0" cellspacing="0" style="vertical-align: top; width: 98%;text-align: left">
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="15pt"
                        Style="z-index: 100; left: 215px; top: 11px" 
                        Width="70%">MOROSITA</asp:Label></td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:Label ID="lblPeriodo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                        Style="z-index: 100; left: 215px;" 
                        Width="70%">Riepilogo</asp:Label></td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                        
                        
                        
                        Style="z-index: 102; right: 805px; left: 12px; top: 16px" TabIndex="1"
                        ToolTip="Esporta in Excel" />
    
                    <asp:ImageButton ID="btnExport0" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
                        
                        
                        
                        Style="z-index: 102; right: 805px; left: 12px; top: 16px" TabIndex="2"
                        ToolTip="Stampa in PDF" />
                    </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
                <asp:DataGrid ID="DataGridMorosita" runat="server" AllowSorting="True" 
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
                        <asp:TemplateColumn HeaderText="POSIZIONE A BILANCIO">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE_BILANCIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="INTERNO">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="SCALA">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="INTESTATARIO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="IMPORTO">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="arial" 
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="arial" 
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 13px;
            top: 12px" Visible="False" Width="525px"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    
    </div>
    </form>
                    <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility='hidden';
           </script>
</body>
</html>
