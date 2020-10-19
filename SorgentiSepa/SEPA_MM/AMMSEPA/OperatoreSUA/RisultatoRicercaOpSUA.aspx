<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoRicercaOpSUA.aspx.vb"
    Inherits="AMMSEPA_OperatoreSUA_RisultatoRicercaOpSUA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RisultatoRicercaD</title>
    <script type="text/javascript">
        var Uscita;
        Uscita = 0;
        var Selezionato;
        function IMG1_onclick() {
        }
    </script>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Elenco Operatori" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                    &nbsp; <span style="font-size: 24pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:Label ID="Label10" runat="server" Font-Names="ARIAL" Font-Size="12pt" Text=""></asp:Label></strong></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="../Immagini/SfondoHome.jpg" height="75px" width="100%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 80%">
                                            <div style="width: 100%; overflow: auto; height: 480px;">
                                                <asp:DataGrid ID="DataGrid1" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                                                    Font-Size="8pt" Width="100%" PageSize="25" BackColor="White" Font-Bold="False"
                                                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                    GridLines="None" AllowPaging="True" ShowFooter="True" BorderColor="Navy" BorderStyle="Solid"
                                                    BorderWidth="1px">
                                                    <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="#3333FF"
                                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                        ForeColor="White" Wrap="False" Height="25px"></HeaderStyle>
                                                    <Columns>
                                                        <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="OPERATORE" HeaderText="Cognome" ReadOnly="True" 
                                                            Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="NOME" HeaderText="NOME" ReadOnly="True" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="ENTE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ENTE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="OPERATORE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OPERATORE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="COGNOME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="NOME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="REVOCA">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FL_REVOCATO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="OP.CLIENT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CLIENT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="OP. WEB">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WEB") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="OP. ESTERNO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OP_ESTERNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="DITTA ESTERNA">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DITTA_ESTERNA") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <AlternatingItemStyle BackColor="Gainsboro" />
                                                    <PagerStyle Mode="NumericPages" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%">
                                            <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                                                Width="777px" BackColor="#F2F5F1" BorderStyle="None" BorderWidth="0px">Nessuna Selezione</asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;">
                                &nbsp;
                            </td>
                            <td style="height: 30px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            <center>
                                                <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="../../NuoveImm/Img_Visualizza.png"
                                                    ToolTip="Visualizza" />
                                            </center>
                                        </td>
                                        <td style="width: 20%">
                                            <center>
                                                <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="../../NuoveImm/Img_NuovaRicerca.png"
                                                    ToolTip="Nuova Ricerca" /></center>
                                        </td>
                                        <td style="width: 20%">
                                            <center>
                                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                                    ToolTip="Home" /></center>
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="LBLID" runat="server" />
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
