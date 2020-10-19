<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Alloggio_VSA.ascx.vb" Inherits="Dom_Alloggio_VSA" %>
<div id="all" style="BORDER-RIGHT: lightsteelblue 1px solid; BORDER-TOP: lightsteelblue 1px solid; LEFT: 10px; BORDER-LEFT: lightsteelblue 1px solid; WIDTH: 641px; BORDER-BOTTOM: lightsteelblue 1px solid; POSITION: absolute; TOP: 107px; HEIGHT: 315px; BACKGROUND-COLOR: #ffffff; z-index:200;">
	<p>
    <img alt="" src="../NuoveImm/Img_Seleziona.png" onclick="ApriRicercaUI();" 
            style="position:absolute; top: 24px; left: 211px; cursor:pointer;"/></p>
	<asp:Label id="Label29" 
        style="Z-INDEX: 101; LEFT: 295px; POSITION: absolute; TOP: 5px; width: 229px;" 
        Font-Size="8pt" Font-Names="Times New Roman" Height="18px" runat="server" 
        Font-Bold="True">ASSEGNATO</asp:Label>
	<asp:Label id="Label13" 
        style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 6px; width: 229px;" 
        Font-Size="8pt" Font-Names="Times New Roman" Height="18px" runat="server" 
        Font-Bold="True">DATI DELL'ALLOGGIO </asp:Label>
    <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 8px; position: absolute;
        top: 136px" Width="626px">DATI DEL CONTRATTO</asp:Label>
    &nbsp;&nbsp;
	<asp:DropDownList id="cmbTipoU" 
        style="Z-INDEX: 103; LEFT: 139px; POSITION: absolute; TOP: 3px" 
        Font-Size="8pt" Font-Names="ARIAL" Font-Bold="True" Height="20px" runat="server" 
        Width="150px" TabIndex="7" AutoPostBack="True">
        <asp:ListItem Value="0">E.R.P.</asp:ListItem>
        <asp:ListItem Value="1">LOTTO 4-5</asp:ListItem>
        <asp:ListItem Value="2">A.L.E.R.</asp:ListItem>
    </asp:DropDownList>
	<asp:DropDownList id="cmbPianoUnita" 
        style="Z-INDEX: 103; LEFT: 211px; POSITION: absolute; TOP: 110px; width: 124px;" 
        ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" 
        Font-Bold="False" Height="20px" runat="server" TabIndex="8">
    </asp:DropDownList>
	<asp:DropDownList id="cmbAscensore" 
        style="Z-INDEX: 103; LEFT: 368px; POSITION: absolute; TOP: 110px; width: 40px;" 
        ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" 
        CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" 
        TabIndex="8">
        <asp:ListItem Value="1">SI</asp:ListItem>
        <asp:ListItem Value="0">NO</asp:ListItem>
    </asp:DropDownList>
    &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 35px" Width="93px" CssClass="CssLabel">Cod. Unità</asp:Label>
    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 62px" Width="93px" CssClass="CssLabel">Comune</asp:Label>
    <asp:TextBox ID="txtCodiceUnita" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 67px; position: absolute; top: 32px; width: 138px;"></asp:TextBox>
    <asp:TextBox ID="txtComune" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 67px; position: absolute; top: 58px" Width="379px"></asp:TextBox>
    <asp:TextBox ID="txtIndirizzo" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 66px; position: absolute; top: 85px" TabIndex="2" Width="379px"></asp:TextBox>
    <asp:TextBox ID="txtCAP" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5" Style="z-index: 124;
        left: 550px; position: absolute; top: 60px" TabIndex="1" Width="81px"></asp:TextBox>
    <asp:TextBox ID="txtDecorrenza" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 107px; position: absolute; top: 181px" TabIndex="11" Width="81px"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 87px" Width="93px" CssClass="CssLabel">Indirizzo</asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 505px; position: absolute;
        top: 60px" Width="28px" CssClass="CssLabel">CAP</asp:Label>
    <asp:TextBox ID="txtCivico" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="8" Style="z-index: 124;
        left: 550px; position: absolute; top: 85px" TabIndex="3" Width="81px"></asp:TextBox>
    &nbsp;<br />
    <br />
    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 505px; position: absolute;
        top: 87px" Width="36px" CssClass="CssLabel">Civico</asp:Label>
    <asp:TextBox ID="txtNetta" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 124;
        left: 466px; position: absolute; top: 111px; height: 18px; right: 137px;" 
        TabIndex="7" Width="36px"></asp:TextBox>
    <asp:TextBox ID="txtLocali" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5" Style="z-index: 124;
        left: 596px; position: absolute; top: 111px" TabIndex="9" Width="36px"></asp:TextBox>
    <br />
    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 107px; position: absolute;
        top: 111px" Width="35px">Scala</asp:Label>
    <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 416px; position: absolute;
        top: 111px" Width="35px" CssClass="CssLabel">Sup.Netta</asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 179px; position: absolute;
        top: 111px" Width="35px" CssClass="CssLabel">Piano</asp:Label>
    <asp:TextBox ID="txtNumContratto" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 107px; position: absolute; top: 159px" TabIndex="10" Width="195px"></asp:TextBox>
    <br />
    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Style="z-index: 101; left: 512px; position: absolute;
        top: 111px; height: 18px; width: 77px;" CssClass="CssLabel">Locali esclusi bagno e cucina</asp:Label>
    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 159px" Width="94px" CssClass="CssLabel">Numero Contratto</asp:Label>
    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 10px; position: absolute;
        top: 182px" Width="62px" CssClass="CssLabel">Decorrenza</asp:Label>
    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 111px" Width="59px" CssClass="CssLabel">Interno/Sub</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Style="z-index: 101; left: 342px; position: absolute;
        top: 111px; width: 25px; height: 17px;" CssClass="CssLabel">Asc.</asp:Label>
    <asp:DropDownList id="cmbSesso" 
        style="Z-INDEX: 103; LEFT: 389px; POSITION: absolute; TOP: 260px" 
        ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" 
        CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" 
        Width="44px" TabIndex="18">
            <asp:ListItem Value="1">M</asp:ListItem>
            <asp:ListItem Value="0">F</asp:ListItem>
        </asp:DropDownList>
    <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 351px; position: absolute;
        top: 261px" Width="31px">Sesso</asp:Label>
            
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
    &nbsp; &nbsp;&nbsp; 
    <asp:TextBox ID="txtCognome" runat="server" Columns="53" CssClass="CssMaiuscolo"
        Font-Bold="False" Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="50"
        Style="z-index: 100; left: 76px; position: absolute; top: 236px" TabIndex="15"></asp:TextBox>
    <asp:Label ID="Label17" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 20px; position: absolute;
        top: 236px" Width="50px">Cognome</asp:Label>
    <asp:Label ID="Label23" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 10px; position: absolute;
        top: 213px" Width="618px">INTESTATARIO EFFETTIVO DEL CONTRATTO</asp:Label>
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
        top: 285px" Width="166px" TabIndex="19">
    </asp:DropDownList>
    <asp:Label ID="Label20" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 107; left: 254px; position: absolute;
        top: 286px" Width="21px">Pr.</asp:Label>
    <asp:DropDownList ID="cmbPrNas" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Names="TIMES" Font-Size="8pt" Style="z-index: 108; left: 274px; position: absolute;
        top: 284px" Width="47px" TabIndex="20">
    </asp:DropDownList>
    <asp:Label ID="Label21" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 109; left: 338px; position: absolute;
        top: 286px" Width="45px">Comune</asp:Label>
    <asp:DropDownList ID="cmbComuneNas" runat="server" CssClass="CssComuniNazioni" Font-Names="TIMES"
        Font-Size="8pt" Style="z-index: 110; left: 389px; position: absolute; top: 285px; right: 94px;"
        Width="156px" TabIndex="21" Enabled="False">
    </asp:DropDownList>
    <asp:Label ID="Label22" runat="server" CssClass="CssLabel" Font-Bold="False" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 111; left: 553px; position: absolute;
        top: 287px" Width="13px">Il</asp:Label>
    <asp:TextBox ID="txtDataNascita" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        MaxLength="10" Style="z-index: 112; left: 565px; position: absolute; top: 286px"
        TabIndex="22"></asp:TextBox>
    <asp:HiddenField ID="HSL" runat="server" Value="0" />
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
    &nbsp; 
    <img alt="" src="../NuoveImm/Img_Seleziona.png" onclick="ApriRicercaRU();" 
        style="position:absolute; top: 158px; left: 312px;cursor:pointer;"/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label14" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Maroon"
        Style="left: 238px; position: absolute; top: 262px" 
        Text="Codice F. ERRATO" Visible="False"></asp:Label>
    &nbsp;
    <asp:TextBox ID="txtInterno" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5" Style="z-index: 124;
        left: 66px; position: absolute; top: 111px" TabIndex="5" Width="30px"></asp:TextBox>
    <asp:TextBox ID="txtScala" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="5" Style="z-index: 124;
        left: 135px; position: absolute; top: 111px" TabIndex="6" Width="30px"></asp:TextBox>
</div>
<script type ="text/javascript">
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
        window.open('RicercaRU.aspx?ID=<%=IdDichiarazione %>', 'RicercaRU', 'height=450,top=0,left=0,width=670,scrollbars=no');
    }
    else {
        alert('Non Disponibile al momento!');
    }
    }

</script>
