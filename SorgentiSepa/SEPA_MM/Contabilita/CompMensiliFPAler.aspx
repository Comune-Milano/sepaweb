<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompMensiliFPAler.aspx.vb" Inherits="Contabilita_CompMensiliFPAler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Compensi Mensili Property/Facility Gestore</title>

    <style type="text/css">
        .style1
        {
            height: 24px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%; visibility: visible;">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="14pt" Text="Compenso Mensile Facility "></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                        <asp:DataGrid style="z-index: 105" AutoGenerateColumns="False" 
                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" ID="DgvFacility" PageSize="15" 
                            runat="server" Width="98%" 
                    CellPadding="0" ForeColor="#333333" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#2461BF" Font-Bold="False" Font-Italic="False" 
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                Wrap="False" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="#D5E1F4" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                Wrap="False" />
                            <ItemStyle BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" 
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ANNO" HeaderText="ANNO" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ANNO_MESE" HeaderText="MESE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AL_LIBERI_F" HeaderText="COMP. ALLOGGI LIBERI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AL_OCCUPATI_F" HeaderText="COMP. ALLOGGI LOCATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="B_LIBERI_F" HeaderText="COMP. BOX LIBERI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="B_OCCUPATI_F" HeaderText="COMP. BOX LOCATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_LIBERI_F" HeaderText="COMP. USI DIVERSI LIBERI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_OCCUPATI_F" HeaderText="COMP. USI DIVERSI LOCATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOT_M" HeaderText="TOT IMPONIBILE" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOT_IVATO" HeaderText="TOT IVATO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="White" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" class="style1">
                    <asp:ImageButton ID="btnExport0" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Export_Grande.png" />
                    </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTitolo2" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="14pt" Text="Compenso Mensile Property"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                        <asp:DataGrid style="z-index: 105; margin-top: 0px;" AutoGenerateColumns="False" 
                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" ID="DataGridCompProp" PageSize="15" 
                            runat="server" Width="98%" 
                    CellPadding="0" ForeColor="#333333" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#2461BF" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="#D5E1F4" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundColumn DataField="ANNO" HeaderText="ANNO" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ANNO_MESE" HeaderText="MESE" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AL_LIBERI_P" HeaderText="COMP. ALLOGGI LIBERI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AL_OCCUPATI_P" HeaderText="COMP. ALLOGGI OCCUPATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="B_LIBERI_P" HeaderText="COMP. BOX LIBERI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="B_OCCUPATI_P" HeaderText="COMP. BOX OCCUPATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_LIBERI_P" HeaderText="COMP. USI DIVERSILIBERI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_OCCUPATI_P" HeaderText="COMP. USI DIVERSI OCCUPATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOT_M" HeaderText="TOT IMPONIBILE" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOT_IVATO" HeaderText="TOT IVATO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="White" />
                        </asp:DataGrid>
            </td>
        </tr>

        <tr>
            <td>
                    <asp:ImageButton ID="btnExport1" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Export_Grande.png" />
            </td>
        </tr>

        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTitolo3" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="14pt" Text="Totale Compenso Mesile di Facility e Property"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                        <asp:DataGrid style="z-index: 105; margin-top: 0px;" AutoGenerateColumns="False" 
                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" ID="DataGridCompTotale" PageSize="15" 
                            runat="server" Width="50%" 
                    CellPadding="2" ForeColor="#333333" CellSpacing="2" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#2461BF" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundColumn DataField="ANNO" HeaderText="ANNO" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" Font-Size="10pt" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" Font-Size="8pt" 
                                        HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ANNO_MESE" HeaderText="MESE" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" Font-Size="10pt" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                                        Wrap="False" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOTALE" HeaderText="TOT IMPONIBILE" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" Font-Size="10pt" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOT_IVATO" HeaderText="TOT IVATO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Size="10pt" Font-Strikeout="False" Font-Underline="False" 
                                        HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" 
                                        HorizontalAlign="Right" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="White" />
                        </asp:DataGrid>
            </td>
        </tr>

        <tr>
            <td>
                    <asp:ImageButton ID="btnExport2" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Export_Grande.png" />
            </td>
        </tr>

        <tr>
            <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 13px; top: 598px; height: 13px; width: 719px;"
                        Text="Label" Visible="False" Width="100%"></asp:Label>
            </td>
        </tr>

    </table>
    </form>
</body>
</html>
