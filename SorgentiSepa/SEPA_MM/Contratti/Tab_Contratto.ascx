<%@ control language="VB" autoeventwireup="false" codefile="Tab_Contratto.ascx.vb"
    inherits="Contratti_Tab_Contratto" %>
<style type="text/css">
     .style1BB {
        width: 140px;
    }

    .style2B {
        width: 119px;
    }
</style>
<div style="width: 1130px; position: absolute; top: 168px; height: 520px;">
    <table width="100%">
        <tr>
            <td style="width: 223px">
                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Codice contratto"></asp:Label>
            </td>
            <td style="width: 223px">
                <img alt="" id="imgDefProroga" src="../NuoveImm/Img_PrgAssegDef.png" style="cursor: pointer; visibility: hidden;"
                    onclick="ApriDefProroga();" />
            </td>
        </tr>
        <tr>
            <td style="width: 223px">
                <asp:TextBox ID="txtCodContratto" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="157px" TabIndex="7"></asp:TextBox>
            </td>
            <td style="width: 223px">
                <asp:CheckBox ID="chTutore" runat="server" Font-Names="ARIAL" Font-Size="8pt" Text="Assistito da Tutore Straordinario" />
            </td>
            <td style="width: 223px">
                <img alt="" id="DRA" src="../NuoveImm/img_DRA.png" style="cursor: pointer; display:none;" onclick="ApriDRA();" />
            </td>
        </tr>
    </table>
    <table style="height: 32px" width="100%">
        <tr>
            <td style="width: 101px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="TIPOLOGIA CONTRATTO" Width="136px"></asp:Label>
            </td>
            <td style="vertical-align: text-bottom">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" />
                <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt"
                    Text="In caso di occupazione abusiva la durata del rapporto e le date sono da intendersi come fittizie, tranne che per la data di decorrenza."
                    Visible="False" Width="676px"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 500px">
                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Nome ufficiale"></asp:Label>
            </td>
            <td style="height: 18px">
                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Descrizione" Width="128px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 500px">
                <asp:DropDownList ID="cmbNomeUfficiale" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="500px" AutoPostBack="True" TabIndex="8">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtDescrcontratto" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="330px" TabIndex="9"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 500px">
                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Rif. Legislativo" Width="128px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Destinazione d'uso" Width="128px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 500px">
                <asp:TextBox ID="txtRifLegislativo" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Width="500px" TabIndex="10"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="cmbDestUso" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Width="330px" TabIndex="11">
                    <asp:ListItem Value="B">POSTO AUTO COPERTO,SCOPERTO,BOX,MOTOBOX ETC.</asp:ListItem>
                    <asp:ListItem Value="N">NEGOZIO, MAGAZZINO, LABORATORIO, UFFICIO, ETC.</asp:ListItem>
                    <asp:ListItem Value="R">RESIDENZIALE</asp:ListItem>
                    <asp:ListItem Value="0">STANDARD</asp:ListItem>
                    <asp:ListItem Value="C">COOPERATIVE</asp:ListItem>
                    <asp:ListItem Value="P">431 P.O.R.</asp:ListItem>
                    <asp:ListItem Value="A">392/78 ASSOCIAZIONI</asp:ListItem>
                    <asp:ListItem Value="D">431/98 Art.15 R.R.1/2004</asp:ListItem>
                    <asp:ListItem Value="V">431/98 Art.15 C.2 R.R.1/2004</asp:ListItem>
                    <asp:ListItem Value="S">431/98 SPECIALI</asp:ListItem>
                    <asp:ListItem Value="Z">FORZE DELL'ORDINE</asp:ListItem>
                    <asp:ListItem Value="X">CONCESSIONE SPAZI P.</asp:ListItem>
                    <asp:ListItem Value="Y">COMODATO D&#39;USO GRATUITO</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 500px">
                <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Origine" Width="128px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Descrizione Destinazione d'uso" Width="185px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="cmbOrigineRU" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="265px" TabIndex="8">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtDescrDestinazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="285px" TabIndex="14"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 500px">
                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Durata"></asp:Label>
            </td>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td valign="top" id="tdDurata">
                <asp:TextBox ID="txtDurata" runat="server" Font-Names="Arial" Font-Size="9pt" Width="30px"
                    TabIndex="12"></asp:TextBox>
                +<asp:TextBox ID="txtDurataRinnovo" runat="server" Font-Names="Arial"
                    Font-Size="9pt" Width="30px" TabIndex="13"></asp:TextBox>
            </td>
            <td id="tdDurataMesi" style="display: none;">
                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    Text="18 mesi"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkTemporanea" runat="server" Font-Names="arial" Font-Size="8pt"
                    Text="Assegnazione Temporanea" Font-Bold="True" />
                <asp:CheckBox ID="ChBolloEsente" runat="server" Font-Names="arial" Font-Size="8pt"
                    Text="Imposta di bollo esente ai sensi dell’art. 17 D.Lgs. n. 460 del 4-12-97;"
                    Width="350px" />
            </td>
        </tr>
    </table>
    <table style="height: 20px" width="100%">
        <tr>
            <td style="vertical-align: text-bottom">
                <hr style="background-color: #A9A9A9" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 150px">
                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Provv. Assegnazione"></asp:Label>
                <img id="INFO" alt="E' richiesto il numero e la data del provvedimento di assegnazione."
                    src="../NuoveImm/INFO.png" />
            </td>
            <td style="width: 113px">
                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Provvedimento"></asp:Label>
            </td>
            <td style="width: 113px">
                <asp:Label ID="Label33" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data T. Provv a S.T." 
                    ToolTip="Data trasmissione provvedimento a S.T."></asp:Label>
            </td>
            <td class="style2B">
                <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Stipula" Width="104px"></asp:Label>
            </td>
            <td class="style2B">
                <asp:Label ID="Label32" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Decorrenza"></asp:Label>
            </td>
            <td class="style2B">
                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Decor.AE"></asp:Label>
            </td>
            <td class="style1B">
                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Consegna"></asp:Label>
            </td>
            <td style="width: 166px">
                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Mesi entro cui disdettare"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 150px">
                <asp:TextBox ID="txtDelibera" runat="server" Font-Names="Arial" Font-Size="9pt" Width="150px"
                    ToolTip="Obbligatorio per Attivare il contratto!" MaxLength="50" TabIndex="15"></asp:TextBox>
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataDelibera" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa. Non può essere superiore della data di sistema!"
                    TabIndex="16"></asp:TextBox>
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataTrasST" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa. Non può essere superiore della data di sistema!"
                    TabIndex="16"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataTrasST"
                    Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                    Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataStipula" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa. Non può essere superiore alla data di decorrenza!"
                    TabIndex="17"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataStipula"
                    Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                    Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td class="style2B">
                <asp:TextBox ID="txtDataDecorrenza" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto!"
                    TabIndex="17"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                    ControlToValidate="txtDataDecorrenza" Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td class="style2B">
                <asp:TextBox ID="txtDataDecAE" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per creare il bollettino attivazione n.2 e Attivare il contratto! Formato gg/mm/aaaa."
                    TabIndex="18"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataDecAE"
                    Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                    Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td class="style1B">
                <asp:TextBox ID="txtDataConsegna" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa"
                    TabIndex="19"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataConsegna"
                    Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                    Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td style="width: 166px">
                <asp:TextBox ID="txtEntroCuiDisdettare" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="48px" ToolTip="Obbligatorio per Attivare il contratto!" MaxLength="2"
                    TabIndex="20"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtEntroCuiDisdettare"
                    ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" TabIndex="303"
                    ValidationExpression="^\d+$" Font-Bold="False" ToolTip="Valore Numerico"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 150px">
                <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data scadenza"></asp:Label>
            </td>
            <td style="width: 113px">
                <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data seconda scadenza" Width="120px"></asp:Label>
            </td>
            <td style="width: 113px">
                <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Invio/Ricez. Disdetta" Width="123px"></asp:Label>
            </td>
            <td class="style2B">&nbsp;
            </td>
            <td class="style2B">
                <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Mittente Disdetta" Width="123px"></asp:Label>
            </td>
            <td class="style1B">
                <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Notifica Disdetta" Width="123px"></asp:Label>
            </td>
            <td style="width: 166px">
                <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Disdetta/R.Forzoso" Width="127px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 150px">
                <asp:TextBox ID="txtDataScadenza" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa"
                    TabIndex="21" ReadOnly="True"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDataScadenza"
                    Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                    Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataSecScadenza" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="22" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa"
                    ReadOnly="True"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataSecScadenza"
                    Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                    Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataDisdetta0" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="23" ToolTip="Formato gg/mm/aaaa"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                    ControlToValidate="txtDataDisdetta0" Display="Dynamic" ErrorMessage="Errata!"
                    Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td class="style2B">&nbsp;
            </td>
            <td class="style2B">
                <asp:DropDownList ID="cmbMittenteDisdetta" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="100px" TabIndex="24">
                    <asp:ListItem Value="0">LOCATORE</asp:ListItem>
                    <asp:ListItem Value="1">CONDUTTORE</asp:ListItem>
                    <asp:ListItem Value="-1" Selected="True">--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style1B">
                <asp:TextBox ID="txtNotificaDisdetta" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="25" ToolTip="Formato gg/mm/aaaa"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                    ControlToValidate="txtNotificaDisdetta" Display="Dynamic" ErrorMessage="Errata!"
                    Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td style="width: 166px">
                <asp:TextBox ID="txtDataDisdetta" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="26" ToolTip="Formato gg/mm/aaaa"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDataDisdetta"
                    Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                    Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
            </td>
            <td style="width: 166px">&nbsp
            </td>
        </tr>
        <tr>
            <td style="width: 150px">
                <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Motivo R.Forzoso" ToolTip="Indicare il motivo del recupero forzoso. Non valorizzare in caso di disdetta."></asp:Label>
            </td>
            <td style="width: 113px">&nbsp;
            </td>
            <td style="width: 113px">
                <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Sloggio"></asp:Label>
            </td>
            <td class="style2B">&nbsp;
            </td>
            <td class="style2B">&nbsp;
            </td>



        </tr>
        <tr>
            <td style="width: 150px">
                <asp:DropDownList ID="cmbForzoso" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="120px" TabIndex="27">
                    <asp:ListItem Value="0">Morosità</asp:ListItem>
                    <asp:ListItem Value="1">Decadenza</asp:ListItem>
                    <asp:ListItem Value="2">O.Abusiva</asp:ListItem>
                    <asp:ListItem Value="3">Cessione</asp:ListItem>
                    <asp:ListItem Value="4" Selected="True">--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 113px">
                <img id="DRA0" src="../NuoveImm/Img_DateForzoso.png" style="cursor: pointer" onclick="ApriSfratto();" />
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataRiconsegna" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="28" ToolTip="La data, tranne nei rapporti virtuali, deve essere inserita dall'operatore di filiale."></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                    ControlToValidate="txtDataRiconsegna" Display="Dynamic" ErrorMessage="Errata!"
                    Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>

            </td>
            <td class="style2B">&nbsp;
            </td>
            <td class="style2B">&nbsp;
            </td>
            <td colspan="7">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblMotivoChiusura" runat="server" Style="display: none;" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" style="text-align: left">
                            <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Data/Ora Appuntamento Pre-Sloggio"></asp:Label>
                        </td>
                        <td style="width: 5px">&nbsp;
                        </td>
                        <td colspan="2" style="text-align: left">
                            <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Data/Ora Appuntamento Sloggio"></asp:Label>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDataAppPresloggio" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="75px" MaxLength="10" ToolTip="Formato gg/mm/aaaa" TabIndex="29"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                ControlToValidate="txtDataAppPresloggio" Display="Dynamic" ErrorMessage="Errata!"
                                Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOraAppPresloggio" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="50px" MaxLength="10" ToolTip="Formato HH:MM" TabIndex="30"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                ControlToValidate="txtOraAppPresloggio" ErrorMessage="Errata!" Font-Names="Arial"
                                Font-Size="8pt" ToolTip="Inserire orario formato HH:MM" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 10px">&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataAppSloggio" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="75px" MaxLength="10" TabIndex="31" ToolTip="Formato gg/mm/aaaa"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDataAppSloggio"
                                Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                                Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                        </td>
                        <td>&nbsp;<asp:TextBox ID="txtOraAppSloggio" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Width="50px" MaxLength="10" TabIndex="32"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                ControlToValidate="txtOraAppSloggio" ErrorMessage="Errata!" Font-Names="Arial"
                                Font-Size="8pt" ToolTip="Inserire orario formato HH:MM" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <table style="height: 50px" width="100%">
        <tr>
            <td style="width: 34px" valign="top">
                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="NOTE" Width="32px"></asp:Label>
            </td>
            <td style="vertical-align: top">
                <div style="border: 1px solid #000080; overflow: auto; width: 1000px; height: 70px;">
                    <asp:DataGrid ID="DataGridNote" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                        BorderWidth="1px" CellPadding="1" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        PageSize="3" Width="995px">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Mode="NumericPages" Visible="False" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="White" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_RIFERIMENTO" HeaderText="ID_RIF" ReadOnly="True" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATAORA" HeaderStyle-HorizontalAlign="Center" HeaderText="DATA INSERIM.">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_EVENTO" HeaderStyle-HorizontalAlign="Center" HeaderText="DATA EVENTO">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderStyle-HorizontalAlign="Center" HeaderText="NOTE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="55%" Wrap="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OPERATORE" HeaderStyle-HorizontalAlign="Center" HeaderText="OPERATORE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="15%" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </td>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Image ID="imgAggNota" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png" ToolTip="Aggiungi Nota"
                                Style="width: 16px; cursor: pointer;" onclick="AggiungiNote();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="ImgModifyNota" runat="server" ImageUrl="~/ANAUT/img/Pencil-icon.png"
                                ToolTip="Modifica Nota" Style="width: 16px; cursor: pointer;" onclick="ModificaNote();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    function ApriDRA() {
    if (document.getElementById('HStatoContratto').value != 'CHIUSO' && document.getElementById('VIRTUALE').value == '0') {
        if (<%=indiceconnessione %>!='-1') {
          window.open('DisdRinnAzLegali.aspx?ID_CONTRATTO=<%=indicecontratto %>','DRA','height=600,width=1300');
          }
          else
          {
          alert('Salvare prima di richiamare questa fuzione!');
          }
          }
          else
          {
          alert('Operazione non possibile!');
          }
    }

    function ApriSfratto() {
    if (document.getElementById('HStatoContratto').value != 'CHIUSO' && document.getElementById('VIRTUALE').value == '0') {
        if (<%=indiceconnessione %>!='-1') {
            window.open('SfrattoEsecutivo.aspx?ID_CNS=<%=indiceconnessione %>&ID_CONTRATTO=<%=indicecontratto %>', 'Sfratti', 'height=280,width=500');
          }
          else
          {
          alert('Salvare prima di richiamare questa fuzione!');
          }
          }
          else
          {
          alert('Operazione non possibile!');
          }
    }
    function ApriDefProroga() {
        document.getElementById('USCITA').value='1';
        if (<%=indiceconnessione %>!='-1') {
            var dialogResults = window.showModalDialog('Proroga_AssDefinitiva/PaginaScelta.aspx?IDC='+document.getElementById('txtIdContratto').value +'&COD='+document.getElementById('Tab_Contratto1_txtCodContratto').value,'','status:no;dialogWidth:450px;dialogHeight:320px;dialogHide:true;help:no;scroll:no');
            if (dialogResults != undefined)
            {
                if (document.getElementById('imgSalva')) {
                    if (dialogResults != '1')
                    {
                      document.getElementById('dataScad2').value = dialogResults;
                    }
                    document.getElementById('imgSalva').click();
                }
            }
        }
        else
        {
            alert('Salvare prima di richiamare questa fuzione!');
        }
    }
</script>
