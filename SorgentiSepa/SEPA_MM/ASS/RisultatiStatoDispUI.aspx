<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiStatoDispUI.aspx.vb" Inherits="ASS_RisultatiStatoDispUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table style="left: 0px; BACKGROUND-IMAGE: url(../NuoveImm/SfondoMaschere.jpg); WIDTH: 676px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 698px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Risultati
                        Stato Disponibilità U.I.
                        <asp:Label ID="LnlNumeroRisultati" runat="server" 
                            Text="Label" Width="332px"></asp:Label>&nbsp;</strong></span><br />
                    <br />
                    <div style="z-index: 150; left: 11px; overflow: auto; width: 654px; position: absolute;
                        top: 56px; height: 367px">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                            PageSize="20" Style="z-index: 105" Width="133%" HorizontalAlign="Left">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COD. UNITA IMMOBILIARE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SCALA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INTERNO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PIANO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIV_PIANO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA DISDETTA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_DISDETTA_LOCATARIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA SLOGGIO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RICONSEGNA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
                        </asp:DataGrid></div>

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
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="left: 10px; position: absolute; top: 447px" Text="Label"
                        Visible="False" Width="624px"></asp:Label>
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
                        Style="z-index: 2; left: 13px; position: absolute; top: 425px" Width="632px" Visible="False">Nessuna Selezione</asp:TextBox>
                    &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<br />
                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
                        Style="z-index: 102; left: 320px; position: absolute; top: 465px" TabIndex="2"
                        ToolTip="Export  file XLS" />
                    <br />
                    <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                        Style="z-index: 103; left: 456px; position: absolute; top: 465px" TabIndex="3"
                        ToolTip="Nuova Ricerca" />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        Style="z-index: 101; left: 590px; position: absolute; top: 464px" TabIndex="4"
                        ToolTip="Home" />
                    <asp:HiddenField ID="IdContratto" runat="server" Value="0" />
                    <asp:HiddenField ID="CodContratto" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </div>
    </form>
        <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility='hidden';
        </script>
</body>
</html>
