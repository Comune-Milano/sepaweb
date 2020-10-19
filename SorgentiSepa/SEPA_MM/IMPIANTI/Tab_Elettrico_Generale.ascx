<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Elettrico_Generale.ascx.vb" Inherits="Tab_Elettrico_Generale" %>
<table>
    <tr>
        <td>
            <table style="width: 765px">
                <tr>
                    <td>
                        <asp:Label ID="LblAvanquadro" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                            TabIndex="-1" Width="90px">Avanquadro</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbAvanquadro" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="6" ToolTip="Avanquadro" Width="56px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height: 24px">
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="90px">Protezione Differenziale</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbDifferenziale" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="7" ToolTip="Protezione Differenziale" Width="120px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="NO">NO</asp:ListItem>
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem>0.5</asp:ListItem>
                            <asp:ListItem>0.3</asp:ListItem>
                            <asp:ListItem>0.03</asp:ListItem>
                            <asp:ListItem>PARZIALE</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="height: 24px">
                    </td>
                    <td style="height: 24px">
                    </td>
                    <td style="height: 24px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="90px">A norma</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbNorma" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="8" ToolTip="Norma" Width="56px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="90px">Ditta di Gestione</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="300" Style="left: 80px; top: 88px;" TabIndex="9" TextMode="MultiLine"
                            Width="370px" Height="50px"></asp:TextBox></td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="50" TabIndex="10" ToolTip="Numero telefonico di Riferimento" Width="180px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table style="width: 765px; height: 1px;">
                <tr>
                    <td style="height: 74px">
                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="90px"></asp:Label></td>
                    <td style="height: 74px">
                        <asp:TextBox ID="txtNote" runat="server" Height="130px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="11" TextMode="MultiLine" Width="650px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
