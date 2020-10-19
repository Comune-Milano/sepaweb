<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Sollevamento_Cert.ascx.vb" Inherits="Tab_Sollevamento_Certificazioni" %>
<table style="width: 765px; height: 310px;">
    <tr>
        <td style="width: 140px">
            &nbsp;</td>
        <td style="width: 100px">
        </td>
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
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="width: 140px">
            <asp:Label ID="LblSchema" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="140px">Schema Impianto</asp:Label></td>
        <td style="width: 100px">
            <asp:DropDownList ID="cmbSchema" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="20" ToolTip="Schema Impianto" Width="56px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList></td>
        <td>
            &nbsp;&nbsp;
        </td>
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
        <td>
        </td>
    </tr>
    <tr>
        <td style="width: 140px">
            <asp:Label ID="LblConformita46_90" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                Width="140px">Certificazione DM 37/2008</asp:Label></td>
        <td style="width: 100px">
            <asp:DropDownList ID="cmbConformita37_08" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="21" ToolTip="Certificazione impianti DM 37/2008" Width="56px">
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
        <td>
        </td>
        <td>
            <asp:Label ID="LblConformita459_96" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                TabIndex="-1" Visible="False" Width="130px">Certif. Conformità 459/96</asp:Label></td>
        <td>
            <asp:DropDownList ID="cmbConformita459_96" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="-1" ToolTip="Dichiarazione di conformita legge n.459  D.P.R 1996 (per montacarichi o piattaforme)"
                Visible="False" Width="56px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList></td>
        <td>
            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="130px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 140px">
            <asp:Label ID="lblLibretto" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                TabIndex="-1" Width="140px">Libretto</asp:Label></td>
        <td style="width: 100px">
            <asp:DropDownList ID="cmbLibretto" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="22" Width="56px">
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
        <td style="width: 140px">
            <asp:Label ID="LblConformitaCE" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                TabIndex="-1" Width="140px">Certif. Conformità CE</asp:Label></td>
        <td style="width: 100px">
            <asp:DropDownList ID="cmbConformitaCE" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="23" ToolTip="Dichiarazione di conformita CE" Width="56px">
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
        <td style="width: 140px">
            <asp:Label ID="lblMatricola" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                TabIndex="-1" Width="130px">Num. Matricola</asp:Label></td>
        <td style="width: 100px">
            <asp:DropDownList ID="cmbNum_Matricola" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="24" ToolTip="Dichiarazione di conformita legge n.162  D.P.R 1999 (per ascensore)" Width="56px">
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
        <td style="width: 140px">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </td>
        <td style="width: 100px">
        </td>
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
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
