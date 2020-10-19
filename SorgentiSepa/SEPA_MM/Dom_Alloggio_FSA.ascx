<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Alloggio_FSA.ascx.vb" Inherits="Dom_Alloggio_FSA" %>
<div id="abuno" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 107px; height: 315px; background-color: #ffffff; z-index: 196;">
    
        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<br />
    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label13" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 25px"
        Width="109px">Superficie Utile in mq</asp:Label>
    <asp:TextBox ID="txtSuperficie" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="2" Style="z-index: 128; left: 130px; position: absolute;
        top: 22px" TabIndex="9" Width="33px">0</asp:TextBox>
    <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 171px; position: absolute; top: 25px"
        Width="38px">Foglio</asp:Label>
    <asp:TextBox ID="txtFoglio" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="50"
        Style="z-index: 128; left: 210px; position: absolute; top: 22px" TabIndex="9"
        Width="80px">0</asp:TextBox>
    <asp:Label ID="Label2" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 293px; position: absolute; top: 25px"
        Width="48px">Particella</asp:Label>
    <asp:TextBox ID="txtParticella" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="50" Style="z-index: 128; left: 343px; position: absolute;
        top: 22px" TabIndex="9" Width="80px">0</asp:TextBox>
    <asp:CheckBox ID="ChDegrado" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 66px" TabIndex="2" />
    <asp:CheckBox ID="ChPotabile" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 88px" TabIndex="2" />
    <asp:CheckBox ID="ChCucina" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 110px" TabIndex="2" />
    <asp:CheckBox ID="ChImproprio" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 132px" TabIndex="2" />
    <asp:CheckBox ID="ChServizi" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 154px" TabIndex="2" />
    <asp:CheckBox ID="ChRiscaldamento" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 176px" TabIndex="2" />
    <asp:CheckBox ID="ChBox" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 230px" TabIndex="2" />
    <asp:CheckBox ID="ChAuto" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 251px" TabIndex="2" />
    <asp:CheckBox ID="ChIndivisa" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 272px" TabIndex="2" />
    <asp:CheckBox ID="ChReq" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Style="left: 413px; position: absolute; top: 292px" TabIndex="2" />
    <asp:Label ID="Label17" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 295px"
        Width="400px">Sussistenza di uno dei req. B,C,D,E,F,G dell'art.3 c.2 All.1 G.R.N. 5075  10/07/2007</asp:Label>
    <asp:Label ID="Label16" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 275px"
        Width="389px">Assegnazione in godimento di unita imm. di cooperativa edilizia a proprietà indivisa</asp:Label>
    <asp:Label ID="Label15" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 254px"
        Width="355px">L'alloggio è dotato di posto macchina</asp:Label>
    <asp:Label ID="Label14" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 233px"
        Width="355px">L'alloggio è dotato di box</asp:Label>
    <asp:Label ID="Label11" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 179px"
        Width="355px">L'alloggio dispone di adeguati impianti per il riscaldamento</asp:Label>
    <asp:Label ID="Label12" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 200px"
        Width="383px">Numero locali (esclusi locale cucina, servizi, soffitte, cantine e ripostiglio; soggiorno con angolo cottura va indicato come un solo locale)</asp:Label>
    <asp:Label ID="Label10" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 157px"
        Width="355px">L'alloggio dispone di servizi igienici propri o incorporati nell'alloggio</asp:Label>
    <asp:Label ID="Label9" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 135px"
        Width="302px">L'alloggio è improprio (soffitto, seminterrato, rustico, box)</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 113px"
        Width="265px">L'alloggio dispone di locale cucina </asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 91px"
        Width="265px">L'alloggio dispone di acqua potabile</asp:Label>
    <asp:Label ID="Label3" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 426px; position: absolute; top: 25px"
        Width="58px">Subalterno</asp:Label>
    <asp:TextBox ID="txtSub" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="50"
        Style="z-index: 128; left: 479px; position: absolute; top: 22px" TabIndex="9"
        Width="80px">0</asp:TextBox>
    <asp:Label ID="Label4" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 47px"
        Width="94px">Anno Costruzione</asp:Label>
    <asp:TextBox ID="txtAnno" runat="server" Columns="7" CssClass="CssMaiuscolo" Font-Bold="True"
        Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="4"
        Style="z-index: 128; left: 130px; position: absolute; top: 44px" TabIndex="9"
        Width="33px">0</asp:TextBox>
    <asp:TextBox ID="txtNumLocali" runat="server" Columns="7" CssClass="CssMaiuscolo"
        Font-Bold="True" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
        Height="20px" MaxLength="2" Style="z-index: 128; left: 417px; position: absolute;
        top: 200px" TabIndex="9" Width="29px">0</asp:TextBox>
    <asp:Label ID="Label5" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 172px; position: absolute; top: 47px"
        Width="92px">Categoria Catastale</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="1px" Style="z-index: 101; left: 20px; position: absolute; top: 69px"
        Width="389px">L'alloggio è in condizioni di degrado tali da pregiudicare l'incolumità degli occupanti </asp:Label>
    <asp:DropDownList ID="cmbCat" runat="server" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 136; left: 267px; position: absolute; top: 45px"
        TabIndex="7" Width="81px">
        <asp:ListItem>A1</asp:ListItem>
        <asp:ListItem>A2</asp:ListItem>
        <asp:ListItem>A3</asp:ListItem>
        <asp:ListItem>A4</asp:ListItem>
        <asp:ListItem>A5</asp:ListItem>
        <asp:ListItem>A6</asp:ListItem>
        <asp:ListItem>A7</asp:ListItem>
        <asp:ListItem>A8</asp:ListItem>
        <asp:ListItem>A9</asp:ListItem>
        <asp:ListItem>A10</asp:ListItem>
        <asp:ListItem>A11</asp:ListItem>
        <asp:ListItem>C1</asp:ListItem>
        <asp:ListItem>C2</asp:ListItem>
        <asp:ListItem>C3</asp:ListItem>
        <asp:ListItem>C4</asp:ListItem>
        <asp:ListItem>C5</asp:ListItem>
        <asp:ListItem>C6</asp:ListItem>
        <asp:ListItem>E1</asp:ListItem>
        <asp:ListItem>BBBB</asp:ListItem>
    </asp:DropDownList>
    
    </div>
