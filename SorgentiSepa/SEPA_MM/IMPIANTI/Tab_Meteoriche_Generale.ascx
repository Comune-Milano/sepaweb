<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Meteoriche_Generale.ascx.vb" Inherits="Tab_Meteoriche_Generale" %>
<table>
    <tr>
        <td>
            <table style="width: 765px">
                <tr>
                    <td>
                        <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="80px">Ditta di Gestione</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Height="50px" MaxLength="300" Style="left: 80px; top: 88px" TabIndex="7" TextMode="MultiLine"
                            Width="408px"></asp:TextBox></td>
                    <td style="width: 11px">
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblTelefono" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="50" TabIndex="8" ToolTip="Numero telefonico di Riferimento" Width="180px"></asp:TextBox></td>
                </tr>
            </table>
            <table style="width: 765px">
                <tr>
                    <td>
                        <asp:Label ID="lblQuadro" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                            TabIndex="-1" Width="80px">Quadro Elettrico</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbQuadro" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="9" ToolTip="Libretto della Centrale Termica" Width="56px" AutoPostBack="True">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        &nbsp; &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblUbicazione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" TabIndex="-1"
                            Width="60px">Ubicazione Quadro</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtUbicazione" runat="server" Height="80px" MaxLength="2000" Style="left: 8px; top: 432px;" TabIndex="10" TextMode="MultiLine" Width="530px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVasca" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" TabIndex="-1"
                            Width="80px">Vasca Raccolta Acque</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbVasca" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="11" Width="56px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblDisoleatore" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                            TabIndex="-1" Width="60px">Disoleatore</asp:Label></td>
                    <td>
                        <table style="width: 530px">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="cmbDisoleatore" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="12" Width="60px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="S">SI</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblContinuita" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                                        TabIndex="-1" Width="60px">Impianto Continuità</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbContinuita" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="13" Width="60px" AutoPostBack="True">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="S">SI</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblDurata" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" TabIndex="-1"
                                        Width="60px">Durata (H)</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDurata" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                        Style="z-index: 102; left: 688px; top: 192px; text-align: right" TabIndex="14"
                                        Width="80px"></asp:TextBox></td>
                                <td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDurata"
                                        Display="Dynamic" ErrorMessage="Valore Numerico Intero" Font-Names="arial" Font-Size="8pt"
                                        TabIndex="303" ValidationExpression="\d+" Width="120px"></asp:RegularExpressionValidator></td>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 765px">
                <tr>
                    <td>
                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="80px"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" Height="70px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="15" TextMode="MultiLine" Width="670px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
