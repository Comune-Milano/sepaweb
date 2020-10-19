<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnomCantUC.aspx.vb" Inherits="MANUTENZIONI_AnomCantUC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ANOMALIE</title>
</head>
<body bgcolor="#ffffff" text="#ede0c0">
    <form id="form1" runat="server">
    <div>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 16px; position: absolute; top: 16px">TIPOLOGIA</asp:Label>
        <asp:DropDownList ID="cmbtipoanomalie" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 16px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 40px" TabIndex="5" Width="208px">
        </asp:DropDownList>
        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 16px; position: absolute; top: 72px">VALORE</asp:Label>
        <asp:TextBox ID="TxtValore" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" MaxLength="8" Style="z-index: 102; left: 64px;
            position: absolute; top: 72px" Width="56px"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtValore"
            ErrorMessage="RegularExpressionValidator" Style="left: 144px; position: absolute;
            top: 72px" ValidationExpression="^100(\.0{0,2})? *%?$|^\d{1,2}(\.\d{1,2})? *%?$" Width="1px">!</asp:RegularExpressionValidator>
        <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 128px; position: absolute; top: 80px"
            Width="1px">%</asp:Label>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="left: 126px; position: absolute; top: 147px" ToolTip="Salva" />
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="left: 195px; position: absolute; top: 147px" ToolTip="Esci" />
    
    </div>
    </form>
</body>
</html>
