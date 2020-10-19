<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Citofonico_Generale.ascx.vb" Inherits="Tab_Citofonico_Generale" %>
<table style="width: 0px; height: 0px">
    <tr>
        <td>
            <table style="width: 765px; height: 1px;">
                <tr>
                    <td>
                        <asp:Label ID="lblDittaInstalla" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="100px">Ditta Installatrice</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDittaInstalla" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Height="60px" MaxLength="300" Style="left: 80px; top: 88px" TabIndex="5" TextMode="MultiLine"
                            Width="390px"></asp:TextBox></td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblNumTelI" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNumTelI" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="50" TabIndex="6" ToolTip="Numero telefonico di Riferimento" Width="180px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="100px">Ditta di Gestione</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Height="60px" MaxLength="300" Style="left: 80px; top: 88px" TabIndex="7" TextMode="MultiLine"
                            Width="390px"></asp:TextBox></td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblNumTelG" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNumTelG" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="50"
                            TabIndex="8" ToolTip="Numero telefonico di Riferimento" Width="180px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        </td>
                    <td>
                    </td>
                    <td>
                        &nbsp;&nbsp;
                        </td>
                    <td>
                        </td>
                    <td>
                        </td>
                </tr>
            </table>
            <table style="width: 765px; height: 1px;">
                <tr>
                    <td style="width: 93px">
                        <asp:Label ID="lblNote" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="100px"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" Height="100px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="9" TextMode="MultiLine" Width="650px"></asp:TextBox></td>
                </tr>
            </table>
            <br />
        </td>
    </tr>
</table>