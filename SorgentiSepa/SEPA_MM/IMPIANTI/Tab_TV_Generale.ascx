<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_TV_Generale.ascx.vb" Inherits="Tab_TV_Generale" %>
<table>
    <tr>
        <td>
            <table style="width: 765px;">
                <tr>
                    <td>
                        <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="100px">Ditta di Gestione</asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Height="70px" MaxLength="300" Style="left: 80px; top: 88px" TabIndex="5" TextMode="MultiLine"
                            Width="390px"></asp:TextBox></td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblNumTelG" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtNumTelG" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="50"
                            TabIndex="6" ToolTip="Numero telefonico di Riferimento" Width="180px"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNumPrese" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Numero Prese</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNumPrese" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                            Style="z-index: 102; left: 688px; top: 192px; text-align: right" TabIndex="7"
                            Width="80px"></asp:TextBox>
                        &nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumPrese"
                            Display="Dynamic" ErrorMessage="Valore Numerico Intero" Font-Names="arial" Font-Size="8pt"
                            TabIndex="303" ValidationExpression="\d+" Width="200px"></asp:RegularExpressionValidator></td>
                    <td></td>
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
                        &nbsp;&nbsp;
                        </td>
                    <td>
                        </td>
                    <td>
                        </td>
                </tr>
            </table>
            <table style="width: 765px;">
                <tr>
                    <td style="width: 93px">
                        <asp:Label ID="lblNote" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="100px"></asp:Label><br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" Height="120px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="8" TextMode="MultiLine" Width="650px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 93px">
                        <br />
                        <br />
                        <br />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>