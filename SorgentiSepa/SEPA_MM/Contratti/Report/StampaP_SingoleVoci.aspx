<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaP_SingoleVoci.aspx.vb" Inherits="Contratti_Report_StampaP_SingoleVoci" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: left">
        &nbsp;
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 13px; position: absolute;
            top: 12px" Visible="False" Width="525px"></asp:Label>
        <table width="100%">
            <tr>
                <td style="text-align: center">
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
            Style="z-index: 100; left: 215px; top: 11px; font-size: small;" Width="558px">PAGAMENTI PERVENUTI PER SINGOLE VOCI BOLLETTA</asp:Label></td>
            </tr>
        </table>
        &nbsp;
        <br />
        <asp:HiddenField ID="TextBox1" runat="server" />
        <br />
        <asp:ImageButton ID="imgExcel" runat="server" 
            ImageUrl="~/NuoveImm/Img_ExportExcel.png" />
        <br />


    <br />
        <br />
                    <asp:Label ID="lblErp0" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" Width="558px">COMPETENZA COMUNE</asp:Label>
    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
        <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateColumn HeaderText="DESCRIZIONE VOCE">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="IMPORTO">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DETTAGLI">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
        <br />
                    <asp:Label ID="lblErp1" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" Width="558px">COMPETENZA GESTORE</asp:Label>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse0" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
        <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateColumn HeaderText="DESCRIZIONE VOCE">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="IMPORTO">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DETTAGLI">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
        <br />
    

                    <asp:Label ID="lblErp2" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" Width="558px">SINDACATI</asp:Label>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse1" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
        <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateColumn HeaderText="DESCRIZIONE VOCE">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="IMPORTO">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DETTAGLI">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
        <br />
                    <asp:Label ID="lblErp3" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" Width="558px">ALTRO</asp:Label>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse2" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
        <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateColumn HeaderText="DESCRIZIONE VOCE">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="IMPORTO">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DETTAGLI">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
        <br />

        <br />
        <table style="border-right: dodgerblue thin solid; border-top: dodgerblue thin solid;
            border-left: dodgerblue thin solid; border-bottom: dodgerblue thin solid" 
            width="100%" id="TableSingoli">
            <tr>
                <td>
        <table width="100%">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblErp" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" Width="558px">DI CUI CONTRATTI ERP</asp:Label></td>
            </tr>
        </table>
    
    <asp:DataGrid ID="DataGridERP" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
        <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" /><Columns>
            <asp:TemplateColumn HeaderText="DESCRIZIONE VOCE">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="IMPORTO">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DETTAGLI">
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    </asp:DataGrid><br />
        <table width="100%">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" Width="558px">DI CUI CONTRATTI EQUO CANONE</asp:Label></td>
            </tr>
        </table>
        <asp:DataGrid ID="datagidEQC" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
            <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:TemplateColumn HeaderText="DESCRIZIONE VOCE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="IMPORTO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DETTAGLI">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:DataGrid><br />
                    <table width="100%">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" Width="558px">DI CUI CONTRATTI L. 431</asp:Label></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGridL431" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
            <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:TemplateColumn HeaderText="DESCRIZIONE VOCE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="IMPORTO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DETTAGLI">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:DataGrid><br />
                    <table width="100%">
            <tr>
                <td style="text-align: left; height: 24px;">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" Width="558px">DI CUI CONTRATTI USI DIVERSI</asp:Label></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGridUSDX" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
            <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:TemplateColumn HeaderText="DESCRIZIONE VOCE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="IMPORTO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DETTAGLI">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:DataGrid><br />
                    <table width="100%">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100; left: 215px; top: 11px" Width="558px">DI CUI CONTRATTI ABUSIVI</asp:Label></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGridNone" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
            <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:TemplateColumn HeaderText="DESCRIZIONE VOCE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="IMPORTO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DETTAGLI">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    </asp:DataGrid><br />
                    <table width="100%">
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="lblTotDeiTot" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                    ForeColor="RoyalBlue" Style="z-index: 100; left: 215px; top: 11px" Width="100%">SOMMA DEI TOTALI PER CIASCUNA TIPOLOGIA DI CONTRATTO PARI A €</asp:Label>
                                <br />
                                <br />
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <br />
        <br />
    
    </div>
        &nbsp;
    </form>
                <script  language="javascript" type="text/javascript">
         if (document.getElementById('TextBox1').value!='1') {
                 document.getElementById('TableSingoli').style.visibility='hidden';
            }
    </script>
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    
    </script>
</body>
</html>
