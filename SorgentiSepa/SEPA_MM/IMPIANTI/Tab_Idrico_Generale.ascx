<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Idrico_Generale.ascx.vb" Inherits="Tab_Idrico_Generale" %>
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
                overflow: auto; border-left: navy 1px solid; width: 610px; border-bottom: navy 1px solid;
                height: 180px">
                <asp:CheckBoxList ID="CheckBoxEdifici" runat="server" AutoPostBack="True" BorderColor="Black"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="184px" TabIndex="7"
                    Width="552px">
                </asp:CheckBoxList></div>
        </td>
        <td>
            <table style="width: 100px">
                <tr>
                    <td style="width: 4px">
                        <asp:Label ID="lblTotaleUI" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="60px">Totale U.I.</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 4px">
                        <asp:TextBox ID="txtTotUI" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="15" Style="left: 144px; top: 192px; text-align: right" TabIndex="-1"
                            Width="70px"></asp:TextBox></td>
                </tr>
            </table>
            <br />
            <br />
            <br />
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
                MaxLength="300" Style="left: 80px; top: 88px;" TabIndex="8" TextMode="MultiLine"
                Width="310px" Height="40px"></asp:TextBox></td>
        <td>
            &nbsp;&nbsp;
        </td>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
        <td>
            <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                MaxLength="50" TabIndex="9" ToolTip="Numero telefonico di Riferimento" Width="160px"></asp:TextBox>&nbsp;</td>
        <td>
            &nbsp;&nbsp;
        </td>
        <td>
            <asp:Label ID="lblCompressore" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="60px">Compressore</asp:Label></td>
        <td>
            <asp:DropDownList ID="cmbCompressore" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="10" Width="56px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="NOTE" Width="60px"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtNote" runat="server" Height="50px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="11" TextMode="MultiLine" Width="700px"></asp:TextBox></td>
    </tr>
</table>
        </td>
    </tr>
</table>
