<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultSfEsec.aspx.vb" Inherits="Contratti_RisultSfEsec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table style="left: 0px; BACKGROUND-IMAGE: url(../NuoveImm/SfondoMascheraContratti.jpg); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Risulati
                        Sfratto Esecutivo
                        <asp:Label ID="LnlNumeroRisultati" runat="server" 
                            Text="Label" Width="332px"></asp:Label>&nbsp;</strong></span><br />
                    <br />
                    <div style="z-index: 150; left: 11px; overflow: auto; width: 776px; position: absolute;
                        top: 56px; height: 367px">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                            PageSize="20" Style="z-index: 105" Width="180%">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD_CONTRATTO" ReadOnly="True"
                                    Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COD. CONT.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="65px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA CONVALIDA SFRATTO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_CONVALIDA_SFRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA ESECUZIONE SFRATTO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_ESECUZIONE_SFRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA CONFERMA F. P.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_CONFERMA_FP") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA RINVIO SFRATTO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RINVIO_SFRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INTESTATARIO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COD.FISCALE/P.IVA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CFIVA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COD. U.I.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SC.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INTERNO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CAP">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CAP") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COMUNE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PROV_COMUNE") %>'></asp:Label>
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
                        Style="z-index: 2; left: 13px; position: absolute; top: 425px" Width="632px">Nessuna Selezione</asp:TextBox>
                    &nbsp; &nbsp;&nbsp; &nbsp;
                    <br />
                    <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                        Style="z-index: 102; left: 306px; position: absolute; top: 465px" TabIndex="1"
                        ToolTip="Visualizza" />
                    &nbsp;
                    <br />
                    <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                        Style="z-index: 103; left: 582px; position: absolute; top: 465px" TabIndex="3"
                        ToolTip="Nuova Ricerca" />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        Style="z-index: 101; left: 716px; position: absolute; top: 464px" TabIndex="4"
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
