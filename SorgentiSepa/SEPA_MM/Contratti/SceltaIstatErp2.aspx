<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaIstatErp2.aspx.vb" Inherits="Contratti_SceltaIstatErp2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Adeguamento ISTAT</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="text-align: center">
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
            Style="z-index: 100; left: 449px; position: static; top: 10px" Width="280px">AGGIORNAMENTI ISTAT ERP</asp:Label></td>
            </tr>
            <tr>
                <td style="height: 18px">
    <asp:Image ID="imgExcel" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
        Style="cursor: pointer" /></td>
            </tr>
            <tr>
                <td>
                    <br />
    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="#2461BF" Font-Names="Arial" Font-Size="9pt" />
        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" Font-Names="Arial" 
            ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
        <ItemStyle BackColor="#EFF3FB" Font-Names="Arial" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateColumn HeaderText="COD. CONTRATTO">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DATA DECORRENZA">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DECORRENZA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DAL">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INIZIO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="AL">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FINE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="CANONE INIZIALE">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMP_CANONE_INIZIALE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="IMPORTO AGGIORNAMENTO">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_TR_AGG") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="CANONE AGGIORNATO">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_CANONE_AGG") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid></td>
            </tr>
        </table>
        <br />
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 19px; position: absolute;
            top: 584px" Visible="False" Width="525px"></asp:Label>
        &nbsp;
    
    </div>
    </form>


    &nbsp;&nbsp;<br />
    <br />
    <br />


    <br />
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    
    </script>
    
            <script type="text/javascript">
                window.focus();
                self.focus();
</script>
</body>
</html>
