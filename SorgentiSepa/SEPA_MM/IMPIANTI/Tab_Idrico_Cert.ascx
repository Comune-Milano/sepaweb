<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Idrico_Cert.ascx.vb" Inherits="Tab_Idrico_Certificazioni" %>
<table style="width: 765px; height: 310px;">
    <tr>
        <td style="width: 130px">
            &nbsp;</td>
        <td style="width: 65px">
        </td>
        <td>
        </td>
        <td style="width: 130px">
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
        <td style="width: 130px">
            <asp:Label ID="LblPraticaISPESL" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                TabIndex="-1" Width="95px">Pratica ISPESL/ASL</asp:Label></td>
        <td style="width: 65px">
            <asp:DropDownList ID="cmbISPESL" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="12" ToolTip="Pratica ISPESL/ASL" Width="56px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList></td>
        <td>
            &nbsp;&nbsp;
        </td>
        <td style="width: 130px">
            <asp:Label ID="LblData" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" Width="120px">Data ISPESL/ASL</asp:Label></td>
        <td>
            <asp:TextBox ID="txtDataISPESL" runat="server" Font-Names="Arial" Font-Size="9pt"
                Style="left: 504px; top: 152px" TabIndex="13" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataISPESL"
                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>&nbsp;</td>
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
    </tr>
    <tr>
        <td style="width: 130px">
            <asp:Label ID="LblLibretto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" TabIndex="-1"
                Width="95px">Libretto Matricola Serbatoi</asp:Label></td>
        <td style="width: 65px">
            <asp:DropDownList ID="cmbLibretto" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="14" ToolTip="Libretto della Centrale Termica" Width="56px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList></td>
        <td>
        </td>
        <td style="width: 130px">
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
        <td style="width: 130px">
            <asp:Label ID="LblConformita" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                TabIndex="-1" Width="95px">Certif. Conformità</asp:Label></td>
        <td style="width: 65px">
            <asp:DropDownList ID="cmbConformita" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="15" ToolTip="Certificati di Conformità legge 46/90 " Width="56px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList></td>
        <td>
        </td>
        <td style="width: 130px">
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
        <td style="width: 130px">
        </td>
        <td style="width: 65px">
        </td>
        <td>
        </td>
        <td style="width: 130px">
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
        <td style="width: 130px">
        </td>
        <td style="width: 65px">
        </td>
        <td>
        </td>
        <td style="width: 130px">
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
        <td style="width: 130px">
        </td>
        <td style="width: 65px">
        </td>
        <td>
        </td>
        <td style="width: 130px">
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
        <td style="width: 130px;">
        </td>
        <td style="width: 65px;">
        </td>
        <td>
        </td>
        <td style="width: 130px;">
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
        <td style="width: 130px">
            <br />
            <br />
            <br />
            <br />
        </td>
        <td style="width: 65px">
        </td>
        <td>
        </td>
        <td style="width: 130px">
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
