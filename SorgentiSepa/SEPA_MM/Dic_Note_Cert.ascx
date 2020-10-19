<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Note_Cert.ascx.vb" Inherits="Dic_Note_Cert" %>
<div id="not" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 170; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 315px;
    background-color: #ffffff">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" Height="99px"
        MaxLength="4000" Style="z-index: 100; left: 9px; position: absolute; top: 209px"
        TextMode="MultiLine" Width="617px"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 190px" Text="NOTE"></asp:Label>
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 16px" Text="CERTIFICAZIONI"></asp:Label>
    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 38px" Text="ANAGRAFICA"></asp:Label>
    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 68px" Text="COMPONENTI"></asp:Label>
    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 98px" Text="FAMIGLIA"></asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 127px" Text="PATR. MOBILIARE"></asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 11px; position: absolute; top: 157px" Text="PATR. IMMOBILIARE"></asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 292px; position: absolute; top: 37px" Text="REDDITO"></asp:Label>
    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 292px; position: absolute; top: 68px" Text="ALTRO REDDITO"></asp:Label>
    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 292px; position: absolute; top: 98px" Text="DETRAZIONI"></asp:Label>
    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 292px; position: absolute; top: 127px" Text="SOTTOSCRITTORE"></asp:Label>
    <asp:DropDownList ID="cmbAnagrafica" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 36px"
        TabIndex="9" Width="152px">
        <asp:ListItem Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbSottoscrittore" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 400px; position: absolute; top: 125px"
        TabIndex="9" Width="152px">
        <asp:ListItem Value="A" Selected="True">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbComponenti" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 66px"
        TabIndex="9" Width="152px">
        <asp:ListItem Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbFamiglia" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 96px"
        TabIndex="9" Width="152px">
        <asp:ListItem Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbMobiliare" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 125px"
        TabIndex="9" Width="152px">
        <asp:ListItem Selected="True" Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbImmobiliare" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 154px"
        TabIndex="9" Width="152px">
        <asp:ListItem Selected="True" Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbReddito" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 398px; position: absolute; top: 36px"
        TabIndex="9" Width="152px">
        <asp:ListItem Selected="True" Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbAltroReddito" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 398px; position: absolute; top: 66px"
        TabIndex="9" Width="152px">
        <asp:ListItem Selected="True" Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbDetrazioni" runat="server" AutoPostBack="True" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 399px; position: absolute; top: 96px"
        TabIndex="9" Width="152px">
        <asp:ListItem Selected="True" Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
</div>
