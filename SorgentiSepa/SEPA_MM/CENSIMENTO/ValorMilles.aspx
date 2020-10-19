<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ValorMilles.aspx.vb" Inherits="CENSIMENTO_ValorMilles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Valori Millesimali</title>
</head>
<body bgcolor="white">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 16px; position: absolute; top: 104px" Width="56px">VALORE</asp:Label>
        <asp:TextBox ID="TxtVal" runat="server" MaxLength="7" Style="left: 72px;
            position: absolute; top: 104px" Width="56px"></asp:TextBox>
        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 16px; position: absolute; top: 32px" Width="160px">Tipologia di dimensione</asp:Label>
        <asp:DropDownList ID="DrLDimens" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 16px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 56px" TabIndex="5" Width="576px">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp;
    
    </div>
        <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            
        Style="z-index: 103; left: 493px; position: absolute; top: 160px; height: 12px;" 
        ToolTip="Salva" />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="z-index: 103; left: 557px; position: absolute; top: 160px" ToolTip="Esci" />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 136px; position: absolute; top: 112px" Width="24px">mq</asp:Label>
    </form>
</body>
</html>
