<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CarSingolaUC.aspx.vb" Inherits="MANUTENZIONI_CarSingolaUC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CARATTERISTICHE</title>
</head>
<body bgcolor="white" text="#ede0c0">
    <form id="form1" runat="server">
    <div>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 24px; position: absolute; top: 32px">TIPOLOGIA</asp:Label>
        <asp:DropDownList ID="cmbTipoDotaz" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 24px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 56px" TabIndex="5"
            Width="264px">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 24px; position: absolute; top: 88px">QUANTITA</asp:Label>
        <asp:TextBox ID="txtQuantita" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" MaxLength="8" Style="z-index: 102; left: 80px;
            position: absolute; top: 88px" Width="56px"></asp:TextBox>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQuantita"
            ErrorMessage="RegularExpressionValidator" Style="left: 144px; position: absolute;
            top: 88px" ValidationExpression="\d{0,2}" Width="1px">!</asp:RegularExpressionValidator>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="left: 181px; position: absolute; top: 147px" ToolTip="Salva" />
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="left: 251px; position: absolute; top: 147px" ToolTip="Esci" />
    
    </div>
    </form>
</body>
</html>



