<%@ Control Language="vb" AutoEventWireup="false" Inherits="CM.Dom_Richiedente" enableViewState="True" CodeFile="Dom_RichiedenteVSA.ascx.vb" %>
<P>
    &nbsp;&nbsp;</P>
<P>&nbsp;</P>
<P>&nbsp;</P>
<DIV id="ric" style="BORDER-RIGHT: lightsteelblue 1px solid; BORDER-TOP: lightsteelblue 1px solid; LEFT: 10px; BORDER-LEFT: lightsteelblue 1px solid; WIDTH: 641px; BORDER-BOTTOM: lightsteelblue 1px solid; POSITION: absolute; TOP: 107px; HEIGHT: 315px; BACKGROUND-COLOR: #ffffff; z-index: 200;">
    <asp:textbox id="txtCognome" 
        style="Z-INDEX: 100; LEFT: 76px; POSITION: absolute; TOP: 41px" tabIndex="1" 
        Columns="53" runat="server" Height="20px" Font-Bold="False" 
        CssClass="CssMaiuscolo" Font-Names="Times New Roman" Font-Size="8pt" 
        ForeColor="Blue" MaxLength="50" ReadOnly="True"></asp:textbox>
        <asp:label id="lblRichiedente" 
        style="Z-INDEX: 101; LEFT: 11px; POSITION: absolute; TOP: 8px" runat="server" 
        Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" 
        Font-Size="10pt" Width="50px" ForeColor="Black">RICHIEDENTE</asp:label>
        <asp:Label ID="lblNumDom" runat="server" CssClass="CssLabel" 
            Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 389px; position: absolute;
            top: 7px; width: 226px;" ForeColor="Blue" Visible="False"></asp:Label>
    <asp:label id="Label1" 
        style="Z-INDEX: 101; LEFT: 11px; POSITION: absolute; TOP: 43px" runat="server" 
        Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" 
        Font-Size="8pt" Width="50px">Cognome</asp:label><asp:label id="Label2" 
        style="Z-INDEX: 102; LEFT: 350px; POSITION: absolute; TOP: 46px" runat="server" 
        Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" 
        Font-Size="8pt" Width="31px">Nome</asp:label>
	<P><asp:textbox id="txtNome" 
            style="Z-INDEX: 103; LEFT: 390px; POSITION: absolute; TOP: 41px" tabIndex="2" 
            Columns="53" runat="server" Height="20px" Font-Bold="False" 
            CssClass="CssMaiuscolo" Font-Names="Times New Roman" Font-Size="8pt" 
            ForeColor="Blue" MaxLength="50" ReadOnly="True"></asp:textbox></P>
	<asp:label id="Label3" 
        style="Z-INDEX: 104; LEFT: 13px; POSITION: absolute; TOP: 72px" runat="server" 
        Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" 
        Font-Size="8pt" Width="50px">Sesso</asp:label><asp:label id="Label4" 
        style="Z-INDEX: 105; LEFT: 132px; POSITION: absolute; TOP: 70px" runat="server" 
        Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" 
        Font-Size="8pt" Width="64px">Cod. Fiscale</asp:label><asp:label id="Label5" 
        style="Z-INDEX: 106; LEFT: 12px; POSITION: absolute; TOP: 136px" runat="server" 
        Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" 
        Font-Size="8pt" Width="50px">Nato in</asp:label><asp:label id="Label6" 
        style="Z-INDEX: 107; LEFT: 248px; POSITION: absolute; TOP: 131px" 
        runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" 
        Font-Names="Times New Roman" Font-Size="8pt" Width="21px">Pr.</asp:label>
    <asp:label id="Label7" 
        style="Z-INDEX: 108; LEFT: 342px; POSITION: absolute; TOP: 129px" 
        runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" 
        Font-Names="Times New Roman" Font-Size="8pt" Width="45px">Comune</asp:label>
    <asp:label id="Label8" 
        style="Z-INDEX: 109; LEFT: 565px; POSITION: absolute; TOP: 128px" 
        runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" 
        Font-Names="Times New Roman" Font-Size="8pt" Width="13px">Il</asp:label>
    <asp:label id="Label12" 
        style="Z-INDEX: 110; LEFT: 12px; POSITION: absolute; TOP: 167px" runat="server" 
        Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" 
        Font-Size="8pt" Width="55px">Residente</asp:label><asp:label id="Label11" 
        style="Z-INDEX: 111; LEFT: 249px; POSITION: absolute; TOP: 166px" 
        runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" 
        Font-Names="Times New Roman" Font-Size="8pt" Width="18px">Pr.</asp:label>
    <asp:label id="Label10" 
        style="Z-INDEX: 112; LEFT: 343px; POSITION: absolute; TOP: 163px" 
        runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" 
        Font-Names="Times New Roman" Font-Size="8pt" Width="44px">Comune</asp:label>
    <asp:label id="Label9" 
        style="Z-INDEX: 113; LEFT: 13px; POSITION: absolute; TOP: 104px" runat="server" 
        Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" 
        Font-Size="8pt" Width="188px">Periodo di residenza in Lombardia</asp:label>
    <asp:label id="Label13" 
        style="Z-INDEX: 114; LEFT: 13px; POSITION: absolute; TOP: 195px" runat="server" 
        Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" 
        Font-Size="8pt" Width="51px">Indirizzo</asp:label><asp:label id="Label14" 
        style="Z-INDEX: 115; LEFT: 344px; POSITION: absolute; TOP: 194px" 
        runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" 
        Font-Names="Times New Roman" Font-Size="8pt" Width="37px">Civico</asp:label>
    <asp:label id="Label15" 
        style="Z-INDEX: 116; LEFT: 444px; POSITION: absolute; TOP: 194px" 
        runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" 
        Font-Names="Times New Roman" Font-Size="8pt" Width="29px">CAP</asp:label>
    <asp:label id="Label16" 
        style="Z-INDEX: 117; LEFT: 528px; POSITION: absolute; TOP: 195px" 
        runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" 
        Font-Names="Times New Roman" Font-Size="8pt" Width="46px">Tel.</asp:label>
    <asp:label id="Label17" 
        style="Z-INDEX: 118; LEFT: 14px; POSITION: absolute; TOP: 220px" runat="server" 
        Height="10px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
        ForeColor="#0000C0" Width="616px" BackColor="LemonChiffon" BorderStyle="Solid" 
        BorderWidth="1px">.............................................................................................. RECAPITO...........................................................................................</asp:label><asp:label id="Label18" style="Z-INDEX: 119; LEFT: 14px; POSITION: absolute; TOP: 234px" runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Width="50px">Presso</asp:label><asp:label id="Label20" style="Z-INDEX: 120; LEFT: 15px; POSITION: absolute; TOP: 274px" runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Width="24px">Pr.</asp:label>
	<P><asp:label id="Label19" style="Z-INDEX: 121; LEFT: 67px; POSITION: absolute; TOP: 274px" runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Width="63px">Comune di</asp:label></P>
	<asp:label id="Label21" style="Z-INDEX: 122; LEFT: 235px; POSITION: absolute; TOP: 274px" runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Width="29px">CAP</asp:label><asp:label id="Label22" style="Z-INDEX: 123; LEFT: 280px; POSITION: absolute; TOP: 274px" runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Width="51px">Indirizzo</asp:label><asp:label id="Label23" style="Z-INDEX: 124; LEFT: 520px; POSITION: absolute; TOP: 274px" runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Width="24px">N°</asp:label><asp:label id="Label24" style="Z-INDEX: 125; LEFT: 563px; POSITION: absolute; TOP: 274px" runat="server" Height="18px" Font-Bold="True" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Width="46px">Telefono</asp:label><asp:textbox id="txtPresso" style="Z-INDEX: 126; LEFT: 14px; POSITION: absolute; TOP: 248px" Columns="147" runat="server" Height="20px" Font-Bold="False" CssClass="CssMaiuscolo" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" TabIndex="18"></asp:textbox>
    <asp:textbox id="txtCF" 
        style="Z-INDEX: 127; LEFT: 200px; POSITION: absolute; TOP: 68px" tabIndex="4" 
        Columns="22" runat="server" Height="20px" Font-Bold="False" 
        CssClass="CssMaiuscolo" Font-Names="Times New Roman" Font-Size="8pt" 
        ForeColor="Blue" MaxLength="16" AutoPostBack="True"></asp:textbox>
    <asp:textbox id="txtDataNascita" 
        style="Z-INDEX: 128; LEFT: 579px; POSITION: absolute; TOP: 125px" Columns="7" 
        runat="server" Height="20px" Font-Bold="True" CssClass="CssMaiuscolo" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10" 
        TabIndex="9"></asp:textbox><asp:textbox id="txtIndRes" 
        style="Z-INDEX: 129; LEFT: 144px; POSITION: absolute; TOP: 191px" Columns="36" 
        runat="server" Height="20px" Font-Bold="False" CssClass="CssMaiuscolo" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="40" 
        TabIndex="14"></asp:textbox><asp:textbox id="txtCivicoRes" 
        style="Z-INDEX: 130; LEFT: 393px; POSITION: absolute; TOP: 192px" Columns="4" 
        runat="server" Height="20px" Font-Bold="False" CssClass="CssMaiuscolo" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5" 
        TabIndex="15"></asp:textbox><asp:textbox id="txtTelRes" 
        style="Z-INDEX: 131; LEFT: 551px; POSITION: absolute; TOP: 191px" Columns="13" 
        runat="server" Height="20px" Font-Bold="False" CssClass="CssMaiuscolo" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="15" 
        TabIndex="17"></asp:textbox><asp:textbox id="txtCAPRes" 
        style="Z-INDEX: 132; LEFT: 475px; POSITION: absolute; TOP: 191px" Columns="4" 
        runat="server" Height="20px" Font-Bold="False" CssClass="CssMaiuscolo" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5" 
        TabIndex="16"></asp:textbox><asp:dropdownlist id="cmbSesso" 
        style="Z-INDEX: 133; LEFT: 76px; POSITION: absolute; TOP: 69px" tabIndex="3" 
        runat="server" Height="20px" Font-Bold="False" CssClass="CssProv" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" Width="48px">
		<asp:ListItem Value="F" Selected="True">M</asp:ListItem>
		<asp:ListItem Value="F">F</asp:ListItem>
	</asp:dropdownlist><asp:dropdownlist id="cmbNazioneNas" 
        style="Z-INDEX: 134; LEFT: 76px; POSITION: absolute; TOP: 131px" runat="server" 
        Height="20px" Font-Bold="False" CssClass="CssComuniNazioni" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" 
        AutoPostBack="True" Width="166px" TabIndex="6"></asp:dropdownlist>
    <asp:dropdownlist id="cmbNazioneRes" 
        style="Z-INDEX: 135; LEFT: 76px; POSITION: absolute; TOP: 163px" runat="server" 
        Height="20px" Font-Bold="False" CssClass="CssComuniNazioni" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" 
        AutoPostBack="True" Width="166px" TabIndex="10"></asp:dropdownlist>
    <asp:dropdownlist id="cmbPrNas" 
        style="Z-INDEX: 136; LEFT: 270px; POSITION: absolute; TOP: 129px" 
        runat="server" Height="20px" Font-Bold="False" CssClass="CssProv" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" 
        AutoPostBack="True" Width="48px" TabIndex="7"></asp:dropdownlist>
    <asp:dropdownlist id="cmbPrRes" 
        style="Z-INDEX: 137; LEFT: 270px; POSITION: absolute; TOP: 163px" 
        runat="server" Height="20px" Font-Bold="False" CssClass="CssProv" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" 
        AutoPostBack="True" Width="48px" TabIndex="11"></asp:dropdownlist>
    <asp:dropdownlist id="cmbComuneNas" 
        style="Z-INDEX: 138; LEFT: 393px; POSITION: absolute; TOP: 125px" 
        runat="server" Height="20px" Font-Bold="False" CssClass="CssComuniNazioni" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" Width="166px" 
        TabIndex="8"></asp:dropdownlist><asp:dropdownlist id="cmbComuneRes" 
        style="Z-INDEX: 139; LEFT: 393px; POSITION: absolute; TOP: 161px" 
        runat="server" Height="20px" Font-Bold="False" CssClass="CssComuniNazioni" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" 
        AutoPostBack="True" Width="166px" TabIndex="12"></asp:dropdownlist>
    <asp:dropdownlist id="cmbTipoIRes" 
        style="Z-INDEX: 140; LEFT: 75px; POSITION: absolute; TOP: 192px" runat="server" 
        Height="20px" Font-Bold="False" CssClass="CssIndirizzo" 
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" Width="66px" 
        TabIndex="13">
		<asp:ListItem Value="F" Selected="True">M</asp:ListItem>
		<asp:ListItem Value="F">F</asp:ListItem>
	</asp:dropdownlist><asp:dropdownlist id="cmbProvRec" style="Z-INDEX: 141; LEFT: 15px; POSITION: absolute; TOP: 288px" runat="server" Height="20px" Font-Bold="False" CssClass="CssProv" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" AutoPostBack="True" Width="50px" TabIndex="19"></asp:dropdownlist><asp:dropdownlist id="cmbComuneRec" style="Z-INDEX: 142; LEFT: 67px; POSITION: absolute; TOP: 288px" runat="server" Height="20px" Font-Bold="False" CssClass="CssComuniNazioni" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" AutoPostBack="True" Width="166px" TabIndex="20"></asp:dropdownlist><asp:textbox id="txtCAPRec" style="Z-INDEX: 143; LEFT: 235px; POSITION: absolute; TOP: 288px" Columns="3" runat="server" Height="20px" Font-Bold="False" CssClass="CssMaiuscolo" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5" TabIndex="21"></asp:textbox><asp:dropdownlist id="cmbTipoIRec" style="Z-INDEX: 144; LEFT: 280px; POSITION: absolute; TOP: 288px" runat="server" Height="20px" Font-Bold="False" CssClass="CssIndirizzo" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" Width="66px" TabIndex="22">
		<asp:ListItem Value="F" Selected="True">M</asp:ListItem>
		<asp:ListItem Value="F">F</asp:ListItem>
	</asp:dropdownlist><asp:textbox id="txtIndirizzoRec" style="Z-INDEX: 145; LEFT: 347px; POSITION: absolute; TOP: 288px" Columns="33" runat="server" Height="20px" Font-Bold="False" CssClass="CssMaiuscolo" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="40" TabIndex="23"></asp:textbox><asp:textbox id="txtCivicoRec" style="Z-INDEX: 146; LEFT: 519px; POSITION: absolute; TOP: 288px" Columns="2" runat="server" Height="20px" Font-Bold="False" CssClass="CssMaiuscolo" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5" TabIndex="24"></asp:textbox><asp:textbox id="txtTelRec" style="Z-INDEX: 147; LEFT: 563px; POSITION: absolute; TOP: 288px" Columns="10" runat="server" Height="20px" Font-Bold="False" CssClass="CssMaiuscolo" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="15" TabIndex="25"></asp:textbox>
	<P><asp:label id="lblErrData" 
            style="Z-INDEX: 148; LEFT: 579px; POSITION: absolute; TOP: 143px" 
            runat="server" Height="16px" Font-Names="Times New Roman" Font-Size="X-Small" 
            ForeColor="Red" Width="44px" Visible="False"></asp:label>
        <asp:dropdownlist id="cmbResidenza" 
            style="Z-INDEX: 149; LEFT: 199px; POSITION: absolute; TOP: 99px" runat="server" 
            Height="20px" Font-Bold="False" CssClass="CssComuniNazioni" 
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" Width="436px" 
            TabIndex="5">
</asp:DropDownList>
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
            ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#DOM" Style="z-index: 151;
            left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px" >Aiuto</asp:HyperLink>
    </P>
</DIV>
