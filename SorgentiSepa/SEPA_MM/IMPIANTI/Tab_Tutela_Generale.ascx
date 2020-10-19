<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Tutela_Generale.ascx.vb" Inherits="Tab_Tutela_Generale" %>
<table style="width: 765px; height: 310px">
    <tr>
        <td style="height: 309px">
            <table style="width: 765px; height: 1px;">
                <tr>
                    <td style="height: 34px">
                        <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="90px">Ditta di Gestione</asp:Label></td>
                    <td style="height: 34px">
                        <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="300" Style="left: 80px; top: 88px;" TabIndex="4" TextMode="MultiLine"
                            Width="360px" Height="50px"></asp:TextBox></td>
                    <td style="width: 11px; height: 34px;">
                    </td>
                    <td style="height: 34px">
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
                    <td style="height: 34px">
                        <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="50" TabIndex="5" ToolTip="Numero telefonico di Riferimento" Width="200px"></asp:TextBox></td>
                </tr>
            </table>
            <table style="width: 765px">
                <tr>
                    <td>
                        <asp:Label ID="lblRecinsione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="90px">Recinzione</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbRecinzione" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="6" ToolTip="Recinzione" Width="60px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblTipoRecinzione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="130px">Tipo Recinzione</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbTipoRecinzione" runat="server" AutoPostBack="True" BackColor="White"
                            Font-Names="arial" Font-Size="8pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 552px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 192px" TabIndex="7" Width="360px">
                        </asp:DropDownList></td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="80px"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCarrai" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="90px" Visible="False">Passi Carrai</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbCarrai" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="-1" ToolTip="Passi Carrai" Width="60px" AutoPostBack="True" Visible="False">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblNumCarrai" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="130px" Visible="False">Quantità Passi Carrai</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNumCarrai" runat="server" Font-Names="arial" Font-Size="9pt"
                            MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                            TabIndex="-1" Width="80px" Visible="False"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                runat="server" ControlToValidate="txtNumCarrai" Display="Dynamic" ErrorMessage="Valore Numerico Intero"
                                Font-Names="arial" Font-Size="8pt" TabIndex="303" ValidationExpression="\d+"
                                Width="184px" Visible="False"></asp:RegularExpressionValidator></td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVideoSorveglianza" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="90px">Video Sorveglianza</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbVideoSorveglianza" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="8" ToolTip="Video Sorveglianza" Width="60px" AutoPostBack="True">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblTrattamento" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="130px">Responsabile Trattamento</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtResponsabile" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Height="40px" MaxLength="200" Style="left: 80px; top: 88px" TabIndex="9" TextMode="MultiLine"
                            Width="360px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
            </table>
            <table style="width: 496px">
                <tr>
                    <td>
                        <asp:Label ID="lblTitoloBox" runat="server" Font-Bold="False" Font-Italic="True"
                            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px;
                            top: 184px" TabIndex="-1" Width="248px">Cancelli Box dotati di:</asp:Label></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table style="width: 765px">
                <tr>
                    <td style="height: 25px">
                        <asp:Label ID="lblCarrabile" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="90px">Cancello Carrabile</asp:Label></td>
                    <td style="height: 25px">
                        <asp:DropDownList ID="cmbCarrabile" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="10" ToolTip="Cancello Carrabile" Width="60px" AutoPostBack="True">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="height: 25px">
                        &nbsp;&nbsp;
                    </td>
                    <td style="height: 25px">
                        <asp:Label ID="lblNumCarrabili" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="130px">Quantità Cancelli Carrabili</asp:Label></td>
                    <td style="height: 25px">
                        <asp:TextBox ID="txtNumCarrabile" runat="server" Font-Names="arial" Font-Size="9pt"
                            MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                            TabIndex="11" Width="80px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server" ControlToValidate="txtNumCarrabile" Display="Dynamic" ErrorMessage="Valore Numerico Intero"
                                Font-Names="arial" Font-Size="8pt" TabIndex="303" ValidationExpression="\d+"
                                Width="80px"></asp:RegularExpressionValidator></td>
                    <td style="height: 25px">
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="350px"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAutomatizzato" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="90px">Cancello Automatizzato</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbAutomatizzato" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="12" ToolTip="Cancello Automatizzato" Width="60px" AutoPostBack="True">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblNumAutomatizzato" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="130px">Quantità Cancelli Automatizzati</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNumAutomatizzato" runat="server" Font-Names="arial" Font-Size="9pt"
                            MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                            TabIndex="13" Width="80px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                runat="server" ControlToValidate="txtNumAutomatizzato" Display="Dynamic" ErrorMessage="Valore Numerico Intero"
                                Font-Names="arial" Font-Size="8pt" TabIndex="303" ValidationExpression="\d+"
                                Width="80px"></asp:RegularExpressionValidator></td>
                    <td>
                    </td>
                </tr>
            </table>
            <table style="width: 765px; height: 1px;">
                <tr>
                    <td style="width: 93px">
                        <asp:Label ID="lblNote" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="90px"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" Height="80px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="14" TextMode="MultiLine" Width="660px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
</table>