<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoSospensioni.aspx.vb" Inherits="ANAUT_ElencoSospensioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 129px">
    
        <table style="width:100%;">
            <tr>
                <td style="text-align: center">
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
            Style="z-index: 100; left: 337px; top: 11px" Width="100%">ELENCO SOSPENSIONI IN DATA </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
    <asp:Image ID="imgExcel" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
        Style="cursor: pointer; " />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Label ID="lblContatore" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="8pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-family: arial; font-size: 10pt">
                    Ordina per&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" 
                        Checked="True" Font-Names="arial" Font-Size="10pt" GroupName="A" 
                        Text="COGNOME+NOME" />
&nbsp;
                    <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" 
                        Font-Names="arial" Font-Size="10pt" GroupName="A" Text="PROTOCOLLO" />
&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton3" runat="server" AutoPostBack="True" 
                        Font-Names="arial" Font-Size="10pt" GroupName="A" 
                        Text="INDIRIZZO" />
                </td>
            </tr>
            <tr>
                <td style="font-family: arial; font-size: 10pt">
    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="3" GridLines="Vertical" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" Width="100%" BackColor="White" BorderColor="#999999" 
                        BorderStyle="None" BorderWidth="1px">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <EditItemStyle Font-Names="Arial" Font-Size="9pt" Wrap="False" />
        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" Font-Names="Arial" 
            ForeColor="White" Wrap="False" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
            Mode="NumericPages" />
        <AlternatingItemStyle BackColor="#DCDCDC" Font-Names="Arial" 
            Wrap="False" />
        <ItemStyle BackColor="#EEEEEE" Font-Names="Arial" 
            Font-Size="8pt" Wrap="False" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            Font-Names="Arial" Font-Size="8pt" Wrap="False" />
        <Columns>
            <asp:TemplateColumn HeaderText="PROTOCOLLO">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="COD.CONTRATTO (reale)">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.RAPPORTO_REALE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="COGNOME">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="NOME">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="TIPO INDIRIZZO">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.TIPO_INDIRIZZO") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.TIPO_INDIRIZZO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="INDIRIZZO">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.IND_RES_DNTE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="CIVICO">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO_RES_DNTE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="COMUNE">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="SEDE T.">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.FILIALE") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.FILIALE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="MOTIVAZIONI">
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.MOTIVAZIONI") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.MOTIVAZIONI") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="NOTE">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.NOTE_WEB") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.NOTE_WEB") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="OPERATORE">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.OPERATORE") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.OPERATORE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
                </td>
            </tr>
            <tr><td style="font-family: arial; font-size: 9pt">
                <br />
                Elenco Motivazioni:<br />
                1) Tit. Deceduto 
                <br />
                2) Tit. Separato
                <br />
                3) Tit. Trasferito
                <br />
                4) Ricongiungimento
                <br />
                5) Ampliamento
                <br />
                6) Diminuzione
                <br />
                7) Doc.Mancante</td></tr>
        </table>
<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                        <script  language="javascript" type="text/javascript">
                            document.getElementById('dvvvPre').style.visibility = 'hidden';
    
    </script>
    
    </div>
                <p>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 19px; position: absolute;
            top: 584px" Visible="False" Width="525px"></asp:Label>
                </p>
    </form>
</body>
</html>
