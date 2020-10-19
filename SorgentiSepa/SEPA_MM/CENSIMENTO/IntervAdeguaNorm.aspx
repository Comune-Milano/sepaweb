<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IntervAdeguaNorm.aspx.vb" Inherits="CENSIMENTO_IntervAdeguaNorm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Adeguamento</title>
</head>
<body bgcolor="white">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 72px" Width="88px">Descrizione</asp:Label>
        <asp:TextBox ID="TxtDescr" runat="server" MaxLength="150" Style="left: 24px;
            position: absolute; top: 88px" Width="584px" TextMode="MultiLine" Height="80px" TabIndex="2"></asp:TextBox>
        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 16px" Width="136px">Tipologia Adeguamento</asp:Label>
        <asp:DropDownList ID="DrlAdeg" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 24px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 40px" TabIndex="1" Width="592px">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp;
    
    </div>
        <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            
        Style="z-index: 103; left: 516px; position: absolute; top: 184px; " 
        ToolTip="Salva" TabIndex="3" />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="z-index: 103; left: 580px; position: absolute; top: 184px" ToolTip="Esci" TabIndex="4" />
    <p>
        &nbsp;</p>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
        Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="left: 27px; position: absolute; top: 180px" Text="Label"
                Visible="False" Width="448px"></asp:Label>
    </form>
</body>
</html>
