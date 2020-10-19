<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Contratto_FSA.ascx.vb" Inherits="Dom_Contratto_FSA" %>
<DIV id="fam" style="BORDER-RIGHT: lightsteelblue 1px solid; BORDER-TOP: lightsteelblue 1px solid; LEFT: 10px; BORDER-LEFT: lightsteelblue 1px solid; WIDTH: 641px; BORDER-BOTTOM: lightsteelblue 1px solid; POSITION: absolute; TOP: 107px; HEIGHT: 315px; BACKGROUND-COLOR: #ffffff; z-index: 199;">
	<P>&nbsp;<asp:Image ID="alert1" runat="server" ImageUrl="~/IMG/Alert.gif" ToolTip="IL NUCLEO FAMILIARE E' COMPOSTO DA UN SOLO COMPONENTE!" Visible="False" style="z-index: 100; left: 4px; position: absolute; top: 262px" /></P>
	<asp:Label id="Label13" style="Z-INDEX: 101; LEFT: 20px; POSITION: absolute; TOP: 25px" Font-Size="8pt" Font-Names="Times New Roman" Height="18px" Width="85px" runat="server">Data Decorrenza</asp:Label>
    <asp:Label ID="Label4" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 48px"
        Width="83px">Stato Contratto</asp:Label>
    <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 181px; position: absolute; top: 25px"
        Width="72px">Data Scadenza</asp:Label>
    <asp:Label ID="Label3" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 327px; position: absolute; top: 25px"
        Width="68px">Data Disdetta</asp:Label>
    &nbsp; &nbsp;&nbsp;
    <asp:DropDownList id="cmbStatoC" style="Z-INDEX: 103; LEFT: 109px; POSITION: absolute; TOP: 46px" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" Width="145px" TabIndex="3">
        <asp:ListItem Selected="True" Value="REG">REGISTRATO</asp:ListItem>
        <asp:ListItem Value="COR">IN CORSO DI REGISTRAZ.</asp:ListItem>
        <asp:ListItem Value="NOR">NON REGISTRATO</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="Label8" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 118px"
        Width="83px">Tipo Contratto</asp:Label>
    <asp:DropDownList id="cmbTipoContratto" style="Z-INDEX: 103; LEFT: 109px; POSITION: absolute; TOP: 116px" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" Width="238px" TabIndex="7">
        <asp:ListItem Value="1">STIPULATO AI SENSI DELLA L. 431 / 98</asp:ListItem>
        <asp:ListItem Value="2">PATTI IN DEROGA</asp:ListItem>
        <asp:ListItem Value="3">EQUO CANONE</asp:ListItem>
        <asp:ListItem Value="4">NORMATIVA REGIONALE ERP</asp:ListItem>
        <asp:ListItem Value="5">ASSEGNAZIONE IN GODIMENTO</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="Label9" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 21px; position: absolute; top: 141px"
        Width="66px">Figura Propr.</asp:Label>
    <asp:DropDownList id="cmbTipoFigura" style="Z-INDEX: 103; LEFT: 109px; POSITION: absolute; TOP: 139px" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" Width="238px" TabIndex="8">
        <asp:ListItem Value="1">PERSONA FISICA</asp:ListItem>
        <asp:ListItem Value="2">COMUNE O GESTORE</asp:ListItem>
        <asp:ListItem Value="3">ALTRO ENTE</asp:ListItem>
        <asp:ListItem Value="4">SOCIETA</asp:ListItem>
    </asp:DropDownList>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TD" Style="z-index: 128;
        left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
    &nbsp;&nbsp;
    <asp:TextBox ID="txtDataDecorrenza" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="10" Style="z-index: 128; left: 109px; position: absolute;
        top: 23px"></asp:TextBox>
    <asp:TextBox ID="txtDataScadenza" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="10" Style="z-index: 128; left: 255px; position: absolute;
        top: 23px" TabIndex="1"></asp:TextBox>
    <asp:TextBox ID="txtDataDisdetta" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="10" Style="z-index: 128; left: 397px; position: absolute;
        top: 23px" TabIndex="2"></asp:TextBox>
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 71px"
        Width="85px">Data Stipula</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 21px; position: absolute; top: 94px"
        Width="72px">Data Registraz.</asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 178px; position: absolute; top: 94px"
        Width="72px">Estremi Reg.</asp:Label>
    <asp:TextBox ID="txtDataStipula" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="10" Style="z-index: 128; left: 109px; position: absolute;
        top: 69px" TabIndex="4"></asp:TextBox>
    <asp:Label ID="Label11" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 165px"
        Width="85px">Affitto Annuo E.</asp:Label>
    <asp:Label ID="Label14" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 155px; position: absolute; top: 165px"
        Width="18px">,00</asp:Label>
    <asp:Label ID="Label15" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 154px; position: absolute; top: 189px"
        Width="18px">,00</asp:Label>
    <asp:Label ID="Label16" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 155px; position: absolute; top: 213px"
        Width="18px">,00</asp:Label>
    <asp:TextBox ID="txtAffitto" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="10"
        Style="z-index: 128; left: 109px; position: absolute; top: 163px; text-align: right"
        TabIndex="9" Width="42px"></asp:TextBox>
    <asp:Label ID="Label12" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 189px"
        Width="74px">Spese Cond. E.</asp:Label>
    <asp:TextBox ID="txtSpese" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="10"
        Style="z-index: 128; left: 109px; position: absolute; top: 187px; text-align: right"
        TabIndex="10" Width="42px"></asp:TextBox>
    <asp:Label ID="Label10" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 21px; position: absolute; top: 213px"
        Width="68px">Spese Risc. E.</asp:Label>
    <asp:TextBox ID="txtRiscaldamento" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="10" Style="z-index: 128; left: 109px; position: absolute;
        top: 211px; text-align: right" TabIndex="11" Width="42px"></asp:TextBox>
    <asp:Label ID="Label17" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 238px"
        Width="90px">Contratti Idonei</asp:Label>
    <asp:TextBox ID="txtIdonei" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="10"
        Style="z-index: 128; left: 109px; position: absolute; top: 236px; text-align: right"
        TabIndex="12" Width="42px"></asp:TextBox>
    <asp:Label ID="Label18" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 21px; position: absolute; top: 263px"
        Width="72px">Mesi Contratto</asp:Label>
    <asp:TextBox ID="TxtMesi" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="10"
        Style="z-index: 128; left: 109px; position: absolute; top: 261px; text-align: right"
        TabIndex="13" Width="42px"></asp:TextBox>
    <asp:TextBox ID="txtDataReg" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="10"
        Style="z-index: 128; left: 108px; position: absolute; top: 92px" TabIndex="5"></asp:TextBox>
    <asp:TextBox ID="txtEstremi" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="200"
        Style="z-index: 128; left: 247px; position: absolute; top: 92px" TabIndex="6"
        Width="377px"></asp:TextBox>
    <br />
    <br />
    <br />
    &nbsp;<br />
    <br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp;<br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp;</DIV>

