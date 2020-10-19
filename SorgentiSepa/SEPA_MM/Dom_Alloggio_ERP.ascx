<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Alloggio_ERP.ascx.vb" Inherits="Dom_Alloggio_ERP" %>
<DIV id="all" style="BORDER-RIGHT: lightsteelblue 1px solid; BORDER-TOP: lightsteelblue 1px solid; LEFT: 10px; BORDER-LEFT: lightsteelblue 1px solid; WIDTH: 641px; BORDER-BOTTOM: lightsteelblue 1px solid; POSITION: absolute; TOP: 107px; HEIGHT: 315px; BACKGROUND-COLOR: #ffffff; z-index: 199;">
	<P>&nbsp;</P>
	<asp:Label id="Label13" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 6px" Font-Size="8pt" Font-Names="Times New Roman" Height="18px" Width="533px" runat="server" Font-Bold="True">DATI DELL'ALLOGGIO ERP ASSEGNATO</asp:Label>
    <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 8px; position: absolute;
        top: 135px" Width="626px">DATI DEL CONTRATTO</asp:Label>
    &nbsp;&nbsp;
	<asp:DropDownList id="cmbGestore" style="Z-INDEX: 103; LEFT: 107px; POSITION: absolute; TOP: 84px" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" Width="523px" TabIndex="4">
	</asp:DropDownList><asp:DropDownList id="cmbAscensore" style="Z-INDEX: 103; LEFT: 341px; POSITION: absolute; TOP: 110px" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" Width="44px" TabIndex="8">
        <asp:ListItem Value="1">SI</asp:ListItem>
        <asp:ListItem Value="0">NO</asp:ListItem>
    </asp:DropDownList>
    &nbsp;
    <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 85px" Width="93px">Gestore</asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 33px" Width="93px" ForeColor="#0000C0">Comune</asp:Label>
    <asp:TextBox ID="txtComune" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 107px; position: absolute; top: 33px" Width="379px"></asp:TextBox>
    <asp:TextBox ID="txtIndirizzo" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 107px; position: absolute; top: 59px" TabIndex="2" Width="379px"></asp:TextBox>
    <asp:TextBox ID="txtCAP" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5" Style="z-index: 124;
        left: 550px; position: absolute; top: 33px" TabIndex="1" Width="81px"></asp:TextBox>
    <asp:TextBox ID="txtDecorrenza" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 374px; position: absolute; top: 154px" TabIndex="11" Width="81px"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 59px" Width="93px" ForeColor="#0000C0">Indirizzo</asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 505px; position: absolute;
        top: 33px" Width="28px" ForeColor="#0000C0">CAP</asp:Label>
    <asp:TextBox ID="txtCivico" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="8" Style="z-index: 124;
        left: 550px; position: absolute; top: 59px" TabIndex="3" Width="81px"></asp:TextBox>
    &nbsp;<br />
    <br />
    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 505px; position: absolute;
        top: 59px" Width="36px" ForeColor="#0000C0">Civico</asp:Label>
    <asp:TextBox ID="txtPiano" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5" Style="z-index: 124;
        left: 229px; position: absolute; top: 111px" TabIndex="7" Width="36px"></asp:TextBox>
    <asp:TextBox ID="txtLocali" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5" Style="z-index: 124;
        left: 596px; position: absolute; top: 111px" TabIndex="9" Width="36px"></asp:TextBox>
    <br />
    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 107px; position: absolute;
        top: 111px" Width="35px">Scala</asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 191px; position: absolute;
        top: 111px" Width="35px" ForeColor="#0000C0">Piano</asp:Label>
    <asp:TextBox ID="txtNumContratto" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 107px; position: absolute; top: 154px" TabIndex="10" Width="195px"></asp:TextBox>
    <br />
    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 404px; position: absolute;
        top: 111px" Width="183px" ForeColor="#0000C0">Numero Locali esclusi bagno e cucina</asp:Label>
    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 154px" Width="94px" ForeColor="#0000C0">Numero Contratto</asp:Label>
    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 313px; position: absolute;
        top: 154px" Width="62px" ForeColor="#0000C0">Decorrenza</asp:Label>
    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 111px" Width="59px" ForeColor="#0000C0">Interno/Sub</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 277px; position: absolute;
        top: 111px" Width="61px" ForeColor="#0000C0">Ascensore</asp:Label><asp:DropDownList id="cmbSesso" style="Z-INDEX: 103; LEFT: 389px; POSITION: absolute; TOP: 260px" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" Width="44px" TabIndex="18" Enabled="False">
            <asp:ListItem Value="1">M</asp:ListItem>
            <asp:ListItem Value="0">F</asp:ListItem>
        </asp:DropDownList>
    <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 351px; position: absolute;
        top: 261px" Width="31px">Sesso</asp:Label>
            <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TD" Style="z-index: 128;
        left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;&nbsp; <asp:DropDownList id="cmbTipoRapporto" style="Z-INDEX: 103; LEFT: 107px; POSITION: absolute; TOP: 175px" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" Width="349px" TabIndex="14">
    </asp:DropDownList>
    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 10px; position: absolute;
        top: 176px" Width="81px" ForeColor="#0000C0">Tipo Rapporto</asp:Label>
    <asp:TextBox ID="txtCognome" runat="server" Columns="53" CssClass="CssMaiuscolo"
        Font-Bold="False" Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="50"
        Style="z-index: 100; left: 76px; position: absolute; top: 236px" TabIndex="15"></asp:TextBox>
    <asp:Label ID="Label17" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 20px; position: absolute;
        top: 236px" Width="50px">Cognome</asp:Label>
    <asp:Label ID="Label23" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 10px; position: absolute;
        top: 198px" Width="618px">Compilare i dati sottostanti solo se il rapporto contrattuale è "Voltura/Subentro con titolo non formalizzato" oppure</asp:Label>
    <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 330px; position: absolute;
        top: 216px" Width="211px">"Voltura/Subentro in corso"</asp:Label>
    <asp:Label ID="Label18" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 102; left: 350px; position: absolute;
        top: 237px" Width="31px">Nome</asp:Label>
    <asp:TextBox ID="txtNome" runat="server" Columns="53" CssClass="CssMaiuscolo" Font-Bold="False"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="50"
        Style="z-index: 103; left: 389px; position: absolute; top: 236px" TabIndex="16"></asp:TextBox>
    <asp:Label ID="Label19" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 105; left: 20px; position: absolute;
        top: 287px" Width="50px">Nato in</asp:Label>
    <asp:DropDownList ID="cmbNazioneNas" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
        Font-Names="TIMES" Font-Size="8pt" Style="z-index: 106; left: 76px; position: absolute;
        top: 285px" Width="166px" TabIndex="19" Enabled="False">
    </asp:DropDownList>
    <asp:Label ID="Label20" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 107; left: 254px; position: absolute;
        top: 286px" Width="21px">Pr.</asp:Label>
    <asp:DropDownList ID="cmbPrNas" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Names="TIMES" Font-Size="8pt" Style="z-index: 108; left: 274px; position: absolute;
        top: 284px" Width="47px" TabIndex="20" Enabled="False">
    </asp:DropDownList>
    <asp:Label ID="Label21" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 109; left: 338px; position: absolute;
        top: 286px" Width="45px">Comune</asp:Label>
    <asp:DropDownList ID="cmbComuneNas" runat="server" CssClass="CssComuniNazioni" Font-Names="TIMES"
        Font-Size="8pt" Style="z-index: 110; left: 389px; position: absolute; top: 285px"
        Width="156px" TabIndex="21" Enabled="False">
    </asp:DropDownList>
    <asp:Label ID="Label22" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 111; left: 553px; position: absolute;
        top: 287px" Width="13px">Il</asp:Label>
    <asp:TextBox ID="txtDataNascita" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        MaxLength="10" Style="z-index: 112; left: 565px; position: absolute; top: 286px"
        TabIndex="22" Enabled="False"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;
    <asp:TextBox ID="txtCF" runat="server" Columns="53" CssClass="CssMaiuscolo" Font-Bold="False"
        Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 100;
        left: 76px; position: absolute; top: 261px" TabIndex="17" Width="142px" AutoPostBack="True"></asp:TextBox>
    <asp:Label ID="Label24" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 20px; position: absolute;
        top: 261px" Width="50px">C. Fiscale</asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label14" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Maroon"
        Style="left: 224px; position: absolute; top: 262px" Text="Codice F. ERRATO" Visible="False"></asp:Label>
    &nbsp;
    <asp:TextBox ID="txtInterno" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5" Style="z-index: 124;
        left: 67px; position: absolute; top: 111px" TabIndex="5" Width="36px"></asp:TextBox>
    <asp:TextBox ID="txtScala" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5" Style="z-index: 124;
        left: 144px; position: absolute; top: 111px" TabIndex="6" Width="36px"></asp:TextBox>
</DIV>
