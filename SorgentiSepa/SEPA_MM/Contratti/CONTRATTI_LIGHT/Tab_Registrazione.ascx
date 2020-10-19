<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Registrazione.ascx.vb" Inherits="Contratti_Registrazione" %>
<div style="left: 8px; width: 1130px; position: absolute;  top: 168px; height: 520px">
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="DATI RELATIVI ALLA REGISTRAZIONE" Width="240px"></asp:Label>
                <table style="border-right: lightgrey 3px solid; border-top: lightgrey 3px solid;
                    border-left: lightgrey 3px solid; border-bottom: lightgrey 3px solid" width="100%">
                    <tr>
                        <td style="width: 254px; height: 16px">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Ufficio registro"></asp:Label>
                        </td>
                        <td style="width: 208px; height: 16px">
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Serie"></asp:Label>
                        </td>
                        <td style="width: 241px; height: 16px">
                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="N.ro registrazione"></asp:Label>
                        </td>
                        <td style="height: 16px">
                            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Data registrazione"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 254px">
                            <asp:DropDownList ID="cmbUfficioRegistro" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="245px" TabIndex="51" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 208px">
                            <asp:TextBox ID="txtSerie" runat="server" Font-Names="Arial" Font-Size="9pt" Width="200px"
                                MaxLength="2" TabIndex="52" ReadOnly="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 241px">
                            <asp:TextBox ID="txtNumRegistrazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="225px" MaxLength="50" TabIndex="53" ReadOnly="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataRegistrazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="10" Width="75px" TabIndex="54" ToolTip="Formato gg/mm/aaaa" 
                                ReadOnly="True" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 254px">
                            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="N.ro repertorio"></asp:Label>
                        </td>
                        <td style="width: 208px">
                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Data repertorio"></asp:Label>
                        </td>
                        <td style="width: 241px">
                            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="N.ro assegnazione PG"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Data assegnazione PG"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 254px">
                            <asp:TextBox ID="txtNumRepertorio" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="245px" MaxLength="100" TabIndex="55" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 208px">
                            <asp:TextBox ID="txtDataRepertorio" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="75px" MaxLength="10" TabIndex="56" ToolTip="Formato gg/mm/aaaa" 
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 241px">
                            <asp:TextBox ID="txtNumAssegnPg" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="225px" MaxLength="50" TabIndex="57" ReadOnly="True" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataAssegnPG" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="75px" MaxLength="10" TabIndex="58" ToolTip="Formato gg/mm/aaaa" 
                                ReadOnly="True" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 254px">
                            <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Modalità Versamento Imposta Registrazione"></asp:Label>
                        </td>
                        <td style="width: 208px">
                        </td>
                        <td style="width: 241px">
                            <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Tipologia Posizione"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Modalità Pagamento"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 254px">
                            <asp:DropDownList ID="cmbModVersamento" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="245px" TabIndex="59" Enabled="False">
                                <asp:ListItem Value="A">ANNUALE</asp:ListItem>
                                <asp:ListItem Value="U">UNICA SOLUZIONE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 208px">
                            <img id="PagAE" alt="Elenco Pagamenti Agenzia delle entrate" src="../../NuoveImm/ElencoPagamenti.png"
                                onclick="ElencoPagamenti();" style="cursor: pointer" />
                        </td>
                        <td style="width: 241px">
                            <asp:Label ID="lblTipoPosizione" runat="server" Font-Bold="True" 
                                Font-Names="Arial" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblModoPagamento" runat="server" Font-Bold="True" 
                                Font-Names="Arial" Font-Size="8pt"></asp:Label>
                        </td>
                    </tr>
                    <tr><td>
                        <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Note"></asp:Label></td></tr>
                    <tr><td colspan="4">
                            <asp:TextBox ID="txtNotereg" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="794px" MaxLength="4000" TabIndex="53" Height="50px" 
                                TextMode="MultiLine" Enabled="False"></asp:TextBox>
                        </td></tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="IMPOSTA DI REGISTRAZIONE" Width="160px"></asp:Label>
                <table style="border-right: lightgrey 3px solid; border-top: lightgrey 3px solid;
                    border-left: lightgrey 3px solid; border-bottom: lightgrey 3px solid" width="100%">
                    <tr>
                        <td style="width: 140px">
                            <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Percentuale tot sul canone"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Percentuale spettante al conduttore"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Percentuale spettante al locatore"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px; height: 21px">
                            <asp:Label ID="txtPercTotSu" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" Text="0" Style="text-align: right" Width="110px" TabIndex="60"></asp:Label>
                            <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="text-align: right" Text="%" Width="16px" TabIndex="61"></asp:Label>
                        </td>
                        <td style="width: 177px; height: 21px">
                            <asp:Label ID="txtPercConduttore" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" Style="text-align: right" Text="0" Width="150px" TabIndex="62"></asp:Label>
                            <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="text-align: right" Text="%" Width="16px" TabIndex="63"></asp:Label>
                        </td>
                        <td style="height: 21px">
                            <asp:Label ID="txtPercLocatore" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" Text="0" Style="text-align: right" Width="139px" TabIndex="64"></asp:Label>
                            <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="text-align: right" Text="%" Width="16px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="RIPARTIZIONE IMPOSTA DI REGISTRAZIONE" Width="256px"></asp:Label>
                <table style="border-right: lightgrey 3px solid; border-top: lightgrey 3px solid;
                    border-left: lightgrey 3px solid; border-bottom: lightgrey 3px solid" width="100%">
                    <tr>
                        <td style="width: 135px">
                            <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Percentuale sul canone"></asp:Label>
                        </td>
                        <td style="width: 115px">
                            <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Percentuale Conduttore"></asp:Label>
                        </td>
                        <td style="width: 106px">
                            <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Percentuale Locatore"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 135px; height: 21px">
                            <asp:Label ID="lblRegTot" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="text-align: right" Text="0" Width="112px" TabIndex="65"></asp:Label>
                            <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="text-align: right" Text="€" Width="5px"></asp:Label>
                        </td>
                        <td style="height: 21px; width: 115px;">
                            <asp:Label ID="lblRegCond" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="0" Style="text-align: right" Width="92px" TabIndex="66"></asp:Label>
                            <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="text-align: right" Text="€" Width="5px"></asp:Label>
                        </td>
                        <td style="height: 21px; width: 106px;">
                            <asp:Label ID="lblRegLoc" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="text-align: right" Text="0" Width="82px" TabIndex="67"></asp:Label>
                            <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="text-align: right" Text="€" Width="5px"></asp:Label>
                        </td>
                        <td style="height: 21px">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
