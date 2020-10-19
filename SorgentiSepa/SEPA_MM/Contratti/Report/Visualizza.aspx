<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Visualizza.aspx.vb" Inherits="Contratti_Report_Visualizza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Distinta</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text="Label"></asp:Label>
    <br />
    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="#2461BF" Font-Names="Arial" Font-Size="9pt" />
        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <ItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTATORE") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="INTESTATARIO">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="RATA NUMERO">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RATA") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="PERIODO DI RIFERIMENTO">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PERIODO") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="COMUNE">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="GESTORE">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.ALER") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="SINDACATI">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.SINDACATO") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="ALTRO">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.ALTRO") %>'></asp:Label>
                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="TOT">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TOT") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                    VerticalAlign="Top" Wrap="False" />
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    <div>
    
    </div>
    </form>
        <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    
    </script>
</body>
</html>
