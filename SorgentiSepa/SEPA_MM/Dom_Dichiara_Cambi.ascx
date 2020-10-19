<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Dichiara_Cambi.ascx.vb" Inherits="Dom_Dichiara_Cambi" %>
<DIV id="dic" style="BORDER-RIGHT: lightsteelblue 1px solid; BORDER-TOP: lightsteelblue 1px solid; LEFT: 10px; BORDER-LEFT: lightsteelblue 1px solid; WIDTH: 641px; BORDER-BOTTOM: lightsteelblue 1px solid; POSITION: absolute; TOP: 107px; HEIGHT: 315px; BACKGROUND-COLOR: #ffffff; z-index: 199;">
	<P>&nbsp;</P>
	<asp:Label id="Label13" style="Z-INDEX: 101; LEFT: 9px; POSITION: absolute; TOP: 38px" Font-Size="8pt" Font-Names="Times New Roman" Height="18px" Width="129px" runat="server" ForeColor="#0000C0">Di presentare domanda per</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 210px" Width="129px">Doc. Identita N.</asp:Label>
    <asp:Label ID="Label9" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 269px; position: absolute;
        top: 210px" Width="36px">Data</asp:Label>
    <asp:Label ID="Label12" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 383px; position: absolute;
        top: 210px" Width="80px">Rilasciata Da</asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 234px" Width="129px">Permesso Soggiorno N.</asp:Label>
    <asp:Label ID="Label10" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 269px; position: absolute;
        top: 234px" Width="34px">Data</asp:Label>
    <asp:Label ID="Label11" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 384px; position: absolute;
        top: 234px" Width="57px">Scadenza</asp:Label>
    <asp:Label ID="Label15" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 509px; position: absolute;
        top: 234px" Width="81px">Rinnovo</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 259px" Width="129px">Carta Soggiorno N.</asp:Label>
    <asp:Label ID="Label14" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 269px; position: absolute;
        top: 259px" Width="32px">Data</asp:Label>
    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Black" Height="18px" Style="z-index: 101; left: 9px;
        position: absolute; top: 182px" Width="290px">ESTREMI DOCUMENTO DI RICONOSCIMENTO</asp:Label>
    <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 8px; position: absolute; top: 65px"
        Width="491px" ForeColor="#0000C0" Visible="False">Il nucleo familiare richiedente ha effettuato gli adempimenti connessi all'anagrafe utenza dell'anno 2007 ?</asp:Label>
    &nbsp;&nbsp;
    <asp:DropDownList id="cmbPresentaD" style="Z-INDEX: 103; LEFT: 145px; POSITION: absolute; TOP: 35px" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" Width="480px">
    </asp:DropDownList>
    <asp:DropDownList id="cmbFattaAU" 
        style="Z-INDEX: 103; LEFT: 513px; POSITION: absolute; TOP: 63px" 
        ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" 
        CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" 
        Width="112px" TabIndex="1" Visible="False">
        <asp:ListItem Value="1">SI</asp:ListItem>
        <asp:ListItem Value="0">NO</asp:ListItem>
    </asp:DropDownList>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TD" Style="z-index: 128;
        left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
    &nbsp;&nbsp;&nbsp;&nbsp;<br />
    <br />
    <br />
    <br />
    <asp:Label ID="lblERP" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
        ForeColor="#0000C0" Style="left: 10px; cursor: pointer; position: absolute; top: 105px"
        Text="Clicca qui per visualizzare la domanda ERP"></asp:Label>
    <br />
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
    <asp:TextBox ID="txtCINum" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="25" Style="z-index: 124;
        left: 142px; position: absolute; top: 210px" TabIndex="5" Width="120px"></asp:TextBox>
    <asp:TextBox ID="txtCIRilascio" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 467px; position: absolute; top: 210px" TabIndex="7" Width="157px"></asp:TextBox>
    <asp:TextBox ID="txtCIData" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 306px; position: absolute; top: 210px" TabIndex="6" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSData" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 306px; position: absolute; top: 234px" TabIndex="9" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSScade" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 431px; position: absolute; top: 234px" TabIndex="10" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSRinnovo" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 552px; position: absolute; top: 234px" TabIndex="11" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtCSData" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 306px; position: absolute; top: 259px" TabIndex="13" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSNum" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="25" Style="z-index: 124;
        left: 142px; position: absolute; top: 234px" TabIndex="8" Width="120px"></asp:TextBox>
    <asp:TextBox ID="txtCSNum" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="25" Style="z-index: 124;
        left: 142px; position: absolute; top: 259px" TabIndex="12" Width="120px"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp;</DIV>
