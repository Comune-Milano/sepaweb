<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompensiFacProp.aspx.vb" Inherits="Contabilita_CompensiFacility" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Dettaglio Giornaliero Compensi Facility/Porperty Gestore</title>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="14pt" Text="Compenso Giornaliero Facility "></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                        <asp:DataGrid style="z-index: 105" AutoGenerateColumns="False" 
                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" GridLines="None" ID="DataGridComp" PageSize="15" 
                            runat="server" Width="100%" 
                    CellPadding="2" ForeColor="#333333" CellSpacing="2" >
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
                                <asp:BoundColumn DataField="GIORNO" HeaderText="GIORNO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="ALLOGGI LIBERI" 
                                    DataField="AL_LIBERI" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" 
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                        HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AL_OCCUPATI" HeaderText="ALLOGGI LOCATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="B_LIBERI" HeaderText="BOX LIBERI" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="B_OCCUPATI" HeaderText="BOX LOCATI" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_LIBERI" HeaderText="USI DIVERSI LIBERI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_OCCUPATI" HeaderText="USI DIVERSI LOCATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" Font-Names="Arial" Font-Size="8pt" />
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
                            </Columns>
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="White" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                    <asp:ImageButton ID="btnExport" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Export_Grande.png" />
&nbsp;<asp:ImageButton ID="ImgPDF" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Stampa_Grande.png" style="height: 20px" 
                        Visible="False" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTitolo2" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="14pt" Text="Compenso Giornaliero Property"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                        <asp:DataGrid style="z-index: 105" AutoGenerateColumns="False" 
                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" GridLines="None" ID="DataGridCompProp" PageSize="15" 
                            runat="server" Width="99%" 
                    CellPadding="2" ForeColor="#333333" CellSpacing="2" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#2461BF" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="#D5E1F4" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundColumn DataField="GIORNO" HeaderText="GIORNO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="ALLLOGGI LIBERI" 
                                    DataField="AL_LIBERI" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" 
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                        HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AL_OCCUPATI" HeaderText="ALLOGGI LOCATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="B_LIBERI" HeaderText="BOX LIBERI" ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="B_OCCUPATI" HeaderText="BOX LOCATI" ReadOnly="True">
                                    <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_LIBERI" HeaderText="USI DIVERSI LIBERI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_OCCUPATI" HeaderText="USI DIVERSI LOCATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
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
                                <asp:BoundColumn DataField="AL_OCCUPATI_P" HeaderText="COMP. ALLOGGI LOCATI" 
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
                                <asp:BoundColumn DataField="B_OCCUPATI_P" HeaderText="COMP. BOX LOCATI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_LIBERI_P" HeaderText="COMP. USI DIVERSI LIBERI" 
                                    ReadOnly="True">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="N_OCCUPATI_P" HeaderText="COMP. USI DIVERSILOCATI" 
                                    ReadOnly="True">
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
                    <asp:ImageButton ID="btnExport0" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Export_Grande.png" />
                    <asp:ImageButton ID="ImgPDF0" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Stampa_Grande.png" style="height: 20px" 
                        Visible="False" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
