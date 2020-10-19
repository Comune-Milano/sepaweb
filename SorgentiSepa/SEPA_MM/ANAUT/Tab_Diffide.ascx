<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Diffide.ascx.vb"
    Inherits="ANAUT_Tab_Diffide" %>
<table width="97%" style="border: 1px solid #0066FF; vertical-align: top;">
    <tr>
        <td style="padding-left: 15px;">
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                Font-Size="9pt">ELENCO DIFFIDE</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table style="margin-left: 10px; width: 99%; height: 50px;">
                <tr>
                    <td style="vertical-align: top;">
                        <div style="overflow-x: hidden; overflow-y: auto; width: 100%; height: 70px;" id="elencoComp">
                            <asp:DataGrid ID="DataGridDiffide" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" Width="100%" CellPadding="1">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" Wrap="False" Visible="False" />
                                <HeaderStyle BackColor="#FFCF88" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#800000" Wrap="False" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PROTOCOLLO" HeaderText="PROTOCOLLO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO1" HeaderText="TIPO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_CREAZIONE" HeaderText="DATA CREAZIONE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_ANNULLO" HeaderText="DATA ANNULLO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="VISUALIZZA" HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        &nbsp
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
