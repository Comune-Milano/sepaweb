<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Comunicazioni.ascx.vb"
    Inherits="Contratti_Tab_Comunicazioni" %>
<div style="left: 8px; width: 856px; position: absolute; top: 168px; height: 520px">
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="RECAPITO PER LE COMUNICAZIONI" Width="190px"></asp:Label>
                <img id="INFO" alt="Questo recapito sarà utilizzato anche per l'invio delle bollette."
                    src="../../NuoveImm/INFO.png" /><br />
                <table style="border-top-width: 3px; border-left-width: 3px; border-left-color: lightgrey;
                    border-bottom-width: 3px; border-bottom-color: lightgrey; border-top-color: lightgrey;
                    border-right-width: 3px; border-right-color: lightgrey; width: 100%;">
                    <tr>
                        <td style="width: 100%">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Presso"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:TextBox ID="txtPresso" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="100"
                                Width="624px" TabIndex="75" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Indirizzo"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="cmbTipoViaCor" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="119px" TabIndex="76" Enabled="False">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtViaCor" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="50"
                                Width="289px" TabIndex="77" Enabled="False"></asp:TextBox>
                            ,
                            <asp:TextBox ID="txtCivicoCor" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="10" ToolTip="CIVICO" Width="82px" TabIndex="78" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%" valign="top">
                            <table style="width: 80%">
                                <tr>
                                    <td style="width: 77px">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Text="Cap"></asp:Label>
                                    </td>
                                    <td style="width: 87px">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Text="Luogo"></asp:Label>
                                    </td>
                                    <td style="width: 246px">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Text="Sigla"></asp:Label>
                                    </td>
                                    <td style="width: 246px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 77px">
                                        <asp:TextBox ID="txtCAPCor" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="5"
                                            Width="52px" TabIndex="79" Enabled="False"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="width: 87px">
                                        <asp:TextBox ID="txtLuogoCor" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="50"
                                            Width="223px" TabIndex="80" Enabled="False"></asp:TextBox><br />
                                    </td>
                                    <td style="width: 246px">
                                        <asp:TextBox ID="txtSiglaCor" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="2"
                                            ToolTip="Cap" Width="36px" TabIndex="81" Enabled="False"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="width: 246px">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" valign="top">
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="#8080FF" Text="ALTRE INFORMAZIONI" Width="190px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" valign="top">
                            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Commissariato di P.S."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" valign="top">
                            <asp:DropDownList ID="cmbCommissariato" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="822px" TabIndex="82" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
