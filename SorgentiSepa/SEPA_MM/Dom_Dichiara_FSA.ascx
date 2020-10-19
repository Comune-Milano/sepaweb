<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Dichiara_FSA.ascx.vb" Inherits="Dom_Dichiara_FSA" %>
<DIV id="dic" style="BORDER-RIGHT: lightsteelblue 1px solid; BORDER-TOP: lightsteelblue 1px solid; LEFT: 10px; BORDER-LEFT: lightsteelblue 1px solid; WIDTH: 641px; BORDER-BOTTOM: lightsteelblue 1px solid; POSITION: absolute; TOP: 107px; HEIGHT: 315px; BACKGROUND-COLOR: #ffffff; z-index: 199;">
	<P>&nbsp;<asp:Image ID="alert1" runat="server" ImageUrl="~/IMG/Alert.gif" ToolTip="IL NUCLEO FAMILIARE E' COMPOSTO DA UN SOLO COMPONENTE!" Visible="False" style="z-index: 100; left: 434px; position: absolute; top: 105px" /></P>
	<asp:Label id="Label13" style="Z-INDEX: 101; LEFT: 20px; POSITION: absolute; TOP: 25px" Font-Size="8pt" Font-Names="Times New Roman" Height="1px" Width="266px" runat="server">Numero complessivo di persone che occupano l'alloggio</asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TD" Style="z-index: 128;
        left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
    &nbsp;&nbsp;
    <asp:TextBox ID="txtOccupanti" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="2" Style="z-index: 128; left: 353px; position: absolute;
        top: 22px" Width="29px">0</asp:TextBox>
    &nbsp;&nbsp;
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 47px"
        Width="330px">In caso di coabitazione indicare il numero di nuclei familiari coabitanti</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 70px"
        Width="285px">Numero componenti che hanno redditi da lavoro autonomo</asp:Label>
    &nbsp;
    <asp:TextBox ID="txtCoabitanti" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="2" Style="z-index: 128; left: 353px; position: absolute;
        top: 45px" TabIndex="1" Width="29px">0</asp:TextBox>
    <asp:TextBox ID="txtAutonomo" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="2" Style="z-index: 128; left: 353px; position: absolute;
        top: 68px" TabIndex="2" Width="29px">0</asp:TextBox>
    <asp:Label ID="Label3" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 93px"
        Width="329px">Numero componenti che hanno redditi da lavoro dipendente/assimilati</asp:Label>
    <asp:TextBox ID="txtDipendenti" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="2" Style="z-index: 128; left: 353px; position: absolute;
        top: 91px" TabIndex="3" Width="29px">0</asp:TextBox>
    <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 116px"
        Width="285px">Numero componenti che hanno redditi da pensione</asp:Label>
    <asp:TextBox ID="txtPensione" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="2" Style="z-index: 128; left: 353px; position: absolute;
        top: 114px" TabIndex="4" Width="29px">0</asp:TextBox>
    <asp:Label ID="Label2" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 139px"
        Width="285px">Numero componenti che hanno redditi da lavoro subordinato</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 224px"
        Width="170px">Contributo da corrispondere tramite</asp:Label>
    <asp:Label ID="Label9" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 353px; position: absolute; top: 224px"
        Width="65px">Intestato a:</asp:Label>
    <asp:Label ID="Label4" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 164px"
        Width="296px">Il nucleo famigliare si trva in particolare difficoltà socioeconomica, se già attestate da parte della Amministrazione Comunale:</asp:Label>
    <asp:TextBox ID="txtSubordinato" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="2" Style="z-index: 128; left: 353px; position: absolute;
        top: 137px" TabIndex="5" Width="29px">0</asp:TextBox>
    <asp:TextBox ID="txtIntestato" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="100" Style="z-index: 128; left: 417px; position: absolute;
        top: 222px" TabIndex="9" Width="209px"></asp:TextBox>
    <asp:Label ID="Label10" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 269px"
        Width="65px">Banca</asp:Label>
    <asp:Label ID="Label14" runat="server" Font-Italic="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 20px; position: absolute;
        top: 249px" Width="465px">Compilare i dati sottostanti solo se il contributo sarà corrisposto tramite Bonifico</asp:Label>
    <asp:TextBox ID="txtBanca" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="50"
        Style="z-index: 128; left: 87px; position: absolute; top: 267px" TabIndex="10"
        Width="226px"></asp:TextBox>
    <asp:Label ID="Label11" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 321px; position: absolute; top: 269px"
        Width="65px">Ubicazione</asp:Label>
    <asp:TextBox ID="txtUbicazione" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="50" Style="z-index: 128; left: 388px; position: absolute;
        top: 267px" TabIndex="11" Width="235px"></asp:TextBox>
    <asp:Label ID="Label12" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 20px; position: absolute; top: 290px"
        Width="33px">iban</asp:Label>
    <asp:TextBox ID="txtIban" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="27"
        Style="z-index: 128; left: 87px; position: absolute; top: 289px" TabIndex="12"
        Width="226px"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
    <br />
    <asp:CheckBox ID="ChDifficolta" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 349px; position: absolute; top: 162px" TabIndex="6" />
    <br />
    &nbsp;<br />
    <asp:TextBox ID="txtNoteIndigente" runat="server" Font-Names="Arial" Font-Size="8pt" Height="48px"
        MaxLength="4000" Style="z-index: 100; left: 416px; position: absolute; top: 168px"
        TextMode="MultiLine" Width="209px" TabIndex="7"></asp:TextBox>
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 385px; position: absolute; top: 166px" Text="NOTE"></asp:Label>
    &nbsp;<br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
    <asp:DropDownList ID="cmbContributo" runat="server" CssClass="CssPresenta" Font-Bold="False"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0" Height="20px"
        Style="z-index: 103; left: 191px; position: absolute; top: 222px" TabIndex="8"
        Width="156px">
        <asp:ListItem Value="RID">RIMESSA DIRETTA</asp:ListItem>
        <asp:ListItem Value="ASE">ASSEGNO CON VALUTA IN EURO</asp:ListItem>
        <asp:ListItem Value="ACE">BONIFICO</asp:ListItem>
    </asp:DropDownList>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp;&nbsp;<br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp;</DIV>

