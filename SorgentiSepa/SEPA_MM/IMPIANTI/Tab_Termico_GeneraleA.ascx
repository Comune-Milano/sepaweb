<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_GeneraleA.ascx.vb" Inherits="TabGeneraleA" %>

<div style="visibility: visible; overflow: auto; border-top-width: thin; border-left-width: thin; border-left-color: yellow; border-bottom-width: thin; border-bottom-color: yellow; border-top-color: yellow; border-right-width: thin; border-right-color: yellow;">
    <table>
        <tr>
            <td>
                <asp:Label ID="LblDitta" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">Ditta Installatrice</asp:Label></td>
            <td>
                <asp:TextBox ID="txtDitta" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="100"
                    Style="left: 80px; top: 88px" TabIndex="6" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
            <td>
                &nbsp;&nbsp;
            </td>
            <td>
                <asp:Label ID="LblAnnoRealizzazione" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="60px">Data installazione</asp:Label></td>
            <td style="width: 190px">
                <asp:TextBox ID="txtAnnoRealizzazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Style="left: 504px; top: 152px" TabIndex="7" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtAnnoRealizzazione"
                        Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                        Font-Names="arial" Font-Size="8pt" Style="text-align: right" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="150px"></asp:RegularExpressionValidator></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">Ditta di Gestione</asp:Label></td>
            <td>
                <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="300"
                    Style="left: 80px; top: 88px; height: 30px" TabIndex="8" TextMode="MultiLine"
                    Width="400px"></asp:TextBox></td>
            <td style="width: 11px">
                &nbsp;&nbsp;
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
            <td>
                <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="50" TabIndex="9" Width="190px" ToolTip="Numero telefonico di Riferimento"></asp:TextBox></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="LblApparecchio" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px;
                    top: 144px" Width="80px" TabIndex="-1">Tipo Apparecchio</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbApparecchio" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid;
                    border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid;
                    border-bottom: black 1px solid; top: 192px" TabIndex="10" Width="210px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="1">B (Camera Aperta)</asp:ListItem>
                    <asp:ListItem Value="2">C (Camera Stagna)</asp:ListItem>
                </asp:DropDownList></td>
            <td>
            </td>
            <td>
                <asp:Label ID="LblConsumo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" Width="100px" TabIndex="-1">Consumo Medio</asp:Label></td>
            <td>
                <asp:TextBox ID="txtConsumo" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                    Style="z-index: 102; left: 688px; top: 192px; text-align: right;" TabIndex="11" Width="50px"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtConsumo"
                        Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                        Font-Size="8pt" Style="left: 168px; top: 8px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" TabIndex="-1">Valore Numerico</asp:RegularExpressionValidator></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblCappa" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" TabIndex="-1"
                    Width="100px">Cappa d'Aspirazione Piano Cottura</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbCappa" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid;
                    border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid;
                    border-bottom: black 1px solid; top: 192px" TabIndex="12" Width="100px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="1">ELETTRICO</asp:ListItem>
                    <asp:ListItem Value="2">NATURALE</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblUbicazione" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 472px;
                    top: 192px" Width="80px">Tipo Ubicazione</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbTipoUbicazione" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid;
                    border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid;
                    border-bottom: black 1px solid; top: 192px" TabIndex="13" Width="210px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="1">INTERNO</asp:ListItem>
                    <asp:ListItem Value="2">ESTERNO</asp:ListItem>
                </asp:DropDownList></td>
            <td>
                &nbsp;&nbsp;
            </td>
            <td id="TAB3">
                <asp:Label ID="LblPotenza" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px;
                    top: 8px" Width="60px">Potenza</asp:Label></td>
            <td>
                <asp:TextBox ID="txtPotenza" runat="server"
                    Font-Names="arial" Font-Size="9pt" MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right;" TabIndex="14" Width="50px"></asp:TextBox>
                <asp:Label ID="lblKW" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 528px; top: 192px" TabIndex="-1"
                    Width="30px">(Kw)</asp:Label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPotenza"
                    ErrorMessage="RegularExpressionValidator" Style="left: 168px;
                    top: 8px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="80px" Font-Names="Arial" Font-Size="8pt" Display="Dynamic" TabIndex="-1">Valore Numerico</asp:RegularExpressionValidator></td>
            <td>
                &nbsp;&nbsp;
            </td>
            <td>
                </td>
            <td>
                &nbsp; &nbsp;
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPosizionamento" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="80px">Posizionamento</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbPosizionamento" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid;
                    border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid;
                    border-bottom: black 1px solid; top: 192px" TabIndex="15" Width="210px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="1">BASAMENTO</asp:ListItem>
                    <asp:ListItem Value="2">MURALE</asp:ListItem>
            </asp:DropDownList></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblForoVentilazione" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="100px">Foro di Ventilazione</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbVentilazione" runat="server" BackColor="White" Font-Names="arial"
                    Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                    z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                    top: 184px" TabIndex="16" Width="50px" AutoPostBack="True">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="S">SI</asp:ListItem>
                    <asp:ListItem Value="N">NO</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblVentilazione" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="60px">Dimensione</asp:Label></td>
            <td>
                <asp:TextBox ID="txtVentilazione" runat="server" Font-Names="arial" Font-Size="9pt"
                    MaxLength="10" Style="z-index: 102; left: 592px; top: 192px; text-align: right"
                    TabIndex="17" Width="48px"></asp:TextBox>
                <asp:Label ID="lblVentilazioneCM2" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 528px; top: 192px"
                    TabIndex="-1" Width="30px">(cm2)</asp:Label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtVentilazione"
                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                    Font-Size="8pt" Style="left: 464px; top: 144px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                    Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td>
                            <asp:Label ID="lblScaricoFumi" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" TabIndex="-1"
                                Width="80px">Scarico Fumi</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbScaricoFumi" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid;
                    border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid;
                    border-bottom: black 1px solid; top: 192px" TabIndex="18" Width="210px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">IN CANNA FUMARIA SINGOLA</asp:ListItem>
                <asp:ListItem Value="2">IN CANNA FUMARIA COLLETTIVA</asp:ListItem>
                <asp:ListItem Value="3">IN FACCIATA</asp:ListItem>
            </asp:DropDownList></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblForoAreazione" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="100px">Foro d'areazione</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbAreazione" runat="server" BackColor="White" Font-Names="arial"
                    Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                    z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                    top: 184px" TabIndex="19" Width="50px" AutoPostBack="True">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="S">SI</asp:ListItem>
                    <asp:ListItem Value="N">NO</asp:ListItem>
                </asp:DropDownList></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblAreazione" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 528px; top: 192px"
                    TabIndex="-1" Width="60px">Dimensione</asp:Label></td>
            <td>
                <asp:TextBox ID="txtAreazione" runat="server" Font-Names="arial" Font-Size="9pt"
                    MaxLength="10" Style="z-index: 102; left: 592px; top: 192px; text-align: right"
                    TabIndex="20" Width="48px"></asp:TextBox>
                <asp:Label ID="lblAreazioneCM2" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 528px; top: 192px"
                    TabIndex="-1" Width="30px">(cm2)</asp:Label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAreazione"
                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                    Font-Size="8pt" Style="left: 464px; top: 144px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                    Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="height: 56px">
                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="NOTE" Width="60px" TabIndex="-1"></asp:Label></td>
            <td style="height: 56px">
                <asp:TextBox ID="txtNote" runat="server" MaxLength="4000" Style="left: 8px;
                    top: 432px;" TextMode="MultiLine" Width="685px" TabIndex="21" Height="80px"></asp:TextBox></td>
        </tr>
    </table>
</div>