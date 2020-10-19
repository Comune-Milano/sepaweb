<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioVociStruttura.aspx.vb"
    Inherits="DettaglioVociStruttura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Situazione contabile</title>
</head>
<body style="background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server" target="modal">
    <table style="width: 99%; position: absolute; top: 15px; left: 9px;">
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">SITUAZIONE
                    CONTABILE DAL <%= informazioniData%></span></strong>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <%= titolo%></span></strong>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; width: 877px; height: 480px;">
                    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="Black" Height="200px" PageSize="30" Style="table-layout: auto;
                        clip: rect(auto auto auto auto); direction: ltr; border-collapse: separate" Width="860px"
                        GridLines="Vertical" CellPadding="4" CellSpacing="0" BorderStyle="Solid" EditItemStyle-BorderWidth="1" EditItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" ForeColor="Fuchsia" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" Mode="NumericPages" ForeColor="Blue" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="STRUTTURA" HeaderText="STRUTTURA" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderWidth="1">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BUDGET_INIZIALE" HeaderText="BUDGET INIZIALE" ItemStyle-HorizontalAlign="Right" ItemStyle-BorderWidth="1">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BUDGET_ASSESTATO_+_VAR." HeaderText="BUDGET ASSESTATO + VAR."
                                ItemStyle-HorizontalAlign="Right" ItemStyle-BorderWidth="1">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DISPONIBILITA_RESIDUA" HeaderText="DISPONIBILITA' RESIDUA"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-BorderWidth="1">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_PRENOTATO" HeaderText="TOTALE PRENOTATO" ItemStyle-HorizontalAlign="Right" ItemStyle-BorderWidth="1">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_CONSUNTIVATO" HeaderText="TOTALE CONSUNTIVATO"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-BorderWidth="1">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_RIT" HeaderText="TOTALE RIT. LEGGE CONSUNTIVATE" ItemStyle-HorizontalAlign="Right"  ItemStyle-BorderWidth="1">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_CERTIFICATO" HeaderText="TOTALE CERTIFICATO"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-BorderWidth="1">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_PAGATO" HeaderText="TOTALE PAGATO" ItemStyle-HorizontalAlign="Right"  ItemStyle-BorderWidth="1">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            
                        </Columns>
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="White" Wrap="True" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
