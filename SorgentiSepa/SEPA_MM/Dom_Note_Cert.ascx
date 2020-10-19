<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Note_Cert.ascx.vb" Inherits="Dom_Note_Cert" %>
<div id="not" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 170; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 107px; height: 315px;
    background-color: #ffffff">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" Height="99px"
        MaxLength="4000" Style="z-index: 100; left: 9px; position: absolute; top: 209px"
        TextMode="MultiLine" Width="617px" TabIndex="5"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 190px" Text="NOTE"></asp:Label>
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 16px" Text="CERTIFICAZIONI"></asp:Label>
    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 38px" Text="ANAGRAFICA"></asp:Label>
    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 68px" Text="DICHIARA"></asp:Label>
    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 98px" Text="LOCAZIONE"></asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 127px" Text="NUCLEO"></asp:Label>
    &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        Style="left: 10px; position: absolute; top: 155px" Text="SOTTOSCRITTORE"></asp:Label>
    &nbsp;
    <asp:DropDownList ID="cmbAnagrafica" runat="server" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 36px" Width="152px">
        <asp:ListItem Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbSottoscrittore" runat="server" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 153px"
        TabIndex="7" Width="152px">
        <asp:ListItem Value="A" Selected="True">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbDichiara" runat="server" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 66px"
        TabIndex="1" Width="152px">
        <asp:ListItem Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbLocazione" runat="server" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 96px"
        TabIndex="2" Width="152px">
        <asp:ListItem Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbNucleo" runat="server" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 113px; position: absolute; top: 125px"
        TabIndex="3" Width="152px">
        <asp:ListItem Selected="True" Value="A">AUTOCERTIFICAZIONE</asp:ListItem>
        <asp:ListItem Value="C">CERTIFICAZIONE</asp:ListItem>
    </asp:DropDownList>
    &nbsp; &nbsp;&nbsp; &nbsp;
    <asp:CheckBox ID="ChNonIdoneo" runat="server" Font-Names="arial" 
        Font-Size="8pt" Style="left: 359px;
        position: absolute; top: 126px; height: 19px;" 
        Text="EMESSO PROVVEDIMENTO NEGATIVO" TabIndex="4" />
    <asp:CheckBox ID="ChCartacea" runat="server" Font-Names="arial" Font-Size="8pt" Style="left: 359px;
        position: absolute; top: 152px" Text="DOCUMENTAZIONE CARTACEA NON PERVENUTA" />
</div>
