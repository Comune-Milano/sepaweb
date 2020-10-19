<%@ Control Language="VB" AutoEventWireup="false" CodeFile="dic_Utenza.ascx.vb" Inherits="dic_Utenza" %>
&nbsp;&nbsp;
<div id="dic" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 106px; height: 400px; background-color: #ffffff; z-index: 103;">
    &nbsp;&nbsp;
    <asp:TextBox ID="txtCognome" runat="server" Columns="53" Font-Bold="False" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" MaxLength="50" Style="z-index: 100; left: 76px;
        position: absolute; top: 73px" TabIndex="4" Height="18px"></asp:TextBox>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 10px; position: absolute;
        top: 73px" Width="50px">Cognome</asp:Label>
    <asp:Label ID="lbl45_Lotto" runat="server" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="10pt" Height="18px" Style="z-index: 101; left: 180px; position: absolute;
        top: 27px" Visible="False">TRATTASI DI A.U. 4-5 LOTTO - Cod.Convocazione:</asp:Label>
    <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 102; left: 343px; position: absolute;
        top: 73px" Width="31px">Nome</asp:Label>
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" Style="z-index: 103; left: 620px; position: absolute;
        top: 2px" Width="16px">Aiuto</asp:HyperLink>
    <p>
        <asp:TextBox ID="txtNome" runat="server" Columns="53" Font-Bold="False" Font-Names="Times New Roman"
            Font-Size="8pt" ForeColor="Blue" MaxLength="50" Style="z-index: 104; left: 390px;
            position: absolute; top: 73px" TabIndex="5" CssClass="CssMaiuscolo" Height="18px"></asp:TextBox>
    </p>
    <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 105; left: 9px; position: absolute;
        top: 101px" Width="64px">Cod. Fiscale</asp:Label>
    <asp:Label ID="Label9" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 106; left: 9px; position: absolute;
        top: 29px" Width="64px">Milano, lì</asp:Label>
    <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 107; left: 11px; position: absolute;
        top: 129px" Width="50px">Nato in</asp:Label>
    <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 108; left: 248px; position: absolute;
        top: 129px" Width="21px">Pr.</asp:Label>
    <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 109; left: 342px; position: absolute;
        top: 129px" Width="45px">Comune</asp:Label>
    <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 110; left: 562px; position: absolute;
        top: 129px" Width="13px">Il</asp:Label>
    <asp:Label ID="Label12" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 111; left: 12px; position: absolute;
        top: 170px" Width="55px">Residente</asp:Label>
    <asp:Label ID="Label11" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 112; left: 249px; position: absolute;
        top: 170px" Width="18px">Pr.</asp:Label>
    <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 113; left: 343px; position: absolute;
        top: 170px" Width="44px">Comune</asp:Label>
    <asp:Label ID="Label13" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 114; left: 13px; position: absolute;
        top: 196px" Width="51px">Indirizzo</asp:Label>
    <asp:Label ID="Label14" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 115; left: 344px; position: absolute;
        top: 196px" Width="37px">Civico</asp:Label>
    <asp:Label ID="Label15" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 116; left: 444px; position: absolute;
        top: 196px" Width="29px">CAP</asp:Label>
    <asp:Label ID="Label16" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 117; left: 528px; position: absolute;
        top: 196px" Width="46px">Tel.</asp:Label>
    &nbsp;&nbsp;
    <asp:TextBox ID="messaggio" runat="server" BorderColor="White" BorderStyle="Solid"
        BorderWidth="1px" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ReadOnly="True"
        Style="z-index: 118; left: 11px; position: absolute; top: 52px" Width="617px"></asp:TextBox>
    <p>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 4px; position: absolute;
            top: 4px" Width="410px">QUADRO A: DATI ANAGRAFICI</asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="cmbNazioneNas" runat="server" AutoPostBack="True" Style="z-index: 120;
            left: 76px; position: absolute; top: 128px" Width="168px" CssClass="CssComuniNazioni"
            Font-Names="TIMES" Font-Size="8pt" TabIndex="7" Enabled="False">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbPrNas" runat="server" AutoPostBack="True" CssClass="CssProv"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
            Height="20px" Style="left: 270px; position: absolute; top: 128px; z-index: 121;"
            TabIndex="8" Width="48px" Enabled="False">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <asp:TextBox ID="txtCF" runat="server" AutoPostBack="True" Columns="22" Font-Bold="False"
        Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 122;
        left: 76px; position: absolute; top: 101px" TabIndex="6" CssClass="CssMaiuscolo"
        Height="18px"></asp:TextBox>
    <asp:TextBox ID="txtDataNascita" runat="server" Columns="7" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 123; left: 573px;
        position: absolute; top: 129px" TabIndex="10" CssClass="CssMaiuscolo" Height="18px"></asp:TextBox>
    <asp:TextBox ID="txtData1" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 124; left: 76px;
        position: absolute; top: 28px;" TabIndex="3" Width="76px" Height="18px"></asp:TextBox>
    <asp:TextBox ID="txtCodConvocazione" runat="server" Columns="7" Font-Bold="True"
        Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 124;
        left: 467px; position: absolute; top: 26px; width: 142px;" TabIndex="3" Height="18px"
        Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtIndRes" runat="server" Columns="36" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="40" Style="z-index: 125; left: 144px;
        position: absolute; top: 196px" TabIndex="15" CssClass="CssMaiuscolo" Width="180px"
        Height="18px"></asp:TextBox>
    <asp:TextBox ID="txtCivicoRes" runat="server" Columns="4" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 126; left: 393px;
        position: absolute; top: 196px" TabIndex="16" CssClass="CssMaiuscolo" Height="18px"></asp:TextBox>
    <asp:TextBox ID="txtTelRes" runat="server" Columns="13" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="15" Style="z-index: 127; left: 551px;
        position: absolute; top: 196px" TabIndex="18" CssClass="CssMaiuscolo" Height="18px"></asp:TextBox>
    <asp:TextBox ID="txtCAPRes" runat="server" Columns="4" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 128; left: 475px;
        position: absolute; top: 196px" TabIndex="17" CssClass="CssMaiuscolo" Height="18px"
        Width="48px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
    <p>
        <asp:Label ID="lblErrData" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="X-Small" ForeColor="Red" Height="16px" Style="z-index: 129; left: 471px;
            position: absolute; top: 151px" Visible="False" Width="164px"></asp:Label>
        <asp:Label ID="Label19" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="X-Small" ForeColor="Red" Height="16px" Style="z-index: 130; left: 77px;
            position: absolute; top: 50px" Visible="False" Width="405px"></asp:Label>
        &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:HiddenField ID="txtId" runat="server" />
        &nbsp;
        <asp:LinkButton ID="CFLABEL" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            Style="z-index: 132; left: 205px; position: absolute; top: 102px" Width="424px"></asp:LinkButton>
        <asp:HiddenField ID="txtbinserito" runat="server" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:DropDownList ID="cmbNazioneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
            Height="20px" Style="left: 75px; position: absolute; top: 170px; z-index: 134;"
            TabIndex="11" Width="166px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbPrRes" runat="server" AutoPostBack="True" CssClass="CssProv"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
            Height="20px" Style="left: 277px; position: absolute; top: 169px; z-index: 135;"
            TabIndex="12" Width="48px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbComuneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
            Height="20px" Style="left: 394px; position: absolute; top: 169px; z-index: 136;"
            TabIndex="13" Width="238px">
        </asp:DropDownList>
        &nbsp;
        <asp:DropDownList ID="cmbTipoIRes" runat="server" CssClass="CssIndirizzo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" Height="20px"
            Style="left: 75px; position: absolute; top: 196px; z-index: 137;" TabIndex="14"
            Width="66px">
            <asp:ListItem Selected="True" Value="F">M</asp:ListItem>
            <asp:ListItem Value="F">F</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="cmbComuneNas" runat="server" Style="z-index: 138; left: 387px;
            position: absolute; top: 129px" Font-Names="Times New Roman" Font-Size="8pt"
            Width="164px" CssClass="CssComuniNazioni" TabIndex="9" Enabled="False">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label20" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 139; left: 13px; position: absolute;
            top: 221px" Width="51px">Scala</asp:Label>
        <asp:Label ID="Label21" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 140; left: 121px; position: absolute;
            top: 221px" Width="36px">Piano</asp:Label>
        <asp:Label ID="Label22" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 141; left: 204px; position: absolute;
            top: 221px" Width="66px">N° Alloggio</asp:Label>
        <asp:CheckBox ID="ch1" runat="server" Font-Bold="True" Font-Names="times" Font-Size="8pt"
            Style="z-index: 142; left: 10px; position: absolute; top: 242px" Text="Intestatario di contratto"
            TabIndex="25" />
        <asp:CheckBox ID="Ch2" runat="server" Font-Bold="True" Font-Names="times" Font-Size="8pt"
            Style="z-index: 143; left: 326px; position: absolute; top: 243px" Text="Utente in corso di voltura"
            TabIndex="27" />
        <asp:CheckBox ID="Sosp7" runat="server" Font-Bold="False" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 485px; position: absolute; top: 358px" Text="Documentazione Mancante"
            TabIndex="45" />
        <asp:CheckBox ID="Sosp6" runat="server" Font-Bold="False" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 392px; position: absolute; top: 377px" Text="Diminuzione"
            TabIndex="48" />
        <asp:CheckBox ID="Sosp5" runat="server" Font-Bold="False" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 392px; position: absolute; top: 358px" Text="Ampliamento"
            TabIndex="44" />
        <asp:CheckBox ID="Sosp4" runat="server" Font-Bold="False" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 276px; position: absolute; top: 377px" Text="Ricongiungimento"
            TabIndex="47" />
        <asp:CheckBox ID="Sosp3" runat="server" Font-Bold="False" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 276px; position: absolute; top: 358px" Text="Titolare Trasferito"
            TabIndex="43" />
        <asp:CheckBox ID="Sosp2" runat="server" Font-Bold="False" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 164px; position: absolute; top: 377px" Text="Titolare Separato"
            TabIndex="46" />
        <asp:CheckBox ID="X2" runat="server" Font-Bold="False" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 10px; position: absolute; top: 358px" Text="Sospensione indagine per"
            TabIndex="41" />
        <asp:CheckBox ID="X1" runat="server" Font-Bold="False" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 10px; position: absolute; top: 338px" Text="Dichiarazione da Verificare"
            TabIndex="40" />
        <asp:CheckBox ID="chPosta" runat="server" Font-Bold="True" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 453px; position: absolute; top: 261px" Text="Dichiarazione ricev. per posta"
            TabIndex="31" ToolTip="Dichiarazione ricevuta tramite posta" />
        <asp:CheckBox ID="chInServizio" runat="server" Font-Bold="True" Font-Names="times"
            Font-Size="8pt" Style="z-index: 144; left: 453px; position: absolute; top: 281px"
            Text="In servizio" TabIndex="31" ToolTip="Specificare se la persona è attualmente in servizio" />
        <%--<asp:Label ID="Label31" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 150; left: 77px; position: absolute;
            top: 50px; width: 86px;">Stato di servizio</asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
            Height="20px" Style="left: 175px; position: absolute; top: 50px; z-index: 136;"
            TabIndex="13" Width="320px">
            <asp:ListItem Value="1">In servizio</asp:ListItem>
            <asp:ListItem Value="2">Quiescenza per cessazione del rapporto di lavoro</asp:ListItem>
            
        </asp:DropDownList>--%>
        <asp:CheckBox ID="Sosp1" runat="server" Font-Bold="False" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 164px; position: absolute; top: 358px" Text="Titolare Deceduto"
            TabIndex="42" />
        <asp:CheckBox ID="Ch6" runat="server" Font-Bold="True" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 163px; position: absolute; top: 261px" Text="Dichiarazione presentata da altro parente delegato"
            TabIndex="30" />
        <asp:CheckBox ID="Ch5" runat="server" Font-Bold="True" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 10px; position: absolute; top: 261px" Text="Rapp.Legale"
            TabIndex="29" />
        <asp:CheckBox ID="Ch3" runat="server" Font-Bold="True" Font-Names="times" Font-Size="8pt"
            Style="z-index: 144; left: 484px; position: absolute; top: 243px" Text="Occupante Abusivo"
            TabIndex="28" />
        <asp:TextBox ID="txtScala" runat="server" Columns="4" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5"
            Style="z-index: 145; left: 75px; position: absolute; top: 219px" TabIndex="19"
            Width="40px"></asp:TextBox>
        <asp:TextBox ID="txtPiano" runat="server" Columns="4" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5"
            Style="z-index: 146; left: 160px; position: absolute; top: 219px" TabIndex="20"
            Width="36px"></asp:TextBox>
        <asp:TextBox ID="txtAlloggio" runat="server" Columns="4" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="18px" MaxLength="5" Style="z-index: 147; left: 276px; position: absolute;
            top: 219px" TabIndex="21" Width="46px"></asp:TextBox>
        <asp:Label ID="Label23" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 148; left: 216px; position: absolute;
            top: 283px" Width="86px">Cod. Utente</asp:Label>
        <asp:TextBox ID="txtCodAlloggio" runat="server" Columns="4" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="18px" MaxLength="20" Style="z-index: 149; left: 285px; position: absolute;
            top: 284px; width: 83px;" TabIndex="33"></asp:TextBox>
        <asp:Label ID="Label24" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 150; left: 345px; position: absolute;
            top: 221px" Width="40px">Foglio</asp:Label>
        <asp:Label ID="Label25" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 151; left: 444px; position: absolute;
            top: 222px; width: 34px;">Map.</asp:Label>
        <asp:Label ID="Label26" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 152; left: 529px; position: absolute;
            top: 221px" Width="66px">Sub</asp:Label>
        <asp:TextBox ID="txtFoglio" runat="server" Columns="4" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10"
            Style="z-index: 153; left: 393px; position: absolute; top: 220px" TabIndex="22"></asp:TextBox>
        <asp:TextBox ID="txtMappale" runat="server" Columns="4" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10"
            Style="z-index: 154; left: 475px; position: absolute; top: 220px; width: 49px;"
            TabIndex="23"></asp:TextBox>
        <asp:TextBox ID="txtSub" runat="server" Columns="4" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10"
            Style="z-index: 155; left: 551px; position: absolute; top: 220px; width: 60px;"
            TabIndex="24"></asp:TextBox>
        <asp:CheckBox ID="Ch4" runat="server" Font-Bold="True" Font-Names="times" Font-Size="8pt"
            Style="z-index: 156; left: 163px; position: absolute; top: 243px" Text="Altro Comp. Maggiorenne"
            TabIndex="26" />
        <asp:Label ID="Label18" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 157; left: 13px; position: absolute;
            top: 288px" Width="60px">Cod. U.I.</asp:Label>
        <asp:Label ID="Label30" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Style="z-index: 158; left: 463px; position: absolute; top: 311px;
            width: 50px; height: 16px;" ToolTip="Codice Rapporto Reale">R. Reale</asp:Label>
        <asp:Label ID="Label27" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 158; left: 13px; position: absolute;
            top: 311px" Width="60px">Cod.Rapp.</asp:Label>
        <asp:TextBox ID="txtPosizione" runat="server" Columns="4" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="18px" MaxLength="250" Style="z-index: 159; left: 74px; position: absolute;
            top: 284px; width: 107px;" TabIndex="32"></asp:TextBox>
        <asp:TextBox ID="TxtRapportoReale" runat="server" Columns="4" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="18px" MaxLength="50" Style="z-index: 160; left: 509px; position: absolute;
            top: 309px; width: 119px;" TabIndex="39"></asp:TextBox>
        <asp:TextBox ID="TxtRapporto" runat="server" Columns="4" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="18px" MaxLength="250" Style="z-index: 160; left: 74px; position: absolute;
            top: 309px; width: 119px;" TabIndex="34"></asp:TextBox>
        <asp:RadioButton ID="R1" runat="server" Font-Names="ARIAL" Font-Size="8pt" Style="z-index: 161;
            left: 407px; position: absolute; top: 300px; width: 42px;" Text="erp" Checked="True"
            GroupName="AA" TabIndex="37" />
        <asp:RadioButton ID="R2" runat="server" Font-Names="ARIAL" Font-Size="8pt" Style="z-index: 163;
            left: 407px; position: absolute; top: 317px" Text="Ec" GroupName="AA" TabIndex="38" />
        <asp:Label ID="Label17" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 110; left: 303px; position: absolute;
            top: 311px; width: 31px;" ToolTip="Data Fine Contratto">Cess.</asp:Label>
        <asp:Label ID="Label29" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 110; left: 202px; position: absolute;
            top: 311px; width: 26px;" ToolTip="Data Decorrenza">Dec.</asp:Label>
        <asp:TextBox ID="txtDataCessazione" runat="server" Columns="7" CssClass="CssMaiuscolo"
            Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="18px" MaxLength="10" Style="z-index: 123; left: 336px; position: absolute;
            top: 309px; width: 63px;" TabIndex="36" ToolTip="Data cessazione del contratto"></asp:TextBox>
        <asp:TextBox ID="txtDataDec" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10"
            Style="z-index: 123; left: 232px; position: absolute; top: 309px; width: 63px;"
            TabIndex="35" ToolTip="Data di decorrenza del contratto"></asp:TextBox>
        <asp:Label ID="Label28" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="X-Small" ForeColor="Red" Style="z-index: 129; left: 73px; position: absolute;
            top: 326px; height: 15px; width: 35px;" Visible="False"></asp:Label>
        <asp:Image ID="imgPatrimonio" runat="server" Style="position: absolute; top: 282px;
            left: 187px; cursor: pointer" ImageUrl="~/NuoveImm/home16.png" ToolTip="Visualizza i dati dell'unità immobiliare" />
    </p>
</div>
<script type="text/javascript">
    document.getElementById('Dic_Utenza1_txtbinserito').style.visibility = 'hidden';

    function AttendiCF() {

        LeftPosition = (screen.width) ? (screen.width - 250) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 150) / 2 : 0;
        LeftPosition = LeftPosition;
        TopPosition = TopPosition;
        aaa = window.open('loadingCF.htm', '', 'height=150,top=' + TopPosition + ',left=' + LeftPosition + ',width=250');
        setTimeout("aaa.close();", 5000);
    }
</script>
