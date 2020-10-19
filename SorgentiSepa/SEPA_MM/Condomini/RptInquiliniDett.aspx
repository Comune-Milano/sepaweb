<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptInquiliniDett.aspx.vb" Inherits="CondominiNew_RptInquiliniDett" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 15pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <table style="width:100%;">
        <tr>
            <td class="style1" style="text-align: center">
                REPORT INQUILINI SU
                <asp:Label ID="lbltitle" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Visible="False" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
    
                    <asp:ImageButton ID="btnExportPrev" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                        
                        
                        Style="z-index: 102; right: 28px; left: 0px;top: 16px" TabIndex="2"
                        ToolTip="Esporta in Excel il Preventivo" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="DataGridInquilini" runat="server" 
                        AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="100%" CellPadding="4" ForeColor="#333333">
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#2461BF" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False">
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="COD. UNITA">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="POSIZIONE BIL.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE_BILANCIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="OCCUPAZIONE" HeaderText="STATO OCCUPAZIONE">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="INTERNO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIPO">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="NOMINATIVO">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL.PROP.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MIL_PRO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL. ASC.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.MIL_ASC") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL. COMP.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.MIL_COMPRO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL. GEST.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.MIL_GEST") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL. RISC.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.MIL_RISC") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO" Visible="False"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
                   
                   
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>

    </form>
</body>
</html>
