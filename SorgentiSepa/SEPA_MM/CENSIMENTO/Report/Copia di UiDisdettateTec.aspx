<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copia di UiDisdettateTec.aspx.vb" Inherits="CENSIMENTO_UiDisponibili" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Elenco U.I. Disponibili</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
    <table width="100%">
    <tr>
    <td align="center">
                    <span style="font-family: Arial"><strong style="text-align: center">
                        Dati Tecnici Unita Immobiliari Disdettate</strong></span></td>
    </tr>
    </table>
        <table width="200%">
            <tr>
                <td style="text-align: center; height: 21px;">
                    <span style="font-family: Arial"><strong>
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                            Style="z-index: 102; right: 28px; left: 16px; position: absolute; top: 16px"
                            TabIndex="2" ToolTip="Esporta in Excel" />
                        </strong></span></td>
            </tr>
            <tr>
                <td style="text-align: center; height: 21px;">
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; height: 21px;">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                    <asp:DataGrid ID="DataGridUnitDispo" runat="server" AutoGenerateColumns="False" CellPadding="5"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Height="147px" Style="z-index: 11; left: 18px; top: 81px" Width="100%" BorderColor="PowderBlue" BorderWidth="2px">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White"
                            Wrap="False" />
                        <EditItemStyle BackColor="#2461BF" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            Wrap="False" />
                        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            Wrap="False" Font-Size="8pt" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" Font-Size="8pt" />
                        <ItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" Font-Size="8pt" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="TIPOLOGIA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="STATO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DISPONIBILITA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COD. U.I.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COMPLESSO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMPLESSO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SEDE T.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FILIALE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COMUNE">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="QUARTIERE">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUARTIERE") %>'></asp:Label>
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
                            <asp:TemplateColumn HeaderText="PIANO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIVELLO_PIANO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="INTERNO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SUP. NETTA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SUP_NETTA") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SUP. CONV.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SUP_CONV") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SUP. CATAST.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SUPERFICIE_CATASTALE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SUP. COMMERC.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SUP_COMM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PIANO VENDITA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PIANO_VENDITA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RISERVATA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RISERVATA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA DISDETTA">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_DISDETTA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA PRE-SLOGGIO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PRE_SLOGGIO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="White" Wrap="False" />
                    </asp:DataGrid></td>
            </tr>
        </table>
    
    </div>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 12; left: -11px; top: 22px" Text="Label"
            Visible="False" Width="717px"></asp:Label></form>
                <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>

</body>
</html>
