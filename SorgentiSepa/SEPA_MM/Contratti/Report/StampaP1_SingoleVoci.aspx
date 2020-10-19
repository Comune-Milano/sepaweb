<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaP1_SingoleVoci.aspx.vb" Inherits="Contratti_Report_StampaP1_SingoleVoci" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: left">
        &nbsp;
        <table width="100%">
            <tr>
                <td style="text-align: center">
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
            Style="z-index: 100; left: 215px; top: 11px" Width="100%">DISTINTA PER SINGOLE VOCI BOLLETTA</asp:Label></td>
            </tr>
        </table>
        &nbsp;
        <br />
    <asp:Image ID="imgExcel" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
        Style="cursor: pointer" /><br />
    <br />
        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="12pt" Text="Voci Comune di Milano"></asp:Label>
        <br />


    <br />
    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
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
                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
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
                    <asp:Label ID="Label3" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
        <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="12pt" Text="Deposito Cauzionale"></asp:Label>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse0" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
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
                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
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
                    <asp:Label ID="Label6" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
        <br />
    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="12pt" Text="Oneri Accessori"></asp:Label>
        &nbsp;<script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    
    </script><br />
    <asp:DataGrid ID="DataGridRateEmesse1" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
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
                    <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                    <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
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
                    <asp:Label ID="Label9" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    
    
        <br />
    
    
    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="12pt" Text="Quota Sindacale"></asp:Label>
    <asp:DataGrid ID="DataGridRateEmesse2" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
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
                    <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                    <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
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
                    <asp:Label ID="Label12" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    
        <br />
    
    
    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="12pt" Text="Bollo MAV"></asp:Label>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse3" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
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
                    <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                    <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
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
                    <asp:Label ID="Label15" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    
        <br />
    
    
    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="12pt" Text="Spese MAV"></asp:Label>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse4" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
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
                    <asp:Label ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                    <asp:Label ID="Label17" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
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
                    <asp:Label ID="Label18" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    
        <br />
    
    
    <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="12pt" Text="Imposte di Registro"></asp:Label>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse5" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
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
                    <asp:Label ID="Label19" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                    <asp:Label ID="Label20" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
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
                    <asp:Label ID="Label21" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    
        <br />
    
    
    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="arial" 
            Font-Size="12pt" Text="Imposte di Bollo su Contratti"></asp:Label>
        <br />
    <asp:DataGrid ID="DataGridRateEmesse6" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
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
                    <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                    <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
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
                    <asp:Label ID="Label24" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    
    <p>
                                <asp:Label ID="lblTotDeiTot" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                    ForeColor="RoyalBlue" Style="z-index: 100; left: 215px; top: 11px" Width="100%">TOTALE INCASSATO  PARI A €</asp:Label></p>
        <p>
                                <asp:Label ID="lblTotDeiTot0" runat="server" Font-Bold="True" 
                                    Font-Names="Arial" Font-Size="12pt"
                                    ForeColor="#CC0000" Style="z-index: 100; left: 215px; top: 11px" 
                                    Width="100%" Visible="False">ATTENZIONE...TOTALE INCASSATO PER BOLLETTE ANNULLATE  PARI A €</asp:Label></p>
    <p>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 19px; 
            top: 584px" Visible="False" Width="525px"></asp:Label>
        </p>
        </div>
    </form>
</body>
</html>