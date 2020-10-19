<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Contratto.ascx.vb"
    Inherits="Contratti_Tab_Contratto" %>
<style type="text/css">
    .style1
    {
        width: 140px;
    }
    .style2
    {
        width: 140px;
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
                &nbsp;
            </td>
            <td style="width: 223px">
                <img alt="" id="imgDefProroga" src="../../NuoveImm/Img_PrgAssegDef.png" style="cursor: pointer;
                    visibility: hidden;" onclick="ApriDefProroga();" />
            </td>
        </tr>
        <tr>
            <td style="width: 223px">
                <asp:TextBox ID="txtCodContratto" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="157px" TabIndex="7" Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 223px">
                <asp:CheckBox ID="chTutore" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                    Text="Assistito da Tutore Straordinario" Enabled="False" />
            </td>
            <td style="width: 223px">
                &nbsp;</td>
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
                    Width="500px" TabIndex="8" Enabled="False">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtDescrcontratto" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="330px" TabIndex="9" Enabled="False"></asp:TextBox>
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
                    Width="500px" TabIndex="10" Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="cmbDestUso" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Width="330px" TabIndex="11" Enabled="False">
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
                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Durata"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Descrizione Destinazione d'uso" Width="185px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:TextBox ID="txtDurata" runat="server" Font-Names="Arial" Font-Size="9pt" Width="30px"
                    TabIndex="12" Enabled="False"></asp:TextBox>+<asp:TextBox 
                    ID="txtDurataRinnovo" runat="server" Font-Names="Arial"
                        Font-Size="9pt" Width="30px" TabIndex="13" Enabled="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="ChBolloEsente" runat="server" Font-Names="arial" Font-Size="8pt"
                    
                    Text="Imposta di bollo esente ai sensi dell’art. 17 D.Lgs. n. 460 del 4-12-97;" 
                    Enabled="False" />
            </td>
            <td>
                <asp:TextBox ID="txtDescrDestinazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="285px" TabIndex="14" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
            <td>
                <asp:CheckBox ID="chkTemporanea" runat="server" Font-Names="arial" Font-Size="8pt"
                    Text="Assegnazione Temporanea" Font-Bold="True" Enabled="False" />
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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 150px">
                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Provv. Assegnazione"></asp:Label>
                <img id="INFO" alt="E' richiesto il numero e la data del provvedimento di assegnazione."
                    src="../../NuoveImm/INFO.png" />
            </td>
            <td style="width: 113px">
                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Provvedimento"></asp:Label>
            </td>
            <td style="width: 113px">
                <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Stipula" Width="104px"></asp:Label>
            </td>
            <td class="style2">
                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data decorrenza"></asp:Label>
            </td>
            <td class="style1">
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
                    ToolTip="Obbligatorio per Attivare il contratto!" MaxLength="50" 
                    TabIndex="15" Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataDelibera" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa. Non può essere superiore della data di sistema!"
                    TabIndex="16" Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataStipula" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa. Non può essere superiore alla data di decorrenza!"
                    TabIndex="17" Enabled="False"></asp:TextBox>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtDataDecorrenza" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa. Non può essere inferiore alla data di stipula."
                    TabIndex="18" Enabled="False"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:TextBox ID="txtDataConsegna" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa"
                    TabIndex="19" Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 166px">
                <asp:TextBox ID="txtEntroCuiDisdettare" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="48px" ToolTip="Obbligatorio per Attivare il contratto!" MaxLength="2"
                    TabIndex="20" Enabled="False"></asp:TextBox>
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
            <td class="style2">
                <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Mittente Disdetta" Width="123px"></asp:Label>
            </td>
            <td class="style1">
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
                    TabIndex="21" Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataSecScadenza" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="22" 
                    ToolTip="Obbligatorio per Attivare il contratto! Formato gg/mm/aaaa" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataDisdetta0" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="23" ToolTip="Formato gg/mm/aaaa" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td class="style2">
                <asp:DropDownList ID="cmbMittenteDisdetta" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="100px" TabIndex="24" Enabled="False">
                    <asp:ListItem Value="0">LOCATORE</asp:ListItem>
                    <asp:ListItem Value="1">CONDUTTORE</asp:ListItem>
                    <asp:ListItem Value="-1" Selected="True">--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style1">
                <asp:TextBox ID="txtNotificaDisdetta" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="25" ToolTip="Formato gg/mm/aaaa" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 166px">
                <asp:TextBox ID="txtDataDisdetta" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="26" ToolTip="Formato gg/mm/aaaa" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 150px">
                <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Motivo R.Forzoso" ToolTip="Indicare il motivo del recupero forzoso. Non valorizzare in caso di disdetta."></asp:Label>
            </td>
            <td style="width: 113px">
                &nbsp;
            </td>
            <td style="width: 113px">
                <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Sloggio"></asp:Label>
            </td>
            <td class="style2">
                <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data/Ora App. Pre-Sloggio"></asp:Label>
            </td>
            <td class="style1">
                <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data/Ora App. Sloggio"></asp:Label>
            </td>
            <td style="width: 166px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 150px">
                <asp:DropDownList ID="cmbForzoso" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="120px" TabIndex="27" Enabled="False">
                    <asp:ListItem Value="0">Morosità</asp:ListItem>
                    <asp:ListItem Value="1">Decadenza</asp:ListItem>
                    <asp:ListItem Value="2">O.Abusiva</asp:ListItem>
                    <asp:ListItem Value="3">Cessione</asp:ListItem>
                    <asp:ListItem Value="4" Selected="True">--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 113px">
                &nbsp;</td>
            <td style="width: 113px">
                <asp:TextBox ID="txtDataRiconsegna" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="75px" MaxLength="10" TabIndex="28" 
                    ToolTip="La data, tranne nei rapporti virtuali, deve essere inserita dall'operatore di filiale." 
                    Enabled="False"></asp:TextBox>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtDataAppPresloggio" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="65px" MaxLength="10" ToolTip="Formato gg/mm/aaaa" TabIndex="21" 
                    Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txtOraAppPresloggio" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="40px" MaxLength="10" ToolTip="Formato HH:MM" TabIndex="21" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:TextBox ID="txtDataAppSloggio" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="65px" MaxLength="10" TabIndex="28" ToolTip="Formato gg/mm/aaaa" 
                    Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txtOraAppSloggio" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="40px" MaxLength="10" TabIndex="21" Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 166px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="6">
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="height: 50px" width="100%">
        <tr>
            <td style="width: 34px" valign="top">
                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="NOTE" Width="32px"></asp:Label>
            </td>
            <td style="vertical-align: top">
                <asp:TextBox ID="txtNote" runat="server" Enabled="False" Font-Names="Arial" 
                    Font-Size="9pt" Height="75px" MaxLength="2000" TabIndex="29" 
                    TextMode="MultiLine" Width="99%"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    function ApriDRA() {

    }

    function ApriSfratto() {

    }
    function ApriDefProroga() {

    }
</script>
