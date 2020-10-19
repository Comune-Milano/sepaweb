<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Canna_Generale.ascx.vb" Inherits="Tab_Canna_Generale" %>
<table style="width: 765px; height: 310px">
    <tr>
        <td style="height: 309px">
            <table>
                <tr>
                    <td style="width: 100px;">
                        <asp:Label ID="lblTipoInstallazione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="100px">Tipo Installazione</asp:Label></td>
                    <td style="width: 10px;">
                        </td>
                    <td>
                        <asp:DropDownList ID="cmbTipologia" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="4" ToolTip="Tipologia Impianto" Width="130px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="ESTERNA">ESTERNA</asp:ListItem>
                            <asp:ListItem Value="INTERNA">INTERNA</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 11px;">
                    </td>
                    <td>
                        <asp:Label ID="lblCertificato" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="100px">Certificato Conformità</asp:Label>
                        <asp:DropDownList ID="cmbCertificato" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="5" ToolTip="Certificato Conformità" Width="56px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td style="width: 100px;">
                        <asp:Label ID="lblServitu" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Tipo Servitù</asp:Label></td>
                    <td style="width: 10px;">
                    </td>
                    <td><asp:DropDownList ID="cmbServitu" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="6" ToolTip="Tipologia Impianto" Width="130px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="SCALDABAGNO">SCALDABAGNO</asp:ListItem>
                        <asp:ListItem Value="CALDAIE">CALDAIE</asp:ListItem>
                        <asp:ListItem>CAPPE</asp:ListItem>
                    </asp:DropDownList></td>
                    <td style="width: 11px;">
                    </td>
                    <td>
                        <asp:Label ID="lblNumUtenze" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Numero Utenze</asp:Label>
                        <asp:TextBox ID="txtNumUtenze" runat="server" Font-Names="arial" Font-Size="9pt"
                            MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                            TabIndex="7" Width="100px"></asp:TextBox></td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server" ControlToValidate="txtNumUtenze" Display="Dynamic" ErrorMessage="Valore Numerico Intero"
                                Font-Names="arial" Font-Size="8pt" TabIndex="303" ValidationExpression="\d+"
                                Width="120px"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td style="width: 100px;">
                        <asp:Label ID="lblPotenzaTOT" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="100px">Potenzialità Totale</asp:Label></td>
                    <td style="width: 10px;">
                    </td>
                    <td>
                        <asp:TextBox ID="txtPotenzaTOT" runat="server" Font-Names="arial" Font-Size="9pt"
                            MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                            TabIndex="8" Width="80px"></asp:TextBox>&nbsp;
                        <asp:Label ID="lblKW1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="30px">(Kw)</asp:Label>
                        </td>
                    <td style="width: 11px;">
                        </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPotenzaTOT"
                            Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                            Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    <td>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px;">
                        <asp:Label ID="lblPotenza" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="100px">Potenzialità Singola Utenza</asp:Label></td>
                    <td style="width: 10px;">
                    </td>
                    <td>
                        <asp:TextBox ID="txtPotenza" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                            Style="z-index: 102; left: 688px; top: 192px; text-align: right" TabIndex="9"
                            Width="80px"></asp:TextBox>&nbsp;
                        <asp:Label ID="lblKW2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="30px">(Kw)</asp:Label></td>
                    <td style="width: 11px;">
                        </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPotenza"
                            Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                            Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    <td>
                    </td>
                </tr>
            </table>
            <table style="width: 765px">
                <tr>
                    <td>
                        <asp:Label ID="lblElencoImpianti" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="100px">Elenco Centrali Termiche</asp:Label></td>
                    <td>
                        <div style="border-right: navy 1px solid; border-top: navy 1px solid; visibility: visible;
                            overflow: auto; border-left: navy 1px solid; width: 650px; border-bottom: navy 1px solid;
                            height: 90px">
                            <asp:CheckBoxList ID="CheckBoxImpiantiTE" runat="server" BorderColor="Black" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Height="70px" TabIndex="10" Width="592px">
                            </asp:CheckBoxList></div>
                    </td>
                </tr>
            </table>
            <table style="width: 765px; height: 1px;">
                <tr>
                    <td style="width: 93px">
                        <asp:Label ID="lblNote" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="100px"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" Height="85px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="11" TextMode="MultiLine" Width="650px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
</table>