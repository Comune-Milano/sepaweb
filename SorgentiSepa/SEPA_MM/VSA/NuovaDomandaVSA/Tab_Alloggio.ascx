<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Alloggio.ascx.vb" Inherits="VSA_NuovaDomandaVSA_Tab_Alloggio" %>
<link rel="stylesheet" type="text/css" href="Styles/impromptu.css" />
<link href="Styles/StileAU.css" rel="stylesheet" type="text/css" />
<table width="97%">
    <tr>
        <td>
            <table>
                <tr>
                    <td style="width: 24%">
                        <asp:Label ID="Label13" Font-Size="9pt" Font-Names="Arial" runat="server" Font-Bold="True">DATI DELL'ALLOGGIO </asp:Label>
                    </td>
                    <td style="padding-left: 15px; width: 20%">
                        <asp:DropDownList ID="cmbTipoU" Font-Size="9pt" Font-Names="ARIAL" Font-Bold="True" runat="server" Width="150px" TabIndex="7" AutoPostBack="True">
                            <asp:ListItem Value="0">E.R.P.</asp:ListItem>
                            <asp:ListItem Value="1">LOTTO 4-5</asp:ListItem>
                            <asp:ListItem Value="2">A.L.E.R.</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="2" style="padding-left: 15px; padding-right: 250px; width: 60%">
                        <asp:Label ID="Label29" Style="width: 129px;" Font-Size="9pt" Font-Names="Arial" runat="server" Font-Bold="True">ASSEGNATO</asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
           
            <table >
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" CssClass="CssLabel">Cod. Unità</asp:Label>
                    </td>
                    <td style="width: 40%">
                        <asp:TextBox ID="txtCodiceUnita" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="50" Style="width: 138px;"></asp:TextBox>
                    </td>
                    <td style="width: 10%">
                        <asp:Button ID="Button1" runat="server" CssClass="bottone" Text="Seleziona" OnClientClick="ApriRicercaUI(); return false;" Width="100px" Visible="false" />
                    </td>
                    <td style="width: 20%">
                        &nbsp;
                    </td>
                    
                </tr>
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="93px" CssClass="CssLabel">Comune</asp:Label>
                    </td>
                    <td style="width: 40%">
                        <asp:TextBox ID="txtComune" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="50" Width="379px"></asp:TextBox>
                    </td>
                    <td align="center" style="width: 10%">
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="50px" CssClass="CssLabel" style="margin-right: 9px;">CAP</asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtCAP" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" Width="81px" style="margin-left: 100px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="93px" CssClass="CssLabel">Indirizzo</asp:Label>
                    </td>
                    <td style="width: 40%">
                        <asp:TextBox ID="txtIndirizzo" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="50" TabIndex="2" Width="379px"></asp:TextBox>
                    </td>
                    <td align="center" style="width: 10%">
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="50px" CssClass="CssLabel">Civico</asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtCivico" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="8" TabIndex="3" Width="81px" style="margin-left: 100px;"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td style="width: 18.6%">
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="90px" CssClass="CssLabel">Interno/Sub</asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="txtInterno" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="5" Width="138px"></asp:TextBox>
                    </td>
                    <td align="center" style="width: 10%">
                        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="35px" style="margin-right: 25px;">Scala</asp:Label>
                    </td>
                    <td style="width: 40%">
                        <asp:TextBox ID="txtScala" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="8" TabIndex="3" Width="81px"></asp:TextBox>
                        
                        <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="100px" CssClass="CssLabel" style="margin-left: 18.5%;">Superficie Netta</asp:Label>
                       <asp:TextBox ID="txtNetta" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="7" Width="36px" style="margin-left: 16%;"></asp:TextBox>
                    </td>
                    <%--<td style="width: 20%">
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="90px" CssClass="CssLabel">Interno/Sub</asp:Label> 85px
                    </td>
                    <td style="width: 40%">
                        <asp:TextBox ID="txtInterno" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="5" Width="30px"></asp:TextBox>
                        &nbsp;
                         <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="35px">Scala</asp:Label>
                         &nbsp;
                         <asp:TextBox ID="txtScala" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="6" Width="30px"></asp:TextBox>
                         &nbsp;
                         <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="35px" CssClass="CssLabel">Piano</asp:Label>
                         &nbsp;
                         <asp:DropDownList ID="cmbPianoUnita" Style="width: 124px;" Font-Size="9pt" Font-Names="Arial" Font-Bold="False" runat="server" TabIndex="8">
                        </asp:DropDownList>
                    </td>
                    <td align="center" style="width: 10%">
                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Style="width: 25px;" CssClass="CssLabel">Asc.</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cmbAscensore" Style="width: 45px;" Font-Size="9pt" Font-Names="Arial" CssClass="CssPresenta" Font-Bold="False" runat="server" TabIndex="8">
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20%">
                        <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="80px" CssClass="CssLabel">Sup.Netta</asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtNetta" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="7" Width="36px"></asp:TextBox>
                        &nbsp;
                        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Style="width: 150px;" CssClass="CssLabel">Locali esclusi bagno e cucina</asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtLocali" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="9" Width="36px"></asp:TextBox>
                    </td>--%>

                    <%--<td style="width: 20%;">
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="90px" CssClass="CssLabel">Interno/Sub</asp:Label>
                    </td>
                    <td align="left" style="width: 8%;">
                        <asp:TextBox ID="txtInterno" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="5" Width="30px"></asp:TextBox>
                    </td>
                    <td style="width: 8%;">
                        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="35px">Scala</asp:Label>
                    </td>
                    <td align="left" style="width: 8%;">
                        <asp:TextBox ID="txtScala" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="6" Width="30px"></asp:TextBox>
                    </td>
                    <td style="width: 8%;">
                        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="35px" CssClass="CssLabel">Piano</asp:Label>
                    </td>
                    <td align="left" style="width: 8%;">
                        <asp:DropDownList ID="cmbPianoUnita" Style="width: 124px;" Font-Size="9pt" Font-Names="Arial" Font-Bold="False" runat="server" TabIndex="8">
                        </asp:DropDownList>
                    </td>
                    <td align="center" style="width: 8%;">
                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Style="width: 25px;" CssClass="CssLabel">Asc.</asp:Label>
                    </td>
                    <td align="left" style="width: 8%;">
                        <asp:DropDownList ID="cmbAscensore" Style="width: 45px;" Font-Size="9pt" Font-Names="Arial" CssClass="CssPresenta" Font-Bold="False" runat="server" TabIndex="8">
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 8%;">
                        <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="80px" CssClass="CssLabel">Sup.Netta</asp:Label>
                    </td>
                    <td align="left" style="width: 8%;">
                        <asp:TextBox ID="txtNetta" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="7" Width="36px"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Style="width: 150px;" CssClass="CssLabel">Locali esclusi bagno e cucina</asp:Label>
                    </td>
                    <td align="center" style="width: 8%;">
                        <asp:TextBox ID="txtLocali" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="9" Width="36px"></asp:TextBox> 125px
                    </td>--%>
                </tr>
                <tr>
                    <td style="width: 18.6%">
                        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="35px" CssClass="CssLabel">Piano</asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:DropDownList ID="cmbPianoUnita" Style="width: 124px;" Font-Size="9pt" Font-Names="Arial" Font-Bold="False" runat="server" TabIndex="8">
                        </asp:DropDownList>
                    </td>
                    <td align="center" style="width: 10%">
                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Style="width: 25px;" CssClass="CssLabel">Ascensore</asp:Label>
                    </td>
                    <td style="width: 40%">
                        <asp:DropDownList ID="cmbAscensore" Style="width: 45px;" Font-Size="9pt" Font-Names="Arial" CssClass="CssPresenta" Font-Bold="False" runat="server" TabIndex="8">
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Style="width: 150px;margin-left: 27.5%;" CssClass="CssLabel" >Locali esclusi bagno e cucina</asp:Label>

                        <asp:TextBox ID="txtLocali" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="9" Width="36px" style="margin-left: 3%;"></asp:TextBox>
                    </td>
                 </tr>
            </table>
            <%--<table  cellpadding="0" cellspacing ="0">
                <tr>
                    <td style="width: 8%">
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="90px" CssClass="CssLabel">Interno/Sub</asp:Label>
                    </td>
                    <td style="width: 8%">
                        <asp:TextBox ID="txtInterno" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="5" Width="30px"></asp:TextBox>
                    </td>
                    <td style="padding-left: 10px; width: 5%" align="right">
                        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="35px">Scala</asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:TextBox ID="txtScala" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="6" Width="30px"></asp:TextBox>
                    </td>
                    <td style="padding-left: 10px; width: 5%" align="right">
                        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="35px" CssClass="CssLabel">Piano</asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:DropDownList ID="cmbPianoUnita" Style="width: 124px;" Font-Size="9pt" Font-Names="Arial" Font-Bold="False" runat="server" TabIndex="8">
                        </asp:DropDownList>
                    </td>
                    <td style="padding-left: 10px; width: 5%" align="right">
                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Style="width: 25px;" CssClass="CssLabel">Asc.</asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:DropDownList ID="cmbAscensore" Style="width: 45px;" Font-Size="9pt" Font-Names="Arial" CssClass="CssPresenta" Font-Bold="False" runat="server" TabIndex="8">
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 5%">
                        <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="80px" CssClass="CssLabel">Sup.Netta</asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:TextBox ID="txtNetta" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="7" Width="36px"></asp:TextBox>
                    </td>
                    <td style="width: 20%" align="right">
                        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Style="width: 150px;" CssClass="CssLabel">Locali esclusi bagno e cucina</asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:TextBox ID="txtLocali" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="5" TabIndex="9" Width="36px"></asp:TextBox>
                    </td>
                </tr>
            </table>--%>
        </td>
    </tr>
    <tr><td>&nbsp</td></tr>
    <tr>
        <td>
            <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt">DATI DEL CONTRATTO</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%"  cellpadding="0" cellspacing ="0">
                <tr>
                    <td style="width: 218px;">
                        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="120px">Numero Contratto</asp:Label>
                    </td>
                    <td style="width: 250px;">
                        <asp:TextBox ID="txtNumContratto" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="50" TabIndex="10" Width="195px" style="margin-bottom:4px;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Button2" runat="server" CssClass="bottone" Text="Seleziona" OnClientClick="ApriRicercaRU();" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 218px;">
                        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="120px" CssClass="CssLabel">Decorrenza</asp:Label>
                    </td>
                    <td style="width: 250px;">
                        <asp:TextBox ID="txtDecorrenza" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="11" Width="81px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td>&nbsp</td></tr>
    <tr>
        <td>
            <asp:Label ID="Label23" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Width="300px">INTESTATARIO EFFETTIVO DEL CONTRATTO</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%"  cellpadding="0" cellspacing ="0">
                <tr>
                    <td style="width: 218px;">
                        <asp:Label ID="Label17" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="80px">Cognome</asp:Label>
                    </td>
                    <td style="width: 450px;" >
                        <asp:TextBox ID="txtCognome" runat="server" Columns="53" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" MaxLength="50" TabIndex="15" style="margin-bottom:4px;"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label18" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="31px">Nome</asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtNome" runat="server" Columns="53" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" MaxLength="50" TabIndex="16" style="margin-left:120px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 218px;">
                        <asp:Label ID="Label24" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="80px">C. Fiscale</asp:Label>
                    </td>
                    <td style="width: 450px;" >
                        <asp:TextBox ID="txtCF" runat="server" Columns="53" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" MaxLength="16" TabIndex="17" Width="142px" AutoPostBack="True" style="margin-bottom:4px;"></asp:TextBox>
                        &nbsp;
                        <asp:Label ID="Label14" runat="server" Font-Names="arial" Font-Size="9pt" ForeColor="Maroon" Text="Codice F. ERRATO" Visible="False"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="31px">Sesso</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbSesso" Font-Size="9pt" Font-Names="Arial" CssClass="CssPresenta" Font-Bold="False" runat="server" Width="44px" TabIndex="18" style=" margin-left:120px;">
                            <asp:ListItem Value="1">M</asp:ListItem>
                            <asp:ListItem Value="0">F</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label22" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="35px" style="margin-left: 125px;">Nato il</asp:Label> <!-- prima era 15% -->
                        <asp:TextBox ID="txtDataNascita" runat="server" CssClass="CssMaiuscolo" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="22" width="76px"></asp:TextBox>           
                    </td>
                </tr>
                <tr>
                    <td style="width: 218px;">
                        <asp:Label ID="Label19" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="80px">Nato in</asp:Label>
                    </td>
                    <td style="width: 450px;" >
                       <asp:DropDownList ID="cmbNazioneNas" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni" Font-Names="Arial" Font-Size="9pt" Width="166px" TabIndex="19">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Label ID="Label20" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="40px" style="padding-left:30px;">Provincia</asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="cmbPrNas" runat="server" AutoPostBack="True" CssClass="CssProv" Font-Names="Arial" Font-Size="9pt" Width="47px" TabIndex="20">
                        </asp:DropDownList>
                    </td>
                    <td align="center">
                       <asp:Label ID="Label21" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="45px">Comune</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbComuneNas" runat="server" CssClass="CssComuniNazioni" Font-Names="Arial" Font-Size="9pt" Width="156px" TabIndex="21" Enabled="False" style="margin-left:120px;">
                        </asp:DropDownList>    
                    </td>
                    <%--<td style="width: 16.3%"> prima sopra era 15% style="padding-left: 20px;" 
                        <asp:Label ID="Label22" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="13px">Il</asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtDataNascita" runat="server" CssClass="CssMaiuscolo" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="22"></asp:TextBox>
                    </td>--%>
                    <%--<td style="width: 13%">
                        <asp:Label ID="Label19" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="80px">Nato in</asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:DropDownList ID="cmbNazioneNas" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni" Font-Names="Arial" Font-Size="9pt" Width="166px" TabIndex="19">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="Label20" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="21px">Provincia</asp:Label>
                    </td>
                    <td style="width: 13%">
                        <asp:DropDownList ID="cmbPrNas" runat="server" AutoPostBack="True" CssClass="CssProv" Font-Names="Arial" Font-Size="9pt" Width="47px" TabIndex="20">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 12%" align="center">
                        <asp:Label ID="Label21" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="45px">Comune</asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 30%">
                        <asp:DropDownList ID="cmbComuneNas" runat="server" CssClass="CssComuniNazioni" Font-Names="Arial" Font-Size="9pt" Width="156px" TabIndex="21" Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 3%">
                        <asp:Label ID="Label22" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" Width="13px">Il</asp:Label>
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtDataNascita" runat="server" CssClass="CssMaiuscolo" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="22"></asp:TextBox>
                    </td>--%>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div>
    <asp:HiddenField ID="HSL" runat="server" Value="0" />
</div>
<script type="text/javascript">
    function ApriRicercaUI() {
        if (document.getElementById('Dom_Alloggio_ERP1_HSL').value == '0') {
            //var win = null;
            //LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
            //TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
            //LeftPosition = LeftPosition - 20;
            //TopPosition = TopPosition - 20;
            //window.open('RicercaUI.aspx', 'Ricerca', 'height=450,top=0,left=0,width=670,scrollbars=no');
            alert('Non Disponibile al momento! USARE IL PULSANTE SELEZIONA CONTRATTO.');
        }
        else {
            alert('Non Disponibile al momento!');
        }
    }

    function ApriRicercaRU() {
        if (document.getElementById('Dom_Alloggio_ERP1_HSL').value == '0') {
            var win = null;
            LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
            TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
            LeftPosition = LeftPosition - 20;
            TopPosition = TopPosition - 20;
            //window.showModalDialog('RicercaUI.aspx', window, 'status:no;dialogTop=' + TopPosition + ';dialogLeft=' + LeftPosition + ';dialogWidth:670px;dialogHeight:450px;dialogHide:true;help:no;scroll:no');
            window.open('../RicercaRU.aspx?ID=<%=IdDichiarazione %>', 'RicercaRU', 'height=450,top=0,left=0,width=670,scrollbars=no');
        }
        else {
            alert('Non Disponibile al momento!');
        }
    }

</script>
