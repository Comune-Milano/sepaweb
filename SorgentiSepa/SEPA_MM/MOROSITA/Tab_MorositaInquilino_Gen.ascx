<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_MorositaInquilino_Gen.ascx.vb" Inherits="Tab_MorositaInquilino_Gen" %>
<table style="width: 765px; height: 200px">
    <tr>
        <td><table>
            <tr>
                <td>
                    <table style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid">
                        <tr>
                            <td colspan = "2">
                    <asp:Label ID="label_Lettera1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="224px">Dettaglio M.AV. Global Service:</asp:Label></td>
                            <td colspan = "4">
                                <asp:ImageButton ID="btnAnnulloGlobal" runat="server" 
                                    ImageUrl="Immagini/btnAnnulla.png" style="height: 12px" OnClientClick ="Conferma();"/>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="85px">Bollo:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtBollo_MG" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="80px"></asp:TextBox></td>
                            <td style="text-align: left">
                                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                            <td style="width: 10px">
                            </td>
                            <td>
                                <asp:Label ID="lblNumRata" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">N./Tipo</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTipo_MG" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    MaxLength="50" ReadOnly="True" TabIndex="5" Width="90px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRimborso" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="85px">Rimborso Spese:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtRimborso_MG" runat="server" Font-Bold="True" Font-Size="8pt"
                                    MaxLength="10" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="80px"></asp:TextBox></td>
                            <td style="text-align: left;">
                                <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                            <td style="width: 10px;">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblNettoC" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Periodo Dal:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDataDal_MG" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                    TabIndex="8" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNotifica" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="85px">Spese di Notifica:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNotifica_MG" runat="server" Font-Bold="True" Font-Size="8pt"
                                    MaxLength="10" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="80px"></asp:TextBox></td>
                            <td style="text-align: left">
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                            <td style="width: 10px">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblNumMandato" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                    TabIndex="-1" Width="60px">Periodo Al:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDataAL_MG" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                    TabIndex="8" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMAV" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="85px">Spese MAV:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtMAV_MG" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="80px"></asp:TextBox></td>
                            <td style="text-align: left">
                                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                            <td style="width: 10px">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblCassaC" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Emissione</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDataEmissione_MG" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                    TabIndex="8" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label29" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                    Width="85px">Debito INIZIALE:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtImportoINIZIALE_MG" runat="server" Font-Bold="True" 
                                    Font-Size="8pt" MaxLength="10"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="80px" 
                                    ToolTip="Debito Iniziale (senza bollo, spese e rimborso)"></asp:TextBox></td>
                            <td style="text-align: left">
                                <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label>
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td>
                                <asp:Label ID="lblDataMandato" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                    TabIndex="-1" Width="60px">Scadenza</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDataScadenza_MG" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                    TabIndex="8" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                    Width="90px">Debito CORRENTE:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtImporto_MG" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="80px" 
                                    ToolTip="Debito Corrente (senza bollo, spese e rimborso)"></asp:TextBox></td>
                            <td style="text-align: left">
                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                            <td style="width: 10px">
                            </td>
                            <td>
                                <asp:Label ID="lblDataPagamentoMG" runat="server" Font-Bold="False" 
                                    Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                    Width="60px" ToolTip="Data Pagamento">Pagamento</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDataPagamento_MG" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                    TabIndex="8" ToolTip="Data Pagamento (gg/mm/aaaa)" Width="70px"></asp:TextBox></td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid">
                            <tr>
                            <td colspan = "2">
                    <asp:Label ID="label_lettera2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="184px">Dettaglio M.AV. Gestore:</asp:Label></td>
                            <td colspan = "4">
                                <asp:ImageButton ID="btnAnnulloAler" runat="server" 
                                    ImageUrl="Immagini/btnAnnulla.png"  OnClientClick ="Conferma();" />
                                </td>
                            
                        </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="85px">Bollo:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtBollo_MA" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                        ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        TabIndex="-1" Width="80px"></asp:TextBox></td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">N./Tipo</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtTipo_MA" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        MaxLength="50" ReadOnly="True" TabIndex="5" Width="90px"></asp:TextBox></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="85px">Rimborso Spese:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtRimborso_MA" runat="server" Font-Bold="True" Font-Size="8pt"
                                        MaxLength="10" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        TabIndex="-1" Width="80px"></asp:TextBox></td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Periodo Dal:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtDataDal_MA" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                        TabIndex="8" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="85px">Spese di Notifica:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtNotifica_MA" runat="server" Font-Bold="True" Font-Size="8pt"
                                        MaxLength="10" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        TabIndex="-1" Width="80px"></asp:TextBox></td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                                <td style="width: 10px">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Periodo Al:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtDataAL_MA" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                        TabIndex="8" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="85px">Spese MAV:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtMAV_MA" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                        ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        TabIndex="-1" Width="80px"></asp:TextBox></td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                                <td style="width: 10px">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Emissione</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtDataEmissione_MA" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                        TabIndex="8" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                        Width="85px">Debito INIZIALE:</asp:Label></td>
                                <td>
                                <asp:TextBox ID="txtImportoINIZIALE_MA" runat="server" Font-Bold="True" 
                                        Font-Size="8pt" MaxLength="10"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="80px" 
                                        ToolTip="Debito Iniziale (senza bollo, spese e rimborso)"></asp:TextBox></td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label32" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Scadenza</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtDataScadenza_MA" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                        TabIndex="8" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                        Width="90px">Debito CORRENTE:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtImporto_MA" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                        ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                        TabIndex="-1" Width="80px" 
                                        ToolTip="Debito Corrente (senza bollo, spese e rimborso)"></asp:TextBox></td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€"></asp:Label></td>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <asp:Label ID="lblDataPagamentoMA" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                        Width="60px" ToolTip="Data Pagamento">Pagamento</asp:Label></td>
                                <td>
                                <asp:TextBox ID="txtDataPagamento_MA" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                    TabIndex="8" ToolTip="Data Pagamento (gg/mm/aaaa)" Width="70px"></asp:TextBox></td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblNote" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="70px"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNote_MG" runat="server" Height="40px" MaxLength="4000" ReadOnly="True"
                            Style="left: 8px; top: 432px" TabIndex="14" TextMode="MultiLine" 
                            Width="300px"></asp:TextBox></td>
                    <td width="15px">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="65px"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNote_MA" runat="server" Height="40px" MaxLength="4000" ReadOnly="True"
                            Style="left: 8px; top: 432px" TabIndex="14" TextMode="MultiLine" 
                            Width="300px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
</table>

