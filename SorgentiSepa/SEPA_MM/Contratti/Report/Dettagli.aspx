<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dettagli.aspx.vb" Inherits="Contratti_Report_Dettagli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dettagli</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/NuoveImm/Img_ExportExcel.png" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
    
    </div>
    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" Font-Bold="False" Font-Italic="False" 
        Font-Names="Arial" Font-Overline="False" Font-Size="XX-Small" 
        Font-Strikeout="False" Font-Underline="False">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="Aqua" Font-Names="Arial" Font-Size="9pt" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Names="Arial" />
        <ItemStyle BackColor="Gainsboro" Font-Names="Arial" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
            Font-Italic="False" Font-Names="ARIAL" Font-Overline="False" 
            Font-Strikeout="False" Font-Underline="False" />
        <Columns>
            <asp:TemplateColumn HeaderText="N.BOLLETTA">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                    Font-Strikeout="False" Font-Underline="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="RATA">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="COD.CONTRATTO">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
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
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DATA EMISSIONE">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_EMISSIONE") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_EMISSIONE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DATA PAGAMENTO">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PAGAMENTO") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PAGAMENTO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="PERIODO">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.PERIODO") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.PERIODO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DATA SCADENZA">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="INDIRIZZO">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="NOTE">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="EMESSO">
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="PAGATO">
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.IMP_PAGATO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    </form>
    

    
</body>
</html>
