<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_Cert.ascx.vb" Inherits="Tab_Termico_Certificazioni" %>
<table style="width: 765px">
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
                top: 184px" TabIndex="55" ToolTip="Pratica ISPESL/ASL" Width="56px">
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
                Style="left: 504px; top: 152px" TabIndex="56" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
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
                Width="95px">Libretto CT</asp:Label></td>
        <td style="width: 65px">
            <asp:DropDownList ID="cmbLibretto" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="57" ToolTip="Libretto della Centrale Termica" Width="56px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList></td>
        <td>
        </td>
        <td style="width: 130px">
            <asp:Label ID="lblDataCT" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" Width="120px">Data Dismissione C.T.</asp:Label></td>
        <td>
            <asp:TextBox ID="txtDataCT" runat="server" Font-Names="Arial" Font-Size="9pt" Style="left: 504px;
                top: 152px" TabIndex="58" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataCT"
                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
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
            <asp:Label ID="LblContabilizzatore" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                TabIndex="-1" Width="120px">Contabilizzatore Energia</asp:Label></td>
        <td style="width: 65px">
            <asp:DropDownList ID="cmbContEnergia" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="59" ToolTip="Contabilizzatore Energia in CT" Width="56px">
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
            <asp:Label ID="LblTrattamentoAcqua" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="120px">Trattamento Acqua</asp:Label></td>
        <td style="width: 65px">
            <asp:DropDownList ID="cmbTrattamentoAcqua" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="60" ToolTip="Trattamento Acqua" Width="56px">
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
            <asp:Label ID="lblLicenzaUTF" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                Width="95px">Licenza UTF</asp:Label></td>
        <td style="width: 65px">
            <asp:DropDownList ID="cmbLicenzaUTF" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="61" ToolTip="Licenza UTF" Width="56px">
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
                top: 184px" TabIndex="62" ToolTip="Certificati di Conformità legge 46/90 " Width="56px">
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
        <td style="width: 130px; height: 24px">
            <asp:Label ID="lblDecreto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="95px">Decreto Prefettizio</asp:Label></td>
        <td style="width: 65px; height: 24px">
            <asp:DropDownList ID="cmbDecreto" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="63" ToolTip="Decreto Prefettizio Serbatoi" Width="56px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList></td>
        <td style="height: 24px">
        </td>
        <td style="width: 130px; height: 24px">
        </td>
        <td style="height: 24px">
        </td>
        <td style="height: 24px">
        </td>
        <td style="height: 24px">
        </td>
        <td style="height: 24px">
        </td>
        <td style="height: 24px">
        </td>
        <td style="height: 24px">
        </td>
        <td style="height: 24px">
        </td>
    </tr>
    <tr>
        <td style="width: 130px; height: 24px">
            <asp:Label ID="lblCPI" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px" TabIndex="-1" Width="95px">Pratica VVF</asp:Label></td>
        <td style="width: 65px; height: 24px">
            <asp:DropDownList ID="cmbCPI" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                top: 184px" TabIndex="64" ToolTip="Pratica VVF (NOP o CPI)" Width="56px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="NOP">NOP</asp:ListItem>
                <asp:ListItem Value="CPI">CPI</asp:ListItem>
            </asp:DropDownList></td>
        <td style="height: 24px">
        </td>
        <td style="width: 130px; height: 24px">
            <asp:Label ID="lblDataRilascio" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                TabIndex="-1" Width="95px">Data Rilascio</asp:Label></td>
        <td style="height: 24px">
            <asp:TextBox ID="txtDataRilascio" runat="server" Font-Names="Arial" Font-Size="9pt"
                Style="left: 504px; top: 152px" TabIndex="65" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataRilascio"
                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
        <td style="height: 24px">
        </td>
        <td style="height: 24px">
            <asp:Label ID="lblDataScadenza" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 184px"
                TabIndex="-1" Width="95px">Data Scadenza</asp:Label></td>
        <td style="height: 24px">
            <asp:TextBox ID="txtDataScadenza" runat="server" Font-Names="Arial" Font-Size="9pt"
                Style="left: 504px; top: 152px" TabIndex="66" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataScadenza"
                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
        <td style="height: 24px">
            &nbsp; &nbsp;
        </td>
        <td style="height: 24px">
            &nbsp; &nbsp;
        </td>
        <td style="height: 24px">
            &nbsp; &nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 130px">
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
