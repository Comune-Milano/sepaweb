<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaPagamentiPerv.aspx.vb" Inherits="Contratti_Report_StampaPagamentiPerv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagamenti</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: center">
        <asp:Label ID="lblTipoPagamento" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="14pt" Style="z-index: 100; left: 256px; top: 8px"
            Width="636px"></asp:Label></td>
            </tr>
        </table>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 16px; position: absolute;
            top: 577px" Visible="False" Width="525px"></asp:Label>
        &nbsp; &nbsp;&nbsp;
        <br />
        <asp:ImageButton ID="imgExcel" runat="server" 
            ImageUrl="~/NuoveImm/Img_ExportExcel.png" />
        <br />
        <br />
        <br />
        <asp:DataGrid ID="DataGridPagamenti" runat="server" AutoGenerateColumns="False"
            CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
            GridLines="None" Height="147px" Style="z-index: 11; left: 18px; top: 81px" Width="100%">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White"
                Wrap="False" />
            <EditItemStyle BackColor="#2461BF" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                Wrap="False" />
            <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <ItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="12px" Font-Strikeout="False" Font-Underline="False"
                ForeColor="White" Wrap="False" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTATORE") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="COD.CONTRATTO">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Size="8pt" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INTESTATARIO">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INDIRIZZO">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Size="8pt" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CAP-CITTA'">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Size="8pt" />
                    <FooterStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CAP_CITTA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="RATA N&#176;">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Size="8pt" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.N_RATA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DATA EMISSIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_EMISSIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DATA SCADENZA">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Size="8pt" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="PERIODO RIF.">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.PERIODO") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="ARIAL" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DATA PAGAMENTO">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Size="8pt" Font-Names="ARIAL" 
                        HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PAGAMENTO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="IMPORTO BOLLETTA">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_EMESSO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="ARIAL" 
                        Font-Overline="False" Font-Size="12px" Font-Strikeout="False" 
                        Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="ARIAL" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="IMPORTO PAGATO">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                        Wrap="False" Font-Names="ARIAL" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_PAGATO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                        VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid></div>
    </form>
                                     <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
</body>
</html>