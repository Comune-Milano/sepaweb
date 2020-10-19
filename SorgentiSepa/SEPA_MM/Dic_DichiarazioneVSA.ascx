<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_DichiarazioneVSA.ascx.vb"
    Inherits="Dic_Dichiarazione" %>
&nbsp;&nbsp;
<div id="dic" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 106px; height: 315px; background-color: #ffffff; z-index: 103;">
    &nbsp;&nbsp;
    <asp:TextBox ID="txtCognome" runat="server" Columns="53" Font-Bold="False" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" MaxLength="50" Style="z-index: 100; left: 76px;
        position: absolute; top: 74px" TabIndex="4" CssClass="CssMaiuscolo"></asp:TextBox>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 10px; position: absolute;
        top: 74px" Width="50px">Cognome</asp:Label>
    <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 102; left: 343px; position: absolute;
        top: 74px" Width="31px">Nome</asp:Label>
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QA" Style="z-index: 103;
        left: 620px; position: absolute; top: 2px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
    <p>
        <asp:TextBox ID="txtNome" runat="server" Columns="53" Font-Bold="False" Font-Names="Times New Roman"
            Font-Size="8pt" ForeColor="Blue" MaxLength="50" Style="z-index: 104; left: 390px;
            position: absolute; top: 74px" TabIndex="5" CssClass="CssMaiuscolo"></asp:TextBox>
    </p>
    <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 105; left: 9px; position: absolute;
        top: 105px" Width="64px">Cod. Fiscale</asp:Label>
    <asp:Label ID="Label9" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 106; left: 9px; position: absolute;
        top: 29px" Width="64px">Milano, lì</asp:Label>
    <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 107; left: 11px; position: absolute;
        top: 162px" Width="50px">Nato in</asp:Label>
    <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 108; left: 248px; position: absolute;
        top: 162px" Width="21px">Pr.</asp:Label>
    <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 109; left: 342px; position: absolute;
        top: 162px" Width="45px">Comune</asp:Label>
    <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 110; left: 562px; position: absolute;
        top: 162px" Width="13px">Il</asp:Label>
    <asp:Label ID="Label12" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 111; left: 12px; position: absolute;
        top: 223px" Width="55px">Residente</asp:Label>
    <asp:Label ID="Label11" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 112; left: 249px; position: absolute;
        top: 223px" Width="18px">Pr.</asp:Label>
    <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 113; left: 343px; position: absolute;
        top: 223px" Width="44px">Comune</asp:Label>
    <asp:Label ID="Label13" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 114; left: 13px; position: absolute;
        top: 248px" Width="51px">Indirizzo</asp:Label>
    <asp:Label ID="Label14" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 115; left: 344px; position: absolute;
        top: 248px" Width="37px">Civico</asp:Label>
    <asp:Label ID="Label15" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 116; left: 444px; position: absolute;
        top: 248px" Width="29px">CAP</asp:Label>
    <asp:Label ID="Label16" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 117; left: 528px; position: absolute;
        top: 248px" Width="46px">Tel.</asp:Label>
    &nbsp;&nbsp;
    <asp:TextBox ID="messaggio" runat="server" BorderColor="White" BorderStyle="Solid"
        BorderWidth="1px" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ReadOnly="True"
        Style="z-index: 142; left: 11px; position: absolute; top: 52px" Width="617px"></asp:TextBox>
    <p>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 4px; position: absolute;
            top: 4px; width: 279px;">QUADRO A: DATI ANAGRAFICI DEL RICHIEDENTE</asp:Label>
            <asp:Label ID="lblNumDich" runat="server" CssClass="CssLabel" 
            Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 389px; position: absolute;
            top: 3px; width: 226px;" ForeColor="Blue" Visible="False"></asp:Label>
        <asp:DropDownList ID="cmbNazioneNas" runat="server" AutoPostBack="True" Style="z-index: 120;
            left: 76px; position: absolute; top: 161px" Width="168px" CssClass="CssComuniNazioni"
            Font-Names="TIMES" Font-Size="8pt" TabIndex="8" Enabled="False">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbPrNas" runat="server" AutoPostBack="True" CssClass="CssProv"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
            Height="20px" Style="left: 270px; position: absolute; top: 161px; z-index: 121;"
            TabIndex="9" Width="48px" Enabled="False">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <asp:TextBox ID="txtCF" runat="server" AutoPostBack="True" Columns="22" Font-Bold="False"
        Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 122;
        left: 76px; position: absolute; top: 105px" TabIndex="6" CssClass="CssMaiuscolo"></asp:TextBox>
    <asp:TextBox ID="txtDataNascita" runat="server" Columns="7" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 123; left: 573px;
        position: absolute; top: 162px" TabIndex="11" CssClass="CssMaiuscolo"></asp:TextBox>
    <asp:TextBox ID="txtData1" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 124; left: 76px;
        position: absolute; top: 28px;" TabIndex="3" Width="76px" CssClass="CssMaiuscolo"
        Height="20px"></asp:TextBox>
    <asp:TextBox ID="txtIndRes" runat="server" Columns="36" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="40" Style="z-index: 125; left: 144px;
        position: absolute; top: 248px" TabIndex="16" CssClass="CssMaiuscolo" Width="180px"></asp:TextBox>
    <asp:TextBox ID="txtCivicoRes" runat="server" Columns="4" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 126; left: 393px;
        position: absolute; top: 248px" TabIndex="17" CssClass="CssMaiuscolo"></asp:TextBox>
    <asp:TextBox ID="txtTelRes" runat="server" Columns="13" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="15" Style="z-index: 127; left: 551px;
        position: absolute; top: 248px" TabIndex="19" CssClass="CssMaiuscolo"></asp:TextBox>
    <asp:TextBox ID="txtCAPRes" runat="server" Columns="4" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 128; left: 475px;
        position: absolute; top: 248px" TabIndex="18" CssClass="CssMaiuscolo"></asp:TextBox>
    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
    <p>
        <asp:Label ID="lblErrData" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="X-Small" ForeColor="Red" Height="16px" Style="z-index: 129; left: 471px;
            position: absolute; top: 180px" Visible="False" Width="164px"></asp:Label>
        <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="False" Font-Names="ARIAL"
            Font-Size="7pt" Font-Strikeout="False" ForeColor="#C00000" Style="z-index: 133;
            left: 390px; position: absolute; top: 106px" Width="117px"></asp:LinkButton>
        <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="False" Font-Names="ARIAL"
            Font-Size="7pt" Font-Strikeout="False" ForeColor="#C00000" Style="z-index: 133;
            left: 512px; position: absolute; top: 106px" Width="119px"></asp:LinkButton>
        &nbsp;&nbsp;
        <asp:Label ID="Label19" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="X-Small" ForeColor="Red" Height="16px" Style="z-index: 130; left: 159px;
            position: absolute; top: 28px" Visible="False" Width="405px"></asp:Label>
        <asp:Label ID="Label18" runat="server" BackColor="LemonChiffon" BorderStyle="Solid"
            BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000C0"
            Height="10px" Style="z-index: 131; left: 14px; position: absolute; top: 136px"
            Width="616px">................................................................................................ NATO A.............................................................................................</asp:Label>
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="txtId" runat="server" Style="left: 120px; position: absolute; top: 29px"
            Visible="False" Width="14px" Height="12px"></asp:TextBox>
        &nbsp;
        <asp:LinkButton ID="CFLABEL" runat="server" Font-Bold="False" Font-Names="ARIAL"
            Font-Size="7pt" Style="z-index: 133; left: 205px; position: absolute; top: 106px"
            Width="166px" Font-Strikeout="False" ForeColor="#C00000"></asp:LinkButton>
        <asp:TextBox ID="txtbinserito" runat="server" Style="left: 170px; position: absolute;
            top: 252px;" Width="15px" Height="8px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:DropDownList ID="cmbNazioneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
            Height="20px" Style="left: 75px; position: absolute; top: 223px; z-index: 135;"
            TabIndex="12" Width="166px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbPrRes" runat="server" AutoPostBack="True" CssClass="CssProv"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
            Height="20px" Style="left: 277px; position: absolute; top: 222px; z-index: 136;"
            TabIndex="13" Width="48px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbComuneRes" runat="server" AutoPostBack="True" CssClass="CssComuniNazioni"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
            Height="20px" Style="left: 394px; position: absolute; top: 222px; z-index: 137;"
            TabIndex="14" Width="238px">
        </asp:DropDownList>
        <asp:Label ID="Label17" runat="server" BackColor="LemonChiffon" BorderStyle="Solid"
            BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000C0"
            Height="10px" Style="z-index: 138; left: 14px; position: absolute; top: 198px"
            Width="616px">........................................................................................... RESIDENTE IN........................................................................................</asp:Label>
        <asp:DropDownList ID="cmbTipoIRes" runat="server" CssClass="CssIndirizzo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" Height="20px"
            Style="left: 75px; position: absolute; top: 248px; z-index: 139;" TabIndex="15"
            Width="66px">
            <asp:ListItem Selected="True" Value="F">M</asp:ListItem>
            <asp:ListItem Value="F">F</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="cmbComuneNas" runat="server" Style="z-index: 140; left: 387px;
            position: absolute; top: 162px" Font-Names="Times New Roman" Font-Size="8pt"
            Width="164px" CssClass="CssComuniNazioni" TabIndex="10" Enabled="False">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp; &nbsp;
        <asp:CheckBox ID="chTitolare" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Names="TIMES" Font-Size="8pt" Style="z-index: 141; left: 10px; position: absolute;
            top: 275px" Text="Nel nucleo familiare del richiedente esistono titolari di un contratto di assegnazione di alloggio di edilizia residenziale pubblica"
            Width="619px" />
    </p>
</div>
<asp:Label ID="Label20" runat="server" Visible="False"></asp:Label>
<script type="text/javascript">
    document.getElementById('Dic_Dichiarazione1_txtbinserito').style.visibility = 'hidden';

    function AttendiCF() {

        LeftPosition = (screen.width) ? (screen.width - 250) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 150) / 2 : 0;
        LeftPosition = LeftPosition;
        TopPosition = TopPosition;
        aaa = window.open('loadingCF.htm', '', 'height=150,top=' + TopPosition + ',left=' + LeftPosition + ',width=250');
        setTimeout("aaa.close();", 5000);
    }
</script>
