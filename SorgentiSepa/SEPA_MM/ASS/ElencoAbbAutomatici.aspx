<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoAbbAutomatici.aspx.vb"
    Inherits="ASS_ElencoAbbAutomatici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Abbinamenti</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 14pt;
            color: #FFFFFF;
            background-color: #800000;
            text-align: center;
        }
        .style2
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 10pt;
            color: black;
            text-align: justify;
        }
        .giustificato
        {
            font-family: Arial;
            font-size: 8pt;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td class="style1" colspan="3">
                ELENCO ABBINAMENTI
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lblTotale" runat="server" Width="100%"></asp:Label>
            </td>
            <td align="right">
                <table style="width: 180px;">
                    <tr>
                        <td>
                            <asp:Menu ID="MenuReport" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                                Orientation="Horizontal" RenderingMode="Table">
                                <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" Width="150px"/>
                                <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                                    ForeColor="#0066FF" CssClass="giustificato" Width="150px" />
                                <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                                    VerticalPadding="1px" />
                                <Items>
                                    <asp:MenuItem ImageUrl="../NuoveImm/Img_VisualizzaPiccolo.png" Selectable="False"
                                        Value="">
                                        <asp:MenuItem Text="Abbinamenti Convalidati" Value="1"></asp:MenuItem>
                                        <asp:MenuItem Text="Abbinamenti Scartati" Value="2"></asp:MenuItem>
                                    </asp:MenuItem>
                                </Items>
                            </asp:Menu>
                        </td>
                        <td style="width: 10px; text-align: center;">
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" TabIndex="2"
                                ToolTip="Esporta in Excel" Style="height: 12px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 100%; height: 420px;">
                                <asp:DataGrid ID="DataGridAbb" runat="server" BackColor="White" Font-Bold="False"
                                    Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                    Font-Strikeout="False" Font-Underline="False" GridLines="None" PageSize="20"
                                    Style="z-index: 105; left: 193px; top: 54px" Width="100%" EnableViewState="False"
                                    BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="2" CellSpacing="5"
                                    AutoGenerateColumns="False">
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <ItemStyle ForeColor="Black" BackColor="#DEDFDE" />
                                    <Columns>
                                        <asp:BoundColumn DataField="NUM_OFFERTA" HeaderText="N. OFFERTA">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPO_GRAD" HeaderText="SEZIONE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_SCADENZA_OFF" HeaderText="DATA SCAD. OFFERTA">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="COD_UI" HeaderText="COD. ALLOGGIO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SUPERFICIE" HeaderText="SUPERFICIE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_DISP" HeaderText="DATA DISPONIBILITA'">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NUM_ALL" HeaderText="N. ALLOGGIO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SCALA" HeaderText="SCALA">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PIANO" HeaderText="PIANO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                    <HeaderStyle BackColor="#FFFFCC" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="#990000" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                    <SelectedItemStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                </asp:DataGrid>
                            </div>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
