<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Note_UT.ascx.vb" Inherits="Dic_Note_UT" %>
<div id="not" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 170; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 315px;
    background-color: #ffffff">
    &nbsp; &nbsp; &nbsp; 
    <asp:CheckBox ID="ChNatoEstero" runat="server" 
        style="position:absolute; top: 281px; left: 179px;" Font-Names="times" 
        Font-Size="8pt" ForeColor="#0000C0" 
        Text="Trattasi di Italiano nato all'estero" TabIndex="121" />
        <asp:CheckBox ID="ChCittadinanza" runat="server" 
        style="position:absolute; top: 281px; left: 378px;" Font-Names="times" 
        Font-Size="8pt" ForeColor="#0000C0" 
        Text="Nato all'estero ma in possesso di cittadinanza." TabIndex="122" />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt"
        MaxLength="4000" Style="z-index: 100; left: 9px; position: absolute; top: 13px; height: 128px; width: 591px;"
        TextMode="MultiLine" TabIndex="100"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label17" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 187px" Width="129px">Tipo Documento</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 210px" Width="129px">Doc. Identita N.</asp:Label>
    <asp:Label ID="Label9" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 269px; position: absolute;
        top: 210px" Width="36px">Data</asp:Label>
    <asp:Label ID="Label12" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 383px; position: absolute;
        top: 210px" Width="80px">Rilasc. Da</asp:Label>
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
    <asp:Label ID="Label16" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 10px; position: absolute;
        top: 283px; width: 98px;">Attività lavorativa</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 259px" Width="129px">Carta Soggiorno N.</asp:Label>
    <asp:Label ID="Label14" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 269px; position: absolute;
        top: 259px" Width="32px">Data</asp:Label>
    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Black" Height="18px" Style="z-index: 101; left: 9px;
        position: absolute; top: 159px" Width="290px">ESTREMI DOCUMENTO DI RICONOSCIMENTO</asp:Label>
        <asp:TextBox ID="txtCINum" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="25" Style="z-index: 124;
        left: 122px; position: absolute; top: 210px" TabIndex="111" Width="140px"></asp:TextBox>
    <asp:TextBox ID="txtCIRilascio" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 432px; position: absolute; top: 210px; width: 192px;" TabIndex="113"></asp:TextBox>
    <asp:TextBox ID="txtCIData" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 306px; position: absolute; top: 210px" TabIndex="112" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSData" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 306px; position: absolute; top: 234px" TabIndex="115" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSScade" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 431px; position: absolute; top: 234px; right: 136px;" TabIndex="116" 
        Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSRinnovo" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 552px; position: absolute; top: 234px" TabIndex="117" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtCSData" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 306px; position: absolute; top: 259px" TabIndex="119" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSNum" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="25" Style="z-index: 124;
        left: 122px; position: absolute; top: 234px" TabIndex="114" Width="140px"></asp:TextBox>
    <asp:TextBox ID="txtCSNum" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="25" Style="z-index: 124;
        left: 122px; position: absolute; top: 259px; right: 397px; bottom: 36px;" TabIndex="118" 
        Width="140px"></asp:TextBox>
    <asp:DropDownList ID="cmbTipoDocumento" runat="server" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 122px; position: absolute; top: 183px"
        TabIndex="110" Width="140px">
        <asp:ListItem Selected="True" Value="0">CARTA IDENTITA</asp:ListItem>
        <asp:ListItem Value="1">PASSAPORTO</asp:ListItem>
        <asp:ListItem Value="2">PATENTE DI GUIDA</asp:ListItem>
        <asp:ListItem Value="-1">--</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbLavorativa" runat="server" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 122px; position: absolute; top: 282px"
        TabIndex="120" Width="45px">
        <asp:ListItem Selected="True" Value="1">SI</asp:ListItem>
        <asp:ListItem Value="0">NO</asp:ListItem>
        <asp:ListItem Value="9">--</asp:ListItem>
    </asp:DropDownList>
    <asp:HyperLink ID="HyperLinkNote" runat="server" Font-Names="arial" Font-Size="8pt"
            ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QB" Style="z-index: 115;
            left: 620px; position: absolute; top: 2px" Target="_blank" Width="17px">Aiuto</asp:HyperLink>
</div>
