<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Sollevamento_Generale.ascx.vb" Inherits="IMPIANTI_Tab_Sollevamento_Generale" %>
<table>
    <tr>
        <td rowspan="">
<table style="width: 765px">
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="DATI TECNICI" Width="136px"></asp:Label></td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="lblModello" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 472px; top: 192px"
                Width="80px">Marca</asp:Label></td>
        <td>
            <asp:TextBox ID="txtMarcaModello" runat="server" Font-Names="Arial" Font-Size="9pt"
                MaxLength="100" Style="left: 80px; top: 88px;" TabIndex="8"
                Width="330px"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;
        </td>
        <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 472px; top: 192px" Width="95px">Modello</asp:Label></td>
        <td>
                        <asp:DropDownList ID="cmbModello" runat="server" BackColor="White"
                            Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid; border-bottom: black 1px solid; top: 192px"
                            TabIndex="9" Width="225px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1">MONOSPACE</asp:ListItem>
                            <asp:ListItem Value="2">TRADIZIONALE</asp:ListItem>
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNumImpianto" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 472px; top: 192px"
                Width="80px">Num. Impianto *</asp:Label></td>
        <td>
            <asp:TextBox ID="txtNumImpianto" runat="server" Font-Names="Arial"
                Font-Size="9pt" MaxLength="30" TabIndex="10" Width="330px"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;
        </td>
        <td>
            <asp:Label ID="lblUbicazione" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px"
                TabIndex="-1" Width="95px">Locale Macchine</asp:Label></td>
        <td>
            <asp:DropDownList ID="cmbUbicazione" runat="server" BackColor="White"
                            Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid; border-bottom: black 1px solid; top: 192px"
                            TabIndex="11" Width="225px" ToolTip="Ubicazione Locale Macchine">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>ASSENTE</asp:ListItem>
                <asp:ListItem>ALTO</asp:ListItem>
                <asp:ListItem>BASSO</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMatricola" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 472px; top: 192px"
                Width="80px">Num. Matricola *</asp:Label></td>
        <td>
            <asp:TextBox ID="txtNumMatricola" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="30" TabIndex="12" Width="330px"></asp:TextBox></td>
                    <td></td>
        <td>
            <asp:Label ID="LblAzionamento" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px"
                TabIndex="-1" Width="95px">Tipo Azionamento *</asp:Label></td>
        <td>
            <asp:DropDownList ID="cmbTipoAzionamento" runat="server" BackColor="White"
                            Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid; border-bottom: black 1px solid; top: 192px"
                            TabIndex="13" Width="225px">
            <asp:ListItem Value="0">NON DISPONIBILE</asp:ListItem>
            <asp:ListItem Value="1">ELETTRICO A FUNE</asp:ListItem>
            <asp:ListItem Value="2">IDRAULICO</asp:ListItem>
        </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNumLotto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 472px; top: 192px" Width="80px">Num. Lotto</asp:Label></td>
        <td>
            <asp:TextBox ID="txtNumLotto" runat="server" Font-Names="Arial" Font-Size="9pt"
                MaxLength="30" TabIndex="14" Width="330px"></asp:TextBox></td>
                    <td></td>
        <td>
            <asp:Label ID="lblManovra" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="95px">Tipo Manovra</asp:Label></td>
        <td>
            <asp:DropDownList ID="cmbTipoManovra" runat="server" BackColor="White"
                            Font-Names="arial" Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid; border-bottom: black 1px solid; top: 192px"
                            TabIndex="15" Width="225px">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">NORMALE</asp:ListItem>
                <asp:ListItem Value="2">A PRENOTAZIONE</asp:ListItem>
            <asp:ListItem Value="3">DUPLEX</asp:ListItem>
            <asp:ListItem Value="4">TRIPLEX</asp:ListItem>
        </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
                        <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="80px">Ditta di Gestione</asp:Label></td>
        <td>
                        <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="300" Style="left: 80px; top: 88px; height: 30px" TabIndex="16" TextMode="MultiLine"
                            Width="330px"></asp:TextBox></td>
                    <td></td>
        <td>
                        <asp:Label ID="lblNumTelefono" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="95px">Num. Telefonico</asp:Label></td>
        <td>
                        <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="50" TabIndex="17" ToolTip="Numero telefonico di Riferimento" Width="225px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblTeleallarme" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px"
                TabIndex="-1" Width="80px">Teleallarme</asp:Label></td>
        <td>
            <asp:DropDownList ID="cmbTeleallarme" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid; top: 184px"
                            TabIndex="18" Width="50px" AutoPostBack="True">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="S">SI</asp:ListItem>
                <asp:ListItem Value="N">NO</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:Label ID="lblNumTelAllarme" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px"
                TabIndex="-1" Width="80px">Num. Telefonico</asp:Label><asp:TextBox ID="txtNumTelAllarme"
                    runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="50" TabIndex="19"
                    ToolTip="Numero telefonico di Riferimento" Width="190px"></asp:TextBox></td>
                    <td></td>
        <td>
            <asp:Label ID="lblNumFermate" runat="server" Font-Bold="False" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" TabIndex="-1"
                Width="95px">Num. Fermate *</asp:Label></td>
        <td>
            <asp:TextBox ID="txtNumFermate" runat="server" Font-Names="arial" Font-Size="9pt"
                MaxLength="2" Style="z-index: 102; left: 688px; top: 192px" TabIndex="20" Width="50px" AutoPostBack="True"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNumFermate"
                Display="Dynamic" ErrorMessage="max 2 Numeri" Font-Names="arial" Font-Size="8pt"
                TabIndex="303" ValidationExpression="\d+" Width="50px"></asp:RegularExpressionValidator>
            &nbsp;
            <asp:Label ID="lblNumero" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" TabIndex="-1" Width="100px">Tipologia Canone</asp:Label><asp:TextBox ID="txtNumero" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="2"
                Style="z-index: 102; left: 688px; top: 192px" TabIndex="-1" Width="50px" Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblVelocita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 472px; top: 192px" Width="80px">Velocita (m/s) *</asp:Label></td>
        <td>
            <asp:TextBox ID="txtVelocita" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="5"
                Style="z-index: 102; left: 592px; top: 192px" TabIndex="21" Width="90px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtVelocita"
                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                Font-Size="8pt" Style="left: 464px; top: 144px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                Width="80px">Valore Numerico</asp:RegularExpressionValidator>
            &nbsp; &nbsp;
            <asp:Label ID="lblPortata" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 528px; top: 192px" TabIndex="-1"
                Width="80px">Portata (kg) *</asp:Label>
            <asp:TextBox ID="txtPortata" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="6"
                Style="z-index: 102; left: 688px; top: 192px" TabIndex="22" Width="90px"></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPortata"
                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                    Font-Size="8pt" Style="left: 168px; top: 8px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                    Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    <td></td>
        <td>
            <asp:Label ID="lblCorsa" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" Width="95px">Corsa (m) *</asp:Label></td>
        <td>
            <asp:TextBox ID="txtCorsa" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="5"
                Style="z-index: 102; left: 688px; top: 192px" TabIndex="23" Width="50px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCorsa"
                Display="Dynamic" ErrorMessage="solo numeri" Font-Names="arial" Font-Size="8pt"
                TabIndex="303" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"></asp:RegularExpressionValidator>
            &nbsp;
            <asp:Label ID="lblPersone" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" TabIndex="-1" Width="100px">Num. Max Persone *</asp:Label><asp:TextBox ID="txtNumPersone" runat="server" Font-Names="arial" Font-Size="9pt"
                MaxLength="2" Style="z-index: 102; left: 688px; top: 192px" TabIndex="24" Width="50px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumPersone"
                Display="Dynamic" ErrorMessage="max 2 Numeri" Font-Names="arial" Font-Size="8pt"
                TabIndex="303" ValidationExpression="\d+" Width="72px"></asp:RegularExpressionValidator></td>
    </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVisite" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" Width="95px">N° visite mensili *</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNumVisite" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="5"
                            Style="z-index: 102; left: 688px; top: 192px" TabIndex="24" Width="50px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNumVisite"
                            Display="Dynamic" ErrorMessage="solo numeri" Font-Names="arial" Font-Size="8pt"
                            TabIndex="303" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"></asp:RegularExpressionValidator>
                    </td>
                </tr>
</table>
<table>
    <tr>
        <td>
            <br />
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="NOTE" Width="80px"></asp:Label><br />
        </td>
        <td>
            <asp:TextBox ID="txtNote" runat="server" Height="75px" MaxLength="4000" Style="left: 8px; top: 432px;" TabIndex="25" TextMode="MultiLine" Width="680px"></asp:TextBox></td>
    </tr>
</table>
        </td>
    </tr>
</table>
