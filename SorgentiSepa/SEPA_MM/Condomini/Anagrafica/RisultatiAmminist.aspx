<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiAmminist.aspx.vb" Inherits="Condomini_Anagrafica_RisultatiAmminist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 786px;
        }
        .style1
        {
            font-family: Arial;
            color: #800000;
            font-size: 14pt;
        }
    </style>
</head>
<body  style="background-attachment: fixed; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server">

    <table style="width:100%;">
        <tr>
            <td class="style1">
                <strong>RISULTATI RICERCA AMMINISTRATORI</strong></td>
        </tr>
        <tr>
            <td style="font-size: 8pt">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                        <div style=" width: 98%; height: 478px; overflow: auto;">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            PageSize="120" 
                            
                            meta:resourcekey="DataGrid1Resource1" TabIndex="1" Width="95%">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Mode="NumericPages" Wrap="False" 
                                Position="TopAndBottom" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COGNOME">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOME">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COD.FISCALE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CF") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="P.IVA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARTITA_IVA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        </div>
                        </td>
        </tr>
        <tr>
            <td>
                        <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                            
                            Style="border: 1px solid white; z-index: 5; " 
                            Width="482px" meta:resourcekey="TextBox3Resource1" Text="Nessua Selezione"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td>
                        <table style="width:100%;">
                            <tr>
                                <td>
                        <asp:HiddenField ID="txtid" runat="server" />
                                </td>
                                <td style="text-align: right">
                            <asp:ImageButton ID="btnVisualizza" runat="server" 
                                ImageUrl="~/NuoveImm/Img_Visualizza.png" ToolTip="Visualizza l'amministratore" />
                                </td>
                                <td>
                            <img alt="Torna alla pagina HOME" src="../Immagini/Img_Home.png" 
                                style="float: right; cursor:pointer "onclick="document.location.href='../pagina_home.aspx';" /></td>
                            </tr>
                        </table>
                        </td>
        </tr>
    </table>

    </form>
</body>
</html>
