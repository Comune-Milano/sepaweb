<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiEdifici.aspx.vb"
    Inherits="CENSIMENTO_RisultatiEdifici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>RisultatoRicercaD</title>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat;">
    <form id="Form1" method="post" runat="server" defaultbutton="btnVisualizza" defaultfocus="btnVisualizza">
    &nbsp;&nbsp;&nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 101; left: 719px; position: absolute; top: 551px" ToolTip="Home" />
    <table style="left: 0px; position: absolute; top: 0px; height: 466px; width: 703px;">
        <tr>
            <td style="width: 670px">
                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
                    Style="z-index: 102; left: 172px; position: absolute; top: 551px; right: 401px;" />
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                    Edifici Trovati<asp:Label ID="LnlNumeroRisultati" runat="server" Text="Label"></asp:Label>
                    &nbsp;</strong></span><br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <div style="left: 8px; overflow: auto; width: 776px; position: absolute; top: 51px;
                    height: 469px">
                    <asp:DataGrid ID="DataGrid1" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                        AllowPaging="True" Font-Size="8pt" Width="97%" PageSize="200" Style="z-index: 105;
                        left: 193px; top: 54px" BackColor="White" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" GridLines="None">
                        <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="White"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="COD.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_EDIFICIO") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_EDIFICIO") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DENOMINAZIONE">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="INDIRIZZO">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CIVICO">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COMUNE">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ZONA">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ZONA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SEDE T.">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SEDET") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="B.M.">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Modifica" CausesValidation="false"
                                        CommandName="Edit">Seleziona</asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton3" runat="server" Text="Aggiorna" CommandName="Update"></asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Annulla" CausesValidation="false"
                                        CommandName="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid>
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
    <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
        Style="z-index: 102; left: 309px; position: absolute; top: 551px; right: 685px;"
        ToolTip="Visualizza" Height="20" />
    <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
        Style="z-index: 103; left: 553px; position: absolute; top: 551px; right: 366px;"
        ToolTip="Nuova Ricerca" Height="20" />
    &nbsp; &nbsp;&nbsp; &nbsp;
    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
        Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
        Style="z-index: 2; left: 7px; position: absolute; top: 522px" Width="632px">Nessuna Selezione</asp:TextBox>
    <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
        MaxLength="100" Style="left: 166px; position: absolute; top: 460px; background-color: #ffffff;
        z-index: -1;" Width="152px" ForeColor="White"></asp:TextBox>
    <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
        BorderStyle="None" MaxLength="100" Style="left: 5px; position: absolute; top: 459px;
        background-color: #ffffff; z-index: -1;" Width="152px" ForeColor="White"></asp:TextBox>
    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Style="left: 8px; position: absolute; top: 546px; z-index: 2;"
        Text="Label" Visible="False" Width="624px"></asp:Label>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
