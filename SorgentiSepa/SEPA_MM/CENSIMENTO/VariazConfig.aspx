<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VariazConfig.aspx.vb" Inherits="CENSIMENTO_VariazConfig" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Tipo Variazione</title>
</head>
<body bgcolor="white">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 16px; position: absolute; top: 92px" Width="88px">Descrizione*</asp:Label>
        <asp:TextBox ID="TxtVariaz" runat="server" MaxLength="120" Style="left: 16px;
            position: absolute; top: 112px" Width="192px" Height="40px" TextMode="MultiLine" TabIndex="2"></asp:TextBox>
        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 16px; position: absolute; top: 32px" Width="136px">TIPOLOGIA VARIAZIONE*</asp:Label>
        <asp:DropDownList ID="DrlTipoVariaz" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 16px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 56px" TabIndex="1" Width="200px">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp;
    
    </div>
        <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            
        Style="z-index: 103; left: 232px; position: absolute; top: 133px; height: 12px;" 
        ToolTip="Salva" TabIndex="4" />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="z-index: 103; left: 232px; position: absolute; top: 56px" ToolTip="Esci" TabIndex="3" />
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 3; left: 6px; position: absolute; top: 161px"
            Text="Label" Visible="False" Width="212px"></asp:Label>
    </form>
</body>
</html>
