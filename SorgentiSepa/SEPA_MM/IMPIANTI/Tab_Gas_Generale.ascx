<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Gas_Generale.ascx.vb" Inherits="Tab_Gas_Generale" %>
<table style="width: 765px; height: 310px">
    <tr>
        <td>
            <table style="width: 765px; height: 1px;">
                <tr>
                    <td>
                        <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="100px">Ditta di Gestione</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Height="50px" MaxLength="300" Style="left: 80px; top: 88px" TabIndex="9" TextMode="MultiLine"
                            Width="370px"></asp:TextBox></td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblNumTel" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="50" TabIndex="10" ToolTip="Numero telefonico di Riferimento" Width="180px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoTubazione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="100px">Tipo Tubazione</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbTubazione" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="11" ToolTip="Tipo Tubazione" Width="300px">
                        </asp:DropDownList></td>
                    <td></td>
                    <td>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoServizio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Tipo Servizio</asp:Label></td>
                    <td>
                        <div style="border-right: navy 1px solid; border-top: navy 1px solid; overflow: auto;
                            border-left: navy 1px solid; width: 300px; border-bottom: navy 1px solid; height: 100px">
                            <asp:CheckBoxList ID="chkTipoServizio" runat="server" BorderColor="Black" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Height="96px" TabIndex="12" Width="272px">
                            </asp:CheckBoxList>
                        </div>
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
                        <asp:TextBox ID="txtNote" runat="server" Height="100px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="13" TextMode="MultiLine" Width="650px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
</table>