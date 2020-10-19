<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Antincendio_Generale.ascx.vb" Inherits="Tab_Antincendio_Generale" %>
<table>
    <tr>
        <td>
<table>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="left: 0px; top: -30px" TabIndex="-1" Text="Edifici Alimentati"
                Width="60px"></asp:Label></td>
        <td>
            <div style="border-right: navy 1px solid; border-top: navy 1px solid; visibility: visible;
                overflow: auto; border-left: navy 1px solid; width: 540px; border-bottom: navy 1px solid;
                height: 100px">
                <asp:CheckBoxList ID="CheckBoxEdifici" runat="server" AutoPostBack="True" BorderColor="Black"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="80px" TabIndex="7"
                    Width="520px">
                </asp:CheckBoxList></div>
        </td>
        <td>
            <table style="width: 100px">
                <tr>
                    <td style="width: 4px">
                        <asp:Label ID="lblTotaleUI" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="100px">Totale U.I.</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 4px">
                        <asp:TextBox ID="txtTotUI" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="15" Style="left: 144px; top: 192px; text-align: right" TabIndex="-1"
                            Width="80px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 4px">
                    </td>
                </tr>
            </table>
            <br />
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="60px">Ditta di Gestione</asp:Label></td>
        <td>
            <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt"
                MaxLength="300" Style="left: 80px; top: 88px; height: 30px" TabIndex="8" TextMode="MultiLine"
                Width="410px"></asp:TextBox></td>
        <td style="width: 11px">
            &nbsp;&nbsp;
        </td>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
        <td>
            <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                MaxLength="50" TabIndex="9" ToolTip="Numero telefonico di Riferimento" Width="180px"></asp:TextBox></td>
    </tr>
</table>
            <table style="width: 240px">
                <tr>
                    <td>
            <asp:Label ID="lblGruppo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Gruppo Elettrogeno</asp:Label></td>
                    <td>
            <asp:DropDownList ID="cmbGruppo" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="10" ToolTip="Norma" Width="60px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList></td>
                    <td>
                        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblBox" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" TabIndex="-1"
                            Width="60px">Presenza Box</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbBox" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 184px" TabIndex="11" ToolTip="Norma" Width="60px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblEstintori" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                            TabIndex="-1" Width="100px" Visible="False">Quantità Estintori</asp:Label>
                        &nbsp; &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantitaEstintori" runat="server" Font-Names="arial" Font-Size="9pt"
                            MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                            TabIndex="-1" Width="80px" Visible="False"></asp:TextBox>&nbsp;
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server" ControlToValidate="txtQuantitaEstintori" Display="Dynamic" ErrorMessage="Valore Numerico Intero"
                                Font-Names="arial" Font-Size="8pt" TabIndex="303" ValidationExpression="\d+"
                                Width="120px" Visible="False"></asp:RegularExpressionValidator>&nbsp;
                    </td>
                    <td>
            <asp:Label ID="lblTipo" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                TabIndex="-1" Width="60px" Visible="False">Tipo</asp:Label></td>
                    <td>
            <asp:DropDownList ID="cmbTipo" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="-1" ToolTip="Tipologia" Width="50px" Visible="False">
            </asp:DropDownList></td>
                </tr>
            </table>
<table>
    <tr>
        <td>
            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="60px"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtNote" runat="server" Height="60px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="12" TextMode="MultiLine" Width="690px"></asp:TextBox></td>
    </tr>
</table>
        </td>
    </tr>
</table>