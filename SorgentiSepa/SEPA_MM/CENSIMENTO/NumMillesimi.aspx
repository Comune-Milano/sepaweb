<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NumMillesimi.aspx.vb" Inherits="CENSIMENTO_NumMillesimi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Tipologie Tabelle Millesimali</title>
</head>
<body bgcolor="white">
    <form id="form1" runat="server">
    <div>
        &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp;
    
    </div>
        <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="z-index: 103; left: 524px; position: absolute; top: 113px" ToolTip="Salva" TabIndex="3" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:DropDownList ID="DrlTabMillesim" runat="server" Style="left: 8px; position: absolute;
            top: 40px" Width="612px" TabIndex="1">
        </asp:DropDownList>
        <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 16px" Width="120px">TABELLA ASSOCIATA</asp:Label>
        <asp:TextBox ID="TxtValore" runat="server" Style="left: 48px; position: absolute;
            top: 80px" Width="96px" MaxLength="5" TabIndex="2"></asp:TextBox>
        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 80px" Width="40px">Valore</asp:Label>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="z-index: 103; left: 588px; position: absolute; top: 113px" ToolTip="Esci" TabIndex="4" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
            ControlToValidate="TxtValore" ErrorMessage="!" Font-Bold="True" Height="1px"
            Style="left: 152px; position: absolute; top: 80px" ValidationExpression="\d+(\.\d\d\d\d)?(,\d\d\d\d)?(\.\d\d\d)?(,\d\d\d)?(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
            Width="1px"></asp:RegularExpressionValidator>
    </form>
</body>
</html>
