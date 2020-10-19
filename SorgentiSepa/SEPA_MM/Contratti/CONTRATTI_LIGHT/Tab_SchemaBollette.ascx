<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_SchemaBollette.ascx.vb" Inherits="Contratti_Tab_SchemaBollette" %>
<div style="left: 8px; width: 1130px; position: absolute; top: 168px; height: 520px">
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Text="SCHEMA VOCI BOLLETTA" Width="649px" Height="16px"></asp:Label><br />
                <table style="border-color: #C0C0C0; border-top-width: 3px; border-left-width: 3px;
                    border-bottom-width: 3px; width: 100%; border-right-width: 3px;">
                    <tr>
                        <td style="width: 86%">
                            &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            &nbsp;&nbsp;
                            </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 86%">
                            <div id="DivRoot3" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow3">
    </div>
    <div style="width: 1000px;overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent3" >
        <asp:DataGrid ID="DataGridSchema" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="30" Style="z-index: 101; width: 100%;" TabIndex="1" 
            BorderWidth="0px" CellPadding="2">
            <AlternatingItemStyle BackColor="#CCCCCC" BorderStyle="None" />
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID_SCHEMA" Visible="False">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE" ReadOnly="True">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Names="Courier New" 
                        Font-Size="8pt" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" Font-Names="Courier New" 
                        Font-Size="8pt" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IMPORTO_SINGOLA_RATA" HeaderText="IMPORTO">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DA_RATA" HeaderText="DA RATA">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PER_RATE" HeaderText="PER RATE">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" 
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Italic="False" Font-Names="Courier New"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="White" Wrap="False" Height="25px" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
    </div>

    <div id="DivFooterRow3" style="overflow:hidden">
    </div>
</div>
                        </td>
                        <td style="width: 100%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td height="20">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="20">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="20">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblVociAutomatiche" runat="server" Font-Names="arial" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td valign="top">
                            <asp:Label ID="lblProssimaEmissione" runat="server" Font-Names="arial" Font-Size="8pt"
                                Style="font-weight: 700">Prossima Emissione:</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblVociOneri" runat="server" Font-Names="arial" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
<td valign="middle">
        
            <table style="width:100%;">
                <tr>
                    <td>
        <asp:Label ID="lblSindaCato" runat="server" Font-Names="arial" 
                Font-Size="8pt">Sindacato di Riferimento:</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="lstSindacati" runat="server" Font-Names="arial" 
                            Font-Size="8pt" Enabled="False">
                        </asp:DropDownList>
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
<script type="text/javascript">
    
    

    
</script>
